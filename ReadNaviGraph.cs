using System;
using System.IO;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SCTBuilder
{
    class ReadNaviGraph
    {
        static string Msg;
        static string Line = string.Empty;
        public static void SIDSTARS(string Airport)
        {
            // The output strings will be
            // <SID/STAR>:<AIRPORT ICAO>:<RUNWAY>:<TRANSITIONxPROCEDURE>:<ROUTE>
            // There will be a line for every RWY with the Procedure and every RWY with Transition.Procedure
            // Therefore, read all the procedures for each runway and save transitions to reuse later

            string Section = string.Empty;
            List<string> RNWS = new List<string>();
            string SSDcode = string.Empty;
            string FullFilename = SCTcommon.GetFullPathname(FolderMgt.NGFolder, Airport + ".txt");
            List<string> Words = new List<string>();
            if (FullFilename.IndexOf("ERROR") == -1)
            {
                using (StreamReader reader = new StreamReader(FullFilename))
                {
                    Line = reader.ReadLine();
                    Words.AddRange(ParseLine(Line));
                    if (Words.Count != 0)
                    {
                        switch (Words[0])       // Which level is the line
                        {
                            case "Title":
                                switch (Words[1])           // First word starts in column 0
                                {
                                    // Capture the section we are working with
                                    case "FIXES":           // Indicates start of the FIXes data
                                        Section = Words[1];
                                        break;
                                    case "FIX":             // FiX data - don't need anything except the name but capture all
                                        if (Section == "FIXES")
                                            AddFixString(Line);
                                        else
                                        {
                                            // This should never occur
                                            Msg = "Error reading Title FIX";
                                            SCTcommon.SendMessage(Msg);
                                        }
                                        break;
                                    case "ENDFIXES":        // Indicates end of the FIXES data
                                        Section = string.Empty;
                                        break;
                                    case "RNWS":            // Indicates start of the RNW data
                                        Section = Words[1];
                                        break;
                                    case "RNW":             // As a title only identifies each RNW in the data
                                        if (Section == "RNWS")
                                            RNWS.Add(Words[1]);
                                        else
                                        {
                                            // This should never occur
                                            Msg = "Error reading Title RNW";
                                            SCTcommon.SendMessage(Msg);
                                        }
                                        break;
                                    case "ENDRNWS":         // Indicates end of the RNW data
                                        Section = string.Empty;
                                        break;
                                    case "SIDS":            // Indicates start of the SIDs data
                                        Section = Words[1];
                                        break;
                                    case "SID":
                                        // This begins the read of one SID
                                        if (SCTcommon.CountListOccurrences(Words, "RNW") == 0)
                                            SSDcode = Words[3];
                                        else
                                            AddSID(Words, Airport, Words[3]);
                                        break;
                                    case "STARS":            // Indicates start of the STARs data
                                        Section = Words[1];
                                        break;
                                    case "STAR":
                                        // This begins the read of one STAR
                                        SSDcode = AddSTAR(Words, Line, Airport);
                                        break;
                                    case "APPROACHES":      // Indicates start of the APPROACHes data
                                        Section = Words[1];
                                        break;
                                    case "APPROACH":
                                        // This begins the read of one APPROACH
                                        SSDcode = Words[1];
                                        break;

                                    case "//":                  // Comment line
                                    default:
                                        break;
                                }
                                break;
                            case "Content":
                                switch (Words[1])
                                {
                                    case "RNW":
                                        // Should have the SSDcode for the SID and ICOA from calling routine
                                        AddSID(Words, Airport, SSDcode);
                                        break;
                                    case "TRANSITION":
                                        // This Transition is always a STAR transition
                                        // These transitions can be split across airports or runways
                                        STARTransition(Words, Line, Airport, SSDcode, RNWS);
                                        break;
                                }
                                break;
                            case "Addenda":
                                switch (Words[1])
                                {
                                    case "RNW":
                                        //Only occurs in STARS
                                        CopySTAR(Words, SSDcode, Airport);
                                        break;
                                    case "TRANSITION":
                                        // Only occurs in SIDs
                                        // Can load this group
                                        // Transitions are inbound and don't specify RWYs
                                        SIDTransition(Words, SSDcode);
                                        break;
                                }
                                break;
                        }
                    }
                    Line = string.Empty;
                    Words.Clear();
                }
            }
        }

        private static void AddFixString(string Line)
        {
            int LocSpace1 = Line.IndexOf(" ");
            int LocSpace2 = Line.IndexOf(" ", LocSpace1 + 1);
            int LocLat = Line.IndexOf(" N ");
            if (LocLat == -1) Line.IndexOf(" S ");
            int LocLon = Line.IndexOf("W");
            if (LocLon == -1) LocLon = Line.IndexOf("E");
            int LocFix = Line.IndexOf(" " + 1);

            string FixName = Line.Substring(LocFix, LocSpace2 - LocSpace1).Trim();
            string Latitude = Line.Substring(LocLat, LocLon - LocLat).Trim();
            string Longitude = Line.Substring(LocLon, Line.Length - LocLon);
            Console.WriteLine("Fix: " + FixName + ", Lat: " + Latitude + ", Lon: " + Longitude);

            DataView dvNGFix = new DataView(Form1.NGFixes);
            DataRowView newrow = dvNGFix.AddNew();
            newrow["FixName"] = FixName;
            newrow["Latitude"] = Conversions.String2DecDeg(Latitude, Delim: " ");
            newrow["Longitude"] = Conversions.String2DecDeg(Longitude, Delim: " ");
            newrow.EndEdit();
            dvNGFix.Dispose();
        }

        private static void AddSID(List<string> Words, string Airport, string SSDcode)
        {
            DataView dvNGSID = new DataView(Form1.NGSID);
            DataRowView newFIX;
            string runway = string.Empty; int Sequence ;
            string fix = string.Empty; string oldfix = string.Empty;
            //Build a list of RNWs then a list of FIXes
            for (int r = 1; r <= Words.Count; r++)
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
                                newFIX["AltAOA"] = AoARestrict(Line, fix);
                                newFIX["AltAOB"] = AoBRestrict(Line, fix);
                                newFIX["AltAt"] = AltAtRestrict(Line, fix);
                                newFIX["Speed"] = SpeedRestrict(Line, fix);
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

        private static void SIDTransition(List<string>Words, string SSDcode)
        {
            // SID Transition FIXes do not have restrictions
            DataView dvNGSIDTransition = new DataView(Form1.NGSIDTransition);
            DataRowView newrow; int Sequence = 0;
            string Transition = Words[2];
            for (int t = 1; t <= Words.Count; t++)
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

        private static string AddSTAR(List<string>Words, string Line, string Airport)
        {
            // IF the STAR name includes the runway, it's a single runway STAR and can be added.
            // If it does not, it applies to many runways. Load 1 and copy it later.
            DataView dvNGStar = new DataView(Form1.NGSTAR);
            DataRowView newFIX; int Sequence = 0; string fix; string runway;
            string SSDcode = string.Empty;
            if (Words[2].IndexOf(".") != -1)
                runway = Words[2].Substring(Words[2].IndexOf(".") + 1, 2);
            else runway = string.Empty;
            // If this is a multirunway fix, load it for copying later
            for (int f = 1; f <= Words.Count; f++)
            {
                SSDcode = Words[2];
                if (Words[f] == "FIX")
                {
                    Sequence++;
                    fix = Words[f + 1];
                    newFIX = dvNGStar.AddNew();
                    newFIX["FacilityID"] = Airport;
                    newFIX["SSDcode"] = SSDcode;
                    newFIX["RWY"] = runway;
                    newFIX["Fix"] = fix;
                    newFIX["AltAOA"] = AoARestrict(Line, fix);
                    newFIX["AltAOB"] = AoBRestrict(Line, fix);
                    newFIX["AltAt"] = AltAtRestrict(Line, fix);
                    newFIX["Speed"] = SpeedRestrict(Line, fix);
                    newFIX["Sequence"] = Sequence;
                    newFIX.EndEdit();
                }
            }
            return SSDcode;
        }

        private static void CopySTAR(List<string>Words, string SSDcode, string Airport)
        {
            // Use this to duplicate STARs for multiple runways
            DataRow foundRow = null;
            DataTable dtNGSTAR = Form1.NGSTAR;
            int NumRunways = SCTcommon.CountListOccurrences(Words, "RNW");
            if (NumRunways != 1)
            {
                // If there was only 1 runway, it was added in the STAR process
                DataView dvNGSTAR = new DataView(Form1.NGSTAR)
                {
                    RowFilter = "([FacilityID] = '" + Airport + "') & " +
                                "[SSDcode] = '" + SSDcode + "') & " +
                                ("[RWY] = ''")
                };
                if ((dvNGSTAR.Count == 0) || (dvNGSTAR.Count > 1))
                    SCTcommon.SendMessage("Row count error finding STAR " + SSDcode + " at " + Airport);
                else
                    foundRow = dvNGSTAR[0].Row;
                //  Copy the existing elements
                string Fix = foundRow["Fix"].ToString();
                string AltAOA = foundRow["AltAOA"].ToString();
                string AltAOB = foundRow["AltAOB"].ToString();
                string AltAt = foundRow["AltAt"].ToString();
                string Speed = foundRow["Speed"].ToString();
                int Sequence = Convert.ToInt32(foundRow["Sequence"]);
                // Insert the first runway
                dvNGSTAR[0]["RWY"] = Words[2]; 
                dvNGSTAR.Dispose();
                // Start past the first Rnw
                for (int r = 3; r <= Words.Count; r++)
                {
                    if (Words[r] == "RNW")
                    {
                        Sequence++;
                        dtNGSTAR.Rows.Add(Airport, SSDcode, Words[r + 1], Fix, AltAOA, AltAOB, AltAt, Speed, Sequence);
                    }
                }
            }
        }

        private static void STARTransition(List<string> Words, string Line, string Airport, string SSDcode, List<string>RNWS)
        { 
            // If the SSDcode has the runway in the name, this is a single runway transition
            // If it does not, then the transition applies to all the runways in the STAR
        }

        private static string SpeedRestrict(string Line, string FIX)
        {
            string result = string.Empty;  int LocNextFix;
            int LocFix = Line.IndexOf(FIX);
            int LocSpeed = Line.IndexOf("SPEED", LocFix);
            if (LocSpeed != -1)
            {
                LocNextFix = Line.IndexOf("FIX", LocFix);
                if ( (LocNextFix > LocSpeed) || (LocNextFix == -1) )
                {
                    result = Line.Substring(LocSpeed + 6, 3);
                }
            }
            return result;
        }

        private static string AltAtRestrict(string Line, string FIX)
        {
            // Returns an At or Above restriction, if it exists
            string result = string.Empty;
            int LocFix = Line.IndexOf(FIX);
            result = Line.Substring(LocFix + FIX.Length, 6).Trim();
            if (Line.All(char.IsDigit))
                return result;
            else
                return string.Empty;
        }

        private static string AoARestrict (string Line, string FIX)
        {
            // Returns an At or Above restriction, if it exists
            string result = string.Empty; int LocNextFix;
            int LocFix = Line.IndexOf(FIX);
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
            return result;
        }

        private static string AoBRestrict(string Line, string FIX)
        {
            // Returns an At or Above restriction, if it exists
            string result = string.Empty; int LocNextFix;
            int LocFix = Line.IndexOf(FIX);
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
            return result;
        }

        private static string[] ParseLine(string Line)
        {
            // Parse the line into individual words
            // First word indicates level: Title, Content, Addenda
            List<string> Words = new List<string>();
            if ( (Line.Length != 0) && (Line.Substring(0, 2) != "//") )    // Blank line or comment
            {
                if (Line.Substring(0, 1) == " ") Words.Add("Content");
                else if (Line.Substring(0, 1) == "  ") Words.Add("Addenda");
                else Words.Add("Title");

                Line = Line.Trim();                 // Remove the whitespace
                // Single Word
                int Loc1 = Line.IndexOf(" ");
                if (Loc1 == -1)
                {
                    Words.Add(Line);
                }
                else
                // Multiple Words
                {
                    while (Line.Length != 0)
                    {
                        Loc1 = Line.IndexOf(" ");
                        if (Loc1 != 0)
                        {
                            Words.Add(Line.Substring(0, Loc1).Trim());
                            Line = Line.Substring(Loc1).Trim();
                        }
                        else
                        {
                            Words.Add(Line.Trim());
                            Line = string.Empty;
                        }
                    }
                }
            }
            return Words.ToArray();
        }

        public static int AIRAC()
        {
            // Find the cycleinfo.txt file.  -1 if doesnt exist (bad folder) or return AIRAC
            int result = -1; string Line; string sResult;
            string FullFilename = SCTcommon.GetFullPathname(FolderMgt.NGFolder, "cycleinfo.txt");
            if (FullFilename.IndexOf("ERROR") == -1) return result;
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
