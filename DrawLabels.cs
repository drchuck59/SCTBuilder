using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace SCTBuilder
{
    public partial class DrawLabels : Form
    {
        public DrawLabels()
        {
            InitializeComponent();
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
                    SCTcommon.SendMessage(Msg);
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
                    SCTcommon.SendMessage(Msg);
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
                    SCTcommon.SendMessage(Msg);
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
                    SCTcommon.SendMessage(Msg);
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
                    SCTcommon.SendMessage(Msg);
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
                SCTcommon.SendMessage(Msg);
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
                SCTcommon.SendMessage(Msg);
                ScaleTextBox.Text = string.Empty;
            }
        }

        private void DrawButton_Click(object sender, EventArgs e)
        {
            string result = string.Empty;
            object[] NavData; string curFix;
            double Lat1 = Conversions.String2DecDeg(LatTextBox.Text, ".");
            double Lon1 = Conversions.String2DecDeg(LonTextBox.Text, ".");
            int Brg = Convert.ToInt32(BearingTextBox.Text) - 90;            // Rotation in addition to MagVar
            float Scale = Convert.ToSingle(ScaleTextBox.Text);              // Scaling beyond internal 1/3600
            if (IncludeSymbolCheckBox.Checked && (FixListDataGridView.CurrentRow != null))
            {
                curFix = FixListDataGridView.CurrentRow.Cells[0].Value.ToString();
                NavData = SCTcommon.GetNavData(curFix);
                result += Hershey.DrawSymbol(NavData);
            }
            result += Hershey.WriteHF(LabelTextBox.Text, Lat1, Lon1, Brg, Scale);
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
            bool CanUseFix = (CycleInfo.AIRAC != 1501);
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
            if (Extensions.IsValidDecCoordKey(e.KeyChar, LatTextBox))
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void Copy2ClipboardButton_Click(object sender, EventArgs e)
        {
            string Msg;
            if (OutputTextBox.TextLength != 0)
            {
                Clipboard.Clear();
                Clipboard.SetText(OutputTextBox.Text.ToString());
                Msg = "Contents of output textbox copied to clipboard";
            }
            else
            {
                Msg = "No text in output textbox to copy!";
            }
            SCTcommon.SendMessage(Msg, MessageBoxIcon.Information);
        }

        private void SaveOutput2FileButton_Click(object sender, EventArgs e)
        {
            string Msg;
            if (OutputTextBox.TextLength != 0)
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog
                {
                    Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*",
                    FilterIndex = 2,
                    RestoreDirectory = true
                };
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    if (saveFileDialog1.FileName.Length != 0)
                    {
                        string path = saveFileDialog1.FileName;
                        File.WriteAllText(path, OutputTextBox.Text);
                        Msg = "Output written to " + path;
                        SCTcommon.SendMessage(Msg);
                    }
                }
                else
                {
                    Msg = "No file selected; file not saved.";
                    SCTcommon.SendMessage(Msg);
                }
            }
            else
            {
                Msg = "No text in output textbox to save!";
                SCTcommon.SendMessage(Msg);
            }
        }
    }
}
