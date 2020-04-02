using System;
using System.Xml;
using System.IO;

namespace SCTBuilder
{
    class CycleInfo
    {
        private readonly static DateTime Start1501 = Convert.ToDateTime("01/08/2015");
        public static int AIRAC = 1501;
        public static DateTime CycleStart;
        public static DateTime CycleEnd;

        public static void WriteINIxml()
        {
            XmlWriter xml = XmlWriter.Create(FolderMgt.INIxml);
            xml.WriteStartDocument();
            xml.WriteStartElement("SCT_Builder");
            WriteXmlElement(xml, "Version", VersionInfo.Title.ToString());
            WriteXmlElement(xml, "AIRAC", AIRAC.ToString());
            WriteXmlElement(xml, "CycleStart", CycleStart.ToString());
            WriteXmlElement(xml, "CycleEnd", CycleEnd.ToString());
            WriteXmlElement(xml, "DataFolder", FolderMgt.DataFolder.ToString());
            WriteXmlElement(xml, "OutputFolder", FolderMgt.OutputFolder.ToString());
            WriteXmlElement(xml, "SponsorARTCC", InfoSection.SponsorARTCC.ToString());
            WriteXmlElement(xml, "DefaultAirport", InfoSection.DefaultAirport.ToString());
            WriteXmlElement(xml, "FacilityEngineer", InfoSection.FacilityEngineer.ToString());
            WriteXmlElement(xml, "AsstFacilityEngineer", InfoSection.AsstFacilityEngineer.ToString());
            WriteXmlElement(xml, "ChkAll", SCTchecked.ChkALL.ToString());
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
            WriteXmlElement(xml, "UseFixNames", InfoSection.UseFixes.ToString());
            WriteXmlElement(xml, "UseNaviGraphData", InfoSection.UseNaviGraph.ToString());
            WriteXmlElement(xml, "NorthLimit", FilterBy.NorthLimit.ToString());
            WriteXmlElement(xml, "SouthLimit", FilterBy.SouthLimit.ToString());
            WriteXmlElement(xml, "WestLimit", FilterBy.WestLimit.ToString());
            WriteXmlElement(xml, "EastLimit", FilterBy.EastLimit.ToString());
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
            int result = 1503;
            if (File.Exists(FolderMgt.INIxml))
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(FolderMgt.INIxml);
                foreach (XmlNode node in doc.DocumentElement)
                {
                    if (node.Name != "Version")
                    {
                        string value = node.InnerText;
                        switch (node.Name)
                        {
                            case "AIRAC":
                                AIRAC = Convert.ToInt32(value);
                                result = AIRAC;
                                break;
                            case "CycleStart":
                                CycleStart = Convert.ToDateTime(value);
                                break;
                            case "CycleEnd":
                                CycleEnd = Convert.ToDateTime(value);
                                break;
                            case "DataFolder":
                                FolderMgt.DataFolder = value;
                                break;
                            case "OutputFolder":
                                FolderMgt.OutputFolder = value;
                                break;
                            case "SponsorARTCC":
                                InfoSection.SponsorARTCC = value;
                                break;
                            case "DefaultAirport":
                                InfoSection.DefaultAirport = value;
                                break;
                            case "FacilityEngineer":
                                InfoSection.FacilityEngineer = value;
                                break;
                            case "AsstFacilityEngineer":
                                InfoSection.AsstFacilityEngineer = value;
                                break;
                            case "ChkALL":
                                SCTchecked.ChkALL = Convert.ToBoolean(value);
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
                            case "ChkVOR":
                                SCTchecked.ChkVOR = Convert.ToBoolean(value);
                                break;
                            case "UseFixNames":
                                InfoSection.UseFixes = Convert.ToBoolean(value);
                                break;
                            case "UseNaviGraphData":
                                InfoSection.UseNaviGraph = Convert.ToBoolean(value);
                                break;
                            case "NorthLimit":
                                FilterBy.NorthLimit = Convert.ToDouble(value);
                                break;
                            case "SouthLimit":
                                FilterBy.SouthLimit = Convert.ToDouble(value);
                                break;
                            case "WestLimit":
                                FilterBy.WestLimit = Convert.ToDouble(value);
                                break;
                            case "EastLimit":
                                FilterBy.EastLimit = Convert.ToDouble(value);
                                break;
                            default:
                                break;

                        }
                    }
                }
            }
            else
            {
                result = -1;
            }
            return result;
        }

        public static void ResetCycleInfo()
        {
            // Resets the cycle information
            AIRAC = 1501;
            CycleStart = Convert.ToDateTime("1/1/1900");
            CycleEnd = Convert.ToDateTime("1/2/1900");
            FolderMgt.OutputFolder = string.Empty;
            FolderMgt.DataFolder = string.Empty;
            InfoSection.SponsorARTCC = string.Empty;
            InfoSection.DefaultAirport = string.Empty;
            InfoSection.FacilityEngineer = "Facilities Engineer name";
            InfoSection.AsstFacilityEngineer = "Ass't  Engineer name";
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
                //Console.WriteLine("AIRAC: " + CalcAIRAC(Convert.ToInt32(Convert.ToString(CycleYear).Substring(2, 2)), iCounter) + " Cycle: " + WorkingDate.ToShortDateString());
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
                // Console.WriteLine("AIRAC: " + WorkingAIRAC + " Cycle: " + WorkingDate.ToShortDateString());
            }
            result = WorkingDate;
            // OPTIONAL opportunity to save the data in the CycleInfo class
            if (Save2CycleInfo)
            {
                AIRAC = WorkingAIRAC;
                CycleStart = WorkingDate;
                CycleEnd = WorkingDate.AddDays(CycleInterval);
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

        public static string BuildCycleText()
        {
            string cr = Environment.NewLine;
            string Message =
                "AIRAC Cycle: " + AIRAC + cr +
                "Cycle Start:    " + CycleStart.ToString("dd MMM yyyy") + cr +
                "Cycle End:     " + CycleEnd.ToString("dd MMM yyyy");
            if (CycleEnd < DateTime.Today)
            {
                Message = Message + cr + "*** Outdated Cycle Data ***";
            }
            return Message;
        }
    }
}
