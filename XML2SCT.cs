using System;
using System.Data;
using System.Windows.Forms;
using System.Xml;
using System.IO;

namespace SCTBuilder
{
    public partial class XML2SCT : Form
    {
        public XML2SCT()
        {
            InitializeComponent();
        }

        private void XMLOpenFileDialogButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog
            {
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
            };

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                SourceFileTextBox.Text = fileDialog.FileName;
            }
            if (SCTFileTextBox.Text.Length == 0)
                SCTFileTextBox.Text = SourceFileTextBox.Text.Substring(0, SourceFileTextBox.Text.IndexOf(".") - 1) + ".SCT";
            fileDialog.Dispose();
        }

        private void SSDRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            SSDLabelsCheckBox.Enabled = SSDRadioButton.Checked;
        }

        private void ConvertXML2SCTButton_Click(object sender, EventArgs e)
        {
            if (File.Exists(SourceFileTextBox.Text))
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(FolderMgt.INIxml);
                foreach (XmlNode node in doc.DocumentElement)
                {

                }
            }
        }
    }
}
