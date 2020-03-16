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
            this.LatDMSTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.LonDMSTextBox = new System.Windows.Forms.TextBox();
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
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(39, 62);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Latitude DMS";
            // 
            // LatDMSTextBox
            // 
            this.LatDMSTextBox.Location = new System.Drawing.Point(43, 89);
            this.LatDMSTextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.LatDMSTextBox.Name = "LatDMSTextBox";
            this.LatDMSTextBox.Size = new System.Drawing.Size(176, 26);
            this.LatDMSTextBox.TabIndex = 1;
            this.LatDMSTextBox.DoubleClick += new System.EventHandler(this.LatDMSTextBox_DoubleClick);
            this.LatDMSTextBox.Validated += new System.EventHandler(this.LatDMSTextBox_Validated);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(39, 125);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(120, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "Longitude DMS";
            // 
            // LonDMSTextBox
            // 
            this.LonDMSTextBox.Location = new System.Drawing.Point(43, 151);
            this.LonDMSTextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.LonDMSTextBox.Name = "LonDMSTextBox";
            this.LonDMSTextBox.Size = new System.Drawing.Size(176, 26);
            this.LonDMSTextBox.TabIndex = 3;
            this.LonDMSTextBox.DoubleClick += new System.EventHandler(this.LonDMSTextBox_DoubleClick);
            this.LonDMSTextBox.Validated += new System.EventHandler(this.LonDMSTextBox_Validated);
            // 
            // LonDecTextBox
            // 
            this.LonDecTextBox.Location = new System.Drawing.Point(393, 151);
            this.LonDecTextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.LonDecTextBox.Name = "LonDecTextBox";
            this.LonDecTextBox.Size = new System.Drawing.Size(176, 26);
            this.LonDecTextBox.TabIndex = 7;
            this.LonDecTextBox.DoubleClick += new System.EventHandler(this.LonDecTextBox_DoubleClick);
            this.LonDecTextBox.Validated += new System.EventHandler(this.LonDecTextBox_Validated);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(389, 125);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(141, 20);
            this.label3.TabIndex = 6;
            this.label3.Text = "Longitude Decimal";
            // 
            // LatDecTextBox
            // 
            this.LatDecTextBox.Location = new System.Drawing.Point(393, 89);
            this.LatDecTextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.LatDecTextBox.Name = "LatDecTextBox";
            this.LatDecTextBox.Size = new System.Drawing.Size(176, 26);
            this.LatDecTextBox.TabIndex = 5;
            this.LatDecTextBox.DoubleClick += new System.EventHandler(this.LatDecTextBox_DoubleClick);
            this.LatDecTextBox.Validated += new System.EventHandler(this.LatDecTextBox_Validated);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(389, 62);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(128, 20);
            this.label4.TabIndex = 4;
            this.label4.Text = "Latitude Decimal";
            // 
            // DMSTextBox
            // 
            this.DMSTextBox.Location = new System.Drawing.Point(43, 264);
            this.DMSTextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.DMSTextBox.Name = "DMSTextBox";
            this.DMSTextBox.Size = new System.Drawing.Size(331, 26);
            this.DMSTextBox.TabIndex = 8;
            this.DMSTextBox.DoubleClick += new System.EventHandler(this.DMSTextBox_DoubleClick);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(43, 235);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(135, 20);
            this.label5.TabIndex = 9;
            this.label5.Text = "DMS Coordinates";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(393, 235);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(156, 20);
            this.label6.TabIndex = 11;
            this.label6.Text = "Decimal Coordinates";
            // 
            // DECTextBox
            // 
            this.DECTextBox.Location = new System.Drawing.Point(393, 264);
            this.DECTextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.DECTextBox.Name = "DECTextBox";
            this.DECTextBox.Size = new System.Drawing.Size(331, 26);
            this.DECTextBox.TabIndex = 10;
            this.DECTextBox.DoubleClick += new System.EventHandler(this.DECTextBox_DoubleClick);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(731, 235);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(130, 20);
            this.label7.TabIndex = 17;
            this.label7.Text = "SCT Coordinates";
            // 
            // SCTTextBox
            // 
            this.SCTTextBox.Location = new System.Drawing.Point(731, 264);
            this.SCTTextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.SCTTextBox.Name = "SCTTextBox";
            this.SCTTextBox.Size = new System.Drawing.Size(331, 26);
            this.SCTTextBox.TabIndex = 16;
            this.SCTTextBox.DoubleClick += new System.EventHandler(this.SCTTextBox_DoubleClick);
            // 
            // LonSCTTextBox
            // 
            this.LonSCTTextBox.Location = new System.Drawing.Point(731, 151);
            this.LonSCTTextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.LonSCTTextBox.Name = "LonSCTTextBox";
            this.LonSCTTextBox.Size = new System.Drawing.Size(176, 26);
            this.LonSCTTextBox.TabIndex = 15;
            this.LonSCTTextBox.DoubleClick += new System.EventHandler(this.LonSCTTextBox_DoubleClick);
            this.LonSCTTextBox.Validated += new System.EventHandler(this.LonSCTTextBox_Validated);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(728, 125);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(115, 20);
            this.label8.TabIndex = 14;
            this.label8.Text = "Longitude SCT";
            // 
            // LatSCTTextBox
            // 
            this.LatSCTTextBox.Location = new System.Drawing.Point(731, 89);
            this.LatSCTTextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.LatSCTTextBox.Name = "LatSCTTextBox";
            this.LatSCTTextBox.Size = new System.Drawing.Size(176, 26);
            this.LatSCTTextBox.TabIndex = 13;
            this.LatSCTTextBox.DoubleClick += new System.EventHandler(this.LatSCTTextBox_DoubleClick);
            this.LatSCTTextBox.Validated += new System.EventHandler(this.LatSCTTextBox_Validated);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(728, 62);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(102, 20);
            this.label9.TabIndex = 12;
            this.label9.Text = "Latitude SCT";
            // 
            // ParseSCTInsertButton
            // 
            this.ParseSCTInsertButton.Location = new System.Drawing.Point(926, 225);
            this.ParseSCTInsertButton.Name = "ParseSCTInsertButton";
            this.ParseSCTInsertButton.Size = new System.Drawing.Size(136, 30);
            this.ParseSCTInsertButton.TabIndex = 18;
            this.ParseSCTInsertButton.Text = "Convert all";
            this.toolTip1.SetToolTip(this.ParseSCTInsertButton, "Parses line below to ALL boxes and copies Decimal Lat/Lon to clipboard");
            this.ParseSCTInsertButton.UseVisualStyleBackColor = true;
            this.ParseSCTInsertButton.Click += new System.EventHandler(this.ParseSCTInsertButton_Click);
            // 
            // PasteButton
            // 
            this.PasteButton.Location = new System.Drawing.Point(926, 189);
            this.PasteButton.Name = "PasteButton";
            this.PasteButton.Size = new System.Drawing.Size(136, 30);
            this.PasteButton.TabIndex = 19;
            this.PasteButton.Text = "Paste from VRC";
            this.toolTip1.SetToolTip(this.PasteButton, "Pastes clipboard to line below");
            this.PasteButton.UseVisualStyleBackColor = true;
            this.PasteButton.Click += new System.EventHandler(this.PasteButton_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.PowderBlue;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(317, 9);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(364, 25);
            this.label10.TabIndex = 20;
            this.label10.Text = "Double click any box to copy to clipboard";
            // 
            // DMS_DecDeg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.CornflowerBlue;
            this.ClientSize = new System.Drawing.Size(1096, 312);
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
            this.Controls.Add(this.LonDMSTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.LatDMSTextBox);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "DMS_DecDeg";
            this.Text = "Coordinate Converter";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox LatDMSTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox LonDMSTextBox;
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
    }
}