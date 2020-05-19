using System;
using System.Diagnostics;
using System.Linq;
using System.Data;
using System.IO;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp;

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
        static public DataTable Colors = new SCTdata.ColorDefsDataTable();
        static public DataTable LocalSector = new SCTdata.LocalSectorsDataTable();
        static public DataTable NGSID = new SCTdata.NGSIDDataTable();
        static public DataTable NGSIDTransition = new SCTdata.NGSIDTransitionDataTable();
        static public DataTable NGSTAR = new SCTdata.NGSTARDataTable();
        static public DataTable NGSTARTransition = new SCTdata.NGSTARTransitionDataTable();
        static public DataTable NGFixes = new SCTdata.NGFixesDataTable();
        static public DataSet SCT = new SCTdata();
        static public bool ExitClicked = false;
        readonly string cr = Environment.NewLine; string Msg;

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
            SCT.Tables.Add(Colors);
            SCTcommon.DefineColorConstants(Colors);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Debug.WriteLine("--------Form1 loaded-------");
            LoadForm1();
        }

        private void LoadForm1()
        {
            Debug.WriteLine("Load Form subroutine...");
            int iniAIRAC = CycleInfo.ReadINIxml();
            // Three things could result: No file (-1), Corrupted file (0) or OK (Last AIRAC)
            if (iniAIRAC > 0)
            {
                // GOOD FILE - Does the DATA match the last used AIRAC?
                int dataAIRAC = ReadNASR.GetNASR_AIRAC();
                if (dataAIRAC != iniAIRAC)
                {
                    MismatchedXMLmessage();
                }
                // Load the subscription data
                if (LoadFAATextData() != -1) PostLoadTasks();
            }
            // The user never previously did not save a data folder or did not install a data set
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

        private void PostLoadTasks()
        {
            // Assumes we have a fresh FAA text folder and need to update
            UpdateCycleInfoOnForm();
            SetForm1Defaults();
            UpdateEngineers();
        }

        private void UpdateCycleInfoOnForm(bool visible = true, string message = "")
        {
            if (message.Length == 0)
            {
                if (CycleInfo.AIRAC == 1501)
                {
                    CycleInfoLabel.Text = "No Active Cycle Data!";
                }
                else
                {
                    CycleInfoLabel.Text = CycleInfo.BuildCycleText();
                }
            }
            else CycleInfoLabel.Text = message;
            CycleInfoLabel.Visible = visible;
            CycleInfoLabel.Refresh();
            Refresh();
        }

        private int LoadFAATextData()
        {
            // Get the NASR AIRAC (from NATFIX) to later compare with the folder ID
            int result = ReadNASR.GetNASR_AIRAC();
            // This also confirms that the properly named folder does indeed contain data
            if (result != -1)
            {
                CycleInfo.CycleDateFromAIRAC(result, true);     // Save the cycle information
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
            if (InfoSection.NorthSquare != 0)
                GetSquareAndOffset();                        // ... Use previously set limits
            if (LoadAirportComboBox() != 0)         // Using the desired filter format
                UpdateAirportComboBox();            // Set the combobox to the last Default Airport or top of list
            else ClearAirportComboBox();
            gridViewToolStripButton.Enabled = true;
            CheckARTCCAsCenterButton();
            CheckARTCC2SquareButton();
            CenterAPTButton.Enabled = AirportComboBox.SelectedIndex != -1;
            GetChecked();
            TestWriteSCT();
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
            }
            Console.WriteLine("APTcombobox Rows found: " + APTtable.Rows.Count);
            // Create the dataview, filtering by the selected class
            DataView dvAPTcombo = new DataView(APTtable)
            {
                RowFilter = "([Class] = 'B') OR ([Class] = 'C') OR ([Class] = 'D')",
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
            UseARTCCAsSquareButton.Enabled = CenterARTCCButton.Enabled = false;
        }

        private void UpdateAirportComboBox()
        {
            int FoundItem = -1;
            if (AirportComboBox.Items.Count != 0)
            {
                if (InfoSection.DefaultAirport.Length != 0)
                {
                    Debug.WriteLine("Looking for " + InfoSection.DefaultAirport + " in CboAirport");
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
                || SIDsCheckBox.Checked || STARsCheckBox.Checked;
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
                SetChecked();               // Save all checkboxes to COMMON
                SetSquareAndOffset();       // Save Square and offset to COMMON
                SetFilterBy();              // Save Lat-Lon filter limits to COMMON
                UpdatingLabel.Visible = true;
                QuickSearchLabel.Visible = QuickSearchTextBox.Visible = false;
                Refresh();
                FilterBy.Method = "Square";
                string filter; bool APTHasRows = false;
                if (APTsCheckBox.Checked || RWYsCheckBox.Checked || SIDsCheckBox.Checked || STARsCheckBox.Checked)
                {
                    if (LimitAPT2ARTCCCheckBox.Checked)
                        FilterBy.Method = "ARTCC";
                    else
                        FilterBy.Method = "Square";
                    filter = SetFilter();
                    APTHasRows = SelectTableItems(APT, filter) != 0;
                }
                if (SCTchecked.ChkAPT)
                {
                    UpdatingLabel.Text = "Building grid view from selection"; UpdatingLabel.Refresh();
                    if (APTHasRows) lastTab = LoadAPTDataGridView();
                }
                if (SCTchecked.ChkRWY)
                {
                    if (APTHasRows)
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
                    UpdatingLabel.Text = "Building grid view from selection"; UpdatingLabel.Refresh();
                    lastTab = LoadVORGridView();
                }
                if (SCTchecked.ChkNDB)
                {
                    SelectTableItems(NDB, filter);
                    UpdatingLabel.Text = "Building grid view from selection"; UpdatingLabel.Refresh();
                    lastTab = LoadNDBGridView();
                }
                if (SCTchecked.ChkFIX)
                {
                    SelectTableItems(FIX, filter);
                    UpdatingLabel.Text = "Building grid view from selection"; UpdatingLabel.Refresh();
                    lastTab = LoadFIXGridView();
                }
                // AWYs must come after VOR, NDB and FIX
                if (SCTchecked.ChkAWY)
                {
                    if (dgvVOR.Rows.Count == 0)
                    {
                        SelectTableItems(VOR, filter);
                    }
                    if (dgvNDB.Rows.Count == 0)
                    {
                        SelectTableItems(NDB, filter);
                    }
                    if (dgvFIX.Rows.Count == 0)
                    {
                        SelectTableItems(FIX, filter);
                    }
                    if (SelectAWYs() != 0)
                    {
                        UpdatingLabel.Text = "Building grid view from selection"; UpdatingLabel.Refresh();
                        lastTab = LoadAWYDataGridView();
                    }
                    else ClearDataGridView(dgvAWY);
                }
                FilterBy.Method = "ARTCC";
                filter = SetFilter();
                if (SCTchecked.ChkARB) SelectTableItems(ARB, filter);
                FilterBy.Method = "Square";
                filter = SetFilter();
                if (SCTchecked.ChkSID)
                    // APTs were selected above
                    if (!APTHasRows)
                    {
                        Msg = "Cannot select SIDs.  Mo airports with runways in the selected square.";
                        SCTcommon.SendMessage(Msg);
                    }
                    else
                    {
                        if (dgvVOR.Rows.Count == 0)
                        {
                            SelectTableItems(VOR, filter);
                        }
                        if (dgvNDB.Rows.Count == 0)
                        {
                            SelectTableItems(NDB, filter);
                        }
                        if (dgvFIX.Rows.Count == 0)
                        {
                            SelectTableItems(FIX, filter);
                        }
                        if (SelectSSD(true) != 0)
                        {
                            UpdatingLabel.Text = "Building grid view from selection"; UpdatingLabel.Refresh();
                            lastTab = LoadSSDDataGridView(true);
                        }
                    }
                if (SCTchecked.ChkSTAR)
                {
                    // APTs were selected above
                    if (!APTHasRows)
                    {
                        Msg = "Cannot select STARs.  Mo airports with runways in the selected square.";
                        SCTcommon.SendMessage(Msg);
                    }
                    else
                    {
                        if (dgvVOR.Rows.Count == 0)
                        {
                            SelectTableItems(VOR, filter);
                        }
                        if (dgvNDB.Rows.Count == 0)
                        {
                            SelectTableItems(NDB, filter);
                        }
                        if (dgvFIX.Rows.Count == 0)
                        {
                            SelectTableItems(FIX, filter);
                        }
                        if (SelectSSD(false) != 0)
                        {
                            UpdatingLabel.Text = "Building grid view from selection";
                            lastTab = LoadSSDDataGridView(false);
                        }
                    }
                }
                SelectedTabControl.SelectedTab = SelectedTabControl.TabPages[lastTab];
                UpdatingLabel.Visible = false;
                Refresh();
                SCTtoolStripButton.Enabled = true;
            }
            else
            {
                Msg = "You must have a square identifed, an output folder, and some selection items before you can Preview.";
                SCTcommon.SendMessage(Msg);
            }
        }

        private void ClearDataGridView(DataGridView dgv)
        {
            dgv.DataSource = null;
        }

        private int SelectTableItems(DataTable dt, string filter)
        {
            string table = dt.TableName;
            DataView dataView = new DataView(dt); 
            ClearSelected(dataView);
            dataView.RowFilter = filter;
            int result = dataView.Count;
            SetSelected(dataView);
            Console.WriteLine("Selected " + result + " rows from " + table);
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
            int result;
            ClearSelected(dvRWY);
            foreach (DataRowView drvAPT in dvAPT)
            {
                dvRWY.RowFilter = "[ID] = '" + drvAPT["ID"] + "'";
                if (dvRWY.Count != 0) SetSelected(dvRWY);
            }
            dvRWY.RowFilter = "[Selected]";
            result = dvRWY.Count;
            UpdatingLabel.Text = "Selecting " + result + " runways..."; UpdatingLabel.Refresh();
            dvRWY.Dispose();
            dvAPT.Dispose();
            SelectedTabControl.SelectedTab = SelectedTabControl.TabPages["RWYtabPage"];
            return result;
        }

        private int SelectAWYs()
        {
            // Always select all airways in the selection box
            FilterBy.Method = "Square";
            string filter = SetFilter();
            string awy1; int SeqNo;
            DataView dvAWY = new DataView(AWY);
            ClearSelected(dvAWY);
            dvAWY.RowFilter = filter;
            int result = dvAWY.Count;
            UpdatingLabel.Text = "Selecting " + result + " airways..."; UpdatingLabel.Refresh();
            SetSelected(dvAWY);
            dvAWY.RowFilter = "[Selected]";
            result = dvAWY.Count;
            UpdatingLabel.Text = "Validating " + result + " airways..."; UpdatingLabel.Refresh();
            DataTable dtAWYcheck = dvAWY.ToTable(true, "AWYID");
            foreach (DataRow dataRow in dtAWYcheck.Rows)
            {
                awy1 = dataRow[0].ToString();
                dvAWY.RowFilter = "[SELECTED] and [AWYID] = '" + awy1 + "'";
                // Console.WriteLine("Testing " + awy1 + " has " + dvAWY.Count + " rows.");
                if (dvAWY.Count == 1)
                {
                    SeqNo = (int)dvAWY[0]["Sequence"];
                    // Add the next leg unless it's the last leg
                    dvAWY.RowFilter = "([AWYID] = '" + awy1 + "') AND " +
                    "([Sequence] = " + (SeqNo + 10).ToString() + ")";
                    if (dvAWY.Count != 0)
                    {
                        dvAWY[0]["SELECTED"] = true;
                        SelectAWYNavaid(dvAWY[0]["NAVAID"].ToString());
                    }
                    // As long as it not the first waypoint, add the prior leg
                    if (SeqNo != 10)
                    {
                        dvAWY.RowFilter = "([AWYID] = '" + awy1 + "') AND " +
                        "([Sequence] = " + (SeqNo - 10).ToString() + ")";
                        dvAWY[0]["SELECTED"] = true;
                        SelectAWYNavaid(dvAWY[0]["NAVAID"].ToString());
                    }
                }
            }
            // Clean up
            dvAWY.Dispose();
            return result;
        }

        private void SelectAWYNavaid(string navaid)
        {
            // Assures that navaids represented in a child table SELECTED row have been SELECTED
            string filter = "[FacilityID] = '" + navaid + "'";
            DataView dvVOR = new DataView(VOR)
            {
                RowFilter = filter
            };
            DataView dvNDB = new DataView(NDB)
            {
                RowFilter = filter
            };
            DataView dvFIX = new DataView(FIX)
            {
                RowFilter = filter
            };
            if (dvVOR.Count != 0) dvVOR[0]["SELECTED"] = true;
            if (dvNDB.Count != 0) dvNDB[0]["SELECTED"] = true;
            if (dvFIX.Count != 0) dvFIX[0]["SELECTED"] = true;
            dvVOR.Dispose();
            dvNDB.Dispose();
            dvFIX.Dispose();
        }

        private string LoadAPTDataGridView()
        {
            DataView dvAPT = new DataView(APT);
            DataTable dtAPT = dvAPT.ToTable(true, "Selected", Conversions.ICOA("FacilityID"), "Name", "ID", "ARTCC");
            dgvAPT.DataSource = dtAPT;
            (dgvAPT.DataSource as DataTable).DefaultView.RowFilter = "[Selected]";
            dgvAPT.Columns[1].HeaderText = "Apt";
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
            DataTable dtAWY = dvAWY.ToTable(false, "Selected", "AWYID", "NAVAID", "MinEnrAlt", "MaxAuthAlt", "MinObstClrAlt","Sequence");
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
            string UpdateText = "Clearing prior selections in " + proc;
            UpdatingLabel.Text = UpdateText; UpdatingLabel.Refresh();
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
            UpdateText = "Selecting " + proc + " across ";
            UpdatingLabel.Text = UpdateText + dvAPT.Count + " airports."; UpdatingLabel.Refresh();
            DataTable dtAirports = dvAPT.ToTable(true, "FacilityID");
            // Go through each APT, to find SSDID and not selected
            string AAfilter = "([FixType] = 'AA') AND ([IsSID] = " + isSID + ") AND " +
                "([SELECTED] = " + false + ")";
            // This loop will find the SSDID for each airport NOT selected.
            // As the loop runs, duplicates will be skipped by the filter
            foreach (DataRow dtAptRow in dtAirports.AsEnumerable())
            {
                string FacIDfilter = " AND ([NavAid] = '" + dtAptRow["FacilityID"].ToString() + "')";
                dvSSD.RowFilter = AAfilter + FacIDfilter;
                UpdateText = "Selecting " + proc + " for " + dtAptRow["FacilityID"].ToString();
                UpdatingLabel.Text = UpdateText; UpdatingLabel.Refresh();
                DataTable IDdata = dvSSD.ToTable(true, "ID");
                if (IDdata.Rows.Count != 0)
                {
                    foreach (DataRow data in IDdata.AsEnumerable())
                    {
                        dvSSD.RowFilter = "[ID] = '" + data[0].ToString() + "'";
                        SetSelected(dvSSD);
                    }
                }
            }
            dvSSD.RowFilter = "[SELECTED]";
            DataTable dtReturnCount = dvSSD.ToTable(true, "ID");
            dvSSD.Dispose();
            dvAPT.Dispose();
            return dtReturnCount.Rows.Count;
        }

        private string LoadSSDDataGridView(bool isSID)
        {
            // Assumes the SID/STARs have been selected
            DataView dvSSD = new DataView(SSD)
            {
                RowFilter = "[SELECTED] AND ([FixType] = 'AA') AND ([isSID] = " + isSID + ")",
                Sort = "NavAid"
            };
            // The gridview needs only show the APT affect and SID/STAR
            if (isSID)
            {
                DataTable dtSID = dvSSD.ToTable(true, "Selected", "NavAid", "TransCode", "TransName", "ID");
                dgvSID.DataSource = dtSID;
                (dgvSID.DataSource as DataTable).DefaultView.RowFilter = "[Selected]";
                foreach (DataGridViewColumn dc in dgvSID.Columns) dc.ReadOnly = true;
                dgvAWY.Columns[0].ReadOnly = false;
                dgvSID.Columns[1].HeaderText = "Apt";
                dgvSID.Columns[2].HeaderText = "SID";
                dgvSID.Columns[3].HeaderText = "Name";
                dgvSID.Columns[4].Visible = false;      // This is for later use
                dgvSID.Sort(dgvSID.Columns["NavAid"], ListSortDirection.Ascending);
                dgvSID.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                dvSSD.Dispose();
                SelectedTabControl.SelectedTab = SelectedTabControl.TabPages["SIDtabPage"];
            }
            else
            {
                DataTable dtSTAR = dvSSD.ToTable(true, "Selected", "NavAid", "TransCode", "TransName", "ID");
                dgvSTAR.DataSource = dtSTAR;
                (dgvSTAR.DataSource as DataTable).DefaultView.RowFilter = "[Selected]";
                foreach (DataGridViewColumn dc in dgvSTAR.Columns) dc.ReadOnly = true;
                dgvSTAR.Columns[0].ReadOnly = false;
                dgvSTAR.Columns[1].HeaderText = "Apt";
                dgvSTAR.Columns[2].HeaderText = "STAR";
                dgvSTAR.Columns[3].HeaderText = "Name";
                dgvSTAR.Columns[4].Visible = false;      // This is for later use
                dgvSTAR.Sort(dgvSTAR.Columns["NavAid"], ListSortDirection.Ascending);
                SelectedTabControl.SelectedTab = SelectedTabControl.TabPages["STARtabPage"];
                dvSSD.Dispose();
                dgvSTAR.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            }
            if (isSID) return "SIDtabPage";
            else return "STARtabPage";
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

        private void ClearSelected(DataView dv)
        {
            // If the filter is applied, selected boxes are true
            // otherwise, ALL the selected boxes are false
            // But if the ShowAll box is checked, ignore the update
            dv.RowFilter = "";
            foreach (DataRowView row in dv)
            {
                row["Selected"] = false;
            }
        }
        private void SetSelected(DataView dv)
        {
            // If the filter is applied, selected boxes are true
            // otherwise, ALL the selected boxes are false
            // But if the ShowAll box is checked, ignore the update
            int result = dv.Count; int Counter = 0;
            string UpdateText = "Selecting " + result + " rows from " + dv.Table.TableName;
            if (dv.Table.TableName != "SSD")
            {
                UpdatingLabel.Text = UpdateText; UpdatingLabel.Refresh();
            }
            foreach (DataRowView row in dv)
            {
                Counter++;
                row["Selected"] = true;
                UpdatingLabel.Text = UpdateText + " (" + (Counter*100/dv.Count).ToString() + "% done)"; 
                UpdatingLabel.Refresh();
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
                default:
                    txtGridViewCount.Text = "Tab not found)";
                    break;
            }
        }

        private string SetFilter()
        {
            string result = string.Empty;
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
            return result;
        }

        private void SetFilterBy()
        {
            FilterBy.NorthLimit = InfoSection.NorthSquare + InfoSection.NorthOffset / InfoSection.NMperDegreeLatitude;
            FilterBy.SouthLimit = InfoSection.SouthSquare - InfoSection.SouthOffset / InfoSection.NMperDegreeLatitude;
            FilterBy.EastLimit = InfoSection.EastSquare - InfoSection.EastOffset / InfoSection.NMperDegreeLongitude;
            FilterBy.WestLimit = InfoSection.WestSquare + InfoSection.WestOffset / InfoSection.NMperDegreeLongitude;
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
                    Console.WriteLine(dataRow[0] + "  " + dataRow[1] + "  " + dataRow[2] + "  " + dataRow[3]);
                    Console.WriteLine(LatNorth + "  " + LongWest + "  " + LatSouth + "  " + LongEast);
                }
            }
            InfoSection.NorthSquare = LatNorth;
            InfoSection.SouthSquare = LatSouth;
            InfoSection.EastSquare = LongEast;
            InfoSection.WestSquare = LongWest;
            UpdateSquare(false);
            dataview.Dispose();
        }

        private void UpdateSquare(bool Save)
        {
            if (Save)
            {
                InfoSection.NorthSquare = Conversions.String2DecDeg(NorthLimitTextBox.Text);
                InfoSection.SouthSquare = Conversions.String2DecDeg(SouthLimitTextBox.Text);
                InfoSection.EastSquare = Conversions.String2DecDeg(EastLimitTextBox.Text);
                InfoSection.WestSquare = Conversions.String2DecDeg(WestLimitTextBox.Text);
            }
            else
            {
                NorthLimitTextBox.Text = Conversions.DecDeg2SCT(InfoSection.NorthSquare,IsLatitude: true);
                SouthLimitTextBox.Text = Conversions.DecDeg2SCT(InfoSection.SouthSquare, IsLatitude: true);
                EastLimitTextBox.Text = Conversions.DecDeg2SCT(InfoSection.EastSquare, IsLatitude: false);
                WestLimitTextBox.Text = Conversions.DecDeg2SCT(InfoSection.WestSquare, IsLatitude: false);
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

        private void NGDataFolderButton_Click(object sender, EventArgs e)
        {
            NGDataFolderTextBox.Text = UpdateFolder(NGDataFolderTextBox, "Select NaviGraph data folder");
            if (NGDataFolderTextBox.TextLength != 0)
            {
                UpdateFolderMgt(toFolderMgt: true);
            }
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
                if (FolderMgt.NGFolder != NGDataFolderTextBox.Text)
                    FolderMgt.NGFolder = NGDataFolderTextBox.Text;
            }
            else
            {
                if (FolderMgt.DataFolder != FAADataFolderTextBox.Text)
                    FAADataFolderTextBox.Text = FolderMgt.DataFolder;
                if (FolderMgt.OutputFolder != OutputFolderTextBox.Text)
                    OutputFolderTextBox.Text = FolderMgt.OutputFolder;
                if (FolderMgt.NGFolder != NGDataFolderTextBox.Text)
                    NGDataFolderTextBox.Text = FolderMgt.NGFolder;
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
            SCTtoolStripButton.Enabled = result;
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
            UpdateGridCount();
            if (chkbxShowAll.Checked) chkbxShowAll.BackColor = Color.White;
            else
            {
                chkbxShowAll.BackColor = Color.Transparent;
                QuickSearchLabel.Visible = QuickSearchTextBox.Visible = false;
            }

        }

        private void ToggleSelectedDGV(DataGridView dgv)
        {
            if (chkbxShowAll.Checked) (dgv.DataSource as DataTable).DefaultView.RowFilter = null;
            else (dgv.DataSource as DataTable).DefaultView.RowFilter = "[Selected]";
            Console.WriteLine("Now showing " + dgv.Rows.Count + " rows in " + dgv.Name);
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
            Console.WriteLine("TxtDataFolder_Validating...");
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
                    CycleInfoLabel.Text = "Choose folder to contain FAA text data";
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
            CheckARTCC2SquareButton();
        }

        private void CheckARTCCAsCenterButton()
        {
            CenterARTCCButton.Enabled = ARTCCComboBox.SelectedIndex != -1;
        }

        private void CheckARTCC2SquareButton()
        {
            UseARTCCAsSquareButton.Enabled = ARTCCComboBox.SelectedIndex != -1;
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
            Console.WriteLine("LocalSectors_Click...");
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
            Console.WriteLine("SetChecked...");
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
            SCTchecked.ChkSSDname = SIDNameCheckBox.Checked;
            SCTchecked.ChkSUA_ClassB = SUA_ClassBCheckBox.Checked;
            SCTchecked.ChkSUA_ClassC = SUA_ClassCCheckBox.Checked;
            SCTchecked.ChkSUA_ClassD = SUA_ClassDCheckBox.Checked;
            SCTchecked.ChkSUA_Danger = SUA_DangerCheckBox.Checked;
            SCTchecked.ChkSUA_Prohibited = SUA_ProhibitedCheckBox.Checked;
            SCTchecked.ChkSUA_Restricted = SUA_RestrictedCheckBox.Checked;
            InfoSection.UseFixes = useFixesForCoordinatesToolStripMenuItem.Checked;
            InfoSection.UseNaviGraph = includeNaviGraphDataToolStripMenuItem.Checked;
        }
        private void GetChecked()
        {
            Console.WriteLine("GetChecked...");
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
            SIDNameCheckBox.Checked = SCTchecked.ChkSSDname;
            SUA_ClassBCheckBox.Checked = SCTchecked.ChkSUA_ClassB;
            SUA_ClassCCheckBox.Checked = SCTchecked.ChkSUA_ClassC;
            SUA_ClassDCheckBox.Checked = SCTchecked.ChkSUA_ClassD;
            SUA_DangerCheckBox.Checked = SCTchecked.ChkSUA_Danger;
            SUA_ProhibitedCheckBox.Checked = SCTchecked.ChkSUA_Prohibited;
            SUA_RestrictedCheckBox.Checked = SCTchecked.ChkSUA_Restricted;
            useFixesForCoordinatesToolStripMenuItem.Checked = InfoSection.UseFixes;
            includeNaviGraphDataToolStripMenuItem.Checked = InfoSection.UseNaviGraph;
        }

        private void SetSquareAndOffset()
        {
            InfoSection.NorthSquare = Conversions.String2DecDeg(NorthLimitTextBox.Text);
            InfoSection.SouthSquare = Conversions.String2DecDeg(SouthLimitTextBox.Text);
            InfoSection.WestSquare = Conversions.String2DecDeg(WestLimitTextBox.Text);
            InfoSection.EastSquare = Conversions.String2DecDeg(EastLimitTextBox.Text);
            InfoSection.NorthOffset = Convert.ToDouble(NorthMarginNumericUpDown.Value);
            InfoSection.EastOffset = Convert.ToDouble(EastMarginNumericUpDown.Value);
            InfoSection.SouthOffset = Convert.ToDouble(SouthMarginNumericUpDown.Value);
            InfoSection.WestOffset = Convert.ToDouble(WestMarginNumericUpDown.Value);
        }

        private void GetSquareAndOffset()
        {
            NorthLimitTextBox.Text = Conversions.DecDeg2SCT(InfoSection.NorthSquare, true);
            SouthLimitTextBox.Text = Conversions.DecDeg2SCT(InfoSection.SouthSquare, true);
            WestLimitTextBox.Text = Conversions.DecDeg2SCT(InfoSection.WestSquare, false);
            EastLimitTextBox.Text = Conversions.DecDeg2SCT(InfoSection.EastSquare, false);
            NorthMarginNumericUpDown.Text = InfoSection.NorthOffset.ToString();
            EastMarginNumericUpDown.Text = InfoSection.EastOffset.ToString();
            SouthMarginNumericUpDown.Text = InfoSection.SouthOffset.ToString();
            WestMarginNumericUpDown.Text = InfoSection.WestOffset.ToString();
        }

        private void ChkSSDs_CheckedChanged(object sender, EventArgs e)
        {
            SIDNameCheckBox.Enabled = SIDsCheckBox.Checked;
            if (SIDsCheckBox.Checked)
            {
                Msg = "In order to select SIDs, airports and NavAids will be selected," + cr +
                    "and airports will be limited to primary ARTCC.  UNcheck the limit box" + cr +
                    "for ALL airports in square - expect several minutes to complete survey!";
                SendMessage(Msg);
                APTsCheckBox.Checked = true;
                LimitAPT2ARTCCCheckBox.Checked = true;
                VORsCheckBox.Checked = true;
                NDBsCheckBox.Checked = true;
                FIXesCheckBox.Checked = true;
            }
        }

        private void STARsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            STARNameCheckBox.Enabled = STARsCheckBox.Checked;
            if (STARsCheckBox.Checked)
            {
                Msg = "In order to select STARs, airports and NavAids will be selected," + cr +
                    "and airports will be limited to primary ARTCC.  UNcheck the limit box" + cr +
                    "for ALL airports in square - expect several minutes to complete survey!";
                SendMessage(Msg);
                APTsCheckBox.Checked = true;
                LimitAPT2ARTCCCheckBox.Checked = true;
                VORsCheckBox.Checked = true;
                NDBsCheckBox.Checked = true;
                FIXesCheckBox.Checked = true;
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
            form.ShowDialog(this);
            form.Dispose();
        }

        private void LineGeneratorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form form = new LineGenerator();
            form.ShowDialog(this);
            form.Dispose();
        }

        private void ConfirmOverwriteOfFilesToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            SCTchecked.ChkConfirmOverwrite = ConfirmOverwriteOfFilesToolStripMenuItem.Checked;
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

        private void SouthLimitTextBox_Validated(object sender, EventArgs e)
        {
            if ((SouthLimitTextBox.TextLength != 0) &
                    (NorthLimitTextBox.TextLength != 0))
            {
                double SLat = Conversions.String2DecDeg(SouthLimitTextBox.Text);
                double NLat = Conversions.String2DecDeg(NorthLimitTextBox.Text);
                if (SLat == -1)
                {
                    Msg = "You used an invalid format or "
                        + cr + "your latitude is out of range (-90 to 90)."
                        + cr + "Click the question mark for help on formats.";
                    SCTcommon.SendMessage(Msg);
                }
                else if (NLat < SLat)
                {
                    Msg = "Cannot place SE position north of NW position!";
                    SCTcommon.SendMessage(Msg);
                    SouthLimitTextBox.Text = string.Empty;
                }
                else
                {
                    InfoSection.SouthSquare = SLat;
                    SouthLimitTextBox.Text = Conversions.DecDeg2SCT(SLat, true);
                }
            }
            CheckARTCC2SquareButton();
            CheckARTCCAsCenterButton();
        }

        private void NorthLimitTextBox_Validated(object sender, EventArgs e)
        {
            if ((SouthLimitTextBox.TextLength != 0) &
                (NorthLimitTextBox.TextLength != 0))
            {
                double SLat = Conversions.String2DecDeg(SouthLimitTextBox.Text);
                double NLat = Conversions.String2DecDeg(NorthLimitTextBox.Text);
                if (NLat == -1)
                {
                    Msg = "You used an invalid format or "
                        + cr + "your latitude is out of range (-90 to 90)."
                        + cr + "Click the question mark for help on formats.";
                    SCTcommon.SendMessage(Msg);
                }
                else if (NLat < SLat)
                {
                    Msg = "Cannot place NW position south of SE position!";
                    SCTcommon.SendMessage(Msg);
                    NorthLimitTextBox.Text = string.Empty;
                }
                else
                {
                    InfoSection.NorthSquare = NLat;
                    NorthLimitTextBox.Text = Conversions.DecDeg2SCT(NLat, true);
                }
            }
            CheckARTCC2SquareButton();
            CheckARTCCAsCenterButton();
        }

        private void WestLimitTextBox_Validated(object sender, EventArgs e)
        {
            if ((WestLimitTextBox.TextLength != 0) &
                    (EastLimitTextBox.TextLength != 0))
            {
                double WLon = Conversions.String2DecDeg(WestLimitTextBox.Text);
                double ELon = Conversions.String2DecDeg(EastLimitTextBox.Text);
                if (WLon == -1)
                {
                    Msg = "You used an invalid format or "
                        + cr + "your latitude is out of range (-180 to 180)."
                        + cr + "Click the question mark for help on formats.";
                    SCTcommon.SendMessage(Msg);
                }
                else if (ELon < WLon)
                {
                    Msg = "Cannot place NW position east of SE position!";
                    SCTcommon.SendMessage(Msg);
                    WestLimitTextBox.Text = string.Empty;
                }
                else
                {
                    InfoSection.WestSquare = WLon;
                    WestLimitTextBox.Text = Conversions.DecDeg2SCT(WLon, false);
                }
            }
            CheckARTCC2SquareButton();
            CheckARTCCAsCenterButton();
        }

        private void EastLimitTextBox_Validated(object sender, EventArgs e)
        {
            if ((WestLimitTextBox.TextLength != 0) &
                (EastLimitTextBox.TextLength != 0))
            {
                double WLon = Conversions.String2DecDeg(WestLimitTextBox.Text);
                double ELon = Conversions.String2DecDeg(EastLimitTextBox.Text);
                if (ELon == -1)
                {
                    Msg = "You used an invalid format or "
                        + cr + "your latitude is out of range (-180 to 180)."
                        + cr + "Click the question mark for help on formats.";
                    SCTcommon.SendMessage(Msg);
                }
                else if (ELon < WLon)
                {
                    Msg = "Cannot place SE position west of NW position!";
                    SCTcommon.SendMessage(Msg);
                    EastLimitTextBox.Text = string.Empty;
                }
                else
                {
                    InfoSection.EastSquare = ELon;
                    EastLimitTextBox.Text = Conversions.DecDeg2SCT(ELon, false);
                }
            }
            CheckARTCC2SquareButton();
            CheckARTCCAsCenterButton();
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
                InfoSection.NorthSquare = Convert.ToDouble(FixListDataGridView.SelectedRows[0].Cells["Latitude"].Value);
                InfoSection.WestSquare = Convert.ToDouble(FixListDataGridView.SelectedRows[0].Cells["Longitude"].Value);
                GetSquareAndOffset();
            }
            CheckARTCC2SquareButton();
            CheckARTCCAsCenterButton();
        }

        private void SEByFIXButton_Click(object sender, EventArgs e)
        {
            if (FixListDataGridView.SelectedRows.Count != 0)
            {
                InfoSection.SouthSquare = Convert.ToDouble(FixListDataGridView.SelectedRows[0].Cells["Latitude"].Value);
                InfoSection.EastSquare = Convert.ToDouble(FixListDataGridView.SelectedRows[0].Cells["Longitude"].Value);
                GetSquareAndOffset();
            }
            CheckARTCC2SquareButton();
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
                while (dialogResult != DialogResult.None)
                { Application.DoEvents(); }

                if (dialogResult == DialogResult.OK)
                {
                    if (LoadFAATextData() != -1) PostLoadTasks();
                }
                else if (dialogResult == DialogResult.Abort)
                {
                    Msg = "FAA download returned an error.  Correct the error and retry.";
                    SCTcommon.SendMessage(Msg, MessageBoxIcon.Error);
                }
                else
                    Console.WriteLine("Returned with Cancel");
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
            if ((NorthLimitTextBox.TextLength != 0) && (SouthLimitTextBox.TextLength != 0) &&
                (EastLimitTextBox.TextLength != 0) && (WestLimitTextBox.TextLength != 0))
                CenterSquare();
            else
            {
                Msg = "Must have value in all Square textboxes to find center.";
                SCTcommon.SendMessage(Msg);
            }
        }

        private void CenterSquare()
        {
            InfoSection.CenterLatitude_Dec = (InfoSection.NorthSquare + InfoSection.SouthSquare) / 2;
            CenterLatTextBox.Text = InfoSection.CenterLatitude_SCT;
            InfoSection.CenterLongitude_Dec = (InfoSection.WestSquare + InfoSection.EastSquare) / 2;
            CenterLonTextBox.Text = InfoSection.CenterLongitude_SCT;
            MagVarTextBox.Text = InfoSection.MagneticVariation.ToString();
        }

        private void FixListDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            NWByFIXButton.Enabled = SEByFIXButton.Enabled = FixListDataGridView.SelectedRows.Count != 0;
        }

        private void APTsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            RWYsCheckBox.Enabled = LimitAPT2ARTCCCheckBox.Enabled = APTsCheckBox.Checked;
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
            UpdatingLabel.Visible = true;
            UpdatingLabel.Text = "Writing files. Please wait for completion message."; UpdatingLabel.Refresh();
            UseWaitCursor = true;
            SCToutput.WriteSCT();
            UpdatingLabel.Visible = false;
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
            Form ConvertDMS = new DMS_DecDeg();
            ConvertDMS.Show();
        }

        private void UseFixesForCoordinatesToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            InfoSection.UseFixes = useFixesForCoordinatesToolStripMenuItem.Checked;
        }

        private void IncludeNaviGraphDataToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            NGDataFolderLabel.Enabled = NGDataFolderTextBox.Enabled = NGDataFolderButton.Enabled =
                InfoSection.UseNaviGraph = includeNaviGraphDataToolStripMenuItem.Checked;
        }

        private void ARTCCComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            CenterARTCCButton.Enabled = UseARTCCAsSquareButton.Enabled = (ARTCCComboBox.SelectedIndex != -1);
        }

        private void AirportComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            CenterAPTButton.Enabled = AirportComboBox.SelectedIndex != -1;
        }

        private void CenterAPTButton_Click(object sender, EventArgs e)
        {
            if (AirportComboBox.SelectedIndex != -1)
            {
                double[] coords = SCTcommon.GetCoords(AirportComboBox.Text, "APT");
                InfoSection.CenterLatitude_Dec = coords[0];
                InfoSection.CenterLongitude_Dec = coords[1];
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

        private void UseARTCCAsSquareButton_Click(object sender, EventArgs e)
        {
            SquarebyARTCC();
            GetSquareAndOffset();
        }

        private void SelectedTabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateGridCount();
            DataGridView dgv = DGVinTab();
            QuickSearchLabel.Visible = QuickSearchTextBox.Visible = dgv.Rows.Count != 0;
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
    }
}
