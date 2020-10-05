using System;
using System.IO;
using System.ComponentModel;
using System.Windows.Forms;
using Org.BouncyCastle.Asn1.Crmf;
using System.Data;
using System.Drawing;
using System.Linq.Expressions;
using System.Diagnostics;

namespace SCTBuilder
{
    public partial class POFmanager : Form
    {
        public string cr = Environment.NewLine;

        public POFmanager()
        {
            InitializeComponent();
        }

        private void OpenPOFButton_Click(object sender, EventArgs e)
        {
            string filePath = FolderMgt.DataFolder;
            if (filePath.Length == 0) filePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = filePath;
                openFileDialog.Title = "Select your POF file";
                openFileDialog.Filter = "POF files (*.pof)|*.pof|txt files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.RestoreDirectory = false;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    filePath = openFileDialog.FileName;
                    if (ReadPOF.ReadPOFfile(filePath) > 0)
                        LoadPOFdatagridview();
                }
                POFpathTextBox.Text = filePath;
            }
        }

        private void POFpathTextBox_Validated(object sender, EventArgs e)
        {
            if (POFpathTextBox.TextLength > 0)
                LoadFromTextBox(POFpathTextBox);
        }

        private void LoadFromTextBox(TextBox tb)
        {
            if (File.Exists(tb.Text))
            {
                if (ReadPOF.ReadPOFfile(tb.Text) > 0) LoadPOFdatagridview();
            }
            else
            {
                string msg = "File not found - consider using file dialog button";
                SCTcommon.SendMessage(msg);
            }
        }

        private void LoadPOFdatagridview()
        {
            DataGridView dgvPOF = POFDataGridView;
            DataView dvPOF = new DataView(Form1.POFdata);
            dgvPOF.DataSource = dvPOF;
            dgvPOF.Columns[0].HeaderText = "Sector";
            dgvPOF.Columns[1].HeaderText = "Callsign";
            dgvPOF.Columns[2].HeaderText = "Frequency";
            dgvPOF.Columns[3].HeaderText = "Sector ID";
            dgvPOF.Columns[4].HeaderText = "Posn Sym";
            dgvPOF.Columns[5].HeaderText = "Prefix";
            dgvPOF.Columns[6].HeaderText = "Suffix";
            dgvPOF.Columns[7].HeaderText = "VSCS Ln 1";
            dgvPOF.Columns[8].HeaderText = "VSCS Ln 2";
            dgvPOF.Columns[9].HeaderText = "Low Squawk";
            dgvPOF.Columns[10].HeaderText = "Hi Squawk";
            dgvPOF.Columns["Sequence"].Visible = false;
            dgvPOF.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dgvPOF.RowHeadersDefaultCellStyle.SelectionBackColor = Color.Empty;
            dgvPOF.Sort(dgvPOF.Columns["Sequence"],ListSortDirection.Ascending);
            dgvPOF.Refresh();
        }

        private void POFmanager_Load(object sender, EventArgs e)
        {
            string msg; 
            string[] files = Directory.GetFiles(FolderMgt.DataFolder, "*.pof");
            if (files.Length > 0)
            {
                foreach (string file in files)
                {
                    msg = "Found " + file + ". Use this POF file?";
                    if (SCTcommon.SendMessage(msg, MessageBoxIcon.Question, MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        POFpathTextBox.Text = file;
                        POFpathTextBox.Refresh();
                        break;
                    }
                }
            }
            if (POFpathTextBox.TextLength > 0)
                LoadFromTextBox(POFpathTextBox);
        }

        private int WritePOF(bool AsESE = false)
        {
            DataTable POF = Form1.POFdata;
            string line = string.Empty;
            int maxCol = 11;
            string extension = ".pof";
            if (AsESE)
            {
                maxCol += 8;
                extension = ".ese";
            }
            int result = 0;
            // Open and write the lines
            string filename = FolderMgt.OutputFolder + "\\" + InfoSection.SponsorARTCC + "_POF_" + CycleInfo.AIRAC + extension;
            using (StreamWriter sw = new StreamWriter(filename))
            {
                sw.WriteLine(CycleHeader());
                if (AsESE) sw.WriteLine("[POSITIONS]");
                // Loop the rows
                foreach (DataRow POFentry in POF.AsEnumerable())
                {
                    // Loop the columns
                    for (int i = 0; i < maxCol; i++)
                    {
                        if (POFentry[i].ToString().Length != 0)
                            line += POFentry[i].ToString();
                        line += ":";
                    }
                    // Remove the last colons
                    while (line.Substring(line.Length-1,1) == ":")
                        line = line.Substring(0, line.Length - 1);
                    sw.WriteLine(line);
                    result++;
                    line = string.Empty;
                }
            }
            string msg = result + " lines written to " + filename;
            SCTcommon.SendMessage(msg, MessageBoxIcon.Information);
            return result;
        }

        private void WriteSCTButton_Click(object sender, EventArgs e)
        {
            WritePOF(false);
        }

        private void WriteESEButton_Click(object sender, EventArgs e)
        {
            WritePOF(true);
        }

        private string CycleHeader()
        {
            return
                "; ================================================================" + cr +
                "; AIRAC CYCLE: " + CycleInfo.AIRAC + cr +
                "; Cycle: " + CycleInfo.CycleStart + " to " + CycleInfo.CycleEnd + cr +
                "; ================================================================" + cr;
        }

        private void IdentifierTextBox_TextChanged(object sender, EventArgs e)
        {
            {
                // Search the tables for fixes and return the coordinates, 
                // then place the coordinates into the list box.
                //
                if (IdentifierTextBox.TextLength != 0)
                {
                    // First, be sure there is data in the database!
                    DataTable dtVOR = Form1.VOR;
                    DataTable dtNDB = Form1.NDB;
                    DataTable dtFIX = Form1.FIX;
                    DataTable dtAPT = Form1.APT;
                    if (dtFIX.Rows.Count != 0)
                    {
                        // Load the gridview - can be sorted later.  Future: add [Selected]
                        string filter = "[FacilityID] LIKE '" + IdentifierTextBox.Text + "*" + "'";
                        DataView dvVOR = new DataView(dtVOR, filter, "FacilityID", DataViewRowState.CurrentRows);
                        DataView dvNDB = new DataView(dtNDB, filter, "FacilityID", DataViewRowState.CurrentRows);
                        DataView dvFIX = new DataView(dtFIX, filter, "FacilityID", DataViewRowState.CurrentRows);
                        DataView dvAPT = new DataView(dtAPT, filter, "FacilityID", DataViewRowState.CurrentRows);
                        DataTable dtFixList = dvVOR.ToTable(true, "FacilityID", "Latitude", "Longitude");
                        dtFixList.Merge(dvNDB.ToTable(true, "FacilityID", "Latitude", "Longitude"));
                        dtFixList.Merge(dvFIX.ToTable(true, "FacilityID", "Latitude", "Longitude"));
                        dtFixList.Merge(dvAPT.ToTable(true, "FacilityID", "Latitude", "Longitude"));
                        FixListDataGridView.DataSource = dtFixList;
                        FixListDataGridView.DefaultCellStyle.Font = new Font("Arial", 9);
                        FixListDataGridView.Columns[0].HeaderText = "ID";
                        if (FixListDataGridView.Rows.Count != 0)
                        {
                            FixListDataGridView.AutoResizeColumn(0, DataGridViewAutoSizeColumnMode.AllCells);
                        }
                    }
                    else
                    {
                        string Msg = "You must select your FAA data folder before you can search for Fixes.";
                        SCTcommon.SendMessage(Msg);
                        IdentifierTextBox.Text = string.Empty;
                    }
                }
                else
                {
                    FixListDataGridView.DataSource = null;
                }
            }
        }

        private void POFDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // This needs to use a similar switch sequence
            if ((e.RowIndex != -1) && (e.ColumnIndex != -1))
            {
                if (POFDataGridView[1, e.RowIndex].Value.ToString().IndexOf("Center") != -1)
                    EnterCoordsButton.Enabled = (e.ColumnIndex > 10) && (e.ColumnIndex < 18);
                else
                    EnterCoordsButton.Enabled = (e.ColumnIndex == 11) || (e.ColumnIndex == 12);
            }
        }

        private void POFDataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if ((this.POFDataGridView.Columns[e.ColumnIndex].Name.IndexOf("Latitude") != -1) ||
                (this.POFDataGridView.Columns[e.ColumnIndex].Name.IndexOf("Longitude") != -1))
            {
                if (e.Value != null)
                {
                    string value = POFDataGridView[6, e.RowIndex].Value.ToString();
                    switch (value)
                    {
                        case "CTR":
                            for (int i = 11; i < 19; i++)
                            {
                                POFDataGridView[i, e.RowIndex].ReadOnly = false;
                            }
                            if (e.ColumnIndex > 10) e.CellStyle.BackColor =  Color.White;
                            break;
                        case "APP":
                        case "TWR":
                        case "GND":
                        case "DEL":
                            POFDataGridView[11, e.RowIndex].ReadOnly = false;
                            POFDataGridView[12, e.RowIndex].ReadOnly = false;
                            for (int i = 13; i < 19; i++)
                            {
                                POFDataGridView[i, e.RowIndex].ReadOnly = true;
                            }
                            if ((e.ColumnIndex == 11) || (e.ColumnIndex == 12))
                                e.CellStyle.BackColor = Color.White;
                            else
                                if (e.ColumnIndex > 12) e.CellStyle.BackColor = Color.Gray;
                            break;
                        default:
                            for (int i = 11; i < 19; i++)
                            {
                                POFDataGridView[i, e.RowIndex].ReadOnly = true;
                            }
                            if (e.ColumnIndex > 10) e.CellStyle.BackColor = Color.Gray;
                            break;
                    }
                }
            }
        }

        private void ResetSortButton_Click(object sender, EventArgs e)
        {
            DataGridView dgvPOF = POFDataGridView;
            dgvPOF.Sort(dgvPOF.Columns["Sequence"], ListSortDirection.Ascending);
        }
    }
}
