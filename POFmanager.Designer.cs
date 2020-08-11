namespace SCTBuilder
{
    partial class POFmanager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(POFmanager));
            this.label1 = new System.Windows.Forms.Label();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.POFpathTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.OpenPOFButton = new System.Windows.Forms.Button();
            this.POFDataGridView = new System.Windows.Forms.DataGridView();
            this.WriteSCTButton = new System.Windows.Forms.Button();
            this.WriteESEButton = new System.Windows.Forms.Button();
            this.FixImportGroupBox = new System.Windows.Forms.GroupBox();
            this.FixListDataGridView = new System.Windows.Forms.DataGridView();
            this.IdentifierLabel = new System.Windows.Forms.Label();
            this.IdentifierTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.EnterCoordsButton = new System.Windows.Forms.Button();
            this.ResetSortButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.POFDataGridView)).BeginInit();
            this.FixImportGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FixListDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(282, 65);
            this.label1.TabIndex = 0;
            this.label1.Text = resources.GetString("label1.Text");
            // 
            // POFpathTextBox
            // 
            this.POFpathTextBox.Location = new System.Drawing.Point(404, 10);
            this.POFpathTextBox.Name = "POFpathTextBox";
            this.POFpathTextBox.Size = new System.Drawing.Size(229, 20);
            this.POFpathTextBox.TabIndex = 1;
            this.POFpathTextBox.Validated += new System.EventHandler(this.POFpathTextBox_Validated);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(315, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "POF INPUT file:";
            // 
            // OpenPOFButton
            // 
            this.OpenPOFButton.Location = new System.Drawing.Point(640, 10);
            this.OpenPOFButton.Name = "OpenPOFButton";
            this.OpenPOFButton.Size = new System.Drawing.Size(33, 23);
            this.OpenPOFButton.TabIndex = 3;
            this.OpenPOFButton.Text = "...";
            this.OpenPOFButton.UseVisualStyleBackColor = true;
            this.OpenPOFButton.Click += new System.EventHandler(this.OpenPOFButton_Click);
            // 
            // POFDataGridView
            // 
            this.POFDataGridView.AllowDrop = true;
            this.POFDataGridView.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            this.POFDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.POFDataGridView.Location = new System.Drawing.Point(7, 84);
            this.POFDataGridView.MultiSelect = false;
            this.POFDataGridView.Name = "POFDataGridView";
            this.POFDataGridView.Size = new System.Drawing.Size(626, 334);
            this.POFDataGridView.TabIndex = 4;
            this.POFDataGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.POFDataGridView_CellClick);
            this.POFDataGridView.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.POFDataGridView_CellFormatting);
            // 
            // WriteSCTButton
            // 
            this.WriteSCTButton.Location = new System.Drawing.Point(340, 46);
            this.WriteSCTButton.Name = "WriteSCTButton";
            this.WriteSCTButton.Size = new System.Drawing.Size(86, 32);
            this.WriteSCTButton.TabIndex = 5;
            this.WriteSCTButton.Text = "Output as SCT";
            this.WriteSCTButton.UseVisualStyleBackColor = true;
            this.WriteSCTButton.Click += new System.EventHandler(this.WriteSCTButton_Click);
            // 
            // WriteESEButton
            // 
            this.WriteESEButton.Location = new System.Drawing.Point(432, 46);
            this.WriteESEButton.Name = "WriteESEButton";
            this.WriteESEButton.Size = new System.Drawing.Size(86, 32);
            this.WriteESEButton.TabIndex = 6;
            this.WriteESEButton.Text = "Output as ESE";
            this.WriteESEButton.UseVisualStyleBackColor = true;
            this.WriteESEButton.Click += new System.EventHandler(this.WriteESEButton_Click);
            // 
            // FixImportGroupBox
            // 
            this.FixImportGroupBox.Controls.Add(this.FixListDataGridView);
            this.FixImportGroupBox.Controls.Add(this.IdentifierLabel);
            this.FixImportGroupBox.Controls.Add(this.IdentifierTextBox);
            this.FixImportGroupBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FixImportGroupBox.Location = new System.Drawing.Point(638, 84);
            this.FixImportGroupBox.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.FixImportGroupBox.Name = "FixImportGroupBox";
            this.FixImportGroupBox.Padding = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.FixImportGroupBox.Size = new System.Drawing.Size(150, 211);
            this.FixImportGroupBox.TabIndex = 10;
            this.FixImportGroupBox.TabStop = false;
            this.FixImportGroupBox.Text = "Import From Fix/Apt";
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
            this.FixListDataGridView.Size = new System.Drawing.Size(125, 154);
            this.FixListDataGridView.TabIndex = 2;
            // 
            // IdentifierLabel
            // 
            this.IdentifierLabel.AutoSize = true;
            this.IdentifierLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IdentifierLabel.Location = new System.Drawing.Point(7, 21);
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
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(645, 327);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(142, 91);
            this.label3.TabIndex = 11;
            this.label3.Text = "Euroscope only\r\nTo add a coordinate to POF:\r\n1. Select the Latitude of the \r\ncoor" +
    "dinate pair to be used\r\n2. Use \"Identifier\" to search \r\nfor the navaid or airpor" +
    "t\r\n3. Click \"Enter coordinates\"";
            // 
            // EnterCoordsButton
            // 
            this.EnterCoordsButton.Location = new System.Drawing.Point(652, 298);
            this.EnterCoordsButton.Name = "EnterCoordsButton";
            this.EnterCoordsButton.Size = new System.Drawing.Size(125, 23);
            this.EnterCoordsButton.TabIndex = 12;
            this.EnterCoordsButton.Text = "Enter Coordinates";
            this.EnterCoordsButton.UseVisualStyleBackColor = true;
            // 
            // ResetSortButton
            // 
            this.ResetSortButton.Location = new System.Drawing.Point(7, 424);
            this.ResetSortButton.Name = "ResetSortButton";
            this.ResetSortButton.Size = new System.Drawing.Size(168, 23);
            this.ResetSortButton.TabIndex = 13;
            this.ResetSortButton.Text = "Resequence to original file";
            this.ResetSortButton.UseVisualStyleBackColor = true;
            this.ResetSortButton.Click += new System.EventHandler(this.ResetSortButton_Click);
            // 
            // POFmanager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Cyan;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.ResetSortButton);
            this.Controls.Add(this.EnterCoordsButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.FixImportGroupBox);
            this.Controls.Add(this.WriteESEButton);
            this.Controls.Add(this.WriteSCTButton);
            this.Controls.Add(this.POFDataGridView);
            this.Controls.Add(this.OpenPOFButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.POFpathTextBox);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "POFmanager";
            this.Text = "POF Manager";
            this.Load += new System.EventHandler(this.POFmanager_Load);
            ((System.ComponentModel.ISupportInitialize)(this.POFDataGridView)).EndInit();
            this.FixImportGroupBox.ResumeLayout(false);
            this.FixImportGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FixListDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.TextBox POFpathTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button OpenPOFButton;
        private System.Windows.Forms.DataGridView POFDataGridView;
        private System.Windows.Forms.Button WriteSCTButton;
        private System.Windows.Forms.Button WriteESEButton;
        private System.Windows.Forms.GroupBox FixImportGroupBox;
        private System.Windows.Forms.DataGridView FixListDataGridView;
        private System.Windows.Forms.Label IdentifierLabel;
        private System.Windows.Forms.TextBox IdentifierTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button EnterCoordsButton;
        private System.Windows.Forms.Button ResetSortButton;
    }
}