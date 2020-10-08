namespace SCTBuilder
{
    partial class HoldingPattern
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
            this.FixBrgNUD = new System.Windows.Forms.NumericUpDown();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.FixDistTextBox = new System.Windows.Forms.TextBox();
            this.FixListDataGridView = new System.Windows.Forms.DataGridView();
            this.IdentifierLabel = new System.Windows.Forms.Label();
            this.IdentifierTextBox = new System.Windows.Forms.TextBox();
            this.AltitudeTypeComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.FixImportGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FixBrgNUD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FixListDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // FixImportGroupBox
            // 
            this.FixImportGroupBox.Controls.Add(this.FixBrgNUD);
            this.FixImportGroupBox.Controls.Add(this.label15);
            this.FixImportGroupBox.Controls.Add(this.label14);
            this.FixImportGroupBox.Controls.Add(this.FixDistTextBox);
            this.FixImportGroupBox.Controls.Add(this.FixListDataGridView);
            this.FixImportGroupBox.Controls.Add(this.IdentifierLabel);
            this.FixImportGroupBox.Controls.Add(this.IdentifierTextBox);
            this.FixImportGroupBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FixImportGroupBox.Location = new System.Drawing.Point(11, 10);
            this.FixImportGroupBox.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.FixImportGroupBox.Name = "FixImportGroupBox";
            this.FixImportGroupBox.Padding = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.FixImportGroupBox.Size = new System.Drawing.Size(232, 261);
            this.FixImportGroupBox.TabIndex = 40;
            this.FixImportGroupBox.TabStop = false;
            this.FixImportGroupBox.Text = "Import From Fix";
            // 
            // FixBrgNUD
            // 
            this.FixBrgNUD.Location = new System.Drawing.Point(76, 230);
            this.FixBrgNUD.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.FixBrgNUD.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.FixBrgNUD.Name = "FixBrgNUD";
            this.FixBrgNUD.Size = new System.Drawing.Size(47, 23);
            this.FixBrgNUD.TabIndex = 71;
            this.FixBrgNUD.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(129, 233);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(40, 15);
            this.label15.TabIndex = 70;
            this.label15.Text = "DME*";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(11, 233);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(62, 15);
            this.label14.TabIndex = 68;
            this.label14.Text = "Brg (mag)";
            // 
            // FixDistTextBox
            // 
            this.FixDistTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FixDistTextBox.Location = new System.Drawing.Point(175, 230);
            this.FixDistTextBox.Name = "FixDistTextBox";
            this.FixDistTextBox.Size = new System.Drawing.Size(40, 21);
            this.FixDistTextBox.TabIndex = 69;
            // 
            // FixListDataGridView
            // 
            this.FixListDataGridView.AllowUserToAddRows = false;
            this.FixListDataGridView.AllowUserToDeleteRows = false;
            this.FixListDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader;
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
            this.IdentifierTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IdentifierTextBox.Location = new System.Drawing.Point(71, 18);
            this.IdentifierTextBox.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.IdentifierTextBox.MaxLength = 6;
            this.IdentifierTextBox.Name = "IdentifierTextBox";
            this.IdentifierTextBox.Size = new System.Drawing.Size(68, 21);
            this.IdentifierTextBox.TabIndex = 0;
            this.IdentifierTextBox.TabStop = false;
            this.IdentifierTextBox.TextChanged += new System.EventHandler(this.IdentifierTextBox_TextChanged);
            // 
            // AltitudeTypeComboBox
            // 
            this.AltitudeTypeComboBox.FormattingEnabled = true;
            this.AltitudeTypeComboBox.Location = new System.Drawing.Point(143, 279);
            this.AltitudeTypeComboBox.Name = "AltitudeTypeComboBox";
            this.AltitudeTypeComboBox.Size = new System.Drawing.Size(100, 21);
            this.AltitudeTypeComboBox.TabIndex = 41;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 282);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(128, 13);
            this.label1.TabIndex = 42;
            this.label1.Text = "Altitude/AC Type for Hold";
            // 
            // HoldingPattern
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(479, 450);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.AltitudeTypeComboBox);
            this.Controls.Add(this.FixImportGroupBox);
            this.Name = "HoldingPattern";
            this.Text = "Holding Pattern";
            this.Load += new System.EventHandler(this.HoldingPattern_Load);
            this.FixImportGroupBox.ResumeLayout(false);
            this.FixImportGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FixBrgNUD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FixListDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox FixImportGroupBox;
        private System.Windows.Forms.NumericUpDown FixBrgNUD;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox FixDistTextBox;
        private System.Windows.Forms.DataGridView FixListDataGridView;
        private System.Windows.Forms.Label IdentifierLabel;
        private System.Windows.Forms.TextBox IdentifierTextBox;
        private System.Windows.Forms.ComboBox AltitudeTypeComboBox;
        private System.Windows.Forms.Label label1;
    }
}