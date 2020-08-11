using System;
using System.IO;
using System.Windows.Forms;
using System.Data;
using System.Linq;
using System.Collections.Generic;
using System.Media;
using System.Diagnostics;

namespace SCTBuilder
{
    class ESEoutput
    {
        public static string CycleHeader;
        public static readonly string cr = Environment.NewLine;
        static readonly string PartialPath = FolderMgt.OutputFolder + "\\" + InfoSection.SponsorARTCC + "_ES_";

        public static void WriteESE()
        {
            // DataTable LS = Form1.LocalSector;
            var TextFiles = new List<string>();
            string Message; string path;
            if (SCTchecked.ChkES_SCTfile)
            {
                path = SCTcommon.CheckFile(PartialPath, "Header");
                Console.WriteLine("Header...");
                WriteHeader(path);
                TextFiles.Add(path);

                path = SCTcommon.CheckFile(PartialPath, "Colors");
                Console.WriteLine("ColorDefinitions");
                SCToutput.WriteColors(path);
                TextFiles.Add(path);

                path = SCTcommon.CheckFile(PartialPath, "Info");
                Console.WriteLine("INFO section...");
                SCToutput.WriteINFO(path);
                TextFiles.Add(path);

                path = SCTcommon.CheckFile(PartialPath, "VOR");
                Console.WriteLine("VORs...");
                SCToutput.WriteVOR(path);
                TextFiles.Add(path);

                path = SCTcommon.CheckFile(PartialPath, "NDB");
                Console.WriteLine("VORs...");
                SCToutput.WriteNDB(path);
                TextFiles.Add(path);

                path = SCTcommon.CheckFile(PartialPath, "APT");
                Console.WriteLine("Airports...");
                SCToutput.WriteAPT(path);
                TextFiles.Add(path);

                path = SCTcommon.CheckFile(PartialPath, "RWY");
                Console.WriteLine("Airport Runways...");
                WriteRWY(path);
                TextFiles.Add(path);

                path = SCTcommon.CheckFile(PartialPath, "FIX");
                Console.WriteLine("Fixes...");
                SCToutput.WriteFixes(path);
                TextFiles.Add(path);

                if (SCTchecked.ChkARB)
                {
                    path = SCTcommon.CheckFile(PartialPath, "ARTCC_HIGH");
                    Console.WriteLine("ARTCC HIGH...");
                    SCToutput.WriteARB(path, true);
                    TextFiles.Add(path);

                    path = SCTcommon.CheckFile(PartialPath, "ARTCC_LOW");
                    Console.WriteLine("ARTCC LOW...");
                    SCToutput.WriteARB(path, false);
                    TextFiles.Add(path);
                }

                if (SCTchecked.ChkAWY)
                {
                    Console.WriteLine("Low AirWays...");
                    path = SCTcommon.CheckFile(PartialPath, "AirwayLow");
                    SCToutput.WriteAWY(path, IsLow: true);
                    TextFiles.Add(path);

                    path = SCTcommon.CheckFile(PartialPath, "AirwayHigh");
                    Console.WriteLine("High AirWays...");
                    SCToutput.WriteAWY(path, IsLow: false);
                    TextFiles.Add(path);
                }

                if (SCTchecked.ChkSID)
                {
                    Debug.WriteLine("SIDS...");
                    SCToutput.WriteSIDSTAR(IsSID: true);
                    TextFiles.Add(path);
                }

                if (SCTchecked.ChkSTAR)
                {
                    Debug.WriteLine("STARS...");
                    SCToutput.WriteSIDSTAR(IsSID: false);
                    TextFiles.Add(path);
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
            path = SCTcommon.CheckFile(PartialPath, "SIDSTAR", ".ese");
            if (SCTchecked.ChkES_SSDfile && path != string.Empty)
            {
                Console.WriteLine("SIDS & STARS...");
                WriteSSD(path);
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
                    strOut[4] = Conversions.DecDeg2SCT(Convert.ToSingle(row["Latitude"]), true);
                    strOut[5] = Conversions.DecDeg2SCT(Convert.ToSingle(row["Longitude"]), false);
                    strOut[6] = Conversions.DecDeg2SCT(Convert.ToSingle(row["EndLatitude"]), true);
                    strOut[7] = Conversions.DecDeg2SCT(Convert.ToSingle(row["EndLongitude"]), false);
                    strOut[8] = FacID + "-" + FacName ;
                    sw.Write(SCTstrings.RWYout(strOut, ESformat: true));
                    DRAW.Rows.Add(new object[] { strOut[0].ToString(), strOut[4].ToString(), strOut[5].ToString(), strOut[8].ToString() });
                    DRAW.Rows.Add(new object[] { strOut[1].ToString(), strOut[6].ToString(), strOut[7].ToString(), strOut[8].ToString() });
                }
                WriteRWYLabels(DRAW, sw);
            }
            dvRWY.Dispose();
        }
  
        private static void WriteSSD(string path)
        {
            // ESE's SIDSTAR format is VERY different than VRCs
            // <SID/STAR>:<AIRPORT ICAO>:<RUNWAY>:<TRANSITIONxPROCEDURE>:<ROUTE>
            // Example:
            // SID:PHNL:08L:PALAY2xLNY:PALAY ROSHE LNY 
            // STAR:PHNL:08L:APACKxMAGGI3:APACK TOADS SOPIW MAGGI BAMBO GRITL CKH
            // Calling routine for SID and STAR diagrams
            // SSD = Either SID or STAR, depending on flag
            string SIDmain; string ICOAapt;
            List<string> Rwys = new List<string>();
            List<string> SIDout = new List<string>();
            List<string> STARout = new List<string>();
            DataTable APT = Form1.APT;
            DataTable NGSID = Form1.NGSID;
            DataView dvNGSID = new DataView(NGSID);
            DataTable NGSIDTransition = Form1.NGSIDTransition;
            DataView dvNGSIDTransition = new DataView(NGSIDTransition);
            // Using the SELECTED airports, select the SIDS from NG
            // Get the list of Selected Airports
            DataView dvAPT = new DataView(APT)
            {
                RowFilter = "[Selected]",
                Sort = "FacilityID",
            };
            // Read each NG file as needed to build the ESE text
            using (StreamWriter sw = new StreamWriter(path))
            {
                foreach (DataRowView drvAPT in dvAPT)
                {
                    ICOAapt = Conversions.ICOA(drvAPT["FacilityID"].ToString());
                    string FullFilename = SCTcommon.GetFullPathname(FolderMgt.DataFolder, ICOAapt + ".txt");
                    if (FullFilename.IndexOf("ERROR") == -1)
                    {
                        if (ReadNaviGraph.SIDSTARS(ICOAapt, FullFilename))
                        {
                            if (dvNGSID.Count > 0)
                            {
                                SIDout.Clear();
                                foreach (DataRowView dvrNGSID in dvNGSID)
                                {
                                    Rwys = AddUniqueToList(Rwys, dvrNGSID["Rwy"].ToString());
                                }
                                foreach (string Rwy in Rwys)
                                {
                                    // SID output
                                    dvNGSID.RowFilter = "[FacilityID] = '" + drvAPT["FacilityID"] + "' AND [RWY] = '" + Rwy + "'";
                                    dvNGSIDTransition.RowFilter = "[SSDCode] = '" + dvNGSID[0]["SSDcode"].ToString() + "'";
                                    SIDmain = "SID:" + drvAPT["FacilityID"].ToString() + ":" + Rwy + ":";
                                    SIDmain += dvNGSID[0]["SSDcode"].ToString() + "x" + dvNGSIDTransition[0]["Transition"].ToString() + ":";
                                    SIDout.Add(SIDmain + " " + TransposeFixes(dvNGSID));
                                    SIDmain += " " + TransposeFixes(dvNGSIDTransition) + " " + TransposeFixes(dvNGSID);
                                    SIDout.Add(SIDmain);
                                }
                                // TEST
                                if (SIDout.Count > 0)
                                    foreach (string test in SIDout)
                                        Debug.WriteLine(test);
                            }
                        }
                    }
                    else Debug.WriteLine("Skipped " + ICOAapt);
                }
            }
        }

        private static string TransposeFixes (DataView dataView)
        {
            string result = string.Empty;
            foreach (DataRowView dataRowView in dataView)
            {
                if (dataRowView["Fix"].ToString().Length != 0) result += dataRowView["Fix"].ToString() + " ";
            }
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
