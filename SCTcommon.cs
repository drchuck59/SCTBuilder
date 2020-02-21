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
        public static double DefaultLatitude(string Arpt)
        {
            double result;
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

        public static double DefaultLongitude(string Arpt)
        {
            double result;
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

        public static double DefaultMagVar(string Arpt)
        {
            double result;
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

        public static double AdjustedLatLong(string LL, string nud, string LLedge)
        {
            double result = Convert.ToSingle(LL);
            double offset = Convert.ToSingle(nud);
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

        public static double String2DecDeg(string DMS, string Delim = "")
        /// <summary>
        /// Returns a Decimal degrees value from the OpenAIG formatted string
        /// OpenAIG is [#]##:##:##? 
        /// FAA is [#]##-##-##.##?
        /// VRC is ?###.##.##.###  (Leading zero for latitudes)
        ///     where ? is the quadrant
        /// </summary>
        // If possible, find the delimiter
        {
            double result = -1; double DD; double MM; double SS; string quadrant;
            string tempDMS; double factor; string newDelim;
            if (DMS.Length > 5)
            {
                // Check that caller didn't "forget" the delim (or may not know it)
                newDelim = FindDelimiter(DMS);
                if (Delim.Length != 0)
                    if (newDelim != Delim) return result;       // Result is still -1

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
                // Now that we know the real Delim, use it to find the values
                if (newDelim.Length == 0)
                // Truely no delimiter
                {
                    if (quadrant == "W" || quadrant == "E")
                    {
                        // Longitudinal conversion
                        DD = double.Parse(tempDMS.Substring(0, 3));
                        MM = double.Parse(tempDMS.Substring(3, 2));
                        SS = double.Parse(tempDMS.Substring(5, 2));
                        if (DMS.Length > 7)                         // Add faction of seconds if they exist
                        {
                            factor = (double)Math.Pow(10f, Convert.ToSingle(tempDMS.Length - 7));
                            SS += double.Parse(tempDMS.Substring(6, 2)) / factor;
                        }
                    }
                    else
                    {
                        // Latitude conversion
                        DD = double.Parse(tempDMS.Substring(0, 2));
                        MM = double.Parse(tempDMS.Substring(2, 2));
                        SS = double.Parse(tempDMS.Substring(4, 2));
                        if (DMS.Length > 6)                         // Add fraction of seconds if they exist
                        {
                            factor = (double)Math.Pow(10f, Convert.ToSingle(tempDMS.Length - 6));
                            SS += double.Parse(tempDMS.Substring(5, 2)) / factor;
                        }
                    }
                    result = LatLongCalc.DMS2DecDeg(DD, MM, SS, quadrant);
                }
                else
                // Has a delimiter
                {
                    int loc1 = tempDMS.IndexOf(newDelim, 0, tempDMS.Length, StringComparison.CurrentCulture); // end of DD
                    int loc2 = tempDMS.IndexOf(newDelim, loc1 + 1, tempDMS.Length - loc1 - 1, StringComparison.CurrentCulture);   // End of MM
                    DD = double.Parse(tempDMS.Substring(0, loc1));
                    MM = double.Parse(tempDMS.Substring(loc1 + 1, loc2 - loc1 - 1));
                    SS = double.Parse(tempDMS.Substring(loc2 + 1, tempDMS.Length - loc2 - 1));
                    result = LatLongCalc.DMS2DecDeg(DD, MM, SS, quadrant);
                }
                // Last step: Ensure the result value falls within the range of the latitude or longitude
                switch (quadrant)
                {
                    case "N":
                    case "S":
                        if (Math.Abs(result) > 90) result = -1;
                        break;
                    case "E":
                    case "W":
                        if (Math.Abs(result) > 180) result = -1;
                        break;
                }
            }
            return result;
        }

        private static string FindDelimiter(string DMS)
        {
            // The delimiter must occur within 4 characters (?###^)
            string result = string.Empty;
            if ( (DMS.IndexOf('.') > -1) && (DMS.IndexOf('.') < 5) ) result = ".";
            if ((DMS.IndexOf(':') > -1) && (DMS.IndexOf(':') < 5)) result = ":";
            if ((DMS.IndexOf('-') > -1) && (DMS.IndexOf('-') < 5)) result = "-";
            if ((DMS.IndexOf(' ') > -1) && (DMS.IndexOf(' ') < 5)) result = " ";
            return result;
        }

        public static string DecDeg2SCT(double DecDeg, bool IsLatitude)
        {
            string quadrant;
            string result;     // An empty string indicates an error occurred
            double tempDecDeg;
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
            double tmpDecDeg = (tempDecDeg - DD) * 60;
            int MM = (int)Math.Floor(tmpDecDeg);
            string strMM = MM.ToString("00");
            double SS = (tmpDecDeg - MM) * 60;
            string strSS = SS.ToString("00.000");
            result = quadrant + strDD + "." + strMM + "." + strSS;
            return result;
        }
        public static double Seconds2DecDeg(string seconds)
        {
            if (seconds.Length == 0) return -1;
            try
            {
                double DD = Convert.ToSingle(Extensions.Left(seconds, seconds.Length - 1));
                DD /= 3600f;
                string s = Extensions.Right(seconds, 1);
                if ("SW".IndexOf(s) != -1) DD *= -1;    // Invert if southern latitude or west longitude
                return DD;
            }
            catch { return -1f; }
        }
        public static double MagVar2DecMag(string Mag)
        {
            if (Mag.Trim().Length > 0)
            {
                return Convert.ToSingle(Mag.Substring(0, Mag.Length - 1).Trim());
            }
            else return 0f;
        }
        public static void BuildSUAPolygon(string Polygon, string ID)
        {
            // For a given SUA [ID], use the imported local SUA file to create the SID-Star
            string tempPoly = Polygon; double Latitude; double Longitude;
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
        public static void BorderCoord(string Polygon, out double North, out double South, out double East, out double West)
        {
            // Returns the quadrant-most coordinate for a given polygon
            string tempPoly = Polygon; int loc1; string temp1;
            string Lat1; string Long1; double Latitude; double Longitude;
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
            return double.TryParse(text, out double test);
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

    public static class SCTstrings
    {
        public static string VORNDBout(string[] strOut)
        {
            string result = strOut[0] + " " + strOut[1] + " " +
                        strOut[2] + " " + strOut[3] + " ;" + strOut[4];
            return result;
        }
        public static string APTout(string[] strOut)
        {
            string result = strOut[0] + " " + strOut[1] + " " + strOut[2] + " " +
                            strOut[3] + " " + " ;" + strOut[4] + strOut[5] + strOut[6];
            return result;
        }
        public static string FIXout(string[] strOut)
        {
            string result = strOut[0] + " " + strOut[2] + " " + strOut[3] + " ;" + strOut[4];
            return result;
        }
        public static string RWYout(string[] strOut)
        {
            string result = strOut[0] + " " + strOut[1] + " " + strOut[2] + " " + strOut[3] + " "
                        + strOut[4] + " " + strOut[5] + " " + strOut[6] + " " + strOut[7] + Environment.NewLine;
            return result;
        }
        public static string AWYout(string Awy, string StartLat, 
            string StartLong, string EndLat, string EndLong,
            string NavAid0, string NavAid1, bool UseFix = false)
        {
            string result;
            string str = new string(' ', 27 - Awy.Length);
            if (!UseFix)
                result = Awy + str + StartLat + " " + StartLong + " " +
                    EndLat + " " + EndLong + "; " + NavAid0 + " " + NavAid1;
            else
                result = Awy + str + NavAid0 + " " + NavAid0 + " " +
                    NavAid1 + " " + NavAid1;
            return result;
        }
        public static string SSDout(string StartLat,
            string StartLong, string EndLat, string EndLong,
            string NavAid0 = "", string NavAid1 = "", bool UseFix = false)
            // Lat/longs are assumed to be in SCT format!!
        {
            string str = new string(' ', 27);
            string result;
            if (!UseFix)
                result = str + StartLat + " " + StartLong + " " + EndLat + " " + EndLong +
                    "; " + NavAid0 + NavAid1;
            else
                result = str + NavAid0 + " " + NavAid0 + " " + NavAid1 + " " + NavAid1;
            return result;
        }
        public static string BoundaryOut(string prefix, string StartLat,
            string StartLong, string EndLat, string EndLong,
            string suffix = "")
        {
            string result = prefix + " " + StartLat + " " + StartLong + " " +
                                        EndLat + " " + EndLong;
            if (suffix.Length != 0) result += "; " + suffix;
            return result;
        }
        public static string LabelOut(string label, string Lat, string Long, string Color, string Comment = "")
        {
            string strText = "\"" + label.Trim() + "\"";
            string result = strText + " " + Lat + " " + Long + " " + Color + Comment;
            return result;
        }
    }
}
