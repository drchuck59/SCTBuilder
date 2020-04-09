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
        public static void SIDSTARS(string Airport)
        {
            // The output strings will be
            // <SID/STAR>:<AIRPORT ICAO>:<RUNWAY>:<TRANSITIONxPROCEDURE>:<ROUTE>
            // There will be a line for every RWY with the Procedure and every RWY with Transition.Procedure
            // Therefore, read all the procedures for each runway and save transitions to reuse later
            string ICOA = Conversions.ICOA(Airport); string Transition;
            string SSDcode = string.Empty; string Vectors; string Procedure = string.Empty;
            string FullFilename = SCTcommon.GetFullPathname(FolderMgt.NGFolder, ICOA + ".txt");
            DataRowView newrow;
            List<string> Words = new List<string>();
            List<string> TransitionFixes = new List<string>();
            List<string> RNWS = new List<string>();
            List<string> Speed = new List<string>();
            List<string> AOA = new List<string>();
            List<string> AOB = new List<string>();
            List<string> At = new List<string>();
            List<string> TSpeed = new List<string>();
            List<string> TAOA = new List<string>();
            List<string> TAOB = new List<string>();
            List<string> TAt = new List<string>();
            string Section; string Line;
            if (FullFilename.IndexOf("ERROR") == -1)
            {
                using (StreamReader reader = new StreamReader(FullFilename))
                {
                    Line = reader.ReadLine();
                    Words.AddRange(ParseLine(Line));
                    if (Words.Count != 0)
                    {
                        // Use Words vs OldWords to detect a new line
                        switch (Words[0])
                        {
                            case "Title":
                                switch (Words[1])
                                {
                                    // Capture the section we are working with
                                    case "FIXES":
                                        Section = Words[1];
                                        break;
                                    case "SIDS":
                                        Section = Words[1];
                                        break;
                                    case "STARS":
                                        Section = Words[1];
                                        break;
                                    case "APPROACHES":
                                        Section = Words[1];
                                        break;
                                    // Capture the SSCcode we are working with
                                    case "SID":
                                        // This begins the read of one SID
                                        SSDcode = Words[1];
                                        // In some cases, there is only one line, concatenated
                                        if (Words.Count > 3)
                                        {
                                            for (int i = 1; i < Words.Count(); i++)
                                            {
                                                if (Words[i] == "RNW") RNWS.Add(Words[i + 1]);
                                            }
                                            DataView dvSID = new DataView(Form1.NGSID);
                                            foreach(string RWY in RNWS)
                                            {
                                                newrow = dvSID.AddNew();
                                                newrow["SSDcode"] = SSDcode;
                                                newrow["FacilityID"] = ICOA;
                                                newrow["Rwy"] = RWY;
                                                newrow.EndEdit();
                                            }
                                        }
                                        else
                                        {
                                            SSDcode = Words[3];
                                        }
                                        break;
                                    case "STAR":
                                        // This begins the read of one STAR
                                        if (Procedure.IndexOf('.') != -1)
                                        {
                                            Procedure = Procedure.Substring(0, Procedure.IndexOf('.') - 1);
                                            RNWS.Add( Procedure.Substring(Procedure.IndexOf('.') + 1));
                                        }
                                        // will need to loop the data for mult runways if no defining rwy
                                        int FixCount = SCTcommon.CountListOccurrences(Words, "FIX");
                                        for (int i = 1; i <= Words.Count(); i++)
                                        {
                                            if (Words[i] == "FIX")
                                            {
                                                AOA.Add(AoARestrict(Line, Words[i + 1]));
                                                AOB.Add(AoBRestrict(Line, Words[i + 1]));
                                                At.Add(AltAtRestrict(Line, Words[i + 1]));
                                                Speed.Add(SpeedRestrict(Line, Words[i + 1]));
                                            }
                                        }
                                        break;
                                    case "APPROACH":
                                        // This begins the read of one APPROACH
                                        SSDcode = Words[1];
                                        break;
                                    case "RNW":
                                        // This is a title - adds the expected RNWS only
                                        // (Don't confuse with RNW in Content)
                                        RNWS.Add(Words[1]);
                                        break;
                                    default:
                                        break;
                                }
                                break;
                            case "Content":
                                switch (Words[1])
                                {
                                    case "RNW":
                                        // Should have the SSDcode for the SID and ICOA from calling routine
                                        DataView dvNGSID = new DataView(Form1.NGSID);
                                        newrow = dvNGSID.AddNew();
                                        newrow["SSDcode"] = SSDcode;
                                        newrow["Facility"] = ICOA;
                                        newrow["RWY"] = Words[1];
                                        newrow["InitHdg"] = SIDInitHdg(Words.ToArray());
                                        newrow["InitAlt"] = SIDInitAlt(Words.ToArray());
                                        newrow["Vectors"] = SIDVectorString(Words.ToArray());
                                        newrow.EndEdit();
                                        dvNGSID.Dispose();
                                        break;
                                    case "TRANSITION":
                                        Transition = Words[1];
                                        TAOA.Clear(); TAOB.Clear(); TAt.Clear(); TSpeed.Clear(); 
                                        for (int i = 1; i < Words.Count(); i++)
                                        {
                                            if (Words[i] == "FIX")
                                            {
                                                TransitionFixes.Add(Words[i + 1]);
                                                TAOA.Add(AoARestrict(Line, Words[i + 1]));
                                                TAOB.Add(AoBRestrict(Line, Words[i + 1]));
                                                TAt.Add(AltAtRestrict(Line, Words[i + 1]));
                                                TSpeed.Add(SpeedRestrict(Line, Words[i + 1]));
                                            }
                                        }
                                        break;
                                }
                                break;
                            case "Addenda":
                                switch (Words[1])
                                {
                                    case "RNW":
                                        //Only occurs in STARS
                                        for (int i = 0; i < Words.Count; i++)
                                        {
                                            if (Words[i] == "RNW") RNWS.Add(Words[i + 1]);
                                        }
                                        // At this point, the STARS data should be ready to save
                                        DataView dvSTAR = new DataView(Form1.NGSTAR);
                                        DataView dvTransition = new DataView(Form1.NGSTARTransition);

                                        newrow = dvSTAR.AddNew();

                                        break;
                                    case "TRANSITION":
                                        // Only occurs in SIDs
                                        // Can load this group
                                        // Transitions are inbound and don't specify RWYs
                                        Transition = Words[1];
                                        Vectors = SIDVectorString(Words.ToArray());
                                        DataView dvNGSIDTransition = new DataView(Form1.NGSIDTransition);
                                        DataView dvNGSID = new DataView(Form1.NGSID)
                                        {
                                            // Only write the main SID once
                                            RowFilter = "[SSDCode] = '" + SSDcode + "'"
                                        };
                                        if (dvNGSID.Count == 0)
                                        {
                                            foreach (string Rwy in RNWS)
                                            {
                                                newrow = dvNGSID.AddNew();
                                                newrow["SSDcode"] = SSDcode;
                                                newrow["Transition"] = Transition;
                                                newrow["Rwy"] = Rwy;
                                                newrow["Vectors"] = Vectors;
                                                newrow.EndEdit();
                                            }
                                        }
                                        newrow = dvNGSIDTransition.AddNew();
                                        newrow["SSDCode"] = SSDcode;
                                        newrow["Transition"] = Transition;
                                        newrow["Vectors"] = Vectors;
                                        break;
                                }
                                break;
                        }
                    }
                }
            }
        }

        private static string SIDVectorString(string[] Words)
        {
            // Return a string of found FIXes
            string vectors = string.Empty;
            for (int w = 1; w <= Words.Length; w++)
            {
                if (Words[w] == "FIX")
                {
                    if (Words[w + 1] == "OVERFLY")
                        vectors += Words[w + 2] + " ";
                    else
                        vectors += Words[w + 1] + " ";
                }
            }
            return vectors.Trim();
        }

        private static string SIDInitHdg(string[] Words)
        {
            string result = string.Empty;
            for (int i = 1; i < Words.Length; i++)
            {
                if (Words[i] == "HDG") result = Words[i + 1];
            }
            return result;
        }

        private static string SIDInitAlt(string[] Words)
        {
            string result = string.Empty;
            for (int i = 1; i < Words.Length; i++)
            {
                if (Words[i] == "UNTIL") result = Words[i + 1];
            }
            return result;
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
