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
                XMLFileTextBox.Text = fileDialog.FileName;
            }
            if (SCTFileTextBox.Text.Length == 0)
                SCTFileTextBox.Text = XMLFileTextBox.Text.Substring(0, XMLFileTextBox.Text.IndexOf(".") - 1) + ".SCT";
            fileDialog.Dispose();
        }

        private void SSDRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            SSDLabelsCheckBox.Enabled = SSDRadioButton.Checked;
        }
    }
}
