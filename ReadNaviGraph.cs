using System;
using System.IO;
using System.Globalization;
using System.Windows.Forms;
using System.Linq;
using System.Collections.Generic;
using System.Xml;
using System.Data;

namespace SCTBuilder
{
    class ReadNaviGraph
    {
        // For the given STAR (doesn't apply to SIDs)
        // Assumes calling program tests for Navigraph files (SIDSTAR folder)
        // 1. Does a file exist for the airport?
        // 2. Does a STAR match the STAR from FAA?
        // 3. Read the Data (from any runway as we only want the AOA, AOB and SPDs.)
        // 4. Apdate the data to the SSD table
        public static bool NGFileExists(string NGSIDSTARS)
        {
            string FullFilename = SCTcommon.GetFullPathname(FolderMgt.DataFolder, NGSIDSTARS);
            return !(FullFilename == "NotFound");
        }

        public static string[] NGFixLimits(string NGSIDSTARS, string STARcode, string FIX)
        {
            // 0 - AOA, 1 - AOB, 2 - Speed
            string Line; int FixStart; int NextFix;  
            int AOAstart; int AOBstart; int SPEEDstart;
            string AOA = "AT OR ABOVE"; string AOB = "AT OR BELOW"; string SPEED = "SPEED";
            string AOAft = string.Empty; string AOBft = string.Empty; string SpeedKTs = string.Empty;
            string FullFilename = SCTcommon.GetFullPathname(FolderMgt.DataFolder, NGSIDSTARS);
            using (StreamReader reader = new StreamReader(FullFilename))
            {
                while ((Line = reader.ReadLine()) != null)
                {
                    if (Line.IndexOf(STARcode) != -1)
                    {
                        FixStart = Line.IndexOf(FIX);
                        NextFix = Line.IndexOf(FIX, FixStart + 1);
                        if (FixStart != -1)
                        {
                            AOAstart = Line.IndexOf(AOA, FixStart);
                            if ((AOAstart < NextFix) & (AOAstart > -1))
                                AOAft = Line.Substring(AOAstart + 11, 5).Trim();

                            AOBstart = Line.IndexOf(AOB, FixStart);
                            if ((AOBstart < NextFix) & (AOBstart > -1))
                                AOBft = Line.Substring(AOBstart + 11, 5).Trim();

                            SPEEDstart = Line.IndexOf(SPEED, FixStart);
                            if ((SPEEDstart < NextFix) & (SPEEDstart > -1))
                                SpeedKTs = Line.Substring(SPEEDstart + 11, 5).Trim();
                        }
                    }
                }
            }
            string[] array = { AOAft, AOBft, SpeedKTs };
            return array;
        }
    }
}
