using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SCTBuilder
{
    public partial class FormInstructions : Form
    {
        public FormInstructions()
        {
            InitializeComponent();
        }

        private void FormInstructions_Activated(object sender, EventArgs e)
        {
            string cr = Environment.NewLine;
            string Message =
                "Welcome to the VATUSA SCT Builder by ZJX ARTCC!" + cr + cr +
                "This program will read the 28-day NASR subscription cycle (see link below) " + cr +
                "and import the data into the program.  You may then modify some settings, " + cr +
                "and the program will generate an ENTIRE sector file with airports, VORs, NDBs," + cr +
                "FIXes, Airways, SIDs, STARs, and ARTCC boundaries.  To do this: " + cr + cr +
                "1.   Open the link below and download the desired set of files." + cr +
                "2.   Unzip the files to any desired folder." + cr +
                "3.   Use the Data Folder textbox or selection button to identify the folder." + cr +
                "      NOTE: SCT Builder reads subfolders but will only accept ONE folder of data." + cr +
                "            Do not select a parent folder containing multiple NASR subscriptions!" + cr +
                "4. Choose a folder to receive the data (Output folder)." + cr +
                "5. Select YOUR ARTCC and YOUR center for VRC (must be a Class B or Class C airport)." + cr +
                "6. Modify the format (boundaries, a square defined by the limits of the ARTCC, or a " + cr +
                "   circle based upon the airport used for centering." + cr +
                "7. Review the datagrids, unchecking items that you do not feel should be included." + cr +
                "   NOTE: Changing your ARTCC will reset ALL the selected items for boundary or squares." + cr +
                "         Changing the Center Airport will reset ALL selected items ONLY if circle was" + cr +
                "         previously selected AND you choose yes to the challenge question." + cr +
                " 8. When you are satisified with your settings, click the 'WRITE SCT' button!" + cr +
                "    The file will always be named 'cycle_ARTCC' in the output folder.";
            label1.Text = Message;
        }

        private void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            linkLabel1.LinkVisited = true;
            System.Diagnostics.Process.Start("https://www.faa.gov/air_traffic/flight_info/aeronav/aero_data/NASR_Subscription/");
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
