using MySqlX.XDevAPI.Relational;
using System;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace SCTBuilder
{
    public class ReadPOF
    {
        private static readonly string cr = Environment.NewLine;

        public static int ReadPOFfile(string filepath = "")
        {
            int result;
            string line; string FullFilename;
            Form1.POFdata.Clear();
            if (filepath.Length == 0)
            {
                FullFilename = SCTcommon.GetFullPathname(FolderMgt.DataFolder, "ARTCC.pof");
                string msg = "In order to generate the [POSITIONS] section of the ESE file, " + cr +
                    "you must place a copy of your POF file in the SCT Builder resource folder." + cr +
                    "Name the file ARTCC.pof. You can do this now." + cr + cr +
                    "Click OK to continue or Cancel to skip creating the [POSITIONS] section";
                while (FullFilename == "ERROR")
                {
                    DialogResult dialogResult = SCTcommon.SendMessage(msg, MessageBoxIcon.Warning, MessageBoxButtons.OKCancel); ;
                    if (dialogResult == DialogResult.Cancel) break;
                    FullFilename = SCTcommon.GetFullPathname(FolderMgt.DataFolder, "ARTCC.pof");
                }
            }
            else
                FullFilename = filepath;
            using (StreamReader sr = new StreamReader(FullFilename))
            {
                while ((line = sr.ReadLine()) != null)
                {
                    if ((line.Length != 0) && (line.Substring(0,1) != ";"))
                        LoadPOFrow(line);
                }
            }
            result = Form1.POFdata.Rows.Count;
            if (result > 0) AddCoordsToPOF();
            Console.WriteLine("ReadPOF.ReadPOFfile added " + result + " rows to POFdata table.");
            return result;
        }

        private static void LoadPOFrow(string line)
        {
            // blank lines and comment lines assumed not sent
            int i = 0; int loc1; int rowCounter = 1;
            DataView dvPOF = new DataView(Form1.POFdata);
            DataRowView newRow = dvPOF.AddNew();
            while (line.Length != 0)
            {
                // remove comment if exists
                loc1 = line.Substring(1, line.Length - 1).IndexOf(";");
                if (loc1 != -1) line = line.Substring(0, loc1);
                // Loop the line and parse out details (last item will not have trailing :)
                if (line.IndexOf(":") != -1)
                {
                    if (line.IndexOf(":") == 0)
                    {
                        // Account for no data in this entry (that is, two colons)
                        newRow[i] = string.Empty;
                        line = line.Substring(1);
                    }
                    else
                    {
                        // regular cut
                        newRow[i] = line.Substring(0, line.IndexOf(":")).Trim();
                        line = line.Substring(line.IndexOf(":") + 1);
                    }
                    i++;
                }
                else
                {
                    // last item in line
                    newRow[i] = line.Trim();
                    line = string.Empty;
                    newRow["Sequence"] = rowCounter;
                    newRow.EndEdit();
                    rowCounter++;
                }
            }
        }

        public static void AddCoordsToPOF()
        {
            DataView dvPOF = new DataView(Form1.POFdata);
            DataView dvAPT = new DataView(Form1.APT);
            int ErrorCount = 0;
            // POF prefix should have the abbreviation of the Airport
            // If RName has 'Center', use saved centering coordinates
            // Otherwise, lookup the airport to use as center coords
            foreach(DataRowView drvPOF in dvPOF)
            {
                switch (drvPOF["CallSuffix"].ToString())
                { 
                case "CTR":
                        drvPOF["Latitude"] = InfoSection.CenterLatitude_SCT;
                        drvPOF["Longitude"] = InfoSection.CenterLongitude_SCT;
                        break;
                case "APP":
                case "TWR":
                case "GND":
                case "DEL":
                    dvAPT.RowFilter = "[FacilityID] = '" + drvPOF["CallPrefix"].ToString() + "'";
                    if (dvAPT.Count > 0)
                    {
                        drvPOF["Latitude"] = Conversions.DecDeg2SCT(Convert.ToDouble(dvAPT[0]["Latitude"]), true);
                        drvPOF["Longitude"] = Conversions.DecDeg2SCT(Convert.ToDouble(dvAPT[0]["Longitude"]), false);
                    }
                    else
                    {
                        drvPOF["Latitude"] = "Not Found";
                        drvPOF["Longitude"] = "Not Found";
                        ErrorCount++;
                    }
                    break;
                default:
                    break;
                }
            }
            if (ErrorCount > 0)
            {
                string msg = ErrorCount.ToString() + " positions do not have coordinates (Euroscope only).";
                SCTcommon.SendMessage(msg, MessageBoxIcon.Error);
            }
        }
    }
}
