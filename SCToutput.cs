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
            DataTable SSD = Form1.SSD;
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
                    Console.WriteLine("VORs...");
                    sw.WriteLine("[VOR]");
                    WriteVOR(VOR, sw);
                    Console.WriteLine("NDBs...");
                    sw.WriteLine("[NDB]");
                    WriteNDB(NDB, sw);
                    Console.WriteLine("Airports...");
                    sw.WriteLine("[AIRPORT]");
                    WriteAPT(APT, TWR, sw);
                    Console.WriteLine("Airport Runways...");
                    sw.WriteLine("[RUNWAY]");
                    WriteRWY(RWY, sw, dtSTL);
                    Console.WriteLine("Fixes...");
                    sw.WriteLine("[FIXES]");
                    WriteFixes(FIX, sw);
                    Console.WriteLine("ARTCC...");
                    sw.WriteLine("[ARTCC HIGH]");
                    WriteARB(ARB, sw, "UTA");
                    WriteARB(ARB, sw, "FIR ONLY");
                    WriteARB(ARB, sw, "HIGH");
                    WriteARB(ARB, sw, "BDRY");
                    sw.WriteLine("[ARTCC LOW]");
                    WriteARB(ARB, sw, "LOW");
                    Console.WriteLine("AirWays...");
                    sw.WriteLine("[LOW AIRWAY]");
                    WriteAWY(AWY, sw, LOWawy: true);
                    sw.WriteLine("[HIGH AIRWAY]");
                    WriteAWY(AWY, sw, LOWawy: false);
                    Console.WriteLine("SIDS and STARS...");
                    sw.WriteLine("[SID]");
                    WriteSSD(SSD, sw, IsSID: true);
                    sw.WriteLine("[STAR]");
                    WriteSSD(SSD, sw, IsSID: false);
                    WriteLabels(dtSTL, sw);
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
            DataTable dataTable = dataView.ToTable(true, "ID", "FacilityID", "Latitude", "Longitude", "Name");
            DataRow foundRow; string Freq;
            foreach (DataRow row in dataTable.AsEnumerable())
            {
                strOut[0] = Conversions.ICOA(row["FacilityID"].ToString());
                foundRow = TWR.Rows.Find("[ID] = " + row["ID"].ToString());
                if (foundRow != null)
                    Freq = foundRow["Frequency"].ToString();
                else
                    Freq = "122.8";
                strOut[1] = string.Format("{0:000.000}", Freq);
                strOut[2] = Conversions.DecDeg2SCT(Convert.ToSingle(row["Latitude"]), true);
                strOut[3] = Conversions.DecDeg2SCT(Convert.ToSingle(row["Longitude"]), false);
                strOut[4] = row["Name"].ToString();
                LineOut = strOut[0] + " " + strOut[1] + " " + strOut[2] + " " +
                    strOut[3] + " " + strOut[5] + " ;" + strOut[4];
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
        public static void WriteSSD(DataTable SSD, StreamWriter sw, bool IsSID)
        {
            ///<summary>
            /// Everything works.  Need to find a way to add the FIXes from the SSDs outside
            /// of the search box, so that the "Find" command can show the FIX.
            /// Also need to test the SSD labeling routine.
            /// </summary>
            string Section; string Prefix; string SSDtitle; // bool WriteTitle = true;
            string SSDName; string ID0 = string.Empty; string ID1;
            string Lat0 = string.Empty; string Long0 = string.Empty;
            string Lat1; string Long1; string cr = Environment.NewLine;
            string Fix0 = string.Empty; string Fix1;
            string SSDcode0 = string.Empty; string SSDcode1;
            string SIDtitle = "=====SIDS=====             ";
            string STARtitle = "=====STARS=====           ";
            string DummyCoords = "N000.00.00.000 E000.00.00.000 N000.00.00.000 E000.00.00.000";
            string strARTCC = "====" + InfoSection.SponsorARTCC + "====                ";
            string strLL = new string(' ', 27);
            string str; // For calculated spaces
            string SSDfilter; string SSDResult = string.Empty;
            // Write the Section header
            if (IsSID) { Section = SIDtitle; } else { Section = STARtitle; }
            sw.WriteLine(Section + DummyCoords);
            sw.WriteLine(strARTCC + DummyCoords);
            // 
            DataView dvSSD = new DataView(SSD);
            if (IsSID) Prefix = "+ "; else Prefix = "- ";
            if (IsSID) SSDfilter = "[IsSID]"; else SSDfilter = "NOT [IsSID]";
            SSDfilter += " AND [Selected]";
            dvSSD.RowFilter = SSDfilter;
            dvSSD.Sort = "Sequence";
            var SSDNames = new List<string>();
            foreach (DataRowView SSDrow in dvSSD)
            {
                ID1 = SSDrow["ID"].ToString();
                if (ID0 != ID1)
                {
                    // Output the result if there were FIXes in the SSD
                    if (SSDNames.Count != 0)
                    {
                        sw.WriteLine(SSDResult);
                        WriteFixNames(SSDNames, sw);
                        SSDNames.Clear();
                        SSDResult = string.Empty;
                    }
                    SSDName = SSDrow["SSDName"].ToString();
                    str = new string(' ', 27 - SSDName.Length);
                    // Not all SIDs/STARs have vectors to grid (e.g., Radar Departure with one airport)
                    SSDtitle = Prefix + SSDName + str + DummyCoords + "; ** FAA SSD ID:" + SSDrow["ID"].ToString();
                    SSDResult = cr + SSDtitle + cr;
                    ID0 = ID1;
                    Lat1 = Conversions.DecDeg2SCT(Convert.ToSingle(SSDrow["Latitude"]), true);
                    Long1 = Conversions.DecDeg2SCT(Convert.ToSingle(SSDrow["Longitude"]), false);
                    Fix1 = SSDrow["NavAId"].ToString();
                    SSDcode0 = SSDrow["SSDcode"].ToString();
                }
                else                    //  NOT a new ID, but maybe a transition?
                {
                    if (SSDrow["FixType"].ToString() != "AA")
                    {
                        SSDcode1 = SSDrow["SSDcode"].ToString();
                        Lat1 = Conversions.DecDeg2SCT(Convert.ToSingle(SSDrow["Latitude"]), true);
                        Long1 = Conversions.DecDeg2SCT(Convert.ToSingle(SSDrow["Longitude"]), false);
                        Fix1 = SSDrow["NavAId"].ToString();
                        if ((SSDcode1.Length == 0) ^ (SSDcode1 == SSDcode0))
                        {
                            if ((Fix0.Length != 0) & (Fix0 != Fix1))
                            {
                                SSDResult+= strLL + Lat0 + " " + Long0 + " " + Lat1 + " " + Long1 + "; " + Fix0 + " " + Fix1 + cr;
                                ListFixes(SSDNames, Fix0);
                                ListFixes(SSDNames, Fix1);
                            }
                        }
                        else
                            SSDcode0 = SSDcode1;
                    }
                    else
                        Lat1 = Long1 = Fix1 = string.Empty;
                }
                Lat0 = Lat1; Long0 = Long1; Fix0 = Fix1;
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
    }
}

