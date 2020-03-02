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
using System.Data.Sql;

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
        static public DataSet SCT = new SCTdata();
        string cr = Environment.NewLine; string Msg;

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
                else if (DownloadInstallFAAfiles())
                    if (LoadFAATextData() != -1) PostLoadTasks();
                    else FAATextDataLoadFailed();
            }
            // If we cannot use the INI file, get a new data set
            // Assuming the data downloads correctly, install in the various tables and update form
            else if (DownloadInstallFAAfiles())
                if (LoadFAATextData() != -1) PostLoadTasks();
                else FAATextDataLoadFailed();
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
            UpdateSquarebyARTCC();                  // Set the parameters of the square to the ARTCC limits
            if (LoadAirportComboBox() != 0)         // Using the desired filter format
                UpdateAirportComboBox();            // Set the combobox to the last Default Airport or top of list
            else ClearAirportComboBox();
            SCTtoolStripButton.Enabled = TestWriteSCT();
            gridViewToolStripButton.Enabled = true;
            CheckARTCCAsCenterButton();
            CheckAPTasCenterButton();
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
            UpdatingLabel.Visible = true;
            FilterBy.Method = "Square";
            string filter = SetFilter(); bool APTHasRows = false;
            if (APTsCheckBox.Checked || RWYsCheckBox.Checked || SIDsCheckBox.Checked || STARsCheckBox.Checked)
            {
                APTHasRows = SelectTableItems(APT, filter) != 0;
            };
            if (APTsCheckBox.Checked)
            {
                if (APTHasRows) LoadAPTDataGridView();
            }
            if (RWYsCheckBox.Checked)
                if (APTHasRows)
                {
                    if (SelectRWYs() != 0) LoadRWYDataGridView();
                    else ClearDataGridView(dgvRWY);
                }
                else
                {
                    Msg = "Cannot select Runways.  Mo airports with runways in the selected square.";
                    SCTcommon.SendMessage(Msg);
                }
            if (VORsCheckBox.Checked)
            {
                SelectTableItems(VOR, filter);
                LoadVORGridView();
            }
            if (NDBsCheckBox.Checked) 
            {
                SelectTableItems(NDB, filter);
                LoadNDBGridView();
            }
            if (FIXesCheckBox.Checked)
            {
                SelectTableItems(FIX, filter);
                LoadFIXGridView();
            }
            // AWYs must come after VOR, NDB and FIX
            if (AWYsCheckBox.Checked)
            {
                if (!VORsCheckBox.Checked) SelectTableItems(VOR, filter);
                if (!NDBsCheckBox.Checked) SelectTableItems(NDB, filter);
                if (!FIXesCheckBox.Checked) SelectTableItems(FIX, filter);
                if (SelectAWYs() != 0) LoadAWYDataGridView();
                else ClearDataGridView(dgvAWY);
            }
            if (ARTCCCheckBox.Checked) SelectTableItems(ARB, filter);
            if (SIDsCheckBox.Checked)
                if (SelectSSD(isSID: true) != 0) LoadSSDDataGridView(true);
            if (STARsCheckBox.Checked)
                if (SelectSSD(isSID: false) != 0) LoadSSDDataGridView(false);
            UpdatingLabel.Visible = false;
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
            return result;
        }

        private void LoadAPTDataGridView()
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
        }

        private void LoadRWYDataGridView()
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
        }

        private void LoadVORGridView()
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
        }

        private void LoadNDBGridView()
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
        }

        private void LoadFIXGridView()
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
        }

        private void LoadAWYDataGridView()
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
        }

        private int SelectSSD(bool isSID)
        {
            // This one is different and gets [Selected] based upon included APTs
            // To make this one faster, use a DataView rather than the dataviewgrid
            // First, get all the airports affected (It's easier to just make a new one)
            var SSDID = new List<string>(); string IDfilter;
            DataView dvSSD = new DataView(SSD);
            DataView dvAirports = new DataView(APT)
            {
                RowFilter = "[SELECTED]"
            };
            // Build a table of the (unique) airports
            DataTable dtAirports = dvAirports.ToTable(true, "FacilityID");

            // Use the SELECTED airports to find the SID/STARs           
            ClearSelected(dvSSD);
            // Build a list of SID/STAR ID values from the filter
            string AAfilter = "([FixType] = 'AA') AND ([isSID] = " + isSID + ") AND (";
            foreach (DataRow dataRow in dtAirports.AsEnumerable())
            {
                string FacIDfilter = "[NavAid] = '" + dataRow[0].ToString() + "')";
                dvSSD.RowFilter = AAfilter + FacIDfilter;
                DataTable IDdata = dvSSD.ToTable(true, "ID");
                foreach (DataRow data in IDdata.AsEnumerable())
                {
                    SSDID.Add(data["ID"].ToString());
                }
            }
            // Apply the selected flag to those SIDs/STARs in the list
            IEnumerable<string> distinctSSDID = SSDID.Distinct();
            foreach (var item in distinctSSDID)
            {
                IDfilter = "[ID] = '" + item.ToString() + "'";
                dvSSD.RowFilter = IDfilter;
                SetSelected(dvSSD);
            }
            return distinctSSDID.Count();
        }

        private void LoadSSDDataGridView(bool isSID)
        {
            // Assumes the SID/STARs have been selected
            DataView dvSSD = new DataView(SSD)
            {
                RowFilter = "[SELECTED] AND [isSSD] = " + isSID,
                Sort = "SSDcode"
            };
            if (isSID)
            {
                DataTable dtSID = dvSSD.ToTable(true, "SSDcode", "SSDname");
                dgvSID.DataSource = dtSID;
                dgvSID.Sort(dgvSID.Columns["Sequence"], ListSortDirection.Ascending);
                dgvSID.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCellsExceptHeader;
                dvSSD.Dispose();
            }
            else
            {
                DataTable dtSTAR = dvSSD.ToTable(true, "SSDcode", "SSDname");
                dgvSTAR.DataSource = dtSTAR;
                dgvSTAR.Sort(dgvSID.Columns["Sequence"], ListSortDirection.Ascending);
                dgvSTAR.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCellsExceptHeader;
                dvSSD.Dispose();
            }
        }


        private void SetdgvSUA()
        {

            DataView dvSUA = new DataView(SUA);
            foreach (DataRowView rowView in dvSUA) rowView["Selected"] = false;
            dvSUA.RowFilter = SetSUAfilter();
            foreach (DataRowView rowView in dvSUA) rowView["Selected"] = true;
            // DataTable dtgv = dvSUA.ToTable(true, "Selected", "Category", "Name", "ID");
        }

        private string SetSUAfilter()
        {
            string Filter = " ( ([Latitude_North] <= " + FilterBy.NorthLimit.ToString() + ")" +
                            " AND  ([Latitude_South] >= " + FilterBy.SouthLimit.ToString() + ")" +
                            " AND ([Longitude_East] <= " + FilterBy.EastLimit.ToString() + ")" +
                            " AND ([Longitude_West] >= " + FilterBy.WestLimit.ToString() + ") )";
            string AddlFilter = string.Empty;
            if (SCTchecked.ChkSUA_ClassB)
            {
                AddlFilter += "([Category] = 'B')";
            }
            if (SCTchecked.ChkSUA_ClassC)
            {
                if (AddlFilter.Length != 0) AddlFilter += " OR ";
                AddlFilter += "([Category] = 'C')";
            }
            if (SCTchecked.ChkSUA_ClassD)
            {
                if (AddlFilter.Length != 0) AddlFilter += " OR ";
                AddlFilter += "([Category] = 'D')";
            }
            if (SCTchecked.ChkSUA_Restricted)
            {
                if (AddlFilter.Length != 0) AddlFilter += " OR ";
                AddlFilter += "([Category] = 'RESTRICTED')";
            }
            if (SCTchecked.ChkSUA_Prohibited)
            {
                if (AddlFilter.Length != 0) AddlFilter += " OR ";
                AddlFilter += "([Category] = 'PROHIBITED')";
            }
            if (SCTchecked.ChkSUA_Danger)
            {
                if (AddlFilter.Length != 0) AddlFilter += " OR ";
                AddlFilter += "([Category] = 'DANGER')";
            }
            if (AddlFilter.Length != 0)
                AddlFilter = "AND ( " + AddlFilter + " )";
            Console.WriteLine(Filter);
            Console.WriteLine(AddlFilter);
            return Filter + AddlFilter;
        }

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
                    else
                    {
                        Msg = "SEVERE ERROR selecting ARTCC as filter in SetFilter.  Contact developer.";
                        SCTcommon.SendMessage(Msg, MessageBoxIcon.Exclamation);
                        CmdExit_Click(null, null);
                    }
                    break;
                case "Square":
                    result = result +
                        " ( ([Latitude] <= " + FilterBy.NorthLimit.ToString() + ")" +
                        " AND  ([Latitude] >= " + FilterBy.SouthLimit.ToString() + ")" +
                        " AND ([Longitude] <= " + FilterBy.EastLimit.ToString() + ")" +
                        " AND ([Longitude] >= " + FilterBy.WestLimit.ToString() + ") )";
                    break;
            }
            return result;
        }

        private void PictureBox2_Click(object sender, EventArgs e)
        {
            Process.Start("https://zjxartcc.org");
        }
        private void AsstFacilityEngineerTextBox_Validated(object sender, EventArgs e)
        {
            // Note - this textbox does not need "Validating"
            bool ToClass = true;
            InfoSection.AsstFacilityEngineer = AsstFacilityEngineerTextBox.Text;
            SCTtoolStripButton.Enabled = TestWriteSCT();
            UpdateEngineers(ToClass);
        }

        private void FacilityEngineerTextBox_Validated(object sender, EventArgs e)
        {
            bool ToClass = true;
            InfoSection.FacilityEngineer = FacilityEngineerTextBox.Text;
            UpdateEngineers(ToClass);
            SCTtoolStripButton.Enabled = TestWriteSCT();
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
            FilterBy.NorthLimit = LatNorth;
            FilterBy.SouthLimit = LatSouth;
            FilterBy.EastLimit = LongEast;
            FilterBy.WestLimit = LongWest;
            EastLimitTextBox.Text = Conversions.DecDeg2SCT(LongEast, false);
            WestLimitTextBox.Text = Conversions.DecDeg2SCT(LongWest, false);
            NorthLimitTextBox.Text = Conversions.DecDeg2SCT(LatNorth, true);
            SouthLimitTextBox.Text = Conversions.DecDeg2SCT(LatSouth, true);
            dataview.Dispose();
        }

        private void CmdOutputFolder_Click(object sender, EventArgs e)
        {
            txtOutputFolder.Text = UpdateFolder(txtOutputFolder, "Select folder to save SCT files");
            if (txtOutputFolder.TextLength != 0)
            {
                UpdateFolderMgt(toFolderMgt: true);
            }
            SCTtoolStripButton.Enabled = TestWriteSCT();
        }

        private void CmdDataFolder_Click(object sender, EventArgs e)
        {
            DataFolderTextBox.Text = UpdateFolder(DataFolderTextBox, "Select FAA AIRAC data folder");
            if (DataFolderTextBox.TextLength != 0)
            {
                UpdateFolderMgt(toFolderMgt: true);
            }
            SCTtoolStripButton.Enabled = TestWriteSCT();
        }

        private string UpdateFolder(TextBox textBox, string dialogTitle)
        {
            // This calling routine allows other classes to update a folder
            string result;
            result = GetFolderPath(textBox.Text, dialogTitle);
            return result;
        }

        private void UpdateFolderMgt(bool toFolderMgt)
        {
            if (toFolderMgt)
            {
                if (FolderMgt.DataFolder != DataFolderTextBox.Text)
                    FolderMgt.DataFolder = DataFolderTextBox.Text;
                if (FolderMgt.OutputFolder != txtOutputFolder.Text)
                    FolderMgt.OutputFolder = txtOutputFolder.Text;
            }
            else
            {
                if (FolderMgt.DataFolder != DataFolderTextBox.Text)
                    DataFolderTextBox.Text = FolderMgt.DataFolder;
                if (FolderMgt.OutputFolder != txtOutputFolder.Text)
                    txtOutputFolder.Text = FolderMgt.OutputFolder;
            }
        }

        private string GetFolderPath(string selectedPath, string dialogTitle)
        {
            FolderBrowserDialog fBD = new FolderBrowserDialog();
            string result;
            // Set default folder to start
            fBD.SelectedPath = selectedPath;
            fBD.Description = dialogTitle;
            if (selectedPath.Length != 0) fBD.SelectedPath = selectedPath;
            else fBD.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            // Get user's desired folder
            fBD.ShowDialog();
            result = fBD.SelectedPath;
            fBD.Dispose();
            return result;
        }

        private void CmdWriteSCT_Click(object sender, EventArgs e)
        {
            SCToutput.WriteSCT();
        }

        private bool TestWriteSCT()
        {
            return !(string.IsNullOrEmpty(FolderMgt.DataFolder.ToString()) ||
                string.IsNullOrEmpty(FolderMgt.OutputFolder.ToString()) ||
                string.IsNullOrEmpty(InfoSection.SponsorARTCC.ToString()) ||
                string.IsNullOrEmpty(InfoSection.DefaultAirport) ||
                InfoSection.CenterLatitude_Dec == 0 ||
                InfoSection.CenterLongitude_Dec == 0 ||
                string.IsNullOrEmpty(InfoSection.MagneticVariation.ToString()));
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
            if (DataFolderTextBox.TextLength > 0)
            {
                LoadFAATextData();
                SetForm1Defaults();
                // LoadFixGrid();
                FolderMgt.DataFolder = DataFolderTextBox.Text;
            }
            if (DataFolderTextBox.Text != FolderMgt.DataFolder)
                CmdDataFolder_Click(sender, e);
            gridViewToolStripButton.Enabled = true;
            SCTtoolStripButton.Enabled = TestWriteSCT();
        }

        private void TxtDataFolder_Validating(object sender, CancelEventArgs e)
        {
            Console.WriteLine("TxtDataFolder_Validating...");
            if (DataFolderTextBox.TextLength > 0)
            {
                if (Directory.Exists(DataFolderTextBox.Text))
                {
                    FolderMgt.DataFolder = DataFolderTextBox.Text;
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
            CycleInfo.WriteINIxml();
            Application.Exit();
        }

        private void ARTCCComboBox_Validated(object sender, EventArgs e)
        {
            InfoSection.SponsorARTCC = ARTCCComboBox.Text.ToString();
            if (LoadAirportComboBox() != 0)
                UpdateAirportComboBox();
            else ClearAirportComboBox();
            SCTtoolStripButton.Enabled = TestWriteSCT();
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
            SCTtoolStripButton.Enabled = TestWriteSCT();
        }

        private void LocalSectors_Click(object sender, EventArgs e)
        {
            Console.WriteLine("LocalSectors_Click...");
            if (ReadNASR.FillLocalSectors())
                SCToutput.WriteLS_SID(LocalSector);
        }

        private void TxtOutputFolder_Validated(object sender, EventArgs e)
        {
            if (txtOutputFolder.TextLength > 0)
            {
                FolderMgt.OutputFolder = txtOutputFolder.Text;
            }
            SCTtoolStripButton.Enabled = TestWriteSCT();
        }

        private void SetChecked()
        {
            Console.WriteLine("SetChecked...");
            SCTchecked.ChkAPT = APTsCheckBox.Checked;
            SCTchecked.ChkARB = ARTCCCheckBox.Checked;
            SCTchecked.ChkAWY = AWYsCheckBox.Checked;
            SCTchecked.ChkFIX = FIXesCheckBox.Checked;
            SCTchecked.ChkNDB = NDBsCheckBox.Checked;
            SCTchecked.ChkRWY = RWYsCheckBox.Checked;
            SCTchecked.ChkSSD = SIDsCheckBox.Checked;
            SCTchecked.ChkVOR = VORsCheckBox.Checked;
            SCTchecked.ChkSSDname = SIDNameCheckBox.Checked;
            SCTchecked.ChkSUA_ClassB = SUA_ClassBCheckBox.Checked;
            SCTchecked.ChkSUA_ClassC = SUA_ClassCCheckBox.Checked;
            SCTchecked.ChkSUA_ClassD = SUA_ClassDCheckBox.Checked;
            SCTchecked.ChkSUA_Danger = SUA_DangerCheckBox.Checked;
            SCTchecked.ChkSUA_Prohibited = SUA_ProhibitedCheckBox.Checked;
            SCTchecked.ChkSUA_Restricted = SUA_RestrictedCheckBox.Checked;
        }

        private void GetChecked()
        {
            Console.WriteLine("GetChecked...");
            APTsCheckBox.Checked = SCTchecked.ChkAPT;
            ARTCCCheckBox.Checked = SCTchecked.ChkARB;
            AWYsCheckBox.Checked = SCTchecked.ChkAWY;
            FIXesCheckBox.Checked = SCTchecked.ChkFIX;
            NDBsCheckBox.Checked = SCTchecked.ChkNDB;
            RWYsCheckBox.Checked = SCTchecked.ChkRWY;
            SIDsCheckBox.Checked = SCTchecked.ChkSSD;
            VORsCheckBox.Checked = SCTchecked.ChkVOR;
            SIDNameCheckBox.Checked = SCTchecked.ChkSSDname;
            SUA_ClassBCheckBox.Checked = SCTchecked.ChkSUA_ClassB;
            SUA_ClassCCheckBox.Checked = SCTchecked.ChkSUA_ClassC;
            SUA_ClassDCheckBox.Checked = SCTchecked.ChkSUA_ClassD;
            SUA_DangerCheckBox.Checked = SCTchecked.ChkSUA_Danger;
            SUA_ProhibitedCheckBox.Checked = SCTchecked.ChkSUA_Prohibited;
            SUA_RestrictedCheckBox.Checked = SCTchecked.ChkSUA_Restricted;
        }

        private void ChkSSDs_CheckedChanged(object sender, EventArgs e)
        {
            SIDNameCheckBox.Enabled = SIDsCheckBox.Checked;
            CheckPreviewButton();
        }

        private void STARsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            STARsNameCheckBox.Enabled = STARsCheckBox.Checked;
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
                else if (NLat <= SLat)
                {
                    Msg = "Cannot place SE position north of NW position!";
                    SCTcommon.SendMessage(Msg);
                    SouthLimitTextBox.Text = string.Empty;
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
                else if (NLat <= SLat)
                {
                    Msg = "Cannot place NW position south of SE position!";
                    SCTcommon.SendMessage(Msg);
                    NorthLimitTextBox.Text = string.Empty;
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
                else if (ELon <= WLon)
                {
                    Msg = "Cannot place NW position east of SE position!";
                    SCTcommon.SendMessage(Msg);
                    WestLimitTextBox.Text = string.Empty;
                }
                else
                    FilterBy.WestLimit =
                        Conversions.AdjustedLatLong(WestLimitTextBox.Text, WestMarginNumericUpDown.Value.ToString(), "N");
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
                else if (ELon <= WLon)
                {
                    Msg = "Cannot place SE position west of NW position!";
                    SCTcommon.SendMessage(Msg);
                    EastLimitTextBox.Text = string.Empty;
                }
                else
                    FilterBy.EastLimit =
                        Conversions.AdjustedLatLong(EastLimitTextBox.Text, EastMarginNumericUpDown.Value.ToString(), "N");
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
                Console.WriteLine(FixListDataGridView.SelectedRows[0].Cells["Latitude"].Value);
                Console.WriteLine(FixListDataGridView.SelectedRows[0].Cells["Longitude"].Value);
                FilterBy.NorthLimit = Convert.ToDouble(FixListDataGridView.SelectedRows[0].Cells[1].Value);
                FilterBy.WestLimit = Convert.ToDouble(FixListDataGridView.SelectedRows[0].Cells[2].Value);
                NorthLimitTextBox.Text = Conversions.DecDeg2SCT(FilterBy.NorthLimit, true);
                WestLimitTextBox.Text = Conversions.DecDeg2SCT(FilterBy.WestLimit, false);
            }
            CheckPreviewButton();
            CheckARTCC2SquareButton();
            CheckARTCCAsCenterButton();
        }

        private void SEByFIXButton_Click(object sender, EventArgs e)
        {
            if (FixListDataGridView.SelectedRows.Count != 0)
            {
                FilterBy.SouthLimit = Convert.ToDouble(FixListDataGridView.SelectedRows[0].Cells[1].Value);
                FilterBy.EastLimit = Convert.ToDouble(FixListDataGridView.SelectedRows[0].Cells[2].Value);
                SouthLimitTextBox.Text = Conversions.DecDeg2SCT(FilterBy.SouthLimit, true);
                EastLimitTextBox.Text = Conversions.DecDeg2SCT(FilterBy.EastLimit, false);
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
            LoadForm1();
            UpdateAIRACbutton.Visible = true;
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
                DataFolderTextBox.Text = extractPath;
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
            InfoSection.CenterLatitude_Dec = (FilterBy.NorthLimit + FilterBy.SouthLimit)/ 2;
            CenterLatTextBox.Text = InfoSection.CenterLatitude_SCT;
            InfoSection.CenterLongitude_Dec = (FilterBy.WestLimit + FilterBy.EastLimit) / 2;
            CenterLonTextBox.Text = InfoSection.CenterLongitude_SCT;
        }

        private void InsertARTCCinSquareButton_Click(object sender, EventArgs e)
        {
            UpdateSquarebyARTCC();
        }

        private void FixListDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            NWByFIXButton.Enabled = SEByFIXButton.Enabled = FixListDataGridView.SelectedRows.Count != 0;
        }

        private void APTsCheckBox_Click(object sender, EventArgs e)
        {
            CheckPreviewButton();
            RWYsCheckBox.Enabled = APTsCheckBox.Checked;
        }

        private void RWYsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            CheckPreviewButton();
        }

        private void AWYsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
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

        private void dgvAWY_DoubleClick(object sender, EventArgs e)
        {
            // Use this to edit the airways
        }
    }
}
