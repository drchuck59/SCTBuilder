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
            this.VRCCombineCheckBox = new System.Windows.Forms.CheckBox();
            this.IncludeSUACheckBox = new System.Windows.Forms.CheckBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.GeneralPrefTabPage = new System.Windows.Forms.TabPage();
            this.VRCprefTabPage = new System.Windows.Forms.TabPage();
            this.EuroScopePrefTabPage = new System.Windows.Forms.TabPage();
            this.ESCombineCheckBox = new System.Windows.Forms.CheckBox();
            this.SSDprefTabPage = new System.Windows.Forms.TabPage();
            this.RolloverLonCheckBox = new System.Windows.Forms.CheckBox();
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
            this.ConfirmOverwriteCheckBox.Location = new System.Drawing.Point(6, 16);
            this.ConfirmOverwriteCheckBox.Name = "ConfirmOverwriteCheckBox";
            this.ConfirmOverwriteCheckBox.Size = new System.Drawing.Size(192, 19);
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
            this.UseNaviGraphCheckBox.Location = new System.Drawing.Point(6, 40);
            this.UseNaviGraphCheckBox.Name = "UseNaviGraphCheckBox";
            this.UseNaviGraphCheckBox.Size = new System.Drawing.Size(189, 19);
            this.UseNaviGraphCheckBox.TabIndex = 1;
            this.UseNaviGraphCheckBox.Text = "Use NaviGraph data if present";
            this.toolTip1.SetToolTip(this.UseNaviGraphCheckBox, "If checked, Navigraph data will take priority over FAA text data");
            this.UseNaviGraphCheckBox.UseVisualStyleBackColor = true;
            // 
            // UseFixesAsCoordinatesCheckBox
            // 
            this.UseFixesAsCoordinatesCheckBox.AutoSize = true;
            this.UseFixesAsCoordinatesCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UseFixesAsCoordinatesCheckBox.Location = new System.Drawing.Point(6, 71);
            this.UseFixesAsCoordinatesCheckBox.Name = "UseFixesAsCoordinatesCheckBox";
            this.UseFixesAsCoordinatesCheckBox.Size = new System.Drawing.Size(261, 34);
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
            this.IncludeSIDSTARReferencesCheckBox.Location = new System.Drawing.Point(6, 35);
            this.IncludeSIDSTARReferencesCheckBox.Name = "IncludeSIDSTARReferencesCheckBox";
            this.IncludeSIDSTARReferencesCheckBox.Size = new System.Drawing.Size(266, 34);
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
            this.DrawSymbolsCheckBox.Location = new System.Drawing.Point(6, 107);
            this.DrawSymbolsCheckBox.Name = "DrawSymbolsCheckBox";
            this.DrawSymbolsCheckBox.Size = new System.Drawing.Size(165, 19);
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
            this.DrawLabelsCheckBox.Location = new System.Drawing.Point(6, 130);
            this.DrawLabelsCheckBox.Name = "DrawLabelsCheckBox";
            this.DrawLabelsCheckBox.Size = new System.Drawing.Size(168, 19);
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
            this.OneFilePerSIDSTARCheckBox.Location = new System.Drawing.Point(6, 12);
            this.OneFilePerSIDSTARCheckBox.Name = "OneFilePerSIDSTARCheckBox";
            this.OneFilePerSIDSTARCheckBox.Size = new System.Drawing.Size(213, 19);
            this.OneFilePerSIDSTARCheckBox.TabIndex = 7;
            this.OneFilePerSIDSTARCheckBox.Text = "Save each SID STAR in its own file";
            this.OneFilePerSIDSTARCheckBox.UseVisualStyleBackColor = true;
            // 
            // DrawAltitudeRestrictionsOnDiagramsCheckBox
            // 
            this.DrawAltitudeRestrictionsOnDiagramsCheckBox.AutoSize = true;
            this.DrawAltitudeRestrictionsOnDiagramsCheckBox.Enabled = false;
            this.DrawAltitudeRestrictionsOnDiagramsCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DrawAltitudeRestrictionsOnDiagramsCheckBox.Location = new System.Drawing.Point(27, 154);
            this.DrawAltitudeRestrictionsOnDiagramsCheckBox.Name = "DrawAltitudeRestrictionsOnDiagramsCheckBox";
            this.DrawAltitudeRestrictionsOnDiagramsCheckBox.Size = new System.Drawing.Size(244, 19);
            this.DrawAltitudeRestrictionsOnDiagramsCheckBox.TabIndex = 8;
            this.DrawAltitudeRestrictionsOnDiagramsCheckBox.Text = "Draw Altitude Restrictions next to Labels";
            this.DrawAltitudeRestrictionsOnDiagramsCheckBox.UseVisualStyleBackColor = true;
            // 
            // DrawSpeedRestrictionsCheckBox
            // 
            this.DrawSpeedRestrictionsCheckBox.AutoSize = true;
            this.DrawSpeedRestrictionsCheckBox.Enabled = false;
            this.DrawSpeedRestrictionsCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DrawSpeedRestrictionsCheckBox.Location = new System.Drawing.Point(27, 178);
            this.DrawSpeedRestrictionsCheckBox.Name = "DrawSpeedRestrictionsCheckBox";
            this.DrawSpeedRestrictionsCheckBox.Size = new System.Drawing.Size(240, 19);
            this.DrawSpeedRestrictionsCheckBox.TabIndex = 9;
            this.DrawSpeedRestrictionsCheckBox.Text = "Draw Speed Restrictions next to Labels";
            this.DrawSpeedRestrictionsCheckBox.UseVisualStyleBackColor = true;
            // 
            // OKButton
            // 
            this.OKButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OKButton.Location = new System.Drawing.Point(241, 277);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(123, 46);
            this.OKButton.TabIndex = 10;
            this.OKButton.Text = "Save Preferences\r\nand Close form";
            this.OKButton.UseVisualStyleBackColor = true;
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // LeaveWOchanges
            // 
            this.LeaveWOchanges.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.LeaveWOchanges.Location = new System.Drawing.Point(132, 277);
            this.LeaveWOchanges.Name = "LeaveWOchanges";
            this.LeaveWOchanges.Size = new System.Drawing.Size(103, 46);
            this.LeaveWOchanges.TabIndex = 11;
            this.LeaveWOchanges.Text = "Cancel (do not save changes)";
            this.LeaveWOchanges.UseVisualStyleBackColor = true;
            this.LeaveWOchanges.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // RestoreButton
            // 
            this.RestoreButton.Location = new System.Drawing.Point(51, 277);
            this.RestoreButton.Name = "RestoreButton";
            this.RestoreButton.Size = new System.Drawing.Size(75, 46);
            this.RestoreButton.TabIndex = 13;
            this.RestoreButton.Text = "Restore preferences";
            this.toolTip1.SetToolTip(this.RestoreButton, "Restores preferences to before changes were made");
            this.RestoreButton.UseVisualStyleBackColor = true;
            this.RestoreButton.Click += new System.EventHandler(this.RestoreButton_Click);
            // 
            // VRCCombineCheckBox
            // 
            this.VRCCombineCheckBox.AutoSize = true;
            this.VRCCombineCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.VRCCombineCheckBox.Location = new System.Drawing.Point(6, 17);
            this.VRCCombineCheckBox.Name = "VRCCombineCheckBox";
            this.VRCCombineCheckBox.Size = new System.Drawing.Size(282, 19);
            this.VRCCombineCheckBox.TabIndex = 14;
            this.VRCCombineCheckBox.Text = "Combine individual files into one VRC SCT2 file";
            this.VRCCombineCheckBox.UseVisualStyleBackColor = true;
            // 
            // IncludeSUACheckBox
            // 
            this.IncludeSUACheckBox.AutoSize = true;
            this.IncludeSUACheckBox.Enabled = false;
            this.IncludeSUACheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IncludeSUACheckBox.Location = new System.Drawing.Point(6, 87);
            this.IncludeSUACheckBox.Name = "IncludeSUACheckBox";
            this.IncludeSUACheckBox.Size = new System.Drawing.Size(230, 19);
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
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(356, 259);
            this.tabControl1.TabIndex = 16;
            // 
            // GeneralPrefTabPage
            // 
            this.GeneralPrefTabPage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.GeneralPrefTabPage.Controls.Add(this.RolloverLonCheckBox);
            this.GeneralPrefTabPage.Controls.Add(this.UseNaviGraphCheckBox);
            this.GeneralPrefTabPage.Controls.Add(this.IncludeSUACheckBox);
            this.GeneralPrefTabPage.Controls.Add(this.ConfirmOverwriteCheckBox);
            this.GeneralPrefTabPage.Location = new System.Drawing.Point(4, 22);
            this.GeneralPrefTabPage.Name = "GeneralPrefTabPage";
            this.GeneralPrefTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.GeneralPrefTabPage.Size = new System.Drawing.Size(348, 233);
            this.GeneralPrefTabPage.TabIndex = 0;
            this.GeneralPrefTabPage.Text = "General";
            // 
            // VRCprefTabPage
            // 
            this.VRCprefTabPage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.VRCprefTabPage.Controls.Add(this.VRCCombineCheckBox);
            this.VRCprefTabPage.Location = new System.Drawing.Point(4, 22);
            this.VRCprefTabPage.Name = "VRCprefTabPage";
            this.VRCprefTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.VRCprefTabPage.Size = new System.Drawing.Size(348, 233);
            this.VRCprefTabPage.TabIndex = 1;
            this.VRCprefTabPage.Text = "VRC files";
            // 
            // EuroScopePrefTabPage
            // 
            this.EuroScopePrefTabPage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.EuroScopePrefTabPage.Controls.Add(this.ESCombineCheckBox);
            this.EuroScopePrefTabPage.Location = new System.Drawing.Point(4, 22);
            this.EuroScopePrefTabPage.Name = "EuroScopePrefTabPage";
            this.EuroScopePrefTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.EuroScopePrefTabPage.Size = new System.Drawing.Size(348, 233);
            this.EuroScopePrefTabPage.TabIndex = 2;
            this.EuroScopePrefTabPage.Text = "Euroscope files";
            // 
            // ESCombineCheckBox
            // 
            this.ESCombineCheckBox.AutoSize = true;
            this.ESCombineCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ESCombineCheckBox.Location = new System.Drawing.Point(6, 15);
            this.ESCombineCheckBox.Name = "ESCombineCheckBox";
            this.ESCombineCheckBox.Size = new System.Drawing.Size(274, 19);
            this.ESCombineCheckBox.TabIndex = 15;
            this.ESCombineCheckBox.Text = "Combine individual files into one ES SCT2 file";
            this.ESCombineCheckBox.UseVisualStyleBackColor = true;
            // 
            // SSDprefTabPage
            // 
            this.SSDprefTabPage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.SSDprefTabPage.Controls.Add(this.DrawSymbolsCheckBox);
            this.SSDprefTabPage.Controls.Add(this.UseFixesAsCoordinatesCheckBox);
            this.SSDprefTabPage.Controls.Add(this.IncludeSIDSTARReferencesCheckBox);
            this.SSDprefTabPage.Controls.Add(this.DrawLabelsCheckBox);
            this.SSDprefTabPage.Controls.Add(this.OneFilePerSIDSTARCheckBox);
            this.SSDprefTabPage.Controls.Add(this.DrawSpeedRestrictionsCheckBox);
            this.SSDprefTabPage.Controls.Add(this.DrawAltitudeRestrictionsOnDiagramsCheckBox);
            this.SSDprefTabPage.Location = new System.Drawing.Point(4, 22);
            this.SSDprefTabPage.Name = "SSDprefTabPage";
            this.SSDprefTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.SSDprefTabPage.Size = new System.Drawing.Size(348, 233);
            this.SSDprefTabPage.TabIndex = 3;
            this.SSDprefTabPage.Text = "SID/STAR Diagrams";
            // 
            // RolloverLonCheckBox
            // 
            this.RolloverLonCheckBox.AutoSize = true;
            this.RolloverLonCheckBox.Checked = true;
            this.RolloverLonCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.RolloverLonCheckBox.Enabled = false;
            this.RolloverLonCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RolloverLonCheckBox.Location = new System.Drawing.Point(6, 62);
            this.RolloverLonCheckBox.Name = "RolloverLonCheckBox";
            this.RolloverLonCheckBox.Size = new System.Drawing.Size(200, 19);
            this.RolloverLonCheckBox.TabIndex = 16;
            this.RolloverLonCheckBox.Text = "Allow E/W Longitudes to rollover";
            this.toolTip1.SetToolTip(this.RolloverLonCheckBox, "If a longitude exceeds crosses from \r\nW to E or vice versa, the longitude is \r\nco" +
        "nverted to to allow correct on VRC.\r\nClear this box to BLOCK stop rollovers.\r\nNO" +
        "TE: Unintended results may occur.\r\n");
            this.RolloverLonCheckBox.UseVisualStyleBackColor = true;
            // 
            // Preferences
            // 
            this.AcceptButton = this.OKButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.CancelButton = this.LeaveWOchanges;
            this.ClientSize = new System.Drawing.Size(406, 333);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.RestoreButton);
            this.Controls.Add(this.LeaveWOchanges);
            this.Controls.Add(this.OKButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
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
    }
}