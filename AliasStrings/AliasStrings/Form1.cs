using System.Data;
using System.Diagnostics;
using System.Net;
using System.Reflection;
using System.Security.Policy;
using System.Xml;
using System.Windows;

namespace AliasStrings
{
    public partial class Form1 : Form
    {
        public DataTable? dt;
        public string FilterOn = "Orig";
        public string SortOn = "Dest";
        public string RouteType = "All";
        public DataTable dgvTable;

        public Form1()
        {
            InitializeComponent();
            ReadCsvFile();
        }

        private void ReadCsvFile()
        {
            // Needs to be changed to on of these two:
            //This one will return the directory of whatever is executing at the time (EXE or DLL)
            // string BasePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            // This one will return the same but with the executing file name
            // BasePath = System.Windows.Forms.Application.ExecutablePath;
            // For Windows Forms System.IO.Path.GetDirectoryName(Application.ExecutablePath);
            
            string CsvFilePath = "D:\\OneDrive\\Documents\\vFE_Files\\Resources\\AliasBuilder\\prefroutes_db.csv";
            char csvDelimiter = ',';
            dt = CSVtoDataTable(CsvFilePath, csvDelimiter);
            Orig_ComboBox_Populate();
            ARTCC_Textbox_Populate();
            ARTCC_ComboBox_Populate();
            Dgv_Populate();
        }


        private void Dgv_Populate()
        {
            string filter = string.Empty;
            if (ARTCC_ComboBox.SelectedIndex > 0)
            {
                filter = "(DCNTR = '";
                if (FilterOn == "Dest") filter = "(ACNTR = '";
                filter += ARTCC_ComboBox.SelectedValue.ToString() + "')";
            }
            if (Orig_ComboBox.SelectedIndex != null)
            {
                if (Orig_ComboBox.SelectedIndex != -1)
                {
                    if (filter.Length != 0) filter += " AND ";
                    filter += "(" + FilterOn + " = '" + Orig_ComboBox.SelectedValue.ToString() + "')";
                }
            }
            if (!Type_All_checkBox.Checked)
            {
                string filterTypes = " AND ( ";
                bool first = true;
                foreach (CheckBox cb in Types_GroupBox.Controls)
                {
                    if (cb.Checked)
                    {
                        if (!first) filterTypes += " OR ";
                        filterTypes += " ([Type] = '" + cb.Tag.ToString() + "')";
                        first = false;
                    }
                }
                if (!first)
                {
                    filterTypes += " )";
                    filter += filterTypes;
                }
            }
            string sort = SortOn + " ASC";
            DataView dv = new(dt)
            {
                RowFilter = filter,
                Sort = sort,
            };
            dgvTable = dv.ToTable();
            dataGridView1.DataSource = dgvTable;
            dv.Dispose();
        }

        private void Orig_ComboBox_Populate(bool byARTCC = false)
        {
            DataView dv = new(dt)
            {
                Sort = FilterOn + " ASC"
            };
            if (byARTCC)
            {
                string filter = "DCNTR = '";
                if (FilterOn == "Dest") filter = "ACNTR = '";
                filter += ARTCC_ComboBox.SelectedValue.ToString() + "'";
                dv.RowFilter = filter;
            }
            DataTable Origdt = dv.ToTable(true, FilterOn);
            Orig_ComboBox.DataSource = Origdt;
            Orig_ComboBox.DisplayMember = FilterOn;
            Orig_ComboBox.ValueMember = FilterOn;
            dv.Dispose();
        }

        private void ARTCC_ComboBox_Populate()
        {
            string ARTCCfilter = "DCNTR";
            if (FilterOn == "Dest") ARTCCfilter = "ACNTR";
            DataView dv = new(dt)
            {
                Sort = ARTCCfilter + " ASC"
            };
            DataTable ARTCCdt = dv.ToTable(true, ARTCCfilter);
            ARTCC_ComboBox.DataSource = ARTCCdt;
            ARTCC_ComboBox.DisplayMember = ARTCCfilter;
            ARTCC_ComboBox.ValueMember = ARTCCfilter;
            dv.Dispose();
        }

        private void ARTCC_Textbox_Populate()
        {
            string filter;
            if (Orig_ComboBox.SelectedValue != null)
            {
                filter = FilterOn + " = '" + Orig_ComboBox.SelectedValue.ToString() + "'";
                string CNTR = "DCNTR";
                if (FilterOn == "Dest") CNTR = "ACNTR";

                DataView dv = new(dt)
                {
                    RowFilter = filter,
                };
                if ((dv.Count != 0) && (dv[0][CNTR].ToString().Length != 0))
                    OrigARTCC_TextBox.Text = "(" + dv[0][CNTR].ToString() + ")";
                dv.Dispose();
            }
            else
                OrigARTCC_TextBox.Text = "(n/a)";
        }

        public static DataTable CSVtoDataTable(string strFilePath, char csvDelimiter)
        {
            DataTable Tempdt = new DataTable();
            using (StreamReader sr = new StreamReader(strFilePath))
            {
                string[] headers = sr.ReadLine().Split(csvDelimiter);
                foreach (string header in headers)
                {
                    try
                    {
                        Tempdt.Columns.Add(header);
                    }
                    catch { }
                }
                while (!sr.EndOfStream)
                {
                    string[] rows = sr.ReadLine().Split(csvDelimiter);
                    DataRow dr = Tempdt.NewRow();
                    for (int i = 0; i < headers.Length; i++)
                    {
                        dr[i] = rows[i];
                    }
                    Tempdt.Rows.Add(dr);
                }

            }
            return Tempdt;
        }

        private void Orig_ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ARTCC_Textbox_Populate();
            Dgv_Populate();
        }

        private void Origin_RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (Origin_RadioButton.Checked)
            {
                FilterOn = "Orig";
                SortOn = "Dest";
            }
            ARTCC_ComboBox_Populate();
            Dgv_Populate();
        }

        private void Dest_RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (Dest_RadioButton.Checked)
            {
                FilterOn = "Dest";
                SortOn = "Orig";
            }
            Dgv_Populate();
        }

        private void ClearRouteTypes()
        {
            foreach (CheckBox cb in Types_GroupBox.Controls)
            {
                if (cb.Text != "All")
                    cb.Checked = false;
            }
        }

        private void ClearAllType()
        {
            bool CheckAll = false;
            foreach (CheckBox tb in Types_GroupBox.Controls)
            {
                if (tb.Text != "All")
                    CheckAll |= tb.Checked;
            }
            Type_All_checkBox.Checked = !CheckAll;
        }

        private void Type_All_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            if (Type_All_checkBox.Checked)
            ClearRouteTypes();
            Dgv_Populate();
        }

        private void Type_L_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            ClearAllType();
            Dgv_Populate();
        }

        private void Type_LSD_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            ClearAllType();
            Dgv_Populate();
        }

        private void Type_SLD_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            ClearAllType();
            Dgv_Populate();
        }

        private void Type_H_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            ClearAllType();
            Dgv_Populate();
        }

        private void Type_HSD_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            if (Type_HSD_checkBox.Checked)
            ClearAllType();
            Dgv_Populate();
        }

        private void Type_SHD_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            if (Type_SHD_checkBox.Checked)
            ClearAllType();
            Dgv_Populate();
        }

        private void Type_TEC_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            if (Type_TEC_checkBox.Checked)
            ClearAllType();
            Dgv_Populate();
        }

        //function to read gridview settings from an xml file  
        public void ReadDataGridViewSetting(DataGridView dgv, string FileName)
        {
            string appDatafolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            appDatafolder = Path.Combine(appDatafolder, "AliasStrings");

            //declare the filestream for reading and accessing the xml file and test if it exists
            string FAAfile = appDatafolder + "\\" + FileName + ".xml";
            if (File.Exists(FAAfile))
            {
                FileStream fs = new(FAAfile, FileMode.Open, FileAccess.Read);

                //declare the xmldocument object  
                XmlDocument xmldoc = new XmlDocument();
                //and also xmllistnode  
                XmlNodeList xmlnode;

                //pass the filestreanm as object for xmlnode load event  
                xmldoc.Load(fs);

                xmlnode = xmldoc.GetElementsByTagName("column");
                for (int i = 0; i <= xmlnode.Count - 1; i++)
                {
                    //read the first node of the current column node and set it datagrideview name'  
                    //and we are going to use it in our code for setting the columns properties  
                    string columnName = xmlnode[i].ChildNodes.Item(0).InnerText.Trim();
                    int width = int.Parse(xmlnode[i].ChildNodes.Item(1).InnerText.Trim());
                    string headertext = xmlnode[i].ChildNodes.Item(2).InnerText.Trim();
                    int displayindex = int.Parse(xmlnode[i].ChildNodes.Item(3).InnerText.Trim());
                    Boolean visible = Convert.ToBoolean(xmlnode[i].ChildNodes.Item(4).InnerText.Trim());
                    //after finding and pereparing data now i set the grid properties  
                    //set the witdh  
                    dgv.Columns[columnName].Width = width;
                    //set the headertext  
                    dgv.Columns[columnName].HeaderText = headertext;
                    //set the column index it means the order of the column  
                    dgv.Columns[columnName].DisplayIndex = displayindex;
                    //set the visibility  
                    dgv.Columns[columnName].Visible = visible;
                }
                fs.Close();
            }
        }
        //on the closing event of the form save the gridview settings
        public void WriteGrideViewSetting(DataGridView dgv, string FileName)
        {
            //Build or declare the appdata folder
            string appDatafolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            appDatafolder = Path.Combine(appDatafolder, "AliasStrings");
            Directory.CreateDirectory(appDatafolder);

            //declaring the xmlwriter object  
            XmlTextWriter settingwriter = new XmlTextWriter(appDatafolder + "\\" + FileName + ".xml", null);
 
            // declare the setting tag of the current example and specify the name for the this tag  
            settingwriter.WriteStartDocument();
            settingwriter.WriteStartElement(dgv.Name);
            int count = dgv.Columns.Count;
        //count the gridview column  
            for (int i = 0; i < count; i++)
            {
                //now create the column root node  
                settingwriter.WriteStartElement("column");
                //then create the name node and fill the value in this node  
                settingwriter.WriteStartElement("Name");
                settingwriter.WriteString(dgv.Columns[i].Name);
                // close the name node  
                settingwriter.WriteEndElement();
                //these three node are declared similar to previous node  
                settingwriter.WriteStartElement("width");
                settingwriter.WriteString(dgv.Columns[i].Width.ToString());
                settingwriter.WriteEndElement();
                settingwriter.WriteStartElement("headertext");
                settingwriter.WriteString(dgv.Columns[i].HeaderText);
                settingwriter.WriteEndElement();
                settingwriter.WriteStartElement("displayindex");
                settingwriter.WriteString(dgv.Columns[i].DisplayIndex.ToString());
                settingwriter.WriteEndElement();
                settingwriter.WriteStartElement("visible");
                settingwriter.WriteString(dgv.Columns[i].Visible.ToString());
                settingwriter.WriteEndElement();
                //end the column node  
                settingwriter.WriteEndElement();
            }
            //end the main root of the xml file which is datagrid name  
            settingwriter.WriteEndElement();
            //end the settingwritter  
            settingwriter.WriteEndDocument();
            //the close the wriiter  
            settingwriter.Close();
        }

        //  Save the user's last search settings
        public void WriteSearchSetting(string FileName)
        {
            //Build or declare the appdata folder
            string appDatafolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            appDatafolder = Path.Combine(appDatafolder, "AliasStrings");
            Directory.CreateDirectory(appDatafolder);

            //declare the xmlwriter object  
            XmlTextWriter settingwriter = new XmlTextWriter(appDatafolder + "\\" + FileName + ".xml", null);

            // thdeclare the setting tag of the current example and specify the name for the this tag  
            settingwriter.WriteStartDocument();
            //now create the combobox root node  
            settingwriter.WriteStartElement("combobox");
            //then create the name node and fill the value in this node  
            ComboBox cb = Orig_ComboBox;
            settingwriter.WriteStartElement(cb.Name);
            settingwriter.WriteString(cb.SelectedValue.ToString());
            settingwriter.WriteEndElement();
            settingwriter.WriteEndElement();
            // create the radiobutton node
            settingwriter.WriteStartElement("radiobuttons");
            foreach (RadioButton ctrl in Types_GroupBox.Controls)
            {
                settingwriter.WriteStartElement(ctrl.Text);
                settingwriter.WriteString(ctrl.Checked.ToString());
                settingwriter.WriteEndElement();
            }
            settingwriter.WriteEndElement();
            // create the checkbox node
            settingwriter.WriteStartElement("checkboxes");
            foreach (CheckBox ctrl in Types_GroupBox.Controls)
            {
                settingwriter.WriteStartElement(ctrl.Text);
                settingwriter.WriteString(ctrl.Checked.ToString());
                settingwriter.WriteEndElement();
            }
            settingwriter.WriteEndElement();
            //end the settingwritter  
            settingwriter.WriteEndDocument();
            //the close the wriiter  
            settingwriter.Close();
        }

        public void ReadSearchSetting(string FileName)
        {
            string appDatafolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            appDatafolder = Path.Combine(appDatafolder, "AliasStrings");
            Form form = this;

            //declare the filestream for reading and accessing the xml file and test if it exists
            string FAAfile = appDatafolder + "\\" + FileName + ".xml";
            if (File.Exists(FAAfile))
            {
                FileStream fs = new(FAAfile, FileMode.Open, FileAccess.Read);

                //declare the xmldocument object  
                XmlDocument xmldoc = new XmlDocument();
                //and also xmllistnode  
                XmlNodeList xmlnode;
                xmlnode = xmldoc.GetElementsByTagName("combobox");
                for (int i = 0; i <= xmlnode.Count - 1; i++)
                {
                    Orig_ComboBox.SelectedValue = xmlnode[i].ChildNodes.Item(1).InnerText.Trim();
                }
                xmlnode = xmldoc.GetElementsByTagName("radiobutton");
                int j = 0;
                foreach (RadioButton ctrl in Filter_groupBox.Controls)
                {
                    if (xmlnode[j].ChildNodes.Item(j).InnerText.Trim() == "true")
                        ctrl.Checked = true;
                    else ctrl.Checked = false;
                    j++;
                }
                xmlnode = xmldoc.GetElementsByTagName("checkboxes");
                j = 0;
                foreach (RadioButton ctrl in Types_GroupBox.Controls)
                {
                    if (xmlnode[j].ChildNodes.Item(j).InnerText.Trim() == "true")
                        ctrl.Checked = true;
                    else ctrl.Checked = false;
                    j++;
                }
                fs.Close();
            }
        }

        private async void GetFAAPreferredRoutes()
        {
            // This will retrieve the FAA preferred routes for the program.
            // NOTE that the file does not give the AIRAC, so the user should
            // update the file regularly.  (Save to location of installed program)
            // Location of download: https://www.fly.faa.gov/rmt/data_file/prefroutes_db.csv
            string appDatafolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            appDatafolder = Path.Combine(appDatafolder, "AliasStrings");
            Directory.CreateDirectory(appDatafolder);           // Should already be present
            string urlFAA = "https://www.fly.faa.gov/rmt/data_file/prefroutes_db.csv";
            var fileInfo = new FileInfo(appDatafolder + "\\" + "prefroutes_db.csv");

            using var HttpClient = new HttpClient();
            try
            {
                HttpResponseMessage response = await HttpClient.GetAsync(urlFAA);
                response.EnsureSuccessStatusCode();
                await using var ms = await response.Content.ReadAsStreamAsync();
                await using var fs = File.Create(fileInfo.FullName);
                ms.Seek(0, SeekOrigin.Begin);
                ms.CopyTo(fs);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            GetFAAPreferredRoutes();
            ReadDataGridViewSetting(dataGridView1, "firstgrid");
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            WriteGrideViewSetting(dataGridView1, "firstgrid");
        }

        private void ARTCC_ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Orig_ComboBox_Populate(true);
            Orig_ComboBox.SelectedIndex = -1;
            Dgv_Populate();
        }

        private string CreateAliasLine(DataGridViewRow row, bool asAlias)
        {
            string result = string.Empty;
            string orig = row.Cells["Orig"].Value.ToString().ToLower();
            string rte = row.Cells["Route String"].Value.ToString() + " ";
            string dest = row.Cells["Dest"].Value.ToString().ToLower();
            string hours = "(" + row.Cells["Hours1"].Value.ToString() + ")";
            if (hours.Length < 4) hours = string.Empty;
            string type = "(" + row.Cells["Type"].Value.ToString() + ")";
            string ac = "(" + row.Cells["Aircraft"].Value.ToString() + ")";
            if (ac.Length < 4) ac = string.Empty;
            string altitude = "(" + row.Cells["Altitude"].Value.ToString() + ")";
            if (altitude.Length < 4) altitude = string.Empty;
            string area = "(" + row.Cells["Area"].Value.ToString() + ")";
            if (area.Length < 4) area = string.Empty;
            string direction = "(" + row.Cells["Direction"].Value.ToString() + ")";
            if (direction.Length < 4) direction = string.Empty;
            if (asAlias)
                result += "." + orig + dest + " .msg PREF_RTES :: " + rte + hours + type + ac + altitude + area + direction;
            else
                result += rte + hours + type + ac + altitude + area + direction;
            return result;   
        }

        private void CallAliasCreate(bool asAlias)
        {
            string cr = Environment.NewLine;
            string result = string.Empty;
            if (dataGridView1.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    result += CreateAliasLine(row, asAlias) + cr;
                }

                if (result.Length != 0)
                {
                    Message_TextBox.Text = result;
                    Clipboard.SetText(Message_TextBox.Text);
                    ShowCopiedLabel();
                }
            }
            else
                MessageBox.Show("You must select at least one row.", "AliasBuilder", MessageBoxButtons.OK);
        }

        private void Clip_button_Click(object sender, EventArgs e)
        {
            CallAliasCreate(asAlias: false);
        }

        private void Alias_button_Click(object sender, EventArgs e)
        {
            CallAliasCreate(asAlias: true);
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Clipboard.SetText(dataGridView1.SelectedCells[0].Value.ToString());
            ShowCopiedLabel();
        }

        private void ShowCopiedLabel()
        {
            Copied_label.Visible = true;
            Copied_label.Refresh();
            Thread.Sleep(750);
            Copied_label.Visible = false;
            Copied_label.Refresh();
        }
    }
}