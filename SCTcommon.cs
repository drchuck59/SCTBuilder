using System;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.IO;
using System.Collections.Generic;

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

        public static string GetFullPathname(string DataFolder, string Filename)
        /// <summary>
        /// Checks that the data exists, returns an error if not found
        /// </summary>
        {
            try
            {
                var file = Directory.GetFiles(DataFolder, Filename, SearchOption.AllDirectories).FirstOrDefault();
                if (file == null) return "ERROR";
                return file.ToString();
            }
            catch (FileNotFoundException)
            {
                return "FILE ERROR";
            }
            catch (Exception)
            {
                return "ERROR";
            }
        }

        public static string GetFolderPath(string selectedPath, string dialogTitle)
        {
            FolderBrowserDialog fBD = new FolderBrowserDialog();
            string result = string.Empty;
            // Set default folder to start
            fBD.SelectedPath = selectedPath;
            fBD.Description = dialogTitle;
            if (selectedPath.Length != 0) fBD.SelectedPath = selectedPath;
            else fBD.SelectedPath = AppDomain.CurrentDomain.BaseDirectory;
            //Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            // Get user's desired folder

            DialogResult dialogResult = fBD.ShowDialog();
            if (dialogResult == DialogResult.OK)
                result = fBD.SelectedPath;
            fBD.Dispose();
            return result;
        }

        public static string FAASubscriptionDirectory()
        {
            string result = string.Empty; ;
            string filter = "*28DaySubscription*";
            if (FolderMgt.DataFolder.Length == 0)
            {
                result = "ERROR - Directory not set";
            }
            else
            if (!Directory.Exists(FolderMgt.DataFolder))
            {
                result = "ERROR - Directory not found";
            }
            else
            {
                string[] dirs = Directory.GetDirectories(@FolderMgt.DataFolder, filter, SearchOption.TopDirectoryOnly);
                if (dirs.Length == 0)
                    result = "ERROR - No Subscription file";
                if (dirs.Length > 1)
                    result = "ERROR - multiple Subscription files";
                foreach (string dir in dirs)
                {
                    result = dir;
                }
            }
            return result;
        }

        public static DialogResult SendMessage(string Msg,
                MessageBoxIcon icon = MessageBoxIcon.Warning, MessageBoxButtons buttons = MessageBoxButtons.OK)
        {
            return MessageBox.Show(Msg, VersionInfo.Title, buttons, icon);
        }

        public static object[] FixInfo(string FIX)
        {
            // Fix, Frequency(opt), Latitude, Longitude, Name, FixType
            {
                // Finds the given fix and RETURNS
                // Fix, Frequency(opt), Latitude, Longitude, Name, FixType
                List<object> result;
                string Filter = "[FacilityID] = '" + FIX + "'";
                // Search each table for the NavAid and return result
                DataView dvFIX = new DataView(Form1.FIX)
                {
                    RowFilter = Filter
                };
                if (dvFIX.Count != 0)
                {
                    result = new List<object>()
                    {
                    FIX,
                    string.Empty,
                    dvFIX[0]["Latitude"],
                    dvFIX[0]["Longitude"],
                    dvFIX[0]["Use"].ToString(),
                    "FIX",
                    };
                    dvFIX.Dispose();
                }
                else
                {
                    DataView dvVOR = new DataView(Form1.VOR)
                    {
                        RowFilter = Filter
                    };
                    if (dvVOR.Count != 0)
                    {
                        result = new List<object>()
                        {
                        FIX,
                        dvVOR[0]["Frequency"],
                        dvVOR[0]["Latitude"],
                        dvVOR[0]["Longitude"],
                        dvVOR[0]["Name"].ToString(),
                        dvVOR[0]["Type"].ToString()
                        };
                        dvVOR.Dispose();
                    }

                    else
                    {
                        DataView dvNDB = new DataView(Form1.NDB)
                        {
                            RowFilter = Filter
                        };
                        if (dvNDB.Count != 0)
                        {
                            result = new List<object>()
                        {
                        FIX,
                        dvNDB[0]["Frequency"],
                        dvNDB[0]["Latitude"],
                        dvNDB[0]["Longitude"],
                        dvNDB[0]["Type"].ToString(),
                        "NDB",
                        };
                            dvNDB.Dispose();
                        }
                        else
                        {
                            DataView dvAPT = new DataView(Form1.APT)
                            {
                                RowFilter = Filter
                            };
                            if (dvAPT.Count != 0)
                            {
                                result = new List<object>()
                                {
                                FIX,
                                string.Empty,
                                dvFIX[0]["Latitude"],
                                dvFIX[0]["Longitude"],
                                dvFIX[0]["Use"].ToString(),
                                "FIX",
                                };
                                dvFIX.Dispose();
                            }
                            else
                            {
                                result = new List<object>()
                                {
                                    FIX,
                                    string.Empty,
                                    -1,
                                    -1,
                                    "NA",
                                    "NA",
                                };
                            }
                        }
                    }
                }
                return result.ToArray();
            }
        }

        public static double GetMagVar(string Arpt)
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
        public static int CountStringOccurrences(string text, string pattern)
        {
            // Loop through all instances of the string 'text'.
            int count = 0;
            int i = 0;
            while ((i = text.IndexOf(pattern, i)) != -1)
            {
                i += pattern.Length;
                count++;
            }
            return count;
        }

        public static int CountListOccurrences(List<string> Words, string pattern)
        {
            // Loop through all instances of the string 'text'.
            int count = 0;
            foreach (string word in Words)
            {
                if (word == pattern) count++;
            }
            return count;
        }

        public static int GetMinDataView(DataView dv, string Column)
        {
            int minValue = int.MaxValue;
            foreach(DataRowView dataRowView in dv)
            {
                int current = (int)dataRowView[Column];
                minValue = Math.Min(minValue, current);
            }
            return minValue;
        }

        public static int GetMaxDataView(DataView dv, string Column)
        {
            int maxValue = int.MinValue;
            foreach (DataRowView dataRowView in dv)
            {
                int current = (int)dataRowView[Column];
                maxValue = Math.Max(maxValue, current);
            }
            return maxValue;
        }
    }

    public class Conversions
    // Convert a variety of strings 
    {
        public static double ToNM(double value, string FromType)
        {
            double result;
            switch (FromType)
            {
                case "N":
                default:
                    result = value;
                    break;
                case "S":
                    result = value * 0.868976;
                    break;
                case "f":
                    result = value * 0.000164579;
                    break;
                case "m":
                    result = value * 0.000539957;
                    break;
            }
            return result;
        }

        public static double SetFilterByLimit(string LL, string nud, string LLedge)
        {
            double result = Conversions.String2DecDeg(LL);
            double offset = Convert.ToDouble(nud);
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
        ///         where ? is the quadrant
        /// OpenAIG is [#]##:##:##? 
        /// FAA is [#]##-##-##.##?
        /// StarDP is ?ddmmsss The decimal for 3d s is implied
        /// VRC is ?###.##.##.###  (Leading zero for latitudes)
        /// NaviGraph is ? [#]## ##.###
        /// </summary>
        {
            DMS = DMS.Trim();
            double result = -199; double DD; double MM; double SS; string quadrant;
            string tempDMS; double factor; string newDelim;
            if (DMS.Length > 5)
            {
                // Check that caller didn't "forget" the delim (or may not know it)
                newDelim = FindDelimiter(DMS);
                if (Delim.Length != 0)
                    if (newDelim != Delim) return result;       // Result is still -199

                // Sometimes the quadrant is in the front and other times in the back!
                if (DMS.Substring(0, 1).IsNumeric())
                {
                    quadrant = Extensions.Right(DMS, 1);
                    tempDMS = DMS.Substring(0, DMS.Length - 1).Trim();              // Strip off quadrant
                }
                else
                {
                    quadrant = DMS.Substring(0, 1);
                    tempDMS = DMS.Substring(1, DMS.Length - 1).Trim();
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
                        if (DMS.Length > 7)                         // Add fraction of seconds if they exist
                        {
                            factor = (double)Math.Pow(10f, Convert.ToSingle(tempDMS.Length - 7));
                            SS += double.Parse(tempDMS.Substring(6)) / factor;
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
                            SS += double.Parse(tempDMS.Substring(5)) / factor;
                        }
                    }
                    result = LatLongCalc.DMS2DecDeg(DD, MM, SS, quadrant);
                }
                else
                // Has a delimiter
                {
                    int loc1 = tempDMS.IndexOf(newDelim, 0, tempDMS.Length, StringComparison.CurrentCulture); // end of DD
                    DD = double.Parse(tempDMS.Substring(0, loc1));
                    int loc2 = tempDMS.IndexOf(newDelim, loc1 + 1, tempDMS.Length - loc1 - 1, StringComparison.CurrentCulture); // End of MM
                    if (loc2 == -1)
                    // This is a Navigraph format (no minutes as decimal, no seconds)
                    {
                        MM = double.Parse(tempDMS.Substring(loc1 + 1));
                        SS = 0;
                    }
                    else
                    {
                        MM = double.Parse(tempDMS.Substring(loc1 + 1, loc2 - loc1 - 1));
                        SS = double.Parse(tempDMS.Substring(loc2 + 1, tempDMS.Length - loc2 - 1));
                    }
                    result = LatLongCalc.DMS2DecDeg(DD, MM, SS, quadrant);
                }
                // Last step: Ensure the result value falls within the range of the latitude or longitude
                switch (quadrant)
                {
                    case "N":
                    case "S":
                        if (Math.Abs(result) > 90) result = -199;
                        break;
                    case "E":
                    case "W":
                        if (Math.Abs(result) > 180) result = -199;
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

        public static string DecDeg2DMS(double DecDeg, bool IsLatitude)
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
            result = strDD + " " + strMM + " " + strSS + quadrant;
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
        public static float MagVar2DecMag(string Mag)
        {
            // For THIS program, W deviations are positive, E deviations are negative
            float result;
            if (Mag.Trim().Length > 0)
            {
                result = Convert.ToSingle(Mag.Substring(0, Mag.Length - 1).Trim());
                if (Mag.Right(1) == "E") result *= -1;
                return result;
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
#pragma warning disable IDE0059 // Unnecessary assignment of a value
            return double.TryParse(text, out double test);
#pragma warning restore IDE0059 // Unnecessary assignment of a value
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

        public static bool ContainsAny(this string Haystack, params string[] Needles)
        {
            bool result = false;
            foreach (string Needle in Needles)
            {
                result = result || Needles.Any(Haystack.Contains);
            }
            return result;
        }

        public static bool Numcheck(char chr)
        {
            /** 
           Purpose    : to allow only numbers 
           Returns    : True - character is number , False - Other than numbers 
           **/
            bool blnRetVal = false;
            try
            {
                if (!char.IsControl(chr) && !char.IsDigit(chr))
                {
                    blnRetVal = true;
                }
            }
            catch (Exception)
            {
            }
            return blnRetVal;
        }

        public static bool DecimalControl(char chrInput, ref TextBox txtBox, int intNoOfDec)
        {
            /** 
            Purpose    : To control the decimal places as per user option 
            Assumptions: 
            Effects    : 
            Inputs     :  
            Returns    : None 
            **/
            bool chrRetVal = false;
            try
            {
                string strSearch = string.Empty;

                if (chrInput == '\b')
                {
                    return false;
                }

                if (intNoOfDec == 0)
                {
                    strSearch = "0123456789";
                    int INDEX = (int)strSearch.IndexOf(chrInput.ToString());
                    if (strSearch.IndexOf(chrInput.ToString(), 0) == -1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    strSearch = "0123456789.";
                    if (strSearch.IndexOf(chrInput, 0) == -1)
                    {
                        return true;
                    }
                }

                if ((txtBox.Text.Length - txtBox.SelectionStart) > (intNoOfDec) && chrInput == '.')
                {
                    return true;
                }

                if (chrInput == '\b')
                {
                    chrRetVal = false;
                }
                else
                {
                    strSearch = txtBox.Text;
                    if (strSearch != string.Empty)
                    {
                        if (strSearch.IndexOf('.', 0) > -1 && chrInput == '.')
                        {
                            return true;
                        }
                    }
                    int intPos;
                    int intAftDec;

                    strSearch = txtBox.Text;
                    if (strSearch == string.Empty) return false;
                    intPos = (strSearch.IndexOf('.', 0));

                    if (intPos == -1)
                    {
                        strSearch = "0123456789.";
                        if (strSearch.IndexOf(chrInput, 0) == -1)
                        {
                            chrRetVal = true;
                        }
                        else
                            chrRetVal = false;
                    }
                    else
                    {
                        if (txtBox.SelectionStart > intPos)
                        {
                            intAftDec = txtBox.Text.Length - txtBox.Text.IndexOf('.', 0);
                            if (intAftDec > intNoOfDec)
                            {
                                chrRetVal = true;
                            }
                            else
                                chrRetVal = false;
                        }
                        else
                            chrRetVal = false;
                    }
                }
            }
            catch (Exception)
            {
            }
            return chrRetVal;
        }
    }



    public static class SCTstrings
    {

        public static string APTout(string[] strOut, string Apt = "", string Freq = "", 
            string Lat = "", string Lon = "", string Name = "", string Comment = "")
        {
            // ACCEPTS either APT details in a list, or individually
            // RETURNS [AIRPORT] format for airport
            string result;
            if (strOut.Count() != 0)
            {
                result = strOut[0].PadRight(4) + " " + strOut[1].PadRight(7) + " " + strOut[2] + " " +
                strOut[3] + " " + " ; " + strOut[4] + " " + strOut[5];
            }
            else
            {
                result = Apt.PadRight(4) + " " + Freq.PadRight(7) + " " + Lat + " " + Lon + " " + " ;" + Name + " " + Comment;
            }
            return result;
        }

        public static string FIXout(string[] strOut, string Fix = "", string Lat = "", string Lon = "", string Comment = "")
        {
            string result;
            if (strOut.Count() != 0)
                result = strOut[0].PadRight(5) + " " + strOut[2] + " " + strOut[3] + " ;" + strOut[4];
            else
                result = Fix.PadRight(5) + " " + Lat + " " + Lon + " ;" + Comment;
            return result;
        }

        public static string VORout(string[] strOut, string Fac = "", string Freq = "", string Lat = "", 
            string Lon = "", string Name = "")
        {
            string result;
            if (strOut.Count() != 0)
                result = strOut[0].PadRight(3) + " " + strOut[1].PadRight(6) + " " + strOut[2] + " " + strOut[3] + " ;" + strOut[4];
            else
                result = Fac.PadRight(5) + " " + Freq.PadRight(7) + " " + Lat + " " + Lon + " ;" + Name;
            return result;
        }

        public static string NDBout(string[] strOut, string Fac = "", string Freq = "", string Lat = "",
             string Lon = "", string Name = "")
        {
            string result;
            if (strOut.Count() != 0)
                result = strOut[0].PadRight(3) + " " + strOut[1].PadRight(3) + " " + strOut[2] + " " + strOut[3] + " ;" + strOut[4];
            else
                result = Fac.PadRight(3) + " " + Freq.PadRight(3) + " " + Lat + " " + Lon + " ;" + Name;
            return result;
        }

        public static string RWYout(string[] strOut)
        {
            string result = strOut[0] + " " + strOut[1] + " " + strOut[2] + " " + strOut[3] + " "
                        + strOut[4] + " " + strOut[5] + " " + strOut[6] + " " + strOut[7] + "; " + strOut[8];
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

        public static string SSDout(object StartLat,
            object StartLong, object EndLat, object EndLong,
            string NavAid0 = "", string NavAid1 = "", bool UseFix = false)
            // If the Lat/Long is NOT a string, convert it
        {
            string Lat0; string Lat1; string Lon0; string Lon1;
            if (Extensions.IsNumeric(StartLat.ToString()))
                Lat0 = Conversions.DecDeg2SCT(Convert.ToDouble(StartLat), true);
            else
                Lat0 = StartLat.ToString();
            if (Extensions.IsNumeric(StartLong.ToString()))
                Lon0 = Conversions.DecDeg2SCT(Convert.ToDouble(StartLong), false);
            else
                Lon0 = StartLong.ToString();
            if (Extensions.IsNumeric(EndLat.ToString()))
                Lat1 = Conversions.DecDeg2SCT(Convert.ToDouble(EndLat), true);
            else
                Lat1 = EndLat.ToString();
            if (Extensions.IsNumeric(EndLong.ToString()))
                Lon1 = Conversions.DecDeg2SCT(Convert.ToDouble(EndLong), false);
            else
                Lon1 = EndLong.ToString();
            string str = new string(' ', 27);
            string result;
            if (!UseFix)
                result = str + Lat0 + " " + Lon0 + " " + Lat1 + " " + Lon1 +
                    " ; " + NavAid0 + " " + NavAid1;
            else
                result = str + Lat0 + " " + Lon0 + " " + Lat1 + " " + Lon1;
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

        public static string GeoOut(string StartLat,
            string StartLong, string EndLat, string EndLong,
            string suffix)
        {
            string result = StartLat + " " + StartLong + " " +
                            EndLat + " " + EndLong + " " + suffix;
            return result;
        }

        public static string LabelOut(string label, string Lat, string Long, string Color, string Comment = "")
        {
            string result;
            string strText = "\"" + label.Trim() + "\"";
            if (Comment.Length != 0)
                result = strText + " " + Lat + " " + Long + " " + Color + "; " + Comment;
            else
                result = strText + " " + Lat + " " + Long + " " + Color;
            return result;
        }
    }

    public static class ESEstrings
    {
        // Only those not identical to SCTstring
        public static string RWYout(string[] strOut)
        {
            string result = strOut[0] + " " + strOut[1] + " " + strOut[2] + " " + strOut[3] + " "
                        + strOut[4] + " " + strOut[5] + " " + strOut[6] + " " + strOut[7] + " " + strOut[8];
            return result;
        }

        public static string SSDout(bool IsSID, string Airport, string RWY, string Transition, 
            string SSDname, List<string>Fix)
        {
            string SSD;
            if (IsSID) SSD = "SID"; else SSD = "STAR"; 
            string result;
            string Fixes = string.Empty;
            foreach (string f in Fix) Fixes += f + " ";
            result = SSD + ":" + Airport + ":" + RWY + ":" + Transition + "x" + SSDname + ":" + Fixes;
            return result;
        }
    }

    /// <summary>
    /// Class containing methods to retrieve specific file system paths.
    /// </summary>
    public static class KnownFolders
    {
        private readonly static string[] _knownFolderGuids = new string[]
        {
        "{56784854-C6CB-462B-8169-88E350ACB882}", // Contacts
        "{B4BFCC3A-DB2C-424C-B029-7FE99A87C641}", // Desktop
        "{FDD39AD0-238F-46AF-ADB4-6C85480369C7}", // Documents
        "{374DE290-123F-4565-9164-39C4925E467B}", // Downloads
        "{1777F761-68AD-4D8A-87BD-30B759FA33DD}", // Favorites
        "{BFB9D5E0-C6A9-404C-B2B2-AE6DB6AF4968}", // Links
        "{4BD8D571-6D19-48D3-BE97-422220080E43}", // Music
        "{33E28130-4E1E-4676-835A-98395C3BC3BB}", // Pictures
        "{4C5C32FF-BB9D-43B0-B5B4-2D72E54EAAA4}", // SavedGames
        "{7D1D3A04-DEBB-4115-95CF-2F29DA2920DA}", // SavedSearches
        "{18989B1D-99B5-455B-841C-AB7C74E4DDFC}", // Videos
        };

        /// <summary>
        /// Gets the current path to the specified known folder as currently configured. This does
        /// not require the folder to be existent.
        /// </summary>
        /// <param name="knownFolder">The known folder which current path will be returned.</param>
        /// <returns>The default path of the known folder.</returns>
        /// <exception cref="System.Runtime.InteropServices.ExternalException">Thrown if the path
        ///     could not be retrieved.</exception>
        public static string GetPath(KnownFolder knownFolder)
        {
            return GetPath(knownFolder, false);
        }

        /// <summary>
        /// Gets the current path to the specified known folder as currently configured. This does
        /// not require the folder to be existent.
        /// </summary>
        /// <param name="knownFolder">The known folder which current path will be returned.</param>
        /// <param name="defaultUser">Specifies if the paths of the default user (user profile
        ///     template) will be used. This requires administrative rights.</param>
        /// <returns>The default path of the known folder.</returns>
        /// <exception cref="System.Runtime.InteropServices.ExternalException">Thrown if the path
        ///     could not be retrieved.</exception>
        public static string GetPath(KnownFolder knownFolder, bool defaultUser)
        {
            return GetPath(knownFolder, KnownFolderFlags.DontVerify, defaultUser);
        }

        private static string GetPath(KnownFolder knownFolder, KnownFolderFlags flags,
            bool defaultUser)
        {
            int result = SHGetKnownFolderPath(new Guid(_knownFolderGuids[(int)knownFolder]),
                (uint)flags, new IntPtr(defaultUser ? -1 : 0), out IntPtr outPath);
            if (result >= 0)
            {
                string path = Marshal.PtrToStringUni(outPath);
                Marshal.FreeCoTaskMem(outPath);
                return path;
            }
            else
            {
                throw new ExternalException("Unable to retrieve the known folder path. It may not "
                    + "be available on this system.", result);
            }
        }

        [DllImport("Shell32.dll")]
        private static extern int SHGetKnownFolderPath(
            [MarshalAs(UnmanagedType.LPStruct)]Guid rfid, uint dwFlags, IntPtr hToken,
            out IntPtr ppszPath);

        [Flags]
        private enum KnownFolderFlags : uint
        {
            SimpleIDList = 0x00000100,
            NotParentRelative = 0x00000200,
            DefaultPath = 0x00000400,
            Init = 0x00000800,
            NoAlias = 0x00001000,
            DontUnexpand = 0x00002000,
            DontVerify = 0x00004000,
            Create = 0x00008000,
            NoAppcontainerRedirection = 0x00010000,
            AliasOnly = 0x80000000
        }
    }

    /// <summary>
    /// Standard folders registered with the system. These folders are installed with Windows Vista
    /// and later operating systems, and a computer will have only folders appropriate to it
    /// installed.
    /// </summary>
    public enum KnownFolder
    {
        Contacts,
        Desktop,
        Documents,
        Downloads,
        Favorites,
        Links,
        Music,
        Pictures,
        SavedGames,
        SavedSearches,
        Videos
    }
}
