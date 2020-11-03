namespace SCTBuilder
{
    partial class CoordConverter
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CoordConverter));
            this.ResetButton = new System.Windows.Forms.Button();
            this.LonDMSTextBox = new System.Windows.Forms.TextBox();
            this.DMSHelpButton = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.LonSCTTextBox = new System.Windows.Forms.TextBox();
            this.LatSCTTextBox = new System.Windows.Forms.TextBox();
            this.LonDecTextBox = new System.Windows.Forms.TextBox();
            this.LatDecTextBox = new System.Windows.Forms.TextBox();
            this.LatDMSTextBox = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.SCTTextBox = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.DECTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.DMSTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ResetButton
            // 
            this.ResetButton.Location = new System.Drawing.Point(479, 70);
            this.ResetButton.Margin = new System.Windows.Forms.Padding(2);
            this.ResetButton.Name = "ResetButton";
            this.ResetButton.Size = new System.Drawing.Size(77, 45);
            this.ResetButton.TabIndex = 51;
            this.ResetButton.Text = "Reset all text boxes";
            this.toolTip1.SetToolTip(this.ResetButton, "Pastes clipboard to line below");
            this.ResetButton.UseVisualStyleBackColor = true;
            this.ResetButton.Validated += new System.EventHandler(this.ResetButton_Click);
            // 
            // LonDMSTextBox
            // 
            this.LonDMSTextBox.Location = new System.Drawing.Point(11, 110);
            this.LonDMSTextBox.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.LonDMSTextBox.Name = "LonDMSTextBox";
            this.LonDMSTextBox.Size = new System.Drawing.Size(153, 20);
            this.LonDMSTextBox.TabIndex = 33;
            this.LonDMSTextBox.DoubleClick += new System.EventHandler(this.LatDMSTextBox_DoubleClick);
            this.LonDMSTextBox.Validated += new System.EventHandler(this.LonDMSTextBox_Validated);
            // 
            // DMSHelpButton
            // 
            this.DMSHelpButton.BackColor = System.Drawing.Color.Violet;
            this.DMSHelpButton.Location = new System.Drawing.Point(144, 155);
            this.DMSHelpButton.Name = "DMSHelpButton";
            this.DMSHelpButton.Size = new System.Drawing.Size(20, 24);
            this.DMSHelpButton.TabIndex = 50;
            this.DMSHelpButton.TabStop = false;
            this.DMSHelpButton.Text = "?";
            this.DMSHelpButton.UseVisualStyleBackColor = false;
            this.DMSHelpButton.Click += new System.EventHandler(this.DMSHelpButton_Click);
            // 
            // LonSCTTextBox
            // 
            this.LonSCTTextBox.Location = new System.Drawing.Point(342, 110);
            this.LonSCTTextBox.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.LonSCTTextBox.Name = "LonSCTTextBox";
            this.LonSCTTextBox.Size = new System.Drawing.Size(119, 20);
            this.LonSCTTextBox.TabIndex = 40;
            this.toolTip1.SetToolTip(this.LonSCTTextBox, "(W/E)DDD.MM.DD.ddd");
            this.LonSCTTextBox.DoubleClick += new System.EventHandler(this.LonSCTTextBox_DoubleClick);
            this.LonSCTTextBox.Validated += new System.EventHandler(this.LonSCTTextBox_Validated);
            // 
            // LatSCTTextBox
            // 
            this.LatSCTTextBox.Location = new System.Drawing.Point(343, 70);
            this.LatSCTTextBox.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.LatSCTTextBox.Name = "LatSCTTextBox";
            this.LatSCTTextBox.Size = new System.Drawing.Size(119, 20);
            this.LatSCTTextBox.TabIndex = 38;
            this.toolTip1.SetToolTip(this.LatSCTTextBox, "(N/S)DDD.MM.DD.ddd");
            this.LatSCTTextBox.DoubleClick += new System.EventHandler(this.LatSCTTextBox_DoubleClick);
            this.LatSCTTextBox.Validated += new System.EventHandler(this.LatSCTTextBox_Validated);
            // 
            // LonDecTextBox
            // 
            this.LonDecTextBox.Location = new System.Drawing.Point(189, 110);
            this.LonDecTextBox.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.LonDecTextBox.Name = "LonDecTextBox";
            this.LonDecTextBox.Size = new System.Drawing.Size(119, 20);
            this.LonDecTextBox.TabIndex = 37;
            this.toolTip1.SetToolTip(this.LonDecTextBox, "(+/-)DDD.dddd");
            this.LonDecTextBox.DoubleClick += new System.EventHandler(this.LonDecTextBox_DoubleClick);
            this.LonDecTextBox.Validated += new System.EventHandler(this.LonDecTextBox_Validated);
            // 
            // LatDecTextBox
            // 
            this.LatDecTextBox.Location = new System.Drawing.Point(190, 70);
            this.LatDecTextBox.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.LatDecTextBox.Name = "LatDecTextBox";
            this.LatDecTextBox.Size = new System.Drawing.Size(119, 20);
            this.LatDecTextBox.TabIndex = 35;
            this.toolTip1.SetToolTip(this.LatDecTextBox, " (+/-)DD.dddd");
            this.LatDecTextBox.DoubleClick += new System.EventHandler(this.LatDecTextBox_DoubleClick);
            this.LatDecTextBox.Validated += new System.EventHandler(this.LatDecTextBox_Validated);
            // 
            // LatDMSTextBox
            // 
            this.LatDMSTextBox.Location = new System.Drawing.Point(11, 69);
            this.LatDMSTextBox.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.LatDMSTextBox.Name = "LatDMSTextBox";
            this.LatDMSTextBox.Size = new System.Drawing.Size(153, 20);
            this.LatDMSTextBox.TabIndex = 32;
            this.LatDMSTextBox.DoubleClick += new System.EventHandler(this.LatDMSTextBox_DoubleClick);
            this.LatDMSTextBox.Validated += new System.EventHandler(this.LatDMSTextBox_Validated);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.PowderBlue;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(11, 18);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(367, 17);
            this.label10.TabIndex = 49;
            this.label10.Text = "Double click any textbox to copy that textbox to clipboard.";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(469, 166);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(87, 13);
            this.label7.TabIndex = 48;
            this.label7.Text = "SCT Coordinates";
            // 
            // SCTTextBox
            // 
            this.SCTTextBox.Location = new System.Drawing.Point(469, 185);
            this.SCTTextBox.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.SCTTextBox.Name = "SCTTextBox";
            this.SCTTextBox.Size = new System.Drawing.Size(222, 20);
            this.SCTTextBox.TabIndex = 43;
            this.SCTTextBox.DoubleClick += new System.EventHandler(this.SCTTextBox_DoubleClick);
            this.SCTTextBox.Validated += new System.EventHandler(this.SCTTextBox_Validated);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(340, 93);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(78, 13);
            this.label8.TabIndex = 47;
            this.label8.Text = "Longitude SCT";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(341, 52);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(69, 13);
            this.label9.TabIndex = 46;
            this.label9.Text = "Latitude SCT";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(244, 166);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(152, 13);
            this.label6.TabIndex = 45;
            this.label6.Text = "Decimal Coordinates (Lat, Lon)";
            // 
            // DECTextBox
            // 
            this.DECTextBox.Location = new System.Drawing.Point(244, 185);
            this.DECTextBox.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.DECTextBox.Name = "DECTextBox";
            this.DECTextBox.Size = new System.Drawing.Size(222, 20);
            this.DECTextBox.TabIndex = 42;
            this.DECTextBox.DoubleClick += new System.EventHandler(this.DECTextBox_DoubleClick);
            this.DECTextBox.Validated += new System.EventHandler(this.DECTextBox_Validated);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(11, 166);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(138, 13);
            this.label5.TabIndex = 44;
            this.label5.Text = "DMS Coordinates (Lat, Lon)";
            // 
            // DMSTextBox
            // 
            this.DMSTextBox.Location = new System.Drawing.Point(11, 185);
            this.DMSTextBox.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.DMSTextBox.Name = "DMSTextBox";
            this.DMSTextBox.Size = new System.Drawing.Size(222, 20);
            this.DMSTextBox.TabIndex = 41;
            this.DMSTextBox.DoubleClick += new System.EventHandler(this.DMSTextBox_DoubleClick);
            this.DMSTextBox.Validated += new System.EventHandler(this.DMSTextBox_Validated);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(186, 93);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(95, 13);
            this.label3.TabIndex = 39;
            this.label3.Text = "Longitude Decimal";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(187, 52);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(86, 13);
            this.label4.TabIndex = 36;
            this.label4.Text = "Latitude Decimal";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 94);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 13);
            this.label2.TabIndex = 34;
            this.label2.Text = "Longitude DMS";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 53);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 30;
            this.label1.Text = "DMS Latitude ";
            // 
            // CoordConverter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(800, 228);
            this.Controls.Add(this.ResetButton);
            this.Controls.Add(this.LonDMSTextBox);
            this.Controls.Add(this.DMSHelpButton);
            this.Controls.Add(this.LatDMSTextBox);
            this.Controls.Add(this.label10);
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
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CoordConverter";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CoordConverter";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ResetButton;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.TextBox LonDMSTextBox;
        private System.Windows.Forms.Button DMSHelpButton;
        private System.Windows.Forms.TextBox LonSCTTextBox;
        private System.Windows.Forms.TextBox LatSCTTextBox;
        private System.Windows.Forms.TextBox LonDecTextBox;
        private System.Windows.Forms.TextBox LatDecTextBox;
        private System.Windows.Forms.TextBox LatDMSTextBox;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox SCTTextBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox DECTextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox DMSTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}