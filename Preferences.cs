using System;
using System.Windows.Forms;

namespace SCTBuilder
{
    public partial class Preferences : Form
    {
        public Preferences()
        {
            InitializeComponent();
        }

        private void UseFixesAsCoordinatesCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (UseFixesAsCoordinatesCheckBox.Checked) IncludeSIDSTARReferencesCheckBox.Checked = true;
        }

        private void Preferences_Load(object sender, EventArgs e)
        {
            RestorePreferences();
        }

        private void RestoreButton_Click(object sender, EventArgs e)
        {
            RestorePreferences();
            Refresh();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            // First 2 lines not required, but give user reassuring feedback
            RestorePreferences();
            Refresh();
            Close();
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            SCTchecked.ChkConfirmOverwrite = ConfirmOverwriteCheckBox.Checked;
            InfoSection.UseFixesAsCoords = UseFixesAsCoordinatesCheckBox.Checked;
            InfoSection.OneFilePerSidStar = OneFilePerSIDSTARCheckBox.Checked;
            InfoSection.IncludeSidStarReferences = IncludeSIDSTARReferencesCheckBox.Checked;
            InfoSection.DrawFixLabelsOnDiagrams = DrawLabelsCheckBox.Checked;
            InfoSection.DrawFixSymbolsOnDiagrams = DrawSymbolsCheckBox.Checked;
            InfoSection.DrawAltRestrictsOnDiagrams = DrawAltitudeRestrictionsOnDiagramsCheckBox.Checked;
            InfoSection.DrawSpeedRestrictsOnDiagrams = DrawSpeedRestrictionsCheckBox.Checked;
            SCTchecked.ChkOneFile = CombineCheckBox.Checked;
            SCTchecked.IncludeSUAfile = IncludeSIDSTARReferencesCheckBox.Checked;
            Close();
        }

        private void RestorePreferences()
        {
            ConfirmOverwriteCheckBox.Checked = SCTchecked.ChkConfirmOverwrite;
            UseFixesAsCoordinatesCheckBox.Checked = InfoSection.UseFixesAsCoords;
            OneFilePerSIDSTARCheckBox.Checked = InfoSection.OneFilePerSidStar;
            IncludeSIDSTARReferencesCheckBox.Checked = InfoSection.IncludeSidStarReferences;
            DrawLabelsCheckBox.Checked = InfoSection.DrawFixLabelsOnDiagrams;
            DrawSymbolsCheckBox.Checked = InfoSection.DrawFixSymbolsOnDiagrams;
            DrawAltitudeRestrictionsOnDiagramsCheckBox.Checked = InfoSection.DrawAltRestrictsOnDiagrams;
            DrawSpeedRestrictionsCheckBox.Checked = InfoSection.DrawSpeedRestrictsOnDiagrams;
            CombineCheckBox.Checked = SCTchecked.ChkOneFile;
            IncludeSUACheckBox.Checked = SCTchecked.IncludeSUAfile;
        }
    }
}
