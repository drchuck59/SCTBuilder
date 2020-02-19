using System;
using System.Net;
using System.Linq;
using System.Linq.Expressions;
using System.Data;
using System.IO;
using System.IO.Compression;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Generic;

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
        bool DataIsLoaded = false;
        bool DataIsSelected = false;
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
            Console.WriteLine("Exiting Form1");
        }

        private void CmdInstructions_Click(object sender, EventArgs e)
        {
            Form Instructions = new FormInstructions();
            Instructions.ShowDialog(this);
            Instructions.Dispose();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Eventually text colors will be modifiable by user
            Console.WriteLine("Form1_Load...");
            TextColors.RWYTextColor = "White";
            TextColors.SSDTextColor = "Green";
            Form Instructions = new FormInstructions();
            DataIsLoaded = false;
            if (File.Exists(FolderMgt.INIxml))
            {
                CycleInfo.ReadINIxml();
                GetChecked();
                HoldForm(true);
                LoadData();
                LoadForm();
                HoldForm(false);
                DataIsSelected = false;
                SCTtoolStripButton.Enabled = gridViewToolStripButton.Enabled = true;
            }
            else
            {
                Instructions.ShowDialog(this);
            }
            Instructions.Dispose();
        }
        private void LoadData()
        {
            string Message;
            lblUpdating.Text = "Loading data from FAA files";
            lblUpdating.Visible = true;
            UseWaitCursor = true;
            if (ReadNASR.FillCycleInfo() != -1)
            {
                ReadNASR.FillARB();
                ReadNASR.FillVORNDB();
                ReadNASR.FillFIX();
                ReadNASR.FillAPT();        // Includes RWY table
                ReadNASR.FillTWR();
                ReadNASR.FillAWY();
                ReadNASR.FillStarDP();
                DataIsLoaded = true;
                DataIsSelected = false;
            }
            else
            {
                Message = "File Data error - missing data files in folder.";
                MessageBox.Show(Message, VersionInfo.Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            lblUpdating.Visible = false;
            UseWaitCursor = false;
        }

        private void LoadForm()
        // Calls a load of all data and repopulates the form
        // Called by Load, DataFolder validated, and DatafolderButton
        {
            Console.WriteLine("LoadForm...");
            lblCycleInfo.Text = CycleInfo.BuildCycleText();
            txtDataFolder.Text = FolderMgt.DataFolder;
            txtOutputFolder.Text = FolderMgt.OutputFolder;
            txtFacilityEngineer.Text = InfoSection.FacilityEngineer;
            txtAsstFacilityEngineer.Text = InfoSection.AsstFacilityEngineer;
            LoadCboARTCC();
            CboARTCC.SelectedIndex = CboARTCC.FindStringExact(InfoSection.SponsorARTCC);
            FilterBy.Method = "ARTCC";
            FilterBy.NorthLimit = CboARTCC.GetItemText(CboARTCC.SelectedItem);
            grpCircle.Visible = false;
            grpSquareLimits.Visible = false;
            LoadCboAirport();
            CboAirport.SelectedIndex = CboAirport.FindStringExact(InfoSection.DefaultAirport);
            SCTtoolStripButton.Enabled = TestWriteSCT();
            DataIsSelected = false;
            gridViewToolStripButton.Enabled = true;
            Refresh();
        }

        private void HoldForm(bool FormIsFrozen)
        {
            lblUpdating.Visible = FormIsFrozen;
            //progressBar1.Visible = FormIsFrozen;
            //gridViewToolStripButton.Visible = !FormIsFrozen;
            //gridViewToolStripButton.Refresh();
            //txtDataFolder.Enabled = !FormIsFrozen;
            //txtOutputFolder.Enabled = !FormIsFrozen;
            //CboARTCC.Enabled = !FormIsFrozen;
            //CboAirport.Enabled = !FormIsFrozen;
            //grpSelectionMethod.Enabled = !FormIsFrozen;
            //SCTtoolStripButton.Enabled = !FormIsFrozen;
            //cmdInstructions.Enabled = !FormIsFrozen;
            //cmdExit.Visible = !FormIsFrozen;
            //chkbxShowAll.Enabled = !FormIsFrozen;
            //tabControl1.Enabled = !FormIsFrozen;
        }

        private void UpdateLabel(string text)
        {
            lblUpdating.Text = text;
            lblUpdating.Refresh();
        }

        private void LoadFixGrid()
        {
            string Filter;
            SCTtoolStripButton.Enabled = false;
            SetChecked();
            if (DataIsLoaded)
            {
                DataIsSelected = true;
                gridViewToolStripButton.Enabled = false;
                // ARB must always be selected by Sponsor ARTCC
                if (SCTchecked.ChkARB)
                {
                    Filter = FixFilter("ARTCC");
                    UpdateLabel("Selecting ARTCC boundaries...");
                    Setdgv(dgvARB, ARB, Filter);
                }
                else DataIsSelected = false;
                // Everything else can be selected by user choice
                Filter = FixFilter(FilterBy.Method);
                if (SCTchecked.ChkAPT)
                {
                    UpdateLabel("Selecting Airports...");
                    Setdgv(dgvAPT, APT, Filter);
                }
                else DataIsSelected = false;
                if (SCTchecked.ChkVOR)
                {
                    UpdateLabel("Selecting VORs...");
                    Setdgv(dgvVOR, VOR, Filter);
                }
                else DataIsSelected = false;
                if (SCTchecked.ChkNDB)
                {
                    UpdateLabel("Selecting NDBs...");
                    Setdgv(dgvNDB, NDB, Filter);
                }
                else DataIsSelected = false;
                if (SCTchecked.ChkFIX)
                {
                    UpdateLabel("Selecting FIXes...");
                    Setdgv(dgvFIX, FIX, Filter);
                }
                else DataIsSelected = false;
                if (SCTchecked.ChkRWY)
                {
                    UpdateLabel("Selecting Runways...");
                    Setdgv(dgvRWY, RWY, Filter);
                }
                else DataIsSelected = false;
                if (SCTchecked.ChkAWY)
                {
                    UpdateLabel("Selecting Airways...");
                    Setdgv(dgvAWY, AWY, Filter);
                }
                else DataIsSelected = false;
                if (SCTchecked.ChkSSD)
                {
                    UpdateLabel("Selecting SIDs && STARs (slow)...");
                    SetdgvSSD(Filter);
                }
                else DataIsSelected = false;
                if (SCTchecked.ChkSUA) SetdgvSUA();
                UpdateGridCount();
                SCTtoolStripButton.Enabled = true;
            }
        }

        private void SetdgvSUA()
        {

            DataView dvSUA = new DataView(SUA);
            foreach (DataRowView rowView in dvSUA) rowView["Selected"] = false;
            dvSUA.RowFilter = SetSUAfilter();
            foreach (DataRowView rowView in dvSUA) rowView["Selected"] = true;
            DataTable dtgv = dvSUA.ToTable(true, "Selected", "Category", "Name", "ID");
            dgvSUA.DataSource = dtgv;
        }

        private string SetSUAfilter()
        {
            UpdateSquare();
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

        private void LoadCboARTCC()
        {
            DataView dvARB = new DataView(ARB)
            {
                Sort = "ARTCC"
            };
            DataTable datacboARTCC = dvARB.ToTable(true, "ARTCC");
            CboARTCC.DisplayMember = "ARTCC";
            CboARTCC.ValueMember = "ARTCC";
            CboARTCC.DataSource = datacboARTCC;
            dvARB.Dispose();
        }
        private void LoadCboAirport()
        {
            // Get APTs we will use in the usual manner
            string filter = FixFilter(FilterBy.Method);
            DataView dvAPT = new DataView(APT)
            {
                RowFilter = filter,
                Sort = "FacilityID"
            };
            // Move into a table and add the missing column Class
            DataTable dtAirports = dvAPT.ToTable(false, "ID", "FacilityID", "Latitude", "Longitude", "MagVar");
            dtAirports.Columns.Add("Class", typeof(string));
            // Loop the TWR table to add the Class airspace
            DataRow foundrow;
            foreach (DataRow dataRow in dtAirports.AsEnumerable())
            {
                foundrow = TWR.Rows.Find(dataRow["ID"]);
                if (foundrow != null)
                    dataRow["Class"] = foundrow["Class"];
            }
            // Create the dataview, filtering by the selected class
            filter = "([Class] = '" + GetClass() + "')";
            DataView dvAirports = new DataView(dtAirports)
            {
                RowFilter = filter
            };
            DataTable dtAPT = dvAirports.ToTable(true, "FacilityID");
            CboAirport.DisplayMember = "FacilityID";
            CboAirport.ValueMember = "FacilityID";
            CboAirport.DataSource = dtAPT;
            if (CboAirport.Items.Count != 0) CboAirport.SelectedIndex = 0;
            dvAirports.Dispose();
            dvAPT.Dispose();
        }

        private void Setdgv(DataGridView dgv, DataTable dt, string filter)
        {
            DataView dataView = new DataView(dt);
            ClearSelected(dataView);
            dataView.RowFilter = filter;
            Console.WriteLine(dgv.Name + " has " + dgv.Rows.Count + " in filter " + filter);
            Console.WriteLine(dt.TableName + " has " + dt.Rows.Count + " rows available.");
            SetSelected(dataView);
            dgv.DataSource = dt;
            ColumnSortOrder(dgv);
            string curDGV = dgv.Name;
            int sortIndex = 1;
            switch (curDGV)
            {
                case "dgvAPT":
                    dgv.Columns[1].HeaderText = "ICOA";
                    break;
                case "dgvRWY":
                    dgv.Columns[1].HeaderText = "Apt";
                    dgv.Columns[2].HeaderText = "Rwy";
                    break;
                case "dgvAWY":
                    dgv.Columns[1].HeaderText = "AWY";
                    sortIndex = 0;
                    break;
                case "dgvSTAR":
                case "dgvSID":
                    dgv.Columns[1].HeaderText = "Name";
                    break;
                case "dgvFIX":
                    dgv.Columns[0].HeaderText = "Name";
                    sortIndex = 0;
                    break;
                case "dgvARB":
                    dgv.Columns[1].HeaderText = "ARTCC";
                    break;
                default:
                    dgv.Columns[1].HeaderText = "NavAid";
                    break;
            }
            dgv.Sort(dgv.Columns[sortIndex], ListSortDirection.Ascending);
            if (chkbxShowAll.Checked)
                (dgv.DataSource as DataTable).DefaultView.RowFilter = "";
            else
                (dgv.DataSource as DataTable).DefaultView.RowFilter = "[Selected]";
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCellsExceptHeader;
            // dgv.AutoResizeColumns();
            dataView.Dispose();
        }
        private void SetdgvSSD(string filter)
        {
            // This one is different and gets [Selected] based upon included APTs
            // To make this one faster, use a DataView rather than the dataviewgrid
            // First, get all the airports affected (It's easier to just make a new one)
            var SSDID = new List<string>(); string IDfilter;
            DataView dvSSD = new DataView(SSD);
            DataView dvAirports = new DataView(APT)
            {
                RowFilter = filter
            };
            // Clear the SSD list            
            ClearSelected(dvSSD);

            // Build a table of the (unique) airports
            DataTable dtAirports = dvAirports.ToTable(true, "FacilityID");

            // Build a List of affected airports (may or may not have SSDs)
            string AAfilter = "([FixType] = 'AA') AND ";
            foreach (DataRow dataRow in dtAirports.AsEnumerable())
            {
                string FacIDfilter = "[NavAid] = '" + dataRow[0].ToString() + "'";       // Only 1 item in this table
                dvSSD.RowFilter = AAfilter + FacIDfilter;
                DataTable IDdata = dvSSD.ToTable(true, "ID");            // I hope it lets me do this, or I'll need another list!
                foreach (DataRow data in IDdata.AsEnumerable())
                {
                    SSDID.Add(data["ID"].ToString());
                }
            }
            // FINALLY I can set the [Selected] - damn you, FAA, for inconsistent data tables
            // Use a dataview to update the column
            IEnumerable<string> distinctSSDID = SSDID.Distinct();           // Remove duplicates (multiple APTS in SSDs)
            foreach (var item in distinctSSDID)
            {
                IDfilter = "[ID] = '" + item.ToString() + "'";
                dvSSD.RowFilter = IDfilter;
                SetSelected(dvSSD);
            }
            string strSID = "[IsSID]";
            string strSTAR = "(NOT [IsSID])";
            if (!chkbxShowAll.Checked)
            {
                strSID += " AND [Selected]";
                strSTAR += " AND [Selected]";
            }
            dvSSD.RowFilter = strSID;
            DataTable dtSID = dvSSD.ToTable();
            dgvSID.DataSource = dtSID;
            dgvSID.Sort(dgvSID.Columns["Sequence"], ListSortDirection.Ascending);
            dgvSID.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCellsExceptHeader;
            ColumnSortOrder(dgvSID);

            dvSSD.RowFilter = strSTAR;
            DataTable dtSTAR = dvSSD.ToTable();
            dgvSTAR.DataSource = dtSTAR;
            dgvSTAR.Sort(dgvSTAR.Columns["Sequence"], ListSortDirection.Ascending);
            dgvSTAR.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCellsExceptHeader;
            ColumnSortOrder(dgvSTAR);
            dvAirports.Dispose();
            dvSSD.Dispose();
        }
        private void ClearSelected(DataView dv)
        {
            // If the filter is applied, selected boxes are true
            // otherwise, ALL the selected boxes are false
            // But if the ShowAll box is checked, ignore the update
            Console.WriteLine("Current filter: " + dv.RowFilter + " for " + dv.Count + " rows.");
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
            switch (tabControl1.SelectedTab.Text)
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
                case "ARBs":
                    txtGridViewCount.Text = dgvARB.Rows.Count.ToString() + " / " + ARB.Rows.Count.ToString();
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
        private void ColumnSortOrder(DataGridView dgv)
        {
            bool hasName = false;
            foreach (DataGridViewColumn dgvc in dgv.Columns)
            {
                switch (dgvc.Name)
                {
                    case "Selected":
                        dgvc.DisplayIndex = 0;
                        dgvc.ReadOnly = false;
                        break;
                    case "FacilityID":
                    case "STARname":
                    case "AWYID":
                        dgvc.DisplayIndex = 1;
                        dgvc.ReadOnly = true;
                        break;
                    case "RwyIdentifier":
                        dgvc.DisplayIndex = 2;
                        dgvc.ReadOnly = true;
                        break;
                    case "Name":
                        dgvc.DisplayIndex = 2;
                        dgvc.ReadOnly = true;
                        hasName = true;
                        break;
                    case "Latitude":
                        if (hasName) dgvc.DisplayIndex = 3;
                        else dgvc.DisplayIndex = 2;
                        dgvc.ReadOnly = true;
                        break;
                    case "Longitude":
                        if (hasName) dgvc.DisplayIndex = 4;
                        else dgvc.DisplayIndex = 3;
                        dgvc.ReadOnly = true;
                        break;
                    case "Frequency":
                        if (hasName) dgvc.DisplayIndex = 5;
                        else dgvc.DisplayIndex = 4;
                        dgvc.ReadOnly = true;
                        break;
                    case "Use":
                        if (hasName) dgvc.DisplayIndex = 5;
                        else dgvc.DisplayIndex = 4;
                        dgvc.ReadOnly = true;
                        break;
                    default:
                        dgvc.ReadOnly = true;
                        dgvc.Visible = false;
                        break;
                }
            }
        }
        private string FixFilter(string Method)
        {
            string FilterString=string.Empty;
            if (chkbxShowAll.Checked == false)
           
                switch (Method)
                {
                    default:
                    case "ARTCC":
                        FilterString = " ([ARTCC] ='" + CboARTCC.GetItemText(CboARTCC.SelectedItem) + "')";
                        UpdateSquare();             // Calculates the limits N/S/E/W of selected ARTCC
                        break;
                    case "Square":
                        FilterBy.NorthLimit = Conversions.AdjustedLatLong(txtLatNorth.Text, nudNorth.Value.ToString(), "N");
                        FilterBy.SouthLimit = Conversions.AdjustedLatLong(txtLatSouth.Text, nudSouth.Value.ToString(), "S");
                        FilterBy.EastLimit = Conversions.AdjustedLatLong(txtLongEast.Text, nudWest.Value.ToString(), "W");
                        FilterBy.WestLimit = Conversions.AdjustedLatLong(txtLongWest.Text, nudEast.Value.ToString(), "E");
                        FilterString = FilterString +
                            " ( ([Latitude] <= " + FilterBy.NorthLimit.ToString() + ")" +
                            " AND  ([Latitude] >= " + FilterBy.SouthLimit.ToString() + ")" +
                            " AND ([Longitude] <= " + FilterBy.EastLimit.ToString() + ")" +
                            " AND ([Longitude] >= " + FilterBy.WestLimit.ToString() + ") )";
                        break;
                }
            return FilterString;
        }
         
            private void PictureBox2_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://zjxartcc.org");
        }

        private string GetClass()
        {
            if (btnClassC.Checked) return "C";
            else return "B";
        }

        private void TxtFacilityEngineer_Validated(object sender, EventArgs e)
        {
            InfoSection.FacilityEngineer = txtFacilityEngineer.Text;
            SCTtoolStripButton.Enabled = TestWriteSCT();
        }

        private void TxtFacilityEngineer_Validating(object sender, CancelEventArgs e)
        {
            string Message = "Facility Engineer name may not be blank";
            MessageBoxButtons buttons = MessageBoxButtons.OK;
            MessageBoxIcon icon = MessageBoxIcon.Warning;
            if (txtFacilityEngineer.TextLength == 0)
            {
                MessageBox.Show(Message, VersionInfo.Title, buttons, icon);
                txtFacilityEngineer.Text = "Facility Engineer Name";
            }
        }

        private void BtnARTCC_CheckedChanged(object sender, EventArgs e)
        {
            grpSquareLimits.Visible = !btnARTCC.Checked;
            grpCircle.Visible = !btnARTCC.Checked;
            gridViewToolStripButton.Enabled = !btnSquare.Checked;
            if (btnARTCC.Checked)
                if (CboARTCC.SelectedIndex == -1)
                {
                    string Message = "You must select an ARTCC before selecting this method.";
                    MessageBoxIcon icon = MessageBoxIcon.Information;
                    MessageBoxButtons buttons = MessageBoxButtons.OK;
                    MessageBox.Show(Message, VersionInfo.Title, buttons, icon);
                    btnARTCC.Checked = false;
                }
                else
                {
                    FilterBy.Method = "ARTCC";
                    FilterBy.NorthLimit = CboARTCC.GetItemText(CboARTCC.SelectedItem);
                    SCTtoolStripButton.Enabled = TestWriteSCT();
                }
        }

        private void BtnSquare_CheckedChanged(object sender, EventArgs e)
        {
            grpSquareLimits.Visible = btnSquare.Checked;
            gridViewToolStripButton.Enabled = btnSquare.Checked;
            grpCircle.Visible = !btnSquare.Checked;
            if ((btnSquare.Checked) && (CboARTCC.SelectedIndex > -1))
                UpdateSquare();
        }

        private void UpdateSquare()
        {
            string FilterARTCC = CboARTCC.GetItemText(CboARTCC.SelectedItem);
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
            FilterBy.Method = "Square";
            FilterBy.NorthLimit = LatNorth;
            FilterBy.SouthLimit = LatSouth;
            FilterBy.EastLimit = LongEast;
            FilterBy.WestLimit = LongWest;
            txtLongEast.Text = Math.Round(LongEast, 6).ToString();
            txtLongWest.Text = Math.Round(LongWest, 6).ToString();
            txtLatNorth.Text = Math.Round(LatNorth, 6).ToString();
            txtLatSouth.Text = Math.Round(LatSouth, 6).ToString();
            dataview.Dispose();
        }

        private void BtnCircle_CheckedChanged(object sender, EventArgs e)
        {
            grpSquareLimits.Visible = !btnCircle.Checked;
            grpCircle.Visible = btnCircle.Checked;
        }

        private void BtnClassB_CheckedChanged(object sender, EventArgs e)
        {
            LoadCboAirport();
            SCTtoolStripButton.Enabled = TestWriteSCT();
        }

        private void BtnClassC_CheckedChanged(object sender, EventArgs e)
        {
            LoadCboAirport();
            SCTtoolStripButton.Enabled = TestWriteSCT();
        }

        private void CmdOutputFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fBD = new FolderBrowserDialog();
            if (txtOutputFolder.TextLength > 0)
            {
                fBD.SelectedPath = txtOutputFolder.Text;
            }
            else
            {
                fBD.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            }
            if (fBD.ShowDialog() == DialogResult.OK)
            {
                txtOutputFolder.Text = fBD.SelectedPath;
            }
            fBD.Dispose();
            SCTtoolStripButton.Enabled = TestWriteSCT();
        }

        private void CmdDataFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fBD = new FolderBrowserDialog();
            if (txtDataFolder.TextLength > 0)
            {
                fBD.SelectedPath = txtDataFolder.Text;
            }
            else
            {
                fBD.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            }
            if (fBD.ShowDialog() == DialogResult.OK)
            {
                txtDataFolder.Text = fBD.SelectedPath;
                if (FolderMgt.DataFolder != txtDataFolder.Text)
                {
                    FolderMgt.DataFolder = txtDataFolder.Text;
                }
            }
            fBD.Dispose();
            DataIsSelected = false;
            gridViewToolStripButton.Enabled = true;
            SCTtoolStripButton.Enabled = TestWriteSCT();
        }

        private void CmdWriteSCT_Click(object sender, EventArgs e)
        {
            if (!DataIsSelected)
            {
                gridViewToolStripButton.Enabled = false;
                HoldForm(true);
                UpdateInfoSection();
                LoadFixGrid();
                HoldForm(false);
            }
            SCToutput.WriteSCT();
        }

        private bool TestWriteSCT()
        {
            Console.WriteLine("Data: " + FolderMgt.DataFolder.ToString());
            Console.WriteLine("Output: " + FolderMgt.OutputFolder.ToString());
            Console.WriteLine("ARTCC: " + InfoSection.SponsorARTCC.ToString());
            Console.WriteLine("Airport: " + InfoSection.DefaultAirport.ToString());
            Console.WriteLine("Latitude: " + InfoSection.DefaultCenterLatitude.ToString());
            Console.WriteLine("Longitude: " + InfoSection.DefaultCenterLongitude.ToString());
            Console.WriteLine("Mag Var: " + InfoSection.MagneticVariation.ToString());
            return (FolderMgt.OutputFolder.Length != 0) &&
                (FolderMgt.DataFolder.Length != 0) &&
                (InfoSection.SponsorARTCC.Length != 0) &&
                (InfoSection.DefaultAirport.Length != 0) &&
                (InfoSection.DefaultCenterLatitude.ToString().Length != 0) &&
                (InfoSection.DefaultCenterLongitude.ToString().Length != 0) &&
                (InfoSection.MagneticVariation.ToString().Length != 0);
        }
        private void UpdateInfoSection()

        {
            if (CboAirport.Items.Count != 0)
            {
                //string cr = Environment.NewLine;
                //string Message = "InfoSection Update Data" + cr + "Airport: " + cboAirport.GetItemText(cboAirport.SelectedItem).ToString() + cr +
                //   "Latitude: " + Convert.ToSingle(foundRow["Latitude"].ToString()) + cr +
                //   "Longitude: " + Convert.ToSingle(foundRow["Longitude"].ToString()) + cr +
                //   "Mag Var: " + Convert.ToSingle(foundRow["MagVar"].ToString());
                //MessageBox.Show(Message, "UpdateInfoSection", MessageBoxButtons.OK, MessageBoxIcon.Information);
                InfoSection.SponsorARTCC = CboARTCC.GetItemText(CboARTCC.SelectedItem).ToString();
                InfoSection.DefaultAirport = CboAirport.GetItemText(CboAirport.SelectedItem).ToString();
            }
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
            if (txtDataFolder.TextLength > 0)
            {
                LoadData();
                LoadForm();
                // LoadFixGrid();
                FolderMgt.DataFolder = txtDataFolder.Text;
                grpCircle.Visible = false;
                grpSquareLimits.Visible = false;
            }
            if (txtDataFolder.Text != FolderMgt.DataFolder)
                CmdDataFolder_Click(sender, e);
            DataIsSelected = false;
            gridViewToolStripButton.Enabled = true;
            SCTtoolStripButton.Enabled = TestWriteSCT();
        }

        private void TxtDataFolder_Validating(object sender, CancelEventArgs e)
        {
            Console.WriteLine("TxtDataFolder_Validating...");
            if (txtDataFolder.TextLength > 0)
            {
                if (Directory.Exists(txtDataFolder.Text))
                {
                    FolderMgt.DataFolder = txtDataFolder.Text;
                    e.Cancel = false;
                }
                else
                {
                    string message = "Invalid folder path. Consider using the folder finder button.";
                    string caption = VersionInfo.Title;
                    MessageBoxButtons buttons = MessageBoxButtons.OK;
                    MessageBox.Show(message, caption, buttons);
                    FolderMgt.DataFolder = string.Empty;
                    lblCycleInfo.Text = "Choose folder containing FAA text data";
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

        private void CmdUpdateGrid_Click(object sender, EventArgs e)
        {
            Console.WriteLine("cmdUpdateGrid_Click...");
            string Message = "You must at-least select an ARTCC and Airport";
            MessageBoxButtons buttons = MessageBoxButtons.OK;
            MessageBoxIcon icon = MessageBoxIcon.Exclamation;
            if (TestUpdateGrid())
            {
                HoldForm(true);
                UpdateInfoSection();
                SetChecked();
                LoadFixGrid();
                HoldForm(false);
                Refresh();
            }
            else
                MessageBox.Show(Message, VersionInfo.Title, buttons, icon);
        }

        private bool TestUpdateGrid()
        {
            bool Result;
            Result = (InfoSection.SponsorARTCC.Length != 0) &&
                    (InfoSection.DefaultAirport.Length != 0) &&
                    (InfoSection.DefaultCenterLatitude.ToString().Length != 0) &&
                    (InfoSection.DefaultCenterLongitude.ToString().Length != 0) &&
                    (InfoSection.MagneticVariation.ToString().Length != 0);
            Console.WriteLine ("TestUpdateGrid is " + Result.ToString());
            return Result;
        }
        private void CboARTCC_Validated(object sender, EventArgs e)
        {
            if (CboARTCC.SelectedIndex != -1)
            {
                InfoSection.SponsorARTCC = CboARTCC.Text.ToString();
                if (btnSquare.Checked && (CboARTCC.SelectedIndex > -1))
                    UpdateSquare();
                LoadCboAirport();
            }
            SCTtoolStripButton.Enabled = TestWriteSCT();
        }

        private void CboAirport_Validated(object sender, EventArgs e)
        {
            if (CboAirport.SelectedIndex != -1)
            {
                InfoSection.DefaultAirport = CboAirport.Text.ToString();
            }
            SCTtoolStripButton.Enabled = TestWriteSCT();
        }

        private void LocalSectors_Click(object sender, EventArgs e)
        {
            Console.WriteLine("LocalSectors_Click...");
            if (ReadNASR.FillLocalSectors() )
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
            SCTchecked.ChkAPT = chkAPTs.Checked;
            SCTchecked.ChkARB = chkARBs.Checked;
            SCTchecked.ChkAWY = chkAWYs.Checked;
            SCTchecked.ChkFIX = chkFIXes.Checked;
            SCTchecked.ChkNDB = chkNDBs.Checked;
            SCTchecked.ChkRWY = chkRWYs.Checked;
            SCTchecked.ChkSSD = chkSSDs.Checked;
            SCTchecked.ChkVOR = chkVORs.Checked;
            SCTchecked.ChkALL = chkALL.Checked;
            SCTchecked.ChkSSDname = chkSSDName.Checked;
            SCTchecked.ChkSUA = ChkSUA.Checked;
            SCTchecked.ChkSUA_ClassB = chkSUA_ClassB.Checked;
            SCTchecked.ChkSUA_ClassC = chkSUA_ClassC.Checked;
            SCTchecked.ChkSUA_ClassD = chkSUA_ClassD.Checked;
            SCTchecked.ChkSUA_Danger = chkSUA_Danger.Checked;
            SCTchecked.ChkSUA_Prohibited = chkSUA_Prohibited.Checked;
            SCTchecked.ChkSUA_Restricted = chkSUA_Restricted.Checked;
        }

        private void GetChecked()
        {
            Console.WriteLine("GetChecked...");
            chkAPTs.Checked = SCTchecked.ChkAPT;
            chkARBs.Checked = SCTchecked.ChkARB;
            chkAWYs.Checked = SCTchecked.ChkAWY;
            chkFIXes.Checked = SCTchecked.ChkFIX;
            chkNDBs.Checked = SCTchecked.ChkNDB;
            chkRWYs.Checked = SCTchecked.ChkRWY;
            chkSSDs.Checked = SCTchecked.ChkSSD;
            chkVORs.Checked = SCTchecked.ChkVOR;
            chkALL.Checked = SCTchecked.ChkALL;
            chkSSDName.Checked = SCTchecked.ChkSSDname;
            ChkSUA.Checked = SCTchecked.ChkSUA;
            chkSUA_ClassB.Checked = SCTchecked.ChkSUA_ClassB;
            chkSUA_ClassC.Checked = SCTchecked.ChkSUA_ClassC;
            chkSUA_ClassD.Checked = SCTchecked.ChkSUA_ClassD;
            chkSUA_Danger.Checked = SCTchecked.ChkSUA_Danger;
            chkSUA_Prohibited.Checked = SCTchecked.ChkSUA_Prohibited;
            chkSUA_Restricted.Checked = SCTchecked.ChkSUA_Restricted;
        }

        private void ChkALL_CheckedChanged(object sender, EventArgs e)
        {
            string cr = Environment.NewLine;
            if (chkALL.Checked)
            {
                chkAPTs.Checked = chkALL.Checked;
                chkARBs.Checked = chkALL.Checked;
                chkAWYs.Checked = chkALL.Checked;
                chkFIXes.Checked = chkALL.Checked;
                chkNDBs.Checked = chkALL.Checked;
                chkRWYs.Checked = chkALL.Checked;
                chkSSDs.Checked = chkALL.Checked;
                chkVORs.Checked = chkALL.Checked;
                lblFilesWarning.Text = "A text file (.txt) will be written for" + cr +
                                        "each item checked. You can review " + cr +
                                        "and place in your SCT2 file.";
            }
            else
                lblFilesWarning.Text = "All checked items will be written to" + cr +
                                        "a single SCT2 file.";
            lblFilesWarning.Refresh();
        }

        private void ChkSSDs_CheckedChanged(object sender, EventArgs e)
        {
            chkSSDName.Visible = chkSSDs.Checked;
        }

        private void CmdAddSUAs_Click(object sender, EventArgs e)
        {
            ReadNASR.FillAirSpace();
        }

        private void ChkSUA_CheckedChanged(object sender, EventArgs e)
        {
            panelSUAs.Visible = ChkSUA.Checked;
        }

        private void ChkOverwrite_CheckedChanged(object sender, EventArgs e)
        {
            SCTchecked.ChkConfirmOverwrite = chkOverwrite.Checked;
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
    }
}
