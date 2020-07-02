using System;
using System.IO;
using System.Windows.Forms;
using System.Data;
using System.Linq;
using System.Collections.Generic;
using System.Media;
using System.Globalization;
using System.Diagnostics.Eventing.Reader;
using MySqlX.XDevAPI.Common;
using Mono.Cecil;
using System.Diagnostics;
using System.Collections.Specialized;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using MySqlX.XDevAPI.Relational;
using System.Security.Permissions;

namespace SCTBuilder
{
    class SCToutput
    {
        static string CycleHeader;
        static readonly string cr = Environment.NewLine;
        static List<object> FixData = new List<object>();
        static List<object> VORData = new List<object>();
        static List<object> NDBData = new List<object>();
        static List<string> SSDlines = new List<string>();
        static List<string> FixesUsed = new List<string>();
        static List<string> APTsUsed = new List<string>();
        static string BigResult = string.Empty;

        public static void WriteSCT()
        {
            // DataTable LS = Form1.LocalSector;
            var TextFiles = new List<string>();
            string Message;

            Debug.WriteLine("Header...");
            string path = CheckFile("Header");
            WriteHeader(path);
            TextFiles.Add(path);

            path = CheckFile("Colors");
            Debug.WriteLine("ColorDefinitions");
            WriteColors(path);
            TextFiles.Add(path);

            path = CheckFile("Info");
            Debug.WriteLine("INFO section...");
            WriteINFO(path);
            TextFiles.Add(path);

            if (SCTchecked.ChkVOR)
            {
                path = CheckFile("VOR");
                Debug.WriteLine("VORs...");
                WriteVOR(path);
                TextFiles.Add(path);
            }
            if (SCTchecked.ChkNDB)
            {
                path = CheckFile("NDB");
                Debug.WriteLine("NDBs...");
                WriteNDB(path);
                TextFiles.Add(path);
            }
            if (SCTchecked.ChkAPT)
            {
                path = CheckFile("APT");
                Debug.WriteLine("Airports...");
                WriteAPT(path);
                TextFiles.Add(path);
            }
            if (SCTchecked.ChkRWY)
            {
                path = CheckFile("RWY");
                Debug.WriteLine("Airport Runways...");
                WriteRWY(path);
                TextFiles.Add(path);
            }
            if (SCTchecked.ChkFIX)
            {
                path = CheckFile("FIX");
                Debug.WriteLine("Fixes...");
                WriteFixes(path);
                TextFiles.Add(path);
            }
            if (SCTchecked.ChkARB)
            {
                Debug.WriteLine("ARTCC HIGH...");
                path = CheckFile("ARTCC_HIGH");
                WriteARB(path, true);
                TextFiles.Add(path);
            }
            if (SCTchecked.ChkARB)
            {
                Debug.WriteLine("ARTCC LOW...");
                path = CheckFile("ARTCC_LOW");
                WriteARB(path, false);
                TextFiles.Add(path);
            }
            if (SCTchecked.ChkAWY)
            {
                path = CheckFile("AirwayLow");
                Debug.WriteLine("Low AirWays...");
                WriteAWY(path, IsLow: true);
                TextFiles.Add(path);
            }

            if (SCTchecked.ChkAWY)
            {
                path = CheckFile("AirwayHigh");
                Debug.WriteLine("High AirWays...");
                WriteAWY(path, IsLow: false);
                TextFiles.Add(path);
            }
            if (SCTchecked.ChkSID)
            {
                Debug.WriteLine("SIDS...");
                WriteSIDSTAR(IsSID: true);
                TextFiles.Add(path);
            }
            if (SCTchecked.ChkSTAR)
            {
                Debug.WriteLine("STARS...");
                WriteSIDSTAR(IsSID: false);
                TextFiles.Add(path);
            }
            path = CheckFile("SUA");
            if (path != string.Empty)
            {
                Debug.WriteLine("SUAs...");
                WriteSUA();
                TextFiles.Add(path);
            }

            if (SCTchecked.ChkALL)
            {
                path = CheckFile(CycleInfo.AIRAC.ToString(), ".sct2");
                using (var SCTfile = File.Create(path))
                {
                    foreach (var file in TextFiles)
                    {
                        using (var input = File.OpenRead(file))
                        {
                            input.CopyTo(SCTfile);
                        }
                    }
                }
                Message = "Sector file written to" + path;
                SCTcommon.SendMessage(Message, MessageBoxIcon.Information);
            }
            else
            {
                Message = TextFiles.Count + " text file(s) written to " + FolderMgt.OutputFolder;
                SCTcommon.SendMessage(Message, MessageBoxIcon.Information);
            }
            Console.WriteLine("End writing output files");
        }

        public static string CheckFile(string file, string type = ".txt")
        {
            // Looks for the file in the PartialPath.  If found, optionally seeks confirm to overwrite.
            // RETURNS the fully qualified path to the file unles overwite denied, then returns empty.
            string caption = VersionInfo.Title;
            string Message; MessageBoxIcon icon; MessageBoxButtons buttons;
            DialogResult result;
            string PartialPath = FolderMgt.OutputFolder + "\\" + InfoSection.SponsorARTCC + "_";
            string path = PartialPath + file + type;
            if (File.Exists(path))
            {
                if (SCTchecked.ChkConfirmOverwrite)
                {
                    Message = "OK to overwrite " + path + "?";
                    icon = MessageBoxIcon.Question;
                    buttons = MessageBoxButtons.YesNo;
                    SystemSounds.Question.Play();
                    result = MessageBox.Show(Message, caption, buttons, icon);
                }
                else result = DialogResult.Yes;
                if (result == DialogResult.Yes)
                {
                    File.Delete(path);
                    result = DialogResult.OK;
                }
                else
                {
                    result = DialogResult.Cancel;
                }
            }
            else
                result = DialogResult.OK;
            if (result == DialogResult.OK)
            {
                return path;
            }
            else return string.Empty;
        }

        private static void WriteHeader(string path)
        {
            using (StreamWriter sw = File.CreateText(path))
            {
                string Message =
                ";              ** Not for real world navigation **" + cr +
                "; File may be distributed only as freeware." + cr +
                "; Provided 'as is' - use at your own risk." + cr + cr +
                "; Software-generated sector file using " + VersionInfo.Title + cr +
                "; For questions, contact Donald Kowalewski at www.zjcartcc.org" + cr +
                "; Sponsoring ARTCC: " + InfoSection.SponsorARTCC + cr +
                "; Facilities Engineer: " + InfoSection.FacilityEngineer + cr +
                "; Assistant Facilities Engineer:" + InfoSection.AsstFacilityEngineer + cr +
                "; AIRAC CYCLE: " + CycleInfo.AIRAC + cr +
                "; Cycle: " + CycleInfo.CycleStart + " to " + CycleInfo.CycleEnd + cr + cr +
                "; <Add last modified and contributers from prior file>" + cr + cr;
                sw.WriteLine(Message);
            }
            CycleHeader =
                "; ================================================================" + cr +
                "; AIRAC CYCLE: " + CycleInfo.AIRAC + cr +
                "; Cycle: " + CycleInfo.CycleStart + " to " + CycleInfo.CycleEnd + cr +
                "; ================================================================" + cr;
        }
        private static void WriteColors(string path)
        {
            DataTable dtColors = Form1.Colors;
            string Output = "; Color definition table" + cr;
            Debug.WriteLine(dtColors.Rows.Count + " colors in color table");
            foreach (DataRow row in dtColors.Rows)
            {
                Output += "#define " + row[0] + " " + row[1] + cr;
            }
            using (StreamWriter sw = new StreamWriter(path))
                sw.WriteLine(Output);
        }
        private static void WriteINFO(string path)
        {
            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.WriteLine();
                sw.WriteLine("[INFO]");
                sw.WriteLine(InfoSection.SectorName);
                sw.WriteLine(InfoSection.DefaultPosition);
                sw.WriteLine(InfoSection.DefaultAirport);
                sw.WriteLine(InfoSection.CenterLatitude_SCT);
                sw.WriteLine(InfoSection.CenterLongitude_SCT);
                sw.WriteLine(InfoSection.NMperDegreeLatitude);
                sw.WriteLine(InfoSection.NMperDegreeLongitude.ToString("F1", CultureInfo.InvariantCulture));
                sw.WriteLine(InfoSection.MagneticVariation);
                sw.WriteLine(InfoSection.SectorScale);
            }
        }
        private static void WriteVOR(string path)
        {
            string[] strOut = new string[5];
            DataView dataView = new DataView(Form1.VOR)
            {
                RowFilter = "[Selected]",
                Sort = "FacilityID"
            };
            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.WriteLine(CycleHeader);
                sw.WriteLine("[VOR]");
                foreach (DataRowView row in dataView)
                {
                    strOut[0] = row["FacilityID"].ToString();
                    strOut[1] = string.Format("{0:000.000}", row["Frequency"]);
                    strOut[2] = Conversions.DecDeg2SCT(Convert.ToSingle(row["Latitude"]), true);
                    strOut[3] = Conversions.DecDeg2SCT(Convert.ToSingle(row["Longitude"]), false);
                    strOut[4] = row["Name"].ToString();
                    if (!(strOut[2] + strOut[3]).Contains("-1 "))      // Do NOT write VORs having no fix
                        sw.WriteLine(SCTstrings.VORout(strOut));
                }
                dataView.Dispose();
            }
        }
        private static void WriteNDB(string path)
        {
            string[] strOut = new string[5]; string LineOut;
            DataTable NDB = Form1.NDB;
            DataView dataView = new DataView(NDB)
            {
                RowFilter = "[Selected]",
                Sort = "FacilityID"
            };
            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.WriteLine(CycleHeader);
                sw.WriteLine("[NDB]");
                foreach (DataRowView row in dataView)
                {
                    strOut[0] = row["FacilityID"].ToString().PadRight(3);
                    strOut[1] = string.Format("{0:000.000}", row["Frequency"]);
                    strOut[2] = Conversions.DecDeg2SCT(Convert.ToSingle(row["Latitude"]), true);
                    strOut[3] = Conversions.DecDeg2SCT(Convert.ToSingle(row["Longitude"]), false);
                    strOut[4] = row["Name"].ToString();
                    LineOut = strOut[0] + " " + strOut[1] + " " +
                        strOut[2] + " " + strOut[3] + " ;" + strOut[4];
                    if (!(strOut[2] + strOut[3]).Contains("-1 "))      // Do NOT write NDBs having no fix
                        sw.WriteLine(SCTstrings.NDBout(strOut));
                }
                dataView.Dispose();
            }
        }
        private static void WriteAPT(string path)
        {
            string[] strOut = new string[7]; string ATIStype = "ATIS";
            DataTable APT = Form1.APT;
            DataTable TWR = Form1.TWR;
            DataView dvTWR = new DataView(TWR);
            DataView dvAPT = new DataView(APT)
            {
                RowFilter = "[Selected]",
                Sort = "FacilityID"
            };
            // Output only what we need
            DataTable dataTable = dvAPT.ToTable(true, "ID", "FacilityID", "Latitude", "Longitude", "Name", "Public");
            DataRow foundRow; string LCL; string ATIS;
            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.WriteLine(CycleHeader);
                sw.WriteLine("[AIRPORT]");
                foreach (DataRow row in dataTable.AsEnumerable())
                {
                    strOut[0] = Conversions.ICOA(row["FacilityID"].ToString()).PadRight(4);
                    dvTWR.Sort = "ID";
                    foundRow = TWR.Rows.Find(row["ID"]);
                    if (foundRow != null)
                    {
                        LCL = foundRow["LCLfreq"].ToString();
                        ATIS = foundRow["ATISfreq"].ToString();
                        if (Convert.ToBoolean(foundRow["IsD-ATIS"])) ATIStype = "D-ATIS:"; else ATIStype = "ATIS:";
                    }
                    else
                    {
                        if (Convert.ToBoolean(row["Public"]))
                            LCL = "122.8";
                        else
                            LCL = "0";
                        ATIS = string.Empty;
                    }
                    strOut[1] = LCL.PadRight(7);
                    strOut[2] = Conversions.DecDeg2SCT(Convert.ToSingle(row["Latitude"]), true);
                    strOut[3] = Conversions.DecDeg2SCT(Convert.ToSingle(row["Longitude"]), false);
                    strOut[4] = row["Name"].ToString();
                    if (Convert.ToBoolean(row["Public"]))
                        strOut[5] = " (Public) ";
                    else
                        strOut[5] = " {Private} ";
                    strOut[6] = ATIS;
                    if (ATIS.Length != 0) strOut[6] = ATIStype + strOut[6];
                    else strOut[6] = string.Empty;
                    if (!(strOut[2] + " " + strOut[3]).Contains("-1 "))      // Do NOT write APTs having no fix
                        sw.WriteLine(SCTstrings.APTout(strOut));
                }
            }
            dvAPT.Dispose();
        }
        private static void WriteFixes(string path)
        {
            string[] strOut = new string[5];
            DataTable FIX = Form1.FIX;
            DataView dataView = new DataView(FIX)
            {
                RowFilter = "[Selected]",
                Sort = "FacilityID"
            };
            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.WriteLine(CycleHeader);
                sw.WriteLine("[FIXES]");
                foreach (DataRowView row in dataView)
                {
                    strOut[0] = row["FacilityID"].ToString();
                    strOut[2] = Conversions.DecDeg2SCT(Convert.ToSingle(row["Latitude"]), true);
                    strOut[3] = Conversions.DecDeg2SCT(Convert.ToSingle(row["Longitude"]), false);
                    strOut[4] = row["Use"].ToString();
                    sw.WriteLine(SCTstrings.FIXout(strOut));  // Uses 0, 2, 3, and 4
                }
            }
            dataView.Dispose();
        }

        private static void WriteRWY(string path)
        {
            string[] strOut = new string[9]; string FacID = string.Empty;
            string RWYtextColor = TextColors.RWYTextColor; double MagBHdg; double MagEHdg;
            bool FirstLine = true; string FacFullName = string.Empty;
            DataTable DRAW = new SCTdata.DrawLabelDataTable();
            DataTable RWY = Form1.RWY;
            DataView dvRWY = new DataView(RWY)
            {
                RowFilter = "[Selected]",
                Sort = "FacilityID, BaseIdentifier"
            };
            using (StreamWriter sw = new StreamWriter(path))
            {
                foreach (DataRowView row in dvRWY)
                {
                    if (row["FacilityID"].ToString() != FacID)
                    {
                        if (FirstLine)
                        {
                            sw.WriteLine(CycleHeader);
                            sw.WriteLine("[RUNWAY]");
                            FirstLine = false;
                        }
                        FacID = row["FacilityID"].ToString();
                        FacFullName = FacID + '-' + row["FacilityName"].ToString();
                    }
                    // FAA bearings are in "True" format and must be converted to "Magnetic"
                    strOut[0] = row["BaseIdentifier"].ToString().Trim().PadRight(3);
                    strOut[1] = row["EndIdentifier"].ToString().Trim().PadRight(3);
                    MagBHdg = Convert.ToDouble(row["BaseHeading"]) + InfoSection.MagneticVariation;
                    if (MagBHdg > 360) MagBHdg %= 360;
                    else if (MagBHdg < 0)
                        MagBHdg = (MagBHdg + 360) % 360;
                    if (MagBHdg == 0) MagBHdg = 360;
                    strOut[2] = Convert.ToString(MagBHdg).PadRight(3);
                    MagEHdg = Convert.ToDouble(row["EndHeading"]) + InfoSection.MagneticVariation;
                    if (MagEHdg > 360) MagEHdg %= 360;
                    else if (MagBHdg < 0) MagEHdg = (MagEHdg + 360) % 360;
                    if (MagBHdg == 0) MagBHdg = 360;
                    strOut[3] = Convert.ToString(MagEHdg).PadRight(3);
                    strOut[4] = Conversions.DecDeg2SCT(Convert.ToSingle(row["Latitude"]), true);
                    strOut[5] = Conversions.DecDeg2SCT(Convert.ToSingle(row["Longitude"]), false);
                    strOut[6] = Conversions.DecDeg2SCT(Convert.ToSingle(row["EndLatitude"]), true);
                    strOut[7] = Conversions.DecDeg2SCT(Convert.ToSingle(row["EndLongitude"]), false);
                    strOut[8] = FacFullName;
                    sw.WriteLine(SCTstrings.RWYout(strOut));
                    DRAW.Rows.Add(new object[] { strOut[0].ToString(), strOut[4].ToString(), strOut[5].ToString(), RWYtextColor, FacFullName });
                    DRAW.Rows.Add(new object[] { strOut[1].ToString(), strOut[6].ToString(), strOut[7].ToString(), RWYtextColor, FacFullName });
                }
                WriteLabels(DRAW, sw);
            }
            dvRWY.Dispose();
        }
        private static void WriteAWY(string path, bool IsLow)
        {
            DataTable AWY = Form1.AWY;
            string Awy0 = string.Empty; string Awy1;
            string NavAid0 = string.Empty; string NavAid1;
            string Lat0 = string.Empty; string Lat1;
            string Lon0 = string.Empty; string Lon1;
            bool IsBreak;
            string filter = "[Selected]";
            if (IsLow) filter += " AND [IsLow]";
            else filter += " AND NOT [IsLow]";
            DataView dvAWY = new DataView(AWY)
            {
                RowFilter = filter,
                Sort = "AWYID, Sequence",
            };
            Debug.WriteLine("Found " + dvAWY.Count + " rows");
            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.WriteLine(CycleHeader);
                if (IsLow) sw.WriteLine("[LOW AIRWAY]");
                else sw.WriteLine("[HIGH AIRWAY]");
                foreach (DataRowView rowAWY in dvAWY)
                {
                    Awy1 = rowAWY["AWYID"].ToString();
                    NavAid1 = rowAWY["NAVAID"].ToString();
                    Lat1 = Conversions.DecDeg2SCT(Convert.ToSingle(rowAWY["Latitude"]), true);
                    Lon1 = Conversions.DecDeg2SCT(Convert.ToSingle(rowAWY["Longitude"]), false);
                    // Continuing segment -is this a break in airway?
                    IsBreak = (bool)rowAWY["IsBreak"];
                    if ((Awy1 == Awy0) && !IsBreak)
                        sw.WriteLine(SCTstrings.AWYout(Awy1, Lat0, Lon0, Lat1, Lon1, NavAid0, NavAid1));
                    NavAid0 = NavAid1;
                    Lat0 = Lat1;
                    Lon0 = Lon1;
                    Awy0 = Awy1;
                }
            }
            dvAWY.Dispose();
        }

        private static void WriteSIDSTAR(bool IsSID)
        {
            // Calling routine for SID and STAR diagrams
            // SSD = Either SID or STAR, depending on flag
            string path;
            string SSDfilter;
            string Section = "STAR"; if (IsSID) Section = "SID";

            DataTable SSD = Form1.SSD;
            // Get the list of Selected SSDs
            if (IsSID) SSDfilter = "[IsSID]"; else SSDfilter = "NOT [IsSID]";
            SSDfilter += " AND [Selected]";
            DataView dvSSD = new DataView(SSD)
            {
                RowFilter = SSDfilter,
                Sort = "ID",
            };
            DataTable SSDlist = dvSSD.ToTable(true, "ID");

            // Loop the SSDIDs. Create a text file for each one...
            // No other way to manage the one-file and multifile option

            if (InfoSection.OneFilePerSidStar)
            {
                foreach (DataRow drSSDlist in SSDlist.Rows)
                {
                    
                    // Clear the prior data, if any
                    FixData = new List<object>();
                    VORData = new List<object>();
                    NDBData = new List<object>();
                    SSDlines = new List<string>();
                    FixesUsed = new List<string>();
                    APTsUsed = new List<string>();
                    BigResult = string.Empty;
                    // Get the SSD in question - on this loop, the path is the SSDname
                    dvSSD.RowFilter = "ID = '" + drSSDlist[0].ToString() + "'";
                    // Pass the single row of SSD data to the writing programs
                    if (dvSSD.Count != 0)
                        BuildSSD(dvSSD);
                    // Creating a file for each dvSSD...
                    path = CheckFile(dvSSD[0]["SSDcode"].ToString());
                    using (StreamWriter sw = new StreamWriter(path))
                    {
                        sw.WriteLine(CycleHeader);
                        if (InfoSection.IncludeSidStarReferences)
                            WriteSSDrefs(sw);
                        // Is this a SID or STAR section?  The 1st character will tell
                        sw.WriteLine(cr + "[" + Section + "]");
                        WriteSSDLines(sw);          // Since Labels depend on lines, call them there
                    }
                }
            }
            else
            {
                FixData = new List<object>();
                VORData = new List<object>();
                NDBData = new List<object>();
                SSDlines = new List<string>();
                FixesUsed = new List<string>();
                APTsUsed = new List<string>();
                BigResult = string.Empty;
                foreach (DataRow drSSDlist in SSDlist.Rows)
                {
                    // Get the SSD in question - on this loop, the path is the SSDname
                    dvSSD.RowFilter = "ID = '" + drSSDlist[0].ToString() + "'";
                    if (dvSSD.Count != 0)
                        BuildSSD(dvSSD);
                }
                // Create ONE file for all the SSDs
                path = CheckFile(Section);
                using (StreamWriter sw = new StreamWriter(path))
                {
                    sw.WriteLine(CycleHeader);
                    if (InfoSection.IncludeSidStarReferences)
                        WriteSSDrefs(sw);
                    // Is this a SID or STAR section?  The 1st character will tell
                    sw.WriteLine("[" + Section + "]");
                    WriteSSDLines(sw);          // Since Labels depend on lines, call them there
                }
            }
            dvSSD.Dispose();
            SSDlist.Dispose();
        }

        public static void BuildSSD (DataView dvSSD)
        {
            // Builds ONE SID or STAR from ONE SSD dataview (preselected)
            // RETURNS a string for the diagram
            // Everything goes in List<string>s first
            object[] NavData;

            // Various and sundry variables for the loop
            double Lat1 = -1; double Lon1 = -1; string space = new string(' ', 27);
            double Lat0 = -1; double Lon0 = -1;
            string lastFix = string.Empty; string curFix; string FixType0;
            string FixType1; string SSDname; string TransitionName;
            string SSDcode; string TransitionCode;
            int FixCount0; int FixCount1;

            // Get the name and code for this SSD
            SSDname = dvSSD[0]["SSDName"].ToString();
            SSDcode = dvSSD[0]["SSDcode"].ToString();

            SSDlines.Add(cr);
            SSDlines.Add(SSDHeader(SSDcode, "(" + SSDname + ")", 1, '-'));

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
                        // Debug.WriteLine(SCTstrings.SSDout(Lat0, Lon0, Lat1, Lon1, lastFix, curFix, InfoSection.UseFixesAsCoords));
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

        public static void WriteSSDrefs(StreamWriter sw)
        {
            // Sends the results of BuildSSD to the designated file
            // This is the header references
            // Write the file for this SSD
            string[] strOut = new string[6];
            sw.WriteLine(cr + "[AIRPORT]");
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
                strOut[2] = Conversions.DecDeg2SCT(Convert.ToDouble(dvAPT[0]["Latitude"]), true);
                strOut[3] = Conversions.DecDeg2SCT(Convert.ToDouble(dvAPT[0]["Longitude"]), false);
                strOut[4] = dvAPT[0]["Name"].ToString();
                sw.WriteLine(SCTstrings.APTout(strOut.ToArray()));
            }
            dvAPT.Dispose();
            dvTWR.Dispose();
            // Write the NavAids for this curFix
            // Fix, Frequency(opt), Latitude, Longitude, Name, FixType
            if (VORData.Count > 0)
            {
                // NavData: ID(opt), FacilityID, Frequency(opt), Latitude, Longitude, Name, FixType
                sw.WriteLine(cr + "[VOR]");
                foreach (object[] VORs in VORData)
                {
                    // strOut expects 0-Fix, 1-Freq, 2-Lat, 3-Lon, 4-Name, 5-Type
                    strOut[0] = VORs[1].ToString();
                    strOut[1] = VORs[2].ToString();
                    strOut[2] = Conversions.DecDeg2SCT(Convert.ToDouble(VORs[3]), true);
                    strOut[3] = Conversions.DecDeg2SCT(Convert.ToDouble(VORs[4]), false);
                    strOut[4] = VORs[5].ToString();
                    strOut[5] = VORs[6].ToString();
                    sw.WriteLine(SCTstrings.VORout(strOut));
                }
            }
            // NavData: ID(opt), FacilityID, Frequency(opt), Latitude, Longitude, Name, FixType
            if (NDBData.Count > 0)
            {
                sw.WriteLine(cr + "[NDB]");
                foreach (object[] NDBs in NDBData)
                {
                    strOut[0] = NDBs[1].ToString();
                    strOut[1] = NDBs[2].ToString();
                    strOut[2] = Conversions.DecDeg2SCT(Convert.ToDouble(NDBs[3]), true);
                    strOut[3] = Conversions.DecDeg2SCT(Convert.ToDouble(NDBs[4]), false);
                    strOut[4] = NDBs[5].ToString();
                    strOut[5] = NDBs[6].ToString();
                    sw.WriteLine(SCTstrings.NDBout(strOut));
                }
            }
            // NavData: ID(opt), FacilityID, Frequency(opt), Latitude, Longitude, FixUse, FixType
            if (FixData.Count > 0)
            {
                sw.WriteLine(cr + "[FIXES]");
                foreach (object[] FIXes in FixData)
                {
                    strOut[0] = FIXes[1].ToString();
                    strOut[2] = Conversions.DecDeg2SCT(Convert.ToDouble(FIXes[3]), true);
                    strOut[3] = Conversions.DecDeg2SCT(Convert.ToDouble(FIXes[4]), false);
                    strOut[4] = FIXes[5].ToString();
                    sw.WriteLine(SCTstrings.FIXout(strOut));
                }
            }
        }

        private static void WriteSSDLines(StreamWriter sw)
        {
            foreach (string Line in SSDlines)
            {
                sw.WriteLine(Line);         // These have crs on each line
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


        private static string DrawFixInfo(List<string> FixNames, int Angle = 0, float Scale = 1f)
        {
            // Calling function for fixes in diagrams
            // Because labels and/or symbols might be drawn
            // Will need to add the DrawFixNames for ALT and SPEED here
            string result = string.Empty;
            float Latitude;
            float Longitude;
            float[] AdjustedCoords = null;
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
                            AdjustedCoords = Hershey.Adjust(Latitude, Longitude, 15, -30);
                        }
                        if (InfoSection.DrawFixLabelsOnDiagrams)
                        {
                            if (!InfoSection.DrawFixSymbolsOnDiagrams)
                                AdjustedCoords = Hershey.Adjust(Latitude, Longitude, 5, 0);
                            result += Hershey.WriteHF(Fix, AdjustedCoords[0], AdjustedCoords[1], Angle, Scale);
                        }
                    }
                }
            }
            return result;
        }

        private static void WriteARB(string path, bool High)
        {
            // This doesn't work as designed.  Need to search for affected ARTCCs,
            // then draw all the ARTCCs (filter ARTCC =) with ANY borders in the area.
            // MAY want to do that in the "SELECTED" phase (dgvARB), then sort by ARTCC.
            DataTable ARB = Form1.ARB;
            string FacID0 = string.Empty; string FacID1;
            string ARBname; string HL;  string filter; string Sector;
            string Lat1; string Long1; string Descr1; string Descr0 = string.Empty;
            string Lat0 = string.Empty; string Long0 = string.Empty;
            string LatFirst; string LongFirst;
            string Output = Environment.NewLine;
            if (High)
            {
                filter = "[Selected] AND (" +
                    " ([DECODE] = 'UTA') OR " +
                    " ([DECODE] = 'FIR ONLY') OR " +
                    " ([DECODE] = 'BDRY') OR " +
                    " ([DECODE] = 'HIGH') )";
                HL = "_H_CTR";
                Sector = "HIGH";
            }
            else
            {
                filter = "([DECODE] = 'LOW') AND [Selected]";     //  
                HL = "_L_CTR";
                Sector = "LOW";
            }
            // First, find all the ARBs in the group (may be more than one)
            DataView ARBview = new DataView(ARB)
            {
                RowFilter = filter,
                Sort = "Sequence",
            };
            Console.WriteLine("ARB lines found: " + ARBview.Count);
            using (StreamWriter sw = new StreamWriter(path))
            {
                Output += "[ARTCC " + Sector + "]" + cr;
                if (ARBview.Count != 0)
                {
                    // Build a list of the boundaries. The last one always has "To Point of Beginning"
                    // OR... It's a different ARTCC
                    var ARBlist = new List<string>();
                    foreach (DataRowView ARBdataRowView in ARBview)
                    {
                        if (Lat0.Length == 0)                   // First point of line
                        {
                            Lat1 = Conversions.DecDeg2SCT(Convert.ToSingle(ARBdataRowView["Latitude"]), true);
                            Long1 = Conversions.DecDeg2SCT(Convert.ToSingle(ARBdataRowView["Longitude"]), false);
                            LatFirst = Lat1; LongFirst = Long1;             // Save the first point
                            Descr1 = ARBdataRowView["Description"].ToString();
                            ARBname = ARBdataRowView["Name"].ToString();   // Initialize AARTC name
                            FacID1 = ARBdataRowView["ARTCC"].ToString();      // Initialize FacID
                            Output += "; " + ARBname + cr;
                        }
                        else
                        {
                            FacID1 = ARBdataRowView["ARTCC"].ToString();
                            Descr1 = ARBdataRowView["Description"].ToString();
                            Lat1 = Conversions.DecDeg2SCT(Convert.ToSingle(ARBdataRowView["Latitude"]), true);
                            Long1 = Conversions.DecDeg2SCT(Convert.ToSingle(ARBdataRowView["Longitude"]), false);
                            if ((FacID0.Length != 0) && (FacID0 == FacID1))
                                Output += SCTstrings.BoundaryOut(FacID1 + HL, Lat0, Long0, Lat1, Long1, Descr0) + cr;
                        }
                        if (Descr1.IndexOf("POINT OF BEGINNING") != -1)    // Last line in this group
                        {
                            Output += SCTstrings.BoundaryOut(FacID1 + HL, Lat0, Long0, Lat1, Long1, Descr0) + cr;

                            sw.WriteLine(Output);
                            Lat1 = Long1 = FacID1 = Descr1 = Output = string.Empty;
                        }
                        if ((FacID0.Length != 0) && (FacID0 != FacID1))    // Changed ARTCC
                        {
                            // Do NOT add a line to close boundary
                            // Check for dual condition; end of group AND new ARTCC...
                            if (Output.Length !=0)
                            {
                                sw.WriteLine(Output);
                            }
                            Output = string.Empty;
                        }
                        Lat0 = Lat1; Long0 = Long1; FacID0 = FacID1; Descr0 = Descr1;
                    }
                }
            }
            ARBview.Dispose();
        }

        private static void WriteLabels(DataTable dtSTL, StreamWriter sw)
        {
            string strText; string Lat; string Long; string TextColor; string Comment;
            string Output;
            // string colorValue = dtColors.Rows[0]["ColorValue"].ToString();
            sw.WriteLine("[LABELS]");
            sw.WriteLine("; Runway labels");
            foreach (DataRow row in dtSTL.AsEnumerable())
            {
                strText = row["LabelText"].ToString();
                Lat = row["Latitude"].ToString();
                Long = row["Longitude"].ToString();
                TextColor = row["TextColor"].ToString();
                Comment = row["Comment"].ToString();
                if (row["Comment"].ToString().Length != 0)
                {
                    Output = SCTstrings.LabelOut(strText, Lat, Long, TextColor, Comment);
                }
                else
                    Output = SCTstrings.LabelOut(strText, Lat, Long, TextColor);
                sw.WriteLine(Output);
            }
        }

        public static void WriteLS_SID(DataTable LS)
        {
            ///<summary
            ///FE may build local sector files and have them written to the ARTCC, LOW ARTCC, or HIGH ARTCC
            ///The text file must be configured as shown in the "LocalSectors.txt" file in the package.
            ///</summary>
            string Lat0 = string.Empty; string Long0 = string.Empty; 
            string Lat1; string Long1; string blank = new string(' ', 27); string Line1; string Line2;
            string LineLL;
            string dummycoords = "N000.00.00.000 E000.00.00.000 N000.00.00.000 E000.00.00.000";
            string path = FolderMgt.OutputFolder + "\\" + InfoSection.SponsorARTCC + "_LocalSectors.sct2";
            var Low = new List<string>();
            var High = new List<string>();
            var Ultra = new List<string>();
            Low.Add(cr + "[ARTCC LOW]");
            High.Add(cr + "[ARTCC HIGH]");
            Ultra.Add(cr + "[ARTCC ULTRA]");
            foreach (DataRow dataRow in LS.Rows)
            {
                if (Convert.ToSingle(dataRow["Latitude"]) == -1f)
                {
                    Lat1 = Long1 = string.Empty;
                    string Spaces = new string(' ', 27 - dataRow["SectorID"].ToString().Length -
                                dataRow["Name"].ToString().Length - 1);
                    Line1 = "; " + dataRow["SectorID"].ToString() + " " + dataRow["Name"].ToString() +
                                " ARTCC-" + dataRow["Level"].ToString() + " From " + dataRow["Base"].ToString() +
                                " to " + dataRow["Top"].ToString();
                    Line2 = dataRow["SectorID"].ToString() + "_" + dataRow["Name"].ToString() +
                                Spaces + dummycoords;
                    switch (dataRow["Level"])
                    {
                        case "L":
                            Low.Add(Line1);
                            Low.Add(Line2);
                            break;
                        case "H":
                            High.Add(Line1);
                            High.Add(Line2);
                            break;
                        case "U":
                            Ultra.Add(Line1);
                            Ultra.Add(Line2);
                            break;
                    }
                }
                else
                {
                    Lat1 = Conversions.DecDeg2SCT(Convert.ToSingle(dataRow["Latitude"]), true);
                    Long1 = Conversions.DecDeg2SCT(Convert.ToSingle(dataRow["Longitude"]), false);
                }
                if ((Lat0 != string.Empty) && (Lat1 != string.Empty))
                {
                    LineLL = blank + Lat0 + " " + Long0 + " " + Lat1 + " " + Long1;
                    if (Convert.ToBoolean(dataRow["Exclude"])) LineLL += " Red";
                    switch (dataRow["Level"])
                    {
                        case "L":
                            Low.Add(LineLL);
                            break;
                        case "H":
                            High.Add(LineLL);
                            break;
                        case "U":
                            Ultra.Add(LineLL);
                            break;
                    }
                }
                Lat0 = Lat1; Long0 = Long1;
            }
            File.WriteAllLines(path, Low);
            File.AppendAllLines(path, High);
            File.AppendAllLines(path, Ultra);
        }
        private static void WriteSUA()
        {
            
        }
    }
}

