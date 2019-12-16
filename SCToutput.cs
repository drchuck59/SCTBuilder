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
        public static void WriteSCT()
        {
            DataTable Colors = Form1.Colors;
            DataTable APT = Form1.APT;
            DataTable TWR = Form1.TWR;
            DataTable RWY = Form1.RWY;
            DataTable VOR = Form1.VOR;
            DataTable NDB = Form1.NDB;
            DataTable FIX = Form1.FIX;
            DataTable ARB = Form1.ARB;
            DataTable AWY = Form1.AWY;
            // DataTable LS = Form1.LocalSector;
            DataTable dtSTL = new SCTdata.StaticTextDataTable();
            DataTable dtColors = new SCTdata.ColorDefsDataTable();
            string path = FolderMgt.OutputFolder + "\\" +
                InfoSection.SponsorARTCC + "_" + CycleInfo.AIRAC + ".sct2";
            // Add header (occurs only once to file)
            string caption = VersionInfo.Title;
            string Message; MessageBoxIcon icon; MessageBoxButtons buttons;
            DialogResult result;
            if (File.Exists(path))
            {
                Message = "OK to overwrite " + path + "?";
                icon = MessageBoxIcon.Question;
                buttons = MessageBoxButtons.YesNo;
                SystemSounds.Question.Play();
                result = MessageBox.Show(Message, caption, buttons, icon);
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
                // Create file
                Console.WriteLine("Begin writing file");
                using (StreamWriter sw = File.CreateText(path))
                {
                    Console.WriteLine("Header...");
                    WriteHeader(sw);
                    Console.WriteLine("ColorDefinitions");
                    sw.WriteLine("; Color definition table");
                    WriteColors(Colors, sw);
                    Console.WriteLine("INFO section...");
                    sw.WriteLine("[INFO]");
                    WriteINFO(sw);
                    if (SCTchecked.ChkVOR)
                    {
                        Console.WriteLine("VORs...");
                        sw.WriteLine("[VOR]");
                        WriteVOR(VOR, sw);
                    }
                    if (SCTchecked.ChkNDB)
                    {
                        Console.WriteLine("NDBs...");
                        sw.WriteLine("[NDB]");
                        WriteNDB(NDB, sw);
                    }
                    if (SCTchecked.ChkAPT)
                    {
                        Console.WriteLine("Airports...");
                        sw.WriteLine("[AIRPORT]");
                        WriteAPT(APT, TWR, sw);
                    }
                    if (SCTchecked.ChkRWY)
                    {
                        Console.WriteLine("Airport Runways...");
                        sw.WriteLine("[RUNWAY]");
                        WriteRWY(RWY, sw, dtSTL);
                    }
                    if (SCTchecked.ChkFIX)
                    {
                        Console.WriteLine("Fixes...");
                        sw.WriteLine("[FIXES]");
                        WriteFixes(FIX, sw);
                    }
                    if (SCTchecked.ChkARB)
                    {
                        Console.WriteLine("ARTCC...");
                        sw.WriteLine("[ARTCC HIGH]");
                        WriteARB(ARB, sw, "UTA");
                        WriteARB(ARB, sw, "FIR ONLY");
                        WriteARB(ARB, sw, "HIGH");
                        WriteARB(ARB, sw, "BDRY");
                        sw.WriteLine("[ARTCC LOW]");
                        WriteARB(ARB, sw, "LOW");
                    }
                    if (SCTchecked.ChkAWY)
                    {
                        Console.WriteLine("AirWays...");
                        sw.WriteLine("[LOW AIRWAY]");
                        WriteAWY(AWY, sw, LOWawy: true);
                        sw.WriteLine("[HIGH AIRWAY]");
                        WriteAWY(AWY, sw, LOWawy: false);
                    }
                    if (SCTchecked.ChkSSD)
                    {
                        Console.WriteLine("SIDS and STARS...");
                        sw.WriteLine("[SID]");
                        WriteSIDSTAR(sw, IsSID: true);
                        sw.WriteLine("[STAR]");
                        WriteSIDSTAR(sw, IsSID: false);
                    }
                    // Console.WriteLine("Local Sectors...");
                    // WriteLS_SID(LS);
                }
                Console.WriteLine("End writing file");
                Message = "Sector file written to" + path + "'";
                buttons = MessageBoxButtons.OK;
                icon = MessageBoxIcon.Information;
                MessageBox.Show(Message, VersionInfo.Title, buttons, icon);
            }
            dtSTL.Dispose();
            dtColors.Dispose();
        }
    private static void WriteHeader(StreamWriter sw)
        {
            string cr = Environment.NewLine;
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
        private static void WriteColors(DataTable dtColors, StreamWriter sw)
        {
            DataView dataView = new DataView(dtColors);
            foreach (DataRowView rowView in dataView)
            {
                sw.WriteLine("#define " + rowView[0] + " " + rowView[1]);
            }
            sw.WriteLine();
            dataView.Dispose();
        }
        private static void WriteINFO(StreamWriter sw)
        {
            sw.WriteLine(InfoSection.SectorName);
            sw.WriteLine(InfoSection.DefaultPosition);
            sw.WriteLine(InfoSection.DefaultAirport);
            sw.WriteLine(Conversions.DecDeg2SCT(Convert.ToSingle(InfoSection.DefaultCenterLatitude)));
            sw.WriteLine(Conversions.DecDeg2SCT(Convert.ToSingle(InfoSection.DefaultCenterLongitude)));
            sw.WriteLine(InfoSection.NMperDegreeLatitude);
            sw.WriteLine(InfoSection.NMperDegreeLongitude);
            sw.WriteLine(InfoSection.MagneticVariation);
            sw.WriteLine(InfoSection.SectorScale);
            sw.WriteLine();
        }
        private static void WriteVOR(DataTable VOR, StreamWriter sw)
        {
            string[] strOut = new string[5]; string LineOut;
            DataView dataView = new DataView(VOR)
            {
                RowFilter = "[Selected]",
                Sort = "FacilityID"
            };
            ;
            foreach (DataRowView row in dataView)
            {
                strOut[0] = row["FacilityID"].ToString();
                strOut[1] = string.Format("{0:000.000}", row["Frequency"]);
                strOut[2] = Conversions.DecDeg2SCT(Convert.ToSingle(row["Latitude"]), true);
                strOut[3] = Conversions.DecDeg2SCT(Convert.ToSingle(row["Longitude"]), false);
                strOut[4] = row["Name"].ToString();
                LineOut = strOut[0] + " " + strOut[1] + " " +
                    strOut[2] + " " + strOut[3] + " ;" + strOut[4];

                if (!LineOut.Contains("-1 "))       // Do NOT write VORs having no fix
                    sw.WriteLine(LineOut);
            }
            dataView.Dispose();
            sw.WriteLine();
        }
        private static void WriteNDB(DataTable NDB, StreamWriter sw)
        {
            string[] strOut = new string[5]; string LineOut;
            DataView dataView = new DataView(NDB)
            {
                RowFilter = "[Selected]",
                Sort = "FacilityID"
            };
            foreach (DataRowView row in dataView)
            {
                strOut[0] = row["FacilityID"].ToString();
                strOut[1] = string.Format("{0:000.000}", row["Frequency"]);
                strOut[2] = Conversions.DecDeg2SCT(Convert.ToSingle(row["Latitude"]), true);
                strOut[3] = Conversions.DecDeg2SCT(Convert.ToSingle(row["Longitude"]), false);
                strOut[4] = row["Name"].ToString();
                LineOut = strOut[0] + " " + strOut[1] + " " +
                    strOut[2] + " " + strOut[3] + " ;" + strOut[4];
                if (!LineOut.Contains("-1 "))       // Do NOT write NDBs having no fix
                    sw.WriteLine(LineOut);
            }
            dataView.Dispose();
            sw.WriteLine();
        }
        private static void WriteAPT(DataTable APT, DataTable TWR, StreamWriter sw)
        {
            string[] strOut = new string[6]; string LineOut;
            DataView dataView = new DataView(APT)
            {
                RowFilter = "[Selected]",
                Sort = "FacilityID"
            };
            // Output only what we need
            DataTable dataTable = dataView.ToTable(true, "ID", "FacilityID", "Latitude", "Longitude", "Name", "Public");
            DataRow foundRow; string Freq;
            foreach (DataRow row in dataTable.AsEnumerable())
            {
                strOut[0] = Conversions.ICOA(row["FacilityID"].ToString()).PadRight(4);
                foundRow = TWR.Rows.Find("[ID] = " + row["ID"].ToString());
                if (foundRow != null)
                    Freq = foundRow["Frequency"].ToString();
                else
                    Freq = "122.8";
                strOut[1] = string.Format("{0:000.000}", Freq);
                strOut[2] = Conversions.DecDeg2SCT(Convert.ToSingle(row["Latitude"]), true);
                strOut[3] = Conversions.DecDeg2SCT(Convert.ToSingle(row["Longitude"]), false);
                strOut[4] = row["Name"].ToString();
                if (Convert.ToBoolean(row["Public"]))
                    strOut[5] = " (Public)";
                else
                    strOut[5] = " {Private}";
                LineOut = strOut[0] + " " + strOut[1] + " " + strOut[2] + " " +
                    strOut[3] + " " + " ;" + strOut[4] + strOut[5];
                if (!LineOut.Contains("-1 "))       // Do NOT write airports having no fix
                    sw.WriteLine(LineOut);
            }
            sw.WriteLine();
            dataView.Dispose();
        }
        private static void WriteFixes(DataTable FIX, StreamWriter sw)
        {
            string[] strOut = new string[5]; string LineOut;
            DataView dataView = new DataView(FIX)
            {
                RowFilter = "[Selected]",
                Sort = "FacilityID"
            };
            foreach (DataRowView row in dataView)
            {
                strOut[0] = row["FacilityID"].ToString();
                strOut[2] = Conversions.DecDeg2SCT(Convert.ToSingle(row["Latitude"]), true);
                strOut[3] = Conversions.DecDeg2SCT(Convert.ToSingle(row["Longitude"]), false);
                strOut[4] = row["Use"].ToString();
                LineOut = strOut[0] + " " + strOut[2] + " " + strOut[3] + " ;" + strOut[4];
                sw.WriteLine(LineOut);
            }
            dataView.Dispose();
            sw.WriteLine();
        }

        private static void WriteRWY(DataTable RWY, StreamWriter sw, DataTable dtSTL)
        {
            string[] strOut = new string[8]; string LineOut; string FacID = string.Empty;
            string RWYtextColor = TextColors.RWYTextColor;
            DataView dataView = new DataView(RWY)
            {
                RowFilter = "[Selected]",
                Sort = "FacilityID, BaseIdentifier"
            };
            foreach (DataRowView row in dataView)
            {
                if (row["FacilityID"].ToString() != FacID)
                {
                    sw.WriteLine("; " + row["FacilityID"].ToString());
                    FacID = row["FacilityID"].ToString();
                }
                strOut[0] = row["BaseIdentifier"].ToString().Trim();
                strOut[1] = row["EndIdentifier"].ToString().Trim();
                strOut[2] = string.Format("{0,3:D3}", row["BaseHeading"].ToString());
                strOut[3] = string.Format("{0,3:D3}", row["EndHeading"].ToString());
                strOut[4] = Conversions.DecDeg2SCT(Convert.ToSingle(row["Latitude"]), true);
                strOut[5] = Conversions.DecDeg2SCT(Convert.ToSingle(row["Longitude"]), false);
                strOut[6] = Conversions.DecDeg2SCT(Convert.ToSingle(row["EndLatitude"]), true);
                strOut[7] = Conversions.DecDeg2SCT(Convert.ToSingle(row["EndLongitude"]), false);
                LineOut = strOut[0] + " " + strOut[1] + " " + strOut[2] + " " + strOut[3] + " "
                    + strOut[4] + " " + strOut[5] + " " + strOut[6] + " " + strOut[7];
                dtSTL.Rows.Add(new object[] { strOut[0].ToString(), strOut[4].ToString(), strOut[5].ToString(), RWYtextColor });
                dtSTL.Rows.Add(new object[] { strOut[1].ToString(), strOut[6].ToString(), strOut[7].ToString(), RWYtextColor });
                sw.WriteLine(LineOut);
            }
            WriteLabels(dtSTL, sw);
            dataView.Dispose();
            sw.WriteLine();
        }
        private static void WriteAWY(DataTable AWY, StreamWriter sw, bool LOWawy = true)
        {
            string LineOut; string CurAwy = string.Empty; string PrevAwy = string.Empty;
            string FirstNavAid = string.Empty; string SecondNavIad = string.Empty;
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
            foreach (DataRowView row in AWYView)
            {
                if (AWYView.Count != 0)
                {
                    if (CurAwy.Length == 0)                               // Initialize loop
                    {
                        CurAwy = row["AWYID"].ToString();
                        FirstNavAid = row["NAVAID"].ToString();
                        Lat0 = Conversions.DecDeg2SCT(Convert.ToSingle(row["Latitude"]), true);
                        Long0 = Conversions.DecDeg2SCT(Convert.ToSingle(row["Longitude"]), false);
                        PrevAwy = CurAwy;
                    }
                    else
                    {
                        CurAwy = row["AWYID"].ToString(); ;
                        if (CurAwy == PrevAwy)                          // If the new row is the same airway...
                        {
                            if (SecondNavIad.Length != 0)                  // Write the line
                            {
                                string str = new string(' ', 27 - CurAwy.Length);
                                if ((FirstNavAid.Length != 0) & (SecondNavIad.Length != 0))     // For the rare occurence of an empty row NavAid, e.g., J1
                                {
                                    LineOut = CurAwy + str;
                                    LineOut += Lat0 + " " + Long0 + " ";
                                    LineOut += Lat1 + " " + Long1;
                                    LineOut += "; " + FirstNavAid + " " + SecondNavIad;
                                    sw.WriteLine(LineOut);
                                }
                                FirstNavAid = SecondNavIad;
                                Lat0 = Lat1;
                                Long0 = Long1;
                                SecondNavIad = string.Empty;
                            }
                            else
                            {
                                SecondNavIad = row["NAVAID"].ToString();
                                Lat1 = Conversions.DecDeg2SCT(Convert.ToSingle(row["Latitude"]), true);
                                Long1 = Conversions.DecDeg2SCT(Convert.ToSingle(row["Longitude"]), false);
                            }
                        }
                        else
                        {
                            PrevAwy = CurAwy;                           // This is a new airway
                            FirstNavAid = row["NAVAID"].ToString();
                            Lat0 = Conversions.DecDeg2SCT(Convert.ToSingle(row["Latitude"]), true);
                            Long0 = Conversions.DecDeg2SCT(Convert.ToSingle(row["Longitude"]), false);
                            SecondNavIad = string.Empty;
                        }
                    }
                }
            }
            AWYView.Dispose();
        }

        public static void WriteSIDSTAR (StreamWriter sw, bool IsSID)
        {
            // Builds the table of Keys by which Write SSD will write the data
            string SSDfilter;
            DataTable APTtable = Form1.APT;
            DataTable SSDtable = Form1.SSD;
            DataTable A2Dtable = new SCTdata.APT2SSDDataTable();
            DataView dvAPT = new DataView(APTtable);
            DataView dvSSD = new DataView(SSDtable);
            char Mark = Convert.ToChar("=");
            if (IsSID) SSDfilter = "[IsSID]"; else SSDfilter = "NOT [IsSID]";
            SSDfilter += " AND [Selected] AND [FixType] = 'AA'";
            dvSSD.RowFilter = SSDfilter;
            dvSSD.Sort = "Sequence";
            // Build the table that will be used to sort and call the WriteSSD function
            A2Dtable.Clear();           // Start with an empty table
            DataView dvS2A = new DataView(A2Dtable);
            foreach (DataRowView SSDrow in dvSSD)
            {
                dvAPT.RowFilter = "[FacilityID] = " + SSDrow["NavAid"].ToString();
                // Check how to add rows to a dataview table
                DataRowView newrow = dvS2A.AddNew();
                newrow["SSD_FK"] = SSDrow["ID"].ToString();
                newrow["APT_FK"] = dvAPT[0]["ID"].ToString();
                newrow["ARTCC"] = dvAPT[0]["ARTCC"].ToString();
                newrow.EndEdit();
            }
            dvS2A.Sort = "ARTCC_FK, APT_FK, SSD_FK";
            // OK to write the Section header here since it's called only once
            string Section;
            if (IsSID) { Section = "SID"; } else { Section = "STAR"; }
            sw.WriteLine(SSDHeader(Section, Mark, 5));

            // Now use that table to call the WriteSSD
            // Sponsor ARTTC first
            string curARTCC = InfoSection.SponsorARTCC.ToString();
            dvS2A.RowFilter = "[ARTCC] = '" + curARTCC + "'";
            sw.WriteLine(SSDHeader(curARTCC, Mark, 4));
            // Loop the SSDIDs in the DV to write the data for Sponsor ARTCC
            string curAirportID = string.Empty;
            foreach (DataRow drS2A in dvS2A)
            {
                if (curAirportID != drS2A["APT_FK"].ToString())
                {
                    curAirportID = drS2A["APT_FK"].ToString();
                    dvAPT.Find(curAirportID);
                    sw.WriteLine(SSDHeader(Conversions.ICOA(dvAPT[0]["FacilityID"].ToString()), Mark, 3));
                }
                WriteSSD(sw, drS2A["SSD_FK"].ToString(), IsSID);
            }

            // All the other ARTCCs that may have been in the filter
            dvS2A.RowFilter = "[ARTCC] != '" + curARTCC + "'";
            // This loop adds the ARTCCs, but is otherwise as above
            foreach (DataRow drS2A in dvS2A)
            {
                if (curARTCC != drS2A["ARTCC"].ToString())
                {
                    curARTCC = drS2A["ARTCC"].ToString();
                    sw.WriteLine(SSDHeader(curARTCC, Mark, 4));
                }
                if (curAirportID != drS2A["APT_FK"].ToString())
                {
                    curAirportID = drS2A["APT_FK"].ToString();
                    dvAPT.Find(curAirportID);
                    sw.WriteLine(SSDHeader(Conversions.ICOA(dvAPT[0]["FacilityID"].ToString()), Mark, 3));
                }
                WriteSSD(sw, drS2A["SSD_FK"].ToString(), IsSID);
            }
            dvS2A.Dispose();
        }

        private static string SSDHeader(string Header, char Marker, int MarkerCount=0)
        {
            string Mask; int factor; string result;
            if (MarkerCount != 0)
            {
                Mask = new string(Marker, MarkerCount);
                factor = 2;
            }
            else
            {
                Mask = Marker.ToString();
                factor = 1;
            }
            string Spaces = new string(' ', 27 - Header.Length - (factor * MarkerCount));
            string DummyCoords = "N000.00.00.000 E000.00.00.000 N000.00.00.000 E000.00.00.000";
            result = Mask + Header;
            if (MarkerCount != 0) result += Mask;
            result += Spaces + DummyCoords;
            return result;
        }

        private static void WriteSSD(StreamWriter sw, string SSDID, bool IsSID)
        {
            ///<summary>
            /// Everything works.  Need to find a way to add the FIXes from the SSDs outside
            /// of the search box, so that the "Find" command can show the FIX.
            /// Also need to test the SSD labeling routine.
            /// </summary>
            DataTable SSD = Form1.SSD;
            char Prefix;
            string Lat0 = string.Empty; string Long0 = string.Empty;
            string Lat1; string Long1; string cr = Environment.NewLine;
            string Fix0 = string.Empty; string Fix1;
            string strLL = new string(' ', 27);
            string SSDfilter = "[ID] = '" + SSDID + "'"; 
            string SSDResult = string.Empty;
            DataView dvSSD = new DataView(SSD)
            {
                RowFilter = SSDfilter,
                Sort = "Sequence"
            };
            if (IsSID) Prefix = Convert.ToChar("+"); else Prefix = Convert.ToChar("-");
            var SSDNames = new List<string>();
            // Write the SSD Header
            sw.WriteLine(SSDHeader(dvSSD[0]["SSDName"].ToString(), Prefix));
            foreach (DataRowView SSDrow in dvSSD)
            {
                Lat1 = Conversions.DecDeg2SCT(Convert.ToSingle(SSDrow["Latitude"]), true);
                Long1 = Conversions.DecDeg2SCT(Convert.ToSingle(SSDrow["Longitude"]), false);
                Fix1 = SSDrow["NavAId"].ToString();
                if ((Fix0.Length != 0) & (Fix0 != Fix1))
                {
                    SSDResult += strLL + Lat0 + " " + Long0 + " " + Lat1 + " " + Long1 + "; " + Fix0 + " " + Fix1 + cr;
                    ListFixes(SSDNames, Fix0);
                    ListFixes(SSDNames, Fix1);
                }
                // If the FixType is "AA", reuse the Fix0-items
                if (SSDrow["FixType"].ToString() != "AA")
                    Lat0 = Lat1; Long0 = Long1; Fix0 = Fix1;
            }
            // Output the result if there were FIXes in the SSD
            if (SSDNames.Count != 0)
            {
                sw.WriteLine(SSDResult);
                WriteFixNames(SSDNames, sw);
                SSDNames.Clear();
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
            float Angle = 0f;
            float Scale = 0.025f;
            float Latitude = 0f;
            float Longitude = 0f;
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
                if ((Latitude != 0) & (Longitude != 0))
                    sw.WriteLine(Hershey.DrawHF(FixName, Latitude, Longitude, Angle, Scale));
            }
            dvFIX.Dispose();
            dvVOR.Dispose();
            dvNDB.Dispose();
        }

        private static void WriteARB(DataTable ARB, StreamWriter sw, string Decode)
        {
            // This doesn't work as designed.  Need to search for affected ARTCCs,
            // then draw all the ARTCCs (filter ARTCC =) with ANY borders in the area.
            // MAY want to do that in the "SELECTED" phase (dgvARB), then sort by ARTCC.
            string FacID0 = string.Empty; string FacID1;
            string ARBname; string HL;  string filter;
            string Lat1; string Long1; string Descr1;
            string Lat0 = string.Empty; string Long0 = string.Empty;
            string LatFirst = string.Empty; string LongFirst = string.Empty;
            switch (Decode)
            {
                case "UTA":
                    filter = "([DECODE] = 'UTA') AND [Selected]"; // 
                    HL = "_H_CTR";
                    break;
                case "FIR ONLY":
                    filter = "([DECODE] = 'FIR ONLY') AND [Selected]"; // 
                    HL = "_H_CTR";
                    break;
                case "BDRY":
                    filter = "([DECODE] = 'BDRY') AND [Selected]"; // 
                    HL = "_H_CTR";
                    break;
                case "HIGH":
                    filter = "([DECODE] = 'HIGH') AND [Selected]"; // 
                    HL = "_H_CTR";
                    break;
                default:
                case "LOW":
                    filter = "([DECODE] = 'LOW') AND [Selected]";     //  
                    HL = "_L_CTR";
                    break;
            }
            // First, find all the ARBs in the group (may be more than one)
            DataView ARBview = new DataView(ARB)
            {
                RowFilter = filter,
                Sort = "Sequence",
            };
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
                        sw.WriteLine("; " + ARBname);
                    }
                    else
                    {
                        FacID1 = ARBdataRowView["ARTCC"].ToString();
                        Descr1 = ARBdataRowView["Description"].ToString();
                        Lat1 = Conversions.DecDeg2SCT(Convert.ToSingle(ARBdataRowView["Latitude"]), true);
                        Long1 = Conversions.DecDeg2SCT(Convert.ToSingle(ARBdataRowView["Longitude"]), false);
                    }
                    if (ARBdataRowView["Description"].ToString().IndexOf("POINT OF BEGINNING") != -1)    // Last line in this group
                    {
                        ARBlist.Add(Lat1); ARBlist.Add(Long1);          // Add the line to close boundary
                        ARBlist.Add(LatFirst); ARBlist.Add(LongFirst);       
                        ARBlist.Add(Descr1);
                        WriteARBItems(sw, ARBlist, FacID1 + HL);
                        ARBlist.Clear();
                        Lat1 = Long1 = FacID1 = string.Empty;
                    }
                    else
                    {
                        if ( (FacID0.Length != 0) & (FacID0 != FacID1) )    // Changed ARTCC
                        {
                            // Do NOT add a line to close boundary
                            WriteARBItems(sw, ARBlist, FacID1 + HL);
                            ARBlist.Clear();
                            ARBlist.Add(Lat0); ARBlist.Add(Long0);
                            ARBlist.Add(Lat1); ARBlist.Add(Long1);
                            ARBlist.Add(Descr1);
                        }
                    }
                    Lat0 = Lat1; Long0 = Long1; FacID0 = FacID1;
                }
                sw.WriteLine();
            }
            ARBview.Dispose();
        }
        private static void WriteARBItems(StreamWriter sw, List<string> myList, string Header)
        {
            int iCounter = 0;
            ArrayList LineOut = new ArrayList(5);
            for (int i = 0; i < myList.Count; i++)
            {
                if (iCounter > 4)
                {
                    sw.WriteLine(Header + " " + LineOut[0].ToString() + " " + LineOut[1].ToString() + " " +
                        LineOut[2].ToString() + " " + LineOut[3].ToString() + "; " + LineOut[4]);
                    iCounter = 0;
                    LineOut.Clear();
                    LineOut.Insert(iCounter, myList[i].ToString());
                    iCounter++;
                }
                else
                {
                    LineOut.Insert(iCounter, myList[i].ToString());
                    iCounter++;
                }
            }
        }
        private static void WriteLabels(DataTable dtSTL, StreamWriter sw)
        {
            string strText; string Lat; string Long; string TextColor;
            // string colorValue = dtColors.Rows[0]["ColorValue"].ToString();
            sw.WriteLine("[LABELS]");
            foreach (DataRow row in dtSTL.AsEnumerable())
            {
                strText = row["LabelText"].ToString();
                strText = "\"" + strText.Trim() + "\"";
                Lat = row["Latitude"].ToString();
                Long = row["Longitude"].ToString();
                TextColor = row["TextColor"].ToString();
                sw.WriteLine(strText + " " + Lat + " " + Long + " " + TextColor);
            }
        }

        public static void WriteLS_SID(DataTable LS)
        {
            ///<summary
            ///FE may build local sector files and have them written to the ARTCC, LOW ARTCC, or HIGH ARTCC
            ///The text file must be configured as shown in the "LocalSectors.txt" file in the package.
            ///</summary>
            string Lat0 = string.Empty; string Long0 = string.Empty; string cr = Environment.NewLine;
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
                if ((Lat0 != string.Empty) & (Lat1 != string.Empty))
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
    }
}

