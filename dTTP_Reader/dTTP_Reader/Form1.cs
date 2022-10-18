using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Xml;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dTTP_Reader
{
    public partial class Form1 : Form
    {
        public static DataSet dTTP = new dTTPDataSet();
        public static DataTable state = new dTTPDataSet.state_dTTPDataTable();
        public static DataTable city = new dTTPDataSet.city_dTTPDataTable();
        public static DataTable airport = new dTTPDataSet.airport_dTTPDataTable();
        public static DataTable record = new dTTPDataSet.recordDataTable();
        public static string AIRAC = string.Empty;
        public static DateTime AIRAC_start;
        public static DateTime AIRAC_end;
        public static CultureInfo provider = CultureInfo.InvariantCulture;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ReadXML_dTTP();
            PopulateComboBoxes();
            CycleLabel.Text = "AIRAC: " + AIRAC + " (" + AIRAC_start.ToUniversalTime().ToString("dd/MMM/yyyy HHmmK") + " to " + AIRAC_end.ToUniversalTime().ToString("dd/MMM/yyyy HHmmK") + ")";
        }

        private static void ReadXML_dTTP()
        {
            string xmlFile = "D:\\dTTP\\d-TPP_Metafile.xml";
            string? state_ID;
            string? city_ID;
            string? airport_ID;

            XmlDocument doc = new();
            doc.Load(xmlFile);

            XmlNode dTTPheader = doc.DocumentElement.SelectSingleNode("/digital_tpp");
            // Get cycle value
            AIRAC = dTTPheader.Attributes["cycle"]?.InnerText;
            // Get start of cycle
            string temp = dTTPheader.Attributes["from_edate"]?.InnerText;
            temp = (temp.Substring(6, temp.Length - 6) + " " + temp.Substring(0,5)).Trim();
            AIRAC_start = DateTime.ParseExact(temp, "MM/dd/yy HHmmZ", provider);
            // Get end of cycle
            temp = dTTPheader.Attributes["to_edate"]?.InnerText;
            temp = (temp.Substring(6, temp.Length - 6) + " " + temp.Substring(0, 5)).Trim();
            AIRAC_end = DateTime.ParseExact(temp, "MM/dd/yy HHmmZ", provider);
            
            foreach (XmlElement node in dTTPheader.ChildNodes)
            {
                if (node.Name == "state_code")
                {
                    state_ID = node.Attributes["ID"]?.InnerText;
                    DataRow drs = state.NewRow();
                    drs[0] = state_ID;
                    drs[1] = node.Attributes["state_fullname"]?.InnerText;
                    state.Rows.Add(drs);
                    foreach (XmlNode node2 in node.ChildNodes)
                    {
                        city_ID = node2.Attributes["ID"]?.InnerText;
                        DataRow drc = city.NewRow();
                        drc[0] = city_ID;
                        drc[1] = node2.Attributes["volume"]?.InnerText;
                        drc[2] = state_ID;
                        city.Rows.Add(drc);
                        foreach (XmlNode node3 in node2.ChildNodes)
                        {
                            airport_ID = node3.Attributes["ID"]?.InnerText;
                            DataRow dra = airport.NewRow();
                            dra[0] = airport_ID;
                            dra[1] = node3.Attributes["military"]?.InnerText;
                            dra[2] = node3.Attributes["apt_ident"]?.InnerText;
                            dra[3] = node3.Attributes["icao_ident"]?.InnerText;
                            dra[4] = node3.Attributes["alnum"]?.InnerText;
                            dra[5] = city_ID;
                            dra[6] = state_ID;
                            airport.Rows.Add(dra);
                            foreach (XmlNode node4 in node3.ChildNodes)
                            {
                                int i = 0;
                                DataRow drr = record.NewRow();
                                foreach (XmlNode recordNode in node4.ChildNodes)
                                {
                                    if (i == 16)
                                    {
                                        if (recordNode.InnerText.Length != 0)
                                        {
                                            drr[i] = ParseDate(recordNode.InnerText);
                                        }
                                    }
                                    else
                                        drr[i] = recordNode.InnerText;
                                    i++;
                                }
                                drr[17] = airport_ID;
                                drr[18] = city_ID;
                                drr[19] = state_ID;
                                record.Rows.Add(drr);
                            }
                        }
                    }
                }
            }
        }

        private void PopulateComboBoxes()
        {
            PopulateStateComboBox();
            PopulateCityComboBox();
        }

        private void PopulateStateComboBox()
        {
            if (state.Rows.Count != 0)
            {
                stateComboBox.DataSource = state.DefaultView;
                stateComboBox.DisplayMember = "fullname";
                stateComboBox.ValueMember = "ID";
                stateComboBox.MaxDropDownItems = 6;
                stateComboBox.SelectedIndex = 0;
                PopulateCityComboBox();
            }
        }

        private void PopulateCityComboBox()
        {
            List<string> list = new();
            DataView cityList = city.DefaultView;
            if ((stateComboBox.SelectedIndex != -1) && (state.Rows.Count != 0))
            {
                cityList.RowFilter = "state_FK = '" + stateComboBox.SelectedValue + "'";
                for (int iCount = 0; iCount < cityList.Count; iCount++)
                {
                    var val = cityList[iCount][0].ToString();
                    if ((val != null) && val.Length != 0)
                        if (!list.Contains(val))
                            list.Add(val);
                }
                cityComboBox.DataSource = list;
                cityComboBox.DisplayMember = "ID";
                cityComboBox.MaxDropDownItems = 6;
                cityComboBox.SelectedIndex = -1;
            }
        }

        private void stateComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateCityComboBox();
        }

        private void cityComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cityComboBox.SelectedIndex != -1)
            {
                List<string> list = new();
                for (int iCount = 0; iCount < airport.Rows.Count; iCount++)
                {
                    var val = airport.Rows[iCount][0].ToString();
                    if ((val != null) && val.Length != 0)
                        if (!list.Contains(val))
                            list.Add(val);
                }
                list.Distinct();
                list.Sort();
            }
            else
            {
                DataView dvAirport = new(airport);
                dvAirport.Sort = "ID";
                if ((cityComboBox.SelectedIndex != -1))
                {
                    dvAirport.RowFilter = "city_FK = '" + cityComboBox.SelectedValue + "'";
                }
            }
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            PopulateRecord();
        }

        private static DateTime ParseDate(string date)
        {
            return DateTime.Parse(date);
        }

        private void PopulateRecord()
        {
            string filter; bool includeDate = false;
            DataView dvRecord = new(record);
            dvRecord.Sort = "airport_FK";
            if (cityComboBox.SelectedIndex != -1)
                filter = "([city_FK] = '" + cityComboBox.SelectedValue + "')";
            else
                filter = "([state_FK] = '" + stateComboBox.SelectedValue + "')";
            if (AddedCheckBox.Checked)
            {
                filter += " AND ([useraction] = 'A')";
                includeDate = true;
            }
            if (ChangedCheckBox.Checked)
            {
                filter += " AND ([useraction] <> 'C')";
                includeDate &= true;
            }
            if (DeletedCheckBox.Checked)
            {
                filter += " AND ([useraction] <> 'D')";
                includeDate &= true;
            }
            if (AirportDiagramsCheckBox.Checked)
            {
                filter += " AND ([chart_code] = 'APD')";
            }
            if (filter.Length != 0)
                dvRecord.RowFilter = filter;
            BuildRecordDGV(dvRecord);
        }

        private void BuildRecordDGV(DataView dv)
        {
            DataTable dT = dv.ToTable(true, "airport_FK", "city_FK", "chart_code", "chart_name", "useraction", "pdf_name");
            if (dT.Rows.Count > 0)
            {
                RecordDataGridView.DataSource = dT;
                RecordDataGridView.Columns[0].HeaderText = "Airport";
                RecordDataGridView.Columns[1].HeaderText = "City";
                RecordDataGridView.Columns[2].HeaderText = "Chart";
                RecordDataGridView.Columns[3].HeaderText = "Name";
                RecordDataGridView.Columns[4].HeaderText = "Action";
                RecordDataGridView.Columns[5].HeaderText = "PDF";
                RecordDataGridView.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);
            }
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
            // Calls the default browser to load the desired URL
            // Note must create a new process instance and must trap for a computer with no browser
        {
            string target = "https://www.faa.gov/air_traffic/flight_info/aeronav/digital_products/dtpp/search/advanced/";
            var psi = new System.Diagnostics.ProcessStartInfo();
            psi.UseShellExecute = true;
            psi.FileName = target;
            try
            {
                Process.Start(psi);
            }
            catch (System.ComponentModel.Win32Exception noBrowser)
            {
                if (noBrowser.ErrorCode == -2147467259)
                    MessageBox.Show(noBrowser.Message);
            }
            catch (System.Exception other)
            {
                MessageBox.Show(other.Message);
            }
        }
    }
}