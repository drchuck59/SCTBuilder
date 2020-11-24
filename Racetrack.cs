using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace SCTBuilder
{
    public partial class Racetrack : Form
    {
        private static double PasteLat = -1;
        private static double PasteLon = -1;
        private static readonly string cr = Environment.NewLine;
        private static double HoldLat = -1;
        private static double HoldLon = -1;
        private static string HoldFix = string.Empty;
        private static double MagVar = 0;
        private static double HoldSpeed = 0;
        private static int Track = 0;
        private static double LegLnthDist = 0;
        private static double ArcRadius = 0;
        private static bool TurnRight = true;
        private static bool DrawTurnArrow = false;
        private static bool DrawFixLabel = false;
        private static bool DrawFixSymbol = false;

        public Racetrack()
        {
            InitializeComponent();
        }

        private void Racetrack_Load(object sender, EventArgs e)
        {
            MagVar = InfoSection.MagneticVariation;
            MagVarTextBox.Text = MagVar.ToString();
            LoadSectionComboBox();
            LoadAltTypeComboBox();
            CheckCalcToParams();
        }
        private void LoadSectionComboBox()
        {
            DataTable dtSections = new DataTable();
            dtSections.Columns.Add("ID", typeof(int));
            dtSections.Columns.Add("Section", typeof(string));
            dtSections.Rows.Add(0, "SID/STAR");
            dtSections.Rows.Add(1, "ARTCC");
            dtSections.Rows.Add(2, "Airways");
            dtSections.Rows.Add(3, "GEO");
            SectionComboBox.DataSource = dtSections;
            SectionComboBox.DisplayMember = "Section";
            SectionComboBox.ValueMember = "ID";
            SectionComboBox.SelectedIndex = 0;
        }

        private void IdentifierTextBox_TextChanged(object sender, EventArgs e)
        {
            // Search the tables for fixes and return the coordinates, 
            // then place the coordinates into the list box.
            // Looks like a lot of work, but the data is in memory
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
                    DataTable dtFixList = dvVOR.ToTable(true, "FacilityID", "Latitude", "Longitude", "MagVar");
                    dtFixList.Merge(dvNDB.ToTable(true, "FacilityID", "Latitude", "Longitude", "MagVar"));
                    dtFixList.Merge(dvFIX.ToTable(true, "FacilityID", "Latitude", "Longitude"));
                    FixListDataGridView.DataSource = dtFixList;
                    FixListDataGridView.DefaultCellStyle.Font = new Font("Arial", 9);
                    FixListDataGridView.Columns[0].HeaderText = "ID";
                    if (FixListDataGridView.Rows.Count != 0)
                    {
                        FixListDataGridView.AutoResizeColumn(0, DataGridViewAutoSizeColumnMode.AllCells);
                        FixListDataGridView.Columns[3].Visible = false;
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
            ImportFix2HoldButton.Enabled = (FixListDataGridView.SelectedRows.Count == 1);
        }

        private void LoadAltTypeComboBox()
        {
            DataTable dT = new DataTable();
            dT.Columns.Add("AltTypeValue", typeof(int));
            dT.Columns.Add("AltTypeName", typeof(string));
            dT.Rows.Add(0, "");
            dT.Rows.Add(1, "AOB 6000 ft");
            dT.Rows.Add(2, "AOB 14000 ft");
            dT.Rows.Add(3, "Above 14000 ft");
            dT.Rows.Add(4, "USAF airfield");
            dT.Rows.Add(5, "USN airfield");
            dT.Rows.Add(6, "Rotary below 1000 ft");
            HoldTypeComboBox.DataSource = dT;
            HoldTypeComboBox.DisplayMember = "AltTypeName";
            HoldTypeComboBox.ValueMember = "AltTypeValue";
            HoldTypeComboBox.SelectedIndex = 3;
            UpdateSelectedData();
        }

        private void UpdateSelectedData()
        {
            if (HoldTypeComboBox.SelectedIndex != 0)
            {
                HoldTypeComboBox.Text = HoldTypeComboBox.SelectedItem.ToString();
                int i = HoldTypeComboBox.SelectedIndex - 1;
                HoldSpeed = Convert.ToDouble(PatternData.MaxSpeed[i].ToString());
                HoldSpeedTextBox.Text = HoldSpeed.ToString();
                HoldLegTimeNUD.Value = Convert.ToDecimal(PatternData.LegTime[i]);
                LegLnthDist = Convert.ToDouble(PatternData.LegLength[i]);
                LegLengthTextBox.Text = LegLnthDist.ToString("F3");
            }
        }

        private void PasteToTextBox_Validated(object sender, EventArgs e)
        {
            CopyToHoldButton.Enabled = TestTextBox(PasteToTextBox);
        }

        private bool TestTextBox(TextBox tb, int method = 0)
        {
            bool ParsedResult = false;
            if (tb.Name.IndexOf("Lat") != -1) method = 1;
            if (tb.Name.IndexOf("Lon") != -1) method = 2;
            if (method == 0)
            {
                // Determine the format if not forced (aka, method 0)
                if ((tb.Text.ToUpperInvariant().IndexOf("N") > -1) || (tb.Text.ToUpperInvariant().IndexOf("S") > -1)) method = 1;
                if ((tb.Text.ToUpperInvariant().IndexOf("W") > -1) || (tb.Text.ToUpperInvariant().IndexOf("E") > -1)) method += 2;
            }
            if ((tb.Modified) && tb.TextLength != 0)
            {
                if (LatLonParser.TryParseAny(tb))
                {
                    switch (method)
                    {
                        case 0:
                        case 3:
                            PasteLat = LatLonParser.ParsedLatitude;
                            PasteLon = LatLonParser.ParsedLongitude;
                            ParsedResult = true;
                            tb.Text = Conversions.DecDeg2SCT(PasteLat, true) + " " +
                                Conversions.DecDeg2SCT(PasteLon, false);
                            break;
                        case 1:
                            PasteLat = LatLonParser.ParsedLatitude;
                            PasteLon = -1;
                            tb.Text = Conversions.DecDeg2SCT(PasteLat, true);
                            ParsedResult = true;
                            break;
                        case 2:
                            PasteLon = LatLonParser.ParsedLongitude;
                            PasteLat = -1;
                            tb.Text = Conversions.DecDeg2SCT(PasteLon, false);
                            ParsedResult = true;
                            break;
                    }
                    tb.BackColor = Color.White;
                }
                else tb.BackColor = Color.Yellow;
            }
            return ParsedResult;
        }

        private void RightTurnRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            TurnRight = RightTurnRadioButton.Checked;
        }

        private void LeftTurnRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            TurnRight = !RightTurnRadioButton.Checked;
        }

        private void AddArc_Click(object sender, EventArgs e)
        {
            OutputTextBox.Text = BuildRaceTrack();
        }

        private void HoldLatitudeTextBox_Validated(object sender, EventArgs e)
        {
            if (TestTextBox(HoldLatitudeTextBox))
            {
                HoldLat = PasteLat;
            }
        }

        private void HoldLongitudeTextBox_Validated(object sender, EventArgs e)
        {
            if (TestTextBox(HoldLongitudeTextBox))
            {
                HoldLon = PasteLon;
            }
        }

        private void ImportFix2HoldButton_Click(object sender, EventArgs e)
        {
            ImportFixToHold();
        }

        private void ImportFixToHold()
        {
            // East declination is positive; west is negative
            // True (map) Heading = Mag Bearing + Declination
            // Mag Hdg = True Brg - Declination
            // VOR headings are always magnetic (Hdg)
            if (FixListDataGridView.SelectedRows.Count > 0)
            {
                string FixText = string.Empty;
                double[] Coords;
                double Lat = Convert.ToDouble(FixListDataGridView.SelectedRows[0].Cells[1].Value);
                double Lon = Convert.ToDouble(FixListDataGridView.SelectedRows[0].Cells[2].Value);
                // VORs are in bearings and need no adjustment
                //double MagVar = Convert.ToDouble(MagVarTextBox.Text);
                if (FixOffsetCheckBox.Checked)
                {
                    double Dist = Convert.ToDouble(FixOffsetDistTextBox.Text);
                    double Brg = Convert.ToDouble(FixOffsetBrgNUD.Value);
                    //if (FixListDataGridView.SelectedRows[0].Cells[3].Value != null)
                    //MagVar = Convert.ToDouble(FixListDataGridView.SelectedRows[0].Cells[3].Value);
                    Coords = LatLongCalc.Destination(Lat, Lon, Dist, Brg, 'N');
                    Lat = Coords[0]; Lon = Coords[1];
                    FixText = String.Format("{0:000}", FixOffsetBrgNUD.Value) +
                        String.Format("{0:000.0}", Convert.ToDouble(FixOffsetDistTextBox.Text));
                }
                HoldLatitudeTextBox.Text = Conversions.DecDeg2SCT(Lat, true);
                HoldLat = Lat;
                HoldLongitudeTextBox.Text = Conversions.DecDeg2SCT(Lon, false);
                HoldLon = Lon;
                //MagVarTextBox.Text = MagVar.ToString();
                HoldFixTextBox.Text = FixListDataGridView.SelectedRows[0].Cells[0].Value.ToString() + FixText;
                AddFixLabelCheckBox.Enabled = AddFixSymbolCheckBox.Enabled = (HoldFixTextBox.TextLength != 0);
            }
        }

        private void CopyToHoldButton_Click(object sender, EventArgs e)
        {
            HoldLatitudeTextBox.Text = Conversions.DecDeg2SCT(PasteLat, true);
            HoldLat = PasteLat;
            HoldLongitudeTextBox.Text = Conversions.DecDeg2SCT(PasteLon, false);
            HoldLon = PasteLon;
            HoldFixTextBox.Text = string.Empty;
            AddFixLabelCheckBox.Enabled = AddFixSymbolCheckBox.Enabled = false;
        }

        private void FixListDataGridView_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (FixListDataGridView.SelectedRows.Count == 1)
            {
                IdentifierTextBox.Text = FixListDataGridView.SelectedRows[0].Cells[0].Value.ToString();
                ImportFixToHold();
            }
        }

        private void FixOffsetCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            FixOffsetBrgNUD.Enabled = FixOffsetDistTextBox.Enabled = FixOffsetCheckBox.Checked;
            if (!FixOffsetCheckBox.Checked)
            {
                FixOffsetBrgNUD.Value = 360; FixOffsetDistTextBox.Text = "0";
            }
        }

        private void FixOffsetDistTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Extensions.CharIsDecimal(e.KeyChar, ref FixOffsetDistTextBox, 1);
        }

        private void FixOffsetDistTextBox_Validating(object sender, CancelEventArgs e)
        {
            if (Convert.ToDouble(FixOffsetDistTextBox.Text) < 0) FixOffsetDistTextBox.Text = "0";
            if (Convert.ToDouble(FixOffsetDistTextBox.Text) > 999.9) FixOffsetDistTextBox.Text = "999.9";
        }

        private void HoldTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateSelectedData();
            CheckCalcToParams();
        }

        private void HoldLegTimeNUD_ValueChanged(object sender, EventArgs e)
        {
            HoldTypeComboBox.SelectedIndex = 0;
            CheckCalcToParams();
        }

        private void CheckCalcToParams()
        {
            if (HoldSpeedTextBox.TextLength != 0)
            {
                LegLnthDist = Convert.ToDouble(HoldSpeedTextBox.Text) * Convert.ToDouble(HoldLegTimeNUD.Value) / 60;
                LegLengthTextBox.Text = LegLnthDist.ToString("F3");
                ArcRadius = PatternData.ArcRadius(HoldSpeed);
                TurnRadiusTextBox.Text = ArcRadius.ToString("F3");
            }
        }

        private void HoldSpeedTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Extensions.CharIsDecimal(e.KeyChar, ref FixOffsetDistTextBox);
        }

        private void HoldAltTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Extensions.CharIsDecimal(e.KeyChar, ref FixOffsetDistTextBox);
        }

        private void HoldSpeedTextBox_Validated(object sender, EventArgs e)
        {
            HoldTypeComboBox.SelectedIndex = 0;
            CheckCalcToParams();
        }

        private void HoldFixTextBox_Validated(object sender, EventArgs e)
        {
            AddFixLabelCheckBox.Enabled = AddFixSymbolCheckBox.Enabled = (HoldFixTextBox.TextLength != 0);
        }

        private void AddTurnArrowCheckBox_Click(object sender, EventArgs e)
        {
            DrawTurnArrow = AddTurnArrowCheckBox.Checked;
        }

        private void AddFixSymbolCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            DrawFixSymbol = AddFixSymbolCheckBox.Checked;
        }

        private void AddFixLabelCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            DrawFixLabel = AddFixLabelCheckBox.Checked;
        }

        private void MagVarTextBox_Validated(object sender, EventArgs e)
        {
            MagVar = Convert.ToDouble(MagVarTextBox.Text);
        }

        private void MagVarTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
           e.Handled = Extensions.CharIsDecimal(e.KeyChar, ref FixOffsetDistTextBox, 1, true);
        }

        private void MagVarTextBox_Validating(object sender, CancelEventArgs e)
        {
            if (Convert.ToDouble(MagVarTextBox.Text) < -180) MagVarTextBox.Text = "-180";
            if (Convert.ToDouble(MagVarTextBox.Text) > 180) MagVarTextBox.Text = "180";
        }

        private string BuildRaceTrack()
        {
            string output = string.Empty;
            // Step 1  - build leg away from hold fix
            double StartLat = HoldLat;
            double StartLon = HoldLon;
            double StartBrg; double EndBrg;
            double EndLat; double EndLon; double[] Coords;
            string Lat0; string Lon0; string Lat1; string Lon1;
            double OutTrack = Track;
            if (InboundTrackRadioButton.Checked) OutTrack = Track + 540 % 360;  // Reverse track to outbnd
            Coords = LatLongCalc.Destination(StartLat, StartLon, LegLnthDist, OutTrack, 'N');
            EndLat = Coords[0]; EndLon = Coords[1];
            Lat0 = Conversions.DecDeg2SCT(HoldLat, true);
            Lon0 = Conversions.DecDeg2SCT(HoldLon, false);
            Lat1 = Conversions.DecDeg2SCT(EndLat, true);
            Lon1 = Conversions.DecDeg2SCT(EndLon, false);
            output += OutputText(Lat0, Lon0, Lat1, Lon1);
            // Step 2 - Enter first turn
            StartLat = EndLat; StartLon = EndLon;   // This is start of turn
            if (TurnRight)
            {
                StartBrg = (OutTrack + 270) % 360;
                EndBrg = (OutTrack + 90) % 360;
            }
            else
            {
                EndBrg = (OutTrack + 270) % 360;
                StartBrg = (OutTrack + 90) % 360;
            }
            Coords = LatLongCalc.Destination(StartLat, StartLon, ArcRadius, EndBrg, 'N');
            double CenterLat = Coords[0]; double CenterLon = Coords[1];                     // Center of arc
            output += BuildArcString(StartBrg, EndBrg, CenterLat, CenterLon);               // Build Arc 1
            // Step 3 - Return leg
             // Need to close the end of arc with next line
            Coords = LatLongCalc.Destination(StartLat, StartLon, ArcRadius*2, EndBrg, 'N');
            StartLat = Coords[0]; StartLon = Coords[1];
            Lat0 = Conversions.DecDeg2SCT(PasteLat, true);
            Lon0 = Conversions.DecDeg2SCT(PasteLon, false);
            Lat1 = Conversions.DecDeg2SCT(StartLat, true);
            Lon1 = Conversions.DecDeg2SCT(StartLon, false);
            output += OutputText(Lat0, Lon0, Lat1, Lon1);
             // Now draw the leg line
            double InTrack = (OutTrack + 180) % 360;
            Coords = LatLongCalc.Destination(StartLat, StartLon, LegLnthDist, InTrack, 'N');
            EndLat = Coords[0]; EndLon = Coords[1];
            Lat0 = Conversions.DecDeg2SCT(StartLat, true);
            Lon0 = Conversions.DecDeg2SCT(StartLon, false);
            Lat1 = Conversions.DecDeg2SCT(EndLat, true);
            Lon1 = Conversions.DecDeg2SCT(EndLon, false);
            output += OutputText(Lat0, Lon0, Lat1, Lon1);
            // Step 4 - Return arc
            if (TurnRight)
            {
                EndBrg = (OutTrack + 270) % 360;
                StartBrg = (OutTrack + 90) % 360;
            }
            else
            {
                StartBrg = (OutTrack + 270) % 360;
                EndBrg = (OutTrack + 90) % 360;
            }
            StartLat = EndLat; StartLon = EndLon;
            Coords = LatLongCalc.Destination(StartLat, StartLon, ArcRadius, EndBrg, 'N');
            CenterLat = Coords[0]; CenterLon = Coords[1];
            output += BuildArcString(StartBrg, EndBrg, CenterLat, CenterLon);
            if (DrawFixSymbol) output += DrawFix_Symbol();
            if (DrawFixLabel) output += DrawFix_Label();
            return output;
        }

        private string DrawFix_Label()
        {
            // This will need to be offset based upon the hold pattern
            string result = string.Empty;
            string curFix = HoldFixTextBox.Text;
            double Lat1 = HoldLat;
            double Lon1 = HoldLon;
            result += Hershey.WriteHF(curFix, Lat1, Lon1, -1*(int)MagVar);
            return result;
        }

        private string DrawFix_Symbol()
        {
            string result = string.Empty;
            string curFix = HoldFixTextBox.Text;
            object[] NavData;
            if (DrawFixSymbol)
            {
                NavData = SCTcommon.GetNavData(curFix);
                result += Hershey.DrawSymbol(NavData);
            }
            return result;
        }

        private string BuildArcString(double StartBrg, double EndBrg, double StartLat, double StartLon)
        {
            string Lat0 = string.Empty; string Lon0 = string.Empty; string Lat1; string Lon1;
            string output = string.Empty;
            double[] Coords;
            // Make sure we aren't crossing 360...
            if ((StartBrg + Math.Abs(EndBrg - StartBrg)) > 360)
            {
                for (double i = StartBrg; i < 360; i++)
                {
                    Coords = LatLongCalc.Destination(StartLat, StartLon, ArcRadius, i, 'N');
                    Lat1 = Conversions.DecDeg2SCT(Coords[0], true);
                    Lon1 = Conversions.DecDeg2SCT(Coords[1], false);
                    output += OutputText(Lat0, Lon0, Lat1, Lon1);
                    Lat0 = Lat1; Lon0 = Lon1;
                }
                for (double i = 1; i < EndBrg; i++)
                {
                    Coords = LatLongCalc.Destination(StartLat, StartLon, ArcRadius, i, 'N');
                    Lat1 = Conversions.DecDeg2SCT(Coords[0], true);
                    Lon1 = Conversions.DecDeg2SCT(Coords[1], false);
                    output += OutputText(Lat0, Lon0, Lat1, Lon1);
                    Lat0 = Lat1; Lon0 = Lon1;
                }
            }
            else
            {
                for (double i = StartBrg; i < EndBrg; i++)
                {
                    Coords = LatLongCalc.Destination(StartLat, StartLon, ArcRadius, i, 'N');
                    Lat1 = Conversions.DecDeg2SCT(Coords[0], true);
                    Lon1 = Conversions.DecDeg2SCT(Coords[1], false);
                    output += OutputText(Lat0, Lon0, Lat1, Lon1);
                    Lat0 = Lat1; Lon0 = Lon1;
                }
            }
            // Draw last segment
            Coords = LatLongCalc.Destination(StartLat, StartLon, ArcRadius, EndBrg, 'N');
            PasteLat = Coords[0]; PasteLon = Coords[1];
            Lat1 = Conversions.DecDeg2SCT(Coords[0], true);
            Lon1 = Conversions.DecDeg2SCT(Coords[1], false);
            output += OutputText(Lat0, Lon0, Lat1, Lon1);
            return output;
        }

        private string OutputText(string Lat0, string Lon0, string Lat1, string Lon1)
        {
            string result = string.Empty;
            if ((Lat0.Length > 0) && (Lon0.Length > 0) && (Lat1.Length > 0) && (Lon1.Length > 0))
            {
                switch (SectionComboBox.SelectedIndex)
                {
                    case 0:         // SIDSTAR
                        result = SCTstrings.SSDout(Lat0, Lon0, Lat1, Lon1) + cr;
                        break;
                    case 1:         // ARTCC
                        result = SCTstrings.BoundaryOut(PrefixTextBox.Text, Lat0, Lon0, Lat1, Lon1) + cr;
                        break;
                    case 2:         // Airway (prefix textbox req'd)
                        result = SCTstrings.AWYout(PrefixTextBox.Text, Lat0, Lon0, Lat1, Lon1, "", "") + cr;
                        break;
                    case 3:         // GEO format
                        result = SCTstrings.GeoOut(Lat0, Lon0, Lat1, Lon1, ColorValueTextBox.Text) + cr;
                        break;
                }
            }
            return result;
        }

        private void TrackNUD_ValueChanged(object sender, EventArgs e)
        {
            Track = (int)TrackNUD.Value;
        }

        private void ClearOutputButton_Click(object sender, EventArgs e)
        {
            OutputTextBox.Text = string.Empty;
            OutputTextBox.Refresh();
        }

        private void Copy2ClipboardButton_Click(object sender, EventArgs e)
        {
            if (OutputTextBox.TextLength > 0)
            {
                OutputTextBox.SelectAll();
                Clipboard.SetText(OutputTextBox.SelectedText);
            }
            else
                SCTcommon.SendMessage("Nothing in output box to copy!");
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

        private void LegLengthTextBox_Validated(object sender, EventArgs e)
        {
            if (LegLengthTextBox.TextLength != 0)
                LegLnthDist = Convert.ToDouble(LegLengthTextBox.Text);
        }

        private void LegLengthTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Extensions.CharIsDecimal(e.KeyChar, ref LegLengthTextBox, 1);
        }

        private void TurnRadiusTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Extensions.CharIsDecimal(e.KeyChar, ref TurnRadiusTextBox, 3);
        }
    }

    static class PatternData
    {
        private const double dist6000 = 3.333;      // Leg length (NM) at or below 6000 feet
        private const double speed6000 = 200.0;
        private const double time6000 = 1.0;    
        private const double dist14000 = 3.8333;    // Leg length (NM) 6001-14000 feet
        private const double speed14000 = 230.0;
        private const double time14000 = 1.0;
        private const double dist14001 = 6.625;     // Leg length (NM) over 14000
        private const double speed14001 = 265.0;
        private const double time14001 = 1.5;
        private const double distUSAF = 5.1666;     // Leg length (NM) for USAF
        private const double speedUSAF = 310.0;
        private const double timeUSAF = 1.0;
        private const double distNavy = 3.8333;     // Leg length (NM) for USN
        private const double speedNavy = 230.0;
        private const double timeNavy = 1.0;
        private const double distCopter = 1.5;      // Helo hold length (rarely used)
        private const double speedCopter = 90.0;
        private const double timeCopter = 1.0;
        public static string[] PatternAlt = new string[]
            {"6000", "14000", "14001", "USAF", "Navy", "Copter"};
        public static double[] LegLength = new double[]
            {dist6000, dist14000, dist14001, distUSAF, distNavy, distCopter };
        public static double[] MaxSpeed = new double[]
            {speed6000, speed14000, speed14001, speedUSAF, speedNavy, speedCopter};
        public static double[] LegTime = new double[]
            {time6000, time14000, time14001, timeUSAF, timeNavy, timeCopter };

        public static double ArcRadius(double KIAS, double degrees = 180)
        {
            /// <summary>
            /// RETURNS the radius of the holding pattern arc(s), reduced to nearest whole value
            /// Knowing the altitude class for the holding pattern and amount of turn (usually 180 deg)
            /// one can calculate the maximum radius for the arc:
            /// Circumference = 2 * PI * Radius
            /// Distance (Circ) = Rate * Time
            /// Radius = Rate * Time / (2 * PI)
            /// Rate == [X]NM/HR
            /// Time == 3 sec/degree * #Degrees[Y]
            /// 2 Pi Radius = (X) NM/Hr * (Y)Degrees * 3sec/deg * Hr/3600
            ///             = X * Y /1200
            /// Radius = X * Y / (1200 * 2 * Pi)
            /// </summary>
            return (KIAS * degrees) / (Math.PI * 2400.0);
        }

        private static int AltitudeClass(string altitude)
        {
            int j = 0;
            if (Extensions.IsNumeric(altitude))
            {
                for (int i = 0; i < 3; i++)
                {
                    if (Convert.ToInt32(altitude) <= Convert.ToInt32(PatternData.PatternAlt[i])) return i;
                }
                return 2;
            }
            else
                foreach (string name in PatternData.PatternAlt)
                {
                    j++;
                    if (name == altitude) return j;
                }
            return j;
        }
    }
}
