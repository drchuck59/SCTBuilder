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

namespace SCTBuilder
{
    class SCToutput
    {
        public static string CycleHeader;
        public static readonly string cr = Environment.NewLine;
        public static void WriteSCT()
        {
            // DataTable LS = Form1.LocalSector;
            var TextFiles = new List<string>();
            string Message;
            MessageBoxButtons buttons = MessageBoxButtons.OK;
            MessageBoxIcon icon = MessageBoxIcon.Information;
            string PartialPath = FolderMgt.OutputFolder + "\\" +
                InfoSection.SponsorARTCC + "_";
            string path = CheckFile(PartialPath, "Header");
            if (path != string.Empty)
            {
                Console.WriteLine("Header...");
                WriteHeader(path);
                TextFiles.Add(path);
            }
            path = CheckFile(PartialPath, "Colors");
            if (path != string.Empty)
            {
                Console.WriteLine("ColorDefinitions");
                WriteColors(path);
                TextFiles.Add(path);
            }
            path = CheckFile(PartialPath, "Info");
            if (path != string.Empty)
            {
                Console.WriteLine("INFO section...");
                WriteINFO(path);
                TextFiles.Add(path);
            }
            path = CheckFile(PartialPath, "VOR");
            if (SCTchecked.ChkVOR && path != string.Empty)
            {
                Console.WriteLine("VORs...");
                WriteVOR(path);
                TextFiles.Add(path);
            }
            path = CheckFile(PartialPath, "NDB");
            if (SCTchecked.ChkNDB && path != string.Empty)
            {
                Console.WriteLine("VORs...");
                WriteNDB(path);
                TextFiles.Add(path);
            }
            path = CheckFile(PartialPath, "APT");
            if (SCTchecked.ChkAPT && path != string.Empty)
            {
                Console.WriteLine("Airports...");
                WriteAPT(path);
                TextFiles.Add(path);
            }
            path = CheckFile(PartialPath, "RWY");
            if (SCTchecked.ChkRWY && path != string.Empty)
            {
                Console.WriteLine("Airport Runways...");
                WriteRWY(path);
                TextFiles.Add(path);
            }
            path = CheckFile(PartialPath, "FIX");
            if (SCTchecked.ChkFIX && path != string.Empty)
            {
                Console.WriteLine("Fixes...");
                WriteFixes(path);
                TextFiles.Add(path);
            }
            path = CheckFile(PartialPath, "ARTCC_HIGH");
            if (SCTchecked.ChkARB && path != string.Empty)
            {
                Console.WriteLine("ARTCC HIGH...");
                WriteARB(path, true);
                TextFiles.Add(path);
            }
            path = CheckFile(PartialPath, "ARTCC_LOW");
            if (SCTchecked.ChkARB && path != string.Empty)
            {
                Console.WriteLine("ARTCC LOW...");
                WriteARB(path, false);
                TextFiles.Add(path);
            }
            path = CheckFile(PartialPath, "AirwayLow");
            if (SCTchecked.ChkAWY && path != string.Empty)
                {
                Console.WriteLine("Low AirWays...");
                WriteAWY(path, LOWawy: true);
                TextFiles.Add(path);
            }
            path = CheckFile(PartialPath, "AirwayHigh");
            if (SCTchecked.ChkAWY && path != string.Empty)
            {
                Console.WriteLine("High AirWays...");
                WriteAWY(path, LOWawy: false);
                TextFiles.Add(path);
            }
            if (SCTchecked.ChkSID)
            {
                Console.WriteLine("SIDS...");
                WriteSIDSTAR(PartialPath, IsSID: true);
                TextFiles.Add(path);
            }
            if (SCTchecked.ChkSTAR)
            {
                Console.WriteLine("STARS...");
                WriteSIDSTAR(PartialPath, IsSID: false);
                TextFiles.Add(path);
            }
            path = CheckFile(PartialPath, "SUA");
            if (path != string.Empty)
            {
                Console.WriteLine("SUAs...");
                WriteSUA();
                // TextFiles.Add(path);
            }
            path = CheckFile(PartialPath, CycleInfo.AIRAC.ToString(),".sct2");
            if (SCTchecked.ChkALL && path != string.Empty)
            {
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
                MessageBox.Show(Message, VersionInfo.Title, buttons, icon);
            }
            else
            {
                Message = "Text file(s) written to" + PartialPath;
                MessageBox.Show(Message, VersionInfo.Title, buttons, icon);
            }
            Console.WriteLine("End writing output files");
        }

        public static string CheckFile(string PartialPath, string file, string type = ".txt")
        {
            // Looks for the file in the PartialPath.  If found, optionally seeks confirm to overwrite.
            // RETURNS the fully qualified path to the file unles overwite denied, then returns empty.
            string caption = VersionInfo.Title;
            string Message; MessageBoxIcon icon; MessageBoxButtons buttons;
            DialogResult result;
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
            CycleHeader = cr +
                "; ================================================================" + cr +
                "; AIRAC CYCLE: " + CycleInfo.AIRAC + cr +
                "; Cycle: " + CycleInfo.CycleStart + " to " + CycleInfo.CycleEnd + cr +
                "; ================================================================" + cr;
        }
        private static void WriteColors(string path)
        {
            DataTable dtColors = Form1.Colors;
            string Output = Environment.NewLine; 
            Output += "; Color definition table" + cr;
            Console.WriteLine("Colors in color table: " + dtColors.Rows.Count);
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
                        sw.WriteLine(SCTstrings.VORNDBout(strOut));
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
                    if (!(strOut[2] + strOut[3]).Contains("-1 "))      // Do NOT write VORs having no fix
                        sw.WriteLine(SCTstrings.VORNDBout(strOut));
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
                    if (!(strOut[2] + " " + strOut[3]).Contains("-1 "))      // Do NOT write VORs having no fix
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
                    sw.WriteLine(SCTstrings.FIXout(strOut));
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
                    else if (MagBHdg < 0) MagBHdg += 360 % 360;
                    strOut[2] = Convert.ToString(MagBHdg).PadRight(3);
                    MagEHdg = Convert.ToDouble(row["EndHeading"]) + InfoSection.MagneticVariation;
                    if (MagEHdg > 360) MagEHdg %= 360;
                    else if (MagEHdg < 0) MagEHdg += 360 % 360;
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
        private static void WriteAWY(string path, bool LOWawy = true)
        {
            DataTable AWY = Form1.AWY;
            string CurAwy = string.Empty; string PrevAwy = string.Empty;
            string NavAid0 = string.Empty; string NavAid1 = string.Empty;
            string Lat0 = string.Empty; string Long0 = string.Empty;
            string Lat1 = string.Empty; string Long1 = string.Empty;
            string filter = "[Selected] AND ";
            if (LOWawy)
            {
                filter += "( ( ([MinEnrAlt] > -1) AND ([MinEnrAlt] < 18000) ) OR ( ([MaxAuthAlt]> -1) AND ([MaxAuthAlt] < 18000) ) )";
            }
            else
            {
                filter += "( ( ([MinEnrAlt] > -1) AND ([MinEnrAlt] >= 18000) ) OR ( ([MaxAuthAlt]> -1) AND ([MaxAuthAlt] >= 18000) ) )";
            }
            DataView AWYView = new DataView(AWY)
            {
                RowFilter = filter,
                Sort = "AWYID, Sequence",
            };
            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.WriteLine(CycleHeader);
                if (LOWawy) sw.WriteLine("[LOW AIRWAY]");
                else sw.WriteLine("[HIGH AIRWAY]");
                foreach (DataRowView row in AWYView)
                {
                    if (AWYView.Count != 0)
                    {
                        if (CurAwy.Length == 0)                               // Initialize loop
                        {
                            CurAwy = row["AWYID"].ToString();
                            NavAid0 = row["NAVAID"].ToString();
                            Lat0 = Conversions.DecDeg2SCT(Convert.ToSingle(row["Latitude"]), true);
                            Long0 = Conversions.DecDeg2SCT(Convert.ToSingle(row["Longitude"]), false);
                            PrevAwy = CurAwy;
                        }
                        else
                        {
                            CurAwy = row["AWYID"].ToString(); ;
                            if (CurAwy != PrevAwy)                          // If the new row is the same airway...
                            {
                                if (NavAid1.Length != 0)                  // Write the line
                                {
                                    string str = new string(' ', 27 - CurAwy.Length);
                                    if ((NavAid0.Length != 0) && (NavAid1.Length != 0))     // For the rare occurence of an empty row NavAid, e.g., J1
                                    {
                                        sw.WriteLine(SCTstrings.AWYout(CurAwy, Lat0, Long0, Lat1, Long1, NavAid0, NavAid1));
                                    }
                                    NavAid0 = NavAid1;
                                    Lat0 = Lat1;
                                    Long0 = Long1;
                                    NavAid1 = string.Empty;
                                }
                                else
                                {
                                    NavAid1 = row["NAVAID"].ToString();
                                    Lat1 = Conversions.DecDeg2SCT(Convert.ToSingle(row["Latitude"]), true);
                                    Long1 = Conversions.DecDeg2SCT(Convert.ToSingle(row["Longitude"]), false);
                                }
                            }
                            else
                            {
                                PrevAwy = CurAwy;                           // This is a new airway
                                NavAid0 = row["NAVAID"].ToString();
                                Lat0 = Conversions.DecDeg2SCT(Convert.ToSingle(row["Latitude"]), true);
                                Long0 = Conversions.DecDeg2SCT(Convert.ToSingle(row["Longitude"]), false);
                                NavAid1 = string.Empty;
                            }
                        }
                    }
                }
            }
            AWYView.Dispose();
        }

        private static void WriteSIDSTAR(string PartialPath, bool IsSID)
        {
            // Creates a list of IDs by SID or STAR, then loops for output
            string SSDfilter;
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
            foreach (DataRow drSSD in SSDlist.Rows)
            {
                dvSSD.RowFilter = "ID = '" + drSSD[0].ToString() + "'";
                Debug.WriteLine("Calling WriteSSD with ID = " + drSSD[0].ToString());
                WriteSSD(drSSD[0].ToString(), PartialPath);
            }
            dvSSD.Dispose();
            SSDlist.Dispose();
        }

        private static void WriteSSD(string SSDID, string PartialPath)
        {
            // Writes ONE SID or STAR from ONE SSD dataview (preselected)
            // Expects folder path, Use FIXes for lat long or not, and SID or STAR
            // Everything goes in List<string>s first, then write them out in order
            List<string> SSDlines = new List<string>();
            List<string> FixesUsed = new List<string>();
            List<string> APTsUsed = new List<string>();
            // Various and sundry variables for the loop
            string Lat1; string Long1; string FixLineOut;
            string Lat0 = string.Empty; string Long0 = string.Empty;
            string lastFix = string.Empty; string curFix; string FixType0 = string.Empty;
            string strLL = new string(' ', 27); string FixType1;
            string SSDname = string.Empty; string TransitionName;
            string SSDcode = string.Empty; string TransitionCode;
            // Get the SSD in question
            DataView dvSSD = new DataView(Form1.SSD)
            {
                RowFilter = "[ID] = '" + SSDID + "'",
                Sort = "Sequence"
            };
            // Is this a SID or STAR?  The 1st character will tell
            bool IsSID = false; if (dvSSD[0][0].ToString().Substring(0, 1) == "D") IsSID = true;
            // Build the string of waypoints and save APTs or FIXes

            // Get the name and code for this SSD
            SSDname = dvSSD[0]["SSDName"].ToString();
            SSDcode = dvSSD[0]["SSDcode"].ToString();

            // Write the Section header and SID or STAR code (Fullname)
            string Section = "STAR"; if (IsSID) Section = "SID";
            Debug.WriteLine("[" + Section + "]" + cr + SSDHeader(SSDcode + " (" + SSDname + ")",' '));
            SSDlines.Add("[" + Section + "]" + cr + SSDHeader(SSDcode + " (" + SSDname + ")",' '));
            // Now loop the entire SSD to get the lines, etc.
            foreach (DataRowView SSDrow in dvSSD)
            {
                // Get the basics - usual process: Lat1, shift to Lat0 or not, print...
                // Regardless, do a shift at the end (Making these empty indicates pen up)
                Lat1 = Conversions.DecDeg2SCT(Convert.ToSingle(SSDrow["Latitude"]), true);
                Long1 = Conversions.DecDeg2SCT(Convert.ToSingle(SSDrow["Longitude"]), false);
                curFix = SSDrow["NavAid"].ToString();
                FixType1 = SSDrow["FixType"].ToString();

                // If it's an airport, record the APT ICOA and move to next row
                if (FixType1 == "AA")
                {
                    // Save the APTs for later...
                    Add2ListIfNew(APTsUsed, curFix);
                    curFix = Lat1 = Long1 = string.Empty;      // Don't use this for a line
                }
                else
                    // Save the FIX for later...
                    Add2ListIfNew(FixesUsed, curFix);
                
                // If there's a Transition Name, it starts a new line set.
                // Keep these coordinates to start the line
                TransitionName = SSDrow["TransitionName"].ToString();
                TransitionCode = SSDrow["TransitionCode"].ToString();
                if (TransitionName.Length != 0)
                {
                    SSDlines.Add(SSDHeader(TransitionCode + " (" + TransitionName + ")"));
                    lastFix = Lat0 = Long0 = string.Empty;      // Don't draw from previous to this
                }
                    // Get the waypoint line
                if ((lastFix.Length != 0) && (curFix.Length != 0) && (lastFix != curFix))
                {
                    if (InfoSection.UseFixes)
                    {
                        Debug.WriteLine(strLL + lastFix + " " + lastFix + " " + curFix + " " + curFix);
                        SSDlines.Add(strLL + lastFix + " " + lastFix + " " + curFix + " " + curFix);
                    }
                    else
                    {
                        Debug.WriteLine(strLL + Lat0 + " " + Long0 + " " + Lat1 + " " +
                               Long1 + "; " + lastFix + " " + curFix);
                        SSDlines.Add(strLL + Lat0 + " " + Long0 + " " + Lat1 + " " +
                            Long1 + "; " + lastFix + ' ' + curFix);
                    }
                    // Need to look up and add the ALT and Speed items here
                }
                // Shift the values for the next item
                Lat0 = Lat1; Long0 = Long1; lastFix = curFix; FixType0 = FixType1;
            }
            // Write the file for this SSD
            string path = CheckFile(PartialPath, SSDcode);
            using (StreamWriter sw = new StreamWriter(path))
            {
                // Write the Airports in this SSD
                sw.WriteLine("; Airports serviced:");
                Debug.WriteLine("Airports serviced:");
                DataView dvAPT = new DataView(Form1.APT);
                DataView dvTWR = new DataView(Form1.TWR);
                foreach(string Arpt in APTsUsed)
                {
                    dvAPT.RowFilter = "FacilityID = '" + Arpt + "'";
                    dvTWR.RowFilter = "FacilityID = '" + Arpt + "'";
                    if (dvTWR.Count != 0)
                    {
                        Debug.WriteLine(Conversions.ICOA(Arpt) + " " + dvTWR[0]["LCLfreq"].ToString().PadRight(7) + " " +
                            Conversions.DecDeg2SCT(Convert.ToDouble(dvAPT[0]["Latitude"]), true) + " " + 
                            Conversions.DecDeg2SCT(Convert.ToDouble(dvAPT[0]["Longitude"]), false) +
                            "; " + dvAPT[0]["Name"].ToString());
                        sw.WriteLine(Conversions.ICOA(Arpt) + " " + dvTWR[0]["LCLfreq"].ToString().PadRight(7) + " " +
                            Conversions.DecDeg2SCT(Convert.ToDouble(dvAPT[0]["Latitude"]), true) + " " +
                            Conversions.DecDeg2SCT(Convert.ToDouble(dvAPT[0]["Longitude"]), false) +
                            "; " + dvAPT[0]["Name"].ToString());
                    }
                    else
                    {
                        Debug.WriteLine(Conversions.ICOA(Arpt) + " " +
                            Conversions.DecDeg2SCT(Convert.ToDouble(dvAPT[0]["Latitude"]), true) + " " +
                            Conversions.DecDeg2SCT(Convert.ToDouble(dvAPT[0]["Longitude"]), false) +
                            "; " + dvAPT[0]["Name"].ToString());
                        sw.WriteLine(Conversions.ICOA(Arpt) + " 122.8   " +
                            Conversions.DecDeg2SCT(Convert.ToDouble(dvAPT[0]["Latitude"]), true) + " " +
                            Conversions.DecDeg2SCT(Convert.ToDouble(dvAPT[0]["Longitude"]), false) +
                            "; " + dvAPT[0]["Name"].ToString());
                    }
                }
                dvAPT.Dispose();
                // Write the NavAids in this SSD
                sw.WriteLine(cr + "Fixes Used:");
                Debug.WriteLine("Fixes Used:");
                DataView dvFIX = new DataView(Form1.FIX);
                DataView dvVOR = new DataView(Form1.VOR);
                DataView dvNDB = new DataView(Form1.NDB);
                List<string> FixData = new List<string>(5);
                foreach (string Fix in FixesUsed)
                {
                    FixData = FixInfo(Fix);
                    if (FixData[0] != "Not Found")
                    {
                        FixLineOut = Fix.PadRight(5) + " " + FixData[1] + " " + FixData[2] + "; " + FixData[0] + " " + FixData[3];
                        if (FixData[4].Length != 0) FixLineOut += " - " + FixData[4];
                        Debug.WriteLine(FixLineOut);
                        sw.WriteLine(FixLineOut);
                    }
                    else
                    {
                        Debug.WriteLine("; " + Fix + " not found.");
                    }
                }
                dvNDB.Dispose();
                dvVOR.Dispose();
                dvFIX.Dispose();
                dvSSD.Dispose();
                // Write the lines for this SSD
                sw.WriteLine();
                foreach (string Line in SSDlines)
                    sw.WriteLine(Line);

                if (InfoSection.DrawFixesOnDiagrams)
                {
                    // Draw the fix names
                    sw.WriteLine(SSDHeader("Fix Names follow...", '*'));
                    DrawFixText(FixesUsed, sw);
                    // Will need to add the DrawFixNames for ALT and SPEED here
                }
            }
            SSDlines.Clear();
            FixesUsed.Clear();
            APTsUsed.Clear();
        }

        private static string SSDHeader(string Header, char Marker = '=', int MarkerCount = 0)
        {
            string Mask; string result; int Pad = 27;
            if (MarkerCount != 0) Mask = new string(Marker, MarkerCount);
            else Mask = Marker.ToString();
            result = Mask + Header;
            if (MarkerCount != 0) result += Mask;
            string DummyCoords = "N000.00.00.000 E000.00.00.000 N000.00.00.000 E000.00.00.000";
            result = result.PadRight(Pad) + DummyCoords;
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

        private static List<string> FixInfo(string Fix)
        {
            // Finds the given fix and RETURNS
            // Name, Latitude, Longitude, FixType, Frequency
            List<string> result = new List<string>();
            string Filter = "[FacilityID] = '" + Fix + "'";
            // Search each table for the NavAid and return result
            DataView dvFIX = new DataView(Form1.FIX)
            {
                RowFilter = Filter
            };
            if (dvFIX.Count != 0)
            {
                result = new List<string>()
                {
                    string.Empty,
                    Conversions.DecDeg2SCT(Convert.ToDouble(dvFIX[0]["Latitude"]), true),
                    Conversions.DecDeg2SCT(Convert.ToDouble(dvFIX[0]["Longitude"]), false),
                    "FIX",
                    string.Empty,
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
                    result = new List<string>()
                    {
                    dvVOR[0]["Name"].ToString(),
                    Conversions.DecDeg2SCT(Convert.ToDouble(dvVOR[0]["Latitude"]), true),
                    Conversions.DecDeg2SCT(Convert.ToDouble(dvVOR[0]["Longitude"]), false),
                    "VOR",
                    dvVOR[0]["Frequency"].ToString()
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
                        result = new List<string>()
                        {
                        dvNDB[0]["Name"].ToString(),
                        Conversions.DecDeg2SCT(Convert.ToDouble(dvNDB[0]["Latitude"]), true),
                        Conversions.DecDeg2SCT(Convert.ToDouble(dvNDB[0]["Longitude"]), false),
                        "NDB",
                        dvNDB[0]["Frequency"].ToString(),
                        };
                        dvNDB.Dispose();
                    }
                    else
                    {
                        result = new List<string>()
                        {
                        "Not Found",
                        "Not Found",
                        "Not Found",
                        "Not Found",
                        "Not Found",
                        };
                    }
                }

            }
            return result;
        }

        private static void DrawFixText(List<string> FixNames, StreamWriter sw)
        {
            double Angle = 0f + InfoSection.MagneticVariation;
            double Scale = 0.025f;
            double Latitude;
            double Longitude;
            foreach (string Fix in FixNames)
            {
                List<string> FixData = FixInfo(Fix);
                if ((FixData[0] != "Not Found") && (FixData[0].Length != 0))        
                {
                    Latitude = Conversions.String2DecDeg(FixData[1]);
                    Longitude = Conversions.String2DecDeg(FixData[2]);
                    if ((Latitude != 0) && (Longitude != 0))
                        sw.WriteLine(Hershey.DrawHF(Fix, Latitude, Longitude, Angle, Scale));
                }
            }
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

