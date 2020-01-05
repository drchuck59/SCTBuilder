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
                "FIXes, Airways, SIDs, STARs, and ARTCC boundaries. This program is intended to" + cr +
                "supplement Herve Sors' IvAcBuilder.  Unfortunately, IvAcBuilder does not keep" + cr +
                "current AIRAC data; sctBuilder allows the user to import current AIRAC data." + cr +
                "NOTE that most users of IvAcBuilder do not use magnetic variation, and IvAcBuilder" + cr +
                "does not adjust for distances by longitude, whereas sctBuilder does both automatically." + cr +
                "This can be reversed by changing the [INFO] section entries to zero and 60, respectively." + cr +
                "Be consistent in your use of these values to avoid rotated/squashed results.";
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

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
