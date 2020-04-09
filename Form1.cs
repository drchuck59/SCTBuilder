using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Data;
using System.IO;
using System.IO.Compression;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Generic;
using System.Drawing;
using MySqlX.XDevAPI.Common;
using System.Threading.Tasks;
using Squirrel;

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
            // Returns -1 if no init file, else returns last AIRAC used (redundant, but I'm lazy)
            int iniAIRAC = CycleInfo.ReadINIxml();
            // If there is no INI file, then there is no datafolder, so clean install (else)
            if (iniAIRAC != -1)
            {
                // We have a good ini file; try to load the FAA text data and update form
                UpdateEngineers();      // I was forgetting to display the engineers on the form
                if (LoadFAATextData() != -1) PostLoadTasks();
                // If we cannot upload the FAA text data using the INI file, get a new data set
                // Assuming the data downloads correctly, install in the various tables and update form
                else
                if (DownloadInstallFAAfiles())
                {
                    if (LoadFAATextData() != -1) PostLoadTasks();
                }
                else FAATextDataLoadFailed();

            }
            // If we cannot use the INI file, see if there is data set or get a new one
            // Assuming the data downloads correctly, install in the various tables and update form
            else
            {
                Msg = "It appears FEBU is running for the first time." + cr +
                    "Use the Update AIRAC button to retrieve the current FAA AIRAC.";
            }
            TestWriteSCT();                 // Update the output button
        }

        private void PostLoadTasks()
        {
            // Assumes we have a fresh FAA text folder and need to update
            UpdateCycleInfoOnForm();
            SetForm1Defaults();
        }

        private void FAATextDataLoadFailed()
        // Inform the user that something is wrong and they cannot use all features of program
        {
            Msg = "WARNING: The FAA text data did not load correctly." + cr +
                "You will be able to use basic tools and functions." + cr +
                "However, the program cannot provide advanced building tools" + cr +
                "that depend upon the FAA AIRAC data to function.";
            SCTcommon.SendMessage(Msg, MessageBoxIcon.Exclamation);
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
            Debug.WriteLine("SetForm1Defaults...");
            UpdateFolderMgt(toFolderMgt: false);
            if (LoadARTCCComboBox() != 0)           // Populates the combobox
                UpdateARTCCComboBox();              // Sets the combobox to the last Sponsor ARTCC
            else ClearARTCCComboBox();
            if (FilterBy.NorthLimit != 0)
                GetSquareAndOffset();                        // ... Use previously set limits
            if (LoadAirportComboBox() != 0)         // Using the desired filter format
                UpdateAirportComboBox();            // Set the combobox to the last Default Airport or top of list
            else ClearAirportComboBox();
            TestWriteSCT();
            gridViewToolStripButton.Enabled = true;
            CheckARTCCAsCenterButton();
            CheckAPTasCenterButton();
            GetChecked();
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
            DataView dvAPT = new DataView(APT)
            {
                RowFilter = filter,
                Sort = "FacilityID"
            };
            // Create the dataview, filtering by the selected class
            DataTable dtAPT = dvAPT.ToTable(true, "ID", "FacilityID");
            AirportComboBox.DisplayMember = "FacilityID";
            AirportComboBox.ValueMember = "ID";
            AirportComboBox.DataSource = dtAPT;
            result = AirportComboBox.Items.Count;
            dvAPT.Dispose();
            return result;
        }

        private void ClearAirportComboBox()
        {
            AirportComboBox.DataSource = null;
            InsertARTCCinSquareButton.Enabled = CenterARTCCButton.Enabled = false;
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
            string lastTab = "APTtabPage";
            SetChecked();
            SetSquareAndOffset();
            UpdatingLabel.Visible = true;
            Refresh();
            FilterBy.Method = "Square";
            string filter = SetFilter(); bool APTHasRows = false;
            if (APTsCheckBox.Checked || RWYsCheckBox.Checked || SIDsCheckBox.Checked || STARsCheckBox.Checked)
            {
                if (dgvAPT.Rows.Count == 0) APTHasRows = SelectTableItems(APT, filter) != 0;
                else APTHasRows = true;
            };
            if (SCTchecked.ChkAPT)
            {
                if (APTHasRows) lastTab = LoadAPTDataGridView();
            }
            if (SCTchecked.ChkRWY)
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
            if (SCTchecked.ChkVOR)
            {
                SelectTableItems(VOR, filter);
                lastTab = LoadVORGridView();
            }
            if (SCTchecked.ChkNDB)
            {
                SelectTableItems(NDB, filter);
                lastTab = LoadNDBGridView();
            }
            if (SCTchecked.ChkFIX)
            {
                SelectTableItems(FIX, filter);
                lastTab = LoadFIXGridView();
            }
            // AWYs must come after VOR, NDB and FIX
            if (SCTchecked.ChkAWY)
            {
                if (dgvVOR.Rows.Count == 0) SelectTableItems(VOR, filter);
                if (dgvNDB.Rows.Count == 0) SelectTableItems(NDB, filter);
                if (dgvFIX.Rows.Count == 0) SelectTableItems(FIX, filter);
                if (SelectAWYs() != 0) lastTab = LoadAWYDataGridView();
                else ClearDataGridView(dgvAWY);
            }
            if (SCTchecked.ChkARB) SelectTableItems(ARB, filter);
            if (SCTchecked.ChkSID)
                // APTs were selected above
                if (!APTHasRows)
                {
                    Msg = "Cannot select SIDs.  Mo airports with runways in the selected square.";
                    SCTcommon.SendMessage(Msg);
                }
                else
                {
                    if (dgvVOR.Rows.Count == 0) SelectTableItems(VOR, filter);
                    if (dgvNDB.Rows.Count == 0) SelectTableItems(NDB, filter);
                    if (dgvFIX.Rows.Count == 0) SelectTableItems(FIX, filter);
                    if (SelectSSD(true) != 0) lastTab = LoadSSDDataGridView(true);
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
                    if (dgvVOR.Rows.Count == 0) SelectTableItems(VOR, filter);
                    if (dgvNDB.Rows.Count == 0) SelectTableItems(NDB, filter);
                    if (dgvFIX.Rows.Count == 0) SelectTableItems(FIX, filter);
                    if (SelectSSD(false) != 0) lastTab = LoadSSDDataGridView(false);
                }
            }
            UpdatingLabel.Visible = false;
            SelectedTabControl.SelectedTab = SelectedTabControl.TabPages[lastTab];
            Refresh();
            TestWriteSCT();
        }

        private void ClearDataGridView(DataGridView dgv)
        {
            dgv.DataSource = null;
        }

        private int SelectTableItems(DataTable dt, string filter)
        {
            Console.WriteLine("Selecting " + dt.TableName);
            DataView dataView = new DataView(dt); int result;
            ClearSelected(dataView);
            dataView.RowFilter = filter;
            SetSelected(dataView);
            result = dataView.Count;
            dataView.Dispose();
            return result;
        }

        //private int SelectAPTs()
        //{
        //    string filter;
        //    if (SCTchecked.LimitAPT2ARTC)
        //    {
        //        FilterBy.Method = "ARTCC";
        //        filter = SetFilter();
        //    }
        //    else
        //    {
        //        FilterBy.Method = "Square";
        //        filter = SetFilter();
        //    }
        //    Console.WriteLine("Selecting airports using ");
        //    DataView dataView = new DataView(APT); int result;
        //    ClearSelected(dataView);
        //    dataView.RowFilter = filter;
        //    SetSelected(dataView);
        //    result = dataView.Count;
        //    dataView.Dispose();
        //    return result;
        //}

        private int SelectRWYs()
        {
            Console.WriteLine("Selecting RWYs");
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
            dvRWY.Dispose();
            dvAPT.Dispose();
            SelectedTabControl.SelectedTab = SelectedTabControl.TabPages["RWYtabPage"];
            return result;
        }

        private int SelectAWYs()
        {
            // Assumes VOR, NDB and FIXes have been selected
            Console.WriteLine("Selecting AWYs");
            string filter = "[Selected]"; int result;
            DataView dvVOR = new DataView(VOR, filter, "FacilityID", DataViewRowState.CurrentRows);
            DataView dvNDB = new DataView(NDB, filter, "FacilityID", DataViewRowState.CurrentRows);
            DataView dvFIX = new DataView(FIX, filter, "FacilityID", DataViewRowState.CurrentRows);
            // Create the table upon which the AWY table will be joined
            DataTable dtFixList = dvVOR.ToTable(true, "FacilityID", "Latitude", "Longitude");
            dtFixList.Merge(dvNDB.ToTable(true, "FacilityID", "Latitude", "Longitude"));
            dtFixList.Merge(dvFIX.ToTable(true, "FacilityID", "Latitude", "Longitude"));
            // Loop the result to set the selected AWYs based upon fixes selected
            DataView dvAWY = new DataView(AWY);
            ClearSelected(dvAWY);
            FilterBy.Method = "Square";
            dvAWY.RowFilter = SetFilter();
            foreach (DataRowView dataRow in dvAWY)
            {
                dataRow["Selected"] = true;
            }
            //foreach (DataRow SelectedFix in dtFixList.Rows)
            //{
            //    dvAWY.RowFilter = "NavAid = '" + SelectedFix["FacilityID"] + "'" ;
            //    foreach (DataRowView dataRow in dvAWY) { dataRow.Row["Selected"] = true; }
            //}
            result = dvAWY.Count;
            // Clean up
            dvAWY.Dispose();
            dvFIX.Dispose();
            dvNDB.Dispose();
            dvVOR.Dispose();
            SelectedTabControl.SelectedTab = SelectedTabControl.TabPages["AWYtabPage"];
            return result;
        }

        private string LoadAPTDataGridView()
        {
            DataView dvAPT = new DataView(APT)
            {
                RowFilter = "[Selected]",
                Sort = "FacilityID"
            };
            DataTable dtAPT = dvAPT.ToTable(true, "Selected", Conversions.ICOA("FacilityID"), "Name", "ID");
            dgvAPT.DataSource = dtAPT;
            dgvAPT.Columns[1].HeaderText = "Apt";
            dgvAPT.AutoResizeColumns();
            dvAPT.Dispose();
            return "APTtabPage";
        }

        private string LoadRWYDataGridView()
        {
            //  Facility ID and RWYs
            DataView dvRWY = new DataView(RWY)
            {
                RowFilter = "[Selected]",
                Sort = "FacilityID"
            };
            DataTable dtRWY = dvRWY.ToTable(true, "Selected", "FacilityName", "RwyIdentifier", "ID");
            dgvRWY.DataSource = dtRWY;
            dgvRWY.Columns[1].HeaderText = "Apt";
            dgvRWY.Columns[2].HeaderText = "Rwys";
            dgvRWY.AutoResizeColumns();
            dvRWY.Dispose();
            return "RWYtabPage";
        }

        private string LoadVORGridView()
        {
            DataView dvVOR = new DataView(VOR)
            {
                RowFilter = "[Selected]",
                Sort = "FacilityID"
            };
            DataTable dtVOR = dvVOR.ToTable(true, "Selected", "FacilityID", "Name");
            dgvVOR.DataSource = dtVOR;
            dgvVOR.Columns[1].HeaderText = "ID";
            dgvVOR.AutoResizeColumns();
            dvVOR.Dispose();
            return "VORtabPage";
        }

        private string LoadNDBGridView()
        {
            DataView dvNDB = new DataView(NDB)
            {
                RowFilter = "[Selected]",
                Sort = "FacilityID"
            };
            DataTable dtNDB = dvNDB.ToTable(true, "Selected", "FacilityID", "Name");
            dgvNDB.DataSource = dtNDB;
            dgvNDB.Columns[1].HeaderText = "ID";
            dgvNDB.AutoResizeColumns();
            dvNDB.Dispose();
            return "NDBtabPage";
        }

        private string LoadFIXGridView()
        {
            DataView dvFIX = new DataView(FIX)
            {
                RowFilter = "[Selected]",
                Sort = "FacilityID"
            };
            DataTable dtFIX = dvFIX.ToTable(true, "Selected", "FacilityID", "Use");
            dgvFIX.DataSource = dtFIX;
            dgvFIX.Columns[1].HeaderText = "ID";
            dgvFIX.AutoResizeColumns();
            dvFIX.Dispose();
            return "FIXtabPage";
        }

        private string LoadAWYDataGridView()
        {
            DataView dvAWY = new DataView(AWY)
            {
                RowFilter = "[Selected]",
                Sort = "AWYID, Sequence"
            };
            DataTable dtAWY = dvAWY.ToTable(false, "AWYID", "NAVAID", "MinEnrAlt", "MaxAuthAlt", "MinObstClrAlt");
            dgvAWY.DataSource = dtAWY;
            dgvAWY.Columns[0].HeaderText = "ID";
            dgvAWY.Columns[1].HeaderText = "NavAid";
            dgvAWY.Columns[2].HeaderText = "MEA";
            dgvAWY.Columns[3].HeaderText = "MAA";
            dgvAWY.Columns[4].HeaderText = "MOCA";
            dvAWY.Dispose();
            return "AWYtabPage";
        }

        private int SelectSSD(bool isSID)
        {
            // This one is different and gets [Selected] based upon included APTs
            // To make this one faster, use a DataView rather than the dataviewgrid
            // First, get all the airports affected (It's easier to just make a new one)
            var SSDID = new List<string>(); string IDfilter;
            // Clear only the SID or STAR selections
            DataView dvSSD = new DataView(SSD)
            {
                RowFilter = "[IsSID] = " + isSID
            };
            ClearSelected(dvSSD);
            Application.DoEvents();
            // Get 'selected' airports
            DataView dvAPT = new DataView(APT)
            {
                RowFilter = "[SELECTED]"
            };
            //// Try this LINQ method
            //DataTable dtResult = new DataTable();
            //var sql =
            //    from DataRowView SSDdrv in dvSSD
            //    where SSDdrv.Row.Field<string>("FixType") == "'AA'"
            //    join DataRowView APTdrv in dvAPT
            //    on SSDdrv.Row.Field<string>("NavAid") equals APTdrv.Row.Field<string>("FacilityID") into lj
            //    select new
            //    {
            //        SSID = SSDdrv.Row.Field<string>("ID").ToString()
            //    };
            //Console.WriteLine(sql.ToList().Count);


            //****************************************************************************
            // Build a list of uniques Facility IDs
            DataTable dtAirports = dvAPT.ToTable(true, "FacilityID");
            string AAfilter = "([FixType] = 'AA') AND ([IsSID] = " + isSID + ") AND ";
            // Loop the list to get SID/STAR IDs for those airports
            Console.WriteLine("Looping airports");
            foreach (DataRow dtAptRow in dtAirports.AsEnumerable())
            {
                string FacIDfilter = "([NavAid] = '" + dtAptRow["FacilityID"].ToString() + "')";
                dvSSD.RowFilter = AAfilter + FacIDfilter;
                DataTable IDdata = dvSSD.ToTable(true, "ID");
                foreach (DataRow data in IDdata.AsEnumerable())
                {
                    SSDID.Add(data["ID"].ToString());
                }
                Application.DoEvents();
            }
            // Make the list IEnumerable and distinct values
            IEnumerable<string> distinctSSDID = SSDID.Distinct();
            // Run that list of SID/STAR IDs to mark the rows 'selected'
            Console.WriteLine("Selecting " + distinctSSDID.Count() + " SSD ");
            foreach (var item in distinctSSDID)
            {
                IDfilter = "[ID] = '" + item.ToString() + "'";
                dvSSD.RowFilter = IDfilter;
                SetSelected(dvSSD);
            }
            // *****************************************************************************
            Console.WriteLine("Done ");
            return distinctSSDID.Count();
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
                dgvSID.Columns[1].HeaderText = "Apt";
                dgvSID.Columns[2].HeaderText = "SID";
                dgvSID.Columns[3].HeaderText = "Name";
                dgvSID.Columns[4].Visible = false;      // This is for later use
                dgvSID.Sort(dgvSID.Columns["NavAid"], ListSortDirection.Ascending);
                dvSSD.Dispose();
                SelectedTabControl.SelectedTab = SelectedTabControl.TabPages["SIDtabPage"];
            }
            else
            {
                DataTable dtSTAR = dvSSD.ToTable(true, "Selected", "NavAid", "TransCode", "TransName", "ID");
                dgvSTAR.DataSource = dtSTAR;
                dgvSTAR.Columns[1].HeaderText = "Apt";
                dgvSTAR.Columns[2].HeaderText = "STAR";
                dgvSTAR.Columns[3].HeaderText = "Name";
                dgvSTAR.Columns[4].Visible = false;      // This is for later use
                dgvSTAR.Sort(dgvSTAR.Columns["NavAid"], ListSortDirection.Ascending);
                SelectedTabControl.SelectedTab = SelectedTabControl.TabPages["STARtabPage"];
                dvSSD.Dispose();
                dgvSTAR.Focus();
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
            foreach (DataRowView row in dv)
            {
                row["Selected"] = true;
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
        private void UpdateSquarebyARTCC()
        {
            // Updates the values of the filter square based on the ARTCC selected
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
            EastLimitTextBox.Text = Conversions.DecDeg2SCT(LongEast, false);
            WestLimitTextBox.Text = Conversions.DecDeg2SCT(LongWest, false);
            NorthLimitTextBox.Text = Conversions.DecDeg2SCT(LatNorth, true);
            SouthLimitTextBox.Text = Conversions.DecDeg2SCT(LatSouth, true);
            SetSquareAndOffset();
            dataview.Dispose();
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
            string result = GetFolderPath(textBox.Text, dialogTitle);
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

        private string GetFolderPath(string selectedPath, string dialogTitle)
        {
            FolderBrowserDialog fBD = new FolderBrowserDialog();
            string result = string.Empty;
            // Set default folder to start
            fBD.SelectedPath = selectedPath;
            fBD.Description = dialogTitle;
            if (selectedPath.Length != 0) fBD.SelectedPath = selectedPath;
            else fBD.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            // Get user's desired folder
            DialogResult dialogResult = fBD.ShowDialog();
            if (dialogResult == DialogResult.OK)
                result = fBD.SelectedPath;
            fBD.Dispose();
            return result;
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
                    SCTtoolStripButton.ToolTipText = "No SCTsection checked or [SCTsection] has no items. Check grid view.";
            }
            else
            {
                SCTtoolStripButton.ToolTipText = "Required items missing in [Info] section";
            }
            result = InfoPopulated & CheckedItemsHaveSelections;
            SCTtoolStripButton.Enabled = result;
            return result;
        }

        private void ChkbxShowAll_CheckedChanged(object sender, EventArgs e)
        {
            ToggleSelectedDGV(dgvAPT);
            ToggleSelectedDGV(dgvVOR);
            ToggleSelectedDGV(dgvNDB);
            ToggleSelectedDGV(dgvFIX);
            ToggleSelectedDGV(dgvRWY);
            ToggleSelectedDGV(dgvAWY);
            ToggleSelectedDGV(dgvSID);
            ToggleSelectedDGV(dgvSTAR);
            UpdateGridCount();
        }

        private void ToggleSelectedDGV(DataGridView dgv)
        {
            if (chkbxShowAll.Checked)
                (dgv.DataSource as DataTable).DefaultView.RowFilter = "";
            else
                (dgv.DataSource as DataTable).DefaultView.RowFilter = "[Selected]";
        }

        private void TxtDataFolder_Validated(object sender, EventArgs e)
        {
            if (FAADataFolderTextBox.TextLength > 0)
            {
                LoadFAATextData();
                SetForm1Defaults();
                // LoadFixGrid();
                FolderMgt.DataFolder = FAADataFolderTextBox.Text;
            }
            if (FAADataFolderTextBox.Text != FolderMgt.DataFolder)
                CmdDataFolder_Click(sender, e);
            gridViewToolStripButton.Enabled = true;
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
                    CycleInfoLabel.Text = "Choose folder containing FAA text data";
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
            CenterARTCCButton.Enabled = SquareSelected();
        }

        private void CheckARTCC2SquareButton()
        {
            InsertARTCCinSquareButton.Enabled = SquareSelected();
        }
        private void AirportComboBox_Validated(object sender, EventArgs e)
        {
            if (AirportComboBox.SelectedIndex != -1)
            {
                InfoSection.DefaultAirport = AirportComboBox.Text.ToString();
            }
            CheckAPTasCenterButton();
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
                if (!APTsCheckBox.Checked)
                {
                    Msg = "In order to select SIDs, airports and NavAids will be selected.";
                    SendMessage(Msg);
                    APTsCheckBox.Checked = true;
                    VORsCheckBox.Checked = true;
                    NDBsCheckBox.Checked = true;
                    FIXesCheckBox.Checked = true;
                }
            }
            CheckPreviewButton();
        }

        private void STARsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            STARNameCheckBox.Enabled = STARsCheckBox.Checked;
            if (STARsCheckBox.Checked)
            {
                if (!APTsCheckBox.Checked)
                {
                    Msg = "In order to select STARs, airports and NavAids will be selected.";
                    SendMessage(Msg);
                    APTsCheckBox.Checked = true;
                    VORsCheckBox.Checked = true;
                    NDBsCheckBox.Checked = true;
                    FIXesCheckBox.Checked = true;
                }
            }
            CheckPreviewButton();
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
                    GetSquareAndOffset();
                }
            }
            CheckPreviewButton();
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
                    GetSquareAndOffset();
                }
            }
            CheckPreviewButton();
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
                    SetSquareAndOffset();
                }
            }
            CheckPreviewButton();
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
                    SetSquareAndOffset();
                }
            }
            CheckPreviewButton();
            CheckARTCC2SquareButton();
            CheckARTCCAsCenterButton();
        }

        private void CheckPreviewButton()
        {
            PreviewButton.Enabled = SquareSelected() && SectionSelected();
        }

        private void NWByFIXButton_Click(object sender, EventArgs e)
        {
            if (FixListDataGridView.SelectedRows.Count != 0)
            {
                InfoSection.NorthSquare = Convert.ToDouble(FixListDataGridView.SelectedRows[0].Cells["Latitude"].Value);
                InfoSection.WestSquare = Convert.ToDouble(FixListDataGridView.SelectedRows[0].Cells["Longitude"].Value);
                GetSquareAndOffset();
            }
            CheckPreviewButton();
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
            CheckPreviewButton();
            CheckARTCC2SquareButton();
            CheckARTCCAsCenterButton();
        }

        private void ARTCCCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            ARTCChighCheckBox.Enabled = ARTCClowCheckBox.Enabled = ARTCCCheckBox.Checked;
            CheckPreviewButton();
        }

        private void SetAllMarginsButton_Click(object sender, EventArgs e)
        {
            NorthMarginNumericUpDown.Value = SouthMarginNumericUpDown.Value =
                EastMarginNumericUpDown.Value = WestMarginNumericUpDown.Value =
                AllMarginsNumericUpDown.Value;
        }

        private void UpdateAIRACbutton_Click(object sender, EventArgs e)
        {
            UpdateAIRACbutton.Visible = false;
            DownloadInstallFAAfiles();
            PostLoadTasks();
            UpdateAIRACbutton.Visible = true;
            TestWriteSCT();
        }

        private string VerifyExtractPath()
        {
            // returns the path to extract the data (the data folder)
            Msg = "Select new / Verify current data folder to contain FAA data subfolder";
            string extractPath = GetFolderPath(FolderMgt.DataFolder, Msg);
            if (extractPath.Length == 0)
            {
                Msg = "No folder selected to save FAA text files. Update aborted.";
                SCTcommon.SendMessage(Msg, MessageBoxIcon.Warning);
            }
            else
            {
                FAADataFolderTextBox.Text = extractPath;
                UpdateFolderMgt(toFolderMgt: true);
            }
            return extractPath;
        }

        private int CleanDataFolder(string newCycleDate)
        {
            // See if the user already has the same data - ask if user wants to rewrite it
            // If old data, confirm the user wants to overwrite it
            // if NO data, do the install without query
            // Start by identifying the datafolder if needed
            bool Continue = false; string Msg; string filter = "*28DaySubscription*";

            // Verify the data folder holding the extraction data
            FolderMgt.DataFolder = VerifyExtractPath();
            if (FolderMgt.DataFolder.Length == 0) return 99;  // user cancelled operation

            // Search the datafolder for duplicate data subfolders and remove them
            string[] dirs = Directory.GetDirectories(@FolderMgt.DataFolder, filter, SearchOption.TopDirectoryOnly);
            Console.WriteLine("Dirs found: " + dirs.Length);
            foreach (string dir in dirs)
            {
                if (dir.IndexOf(newCycleDate) != -1)
                {
                    Msg = "You already have the current dataset" + cr +
                        "Cycle date: " + newCycleDate + cr +
                        "Are you sure that you want to reinstall it?";
                    DialogResult dialogResult = SCTcommon.SendMessage(Msg, MessageBoxIcon.Question, MessageBoxButtons.YesNo);
                    Continue = (dialogResult == DialogResult.Yes);
                }
                else
                if (dir.IndexOf(filter) != 0)
                {
                    Msg = "This will OVERWRITE your current FAA AIRAc (Cycle date " + newCycleDate +")." + cr +
                        "If you wish, click NO and move the folder " + cr +
                        dir.Trim() + cr +
                        "outside of your main data folder." + cr +
                        "Otherwise, click YES to remove this data and add the new AIRAC." + cr + cr +
                        "Are you sure that you want to overwrite your data with the current AIRAC?";
                    DialogResult dialogResult = SCTcommon.SendMessage(Msg, MessageBoxIcon.Question, MessageBoxButtons.YesNo);
                    Continue = (dialogResult == DialogResult.Yes);
                }
                if (Continue)
                {
                    Directory.Delete(path: dir, recursive: true);
                }
                Continue = false;               // Reset the flag or you might delete ALL the dirs!
            }
            // Need to verify that there are no subscription folders in the data folder
            List<string> dirsList = new List<string>
                (Directory.EnumerateFiles(FolderMgt.DataFolder, filter, SearchOption.AllDirectories));
            return (dirsList.Count);        // Accpetable values are 0 (clean) or 1 (kept old data)
        }

        private bool DownloadInstallFAAfiles()
        {
            // Downloads the FAA text files and extracts into a subfolder of the user's data folder
            // Removes any like folders
            // Returns bool for success or failure
            string downloadsPath; DateTime newDate; 

            int newAIRAC = CycleInfo.AIRACfromDate(DateTime.Today, Save2CycleInfo: false);
            newDate = CycleInfo.CycleDateFromAIRAC(newAIRAC, Save2CycleInfo: false);
            string CycleDate = newDate.ToString("yyyy'-'MM'-'dd");

            // Need an empty datafolder (0), or abort for some reason
            if (CleanDataFolder(CycleDate) != 0) return false;
            if (FolderMgt.DataFolder.Length == 0) return false;

            // Now we can proceed
            // Set up values for the download;
            string URL = "https://nfdc.faa.gov/webContent/28DaySub/28DaySubscription_Effective_" + CycleDate + ".zip";
            downloadsPath = KnownFolders.GetPath(KnownFolder.Downloads) + "\\28DaySubscription_Effective_" + CycleDate + ".zip";

            // Show messages in the CycleInfoLabel
            UpdateCycleInfoOnForm(visible: true, "Downloading FAA data...");
            WebClient wc = new WebClient();
            wc.DownloadFile(new System.Uri(URL), downloadsPath);
            wc.Dispose();

            // Unless I messed up, there cannot be a data subdirectory by this name, so create it
            UpdateCycleInfoOnForm(visible: true, "Extracting files to data folder...");
            DirectoryInfo di = Directory.CreateDirectory(FolderMgt.DataFolder + "\\28DaySubscription_Effective_" + CycleDate + "\\");
            string extractPath = di.FullName;
            ZipFile.ExtractToDirectory(downloadsPath, extractPath);
            return true;
        }

        private void CheckAPTasCenterButton()
        {
             APTasCenterButton.Enabled = AirportComboBox.SelectedIndex != -1;
        }

        private void APTasCenterButton_Click(object sender, EventArgs e)
        {
            double[] coords = SCTcommon.GetCoords(AirportComboBox.Text, "APT");
            InfoSection.CenterLatitude_Dec = coords[0];
            InfoSection.CenterLongitude_Dec = coords[1];
            CenterLatTextBox.Text = InfoSection.CenterLatitude_SCT;
            CenterLonTextBox.Text = InfoSection.CenterLongitude_SCT;
            MagVarTextBox.Text = InfoSection.MagneticVariation.ToString("0.00");
        }

        private void CenterARTCCButton_Click(object sender, EventArgs e)
        {
            SetFilterBy();
            InfoSection.CenterLatitude_Dec = (FilterBy.NorthLimit + FilterBy.SouthLimit)/ 2;
            CenterLatTextBox.Text = InfoSection.CenterLatitude_SCT;
            InfoSection.CenterLongitude_Dec = (FilterBy.WestLimit + FilterBy.EastLimit) / 2;
            CenterLonTextBox.Text = InfoSection.CenterLongitude_SCT;
            MagVarTextBox.Text = InfoSection.MagneticVariation.ToString();
        }

        private void InsertARTCCinSquareButton_Click(object sender, EventArgs e)
        {
            UpdateSquarebyARTCC();
        }

        private void FixListDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            NWByFIXButton.Enabled = SEByFIXButton.Enabled = FixListDataGridView.SelectedRows.Count != 0;
        }

        private void APTsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            CheckPreviewButton();
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
            CheckPreviewButton();
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
            CheckPreviewButton();
        }

        private void VORsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            CheckPreviewButton();
        }

        private void NDBsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            CheckPreviewButton();
        }

        private void FIXesCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            CheckPreviewButton();
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
            SCToutput.WriteSCT();
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
    }
}
