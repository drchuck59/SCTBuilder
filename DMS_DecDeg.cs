using System;
using System.Windows.Forms;

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
                tb.Text = Lat + ", " + Lon;
            }
        }

        private void LatDMSTextBox_Validated(object sender, EventArgs e)
        {
            if (LatDMSTextBox.TextLength != 0)
            {
                double result;
                result = Conversions.String2DecDeg(LatDMSTextBox.Text);
                if (result != -1)
                {
                    LatDecTextBox.Text = result.ToString();
                    LatSCTTextBox.Text = Conversions.DecDeg2SCT(result, true);
                }
                UpdateStrings();
            }
        }

        private void LonDMSTextBox_Validated(object sender, EventArgs e)
        {
            if (LonDMSTextBox.TextLength != 0)
            {
                double result;
                result = Conversions.String2DecDeg(LonDMSTextBox.Text);
                if (result != -1)
                {
                    LonDecTextBox.Text = result.ToString();
                    LonSCTTextBox.Text = Conversions.DecDeg2SCT(result, false);
                }
                UpdateStrings();
            }
        }

        private void LatDecTextBox_Validated(object sender, EventArgs e)
        {
            {
                if (LatDecTextBox.TextLength != 0)
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
            if (LonDecTextBox.TextLength != 0)
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
            if (LatSCTTextBox.TextLength != 0)
            {
                string result; double Lat = Convert.ToDouble(LatSCTTextBox.Text);
                result = Conversions.DecDeg2SCT(Lat, true);
                if (result.Length > 0)
                {
                    LatDecTextBox.Text = Lat.ToString();
                    LatDMSTextBox.Text = Conversions.DecDeg2DMS(Lat, true);
                }
                UpdateStrings();
            }
        }

        private void LonSCTTextBox_Validated(object sender, EventArgs e)
        {
            if (LonSCTTextBox.TextLength != 0)
            {
                string result; double Lat = Convert.ToDouble(LonSCTTextBox.Text);
                result = Conversions.DecDeg2SCT(Lat, false);
                if (result.Length > 0)
                {
                    LonDecTextBox.Text = Lat.ToString();
                    LonDMSTextBox.Text = Conversions.DecDeg2DMS(Lat, false);
                }
                UpdateStrings();
            }
        }

        private void DMSTextBox_DoubleClick(object sender, EventArgs e)
        {
            Clipboard.SetText(DMSTextBox.Text);
        }

        private void DECTextBox_DoubleClick(object sender, EventArgs e)
        {
            Clipboard.SetText(DECTextBox.Text);
        }

        private void SCTTextBox_DoubleClick(object sender, EventArgs e)
        {
            Clipboard.SetText(SCTTextBox.Text);
        }
    }
}
