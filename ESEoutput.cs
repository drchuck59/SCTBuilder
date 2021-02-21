using System;
using System.IO;
using System.Windows.Forms;
using System.Data;
using System.Linq;
using System.Collections.Generic;
using System.Media;
using System.Diagnostics;
using System.Linq.Expressions;

namespace SCTBuilder
{
    class ESEoutput
    {
        public static string CycleHeader;
        public static readonly string cr = Environment.NewLine;

        public static void WriteESE()
        {
            // DataTable LS = Form1.LocalSector;
            var TextFiles = new List<string>();
            string Message; string path;
            bool CombineIntoOneFile = SCTchecked.ChkOneESFile;
            string PartialPath = FolderMgt.OutputFolder + "\\" + InfoSection.SponsorARTCC + "_ES_";

            if (SCTchecked.ChkES_SCTfile)
            {
                path = SCTcommon.CheckFile(PartialPath, "Header");
                Console.WriteLine("Header...");
                WriteHeader(path);
                if (CombineIntoOneFile) TextFiles.Add(path);

                path = SCTcommon.CheckFile(PartialPath, "Colors");
                Console.WriteLine("ColorDefinitions");
                SCToutput.WriteColors(path);
                if (CombineIntoOneFile) TextFiles.Add(path);

                path = SCTcommon.CheckFile(PartialPath, "Info");
                Console.WriteLine("INFO section...");
                SCToutput.WriteINFO(path);
                if (CombineIntoOneFile) TextFiles.Add(path);

                if (SCTchecked.ChkVOR)
                {
                    path = SCTcommon.CheckFile(PartialPath, "VOR");
                    Console.WriteLine("VORs...");
                    SCToutput.WriteVOR(path);
                    if (CombineIntoOneFile) TextFiles.Add(path);
                }
                if (SCTchecked.ChkNDB)
                {
                    path = SCTcommon.CheckFile(PartialPath, "NDB");
                    Console.WriteLine("NDBs...");
                    SCToutput.WriteNDB(path);
                    if (CombineIntoOneFile) TextFiles.Add(path);
                }
                if (SCTchecked.ChkFIX)
                {
                    path = SCTcommon.CheckFile(PartialPath, "FIX");
                    Console.WriteLine("Fixes...");
                    SCToutput.WriteFixes(path);
                    if (CombineIntoOneFile) TextFiles.Add(path);
                }
                if (SCTchecked.ChkAPT)
                {
                    path = SCTcommon.CheckFile(PartialPath, "APT");
                    Console.WriteLine("Airports...");
                    WriteAPT(path);
                    if (CombineIntoOneFile) TextFiles.Add(path);
                }
                if (SCTchecked.ChkRWY)
                {
                    path = SCTcommon.CheckFile(PartialPath, "RWY");
                    Console.WriteLine("Airport Runways...");
                    WriteRWY(path);
                    if (CombineIntoOneFile) TextFiles.Add(path);
                }

                if (SCTchecked.ChkARB)
                {
                    path = SCTcommon.CheckFile(PartialPath, "ARTCC_HIGH");
                    Console.WriteLine("ARTCC HIGH...");
                    SCToutput.WriteARB(path, true);
                    if (CombineIntoOneFile) TextFiles.Add(path);

                    path = SCTcommon.CheckFile(PartialPath, "ARTCC_LOW");
                    Console.WriteLine("ARTCC LOW...");
                    SCToutput.WriteARB(path, false);
                    if (CombineIntoOneFile) TextFiles.Add(path);
                }

                if (SCTchecked.ChkAWY)
                {
                    Console.WriteLine("Low AirWays...");
                    path = SCTcommon.CheckFile(PartialPath, "AirwayLow");
                    SCToutput.WriteAWY(path, IsLow: true);
                    if (CombineIntoOneFile) TextFiles.Add(path);

                    path = SCTcommon.CheckFile(PartialPath, "AirwayHigh");
                    Console.WriteLine("High AirWays...");
                    SCToutput.WriteAWY(path, IsLow: false);
                    if (CombineIntoOneFile) TextFiles.Add(path);
                }

                if (SCTchecked.ChkSID)
                {
                    Console.WriteLine("SIDS...");
                    SCToutput.WriteSIDSTAR(PartialPath, IsSID: true);
                    if (CombineIntoOneFile) TextFiles.Add(path);
                }

                if (SCTchecked.ChkSTAR)
                {
                    Console.WriteLine("STARS...");
                    SCToutput.WriteSIDSTAR(PartialPath, IsSID: false);
                    if (CombineIntoOneFile) TextFiles.Add(path);
                }
            }
            Message = TextFiles.Count.ToString() + " text file(s) written to " + PartialPath + cr;

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
                Message += "Sector file written to " + path + cr;
            }
            // ---------------------------  ESE SIDSTAR STARTS HERE
            path = SCTcommon.CheckFile(PartialPath, "SIDSTAR", ".ese");
            if (SCTchecked.ChkES_SSDfile && path != string.Empty)
            {
                Console.WriteLine("SIDS & STARS...");
                WriteSIDSTAR(path);
                Message += "SIDSTARS file written to " + path + cr;
            }
            SCTcommon.SendMessage(Message);
            Console.WriteLine("End writing output files");
        }

        private static void WriteHeader(string path)
        {
            using (StreamWriter sw = File.CreateText(path))
            {
                string Message =
                ";              ** Not for real world navigation **" + cr +
                ";" + cr +
                "; FOR EUROSCOPE - may not work for VRC" +
                "; File may be distributed only as freeware." + cr +
                "; Provided 'as is' - use at your own risk." + cr + cr +
                "; Software-generated sector file using " + VersionInfo.Title + cr +
                "; For questions, contact FE@.zjcartcc.org" + cr +
                "; Sponsoring ARTCC: " + InfoSection.SponsorARTCC + cr +
                "; Facilities Engineer: " + InfoSection.FacilityEngineer + cr +
                "; Assistant Facilities Engineer:" + InfoSection.AsstFacilityEngineer + cr +
                "; AIRAC CYCLE: " + CycleInfo.AIRAC + cr +
                "; Cycle: " + CycleInfo.CycleStart + " to " + CycleInfo.CycleEnd + cr + cr +
                "; Contributers and errata at end of file" + cr + cr;
                sw.WriteLine(Message);
            }
            CycleHeader = cr +
                "; ================================================================" + cr +
                "; AIRAC CYCLE: " + CycleInfo.AIRAC + cr +
                "; Cycle: " + CycleInfo.CycleStart + " to " + CycleInfo.CycleEnd + cr +
                ";       *** EUROSCOPE FILE FORMAT ***" + cr +
                "; ================================================================" + cr;
        }

        public static void WriteAPT(string path)
        {
            // Output looks like:
            // icao freq lat long class
            string strOut;
            string FacID;
            string Lat; string Lon;
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
            DataRow foundRow; string LCL; string Class;
            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.WriteLine(CycleHeader);
                sw.WriteLine("[AIRPORT]");
                foreach (DataRow row in dataTable.AsEnumerable())
                {
                    FacID = row["FacilityID"].ToString(); 
                    if (row["ICAO"].ToString().Length > 0) FacID = row["ICAO"].ToString();
                    LCL = "122.800";
                    Class = "G";
                    dvTWR.Sort = "ID";
                    foundRow = TWR.Rows.Find(row["ID"]);
                    if (foundRow != null)
                    {
                        LCL = foundRow["LCLfreq"].ToString();
                        Class = foundRow["Class"].ToString();
                    }
                    Lat = Conversions.Degrees2SCT(Convert.ToSingle(row["Latitude"]), true);
                    Lon = Conversions.Degrees2SCT(Convert.ToSingle(row["Longitude"]), false);
                    strOut = FacID.PadRight(4) + " " + LCL.PadRight(7) + " " + Lat + " " + Lon + " " + Class;
                    sw.WriteLine(strOut);
                }
            }
            dvAPT.Dispose();
        }

        private static void WriteRWY(string path)
        {
            string[] strOut = new string[9]; string FacID = string.Empty;
            bool FirstLine = true; string FacName = string.Empty;
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
                        FacName = row["FacilityName"].ToString();
                    }
                    strOut[0] = row["BaseIdentifier"].ToString().Trim().PadRight(3);
                    strOut[1] = row["EndIdentifier"].ToString().Trim().PadRight(3);
                    strOut[2] = row["BaseHeading"].ToString().PadRight(3);
                    strOut[3] = row["EndHeading"].ToString().PadRight(3);
                    strOut[4] = Conversions.Degrees2SCT(Convert.ToSingle(row["Latitude"]), true);
                    strOut[5] = Conversions.Degrees2SCT(Convert.ToSingle(row["Longitude"]), false);
                    strOut[6] = Conversions.Degrees2SCT(Convert.ToSingle(row["EndLatitude"]), true);
                    strOut[7] = Conversions.Degrees2SCT(Convert.ToSingle(row["EndLongitude"]), false);
                    strOut[8] = FacID + "-" + FacName ;
                    sw.Write(SCTstrings.RWYout(strOut, ESformat: true));
                    DRAW.Rows.Add(new object[] { strOut[0].ToString(), strOut[4].ToString(), strOut[5].ToString(), strOut[8].ToString() });
                    DRAW.Rows.Add(new object[] { strOut[1].ToString(), strOut[6].ToString(), strOut[7].ToString(), strOut[8].ToString() });
                }
            }
            dvRWY.Dispose();
        }
  
        private static void WriteSIDSTAR(string path)
        {
            // ESE's SIDSTAR format is VERY different than VRCs
            // <SID/STAR>:<AIRPORT ICAO>:<RUNWAY>:<TRANSITIONxPROCEDURE>:<ROUTE>
            // Example:
            // SID:PHNL:08L:PALAY2xLNY:PALAY ROSHE LNY 
            // STAR:PHNL:08L:APACKxMAGGI3:APACK TOADS SOPIW MAGGI BAMBO GRITL CKH
            // Calling routine for SID and STAR diagrams
            // SSD = Either SID or STAR, depending on flag
            string ICOAapt;
            List<string> SSDcodes = new List<string>();
            string SIDout = string.Empty; string STARout = string.Empty;
            DataTable APT = Form1.APT;
            DataView dvNGSID = new DataView(ReadNaviGraph.pmdgSID);
            DataView dvNGSIDTransition = new DataView(ReadNaviGraph.pmdgSIDTransition);
            DataView dvNGSTAR = new DataView(ReadNaviGraph.pmdgSTAR);
            DataView dvNGSTARTransition = new DataView(ReadNaviGraph.pmdgSTARTransition);
            DataView dvNGRWYS = new DataView(ReadNaviGraph.RWYs);
            // Using the SELECTED airports, select the SIDS from NG
            // Get the list of Selected Airports
            DataView dvAPT = new DataView(APT)
            {
                RowFilter = "[Selected] AND (LEN([ICOA]) > 0)",
                Sort = "SSDcode, Sequence",
            };
            // Read each NG file as needed to build the ESE text
            using (StreamWriter sw = new StreamWriter(path))
            {
                foreach (DataRowView drvAPT in dvAPT)
                {
                    ICOAapt = drvAPT["ICAO"].ToString();
                    string FullFilename = SCTcommon.GetFullPathname(FolderMgt.DataFolder, ICOAapt + ".txt");
                    if (FullFilename.IndexOf("ERROR") == -1)
                    {
                        if (ReadNaviGraph.SIDSTARS(ICOAapt, FullFilename))
                        {
                            // Now we have ONE airport of SIDS and STARS in their respective tables
                            if (dvNGSID.Count > 0)
                            {
                                // Loop the SIDs
                                foreach (DataRowView dvrNGSID in dvNGSID)
                                {
                                    SSDcodes = AddUniqueToList(SSDcodes, dvrNGSID["SSDcode"].ToString());
                                }
                                foreach (string SSDcode in SSDcodes)
                                {
                                    Debug.WriteLine("ESEoutput-WriteSSD: Processing " + SSDcode + " in " + ICOAapt);
                                    dvNGSID.RowFilter = "[SSDCode] = '" + SSDcode + "'";
                                    dvNGSIDTransition.RowFilter = "[SSDCode] = '" + SSDcode + "'";
                                    SIDout += OutputSID(ICOAapt, dvNGSID, dvNGSIDTransition, dvNGRWYS);
                                }
                            }
                            if (dvNGSTAR.Count > 0)
                            {
                                // Loop the STARS
                                SSDcodes.Clear();
                                foreach (DataRowView dvrNGSTAR in dvNGSTAR)
                                {
                                    SSDcodes = AddUniqueToList(SSDcodes, dvrNGSTAR["SSDcode"].ToString());
                                }
                                foreach (string SSDcode in SSDcodes)
                                {
                                    Debug.WriteLine("ESEoutput-WriteSSD: Processing " + SSDcode + " in " + ICOAapt);
                                    dvNGSTAR.RowFilter = "[SSDCode] = '" + SSDcode + "'";
                                    dvNGSTARTransition.RowFilter = "[SSDCode] = '" + SSDcode + "'";
                                    // STARout += OutputSTAR(ICOAapt, dvNGSTAR, dvNGSTARTransition, dvNGRWYS);
                                }
                            }
                        }
                    }
                    else Debug.WriteLine("Skipped " + drvAPT["FacilityID"]);
                }
            }
        }

        private static string OutputSID(string ICAO, DataView SID, DataView SIDTransition, DataView RWYS)
        {
            // Expects dataviews for a specific Airport and one SID
            string AptPrefix = "SID:" + ICAO + ":";
            string prefix; string Transition; 
            string SIDfixes = string.Empty; 
            string TransFixes = string.Empty;
            string OrigSIDFilter = SID.RowFilter;
            DataView tempTransition = SIDTransition;
            string result = string.Empty;
            if (SIDTransition.Count > 0)
            {
                // Loop each runway as there's no guarantee every rwy will have this SID
                foreach (DataRowView Rwy in RWYS)
                {
                    prefix = AptPrefix + Rwy.ToString() + ":" + SID[0]["SSDcode"].ToString();
                    SID.RowFilter = OrigSIDFilter + " AND [Rwy] = '" + RWYS + "'";
                    SIDfixes = TransposeFixes(SID);
                    result += prefix + SIDfixes + cr;
                    foreach (DataRowView dvrTransition in SIDTransition)
                    {
                        Transition = dvrTransition["Transition"].ToString();
                        tempTransition.RowFilter = SIDTransition.RowFilter + "[Transition] = '" + Transition + "'";
                        if (tempTransition.Count > 0) TransFixes = TransposeFixes(tempTransition);
                        else TransFixes = string.Empty;
                    }
                    if (TransFixes.Length > 0) result += prefix + TransFixes + SIDfixes + cr;
                }
            }
            else
            {
                // NO transition - just the main fixes
            }
            return result;
        }

        private static string TransposeFixes (DataView dataView)
        {
            string result = string.Empty; List<string> FixList = new List<string>();
            DataTable temp = dataView.ToTable(true, "FIX");
            foreach (DataRow dataRow in temp.Rows)
            {
                AddUniqueToList(FixList, dataRow["FIX"].ToString());
            }
            foreach (string item in FixList) result += item + " ";
            return result.Trim();
        }

        private static List<string> AddUniqueToList(List<string> ListToAdd, string NewListItem)
        {
            bool Found = false;
            foreach (string item in ListToAdd)
            {
                if (item == NewListItem) Found = true;
            }
            if (!Found) ListToAdd.Add(NewListItem);
            return ListToAdd;
        }

        private static void WriteRWYLabels(DataTable dtSTL, StreamWriter sw)
        {
            string strText; string Lat; string Long; string Facility; string Comment;
            string Output;
            // string colorValue = dtColors.Rows[0]["ColorValue"].ToString();
            Debug.WriteLine("WriteRWYLabels...");
            sw.WriteLine("[LABELS]");
            sw.WriteLine("; Runway labels");
            foreach (DataRow row in dtSTL.AsEnumerable())
            {
                strText = row["LabelText"].ToString();
                Lat = row["Latitude"].ToString();
                Long = row["Longitude"].ToString();
                Facility = row["TextColor"].ToString();            // This is actually the Facility ID in ESE
                if (row["Comment"].ToString().Length != 0)
                {
                    Comment = row["Comment"].ToString();
                    Output = SCTstrings.LabelOut(strText, Lat, Long, Facility, Comment);
                }
                else
                    Output = SCTstrings.LabelOut(strText, Lat, Long, Facility);
                sw.WriteLine(Output);
            }
        }
    }
}
