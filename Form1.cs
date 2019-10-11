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
        static public DataTable Colors = new SCTdata.ColorDefsDataTable();
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
            TextColors.RWYTextColor = "White";
            TextColors.SSDTextColor = "Green";
            Form Instructions = new FormInstructions();
            DataIsLoaded = false;
            if (CycleInfo.GetINIfile())
            {
                HoldForm(true);
                LoadData();
                LoadForm();
                HoldForm(false);
                DataIsSelected = false;
                cmdUpdateGrid.Visible = cmdUpdateGrid.Enabled = true;
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
            if (ReadFixes.FillCycleInfo() != -1)
            {
                ReadFixes.FillARB();
                ReadFixes.FillVORNDB();
                ReadFixes.FillFIX();
                ReadFixes.FillAPT();
                ReadFixes.FillTWR();
                ReadFixes.FillAWY();
                ReadFixes.FillStarDP();
                DataIsLoaded = true;
                DataIsSelected = false;
            }
            else
            {
                Message = "File Data error - missing data files in folder.";
                MessageBox.Show(Message, VersionInfo.Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadForm()
        // Calls a load of all data and repopulates the form
        // Called by Load, DataFolder validated, and DatafolderButton
        {
            LoadcboARTCC();
            LoadcboAirport();
            lblCycleInfo.Text = CycleInfo.BuildCycleText();
            txtDataFolder.Text = FolderMgt.DataFolder;
            txtOutputFolder.Text = FolderMgt.OutputFolder;
            txtFacilityEngineer.Text = InfoSection.FacilityEngineer;
            txtAsstFacilityEngineer.Text = InfoSection.AsstFacilityEngineer;
            //  This next lines may not find the Airport if it doesn't exist in the list, 
            //  but the method won't throw an exception and won't call the OnChange event.
            cboARTCC.SelectedItem = cboARTCC.Items.IndexOf(InfoSection.SponsorARTCC);
            cboAirport.SelectedIndex = cboAirport.Items.IndexOf(InfoSection.DefaultAirport);
            grpCircle.Visible = false;
            grpSquareLimits.Visible = false;
            TestWriteSCT();
            DataIsSelected = false;
            cmdUpdateGrid.Enabled = cmdUpdateGrid.Visible = true;
        }

        private void HoldForm(bool FormIsFrozen)
        {
            lblUpdating.Visible = FormIsFrozen;
            txtDataFolder.Enabled = !FormIsFrozen;
            txtOutputFolder.Enabled = !FormIsFrozen;
            cboARTCC.Enabled = !FormIsFrozen;
            cboAirport.Enabled = !FormIsFrozen;
            grpSelectionMethod.Enabled = !FormIsFrozen;
            cmdWriteSCT.Enabled = !FormIsFrozen;
            cmdInstructions.Enabled = !FormIsFrozen;
            cmdExit.Visible = !FormIsFrozen;
            chkbxShowAll.Enabled = !FormIsFrozen;
            tabControl1.Enabled = !FormIsFrozen;
            Refresh();
            Application.DoEvents();
        }

        private void LoadFixGrid()
        {
            cmdWriteSCT.Enabled = false;
            if (DataIsLoaded)
            {
                DataIsSelected = true;
                cmdUpdateGrid.Enabled = cmdUpdateGrid.Visible = false;
                string Filter = FixFilter();
                // Console.WriteLine("Loading FixGrid Arpt...");
                Setdgv(dgvAPT, APT, Filter);
                // Console.WriteLine("Loading FixGrid VOR...");
                Setdgv(dgvVOR, VOR, Filter);
                // Console.WriteLine("Loading FixGrid NDB...");
                Setdgv(dgvNDB, NDB, Filter);
                // Console.WriteLine("Loading FixGrid FIX...");
                Setdgv(dgvFIX, FIX, Filter);
                // Console.WriteLine("Loading FixGrid RWY...");
                Setdgv(dgvRWY, RWY, Filter);
                // Console.WriteLine("Loading FixGrid ARB...");
                Setdgv(dgvARB, ARB, Filter);
                // Console.WriteLine("Loading FixGrid AWY...");
                Setdgv(dgvAWY, AWY, Filter);
                // Console.WriteLine("Loading FixGrid SSD...");
                LoadFixGridSSD(Filter);
                UpdateGridCount();
                cmdWriteSCT.Enabled = true;
            }
        }
        private void LoadcboARTCC()
        {
            DataView dvARB = new DataView(ARB)
            {
                Sort = "ARTCC"
            };
            DataTable datacboARTCC = dvARB.ToTable(true, "ARTCC");
            cboARTCC.DisplayMember = "ARTCC";
            cboARTCC.ValueMember = "ARTCC";
            cboARTCC.DataSource = datacboARTCC;
            dvARB.Dispose();
        }
        private void LoadcboAirport()
        {
            // Get APTs we will use in the usual manner
            string filter = FixFilter();
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
            DataTable dtAPT = dvAirports.ToTable(true, "ID", "FacilityID");
            cboAirport.DisplayMember = "FacilityID";
            cboAirport.ValueMember = "ID";
            cboAirport.DataSource = dtAPT;
            if (cboAirport.Items.Count == 0) cboAirport.Text = "";
            dvAPT.Dispose();
            dvAirports.Dispose();
        }

        private void Setdgv(DataGridView dgv, DataTable dt, string filter)
        {
            DataView dataView = new DataView(dt);
            ClearSelected(dataView);
            dataView.RowFilter = filter;
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
        private void LoadFixGridSSD(string filter)
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

        private string FixFilter()
        {
            string FilterString=string.Empty;
            if (chkbxShowAll.Checked == false)
            {
                switch (FilterBy.Method)
                {
                    default:
                    case "ARTCC":
                        FilterString = " ([ARTCC] ='" + cboARTCC.GetItemText(cboARTCC.SelectedItem) + "')";
                        break;
                    case "Square":
                        float NLat = AdjustedLatLong(txtLatNorth.Text, nudNorth.Value.ToString(), "N");
                        float SLat = AdjustedLatLong(txtLatSouth.Text, nudSouth.Value.ToString(), "S");
                        float WLng = AdjustedLatLong(txtLongWest.Text, nudWest.Value.ToString(), "W");
                        float ELng = AdjustedLatLong(txtLongEast.Text, nudEast.Value.ToString(), "E");
                        FilterString = FilterString +
                            " ( ([Latitude] >= " + SLat.ToString() + ")" +
                            " AND ([Latitude] <= " + NLat.ToString() + ")" +
                            " AND ([Longitude] >= " + WLng.ToString() + ")" +
                            " AND ([Longitude] <= " + ELng.ToString() + ") )";
                        break;
                }
            }
            return FilterString;
        }
         
        private float AdjustedLatLong (string LL, string nud, string LLedge)
        {
            float result = Convert.ToSingle(LL);
            float offset = Convert.ToSingle(nud);
            switch (LLedge)
            {
                case "N":
                    result += offset / InfoSection.NMperDegreeLongitude;
                    break;
                case "E":
                    result += offset / InfoSection.NMperDegreeLatitude;
                    break;
                case "S":
                    result -= offset / InfoSection.NMperDegreeLongitude;
                    break;
                case "W":
                    result -= offset / InfoSection.NMperDegreeLatitude;
                    break;
            }
            return result;
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
            cmdWriteSCT.Enabled = TestWriteSCT();
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
            cmdUpdateGrid.Visible = !btnSquare.Checked;
            cmdUpdateGrid.Enabled = !btnSquare.Checked;
            if (btnARTCC.Checked)
                if (cboARTCC.SelectedIndex == -1)
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
                    FilterBy.Param1 = cboARTCC.GetItemText(cboARTCC.SelectedItem);
                }
        }

        private void BtnSquare_CheckedChanged(object sender, EventArgs e)
        {
            grpSquareLimits.Visible = btnSquare.Checked;
            cmdUpdateGrid.Visible = btnSquare.Checked;
            cmdUpdateGrid.Enabled = btnSquare.Checked;
            grpCircle.Visible = !btnSquare.Checked;
            if (btnSquare.Checked)
            {
                if (cboARTCC.SelectedIndex > -1)
                {
                    string FilterARTCC = cboARTCC.GetItemText(cboARTCC.SelectedItem);
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
                    FilterBy.Param1 = LatNorth;
                    FilterBy.Param2 = LongWest;
                    FilterBy.Param3 = LatSouth;
                    FilterBy.Param4 = LongEast;
                    txtLongEast.Text = Math.Round(LongEast, 6).ToString();
                    txtLongWest.Text = Math.Round(LongWest, 6).ToString();
                    txtLatNorth.Text = Math.Round(LatNorth, 6).ToString();
                    txtLatSouth.Text = Math.Round(LatSouth, 6).ToString();
                    dataview.Dispose();
                }
            }
        }

        private void BtnCircle_CheckedChanged(object sender, EventArgs e)
        {
            grpSquareLimits.Visible = !btnCircle.Checked;
            grpCircle.Visible = btnCircle.Checked;
        }

        private void CboARTCC_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((cboARTCC.SelectedIndex != -1) & (cboARTCC.Items.Count > 0))
            {
                if (cboARTCC.GetItemText(cboARTCC.SelectedItem).ToString() != InfoSection.SponsorARTCC.ToString())
                {
                    btnClassB.Checked = true; btnClassC.Checked = false;
                    LoadcboAirport();
                }
            }
            DataIsSelected = false;
            cmdUpdateGrid.Enabled = cmdUpdateGrid.Visible = true;
        }

        private void CboAirport_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((cboAirport.SelectedIndex != -1) & (cboAirport.Items.Count != 0) )
            {
                UpdateInfoSection();
            }
        }

        private void BtnClassB_CheckedChanged(object sender, EventArgs e)
        {
            LoadcboAirport();
        }

        private void BtnClassC_CheckedChanged(object sender, EventArgs e)
        {
            LoadcboAirport();
        }

        private void TxtOutputFolder_Leave(object sender, EventArgs e)
        {
            if (txtOutputFolder.TextLength > 0)
            {
                if (Directory.Exists(txtOutputFolder.Text))
                {
                    FolderMgt.OutputFolder = txtOutputFolder.Text;
                }
                else
                {
                    string message = "Invalid folder path. Consider using the folder finder button.";
                    string caption = VersionInfo.Title;
                    MessageBoxButtons buttons = MessageBoxButtons.OK;
                    MessageBox.Show(message, caption, buttons);
                    FolderMgt.OutputFolder = string.Empty;
                }
            }
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
            cmdUpdateGrid.Enabled = cmdUpdateGrid.Visible = true;
            cmdWriteSCT.Enabled = TestWriteSCT();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private void CmdWriteSCT_Click(object sender, EventArgs e)
        {
            if (!DataIsSelected)
            {
                cmdUpdateGrid.Enabled = cmdUpdateGrid.Visible = false;
                HoldForm(true);
                LoadFixGrid();
                UpdateInfoSection();
                HoldForm(false);
            }
            SCToutput.WriteSCT();
        }

        private bool TestWriteSCT()
        {
            return (FolderMgt.OutputFolder.Length != 0) &
                (InfoSection.SponsorARTCC.Length != 0) &
                (InfoSection.DefaultAirport.Length != 0) &
                (InfoSection.DefaultCenterLatitude.ToString().Length != 0) &
                (InfoSection.DefaultCenterLongitude.ToString().Length != 0) &
                (InfoSection.MagneticVariation.ToString().Length != 0);

        }
        private void UpdateInfoSection()

        {
            if (cboAirport.Items.Count != 0)
            {
                DataRow foundRow = APT.Rows.Find(cboAirport.SelectedValue.ToString());
                //string cr = Environment.NewLine;
                //string Message = "InfoSection Update Data" + cr + "Airport: " + cboAirport.GetItemText(cboAirport.SelectedItem).ToString() + cr +
                //   "Latitude: " + Convert.ToSingle(foundRow["Latitude"].ToString()) + cr +
                //   "Longitude: " + Convert.ToSingle(foundRow["Longitude"].ToString()) + cr +
                //   "Mag Var: " + Convert.ToSingle(foundRow["MagVar"].ToString());
                //MessageBox.Show(Message, "UpdateInfoSection", MessageBoxButtons.OK, MessageBoxIcon.Information);
                InfoSection.SponsorARTCC = cboARTCC.GetItemText(cboARTCC.SelectedItem).ToString();
                InfoSection.DefaultAirport = cboAirport.GetItemText(cboAirport.SelectedItem).ToString();
                InfoSection.DefaultCenterLatitude = Convert.ToSingle(foundRow["Latitude"].ToString());
                InfoSection.DefaultCenterLongitude = Convert.ToSingle(foundRow["Longitude"].ToString());
                InfoSection.MagneticVariation = Convert.ToSingle(foundRow["MagVar"].ToString());
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
            cmdWriteSCT.Enabled = TestWriteSCT();
            DataIsSelected = false;
            cmdUpdateGrid.Enabled = cmdUpdateGrid.Visible = true;
        }

        private void TxtDataFolder_Validating(object sender, CancelEventArgs e)
        {
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
            using (StreamWriter sw = new StreamWriter(FolderMgt.INIfile))
            {
                sw.WriteLine(VersionInfo.Title.ToString());
                sw.WriteLine(CycleInfo.AIRAC.ToString());
                sw.WriteLine(CycleInfo.CycleStart.ToString());
                sw.WriteLine(CycleInfo.CycleEnd.ToString());
                sw.WriteLine(FolderMgt.DataFolder.ToString());
                sw.WriteLine(FolderMgt.OutputFolder.ToString());
                sw.WriteLine(InfoSection.SponsorARTCC.ToString());
                sw.WriteLine(InfoSection.DefaultAirport);
                sw.WriteLine(InfoSection.FacilityEngineer.ToString());
                sw.WriteLine(InfoSection.AsstFacilityEngineer.ToString());
            }
            Application.Exit();
        }

        private void CmdUpdateGrid_Click(object sender, EventArgs e)
        {
            cmdUpdateGrid.Visible = false;
            HoldForm(true);
            Refresh();
            LoadFixGrid();
            HoldForm(false);
            Refresh();
        }
    }
}
