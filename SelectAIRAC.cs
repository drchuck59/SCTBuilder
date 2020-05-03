using System;
using System.Net;
using System.IO;
using System.IO.Compression;
using System.Windows.Forms;
using System.Globalization;

namespace SCTBuilder
{
    public partial class SelectAIRAC : Form
    {
        readonly string cr = Environment.NewLine;
        public SelectAIRAC()
        {
            InitializeComponent();
            InitializeComboboxes();
        }

        private void InitializeComboboxes()
        {
            // YearComboBox
            InitializeYearComboBox();
            // CycleComboBox
            InitializeCycleComboBox();
            // Cycle Caption
            InitializeConfirmationLabel();
        }

        private void InitializeYearComboBox()
        {
            YearComboBox.Items.Clear();
            for (int i = 2017; i <= DateTime.Now.Year; i++)
            {
                YearComboBox.Items.Add(i);
            }
            DateTime currentCycleDate = CycleInfo.CycleDateFromAIRAC(CycleInfo.CurrentAIRAC).Date;
            DateTime nextCycleDate = currentCycleDate.AddDays(28).Date;
            if (nextCycleDate.Year > DateTime.Now.Year) YearComboBox.Items.Add(nextCycleDate.Year.ToString());
            Console.WriteLine(YearComboBox.Items.Count);
            foreach (var item in YearComboBox.Items) Console.WriteLine(item.ToString());
            YearComboBox.SelectedItem = currentCycleDate.Year;
        }

        private void InitializeCycleComboBox()
        {
            // Three conditions: previous year, current year, future year
            DateTime currentCycleDate = CycleInfo.CycleDateFromAIRAC(CycleInfo.CurrentAIRAC).Date;
            DateTime nextCycleDate = currentCycleDate.AddDays(28).Date;
            int curYear = DateTime.Now.Year;
            int selectedYear = Convert.ToInt32(YearComboBox.SelectedItem);
            int lastCycle; int firstCycle = 11;     //  2017 first archive is cycle 11
            CycleComboBox.Items.Clear();
            if (selectedYear < curYear)
            {
                lastCycle = Convert.ToInt32(CycleInfo.AIRACfromDate(DateTime.Parse("12/31/" + selectedYear.ToString())).ToString(CultureInfo.InvariantCulture).Substring(2,2));
                if (selectedYear != 2017) firstCycle = 1;
                for (int i = firstCycle; i < lastCycle; i++)
                    CycleComboBox.Items.Add(i);
                CycleComboBox.SelectedIndex = 0;
            }
            if (selectedYear == curYear)
            {
                lastCycle = Convert.ToInt32(CycleInfo.AIRACfromDate(DateTime.Now.Date).ToString(CultureInfo.InvariantCulture).Substring(2,2));
                for (int i = 1; i <= lastCycle; i++)
                    CycleComboBox.Items.Add(i);
                CycleComboBox.SelectedItem = lastCycle;
                if (nextCycleDate.Year == DateTime.Now.Year) CycleComboBox.Items.Add(lastCycle + 1);
            }
            if (selectedYear > curYear)
            {
                CycleComboBox.Items.Add(1);
                CycleComboBox.SelectedIndex = 0;
            }
        }

        private void InitializeConfirmationLabel()
        {
            int tempAIRAC = Convert.ToInt32(YearComboBox.Text.Substring(2, 2) + Convert.ToInt32(CycleComboBox.Text).ToString("D2"));
            Console.WriteLine("Temp AIRAC: " + tempAIRAC);
            DateTime tempStart = CycleInfo.CycleDateFromAIRAC(tempAIRAC).Date;
            DateTime tempEnd = CycleInfo.CycleDateFromAIRAC(tempAIRAC).AddDays(28).Date;
            string cr = Environment.NewLine;
            ConfirmationLabel.Text = "AIRAC: " + tempAIRAC.ToString() + cr +
                "Cycle Dates: " + cr + tempStart.ToShortDateString() + " - " + tempEnd.Date.ToShortDateString();
            if (tempEnd < DateTime.Now.Date)
                ConfirmationLabel.Text += cr + "*** Outdated Cycle ***";
        }

        private void YearComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitializeCycleComboBox();
            InitializeConfirmationLabel();
        }

        private void CycleComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitializeConfirmationLabel();
        }

        private bool downloadComplete = false;

        private void OKButton_Click(object sender, EventArgs e)
        {
            MyButtonCancel.Enabled = false;
            MyButtonCancel.Visible = false;
            int tempAIRAC = Convert.ToInt32(YearComboBox.Text.Substring(2, 2) + Convert.ToInt32(CycleComboBox.Text).ToString("D2"));
            CycleInfo.CycleDateFromAIRAC(tempAIRAC, true);
            if (!CleanDataFolder()) Close();        // Must have a clean data folder to place data
            // Set up values for the download;
            string newCycleDate = CycleInfo.CycleStart.Date.ToString("yyyy'-'MM'-'dd");
            DirectoryInfo di;
            di = Directory.CreateDirectory(FolderMgt.DataFolder + "\\28DaySubscription_Effective_" + newCycleDate + "\\");
            string extractPath = di.FullName;
            string URL = "https://nfdc.faa.gov/webContent/28DaySub/28DaySubscription_Effective_" + newCycleDate + ".zip";
            string downloadsPath = KnownFolders.GetPath(KnownFolder.Downloads) + "\\28DaySubscription_Effective_" + newCycleDate + ".zip";
            //

            // If the zipfile is already downloaded, delete it (safer as it may be corrupted)
            if (File.Exists(downloadsPath)) File.Delete(downloadsPath);
            OKButton.Text = "Downloading FAA files";
            OKButton.Enabled = false;
            // Start the download
            WebClient wc = new WebClient();
            wc.DownloadProgressChanged += (s, f) =>
            {
                progressBar1.Value = f.ProgressPercentage;
            };
            wc.DownloadFileCompleted += (s, g) =>
            {
                // Unless I messed up, there cannot be a data subdirectory by this name, so create it
                OKButton.Text = "Extracting files...";
                OKButton.Refresh();
                ZipFile.ExtractToDirectory(downloadsPath, extractPath);
                OKButton.Enabled = false;
                OKButton.Visible = false;
                ContinueButton.Enabled = true;
                ContinueButton.Visible = true;
                downloadComplete = true;
            };
            try
            {
                wc.DownloadFileAsync(new System.Uri(URL), downloadsPath);
                while (!downloadComplete)
                {
                    Application.DoEvents();
                }
                downloadComplete = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private bool CleanDataFolder()
        {
            // See if the user already has the same data - ask if user wants to rewrite it
            // If old data, confirm the user wants to overwrite it
            // if NO data, do the install without query
            // Start by identifying the datafolder if needed
            bool Continue = false; string Msg; string filter = "*28DaySubscription*";
            bool result = false; int foundFilterDir = 0;

            // Verify the data folder holding the extraction data
            if (!Directory.Exists(FolderMgt.DataFolder)) VerifyExtractPath();
            if (FolderMgt.DataFolder.Length == 0) return result;

            // Search the datafolder for duplicate data subfolders and remove them
            string newCycleDate = CycleInfo.CycleStart.Date.ToString("yyyy'-'MM'-'dd");
            string[] dirs = Directory.GetDirectories(@FolderMgt.DataFolder, filter, SearchOption.TopDirectoryOnly);
            foreach (string dir in dirs)
            {
                if (dir.IndexOf(newCycleDate) != -1)
                {
                    Msg = "You already have the current dataset" + cr +
                        "Cycle date: " + newCycleDate + cr +
                        "Are you sure that you want to reinstall it?";
                    DialogResult dialogResult = SCTcommon.SendMessage(Msg, MessageBoxIcon.Question, MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                        Continue = true;
                    else
                        return false;
                }
                else
                {
                    if ((dir.IndexOf(filter) != 0) && !Continue)
                    {
                        Msg = "You have more FAA AIRAC folders in the data repository." + cr +
                            "This program expects only one FAA folder '28DaySubscription_<date>'." + cr +
                            "Click OK to remove ALL your existing FAA AIRAC folder(s), or BEFORE clicking OK" + cr +
                            "move your old Subscription folders outside of your main data folder" + cr +
                            "(" + FolderMgt.DataFolder + ")." + cr + cr +
                            "Click Cancel to abort the FAA text file download and data update.";
                        DialogResult dialogResult = SCTcommon.SendMessage(Msg, MessageBoxIcon.Question, MessageBoxButtons.OKCancel);
                        if (dialogResult == DialogResult.OK)
                            Continue = true;
                        else
                            return false;
                    }
                }
                if (Continue)
                {
                    if (Directory.Exists(dir))
                    {
                        Directory.Delete(path: dir, recursive: true);
                        Continue = false;               // Reset the flag or you might delete ALL the dirs!
                    }
                }

            }
            // Need to verify that there are no subscription folders in the data folder
            foreach (string dir in dirs)
            {
                if (dir.IndexOf(filter) != 0) foundFilterDir++;
            }
            result = foundFilterDir == 0;
            return result;        // Acceptable values are 0 (clean) or 1 (kept old data)
        }

        private string VerifyExtractPath()
        {
            // returns the path to extract the data (the data folder)
            string Msg = "Select new / Verify current data folder to contain FAA data subfolder";
            string extractPath = SCTcommon.GetFolderPath(FolderMgt.DataFolder, Msg);
            if (extractPath.Length == 0)
            {
                Msg = "No folder selected to save FAA text files. Update aborted.";
                SCTcommon.SendMessage(Msg, MessageBoxIcon.Warning);
            }
            else
            {
                FolderMgt.DataFolder = extractPath;
            }
            return extractPath;
        }

        private void ContinueButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void MyButtonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
