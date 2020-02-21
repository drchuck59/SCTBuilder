using System;
using System.IO;
using System.Globalization;
using System.Windows.Forms;
using System.Linq;
using System.Collections.Generic;
using System.Xml;
using System.Data;

namespace SCTBuilder
{
    public class ReadNASR
    {
        public static int FillCycleInfo()
        {
            string FullFilename = GetFullPathname(FolderMgt.DataFolder, "NATFIX.txt");
            if (FullFilename.IndexOf("ERROR", 0) == -1)
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
                        Conversions.String2DecDeg(Line.Substring(62, 14).Trim(), "-"),  // Latitude
                        Conversions.String2DecDeg(Line.Substring(76, 14).Trim(), "-"),  // Longitude
                        Line.Substring(90, 300).Trim(),   // Decode Name
                        Convert.ToInt32(Line.Substring(390, 6).Trim())    // Sequence number
                    };
                    if (Line.Substring(396, 1).Trim().Length == 0)       // If length > 0, do not use this coordinate)
                        AddFixes(arb, FixItems);
                }
                Console.WriteLine("ARB rows read " + arb.Rows.Count.ToString());
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
            string FixType; double Lat; double Long; double Mag;
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
                            Mag = 0f;
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
                                Mag = Convert.ToSingle(Line.Substring(479, 4).Trim());
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
            using (StreamReader reader = new StreamReader(FullFilename))
            {
                string Line = string.Empty;
                while ((Line = reader.ReadLine()) != null)
                {
                    switch (Line.Substring(0, 4))
                    {
                        case "FIX1":
                            if (Line.Substring(67, 14).Trim().Length == 0) break;
                            var FixItems = new List<object>
                                {
                                    Line.Substring(4, 30).Trim(),                            // ID
                                    Conversions.String2DecDeg(Line.Substring(66, 14).Trim(),"-"),      // Latitude
                                    Conversions.String2DecDeg(Line.Substring(80, 14).Trim(),"-"),      // Longitude
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
            char[] FacilityType = { 'A', 'C', 'H' };     // Only interested in Airport, Seaplane, and Heliports
            string FacType = string.Empty; string tempID = string.Empty; string tempBID = string.Empty;
            string tempRID = string.Empty; double tempLatB = 0f; double tempLongB = 0f; string tempFacID = string.Empty;
            double tempLatR = 0f; double tempLongR = 0f; double tempLength = 0f; bool tempOpen = false;
            double tempWidth = 0f; double tempHdgB = -1f; double tempHdgR = -1f; string tempRwyID; string tempRwyName = string.Empty;
            double tempElevB = 0f; double tempElevR = 0f; string tempARTCC = string.Empty;
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
                            FacType = Extensions.Right(tempID, 1);
                            tempOpen = Line.Substring(840, 2).Trim() == "O";

                            if ((FacType.IndexOfAny(FacilityType) != -1) && (tempOpen))     // Only operational APTs of "A", "H" and "C"
                            {
                                tempARTCC = Line.Substring(674, 4).Trim();
                                var AptInfo = new List<object>
                                {
                                tempID,                                        // ID (Landing Facility Site Number)
                                Line.Substring(27, 4).Trim(),                  // Facility ID (ICOA)
                                Line.Substring(133, 50).Trim(),                // Facility Name
                                Conversions.Seconds2DecDeg(Line.Substring(538, 12)),    // Latitude
                                Conversions.Seconds2DecDeg(Line.Substring(565, 12)),    // Longitude
                                tempARTCC,                                     // Responsible ARTCC
                                Line.Substring(50, 20).Trim(),                 // State
                                Conversions.MagVar2DecMag(Line.Substring(586, 3)),    // Magnetic Variation
                                Line.Substring(183, 2),                        // Owner Type
                                Line.Substring(185, 2) == "PU"                 // True if public
                                };
                                AddFixes(APTtable, AptInfo);
                                tempRwyName = string.Empty;
                            }
                            break;
                        case "RWY":
                            /// Only interested in Airport, Seaplane, and Heliports
                            tempRwyID = Line.Substring(3, 11).Trim();
                            if ((tempRwyID == tempID) && tempOpen && (FacType.IndexOfAny(FacilityType) != -1))
                            {
                                if (
                                    (tempRwyName != Line.Substring(16, 7).Trim()) && // Don't duplicate RWY name (e.g, 18L/36R)
                                    (Line.Substring(23, 5).Trim().Length != 0) &&    // tempLength
                                    (Line.Substring(28, 4).Trim().Length != 0) &&    // tempWidth
                                    (Line.Substring(65, 3).Trim().Length != 0) &&    // tempBID
                                    (Line.Substring(68, 3).Trim().Length != 0) &&    // tempHdgB
                                    (Line.Substring(142, 7).Trim().Length != 0) &&   // tempElevB
                                    (Line.Substring(287, 3).Trim().Length != 0) &&   // tempRID
                                    (Line.Substring(290, 3).Trim().Length != 0) &&  // tempHdgR
                                    (Line.Substring(364, 7).Trim().Length != 0))   // tempElevR
                                {
                                    tempRwyName = Line.Substring(16, 7).Trim();
                                    tempLength = Convert.ToSingle(Line.Substring(23, 5).Trim());
                                    tempWidth = Convert.ToSingle(Line.Substring(28, 4).Trim());
                                    tempBID = Line.Substring(65, 3).Trim();
                                    tempHdgB = LatLongCalc.RWYBearing(Line.Substring(68, 3).Trim(), tempBID);
                                    tempLatB = Conversions.Seconds2DecDeg(Line.Substring(103, 12).Trim());   // Latitude
                                    if (tempLatB == -1) tempOpen = false;
                                    tempLongB = Conversions.Seconds2DecDeg(Line.Substring(130, 12).Trim());  // Longitude
                                    if (tempLongB == -1) tempOpen = false;
                                    tempElevB = Convert.ToSingle(Line.Substring(142, 7).Trim());
                                    tempRID = Line.Substring(287, 3).Trim();
                                    tempHdgR = LatLongCalc.RWYBearing(Line.Substring(290, 3).Trim(), tempRID);
                                    tempLatR = Conversions.Seconds2DecDeg(Line.Substring(325, 12).Trim());   // EndLatitude
                                    if (tempLatR == -1) tempOpen = false;
                                    tempLongR = Conversions.Seconds2DecDeg(Line.Substring(352, 12).Trim());  // EndLongitude
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
            Console.WriteLine("APT rows read: " + APTtable.Rows.Count.ToString());
            Console.WriteLine("RWY rows read: " + RWYtable.Rows.Count.ToString());
        }
        public static void FillTWR()
        {
            DataTable TWR = Form1.TWR;
            string FullFilename = GetFullPathname(FolderMgt.DataFolder, "TWR.txt");
            string Line; string rowType; bool isATCT = false; bool LCLfound = false; int LineNo = 0;
            char[] FacType = { 'A', 'C', 'H' };     // Only interested in Airport, Seaplane, and Heliports
            string tempID = string.Empty; string tempFac = string.Empty; string tempName = string.Empty;
            double tempLat = 0f; double tempLong = 0f; string tempLCL = "122.8"; string tempATIS = string.Empty;
            string tempClass = string.Empty; string Classes = "BCDE"; bool isDATIS = false; bool ATISfound = false;
            if (TWR.Rows.Count != 0) TWR.Clear();

            using (StreamReader reader = new StreamReader(FullFilename))
            {
                while (!reader.EndOfStream)
                {
                    Line = reader.ReadLine().ToString();
                    LineNo += 1;
                    rowType = Line.Substring(0, 4);

                    if (rowType == "TWR1")
                    {
                        // Before we start a new set, see if we need to add the old one
                        if (isATCT)
                        {
                            // It's possible to have an empty tempLCL, which VRC can't accept
                            // E.g., this can occur with a MIL TWR having no VHF frequency
                            if (tempLCL.Length == 0) tempLCL = "122.8";
                            var TWRItems = new List<object>
                                {
                                    tempID,                     // Unique facility identifier (matches APT file)
                                    tempFac,                    // Typical facility identifier
                                    tempName,                   // Facility full name
                                    tempLat,                    // Facility latitude
                                    tempLong,                   // Facility longitude
                                    tempLCL,                    // VHF LCL/P frequency
                                    tempATIS,                   // VHF ATIS frequency
                                    isDATIS,                    // True if D-ATIS
                                    tempClass
                                };
                            AddFixes(TWR, TWRItems);
                            isATCT = false;
                            LCLfound = false;
                            ATISfound = false;
                            isDATIS = false;
                            tempLCL = "122.8";      // Default if no frequency found for this airfield (e.g., private)
                            tempClass = "G";        // Default if no Class found for this airfield (no TWR8 line)
                        }
                        // Set up the TWR info line
                        tempID = Line.Substring(18, 11).Trim(); // Airport ID site number (matches to APT file)
                        if (tempID.Length != 0)
                        {
                            // Must be a facility with ATCT (some are TRACON or non-ATCT)
                            isATCT = (Line.Substring(238, 12).IndexOf("NON") == -1) &
                                    (Line.Substring(238, 12).IndexOf("ATCT") != -1);
                            if ((tempID.IndexOfAny(FacType) != -1) && isATCT)
                            {
                                tempFac = Line.Substring(4, 4).Trim();
                                tempName = Line.Substring(104, 50).Trim();
                                tempLat = Conversions.Seconds2DecDeg(Line.Substring(168, 11).Trim());
                                tempLong = Conversions.Seconds2DecDeg(Line.Substring(193, 11).Trim());
                            }
                        }
                    }
                    if ((rowType == "TWR3") && isATCT)
                    {
                        // Because the item we are looking for could be anywhere on the line,
                        // but the frequency is in a fixed location, need to loop the locations of the line.
                        if (!LCLfound)
                        {
                            tempLCL = TWR3Freq(Line, "LCL/P");      // Tower Freq
                            LCLfound = (tempLCL.Length != 0);
                        }
                        if (!ATISfound)
                        {
                            tempATIS = TWR3Freq(Line, "ATIS");     // ATIS frequency
                            ATISfound = (tempATIS.Length != 0);
                            if (ATISfound && !isDATIS) isDATIS = TWR3isDatis(Line);         // Mark if digital ATIS
                        }
                    }
                    if ((rowType == "TWR8") && isATCT)
                    {
                        // Airport airspace Class - ignore if we aren't saving this facility type
                        // Find the first 'Y' position for the class, then apply that position to the Classes string
                        // (Airfields can be multiple classes depending on hours; we only want most complex)
                        int loc2 = Line.IndexOf("Y", 8, 4);
                        if (loc2 != -1) tempClass = Classes[loc2 - 8].ToString();
                    }
                }
            }
            // Console.WriteLine("TWR rows read: " + TWR.Rows.Count);
        }

        private static string TWR3Freq(string Line, string Usage)
        {
            string Result = string.Empty; int loc1;
            int tab = 8;                                           // first frequency location
            int tablength = 94; int freqlength = 44;               // Full field is 44+50, freq field is 44
            while ((Result.Length == 0) && (tab + tablength < Line.Length))
            {
                loc1 = Line.IndexOf(Usage, tab, tablength);
                if (loc1 != -1)
                {
                    Result = Line.Substring(tab, freqlength).Trim();
                    loc1 = Result.IndexOf(";");
                    if (loc1 != -1)
                        Result = Result.Substring(0, loc1 - 1).Trim();
                    // Some entries have a character suffix on the freq
                    bool canConvert = false; decimal testResult;
                    while (!canConvert)
                    {
                        canConvert = decimal.TryParse(Result, out testResult);
                        if (!canConvert)
                        {
                            Result = Result.Substring(0, Result.Length - 1);
                        }
                    }
                    if ((Convert.ToSingle(Result) > 137f) || (Convert.ToSingle(Result) < 108f))
                        Result = string.Empty;
                }
                tab += tablength;
            }
            return Result;
        }

        private static bool TWR3isDatis(string Line)
        {
            int tab = 8;                                           // first frequency location
            string Usage = "D-ATIS";
            return (Line.IndexOf(Usage, tab) != -1);
        }

        public static void FillAWY()
        {
            DataTable AWY = Form1.AWY;
            string FullFilename = GetFullPathname(FolderMgt.DataFolder, "AWY.txt");
            string Line; string aNAVtype = string.Empty; string aNAVID;
            string aSeqNo = string.Empty; bool aFix; string aARTCC = string.Empty; string aMOCA = string.Empty;
            string aMEA = string.Empty; string aMAA = string.Empty; string atype = string.Empty;
            double aLat; double aLong; string aID = string.Empty;
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
                            aLat = Conversions.String2DecDeg(Line.Substring(83, 14).Trim(), "-");      // Latitude
                            aLong = Conversions.String2DecDeg(Line.Substring(97, 14).Trim(), "-");      // Longitude
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
            Console.WriteLine("AWY rows read: " + AWY.Rows.Count.ToString());
        }
        public static void FillStarDP()
        {
            DataTable SSD = Form1.SSD;
            string FullFilename = GetFullPathname(FolderMgt.DataFolder, "STARDP.txt");
            bool isSid; string Line; int Seqno = 0;
            using (StreamReader reader = new StreamReader(FullFilename))
            {
                while ((Line = reader.ReadLine()) != null)
                {
                    if (Line.Substring(0, 1) == "D") isSid = true; else isSid = false;
                    Seqno += 10;
                    var FixItems = new List<object>
                    {
                        Line.Substring(0,5).Trim(),                         // Internal ID
                        Line.Substring(30,6).Trim(),                        // NavAid or Airport
                        Line.Substring(10,2).Trim(),                        // FixType incl 'AA'
                        Conversions.String2DecDeg(Line.Substring(13, 8).Trim()),    // Latitude
                        Conversions.String2DecDeg(Line.Substring(21, 9).Trim()),    // Longitude
                        Line.Substring(38, 13).Trim(),                      // SSDCode
                        Line.Substring(51, 110).Trim(),                     // SSDName
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
        public static bool FillLocalSectors()
        {
            string Line; string Info; string SectorID = string.Empty; string SectorName = string.Empty;
            string SectorAbbr = string.Empty; string SectorLevel = string.Empty; bool Exclude = false;
            string SectorBase = string.Empty; string SectorTop = string.Empty; bool Success = true;
            string Lat0; string Long0; bool PenUp = false; string Item; int LineNo = 0;
            string FullFilename = GetFullPathname(FolderMgt.DataFolder, "LocalSectors.txt");
            string Message = "Invalid context at/near LocalSectors.txt line "; string NewMessage = string.Empty;
            MessageBoxButtons buttons = MessageBoxButtons.OK;
            MessageBoxIcon icon = MessageBoxIcon.Warning;
            int LatStart; int LongStart;
            DataTable LS = Form1.LocalSector; 
            using (StreamReader reader = new StreamReader(FullFilename))
            {
                while ((Line = reader.ReadLine()) != null)
                {
                    LineNo++;
                    if (Line.Trim().Length != 0)
                    {
                        if (Line.Substring(0, 1) != ";")
                        {
                            if (Line.Substring(0, 1) == "<")
                            {
                                Item = Line.Substring(Line.IndexOf("<") + 1, Line.IndexOf(">") - 1).Trim();
                                Info = Line.Substring(Line.IndexOf(">") + 1, Line.Length - Line.IndexOf(">") - 1).Trim();
                                switch (Item)
                                {
                                    case "SECTOR":
                                        SectorID = Info;
                                        break;
                                    case "NAME":
                                        SectorName = Info;
                                        break;
                                    case "ABBR":
                                        SectorAbbr = Info;
                                        break;
                                    case "LEVEL":
                                        SectorLevel = Info;
                                        break;
                                    case "BASE":
                                        SectorBase = Info;
                                        break;
                                    case "TOP":
                                        SectorTop = Info;
                                        break;
                                    case "NEXT":
                                        PenUp = true;
                                        Exclude = false;
                                        break;
                                    case "NOT":
                                        Exclude = true;
                                        break;
                                    case "LL":
                                        LatStart = Line.IndexOf(">") + 2;
                                        LongStart = LatStart + 11;
                                        if (Line.Length < LongStart + 11)
                                        {
                                            NewMessage += Message + LineNo + Environment.NewLine + Line + Environment.NewLine;
                                            MessageBox.Show(NewMessage, VersionInfo.Title, buttons, icon);
                                            Success = false;
                                            break;
                                        }
                                        Lat0 = Line.Substring(LatStart, 10).Trim();
                                        Long0 = Line.Substring(LongStart, 11).Trim();
                                        if (!Extensions.IsNumeric(Extensions.Right(Lat0, 1)) ||
                                            !Extensions.IsNumeric(Extensions.Right(Long0, 1)))
                                        {
                                            NewMessage += Message + LineNo + Environment.NewLine + Line + Environment.NewLine;
                                            MessageBox.Show(NewMessage, VersionInfo.Title, buttons, icon);
                                            Success = false;
                                            break;
                                        }
                                        if (PenUp)
                                        {
                                            var NewItems = new List<object>
                                            {
                                                SectorID,
                                                SectorName,
                                                SectorAbbr,
                                                SectorLevel,
                                                SectorBase,
                                                SectorTop,
                                                -1f,
                                                -1f,
                                                Exclude,
                                            };
                                            AddFixes(LS, NewItems);
                                            PenUp = false;
                                        }
                                        var FixItems = new List<object>
                                        {
                                            SectorID,
                                            SectorName,
                                            SectorAbbr,
                                            SectorLevel,
                                            SectorBase,
                                            SectorTop,
                                            Conversions.String2DecDeg(Lat0," "),
                                            Conversions.String2DecDeg(Long0, " "),
                                            Exclude,
                                        };
                                        AddFixes(LS, FixItems);
                                        break;
                                    default:
                                        NewMessage += Message + LineNo + Environment.NewLine + Line + Environment.NewLine;
                                        MessageBox.Show(NewMessage, VersionInfo.Title, buttons, icon);
                                        Success = false;
                                        break;
                                }
                            }
                        }
                    }
                }
            }
            return Success;
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

        public static void FillAirSpace()
        {
            string FullFilename = GetFullPathname(FolderMgt.DataFolder, "openaip_airspace_united_states_us.aip");
            DataTable SUA = Form1.SUA;
            DataView dvSUA = new DataView(SUA);
            string Category = string.Empty; string ID = string.Empty; 
            string Country = string.Empty; string Name = string.Empty;
            string AltLimitTop = string.Empty; string AltLimitTop_Ref = string.Empty;
            string AltLimitTop_Unit = string.Empty; string AltLimitBottom = string.Empty;
            string AltLimitBottom_Ref = string.Empty; string AltLimitBottom_Unit = string.Empty;
            string Polygon = string.Empty; 
            XmlReader xmlReader = XmlReader.Create(FullFilename);
            while (!xmlReader.EOF)
            {
                xmlReader.Read();
                if (xmlReader.NodeType == XmlNodeType.Element)
                {
                    switch (xmlReader.Name)
                    {
                        case "ASP":
                            Category=xmlReader.GetAttribute("CATEGORY");
                            break;
                        case "ID":
                            xmlReader.Read();
                            ID=xmlReader.Value;
                            break;
                        case "COUNTRY":
                            xmlReader.Read();
                            Country = xmlReader.Value;
                            break;
                        case "NAME":
                            xmlReader.Read();
                            Name = xmlReader.Value;
                            break;
                        case "ALTLIMIT_TOP":
                            AltLimitTop_Ref = xmlReader.GetAttribute("REFERENCE");
                            while (xmlReader.Name != "ALT")
                                xmlReader.Read();
                            AltLimitTop_Unit=xmlReader.GetAttribute("UNIT");
                            xmlReader.Read();
                            AltLimitTop = xmlReader.Value;
                            break;
                        case "ALTLIMIT_BOTTOM":
                            AltLimitBottom_Ref = xmlReader.GetAttribute("REFERENCE");
                            while (xmlReader.Name != "ALT")
                                xmlReader.Read();
                            AltLimitBottom_Unit=xmlReader.GetAttribute("UNIT");
                            xmlReader.Read();
                            AltLimitBottom = xmlReader.Value;
                            break;
                        case "POLYGON":
                            xmlReader.Read();
                            Polygon = xmlReader.Value;
                            break;
                    }
                }
                if ((xmlReader.NodeType == XmlNodeType.EndElement) && (xmlReader.Name == "ASP") )
                {
                    DataRowView newrow = dvSUA.AddNew();
                    newrow["ID"] = ID;
                    newrow["Category"] = Category;
                    newrow["Country"] = Country;
                    newrow["Name"] = Name;
                    newrow["AltLimit_Top"] = AltLimitTop;
                    newrow["AltLimit_Top_Ref"] = AltLimitTop_Ref;
                    newrow["AltLimit_Top_Unit"] = AltLimitTop_Unit;
                    newrow["AltLimit_Bottom"] = AltLimitBottom;
                    newrow["AltLimit_Bottom_Ref"] = AltLimitBottom_Ref;
                    newrow["AltLimit_Bottom_Unit"] = AltLimitBottom_Unit;
                    newrow["Polygon"] = Polygon;
                    // Find the limit of this polygon for later use in selecting airspaces
                    Conversions.BorderCoord(Polygon, out double North, out double South, out double East, out double West);
                    newrow["Latitude_North"] = North;
                    newrow["Latitude_South"] = South;
                    newrow["Longitude_East"] = East;
                    newrow["Longitude_West"] = West;
                    newrow.EndEdit();
                }
            }
            //DataTable dataTable = dvSUA.ToTable(true, "Category");
            //foreach (DataRow dataRow in dataTable.Rows)
            //    Console.WriteLine(dataRow[0]);
            //dataTable.Dispose();
        }
    }
}
