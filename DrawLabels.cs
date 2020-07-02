using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace SCTBuilder
{
    public partial class DrawLabels : Form
    {
        public DrawLabels()
        {
            InitializeComponent();
        }


        private void SendMessage(string Msg, MessageBoxIcon icon = MessageBoxIcon.Information,
            MessageBoxButtons buttons = MessageBoxButtons.OK)
        {
            MessageBox.Show(Msg, VersionInfo.Title, buttons, icon);
        }

        private void IdentifierTextBox_TextChanged(object sender, EventArgs e)
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
                    }
                }
                else
                {
                    string Msg = "You must select your FAA data folder before you can search for Fixes.";
                    MessageBoxIcon icon = MessageBoxIcon.Warning;
                    SendMessage(Msg, icon);
                    IdentifierTextBox.Text = string.Empty;
                }
            }
            else
            {
                FixListDataGridView.DataSource = null;
            }
        }

        private void FixListDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            LatTextBox.Text =
                Conversions.DecDeg2SCT(Convert.ToDouble(FixListDataGridView.SelectedRows[0].Cells[1].Value), true);
            CrossForm.Lat = Convert.ToDouble(FixListDataGridView.SelectedRows[0].Cells[1].Value);
            LonTextBox.Text =
                Conversions.DecDeg2SCT(Convert.ToDouble(FixListDataGridView.SelectedRows[0].Cells[2].Value), true);
            LabelTextBox.Text = FixListDataGridView.SelectedRows[0].Cells[0].Value.ToString();
            CrossForm.Lon = Convert.ToDouble(FixListDataGridView.SelectedRows[0].Cells[2].Value);
            CheckGenerate();
        }

        private void CopyClipButton_Click(object sender, EventArgs e)
        {
            string Msg = string.Empty;
            if (!double.IsNaN(CrossForm.Lat))
            {
                LatTextBox.Text = Conversions.DecDeg2SCT(CrossForm.Lat, true);
            }
            else Msg = "One or both coordinates are not a decimal coordinate";

            if (!double.IsNaN(CrossForm.Lon))
                LonTextBox.Text = Conversions.DecDeg2SCT(CrossForm.Lon, false);
            else Msg = "One or both coordinates are not a number";
            BearingTextBox.Text = CrossForm.Bearing.ToString();
            if (Msg.Length != 0) SendMessage(Msg);
            CheckGenerate();
        }

        private void CheckGenerate()
        {
            DrawButton.Enabled = (LatTextBox.TextLength != 0) && (LonTextBox.TextLength != 0) &&
                (LabelTextBox.TextLength != 0) && (BearingTextBox.TextLength != 0) &&
                (ScaleTextBox.TextLength != 0);
        }

        private void LatTextBox_Validated(object sender, EventArgs e)
        {
            string Msg; double Lat1;
            try
            {
                Lat1 = Convert.ToDouble(LatTextBox.Text);
                if (Math.Abs(Lat1) <= 90)
                {
                    LatTextBox.Text = Conversions.DecDeg2SCT(Lat1,true);
                    CrossForm.Lat = Lat1;
                }
                else
                {
                    Msg = "Latitude must fall between -90 and 90 degrees.";
                    SendMessage(Msg);
                    LatTextBox.Text = string.Empty;
                }
            }
            catch
            {
                Lat1 = Conversions.String2DecDeg(LatTextBox.Text);
                if (Math.Abs(Lat1) <= 90)
                {
                    LatTextBox.Text = Conversions.DecDeg2SCT(Lat1, true);
                    CrossForm.Lat = Lat1;
                }
                else
                {
                    Msg = "Conversion error or Latitude outside -90 and 90 degrees.";
                    SendMessage(Msg);
                    LatTextBox.Text = string.Empty;
                }
            }
            CheckGenerate();
        }

        private void LonTextBox_Validated(object sender, EventArgs e)
        {
            string Msg; double Lon1;
            try
            {
                Lon1 = Convert.ToDouble(LonTextBox.Text);
                if (Math.Abs(Lon1) <= 180)
                {
                    LonTextBox.Text = Conversions.DecDeg2SCT(Lon1, true);
                    CrossForm.Lat = Lon1;
                }
                else
                {
                    Msg = "Longitude must fall between -180 and 180 degrees.";
                    SendMessage(Msg);
                    LonTextBox.Text = string.Empty;
                }
            }
            catch
            {
                Lon1 = Conversions.String2DecDeg(LonTextBox.Text);
                if (Math.Abs(Lon1) <= 90)
                {
                    LonTextBox.Text = Conversions.DecDeg2SCT(Lon1, true);
                    CrossForm.Lat = Lon1;
                }
                else
                {
                    Msg = "Conversion error or Latitude outside -90 and 90 degrees.";
                    SendMessage(Msg);
                    LonTextBox.Text = string.Empty;
                }
            }
            CheckGenerate();
        }

        private void LabelTextBox_Click(object sender, EventArgs e)
        {
            CheckGenerate();
        }

        private void BearingTextBox_Validated(object sender, EventArgs e)
        {
            string Msg;
            try
            {
                double Brg1 = Convert.ToDouble(BearingTextBox.Text);
                CrossForm.Bearing = Brg1;
            }
            catch
            {
                Msg = "Bearing must be a number between 1 and 360 degrees";
                SendMessage(Msg);
                BearingTextBox.Text = string.Empty;
            }
        }

        private void ScaleTextBox_Validated(object sender, EventArgs e)
        {
            string Msg;
            try
            {
                double Scale1 = Convert.ToDouble(ScaleTextBox.Text);
            }
            catch
            {
                Msg = "Scale must be a number between 0 and 10 nautical miles";
                SendMessage(Msg);
                ScaleTextBox.Text = string.Empty;
            }
        }

        private void DrawButton_Click(object sender, EventArgs e)
        {
            string result = string.Empty; 
            object[] NavData;
            double Lat1 = Conversions.String2DecDeg(LatTextBox.Text, ".");
            double Lon1 = Conversions.String2DecDeg(LonTextBox.Text, ".");
            int Brg = Convert.ToInt32(BearingTextBox.Text) - 90;            // Rotation in addition to MagVar
            float Scale = Convert.ToSingle(ScaleTextBox.Text);              // Scaling beyond internal 1/3600
            int rowIndex = -1;

            foreach (DataGridViewRow row in FixListDataGridView.Rows)
            {
                if (row.Cells[0].Value.ToString().Equals(IdentifierTextBox.Text))
                {
                    rowIndex = row.Index;
                    break;
                }
            }
            result += Hershey.WriteHF(LabelTextBox.Text, Lat1, Lon1, Brg, Scale);
            if ((rowIndex != -1) && (IncludeSymbolCheckBox.Checked))
            {                
                NavData = SCTcommon.GetNavData(IdentifierTextBox.Text);
                result += Hershey.DrawSymbol(NavData);
            }
            OutputTextBox.Text = result;
        }

        private void ClearOutputButton_Click(object sender, EventArgs e)
        {
            OutputTextBox.Text = string.Empty;
        }

        private void LabelTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Label must fall within the printing ASCII char set
            if (Extensions.IsASCII(e.KeyChar))
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void DrawLabels_Load(object sender, EventArgs e)
        {
            bool CanUseFix = CycleInfo.AIRAC == CycleInfo.CurrentAIRAC;
            FixImportGroupBox.Enabled = CanUseFix;
            if (CanUseFix)
                toolTip1.SetToolTip(FixImportGroupBox, "Begin typing any FIX");
            else
                toolTip1.SetToolTip(FixImportGroupBox, "Requires current AIRAC data to lookup FIXes");
        }

        private void BearingTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Extensions.CharIsDigit(e.KeyChar))
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void ScaleTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Extensions.CharIsDecimal(e.KeyChar, ref ScaleTextBox, 1))
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void LatTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Extensions.IsValidCoord(e.KeyChar, LatTextBox))
                e.Handled = false;
            else
                e.Handled = true;
        }
    }
}
