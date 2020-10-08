using System;
using System.IO;
using System.Data;
using System.Windows.Forms;
using System.Drawing;
using System.Linq;
using System.Globalization;
using System.Collections.Generic;

namespace SCTBuilder
{
    public partial class LineGenerator : Form
    {
        private static double StartLat;
        private static double StartLon;
        private static double EndLat;
        private static double EndLon;
        private static double Distance;
        private static double PasteLat;
        private static double PasteLon;
        private static double DashLength = 0.25;        // This is in NM. Convert to use
        private static double TrueBrg;     // From the Lat-Long coordinates
        private static double CalcDist;
        private static char CalcType = 'N';
        private static double CalcBrg;
        private static double CalcMag = InfoSection.MagneticVariation;
        private static string OutputType = "SSD";
        string Msg = string.Empty;
        readonly string cr = Environment.NewLine;
        MessageBoxIcon icon = MessageBoxIcon.Warning;

        public LineGenerator()
        {
            InitializeComponent();
        }

        private void LineGenerator_Load(object sender, EventArgs e)
        {
            CalcMagVarTextBox.Text = InfoSection.MagneticVariation.ToString();
        }

        private void Copy2ClipboardButton_Click(object sender, EventArgs e)
        {
            icon = MessageBoxIcon.Information;
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
            SCTcommon.SendMessage(Msg, icon);
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

        private void CopyStart2EndButton_Click(object sender, EventArgs e)
        {
            EndLatitudeTextBox.Text = StartLatitudeTextBox.Text;
            EndLat = StartLat;
            EndLongitudeTextBox.Text = StartLongitudeTextBox.Text;
            EndLon = StartLon;
            EndFixTextBox.Text = StartFixTextBox.Text;
            UpdateCopyButtons();
        }
        private void CopyEnd2StartButton_Click(object sender, EventArgs e)
        {
            StartLatitudeTextBox.Text = EndLatitudeTextBox.Text;
            StartLat = EndLat;
            StartLongitudeTextBox.Text = EndLongitudeTextBox.Text;
            StartLon = EndLon;
            StartFixTextBox.Text = EndFixTextBox.Text;
            UpdateCopyButtons();
        }

        private void SwitchStartEndButton_Click(object sender, EventArgs e)
        {
            string latitude; string longitude; string fix; double Lat1; double Lon1;
            latitude = StartLatitudeTextBox.Text;
            Lat1 = StartLat;
            longitude = StartLongitudeTextBox.Text;
            Lon1 = StartLon;
            fix = StartFixTextBox.Text;
            StartLatitudeTextBox.Text = EndLatitudeTextBox.Text;
            StartLat = EndLat;
            StartLongitudeTextBox.Text = EndLongitudeTextBox.Text;
            StartLon = EndLon;
            StartFixTextBox.Text = EndFixTextBox.Text;
            EndLatitudeTextBox.Text = latitude;
            EndLat = Lat1;
            EndLongitudeTextBox.Text = longitude;
            EndLon = Lon1;
            EndFixTextBox.Text = fix;
            Refresh();
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
                    SCTcommon.SendMessage(Msg);
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
            StartLat = Convert.ToDouble(FixListDataGridView.SelectedRows[0].Cells[1].Value);
            StartLongitudeTextBox.Text =
                Conversions.DecDeg2SCT(Convert.ToDouble(FixListDataGridView.SelectedRows[0].Cells[2].Value), false);
            StartLon = Convert.ToDouble(FixListDataGridView.SelectedRows[0].Cells[2].Value);
            StartFixTextBox.Text = FixListDataGridView.SelectedRows[0].Cells[0].Value.ToString();
            UpdateCopyButtons();
        }

        private void ImportFix2EndButton_Click(object sender, EventArgs e)
        {
            EndLatitudeTextBox.Text =
                Conversions.DecDeg2SCT(Convert.ToDouble(FixListDataGridView.SelectedRows[0].Cells[1].Value), true);
            EndLat = Convert.ToDouble(FixListDataGridView.SelectedRows[0].Cells[1].Value);
            EndLongitudeTextBox.Text =
                Conversions.DecDeg2SCT(Convert.ToDouble(FixListDataGridView.SelectedRows[0].Cells[2].Value), false);
            EndLon = Convert.ToDouble(FixListDataGridView.SelectedRows[0].Cells[2].Value);
            EndFixTextBox.Text = FixListDataGridView.SelectedRows[0].Cells[0].Value.ToString();
            UpdateCopyButtons();
        }

        private void CalcBearingTextBox_TextChanged(object sender, EventArgs e)
        {
            string Msg = string.Empty;
            if (CalcBearingTextBox.TextLength != 0)
            {
                double test = Convert.ToDouble(CalcBearingTextBox.Text);
                if ((test > 360) | (test < 1))
                {
                    Msg = "Value must be 1 to 360.";
                }
                else
                {
                    CalcBearingTextBox.Text = test.ToString();
                    CalcBrg = test;
                }
                if (Msg.Length != 0)
                {
                    SCTcommon.SendMessage(Msg);
                    CalcBearingTextBox.Text = string.Empty;
                }
                UpdateCalcButton();
            }
        }

        private void CalcMagVarTextBox_TextChanged(object sender, EventArgs e)
        {
            if (CalcMagVarTextBox.TextLength != 0)
            {
                int test = Convert.ToInt32(CalcMagVarTextBox.Text);
                if ((test < -40) | (test > 30))
                {
                    Msg = "Are you sure?" +
                        "Historical extremes: US +30, EU -40" + cr +
                        "and cannot exceed -140 to 140.";
                }
                else
                {
                    CalcMagVarTextBox.Text = test.ToString();
                    CalcMag = test;
                }
                if (Msg.Length != 0)
                {
                    SCTcommon.SendMessage(Msg);
                    CalcMagVarTextBox.Text = string.Empty;
                    CalcMag = 0;
                }
            }
            UpdateCalcButton();
        }

        private void ButtonBackColor(Button button)
        {
            if (button.Enabled) button.BackColor = Color.White;
            else button.BackColor = Color.Gray; 
        }

        private void UpdateCopyButtons()
        {
            // Update the CopyTo buttons and Line details
            CopyStart2EndButton.Enabled = (StartLatitudeTextBox.TextLength != 0) && (StartLongitudeTextBox.TextLength != 0);
            ButtonBackColor(CopyStart2EndButton);
            CopyEnd2StartButton.Enabled = (EndLatitudeTextBox.TextLength != 0) && (EndLongitudeTextBox.TextLength != 0);
            ButtonBackColor(CopyEnd2StartButton);
            SwitchStartEndButton.Enabled = CopyEnd2StartButton.Enabled | CopyStart2EndButton.Enabled;
            ButtonBackColor(SwitchStartEndButton);
            AddLineButton.Enabled = CopyEnd2StartButton.Enabled && CopyStart2EndButton.Enabled;
            if (GEORadioButton.Checked && SuffixTextBox.TextLength == 0)
                AddLineButton.Enabled = false;
            ButtonBackColor(AddLineButton);
            AddNextButton.Enabled = AddLineButton.Enabled;
            ButtonBackColor(AddNextButton);
            if (AddLineButton.Enabled)
                UpdateDistBrg();
            else
                ClearDistBrg();
        }

        private void UpdateCalcButton()
        {
            // Update the Calculate button
            CalcEndButton.Enabled =
                (CalcDistanceTextBox.TextLength != 0) && (CalcBearingTextBox.TextLength != 0)
                && (CalcMagVarTextBox.TextLength != 0)
                && (StartLatitudeTextBox.TextLength != 0) && (StartLongitudeTextBox.TextLength != 0);
            ButtonBackColor(CalcEndButton);
        }

        private void UpdateDistBrg()
        {
            Distance = LatLongCalc.Distance(StartLat, StartLon, EndLat, EndLon, 'N');
            DispDistTextBox.Text = decimal.Round(Convert.ToDecimal(Distance), 2, MidpointRounding.AwayFromZero).ToString();
            TrueBrg = LatLongCalc.Bearing(StartLat, StartLon, EndLat, EndLon);
            DispBrgTextBox.Text = decimal.Round(Convert.ToDecimal(TrueBrg), 2, MidpointRounding.AwayFromZero).ToString();
        }

        private void ClearDistBrg()
        {
            Distance = 0;
            DispBrgTextBox.Text = string.Empty;
            DispDistTextBox.Text = string.Empty;
        }

        private void PrefixTextBox_TextChanged(object sender, EventArgs e)
        {
            if (PrefixTextBox.TextLength > 26)
            {
                string Msg = "Prefix may not be longer than 26 characters.";
                SCTcommon.SendMessage(Msg);
                PrefixTextBox.Text = string.Empty;
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
            if (!string.IsNullOrEmpty(CalcDistanceTextBox.Text))
                CalcDist = Convert.ToDouble(CalcDistanceTextBox.Text);
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
            /// West Variations are negative
            /// In the US, the agonic line is roughly along the Mississippi river
            /// In aviation, maps, GPS and runways use true bearings
            double CalcTrueBrg = CalcBrg;
            if (MagBrgCheckBox.Checked)
                CalcTrueBrg -= CalcMag;
            double[] CalcLocation =
                LatLongCalc.Destination(StartLat, StartLon, DistanceAdjust(CalcDist, CalcType), CalcTrueBrg, CalcType);
            EndLat = CalcLocation[0];
            EndLon = CalcLocation[1];
            EndLatitudeTextBox.Text = Conversions.DecDeg2SCT(EndLat, true);
            EndLongitudeTextBox.Text = Conversions.DecDeg2SCT(EndLon, false);
            EndFixTextBox.Text = string.Empty;
            UpdateCopyButtons();
        }

        private static double DistanceAdjust(double Distance, char Type)
        {
            // Convert to Nautical Miles
            double result;
            switch (Type)
            {
                default:
                case 'N':
                    result = Distance;
                    break;
                case 'm':
                    result = Distance / 1852;
                    break;
                case 'f':
                    result = Distance / 6076.12;
                    break;
                case 'S':
                    result = Distance / 1.151;
                    break;
            }
            return result;
        }

        private void ColorValueTextBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                string colorARGB; string colorName;
                Color c = colorDialog1.Color;
                colorName = c.ToString();
                colorARGB = c.R.ToString("X2") + c.G.ToString("X2") + c.B.ToString("X2");
                if (Extensions.IsAColor(colorName)) 
                    ColorNameTextBox.Text = colorName;
                else
                    ColorNameTextBox.Text = "#" + colorARGB;
                colorARGB = c.R.ToString("X2") + c.G.ToString("X2") + c.B.ToString("X2");
                ColorValueTextBox.Text = int.Parse(colorARGB, System.Globalization.NumberStyles.HexNumber).ToString();
            }
        }

        private void ColorNameTextBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            string colorName = ColorNameTextBox.Text;
            string colorValue = Extensions.Right(ColorValueTextBox.Text, ColorValueTextBox.TextLength - 1);
            const string label = "#define";
            if (colorName.Length == 0)
                SCTcommon.SendMessage("Need a name to copy to #define.", MessageBoxIcon.Error);
            else
            {
                if (colorName.Substring(0, 1) == "#")
                    SCTcommon.SendMessage("Name cannot begin with '#' - enter a new name", MessageBoxIcon.Error);
                else
                {
                    if (!Extensions.IsNumeric(colorValue))
                        SCTcommon.SendMessage("colorValue must be numeric.", MessageBoxIcon.Error);
                    else
                        Clipboard.SetText(label + " " + colorName + " " + colorValue);
                }
            }
        }

        private double TestTextBox(TextBox tb)
        {
            double ParsedResult = -199;
            int method = 0;
            if (tb.Name.IndexOf("Lat") != -1) method = 1;
            if (tb.Name.IndexOf("Lon") != -1) method = 2;
            if ((tb.Modified) && tb.TextLength != 0)
            {
                if (LatLonParser.TryParseAny(tb))
                {
                    switch (method)
                    {
                        case 0:
                            PasteLat = ParsedResult = LatLonParser.ParsedLatitude;
                            PasteLon = LatLonParser.ParsedLongitude;
                            tb.Text = Conversions.DecDeg2SCT(PasteLat, true) + " " +
                                Conversions.DecDeg2SCT(PasteLon, false);
                            break;
                        case 1:
                            PasteLat = ParsedResult = LatLonParser.ParsedLatitude;
                            PasteLon = -199;
                            tb.Text = Conversions.DecDeg2SCT(ParsedResult, true);
                            break;
                        case 2:
                            PasteLon = ParsedResult = LatLonParser.ParsedLongitude;
                            PasteLat = -199;
                            tb.Text = Conversions.DecDeg2SCT(ParsedResult, false);
                            break;
                    }
                    tb.BackColor = Color.White; 
                }
                else tb.BackColor = Color.Yellow;
            }
            return ParsedResult;
        }

        private void StartLatitudeTextBox_Validated(object sender, EventArgs e)
        {
            double result = TestTextBox(StartLatitudeTextBox);
            if (result != -199)
            {
                StartLat = PasteLat;
                if (PasteLon != -199) StartLon = PasteLon;
                UpdateCopyButtons();
            }
        }

        private void StartLongitudeTextBox_Validated(object sender, EventArgs e)
        {
            double result = TestTextBox(StartLongitudeTextBox);
            if (result != -199)
            {
                StartLon = PasteLon;
                if (PasteLat != -199) StartLat = PasteLat;
                UpdateCopyButtons();
            }
        }

        private void EndLatitudeTextBox_Validated(object sender, EventArgs e)
        {
            double result = TestTextBox(EndLatitudeTextBox);
            if (result != -199)
            {
                EndLat = PasteLat;
                if (PasteLon != -199) EndLon = PasteLon;
                UpdateCopyButtons();
            }
        }

        private void EndLongitudeTextBox_Validated(object sender, EventArgs e)
        {
            double result = TestTextBox(EndLongitudeTextBox);
            if (result != -199)
            {
                EndLon = PasteLon;
                if (PasteLat != -199) EndLat = PasteLat;
                UpdateCopyButtons();
            }
        }

        private void AddLineButton_Click(object sender, EventArgs e)
        {
            AddLine();
        }

        private void AddLine()
        {
            // Purpose - to Output a series of lines based upon user options
            // RETURNS - Nothing; writes a string to the Output Textbox
            string cr = Environment.NewLine;
            string Msg;
                // Create the list of points for the line (if not dashed, returns original points)
                string[] strOut = new string[4];
                strOut[0] = strOut[1] = strOut[2] = strOut[3] = string.Empty;
                double[][] Lines = DashedLine(DashedLineRadioButton.Checked);
            if (SSDRadioButton.Checked)
            {
                foreach (double[] Line in Lines)
                {
                    if (Line[0] == -1)
                    {
                        strOut[2] = string.Empty; strOut[3] = string.Empty;
                    }
                    else
                    {
                        strOut[2] = Conversions.DecDeg2SCT(Line[0], true);
                        strOut[3] = Conversions.DecDeg2SCT(Line[1], false);
                    }
                    if ((strOut[0].Length != 0) && (strOut[2].Length != 0))
                    {
                        switch (OutputType)
                        {
                            case "SSD":
                                OutputTextBox.Text += SCTstrings.SSDout(strOut[0], strOut[1],
                                    strOut[2], strOut[3]) + cr;
                                break;
                            case "AWY":
                                if (PrefixTextBox.TextLength != 0)
                                    OutputTextBox.Text += SCTstrings.AWYout(PrefixTextBox.Text, strOut[0], strOut[1],
                                    strOut[2], strOut[3], StartFixTextBox.Text, EndFixTextBox.Text) + cr;
                                else
                                {
                                    Msg = "The Airway identifier is required for this format." + cr + "(Place in the prefix text box.)";
                                    SCTcommon.SendMessage(Msg);
                                    PrefixTextBox.Focus();
                                }
                                break;
                            case "ARTCC":
                                if (PrefixTextBox.TextLength != 0)
                                {
                                    OutputTextBox.Text += SCTstrings.BoundaryOut(PrefixTextBox.Text, strOut[0], strOut[1],
                                      strOut[2], strOut[3]);
                                    if (SuffixTextBox.TextLength != 0) OutputTextBox.Text += SuffixTextBox.Text;
                                    OutputTextBox.Text += cr;
                                }
                                else
                                {
                                    Msg = "The ARTCC identifier is required for this format." + cr + "(Place in the prefix text box.)";
                                    SCTcommon.SendMessage(Msg);
                                    PrefixTextBox.Focus();
                                }
                                break;
                            case "GEO":
                                OutputTextBox.Text += SCTstrings.GeoOut(strOut[0], strOut[1],
                                    strOut[2], strOut[3], SuffixTextBox.Text) + cr;
                                break;
                        }
                    }
                    strOut[0] = strOut[2]; strOut[1] = strOut[3];
                }
            }
        }

        private static double[][] DashedLine(bool IsDashed)
        {
            // PURPOSE - To generate a series of decimal-degree coordinates for output
            // RETURNS - One line or a series of dashed lines
            List<double[]> result = new List<double[]>();
            if (!IsDashed)
            {
                result.Add(new double[] { StartLat, StartLon });
                result.Add(new double[] { EndLat, EndLon });
            }
            else
            {
                double rise = EndLat - StartLat;
                double run = EndLon - StartLon;
                double Lat0 = StartLat; double Lon0 = StartLon;
                double slope = rise / run;
                double SpaceDegrees = 0.25f / InfoSection.NMperDegreeLongitude;      // 1/4 NM to degrees
                double DashDegrees = DashLength / InfoSection.NMperDegreeLongitude;
                // start line at origin
                result.Add(new double[] { StartLat, StartLon });
                while (Lon0 < EndLon)
                {
                    // end of line short-of endpoint
                    if ((Lon0 + DashDegrees) < EndLon)
                    {
                        Lon0 += DashDegrees;
                        Lat0 += DashDegrees * slope;             // Rise = slope*Lon + Lat
                        result.Add(new double[] { Lat0, Lon0 });
                    }
                    else
                    {
                        // end of line crosses endpoint, so end at endpoint
                        break;
                    }
                    // move across space
                    if ((Lon0 + SpaceDegrees) < EndLon)
                    {
                        // end of space is short of endpoint
                        // pen up and move pen
                        result.Add(new double[] { -1f, -1f });
                        Lon0 += SpaceDegrees;
                        Lat0 += SpaceDegrees * slope;
                        // Add the next start point, unless it's too far for another dash, then restart here
                        Lon0 += DashDegrees;
                        Lat0 += DashDegrees * slope;  
                        result.Add(new double[] { Lat0, Lon0 });
                    }
                    else break;
                    // increment distance
                }
                result.Add(new double[] { EndLat, EndLon });
            }
            // end at end point (last dash may be a bit longer)
            return result.ToArray();
        }

        private void DashedLineRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (DashedLineRadioButton.Checked)
                DashesLengthNMTextBox.Enabled = true; ;
        }

        private void CalcDistSMRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (CalcDistSMRadioButton.Checked) CalcType = 'S';
        }

        private void CalcDistFeetRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (CalcDistFeetRadioButton.Checked) CalcType = 'f';
        }

        private void CalcDistNMRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (CalcDistNMRadioButton.Checked) CalcType = 'N';
        }

        private void CalcDistMeterRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (CalcDistMeterRadioButton.Checked) CalcType = 'm';
        }

        private void MagBrgCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            CalcMagVarTextBox.Enabled = MagBrgCheckBox.Checked;
        }

        private void SSDRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (SSDRadioButton.Checked)
            {
                SuffixTextBox.Text = "Used by line generator";
                SuffixLabel.Text = "Fixes";
                SuffixTextBox.Enabled = false;
                toolTip1.SetToolTip(SuffixTextBox, "Disabled for SID/STAR lines");
                OutputType = "SSD";
            }
        }

        private void GEORadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (GEORadioButton.Checked)
            {
                OutputType = "GEO";
                ColorNameTextBox.Enabled = ColorValueTextBox.Enabled = true;
            }
        }

        private void AirwayRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (AirwayRadioButton.Checked)
            {
                PrefixLabel.Text = "Prefix Req'd";
                SuffixTextBox.Text = "Used by line generator";
                SuffixLabel.Text = "Fixes";
                SuffixTextBox.Enabled = false;
                toolTip1.SetToolTip(SuffixTextBox, "Disabled for Airway lines");
                OutputType = "AWY";
            }
        }

        private void ARTCCRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (ARTCCRadioButton.Checked)
            {
                PrefixLabel.Text = "Prefix Req'd";
                SuffixTextBox.Text = string.Empty;
                SuffixLabel.Text = "Comment";
                SuffixTextBox.Enabled = true;
                toolTip1.SetToolTip(SuffixTextBox, "Comment after line");
                OutputType = "ARTCC";
            }
            else
            {
                UseFIXNamesCheckBox.Checked = false;
                Refresh();
            }
            UseFIXNamesCheckBox.Enabled = !ARTCCRadioButton.Checked;
        }

        private void SuffixTextBox_TextChanged(object sender, EventArgs e)
        {
            if (GEORadioButton.Checked)
            {
                if (SuffixTextBox.TextLength == 0) SuffixTextBox.BackColor = Color.Yellow;
                else SuffixTextBox.BackColor = Color.White;
            }
            else SuffixTextBox.BackColor = Color.White;
        }

        private void SuffixTextBox_DoubleClick(object sender, EventArgs e)
        {
            if (GEORadioButton.Checked)
            {
                if (colorDialog1.ShowDialog() == DialogResult.OK)
                {
                    int iColor = ColorTranslator.ToWin32(colorDialog1.Color);
                    SuffixTextBox.Text = iColor.ToString() + "; " + colorDialog1.Color;
                }
            }
        }

        private void SolidLineRadioButton_Click(object sender, EventArgs e)
        {
            if (SolidLineRadioButton.Checked)
                DashesLengthNMTextBox.Enabled = false;
        }

        private void DashesLengthNMTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Extensions.CharIsDecimal(e.KeyChar, ref DashesLengthNMTextBox, 2);
        }

        private void ColorValueTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Extensions.CharIsDigit(e.KeyChar);
        }

        private void DashesLengthNMTextBox_Validated(object sender, EventArgs e)
        {
            // Dashlength in degrees
            DashLength = Convert.ToDouble(DashesLengthNMTextBox.Text);
        }

        private void AddNextButton_Click(object sender, EventArgs e)
        {
            AddLine();
            StartLatitudeTextBox.Text = EndLatitudeTextBox.Text;
            StartLat = EndLat;
            StartLongitudeTextBox.Text = EndLongitudeTextBox.Text;
            StartLon = EndLon;
            StartFixTextBox.Text = EndFixTextBox.Text;
            EndLatitudeTextBox.Text = EndLongitudeTextBox.Text = EndFixTextBox.Text = string.Empty;
            UpdateCopyButtons();
        }

        private void PasteToTextBox_Validated(object sender, EventArgs e)
        {
            double result = TestTextBox(PasteToTextBox);
            if (result != -199)
                PasteToEndButton.Enabled = PasteToStartButton.Enabled = (result != -199);
        }

        private void PasteToStartButton_Click(object sender, EventArgs e)
        {
            StartLatitudeTextBox.Text = Conversions.DecDeg2SCT(PasteLat, true);
            StartLongitudeTextBox.Text = Conversions.DecDeg2SCT(PasteLon, false);
            StartLat = PasteLat;
            StartLon = PasteLon;
            UpdateCopyButtons();
        }

        private void PasteToEndButton_Click(object sender, EventArgs e)
        {
            EndLatitudeTextBox.Text = Conversions.DecDeg2SCT(PasteLat, true);
            EndLongitudeTextBox.Text = Conversions.DecDeg2SCT(PasteLon, false);
            EndLat = PasteLat;
            EndLon = PasteLon;
            UpdateCopyButtons();
        }

    }
}
