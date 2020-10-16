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
    public partial class Racetrack : Form
    {
        public Racetrack()
        {
            InitializeComponent();
        }

        private void IdentifierTextBox_TextChanged(object sender, EventArgs e)
        {
            // Search the tables for fixes and return the coordinates, 
            // then place the coordinates into the list box.
            //
            if (IdentifierTextBox.TextLength != 0)
            {
                // First, be sure there is data in the database!
                DataTable dtVOR = Form1.VOR;
                DataTable dtNDB = Form1.NDB;
                DataTable dtFIX = Form1.FIX;
                if ((dtFIX.Rows.Count != 0) && (dtVOR.Rows.Count != 0) && (dtNDB.Rows.Count != 0))
                {
                    // Load the gridview - can be sorted later.  Future: add [Selected]
                    string filter = "[FacilityID] LIKE '" + IdentifierTextBox.Text + "*" + "'";
                    DataView dvVOR = new DataView(dtVOR, filter, "FacilityID", DataViewRowState.CurrentRows);
                    DataView dvNDB = new DataView(dtNDB, filter, "FacilityID", DataViewRowState.CurrentRows);
                    DataView dvFIX = new DataView(dtFIX, filter, "FacilityID", DataViewRowState.CurrentRows);
                    DataTable dtFixList = dvVOR.ToTable(true, "FacilityID", "Latitude", "Longitude", "MagVar");
                    dtFixList.Merge(dvNDB.ToTable(true, "FacilityID", "Latitude", "Longitude", "MagVar"));
                    dtFixList.Merge(dvFIX.ToTable(true, "FacilityID", "Latitude", "Longitude"));
                    FixListDataGridView.DataSource = dtFixList;
                    FixListDataGridView.DefaultCellStyle.Font = new Font("Arial", 9);
                    FixListDataGridView.Columns[0].HeaderText = "ID";
                    if (FixListDataGridView.Rows.Count != 0)
                    {
                        FixListDataGridView.AutoResizeColumn(0, DataGridViewAutoSizeColumnMode.AllCells);
                        FixListDataGridView.Columns[3].Visible = false;
                    }
                }
                else
                {
                    string Msg = "You must select your FAA data folder before you can search for Fixes.";
                    SCTcommon.SendMessage(Msg);
                    IdentifierTextBox.Text = string.Empty;
                }
            }
            else
            {
                FixListDataGridView.DataSource = null;
            }
        }

        private void LoadAltTypeComboBox()
        {
            DataTable dT = new DataTable();
            dT.Columns.Add("AltTypeValue", typeof(int));
            dT.Columns.Add("AltTypeName", typeof(string));
            dT.Rows.Add(0, "AOB 6000 ft");
            dT.Rows.Add(1, "AOB 14000 ft");
            dT.Rows.Add(2, "Above 14000 ft");
            dT.Rows.Add(3, "USAF airfield");
            dT.Rows.Add(4, "USN airfield");
            dT.Rows.Add(5, "Rotary below 1000 ft");
            AltitudeTypeComboBox.DataSource = dT;
            AltitudeTypeComboBox.DisplayMember = "AltTypeName";
            AltitudeTypeComboBox.ValueMember = "AltTypeValue";
            AltitudeTypeComboBox.SelectedIndex = 2;
        }
    }
}
