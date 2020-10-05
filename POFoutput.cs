using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCTBuilder
{
    class POFoutput
    {
        public DataTable POF = Form1.POFdata;

        private void WritePOF(bool AsESE = false)
        {
            string line = string.Empty; 
            int maxCol = 11; if (AsESE) maxCol += 8;
            // Open and write the lines
            string filename = FolderMgt.OutputFolder + CycleInfo.AIRAC + ".pof";
            using (StreamWriter sw = new StreamWriter(filename))
            {
                // Loop the lines
                foreach (DataRow POFentry in POF.AsEnumerable())
                {
                    // Loop the columns
                    for (int i = 0; i < maxCol; i++)
                    {
                        line += POFentry[i] + ":";
                    }
                    // Remove the last colon
                    line = line.Substring(0, line.Length - 1);
                    sw.WriteLine(line);
                }
            }
        }
    }
}
