using System;
using System.IO;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Diagnostics;
using System.Windows.Forms;
using Google.Protobuf.WellKnownTypes;

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

        public static void NavAID()
        {
            string FullFilename = SCTcommon.GetFullPathname(FolderMgt.DataFolder, "wpNavAID.txt");
            DataTable NGNavAid = Form1.NGNavAID;
            DataView dvNGNavAid = new DataView(NGNavAid);
            DataRowView dataRow;
            if (FullFilename.IndexOf("Error") == -1)
            {
                NGNavAid.Clear();
                using (StreamReader sr = new StreamReader(FullFilename))
                {
                    while ((Line = sr.ReadLine()) != null)
                    {
                        if (Line.Substring(0, 1) != ";")
                        {
                            dataRow = dvNGNavAid.AddNew();
                            dataRow["Name"] = Line.Substring(0, 14).Trim();
                            dataRow["FacilityID"] = Line.Substring(24, 5).Trim();
                            dataRow["Type"] = Line.Substring(29, 4).Trim();
                            dataRow["Latitude"] = Convert.ToDouble(Line.Substring(33, 10).Trim());
                            dataRow["Longitude"] = Convert.ToDouble(Line.Substring(43, 11).Trim());
                            dataRow["Frequency"] = Line.Substring(54, 6).Trim();
                            dataRow.EndEdit();
                        }
                    }
                }
                Debug.Print(NGNavAid.Rows.Count + " rows added to NGNavAid table.");
            }
            dvNGNavAid.Dispose();
        }

        public static void NavFIX()
        {
            string FullFilename = SCTcommon.GetFullPathname(FolderMgt.DataFolder, "wpNavFIX.txt");
            DataTable dtNGFIX = Form1.NGFixes;
            DataView dvNGFIX = new DataView(dtNGFIX);
            DataRowView datarow;
            if (FullFilename.IndexOf("Error") == -1)
            {
                using (StreamReader sr = new StreamReader(FullFilename))
                {
                    while ((Line = sr.ReadLine()) != null)
                    {
                        if (Line.Substring(0, 1) != ";")
                        {
                            datarow = dvNGFIX.AddNew();
                            datarow["FacilityID"] = Line.Substring(0, 5).Trim();
                            datarow[1] = Convert.ToDouble(Line.Substring(29, 10).Trim());
                            datarow[2] = Convert.ToDouble(Line.Substring(39, 11).Trim());
                            datarow.EndEdit();
                        }
                    }
                    Debug.WriteLine(dtNGFIX.Rows.Count + " rows added to NTFixes");
                }
            }
        }

        public static void NavRTE()
        {
            // Add NaviGraph routes that are NOT in FAA table
            // That is - Ultra, Oceanic, and World routes
            string curID; string prevID = string.Empty;
            bool IDexists = false; bool Adding;
            string FullFilename = SCTcommon.GetFullPathname(FolderMgt.DataFolder, "wpNavRTE.txt");
            DataView dvRTE = new DataView(Form1.NGRTE);
            if (FullFilename.IndexOf("Error") != -1)
            {
                using (StreamReader sr = new StreamReader(FullFilename))
                {
                    while ((Line=sr.ReadLine()) != null)    // Until EOF
                    {
                        if (Line.Substring(0, 1) != ";")     // Check for comment
                        {
                            curID = Line.Substring(0, Line.IndexOf(" ")).Trim();
                            Adding = curID == prevID;
                            if (!Adding)
                            {
                                dvRTE.RowFilter = "[AWYID] = '" + curID + "'";
                                IDexists = (dvRTE.Count != 0);
                            }
                            if (Adding || !IDexists)
                            {
                                AddRTE(dvRTE);
                            }
                            prevID = curID;
                        }
                    }
                }
            }
        }

        private static void AddRTE(DataView dv)
        {
            string curID = Line.Substring(0, Line.IndexOf(" ")).Trim();
            string[] LowAirways = new string[6] {"V", "A", "B", "G", "R", "T"};
            string temp = Line;
            DataRowView dvRTE = dv.AddNew();
            dvRTE["AWYID"] = curID;
            temp = temp.Substring(temp.IndexOf(" ") + 1);
            dvRTE["Sequence"] = Convert.ToInt32(temp.Substring(0, temp.IndexOf(" ")).Trim()) * 10;
            temp = temp.Substring(temp.IndexOf(" ") + 1);
            dvRTE["NavAid"] = temp.Substring(0, temp.IndexOf(" ")).Trim();
            temp = temp.Substring(Line.IndexOf(" ") + 1);
            dvRTE["Latitude"] = Convert.ToDouble(temp.Substring(0, temp.IndexOf(" ")).Trim());
            Line = Line.Substring(temp.IndexOf(" ") + 1);
            dvRTE["Longitude"] = Convert.ToDouble(temp.Substring(0, temp.IndexOf(" ")).Trim());
            dvRTE["IsLow"] = LowAirways.Any(curID.Contains);
            dvRTE.EndEdit();
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
                                        if (SCTcommon.CountListOccurrences(Words, "RNW") != 0)
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
                result = true;
            }
            return result;
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
            quadrant = temp.Substring(0, 1);
            temp = temp.Substring(2).Trim();
            int LocLon = temp.IndexOf(" ");
            float decLon = Convert.ToSingle(temp.Substring(0, LocLon));
            temp = temp.Substring(LocLon).Trim();
            decLon += Convert.ToSingle(temp) / 60;
            DataView dvNGFix = new DataView(Form1.NGFixes);
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
            DataView dvNGSID = new DataView(Form1.NGSID);
            DataRowView newFIX;
            string runway = string.Empty; int Sequence;
            string fix = string.Empty; string oldfix = string.Empty;
            //Build a list of RNWs then a list of FIXes
            for (int r = 3; r <= Words.Count; r++)          //  Start at 3d word
            {
                if (Words[r] == "RNW") runway = Words[r + 1];
                {
                    // Loop the RNWs and add the FIXes
                    Sequence = 0;
                    for (int w = 1; w <= Words.Count; w++)
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
                dvNGSID.Dispose();
            }
        }


        private static void AddSID()
        {
            // These are multiline SIDs with one or more RNWs and a series of instructions
            // Can load each RNW as part of the SID
            DataView dvNGSID = new DataView(Form1.NGSID);
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
            DataView dvNGSIDTransition = new DataView(Form1.NGSIDTransition);
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
            DataView dvNGStar = new DataView(Form1.NGSTAR);
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
            return SSDcode;
        }

        private static void STAR_RNWS()
        {
            // Add the RNWs applicable to the SSDcode+SSDmode for the Airport
            DataTable dtNGSTAR = Form1.NGSTAR;
            for (int r = 1; r < Words.Count; r++)
            {
                if (Words[r] == "RNW")
                {
                    dtNGSTAR.Rows.Add(Airport, SSDcode, SSDmod, Words[r + 1]);
                }
            }
        }

        private static void STARTransition()
        {
            // Transitions apply to each SSDcode+SSDmod (if present, usually is)
            string TransitionName = Words[2];
            string fix; DataRowView newFIX; int Sequence = 0;
            DataView dvNGSTARTransition = new DataView(Form1.NGSTARTransition);
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
            return result;
        }

        private static string AltAtRestrict(string FIX)
        {
            // Returns an At or Above restriction, if it exists
            string result = string.Empty; int LocNextFix;
            int LocFix = Line.IndexOf(FIX.Trim());
            if (LocFix != -1)
            {
                int LocAOA = Line.IndexOf("AT", LocFix);
                if (LocAOA != -1)
                {
                    LocNextFix = Line.IndexOf("FIX", LocFix);
                    if (LocNextFix == -1)
                    {
                        result = Line.Substring(LocAOA + 2, 7).Trim();
                    }
                    else if (LocNextFix > LocAOA)
                    {
                        result = Line.Substring(LocAOA + 2, Line.IndexOf(" ", LocAOA + 2)).Trim();
                    }
                    if (!result.All(char.IsDigit)) result = string.Empty;
                }
            }
            return result;
        }

        private static string AoARestrict (string FIX)
        {
            // Returns an At or Above restriction, if it exists
            string result = string.Empty; int LocNextFix;
            int LocFix = Line.IndexOf(FIX.Trim());
            if (LocFix != -1)
            {
                int LocAOA = Line.IndexOf("AT OR ABOVE", LocFix);
                if (LocAOA != -1)
                {
                    LocNextFix = Line.IndexOf("FIX", LocFix);
                    if (LocNextFix == -1)
                    {
                        result = Line.Substring(LocAOA + 11, 7).Trim();
                    }
                    else if (LocNextFix > LocAOA)
                    {
                        result = Line.Substring(LocAOA + 11, Line.IndexOf(" ", LocAOA + 11)).Trim();
                    }
                }
            }
            return result;
        }

        private static string AoBRestrict(string FIX)
        {
            // Returns an At or Above restriction, if it exists
            string result = string.Empty; int LocNextFix;
            int LocFix = Line.IndexOf(FIX.Trim());
            if (LocFix != -1)
            {
                int LocAOB = Line.IndexOf("AT OR BELOW", LocFix);
                if (LocAOB != -1)
                {
                    LocNextFix = Line.IndexOf("FIX", LocFix);
                    if (LocNextFix == -1)
                    {
                        result = Line.Substring(LocAOB + 11, 7).Trim();
                    }
                    else if (LocNextFix > LocAOB)
                    {
                        result = Line.Substring(LocAOB + 11, Line.IndexOf(" ", LocAOB + 11)).Trim();
                    }
                }
            }
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
            // First word indicates level: Title, Content, Addenda
            // Blank and commens have already been skipped
            List<string> Words = new List<string>();
            string temp = Line;
            int Loc1;
            // Single Word
                while (temp.Length != 0)
                {
                    Loc1 = temp.IndexOf(" ");
                    if (Loc1 != -1)
                    {
                        Words.Add(temp.Substring(0, Loc1).Trim());
                        temp = temp.Substring(Loc1).Trim();
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
            int result = -1; string Line; string sResult;
            string FullFilename = SCTcommon.GetFullPathname(FolderMgt.DataFolder, "cycle_info.txt");
            if (FullFilename.IndexOf("ERROR") != -1) return result;
            using (StreamReader reader = new StreamReader(FullFilename))
            {
                Line = reader.ReadLine();
                if (result == -1)
                {
                    if (Line.IndexOf("AIRAC") == -1)
                    {
                        sResult = Line.Substring(Line.IndexOf(":") + 1);
                        result = Convert.ToInt32(sResult.Trim());
                    }
                }
            }
            return result;
        }
    }
}
