using System;
using System.Data;
using System.Linq;
using System.IO;
using System.Threading.Tasks;

namespace SCTBuilder
{
    class SCTcommon
    {
        public static void DefineColorConstants(DataTable dT)
        // Color name, Decimal color code
        {
            if (dT.Rows.Count != 0) dT.Clear();
            dT.Rows.Add(new object[] { "Red ", 255 });
            dT.Rows.Add(new object[] { "Pink ", 16764108 });
            dT.Rows.Add(new object[] { "Magenta ", 12648645 });
            dT.Rows.Add(new object[] { "LtGray ", 10066329 });
            dT.Rows.Add(new object[] { "MedGray ", 8421504 });
            dT.Rows.Add(new object[] { "DkGray ", 8421504 });
            dT.Rows.Add(new object[] { "BlueGray ", 6726856 });
            dT.Rows.Add(new object[] { "Blue ", 16711680 });
            dT.Rows.Add(new object[] { "Yellow ", 65535 });
            dT.Rows.Add(new object[] { "Teal ", 8421376 });
            dT.Rows.Add(new object[] { "Aqua ", 50630 });
            dT.Rows.Add(new object[] { "White ", 16777215 });
            dT.Rows.Add(new object[] { "LowBndry ", 13408665 });
            dT.Rows.Add(new object[] { "HighBndry ", 7829367 });
            dT.Rows.Add(new object[] { "c_coast ", 2631720 });
            dT.Rows.Add(new object[] { "Runway ", 14737632 });
            dT.Rows.Add(new object[] { "Text ", 14474460 });
            dT.Rows.Add(new object[] { "Taxiway ", 10772514 });
            dT.Rows.Add(new object[] { "Ramp ", 673153 });
            dT.Rows.Add(new object[] { "Runway ", 14737632 });
            dT.Rows.Add(new object[] { "Building ", 5196883 });
        }
    }
  
    public class Conversions
    // Convert a variety of strings 
    {
        public static float DefaultLatitude(string Arpt)
        {
            float result;
            DataView APTView = new DataView(Form1.APT)
            {
                RowFilter = "[FacilityID] = '" + Arpt + "'"
            };
            if (APTView.Count != 0)
                result = Convert.ToSingle(APTView[0].Row["Latitude"]);
            else
                result = -1f;
            APTView.Dispose();
            return result;
        }

        public static float DefaultLongitude(string Arpt)
        {
            float result;
            DataView APTView = new DataView(Form1.APT)
            {
                RowFilter = "[FacilityID] = '" + Arpt + "'"
            };
            if (APTView.Count != 0)
                result = Convert.ToSingle(APTView[0].Row["Longitude"]);
            else
                result = -1f;
            APTView.Dispose();
            return result;
        }

        public static float DefaultMagVar(string Arpt)
        {
            float result;
            DataView APTView = new DataView(Form1.APT)
            {
                RowFilter = "[FacilityID] = '" + Arpt + "'"
            };
            if (APTView.Count != 0)
                result = Convert.ToSingle(APTView[0].Row["MagVar"]);
            else
                result = 0f;
            APTView.Dispose();
            return result;
        }

        public static float AdjustedLatLong(string LL, string nud, string LLedge)
        {
            float result = Convert.ToSingle(LL);
            float offset = Convert.ToSingle(nud);
            switch (LLedge)
            {
                case "N":
                    result += offset / InfoSection.NMperDegreeLongitude;
                    break;
                case "E":
                    result += offset / InfoSection.NMperDegreeLatitude;
                    break;
                case "S":
                    result -= offset / InfoSection.NMperDegreeLongitude;
                    break;
                case "W":
                    result -= offset / InfoSection.NMperDegreeLatitude;
                    break;
            }
            return result;
        }

        public static string ICOA(string Arpt)
        {
            string result;
            if ((Arpt.Length == 3) && !Arpt.Any(char.IsDigit))
                result = "K" + Arpt;
            else result = Arpt;
            return result;
        }

        public static string RevICOA(string Arpt)
        {
            string result;
            if ((Arpt.Length == 4) && !Arpt.Any(char.IsDigit))
                result = Arpt.Substring(1, Arpt.Length - 1);
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
                    tempDMS = DMS.Substring(0, DMS.Length - 1).Trim();              // Strip off quadrant
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
        public static string DecDeg2SCT(float DecDeg, bool IsLatitude)
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
                return Convert.ToSingle(Mag.Substring(0, Mag.Length - 1).Trim());
            }
            else return 0f;
        }
        public static void BuildPolygon(string Polygon, string ID)
        {
            string tempPoly = Polygon; float Latitude; float Longitude;
            DataTable dtPoly = Form1.Polygon; int loc1; string Lat1; string Long1;
            DataView dvPoly = new DataView(dtPoly); int Counter = 0; string temp1;
            while (tempPoly.Length != 0)
            {
                Counter++;
                loc1 = tempPoly.IndexOf(", ");
                if (loc1 != -1)
                {
                    temp1 = tempPoly.Substring(0, loc1).Trim();
                    Long1 = temp1.Substring(0, temp1.IndexOf(" "));
                    Lat1 = temp1.Substring(temp1.IndexOf(" ") + 1);
                    Latitude = Convert.ToSingle(Lat1);
                    Longitude = Convert.ToSingle(Long1);
                    tempPoly = tempPoly.Substring(loc1 + 2);
                    // Console.WriteLine(ID + " " + Latitude + " " + Longitude);
                }
                else
                {
                    temp1 = tempPoly;               // Last coordinate in string
                    Long1 = temp1.Substring(0, temp1.IndexOf(" "));
                    Lat1 = temp1.Substring(temp1.IndexOf(" ") + 1);
                    Latitude = Convert.ToSingle(Lat1);
                    Longitude = Convert.ToSingle(Long1);
                    tempPoly = string.Empty;
                    // Console.WriteLine(ID + " " + Latitude + " " + Longitude);
                }
                DataRowView newrow = dvPoly.AddNew();
                newrow["SUA_FK"] = ID;
                newrow["Sequence"] = Counter;
                newrow["Latitude"] = Latitude;
                newrow["Longitude"] = Longitude;
                newrow.EndEdit();
            }
            // Console.WriteLine("Added " + Counter + " rows to table (now has " + dtPoly.Rows.Count + " rows).");
        }
        public static void BorderCoord(string Polygon, out float North, out float South, out float East, out float West)
        {
            // Returns the quadrant-most coordinate for a given polygon
            string tempPoly = Polygon; int loc1; string temp1;
            string Lat1; string Long1; float Latitude; float Longitude;
            North = -1; South = 1; East = -1f; West = 1f;
            while (tempPoly.Length != 0)
            {
                loc1 = tempPoly.IndexOf(", ");
                if (loc1 != -1)
                {
                    temp1 = tempPoly.Substring(0, loc1).Trim();
                    Long1 = temp1.Substring(0, temp1.IndexOf(" "));
                    Lat1 = temp1.Substring(temp1.IndexOf(" ") + 1);
                    Latitude = Convert.ToSingle(Lat1);
                    Longitude = Convert.ToSingle(Long1);
                    tempPoly = tempPoly.Substring(loc1 + 2);
                    // Console.WriteLine(ID + " " + Latitude + " " + Longitude);
                }
                else
                {
                    temp1 = tempPoly;               // Last coordinate in string
                    Long1 = temp1.Substring(0, temp1.IndexOf(" "));
                    Lat1 = temp1.Substring(temp1.IndexOf(" ") + 1);
                    Latitude = Convert.ToSingle(Lat1);
                    Longitude = Convert.ToSingle(Long1);
                    tempPoly = string.Empty;
                    // Console.WriteLine(ID + " " + Latitude + " " + Longitude);
                }
                North = Math.Max(North, Latitude);
                South = Math.Min(South, Latitude);
                East = Math.Max(East, Longitude);
                West = Math.Min(West, Longitude);
            }
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
