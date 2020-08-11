using Google.Protobuf;
using Microsoft.CodeAnalysis.CSharp;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

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
        private static bool CenterLatIsSCT = false;
        private static bool CenterLonIsSCT = false; 
        private static bool StartLatIsSCT = false;
        private static bool StartLonIsSCT = false;
        private static bool EndLatIsSCT = false;
        private static bool EndLonIsSCT = false;
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
                    DataTable dtFixList = dvVOR.ToTable(true, "FacilityID", "Latitude", "Longitude");
                    dtFixList.Merge(dvNDB.ToTable(true, "FacilityID", "Latitude", "Longitude"));
                    dtFixList.Merge(dvFIX.ToTable(true, "FacilityID", "Latitude", "Longitude"));
                    FixListDataGridView.DataSource = dtFixList;
                    FixListDataGridView.DefaultCellStyle.Font = new Font("Arial", 9);
                    FixListDataGridView.Columns[0].HeaderText = "ID";
                    if (FixListDataGridView.Rows.Count != 0)
                    {
                        FixListDataGridView.AutoResizeColumn(0, DataGridViewAutoSizeColumnMode.AllCells);
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
                StartLatitudeTextBox.Text =
                Conversions.DecDeg2SCT(Convert.ToDouble(FixListDataGridView.SelectedRows[0].Cells[1].Value), true);
                StartLat = Convert.ToDouble(FixListDataGridView.SelectedRows[0].Cells[1].Value);
                StartLongitudeTextBox.Text =
                    Conversions.DecDeg2SCT(Convert.ToDouble(FixListDataGridView.SelectedRows[0].Cells[2].Value), false);
                StartLon = Convert.ToDouble(FixListDataGridView.SelectedRows[0].Cells[2].Value);
                StartFixTextBox.Text = FixListDataGridView.SelectedRows[0].Cells[0].Value.ToString();
            }
        }

        private void FixToEndButton_Click(object sender, EventArgs e)
        {
            if (FixListDataGridView.SelectedRows.Count > 0)
            {
                EndLatitudeTextBox.Text =
                Conversions.DecDeg2SCT(Convert.ToDouble(FixListDataGridView.SelectedRows[0].Cells[1].Value), true);
                EndLat = Convert.ToDouble(FixListDataGridView.SelectedRows[0].Cells[1].Value);
                EndLongitudeTextBox.Text =
                    Conversions.DecDeg2SCT(Convert.ToDouble(FixListDataGridView.SelectedRows[0].Cells[2].Value), false);
                EndLon = Convert.ToDouble(FixListDataGridView.SelectedRows[0].Cells[2].Value);
                EndFixTextBox.Text = FixListDataGridView.SelectedRows[0].Cells[0].Value.ToString();
            }
        }

        private void ImportFix2CenterButton_Click(object sender, EventArgs e)
        {
            if (FixListDataGridView.SelectedRows.Count > 0)
            {
                CenterLatitudeTextBox.Text =
                    Conversions.DecDeg2SCT(Convert.ToDouble(FixListDataGridView.SelectedRows[0].Cells[1].Value), true);
                CenterLat = Convert.ToDouble(FixListDataGridView.SelectedRows[0].Cells[1].Value);
                CenterLongitudeTextBox.Text =
                    Conversions.DecDeg2SCT(Convert.ToDouble(FixListDataGridView.SelectedRows[0].Cells[2].Value), false);
                CenterLon = Convert.ToDouble(FixListDataGridView.SelectedRows[0].Cells[2].Value);
                CenterFixTextBox.Text = FixListDataGridView.SelectedRows[0].Cells[0].Value.ToString();
                CheckArcButton();
            }
        }

        private void ArcGenerator_Load(object sender, EventArgs e)
        {
            MagOffsetLabel.Text = "*For magnetic bearing, SUBTRACT " + InfoSection.MagneticVariation.ToString() + " degrees.";
        }

        private void StartBrgTextBox_TextChanged(object sender, EventArgs e)
        {
            int tempBrg = Convert.ToInt32(StartBrgTextBox.Text);
            if ((tempBrg < 0) || (tempBrg > 360))
            {
                SCTcommon.SendMessage("Brg must be in range 0 to 360");
                StartBrgTextBox.Text = "0";
            }
            else
                StartBrg = tempBrg;
        }

        private void StartBrgTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Extensions.CharIsDigit(e.KeyChar);
        }

        private void EndBrgTestBox_TextChanged(object sender, EventArgs e)
        {
            int tempBrg = Convert.ToInt32(EndBrgTestBox.Text);
            if ((tempBrg < 0) || (tempBrg > 360))
            {
                SCTcommon.SendMessage("Brg must be in range 0 to 360");
                EndBrgTestBox.Text = "0";
            }
            else
                EndBrg = tempBrg;
        }

        private void EndBrgTestBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Extensions.CharIsDigit(e.KeyChar);
        }

        private void CalcDistanceTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Extensions.CharIsDecimal(e.KeyChar, ref CalcDistanceTextBox, 1);
        }

        private void CalcDistanceTextBox_TextChanged(object sender, EventArgs e)
        {
            int tempRadius = Convert.ToInt32(CalcDistanceTextBox.Text);
            if ((tempRadius < 0) || (tempRadius > 50))
            {
                SCTcommon.SendMessage("Radius must be in range 0 to 50 NM");
                EndBrgTestBox.Text = "0";
            }
            else
                ArcRadius = tempRadius;
        }

        private void CenterLatitudeTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // e.handled = false means let the character pass
            if (CenterLatitudeTextBox.TextLength == 0)
            {
                CenterLatIsSCT = Extensions.IsValidSCTCoordKey(e.KeyChar, CenterLatitudeTextBox);
            }
            if (CenterLatIsSCT)
            {
                e.Handled = !Extensions.IsValidSCTCoordKey(e.KeyChar, CenterLatitudeTextBox);
                CenterLatitudeTextBox.MaxLength = 15;
            }
            else
            {
                e.Handled = !Extensions.IsValidDecCoordKey(e.KeyChar, CenterLatitudeTextBox);
                CenterLatitudeTextBox.MaxLength = 10;
            }
        }

        private void CenterLongitudeTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !Extensions.IsValidDecCoordKey(e.KeyChar, CenterLongitudeTextBox) ||
                !Extensions.IsValidSCTCoordKey(e.KeyChar, CenterLongitudeTextBox);
        }

        private void StartLatitudeTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !Extensions.IsValidDecCoordKey(e.KeyChar, StartLatitudeTextBox) ||
                !Extensions.IsValidSCTCoordKey(e.KeyChar, StartLatitudeTextBox);
        }

        private void EndLatitudeTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !Extensions.IsValidDecCoordKey(e.KeyChar, EndLatitudeTextBox) ||
                !Extensions.IsValidSCTCoordKey(e.KeyChar, EndLatitudeTextBox);
        }

        private void StartLongitudeTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !Extensions.IsValidDecCoordKey(e.KeyChar, StartLongitudeTextBox) ||
                !Extensions.IsValidSCTCoordKey(e.KeyChar, StartLongitudeTextBox);
        }


        private void EndLongitudeTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !Extensions.IsValidDecCoordKey(e.KeyChar, EndLongitudeTextBox) ||
                   !Extensions.IsValidSCTCoordKey(e.KeyChar, EndLongitudeTextBox);
        }

        private void FixListDataGridView_Click(object sender, EventArgs e)
        {
            Fix2CenterButton.Enabled = FixToSTartButton.Enabled =
                FixToEndButton.Enabled = (FixListDataGridView.CurrentRow.Index != -1);

        }

        private void CheckArcButton()
        {
            AddArcByCoordsButton.Enabled = (StartBrg != -1) && (EndBrg != -1) && 
                (CenterLat != -1) && (CenterLon != -1) && (ArcRadius != -1);
        }

        private void CenterLatitudeTextBox_Validated(object sender, EventArgs e)
        {
            double Lat;
            if (Extensions.IsNumeric(CenterLatitudeTextBox.Text))
            {
                Lat = Convert.ToDouble(CenterLatitudeTextBox.Text);
                if (Math.Abs(Lat) > 90)
                {
                    SCTcommon.SendMessage("Latitude must be between -90 and 90 degrees");
                    CenterLatitudeTextBox.Clear();
                }
                else
                {
                    CenterLat = Lat;
                    CenterLatitudeTextBox.Text = Conversions.DecDeg2SCT(Lat, true);
                }
            }
            else
            {
                Lat = Conversions.String2DecDeg(CenterLatitudeTextBox.Text);
                if (Lat == -1)
                {
                    SCTcommon.SendMessage("Invalid Latitude format (try QDD.MM.SS.ss");
                    CenterLatitudeTextBox.Clear();
                }
                else CenterLat = Lat;
            }
        }
    }
}
