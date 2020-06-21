using System;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Globalization;
using Microsoft.CodeAnalysis.CSharp;

namespace SCTBuilder
{
    public partial class DMS_DecDeg : Form
    {
        string DMSLatitude = string.Empty;
        string DMSLongitude = string.Empty;
        double DecLatitude = 0;
        double DecLongitude = 0;
        string SCTLatitude = string.Empty;
        string SCTLongitude = string.Empty;
        public DMS_DecDeg()
        {
            InitializeComponent();
            NSComboBox.SelectedIndex = 0;
            EWComboBox.SelectedIndex = 0;
        }

        private void UpdateStrings()
        {
            if ((DMSLongitude.Length != 0) && (DMSLatitude.Length != 0))
                ToSingleLine(DMSLatitude, DMSLongitude, DMSTextBox);
            if ((DecLatitude != 0) && (DecLongitude != 0))
                ToSingleLine(DecLatitude.ToString(), DecLongitude.ToString(), DECTextBox);
            if ((SCTLatitude.Length != 0) && (SCTLongitude.Length != 0))
                ToSingleLine(SCTLatitude, SCTLongitude, SCTTextBox);
        }

        private void ToSingleLine(string Lat, string Lon, TextBox tb)
        {
            if ((Lat.Length != 0) && (Lon.Length != 0))
            {
                tb.Text = Lat + " " + Lon;
            }
        }


        private void LatSCTTextBox_Validated(object sender, EventArgs e)
        {
            if (LatSCTTextBox.TextLength != 0)
            {
                try
                {
                    SCTLatitude = LatSCTTextBox.Text;
                    DecLatitude = Conversions.String2DecDeg(SCTLatitude);
                    LatDecTextBox.Text = decimal.Round(Convert.ToDecimal(DecLatitude), 6, MidpointRounding.AwayFromZero).ToString();
                    DMSLatitude = Conversions.DecDeg2DMS(DecLatitude, true);
                    UpdateDMSboxes(true);
                    UpdateStrings();
                }
                catch
                {
                    SCTcommon.SendMessage("Invalid SCT format (Hover over textbox for format");
                    LatSCTTextBox.Focus();
                }
            }
        }

        private void LonSCTTextBox_Validated(object sender, EventArgs e)
        {
            if (LonSCTTextBox.TextLength != 0)
            {
                try
                {
                    SCTLongitude = LonSCTTextBox.Text;
                    DecLongitude = Conversions.String2DecDeg(SCTLongitude);
                    LonDecTextBox.Text = decimal.Round(Convert.ToDecimal(DecLongitude), 6, MidpointRounding.AwayFromZero).ToString();
                    DecLongitude = Convert.ToDouble(LonDecTextBox.Text);
                    DMSLongitude = Conversions.DecDeg2DMS(DecLongitude, false);
                    UpdateDMSboxes(false);
                    UpdateStrings();
                }
                catch
                {
                    SCTcommon.SendMessage("Invalid SCT format (Hover over textbox for format");
                    LonSCTTextBox.Focus();
                }
            }
        }

        private void UpdateDMSboxes(bool isLat)
        {
            string toParse; int Loc;
            if (isLat)
            {
                if (DMSLatitude.Length != 0)
                {
                    toParse = DMSLatitude;
                    NSComboBox.SelectedItem = Extensions.Right(toParse, 1);
                    toParse = toParse.Substring(0, toParse.Length - 1);
                    Loc = toParse.IndexOf(" ");
                    LatDMSDegTextBox.Text = toParse.Substring(0, Loc);
                    toParse = toParse.Substring(Loc + 1);
                    Loc = toParse.IndexOf(" ");
                    LatDMSMinTextBox.Text = toParse.Substring(0, Loc);
                    toParse = toParse.Substring(Loc).Trim();
                    LatDMSSecTextBox.Text = toParse;
                }
            }
            else
            {
                if (DMSLongitude.Length != 0)
                {
                    toParse = DMSLongitude;
                    EWComboBox.SelectedItem = Extensions.Right(toParse, 1);
                    toParse = toParse.Substring(0, toParse.Length - 1);
                    Loc = toParse.IndexOf(" ");
                    LonDMSDegTextBox.Text = toParse.Substring(0, Loc);
                    toParse = toParse.Substring(Loc + 1);
                    Loc = toParse.IndexOf(" ");
                    LonDMSMinTextBox.Text = toParse.Substring(0, Loc);
                    toParse = toParse.Substring(Loc + 1);
                    LonDMSSecTextBox.Text = toParse;
                }
            }
        }

        private void ParseSCTInsertButton_Click(object sender, EventArgs e)
        {
            ParseSCTTextBox();
        }

        private void ParseSCTTextBox()
        {
            string workText = SCTTextBox.Text;
            if (workText.Length != 0)
            {
                int loc1 = SCTTextBox.Text.IndexOf(",");
                if (loc1 != -1)
                    workText = SCTTextBox.Text.Substring(0, loc1 - 1) + SCTTextBox.Text.Substring(loc1);
                loc1 = workText.IndexOf(" ");
                SCTLatitude = LatSCTTextBox.Text = workText.Substring(0, loc1).Trim();
                SCTLongitude = LonSCTTextBox.Text = workText.Substring(SCTTextBox.Text.IndexOf(" ")).Trim();
                DecLatitude = Conversions.String2DecDeg(SCTLatitude);
                LatDecTextBox.Text = decimal.Round(Convert.ToDecimal(DecLatitude), 6, MidpointRounding.AwayFromZero).ToString();
                DMSLatitude = Conversions.DecDeg2DMS(DecLatitude, true);
                UpdateDMSboxes(true);
                DecLongitude = Conversions.String2DecDeg(LonSCTTextBox.Text);
                LonDecTextBox.Text = decimal.Round(Convert.ToDecimal(DecLongitude), 6, MidpointRounding.AwayFromZero).ToString();
                DMSLongitude = Conversions.DecDeg2DMS(DecLongitude, false);
                UpdateDMSboxes(false);
                UpdateStrings();
                Clipboard.SetText(DECTextBox.Text);
                DECTextBox.SelectAll();
            }
        }

        private void PasteButton_Click(object sender, EventArgs e)
        {
            SCTTextBox.Text = Clipboard.GetText();
        }

        private void LatDMSTextBox_DoubleClick(object sender, EventArgs e)
        {
            LatDMSDegTextBox.SelectAll();
            Clipboard.SetText(LatDMSDegTextBox.Text);
            CrossForm.Lat = Conversions.String2DecDeg(LatDMSDegTextBox.Text);
        }

        private void LonDMSTextBox_DoubleClick(object sender, EventArgs e)
        {
            if(DMSLongitude.Length != 0)
            Clipboard.SetText(DMSLongitude);
            CrossForm.Lon = Conversions.String2DecDeg(DMSLongitude);
        }

        private void LatDecTextBox_DoubleClick(object sender, EventArgs e)
        {
            LatDecTextBox.SelectAll();
            Clipboard.SetText(LatDecTextBox.Text);
            CrossForm.Lat = Convert.ToDouble(LatDecTextBox.Text);

        }
        private void LonDecTextBox_DoubleClick(object sender, EventArgs e)
        {
            LonDecTextBox.SelectAll();
            Clipboard.SetText(LonDecTextBox.Text);
            CrossForm.Lon = Convert.ToDouble(LonDecTextBox.Text);
        }

        private void LatSCTTextBox_DoubleClick(object sender, EventArgs e)
        {
            LatSCTTextBox.SelectAll();
            Clipboard.SetText(LatSCTTextBox.Text);
            CrossForm.Lat = Conversions.String2DecDeg(LatSCTTextBox.Text);
        }

        private void LonSCTTextBox_DoubleClick(object sender, EventArgs e)
        {
            LonSCTTextBox.SelectAll();
            Clipboard.SetText(LonSCTTextBox.Text);
            CrossForm.Lon = Conversions.String2DecDeg(LonSCTTextBox.Text);
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

        private void LatDMSDegTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
                char c = e.KeyChar;
                e.Handled = Extensions.Numcheck(c);
        }

        private void LatDMSMinTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            char c = e.KeyChar;
            e.Handled =Extensions.Numcheck(c);
        }

        private void LatDMSSecTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            char c = e.KeyChar;
            e.Handled = Extensions.DecimalControl(c, ref LatDMSDegTextBox, 4);
        }

        private void LonDMSDegTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            char c = e.KeyChar;
            e.Handled = Extensions.Numcheck(c);
        }

        private void LonDMSMinTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            char c = e.KeyChar;
            e.Handled = Extensions.Numcheck(c);
        }

        private void LonDMSSecTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            char c = e.KeyChar;
            e.Handled = Extensions.DecimalControl(c, ref LonDMSDegTextBox, 4);
        }

        private void LatDecTextBox_Validated(object sender, EventArgs e)
        {
            double Lat;
            if (LatDecTextBox.TextLength != 0)
            {
                try
                {
                    Lat = Convert.ToDouble(LatDecTextBox.Text);
                    SCTLatitude = Conversions.DecDeg2SCT(Lat, true);
                    LatSCTTextBox.Text = SCTLatitude;
                    DMSLatitude = Conversions.DecDeg2DMS(Lat, true);
                    DecLatitude = Lat;
                    UpdateDMSboxes(true);
                }
                catch
                {
                    SCTcommon.SendMessage("Invalid Latitude - Must be a number between -90 and 90");
                    LatDecTextBox.Focus();
                }
            }
        }

        private void LonDecTextBox_Validated(object sender, EventArgs e)
        {
            double Lon;
            if (LonDecTextBox.TextLength != 0)
            {
                try
                {
                    Lon = Convert.ToDouble(LonDecTextBox.Text);
                    SCTLongitude = Conversions.DecDeg2SCT(Lon, false);
                    LonSCTTextBox.Text = SCTLongitude;
                    DMSLongitude = Conversions.DecDeg2DMS(Lon, false);
                    DecLongitude = Lon;
                    LonDecTextBox.Text = DecLongitude.ToString();
                    UpdateDMSboxes(false);
                }
                catch
                {
                    SCTcommon.SendMessage("Invalid Latitude - Must be a number between -90 and 90");
                    LonDecTextBox.Focus();
                }
            }
        }

        private void ValidateDMSLat()
        {
            if ((NSComboBox.SelectedIndex != -1) && (LatDMSDegTextBox.TextLength != 0)
                && (LatDMSMinTextBox.TextLength != 0) && (LatDMSSecTextBox.TextLength != 0))
            {
                DMSLatitude = NSComboBox.SelectedItem + LatDMSDegTextBox.Text + "-" +
                    LatDMSMinTextBox.Text + "-" + LatDMSSecTextBox.Text;
                DecLatitude = Conversions.String2DecDeg(DMSLatitude, "-");
                LatDecTextBox.Text = DecLatitude.ToString();
                SCTLatitude = Conversions.DecDeg2SCT(DecLatitude, true);
                LatSCTTextBox.Text = SCTLatitude;
            }
            else DMSLatitude = string.Empty;
            UpdateStrings();
        }

        private void LatDMSDegTextBox_Validated(object sender, EventArgs e)
        {
            if (LatDMSDegTextBox.TextLength != 0)
            {
                int test = Convert.ToInt32(LatDMSDegTextBox.Text);
                if (test == 90)
                {
                    LatDMSMinTextBox.Text = "0"; LatDMSSecTextBox.Text = "0"; 
                    ValidateDMSLat();
                }
                else
                {
                    if ((test < 90) && (test >= 0))
                        ValidateDMSLat();
                    else
                    {
                        SCTcommon.SendMessage("Value must be 0 to 90");
                        LatDMSDegTextBox.Focus();
                    }
                }
            }
        }

        private void NSComboBox_Validated(object sender, EventArgs e)
        {
            if ((NSComboBox.Text == "N") || (NSComboBox.Text == "S"))
                ValidateDMSLat();
            else NSComboBox.SelectedIndex = 0;
        }

        private void LatDMSMinTextBox_Validated(object sender, EventArgs e)
        {
            if (LatDMSMinTextBox.TextLength != 0)
            {
                bool atMax = Convert.ToInt32(LatDMSDegTextBox.Text) == 90;
                int test = Convert.ToInt32(LatDMSMinTextBox.Text);
                if (!atMax)
                {
                    if ((test > 59) || (test < 0))
                    {
                        SCTcommon.SendMessage("Value must be 0 to 59");
                        LatDMSMinTextBox.Focus();
                    }
                    else
                        ValidateDMSLat();
                }
                else
                {
                    if (test != 0)
                    {
                        SCTcommon.SendMessage("Cannot exceed 90 degrees.");
                        LatDMSMinTextBox.Text = "0";
                        LatDMSSecTextBox.Focus();
                    }
                    else
                        ValidateDMSLat();
                }
            }
        }

        private void LatDMSSecTextBox_Validated(object sender, EventArgs e)
        {
            if (LatDMSSecTextBox.TextLength != 0)
            {
                bool atMax = Convert.ToInt32(LatDMSDegTextBox.Text) == 90;
                double test = Convert.ToDouble(LatDMSSecTextBox.Text);
                if (!atMax)
                {
                    if ((test < 60f) && (test >= 0f))
                        ValidateDMSLat();
                    else
                    {
                        SCTcommon.SendMessage("Value must be 0 to less than 60");
                        LatDMSSecTextBox.Focus();
                    }
                }
                else
                {
                    if (test != 0)
                    {
                        SCTcommon.SendMessage("Cannot exceed 90 degrees.");
                        LatDMSSecTextBox.Text = "0";
                        LatDMSSecTextBox.Focus();
                    }
                    else
                        ValidateDMSLat();
                }
            }
        }

        private void ValidateDMSLon()
        {
            if ((EWComboBox.SelectedIndex != -1) && (LonDMSDegTextBox.TextLength != 0)
                && (LonDMSMinTextBox.TextLength != 0) && (LonDMSSecTextBox.TextLength != 0))
            {
                DMSLongitude = EWComboBox.SelectedItem + LonDMSDegTextBox.Text + "-" +
                    LonDMSMinTextBox.Text + "-" + LonDMSSecTextBox.Text;
                DecLongitude = Conversions.String2DecDeg(DMSLongitude, "-");
                LonDecTextBox.Text = DecLongitude.ToString();
                SCTLongitude = Conversions.DecDeg2SCT(DecLongitude, true);
                LonSCTTextBox.Text = SCTLongitude;
            }
            else DMSLongitude = string.Empty;
            UpdateStrings();
        }

        private void EWComboBox_Validated(object sender, EventArgs e)
        {
            if ((EWComboBox.Text == "E") || (EWComboBox.Text == "W"))
                ValidateDMSLon();
            else EWComboBox.SelectedIndex = 0;
        }

        private void LonDMSDegTextBox_Validated(object sender, EventArgs e)
        {
            if (LonDMSDegTextBox.TextLength != 0)
            {
                int test = Convert.ToInt32(LonDMSDegTextBox.Text);
                if (test == 180)
                {
                    LonDMSMinTextBox.Text = "0"; LonDMSSecTextBox.Text = "0";
                    ValidateDMSLon();
                }
                else
                {
                    if ((test < 180) && (test >= 0))
                        ValidateDMSLon();
                    else
                    {
                        SCTcommon.SendMessage("Value must be 0 to 180");
                        LonDMSDegTextBox.Focus();
                    }
                }
            }
        }

        private void LonDMSMinTextBox_Validated(object sender, EventArgs e)
        {
            if (LonDMSDegTextBox.TextLength != 0)
            {
                bool atMax = Convert.ToInt32(LonDMSDegTextBox.Text) == 180;
                int test = Convert.ToInt32(LonDMSMinTextBox.Text);
                if (!atMax)
                {
                    if ((test > 59) || (test < 0))
                    {
                        SCTcommon.SendMessage("Value must be 0 to 59");
                        LonDMSMinTextBox.Focus();
                    }
                    else
                        ValidateDMSLon();
                }
                else
                {
                    if (test != 0)
                    {
                        SCTcommon.SendMessage("Cannot exceed 180 degrees.");
                        LonDMSMinTextBox.Text = "0";
                        LonDMSMinTextBox.Focus();
                    }
                    else
                        ValidateDMSLon();
                }
            }
        }

        private void LonDMSSecTextBox_Validated(object sender, EventArgs e)
        {
            if (LonDMSSecTextBox.TextLength != 0)
                if (LonDMSSecTextBox.TextLength != 0)
                {
                    bool atMax = Convert.ToInt32(LonDMSDegTextBox.Text) == 180;
                    double test = Convert.ToDouble(LonDMSSecTextBox.Text);
                    if (!atMax)
                    {
                        if ((test < 60f) && (test >= 0f))
                            ValidateDMSLon();
                        else
                        {
                            SCTcommon.SendMessage("Value must be 0 to less than 60");
                            LonDMSSecTextBox.Focus();
                        }
                    }
                    else
                    {
                        if (test != 0)
                        {
                            SCTcommon.SendMessage("Cannot exceed 180 degrees.");
                            LonDMSSecTextBox.Text = "0";
                            LonDMSSecTextBox.Focus();
                        }
                        else
                            ValidateDMSLon();
                    }
                }
        }

        private void LatDecTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            char c = e.KeyChar;
            if (c == '-')
            {
                if (LatDecTextBox.TextLength > 0)
                    e.Handled = true;
            }
            else
            {
                e.Handled = Extensions.DecimalControl(c, ref LatDecTextBox, 4);
            }
        }

        private void LonDecTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            char c = e.KeyChar;
            if (c == '-')
            {
                if (LonDecTextBox.TextLength > 0)
                    e.Handled = true;
            }
            else
            {
                e.Handled = Extensions.DecimalControl(c, ref LonDecTextBox, 4);
            }
        }
    }
}

