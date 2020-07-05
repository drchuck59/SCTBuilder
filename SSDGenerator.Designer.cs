namespace SCTBuilder
{
    partial class SSDGenerator
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
            this.FixImportGroupBox = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.FixListDataGridView = new System.Windows.Forms.DataGridView();
            this.IdentifierLabel = new System.Windows.Forms.Label();
            this.IdentifierTextBox = new System.Windows.Forms.TextBox();
            this.IncludeSidStarReferencesCheckBox = new System.Windows.Forms.CheckBox();
            this.DrawFixSymbolsOnDiagramsCheckBox = new System.Windows.Forms.CheckBox();
            this.DrawFixLabelsOnDiagramsCheckBox = new System.Windows.Forms.CheckBox();
            this.label9 = new System.Windows.Forms.Label();
            this.OutputTextBox = new System.Windows.Forms.TextBox();
            this.Copy2ClipboardButton = new System.Windows.Forms.Button();
            this.ClearOutputButton = new System.Windows.Forms.Button();
            this.SaveOutput2FileButton = new System.Windows.Forms.Button();
            this.UseFixesAsCoordsCheckBox = new System.Windows.Forms.CheckBox();
            this.AddLinesButton = new System.Windows.Forms.Button();
            this.UpdatingLabel = new System.Windows.Forms.Label();
            this.FixImportGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FixListDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // FixImportGroupBox
            // 
            this.FixImportGroupBox.Controls.Add(this.label1);
            this.FixImportGroupBox.Controls.Add(this.FixListDataGridView);
            this.FixImportGroupBox.Controls.Add(this.IdentifierLabel);
            this.FixImportGroupBox.Controls.Add(this.IdentifierTextBox);
            this.FixImportGroupBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FixImportGroupBox.Location = new System.Drawing.Point(11, 10);
            this.FixImportGroupBox.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.FixImportGroupBox.Name = "FixImportGroupBox";
            this.FixImportGroupBox.Padding = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.FixImportGroupBox.Size = new System.Drawing.Size(228, 230);
            this.FixImportGroupBox.TabIndex = 40;
            this.FixImportGroupBox.TabStop = false;
            this.FixImportGroupBox.Text = "Select SID or STAR";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(145, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 26);
            this.label1.TabIndex = 3;
            this.label1.Text = "Double click \r\nrow to select";
            // 
            // FixListDataGridView
            // 
            this.FixListDataGridView.AllowUserToAddRows = false;
            this.FixListDataGridView.AllowUserToDeleteRows = false;
            this.FixListDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.FixListDataGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
            this.FixListDataGridView.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            this.FixListDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.FixListDataGridView.Location = new System.Drawing.Point(14, 45);
            this.FixListDataGridView.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.FixListDataGridView.MultiSelect = false;
            this.FixListDataGridView.Name = "FixListDataGridView";
            this.FixListDataGridView.ReadOnly = true;
            this.FixListDataGridView.RowHeadersVisible = false;
            this.FixListDataGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.FixListDataGridView.RowTemplate.Height = 28;
            this.FixListDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.FixListDataGridView.Size = new System.Drawing.Size(203, 181);
            this.FixListDataGridView.TabIndex = 2;
            this.FixListDataGridView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.FixListDataGridView_CellDoubleClick);
            // 
            // IdentifierLabel
            // 
            this.IdentifierLabel.AutoSize = true;
            this.IdentifierLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IdentifierLabel.Location = new System.Drawing.Point(11, 21);
            this.IdentifierLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.IdentifierLabel.Name = "IdentifierLabel";
            this.IdentifierLabel.Size = new System.Drawing.Size(62, 17);
            this.IdentifierLabel.TabIndex = 1;
            this.IdentifierLabel.Text = "Identifier";
            // 
            // IdentifierTextBox
            // 
            this.IdentifierTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IdentifierTextBox.Location = new System.Drawing.Point(71, 18);
            this.IdentifierTextBox.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.IdentifierTextBox.Name = "IdentifierTextBox";
            this.IdentifierTextBox.Size = new System.Drawing.Size(68, 23);
            this.IdentifierTextBox.TabIndex = 0;
            this.IdentifierTextBox.TabStop = false;
            this.IdentifierTextBox.TextChanged += new System.EventHandler(this.IdentifierTextBox_TextChanged);
            this.IdentifierTextBox.Validated += new System.EventHandler(this.IdentifierTextBox_Validated);
            // 
            // IncludeSidStarReferencesCheckBox
            // 
            this.IncludeSidStarReferencesCheckBox.AutoSize = true;
            this.IncludeSidStarReferencesCheckBox.Location = new System.Drawing.Point(25, 245);
            this.IncludeSidStarReferencesCheckBox.Name = "IncludeSidStarReferencesCheckBox";
            this.IncludeSidStarReferencesCheckBox.Size = new System.Drawing.Size(211, 17);
            this.IncludeSidStarReferencesCheckBox.TabIndex = 41;
            this.IncludeSidStarReferencesCheckBox.Text = "Include [AIRPORT] and FIX references";
            this.IncludeSidStarReferencesCheckBox.UseVisualStyleBackColor = true;
            this.IncludeSidStarReferencesCheckBox.CheckedChanged += new System.EventHandler(this.IncludeSidStarReferencesCheckBox_CheckedChanged);
            // 
            // DrawFixSymbolsOnDiagramsCheckBox
            // 
            this.DrawFixSymbolsOnDiagramsCheckBox.AutoSize = true;
            this.DrawFixSymbolsOnDiagramsCheckBox.Location = new System.Drawing.Point(25, 269);
            this.DrawFixSymbolsOnDiagramsCheckBox.Name = "DrawFixSymbolsOnDiagramsCheckBox";
            this.DrawFixSymbolsOnDiagramsCheckBox.Size = new System.Drawing.Size(169, 17);
            this.DrawFixSymbolsOnDiagramsCheckBox.TabIndex = 42;
            this.DrawFixSymbolsOnDiagramsCheckBox.Text = "Include Symbols over Navaids";
            this.DrawFixSymbolsOnDiagramsCheckBox.UseVisualStyleBackColor = true;
            this.DrawFixSymbolsOnDiagramsCheckBox.CheckedChanged += new System.EventHandler(this.DrawFixSymbolsOnDiagramsCheckBox_CheckedChanged);
            // 
            // DrawFixLabelsOnDiagramsCheckBox
            // 
            this.DrawFixLabelsOnDiagramsCheckBox.AutoSize = true;
            this.DrawFixLabelsOnDiagramsCheckBox.Location = new System.Drawing.Point(25, 293);
            this.DrawFixLabelsOnDiagramsCheckBox.Name = "DrawFixLabelsOnDiagramsCheckBox";
            this.DrawFixLabelsOnDiagramsCheckBox.Size = new System.Drawing.Size(172, 17);
            this.DrawFixLabelsOnDiagramsCheckBox.TabIndex = 43;
            this.DrawFixLabelsOnDiagramsCheckBox.Text = "Include Labels next to Navaids";
            this.DrawFixLabelsOnDiagramsCheckBox.UseVisualStyleBackColor = true;
            this.DrawFixLabelsOnDiagramsCheckBox.CheckedChanged += new System.EventHandler(this.DrawFixLabelsOnDiagramsCheckBox_CheckedChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(268, 11);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(86, 13);
            this.label9.TabIndex = 45;
            this.label9.Text = "Current Contents";
            // 
            // OutputTextBox
            // 
            this.OutputTextBox.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OutputTextBox.Location = new System.Drawing.Point(267, 28);
            this.OutputTextBox.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.OutputTextBox.Multiline = true;
            this.OutputTextBox.Name = "OutputTextBox";
            this.OutputTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.OutputTextBox.Size = new System.Drawing.Size(465, 282);
            this.OutputTextBox.TabIndex = 44;
            this.OutputTextBox.WordWrap = false;
            // 
            // Copy2ClipboardButton
            // 
            this.Copy2ClipboardButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Copy2ClipboardButton.Location = new System.Drawing.Point(500, 312);
            this.Copy2ClipboardButton.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.Copy2ClipboardButton.Name = "Copy2ClipboardButton";
            this.Copy2ClipboardButton.Size = new System.Drawing.Size(79, 52);
            this.Copy2ClipboardButton.TabIndex = 46;
            this.Copy2ClipboardButton.Text = "Copy to Clipboard";
            this.Copy2ClipboardButton.UseVisualStyleBackColor = true;
            this.Copy2ClipboardButton.Click += new System.EventHandler(this.Copy2ClipboardButton_Click);
            // 
            // ClearOutputButton
            // 
            this.ClearOutputButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.ClearOutputButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ClearOutputButton.Location = new System.Drawing.Point(650, 312);
            this.ClearOutputButton.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.ClearOutputButton.Name = "ClearOutputButton";
            this.ClearOutputButton.Size = new System.Drawing.Size(78, 52);
            this.ClearOutputButton.TabIndex = 48;
            this.ClearOutputButton.Text = "Clear Contents";
            this.ClearOutputButton.UseVisualStyleBackColor = false;
            this.ClearOutputButton.Click += new System.EventHandler(this.ClearOutputButton_Click);
            // 
            // SaveOutput2FileButton
            // 
            this.SaveOutput2FileButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SaveOutput2FileButton.Location = new System.Drawing.Point(574, 312);
            this.SaveOutput2FileButton.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.SaveOutput2FileButton.Name = "SaveOutput2FileButton";
            this.SaveOutput2FileButton.Size = new System.Drawing.Size(81, 52);
            this.SaveOutput2FileButton.TabIndex = 47;
            this.SaveOutput2FileButton.Text = "Save to File";
            this.SaveOutput2FileButton.UseVisualStyleBackColor = true;
            this.SaveOutput2FileButton.Click += new System.EventHandler(this.SaveOutput2FileButton_Click);
            // 
            // UseFixesAsCoordsCheckBox
            // 
            this.UseFixesAsCoordsCheckBox.AutoSize = true;
            this.UseFixesAsCoordsCheckBox.Location = new System.Drawing.Point(25, 317);
            this.UseFixesAsCoordsCheckBox.Name = "UseFixesAsCoordsCheckBox";
            this.UseFixesAsCoordsCheckBox.Size = new System.Drawing.Size(200, 17);
            this.UseFixesAsCoordsCheckBox.TabIndex = 49;
            this.UseFixesAsCoordsCheckBox.Text = "Replace Coordinates with FIX names";
            this.UseFixesAsCoordsCheckBox.UseVisualStyleBackColor = true;
            // 
            // AddLinesButton
            // 
            this.AddLinesButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AddLinesButton.Location = new System.Drawing.Point(267, 317);
            this.AddLinesButton.Name = "AddLinesButton";
            this.AddLinesButton.Size = new System.Drawing.Size(87, 47);
            this.AddLinesButton.TabIndex = 50;
            this.AddLinesButton.Text = "Generate SID/STAR";
            this.AddLinesButton.UseVisualStyleBackColor = true;
            this.AddLinesButton.Click += new System.EventHandler(this.AddLinesButton_Click);
            // 
            // UpdatingLabel
            // 
            this.UpdatingLabel.AutoSize = true;
            this.UpdatingLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.UpdatingLabel.Location = new System.Drawing.Point(361, 326);
            this.UpdatingLabel.Name = "UpdatingLabel";
            this.UpdatingLabel.Size = new System.Drawing.Size(73, 13);
            this.UpdatingLabel.TabIndex = 51;
            this.UpdatingLabel.Text = "Working on it!";
            this.UpdatingLabel.Visible = false;
            // 
            // SSDGenerator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(759, 380);
            this.Controls.Add(this.UpdatingLabel);
            this.Controls.Add(this.AddLinesButton);
            this.Controls.Add(this.UseFixesAsCoordsCheckBox);
            this.Controls.Add(this.Copy2ClipboardButton);
            this.Controls.Add(this.ClearOutputButton);
            this.Controls.Add(this.SaveOutput2FileButton);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.OutputTextBox);
            this.Controls.Add(this.DrawFixLabelsOnDiagramsCheckBox);
            this.Controls.Add(this.DrawFixSymbolsOnDiagramsCheckBox);
            this.Controls.Add(this.IncludeSidStarReferencesCheckBox);
            this.Controls.Add(this.FixImportGroupBox);
            this.Name = "SSDGenerator";
            this.Text = "SID/STAR Generator";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SSDGenerator_FormClosing);
            this.Load += new System.EventHandler(this.SSDGenerator_Load);
            this.FixImportGroupBox.ResumeLayout(false);
            this.FixImportGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FixListDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox FixImportGroupBox;
        private System.Windows.Forms.DataGridView FixListDataGridView;
        private System.Windows.Forms.Label IdentifierLabel;
        private System.Windows.Forms.TextBox IdentifierTextBox;
        private System.Windows.Forms.CheckBox IncludeSidStarReferencesCheckBox;
        private System.Windows.Forms.CheckBox DrawFixSymbolsOnDiagramsCheckBox;
        private System.Windows.Forms.CheckBox DrawFixLabelsOnDiagramsCheckBox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox OutputTextBox;
        private System.Windows.Forms.Button Copy2ClipboardButton;
        private System.Windows.Forms.Button ClearOutputButton;
        private System.Windows.Forms.Button SaveOutput2FileButton;
        private System.Windows.Forms.CheckBox UseFixesAsCoordsCheckBox;
        private System.Windows.Forms.Button AddLinesButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label UpdatingLabel;
    }
}