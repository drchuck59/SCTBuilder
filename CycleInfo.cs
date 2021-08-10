using System;
using System.Xml;
using System.IO;
using System.Globalization;
using System.Diagnostics;

namespace SCTBuilder
{
    class CycleInfo
    {
        private readonly static DateTime Start1501 = Convert.ToDateTime("01/08/2015", CultureInfo.InvariantCulture);
        private readonly static string cr = Environment.NewLine;
        public static int AIRAC = 1501;
        public static DateTime CycleStart;
        public static DateTime CycleEnd;
        private readonly static int curAIRAC = CycleInfo.AIRACfromDate(DateTime.Today);

        public static int CurrentAIRAC
        {
            get
            {
                return curAIRAC;
            }
        }

        public static void WriteINIxml()
        {
            XmlWriter xml = XmlWriter.Create(FolderMgt.INIFilePath);
            xml.WriteStartDocument();
            xml.WriteStartElement("SCT_Builder");
            WriteXmlElement(xml, "Version", VersionInfo.Title.ToString());
            WriteXmlElement(xml, "LastAIRAC", CycleInfo.AIRAC.ToString());
            WriteXmlElement(xml, "DataFolder", FolderMgt.DataFolder.ToString());
            WriteXmlElement(xml, "OutputFolder", FolderMgt.OutputFolder.ToString());
            WriteXmlElement(xml, "SponsorARTCC", InfoSection.SponsorARTCC.ToString());
            WriteXmlElement(xml, "DefaultAirport", InfoSection.DefaultAirport.ToString());
            WriteXmlElement(xml, "FacilityEngineer", InfoSection.FacilityEngineer.ToString());
            WriteXmlElement(xml, "AsstFacilityEngineer", InfoSection.AsstFacilityEngineer.ToString());
            WriteXmlElement(xml, "ChkConfirmOverwrite", SCTchecked.ChkConfirmOverwrite.ToString());
            WriteXmlElement(xml, "ChkAll", SCTchecked.ChkOneVRCFile.ToString());
            WriteXmlElement(xml, "ChkAll", SCTchecked.ChkOneESFile.ToString());
            WriteXmlElement(xml, "ChkAPT", SCTchecked.ChkAPT.ToString());
            WriteXmlElement(xml, "ChkLimitAPT2ARTCC", SCTchecked.LimitAPT2ARTC.ToString());
            WriteXmlElement(xml, "ChkARB", SCTchecked.ChkARB.ToString());
            WriteXmlElement(xml, "ChkAWY", SCTchecked.ChkAWY.ToString());
            WriteXmlElement(xml, "ChkFIX", SCTchecked.ChkFIX.ToString());
            WriteXmlElement(xml, "ChkNDB", SCTchecked.ChkNDB.ToString());
            WriteXmlElement(xml, "ChkRWY", SCTchecked.ChkRWY.ToString());
            WriteXmlElement(xml, "ChkSID", SCTchecked.ChkSID.ToString());
            WriteXmlElement(xml, "ChkSTAR", SCTchecked.ChkSTAR.ToString());
            WriteXmlElement(xml, "ChkSSDname", SCTchecked.ChkSSDname.ToString());
            WriteXmlElement(xml, "ChkSUA", SCTchecked.ChkSUA.ToString());
            WriteXmlElement(xml, "ChkSUA_ClassB", SCTchecked.ChkSUA_ClassB.ToString());
            WriteXmlElement(xml, "ChkSUA_ClassC", SCTchecked.ChkSUA_ClassC.ToString());
            WriteXmlElement(xml, "ChkSUA_ClassD", SCTchecked.ChkSUA_ClassD.ToString());
            WriteXmlElement(xml, "ChkSUA_Danger", SCTchecked.ChkSUA_Danger.ToString());
            WriteXmlElement(xml, "ChkSUA_Prohibited", SCTchecked.ChkSUA_Prohibited.ToString());
            WriteXmlElement(xml, "ChkSUA_Restricted", SCTchecked.ChkSUA_Restricted.ToString());
            WriteXmlElement(xml, "ChkVOR", SCTchecked.ChkVOR.ToString());
            WriteXmlElement(xml, "ChkES_SCTfile", SCTchecked.ChkES_SCTfile.ToString());
            WriteXmlElement(xml, "ChkES_SSDfile", SCTchecked.ChkES_SSDfile.ToString());
            WriteXmlElement(xml, "ChkOceanic", SCTchecked.ChkOceanic.ToString());
            WriteXmlElement(xml, "UseFixesAsCoords", InfoSection.UseFixesAsCoords.ToString());
            WriteXmlElement(xml, "UseNaviGraphData", InfoSection.UseNaviGraph.ToString());
            WriteXmlElement(xml, "OneFilePerSidStar", InfoSection.OneFilePerSidStar.ToString());
            WriteXmlElement(xml, "DrawFixSymbolsOnDiagrams", InfoSection.DrawFixSymbolsOnDiagrams.ToString());
            WriteXmlElement(xml, "DrawFixLabelsOnDiagrams", InfoSection.DrawFixLabelsOnDiagrams.ToString());
            WriteXmlElement(xml, "DrawAltRestrictsOnDiagrams", InfoSection.DrawAltRestrictsOnDiagrams.ToString());
            WriteXmlElement(xml, "DrawSpeedRestrictsOnDiagrams", InfoSection.DrawSpeedRestrictsOnDiagrams.ToString());
            WriteXmlElement(xml, "IncludeSidStarReferences", InfoSection.IncludeSidStarReferences.ToString());
            WriteXmlElement(xml, "NorthLimit", InfoSection.NorthLimit.ToString());
            WriteXmlElement(xml, "SouthLimit", InfoSection.SouthLimit.ToString());
            WriteXmlElement(xml, "WestLimit", InfoSection.WestLimit.ToString());
            WriteXmlElement(xml, "EastLimit", InfoSection.EastLimit.ToString());
            WriteXmlElement(xml, "NorthOffset", InfoSection.NorthOffset.ToString());
            WriteXmlElement(xml, "WestOffset", InfoSection.WestOffset.ToString());
            WriteXmlElement(xml, "SouthOffset", InfoSection.SouthOffset.ToString());
            WriteXmlElement(xml, "EastOffset", InfoSection.EastOffset.ToString());
            WriteXmlElement(xml, "CenterLatitude_Dec", InfoSection.CenterLatitude_Dec.ToString());
            WriteXmlElement(xml, "CenterLongitude_Dec", InfoSection.CenterLongitude_Dec.ToString());
            WriteXmlElement(xml, "RollOverLon", InfoSection.RollOverLongitude.ToString());
            WriteXmlElement(xml, "SIDprefix", InfoSection.SIDprefix.ToString());
            WriteXmlElement(xml, "STARprefix", InfoSection.STARprefix.ToString());

            xml.WriteEndDocument();
            xml.Close();
        }
        private static void WriteXmlElement(XmlWriter xml, string Element, string Value)
        {
            xml.WriteStartElement(Element);
            xml.WriteString(Value);
            xml.WriteEndElement();
        }

        public static int ReadINIxml()
        {
            // reads the INI file saved on a previous use of the program
            // returns -1 if there is no XML file to read, else returns the AIRAC of the last run
            // Of course, that information is also placed into the CycleInfo class
            int result = -1;    // -1 equals XML file doesn't exist
            string temp;
            if (File.Exists(FolderMgt.INIFilePath))
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(FolderMgt.INIFilePath);
                foreach (XmlNode node in doc.DocumentElement)
                {
                    if (node.Name != "Version")
                    {
                        string value = node.InnerText;
                        switch (node.Name)
                        {
                            case "LastAIRAC":
                                result = Convert.ToInt32(value);
                                if ((result.ToString().Length == 0) || (result == 1501))
                                    result = 0;     // This file is corrupted.
                                break;
                            case "DataFolder":
                                temp = value;
                                if ((temp.Length != 0) && (Directory.Exists(value)))
                                    FolderMgt.DataFolder = value;
                                else result = 0;    // This file is corrupted.
                                break;
                            case "OutputFolder":
                                temp = value;
                                if ((temp.Length != 0) && (Directory.Exists(value)))
                                    FolderMgt.OutputFolder = value;
                                else FolderMgt.OutputFolder = string.Empty;
                                break;
                            case "SponsorARTCC":
                                temp = value;
                                if (temp.Length != 0)
                                    InfoSection.SponsorARTCC = value;
                                else InfoSection.SponsorARTCC = string.Empty;
                                break;
                            case "DefaultAirport":
                                temp = value;
                                if (temp.Length != 0)
                                    InfoSection.DefaultAirport = value;
                                else InfoSection.DefaultAirport = string.Empty;
                                break;
                            case "FacilityEngineer":
                                temp = value;
                                if (temp.Length != 0)
                                    InfoSection.FacilityEngineer = value;
                                else InfoSection.FacilityEngineer = "Facility Engineer name";
                                break;
                            case "AsstFacilityEngineer":
                                temp = value;
                                if (temp.Length != 0)
                                    InfoSection.AsstFacilityEngineer = value;
                                else InfoSection.AsstFacilityEngineer = string.Empty;
                                break;
                            case "ChkOneSCTFile":
                                SCTchecked.ChkOneVRCFile = Convert.ToBoolean(value);
                                break;
                            case "ChkOneESFile":
                                SCTchecked.ChkOneESFile = Convert.ToBoolean(value);
                                break;
                            case "ChkConfirmOverwrite":
                                SCTchecked.ChkConfirmOverwrite = Convert.ToBoolean(value);
                                break;
                            case "ChkAPT":
                                SCTchecked.ChkAPT = Convert.ToBoolean(value);
                                break;
                            case "LimitAPT2ARTCC":
                                SCTchecked.LimitAPT2ARTC = Convert.ToBoolean(value);
                                break;
                            case "ChkARB":
                                SCTchecked.ChkARB = Convert.ToBoolean(value);
                                break;
                            case "ChkAWY":
                                SCTchecked.ChkAWY = Convert.ToBoolean(value);
                                break;
                            case "ChkFIX":
                                SCTchecked.ChkFIX = Convert.ToBoolean(value);
                                break;
                            case "ChkNDB":
                                SCTchecked.ChkNDB = Convert.ToBoolean(value);
                                break;
                            case "ChkRWY":
                                SCTchecked.ChkRWY = Convert.ToBoolean(value);
                                break;
                            case "ChkSID":
                                SCTchecked.ChkSID = Convert.ToBoolean(value);
                                break;
                            case "ChkSTAR":
                                SCTchecked.ChkSTAR = Convert.ToBoolean(value);
                                break;
                            case "ChkSSDname":
                                SCTchecked.ChkSSDname = Convert.ToBoolean(value);
                                break;
                            case "ChkSUA":
                                SCTchecked.ChkSUA = Convert.ToBoolean(value);
                                break;
                            case "ChkSUA_ClassB":
                                SCTchecked.ChkSUA_ClassB = Convert.ToBoolean(value);
                                break;
                            case "ChkSUA_ClassC":
                                SCTchecked.ChkSUA_ClassC = Convert.ToBoolean(value);
                                break;
                            case "ChkSUA_ClassD":
                                SCTchecked.ChkSUA_ClassD = Convert.ToBoolean(value);
                                break;
                            case "ChkSUA_Danger":
                                SCTchecked.ChkSUA_Danger = Convert.ToBoolean(value);
                                break;
                            case "ChkSUA_Prohibited":
                                SCTchecked.ChkSUA_Prohibited = Convert.ToBoolean(value);
                                break;
                            case "ChkSUA_Restricted":
                                SCTchecked.ChkSUA_Restricted = Convert.ToBoolean(value);
                                break;
                            case "ChkES_SCTfile":
                                SCTchecked.ChkES_SCTfile = Convert.ToBoolean(value);
                                break;
                            case "ChkES_SSDfile":
                                SCTchecked.ChkES_SSDfile = Convert.ToBoolean(value);
                                break;
                            case "ChkVOR":
                                SCTchecked.ChkVOR = Convert.ToBoolean(value);
                                break;
                            case "ChkOceanic":
                                SCTchecked.ChkOceanic = Convert.ToBoolean(value);
                                break;
                            case "UseFixesAsCoords":
                                InfoSection.UseFixesAsCoords = Convert.ToBoolean(value);
                                break;
                            case "UseNaviGraphData":
                                InfoSection.UseNaviGraph = Convert.ToBoolean(value);
                                break;
                            case "OneFilePerSidStar":
                                InfoSection.OneFilePerSidStar = Convert.ToBoolean(value);
                                break;
                            case "DrawFixSymbolsOnDiagrams":
                                InfoSection.DrawFixSymbolsOnDiagrams = Convert.ToBoolean(value);
                                break;
                            case "DrawFixLabelsOnDiagrams":
                                InfoSection.DrawFixLabelsOnDiagrams = Convert.ToBoolean(value);
                                break;
                            case "DrawAltRestrictsOnDiagrams":
                                InfoSection.DrawAltRestrictsOnDiagrams = Convert.ToBoolean(value);
                                break;
                            case "DrawSpeedRestrictsOnDiagrams":
                                InfoSection.DrawSpeedRestrictsOnDiagrams = Convert.ToBoolean(value);
                                break;
                            case "IncludeSidStarReferences":
                                InfoSection.IncludeSidStarReferences = Convert.ToBoolean(value);
                                break;
                            case "NorthLimit":
                                InfoSection.NorthLimit = Convert.ToDouble(value);
                                break;
                            case "SouthLimit":
                                InfoSection.SouthLimit = Convert.ToDouble(value);
                                break;
                            case "WestLimit":
                                InfoSection.WestLimit = Convert.ToDouble(value);
                                break;
                            case "EastLimit":
                                InfoSection.EastLimit = Convert.ToDouble(value);
                                break;
                            case "NorthOffset":
                                InfoSection.NorthOffset = Convert.ToDouble(value);
                                break;
                            case "SouthOffset":
                                InfoSection.SouthOffset = Convert.ToDouble(value);
                                break;
                            case "WestOffset":
                                InfoSection.WestOffset = Convert.ToDouble(value);
                                break;
                            case "EastOffset":
                                InfoSection.EastOffset = Convert.ToDouble(value);
                                break;
                            case "CenterLatitude_Dec":
                                InfoSection.CenterLatitude_Dec = Convert.ToDouble(value);
                                break;
                            case "CenterLongitude_Dec":
                                InfoSection.CenterLongitude_Dec = Convert.ToDouble(value);
                                break;
                            case "RollOverLon":
                                InfoSection.RollOverLongitude = Convert.ToBoolean(value);
                                break;
                            case "SIDprefix":
                                InfoSection.SIDprefix = Convert.ToChar(value);
                                break;
                            case "STARprefix":
                                InfoSection.STARprefix = Convert.ToChar(value);
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            return result;
        }

        public static void ResetCycleInfo()
        {
            // Resets the cycle information
            AIRAC = 1501;
            CycleStart = Convert.ToDateTime("1/1/1900", CultureInfo.InvariantCulture);
            CycleEnd = Convert.ToDateTime("1/2/1900", CultureInfo.InvariantCulture);
            InfoSection.SponsorARTCC = string.Empty;
            InfoSection.DefaultAirport = string.Empty;
        }

        public static int AIRACfromDate(DateTime Cycledate, bool Save2CycleInfo = false)
        /// <summary>
        /// Return the AIRAC info from any given date
        /// Optionally populates the AIRAC cycle information
        /// Note: At 1506 cycles became 28 days (was 34).
        /// </summary>
        {
            DateTime WorkingDate = Start1501;
            int CycleInterval = 28;
            int CycleYear = WorkingDate.Year;
            int iCounter = 0;
            int result;
            // Must know if future or past AIRAC
            while (WorkingDate.AddDays(CycleInterval) <= Cycledate)
            {
                WorkingDate = WorkingDate.AddDays(CycleInterval);
                if (CycleYear != WorkingDate.Year)
                {
                    iCounter = 1;
                    CycleYear = WorkingDate.Year;
                }
                else
                {
                    iCounter += 1;
                }
            }
            string AIRACyear = Convert.ToString(CycleYear).Substring(2, 2);
            result = CalcAIRAC(Convert.ToInt32(AIRACyear), iCounter);
            // OPTIONAL opportunity to save the data in the CycleInfo class
            if (Save2CycleInfo) CycleDateFromAIRAC(result, true);
            return result;
        }

        public static DateTime CycleDateFromAIRAC(int reqAIRAC, bool Save2CycleInfo = false)
        // Returns the beginning and ending cycle date for a given AIRAC
        {
            DateTime WorkingDate = Start1501; DateTime result;
            // Loop the cycles until the calculated AIRAC matches the requested one
            int CycleInterval = 28;
            int CycleYear = WorkingDate.Year;
            int iCounter = 1;
            int WorkingAIRAC = 1501;
            while (WorkingAIRAC < reqAIRAC)
            {
                WorkingDate = WorkingDate.AddDays(CycleInterval);
                if (CycleYear != WorkingDate.Year)
                {
                    iCounter = 1;
                    CycleYear = WorkingDate.Year;
                    WorkingAIRAC = CalcAIRAC(WorkingDate.Year, iCounter);
                }
                else
                {
                    iCounter += 1;
                    WorkingAIRAC = CalcAIRAC(WorkingDate.Year, iCounter);
                }
            }
            result = WorkingDate;
            // OPTIONAL opportunity to save the data in the CycleInfo class
            if (Save2CycleInfo)
            {
                AIRAC = WorkingAIRAC;
                CycleStart = WorkingDate;
                CycleEnd = WorkingDate.AddDays(CycleInterval);
                BuildCycleHeader();
                BuildCycleText();
            }
            // result[0] has start cycle date, result[1] has end cycle date
            return result;
        }

        private static int CalcAIRAC(int iYear, int iCounter)
        // Internal call to generate sample AIRAC values
        {
            int result;
            result = Convert.ToInt32((Extensions.Right(iYear.ToString(), 2)) + iCounter.ToString("00").ToString());
            return result;
        }

        private static string cycleText;
        public static string CycleText
        {
            get { return cycleText; }
            set
            {
                if (AIRAC == 1501)
                {
                    cycleText = "No Active Cycle Data!";
                }
                else
                {
                    cycleText = "AIRAC Cycle: " + AIRAC + cr +
                    "Cycle Start:    " + CycleStart.Date.ToShortDateString() + cr +
                    "Cycle End:     " + CycleEnd.Date.ToShortDateString();
                }
                if (CycleEnd < DateTime.Today)
                    cycleText += cr + "*** Outdated Cycle Data ***";
            }
        }

        private static void BuildCycleText()
        {
            cycleText = "No Active Cycle Data!";
            if (AIRAC != 1501)
            {
                cycleText = "AIRAC Cycle: " + AIRAC + cr +
                "Cycle Start:    " + CycleStart.Date.ToShortDateString() + cr +
                "Cycle End:     " + CycleEnd.Date.ToShortDateString();
            }
            if (CycleEnd < DateTime.Today)
                cycleText += cr + "*** Outdated Cycle Data ***";
        }

        private static string cycleHeader;
        public static string CycleHeader
        {
            get {return cycleHeader; }
            set
            {
                cycleHeader =
                "; ================================================================" + cr +
                "; AIRAC CYCLE: " + CycleInfo.AIRAC + cr +
                "; Cycle: " + CycleInfo.CycleStart.ToShortDateString() + " to " + CycleInfo.CycleEnd.ToShortDateString() + cr +
                "; ================================================================" + cr;
            }
        }

        private static void BuildCycleHeader()
        {
            cycleHeader =
            "; ================================================================" + cr +
            "; AIRAC CYCLE: " + CycleInfo.AIRAC + cr +
            "; Cycle: " + CycleInfo.CycleStart.ToShortDateString() + " to " + CycleInfo.CycleEnd.ToShortDateString() + cr +
            "; ================================================================" + cr;
        }
    }
}
