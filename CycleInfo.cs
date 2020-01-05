using System;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using System.IO;
using System.Threading.Tasks;

namespace SCTBuilder
{
    class CycleInfo
    {
        private readonly static DateTime Start1506 = Convert.ToDateTime("05/28/2015");
        public static int AIRAC = 1506;
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
            WriteXmlElement(xml, "ChkARB", SCTchecked.ChkARB.ToString());
            WriteXmlElement(xml, "ChkAWY", SCTchecked.ChkAWY.ToString());
            WriteXmlElement(xml, "ChkFIX", SCTchecked.ChkFIX.ToString());
            WriteXmlElement(xml, "ChkNDB", SCTchecked.ChkNDB.ToString());
            WriteXmlElement(xml, "ChkRWY", SCTchecked.ChkRWY.ToString());
            WriteXmlElement(xml, "ChkSSD", SCTchecked.ChkSSD.ToString());
            WriteXmlElement(xml, "ChkSSDname", SCTchecked.ChkSSDname.ToString());
            WriteXmlElement(xml, "ChkSUA", SCTchecked.ChkSUA.ToString());
            WriteXmlElement(xml, "ChkSUA_ClassB", SCTchecked.ChkSUA_ClassB.ToString());
            WriteXmlElement(xml, "ChkSUA_ClassC", SCTchecked.ChkSUA_ClassC.ToString());
            WriteXmlElement(xml, "ChkSUA_ClassD", SCTchecked.ChkSUA_ClassD.ToString());
            WriteXmlElement(xml, "ChkSUA_Danger", SCTchecked.ChkSUA_Danger.ToString());
            WriteXmlElement(xml, "ChkSUA_Prohibited", SCTchecked.ChkSUA_Prohibited.ToString());
            WriteXmlElement(xml, "ChkSUA_Restricted", SCTchecked.ChkSUA_Restricted.ToString());
            WriteXmlElement(xml, "ChkVOR", SCTchecked.ChkVOR.ToString());
            xml.WriteEndDocument();
            xml.Close();
        }
        private static void WriteXmlElement(XmlWriter xml, string Element, string Value)
        {
            xml.WriteStartElement(Element);
            xml.WriteString(Value);
            xml.WriteEndElement();
        }

        public static void ReadINIxml()
        {
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
                            case "ChkSSD":
                                SCTchecked.ChkSSD = Convert.ToBoolean(value);
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
                            default:
                                break;

                        }
                    }
                }
            }
            else
                ResetCycleInfo();
        }

        private static void ResetCycleInfo()
        {
            AIRAC = 1503;
            CycleStart = Convert.ToDateTime("1/1/1900");
            CycleEnd = Convert.ToDateTime("1/2/1900");
            FolderMgt.OutputFolder = string.Empty;
            FolderMgt.DataFolder = string.Empty;
            InfoSection.SponsorARTCC = string.Empty;
            InfoSection.DefaultAirport = string.Empty;
            InfoSection.FacilityEngineer = "Facilities Engineer name";
            InfoSection.AsstFacilityEngineer = "Ass't  Engineer name";
        }

        public static int FindAIRAC(DateTime Cycledate, bool SetCycleInfo = false)
        /// <summary>
        /// Return the AIRAC info from any given date
        /// And populates the AIRAC cycle information
        /// At 1506 cycles became 28 days (was 34).
        /// </summary>
        {
            DateTime WorkingDate = Start1506;
            int CycleInterval = 28;
            int CycleYear = WorkingDate.Year;
            int iCounter = 0;
            // Must know if future or past AIRAC
            while (WorkingDate < Cycledate)
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
            if (SetCycleInfo)
            {
                AIRAC = CalcAIRAC(Convert.ToInt32(Convert.ToString(CycleYear).Substring(2, 2)), (iCounter));
                CycleStart = WorkingDate;
                CycleEnd = WorkingDate.AddDays(CycleInterval);
            }
            // Console.WriteLine("FindAIRAC result: " + CalcAIRAC(Convert.ToInt32(Convert.ToString(CycleYear).Substring(2, 2)), (iCounter)));
            return CalcAIRAC(Convert.ToInt32(Convert.ToString(CycleYear).Substring(2, 2)), (iCounter));
        }
        public static DateTime FindCycle(int reqAIRAC, bool SetCycleInfo = false)
            // Returns the beginning cycle date for a given AIRAC
        {
            DateTime WorkingDate = Start1506;
            int CycleInterval = 28;
            int CycleYear = WorkingDate.Year;
            int iCounter = 1;
            int WorkingAIRAC = CalcAIRAC(CycleYear, iCounter);

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
            if (SetCycleInfo)
            {
                AIRAC = WorkingAIRAC;
                CycleStart = WorkingDate;
                CycleEnd = WorkingDate.AddDays(CycleInterval);
            }
            Console.WriteLine("FindCycle result: " + Convert.ToDateTime(WorkingDate));
            return WorkingDate;
        }
        private static int CalcAIRAC(int aYear, int aCounter)
        {
            return Convert.ToInt32((Extensions.Right(aYear.ToString(), 2)) + aCounter.ToString("D2").ToString());
        }
        public static string BuildCycleText()
        {
            string Message =
                "AIRAC Cycle: " + CycleInfo.AIRAC + Environment.NewLine +
                "Cycle Start:    " + CycleInfo.CycleStart.ToString("dd MMM yyyy") + Environment.NewLine +
                "Cycle End:     " + CycleInfo.CycleEnd.ToString("dd MMM yyyy");
            if (CycleInfo.CycleEnd < DateTime.Today)
            {
                Message = Message + Environment.NewLine + "*** Outdated Cycle Data ***";
            }
            // Console.WriteLine(Message + Environment.NewLine);
            return Message;
        }
    }
}
