using System;
using System.IO;
using System.Data;
using System.Windows.Forms;
using System.Drawing;

namespace SCTBuilder
{
    public partial class LineGenerator : Form
    {
        double StartLat;
        double StartLon;
        double EndLat;
        double EndLon;
        double Distance;
        double TrueBrg;     // From the Lat-Long coordinates
        char DashType;
        double CalcDist;
        char CalcType;
        double CalcBrg;
        double CalcMag;
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
            StartLat = Convert.ToDouble(FixListDataGridView.SelectedRows[0].Cells[1].Value);
            StartLongitudeTextBox.Text =
                Conversions.DecDeg2SCT(Convert.ToDouble(FixListDataGridView.SelectedRows[0].Cells[2].Value), true);
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
                Conversions.DecDeg2SCT(Convert.ToDouble(FixListDataGridView.SelectedRows[0].Cells[2].Value), true);
            EndLon = Convert.ToDouble(FixListDataGridView.SelectedRows[0].Cells[2].Value);
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
                double test = Convert.ToDouble(CalcBearingTextBox.Text);
                if ((test > 360) | (test < 0))
                {
                    Msg = "Value must be 0 to 360.";
                }
                else
                {
                    CalcBearingTextBox.Text = test.ToString();
                    CalcBrg = test;
                }
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
                {
                    CalcMagVarTextBox.Text = test.ToString();
                    CalcMag = test;
                }
                //}
                if (Msg.Length != 0)
                {
                    SendMessage(Msg, icon);
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
            DashedLineLengthNUD.Maximum = decimal.Round(Convert.ToDecimal(Distance), 0, MidpointRounding.AwayFromZero) / 2;
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
            MessageBoxIcon icon = MessageBoxIcon.Warning;
            if (PrefixTextBox.TextLength > 26)
            {
                string Msg = "Prefix may not be longer than 26 characters.";
                SendMessage(Msg, icon);
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
            ///     Magnetic bearing - deviation = Compass bearing
            /// In the US, the agonic line is roughly along the Mississippi river
            /// In aviation, maps, GPS and runways use true bearings
            double CalcTrueBrg = CalcBrg;
            if (MagBrgCheckBox.Checked)
                CalcTrueBrg += CalcMag;
            double[] CalcLocation =
                LatLongCalc.Destination(StartLat, StartLon, DistanceAdjust(CalcDist, CalcType), CalcTrueBrg);
            EndLat = CalcLocation[0];
            EndLon = CalcLocation[1];
            EndLatitudeTextBox.Text = Conversions.DecDeg2SCT(EndLat, true);
            EndLongitudeTextBox.Text = Conversions.DecDeg2SCT(EndLon, false);
            EndFixTextBox.Text = string.Empty;
            UpdateCopyButtons();
        }

        private double DistanceAdjust(double Distance, char Type)
        {
            // Convert from Statute Miles
            double result;
            switch (Type)
            {
                case 'N':
                    result = Distance * 0.868976;
                    break;
                case 'm':
                    result = Distance * 1852;
                    break;
                case 'f':
                    result = Distance * 6076.12;
                    break;
                case 'S':
                default:
                    result = Distance;
                    break;
            }
            return result;
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
                    else
                    {
                        StartLatitudeTextBox.Text = Conversions.DecDeg2SCT(testvalue, true);
                        StartLat = Conversions.String2DecDeg(StartLatitudeTextBox.Text);
                    }
                }

                if (Msg.Length != 0)
                {
                    SendMessage(Msg, icon);
                    StartLatitudeTextBox.Text = string.Empty;
                    StartLat = 0;
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
                    else
                    {
                        StartLongitudeTextBox.Text = Conversions.DecDeg2SCT(testvalue, false);
                        StartLon = Conversions.String2DecDeg(StartLongitudeTextBox.Text);
                    }
                }
                if (Msg.Length != 0)
                {
                    SendMessage(Msg, icon);
                    StartLongitudeTextBox.Text = string.Empty;
                    StartLon = 0;
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
                    else
                    {
                        EndLatitudeTextBox.Text = Conversions.DecDeg2SCT(testvalue, false);
                        EndLat = Conversions.String2DecDeg(EndLatitudeTextBox.Text);
                    }
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
                    EndLat = 0;
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
                    else
                    {
                        EndLongitudeTextBox.Text = Conversions.DecDeg2SCT(testvalue, false);
                        EndLon = Conversions.String2DecDeg(EndLongitudeTextBox.Text);
                    }
                }
                if (Msg.Length != 0)
                {
                    SendMessage(Msg, icon);
                    EndLongitudeTextBox.Text = string.Empty;
                    EndLon = 0;
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
            // There is no error checking for valid data, as that is done to enable buttons
            string cr = Environment.NewLine;
            MessageBoxIcon icon = MessageBoxIcon.Warning; string Msg;
            if (SSDRadioButton.Checked)
            {
                OutputTextBox.Text += SCTstrings.SSDout(StartLatitudeTextBox.Text, StartLongitudeTextBox.Text,
                    EndLatitudeTextBox.Text, EndLongitudeTextBox.Text,
                    StartFixTextBox.Text, EndFixTextBox.Text, UseFIXNamesCheckBox.Checked);
            }
            if (AirwayRadioButton.Checked)
            {
                if (PrefixTextBox.TextLength != 0)
                    // if solid, just write the line
                    if (!DashedLineRadioButton.Checked)
                    {
                        OutputTextBox.Text += SCTstrings.AWYout(PrefixTextBox.Text, StartLatitudeTextBox.Text,
                            StartLongitudeTextBox.Text, EndLatitudeTextBox.Text, EndLongitudeTextBox.Text,
                            StartFixTextBox.Text, EndFixTextBox.Text, UseFIXNamesCheckBox.Checked);
                    }
                    else          // Run a loop to generate the output
                    {
                        double FirstLat = StartLat;
                        double FirstLon = StartLon;
                        double NextDist = 0;
                        while (NextDist <= Distance)
                        {
                            double[] CalcLocation = LatLongCalc.Destination(FirstLat, FirstLon, DistanceAdjust(CalcDist, DashType), TrueBrg);
                            double NextLat = CalcLocation[0];
                            double NextLon = CalcLocation[1];
                            OutputTextBox.Text += SCTstrings.AWYout(PrefixTextBox.Text,
                                Conversions.DecDeg2SCT(FirstLat, true), Conversions.DecDeg2SCT(FirstLon, true),
                                Conversions.DecDeg2SCT(NextLat, true), Conversions.DecDeg2SCT(NextLon, true),
                                StartFixTextBox.Text, EndFixTextBox.Text, UseFIXNamesCheckBox.Checked);
                            OutputTextBox.Text += cr;
                            FirstLat = NextLat; FirstLon = NextLon;
                            NextDist = +CalcDist;
                        }
                    }
                else
                {
                    Msg = "The Airway identifier is required for this format." + cr + "(Place in the prefix text box.)";
                    SendMessage(Msg, icon);
                    PrefixTextBox.Focus();
                }

            }
            if (ARTCCRadioButton.Checked)
            {
                if (PrefixTextBox.TextLength != 0)
                    OutputTextBox.Text += SCTstrings.BoundaryOut(PrefixTextBox.Text, StartLatitudeTextBox.Text,
                        StartLongitudeTextBox.Text, EndLatitudeTextBox.Text, EndLongitudeTextBox.Text);
                if (SuffixTextBox.TextLength != 0) OutputTextBox.Text += SuffixTextBox.Text;
                else
                {
                    Msg = "The ARTCC identifier is required for this format." + cr + "(Place in the prefix text box.)";
                    SendMessage(Msg, icon);
                    PrefixTextBox.Focus();
                }
            }
            if (GEORadioButton.Checked)
            {
                OutputTextBox.Text += SCTstrings.GeoOut(StartLatitudeTextBox.Text, StartLongitudeTextBox.Text,
                         EndLatitudeTextBox.Text, EndLongitudeTextBox.Text, SuffixTextBox.Text);
            }
            OutputTextBox.Text += cr;
        }

        private void DashedLineRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            DashedLineLengthNUD.Enabled = DashFtRadioButton.Enabled =
                DashMeterRadioButton.Enabled = DashNMRadioButton.Enabled =
                DashSMRadioButton.Enabled = DashedLineRadioButton.Checked;
        }

        private void DashSMRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (DashSMRadioButton.Checked) DashType = 'S';
        }

        private void DashFtRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (DashFtRadioButton.Checked) DashType = 'f';
        }

        private void DashNMRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (DashNMRadioButton.Checked) DashType = 'N';
        }

        private void DashMeterRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (DashMeterRadioButton.Checked) DashType = 'm';
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

        private string GenerateLine()
        {
            string result = string.Empty; double tempDist = 0;
            while (tempDist < Distance)
            {
                //LatLongCalc.CalcLocation()
                // generate each point then call the appropriate string out
            }
            return result;
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
            }
        }

        private void GEORadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (GEORadioButton.Checked)
            {
                SuffixLabel.Text = "Color (req)";
                SuffixTextBox.Enabled = GEORadioButton.Checked;
                toolTip1.SetToolTip(SuffixTextBox, "Enter SCT color name or double click to pick");
                if (SuffixTextBox.TextLength == 0) SuffixTextBox.BackColor = Color.Yellow;
                else SuffixTextBox.BackColor = Color.White;
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
    }
}
