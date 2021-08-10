namespace SCTBuilder
{
    partial class Preferences
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Preferences));
            this.ConfirmOverwriteCheckBox = new System.Windows.Forms.CheckBox();
            this.UseNaviGraphCheckBox = new System.Windows.Forms.CheckBox();
            this.UseFixesAsCoordinatesCheckBox = new System.Windows.Forms.CheckBox();
            this.IncludeSIDSTARReferencesCheckBox = new System.Windows.Forms.CheckBox();
            this.DrawSymbolsCheckBox = new System.Windows.Forms.CheckBox();
            this.DrawLabelsCheckBox = new System.Windows.Forms.CheckBox();
            this.OneFilePerSIDSTARCheckBox = new System.Windows.Forms.CheckBox();
            this.DrawAltitudeRestrictionsOnDiagramsCheckBox = new System.Windows.Forms.CheckBox();
            this.DrawSpeedRestrictionsCheckBox = new System.Windows.Forms.CheckBox();
            this.OKButton = new System.Windows.Forms.Button();
            this.LeaveWOchanges = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.RestoreButton = new System.Windows.Forms.Button();
            this.RolloverLonCheckBox = new System.Windows.Forms.CheckBox();
            this.VRCCombineCheckBox = new System.Windows.Forms.CheckBox();
            this.IncludeSUACheckBox = new System.Windows.Forms.CheckBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.GeneralPrefTabPage = new System.Windows.Forms.TabPage();
            this.VRCprefTabPage = new System.Windows.Forms.TabPage();
            this.EuroScopePrefTabPage = new System.Windows.Forms.TabPage();
            this.ESCombineCheckBox = new System.Windows.Forms.CheckBox();
            this.SSDprefTabPage = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.SIDPrefixTextBox = new System.Windows.Forms.TextBox();
            this.STARPrefixTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.GeneralPrefTabPage.SuspendLayout();
            this.VRCprefTabPage.SuspendLayout();
            this.EuroScopePrefTabPage.SuspendLayout();
            this.SSDprefTabPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // ConfirmOverwriteCheckBox
            // 
            this.ConfirmOverwriteCheckBox.AutoSize = true;
            this.ConfirmOverwriteCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ConfirmOverwriteCheckBox.Location = new System.Drawing.Point(8, 20);
            this.ConfirmOverwriteCheckBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ConfirmOverwriteCheckBox.Name = "ConfirmOverwriteCheckBox";
            this.ConfirmOverwriteCheckBox.Size = new System.Drawing.Size(233, 22);
            this.ConfirmOverwriteCheckBox.TabIndex = 0;
            this.ConfirmOverwriteCheckBox.Text = "Ask before overwriting text files";
            this.ConfirmOverwriteCheckBox.UseVisualStyleBackColor = true;
            // 
            // UseNaviGraphCheckBox
            // 
            this.UseNaviGraphCheckBox.AutoSize = true;
            this.UseNaviGraphCheckBox.Checked = true;
            this.UseNaviGraphCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.UseNaviGraphCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UseNaviGraphCheckBox.Location = new System.Drawing.Point(8, 49);
            this.UseNaviGraphCheckBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.UseNaviGraphCheckBox.Name = "UseNaviGraphCheckBox";
            this.UseNaviGraphCheckBox.Size = new System.Drawing.Size(227, 22);
            this.UseNaviGraphCheckBox.TabIndex = 1;
            this.UseNaviGraphCheckBox.Text = "Use NaviGraph data if present";
            this.toolTip1.SetToolTip(this.UseNaviGraphCheckBox, "If checked, Navigraph data will take priority over FAA text data");
            this.UseNaviGraphCheckBox.UseVisualStyleBackColor = true;
            // 
            // UseFixesAsCoordinatesCheckBox
            // 
            this.UseFixesAsCoordinatesCheckBox.AutoSize = true;
            this.UseFixesAsCoordinatesCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UseFixesAsCoordinatesCheckBox.Location = new System.Drawing.Point(8, 87);
            this.UseFixesAsCoordinatesCheckBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.UseFixesAsCoordinatesCheckBox.Name = "UseFixesAsCoordinatesCheckBox";
            this.UseFixesAsCoordinatesCheckBox.Size = new System.Drawing.Size(315, 40);
            this.UseFixesAsCoordinatesCheckBox.TabIndex = 3;
            this.UseFixesAsCoordinatesCheckBox.Text = "Use FIX labels as coordinates\r\n(Requires output of references be checked)";
            this.UseFixesAsCoordinatesCheckBox.UseVisualStyleBackColor = true;
            this.UseFixesAsCoordinatesCheckBox.CheckedChanged += new System.EventHandler(this.UseFixesAsCoordinatesCheckBox_CheckedChanged);
            // 
            // IncludeSIDSTARReferencesCheckBox
            // 
            this.IncludeSIDSTARReferencesCheckBox.AutoSize = true;
            this.IncludeSIDSTARReferencesCheckBox.Checked = true;
            this.IncludeSIDSTARReferencesCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.IncludeSIDSTARReferencesCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IncludeSIDSTARReferencesCheckBox.Location = new System.Drawing.Point(8, 43);
            this.IncludeSIDSTARReferencesCheckBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.IncludeSIDSTARReferencesCheckBox.Name = "IncludeSIDSTARReferencesCheckBox";
            this.IncludeSIDSTARReferencesCheckBox.Size = new System.Drawing.Size(327, 40);
            this.IncludeSIDSTARReferencesCheckBox.TabIndex = 4;
            this.IncludeSIDSTARReferencesCheckBox.Text = "Include references ([AIRPORTS], [VOR], etc.)\r\nin the SID or STAR output file";
            this.IncludeSIDSTARReferencesCheckBox.UseVisualStyleBackColor = true;
            // 
            // DrawSymbolsCheckBox
            // 
            this.DrawSymbolsCheckBox.AutoSize = true;
            this.DrawSymbolsCheckBox.Checked = true;
            this.DrawSymbolsCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.DrawSymbolsCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DrawSymbolsCheckBox.Location = new System.Drawing.Point(8, 132);
            this.DrawSymbolsCheckBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.DrawSymbolsCheckBox.Name = "DrawSymbolsCheckBox";
            this.DrawSymbolsCheckBox.Size = new System.Drawing.Size(202, 22);
            this.DrawSymbolsCheckBox.TabIndex = 5;
            this.DrawSymbolsCheckBox.Text = "Draw Symbols over FIXes";
            this.DrawSymbolsCheckBox.UseVisualStyleBackColor = true;
            // 
            // DrawLabelsCheckBox
            // 
            this.DrawLabelsCheckBox.AutoSize = true;
            this.DrawLabelsCheckBox.Checked = true;
            this.DrawLabelsCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.DrawLabelsCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DrawLabelsCheckBox.Location = new System.Drawing.Point(8, 160);
            this.DrawLabelsCheckBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.DrawLabelsCheckBox.Name = "DrawLabelsCheckBox";
            this.DrawLabelsCheckBox.Size = new System.Drawing.Size(202, 22);
            this.DrawLabelsCheckBox.TabIndex = 6;
            this.DrawLabelsCheckBox.Text = "Draw Labels next to FIXes";
            this.DrawLabelsCheckBox.UseVisualStyleBackColor = true;
            // 
            // OneFilePerSIDSTARCheckBox
            // 
            this.OneFilePerSIDSTARCheckBox.AutoSize = true;
            this.OneFilePerSIDSTARCheckBox.Checked = true;
            this.OneFilePerSIDSTARCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.OneFilePerSIDSTARCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OneFilePerSIDSTARCheckBox.Location = new System.Drawing.Point(8, 15);
            this.OneFilePerSIDSTARCheckBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.OneFilePerSIDSTARCheckBox.Name = "OneFilePerSIDSTARCheckBox";
            this.OneFilePerSIDSTARCheckBox.Size = new System.Drawing.Size(258, 22);
            this.OneFilePerSIDSTARCheckBox.TabIndex = 7;
            this.OneFilePerSIDSTARCheckBox.Text = "Save each SID STAR in its own file";
            this.OneFilePerSIDSTARCheckBox.UseVisualStyleBackColor = true;
            // 
            // DrawAltitudeRestrictionsOnDiagramsCheckBox
            // 
            this.DrawAltitudeRestrictionsOnDiagramsCheckBox.AutoSize = true;
            this.DrawAltitudeRestrictionsOnDiagramsCheckBox.Enabled = false;
            this.DrawAltitudeRestrictionsOnDiagramsCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DrawAltitudeRestrictionsOnDiagramsCheckBox.Location = new System.Drawing.Point(36, 190);
            this.DrawAltitudeRestrictionsOnDiagramsCheckBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.DrawAltitudeRestrictionsOnDiagramsCheckBox.Name = "DrawAltitudeRestrictionsOnDiagramsCheckBox";
            this.DrawAltitudeRestrictionsOnDiagramsCheckBox.Size = new System.Drawing.Size(294, 22);
            this.DrawAltitudeRestrictionsOnDiagramsCheckBox.TabIndex = 8;
            this.DrawAltitudeRestrictionsOnDiagramsCheckBox.Text = "Draw Altitude Restrictions next to Labels";
            this.DrawAltitudeRestrictionsOnDiagramsCheckBox.UseVisualStyleBackColor = true;
            // 
            // DrawSpeedRestrictionsCheckBox
            // 
            this.DrawSpeedRestrictionsCheckBox.AutoSize = true;
            this.DrawSpeedRestrictionsCheckBox.Enabled = false;
            this.DrawSpeedRestrictionsCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DrawSpeedRestrictionsCheckBox.Location = new System.Drawing.Point(36, 219);
            this.DrawSpeedRestrictionsCheckBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.DrawSpeedRestrictionsCheckBox.Name = "DrawSpeedRestrictionsCheckBox";
            this.DrawSpeedRestrictionsCheckBox.Size = new System.Drawing.Size(289, 22);
            this.DrawSpeedRestrictionsCheckBox.TabIndex = 9;
            this.DrawSpeedRestrictionsCheckBox.Text = "Draw Speed Restrictions next to Labels";
            this.DrawSpeedRestrictionsCheckBox.UseVisualStyleBackColor = true;
            // 
            // OKButton
            // 
            this.OKButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OKButton.Location = new System.Drawing.Point(321, 341);
            this.OKButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(164, 57);
            this.OKButton.TabIndex = 10;
            this.OKButton.Text = "Save Preferences\r\nand Close form";
            this.OKButton.UseVisualStyleBackColor = true;
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // LeaveWOchanges
            // 
            this.LeaveWOchanges.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.LeaveWOchanges.Location = new System.Drawing.Point(176, 341);
            this.LeaveWOchanges.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.LeaveWOchanges.Name = "LeaveWOchanges";
            this.LeaveWOchanges.Size = new System.Drawing.Size(137, 57);
            this.LeaveWOchanges.TabIndex = 11;
            this.LeaveWOchanges.Text = "Cancel (do not save changes)";
            this.LeaveWOchanges.UseVisualStyleBackColor = true;
            this.LeaveWOchanges.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // RestoreButton
            // 
            this.RestoreButton.Location = new System.Drawing.Point(68, 341);
            this.RestoreButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.RestoreButton.Name = "RestoreButton";
            this.RestoreButton.Size = new System.Drawing.Size(100, 57);
            this.RestoreButton.TabIndex = 13;
            this.RestoreButton.Text = "Restore preferences";
            this.toolTip1.SetToolTip(this.RestoreButton, "Restores preferences to before changes were made");
            this.RestoreButton.UseVisualStyleBackColor = true;
            this.RestoreButton.Click += new System.EventHandler(this.RestoreButton_Click);
            // 
            // RolloverLonCheckBox
            // 
            this.RolloverLonCheckBox.AutoSize = true;
            this.RolloverLonCheckBox.Checked = true;
            this.RolloverLonCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.RolloverLonCheckBox.Enabled = false;
            this.RolloverLonCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RolloverLonCheckBox.Location = new System.Drawing.Point(8, 76);
            this.RolloverLonCheckBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.RolloverLonCheckBox.Name = "RolloverLonCheckBox";
            this.RolloverLonCheckBox.Size = new System.Drawing.Size(244, 22);
            this.RolloverLonCheckBox.TabIndex = 16;
            this.RolloverLonCheckBox.Text = "Allow E/W Longitudes to rollover";
            this.toolTip1.SetToolTip(this.RolloverLonCheckBox, "If a longitude exceeds crosses from \r\nW to E or vice versa, the longitude is \r\nco" +
        "nverted to to allow correct on VRC.\r\nClear this box to BLOCK stop rollovers.\r\nNO" +
        "TE: Unintended results may occur.\r\n");
            this.RolloverLonCheckBox.UseVisualStyleBackColor = true;
            // 
            // VRCCombineCheckBox
            // 
            this.VRCCombineCheckBox.AutoSize = true;
            this.VRCCombineCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.VRCCombineCheckBox.Location = new System.Drawing.Point(8, 21);
            this.VRCCombineCheckBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.VRCCombineCheckBox.Name = "VRCCombineCheckBox";
            this.VRCCombineCheckBox.Size = new System.Drawing.Size(339, 22);
            this.VRCCombineCheckBox.TabIndex = 14;
            this.VRCCombineCheckBox.Text = "Combine individual files into one VRC SCT2 file";
            this.VRCCombineCheckBox.UseVisualStyleBackColor = true;
            // 
            // IncludeSUACheckBox
            // 
            this.IncludeSUACheckBox.AutoSize = true;
            this.IncludeSUACheckBox.Enabled = false;
            this.IncludeSUACheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IncludeSUACheckBox.Location = new System.Drawing.Point(8, 107);
            this.IncludeSUACheckBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.IncludeSUACheckBox.Name = "IncludeSUACheckBox";
            this.IncludeSUACheckBox.Size = new System.Drawing.Size(274, 22);
            this.IncludeSUACheckBox.TabIndex = 15;
            this.IncludeSUACheckBox.Text = "Include SUA file (See documentation)";
            this.IncludeSUACheckBox.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.GeneralPrefTabPage);
            this.tabControl1.Controls.Add(this.VRCprefTabPage);
            this.tabControl1.Controls.Add(this.EuroScopePrefTabPage);
            this.tabControl1.Controls.Add(this.SSDprefTabPage);
            this.tabControl1.Location = new System.Drawing.Point(16, 15);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(475, 319);
            this.tabControl1.TabIndex = 16;
            // 
            // GeneralPrefTabPage
            // 
            this.GeneralPrefTabPage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.GeneralPrefTabPage.Controls.Add(this.RolloverLonCheckBox);
            this.GeneralPrefTabPage.Controls.Add(this.UseNaviGraphCheckBox);
            this.GeneralPrefTabPage.Controls.Add(this.IncludeSUACheckBox);
            this.GeneralPrefTabPage.Controls.Add(this.ConfirmOverwriteCheckBox);
            this.GeneralPrefTabPage.Location = new System.Drawing.Point(4, 25);
            this.GeneralPrefTabPage.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.GeneralPrefTabPage.Name = "GeneralPrefTabPage";
            this.GeneralPrefTabPage.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.GeneralPrefTabPage.Size = new System.Drawing.Size(467, 290);
            this.GeneralPrefTabPage.TabIndex = 0;
            this.GeneralPrefTabPage.Text = "General";
            // 
            // VRCprefTabPage
            // 
            this.VRCprefTabPage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.VRCprefTabPage.Controls.Add(this.VRCCombineCheckBox);
            this.VRCprefTabPage.Location = new System.Drawing.Point(4, 25);
            this.VRCprefTabPage.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.VRCprefTabPage.Name = "VRCprefTabPage";
            this.VRCprefTabPage.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.VRCprefTabPage.Size = new System.Drawing.Size(467, 290);
            this.VRCprefTabPage.TabIndex = 1;
            this.VRCprefTabPage.Text = "VRC files";
            // 
            // EuroScopePrefTabPage
            // 
            this.EuroScopePrefTabPage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.EuroScopePrefTabPage.Controls.Add(this.ESCombineCheckBox);
            this.EuroScopePrefTabPage.Location = new System.Drawing.Point(4, 25);
            this.EuroScopePrefTabPage.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.EuroScopePrefTabPage.Name = "EuroScopePrefTabPage";
            this.EuroScopePrefTabPage.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.EuroScopePrefTabPage.Size = new System.Drawing.Size(467, 290);
            this.EuroScopePrefTabPage.TabIndex = 2;
            this.EuroScopePrefTabPage.Text = "Euroscope files";
            // 
            // ESCombineCheckBox
            // 
            this.ESCombineCheckBox.AutoSize = true;
            this.ESCombineCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ESCombineCheckBox.Location = new System.Drawing.Point(8, 18);
            this.ESCombineCheckBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ESCombineCheckBox.Name = "ESCombineCheckBox";
            this.ESCombineCheckBox.Size = new System.Drawing.Size(328, 22);
            this.ESCombineCheckBox.TabIndex = 15;
            this.ESCombineCheckBox.Text = "Combine individual files into one ES SCT2 file";
            this.ESCombineCheckBox.UseVisualStyleBackColor = true;
            // 
            // SSDprefTabPage
            // 
            this.SSDprefTabPage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.SSDprefTabPage.Controls.Add(this.STARPrefixTextBox);
            this.SSDprefTabPage.Controls.Add(this.label2);
            this.SSDprefTabPage.Controls.Add(this.SIDPrefixTextBox);
            this.SSDprefTabPage.Controls.Add(this.label1);
            this.SSDprefTabPage.Controls.Add(this.DrawSymbolsCheckBox);
            this.SSDprefTabPage.Controls.Add(this.UseFixesAsCoordinatesCheckBox);
            this.SSDprefTabPage.Controls.Add(this.IncludeSIDSTARReferencesCheckBox);
            this.SSDprefTabPage.Controls.Add(this.DrawLabelsCheckBox);
            this.SSDprefTabPage.Controls.Add(this.OneFilePerSIDSTARCheckBox);
            this.SSDprefTabPage.Controls.Add(this.DrawSpeedRestrictionsCheckBox);
            this.SSDprefTabPage.Controls.Add(this.DrawAltitudeRestrictionsOnDiagramsCheckBox);
            this.SSDprefTabPage.Location = new System.Drawing.Point(4, 25);
            this.SSDprefTabPage.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.SSDprefTabPage.Name = "SSDprefTabPage";
            this.SSDprefTabPage.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.SSDprefTabPage.Size = new System.Drawing.Size(467, 290);
            this.SSDprefTabPage.TabIndex = 3;
            this.SSDprefTabPage.Text = "SID/STAR Diagrams";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 252);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 17);
            this.label1.TabIndex = 10;
            this.label1.Text = "SID Prefix:";
            // 
            // SIDPrefixTextBox
            // 
            this.SIDPrefixTextBox.Location = new System.Drawing.Point(83, 249);
            this.SIDPrefixTextBox.MaxLength = 1;
            this.SIDPrefixTextBox.Name = "SIDPrefixTextBox";
            this.SIDPrefixTextBox.Size = new System.Drawing.Size(21, 22);
            this.SIDPrefixTextBox.TabIndex = 11;
            this.SIDPrefixTextBox.Text = "-";
            this.SIDPrefixTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.SIDPrefixTextBox_KeyPress);
            // 
            // STARPrefixTextBox
            // 
            this.STARPrefixTextBox.Location = new System.Drawing.Point(208, 249);
            this.STARPrefixTextBox.MaxLength = 1;
            this.STARPrefixTextBox.Name = "STARPrefixTextBox";
            this.STARPrefixTextBox.Size = new System.Drawing.Size(21, 22);
            this.STARPrefixTextBox.TabIndex = 13;
            this.STARPrefixTextBox.Text = "+";
            this.STARPrefixTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.STARPrefixTextBox_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(120, 252);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 17);
            this.label2.TabIndex = 12;
            this.label2.Text = "STAR Prefix:";
            // 
            // Preferences
            // 
            this.AcceptButton = this.OKButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.CancelButton = this.LeaveWOchanges;
            this.ClientSize = new System.Drawing.Size(541, 410);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.RestoreButton);
            this.Controls.Add(this.LeaveWOchanges);
            this.Controls.Add(this.OKButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Preferences";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Preferences";
            this.Load += new System.EventHandler(this.Preferences_Load);
            this.tabControl1.ResumeLayout(false);
            this.GeneralPrefTabPage.ResumeLayout(false);
            this.GeneralPrefTabPage.PerformLayout();
            this.VRCprefTabPage.ResumeLayout(false);
            this.VRCprefTabPage.PerformLayout();
            this.EuroScopePrefTabPage.ResumeLayout(false);
            this.EuroScopePrefTabPage.PerformLayout();
            this.SSDprefTabPage.ResumeLayout(false);
            this.SSDprefTabPage.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox ConfirmOverwriteCheckBox;
        private System.Windows.Forms.CheckBox UseNaviGraphCheckBox;
        private System.Windows.Forms.CheckBox UseFixesAsCoordinatesCheckBox;
        private System.Windows.Forms.CheckBox IncludeSIDSTARReferencesCheckBox;
        private System.Windows.Forms.CheckBox DrawSymbolsCheckBox;
        private System.Windows.Forms.CheckBox DrawLabelsCheckBox;
        private System.Windows.Forms.CheckBox OneFilePerSIDSTARCheckBox;
        private System.Windows.Forms.CheckBox DrawAltitudeRestrictionsOnDiagramsCheckBox;
        private System.Windows.Forms.CheckBox DrawSpeedRestrictionsCheckBox;
        private System.Windows.Forms.Button OKButton;
        private System.Windows.Forms.Button LeaveWOchanges;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button RestoreButton;
        private System.Windows.Forms.CheckBox VRCCombineCheckBox;
        private System.Windows.Forms.CheckBox IncludeSUACheckBox;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage GeneralPrefTabPage;
        private System.Windows.Forms.TabPage VRCprefTabPage;
        private System.Windows.Forms.TabPage EuroScopePrefTabPage;
        private System.Windows.Forms.TabPage SSDprefTabPage;
        private System.Windows.Forms.CheckBox ESCombineCheckBox;
        private System.Windows.Forms.CheckBox RolloverLonCheckBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox STARPrefixTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox SIDPrefixTextBox;
    }
}