using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        private static int OutbndTrk = 0;
        private static double LegLnth = 0;
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

        private void LoadAltTypeComboBox()
        {
            DataTable dT = new DataTable();
            dT.Columns.Add("AltTypeValue", typeof(int));
            dT.Columns.Add("AltTypeName", typeof(string));
            dT.Rows.Add(0, "AOB 6000 ft");
            dT.Rows.Add(1, "AOB 14000 ft");
            dT.Rows.Add(2, "Above 14000 ft");
            dT.Rows.Add(3, "USAF airfield");
            dT.Rows.Add(4, "USN airfield");
            dT.Rows.Add(5, "Rotary below 1000 ft");
            AltitudeTypeComboBox.DataSource = dT;
            AltitudeTypeComboBox.DisplayMember = "AltTypeName";
            AltitudeTypeComboBox.ValueMember = "AltTypeValue";
            AltitudeTypeComboBox.SelectedIndex = 2;
        }

        private void PasteToTextBox_Validated(object sender, EventArgs e)
        {
            CopyToHoldButton.Enabled = TestTextBox(PasteToTextBox);
        }

        private void PasteToTextBox_TextChanged(object sender, EventArgs e)
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
                if ((tb.Text.IndexOf("N") > -1) || (tb.Text.IndexOf("S") > -1)) method = 1;
                if (method == 0)
                    if ((tb.Text.IndexOf("W") > -1) || (tb.Text.IndexOf("w") > -1)) method = 2;
                    else method = 0;
            }
            if ((tb.Modified) && tb.TextLength != 0)
            {
                if (LatLonParser.TryParseAny(tb))
                {
                    switch (method)
                    {
                        case 0:
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
    }
}
