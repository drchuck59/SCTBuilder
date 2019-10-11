using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCTBuilder
{
    class SCTcommon
    {
        public static void DefineColorConstants(DataTable dT)
        /// c# does not use #Define statements; must create a static table!
        /// Sadly, there is no way to just load this data
        {
            if (dT.Rows.Count != 0) dT.Clear();
            dT.Rows.Add(new object[] { "Black", 0 });
            dT.Rows.Add(new object[] { "Maroon ", 128 });
            dT.Rows.Add(new object[] { "Green ", 32768 });
            dT.Rows.Add(new object[] { "Olive ", 32896 });
            dT.Rows.Add(new object[] { "Navy ", 8388608 });
            dT.Rows.Add(new object[] { "Purple ", 8388736 });
            dT.Rows.Add(new object[] { "Teal ", 8421376 });
            dT.Rows.Add(new object[] { "Grey ", 8421504 });
            dT.Rows.Add(new object[] { "Silver ", 12632256 });
            dT.Rows.Add(new object[] { "Red ", 255 });
            dT.Rows.Add(new object[] { "Lime ", 65280 });
            dT.Rows.Add(new object[] { "Blue ", 16711680 });
            dT.Rows.Add(new object[] { "Fuchsia ", 16711935 });
            dT.Rows.Add(new object[] { "Aqua ", 16776960 });
            dT.Rows.Add(new object[] { "White ", 16777215 });
        }
    }
    public class Conversions
    // Convert a variety of strings 
    {
        public static string ICOA(string Arpt)
        {
            string result;
            if ((Arpt.Length == 3) & !Arpt.Any(char.IsDigit))
                result = "K" + Arpt;
            else result = Arpt;
            return result;
        }

        public float String2DecDeg(string DMS, string Delim = "")
        /// <summary>
        /// Returns a Decimal degrees value from the OpenAIG formatted string
        /// OpenAIG is [#]##:##:##? 
        /// FAA is [#]##-##-##.##?
        ///     where ? is the quadrant
        /// </summary>
        {
            float result = -1; float DD; float MM; float SS; string quadrant;
            string tempDMS; float factor;
            if (DMS.Length > 5)
            {
                // Sometimes the quadrant is in the front and other times in the back!
                if (DMS.Substring(0, 1).IsNumeric())
                {
                    quadrant = Extensions.Right(DMS, 1);
                    tempDMS = DMS.Substring(0, DMS.Length - 1);              // Strip off quadrant
                }
                else
                {
                    quadrant = DMS.Substring(0, 1);
                    tempDMS = DMS.Substring(1, DMS.Length - 1);
                }
                if (Delim != "")
                {
                    int loc1 = tempDMS.IndexOf(Delim, 0, tempDMS.Length, StringComparison.CurrentCulture); // end of DD
                    int loc2 = tempDMS.IndexOf(Delim, loc1 + 1, tempDMS.Length - loc1 - 1, StringComparison.CurrentCulture);   // End of MM
                    DD = float.Parse(tempDMS.Substring(0, loc1));
                    MM = float.Parse(tempDMS.Substring(loc1 + 1, loc2 - loc1 - 1));
                    SS = float.Parse(tempDMS.Substring(loc2 + 1, tempDMS.Length - loc2 - 1));
                    result = LatLongCalc.DMS2DecDeg(DD, MM, SS, quadrant);
                }
                else
                {
                    if (quadrant == "W" || quadrant == "E")
                    {
                        DD = float.Parse(tempDMS.Substring(0, 3));
                        MM = float.Parse(tempDMS.Substring(3, 2));
                        SS = float.Parse(tempDMS.Substring(5, 2));
                        if (DMS.Length > 7)                         // Add faction of seconds if they exist
                        {
                            factor = (float)Math.Pow(10f, Convert.ToSingle(tempDMS.Length - 7));
                            SS += float.Parse(tempDMS.Substring(6, 2)) / factor;
                        }
                    }
                    else
                    {
                        DD = float.Parse(tempDMS.Substring(0, 2));
                        MM = float.Parse(tempDMS.Substring(2, 2));
                        SS = float.Parse(tempDMS.Substring(4, 2));
                        if (DMS.Length > 6)                         // Add faction of seconds if they exist
                        {
                            factor = (float)Math.Pow(10f, Convert.ToSingle(tempDMS.Length - 6));
                            SS += float.Parse(tempDMS.Substring(5, 2)) / factor;
                        }
                    }
                    result = LatLongCalc.DMS2DecDeg(DD, MM, SS, quadrant);
                }
            }
            return result;
        }
        public static string DecDeg2SCT(float DecDeg, Boolean IsLatitude = false)
        {
            string quadrant;
            string result;     // An empty string indicates an error occurred
            float tempDecDeg;
            if (DecDeg < 0)     // Tests for S or W quadrants
            {
                if (IsLatitude)
                {
                    quadrant = "S";
                }
                else
                {
                    quadrant = "W";
                }
            }
            else
            {
                if (IsLatitude)
                {
                    quadrant = "N";
                }
                else
                {
                    quadrant = "E";
                }
            }
            tempDecDeg = Math.Abs(DecDeg);
            int DD = (int)Math.Floor(tempDecDeg);     // Need integer value WITHOUT rounding
            string strDD = DD.ToString("000");          // This cannot be done in one step
            float tmpDecDeg = (tempDecDeg - DD) * 60;
            int MM = (int)Math.Floor(tmpDecDeg);
            string strMM = MM.ToString("00");
            float SS = (tmpDecDeg - MM) * 60;
            string strSS = SS.ToString("00.000");
            result = quadrant + strDD + "." + strMM + "." + strSS;
            return result;
        }
        public static float SS2DD(string seconds)
        {
            if (seconds.Length == 0) return -1;
            try
            {
                float DD = Convert.ToSingle(Extensions.Left(seconds, seconds.Length - 1));
                DD /= 3600f;
                string s = Extensions.Right(seconds, 1);
                if ("SW".IndexOf(s) != -1) DD *= -1;
                return DD;
            }
            catch { return -1f; }
        }
        public static float MagVar(string Mag)
        {
            if (Mag.Trim().Length > 0)
            {
                float tempFloat = Convert.ToSingle(Mag.Substring(0, Mag.Length - 1).Trim());
                if (Extensions.Right(Mag, 1) == "W") 
                    tempFloat *= -1;
                return tempFloat;
            }
            else return 0f;
        }
    }
    public static class Extensions
    {
        /// <summary>
        /// Get substring of specified number of characters on the right.
        /// </summary>
        public static string Right(this string value, int length)
        {
            if (string.IsNullOrEmpty(value)) return string.Empty;

            return value.Length <= length ? value : value.Substring(value.Length - length);
        }
        public static bool IsNumeric(this string text)
        {
            return float.TryParse(text, out float test);
        }
        public static string Left(this string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value)) return value;
            maxLength = Math.Abs(maxLength);

            return (value.Length <= maxLength
                   ? value
                   : value.Substring(0, maxLength)
                   );
        }

    }
}
