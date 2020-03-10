namespace SCTBuilder
{
    partial class XML2SCT
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XML2SCT));
            this.XML2SCTLabel = new System.Windows.Forms.Label();
            this.XML2SCTInstructionLabel = new System.Windows.Forms.Label();
            this.XMLOpenFileDialogButton = new System.Windows.Forms.Button();
            this.SCTFileTextBox = new System.Windows.Forms.TextBox();
            this.txtOutputFolder_label = new System.Windows.Forms.Label();
            this.SourceFileTextBox = new System.Windows.Forms.TextBox();
            this.txtDataFolder_label = new System.Windows.Forms.Label();
            this.ConvertXML2SCTButton = new System.Windows.Forms.Button();
            this.resultTextBox = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SSDLabelsCheckBox = new System.Windows.Forms.CheckBox();
            this.SSDRadioButton = new System.Windows.Forms.RadioButton();
            this.ARTCCRadioButton = new System.Windows.Forms.RadioButton();
            this.AirwayRadioButton = new System.Windows.Forms.RadioButton();
            this.RunwayRadioButton = new System.Windows.Forms.RadioButton();
            this.AirportRadioButton = new System.Windows.Forms.RadioButton();
            this.FIXRadioButton = new System.Windows.Forms.RadioButton();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // XML2SCTLabel
            // 
            this.XML2SCTLabel.AutoSize = true;
            this.XML2SCTLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.XML2SCTLabel.Location = new System.Drawing.Point(12, 9);
            this.XML2SCTLabel.Name = "XML2SCTLabel";
            this.XML2SCTLabel.Size = new System.Drawing.Size(272, 29);
            this.XML2SCTLabel.TabIndex = 0;
            this.XML2SCTLabel.Text = "XML to SCT Conversion\r\n";
            // 
            // XML2SCTInstructionLabel
            // 
            this.XML2SCTInstructionLabel.AutoSize = true;
            this.XML2SCTInstructionLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.XML2SCTInstructionLabel.Location = new System.Drawing.Point(36, 61);
            this.XML2SCTInstructionLabel.Name = "XML2SCTInstructionLabel";
            this.XML2SCTInstructionLabel.Size = new System.Drawing.Size(711, 104);
            this.XML2SCTInstructionLabel.TabIndex = 1;
            this.XML2SCTInstructionLabel.Text = resources.GetString("XML2SCTInstructionLabel.Text");
            // 
            // XMLOpenFileDialogButton
            // 
            this.XMLOpenFileDialogButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.XMLOpenFileDialogButton.Location = new System.Drawing.Point(564, 186);
            this.XMLOpenFileDialogButton.Name = "XMLOpenFileDialogButton";
            this.XMLOpenFileDialogButton.Size = new System.Drawing.Size(55, 34);
            this.XMLOpenFileDialogButton.TabIndex = 12;
            this.XMLOpenFileDialogButton.Text = "...";
            this.XMLOpenFileDialogButton.UseVisualStyleBackColor = true;
            this.XMLOpenFileDialogButton.Click += new System.EventHandler(this.XMLOpenFileDialogButton_Click);
            // 
            // SCTFileTextBox
            // 
            this.SCTFileTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SCTFileTextBox.Location = new System.Drawing.Point(194, 223);
            this.SCTFileTextBox.Name = "SCTFileTextBox";
            this.SCTFileTextBox.Size = new System.Drawing.Size(425, 31);
            this.SCTFileTextBox.TabIndex = 11;
            this.toolTip1.SetToolTip(this.SCTFileTextBox, "If no output file is entered, a new file will be created with the same location a" +
        "nd name of the XML file adding the extension \"SCT\"");
            // 
            // txtOutputFolder_label
            // 
            this.txtOutputFolder_label.AutoSize = true;
            this.txtOutputFolder_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOutputFolder_label.Location = new System.Drawing.Point(43, 226);
            this.txtOutputFolder_label.Name = "txtOutputFolder_label";
            this.txtOutputFolder_label.Size = new System.Drawing.Size(154, 25);
            this.txtOutputFolder_label.TabIndex = 10;
            this.txtOutputFolder_label.Text = "SCT Filename:";
            // 
            // SourceFileTextBox
            // 
            this.SourceFileTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SourceFileTextBox.Location = new System.Drawing.Point(195, 185);
            this.SourceFileTextBox.Name = "SourceFileTextBox";
            this.SourceFileTextBox.Size = new System.Drawing.Size(361, 31);
            this.SourceFileTextBox.TabIndex = 9;
            this.SourceFileTextBox.DoubleClick += new System.EventHandler(this.XMLOpenFileDialogButton_Click);
            // 
            // txtDataFolder_label
            // 
            this.txtDataFolder_label.AutoSize = true;
            this.txtDataFolder_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDataFolder_label.Location = new System.Drawing.Point(40, 191);
            this.txtDataFolder_label.Name = "txtDataFolder_label";
            this.txtDataFolder_label.Size = new System.Drawing.Size(156, 25);
            this.txtDataFolder_label.TabIndex = 8;
            this.txtDataFolder_label.Text = "XML Filename:";
            // 
            // ConvertXML2SCTButton
            // 
            this.ConvertXML2SCTButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ConvertXML2SCTButton.Location = new System.Drawing.Point(625, 185);
            this.ConvertXML2SCTButton.Name = "ConvertXML2SCTButton";
            this.ConvertXML2SCTButton.Size = new System.Drawing.Size(132, 69);
            this.ConvertXML2SCTButton.TabIndex = 14;
            this.ConvertXML2SCTButton.Text = "Convert file";
            this.ConvertXML2SCTButton.UseVisualStyleBackColor = true;
            this.ConvertXML2SCTButton.Click += new System.EventHandler(this.ConvertXML2SCTButton_Click);
            // 
            // resultTextBox
            // 
            this.resultTextBox.Location = new System.Drawing.Point(47, 356);
            this.resultTextBox.Name = "resultTextBox";
            this.resultTextBox.Size = new System.Drawing.Size(710, 26);
            this.resultTextBox.TabIndex = 15;
            this.resultTextBox.Visible = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.SSDLabelsCheckBox);
            this.groupBox1.Controls.Add(this.SSDRadioButton);
            this.groupBox1.Controls.Add(this.ARTCCRadioButton);
            this.groupBox1.Controls.Add(this.AirwayRadioButton);
            this.groupBox1.Controls.Add(this.RunwayRadioButton);
            this.groupBox1.Controls.Add(this.AirportRadioButton);
            this.groupBox1.Controls.Add(this.FIXRadioButton);
            this.groupBox1.Location = new System.Drawing.Point(48, 260);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(704, 90);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Desired SCT format";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(507, 20);
            this.label1.TabIndex = 8;
            this.label1.Text = "<======== Labels are automatically added if applicable. ==========>";
            // 
            // SSDLabelsCheckBox
            // 
            this.SSDLabelsCheckBox.AutoSize = true;
            this.SSDLabelsCheckBox.Enabled = false;
            this.SSDLabelsCheckBox.Location = new System.Drawing.Point(567, 52);
            this.SSDLabelsCheckBox.Name = "SSDLabelsCheckBox";
            this.SSDLabelsCheckBox.Size = new System.Drawing.Size(123, 24);
            this.SSDLabelsCheckBox.TabIndex = 7;
            this.SSDLabelsCheckBox.Text = "Draw Labels";
            this.toolTip1.SetToolTip(this.SSDLabelsCheckBox, "Lebals are onto the diagrams. For many labels this can take a very long time.");
            this.SSDLabelsCheckBox.UseVisualStyleBackColor = true;
            // 
            // SSDRadioButton
            // 
            this.SSDRadioButton.AutoSize = true;
            this.SSDRadioButton.Location = new System.Drawing.Point(567, 22);
            this.SSDRadioButton.Name = "SSDRadioButton";
            this.SSDRadioButton.Size = new System.Drawing.Size(110, 24);
            this.SSDRadioButton.TabIndex = 5;
            this.SSDRadioButton.TabStop = true;
            this.SSDRadioButton.Text = "SID|STAR";
            this.SSDRadioButton.UseVisualStyleBackColor = true;
            this.SSDRadioButton.CheckedChanged += new System.EventHandler(this.SSDRadioButton_CheckedChanged);
            // 
            // ARTCCRadioButton
            // 
            this.ARTCCRadioButton.AutoSize = true;
            this.ARTCCRadioButton.Location = new System.Drawing.Point(434, 22);
            this.ARTCCRadioButton.Name = "ARTCCRadioButton";
            this.ARTCCRadioButton.Size = new System.Drawing.Size(127, 24);
            this.ARTCCRadioButton.TabIndex = 4;
            this.ARTCCRadioButton.TabStop = true;
            this.ARTCCRadioButton.Text = "ARTCC|SUA";
            this.ARTCCRadioButton.UseVisualStyleBackColor = true;
            // 
            // AirwayRadioButton
            // 
            this.AirwayRadioButton.AutoSize = true;
            this.AirwayRadioButton.Location = new System.Drawing.Point(348, 22);
            this.AirwayRadioButton.Name = "AirwayRadioButton";
            this.AirwayRadioButton.Size = new System.Drawing.Size(80, 24);
            this.AirwayRadioButton.TabIndex = 3;
            this.AirwayRadioButton.TabStop = true;
            this.AirwayRadioButton.Text = "Airway";
            this.AirwayRadioButton.UseVisualStyleBackColor = true;
            // 
            // RunwayRadioButton
            // 
            this.RunwayRadioButton.AutoSize = true;
            this.RunwayRadioButton.Location = new System.Drawing.Point(251, 22);
            this.RunwayRadioButton.Name = "RunwayRadioButton";
            this.RunwayRadioButton.Size = new System.Drawing.Size(91, 24);
            this.RunwayRadioButton.TabIndex = 2;
            this.RunwayRadioButton.TabStop = true;
            this.RunwayRadioButton.Text = "Runway";
            this.RunwayRadioButton.UseVisualStyleBackColor = true;
            // 
            // AirportRadioButton
            // 
            this.AirportRadioButton.AutoSize = true;
            this.AirportRadioButton.Location = new System.Drawing.Point(164, 22);
            this.AirportRadioButton.Name = "AirportRadioButton";
            this.AirportRadioButton.Size = new System.Drawing.Size(81, 24);
            this.AirportRadioButton.TabIndex = 1;
            this.AirportRadioButton.TabStop = true;
            this.AirportRadioButton.Text = "Airport";
            this.AirportRadioButton.UseVisualStyleBackColor = true;
            // 
            // FIXRadioButton
            // 
            this.FIXRadioButton.AutoSize = true;
            this.FIXRadioButton.Checked = true;
            this.FIXRadioButton.Location = new System.Drawing.Point(19, 22);
            this.FIXRadioButton.Name = "FIXRadioButton";
            this.FIXRadioButton.Size = new System.Drawing.Size(139, 24);
            this.FIXRadioButton.TabIndex = 0;
            this.FIXRadioButton.TabStop = true;
            this.FIXRadioButton.Text = "VOR|NDB|FIX";
            this.toolTip1.SetToolTip(this.FIXRadioButton, "Standard SCT VOR/NDB/FIX format. Labels are created by VRC.  A [LABELS] section c" +
        "an be added for other applications.");
            this.FIXRadioButton.UseVisualStyleBackColor = true;
            // 
            // XML2SCT
            // 
            this.AcceptButton = this.ConvertXML2SCTButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.resultTextBox);
            this.Controls.Add(this.ConvertXML2SCTButton);
            this.Controls.Add(this.XMLOpenFileDialogButton);
            this.Controls.Add(this.SCTFileTextBox);
            this.Controls.Add(this.txtOutputFolder_label);
            this.Controls.Add(this.SourceFileTextBox);
            this.Controls.Add(this.txtDataFolder_label);
            this.Controls.Add(this.XML2SCTInstructionLabel);
            this.Controls.Add(this.XML2SCTLabel);
            this.MaximizeBox = false;
            this.Name = "XML2SCT";
            this.Text = "FEBU -> XML 2 SCT Conversion";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label XML2SCTLabel;
        private System.Windows.Forms.Label XML2SCTInstructionLabel;
        private System.Windows.Forms.Button XMLOpenFileDialogButton;
        private System.Windows.Forms.TextBox SCTFileTextBox;
        private System.Windows.Forms.Label txtOutputFolder_label;
        private System.Windows.Forms.TextBox SourceFileTextBox;
        private System.Windows.Forms.Label txtDataFolder_label;
        private System.Windows.Forms.Button ConvertXML2SCTButton;
        private System.Windows.Forms.TextBox resultTextBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox SSDLabelsCheckBox;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.RadioButton SSDRadioButton;
        private System.Windows.Forms.RadioButton ARTCCRadioButton;
        private System.Windows.Forms.RadioButton AirwayRadioButton;
        private System.Windows.Forms.RadioButton RunwayRadioButton;
        private System.Windows.Forms.RadioButton AirportRadioButton;
        private System.Windows.Forms.RadioButton FIXRadioButton;
    }
}