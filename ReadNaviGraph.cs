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
            string ICOA = Conversions.ICOA(Airport); string RWY = string.Empty; string Transition = string.Empty;
            string SSDcode = string.Empty; string Vectors = string.Empty; string SpeedLimit = string.Empty;
            string AoA = string.Empty; string AoB = string.Empty; string AtAlt = string.Empty;
            string FullFilename = SCTcommon.GetFullPathname(FolderMgt.NGFolder, ICOA + ".txt");
            List<string> Words = new List<string>();
            List<string> OldWords = new List<string>();
            List<string> RNWS = new List<string>();
            string Section = string.Empty; string Line;
            DataView dvFIX = new DataView(Form1.FIX);
            DataView dvNGSID = new DataView(Form1.NGSID); DataView dvNGSIDTrans = new DataView(Form1.NGSIDTransition);
            if (FullFilename.IndexOf("ERROR") == -1)
            {
                using (StreamReader reader = new StreamReader(FullFilename))
                {
                    Line = reader.ReadLine();
                    Words.AddRange(ParseLine(Line, Section));
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
                                            Vectors = SSDVectorString(Words.ToArray());
                                            SpeedLimit = Speed(Line, SSDcode);
                                            AtAlt = AltAtRestrict(Line, SSDcode);
                                            AoA = AoARestrict(Line, SSDcode);
                                            AoB = AoBRestrict(Line, SSDcode);
                                            // This should be ready to save ************************
                                        }
                                        else
                                        {
                                            SSDcode = Words[3];
                                        }
                                        break;
                                    case "STAR":
                                        // This begins the read of one STAR
                                        LoadStar(Words, RNWS);
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
                                        // Should have the SSDcode for the SID
                                        RWY = Words[1];
                                        Vectors = SSDVectorString(Words.ToArray());
                                        DataRowView newrow = dvNGSID.AddNew();
                                        newrow["SSDcode"] = SSDcode;
                                        newrow["Facility"] = ICOA;
                                        newrow["RWY"] = RWY;
                                        newrow["Vectors"] = Vectors;
                                        newrow.EndEdit();
                                        break;
                                    case "TRANSITION":
                                        SSDcode = Words[1];
                                        break;
                                }
                                break;
                            case "Addenda":
                                switch (Words[1])
                                {
                                    case "RNW":
                                        

                                        break;
                                    case "TRANSITION":
                                        // Only SIDs have this word in column 2 (zero based)
                                        // So it's coming from a SID
                                        Transition = Words[1];
                                        Vectors = SSDVectorString(Words.ToArray());
                                        DataRowView newrow = dvNGSIDTrans.AddNew();
                                        newrow["SSDcode"] = SSDcode;
                                        newrow["Transition"] = Transition;
                                        newrow["Vectors"] = Vectors;
                                        newrow.EndEdit();
                                        break;
                                }
                                break;
                        }
                    }
                }
            }
        }

        private static void LoadStar (List<string>Words, List<string>RNWS)
        {
            // STARS are always multiline and may contain a rwy
            // But they are always appended so use that
            string RWY = string.Empty;
            string Procedure = Words[2];
            if (Procedure.IndexOf('.') != -1)
            {
                Procedure = Procedure.Substring(0, Procedure.IndexOf('.') - 1);
                RWY = Procedure.Substring(Procedure.IndexOf('.') + 1, 2);
            }
            // will need to loop the data for mult runways if no defining rwy
            int FixCount = SCTcommon.CountListOccurrences(Words, "FIX");

        }

        private static string SSDVectorString(string[] Words)
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

        private static string Speed(string Line, string FIX)
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

        private static string[] ParseLine(string Line, string Section)
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
