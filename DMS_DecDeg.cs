using System;
using System.Windows.Forms;
using System.Text.RegularExpressions;


namespace SCTBuilder
{
    public partial class DMS_DecDeg : Form
    {
        public DMS_DecDeg()
        {
            InitializeComponent();
        }

        private void UpdateStrings()
        {
            if ((LonDMSTextBox.TextLength != 0) && (LatDMSTextBox.TextLength != 0))
                ToSingleLine(LatDMSTextBox.Text, LonDMSTextBox.Text, DMSTextBox);
            if ((LatDecTextBox.TextLength != 0) && (LonDecTextBox.TextLength != 0))
                ToSingleLine(LatDecTextBox.Text, LonDecTextBox.Text, DECTextBox);
            if ((LatSCTTextBox.TextLength != 0) && (LonSCTTextBox.TextLength != 0))
                ToSingleLine(LatSCTTextBox.Text, LonSCTTextBox.Text, SCTTextBox);
        }

        private void ToSingleLine(string Lat, string Lon, TextBox tb)
        {
            if ((Lat.Length != 0) && (Lon.Length != 0))
            {
                tb.Text = Lat + " " + Lon;
            }
        }

        private void LatDMSTextBox_Validated(object sender, EventArgs e)
        {
            if (IsValidCoord(LatDMSTextBox.Text))
            {
                double result;
                result = Conversions.String2DecDeg(LatDMSTextBox.Text);
                if (result != -1)
                {// decimal.Round(yourValue, 2, MidpointRounding.AwayFromZero);
                    LatDecTextBox.Text = decimal.Round(Convert.ToDecimal(result),6,MidpointRounding.AwayFromZero).ToString();
                    LatSCTTextBox.Text = Conversions.DecDeg2SCT(result, true);
                }
                UpdateStrings();
            }
        }

        private void LonDMSTextBox_Validated(object sender, EventArgs e)
        {
            if (IsValidCoord(LonDMSTextBox.Text))
            {
                double result;
                result = Conversions.String2DecDeg(LonDMSTextBox.Text);
                if (result != -1)
                {
                    LonDecTextBox.Text = decimal.Round(Convert.ToDecimal(result), 6, MidpointRounding.AwayFromZero).ToString();
                    LonSCTTextBox.Text = Conversions.DecDeg2SCT(result, false);
                }
                UpdateStrings();
            }
        }

        private void LatDecTextBox_Validated(object sender, EventArgs e)
        {
            {
                if (IsValidCoord(LatDecTextBox.Text))
                {
                    string result; double Lat = Convert.ToDouble(LatDecTextBox.Text);
                    result = Conversions.DecDeg2SCT(Lat, true);
                    if (result.Length > 0)
                    {
                        LatSCTTextBox.Text = Conversions.DecDeg2SCT(Lat, true);
                        LatDMSTextBox.Text = Conversions.DecDeg2DMS(Lat, true);
                    }
                    UpdateStrings(); 
                }
            }
        }
        private void LonDecTextBox_Validated(object sender, EventArgs e)
        {
            if (IsValidCoord(LonDecTextBox.Text))
            {
                string result; double Lon = Convert.ToDouble(LonDecTextBox.Text);
                result = Conversions.DecDeg2SCT(Lon, true);
                if (result.Length > 0)
                {
                    LonSCTTextBox.Text = Conversions.DecDeg2SCT(Lon, false);
                    LonDMSTextBox.Text = Conversions.DecDeg2DMS(Lon, false);
                }
                UpdateStrings();
            }
        }

        private void LatSCTTextBox_Validated(object sender, EventArgs e)
        {
            if (IsValidCoord(LatSCTTextBox.Text))
            {
                string Lat; double result = Convert.ToDouble(LatSCTTextBox.Text);
                Lat = Conversions.DecDeg2SCT(result, true);
                if (Lat.Length > 0)
                {
                    LatDecTextBox.Text = decimal.Round(Convert.ToDecimal(result), 6, MidpointRounding.AwayFromZero).ToString();
                    LatDMSTextBox.Text = Conversions.DecDeg2DMS(result, true);
                }
                UpdateStrings();
            }
        }

        private void LonSCTTextBox_Validated(object sender, EventArgs e)
        {
            if (IsValidCoord(LonSCTTextBox.Text))
            {
                string Lon; double result = Convert.ToDouble(LonSCTTextBox.Text);
                Lon = Conversions.DecDeg2SCT(result, false);
                if (Lon.Length > 0)
                {
                    LonDecTextBox.Text = decimal.Round(Convert.ToDecimal(result), 6, MidpointRounding.AwayFromZero).ToString();
                    LonDMSTextBox.Text = Conversions.DecDeg2DMS(result, false);
                }
                UpdateStrings();
            }
        }

        private void ParseSCTInsertButton_Click(object sender, EventArgs e)
        {
            string workText = SCTTextBox.Text;
            if (IsValidCoord(workText))
            {                
                int loc1 = SCTTextBox.Text.IndexOf(",");
                if (loc1 != -1)
                    workText = SCTTextBox.Text.Substring(0, loc1 - 1) + SCTTextBox.Text.Substring(loc1);
                loc1 = workText.IndexOf(" ");
                LatSCTTextBox.Text = workText.Substring(0, loc1).Trim();
                LonSCTTextBox.Text = workText.Substring(SCTTextBox.Text.IndexOf(" ")).Trim();
                double result = Conversions.String2DecDeg(LatSCTTextBox.Text);
                LatDecTextBox.Text = decimal.Round(Convert.ToDecimal(result), 6, MidpointRounding.AwayFromZero).ToString();
                LatDMSTextBox.Text = Conversions.DecDeg2DMS(result, true);
                result = Conversions.String2DecDeg(LonSCTTextBox.Text);
                LonDecTextBox.Text = decimal.Round(Convert.ToDecimal(result), 6, MidpointRounding.AwayFromZero).ToString();
                LonDMSTextBox.Text = Conversions.DecDeg2DMS(result, false);
                UpdateStrings();
                Clipboard.SetText(DECTextBox.Text);
                DECTextBox.SelectAll();
            }
        }

        private bool IsValidCoord(string coord)
        {
            bool result;
            result = coord.Length != 0;
            return result;
        }

        private void PasteButton_Click(object sender, EventArgs e)
        {
            SCTTextBox.Text = Clipboard.GetText();
        }

        private void LatDMSTextBox_DoubleClick(object sender, EventArgs e)
        {
            LatDMSTextBox.SelectAll();
            Clipboard.SetText(LatDecTextBox.Text);
        }

        private void LonDMSTextBox_DoubleClick(object sender, EventArgs e)
        {
            LonDMSTextBox.SelectAll();
            Clipboard.SetText(LonDecTextBox.Text);
        }

        private void LatDecTextBox_DoubleClick(object sender, EventArgs e)
        {
            LatDecTextBox.SelectAll();
            Clipboard.SetText(LatDecTextBox.Text);
        }

        private void LatSCTTextBox_DoubleClick(object sender, EventArgs e)
        {
            LatSCTTextBox.SelectAll();
            Clipboard.SetText(LatSCTTextBox.Text);
        }

        private void LonDecTextBox_DoubleClick(object sender, EventArgs e)
        {
            LonDecTextBox.SelectAll();
            Clipboard.SetText(LonDecTextBox.Text);
        }

        private void LonSCTTextBox_DoubleClick(object sender, EventArgs e)
        {
            LonSCTTextBox.SelectAll();
            Clipboard.SetText(LonSCTTextBox.Text);
        }

        private void DMSTextBox_DoubleClick(object sender, EventArgs e)
        {
            DMSTextBox.SelectAll();
            Clipboard.SetText(DMSTextBox.Text);
        }

        private void DECTextBox_DoubleClick(object sender, EventArgs e)
        {
            DECTextBox.SelectAll();
            Clipboard.SetText(DECTextBox.Text);
        }

        private void SCTTextBox_DoubleClick(object sender, EventArgs e)
        {
            SCTTextBox.SelectAll();
            Clipboard.SetText(SCTTextBox.Text);
        }
    }
}
