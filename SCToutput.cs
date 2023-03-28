using System;
using System.IO;
using System.Windows.Forms;
using System.Data;
using System.Linq;
using System.Collections.Generic;
using System.Media;
using System.Globalization;
using System.Diagnostics;

namespace SCTBuilder
{
    class SCToutput
    {
        private static readonly string cr = Environment.NewLine;
        public static void WriteSCT(Label UpdateLabel)
        {
            // DataTable LS = Form1.LocalSector;
            var TextFiles = new List<string>();
            string Message;
            string PartialPath = FolderMgt.OutputFolder + "\\" + InfoSection.SponsorARTCC + "_";
            Console.WriteLine("Header...");
            string path = SCTcommon.CheckFile(PartialPath, "Header");
            WriteHeader(path);
            TextFiles.Add(path);

            path = SCTcommon.CheckFile(PartialPath, "Colors");
            Console.WriteLine("ColorDefinitions");
            WriteColors(path);
            TextFiles.Add(path);

            path = SCTcommon.CheckFile(PartialPath, "Info");
            Console.WriteLine("INFO section...");
            WriteINFO(path);
            TextFiles.Add(path);

            if (SCTchecked.ChkVOR)
            {
                path = SCTcommon.CheckFile(PartialPath, "VOR");
                Console.WriteLine("VORs...");
                WriteVOR(path);
                TextFiles.Add(path);
            }
            if (SCTchecked.ChkNDB)
            {
                path = SCTcommon.CheckFile(PartialPath, "NDB");
                Console.WriteLine("NDBs...");
                WriteNDB(path);
                TextFiles.Add(path);
            }
            if (SCTchecked.ChkAPT)
            {
                path = SCTcommon.CheckFile(PartialPath, "APT");
                Console.WriteLine("Airports...");
                WriteAPT(path);
                TextFiles.Add(path);
            }
            if (SCTchecked.ChkRWY)
            {
                path = SCTcommon.CheckFile(PartialPath, "RWY");
                Console.WriteLine("Airport Runways...");
                WriteRWY(path);
                TextFiles.Add(path);
            }
            if (SCTchecked.ChkFIX)
            {
                path = SCTcommon.CheckFile(PartialPath, "FIX");
                Console.WriteLine("Fixes...");
                WriteFixes(path);
                TextFiles.Add(path);
            }
            if (SCTchecked.ChkARB)
            {
                Console.WriteLine("ARTCC HIGH...");
                path = SCTcommon.CheckFile(PartialPath, "ARTCC_HIGH");
                WriteARB(path, true);
                TextFiles.Add(path);

                Console.WriteLine("ARTCC LOW...");
                path = SCTcommon.CheckFile(PartialPath, "ARTCC_LOW");
                WriteARB(path, false);
                TextFiles.Add(path);
            }

            if (SCTchecked.ChkAWY)
            {
                path = SCTcommon.CheckFile(PartialPath, "AirwayLow");
                Console.WriteLine("Low AirWays...");
                WriteAWY(path, "Low");
                TextFiles.Add(path);

                path = SCTcommon.CheckFile(PartialPath, "AirwayHigh");
                Console.WriteLine("High AirWays...");
                WriteAWY(path, "High");
                TextFiles.Add(path);
            }
            if (SCTchecked.ChkOceanic)
            {
                path = SCTcommon.CheckFile(PartialPath, "Oceanic");
                Console.WriteLine("Oceanic AirWays...");
                WriteOceanic(path);
                TextFiles.Add(path);
            }
            if (SCTchecked.ChkSID)
            {
                Console.WriteLine("SIDS...");
                FileSIDSTAR(PartialPath, IsSID: true);
                TextFiles.Add(path);
            }
            if (SCTchecked.ChkSTAR)
            {
                Console.WriteLine("STARS...");
                FileSIDSTAR(PartialPath, IsSID: false);
                TextFiles.Add(path);
            }
            if (SCTchecked.IncludeSUAfile)
            {
                path = SCTcommon.CheckFile(PartialPath, "SUA");
                Console.WriteLine(path);
                if (path != string.Empty)
                {
                    Console.WriteLine("SUAs...");
                    WriteSUA();
                    TextFiles.Add(path);
                }
            }

            if (SCTchecked.ChkOneVRCFile)
            {
                path = SCTcommon.CheckFile(PartialPath, CycleInfo.AIRAC.ToString(), ".sct2");
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
            Console.WriteLine("End writing output files in SCToutput");
        }

        private static void WriteHeader(string path)
        {
            string section = "[HEADER]";
            using (StreamWriter sw = File.CreateText(path))
            {
                SCTstrings.WriteSectionHeader(sw, section);
                string Message =
                ";              ** Not for real world navigation **" + cr +
                ";           Provided 'as is' - use at your own risk." + cr + cr +
                "; SCTBuilder is available as freeware at https://github.com/drchuck59/SCTBuilder" + cr +
                "; Software-generated sector file using " + VersionInfo.Title + cr + cr +
                "; Sponsoring ARTCC: " + InfoSection.SponsorARTCC + cr +
                "; Facilities Engineer: " + InfoSection.FacilityEngineer + cr +
                "; Assistant Facilities Engineer:" + InfoSection.AsstFacilityEngineer + cr +
                "; AIRAC CYCLE: " + CycleInfo.AIRAC + cr +
                "; Cycle: " + CycleInfo.CycleStart + " to " + CycleInfo.CycleEnd + cr +
                "; Console Limits: N: " + Conversions.Degrees2SCT(InfoSection.NorthLimit,true) + ", W: " + Conversions.Degrees2SCT(InfoSection.WestLimit, false) + cr +
                  "                  S: " + Conversions.Degrees2SCT(InfoSection.SouthLimit, true)+ ", E: " + Conversions.Degrees2SCT(InfoSection.EastLimit,false) + cr +
                "; <Contributors and archived comments located at end of this file>";
                sw.WriteLine(Message);
                SCTstrings.WriteSectionFooter(sw, section);
            }
        }

        public static void WriteColors(string path)
        {
            string section = "[COLORS]";
            DataTable dtColors = Form1.ColorDef;
            string Output = "; Color definition table";
            foreach (DataRow row in dtColors.Rows)
            {
                Output += cr + "#define " + row[0] + " " + row[1];
            }
            using (StreamWriter sw = new StreamWriter(path))
            { 
                SCTstrings.WriteSectionHeader(sw, section);
                sw.WriteLine(Output);
                SCTstrings.WriteSectionFooter(sw, section);
            }
                
        }
        public static void WriteINFO(string path)
        {
            string section = "[INFO]";
            using (StreamWriter sw = new StreamWriter(path))
            {
                SCTstrings.WriteSectionHeader(sw, section);
                sw.WriteLine(section);
                sw.WriteLine(InfoSection.SectorName);
                sw.WriteLine(InfoSection.DefaultPosition);
                sw.WriteLine(InfoSection.DefaultAirport);
                sw.WriteLine(InfoSection.CenterLatitude_SCT);
                sw.WriteLine(InfoSection.CenterLongitude_SCT);
                sw.WriteLine(InfoSection.NMperDegreeLatitude);
                sw.WriteLine(InfoSection.NMperDegreeLongitude.ToString("F1", CultureInfo.InvariantCulture));
                sw.WriteLine(InfoSection.MagneticVariation * -1d);   // Subtract the MagVar (converts True-Map to Mag)
                sw.WriteLine(InfoSection.SectorScale);
                SCTstrings.WriteSectionFooter(sw, section);
            }
        }
        public static void WriteVOR(string path)
        {
            string section = "[VOR]";
            string[] strOut = new string[6];
            DataView dataView = new DataView(Form1.VOR)
            {
                RowFilter = "[Selected]",
                Sort = "FacilityID"
            };
            using (StreamWriter sw = new StreamWriter(path))
            {
                SCTstrings.WriteSectionHeader(sw, section);
                sw.WriteLine(section);
                foreach (DataRowView row in dataView)
                {
                    double Lon = Convert.ToSingle(row["Longitude"]);
                    if (Conversions.LonQuadChanged(InfoSection.CenterLongitude_Dec, Lon))
                        Lon = Conversions.FlipCoord(InfoSection.CenterLongitude_Dec, Lon);
                    strOut[0] = row["FacilityID"].ToString();
                    strOut[1] = string.Format("{0:000.000}", row["Frequency"]);
                    strOut[2] = Conversions.Degrees2SCT(Convert.ToSingle(row["Latitude"]), true);
                    strOut[3] = Conversions.Degrees2SCT(Lon, false);
                    strOut[4] = row["Name"].ToString();
                    strOut[5] = row["FixType"].ToString();
                    if (!(strOut[2] + strOut[3]).Contains("-1 "))      // Do NOT write VORs having no fix
                        sw.WriteLine(SCTstrings.VORout(strOut));
                }
                SCTstrings.WriteSectionFooter(sw, section);
                dataView.Dispose();
            }
        }
        public static void WriteNDB(string path)
        {
            string section = "[NDB]";
            string[] strOut = new string[5]; string LineOut;
            DataTable NDB = Form1.NDB;
            DataView dataView = new DataView(NDB)
            {
                RowFilter = "[Selected]",
                Sort = "FacilityID"
            };
            using (StreamWriter sw = new StreamWriter(path))
            {
                SCTstrings.WriteSectionHeader(sw, section);
                sw.WriteLine(section);
                foreach (DataRowView row in dataView)
                {
                    double Lon = Convert.ToSingle(row["Longitude"]);
                    if (Conversions.LonQuadChanged(InfoSection.CenterLongitude_Dec, Lon))
                        Lon = Conversions.FlipCoord(InfoSection.CenterLongitude_Dec, Lon);
                    strOut[0] = row["FacilityID"].ToString().PadRight(3);
                    strOut[1] = string.Format("{0:000.000}", row["Frequency"]);
                    strOut[2] = Conversions.Degrees2SCT(Convert.ToSingle(row["Latitude"]), true);
                    strOut[3] = Conversions.Degrees2SCT(Lon, false);
                    strOut[4] = row["Name"].ToString();
                    LineOut = strOut[0] + " " + strOut[1] + " " +
                        strOut[2] + " " + strOut[3] + " ;" + strOut[4];
                    if (!(strOut[2] + strOut[3]).Contains("-1 "))      // Do NOT write NDBs having no fix
                        sw.WriteLine(SCTstrings.NDBout(strOut));
                }
                SCTstrings.WriteSectionFooter(sw, section);
                dataView.Dispose();
            }
        }
        public static void WriteAPT(string path)
        {
            string section = "[AIRPORT]";
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
            DataTable dataTable = dvAPT.ToTable(true, "ID", "FacilityID", "ICAO", "Latitude", "Longitude", "Name", "Public");
            DataRow foundRow; string LCL; string ATIS;
            using (StreamWriter sw = new StreamWriter(path))
            {
                SCTstrings.WriteSectionHeader(sw, section);
                sw.WriteLine(section);
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
                    double Lon = Convert.ToSingle(row["Longitude"]);
                    if (Conversions.LonQuadChanged(InfoSection.CenterLongitude_Dec, Lon))
                        Lon = Conversions.FlipCoord(InfoSection.CenterLongitude_Dec, Lon);
                    strOut[1] = LCL.PadRight(7);
                    strOut[2] = Conversions.Degrees2SCT(Convert.ToSingle(row["Latitude"]), true);
                    strOut[3] = Conversions.Degrees2SCT(Lon, false);
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
                SCTstrings.WriteSectionFooter(sw, section);
            }
            dvAPT.Dispose();
        }
        public static void WriteFixes(string path)
        {
            string section = "[FIXES]";
            string[] strOut = new string[5];
            double Lon;
            DataTable FIX = Form1.FIX;
            DataView dataView = new DataView(FIX)
            {
                RowFilter = "[Selected]",
                Sort = "FacilityID"
            };
            using (StreamWriter sw = new StreamWriter(path))
            {
                SCTstrings.WriteSectionHeader(sw, section);
                sw.WriteLine(section);
                foreach (DataRowView row in dataView)
                {
                    Lon = Convert.ToSingle(row["Longitude"]); 
                    if (Conversions.LonQuadChanged(InfoSection.CenterLongitude_Dec, Lon))
                        Lon = Conversions.FlipCoord(InfoSection.CenterLongitude_Dec, Lon);
                    strOut[0] = row["FacilityID"].ToString();
                    strOut[2] = Conversions.Degrees2SCT(Convert.ToSingle(row["Latitude"]), true);
                    strOut[3] = Conversions.Degrees2SCT(Lon, false);
                    strOut[4] = row["Use"].ToString();
                    sw.WriteLine(SCTstrings.FIXout(strOut));  // Uses 0, 2, 3, and 4
                }
             SCTstrings.WriteSectionFooter(sw, section);
            }
            dataView.Dispose();
        }

        private static void WriteRWY(string path)
        {
            string section = "[RUNWAY]";
            string[] strOut = new string[9]; string FacID = string.Empty;
            double MagBHdg; double MagEHdg;
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
                            SCTstrings.WriteSectionHeader(sw, section);
                            sw.WriteLine(section);
                            FirstLine = false;
                        }
                        FacID = row["FacilityID"].ToString();
                        FacFullName = FacID + '-' + row["FacilityName"].ToString();
                    }
                    // FAA RWY bearings are in "True" format and must be converted to "Magnetic"
                    // Brg = True - Declination, where W is negative.
                    strOut[0] = row["BaseIdentifier"].ToString().Trim().PadRight(3);
                    strOut[1] = row["EndIdentifier"].ToString().Trim().PadRight(3);
                    MagBHdg = Convert.ToDouble(row["BaseHeading"]) - InfoSection.MagneticVariation;
                    if (MagBHdg > 360) MagBHdg %= 360;
                    else if (MagBHdg < 0)
                        MagBHdg = (MagBHdg + 360) % 360;
                    if (MagBHdg == 0) MagBHdg = 360;
                    strOut[2] = Convert.ToString(MagBHdg).PadRight(3);
                    MagEHdg = Convert.ToDouble(row["EndHeading"]) - InfoSection.MagneticVariation;
                    if (MagEHdg > 360) MagEHdg %= 360;
                    else if (MagBHdg < 0) MagEHdg = (MagEHdg + 360) % 360;
                    if (MagBHdg == 0) MagBHdg = 360;
                    strOut[3] = Convert.ToString(MagEHdg).PadRight(3);
                    strOut[4] = Conversions.Degrees2SCT(Convert.ToSingle(row["Latitude"]), true);
                    strOut[5] = Conversions.Degrees2SCT(Convert.ToSingle(row["Longitude"]), false);
                    strOut[6] = Conversions.Degrees2SCT(Convert.ToSingle(row["EndLatitude"]), true);
                    strOut[7] = Conversions.Degrees2SCT(Convert.ToSingle(row["EndLongitude"]), false);
                    strOut[8] = FacFullName;
                    sw.WriteLine(SCTstrings.RWYout(strOut));
                    DRAW.Rows.Add(new object[] { strOut[0].ToString(), strOut[4].ToString(), strOut[5].ToString(), "", FacFullName });
                    DRAW.Rows.Add(new object[] { strOut[1].ToString(), strOut[6].ToString(), strOut[7].ToString(), "", FacFullName });
                }
                SCTstrings.WriteSectionFooter(sw, section);
                WriteLabels(DRAW, sw);
            }
            dvRWY.Dispose();
        }

        public static void WriteAWY(string path, string LowHigh)
        {
            DataTable AWY;
            if (LowHigh == "Oceanic") AWY = NaviGraph.wpNavRTE;
                else AWY = Form1.AWY;
            string Awy0 = string.Empty; string Awy1;
            string NavAid0 = string.Empty; string NavAid1;
            double Lat0=-1; double Lat1; 
            double Lon0=-1; double Lon1; 
            bool IsBreak; string section;
            string filter = "[Selected]";
            if (LowHigh == "Low")
            {
                section = "[LOW AIRWAY]";
                filter += " AND [IsLow]";
            }
            else
            {
                section = "[HIGH AIRWAY]";
                filter += " AND NOT [IsLow]";
            }
        DataView dvAWY = new DataView(AWY)
            {
                RowFilter = filter,
                Sort = "AWYID, Sequence",
            };
            // Rotate output as in other output loops
            using (StreamWriter sw = new StreamWriter(path))
            {
                SCTstrings.WriteSectionHeader(sw, section);
                sw.WriteLine(section);
                foreach (DataRowView rowAWY in dvAWY)
                {
                    Awy1 = rowAWY["AWYID"].ToString();
                    NavAid1 = rowAWY["NAVAID"].ToString();
                    IsBreak = (bool)rowAWY["IsBreak"];
                    Lat1 = Convert.ToSingle(rowAWY["Latitude"]);
                    Lon1 = Convert.ToSingle(rowAWY["Longitude"]);
                    if (IsBreak) Lat1 = -199f;            // Break in awy; restart sequence with next
                    if (Awy1 != Awy0) Lat0 = -199f;       // New air, last segment was written (but save this coord)
                    {
                        // Don't enter a line with the same NavAid (zero-length line) or if Lat/Lon undefined
                        if ((NavAid0 != NavAid1) && (Lat0 != -199) && (Lat1 != -199))
                        {
                            // The longest VOR segment is 130 NM (High VORs), so that can be used to prevent drawing
                            // airway segments extending too far outside the selection area
                            if (LatLongCalc.Distance(Lat0, Lon0, Lat1, Lon1) <= 130)
                            {
                                // Make sure Lon1 isn't outside the working quadrant (e.g., crosses +/-180)
                                if (Conversions.LonQuadChanged(InfoSection.CenterLongitude_Dec, Lon1))
                                    Lon1 = Conversions.FlipCoord(Lon0, Lon1);
                                sw.WriteLine(SCTstrings.AWYout(Awy1,
                                    Conversions.Degrees2SCT(Convert.ToSingle(Lat0), true),
                                    Conversions.Degrees2SCT(Convert.ToSingle(Lon0), false),
                                    Conversions.Degrees2SCT(Convert.ToSingle(Lat1), true),
                                    Conversions.Degrees2SCT(Convert.ToSingle(Lon1), false),
                                    NavAid0, NavAid1));
                            }
                        }
                    }
                    // Shift all items
                    Awy0 = Awy1; NavAid0 = NavAid1;
                    Lat0 = Lat1; Lon0 = Lon1;
                }
                SCTstrings.WriteSectionFooter(sw, section);
            }
            dvAWY.Dispose();
        }

        public static void WriteOceanic(string path)
        {
            
            string Awy0 = string.Empty; string Awy1;
            string NavAid0 = string.Empty; string NavAid1;
            double Lat0 = -1; double Lat1;
            double Lon0 = -1; double Lon1;
            string section = "[HIGH AIRWAY]";
            string filter = "[Selected]";
            string section_alias = "[Oceanic]";

            DataView dvOceanic = new DataView(NaviGraph.wpNavRTE)
            {
                RowFilter = filter,
                Sort = "AWYID, Sequence",
            };
            // Rotate output as in other output loops
            using (StreamWriter sw = new StreamWriter(path))
            {
                SCTstrings.WriteSectionHeader(sw, section_alias);
                sw.WriteLine(section);
                foreach (DataRowView rowAWY in dvOceanic)
                {
                    Awy1 = rowAWY["AWYID"].ToString();
                    NavAid1 = rowAWY["NAVAID"].ToString();

                    Lat1 = Convert.ToSingle(rowAWY["Latitude"]);
                    Lon1 = Convert.ToSingle(rowAWY["Longitude"]);

                    if (Awy1 != Awy0) 
                        Lat0 = -199f;       // New air, last segment was written (but save this coord)
                    {
                        // Don't enter a line with the same NavAid (zero-length line) or if Lat/Lon undefined
                        if ((NavAid0 != NavAid1) && (Lat0 != -199) && (Lat1 != -199))
                        {
                            // Limit the extension so we don't draw across the Atlantic
                             if (LatLongCalc.Distance(Lat0, Lon0, Lat1, Lon1) <= 150)
                             {
                                // Make sure Lon1 isn't outside the working quadrant (e.g., crosses +/-180)
                                 if (Conversions.LonQuadChanged(InfoSection.CenterLongitude_Dec, Lon1))
                                     Lon1 = Conversions.FlipCoord(Lon0, Lon1);
                                sw.WriteLine(SCTstrings.AWYout(Awy1,
                                    Conversions.Degrees2SCT(Convert.ToSingle(Lat0), true),
                                    Conversions.Degrees2SCT(Convert.ToSingle(Lon0), false),
                                    Conversions.Degrees2SCT(Convert.ToSingle(Lat1), true),
                                    Conversions.Degrees2SCT(Convert.ToSingle(Lon1), false),
                                    NavAid0, NavAid1));
                             }
                        }
                    }
                    // Shift all items
                    Awy0 = Awy1; NavAid0 = NavAid1;
                    Lat0 = Lat1; Lon0 = Lon1;
                }
                SCTstrings.WriteSectionFooter(sw, section);
            }
            dvOceanic.Dispose();
        }


        public static void FileSIDSTAR(string PartialPath, bool IsSID)
        {
            // Calling routine for SID and STAR diagrams
            // SSD = Either SID or STAR, depending on flag
            string SSDCode;
            // Get the list of Selected SSDs
            string section = "[STAR]";
            if (IsSID) section = "[SID]";
            DataTable SSDlist = ActiveSSD(IsSID);

            // Loop the SSDIDs according to choice of one file or each SSD in a file
            if (InfoSection.OneFilePerSidStar)
            {
                // Create a new file for each SSD
                foreach (DataRow drSSDlist in SSDlist.Rows)
                {
                    // Get the SSD in question - on this loop, the path is the SSDname
                    // Pass the single row of SSD data to the writing program
                    string SSDID = drSSDlist["ID"].ToString();
                    SSDGenerator.SSDID = SSDID;
                    SSDGenerator.WriteSidStar();
                    SSDCode = SSDGenerator.SSDcode;

                    // The Diagram is in SSDGenerator.BigResult
                    string path = SCTcommon.CheckFile(PartialPath, SSDCode);
                    using (StreamWriter sw = new StreamWriter(path))
                    {
                        SCTstrings.WriteSectionHeader(sw, section);
                        sw.WriteLine(section);
                        SCTstrings.WriteSectionHeader(sw, SSDID + " " + SSDCode);
                        sw.WriteLine(SSDGenerator.BigResult); 
                        SCTstrings.WriteSectionFooter(sw, SSDID + " " + SSDCode);
                        SCTstrings.WriteSectionFooter(sw, section);
                    }
                }
            }
            else
            {
                // Create one file for all SSDs
                string path = SCTcommon.CheckFile(PartialPath, section);
                using (StreamWriter sw = new StreamWriter(path))
                {
                    SCTstrings.WriteSectionHeader(sw, section);
                    sw.WriteLine(section);
                    foreach (DataRow drSSDlist in SSDlist.Rows)
                    {
                        // Get the SSD in question - on this loop, the path is the SSDname
                        string SSDID = drSSDlist["ID"].ToString();
                        SSDGenerator.SSDID = SSDID;
                        SSDGenerator.WriteSidStar();
                        SSDCode = SSDGenerator.SSDcode;
                        SCTstrings.WriteSectionHeader(sw, SSDID + " " + SSDCode);
                        sw.WriteLine(SSDGenerator.BigResult);
                        SCTstrings.WriteSectionFooter(sw, SSDID + " " + SSDCode);
                    }
                    SCTstrings.WriteSectionFooter(sw, section);
                }
            }
            SSDlist.Dispose();
        }

        private static DataTable ActiveSSD(bool IsSID)
        {
            string SSDfilter = "NOT [IsSID]";
            if (IsSID)
                SSDfilter = "[IsSID]";
            SSDfilter += " AND [Selected]";
            DataView dvSSD = new DataView(Form1.SSD)
            {
                RowFilter = SSDfilter,
                Sort = "ID",
            };
            Console.WriteLine("SCTOutput_ActiveSSD: Rows found " + dvSSD.Count);
            return dvSSD.ToTable(true, "ID");
        }

        public static void WriteARB(string path, bool High)
        {
            // This doesn't work as designed.  Need to search for affected ARTCCs,
            // then draw all the ARTCCs (filter ARTCC =) with ANY borders in the area.
            // MAY want to do that in the "SELECTED" phase (dgvARB), then sort by ARTCC.
            DataTable ARB = Form1.ARB;
            string FacID0 = string.Empty; string FacID1;
            string ARBname; string HL;  string filter; string Sector;
            string Lat1; string Long1; string Descr1; string Descr0 = string.Empty;
            string Lat0 = string.Empty; string Long0 = string.Empty;
            string LatFirst = string.Empty; string LongFirst = string.Empty;
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
                            double Lon = Convert.ToSingle(ARBdataRowView["Longitude"]);
                            if (Conversions.LonQuadChanged(InfoSection.CenterLongitude_Dec, Lon))
                                Lon = Conversions.FlipCoord(InfoSection.CenterLongitude_Dec, Lon);
                            Lat1 = Conversions.Degrees2SCT(Convert.ToSingle(ARBdataRowView["Latitude"]), true);
                            Long1 = Conversions.Degrees2SCT(Lon, false);
                            LatFirst = Lat1; LongFirst = Long1;             // Save the first point
                            Descr1 = ARBdataRowView["Description"].ToString();
                            ARBname = ARBdataRowView["Name"].ToString();   // Initialize AARTC name
                            FacID1 = ARBdataRowView["ARTCC"].ToString();      // Initialize FacID
                            Output += "; " + ARBname + cr;
                        }
                        else
                        {
                            double Lon = Convert.ToSingle(ARBdataRowView["Longitude"]);
                            if (Conversions.LonQuadChanged(InfoSection.CenterLongitude_Dec, Lon))
                                Lon = Conversions.FlipCoord(InfoSection.CenterLongitude_Dec, Lon);
                            FacID1 = ARBdataRowView["ARTCC"].ToString();
                            Descr1 = ARBdataRowView["Description"].ToString();
                            Lat1 = Conversions.Degrees2SCT(Convert.ToSingle(ARBdataRowView["Latitude"]), true);
                            Long1 = Conversions.Degrees2SCT(Lon, false);
                            if ((FacID0.Length != 0) && (FacID0 == FacID1))
                                Output += SCTstrings.BoundaryOut(FacID1 + HL, Lat0, Long0, Lat1, Long1, Descr0) + cr;
                        }
                        if (Descr1.IndexOf("POINT OF BEGINNING") != -1)    // Last line in this group
                        {
                            Output += SCTstrings.BoundaryOut(FacID1 + HL, Lat0, Long0, Lat1, Long1, Descr0) + cr;
                            Output += SCTstrings.BoundaryOut(FacID1 + HL, Lat1, Long1, LatFirst, LongFirst) + cr;
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
                SCTstrings.WriteSectionFooter(sw, "ARTCC " + Sector + "");
            }
            ARBview.Dispose();
        }

        private static void WriteLabels(DataTable dtSTL, StreamWriter sw)
        {
            string strText; string Lat; string Long; string TextColor; string Comment;
            string Output; string section = "[LABELS]";
            SCTstrings.WriteSectionHeader(sw, section);
            sw.WriteLine(section);
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
            SCTstrings.WriteSectionFooter(sw, section);
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
                    Lat1 = Conversions.Degrees2SCT(Convert.ToSingle(dataRow["Latitude"]), true);
                    Long1 = Conversions.Degrees2SCT(Convert.ToSingle(dataRow["Longitude"]), false);
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

