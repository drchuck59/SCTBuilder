using System;
using System.IO;
using System.Windows.Forms;
using System.Data;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System.Media;

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
            path = CheckFile(PartialPath, "SID");
            if (SCTchecked.ChkSID && path != string.Empty)
            {
                Console.WriteLine("SIDS...");
                WriteSIDSTAR(path, UseName:SCTchecked.ChkSSDname, IsSID: true);
                TextFiles.Add(path);
            }
            path = CheckFile(PartialPath, "STAR");
            if (SCTchecked.ChkSTAR && path != string.Empty)
            {
                Console.WriteLine("STARS...");
                WriteSIDSTAR(path, UseName:SCTchecked.ChkSSDname, IsSID: false);
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
                Message = "Test file(s) written to" + PartialPath;
                MessageBox.Show(Message, VersionInfo.Title, buttons, icon);
            }
            Console.WriteLine("End writing output files");
        }

        public static string CheckFile(string PartialPath, string file, string type = ".txt")
        {
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
                sw.WriteLine(InfoSection.NMperDegreeLongitude);
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
                sw.WriteLine();
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
                    if (ATIS.Length != 0) strOut[6] += ATIStype + strOut[6];
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
            string[] strOut = new string[8]; string FacID = string.Empty;
            string RWYtextColor = TextColors.RWYTextColor;
            bool FirstLine = true;
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
                    }
                    strOut[0] = row["BaseIdentifier"].ToString().Trim().PadRight(3);
                    strOut[1] = row["EndIdentifier"].ToString().Trim().PadRight(3);
                    strOut[2] = row["BaseHeading"].ToString().PadRight(3);
                    strOut[3] = row["EndHeading"].ToString().PadRight(3);
                    strOut[4] = Conversions.DecDeg2SCT(Convert.ToSingle(row["Latitude"]), true);
                    strOut[5] = Conversions.DecDeg2SCT(Convert.ToSingle(row["Longitude"]), false);
                    strOut[6] = Conversions.DecDeg2SCT(Convert.ToSingle(row["EndLatitude"]), true);
                    strOut[7] = Conversions.DecDeg2SCT(Convert.ToSingle(row["EndLongitude"]), false);
                    sw.Write(SCTstrings.RWYout(strOut));
                    sw.WriteLine("; " + row["FacilityID"].ToString());
                    DRAW.Rows.Add(new object[] { strOut[0].ToString(), strOut[4].ToString(), strOut[5].ToString(), RWYtextColor });
                    DRAW.Rows.Add(new object[] { strOut[1].ToString(), strOut[6].ToString(), strOut[7].ToString(), RWYtextColor });
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
                            if (CurAwy == PrevAwy)                          // If the new row is the same airway...
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

        private static void WriteSIDSTAR(string path, bool UseName, bool IsSID)
        {
            // Declare variables
            string SSDfilter; 
            char Mark = Convert.ToChar("=");        // Used in the Diagrams headers
            using (StreamWriter sw = new StreamWriter(path))
            {
                // OK to write the Section header here since it's called only once
                string Section;
                if (IsSID) { Section = "SID"; } else { Section = "STAR"; }
                sw.WriteLine();
                sw.WriteLine("[" + Section + "]");
                sw.WriteLine(SSDHeader(Section, Mark, 5));
                Console.WriteLine(SSDHeader(Section, Mark, 5));

                // Declare tables that will be used to write lookup table
                DataTable APTtable = Form1.APT;
                DataTable SSDtable = Form1.SSD;
                DataTable A2Dtable = new SCTdata.APT2SSDDataTable();
                DataView dvAPT = new DataView(APTtable)
                {
                    Sort = "ID"
                };
                DataView dvSSD = new DataView(SSDtable)
                {
                    Sort = "Sequence"
                };

                // Filter SSD dataview to find affected airports and ARTCCs in APT file
                if (IsSID) SSDfilter = "[IsSID]"; else SSDfilter = "NOT [IsSID]";
                SSDfilter += " AND [Selected] AND [FixType] = 'AA'";
                dvSSD.RowFilter = SSDfilter;
                dvSSD.Sort = "Sequence";

                // Build the table that will be used to sort and call the WriteSSD function
                A2Dtable.Clear();                       // Start with an empty table
                DataView dvA2D_raw = new DataView(A2Dtable);
                foreach (DataRowView SSDrow in dvSSD)
                {
                    dvAPT.RowFilter = "[FacilityID] = '" + SSDrow["NavAid"].ToString() + "'";
                    DataRowView newrow = dvA2D_raw.AddNew();
                    newrow["ARTCC"] = dvAPT[0]["ARTCC"].ToString();
                    newrow["APT_FK"] = dvAPT[0]["ID"].ToString();
                    newrow["SSD_FK"] = SSDrow["ID"].ToString();
                    newrow["APT_FACID"] = dvAPT[0]["FacilityID"].ToString();
                    newrow.EndEdit();
                }
                // Remove duplicate entries
                DataTable dtA2D = dvA2D_raw.ToTable(true, "ARTCC", "APT_FK", "SSD_FK", "APT_FACID");
                dvA2D_raw.Dispose();
                // And sort it into a dataview we can use
                DataView dvA2D = new DataView(dtA2D)
                {
                    Sort = "ARTCC, APT_FACID, SSD_FK"
                };
                Console.WriteLine("dtA2D rows: " + dtA2D.Rows.Count.ToString());

                // Call WriteSSD using the sorted dvSSD list
                // Sponsor ARTTC first
                string curARTCC = InfoSection.SponsorARTCC.ToString();
                dvA2D.RowFilter = "[ARTCC] = '" + curARTCC + "'";
                sw.WriteLine(SSDHeader(curARTCC, Mark, 4));
                // Loop the SSDIDs in the DV to write the data for Sponsor ARTCC
                // Use a Dataview so we can sort by airports
                string curAirportID = string.Empty;
                foreach (DataRowView drA2D in dvA2D)
                {
                    if (curAirportID != drA2D["APT_FK"].ToString())
                    {
                        curAirportID = drA2D["APT_FK"].ToString();
                        Console.WriteLine("ID: " + curAirportID + ", ARTCC: " + curARTCC + ", APT: " + drA2D["APT_FACID"].ToString());
                        sw.WriteLine(SSDHeader(Conversions.ICOA(drA2D["APT_FACID"].ToString()), Mark, 3));
                    }
                    WriteSSD(sw, drA2D["SSD_FK"].ToString(), UseName, IsSID);
                }
                // All the other ARTCCs that may have been in the filter
                dvA2D.RowFilter = "[ARTCC] <> '" + curARTCC + "'";
                // This loop adds the ARTCCs, skip if no other ARTCCs
                DialogResult Result;
                if (dvA2D.Count != 0)
                {
                    if (SCTchecked.ChkConfirmOverwrite == true)
                    {
                        string Message = "There are " + dvA2D.Count.ToString();
                        if (IsSID) Message += " SIDs "; else Message += " STARs ";
                        Message += "in other ARTCCs.  They could make the sector file very large. Do you want them generated?";
                        Result = MessageBox.Show(Message, VersionInfo.Title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    }
                    else Result = DialogResult.Yes;
                    if (Result == DialogResult.Yes)
                    {
                        curARTCC = string.Empty;
                        curAirportID = string.Empty;
                        foreach (DataRowView drA2D in dvA2D)
                        {
                            if (curARTCC != drA2D["ARTCC"].ToString())
                            {
                                curARTCC = drA2D["ARTCC"].ToString();
                                sw.WriteLine(SSDHeader(curARTCC, Mark, 4));
                            }
                            if (curAirportID != drA2D["APT_FK"].ToString())
                            {
                                curAirportID = drA2D["APT_FK"].ToString();
                                dvAPT.RowFilter = "ID = '" + curAirportID + "'";
                                Console.WriteLine("ID: " + curAirportID + ", ARTCC: " + curARTCC + ", APT: " + dvAPT[0]["FacilityID"].ToString());
                                sw.WriteLine(SSDHeader(Conversions.ICOA(dvAPT[0]["FacilityID"].ToString()), Mark, 3));
                            }
                            WriteSSD(sw, drA2D["SSD_FK"].ToString(), UseName, IsSID);
                        }
                    }
                    else Console.WriteLine("No other ARTCCs in SID/STAR");
                }
                dvA2D.Dispose();
                dvAPT.Dispose();
                dvSSD.Dispose();
            }
        }

        private static string SSDHeader(string Header, char Marker='=', int MarkerCount=0)
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

        private static void WriteSSD(StreamWriter sw, string SSDID, bool UseName, bool IsSID)
        {
            ///<summary>
            /// Everything works.  Need to find a way to add the FIXes from the SSDs outside
            /// of the search box, so that the "Find" command can show the FIX.
            /// Also need to test the SSD labeling routine.
            /// </summary>
            DataTable SSD = Form1.SSD;
            char Prefix; string SSDName;
            string Lat0 = string.Empty; string Long0 = string.Empty;
            string Lat1; string Long1;
            string Fix0 = string.Empty; string Fix1; string FixType0 = string.Empty;
            string strLL = new string(' ', 27); string FixType1; 
            string SSDfilter = "[ID] = '" + SSDID + "'"; 
            string SSDResult = string.Empty; string SSDCaption;
            if (UseName) SSDCaption = "SSDName"; else SSDCaption = "SSDcode";
                DataView dvSSD = new DataView(SSD)
                {
                    RowFilter = SSDfilter,
                    Sort = "Sequence"
                };
            if (IsSID) Prefix = Convert.ToChar("+"); else Prefix = Convert.ToChar("-");
            var SSDNames = new List<string>();
            // Save the SSD Header
            SSDName = dvSSD[0][SSDCaption].ToString();
            if (!UseName)
            {
                if (IsSID)
                {
                    int loc1 = SSDName.IndexOf(".");
                    if (loc1 != -1) SSDName = SSDName.Substring(0, loc1);
                }
                else
                {
                    int loc1 = SSDName.IndexOf(".");
                    if (loc1 != -1) SSDName = SSDName.Substring(loc1 + 1);
                }
            }
            // Build the string of waypoints (if any)
            foreach (DataRowView SSDrow in dvSSD)
            {
                Lat1 = Conversions.DecDeg2SCT(Convert.ToSingle(SSDrow["Latitude"]), true);
                Long1 = Conversions.DecDeg2SCT(Convert.ToSingle(SSDrow["Longitude"]), false);
                Fix1 = SSDrow["NavAId"].ToString();
                FixType1 = SSDrow["FixType"].ToString();
                if ((Fix0.Length != 0) && (Fix0 != Fix1))
                {
                    SSDResult += strLL + Lat0 + " " + Long0 + " " + Lat1 + " " + 
                        Long1 + "; " + Fix0 + "(" + FixType0 + ") " + Fix1 + "(" + FixType1 + ")" + cr;
                    ListFixes(SSDNames, Fix0);
                    ListFixes(SSDNames, Fix1);
                }
                // If the FixType is "AA", reuse the Fix0-items (don't shift the coordinates)
                if (FixType1 != "AA")
                    Lat0 = Lat1; Long0 = Long1; Fix0 = Fix1; FixType0 = FixType1;
            }
            // Output the result if there were FIXes in the SSD
            if (SSDResult.Length != 0)
            {
                sw.WriteLine(SSDHeader(SSDName + " *RNAV*", Prefix));
                sw.WriteLine(SSDResult);
                WriteFixNames(SSDNames, sw);
                SSDNames.Clear();
            }
            else
            {
                sw.WriteLine(SSDHeader(SSDName + "(RV only)", Prefix));
            }
            dvSSD.Dispose();
        }

        private static List<string> ListFixes(List<string> Fixes, string NewFix)
        {
            bool Found = false;
            foreach (string Name in Fixes)
            {
                if (Name == NewFix) Found = true;
            }
            if (!Found) Fixes.Add(NewFix);
            return Fixes;
        }

        private static void WriteFixNames(List<string> FixNames, StreamWriter sw)
        {
            double Angle = 0f;
            double Scale = 0.025f;
            double Latitude = 0f;
            double Longitude = 0f;
            string Filter;
            DataView dvFIX = new DataView(Form1.FIX);
            DataView dvVOR = new DataView(Form1.VOR);
            DataView dvNDB = new DataView(Form1.NDB);
            foreach (string FixName in FixNames)
            {
                Filter = "[FacilityID] = '" + FixName + "'" ;
                dvFIX.RowFilter = Filter;
                if (dvFIX.Count != 0)
                {
                    Latitude = Convert.ToSingle(dvFIX[0]["Latitude"]);
                    Longitude = Convert.ToSingle(dvFIX[0]["Longitude"]);
                }
                else
                {
                    dvVOR.RowFilter = Filter;
                    if (dvVOR.Count != 0)
                    {
                        Latitude = Convert.ToSingle(dvVOR[0]["Latitude"]);
                        Longitude = Convert.ToSingle(dvVOR[0]["Longitude"]);
                    }
                    else
                    {
                        dvNDB.RowFilter = Filter;
                        if (dvNDB.Count != 0)
                        {
                            Latitude = Convert.ToSingle(dvNDB[0]["Latitude"]);
                            Longitude = Convert.ToSingle(dvNDB[0]["Longitude"]);
                        }
                    }
                }
                if ((Latitude != 0) && (Longitude != 0))
                    sw.WriteLine(Hershey.DrawHF(FixName, Latitude, Longitude, Angle, Scale));
            }
            dvFIX.Dispose();
            dvVOR.Dispose();
            dvNDB.Dispose();
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
                if (row["Comment"].ToString().Length != 0)
                {
                    Comment = row["Comment"].ToString();
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

