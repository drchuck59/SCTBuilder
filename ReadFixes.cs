using System;
using System.IO;
using System.Globalization;
using System.Windows.Forms;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data;

namespace SCTBuilder
{
    public class ReadFixes
    {
        public static int FillCycleInfo()
        {
            string FullFilename = GetFullPathname(FolderMgt.DataFolder, "NATFIX.txt");
            if (FullFilename.IndexOf("ERROR",0) == -1)
                {
                string Line = string.Empty;
                using (StreamReader reader = new StreamReader(FullFilename))
                {
                    Line = reader.ReadLine();
                    Line = reader.ReadLine();
                }
                Line = Line.Substring(1, Line.Length - 1);
                string strDate = Line.Substring(4, 2) + "/" + Line.Substring(6, 2) + "/" + Line.Substring(0, 4);
                DateTime date = Convert.ToDateTime(strDate);
                CycleInfo.FindAIRAC(date, SetCycleInfo: true);
                return CycleInfo.AIRAC;
            }
            else return -1;
        }
        public static void FillARB()
        {
            /// <summary>
            /// Some of the ARTCC boundaries defined by the ARTCC facility are composed of 
            /// more than a single closed shape. In these cases it is necessary to read the point
            /// description text for the key phrase "TO POINT OF BEGINNING" to identify
            /// where the shape returns to the beginning and forms a closed shape.
            /// </summary>
            DataTable arb = Form1.ARB;
            if (arb.Rows.Count != 0) arb.Clear();     // Must start with empty table
            string FullFilename = GetFullPathname(FolderMgt.DataFolder, "ARB.txt");
            Conversions S2D = new Conversions();
            using (StreamReader reader = new StreamReader(FullFilename))
            {
                string Line = string.Empty;
                while ((Line = reader.ReadLine()) != null)
                {
                    var FixItems = new List<object>
                    {
                        Line.Substring(0, 3),             // ARTCC
                        Line.Substring(12, 40).Trim(),    // Name
                        Line.Substring(52, 10).Trim(),    // Boundary type (H/L/Boundary, etc.)
                        S2D.String2DecDeg(Line.Substring(62, 14).Trim(), "-"),  // Latitude
                        S2D.String2DecDeg(Line.Substring(76, 14).Trim(), "-"),  // Longitude
                        Line.Substring(90, 300).Trim(),   // Decode Name
                        Convert.ToInt32(Line.Substring(390, 6).Trim())    // Sequence number
                    };
                    if (Line.Substring(396, 1).Trim().Length == 0)       // If length > 0, do not use this coordinate)
                        AddFixes(arb, FixItems);
                }
                // Console.WriteLine("ARB rows read " + arb.Rows.Count.ToString());
            }
        }
        public static void FillVORNDB()
        {
            /// <summary>
            /// Since both VORs and NDBs are in the same FAA table, do both of them here
            /// </summary>
            // MessageBox.Show("Filling VOR_NDB", VersionInfo.Title, MessageBoxButtons.OK, MessageBoxIcon.Information);
            DataTable VORtable = Form1.VOR;
            DataTable NDBtable = Form1.NDB;
            if (VORtable.Rows.Count != 0) VORtable.Clear();     // Must start with empty tables
            if (NDBtable.Rows.Count != 0) NDBtable.Clear();
            string FullFilename = GetFullPathname(FolderMgt.DataFolder, "NAV.txt");
            string FixType; float Lat; float Long; float Mag;
            using (StreamReader reader = new StreamReader(FullFilename))
            {
                string Temp = DateTime.Now.ToString("yyyyMMdd");
                // First line confirms file name
                // Second line is the cycle start date (publication)
                // Maybe use them for error checking later
                string Line = string.Empty;
                while ((Line = reader.ReadLine()) != null)
                {
                    switch (Line.Substring(0, 4))
                    {
                        case "NAV1":
                            Lat = -1f; Long = -1f; Mag = 0f;
                            FixType = Line.Substring(8, 20).Trim();                     // FixType
                            if (Line.Substring(28, 4).Trim().Length == 0) break;        // Skip fixes with no ID
                            if (Line.Substring(533, 6).Trim().Length == 0) break;       // Skip fixes with no transmitter
                            if (Line.Substring(385, 10).Trim().Length == 0) break;      // Skip fixes with no location
                            if (Line.Substring(410, 10).Trim().Length == 0) break;
                            Lat = Convert.ToSingle(Line.Substring(385, 10).Trim()) / 3600;
                            if (Line.Substring(398, 1) == "S") Lat *= -1;
                            Long = Convert.ToSingle(Line.Substring(410, 10).Trim()) / 3600;
                            if (Line.Substring(420, 1) == "W") Long *= -1;
                            if (Line.Substring(479, 4).Trim().Length != 0)
                                Mag = (Line.Substring(483, 1) == "W" ? Convert.ToSingle(Line.Substring(479, 4).Trim()) * -1
                                        : Convert.ToSingle(Line.Substring(479, 4).Trim()));
                            var FixItems = new List<object>
                                {
                                    Line.Substring(4, 4).Trim() + Line.Substring(9, 20).Trim() + Line.Substring(72,40).Trim(), // ID
                                    Line.Substring(28, 4).Trim(),                            // Facility ID
                                    Line.Substring(42,30).Trim(),                           // Name
                                    Lat,                                                    // Latitude
                                    Long,                                                   // Longitude
                                    Line.Substring(533,6).Trim(),                           // Frequency
                                    Line.Substring(337, 4).Trim(),                          // ARTCC
                                    Line.Substring(142, 2).Trim(),                           // State
                                    Mag,                                                    // Magnetic Variation
                                    FixType,                                                // Type of fix
                                };
                            // Console.WriteLine(Line.Substring(0, 28).Trim().ToString());
                            if (FixType.IndexOf("VOR", 0, FixType.Length) > -1)
                                AddFixes(VORtable, FixItems);
                            else if (FixType.IndexOf("NDB", 0, FixType.Length) > -1)
                                AddFixes(NDBtable, FixItems);
                            break;
                        case "'":
                            if (Line.Substring(0, 1) == "'")
                                Temp = Line.Substring(1, Line.Length - 1);
                            Temp = Temp.Substring(0, 4) + "-" + Temp.Substring(4, 2) + "-" + Temp.Substring(6, 2);
                            CycleInfo.FindAIRAC(
                                DateTime.ParseExact(Temp, "yyyy-MM-dd", CultureInfo.CurrentCulture), true);
                            break;
                        default:
                            break;
                    }
                }
                // Console.WriteLine("VOR rows read: " + VORtable.Rows.Count.ToString());
                // Console.WriteLine("NDB rows read: " + NDBtable.Rows.Count.ToString());
            }
        }
        public static void FillFIX()
        {
            // MessageBox.Show("Filling FIX", VersionInfo.Title, MessageBoxButtons.OK, MessageBoxIcon.Information);
            DataTable FIXtable = Form1.FIX;
            if (FIXtable.Rows.Count != 0) FIXtable.Clear();     // Must start with empty tables
            string FullFilename = GetFullPathname(FolderMgt.DataFolder, "FIX.txt");
            string FixType;
            Conversions S2D = new Conversions();
            using (StreamReader reader = new StreamReader(FullFilename))
            {
                string Line = string.Empty;
                while ((Line = reader.ReadLine()) != null)
                {
                    switch (Line.Substring(0, 4))
                    {
                        case "FIX1":
                            FixType = Line.Substring(213, 15).Trim();                     // FixType
                            if (Line.Substring(67, 14).Trim().Length == 0) break;
                            var FixItems = new List<object>
                                {
                                    Line.Substring(4, 30).Trim(),                            // ID
                                    S2D.String2DecDeg(Line.Substring(66, 14).Trim(),"-"),      // Latitude
                                    S2D.String2DecDeg(Line.Substring(80, 14).Trim(),"-"),      // Longitude
                                    Line.Substring(237, 4).Trim(),                          // ARTCC
                                    Line.Substring(34, 30).Trim(),                          // State
                                    Line.Substring(213, 15).Trim(),                         // Use
                                };
                            AddFixes(FIXtable, FixItems);
                            break;
                        default:
                            break;
                    }
                }
            }
            // Console.WriteLine("FIX rows read: " + FIXtable.Rows.Count.ToString());
        }
        public static void FillAPT()
        {
            DataTable APTtable = Form1.APT;
            DataTable RWYtable = Form1.RWY;
            if (APTtable.Rows.Count != 0) APTtable.Clear();     // Must start with empty tables
            if (RWYtable.Rows.Count != 0) RWYtable.Clear();
            string FullFilename = GetFullPathname(FolderMgt.DataFolder, "APT.txt");
            const string FacilityType = "GUBACH";       // Airport types = WHile we accept the first 3, on 'A' has runway data.
                                                        // ID, ICOA, Name, Lat, Long, ARTCC, State, Mag, Pub, Open
            int FacType = -1; string tempID = string.Empty; string tempBID = string.Empty;
            string tempRID = string.Empty; float tempLatB = 0f; float tempLongB = 0f; string tempFacID = string.Empty;
            float tempLatR = 0f; float tempLongR = 0f; float tempLength = 0f; bool tempOpen = false;
            float tempWidth = 0f; float tempHdgB = -1f; float tempHdgR = -1f; string tempRwyID; string tempRwyName = string.Empty;
            float tempElevB = 0f; float tempElevR = 0f; string tempARTCC = string.Empty;
            using (StreamReader reader = new StreamReader(FullFilename))
            {
                string Line = string.Empty;
                while ((Line = reader.ReadLine()) != null)
                {

                    switch (Line.Substring(0, 3).Trim())
                    {
                        case "APT":
                            // Get the facility type
                            tempID = Line.Substring(3, 11).Trim();
                            FacType = FacilityType.IndexOf(Extensions.Right(tempID, 1));
                            tempOpen = (Line.Substring(840, 2).Trim() == "O");
                            if ((FacType > 2) & (tempOpen))     // Only operational APTs of "A", "H" and "C"
                            {
                                tempARTCC = Line.Substring(674, 4).Trim();
                                tempFacID = Line.Substring(27, 4).Trim();
                                var AptInfo = new List<object>
                                {
                                tempID,                                        // ID
                                tempFacID,                                     // Facility ID
                                Line.Substring(133, 50).Trim(),                // Facility Name
                                Conversions.SS2DD(Line.Substring(538, 12)),    // Latitude
                                Conversions.SS2DD(Line.Substring(565, 12)),    // Longitude
                                tempARTCC,                                     // Responsible ARTCC
                                Line.Substring(50, 20).Trim(),                 // State
                                Conversions.MagVar(Line.Substring(586, 3)),    // Magnetic Variation
                                Line.Substring(183, 2),                        // Owner Type
                                Line.Substring(185, 2) == "PU"                 // True if public
                                };
                                AddFixes(APTtable, AptInfo);
                                tempRwyName = string.Empty;
                            }
                            break;
                        case "RWY":
                            // I only want real airports with real runways
                            tempRwyID = Line.Substring(3, 11).Trim();
                            if ((tempRwyID == tempID) & tempOpen & (FacType == 3))
                            {
                                if (
                                    (tempRwyName != Line.Substring(16, 7).Trim()) & // Don't duplicate RWY name (e.g, 18L/36R)
                                    (Line.Substring(23, 5).Trim().Length != 0) &    // tempLength
                                    (Line.Substring(28, 4).Trim().Length != 0) &    // tempWidth
                                    (Line.Substring(65, 3).Trim().Length != 0) &    // tempBID
                                    (Line.Substring(68, 3).Trim().Length != 0) &    // tempHdgB
                                    (Line.Substring(142, 7).Trim().Length != 0) &   // tempElevB
                                    (Line.Substring(287, 3).Trim().Length != 0) &   // tempRID
                                    (Line.Substring(290, 3).Trim().Length != 0) &  // tempHdgR
                                    (Line.Substring(364, 7).Trim().Length != 0))   // tempElevR
                                {
                                    tempRwyName = Line.Substring(16, 7).Trim();
                                    tempLength = Convert.ToSingle(Line.Substring(23, 5).Trim());
                                    tempWidth = Convert.ToSingle(Line.Substring(28, 4).Trim());
                                    tempBID = Line.Substring(65, 3).Trim();
                                    tempHdgB = LatLongCalc.GetBearing(Line.Substring(68, 3).Trim(), tempBID);
                                    tempLatB = Conversions.SS2DD(Line.Substring(103, 12).Trim());   // Latitude
                                    if (tempLatB == -1) tempOpen = false;
                                    tempLongB = Conversions.SS2DD(Line.Substring(130, 12).Trim());  // Longitude
                                    if (tempLongB == -1) tempOpen = false;
                                    tempElevB = Convert.ToSingle(Line.Substring(142, 7).Trim());
                                    tempRID = Line.Substring(287, 3).Trim();
                                    tempHdgR = LatLongCalc.GetBearing(Line.Substring(290, 3).Trim(), tempRID);
                                    tempLatR = Conversions.SS2DD(Line.Substring(325, 12).Trim());   // EndLatitude
                                    if (tempLatR == -1) tempOpen = false;
                                    tempLongR = Conversions.SS2DD(Line.Substring(352, 12).Trim());  // EndLongitude
                                    if (tempLongR == -1) tempOpen = false;
                                    tempElevR = Convert.ToSingle(Line.Substring(364, 7).Trim());
                                }
                                else tempOpen = false;
                                if (tempOpen)
                                {
                                    var RwyItems = new List<object>
                                    {
                                        tempID,                                                 // ID (from APT row)
                                        tempFacID,                                              // Facility ID
                                        tempARTCC,                                              // ARTCC (from APT row)
                                        tempRwyName,                                            // Runway identifier (e.g., 17L/35R)
                                        tempLength,                                             // Length Feet
                                        tempWidth,                                              // Width Feet
                                        tempBID,                                                // Base identifier (e.g., 17L)
                                        tempHdgB,                                               // Base Heading
                                        tempLatB,                                               // Base Latitude
                                        tempLongB,                                              // Base Longitude
                                        tempElevB,                                              // Base Elevation (feet)
                                        tempRID,                                                // Reciprocal identifier (e.g., 17L)
                                        tempHdgR,                                               // Reciprocal Heading
                                        tempLatR,                                               // Reciprocal Latitude
                                        tempLongR,                                              // Reciprocal Longitude
                                        tempElevR                                               // Reciprocal Elevation (feet)
                                    };
                                    AddFixes(RWYtable, RwyItems);
                                }
                            }
                            break;
                    }
                }
            }
            // Console.WriteLine("APT rows read: " + APTtable.Rows.Count.ToString());
            // Console.WriteLine("RWY rows read: " + RWYtable.Rows.Count.ToString());
        }
        public static void FillTWR()
        {
            DataTable TWR = Form1.TWR;
            string FullFilename = GetFullPathname(FolderMgt.DataFolder, "TWR.txt");
            string Line = string.Empty; string rowType;
            const string FacilityType = "GUBACH"; int FacType;
            string tempID = string.Empty; string tempFac = string.Empty; string tempName = string.Empty;
            float tempLat = 0f; float tempLong = 0f; string tempFreq = "122.8"; string tempClass = string.Empty;
            bool bTWR1 = false; bool bTWR3 = false; bool bTWR8 = false;
            if (TWR.Rows.Count != 0) TWR.Clear();

            using (StreamReader reader = new StreamReader(FullFilename))
            {
                while ((Line = reader.ReadLine()) != null)
                {
                    rowType = Line.Substring(0, 4);
                    if (rowType == "TWR1")
                    {
                        // Before we start a new set, see if we need to add the old one
                        if (bTWR1)
                        {
                            if (!bTWR3) tempFreq = "122.8";
                            if (!bTWR8) tempClass = "G";
                            //Console.WriteLine("ID " + tempID);
                            //Console.WriteLine("Fac " + tempFac);
                            //Console.WriteLine("Name " + tempName);
                            //Console.WriteLine("Lat " + tempLat.ToString());
                            //Console.WriteLine("Long " + tempLong.ToString());
                            //Console.WriteLine("Freq " + tempFreq);
                            //Console.WriteLine("Class " + tempClass);
                            var TWRItems = new List<object>
                                {
                                    tempID,
                                    tempFac,
                                    tempName,
                                    tempLat,
                                    tempLong,
                                    tempFreq,
                                    tempClass
                                };
                            // Console.WriteLine("Writing row");
                            AddFixes(TWR, TWRItems);
                            bTWR1 = bTWR3 = bTWR8 = false;
                            tempFreq = "122.8";
                            tempClass = "G";
                        }
                        tempID = Line.Substring(18, 11).Trim();
                        if (tempID.Length != 0)
                        {
                            FacType = FacilityType.IndexOf(Extensions.Right(tempID, 1));
                            if (FacType < 3)
                            {
                                bTWR1 = false;      // Only operational APTs of "A", "H" and "C"
                            }
                            else
                            {
                                bTWR1 = true;
                                tempFac = Line.Substring(4, 4).Trim();
                                tempName = Line.Substring(104, 50).Trim();
                                tempLat = Conversions.SS2DD(Line.Substring(168, 11).Trim());
                                tempLong = Conversions.SS2DD(Line.Substring(193, 11).Trim());
                            }
                        }
                    }
                    if ((rowType == "TWR3") & bTWR1 & !bTWR3)           // Only set this value once
                    {
                        int loc1 = Line.IndexOf("LCL/P", 0, Line.Length, StringComparison.InvariantCulture);
                        if (loc1 != -1)
                        {
                            tempFreq = Line.Substring(loc1 - 44, 44).Trim();
                            int loc2 = tempFreq.IndexOf(";");
                            if (loc2 != -1) tempFreq = Line.Substring(0, loc2 - 1).Trim();
                            bTWR3 = true;
                        }
                    }
                    if ((rowType == "TWR8") & bTWR1)
                    {
                        tempClass = "G";                                           // Airspace Class
                        string Classes = "BCDE";
                        int loc2 = Line.IndexOf("Y", 8, 4);
                        if (loc2 >= 0) tempClass = Classes[loc2 - 8].ToString();
                        bTWR8 = true;
                    }
                }
            }
            // Console.WriteLine("TWR rows read: " + TWR.Rows.Count);
        }
        public static void FillAWY()
        {
            DataTable AWY = Form1.AWY;
            string FullFilename = GetFullPathname(FolderMgt.DataFolder, "AWY.txt");
            string Line = string.Empty; string aNAVtype = string.Empty; string aNAVID;
            string aSeqNo = string.Empty; bool aFix; string aARTCC = string.Empty; string aMOCA = string.Empty;
            string aMEA = string.Empty; string aMAA = string.Empty; string atype = string.Empty;
            float aLat; float aLong; string aID = string.Empty;
            Conversions S2D = new Conversions();
            using (StreamReader reader = new StreamReader(FullFilename))
            {
                while ((Line = reader.ReadLine()) != null)
                {
                    switch (Line.Substring(0, 4))
                    {
                        case "AWY1":
                            aID = Line.Substring(4, 5).Trim();                        // Airway ID
                            aSeqNo = Line.Substring(10, 5).Trim();                    // Sequency values per-Airway
                            atype = Line.Substring(9, 1).Trim();                      // Used only for Alaskan or Hawaiian routes
                            aARTCC = Line.Substring(141, 3).Trim();                   // Controlling ARTCC
                            aMEA = Line.Substring(74, 5).Trim();                    // Minimum Enroute Altituce
                            if (aMEA.Length == 0) aMEA = "-1";
                            aMAA = Line.Substring(96, 5).Trim();                    // Maximum Allowable Alt (defines High or Low)
                            if (aMAA.Length == 0) aMAA = "-1";
                            aMOCA = Line.Substring(101, 5).Trim();                  // Minimum Obstruction Clearance Altitude (usually blank)
                            if (aMOCA.Length == 0) aMOCA = "-1";
                            break;
                        case "AWY2":
                            aFix = Line.Substring(64, 15).Trim() == "FIX";
                            if (aFix)
                            { aNAVID = Line.Substring(15, 30).Trim(); }
                            else
                            { aNAVID = Line.Substring(116, 4).Trim(); }
                            aLat = S2D.String2DecDeg(Line.Substring(83, 14).Trim(), "-");      // Latitude
                            aLong = S2D.String2DecDeg(Line.Substring(97, 14).Trim(), "-");      // Longitude
                            var FixItems = new List<object>
                            {
                                aID,
                                aSeqNo,
                                aARTCC,
                                atype,
                                Convert.ToInt32(aMEA),
                                Convert.ToInt32(aMAA),
                                Convert.ToInt32(aMOCA),
                                aNAVID,
                                aNAVtype,
                                aFix,
                                aLat,
                                aLong
                            };
                            AddFixes(AWY, FixItems);
                            break;
                        case "AWY3":
                            // Crossing points - not sure if I need them for VRC
                            break;
                        default:
                            break;
                    }
                }
            }
            // Console.WriteLine("AWY rows read: " + AWY.Rows.Count.ToString());
        }
        public static void FillStarDP()
        {
            DataTable SSD = Form1.SSD;
            string FullFilename = GetFullPathname(FolderMgt.DataFolder, "STARDP.txt");
            bool isSid; string Line; int Seqno = 0;
            Conversions S2D = new Conversions();
            using (StreamReader reader = new StreamReader(FullFilename))
            {
                while ((Line = reader.ReadLine()) != null)
                {
                    if (Line.Substring(0, 1) == "D") isSid = true; else isSid = false;
                    Seqno += 10;
                    var FixItems = new List<object>
                    {
                        Line.Substring(0,5).Trim(),                         // Internal ID
                        Line.Substring(30,6).Trim(),                        // NavAid
                        Line.Substring(10,2).Trim(),                        // FixType
                        S2D.String2DecDeg(Line.Substring(13, 8).Trim()),    // Latitude
                        S2D.String2DecDeg(Line.Substring(21, 9).Trim()),    // Longitude
                        Line.Substring(38, 13).Trim(),                      // StarCode
                        Line.Substring(51, 110).Trim(),                     // StarName
                        Seqno,                                              // Sequence Number (mine)
                        isSid                                               // Is a SID
                    };
                    AddFixes(SSD, FixItems);
                    // Console.WriteLine(FixItems[0] + " " + FixItems[8]);
                    // MessageBox.Show("checklist");
                }
            }
            // Console.WriteLine("SSD rows read: " + SSD.Rows.Count);
        }
        private static string GetFullPathname(string DataFolder, string Filename)
        /// <summary>
        /// Checks that the data exists, returns an error if not found
        /// </summary>
        {
            try
            {
                var file = Directory.GetFiles(DataFolder, Filename, System.IO.SearchOption.AllDirectories).FirstOrDefault();
                return file.ToString();
            }
            catch (FileNotFoundException)
            {
                return "FILE ERROR";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return "ERROR";
            }
        }
        private static void AddFixes(DataTable dT, List<object> FixItems)
        {
            dT.Rows.Add(FixItems.ToArray());
        }
    }
}
