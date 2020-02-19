using System;
using System.IO;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace SCTBuilder
{
    public partial class LineGenerator : Form
    {
        public LineGenerator()
        {
            InitializeComponent();
        }

        private void LineGenerator_Load(object sender, EventArgs e)
        {

        }

        private void Copy2ClipboardButton_Click(object sender, EventArgs e)
        {
            MessageBoxIcon icon = MessageBoxIcon.Information;
            string Msg;
            if (OutputTextBox.TextLength != 0)
            {
                Clipboard.Clear();
                Clipboard.SetText(OutputTextBox.ToString());
                Msg = "Contents of output textbox copied to clipboard";
            }
            else
            {
                Msg = "No text in output textbox to copy!";
                icon = MessageBoxIcon.Warning;
            }
            SendMessage(Msg, icon);
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
                        SendMessage(Msg);
                    }
                }
                else
                {
                    Msg = "No file selected; file not saved.";
                    MessageBoxIcon icon = MessageBoxIcon.Warning;
                    SendMessage(Msg, icon);
                }
            }
            else
            {
                Msg = "No text in output textbox to copy!";
                MessageBoxIcon icon = MessageBoxIcon.Warning;
                SendMessage(Msg, icon);
            }
        }

        private void ClearOutputButton_Click(object sender, EventArgs e)
        {
            OutputTextBox.Text = string.Empty;
            OutputTextBox.Refresh();
        }

        private void LatLongHelpButton_Click(object sender, EventArgs e)
        {
            Form form = new LatLongHelp();
            form.ShowDialog(this);
            form.Dispose();
        }

        private void SendMessage(string Msg, MessageBoxIcon icon = MessageBoxIcon.Information,
            MessageBoxButtons buttons = MessageBoxButtons.OK)
        {
            MessageBox.Show(Msg, VersionInfo.Title, buttons, icon);
        }

        private void CopyStart2EndButton_Click(object sender, EventArgs e)
        {
            EndLatitudeTextBox.Text = StartLatitudeTextBox.Text;
            EndLongitudeTextBox.Text = StartLongitudeTextBox.Text;
            EndFixTextBox.Text = StartFixTextBox.Text;
            UpdateCopyButtons();
        }
        private void CopyEnd2StartButton_Click(object sender, EventArgs e)
        {
            StartLatitudeTextBox.Text = EndLatitudeTextBox.Text;
            StartLongitudeTextBox.Text = EndLongitudeTextBox.Text;
            StartFixTextBox.Text = EndFixTextBox.Text;
            UpdateCopyButtons();
        }

        private void SwitchStartEndButton_Click(object sender, EventArgs e)
        {
            string latitude; string longitude; string fix;
            latitude = StartLatitudeTextBox.Text;
            longitude = StartLongitudeTextBox.Text;
            fix = StartFixTextBox.Text;
            StartLatitudeTextBox.Text = EndLatitudeTextBox.Text;
            StartLongitudeTextBox.Text = EndLongitudeTextBox.Text;
            StartFixTextBox.Text = EndFixTextBox.Text;
            EndLatitudeTextBox.Text = latitude;
            EndLongitudeTextBox.Text = longitude;
            EndFixTextBox.Text = fix;
            UpdateCopyButtons();
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
                        ImportFix2EndButton.Enabled = ImportFix2StartButton.Enabled = true;
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
                ImportFix2EndButton.Enabled = ImportFix2StartButton.Enabled = false;
            }
        }

        private void ImportFix2StartButton_Click(object sender, EventArgs e)
        {
            StartLatitudeTextBox.Text =
                Conversions.DecDeg2SCT(Convert.ToDouble(FixListDataGridView.SelectedRows[0].Cells[1].Value), true);
            StartLongitudeTextBox.Text =
                Conversions.DecDeg2SCT(Convert.ToDouble(FixListDataGridView.SelectedRows[0].Cells[2].Value), true);
            StartFixTextBox.Text = FixListDataGridView.SelectedRows[0].Cells[0].Value.ToString();
            UpdateCopyButtons();
        }

        private void ImportFix2EndButton_Click(object sender, EventArgs e)
        {
            EndLatitudeTextBox.Text =
                Conversions.DecDeg2SCT(Convert.ToDouble(FixListDataGridView.SelectedRows[0].Cells[1].Value), true);
            EndLongitudeTextBox.Text =
                Conversions.DecDeg2SCT(Convert.ToDouble(FixListDataGridView.SelectedRows[0].Cells[2].Value), true);
            EndFixTextBox.Text = FixListDataGridView.SelectedRows[0].Cells[0].Value.ToString();
            UpdateCopyButtons();
        }

        private void CalcBearingTextBox_TextChanged(object sender, EventArgs e)
        {
            MessageBoxIcon icon = MessageBoxIcon.Warning;
            string Msg = string.Empty;
            if (CalcBearingTextBox.TextLength != 0)
            {
                //    Msg = "Value must be numeric in range 0 to 360.";
                //}
                //else
                //{
                int test = Convert.ToInt32(CalcBearingTextBox.Text);
                if ((test > 360) | (test < 0))
                {
                    Msg = "Value must be 0 to 360.";
                }
                else
                    CalcBearingTextBox.Text = test.ToString();
                //}
                if (Msg.Length != 0)
                {
                    SendMessage(Msg, icon);
                    CalcBearingTextBox.Text = string.Empty;
                }
                UpdateCalcButton();
            }
        }

        private void CalcMagVarTextBox_TextChanged(object sender, EventArgs e)
        {
            if (CalcMagVarTextBox.TextLength != 0)
            {
                string cr = Environment.NewLine;
                MessageBoxIcon icon = MessageBoxIcon.Warning;
                string Msg = string.Empty;
                //if (Extensions.IsNumeric(CalcMagVarTextBox.Text))
                //{
                //    Msg = "Value must be numeric.";
                //}
                //else
                //{
                int test = Convert.ToInt32(CalcMagVarTextBox.Text);
                if ((test < -40) | (test > 30))
                {
                    Msg = "Are you sure?" +
                        "Historical extremes: US +30, EU -40" + cr +
                        "and cannot exceed -140 to 140.";
                }
                else
                    CalcMagVarTextBox.Text = test.ToString();
                //}
                if (Msg.Length != 0)
                {
                    SendMessage(Msg, icon);
                    CalcMagVarTextBox.Text = string.Empty;
                }
            }
            UpdateCalcButton();
        }

        private void UpdateCopyButtons()
        {
            // Update the CopyTo buttons and Line details
            CopyStart2EndButton.Enabled = (StartLatitudeTextBox.TextLength != 0) && (StartLongitudeTextBox.TextLength != 0);
            CopyEnd2StartButton.Enabled = (EndLatitudeTextBox.TextLength != 0) && (EndLongitudeTextBox.TextLength != 0);
            SwitchStartEndButton.Enabled = CopyEnd2StartButton.Enabled | CopyStart2EndButton.Enabled;
            AddLineButton.Enabled = CopyEnd2StartButton.Enabled && CopyStart2EndButton.Enabled;
            AddNextButton.Enabled = AddLineButton.Enabled;
            if (AddNextButton.Enabled)
            {
                float Lat1 = Conversions.String2DecDeg(StartLatitudeTextBox.Text);
                float Lon1 = Conversions.String2DecDeg(StartLongitudeTextBox.Text);
                float Lat2 = Conversions.String2DecDeg(EndLatitudeTextBox.Text);
                float Lon2 = Conversions.String2DecDeg(EndLongitudeTextBox.Text);
                char Type = 'N';
                decimal Brg = Convert.ToDecimal(LatLongCalc.Bearing(Lat1, Lon1, Lat2, Lon2));
                decimal Dist = Convert.ToDecimal(LatLongCalc.Distance(Lat1, Lon1, Lat2, Lon2, Type));
                LineBrgTextBox.Text = decimal.Round(Brg, 2, MidpointRounding.AwayFromZero).ToString();
                LineDistTextBox.Text = decimal.Round(Dist, 2, MidpointRounding.AwayFromZero).ToString();
            }
            else
            {
                LineBrgTextBox.Text = string.Empty;
                LineDistTextBox.Text = string.Empty;
            }
        }

        private void UpdateCalcButton()
        {
            // Update the Calculate button
            CalcEndButton.Enabled =
                (CalcDistanceTextBox.TextLength != 0) && (CalcBearingTextBox.TextLength != 0)
                && (CalcMagVarTextBox.TextLength != 0)
                && (StartLatitudeTextBox.TextLength != 0) && (StartLongitudeTextBox.TextLength != 0);
        }

        private void PrefixTextBox_TextChanged(object sender, EventArgs e)
        {
            MessageBoxIcon icon = MessageBoxIcon.Warning;
            if (PrefixTextBox.TextLength > 26)
            {
                string Msg = "Prefix may not be longer than 26 characters.";
                SendMessage(Msg, icon);
                PrefixTextBox.Text = string.Empty;
            }
        }

        private void DashedLineLengthTextBox_TextChanged(object sender, EventArgs e)
        {
            MessageBoxIcon icon = MessageBoxIcon.Warning;
            if (CalcDistanceTextBox.TextLength != 0)
            {
                string Msg = "Value must be a number in a range from 0.25 to 10.";
                if (!Extensions.IsNumeric(CalcMagVarTextBox.Text))
                {
                    SendMessage(Msg, icon);
                    CalcDistanceTextBox.Text = string.Empty;
                }
                else
                {
                    float dist = Convert.ToSingle(CalcDistanceTextBox.Text);
                    if ((dist < 0.25) | (dist > 10f))
                    {
                        SendMessage(Msg, icon);
                        CalcDistanceTextBox.Text = string.Empty;
                    }
                }
            }
        }

        private void FixListDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            ImportFix2EndButton.Enabled = ImportFix2StartButton.Enabled = FixListDataGridView.SelectedRows.Count != 0;
        }

        private void CalcBearingTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= '0' && e.KeyChar <= '9' || e.KeyChar == '.' || e.KeyChar == (char)8) //The  character represents a backspace
            {
                e.Handled = false; //Do not reject the input
            }
            else
            {
                e.Handled = true; //Reject the input
            }
        }

        private void CalcMagVarTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= '0' && e.KeyChar <= '9' || e.KeyChar == '.' || e.KeyChar == (char)8 || e.KeyChar == '-') //The  character represents a backspace
            {
                e.Handled = false; //Do not reject the input
            }
            else
            {
                e.Handled = true; //Reject the input
            }
        }

        private void CalcDistanceTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= '0' && e.KeyChar <= '9' || e.KeyChar == '.' || e.KeyChar == (char)8) //The  character represents a backspace
            {
                e.Handled = false; //Do not reject the input
            }
            else
            {
                e.Handled = true; //Reject the input
            }
        }

        private void CalcDistanceTextBox_Validated(object sender, EventArgs e)
        {
            UpdateCalcButton();
        }

        private void CalcEndButton_Click(object sender, EventArgs e)
        {
            /// Summary 
            /// "West is Best, East is least"
            /// To calculate true bearing from compass bearing (and known deviation and variation):
            ///     Compass bearing +deviation = magnetic bearing
            ///     Magnetic bearing + variation = true bearing
            /// To calculate compass bearing from true bearing(and known deviation and variation):
            ///     True bearing -variation = Magnetic bearing
            ///     Magnetic bearing - deviation = Compass bearing
            /// In the US, the agonic line is roughly along the Mississippi river
            /// In aviation, maps, GPS and runways use true bearings
            double trueBrg = Convert.ToDouble(CalcBearingTextBox.Text) + Convert.ToDouble(CalcMagVarTextBox.Text);
            double Lat1 = Conversions.String2DecDeg(StartLatitudeTextBox.Text);
            double Lon1 = Conversions.String2DecDeg(StartLongitudeTextBox.Text);
            double Dist = Convert.ToDouble(CalcDistanceTextBox.Text);
            double[] CalcLocation =
                LatLongCalc.CalcLocation(Lat1, Lon1, DistanceAdjust(Dist), trueBrg);
            double Lat2 = CalcLocation[0];
            double Lon2 = CalcLocation[1];
            EndLatitudeTextBox.Text = Conversions.DecDeg2SCT(Lat2, true);
            EndLongitudeTextBox.Text = Conversions.DecDeg2SCT(Lon2, false);
            EndFixTextBox.Text = string.Empty;
            UpdateCopyButtons();
        }

        private double DistanceAdjust(double Distance)
        {
            if (CalcDistSMRadioButton.Checked) return Distance * 1.15078;
            if (CalcDistMeterRadioButton.Checked) return Distance * 1852;
            if (CalcDistFeetRadioButton.Checked) return Distance * 6076.12;
            return Distance;
        }

        private void ColorValueTextBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                Color c = colorDialog1.Color;
                ColorValueTextBox.Text = c.ToString();
                if (ColorNameTextBox.TextLength != 0)
                    ColorNameTextBox.Text = "#" + c.R.ToString("X2") + c.G.ToString("X2") + c.B.ToString("X2");
            }
        }

        private void StartLatitudeTextBox_Validated(object sender, EventArgs e)
        {
            if (StartLatitudeTextBox.TextLength != 0)
            {
                double testvalue;
                MessageBoxIcon icon = MessageBoxIcon.Warning;
                string cr = Environment.NewLine; string Msg = string.Empty;
                if (Extensions.IsNumeric(StartLatitudeTextBox.Text))
                {
                    testvalue = Convert.ToSingle(StartLatitudeTextBox.Text);
                    if (Math.Abs(testvalue) > 90)
                    {
                        Msg = "Latitude value must fall between -90 and 90.";
                    }
                }
                else
                {
                    testvalue = Conversions.String2DecDeg(StartLatitudeTextBox.Text);
                    if (testvalue == -1)
                    {
                        Msg = "You used an invalid format or "
                            + cr + "your latitude is out of range (-90 to 90)."
                            + cr + "Click the question mark for help on formats.";
                    }
                    else StartLatitudeTextBox.Text = Conversions.DecDeg2SCT(testvalue, true);
                }

                if (Msg.Length != 0)
                {
                    SendMessage(Msg, icon);
                    StartLatitudeTextBox.Text = string.Empty;
                }
            }
            UpdateCopyButtons();
        }

        private void StartLongitudeTextBox_Validated(object sender, EventArgs e)
        {
            if (StartLongitudeTextBox.TextLength != 0)
            {
                double testvalue;
                MessageBoxIcon icon = MessageBoxIcon.Warning;
                string cr = Environment.NewLine; string Msg = string.Empty;
                if (Extensions.IsNumeric(StartLongitudeTextBox.Text))
                {
                    testvalue = Convert.ToSingle(StartLongitudeTextBox.Text);
                    if (Math.Abs(testvalue) > 180)
                    {
                        Msg = "Longitude value must fall between -180 and 180.";
                    }
                }
                else
                {
                    testvalue = Conversions.String2DecDeg(StartLongitudeTextBox.Text);
                    if (testvalue == -1)
                    {
                        Msg = "You used an invalid format or "
                            + cr + "your longitude is out of range (-180 to 180)."
                            + cr + "Click the question mark for help on formats.";
                    }
                    else StartLongitudeTextBox.Text = Conversions.DecDeg2SCT(testvalue, false);
                }
                if (Msg.Length != 0)
                {
                    SendMessage(Msg, icon);
                    StartLongitudeTextBox.Text = string.Empty;
                }
            }
            UpdateCopyButtons();
        }

        private void EndLatitudeTextBox_Validated(object sender, EventArgs e)
        {
            if (EndLatitudeTextBox.TextLength != 0)
            {
                double testvalue;
                MessageBoxIcon icon = MessageBoxIcon.Warning;
                string cr = Environment.NewLine; string Msg = string.Empty;
                if (Extensions.IsNumeric(EndLatitudeTextBox.Text))
                {
                    testvalue = Convert.ToSingle(EndLatitudeTextBox.Text);
                    if (Math.Abs(testvalue) > 90)
                    {
                        Msg = "Latitude value must fall between -90 and 90.";
                    }
                    else EndLatitudeTextBox.Text = Conversions.DecDeg2SCT(testvalue, false);
                }
                else
                {
                    testvalue = Conversions.String2DecDeg(EndLatitudeTextBox.Text);
                    if (testvalue == -1)
                    {
                        Msg = "You used an invalid format or "
                            + cr + "your latitude is out of range (-90 to 90)."
                            + cr + "Click the question mark for help on formats.";
                    }
                }
                if (Msg.Length != 0)
                {
                    SendMessage(Msg, icon);
                    EndLatitudeTextBox.Text = string.Empty;
                }
            }
            UpdateCopyButtons();
        }

        private void EndLongitudeTextBox_Validated(object sender, EventArgs e)
        {
            if (EndLongitudeTextBox.TextLength != 0)
            {
                double testvalue;
                MessageBoxIcon icon = MessageBoxIcon.Warning;
                string cr = Environment.NewLine; string Msg = string.Empty;
                if (Extensions.IsNumeric(EndLongitudeTextBox.Text))
                {
                    testvalue = Convert.ToSingle(EndLongitudeTextBox.Text);
                    if (Math.Abs(testvalue) > 180)
                    {
                        Msg = "Longitude value must fall between -180 and 180.";
                    }
                }
                else
                {
                    testvalue = Conversions.String2DecDeg(EndLongitudeTextBox.Text);
                    if (testvalue == -1)
                    {
                        Msg = "You used an invalid format or "
                            + cr + "your longitude is out of range (-180 to 180)."
                            + cr + "Click the question mark for help on formats.";
                    }
                    else EndLongitudeTextBox.Text = Conversions.DecDeg2SCT(testvalue, false);
                }
                if (Msg.Length != 0)
                {
                    SendMessage(Msg, icon);
                    EndLongitudeTextBox.Text = string.Empty;
                }
            }
            UpdateCopyButtons();
        }

        private void AddLineButton_Click(object sender, EventArgs e)
        {
            AddLine();
        }

        private void AddLine()
        {
            // Need to add intelligent commenting here ; FIX FIX or ; Comment
            string cr = Environment.NewLine;
            if (SSDRadioButton.Checked)
            {
                OutputTextBox.Text += SCTstrings.SSDout(StartLatitudeTextBox.Text, StartLongitudeTextBox.Text,
                    EndLatitudeTextBox.Text, EndLongitudeTextBox.Text);
            }
            if (AirwayRadioButton.Checked)
            {
                OutputTextBox.Text += SCTstrings.AWYout(PrefixTextBox.Text, StartLatitudeTextBox.Text, 
                    StartLongitudeTextBox.Text, EndLatitudeTextBox.Text, EndLongitudeTextBox.Text, 
                    StartFixTextBox.Text, EndFixTextBox.Text, UseFIXNamesCheckBox.Checked) + cr;
            }
            if (ARTCCRadioButton.Checked)
            {
                OutputTextBox.Text += SCTstrings.BoundaryOut(PrefixTextBox.Text, StartLatitudeTextBox.Text,
                    StartLongitudeTextBox.Text, EndLatitudeTextBox.Text, EndLongitudeTextBox.Text);
            }
        }

        private void AirwayRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (AirwayRadioButton.Checked)
                PrefixLabel.Text = "Prefix Req'd";
        }

        private void ARTCCRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (ARTCCRadioButton.Checked)
                PrefixLabel.Text = "Prefix Req'd";
        }

        private void SSDRadioButton_CheckedChanged(object sender, EventArgs e)
        {
        }
    }
}
