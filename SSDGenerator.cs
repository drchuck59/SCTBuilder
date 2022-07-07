using System;
using System.IO;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace SCTBuilder
{
    public partial class SSDGenerator : Form
    {
        private static bool UseFixesAsCoords;
        private static bool IncludeSidStarReferences;
        private static bool DrawFixSymbolsOnDiagrams;
        private static bool DrawFixLabelsOnDiagrams;
        private static char prefix = InfoSection.STARprefix;
        static readonly string cr = Environment.NewLine;
        static List<object> FixData = new List<object>();
        static List<object> VORData = new List<object>();
        static List<object> NDBData = new List<object>();
        static List<string> SSDlines = new List<string>();
        static List<string> FixesUsed = new List<string>();
        static List<string> APTsUsed = new List<string>();
        static string RefResult = string.Empty;
        public static string BigResult = string.Empty;
        public static string SSDID = string.Empty;
        public static string SSDcode = string.Empty;
        public SSDGenerator()
        {
            InitializeComponent();
        }

        private void SSDGenerator_Load(object sender, EventArgs e)
        {
            SaveSettings();
        }

        private void SaveSettings()
        {
            UseFixesAsCoordsCheckBox.Checked = UseFixesAsCoords = InfoSection.UseFixesAsCoords;
            IncludeSidStarReferencesCheckBox.Checked = IncludeSidStarReferences = InfoSection.IncludeSidStarReferences;
            DrawFixLabelsOnDiagramsCheckBox.Checked = DrawFixLabelsOnDiagrams = InfoSection.DrawFixLabelsOnDiagrams;
            DrawFixSymbolsOnDiagramsCheckBox.Checked = DrawFixSymbolsOnDiagrams = InfoSection.DrawFixSymbolsOnDiagrams;
        }

        private void RestoreSettings()
        {
            InfoSection.UseFixesAsCoords = UseFixesAsCoords;
            InfoSection.IncludeSidStarReferences = IncludeSidStarReferences;
            InfoSection.DrawFixSymbolsOnDiagrams = DrawFixSymbolsOnDiagrams;
            InfoSection.DrawFixLabelsOnDiagrams = DrawFixLabelsOnDiagrams;
        }

        private void IdentifierTextBox_TextChanged(object sender, EventArgs e)
        {
            // Search the tables for fixes and return the coordinates, 
            // then place the coordinates into the list box.
            //
            if (IdentifierTextBox.TextLength != 0)
            {
                // First, be sure there is data in the database!
                DataTable dtSSD = Form1.SSD;
                if (dtSSD.Rows.Count != 0) 
                {
                    // Load the gridview - can be sorted later.  Future: add [Selected]
                    string filter = "[SSDcode] LIKE '" + IdentifierTextBox.Text + "*" + "'";
                    DataView dvSSD = new DataView(dtSSD, filter, "SSDcode", DataViewRowState.CurrentRows);
                    DataTable dtSSDList = dvSSD.ToTable(true, "SSDcode", "ID");
                    FixListDataGridView.DataSource = dtSSDList;
                    FixListDataGridView.DefaultCellStyle.Font = new Font("Arial", 9);
                    FixListDataGridView.Columns[0].HeaderText = "Code";
                    FixListDataGridView.Columns[1].Visible = false;
                    if (FixListDataGridView.Rows.Count != 0)
                    {
                        FixListDataGridView.AutoResizeColumn(0, DataGridViewAutoSizeColumnMode.AllCells);
                    }
                }
                else
                {
                    string Msg = "You must activate your FAA data before you can search for SIDs & STARs.";
                    SCTcommon.SendMessage(Msg);
                    IdentifierTextBox.Text = string.Empty;
                }
            }
            else
            {
                FixListDataGridView.DataSource = null;
            }
        }

        private void SSDGenerator_FormClosing(object sender, FormClosingEventArgs e)
        {
            RestoreSettings();
        }

        private void Copy2ClipboardButton_Click(object sender, EventArgs e)
        {
            string Msg = "Textbox copied to clipboard";
            if (OutputTextBox.TextLength != 0)
            {
                Clipboard.Clear();
                Clipboard.SetText(OutputTextBox.Text.ToString());
            }
            else
            {
                Msg = "No text in output textbox to copy!";
            }
            SCTcommon.UpdateLabel(UpdatingLabel, Msg, 1000);
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
            ClearVariables();
        }

        private static void ClearVariables()
        {
            RefResult = string.Empty;
            BigResult = string.Empty;
            FixData = new List<object>();
            VORData = new List<object>();
            NDBData = new List<object>();
            SSDlines = new List<string>();
            FixesUsed = new List<string>();
            APTsUsed = new List<string>();
            prefix = InfoSection.STARprefix;
        }

        private void AddLinesButton_Click(object sender, EventArgs e)
        {
            if (IdentifierTextBox.Text != FixListDataGridView.CurrentRow.Cells[0].Value.ToString())
            {
                SCTcommon.SendMessage("Exact match required. Hint: Double click the desired procedure first.");
                AddLinesButton.Enabled = false;
            }
            else
            {
                SCTcommon.UpdateLabel(UpdatingLabel, "Working on it");
                Refresh();
                if (FixListDataGridView.CurrentRow != null)
                {
                    SSDID = FixListDataGridView.CurrentRow.Cells[1].Value.ToString();
                    WriteSidStar();
                    if (SSDID.Substring(0, 1) == "D")
                        prefix = InfoSection.SIDprefix;
                }
                OutputTextBox.Text = SCTstrings.SectionHeader(SSDID + " " + SSDcode);
                OutputTextBox.Text += cr + SSDHeader(SSDcode, string.Empty, 1, prefix);
                OutputTextBox.Text += cr + BigResult;
                OutputTextBox.Text += cr + SCTstrings.SectionFooter(SSDID + " " + SSDcode);
            }
            SCTcommon.UpdateLabel(UpdatingLabel);
            Refresh();
        }

        public static void WriteSidStar()
            // Called routine to write a SINGLE SSD diagram
            // Diagram in BigResult
            // Calling routine is responsible for writing file and/or headers and footers
        {
            DataTable SSD = Form1.SSD;
            DataView dvSSD = new DataView(SSD)

            {
                RowFilter = "[ID] = '" + SSDID + "'",
                Sort = "Sequence",
            };
            // Build the list of the SSD information 
            if (dvSSD.Count != 0)
                BuildSSD(dvSSD);
            if (InfoSection.IncludeSidStarReferences)
                WriteSSDrefs();
            SSDLinesToBigResult();          // Since Labels depend on lines, call them here
            if (BigResult.Length == 0)
                BigResult = ";   No data for this procedure - possibly radar only?";
        }

        public static void BuildSSD(DataView dvSSD)
        {
            // Builds ONE SID or STAR from ONE SSD dataview (preselected)
            // Everything goes in List<string>s first - this routine  loads the Lists
            object[] NavData;

            // Various and sundry variables for the loop - and clear the others
            ClearVariables();
            double Lat1 = -1; double Lon1 = -1; string space = new string(' ', 27);
            double Lat0 = -1; double Lon0 = -1;
            string lastFix = string.Empty; string curFix; string FixType0;
            string FixType1; string SSDname; string TransitionName;
            string TransitionCode;
            int FixCount0; int FixCount1;

            // Get the name and code for this SSD
            SSDname = dvSSD[0]["SSDName"].ToString();
            SSDcode = dvSSD[0]["SSDcode"].ToString();
            if (Convert.ToChar(dvSSD[0][0].ToString().Substring(0, 1)) == 'D') 
                prefix = InfoSection.SIDprefix;
            // Now loop the entire SSD to get the lines, etc.
            foreach (DataRowView SSDrow in dvSSD)
            {
                // Get the basics - usual process: Lat1, shift to Lat0 or not, print...
                // Regardless, do a shift at the end (Making values empty indicates pen up)
                curFix = SSDrow["NavAid"].ToString();

                // The FixType tells us what to do next
                FixType1 = SSDrow["FixType"].ToString();
                // If it's an airport, record the APT ICOA and move to next row
                if (FixType1 == "AA" || FixType1 == "NA")
                {
                    // Save the APTs for later...
                    Add2ListIfNew(APTsUsed, curFix);
                    curFix = string.Empty;      // Pen up next 2 loops
                }
                else
                {
                    // Save the FIX for later if new...
                    FixCount0 = FixesUsed.Count;
                    Add2ListIfNew(FixesUsed, curFix);
                    FixCount1 = FixesUsed.Count;
                    // NavData: ID(opt), FacilityID, Frequency(opt), Latitude, Longitude, NameOrFixUse, FixType
                    NavData = SCTcommon.GetNavData(curFix);
                    if (FixCount1 > FixCount0)
                    {
                        if (NavData[6].ToString().IndexOf("FIX") != -1)
                            FixData.Add(NavData);
                        if (NavData[6].ToString().IndexOf("VOR") != -1)
                            VORData.Add(NavData);
                        if (NavData[6].ToString().IndexOf("NDB") != -1)
                            NDBData.Add(NavData);
                    }
                    Lat1 = Convert.ToDouble(NavData[3]);
                    Lon1 = Convert.ToDouble(NavData[4]);
                }
                // If there's a Transition Name, it starts a new line set.
                // Keep these coordinates to start the line
                TransitionName = SSDrow["TransitionName"].ToString();
                TransitionCode = SSDrow["TransitionCode"].ToString();
                if (TransitionName.Length != 0)
                {
                    SSDlines.Add(space + "; " + TransitionName);
                }
                else
                {
                    // Finally get the line between waypoints
                    if ((lastFix.Length != 0) && (curFix.Length != 0) && (lastFix != curFix))
                    {
                        SSDlines.Add(SCTstrings.SSDout(Lat0, Lon0, Lat1, Lon1, lastFix, curFix, InfoSection.UseFixesAsCoords));
                        // Draw the fix names.  Angle and Scale not used for SSDs
                    }
                }
                // Shift the values for the next item
                Lat0 = Lat1; Lon0 = Lon1; lastFix = curFix; FixType0 = FixType1;
                TransitionCode = TransitionName = string.Empty;
            }
            // Lastly insert the symbols and labels
            if (InfoSection.DrawFixSymbolsOnDiagrams || InfoSection.DrawFixLabelsOnDiagrams)
                SSDlines.Add(DrawFixInfo(FixesUsed));
            // Need to add the ALT and Speed items here
        }

        public static string WriteSSDrefs()
        {
            // Sends the results of BuildSSD to the designated file
            // This is the header references
            // Write the file for this SSD
            string[] strOut = new string[6];
            RefResult += cr + "[AIRPORT]";
            DataView dvAPT = new DataView(Form1.APT);
            DataView dvTWR = new DataView(Form1.TWR);
            foreach (string Arpt in APTsUsed)
            {
                dvAPT.RowFilter = "FacilityID = '" + Arpt + "'";
                dvTWR.RowFilter = "FacilityID = '" + Arpt + "'";
                strOut[0] = Conversions.ICOA(Arpt);
                if (dvTWR.Count != 0)
                    strOut[1] = string.Format("{0:000.000}", dvTWR[0]["LCLfreq"].ToString());
                else
                    strOut[1] = "122.8  ";
                strOut[2] = Conversions.Degrees2SCT(Convert.ToDouble(dvAPT[0]["Latitude"]), true);
                strOut[3] = Conversions.Degrees2SCT(Convert.ToDouble(dvAPT[0]["Longitude"]), false);
                strOut[4] = dvAPT[0]["Name"].ToString();
                RefResult += cr + SCTstrings.APTout(strOut.ToArray());
            }
            dvAPT.Dispose();
            dvTWR.Dispose();
            // Write the NavAids for this curFix
            // Fix, Frequency(opt), Latitude, Longitude, Name, FixType
            if (VORData.Count > 0)
            {
                // NavData: ID(opt), FacilityID, Frequency(opt), Latitude, Longitude, Name, FixType
                RefResult += cr + "[VOR]";
                foreach (object[] VORs in VORData)
                {
                    // strOut expects 0-Fix, 1-Freq, 2-Lat, 3-Lon, 4-Name, 5-Type
                    strOut[0] = VORs[1].ToString();
                    strOut[1] = VORs[2].ToString();
                    strOut[2] = Conversions.Degrees2SCT(Convert.ToDouble(VORs[3]), true);
                    strOut[3] = Conversions.Degrees2SCT(Convert.ToDouble(VORs[4]), false);
                    strOut[4] = VORs[5].ToString();
                    strOut[5] = VORs[6].ToString();
                    RefResult += cr + SCTstrings.VORout(strOut);
                }
            }
            // NavData: ID(opt), FacilityID, Frequency(opt), Latitude, Longitude, Name, FixType
            if (NDBData.Count > 0)
            {
                RefResult += cr + "[NDB]";
                foreach (object[] NDBs in NDBData)
                {
                    strOut[0] = NDBs[1].ToString();
                    strOut[1] = NDBs[2].ToString();
                    strOut[2] = Conversions.Degrees2SCT(Convert.ToDouble(NDBs[3]), true);
                    strOut[3] = Conversions.Degrees2SCT(Convert.ToDouble(NDBs[4]), false);
                    strOut[4] = NDBs[5].ToString();
                    strOut[5] = NDBs[6].ToString();
                    RefResult += cr + SCTstrings.NDBout(strOut);
                }
            }
            // NavData: ID(opt), FacilityID, Frequency(opt), Latitude, Longitude, FixUse, FixType
            if (FixData.Count > 0)
            {
                RefResult += cr + "[FIXES]";
                foreach (object[] FIXes in FixData)
                {
                    strOut[0] = FIXes[1].ToString();
                    strOut[2] = Conversions.Degrees2SCT(Convert.ToDouble(FIXes[3]), true);
                    strOut[3] = Conversions.Degrees2SCT(Convert.ToDouble(FIXes[4]), false);
                    strOut[4] = FIXes[5].ToString();
                    RefResult += cr + SCTstrings.FIXout(strOut);
                }
            }
            return RefResult;
        }

        public static void SSDLinesToBigResult()
        {
            bool FirstLine = true;
            foreach (string Line in SSDlines)
            {
                if (Line.Length != 0)
                {
                    if (FirstLine)
                    {
                        BigResult += Line;
                        FirstLine = false;
                    }
                    else
                        BigResult += cr + Line;         // These have crs on each line
                }
            }
        }
        private static string SSDHeader(string Header = "", string Comment = "", int MarkerCount = 0, char Marker = '=')
        {
            // RETURNS a SID/STAR header: Can be just dummy coords (no parameters) if Header string empty
            string Mask = string.Empty; string result = string.Empty; int Pad = 27;
            string DummyCoords = "N000.00.00.000 E000.00.00.000 N000.00.00.000 E000.00.00.000";
            if (Header.Length != 0)
            {
                if (MarkerCount != 0) Mask = new string(Marker, MarkerCount);
                result = (Mask + Header).Trim();
            }
            result = result.PadRight(Pad) + DummyCoords;
            if (Comment.Length != 0) result += " ; " + Comment;
            return result;
        }

        private static List<string> Add2ListIfNew(List<string> Fixes, string NewFix)
        {
            bool Found = false;
            foreach (string Name in Fixes)
            {
                if (Name == NewFix) Found = true;
            }
            if (!Found) Fixes.Add(NewFix);
            return Fixes;
        }

        private static string DrawFixInfo(List<string> FixNames)
        {
            // Calling function for fixes in diagrams
            // Because labels and/or symbols might be drawn
            // Will need to add the DrawFixNames for ALT and SPEED here
            string result = string.Empty;
            float Latitude;
            float Longitude;
            double[] AdjustedCoords = null;
            foreach (string Fix in FixNames)
            {
                // FixInfo returns: 0-Fix (calling ID), 1-Freq, 2-Lat, 3-Lon, 4-Name or Use, 5-Type
                object[] FixData = SCTcommon.GetNavData(Fix);
                if ((FixData[5].ToString() != "NA") && (FixData[0].ToString().Length != 0))
                {
                    Latitude = Convert.ToSingle(FixData[3]);
                    Longitude = Convert.ToSingle(FixData[4]);
                    if ((Latitude != 0) && (Longitude != 0))
                    {
                        if (InfoSection.DrawFixSymbolsOnDiagrams)
                        {
                            result += Hershey.DrawSymbol(FixData);
                            float charWidth = Hershey.width / 60f;
                            AdjustedCoords = LatLongCalc.Destination(Latitude, Longitude, charWidth, 90, 'N');
                        }
                        if (InfoSection.DrawFixLabelsOnDiagrams)
                        {
                            if (!InfoSection.DrawFixSymbolsOnDiagrams)
                            {
                                AdjustedCoords = LatLongCalc.Destination(Latitude, Longitude, 45, 90, 'N');
                            }
                            result += Hershey.WriteHF(Fix, AdjustedCoords[0], AdjustedCoords[1], (int)InfoSection.MagneticVariation);
                        }
                    }
                }
            }
            return result;
        }

        private void FixListDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            IdentifierTextBox.Text = FixListDataGridView.CurrentCell.Value.ToString();
            SSDID = FixListDataGridView.CurrentRow.Cells[1].Value.ToString();
            AddLinesButton.Enabled = true;
        }

        private void DrawFixSymbolsOnDiagramsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            InfoSection.DrawFixSymbolsOnDiagrams = DrawFixSymbolsOnDiagramsCheckBox.Checked;
        }

        private void DrawFixLabelsOnDiagramsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            InfoSection.DrawFixLabelsOnDiagrams = DrawFixLabelsOnDiagramsCheckBox.Checked;
        }

        private void IncludeSidStarReferencesCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            InfoSection.IncludeSidStarReferences = IncludeSidStarReferencesCheckBox.Checked;
        }

        private void IdentifierTextBox_Validated(object sender, EventArgs e)
        {
            // If the gridview row was not selected (e.g., Pasting into the cell), must select the ID
            // Make sure user selected a valid identifier
            DataTable dtSSD = Form1.SSD;
            string filter = "[SSDcode] LIKE '" + IdentifierTextBox.Text + "*" + "'";
            DataView dvSSD = new DataView(dtSSD, filter, "SSDcode", DataViewRowState.CurrentRows);
            if (dvSSD.Count == 0)
            {
                SCTcommon.SendMessage("Invalid identifier - select from list");
                AddLinesButton.Enabled = false;
                Refresh();
            }
            else
            {
                foreach (DataGridViewRow row in FixListDataGridView.Rows)
                {
                    if (FixListDataGridView.CurrentRow == null)
                    {
                        if (row.Cells[0].ToString().Equals(IdentifierTextBox.Text))
                        {
                            row.Selected = true;
                            SSDID = row.Cells[1].Value.ToString();
                        }
                    }
                }
            }
                AddLinesButton.Enabled = true;
            dvSSD.Dispose();
        }
    }
}
