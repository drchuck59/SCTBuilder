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
            this.label1 = new System.Windows.Forms.Label();
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
            this.CombineCheckBox = new System.Windows.Forms.CheckBox();
            this.IncludeSUACheckBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // ConfirmOverwriteCheckBox
            // 
            this.ConfirmOverwriteCheckBox.AutoSize = true;
            this.ConfirmOverwriteCheckBox.Location = new System.Drawing.Point(24, 39);
            this.ConfirmOverwriteCheckBox.Name = "ConfirmOverwriteCheckBox";
            this.ConfirmOverwriteCheckBox.Size = new System.Drawing.Size(172, 17);
            this.ConfirmOverwriteCheckBox.TabIndex = 0;
            this.ConfirmOverwriteCheckBox.Text = "Ask before overwriting text files";
            this.ConfirmOverwriteCheckBox.UseVisualStyleBackColor = true;
            // 
            // UseNaviGraphCheckBox
            // 
            this.UseNaviGraphCheckBox.AutoSize = true;
            this.UseNaviGraphCheckBox.Checked = true;
            this.UseNaviGraphCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.UseNaviGraphCheckBox.Location = new System.Drawing.Point(24, 63);
            this.UseNaviGraphCheckBox.Name = "UseNaviGraphCheckBox";
            this.UseNaviGraphCheckBox.Size = new System.Drawing.Size(169, 17);
            this.UseNaviGraphCheckBox.TabIndex = 1;
            this.UseNaviGraphCheckBox.Text = "Use NaviGraph data if present";
            this.UseNaviGraphCheckBox.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(267, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(181, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "SID STAR diagram preferences ";
            // 
            // UseFixesAsCoordinatesCheckBox
            // 
            this.UseFixesAsCoordinatesCheckBox.AutoSize = true;
            this.UseFixesAsCoordinatesCheckBox.Location = new System.Drawing.Point(270, 98);
            this.UseFixesAsCoordinatesCheckBox.Name = "UseFixesAsCoordinatesCheckBox";
            this.UseFixesAsCoordinatesCheckBox.Size = new System.Drawing.Size(232, 30);
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
            this.IncludeSIDSTARReferencesCheckBox.Location = new System.Drawing.Point(270, 62);
            this.IncludeSIDSTARReferencesCheckBox.Name = "IncludeSIDSTARReferencesCheckBox";
            this.IncludeSIDSTARReferencesCheckBox.Size = new System.Drawing.Size(243, 30);
            this.IncludeSIDSTARReferencesCheckBox.TabIndex = 4;
            this.IncludeSIDSTARReferencesCheckBox.Text = "Include references ([AIRPORTS], [VOR], etc.)\r\nin the SID or STAR output file";
            this.IncludeSIDSTARReferencesCheckBox.UseVisualStyleBackColor = true;
            // 
            // DrawSymbolsCheckBox
            // 
            this.DrawSymbolsCheckBox.AutoSize = true;
            this.DrawSymbolsCheckBox.Checked = true;
            this.DrawSymbolsCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.DrawSymbolsCheckBox.Location = new System.Drawing.Point(270, 134);
            this.DrawSymbolsCheckBox.Name = "DrawSymbolsCheckBox";
            this.DrawSymbolsCheckBox.Size = new System.Drawing.Size(147, 17);
            this.DrawSymbolsCheckBox.TabIndex = 5;
            this.DrawSymbolsCheckBox.Text = "Draw Symbols over FIXes";
            this.DrawSymbolsCheckBox.UseVisualStyleBackColor = true;
            // 
            // DrawLabelsCheckBox
            // 
            this.DrawLabelsCheckBox.AutoSize = true;
            this.DrawLabelsCheckBox.Checked = true;
            this.DrawLabelsCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.DrawLabelsCheckBox.Location = new System.Drawing.Point(270, 157);
            this.DrawLabelsCheckBox.Name = "DrawLabelsCheckBox";
            this.DrawLabelsCheckBox.Size = new System.Drawing.Size(150, 17);
            this.DrawLabelsCheckBox.TabIndex = 6;
            this.DrawLabelsCheckBox.Text = "Draw Labels next to FIXes";
            this.DrawLabelsCheckBox.UseVisualStyleBackColor = true;
            // 
            // OneFilePerSIDSTARCheckBox
            // 
            this.OneFilePerSIDSTARCheckBox.AutoSize = true;
            this.OneFilePerSIDSTARCheckBox.Checked = true;
            this.OneFilePerSIDSTARCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.OneFilePerSIDSTARCheckBox.Location = new System.Drawing.Point(270, 39);
            this.OneFilePerSIDSTARCheckBox.Name = "OneFilePerSIDSTARCheckBox";
            this.OneFilePerSIDSTARCheckBox.Size = new System.Drawing.Size(194, 17);
            this.OneFilePerSIDSTARCheckBox.TabIndex = 7;
            this.OneFilePerSIDSTARCheckBox.Text = "Save each SID STAR in its own file";
            this.OneFilePerSIDSTARCheckBox.UseVisualStyleBackColor = true;
            // 
            // DrawAltitudeRestrictionsOnDiagramsCheckBox
            // 
            this.DrawAltitudeRestrictionsOnDiagramsCheckBox.AutoSize = true;
            this.DrawAltitudeRestrictionsOnDiagramsCheckBox.Enabled = false;
            this.DrawAltitudeRestrictionsOnDiagramsCheckBox.Location = new System.Drawing.Point(291, 181);
            this.DrawAltitudeRestrictionsOnDiagramsCheckBox.Name = "DrawAltitudeRestrictionsOnDiagramsCheckBox";
            this.DrawAltitudeRestrictionsOnDiagramsCheckBox.Size = new System.Drawing.Size(216, 17);
            this.DrawAltitudeRestrictionsOnDiagramsCheckBox.TabIndex = 8;
            this.DrawAltitudeRestrictionsOnDiagramsCheckBox.Text = "Draw Altitude Restrictions next to Labels";
            this.DrawAltitudeRestrictionsOnDiagramsCheckBox.UseVisualStyleBackColor = true;
            // 
            // DrawSpeedRestrictionsCheckBox
            // 
            this.DrawSpeedRestrictionsCheckBox.AutoSize = true;
            this.DrawSpeedRestrictionsCheckBox.Enabled = false;
            this.DrawSpeedRestrictionsCheckBox.Location = new System.Drawing.Point(291, 205);
            this.DrawSpeedRestrictionsCheckBox.Name = "DrawSpeedRestrictionsCheckBox";
            this.DrawSpeedRestrictionsCheckBox.Size = new System.Drawing.Size(212, 17);
            this.DrawSpeedRestrictionsCheckBox.TabIndex = 9;
            this.DrawSpeedRestrictionsCheckBox.Text = "Draw Speed Restrictions next to Labels";
            this.DrawSpeedRestrictionsCheckBox.UseVisualStyleBackColor = true;
            // 
            // OKButton
            // 
            this.OKButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OKButton.Location = new System.Drawing.Point(390, 244);
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
            this.LeaveWOchanges.Location = new System.Drawing.Point(281, 244);
            this.LeaveWOchanges.Name = "LeaveWOchanges";
            this.LeaveWOchanges.Size = new System.Drawing.Size(103, 46);
            this.LeaveWOchanges.TabIndex = 11;
            this.LeaveWOchanges.Text = "Cancel (do not save changes)";
            this.LeaveWOchanges.UseVisualStyleBackColor = true;
            this.LeaveWOchanges.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // RestoreButton
            // 
            this.RestoreButton.Location = new System.Drawing.Point(200, 244);
            this.RestoreButton.Name = "RestoreButton";
            this.RestoreButton.Size = new System.Drawing.Size(75, 46);
            this.RestoreButton.TabIndex = 13;
            this.RestoreButton.Text = "Restore preferences";
            this.toolTip1.SetToolTip(this.RestoreButton, "Restores preferences to before changes were made");
            this.RestoreButton.UseVisualStyleBackColor = true;
            this.RestoreButton.Click += new System.EventHandler(this.RestoreButton_Click);
            // 
            // CombineCheckBox
            // 
            this.CombineCheckBox.AutoSize = true;
            this.CombineCheckBox.Location = new System.Drawing.Point(24, 86);
            this.CombineCheckBox.Name = "CombineCheckBox";
            this.CombineCheckBox.Size = new System.Drawing.Size(135, 30);
            this.CombineCheckBox.TabIndex = 14;
            this.CombineCheckBox.Text = "Combine individual files\r\ninto one SCT2 file";
            this.CombineCheckBox.UseVisualStyleBackColor = true;
            // 
            // IncludeSUACheckBox
            // 
            this.IncludeSUACheckBox.AutoSize = true;
            this.IncludeSUACheckBox.Enabled = false;
            this.IncludeSUACheckBox.Location = new System.Drawing.Point(24, 134);
            this.IncludeSUACheckBox.Name = "IncludeSUACheckBox";
            this.IncludeSUACheckBox.Size = new System.Drawing.Size(124, 30);
            this.IncludeSUACheckBox.TabIndex = 15;
            this.IncludeSUACheckBox.Text = "Include SUA file\r\n(See documentation)";
            this.IncludeSUACheckBox.UseVisualStyleBackColor = true;
            // 
            // Preferences
            // 
            this.AcceptButton = this.OKButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.CancelButton = this.LeaveWOchanges;
            this.ClientSize = new System.Drawing.Size(535, 302);
            this.Controls.Add(this.IncludeSUACheckBox);
            this.Controls.Add(this.CombineCheckBox);
            this.Controls.Add(this.RestoreButton);
            this.Controls.Add(this.LeaveWOchanges);
            this.Controls.Add(this.OKButton);
            this.Controls.Add(this.DrawSpeedRestrictionsCheckBox);
            this.Controls.Add(this.DrawAltitudeRestrictionsOnDiagramsCheckBox);
            this.Controls.Add(this.OneFilePerSIDSTARCheckBox);
            this.Controls.Add(this.DrawLabelsCheckBox);
            this.Controls.Add(this.DrawSymbolsCheckBox);
            this.Controls.Add(this.IncludeSIDSTARReferencesCheckBox);
            this.Controls.Add(this.UseFixesAsCoordinatesCheckBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.UseNaviGraphCheckBox);
            this.Controls.Add(this.ConfirmOverwriteCheckBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Preferences";
            this.Text = "Preferences";
            this.Load += new System.EventHandler(this.Preferences_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox ConfirmOverwriteCheckBox;
        private System.Windows.Forms.CheckBox UseNaviGraphCheckBox;
        private System.Windows.Forms.Label label1;
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
        private System.Windows.Forms.CheckBox CombineCheckBox;
        private System.Windows.Forms.CheckBox IncludeSUACheckBox;
    }
}