namespace SCTBuilder
{
    partial class DMS_DecDeg
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
            this.label1 = new System.Windows.Forms.Label();
            this.LatDMSDegTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.LonDecTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.LatDecTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.DMSTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.DECTextBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.SCTTextBox = new System.Windows.Forms.TextBox();
            this.LonSCTTextBox = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.LatSCTTextBox = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.ParseSCTInsertButton = new System.Windows.Forms.Button();
            this.PasteButton = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.NSComboBox = new System.Windows.Forms.ComboBox();
            this.LatDMSMinTextBox = new System.Windows.Forms.TextBox();
            this.LatDMSSecTextBox = new System.Windows.Forms.TextBox();
            this.LonDMSSecTextBox = new System.Windows.Forms.TextBox();
            this.LonDMSMinTextBox = new System.Windows.Forms.TextBox();
            this.EWComboBox = new System.Windows.Forms.ComboBox();
            this.LonDMSDegTextBox = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 40);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "DMS Latitude ";
            // 
            // LatDMSDegTextBox
            // 
            this.LatDMSDegTextBox.Location = new System.Drawing.Point(87, 56);
            this.LatDMSDegTextBox.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.LatDMSDegTextBox.MaxLength = 2;
            this.LatDMSDegTextBox.Name = "LatDMSDegTextBox";
            this.LatDMSDegTextBox.Size = new System.Drawing.Size(25, 20);
            this.LatDMSDegTextBox.TabIndex = 2;
            this.LatDMSDegTextBox.DoubleClick += new System.EventHandler(this.LatDMSTextBox_DoubleClick);
            this.LatDMSDegTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.LatDMSDegTextBox_KeyPress);
            this.LatDMSDegTextBox.Validated += new System.EventHandler(this.LatDMSDegTextBox_Validated);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(26, 81);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Longitude DMS";
            // 
            // LonDecTextBox
            // 
            this.LonDecTextBox.Location = new System.Drawing.Point(192, 97);
            this.LonDecTextBox.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.LonDecTextBox.Name = "LonDecTextBox";
            this.LonDecTextBox.Size = new System.Drawing.Size(119, 20);
            this.LonDecTextBox.TabIndex = 10;
            this.toolTip1.SetToolTip(this.LonDecTextBox, "(+/-)DDD.dddd");
            this.LonDecTextBox.DoubleClick += new System.EventHandler(this.LonDecTextBox_DoubleClick);
            this.LonDecTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.LonDecTextBox_KeyPress);
            this.LonDecTextBox.Validated += new System.EventHandler(this.LonDecTextBox_Validated);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(189, 80);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(95, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Longitude Decimal";
            // 
            // LatDecTextBox
            // 
            this.LatDecTextBox.Location = new System.Drawing.Point(193, 57);
            this.LatDecTextBox.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.LatDecTextBox.Name = "LatDecTextBox";
            this.LatDecTextBox.Size = new System.Drawing.Size(119, 20);
            this.LatDecTextBox.TabIndex = 9;
            this.toolTip1.SetToolTip(this.LatDecTextBox, " (+/-)DD.dddd");
            this.LatDecTextBox.DoubleClick += new System.EventHandler(this.LatDecTextBox_DoubleClick);
            this.LatDecTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.LatDecTextBox_KeyPress);
            this.LatDecTextBox.Validated += new System.EventHandler(this.LatDecTextBox_Validated);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(190, 39);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(86, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Latitude Decimal";
            // 
            // DMSTextBox
            // 
            this.DMSTextBox.Location = new System.Drawing.Point(29, 172);
            this.DMSTextBox.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.DMSTextBox.Name = "DMSTextBox";
            this.DMSTextBox.Size = new System.Drawing.Size(222, 20);
            this.DMSTextBox.TabIndex = 15;
            this.DMSTextBox.DoubleClick += new System.EventHandler(this.DMSTextBox_DoubleClick);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(29, 153);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(90, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "DMS Coordinates";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(262, 153);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(104, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Decimal Coordinates";
            // 
            // DECTextBox
            // 
            this.DECTextBox.Location = new System.Drawing.Point(262, 172);
            this.DECTextBox.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.DECTextBox.Name = "DECTextBox";
            this.DECTextBox.Size = new System.Drawing.Size(222, 20);
            this.DECTextBox.TabIndex = 16;
            this.DECTextBox.DoubleClick += new System.EventHandler(this.DECTextBox_DoubleClick);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(487, 153);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(87, 13);
            this.label7.TabIndex = 17;
            this.label7.Text = "SCT Coordinates";
            // 
            // SCTTextBox
            // 
            this.SCTTextBox.Location = new System.Drawing.Point(487, 172);
            this.SCTTextBox.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.SCTTextBox.Name = "SCTTextBox";
            this.SCTTextBox.Size = new System.Drawing.Size(222, 20);
            this.SCTTextBox.TabIndex = 17;
            this.SCTTextBox.DoubleClick += new System.EventHandler(this.SCTTextBox_DoubleClick);
            // 
            // LonSCTTextBox
            // 
            this.LonSCTTextBox.Location = new System.Drawing.Point(345, 97);
            this.LonSCTTextBox.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.LonSCTTextBox.Name = "LonSCTTextBox";
            this.LonSCTTextBox.Size = new System.Drawing.Size(119, 20);
            this.LonSCTTextBox.TabIndex = 12;
            this.toolTip1.SetToolTip(this.LonSCTTextBox, "(W/E)DDD.MM.DD.ddd");
            this.LonSCTTextBox.DoubleClick += new System.EventHandler(this.LonSCTTextBox_DoubleClick);
            this.LonSCTTextBox.Validated += new System.EventHandler(this.LonSCTTextBox_Validated);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(343, 80);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(78, 13);
            this.label8.TabIndex = 14;
            this.label8.Text = "Longitude SCT";
            // 
            // LatSCTTextBox
            // 
            this.LatSCTTextBox.Location = new System.Drawing.Point(346, 57);
            this.LatSCTTextBox.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.LatSCTTextBox.Name = "LatSCTTextBox";
            this.LatSCTTextBox.Size = new System.Drawing.Size(119, 20);
            this.LatSCTTextBox.TabIndex = 11;
            this.toolTip1.SetToolTip(this.LatSCTTextBox, "(N/S)DDD.MM.DD.ddd");
            this.LatSCTTextBox.DoubleClick += new System.EventHandler(this.LatSCTTextBox_DoubleClick);
            this.LatSCTTextBox.Validated += new System.EventHandler(this.LatSCTTextBox_Validated);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(344, 39);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(69, 13);
            this.label9.TabIndex = 12;
            this.label9.Text = "Latitude SCT";
            // 
            // ParseSCTInsertButton
            // 
            this.ParseSCTInsertButton.Location = new System.Drawing.Point(614, 132);
            this.ParseSCTInsertButton.Margin = new System.Windows.Forms.Padding(2);
            this.ParseSCTInsertButton.Name = "ParseSCTInsertButton";
            this.ParseSCTInsertButton.Size = new System.Drawing.Size(95, 34);
            this.ParseSCTInsertButton.TabIndex = 14;
            this.ParseSCTInsertButton.Text = "Convert this SCT\r\nto other formats";
            this.toolTip1.SetToolTip(this.ParseSCTInsertButton, "Parses line below to ALL boxes and copies Decimal Lat/Lon to clipboard");
            this.ParseSCTInsertButton.UseVisualStyleBackColor = true;
            this.ParseSCTInsertButton.Click += new System.EventHandler(this.ParseSCTInsertButton_Click);
            // 
            // PasteButton
            // 
            this.PasteButton.Location = new System.Drawing.Point(490, 114);
            this.PasteButton.Margin = new System.Windows.Forms.Padding(2);
            this.PasteButton.Name = "PasteButton";
            this.PasteButton.Size = new System.Drawing.Size(99, 37);
            this.PasteButton.TabIndex = 13;
            this.PasteButton.Text = "  Paste to\r\nSCT Textbox";
            this.toolTip1.SetToolTip(this.PasteButton, "Pastes clipboard to line below");
            this.PasteButton.UseVisualStyleBackColor = true;
            this.PasteButton.Click += new System.EventHandler(this.PasteButton_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.PowderBlue;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(528, 9);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(192, 34);
            this.label10.TabIndex = 20;
            this.label10.Text = "Double click any textbox to \r\ncopy that textbox to clipboard";
            // 
            // NSComboBox
            // 
            this.NSComboBox.FormattingEnabled = true;
            this.NSComboBox.Items.AddRange(new object[] {
            "N",
            "S"});
            this.NSComboBox.Location = new System.Drawing.Point(26, 56);
            this.NSComboBox.Name = "NSComboBox";
            this.NSComboBox.Size = new System.Drawing.Size(45, 21);
            this.NSComboBox.TabIndex = 1;
            this.NSComboBox.Validated += new System.EventHandler(this.NSComboBox_Validated);
            // 
            // LatDMSMinTextBox
            // 
            this.LatDMSMinTextBox.Location = new System.Drawing.Point(116, 56);
            this.LatDMSMinTextBox.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.LatDMSMinTextBox.MaxLength = 2;
            this.LatDMSMinTextBox.Name = "LatDMSMinTextBox";
            this.LatDMSMinTextBox.Size = new System.Drawing.Size(25, 20);
            this.LatDMSMinTextBox.TabIndex = 3;
            this.LatDMSMinTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.LatDMSMinTextBox_KeyPress);
            this.LatDMSMinTextBox.Validated += new System.EventHandler(this.LatDMSMinTextBox_Validated);
            // 
            // LatDMSSecTextBox
            // 
            this.LatDMSSecTextBox.Location = new System.Drawing.Point(145, 56);
            this.LatDMSSecTextBox.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.LatDMSSecTextBox.MaxLength = 2;
            this.LatDMSSecTextBox.Name = "LatDMSSecTextBox";
            this.LatDMSSecTextBox.Size = new System.Drawing.Size(34, 20);
            this.LatDMSSecTextBox.TabIndex = 4;
            this.LatDMSSecTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.LatDMSSecTextBox_KeyPress);
            this.LatDMSSecTextBox.Validated += new System.EventHandler(this.LatDMSSecTextBox_Validated);
            // 
            // LonDMSSecTextBox
            // 
            this.LonDMSSecTextBox.Location = new System.Drawing.Point(145, 97);
            this.LonDMSSecTextBox.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.LonDMSSecTextBox.MaxLength = 2;
            this.LonDMSSecTextBox.Name = "LonDMSSecTextBox";
            this.LonDMSSecTextBox.Size = new System.Drawing.Size(34, 20);
            this.LonDMSSecTextBox.TabIndex = 8;
            this.LonDMSSecTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.LonDMSSecTextBox_KeyPress);
            this.LonDMSSecTextBox.Validated += new System.EventHandler(this.LonDMSSecTextBox_Validated);
            // 
            // LonDMSMinTextBox
            // 
            this.LonDMSMinTextBox.Location = new System.Drawing.Point(116, 97);
            this.LonDMSMinTextBox.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.LonDMSMinTextBox.MaxLength = 2;
            this.LonDMSMinTextBox.Name = "LonDMSMinTextBox";
            this.LonDMSMinTextBox.Size = new System.Drawing.Size(25, 20);
            this.LonDMSMinTextBox.TabIndex = 7;
            this.LonDMSMinTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.LonDMSMinTextBox_KeyPress);
            this.LonDMSMinTextBox.Validated += new System.EventHandler(this.LonDMSMinTextBox_Validated);
            // 
            // EWComboBox
            // 
            this.EWComboBox.FormattingEnabled = true;
            this.EWComboBox.Items.AddRange(new object[] {
            "W",
            "E"});
            this.EWComboBox.Location = new System.Drawing.Point(26, 97);
            this.EWComboBox.Name = "EWComboBox";
            this.EWComboBox.Size = new System.Drawing.Size(45, 21);
            this.EWComboBox.TabIndex = 5;
            this.EWComboBox.Validated += new System.EventHandler(this.EWComboBox_Validated);
            // 
            // LonDMSDegTextBox
            // 
            this.LonDMSDegTextBox.Location = new System.Drawing.Point(76, 97);
            this.LonDMSDegTextBox.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.LonDMSDegTextBox.MaxLength = 3;
            this.LonDMSDegTextBox.Name = "LonDMSDegTextBox";
            this.LonDMSDegTextBox.Size = new System.Drawing.Size(36, 20);
            this.LonDMSDegTextBox.TabIndex = 6;
            this.LonDMSDegTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.LonDMSDegTextBox_KeyPress);
            this.LonDMSDegTextBox.Validated += new System.EventHandler(this.LonDMSDegTextBox_Validated);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.PowderBlue;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(204, 9);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(210, 17);
            this.label11.TabIndex = 28;
            this.label11.Text = "Hover over text boxes for format";
            // 
            // DMS_DecDeg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.CornflowerBlue;
            this.ClientSize = new System.Drawing.Size(731, 203);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.LonDMSSecTextBox);
            this.Controls.Add(this.LonDMSMinTextBox);
            this.Controls.Add(this.EWComboBox);
            this.Controls.Add(this.LonDMSDegTextBox);
            this.Controls.Add(this.LatDMSSecTextBox);
            this.Controls.Add(this.LatDMSMinTextBox);
            this.Controls.Add(this.NSComboBox);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.PasteButton);
            this.Controls.Add(this.ParseSCTInsertButton);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.SCTTextBox);
            this.Controls.Add(this.LonSCTTextBox);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.LatSCTTextBox);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.DECTextBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.DMSTextBox);
            this.Controls.Add(this.LonDecTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.LatDecTextBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.LatDMSDegTextBox);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Name = "DMS_DecDeg";
            this.Text = "Coordinate Converter";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox LatDMSDegTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox LonDecTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox LatDecTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox DMSTextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox DECTextBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox SCTTextBox;
        private System.Windows.Forms.TextBox LonSCTTextBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox LatSCTTextBox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button ParseSCTInsertButton;
        private System.Windows.Forms.Button PasteButton;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ComboBox NSComboBox;
        private System.Windows.Forms.TextBox LatDMSMinTextBox;
        private System.Windows.Forms.TextBox LatDMSSecTextBox;
        private System.Windows.Forms.TextBox LonDMSSecTextBox;
        private System.Windows.Forms.TextBox LonDMSMinTextBox;
        private System.Windows.Forms.ComboBox EWComboBox;
        private System.Windows.Forms.TextBox LonDMSDegTextBox;
        private System.Windows.Forms.Label label11;
    }
}