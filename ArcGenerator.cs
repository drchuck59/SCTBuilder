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
        private static double ChordLength = -1;
        private static double ChordHeight = -1;
        private static double ChordArcLength = -1;
        private static double ChordArcAngle = -1;
        private static double ChordRadius = -1;
        private static readonly string cr = Environment.NewLine;

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
            string Msg = "Contents of output textbox copied to clipboard";
            if (OutputTextBox.TextLength != 0)
            {
                Clipboard.Clear();
                Clipboard.SetText(OutputTextBox.Text.ToString());
                SCTcommon.UpdateLabel(UpdateLabel, Msg, 1000);
            }
            else
            {
                Msg = "No text in output textbox to copy!";
                SCTcommon.UpdateLabel(UpdateLabel, Msg, 1000);
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
                if (FixOffsetCheckBox.Checked)
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
                StartLatitudeTextBox.Text = Conversions.Degrees2SCT(Lat, true);
                StartLat = Lat;
                StartLongitudeTextBox.Text = Conversions.Degrees2SCT(Lon, false);
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
                if (FixOffsetCheckBox.Checked)
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
                EndLatitudeTextBox.Text = Conversions.Degrees2SCT(Lat, true);
                EndLat = Lat;
                EndLongitudeTextBox.Text = Conversions.Degrees2SCT(Lon, false);
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
                if (FixOffsetCheckBox.Checked)
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
                CenterLatitudeTextBox.Text = Conversions.Degrees2SCT(Lat, true);
                CenterLat = Lat;
                CenterLongitudeTextBox.Text = Conversions.Degrees2SCT(Lon, false);
                CenterLon = Lon;
                //MagVarTextBox.Text = MagVar.ToString();
                CenterFixTextBox.Text = FixListDataGridView.SelectedRows[0].Cells[0].Value.ToString() + FixText;
                UpdateStats();
            }
        }

        private void ArcGenerator_Load(object sender, EventArgs e)
        {
            MagVarTextBox.Text = InfoSection.MagneticVariation.ToString();
            LoadSectionComboBox();
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
                EndGroupBox.Enabled = false;
            }
            else
            {
                MagBrgCheckBox.Enabled = EndRadialNUD.Enabled = StartRadialNUD.Enabled = true;
                EndGroupBox.Enabled = true;
            }

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
                double CenterLat = Conversions.DMS2Degrees(CenterLatitudeTextBox.Text);
                double CenterLon = Conversions.DMS2Degrees(CenterLongitudeTextBox.Text);
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
                    StartLatitudeTextBox.Text = Conversions.Degrees2SCT(Lat, true);
                    StartLongitudeTextBox.Text = Conversions.Degrees2SCT(Lon, false);
                    StartLat = Lat;
                    StartLon = Lon;
                    EndLatitudeTextBox.Text = Conversions.Degrees2SCT(Lat, true);
                    EndLongitudeTextBox.Text = Conversions.Degrees2SCT(Lon, false);
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
                    StartLatitudeTextBox.Text = Conversions.Degrees2SCT(Lat, true);
                    StartLongitudeTextBox.Text = Conversions.Degrees2SCT(Lon, false);
                    StartLat = Lat;
                    StartLon = Lon;
                    Coords = LatLongCalc.Destination(CenterLat, CenterLon, Dist, BrgEnd, 'N');
                    Lat = Coords[0];
                    Lon = Coords[1];
                    EndLatitudeTextBox.Text = Conversions.Degrees2SCT(Lat, true);
                    EndLongitudeTextBox.Text = Conversions.Degrees2SCT(Lon, false);
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
                StartLatitudeTextBox.Text = Conversions.Degrees2SCT(StartLat, true);
                StartLongitudeTextBox.Text = Conversions.Degrees2SCT(StartLon, false);
            }
            if ((EndLat == -1) || (EndLon == -1))
            {
                EndLatitudeTextBox.Text = EndLongitudeTextBox.Text = EndFixTextBox.Text = string.Empty;
            }
            else
            {
                EndLatitudeTextBox.Text = Conversions.Degrees2SCT(EndLat, true);
                EndLongitudeTextBox.Text = Conversions.Degrees2SCT(EndLon, false);
            }
            UpdateStats();
        }

        private void CenterLatitudeTextBox_Validated(object sender, EventArgs e)
        {
            if (CrossForm.TestTextBox(CenterLatitudeTextBox))
            {
                CenterLat = CrossForm.Lat;
                UpdateStats();
            }
        }

        private void CenterLongitudeTextBox_Validated(object sender, EventArgs e)
        {
            if (CrossForm.TestTextBox(CenterLongitudeTextBox))
            {
                CenterLon = CrossForm.Lon;
                UpdateStats();
            }
        }

        private void StartLatitudeTextBox_Validated(object sender, EventArgs e)
        {
            if (CrossForm.TestTextBox(StartLatitudeTextBox))
            {
                StartLat = CrossForm.Lat;
                UpdateStats();
            }
        }

        private void StartLongitudeTextBox_Validated(object sender, EventArgs e)
        {
            if (CrossForm.TestTextBox(StartLongitudeTextBox))
            {
                StartLon = CrossForm.Lon;
                UpdateStats();
            }
        }

        private void EndLatitudeTextBox_Validated(object sender, EventArgs e)
        {
            if (CrossForm.TestTextBox(EndLatitudeTextBox))
            {
                EndLat = CrossForm.Lat;
                UpdateStats();
            }
        }

        private void EndLongitudeTextBox_Validated(object sender, EventArgs e)
        {
            if (CrossForm.TestTextBox(EndLongitudeTextBox))
            {
                EndLon = CrossForm.Lon;
                UpdateStats();
            }
        }

        private void PasteToTextBox_Validated(object sender, EventArgs e)
        {
            PasteToCenterButton.Enabled = PasteToEndButton.Enabled = PasteToStartButton.Enabled = CrossForm.TestTextBox(PasteToTextBox);
        }

        private void PasteToCenterButton_Click(object sender, EventArgs e)
        {
            CenterLat = CrossForm.Lat;
            CenterLatitudeTextBox.Text = Conversions.Degrees2SCT(CenterLat, true);
            CenterLon = CrossForm.Lon;
            CenterLongitudeTextBox.Text = Conversions.Degrees2SCT(CenterLon, false);
            UpdateStats();
        }

        private void PasteToStartButton_Click(object sender, EventArgs e)
        {
            StartLat = CrossForm.Lat;
            StartLon = CrossForm.Lon;
            StartLatitudeTextBox.Text = Conversions.Degrees2SCT(StartLat, true);
            StartLongitudeTextBox.Text = Conversions.Degrees2SCT(StartLon, false);
            UpdateStats();
        }

        private void PasteToEndButton_Click(object sender, EventArgs e)
        {
            EndLat = CrossForm.Lat;
            EndLon = CrossForm.Lon;
            EndLatitudeTextBox.Text = Conversions.Degrees2SCT(EndLat, true);
            EndLongitudeTextBox.Text = Conversions.Degrees2SCT(EndLon, false);
            UpdateStats();
        }

        private void UpdateStats()
        {
            if ((CenterLat != -1) && (CenterLon != -1))
            {
                if ((StartLat != -1) && (StartLon != -1))
                {
                    StartBrg = LatLongCalc.Bearing(CenterLat, CenterLon, StartLat, StartLon);
                    if (StartBrg == 0) StartBrg = 360;
                    StartRadialNUD.Value = Convert.ToInt32(StartBrg);
                    ArcRadius = LatLongCalc.Distance(CenterLat, CenterLon, StartLat, StartLon);
                    CalcDistanceTextBox.Text = ArcRadius.ToString("F3");
                }
                else
                {
                    ArcRadius = -1;
                    StartBrg = -1;
                    StartRadialNUD.Value = 1;
                    CalcDistanceTextBox.Text = string.Empty;
                }
                if ((EndLat != -1) && (EndLon != -1))
                {
                    EndBrg = LatLongCalc.Bearing(CenterLat, CenterLon, EndLat, EndLon);
                    if (EndBrg == 0) EndBrg = 360;
                    EndRadialNUD.Value = Convert.ToInt32(EndBrg);
                    EndDistTextBox.Text = LatLongCalc.Distance(CenterLat, CenterLon, EndLat, EndLon).ToString("F3");
                    if (ArcRadius != -1) ChordStats();
                }
                else
                {
                    EndBrg = -1;
                    EndRadialNUD.Value = 1;
                    EndDistTextBox.Text = string.Empty;
                }
            }
            else
            {
                ArcRadius = EndBrg = StartBrg - 1;
            }
            CheckArcButton();
        }

        private void ChordStats()
        {
            ChordLength = LatLongCalc.Distance(EndLat, EndLon, StartLat, StartLon);
            ChordArcAngle = LatLongCalc.Deg2Rad(Math.Abs(StartBrg = EndBrg));
            ChordArcLength = ChordArcAngle * ArcRadius;
            ChordRadius = ChordArcLength / 2;
            ChordLengthTextBox.Text = ChordLength.ToString("F3");
            ChordRadiusTextBox.Text = ChordRadius.ToString("F3");
            ChordArcLengthTextBox.Text = ChordArcLength.ToString("F3");
        }

        private void AddArc_Click(object sender, EventArgs e)
        {
            OutputTextBox.Text = BuildArcString();
        }

        private string BuildArcString()
        {
            string Lat0 = string.Empty; string Lon0 = string.Empty; string Lat1; string Lon1;
            string output = string.Empty;
            double[] Coords;
            if (CircleCheckBox.Checked)
            {
                StartBrg = 0; EndBrg = 360;
            }
            // Make sure we aren't crossing 360...
            if ((StartBrg + (EndBrg - StartBrg)) > 360)
            {
                for (double i = StartBrg; i < 360; i++)
                {
                    Coords = LatLongCalc.Destination(StartLat, StartLon, ArcRadius, i, 'N');
                    Lat1 = Conversions.Degrees2SCT(Coords[0], true);
                    Lon1 = Conversions.Degrees2SCT(Coords[1], false);
                    output += OutputText(Lat0, Lon0, Lat1, Lon1);
                    Lat0 = Lat1; Lon0 = Lon1;
                }
                for (double i = 1; i < EndBrg; i++)
                {
                    Coords = LatLongCalc.Destination(StartLat, StartLon, ArcRadius, i, 'N');
                    Lat1 = Conversions.Degrees2SCT(Coords[0], true);
                    Lon1 = Conversions.Degrees2SCT(Coords[1], false);
                    output += OutputText(Lat0, Lon0, Lat1, Lon1);
                    Lat0 = Lat1; Lon0 = Lon1;
                }
            }
            else
            {
                for (double i = StartBrg; i < EndBrg; i++)
                {
                    Coords = LatLongCalc.Destination(StartLat, StartLon, ArcRadius, i, 'N');
                    Lat1 = Conversions.Degrees2SCT(Coords[0], true);
                    Lon1 = Conversions.Degrees2SCT(Coords[1], false);
                    output += OutputText(Lat0, Lon0, Lat1, Lon1);
                    Lat0 = Lat1; Lon0 = Lon1;
                }
            }
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

        private void ColorValueTextBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                Color c = colorDialog1.Color;
                ColorNameTextBox.Text = c.ToString();
                ColorValueTextBox.Text = "#" + c.R.ToString("X2") + c.G.ToString("X2") + c.B.ToString("X2");
            }
        }

        private void FixEndCoordButton_Click(object sender, EventArgs e)
        {
            double[] coords =
            LatLongCalc.Destination(CenterLat, CenterLon, ArcRadius, Convert.ToDouble(EndRadialNUD.Value), 'N');
            EndLat = coords[0];
            EndLon = coords[1];
            EndLatitudeTextBox.Text = Conversions.Degrees2SCT(EndLat, true);
            EndLongitudeTextBox.Text = Conversions.Degrees2SCT(EndLon, false);
            UpdateStats();
        }

        private void StartRadialNUD_ValueChanged(object sender, EventArgs e)
        {
            StartBrg = Convert.ToDouble(StartRadialNUD.Value);
        }

        private void EndRadialNUD_ValueChanged(object sender, EventArgs e)
        {
            EndBrg = Convert.ToDouble(EndRadialNUD.Value);
        }

        private void RadBrgStartButton_Click(object sender, EventArgs e)
        {
            double[] coords;
            if ((CenterLat != -1) && (CenterLon != -1))
            {
                coords = LatLongCalc.Destination(CenterLat, CenterLon, ArcRadius, Convert.ToDouble(StartRadialNUD.Value), 'N');
                StartLat = coords[0];
                StartLon = coords[1];
                StartLatitudeTextBox.Text = Conversions.Degrees2SCT(StartLat, true);
                StartLongitudeTextBox.Text = Conversions.Degrees2SCT(StartLon, false);
            }
        }

        private void FixOffsetCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            FixBrgNUD.Enabled = FixDistTextBox.Enabled = FixOffsetCheckBox.Checked;
            if (!FixOffsetCheckBox.Checked)
            {
                FixBrgNUD.Value = 360; FixDistTextBox.Text = string.Empty;
            }
        }

        private void FixListDataGridView_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (FixListDataGridView.SelectedRows.Count == 1)
            {
                IdentifierTextBox.Text = FixListDataGridView.SelectedRows[0].Cells[0].Value.ToString();
            }
        }

        private void ArcHeightTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Extensions.CharIsDecimal(e.KeyChar, ref FixDistTextBox, 1);
        }

        private void ClearArcButton_Click(object sender, EventArgs e)
        {
            ArcHeightTextBox.Text = string.Empty;
        }

        private void ArcHeightTextBox_Validated(object sender, EventArgs e)
        {
            if (ArcHeightTextBox.TextLength != 0)
            {
                double tempHeight = Convert.ToDouble(ArcHeightTextBox.Text);
                if (tempHeight > ChordRadius)
                {
                    SCTcommon.SendMessage("Height cannot exceed chord radius (R)." + cr +
                        "(One-half distance between start and end coordinate");
                    ArcHeightTextBox.Text = ArcRadius.ToString("F3");
                }
                if (tempHeight <= 0)
                {
                    SCTcommon.SendMessage("Height must be positive value");
                    ArcHeightTextBox.Text = ArcRadius.ToString("F3");
                }
                ChordHeight = Convert.ToDouble(ArcHeightTextBox.Text);
                CenterFromChord();
            }
        }

        private void CenterFromChord()
        {
            // The center is perpendicular to the midpoint of the start/end chord
            // Since I'm always turning to the right, perpendicular is always +90
            double Lat0 = (StartLat + EndLat) / 2.0;
            double Lon0 = (StartLon + EndLon) / 2.0;
            double Brg = (LatLongCalc.Bearing(StartLat, StartLon, EndLat, EndLon) + 90) % 360;
            double[] Coords = LatLongCalc.Destination(Lat0, Lon0, RadiusFromChordArc(), Brg, 'N');
            CenterLat = Coords[0]; CenterLon = Coords[1];
            CenterLatitudeTextBox.Text = Conversions.Degrees2SCT(CenterLat, true);
            CenterLongitudeTextBox.Text = Conversions.Degrees2SCT(CenterLon, false);
            CenterFixTextBox.Text = "<calculated from chord>";
            StartBrg = (LatLongCalc.Bearing(CenterLat, CenterLon, StartLat, StartLon) + 90) % 360;
            StartRadialNUD.Value = (int)StartBrg;
            EndBrg = (LatLongCalc.Bearing(CenterLat, CenterLon, EndLat, EndLon) + 90) % 360;
            EndRadialNUD.Value = (int)EndBrg;
        }

        private double RadiusFromChordArc ()
        {
            return Math.Pow(ChordLength, 2) / (8 * ChordHeight) + (ChordHeight / 2.0);
        }
    }
}

