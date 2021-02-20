using System;
using System.IO;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Diagnostics;
using System.Globalization;

namespace SCTBuilder
{
    class ReadNaviGraph
    {
        static string Line = string.Empty;
        static string Airport = string.Empty;
        static readonly List<string> Words = new List<string>();
        static string SSDcode = string.Empty;
        static string SSDmod = string.Empty;
        private static readonly List<string> RNWS = new List<string>();
        static public DataTable cycleinfo = new NGData.cycle_infoDataTable();
        static public DataTable pmdgSID = new NGData.pmdgSIDDataTable();
        static public DataTable pmdgSIDTransition = new NGData.pmdgSIDTransitionDataTable();
        static public DataTable pmdgSTAR = new NGData.pmdgSTARDataTable();
        static public DataTable pmdgSTARTransition = new NGData.pmdgSTARTransitionDataTable();
        static public DataTable STARRNW = new NGData.pmdgSTARRNWDataTable();
        static public DataTable FIXes = new NGData.wpNavFIXDataTable();
        static public DataTable RWYs = new NGData.pmdgRWYDataTable();
        static public DataTable wpNavRTE = new NGData.wpNavRTEDataTable();
        static public DataTable airports = new NGData.airportsDataTable();
        static public DataTable wpNavAPT = new NGData.wpNavAPTDataTable();
        static public DataTable wpNavAID = new NGData.wpNavAIDDataTable();
        static public DataTable wpNavFIX = new NGData.wpNavFIXDataTable();

        public static void SelectNGTables(string filter)
        {
            SelectNGItems(ReadNaviGraph.airports, filter);
            SelectNGItems(ReadNaviGraph.wpNavAID, filter);
            SelectNGItems(ReadNaviGraph.wpNavAPT, filter);
            SelectNGItems(ReadNaviGraph.wpNavFIX, filter);
        }

        private static int SelectNGItems(DataTable dt, string filter)
        {
            DataView dataView = new DataView(dt);
            SetClearSelected(dataView, false);
            Debug.Print(filter);
            dataView.RowFilter = filter;
            int result = dataView.Count;
            SetClearSelected(dataView, true);
            dataView.Dispose();
            return result;
        }

        private static void SetClearSelected(DataView dv, bool set)
        {
            // If the filter is applied, selected boxes are true
            // otherwise, ALL the selected boxes are false
            // But if the ShowAll box is checked, ignore the update
            foreach (DataRowView row in dv)
            {
                row["Selected"] = set;
            }
        }

        public static int NavAID()
        {
            string FullFilename = SCTcommon.GetFullPathname(FolderMgt.DataFolder, "wpNavAID.txt");
            DataView dv = new DataView(wpNavAID);
            int result = 0;
            if (FullFilename.IndexOf("Error") == -1)
            {
                wpNavAID.Clear();
                using (StreamReader sr = new StreamReader(FullFilename))
                {
                    while ((Line = sr.ReadLine()) != null)
                    {
                        if (Line.Substring(0, 1) != ";")
                        {
                            DataRowView drv = dv.AddNew();
                            drv["Name"] = Line.Substring(0, 14).Trim();
                            drv["FacilityID"] = Line.Substring(24, 5).Trim();
                            drv["Type"] = Line.Substring(29, 4).Trim();
                            drv["Latitude"] = Convert.ToDouble(Line.Substring(33, 10).Trim());
                            drv["Longitude"] = Convert.ToDouble(Line.Substring(43, 11).Trim());
                            drv["Frequency"] = Line.Substring(54, 6).Trim();
                            drv.EndEdit();
                        }
                    }
                }
                result = dv.Count;
                Console.WriteLine(result + " rows added to" + dv.Table.TableName);
            }
            dv.Dispose();
            return result;
        }

        public static int  NavFIX()
        {
            string FullFilename = SCTcommon.GetFullPathname(FolderMgt.DataFolder, "wpNavFIX.txt");
            DataView dv = new DataView(FIXes); 
            int result = 0;
            if (FullFilename.IndexOf("Error") == -1)
            {
                using (StreamReader sr = new StreamReader(FullFilename))
                {
                    while ((Line = sr.ReadLine()) != null)
                    {
                        if (Line.Substring(0, 1) != ";")
                        {
                            DataRowView drv = dv.AddNew();
                            drv = dv.AddNew();
                            drv["FacilityID"] = Line.Substring(0, 5).Trim();
                            drv[1] = Convert.ToDouble(Line.Substring(29, 10).Trim());
                            drv[2] = Convert.ToDouble(Line.Substring(39, 11).Trim());
                            drv.EndEdit();
                        }
                    }
                    result = dv.Count;
                    Console.WriteLine(result + " rows added to " + dv.Table.TableName);
                }
            }
            dv.Dispose();
            return result;
        }

        public static int Airports()
        {
            // Add the Airports index (has only ICAO and Lat/Lon)
            // Use the wpNavAPT file for runways and Airport names
            // RETURNS number of rows added (0 for no file or failed)
            int result = 0; const int ICOA = 0; const int Lat = 1; const int Lon = 2;
            string FullFilename = SCTcommon.GetFullPathname(FolderMgt.DataFolder, "airports.dat");
            DataView dv = new DataView(airports);
            if (FullFilename.IndexOf("Error") == -1)
            {
                using (StreamReader sr = new StreamReader(FullFilename))
                {
                    while ((Line = sr.ReadLine()) != null)    // Until EOF
                    {
                        if (Line.Substring(0, 1) != ";")     // Check for comment
                        {
                            DataRowView drv = dv.AddNew();
                            drv[ICOA] = Line.Substring(0, 4);
                            drv[Lat] = Convert.ToDouble(Line.Substring(4, 10).Trim());
                            drv[Lon] = Convert.ToDouble(Line.Substring(14, 11).Trim());
                            drv.EndEdit();
                        }
                    }
                    result = dv.Count;
                    Console.WriteLine(result + " rows added to" + dv.Table.TableName);
                }
            }
            dv.Dispose();
            return result;
    }

        public static int NavAPT()
        {
            // Add the NavAPT file to the DataTable
            // Because this file is column-delimited, cannot use ParseLine
            // RETURNS number of rows read (0 if no file or failed)
            int result = 0;
            const int FacID = 0; const int Name = 1; const int RWY = 2; const int Length = 3; const int Brg = 4;
            const int FinalBrg = 8;  const int Lat = 5; const int Lon = 6; const int Freq = 7; const int Elev = 9;

            string FullFilename = SCTcommon.GetFullPathname(FolderMgt.DataFolder, "wpNavAPT.txt");
            DataView dv = new DataView(wpNavAPT);
            if (FullFilename.IndexOf("Error") == -1)
            {
                using (StreamReader sr = new StreamReader(FullFilename))
                {
                    while ((Line = sr.ReadLine()) != null)    // Until EOF
                    {
                        if (Line.Substring(0, 1) != ";")     // Check for comment
                        {
                            DataRowView drv = dv.AddNew();
                            drv[Name] = Line.Substring(0, 24);
                            drv[FacID] = Line.Substring(24, 4);
                            drv[RWY] = Line.Substring(28, 3).Trim();
                            drv[Length] = Convert.ToInt32(Line.Substring(31, 5));
                            drv[Brg] = Convert.ToInt32(Line.Substring(36, 3));
                            drv[Lat] = Convert.ToDouble(Line.Substring(39, 10).Trim());
                            drv[Lon] = Convert.ToDouble(Line.Substring(49, 11).Trim());
                            drv[Freq] = Convert.ToDouble(Line.Substring(60, 6).Trim());
                            drv[FinalBrg] = Convert.ToInt32(Line.Substring(66, 3));
                            drv[Elev] = Convert.ToInt32(Line.Substring(69, 5));
                            drv.EndEdit();
                        }
                    }
                    result = dv.Count;
                    Console.WriteLine(result + " rows added to" + dv.Table.TableName);
                }
            }
            dv.Dispose();
            return result;
        }

        public static int  NavRTE()
        {
            // Add NaviGraph routes (wpNavRTE)
            // Returns number of rows read to datatable (0 if failed, no file)
            string FullFilename = SCTcommon.GetFullPathname(FolderMgt.DataFolder, "wpNavRTE.txt");
            DataView dv = new DataView(wpNavRTE);
            int result = 0;
            if (FullFilename.IndexOf("Error") == -1)
            {
                using (StreamReader sr = new StreamReader(FullFilename))
                {
                    while ((Line=sr.ReadLine()) != null)    // Until EOF
                    {
                        if (Line.Substring(0, 1) != ";")     // Check for comment
                        {
                            AddRTE(dv);
                        }
                    }
                    result = dv.Count;
                    Console.WriteLine(result + " rows added to" + dv.Table.TableName);
                }
            }
            dv.Dispose();
            return result;
        }

        private static void AddRTE(DataView dv)
        {
            // Reads one row of wpNavRTE data
            string[] LowAirways = new string[6] {"V", "A", "B", "G", "R", "T"};
            string curID;
            DataRowView drv = dv.AddNew();
            Words.Clear();
            Words.AddRange(ParseLine());
            drv[0] = curID = Words[0].ToString();             // Airway ID
            drv[1] = Convert.ToInt32(Words[1]);               // Sequence
            drv[2] = Words[2].ToString();                     // NavAid
            drv[3] = Convert.ToDouble(Words[3]);              // Latitude
            drv[4] = Convert.ToDouble(Words[4]);              // Longitude
            drv[5] = LowAirways.Any(curID.Contains);          // IsLow
            drv.EndEdit();
        }

        public static bool SIDSTARS(string Apt, string FullFilename)
        {
            // The output strings will be
            // <SID/STAR>:<AIRPORT ICAO>:<RUNWAY>:<TRANSITIONxPROCEDURE>:<ROUTE>
            // There will be a line for every RWY with the Procedure and every RWY with Transition.Procedure
            // Therefore, read all the procedures for each runway and save transitions to reuse later
            Airport = Apt;
            bool result = false;
            string Section = string.Empty; string Level; string Content; string Addenda;
            using (StreamReader reader = new StreamReader(FullFilename))
            {
                while ((Line = reader.ReadLine()) != null)
                {
                    Words.Clear();                              // Start fresh
                    if ((Line.IndexOf("//") == -1) && (Line.Length > 0))
                    {
                        Level = GetLevel();
                        switch (Level)                       // Which "level" is the line
                        {
                            case "Title":                       // No leading whitespace
                                Section = GetLineID();
                                switch (Section)               // Switch based upon the section we are working with
                                {
                                    case "FIXES":               // Indicates start of the FIXes data
                                        break;
                                    case "FIX":                 // FiX data - don't need anything except the name but capture all
                                        AddFixString();
                                        break;
                                    case "ENDFIXES":        // Indicates end of the FIXES data
                                        Section = string.Empty;
                                        break;
                                    case "RNWS":            // Indicates start of the RNW data
                                        break;
                                    case "RNW":             // As a title only identifies each RNW in the data
                                        Words.AddRange(ParseLine());
                                        RNWS.Add(Words[1]);
                                        break;
                                    case "ENDRNWS":         // Indicates end of the RNW data
                                        Section = string.Empty;
                                        break;
                                    case "SIDS":            // Indicates start of the SIDs data
                                        break;
                                    case "SID":
                                        // This begins the read of one SID
                                        Words.AddRange(ParseLine());
                                        SSDcode = Words[1];     // This is always the SSDcode
                                                                // Usually these are multiline and we need the rest of the entry to load
                                                                // Check for a single-line SID - special handling
                                        if (SCTcommon.GetListItemCount(Words, "RNW") != 0)
                                            AddSID_OneLine();
                                        break;
                                    case "STARS":            // Indicates start of the STARs data
                                        break;
                                    case "STAR":
                                        // This begins the read of one STAR
                                        Words.AddRange(ParseLine());
                                        SSDcode = AddSTAR();
                                        break;
                                    case "APPROACHES":      // Indicates start of the APPROACHes data
                                        break;
                                    case "APPROACH":
                                        // This begins the read of one APPROACH
                                        // **** ENTER APPROACH SUBROUTINE ****
                                        Words.AddRange(ParseLine());
                                        SSDcode = Words[1];
                                        break;
                                    case "//":                  // Comment line
                                    default:
                                        break;
                                }
                                break;
                            case "Content":                     // This line has one whitespace.  Again, each word is unique to SID or STAR
                                Content = GetLineID();
                                switch (Content)
                                {
                                    case "RNW":                 // SID RNW information for the current SID (SSDcode)
                                        Words.AddRange(ParseLine());
                                        AddSID();
                                        break;
                                    case "TRANSITION":          // TRANSITION information for a STAR or APPROACH
                                        if (Section == "STAR")
                                        {
                                            Words.AddRange(ParseLine());
                                            STARTransition();
                                        }
                                        break;
                                }
                                break;
                            case "Addenda":                     // This line has two whitespaces.  Each word is unique to a SID or STAR
                                Addenda = GetLineID();
                                switch (Addenda)
                                {
                                    case "RNW":                  //Only occurs in STARS
                                        Words.AddRange(ParseLine());
                                        STAR_RNWS();
                                        break;
                                    case "TRANSITION":          // Only occurs in SIDs  SID transitions apply to all RNWs, so don't need a complex DataTable
                                        Words.AddRange(ParseLine());
                                        SIDTransition();
                                        break;
                                }
                                break;
                            case "":
                                break;
                        }
                    }
                }
                SaveRwys();
                result = true;
            }
            return result;
        }

        private static void SaveRwys()
        {
            DataView dvRWYS = new DataView(RWYs);
            DataRowView drvRWYS;
            foreach (string r in RNWS)
            {
                drvRWYS = dvRWYS.AddNew();
                drvRWYS["FacilityID"] = Airport;
                drvRWYS["Rwy"] = r;
                drvRWYS.EndEdit();
            }
            dvRWYS.Dispose();
        }

        private static void AddFixString()
        {
            // Parse the NaviGraph line for FIX in FIXES section
            string temp = Line;
            temp = temp.Substring(4);
            int LocFix = temp.IndexOf(" ");
            string FixName = temp.Substring(0, temp.IndexOf(" ")).Trim();
            temp = temp.Substring(LocFix + 7).Trim();
            string quadrant = temp.Substring(0, 1);
            temp = temp.Substring(2).Trim();
            float decLat = Convert.ToSingle(temp.Substring(0, 2));
            temp = temp.Substring(3).Trim();
            int LocLat = temp.IndexOf(" ");
            decLat += Convert.ToSingle(temp.Substring(0, LocLat)) / 60;
            temp = temp.Substring(LocLat).Trim();
            if (quadrant == "S") decLat *= -1;
            temp = temp.Substring(2).Trim();
            int LocLon = temp.IndexOf(" ");
            float decLon = Convert.ToSingle(temp.Substring(0, LocLon));
            temp = temp.Substring(LocLon).Trim();
            decLon += Convert.ToSingle(temp) / 60;
            DataView dvNGFix = new DataView(FIXes);
            DataRowView newrow = dvNGFix.AddNew();
            newrow["FacilityID"] = FixName;
            newrow["Latitude"] = decLat;        
            newrow["Longitude"] = decLon;
            newrow.EndEdit();
            dvNGFix.Dispose();
        }

        private static void AddSID_OneLine()
        {
            // Read a SID line which has everything in one line
            // These have no transitions, so can directly enter into table
            DataView dvNGSID = new DataView(pmdgSID);
            DataRowView newFIX;
            string runway = string.Empty; int Sequence;
            string fix = string.Empty; string oldfix = string.Empty;
            //Build a list of RNWs then a list of FIXes
            for (int r = 3; r < Words.Count; r++)          //  Start at 3d word
            {
                if (Words[r] == "RNW") runway = Words[r + 1];
                {
                    // Loop the RNWs and add the FIXes
                    Sequence = 0;
                    for (int w = 1; w < Words.Count; w++)
                    {
                        if (Words[w] == "FIX")
                        {
                            if (Words[w + 1] == "OVERFLY")
                                fix += Words[w + 2] + " ";
                            else
                                fix += Words[w + 1] + " ";
                            if (oldfix != fix)
                            {
                                Sequence++;
                                newFIX = dvNGSID.AddNew();
                                newFIX["FacilityID"] = Airport;
                                newFIX["SSDcode"] = SSDcode;
                                newFIX["RWY"] = runway;
                                newFIX["Fix"] = fix;
                                newFIX["AltAOA"] = AoARestrict(fix);
                                newFIX["AltAOB"] = AoBRestrict(fix);
                                newFIX["AltAt"] = AltAtRestrict(fix);
                                newFIX["Speed"] = SpeedRestrict(fix);
                                newFIX["Sequence"] = Sequence;
                                newFIX.EndEdit();
                                oldfix = fix;
                            }
                        }
                    }
                }
            }
            dvNGSID.Dispose();
        }


        private static void AddSID()
        {
            // These are multiline SIDs with one or more RNWs and a series of instructions
            // Can load each RNW as part of the SID
            DataView dvNGSID = new DataView(pmdgSID);
            DataRowView newFIX;
            string runway; int Sequence; string Radial;
            string fix = string.Empty; string oldfix = string.Empty;
            //Build a list of RNWs then a list of FIXes
            runway = Words[2];
            for (int r = 1; r < Words.Count; r++)          //  Start at 2d word (RNW)
            {
                Sequence = 0;
                // See if SID has RWY departure instructions
                if (Line.IndexOf("HDG") != -1)
                {
                    int WordStart = 0;
                    Sequence++;
                    newFIX = dvNGSID.AddNew();
                    newFIX["FacilityID"] = Airport;
                    newFIX["SSDcode"] = SSDcode;
                    newFIX["RWY"] = runway;
                    newFIX["Hdg"] = GetValue("HDG");
                    newFIX["UntilAlt"] = GetValue("UNTIL");
                    Radial = GetValue("RADIAL");
                    if (Radial.Length != 0)
                    {
                        newFIX["Radial"] = Radial;
                        newFIX["FIX"] = GetValue("FIX");
                    }
                    newFIX["Sequence"] = Sequence;
                    newFIX.EndEdit();
                    // Find first occurence of Fix for above, and save the Loc (will need to skip this FIX in next section)
                    for (int w = 1; w < Words.Count; w++)
                    {
                        if (Words[w] == "FIX")
                        {
                            WordStart = w + 1;
                            break;
                        }
                    }
                    for (int w = WordStart; w < Words.Count; w++)      // See what I did here?  I skipped the takeoff reference FIX
                    {
                        if (Words[w] == "FIX")
                        {
                            if (Words[w + 1] == "OVERFLY")  // Should never occur but...
                                fix += Words[w + 2] + " ";
                            else
                                fix += Words[w + 1] + " ";
                            if (oldfix != fix)
                            {
                                Sequence++;
                                newFIX = dvNGSID.AddNew();
                                newFIX["FacilityID"] = Airport;
                                newFIX["SSDcode"] = SSDcode;
                                newFIX["RWY"] = runway;
                                newFIX["AltAOA"] = AoARestrict(fix);
                                newFIX["AltAOB"] = AoBRestrict(fix);
                                newFIX["AltAt"] = AltAtRestrict(fix);
                                newFIX["Speed"] = SpeedRestrict(fix);
                                newFIX["Sequence"] = Sequence;
                                newFIX.EndEdit();
                                oldfix = fix;
                            }
                        }
                    }
                }
                else
                // SID did NOT have RWY departure instructions, so can go directly to loading the waypoints
                {
                    for (int w = 1; w < Words.Count; w++)
                    {
                        if (Words[w] == "FIX")
                        {
                            if (Words[w + 1] == "OVERFLY")
                                fix += Words[w + 2] + " ";
                            else
                                fix += Words[w + 1] + " ";
                            if (oldfix != fix)
                            {
                                Sequence++;
                                newFIX = dvNGSID.AddNew();
                                newFIX["FacilityID"] = Airport;
                                newFIX["SSDcode"] = SSDcode;
                                newFIX["RWY"] = runway;
                                if (Line.Length > 0)
                                {
                                    newFIX["AltAOA"] = AoARestrict(fix);
                                    newFIX["AltAOB"] = AoBRestrict(fix);
                                    newFIX["AltAt"] = AltAtRestrict(fix);
                                    newFIX["Speed"] = SpeedRestrict(fix);
                                }
                                newFIX["Sequence"] = Sequence;
                                newFIX.EndEdit();
                                oldfix = fix;
                            }
                        }
                    }
                }
            }
            dvNGSID.Dispose();
        }

        private static void SIDTransition()
        {
            // SID Transition FIXes do not have restrictions
            DataView dvNGSIDTransition = new DataView(pmdgSIDTransition);
            DataRowView newrow; int Sequence = 0;
            string Transition = Words[2];
            for (int t = 1; t < Words.Count; t++)
            {
                if (Words[t] == "FIX")
                {
                    Sequence++;
                    newrow = dvNGSIDTransition.AddNew();
                    newrow["SSDCode"] = SSDcode;
                    newrow["Transition"] = Transition;
                    newrow["Fix"] = Words[t + 1];
                    newrow["Sequence"] = Sequence;
                    newrow.EndEdit();
                }
            }
        }

        private static string AddSTAR()
        {
            // Add the STAR information.  We'll add the RNWs later.
            DataView dvNGStar = new DataView(pmdgSTAR);
            DataRowView newFIX; int Sequence = 0; string fix;
            // This next line captures the modifier in the STAR Name
            if (Words[2].IndexOf(".") != -1)
                SSDmod = Words[2].Substring(Words[2].IndexOf(".") + 1, 2);
            else SSDmod = string.Empty;
            // Load the STAR information (Runways and Transitions get added later)
            for (int f = 1; f < Words.Count; f++)
            {
                SSDcode = Words[2];
                if (Words[f] == "FIX")
                {
                    Sequence++;
                    fix = Words[f + 1];
                    newFIX = dvNGStar.AddNew();
                    newFIX["FacilityID"] = Airport;
                    newFIX["SSDcode"] = SSDcode;
                    newFIX["SSDmodifier"] = SSDmod;
                    newFIX["Fix"] = fix;
                    newFIX["AltAOA"] = AoARestrict(fix);
                    newFIX["AltAOB"] = AoBRestrict(fix);
                    newFIX["AltAt"] = AltAtRestrict(fix);
                    newFIX["Speed"] = SpeedRestrict(fix);
                    newFIX["Sequence"] = Sequence;
                    newFIX.EndEdit();
                }
            }
            dvNGStar.Dispose();
            return SSDcode;
        }

        private static void STAR_RNWS()
        {
            // Add the RNWs applicable to the SSDcode+SSDmode for the Airport
            DataView dvNGSTAR = new DataView(pmdgSTAR);
            DataRowView drvNGSTAR;
            for (int r = 1; r < Words.Count; r++)
            {
                if (Words[r] == "RNW")
                {
                    drvNGSTAR = dvNGSTAR.AddNew();
                    drvNGSTAR["FacilityID"] = Airport;
                    drvNGSTAR["SSDcode"] = SSDcode;
                    drvNGSTAR["SSDmodifier"] = SSDmod;
                    drvNGSTAR["Fix"] = Words[r + 1];
                    drvNGSTAR.EndEdit();
                }
            }
            dvNGSTAR.Dispose();
        }

        private static void STARTransition()
        {
            // Transitions apply to each SSDcode+SSDmod (if present, usually is)
            string TransitionName = Words[2];
            string fix; DataRowView newFIX; int Sequence = 0;
            DataView dvNGSTARTransition = new DataView(pmdgSTARTransition);
            {
                for (int t = 1; t < Words.Count; t++)
                {
                    if (Words[t] == "FIX")
                    {
                        fix = Words[t + 1];
                        newFIX = dvNGSTARTransition.AddNew();
                        newFIX["FacilityID"] = Airport;
                        newFIX["SSDcode"] = SSDcode;
                        newFIX["SSDmodifier"] = SSDmod;
                        newFIX["TransitionName"] = TransitionName;
                        newFIX["Fix"] = fix;
                        newFIX["AltAOA"] = AoARestrict(fix);
                        newFIX["AltAOB"] = AoBRestrict(fix);
                        newFIX["AltAt"] = AltAtRestrict(fix);
                        newFIX["Speed"] = SpeedRestrict(fix);
                        newFIX["Sequence"] = Sequence;
                        newFIX.EndEdit();
                    }
                }
            }
            dvNGSTARTransition.Dispose();
        }

        private static string SpeedRestrict(string FIX)
        {
            string result = string.Empty;  int LocNextFix;
            int LocFix = Line.IndexOf(FIX.Trim());
            if (LocFix != -1)
            {
                int LocSpeed = Line.IndexOf("SPEED", LocFix);
                if (LocSpeed != -1)
                {
                    LocNextFix = Line.IndexOf("FIX", LocFix);
                    if ((LocNextFix > LocSpeed) || (LocNextFix == -1))
                    {
                        result = Line.Substring(LocSpeed + 6, 3);
                    }
                }
            }
            if (result.Length > 0) Debug.Print("SPEED result: " + result + " for " + FIX);
            return result;
        }

        private static string AltAtRestrict(string FIX)
        {
            // Returns an At or Above restriction, if it exists
            string result = string.Empty; int LocNextFix;
            int EndAT; int EndAlt;
            int ATlength = "AT".Length;
            int LocFix = Line.IndexOf(FIX.Trim());
            if (LocFix != -1)
            {
                // Need a do-until here to find AT that is NOT AOA or AOB!!
                int LocAT = Line.IndexOf("AT", LocFix);
                if ((LocAT != -1) && (Line.IndexOf("OR", LocAT) == -1))
                {
                    LocNextFix = Line.IndexOf("FIX", LocFix);
                    if (LocNextFix == -1)
                    {
                        result = Line.Substring(LocAT + ATlength).Trim();
                    }
                    else if (LocNextFix > LocAT)
                    {
                        EndAT = Line.IndexOf(" ", LocAT + ATlength);
                        EndAlt = Line.IndexOf(" ", EndAT + 1);
                        result = Line.Substring(EndAT, EndAlt - EndAT).Trim();
                    }
                    if (!result.All(char.IsDigit)) result = string.Empty;
                }
            }
            if (result.Length > 0) Debug.Print("AT result: " + result + " for " + FIX);
            return result;
        }

        private static string AoARestrict (string FIX)
        {
            // Returns an At or Above restriction, if it exists
            string result = string.Empty; int LocNextFix;
            int EndAOA; int EndAlt;
            int AOAlength = "AT OR ABOVE".Length;
            int LocFix = Line.IndexOf(FIX.Trim());
            if (LocFix != -1)
            {
                int LocAOA = Line.IndexOf("AT OR ABOVE", LocFix);
                if (LocAOA != -1)
                {
                    LocNextFix = Line.IndexOf("FIX", LocFix);
                    if (LocNextFix == -1)
                    {
                        result = Line.Substring(LocAOA + AOAlength).Trim();
                    }
                    else if (LocNextFix > LocAOA)
                    {
                        EndAOA = Line.IndexOf(" ", LocAOA + AOAlength);
                        EndAlt = Line.IndexOf(" ", EndAOA + 1);
                        result = Line.Substring(EndAOA, EndAlt - EndAOA).Trim();
                    }
                }
            }
            if (result.Length > 0) Debug.Print("AOA result: " + result + " for " + FIX);
            return result;
        }

        private static string AoBRestrict(string FIX)
        {
            // Returns an At or Above restriction, if it exists
            string result = string.Empty; int LocNextFix;
            int LocFix = Line.IndexOf(FIX.Trim());
            int EndAOB; int EndAlt;
            int AOBlength = "AT OR BELOW".Length;
            if (LocFix != -1)
            {
                int LocAOB = Line.IndexOf("AT OR BELOW", LocFix);
                if (LocAOB != -1)
                {
                    LocNextFix = Line.IndexOf("FIX", LocFix);
                    if (LocNextFix == -1)
                    {
                        result = Line.Substring(LocAOB + AOBlength).Trim();
                    }
                    else if (LocNextFix > LocAOB)
                    {
                        EndAOB = Line.IndexOf(" ", LocAOB + AOBlength);
                        EndAlt = Line.IndexOf(" ", EndAOB + 1);
                        result = Line.Substring(EndAOB, EndAlt - EndAOB).Trim();
                    }
                }
            }
            if (result.Length > 0) Debug.Print("AOB result: " + result + " for " + FIX);
            return result;
        }


        private static string GetValue(string Keyword)
        {
            // Returns an At or Above restriction, if it exists
            string result = string.Empty;
            if (Line.IndexOf(Keyword) != -1)
            {
                for (int h = 1; h < Words.Count; h++)
                {
                    if (Words[h] == Keyword)
                    {
                        result = Words[h + 1];
                        h = Words.Count + 1;            // Force break (Could've just used break; but it's not clean coding
                    }
                }
            }
            return result;
        }

        private static string GetLevel()
        {
            string result = string.Empty;
            if ((Line.Length != 0) && (Line.Substring(0, 2) != "//"))    // Blank line or comment
            {
                if (Line.Substring(0, 1) == " ") result = "Content";
                else if (Line.Substring(0, 1) == "  ") result = "Addenda";
                else result = "Title";
            }
            return result;
        }

        private static string GetLineID()
        {
            string temp = Line.Trim();
            int Loc1 = temp.IndexOf(" ");
            if (Loc1 != -1)
                return temp.Substring(0, temp.IndexOf(" "));
            else
                return temp;
        }

        private static string[] ParseLine()
        {
            // Parse the line into individual words
            // Blank and comments assumed to be skipped (not checked here)
            List<string> Words = new List<string>();
            string temp = Line;
            int Loc1;
            // Single Word
            while (temp.Length != 0)
            {
                Loc1 = temp.IndexOf(" ");
                if (Loc1 != -1)
                {
                    // Test for whitespace string
                    if (!string.IsNullOrWhiteSpace(temp.Substring(0, Loc1).Trim()))
                    {
                        Words.Add(temp.Substring(0, Loc1).Trim());
                        temp = temp.Substring(Loc1).Trim();
                    }
                }
                else
                {
                    Words.Add(temp.Trim());
                    temp = string.Empty;
                }
            }
            return Words.ToArray();
        }

        public static int AIRAC()
        {
            // Find the cycleinfo.txt file.  -1 if doesnt exist (bad folder) or return AIRAC
            int result = -1; string Line; string sResult; string sTemp;
            string FullFilename = SCTcommon.GetFullPathname(FolderMgt.DataFolder, "cycle_info.txt");
            if (FullFilename.IndexOf("ERROR") != -1) return result;
            DataView dv = new DataView(cycleinfo);
            DataRowView drv = dv.AddNew();
            using (StreamReader reader = new StreamReader(FullFilename))
            {
                Line = reader.ReadLine();
                if (result == -1)
                {
                    if (Line.IndexOf("AIRAC") != -1)
                    {
                        sResult = Line.Substring(Line.IndexOf(":") + 1);
                        result = Convert.ToInt32(sResult.Trim());
                        drv["AIRAC"] = Convert.ToInt32(result);
                    }
                    if (Line.IndexOf("Version") != -1)
                    {
                        sResult = Line.Substring(Line.IndexOf(":") + 1);
                        result = Convert.ToInt32(sResult.Trim());
                        drv["Version"] = Convert.ToInt32(result);
                    }
                    if (Line.IndexOf("Valid") != -1)
                    {
                        sResult = Line.Substring(Line.IndexOf(":") + 1).Trim();
                        sTemp = Line.Substring(0, Line.IndexOf("-") - 1).Trim();
                        drv["BeginDate"] = DateTime.ParseExact(sTemp,"dd/MMM/yyyy",CultureInfo.InvariantCulture);
                        sResult = Line.Substring(Line.IndexOf("-")).Trim();
                        drv["EndDate"] = DateTime.ParseExact(sResult, "dd/MMM/yyyy", CultureInfo.InvariantCulture);
                    }
                    if (Line.IndexOf("Files parsed") != -1)
                    {
                        sResult = Line.Substring(Line.IndexOf(":") + 1).Trim();
                        drv["ParsedDate"] = DateTime.ParseExact(sResult, "dd/mm/yyyy", CultureInfo.InvariantCulture);
                    }
                }
            }
            drv.EndEdit();
            dv.Dispose();
            return result;
        }
    }
}
