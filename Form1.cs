using System;
using System.Threading;
using System.Linq;
using System.Data;
using System.IO;
using System.Windows.Forms;
using System.ComponentModel;
using System.Media;
using System.Drawing;
using System.Diagnostics;

namespace SCTBuilder
{
    public partial class Form1 : Form
    {
        static public DataTable ARB = new SCTdata.ARBDataTable();
        static public DataTable VOR = new SCTdata.VORDataTable();
        static public DataTable NDB = new SCTdata.NDBDataTable();
        static public DataTable FIX = new SCTdata.FIXDataTable();
        static public DataTable APT = new SCTdata.APTDataTable();
        static public DataTable RWY = new SCTdata.RWYDataTable();
        static public DataTable TWR = new SCTdata.TWRDataTable();
        static public DataTable AWY = new SCTdata.AWYDataTable();
        static public DataTable SSD = new SCTdata.SSDDataTable();
        static public DataTable SUA = new SCTdata.SUADataTable();
        static public DataTable Polygon = new SCTdata.SUA_PolygonDataTable();
        static public DataTable ColorDef = new SCTdata.ColorDefsDataTable();
        static public DataTable LocalSector = new SCTdata.LocalSectorsDataTable();
        static public DataTable POFdata = new SCTdata.POFdataDataTable();
        static public DataSet SCT = new SCTdata();
        static public bool ExitClicked = false;
        static readonly string cr = Environment.NewLine; 
        string Msg;
        static string SSDIDvalue = string.Empty;
        private const int LatTest = 1;
        private const int LonTest = 2;
        private const bool IsLat = true;
        private const bool IsLon = false;
        private const bool SelectNGdata = true;
        public Form1()
        {
            InitializeComponent();
            SCT.Tables.Add(ARB);
            SCT.Tables.Add(VOR);
            SCT.Tables.Add(NDB);
            SCT.Tables.Add(FIX);
            SCT.Tables.Add(APT);
            SCT.Tables.Add(TWR);
            SCT.Tables.Add(AWY);
            SCT.Tables.Add(SSD);
            SCT.Tables.Add(ColorDef);
            SCTcommon.DefineColorConstants(ColorDef);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Console.WriteLine("--------Form1 loaded-------");
            LoadForm1();
        }

        private void LoadForm1()
        {
            Console.WriteLine("Load Form subroutine...");
            int iniAIRAC = CycleInfo.ReadINIxml();
            // Three things could result: No file (-1), Corrupted file (0) or OK (Last AIRAC)
            if (iniAIRAC > 0)
            {
                // GOOD FILE - Does the DATA match the last used AIRAC?
                int dataAIRAC = ReadNASR.AIRAC();
                Console.WriteLine("Done reading NASR AIRAC " + dataAIRAC);
                if (dataAIRAC != iniAIRAC)
                {
                    MismatchedXMLmessage();
                }
                // Load the subscription data
                if (LoadFAATextData() != -1) PostLoadTasks();
                // Check the Navigraph data after loading FAA data
                int NGAIRAC = ReadNaviGraph.AIRAC();
                if ((NGAIRAC != -1) && (dataAIRAC != NGAIRAC))
                {
                    MismatchedNGmessage(NGAIRAC, dataAIRAC);
                    InfoSection.HasNaviGraph = InfoSection.UseNaviGraph = false;
                }
                else
                {
                    InfoSection.HasNaviGraph = InfoSection.UseNaviGraph = true;
                    Thread background = new Thread(ReadNGFiles)
                    {
                        IsBackground = true
                    };
                    background.Start();
                }
            }
            // No data folder or did not install a data set
            else
            if (iniAIRAC == 0)   // File  corrupted
            {
                BadXMLmessage();
            }
            else
            // No XML file found
            {
                NoXMLmessage();
            }
            TestWriteSCT();                 // Update the output button
        }

        private static void ReadNGFiles()
        {
            // Updates existing data with NG data
            // As the files are used differently, some are preloaded, others are loaded as needed
            // This routine runs in background while Form is loading
            ReadNaviGraph.NavRTE();
            ReadNaviGraph.NavFIX();
            ReadNaviGraph.NavAID();
            ReadNaviGraph.Airports();
            ReadNaviGraph.NavAPT();
        }

        private void NoXMLmessage()
        {
            Msg = "It appears the program is running for the first time." + cr +
                "First create or select a data folder ('DATA')." + cr +
                "This folder will hold ALL the SCTBuilder data subfolders." + cr +
                "Then click the 'Update AIRAC' button to retrieve the current FAA AIRAC.";
            SCTcommon.SendMessage(Msg, MessageBoxIcon.Information);
        }

        private void BadXMLmessage()
        {
            Msg = "SCTBuilder closed with insufficient settings to resume.  " + cr +
                "First create or select a data folder ('DATA')." + cr +
                "This folder will hold ALL the SCTBuilder data subfolders." + cr +
                "Then click the 'Update AIRAC' button to retrieve the current FAA AIRAC.";
            SCTcommon.SendMessage(Msg);
        }

        private void MismatchedXMLmessage()
        {
            Msg = "The last AIRAC you used does not match the AIRAC in the data folder.  " + cr +
                "The program will update to the data in the data folder.";
            SCTcommon.SendMessage(Msg);
        }

        private void MismatchedNGmessage(int NGAIRAC, int dataAIRAC)
        {
            Msg = "The Navigraph AIRAC + " + NGAIRAC.ToString() +
                " does not match the data AIRAC " + dataAIRAC.ToString() + "." + cr +
            "The program cannot provide features that require the Navigraph data.";
            SCTcommon.SendMessage(Msg);
        }
        private void PostLoadTasks()
        {
            // Assumes we have a fresh FAA text folder and need to update
            UpdateCycleInfoOnForm();
            SetForm1Defaults();
            UpdateEngineers();
            UpdateLabel("");
        }

        public void UpdateCycleInfoOnForm()
        {
            if (CycleInfo.AIRAC == 1501)
            {
                CycleInfoLabel.Text = "No Active Cycle Data!";
            }
            else
            {
                CycleInfoLabel.Text = CycleInfo.BuildCycleText();
            }
            CycleInfoLabel.Refresh();
        }

        private int LoadFAATextData()
        {
            // Get the NASR AIRAC (from NATFIX) to later compare with the folder ID
            int result = ReadNASR.AIRAC();
            // This also confirms that the properly named folder does indeed contain data
            if (result != -1)
            {
                CycleInfo.AIRAC = result;
                CycleInfo.CycleDateFromAIRAC(result, true);     // Save the cycle information
                UpdateCycleInfoOnForm();
                CallNASRread();             // Read all the text files
            }
            return result;
        }

        private void CallNASRread()
        {
            ReadNASR.FillARB();
            ReadNASR.FillVORNDB();
            ReadNASR.FillFIX();
            ReadNASR.FillAPT();        // Includes RWY table
            ReadNASR.FillTWR();
            ReadNASR.FillAWY();
            ReadNASR.FillStarDP();
        }

        private void SetForm1Defaults()
        // Calls a load of all data and repopulates the form
        // Called by Load, DataFolder validated, and DatafolderButton
        {
            Console.WriteLine("Setting Form1 Defaults...");
            UpdateFolderMgt(toFolderMgt: false);
            if (LoadARTCCComboBox() != 0)           // Populates the combobox
            {
                UpdateARTCCComboBox();              // Sets the combobox to the last Sponsor ARTCC
            }
            else ClearARTCCComboBox();
            if (InfoSection.NorthLimit != 0)
                GetSquareAndOffset();                        // ... Use previously set limits
            if (LoadAirportComboBox() != 0)         // Using the desired filter format
                UpdateAirportComboBox();            // Set the combobox to the last Default Airport or top of list
            else ClearAirportComboBox();
            gridViewToolStripButton.Enabled = true;
            CheckARTCCAsCenterButton();
            LoadCenterCoords();
            CenterAPTButton.Enabled = AirportComboBox.SelectedIndex != -1;
            GetChecked();
            TestWriteSCT();
        }

        private void LoadCenterCoords()
        {
            if (InfoSection.CenterLatitude_Dec != 0)
                CenterLatTextBox.Text = InfoSection.CenterLatitude_SCT;
            if (InfoSection.CenterLongitude_Dec != 0)
                CenterLonTextBox.Text = InfoSection.CenterLongitude_SCT;
        }

        private int LoadARTCCComboBox()
        {
            // Populates the ARTCC combobox
            DataView dvARB = new DataView(ARB)
            {
                Sort = "ARTCC"
            };
            DataTable dtARTCC = dvARB.ToTable(true, "ARTCC");
            ARTCCComboBox.DisplayMember = "ARTCC";
            ARTCCComboBox.ValueMember = "ARTCC";
            ARTCCComboBox.DataSource = dtARTCC;
            int result = ARTCCComboBox.Items.Count;
            dvARB.Dispose();
            return result;
        }

        private void ClearARTCCComboBox()
        {
            ARTCCComboBox.DataSource = null;
        }

        private void UpdateARTCCComboBox()
        {
            // Sets the combobox to the last Sponsor ARTCC if in list
            if (InfoSection.SponsorARTCC.Length != 0)
                ARTCCComboBox.SelectedIndex = ARTCCComboBox.FindStringExact(InfoSection.SponsorARTCC);
            else
                ARTCCComboBox.SelectedIndex = 0;
        }
        private int LoadAirportComboBox()
        {
            // Populate the Airport combobox
            FilterBy.Method = "ARTCC"; int result;
            string filter = SetFilter();
            DataTable APTtable = new DataTable();
            APTtable.Columns.Add("ID", typeof(string));
            APTtable.Columns.Add("FacilityID", typeof(string));
            APTtable.Columns.Add("Class", typeof(string));
            string Classfilter = string.Empty;
            if (ClassBCheckBox.Checked) Classfilter = "([Class] = 'B')";
            if (ClassCCheckBox.Checked)
            {
                if (Classfilter.Length != 0) Classfilter += " OR ";
                Classfilter += "([Class] = 'C')";
            }
            if (ClassDCheckBox.Checked)
            {
                if (Classfilter.Length != 0) Classfilter += " OR ";
                Classfilter += "([Class] = 'D')";
            }
            if (ClassOtherCheckBox.Checked)
            {
                if (Classfilter.Length != 0) Classfilter += " OR ";
                Classfilter += "( ([Class] = 'E') OR ([Class] = 'F') OR ([Class] = '') )";
            }
            DataView dvTWR = new DataView(TWR);
            DataView dvAPT = new DataView(APT)
            {
                RowFilter = filter,
                Sort = "ID"
            };
            foreach (DataRowView dataRow in dvAPT)
            {
                dvTWR.RowFilter = "ID = '" + dataRow.Row["ID"] + "'";
                if (dvTWR.Count != 0)
                {
                    APTtable.Rows.Add(dataRow.Row["ID"], dataRow.Row["FacilityID"], dvTWR[0]["Class"]);
                }
                else
                {
                    APTtable.Rows.Add(dataRow.Row["ID"], dataRow.Row["FacilityID"], string.Empty);
                }
            }
            // Create the dataview, filtering by the selected class
            DataView dvAPTcombo = new DataView(APTtable)
            {
                RowFilter = Classfilter,
                Sort = "Class, FacilityID"
            };
            DataTable dtAPT = dvAPTcombo.ToTable(true, "ID", "FacilityID");
            AirportComboBox.DisplayMember = "FacilityID";
            AirportComboBox.ValueMember = "ID";
            AirportComboBox.DataSource = dtAPT;
            result = AirportComboBox.Items.Count;
            APTtable.Dispose();
            dvAPT.Dispose();
            dvTWR.Dispose();
            return result;
        }

        private void ClearAirportComboBox()
        {
            AirportComboBox.DataSource = null;
            CenterARTCCButton.Enabled = false;
        }

        private void UpdateAirportComboBox()
        {
            int FoundItem = -1;
            if (AirportComboBox.Items.Count != 0)
            {
                if (InfoSection.DefaultAirport.Length != 0)
                {
                    FoundItem = AirportComboBox.FindStringExact(InfoSection.DefaultAirport);
                }
                if (FoundItem != -1) AirportComboBox.SelectedIndex = FoundItem;
                else AirportComboBox.SelectedIndex = 0;
            }
        }

        private bool SectionSelected()
        {
            return APTsCheckBox.Checked || RWYsCheckBox.Checked || AWYsCheckBox.Checked || VORsCheckBox.Checked
                || NDBsCheckBox.Checked || FIXesCheckBox.Checked || ARTCCCheckBox.Checked
                || SIDsCheckBox.Checked || STARsCheckBox.Checked || OceanicCheckBox.Checked;
        }

        private bool SquareSelected()
        {
            return (SouthLimitTextBox.TextLength != 0) &&
                (NorthLimitTextBox.TextLength != 0) && (WestLimitTextBox.TextLength != 0) &&
                (EastLimitTextBox.TextLength != 0);
        }

        private void PreviewButton_Click(object sender, EventArgs e)
        {
            if (PreviewButtonReady())
            {
                string lastTab = "APTtabPage";
                Cursor.Current = Cursors.WaitCursor;
                SetChecked();               // Save all checkboxes to COMMON
                SetSquareAndOffset();       // Save Square and offset to COMMON
                SetFilterBy();              // Save Lat-Lon filter limits to COMMON
                Refresh();
                FilterBy.Method = "Square";
                string filter;
                if (APTsCheckBox.Checked || RWYsCheckBox.Checked || SIDsCheckBox.Checked || STARsCheckBox.Checked)
                {
                    if (SCTchecked.LimitAPT2ARTC)
                        FilterBy.Method = "ARTCC";
                    else
                        FilterBy.Method = "Square";
                    filter = SetFilter();
                    SelectTableItems(APT, filter);
                }
                if (SCTchecked.ChkAPT)
                {
                    UpdateLabel("Building APT grid view from selection");
                    if (SCTcommon.dtHasRows(APT)) lastTab = LoadAPTDataGridView();
                }
                if (SCTchecked.ChkRWY)
                {
                    if (InfoSection.UseNaviGraph && (SCTcommon.dtHasXSelectedRows(ReadNaviGraph.airports) > 0))
                    {
                        if (SCTcommon.dtHasXSelectedRows(ReadNaviGraph.airports) > 0) SelectNGRWYs();
                    }
                    if (SCTcommon.dtHasRows(APT))
                    {
                        if (SelectRWYs() != 0) lastTab = LoadRWYDataGridView();
                        else ClearDataGridView(dgvRWY);
                    }
                    else
                    {
                        Msg = "Cannot select Runways.  No airports with runways in the selected square.";
                        SCTcommon.SendMessage(Msg);
                    }
                }
                FilterBy.Method = "Square";
                filter = SetFilter();
                if (SCTchecked.ChkVOR)
                {
                    SelectTableItems(VOR, filter);
                    UpdateLabel("Building VOR grid view from selection");
                    lastTab = LoadVORGridView();
                }
                if (SCTchecked.ChkNDB)
                {
                    SelectTableItems(NDB, filter);
                    UpdateLabel("Building NDB grid view from selection");
                    lastTab = LoadNDBGridView();
                }

                if (SCTchecked.ChkFIX)
                {
                    SelectTableItems(FIX, filter);
                    UpdateLabel("Building FIX grid view from selection");
                    lastTab = LoadFIXGridView();
                }
                // AWYs must come after VOR, NDB and FIX
                if (SCTchecked.ChkAWY)
                {
                    if (SelectAWYs() != 0)
                    {
                        UpdateLabel("Building AWY grid view from selection");
                        lastTab = LoadAWYDataGridView();
                    }
                    else ClearDataGridView(dgvAWY);
                }
                FilterBy.Method = "ARTCC";
                filter = SetFilter();
                if (SCTchecked.ChkARB) SelectTableItems(ARB, filter);
                if (SCTchecked.ChkSID)
                {
                    bool SID = true;
                    // APTs were selected above
                    if (!SCTcommon.dtHasRows(APT))
                    {
                        if (LimitAPT2ARTCCCheckBox.Checked)
                            FilterBy.Method = "ARTCC";
                        else
                            FilterBy.Method = "Square";
                        filter = SetFilter();
                        SelectTableItems(APT, filter);
                    }
                    if (SelectSSD(SID) != 0)
                    {
                        UpdateLabel("Building SID grid view from selection");
                        lastTab = LoadSSDDataGridView(SID);
                    }
                }
                if (SCTchecked.ChkSTAR)
                {
                    bool STAR = false;
                    // APTs were selected above
                    if (!SCTcommon.dtHasRows(APT))
                    {
                        if (LimitAPT2ARTCCCheckBox.Checked)
                            FilterBy.Method = "ARTCC";
                        else
                            FilterBy.Method = "Square";
                        filter = SetFilter();
                        SelectTableItems(APT, filter);
                    }
                    if (SelectSSD(STAR) != 0)
                    {
                        UpdateLabel("Building STAR grid view from selection");
                        lastTab = LoadSSDDataGridView(STAR);
                    }
                }
                if (SCTchecked.ChkOceanic)
                {
                    UpdateLabel("Building Oceanic grid view from selection");
                    SelectOceanic();
                    UpdateLabel("Building RTE grid view from selection");
                    lastTab = LoadOceanicDataGridView();
                }
                // Select the items for NaviGraph
                if (InfoSection.UseNaviGraph)
                {
                    UpdateLabel("Selecting NaviGraph APT...");
                    FilterBy.Method = "Square";
                    ReadNaviGraph.SelectNGTables(SetFilter(SelectNGdata));
                }
                SelectedTabControl.SelectedTab = SelectedTabControl.TabPages[lastTab];
                UpdateLabel("");
                Refresh();
                SCTtoolStripButton.Enabled = ESEToolStripButton.Enabled = true;
            }
            else
            {
                Msg = "You must have a square identifed, an output folder, and some selection items before you can Preview.";
                SCTcommon.SendMessage(Msg);
            }
            Cursor.Current = Cursors.Default;
            SystemSounds.Beep.Play();
        }

        private void ClearDataGridView(DataGridView dgv)
        {
            dgv.DataSource = null;
        }

        private int SelectTableItems(DataTable dt, string filter)
        {
            DataView dataView = new DataView(dt);
            ClearSelected(dataView);
            dataView.RowFilter = filter;
            int result = dataView.Count;
            SetSelected(dataView, true);
            dataView.Dispose();
            return result;
        }

        private int SelectRWYs()
        {
            DataView dvAPT = new DataView(APT)
            {
                RowFilter = "[Selected]"
            };
            DataView dvRWY = new DataView(RWY);
            ClearSelected(dvRWY);
            foreach (DataRowView drvAPT in dvAPT)
            {
                dvRWY.RowFilter = "[ID] = '" + drvAPT["ID"].ToString() + "'";
                if (dvRWY.Count != 0)
                {
                    UpdateLabel("Selecting " + dvRWY.Count + " runways from " + drvAPT["FacilityID"]);
                    SetSelected(dvRWY, false);
                }
            }
            dvRWY.RowFilter = "[Selected]";
            int result = dvRWY.Count;
            dvRWY.Dispose();
            dvAPT.Dispose();
            SelectedTabControl.SelectedTab = SelectedTabControl.TabPages["RWYtabPage"];
            return result;
        }

        private int SelectNGRWYs()
        {
            DataView dvAirports = new DataView(ReadNaviGraph.airports)
            {
                RowFilter = "[Selected]"
            };
            DataView dvNavAPT = new DataView(ReadNaviGraph.wpNavAPT);
            foreach (DataRowView drv in dvAirports)
            {
                dvNavAPT.RowFilter = "[FacilityID] = '" + drv["FacilityID"].ToString();
                if (dvNavAPT.Count != 0)
                {
                    SetSelected(dvNavAPT, false);
                }
            }
            dvNavAPT.RowFilter = "[Selected]";
            int result = dvNavAPT.Count;
            dvNavAPT.Dispose();
            dvAirports.Dispose();
            return result;
        }

        private int SelectAWYs()
        {
            int MaxSeqNo; int MinSeqNo; int MaxSelSeqNo; int MinSelSeqNo;
            string awy1; string awyFilter;
            string seqFilter = " AND ([Sequence] = ";
            // Set the filter
            FilterBy.Method = "Square";
            string filter = SetFilter();
            // Clear prior selected
            DataView dvAWY = new DataView(AWY);
            ClearSelected(dvAWY);
            // Apply the square filter
            dvAWY.RowFilter = filter;
            int result = dvAWY.Count;
            UpdateLabel("Selecting " + result + " airway segmentss...");
            // Select all components inside the square
            SetSelected(dvAWY, true);
            // This filter shouldn't do anything...
            dvAWY.RowFilter = "[Selected]";
            result = dvAWY.Count;
            UpdateLabel("Selected " + result + " airway segments...");
            // Build a unique list of airways
            DataTable dtAWYcheck = dvAWY.ToTable(true, "AWYID");
            // Loop the list of Airways to extend a leg beyond the square...
            foreach (DataRow dataRow in dtAWYcheck.Rows)
            {
                // change filter to just one airway
                awy1 = dataRow[0].ToString();
                awyFilter = "([AWYID] = '" + awy1 + "')";
                dvAWY.RowFilter = awyFilter;
                // Get the range of sequence numbers
                MinSeqNo = SCTcommon.GetMinDataView(dvAWY, "Sequence");
                MaxSeqNo = SCTcommon.GetMaxDataView(dvAWY, "Sequence");
                // Filter out the SELECTED for the SELECTED range of sequence numbers
                dvAWY.RowFilter = "[Selected] AND " + awyFilter;
                MinSelSeqNo = SCTcommon.GetMinDataView(dvAWY, "Sequence");
                MaxSelSeqNo = SCTcommon.GetMaxDataView(dvAWY, "Sequence");
                // Extend one leg outside the square, if able (Doesn't matter if only one waypoint found)
                if (MinSeqNo < MinSelSeqNo)
                {
                    dvAWY.RowFilter = awyFilter + seqFilter + (MinSelSeqNo - 10).ToString() + ")";
                    SetSelected(dvAWY);
                }
                if (MaxSeqNo > MaxSelSeqNo)
                {
                    dvAWY.RowFilter = awyFilter + seqFilter + (MaxSelSeqNo + 10).ToString() + ")";
                    SetSelected(dvAWY);
                }
            }
            // Clean up
            dvAWY.Dispose();
            return result;
        }

        private int SelectOceanic()
        {
            int MaxSeqNo; int MinSeqNo; int MaxSelSeqNo; int MinSelSeqNo;
            string awy1; string awyFilter;
            string seqFilter = " AND ([Sequence] = ";
            // Set the filter
            FilterBy.Method = "Square";
            string filter = SetFilter();
            // Clear prior selected
            DataView dvRTE = new DataView(ReadNaviGraph.wpNavRTE);
            ClearSelected(dvRTE);
            // Apply the square filter
            dvRTE.RowFilter = filter;
            int result = dvRTE.Count;
            UpdateLabel("Selecting " + result + " airway segmentss...");
            // Select all components inside the square
            SetSelected(dvRTE, true);
            // This filter shouldn't do anything...
            dvRTE.RowFilter = "[Selected]";
            result = dvRTE.Count;
            UpdateLabel("Selected " + result + " airway segments...");
            // Build a unique list of airways
            DataTable dtRTEcheck = dvRTE.ToTable(true, "AWYID");
            // Loop the list of Airways to extend a leg beyond the square...
            foreach (DataRow dataRow in dtRTEcheck.Rows)
            {
                // change filter to just one airway
                awy1 = dataRow[0].ToString();
                awyFilter = "([AWYID] = '" + awy1 + "')";
                dvRTE.RowFilter = awyFilter;
                // Get the range of sequence numbers
                MinSeqNo = SCTcommon.GetMinDataView(dvRTE, "Sequence");
                MaxSeqNo = SCTcommon.GetMaxDataView(dvRTE, "Sequence");
                // Filter out the SELECTED for the SELECTED range of sequence numbers
                dvRTE.RowFilter = "[Selected] AND " + awyFilter;
                MinSelSeqNo = SCTcommon.GetMinDataView(dvRTE, "Sequence");
                MaxSelSeqNo = SCTcommon.GetMaxDataView(dvRTE, "Sequence");
                // Extend one leg outside the square, if able (Doesn't matter if only one waypoint found)
                if (MinSeqNo < MinSelSeqNo)
                {
                    dvRTE.RowFilter = awyFilter + seqFilter + (MinSelSeqNo - 10).ToString() + ")";
                    SetSelected(dvRTE);
                }
                if (MaxSeqNo > MaxSelSeqNo)
                {
                    dvRTE.RowFilter = awyFilter + seqFilter + (MaxSelSeqNo + 10).ToString() + ")";
                    SetSelected(dvRTE);
                }
            }
            // Clean up
            dvRTE.Dispose();
            return result;
        }

        private string LoadAPTDataGridView()
        {
            DataView dvAPT = new DataView(APT);
            DataTable dtAPT = dvAPT.ToTable(true, "Selected", "FacilityID", "Name", "ID", "ARTCC");
            dgvAPT.DataSource = dtAPT;
            (dgvAPT.DataSource as DataTable).DefaultView.RowFilter = "[Selected]";
            dgvAPT.Columns[1].HeaderText = "Apt";
            dgvAPT.Columns[3].Visible = false;
            foreach (DataGridViewColumn dc in dgvAPT.Columns) dc.ReadOnly = true;
            dgvAPT.Columns[0].ReadOnly = false;
            dgvAPT.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);
            dgvAPT.Sort(dgvAPT.Columns[2], ListSortDirection.Ascending);
            dvAPT.Dispose();
            return "APTtabPage";
        }

        private string LoadRWYDataGridView()
        {
            //  Facility ID and RWYs
            DataView dvRWY = new DataView(RWY);
            DataTable dtRWY = dvRWY.ToTable(true, "Selected", "FacilityName", "RwyIdentifier", "ID", "ARTCC");
            dgvRWY.DataSource = dtRWY;
            (dgvRWY.DataSource as DataTable).DefaultView.RowFilter = "[Selected]";
            foreach (DataGridViewColumn dc in dgvRWY.Columns) dc.ReadOnly = true;
            dgvRWY.Columns[0].ReadOnly = false;
            dgvRWY.Columns[1].HeaderText = "Apt";
            dgvRWY.Columns[2].HeaderText = "Rwys";
            dgvRWY.Sort(dgvRWY.Columns[1], ListSortDirection.Ascending);
            dgvRWY.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);
            dvRWY.Dispose();
            return "RWYtabPage";
        }

        private string LoadVORGridView()
        {
            DataView dvVOR = new DataView(VOR);
            DataTable dtVOR = dvVOR.ToTable(true, "Selected", "FacilityID", "Name");
            dgvVOR.DataSource = dtVOR;
            (dgvVOR.DataSource as DataTable).DefaultView.RowFilter = "[Selected]";
            foreach (DataGridViewColumn dc in dgvVOR.Columns) dc.ReadOnly = true;
            dgvVOR.Columns[0].ReadOnly = false;
            dgvVOR.Columns[1].HeaderText = "ID";
            dgvVOR.Sort(dgvVOR.Columns[1], ListSortDirection.Ascending);
            dgvVOR.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);
            dvVOR.Dispose();
            return "VORtabPage";
        }

        private string LoadNDBGridView()
        {
            DataView dvNDB = new DataView(NDB);
            DataTable dtNDB = dvNDB.ToTable(true, "Selected", "FacilityID", "Name");
            dgvNDB.DataSource = dtNDB;
            (dgvNDB.DataSource as DataTable).DefaultView.RowFilter = "[Selected]";
            foreach (DataGridViewColumn dc in dgvNDB.Columns) dc.ReadOnly = true;
            dgvNDB.Columns[0].ReadOnly = false;
            dgvNDB.Columns[1].HeaderText = "ID";
            dgvNDB.Sort(dgvNDB.Columns[1], ListSortDirection.Ascending);
            dgvNDB.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);
            dvNDB.Dispose();
            return "NDBtabPage";
        }

        private string LoadFIXGridView()
        {
            DataView dvFIX = new DataView(FIX);
            DataTable dtFIX = dvFIX.ToTable(true, "Selected", "FacilityID", "Use");
            dgvFIX.DataSource = dtFIX;
            (dgvFIX.DataSource as DataTable).DefaultView.RowFilter = "[SELECTED]";
            foreach (DataGridViewColumn dc in dgvFIX.Columns) dc.ReadOnly = true;
            dgvFIX.Columns[0].ReadOnly = false;
            dgvFIX.Columns[1].HeaderText = "ID";
            dgvFIX.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            dvFIX.Dispose();
            return "FIXtabPage";
        }

        private string LoadAWYDataGridView()
        {
            DataView dvAWY = new DataView(AWY);
            DataTable dtAWY = dvAWY.ToTable(false, "Selected", "AWYID", "NAVAID", "MinEnrAlt", "MaxAuthAlt", "MinObstClrAlt", "Sequence");
            dgvAWY.DataSource = dtAWY;
            (dgvAWY.DataSource as DataTable).DefaultView.RowFilter = "[Selected]";
            foreach (DataGridViewColumn dc in dgvAWY.Columns) dc.ReadOnly = true;
            dgvAWY.Columns[0].ReadOnly = false;
            dgvAWY.Columns[1].HeaderText = "ID";
            dgvAWY.Columns[2].HeaderText = "NavAid";
            dgvAWY.Columns[3].HeaderText = "MEA";
            dgvAWY.Columns[4].HeaderText = "MAA";
            dgvAWY.Columns[5].HeaderText = "MOCA";
            dgvAWY.Sort(dgvAWY.Columns[1], ListSortDirection.Ascending);
            dgvAWY.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);
            dvAWY.Dispose();
            return "AWYtabPage";
        }

        private int SelectSSD(bool isSID)
        {
            string proc = "STARs"; if (isSID) proc = "SIDs";
            UpdateLabel("Clearing prior selections in " + proc);
            // Create the SSD view for the SID or STAR
            DataView dvSSD = new DataView(SSD)
            {
                RowFilter = "[IsSID] = " + isSID
            };
            ClearSelected(dvSSD);
            Application.DoEvents();

            // Get 'selected' airports and build a unique list
            DataView dvAPT = new DataView(APT)
            {
                RowFilter = "[SELECTED]"
            };
            DataTable dtAirports = dvAPT.ToTable(true, "FacilityID");
            dvAPT.Dispose();

            // Go through each APT (NavAid) looking for that APT in an AA (FixID)
            string AAfilter = "([FixType] = 'AA') AND ([IsSID] = " + isSID + ") AND " +
                "([SELECTED] = " + false + ")";
            foreach (DataRow dtAptRow in dtAirports.AsEnumerable())
            {
                string FacIDfilter = " AND ([NavAid] = '" + dtAptRow["FacilityID"].ToString() + "')";
                dvSSD.RowFilter = AAfilter + FacIDfilter;
                if (dvSSD.Count > 0)
                {
                    DataTable IDdata = dvSSD.ToTable(true, "ID");
                    if (IDdata.Rows.Count != 0)
                    {
                        foreach (DataRow data in IDdata.AsEnumerable())
                        {
                            dvSSD.RowFilter = "[ID] = '" + data[0].ToString() + "'";
                            UpdateLabel("Selecting " + dvSSD[0]["SSDname"].ToString() + " for " + dtAptRow["FacilityID"].ToString());
                            SetSelected(dvSSD);
                        }
                    }
                }
            }
            dvSSD.RowFilter = "[SELECTED]";
            DataTable dtReturnCount = dvSSD.ToTable(true, "ID");
            dvSSD.Dispose();
            return dtReturnCount.Rows.Count;
        }

        private string LoadSSDDataGridView(bool isSID)
        {
            // SIDS have Transitions after the name. STARS have them in front of the name.
            string filter = "([isSID] = " + isSID + ") AND (LEN([SSDcode]) > 0) AND " +
                "(LEN([SSDname]) > 0) AND (NOT ([SSDName] LIKE '*Transition'))";
            DataView dvSSD = new DataView(SSD)
            {
                RowFilter = filter,
                Sort = "SSDcode"
            };
            // The gridview needs only show the SID/STAR name
            if (isSID)
            {
                DataTable dtSID = dvSSD.ToTable(true, "Selected", "SSDname", "SSDcode", "ID");
                dgvSID.DataSource = dtSID;
                (dgvSID.DataSource as DataTable).DefaultView.RowFilter = "[Selected]";
                foreach (DataGridViewColumn dc in dgvSID.Columns) dc.ReadOnly = true;
                dgvSID.Columns[0].ReadOnly = false;
                dgvSID.Columns[1].HeaderText = "Name";
                dgvSID.Columns[2].HeaderText = "SID/RV";
                dgvSID.Columns[3].Visible = false;      // This is for later use
                dgvSID.Sort(dgvSID.Columns[1], ListSortDirection.Ascending);
                dgvSID.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                dvSSD.Dispose();
                SelectedTabControl.SelectedTab = SelectedTabControl.TabPages["SIDtabPage"];
            }
            else
            {
                DataTable dtSTAR = dvSSD.ToTable(true, "Selected", "SSDname", "SSDcode", "ID");
                dgvSTAR.DataSource = dtSTAR;
                (dgvSTAR.DataSource as DataTable).DefaultView.RowFilter = "[Selected]";
                foreach (DataGridViewColumn dc in dgvSTAR.Columns) dc.ReadOnly = true;
                dgvSTAR.Columns[0].ReadOnly = false;
                dgvSTAR.Columns[1].HeaderText = "Name";
                dgvSTAR.Columns[2].HeaderText = "STAR";
                dgvSTAR.Columns[3].Visible = false;      // This is for later use
                dgvSTAR.Sort(dgvSTAR.Columns[1], ListSortDirection.Ascending);
                SelectedTabControl.SelectedTab = SelectedTabControl.TabPages["STARtabPage"];
                dvSSD.Dispose();
                dgvSTAR.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            }
            if (isSID) return "SIDtabPage";
            else return "STARtabPage";
        }

        private string LoadOceanicDataGridView()
        {
            DataView dvOceanic = new DataView(ReadNaviGraph.wpNavRTE);
            DataTable dtOceanic = dvOceanic.ToTable(true, "Selected", "AWYID", "NAVAID");
            dgvOceanic.DataSource = dtOceanic;
            (dgvOceanic.DataSource as DataTable).DefaultView.RowFilter = "[Selected]";
            foreach (DataGridViewColumn dc in dgvVOR.Columns) dc.ReadOnly = true;
            dgvOceanic.Columns[0].ReadOnly = false;
            dgvOceanic.Columns[1].HeaderText = "ID";
            dgvOceanic.Sort(dgvOceanic.Columns[1], ListSortDirection.Ascending);
            dgvOceanic.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);
            dvOceanic.Dispose();
            return "OceanicTabPage";
        }

        //private string SetSUAfilter()
        //{
        //    string Filter = " ( ([Latitude_North] <= " + FilterBy.NorthLimit.ToString() + ")" +
        //                    " AND  ([Latitude_South] >= " + FilterBy.SouthLimit.ToString() + ")" +
        //                    " AND ([Longitude_East] <= " + FilterBy.EastLimit.ToString() + ")" +
        //                    " AND ([Longitude_West] >= " + FilterBy.WestLimit.ToString() + ") )";
        //    string AddlFilter = string.Empty;
        //    if (SCTchecked.ChkSUA_ClassB)
        //    {
        //        AddlFilter += "([Category] = 'B')";
        //    }
        //    if (SCTchecked.ChkSUA_ClassC)
        //    {
        //        if (AddlFilter.Length != 0) AddlFilter += " OR ";
        //        AddlFilter += "([Category] = 'C')";
        //    }
        //    if (SCTchecked.ChkSUA_ClassD)
        //    {
        //        if (AddlFilter.Length != 0) AddlFilter += " OR ";
        //        AddlFilter += "([Category] = 'D')";
        //    }
        //    if (SCTchecked.ChkSUA_Restricted)
        //    {
        //        if (AddlFilter.Length != 0) AddlFilter += " OR ";
        //        AddlFilter += "([Category] = 'RESTRICTED')";
        //    }
        //    if (SCTchecked.ChkSUA_Prohibited)
        //    {
        //        if (AddlFilter.Length != 0) AddlFilter += " OR ";
        //        AddlFilter += "([Category] = 'PROHIBITED')";
        //    }
        //    if (SCTchecked.ChkSUA_Danger)
        //    {
        //        if (AddlFilter.Length != 0) AddlFilter += " OR ";
        //        AddlFilter += "([Category] = 'DANGER')";
        //    }
        //    if (AddlFilter.Length != 0)
        //        AddlFilter = "AND ( " + AddlFilter + " )";
        //    Console.WriteLine(Filter);
        //    Console.WriteLine(AddlFilter);
        //    return Filter + AddlFilter;
        //}

        private  void ClearSelected(DataView dv)
        {
            // DataView arrives filtered (or not)
            // otherwise, ALL the selected boxes are false
            foreach (DataRowView row in dv)
            {
                row["Selected"] = false;
            }
        }
        private void SetSelected(DataView dv, bool Label = false)
        {
            // If the filter is applied, selected boxes are true
            // otherwise, ALL the selected boxes are false
            // But if the ShowAll box is checked, ignore the update
            int result = dv.Count; int Counter = 0;
            foreach (DataRowView row in dv)
            {
                Counter++;
                row["Selected"] = true;
                if (Label)
                    UpdateLabel("Selecting " + result + " rows from " + dv.Table.TableName +
                       " (" + (Counter * 100 / dv.Count).ToString() + "% done)"); ;
            }
        }

        private void UpdateGridCount()
        {
            switch (SelectedTabControl.SelectedTab.Text)
            {
                case "APTs":
                    txtGridViewCount.Text = dgvAPT.Rows.Count.ToString() + " / " + APT.Rows.Count.ToString();
                    break;
                case "VORs":
                    txtGridViewCount.Text = dgvVOR.Rows.Count.ToString() + " / " + VOR.Rows.Count.ToString();
                    break;
                case "NDBs":
                    txtGridViewCount.Text = dgvNDB.Rows.Count.ToString() + " / " + NDB.Rows.Count.ToString();
                    break;
                case "FIXes":
                    txtGridViewCount.Text = dgvFIX.Rows.Count.ToString() + " / " + FIX.Rows.Count.ToString();
                    break;
                case "RWYs":
                    txtGridViewCount.Text = dgvRWY.Rows.Count.ToString() + " / " + RWY.Rows.Count.ToString();
                    break;
                case "AWYs":
                    txtGridViewCount.Text = dgvAWY.Rows.Count.ToString() + " / " + AWY.Rows.Count.ToString();
                    break;
                case "SIDs":
                    txtGridViewCount.Text = dgvSID.Rows.Count.ToString() + " / " + SSD.Rows.Count.ToString();
                    break;
                case "STARs":
                    txtGridViewCount.Text = dgvSTAR.Rows.Count.ToString() + " / " + SSD.Rows.Count.ToString();
                    break;
                case "Oceanic":
                    txtGridViewCount.Text = dgvOceanic.Rows.Count.ToString() + " / " + ReadNaviGraph.wpNavRTE.Rows.Count.ToString();
                    break;
                default:
                    txtGridViewCount.Text = "Tab not found)";
                    break;
            }
        }

        private string SetFilter(bool NGdata = false)
        {
            string result = string.Empty;
            if (NGdata)
            {
                SetFilterBy();
                result = result +
                    " ( ([Latitude] <= " + FilterBy.NorthLimit.ToString() + ")" +
                    " AND  ([Latitude] >= " + FilterBy.SouthLimit.ToString() + ")" +
                    " AND ([Longitude] <= " + FilterBy.EastLimit.ToString() + ")" +
                    " AND ([Longitude] >= " + FilterBy.WestLimit.ToString() + ") )";
            }
            else
            {
                switch (FilterBy.Method)
                {
                    default:
                    case "ARTCC":
                        if (ARTCCComboBox.SelectedIndex != -1)
                            result = " ([ARTCC] ='" + ARTCCComboBox.GetItemText(ARTCCComboBox.SelectedItem) + "')";
                        break;
                    case "Square":
                        SetFilterBy();
                        result = result +
                            " ( ([Latitude] <= " + FilterBy.NorthLimit.ToString() + ")" +
                            " AND  ([Latitude] >= " + FilterBy.SouthLimit.ToString() + ")" +
                            " AND ([Longitude] <= " + FilterBy.EastLimit.ToString() + ")" +
                            " AND ([Longitude] >= " + FilterBy.WestLimit.ToString() + ") )";
                        break;
                }
            }
            return result;
        }

        private void SetFilterBy()
        {
            double temp = InfoSection.NorthLimit + InfoSection.NorthOffset / InfoSection.NMperDegreeLatitude;
            if (temp > 90) FilterBy.NorthLimit = 90;
            else FilterBy.NorthLimit = InfoSection.NorthLimit + InfoSection.NorthOffset / InfoSection.NMperDegreeLatitude;
            temp = InfoSection.SouthLimit - InfoSection.SouthOffset / InfoSection.NMperDegreeLatitude;
            if (temp < -90) FilterBy.SouthLimit = -90;
            else FilterBy.SouthLimit = InfoSection.SouthLimit - InfoSection.SouthOffset / InfoSection.NMperDegreeLatitude;
            temp = FilterBy.EastLimit = InfoSection.EastLimit - InfoSection.EastOffset / InfoSection.NMperDegreeLongitude;
            if (temp > 180) FilterBy.EastLimit = 180;
            else FilterBy.EastLimit = InfoSection.EastLimit - InfoSection.EastOffset / InfoSection.NMperDegreeLongitude;
            temp = InfoSection.WestLimit + InfoSection.WestOffset / InfoSection.NMperDegreeLongitude;
            if (temp < -180) FilterBy.WestLimit = -180;
            else FilterBy.WestLimit = InfoSection.WestLimit + InfoSection.WestOffset / InfoSection.NMperDegreeLongitude;
        }

        private void AsstFacilityEngineerTextBox_Validated(object sender, EventArgs e)
        {
            // Note - this textbox does not need "Validating"
            bool ToClass = true;
            InfoSection.AsstFacilityEngineer = AsstFacilityEngineerTextBox.Text;
            TestWriteSCT();
            UpdateEngineers(ToClass);
        }

        private void FacilityEngineerTextBox_Validated(object sender, EventArgs e)
        {
            bool ToClass = true;
            InfoSection.FacilityEngineer = FacilityEngineerTextBox.Text;
            UpdateEngineers(ToClass);
            TestWriteSCT();
        }

        private void FacilityEngineerTextBox_Validating(object sender, CancelEventArgs e)
        {
            bool ToClass = true;
            Msg = "Facility Engineer name may not be blank";
            if (FacilityEngineerTextBox.TextLength == 0)
            {
                SCTcommon.SendMessage(Msg);
                FacilityEngineerTextBox.Text = "Facility Engineer Name";
                UpdateEngineers(ToClass);
            }
        }
        private void UpdateEngineers(bool ToClass = false)
        {
            if (ToClass)
            {
                InfoSection.FacilityEngineer = FacilityEngineerTextBox.Text;
                InfoSection.AsstFacilityEngineer = AsstFacilityEngineerTextBox.Text;
            }
            else
            {
                FacilityEngineerTextBox.Text = InfoSection.FacilityEngineer;
                AsstFacilityEngineerTextBox.Text = InfoSection.AsstFacilityEngineer;
            }
        }
        private void SquarebyARTCC()
        {
            // Updates the values of the base square based on the ARTCC selected
            string FilterARTCC = ARTCCComboBox.GetItemText(ARTCCComboBox.SelectedItem);
            double LatNorth = double.MinValue;
            double LongWest = double.MaxValue;
            double LatSouth = double.MaxValue;
            double LongEast = double.MinValue;
            DataView dataview = new DataView(ARB);
            string FilterString = "[ARTCC] = '" + FilterARTCC + "'";
            dataview.RowFilter = FilterString;
            foreach (DataRowView dataRow in dataview)
            {
                try
                {
                    double Latitude = Convert.ToDouble(dataRow["Latitude"]);
                    LatNorth = Math.Max(LatNorth, Latitude);
                    LatSouth = Math.Min(LatSouth, Latitude);
                    double Longitude = Convert.ToDouble(dataRow["Longitude"]);
                    LongWest = Math.Min(LongWest, Longitude);
                    LongEast = Math.Max(LongEast, Longitude);
                }
                catch
                {
                    Console.WriteLine("Try FAILED: Form1.SquareByARTCC");
                    Console.WriteLine(dataRow[0] + "  " + dataRow[1] + "  " + dataRow[2] + "  " + dataRow[3]);
                    Console.WriteLine(LatNorth + "  " + LongWest + "  " + LatSouth + "  " + LongEast);
                }
            }
            InfoSection.NorthLimit = LatNorth;
            InfoSection.SouthLimit = LatSouth;
            InfoSection.EastLimit = LongEast;
            InfoSection.WestLimit = LongWest;
            UpdateSquare(false);
            dataview.Dispose();
        }

        private void UpdateSquare(bool Save)
        {
            if (Save)
            {
                InfoSection.NorthLimit = Conversions.String2DecDeg(NorthLimitTextBox.Text);
                InfoSection.SouthLimit = Conversions.String2DecDeg(SouthLimitTextBox.Text);
                InfoSection.EastLimit = Conversions.String2DecDeg(EastLimitTextBox.Text);
                InfoSection.WestLimit = Conversions.String2DecDeg(WestLimitTextBox.Text);
            }
            else
            {
                NorthLimitTextBox.Text = Conversions.DecDeg2SCT(InfoSection.NorthLimit, IsLat);
                SouthLimitTextBox.Text = Conversions.DecDeg2SCT(InfoSection.SouthLimit, IsLat);
                EastLimitTextBox.Text = Conversions.DecDeg2SCT(InfoSection.EastLimit, IsLon);
                WestLimitTextBox.Text = Conversions.DecDeg2SCT(InfoSection.WestLimit, IsLon);
            }
        }

        private void CmdOutputFolder_Click(object sender, EventArgs e)
        {
            OutputFolderTextBox.Text = UpdateFolder(OutputFolderTextBox, "Select folder to save SCT files");
            if (OutputFolderTextBox.TextLength != 0)
            {
                UpdateFolderMgt(toFolderMgt: true);
            }
            TestWriteSCT();
        }

        private void CmdDataFolder_Click(object sender, EventArgs e)
        {
            string newDataFolder = UpdateFolder(FAADataFolderTextBox, "Select FAA AIRAC data folder");
            if ((newDataFolder != FAADataFolderTextBox.Text) && (newDataFolder.Length != 0))
            {
                FAADataFolderTextBox.Text = newDataFolder;
                UpdateFolderMgt(toFolderMgt: true);
                if (LoadFAATextData() != -1) PostLoadTasks();
                ClearAllDataGridViews();
            }
            TestWriteSCT();
        }

        private void ClearAllDataGridViews()
        {
            dgvAPT.DataSource = dgvAWY.DataSource = dgvFIX.DataSource = dgvNDB.DataSource =
                dgvRWY.DataSource = dgvSID.DataSource = dgvSTAR.DataSource = dgvVOR.DataSource =
                null;
            Refresh();
        }

        private string UpdateFolder(TextBox textBox, string dialogTitle)
        {
            // This calling routine allows other classes to update a folder
            string result = SCTcommon.GetFolderPath(textBox.Text, dialogTitle);
            return result;
        }

        private void UpdateFolderMgt(bool toFolderMgt)
        {
            if (toFolderMgt)
            {
                if (FolderMgt.DataFolder != FAADataFolderTextBox.Text)
                    FolderMgt.DataFolder = FAADataFolderTextBox.Text;
                if (FolderMgt.OutputFolder != OutputFolderTextBox.Text)
                    FolderMgt.OutputFolder = OutputFolderTextBox.Text;
            }
            else
            {
                if (FolderMgt.DataFolder != FAADataFolderTextBox.Text)
                    FAADataFolderTextBox.Text = FolderMgt.DataFolder;
                if (FolderMgt.OutputFolder != OutputFolderTextBox.Text)
                    OutputFolderTextBox.Text = FolderMgt.OutputFolder;
            }
        }

        private bool TestWriteSCT()
        {
            bool result; bool CheckedItemsHaveSelections = true;
            bool InfoPopulated =
             !(string.IsNullOrEmpty(FolderMgt.DataFolder.ToString()) ||
                string.IsNullOrEmpty(FolderMgt.OutputFolder.ToString()) ||
                string.IsNullOrEmpty(InfoSection.SponsorARTCC.ToString()) ||
                string.IsNullOrEmpty(InfoSection.DefaultAirport) ||
                InfoSection.CenterLatitude_Dec == 0 ||
                InfoSection.CenterLongitude_Dec == 0 ||
                string.IsNullOrEmpty(InfoSection.MagneticVariation.ToString()));
            if (InfoPopulated)
            {
                if (APTsCheckBox.Checked && (dgvAPT.Rows.Count == 0)) CheckedItemsHaveSelections = false;
                if (RWYsCheckBox.Checked && (dgvRWY.Rows.Count == 0)) CheckedItemsHaveSelections = false;
                if (AWYsCheckBox.Checked && (dgvAWY.Rows.Count == 0)) CheckedItemsHaveSelections = false;
                if (VORsCheckBox.Checked && (dgvVOR.Rows.Count == 0)) CheckedItemsHaveSelections = false;
                if (NDBsCheckBox.Checked && (dgvNDB.Rows.Count == 0)) CheckedItemsHaveSelections = false;
                if (FIXesCheckBox.Checked && (dgvFIX.Rows.Count == 0)) CheckedItemsHaveSelections = false;
                if (SIDsCheckBox.Checked && (dgvSID.Rows.Count == 0)) CheckedItemsHaveSelections = false;
                if (STARsCheckBox.Checked && (dgvSTAR.Rows.Count == 0)) CheckedItemsHaveSelections = false;
                if (!CheckedItemsHaveSelections)
                    SCTtoolStripButton.ToolTipText = "Check grid view; checked item has no output?";
            }
            else
            {
                SCTtoolStripButton.ToolTipText = "Items missing in [Info] section?";
            }
            result = InfoPopulated & CheckedItemsHaveSelections;
            SCTtoolStripButton.Enabled = ESEToolStripButton.Enabled = result;
            return result;
        }

        private void ChkbxShowAll_CheckedChanged(object sender, EventArgs e)
        {
            if (dgvAPT.DataSource != null) ToggleSelectedDGV(dgvAPT);
            if (dgvVOR.DataSource != null) ToggleSelectedDGV(dgvVOR);
            if (dgvNDB.DataSource != null) ToggleSelectedDGV(dgvNDB);
            if (dgvFIX.DataSource != null) ToggleSelectedDGV(dgvFIX);
            if (dgvRWY.DataSource != null) ToggleSelectedDGV(dgvRWY);
            if (dgvAWY.DataSource != null) ToggleSelectedDGV(dgvAWY);
            if (dgvSID.DataSource != null) ToggleSelectedDGV(dgvSID);
            if (dgvSTAR.DataSource != null) ToggleSelectedDGV(dgvSTAR);
            if (dgvOceanic.DataSource != null) ToggleSelectedDGV(dgvOceanic);
            UpdateGridCount();
            if (chkbxShowAll.Checked) chkbxShowAll.BackColor = Color.White;
            else
            {
                chkbxShowAll.BackColor = Color.Transparent;
            }
        }

        private void ToggleSelectedDGV(DataGridView dgv)
        {
            if (chkbxShowAll.Checked) (dgv.DataSource as DataTable).DefaultView.RowFilter = null;
            else (dgv.DataSource as DataTable).DefaultView.RowFilter = "[Selected]";
        }

        private void TxtDataFolder_Validated(object sender, EventArgs e)
        {
            string filter = "*28DaySubscription*"; bool foundDir = false;
            if (FAADataFolderTextBox.TextLength > 0)
            {
                if (Directory.Exists(FAADataFolderTextBox.Text))
                {
                    UpdateFolderMgt(toFolderMgt: true);
                    string[] dirs = Directory.GetDirectories(@FolderMgt.DataFolder, filter, SearchOption.TopDirectoryOnly);
                    foreach (string dir in dirs)
                    {
                        if ((dir.IndexOf(filter) != 0) && !foundDir)
                        {
                            if (LoadFAATextData() != -1) PostLoadTasks();
                            foundDir = true;
                        }
                    }
                }
                else
                {
                    Msg = "This directory folder does not exist.  Consider using the selection button.";
                    SCTcommon.SendMessage(Msg);
                    FAADataFolderTextBox.Text = string.Empty;
                }
                ClearAllDataGridViews();
            }
            TestWriteSCT();
        }

        private void TxtDataFolder_Validating(object sender, CancelEventArgs e)
        {
            if (FAADataFolderTextBox.TextLength > 0)
            {
                if (Directory.Exists(FAADataFolderTextBox.Text))
                {
                    FolderMgt.DataFolder = FAADataFolderTextBox.Text;
                    e.Cancel = false;
                }
                else
                {
                    Msg = "Invalid folder path. Consider using the folder finder button.";
                    string caption = VersionInfo.Title;
                    MessageBoxButtons buttons = MessageBoxButtons.OK;
                    MessageBox.Show(Msg, caption, buttons);
                    FolderMgt.DataFolder = string.Empty;
                    e.Cancel = true;
                }
            }
        }

        private void CmdExit_Click(object sender, EventArgs e)
        {
            SetChecked();
            SetSquareAndOffset();
            CycleInfo.WriteINIxml();
            ExitClicked = true;
            Application.Exit();
        }

        private void ARTCCComboBox_Validated(object sender, EventArgs e)
        {
            InfoSection.SponsorARTCC = ARTCCComboBox.Text.ToString();
            if (LoadAirportComboBox() != 0)
                UpdateAirportComboBox();
            else ClearAirportComboBox();
            TestWriteSCT();
            CheckARTCCAsCenterButton();
        }

        private void CheckARTCCAsCenterButton()
        {
            CenterARTCCButton.Enabled = ARTCCComboBox.SelectedIndex != -1;
        }

        private void AirportComboBox_Validated(object sender, EventArgs e)
        {
            if (AirportComboBox.SelectedIndex != -1)
            {
                InfoSection.DefaultAirport = AirportComboBox.Text.ToString();
            }
            CenterAPTButton.Enabled = AirportComboBox.SelectedIndex != -1;
            TestWriteSCT();
        }

        private void LocalSectors_Click(object sender, EventArgs e)
        {
            if (ReadNASR.FillLocalSectors())
                SCToutput.WriteLS_SID(LocalSector);
        }

        private void TxtOutputFolder_Validated(object sender, EventArgs e)
        {
            if (OutputFolderTextBox.TextLength > 0)
            {
                FolderMgt.OutputFolder = OutputFolderTextBox.Text;
            }
            TestWriteSCT();
        }
        private void SetChecked()
        {
            SCTchecked.ChkAPT = APTsCheckBox.Checked;
            SCTchecked.LimitAPT2ARTC = LimitAPT2ARTCCCheckBox.Checked;
            SCTchecked.ChkARB = ARTCCCheckBox.Checked;
            SCTchecked.ChkAWY = AWYsCheckBox.Checked;
            SCTchecked.ChkFIX = FIXesCheckBox.Checked;
            SCTchecked.ChkNDB = NDBsCheckBox.Checked;
            SCTchecked.ChkRWY = RWYsCheckBox.Checked;
            SCTchecked.ChkSID = SIDsCheckBox.Checked;
            SCTchecked.ChkSTAR = STARsCheckBox.Checked;
            SCTchecked.ChkVOR = VORsCheckBox.Checked;
            SCTchecked.ChkOceanic = OceanicCheckBox.Checked;
            SCTchecked.ChkSUA_ClassB = SUA_ClassBCheckBox.Checked;
            SCTchecked.ChkSUA_ClassC = SUA_ClassCCheckBox.Checked;
            SCTchecked.ChkSUA_ClassD = SUA_ClassDCheckBox.Checked;
            SCTchecked.ChkSUA_Danger = SUA_DangerCheckBox.Checked;
            SCTchecked.ChkSUA_Prohibited = SUA_ProhibitedCheckBox.Checked;
            SCTchecked.ChkSUA_Restricted = SUA_RestrictedCheckBox.Checked;
            SCTchecked.ChkES_SCTfile = ESdataCheckBox.Checked;
            SCTchecked.ChkES_SSDfile = ESSSDCheckBox.Checked;
        }
        private void GetChecked()
        {
            APTsCheckBox.Checked = SCTchecked.ChkAPT;
            LimitAPT2ARTCCCheckBox.Checked = SCTchecked.LimitAPT2ARTC;
            ARTCCCheckBox.Checked = SCTchecked.ChkARB;
            AWYsCheckBox.Checked = SCTchecked.ChkAWY;
            FIXesCheckBox.Checked = SCTchecked.ChkFIX;
            NDBsCheckBox.Checked = SCTchecked.ChkNDB;
            RWYsCheckBox.Checked = SCTchecked.ChkRWY;
            SIDsCheckBox.Checked = SCTchecked.ChkSID;
            STARsCheckBox.Checked = SCTchecked.ChkSTAR;
            VORsCheckBox.Checked = SCTchecked.ChkVOR;
            OceanicCheckBox.Checked = SCTchecked.ChkOceanic;
            SUA_ClassBCheckBox.Checked = SCTchecked.ChkSUA_ClassB;
            SUA_ClassCCheckBox.Checked = SCTchecked.ChkSUA_ClassC;
            SUA_ClassDCheckBox.Checked = SCTchecked.ChkSUA_ClassD;
            SUA_DangerCheckBox.Checked = SCTchecked.ChkSUA_Danger;
            SUA_ProhibitedCheckBox.Checked = SCTchecked.ChkSUA_Prohibited;
            SUA_RestrictedCheckBox.Checked = SCTchecked.ChkSUA_Restricted;
            ESSSDCheckBox.Checked = SCTchecked.ChkES_SSDfile;
            ESdataCheckBox.Checked = SCTchecked.ChkES_SCTfile;
        }

        private void SetSquareAndOffset()
        {
            InfoSection.NorthLimit = Conversions.String2DecDeg(NorthLimitTextBox.Text);
            InfoSection.SouthLimit = Conversions.String2DecDeg(SouthLimitTextBox.Text);
            InfoSection.WestLimit = Conversions.String2DecDeg(WestLimitTextBox.Text);
            InfoSection.EastLimit = Conversions.String2DecDeg(EastLimitTextBox.Text);
            InfoSection.NorthOffset = Convert.ToDouble(NorthMarginNumericUpDown.Value);
            InfoSection.EastOffset = Convert.ToDouble(EastMarginNumericUpDown.Value);
            InfoSection.SouthOffset = Convert.ToDouble(SouthMarginNumericUpDown.Value);
            InfoSection.WestOffset = Convert.ToDouble(WestMarginNumericUpDown.Value);
        }

        private void GetSquareAndOffset()
        {
            NorthLimitTextBox.Text = Conversions.DecDeg2SCT(InfoSection.NorthLimit, IsLat);
            SouthLimitTextBox.Text = Conversions.DecDeg2SCT(InfoSection.SouthLimit, IsLat);
            WestLimitTextBox.Text = Conversions.DecDeg2SCT(InfoSection.WestLimit, IsLon);
            EastLimitTextBox.Text = Conversions.DecDeg2SCT(InfoSection.EastLimit, IsLon);
            NorthMarginNumericUpDown.Text = InfoSection.NorthOffset.ToString();
            EastMarginNumericUpDown.Text = InfoSection.EastOffset.ToString();
            SouthMarginNumericUpDown.Text = InfoSection.SouthOffset.ToString();
            WestMarginNumericUpDown.Text = InfoSection.WestOffset.ToString();
        }

        private void ChkSSDs_CheckedChanged(object sender, EventArgs e)
        {
            if (SIDsCheckBox.Checked)
            {
                APTsCheckBox.Checked = true;
                LimitAPT2ARTCCCheckBox.Checked = true;
            }
        }

        private void STARsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (STARsCheckBox.Checked)
            {
                APTsCheckBox.Checked = true;
                LimitAPT2ARTCCCheckBox.Checked = true;
            }
        }

        private void CmdAddSUAs_Click(object sender, EventArgs e)
        {
            ReadNASR.FillAirSpace();
        }

        private void GoToFAA28dayNASRToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string url = "https://www.faa.gov/air_traffic/flight_info/aeronav/aero_data/NASR_Subscription/";
            System.Diagnostics.Process.Start(url);
        }

        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form form = new About();
            form.ShowDialog(this);
            form.Dispose();
        }

        private void XML2SCTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form form = new XML2SCT();
            form.ShowDialog();
            form.Dispose();
        }

        private void LineGeneratorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form form = new LineGenerator();
            form.ShowDialog();
            form.Dispose();
        }

        private void IdentifierTextBox_TextChanged(object sender, EventArgs e)
        {
            SEByFIXButton.Enabled = NWByFIXButton.Enabled = false;
            if (FIX.Rows.Count != 0)
            {
                if (IdentifierTextBox.TextLength != 0)
                {
                    // First, be sure there is data in the database!
                    DataTable dtVOR = VOR;
                    DataTable dtNDB = NDB;
                    DataTable dtFIX = FIX;
                    if ((dtFIX.Rows.Count != 0) && (dtVOR.Rows.Count != 0) && (dtNDB.Rows.Count != 0))
                    {
                        // Load the gridview - can be sorted later.  Future: add [Selected]
                        string filter = "[FacilityID] LIKE '" + IdentifierTextBox.Text + "*" + "'";
                        DataView dvVOR = new DataView(dtVOR, filter, "FacilityID", DataViewRowState.CurrentRows);
                        DataView dvNDB = new DataView(dtNDB, filter, "FacilityID", DataViewRowState.CurrentRows);
                        DataView dvFIX = new DataView(dtFIX, filter, "FacilityID", DataViewRowState.CurrentRows);
                        DataTable dtFixList = dvVOR.ToTable(true, "FacilityID", "Latitude", "Longitude");
                        dtFixList.Merge(dvNDB.ToTable(true, "FacilityID", "Latitude", "Longitude"));
                        dtFixList.Merge(dvFIX.ToTable(true, "FacilityID", "Latitude", "Longitude"));
                        FixListDataGridView.DataSource = dtFixList;
                        FixListDataGridView.DefaultCellStyle.Font = new Font("Arial", 9);
                        FixListDataGridView.Columns[0].HeaderText = "ID";
                        if (FixListDataGridView.Rows.Count != 0)
                        {
                            FixListDataGridView.AutoResizeColumn(0, DataGridViewAutoSizeColumnMode.AllCells);
                            SEByFIXButton.Enabled = NWByFIXButton.Enabled = true;
                        };
                    }
                    else
                    {
                        Msg = "You must select your FAA data folder before you can search for Fixes.";
                        MessageBoxIcon icon = MessageBoxIcon.Warning;
                        SCTcommon.SendMessage(Msg, icon);
                        IdentifierTextBox.Text = string.Empty;
                    }
                }
                else
                {
                    FixListDataGridView.DataSource = null;
                }
            }
            else
            {
                Msg = "You must first load FAA data";
                SCTcommon.SendMessage(Msg);
            }
        }

        private double TestLatLimits(TextBox tb)
        {
            double limit = CrossForm.Lat;
            if (Math.Abs(limit) > 90)
            {
                Msg = "Latitude must be a value between -90 to +90 degrees";
                SCTcommon.SendMessage(Msg);
                limit = 0;
                tb.Focus();
            }
            else if (InfoSection.SouthLimit > InfoSection.NorthLimit)
            {
                Msg = "The North Limit may not be south of the South Limit";
                SCTcommon.SendMessage(Msg);
                limit = 0;
            }
            else
            {
                tb.Text = Conversions.DecDeg2SCT(limit, IsLat);
                InfoSection.SouthLimit = limit;
                SetSquareAndOffset();
                CheckARTCCAsCenterButton();
            }
            return limit;
        }

        private double TestLonLimits(TextBox tb)
        {
            double limit = CrossForm.Lon;
            if (Math.Abs(limit) > 180)
            {
                Msg = "Longitude must be a value between -180 to +180 degrees";
                SCTcommon.SendMessage(Msg);
                limit = 0;
                tb.Focus();
            }
            else if (InfoSection.WestLimit > InfoSection.EastLimit)
            {
                Msg = "The West Limit may not be east of the East Limit";
                SCTcommon.SendMessage(Msg);
                limit = 0;
            }
            else
            {
                tb.Text = Conversions.DecDeg2SCT(limit, IsLon);
                SetSquareAndOffset();
                CheckARTCCAsCenterButton();
            }
            return limit;
        }


        private void SouthLimitTextBox_Validated(object sender, EventArgs e)
        {
            TextBox tb = SouthLimitTextBox;
            if (CrossForm.TestTextBox(tb, method: LatTest))
            {
                    InfoSection.SouthLimit = TestLatLimits(tb);
            }
            else
            {
                Msg = "Invalid coordinates - please try again";
                SCTcommon.SendMessage(Msg);
                tb.Focus();
            }
        }

        private void NorthLimitTextBox_Validated(object sender, EventArgs e)
        {
            TextBox tb = NorthLimitTextBox;
            if (CrossForm.TestTextBox(tb, method: LatTest))
            {
                InfoSection.NorthLimit = TestLatLimits(tb);
            }
            else
            {
                Msg = "Invalid coordinates - please try again";
                SCTcommon.SendMessage(Msg);
                tb.Focus();
            }
        }

        private void WestLimitTextBox_Validated(object sender, EventArgs e)
        {
            TextBox tb = WestLimitTextBox;
            if (CrossForm.TestTextBox(tb, method: LonTest))
            {
                InfoSection.WestLimit = TestLonLimits(tb);
            }
            else
            {
                Msg = "Invalid coordinates - please try again";
                SCTcommon.SendMessage(Msg);
                tb.Focus();
            }
        }

        private void EastLimitTextBox_Validated(object sender, EventArgs e)
        {
            TextBox tb = EastLimitTextBox;
            if (CrossForm.TestTextBox(tb, method: LonTest))
            {
                InfoSection.EastLimit = TestLonLimits(tb);
            }
            else
            {
                Msg = "Invalid coordinates - please try again";
                SCTcommon.SendMessage(Msg);
                tb.Focus();
            }
        }

        private void CenterLatTextBox_Validated(object sender, EventArgs e)
        {
            TextBox tb = CenterLatTextBox;
            if (CrossForm.TestTextBox(tb))
            {
                double limit = CrossForm.Lat;
                if (Math.Abs(limit) > 90)
                {
                    Msg = "Latitude must be a value between -90 to +90 degrees";
                    SCTcommon.SendMessage(Msg);
                    tb.Focus();
                }
                else
                {
                    InfoSection.CenterLatitude_Dec = limit;
                    tb.Text = InfoSection.CenterLatitude_SCT;
                }
            }
            else
            {
                Msg = "Invalid coordinates - please try again";
                SCTcommon.SendMessage(Msg);
                tb.Focus();
            }
        }

        private void CenterLonTextBox_Validated(object sender, EventArgs e)
        {
            TextBox tb = CenterLonTextBox;
            if (CrossForm.TestTextBox(tb))
            {
                double limit = CrossForm.Lon;
                if (Math.Abs(limit) > 180)
                {
                    Msg = "Longitude must be a value between -180 to +180 degrees";
                    SCTcommon.SendMessage(Msg);
                    tb.Focus();
                }
                else
                {
                    InfoSection.CenterLongitude_Dec = limit;
                    tb.Text = InfoSection.CenterLongitude_SCT;
                }
            }
            else
            {
                Msg = "Invalid coordinates - please try again";
                SCTcommon.SendMessage(Msg);
                tb.Focus();
            }
        }

        private bool PreviewButtonReady()
        {
            bool OutputFileExists = Directory.Exists(FolderMgt.OutputFolder);
            bool result = OutputFileExists && SquareSelected() && SectionSelected();
            return result;
        }

        private void NWByFIXButton_Click(object sender, EventArgs e)
        {
            if (FixListDataGridView.SelectedRows.Count != 0)
            {
                InfoSection.NorthLimit = Convert.ToDouble(FixListDataGridView.SelectedRows[0].Cells["Latitude"].Value);
                InfoSection.WestLimit = Convert.ToDouble(FixListDataGridView.SelectedRows[0].Cells["Longitude"].Value);
                GetSquareAndOffset();
            }
            CheckARTCCAsCenterButton();
        }

        private void SEByFIXButton_Click(object sender, EventArgs e)
        {
            if (FixListDataGridView.SelectedRows.Count != 0)
            {
                InfoSection.SouthLimit = Convert.ToDouble(FixListDataGridView.SelectedRows[0].Cells["Latitude"].Value);
                InfoSection.EastLimit = Convert.ToDouble(FixListDataGridView.SelectedRows[0].Cells["Longitude"].Value);
                GetSquareAndOffset();
            }
            CheckARTCCAsCenterButton();
        }

        private void ARTCCCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            ARTCChighCheckBox.Enabled = ARTCClowCheckBox.Enabled = ARTCCCheckBox.Checked;
        }

        private void SetAllMarginsButton_Click(object sender, EventArgs e)
        {
            NorthMarginNumericUpDown.Value = SouthMarginNumericUpDown.Value =
                EastMarginNumericUpDown.Value = WestMarginNumericUpDown.Value =
                AllMarginsNumericUpDown.Value;
        }

        private void UpdateAIRACbutton_Click(object sender, EventArgs e)
        {
            UpdateFolderMgt(true);
            if ((FolderMgt.DataFolder.Length != 0) &&
                    Directory.Exists(FolderMgt.DataFolder))
            {
                Form getAIRAC = new SelectAIRAC();
                DialogResult dialogResult = getAIRAC.ShowDialog();
                while (dialogResult == DialogResult.None)
                { Application.DoEvents(); }
                if (dialogResult == DialogResult.OK)
                {
                    if (LoadFAATextData() != -1)
                    {
                        getAIRAC.Dispose();
                        PostLoadTasks();
                    }
                }
                else if (dialogResult == DialogResult.Abort)
                {
                    Msg = "FAA download returned an error.  Correct the error and retry.";
                    SCTcommon.SendMessage(Msg, MessageBoxIcon.Error);
                }
                UpdateAIRACbutton.Visible = true;
            }
            else
                SendMessage("You must first identify a data folder to store the AIRAC data.");
            TestWriteSCT();
        }

        private void CenterARTCCButton_Click(object sender, EventArgs e)
        {
            SquarebyARTCC();
            CenterSquare();
        }

        private void CenterSquareButton_Click(object sender, EventArgs e)
        {
            SetSquareAndOffset();
            if ((Math.Abs(InfoSection.NorthLimit) >= 0) && (Math.Abs(InfoSection.NorthLimit) <= 90) &&
                (Math.Abs(InfoSection.SouthLimit) >= 0) && (Math.Abs(InfoSection.SouthLimit) <= 90) &&
                (Math.Abs(InfoSection.EastLimit) >= 0) && (Math.Abs(InfoSection.EastLimit) <= 180) &&
                (Math.Abs(InfoSection.WestLimit) >= 0) && (Math.Abs(InfoSection.WestLimit) <= 180))
                CenterSquare();
            else
            {
                Msg = "Must have value in all Square textboxes to find center." + 
                    "(Latitude -90 to 90, Longitude -180 to 180)";
                SCTcommon.SendMessage(Msg);
            }
        }

        private void CenterSquare()
        {
            InfoSection.CenterLatitude_Dec = (InfoSection.NorthLimit + InfoSection.SouthLimit) / 2;
            CenterLatTextBox.Text = InfoSection.CenterLatitude_SCT;
            InfoSection.CenterLongitude_Dec = (InfoSection.WestLimit + InfoSection.EastLimit) / 2;
            CenterLonTextBox.Text = InfoSection.CenterLongitude_SCT;
        }

        private void FixListDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            NWByFIXButton.Enabled = SEByFIXButton.Enabled = FixListDataGridView.SelectedRows.Count != 0;
        }

        private void APTsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (APTsCheckBox.Checked == false)
            {
                RWYsCheckBox.Checked = LimitAPT2ARTCCCheckBox.Checked = 
                RWYsCheckBox.Enabled = LimitAPT2ARTCCCheckBox.Enabled = false;
            }
            else
            {
                RWYsCheckBox.Enabled = LimitAPT2ARTCCCheckBox.Enabled = true;
            }
        }

        private void RWYsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (RWYsCheckBox.Checked)
            {
                if (!APTsCheckBox.Checked)
                {
                    Msg = "In order to select Runways, airports will be selected.";
                    SendMessage(Msg);
                    APTsCheckBox.Checked = true;
                }
            }
        }

        private void AWYsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (AWYsCheckBox.Checked)
            {
                if (!VORsCheckBox.Checked && NDBsCheckBox.Checked && FIXesCheckBox.Checked)
                {
                    Msg = "In order to select Airways, navaids will be selected.";
                    SendMessage(Msg);
                    VORsCheckBox.Checked = NDBsCheckBox.Checked = FIXesCheckBox.Checked = true;
                }
            }
        }

        private void VORsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void NDBsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void FIXesCheckBox_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void ARTCChighCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            ARTCCCheckBox.Checked = ARTCChighCheckBox.Checked || ARTCClowCheckBox.Checked;
        }

        private void ARTCClowCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            ARTCCCheckBox.Checked = ARTCChighCheckBox.Checked || ARTCClowCheckBox.Checked;
        }

        private void SCTtoolStripButton_Click(object sender, EventArgs e)
        {
            SCTtoolStripButton.ToolTipText = "Please wait for the completion message."; Refresh();
            UpdateLabel("Writing files. Please wait for completion message.");
            UseWaitCursor = true;
            SCToutput.WriteSCT();
            UpdateLabel("");
            UseWaitCursor = false;
        }

        private void LabelGeneratorforDiagramsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form DrawLabel = new DrawLabels();
            DrawLabel.Show();
        }

        private void SendMessage(string Msg, MessageBoxIcon icon = MessageBoxIcon.Information,
            MessageBoxButtons buttons = MessageBoxButtons.OK)
        {
            MessageBox.Show(Msg, VersionInfo.Title, buttons, icon);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!ExitClicked)
            {
                SetChecked();
                SetSquareAndOffset();
                CycleInfo.WriteINIxml();
            }
            Application.Exit();
        }

        private void DMSDecDegToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form ConvertDMS = new CoordConverter();
            ConvertDMS.Show();
        }

        private void ARTCCComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            CenterARTCCButton.Enabled = (ARTCCComboBox.SelectedIndex != -1);
        }

        private void AirportComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            CenterAPTButton.Enabled = AirportComboBox.SelectedIndex != -1;
            if (AirportComboBox.SelectedIndex != -1)
            {
                InfoSection.DefaultAirport = AirportComboBox.Text;
                MagVarTextBox.Text = InfoSection.MagneticVariation.ToString("0.00");
            }
        }

        private void CenterAPTButton_Click(object sender, EventArgs e)
        {
            if (AirportComboBox.SelectedIndex != -1)
            {
                object[] coords = SCTcommon.GetNavData(AirportComboBox.Text);
                InfoSection.CenterLatitude_Dec = Convert.ToDouble(coords[2]);
                InfoSection.CenterLongitude_Dec = Convert.ToDouble(coords[3]);
                CenterLatTextBox.Text = InfoSection.CenterLatitude_SCT;
                CenterLonTextBox.Text = InfoSection.CenterLongitude_SCT;
                MagVarTextBox.Text = InfoSection.MagneticVariation.ToString("0.00");
            }
            else
            {
                Msg = "You must first select an airport to use it as a center point.";
                SCTcommon.SendMessage(Msg);
            }
        }

        private DataGridView DGVinTab()
        {
            switch (SelectedTabControl.SelectedTab.Text)
            {
                default:
                case "APTs":
                    return dgvAPT;
                case "RWYs":
                    return dgvRWY;
                case "VORs":
                    return dgvVOR;
                case "NDBs":
                    return dgvNDB;
                case "FIXes":
                    return dgvFIX;
                case "AWYs":
                    return dgvAWY;
                case "SIDs":
                    return dgvSID;
                case "STARs":
                    return dgvSTAR;
            }
        }

        private void UpdateLabel(string Text)
        {
            if (Text.Length != 0)
            {
                UpdatingLabel.Visible = true;
                UpdatingLabel.Text = Text;
                UpdatingLabel.Refresh();
            }
            else
            {
                UpdatingLabel.Text = "";
                UpdatingLabel.Visible = false;
            }
        }
        private void QuickSearchTextBox_TextChanged(object sender, EventArgs e)
        {
            if (QuickSearchTextBox.TextLength != 0)
            {
                DataGridView dgv = DGVinTab();
                DataTable dt = dgv.DataSource as DataTable;
                // Be sure there is data in the datagridview source!
                if (dt.Rows.Count != 0)
                {
                    dt.DefaultView.RowFilter = string.Format(dgv.SortedColumn.Name + " LIKE '%{0}%'", QuickSearchTextBox.Text);
                }
                chkbxShowAll.Checked = true;
                chkbxShowAll.BackColor = Color.White;
            }
        }

        private void ESEToolStripButton_Click(object sender, EventArgs e)
        {
            ESEToolStripButton.ToolTipText = "Please wait for the completion message."; Refresh();
            UpdateLabel("Writing files. Please wait for completion message.");
            UseWaitCursor = true;
            ESEoutput.WriteESE();
            UpdateLabel("");
            UseWaitCursor = false;
        }

        private void DgvSID_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex;
            if (dgvSID.CurrentCell.ColumnIndex == 0)
            {
                rowIndex = dgvSID.CurrentCell.RowIndex;
                SSDIDvalue = dgvSID.Rows[rowIndex].Cells[3].Value.ToString();
            }
        }

        private void DgvSTAR_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex;
            if (dgvSTAR.CurrentCell.ColumnIndex == 0)
            {
                rowIndex = dgvSTAR.CurrentCell.RowIndex;
                SSDIDvalue = dgvSTAR.Rows[rowIndex].Cells[3].Value.ToString();
            }
        }

        private void dgvSTAR_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (SSDIDvalue.Length != 0) 
            {
                bool Selected = (bool)dgvSTAR.CurrentCell.Value;
                DataView dvSSD = new DataView(SSD)
                {
                    RowFilter = "[ID] = '" + SSDIDvalue + "'"
                };
                if (Selected) SetSelected(dvSSD); else ClearSelected(dvSSD);
                dvSSD.Dispose();
                SSDIDvalue = string.Empty;
            }
        }

        private void dgvSID_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (SSDIDvalue.Length != 0)
            {
                bool Selected = (bool)dgvSID.CurrentCell.Value;
                DataView dvSSD = new DataView(SSD)
                {
                    RowFilter = "[ID] = '" + SSDIDvalue + "'"
                };
                if (Selected) SetSelected(dvSSD); else ClearSelected(dvSSD);
                dvSSD.Dispose();
                SSDIDvalue = string.Empty;
            }
        }

        private void savePreferencesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetChecked();
            SetSquareAndOffset();
            ExitClicked = false;
            CycleInfo.WriteINIxml();
            SendMessage("SCT Builder preferences saved.");
        }

        private void exitProgramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetChecked();
            SetSquareAndOffset();
            ExitClicked = true;
            CycleInfo.WriteINIxml();
            Application.Exit();
        }

        private void OceanicCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            SCTchecked.ChkOceanic = OceanicCheckBox.Checked;
        }

        private void SSDGeneratorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form form = new SSDGenerator();
            form.ShowDialog();
            form.Dispose();
        }

        private void ClassBCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            LoadAirportComboBox();
        }

        private void ClassCCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            LoadAirportComboBox();
        }

        private void ClassDCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            LoadAirportComboBox();
        }

        private void ClassOtherCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            LoadAirportComboBox();
        }

        private void preferencesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form form = new Preferences();
            form.ShowDialog(this);
            form.Dispose();
        }

        private void ESdataCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            SCTchecked.ChkES_SCTfile = ESdataCheckBox.Checked;
            if (SCTchecked.ChkES_SCTfile)
            {
                APTsCheckBox.Checked = true;
                RWYsCheckBox.Checked = true;
                VORsCheckBox.Checked = true;
                NDBsCheckBox.Checked = true;
                FIXesCheckBox.Checked = true;
            }
            SetChecked();
        }

        private void ESSSDCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            SCTchecked.ChkES_SSDfile = ESSSDCheckBox.Checked;
            if (SCTchecked.ChkES_SSDfile)
            {
                APTsCheckBox.Checked = true;
            }
            SetChecked();
        }

        private void RacetrackholdToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form form = new Racetrack();
            form.ShowDialog();
            form.Dispose();
        }

        private void POFManagerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form form = new POFmanager();
            form.ShowDialog();
            form.Dispose();
        }

        private void ArcGeneratorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form form = new ArcGenerator();
            form.ShowDialog();
            form.Dispose();
        }

        private void LimitAPT2ARTCCCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (ARTCCComboBox.SelectedIndex != -1)
                SCTchecked.LimitAPT2ARTC = LimitAPT2ARTCCCheckBox.Checked;
            else
                SCTcommon.SendMessage("Select an ARTCC first");
        }
    }
}
