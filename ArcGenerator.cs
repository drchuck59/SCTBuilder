using Google.Protobuf;
using System.Diagnostics;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace SCTBuilder
{
    public partial class ArcGenerator : Form
    {
        private static double CenterLat = -1;
        private static double CenterLon = -1;
        private static double StartLat = -1;
        private static double StartLon = -1;
        private static double EndLat = -1;
        private static double EndLon = -1;
        private static double StartBrg = -1;
        private static double EndBrg = -1;
        private static double ArcRadius = -1;
        private static double PasteLat = -1;
        private static double PasteLon = -1;

        public ArcGenerator()
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
                        Fix2CenterButton.Enabled = true;
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
                Fix2CenterButton.Enabled = false;
            }
        }

        private void Copy2ClipboardButton_Click(object sender, EventArgs e)
        {
            MessageBoxIcon icon = MessageBoxIcon.Information;
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

        private void FixToSTartButton_Click(object sender, EventArgs e)
        {
            if (FixListDataGridView.SelectedRows.Count > 0)
            {
                string FixText = string.Empty;
                double[] Coords;
                double Lat = Convert.ToDouble(FixListDataGridView.SelectedRows[0].Cells[1].Value);
                double Lon = Convert.ToDouble(FixListDataGridView.SelectedRows[0].Cells[2].Value);
                // VORs are in bearings and need no adjustment
                //double MagVar = Convert.ToDouble(MagVarTextBox.Text);
                if ((FixDistTextBox.TextLength > 0) && (Convert.ToDouble(FixDistTextBox.Text)) != 0f)
                {
                    double Dist = Convert.ToDouble(FixDistTextBox.Text);
                    double Brg = Convert.ToDouble(FixBrgNUD.Value);
                    //if (FixListDataGridView.SelectedRows[0].Cells[3].Value != null)
                    //MagVar = Convert.ToDouble(FixListDataGridView.SelectedRows[0].Cells[3].Value);
                    Coords = LatLongCalc.Destination(Lat, Lon, Dist, Brg, 'N');
                    Lat = Coords[0]; Lon = Coords[1];
                    FixText = String.Format("{0:000}", FixBrgNUD.Value) +
                        String.Format("{0:000.0}", Convert.ToDouble(FixDistTextBox.Text));
                }
                StartLatitudeTextBox.Text = Conversions.DecDeg2SCT(Lat, true);
                StartLat = Lat;
                StartLongitudeTextBox.Text = Conversions.DecDeg2SCT(Lon, false);
                StartLon = Lon;
                //MagVarTextBox.Text = MagVar.ToString();
                StartFixTextBox.Text = FixListDataGridView.SelectedRows[0].Cells[0].Value.ToString() + FixText;
                UpdateStats();
            }
        }

        private void FixToEndButton_Click(object sender, EventArgs e)
        {
            if (FixListDataGridView.SelectedRows.Count > 0)
            {
                string FixText = string.Empty;
                double[] Coords;
                double Lat = Convert.ToDouble(FixListDataGridView.SelectedRows[0].Cells[1].Value);
                double Lon = Convert.ToDouble(FixListDataGridView.SelectedRows[0].Cells[2].Value);
                // VORs are in bearings and need no adjustment
                //double MagVar = Convert.ToDouble(MagVarTextBox.Text);
                if ((FixDistTextBox.TextLength > 0) && (Convert.ToDouble(FixDistTextBox.Text)) != 0f)
                {
                    double Dist = Convert.ToDouble(FixDistTextBox.Text);
                    double Brg = Convert.ToDouble(FixBrgNUD.Value);
                    //if (FixListDataGridView.SelectedRows[0].Cells[3].Value != null)
                    //MagVar = Convert.ToDouble(FixListDataGridView.SelectedRows[0].Cells[3].Value);
                    Coords = LatLongCalc.Destination(Lat, Lon, Dist, Brg, 'N');
                    Lat = Coords[0]; Lon = Coords[1];
                    FixText = String.Format("{0:000}", FixBrgNUD.Value) +
                        String.Format("{0:000.0}", Convert.ToDouble(FixDistTextBox.Text));
                }
                EndLatitudeTextBox.Text = Conversions.DecDeg2SCT(Lat, true);
                EndLat = Lat;
                EndLongitudeTextBox.Text = Conversions.DecDeg2SCT(Lon, false);
                EndLon = Lon;
                //MagVarTextBox.Text = MagVar.ToString();
                EndFixTextBox.Text = FixListDataGridView.SelectedRows[0].Cells[0].Value.ToString() + FixText;
                UpdateStats();
            }
        }

        private void Fix2CenterButton_Click(object sender, EventArgs e)
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
                if ((FixDistTextBox.TextLength > 0) && (Convert.ToDouble(FixDistTextBox.Text)) != 0f)
                {
                    double Dist = Convert.ToDouble(FixDistTextBox.Text);
                    double Brg = Convert.ToDouble(FixBrgNUD.Value);
                    //if (FixListDataGridView.SelectedRows[0].Cells[3].Value != null)
                        //MagVar = Convert.ToDouble(FixListDataGridView.SelectedRows[0].Cells[3].Value);
                    Coords = LatLongCalc.Destination(Lat, Lon, Dist, Brg, 'N');
                    Lat = Coords[0]; Lon = Coords[1];
                    FixText = String.Format("{0:000}", FixBrgNUD.Value) +
                        String.Format("{0:000.0}", Convert.ToDouble(FixDistTextBox.Text));
                }
                CenterLatitudeTextBox.Text = Conversions.DecDeg2SCT(Lat, true);
                CenterLat = Lat;
                CenterLongitudeTextBox.Text = Conversions.DecDeg2SCT(Lon, false);
                CenterLon = Lon;
                //MagVarTextBox.Text = MagVar.ToString();
                CenterFixTextBox.Text = FixListDataGridView.SelectedRows[0].Cells[0].Value.ToString() + FixText;
                UpdateStats();
            }
        }

        private void ArcGenerator_Load(object sender, EventArgs e)
        {
            MagVarTextBox.Text = InfoSection.MagneticVariation.ToString();
        }

        private void CalcDistanceTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Extensions.CharIsDecimal(e.KeyChar, ref CalcDistanceTextBox, 1);
        }

        private void CalcDistanceTextBox_TextChanged(object sender, EventArgs e)
        {
            double tempRadius = Convert.ToDouble(CalcDistanceTextBox.Text);
            if ((tempRadius < 0.0) || (tempRadius > 999.9))
            {
                SCTcommon.SendMessage("Radius must be in range 0 to 999.9 NM");
                CalcDistanceTextBox.Text = "0";
            }
            else
                ArcRadius = tempRadius;
        }

        private void FixListDataGridView_Click(object sender, EventArgs e)
        {
            Fix2CenterButton.Enabled = FixToSTartButton.Enabled =
                FixToEndButton.Enabled = (FixListDataGridView.CurrentRow.Index != -1);

        }

        private void CheckArcButton()
        {
            AddArc.Enabled = (StartBrg != -1) && (EndBrg != -1) && 
                (CenterLat != -1) && (CenterLon != -1) && (ArcRadius != -1);
        }


        private void AddArcByRadialsButton_Click(object sender, EventArgs e)
        {

        }

        private void SectionComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ColorComboBox.Enabled = (SectionComboBox.Text == "GEO");
        }

        private void FixDistTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Extensions.CharIsDecimal(e.KeyChar, ref FixDistTextBox, 1);
        }

        private void FixDistTextBox_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (Convert.ToDouble(FixDistTextBox.Text) < 0) FixDistTextBox.Text = "0";
            if (Convert.ToDouble(FixDistTextBox.Text) > 999.9) FixDistTextBox.Text = "999.9";
        }

        private void CircleCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (CircleCheckBox.Checked)
            {
                EndRadialNUD.Value = StartRadialNUD.Value = 360;
                MagBrgCheckBox.Enabled = EndRadialNUD.Enabled = StartRadialNUD.Enabled = false;
            }
            else
                MagBrgCheckBox.Enabled = EndRadialNUD.Enabled = StartRadialNUD.Enabled = true;

        }

        private void AddArc_Click(object sender, EventArgs e)
        {

        }

        private void InsertCoordsButton_Click(object sender, EventArgs e)
        {
            if ((CenterLat == -1) || (CenterLon == -1))
            {
                SCTcommon.SendMessage("First select the center point of the arc.");
                return;
            }
            if ((CalcDistanceTextBox.TextLength == 0) || (Convert.ToDouble(CalcDistanceTextBox.Text) < 1))
            {
                SCTcommon.SendMessage("Radius of arc must be at least 1 NM");
                return;
            }
            else
            {
                double[] Coords;
                double CenterLat = Conversions.String2DecDeg(CenterLatitudeTextBox.Text);
                double CenterLon = Conversions.String2DecDeg(CenterLongitudeTextBox.Text);
                double Dist = Convert.ToDouble(CalcDistanceTextBox.Text);
                double BrgStart = Convert.ToDouble(StartRadialNUD.Value);
                double BrgEnd = Convert.ToDouble(EndRadialNUD.Value);
                double MagVar = Convert.ToDouble(MagVarTextBox.Text);
                double Lat; double Lon;
                if (CircleCheckBox.Checked)
                {
                    Coords = LatLongCalc.Destination(CenterLat, CenterLon, Dist, 90, 'N');
                    Lat = Coords[0];
                    Lon = Coords[1];
                    StartLatitudeTextBox.Text = Conversions.DecDeg2SCT(Lat, true);
                    StartLongitudeTextBox.Text = Conversions.DecDeg2SCT(Lon, false);
                    StartLat = Lat;
                    StartLon = Lon;
                    EndLatitudeTextBox.Text = Conversions.DecDeg2SCT(Lat, true);
                    EndLongitudeTextBox.Text = Conversions.DecDeg2SCT(Lon, false);
                    EndLat = Lat;
                    EndLon = Lon;
                }
                else
                {
                    if (MagBrgCheckBox.Checked == true)
                    {
                        BrgStart -= MagVar; BrgEnd -= MagVar;
                    }
                    Coords = LatLongCalc.Destination(CenterLat, CenterLon, Dist, BrgStart, 'N');
                    Lat = Coords[0];
                    Lon = Coords[1];
                    StartLatitudeTextBox.Text = Conversions.DecDeg2SCT(Lat, true);
                    StartLongitudeTextBox.Text = Conversions.DecDeg2SCT(Lon, false);
                    StartLat = Lat;
                    StartLon = Lon;
                    Coords = LatLongCalc.Destination(CenterLat, CenterLon, Dist, BrgEnd, 'N');
                    Lat = Coords[0];
                    Lon = Coords[1];
                    EndLatitudeTextBox.Text = Conversions.DecDeg2SCT(Lat, true);
                    EndLongitudeTextBox.Text = Conversions.DecDeg2SCT(Lon, false);
                    EndLat = Lat;
                    EndLon = Lon;
                }
                UpdateStats();
            }
                    
        }

        private void TradeStartEndButton_Click(object sender, EventArgs e)
        {
            double tempLat = StartLat;
            double tempLon = StartLon;
            StartLat = EndLat;
            StartLon = EndLon;
            EndLat = tempLat;
            EndLon = tempLon;
            string tempFix = StartFixTextBox.Text;
            StartFixTextBox.Text = EndFixTextBox.Text;
            EndFixTextBox.Text = tempFix;
            if ((StartLat == -1) || (StartLon == -1))
            {
                StartLatitudeTextBox.Text = StartLongitudeTextBox.Text = StartFixTextBox.Text = string.Empty;
            }
            else
            {
                StartLatitudeTextBox.Text = Conversions.DecDeg2SCT(StartLat, true);
                StartLongitudeTextBox.Text = Conversions.DecDeg2SCT(StartLon, false);
            }
            if ((EndLat == -1) || (EndLon == -1))
            {
                EndLatitudeTextBox.Text = EndLongitudeTextBox.Text = EndFixTextBox.Text = string.Empty;
            }
            else
            {
                EndLatitudeTextBox.Text = Conversions.DecDeg2SCT(EndLat, true);
                EndLongitudeTextBox.Text = Conversions.DecDeg2SCT(EndLon, false);
            }
            UpdateStats();
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
                            ParsedResult = LatLonParser.ParsedLatitude;
                            tb.Text = Conversions.DecDeg2SCT(ParsedResult, true);
                            break;
                        case 2:
                            ParsedResult = LatLonParser.ParsedLongitude;
                            tb.Text = Conversions.DecDeg2SCT(ParsedResult, false);
                            break;
                    }
                    tb.BackColor = Color.White;
                }
                else tb.BackColor = Color.Yellow;
            }
            return ParsedResult;
        }

        private void CenterLatitudeTextBox_Validated(object sender, EventArgs e)
        {
            double result = TestTextBox(CenterLatitudeTextBox);
            if (result != -199)
            {
                CenterLat = result;
                UpdateStats();
            }
        }

        private void CenterLongitudeTextBox_Validated(object sender, EventArgs e)
        {
            double result = TestTextBox(CenterLongitudeTextBox);
            if (result != -199)
            {
                CenterLon = result;
                UpdateStats();
            }
        }

        private void StartLatitudeTextBox_Validated(object sender, EventArgs e)
        {
            double result = TestTextBox(StartLatitudeTextBox);
            if (result != -199)
            {
                StartLat = result;
                UpdateStats();
            }
        }

        private void StartLongitudeTextBox_Validated(object sender, EventArgs e)
        {
            double result = TestTextBox(StartLongitudeTextBox);
            if (result != -199)
            {
                StartLon = result;
                UpdateStats();
            }
            }

        private void EndLatitudeTextBox_Validated(object sender, EventArgs e)
        {
            double result = TestTextBox(EndLatitudeTextBox);
            if (result != -199)
            {
                EndLat = result;
                UpdateStats();
            }
            }

        private void EndLongitudeTextBox_Validated(object sender, EventArgs e)
        {
            double result = TestTextBox(EndLongitudeTextBox);
            if (result != -199)
            {
                EndLon = result;
                UpdateStats();
            }
            }

        private void PasteToTextBox_Validated(object sender, EventArgs e)
        {
            double result = TestTextBox(PasteToTextBox);
            if (result != -199)
            PasteToCenterButton.Enabled = PasteToEndButton.Enabled = PasteToStartButton.Enabled = (result != -199);
        }

        private void PasteToCenterButton_Click(object sender, EventArgs e)
        {
            CenterLatitudeTextBox.Text = Conversions.DecDeg2SCT(PasteLat, true);
            CenterLat = PasteLat;
            CenterLongitudeTextBox.Text = Conversions.DecDeg2SCT(PasteLon, false);
            CenterLon = PasteLon;
            UpdateStats();
        }

        private void PasteToStartButton_Click(object sender, EventArgs e)
        {
            StartLatitudeTextBox.Text = Conversions.DecDeg2SCT(PasteLat, true);
            StartLongitudeTextBox.Text = Conversions.DecDeg2SCT(PasteLon, false);
            StartLat = PasteLat;
            StartLon = PasteLon;
            UpdateStats();
        }

        private void PasteToEndButton_Click(object sender, EventArgs e)
        {
            EndLatitudeTextBox.Text = Conversions.DecDeg2SCT(PasteLat, true);
            EndLongitudeTextBox.Text = Conversions.DecDeg2SCT(PasteLon, false);
            EndLat = PasteLat;
            EndLon = PasteLon;
            UpdateStats();
        }

        private void UpdateStats()
        {
            bool setRadius = false;
            if ((CenterLat != -1) && (CenterLon != -1))
            {
                if ((StartLat != -1) && (StartLon != -1))
                {
                    StartBrg = LatLongCalc.Bearing(CenterLat, CenterLon, StartLat, StartLon);
                    StartBrgTextBox.Text = StartBrg.ToString("000");
                    ArcRadius = LatLongCalc.Distance(CenterLat, CenterLon, StartLat, StartLon);
                    StartDistTextBox.Text = ArcRadius.ToString("0.000");
                    setRadius = true;
                }
                else
                {
                    StartBrg = -1;
                    StartBrgTextBox.Text = string.Empty;
                    StartDistTextBox.Text = string.Empty;
                }
                if ((EndLat != -1) && (EndLon != -1))
                {
                    EndBrg = LatLongCalc.Bearing(CenterLat, CenterLon, EndLat, EndLon);
                    EndBrgTextBox.Text = EndBrg.ToString("000");
                    ArcRadius = LatLongCalc.Distance(CenterLat, CenterLon, EndLat, EndLon);
                    StartDistTextBox.Text = ArcRadius.ToString("0.000");
                    setRadius = true;
                }
                else
                {
                    EndBrg = -1;
                    EndBrgTextBox.Text = string.Empty;
                }
            }
            else
            {
                ArcRadius = EndBrg = StartBrg -1;
            }
            if (!setRadius)
            {
                ArcRadius = -1;
                StartDistTextBox.Text = string.Empty;
            }
        }
    }
}
