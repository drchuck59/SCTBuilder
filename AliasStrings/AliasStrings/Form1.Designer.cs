namespace AliasStrings
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Orig_ComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.OrigARTCC_TextBox = new System.Windows.Forms.TextBox();
            this.Filter_groupBox = new System.Windows.Forms.GroupBox();
            this.Dest_RadioButton = new System.Windows.Forms.RadioButton();
            this.Origin_RadioButton = new System.Windows.Forms.RadioButton();
            this.Types_GroupBox = new System.Windows.Forms.GroupBox();
            this.Type_TEC_checkBox = new System.Windows.Forms.CheckBox();
            this.Type_SHD_checkBox = new System.Windows.Forms.CheckBox();
            this.Type_HSD_checkBox = new System.Windows.Forms.CheckBox();
            this.Type_H_checkBox = new System.Windows.Forms.CheckBox();
            this.Type_SLD_checkBox = new System.Windows.Forms.CheckBox();
            this.Type_LSD_checkBox = new System.Windows.Forms.CheckBox();
            this.Type_L_checkBox = new System.Windows.Forms.CheckBox();
            this.Type_All_checkBox = new System.Windows.Forms.CheckBox();
            this.Message_TextBox = new System.Windows.Forms.TextBox();
            this.Clip_button = new System.Windows.Forms.Button();
            this.ARTCC_ComboBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.Alias_button = new System.Windows.Forms.Button();
            this.Copied_label = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.Filter_groupBox.SuspendLayout();
            this.Types_GroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridView1.Location = new System.Drawing.Point(21, 128);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 25;
            this.dataGridView1.Size = new System.Drawing.Size(776, 427);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            // 
            // Orig_ComboBox
            // 
            this.Orig_ComboBox.FormattingEnabled = true;
            this.Orig_ComboBox.Location = new System.Drawing.Point(116, 99);
            this.Orig_ComboBox.Name = "Orig_ComboBox";
            this.Orig_ComboBox.Size = new System.Drawing.Size(52, 23);
            this.Orig_ComboBox.TabIndex = 1;
            this.Orig_ComboBox.SelectedIndexChanged += new System.EventHandler(this.Orig_ComboBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 107);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "3-letter Identifier";
            // 
            // OrigARTCC_TextBox
            // 
            this.OrigARTCC_TextBox.Enabled = false;
            this.OrigARTCC_TextBox.Location = new System.Drawing.Point(174, 99);
            this.OrigARTCC_TextBox.Name = "OrigARTCC_TextBox";
            this.OrigARTCC_TextBox.Size = new System.Drawing.Size(40, 23);
            this.OrigARTCC_TextBox.TabIndex = 5;
            // 
            // Filter_groupBox
            // 
            this.Filter_groupBox.Controls.Add(this.Dest_RadioButton);
            this.Filter_groupBox.Controls.Add(this.Origin_RadioButton);
            this.Filter_groupBox.Location = new System.Drawing.Point(12, 12);
            this.Filter_groupBox.Name = "Filter_groupBox";
            this.Filter_groupBox.Size = new System.Drawing.Size(219, 52);
            this.Filter_groupBox.TabIndex = 7;
            this.Filter_groupBox.TabStop = false;
            this.Filter_groupBox.Text = "Filter by";
            // 
            // Dest_RadioButton
            // 
            this.Dest_RadioButton.AutoSize = true;
            this.Dest_RadioButton.Location = new System.Drawing.Point(79, 22);
            this.Dest_RadioButton.Name = "Dest_RadioButton";
            this.Dest_RadioButton.Size = new System.Drawing.Size(85, 19);
            this.Dest_RadioButton.TabIndex = 1;
            this.Dest_RadioButton.TabStop = true;
            this.Dest_RadioButton.Text = "Destination";
            this.Dest_RadioButton.UseVisualStyleBackColor = true;
            this.Dest_RadioButton.CheckedChanged += new System.EventHandler(this.Dest_RadioButton_CheckedChanged);
            // 
            // Origin_RadioButton
            // 
            this.Origin_RadioButton.AutoSize = true;
            this.Origin_RadioButton.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            this.Origin_RadioButton.Checked = true;
            this.Origin_RadioButton.Location = new System.Drawing.Point(14, 21);
            this.Origin_RadioButton.Name = "Origin_RadioButton";
            this.Origin_RadioButton.Size = new System.Drawing.Size(58, 19);
            this.Origin_RadioButton.TabIndex = 0;
            this.Origin_RadioButton.TabStop = true;
            this.Origin_RadioButton.Text = "Origin";
            this.Origin_RadioButton.UseVisualStyleBackColor = true;
            this.Origin_RadioButton.CheckedChanged += new System.EventHandler(this.Origin_RadioButton_CheckedChanged);
            // 
            // Types_GroupBox
            // 
            this.Types_GroupBox.Controls.Add(this.Type_TEC_checkBox);
            this.Types_GroupBox.Controls.Add(this.Type_SHD_checkBox);
            this.Types_GroupBox.Controls.Add(this.Type_HSD_checkBox);
            this.Types_GroupBox.Controls.Add(this.Type_H_checkBox);
            this.Types_GroupBox.Controls.Add(this.Type_SLD_checkBox);
            this.Types_GroupBox.Controls.Add(this.Type_LSD_checkBox);
            this.Types_GroupBox.Controls.Add(this.Type_L_checkBox);
            this.Types_GroupBox.Controls.Add(this.Type_All_checkBox);
            this.Types_GroupBox.Location = new System.Drawing.Point(237, 12);
            this.Types_GroupBox.Name = "Types_GroupBox";
            this.Types_GroupBox.Size = new System.Drawing.Size(560, 93);
            this.Types_GroupBox.TabIndex = 8;
            this.Types_GroupBox.TabStop = false;
            this.Types_GroupBox.Text = "Route Types";
            // 
            // Type_TEC_checkBox
            // 
            this.Type_TEC_checkBox.AutoSize = true;
            this.Type_TEC_checkBox.Location = new System.Drawing.Point(411, 21);
            this.Type_TEC_checkBox.Name = "Type_TEC_checkBox";
            this.Type_TEC_checkBox.Size = new System.Drawing.Size(144, 19);
            this.Type_TEC_checkBox.TabIndex = 7;
            this.Type_TEC_checkBox.Tag = "TEC";
            this.Type_TEC_checkBox.Text = "Tower Enroute Control";
            this.Type_TEC_checkBox.UseVisualStyleBackColor = true;
            this.Type_TEC_checkBox.CheckedChanged += new System.EventHandler(this.Type_TEC_checkBox_CheckedChanged);
            // 
            // Type_SHD_checkBox
            // 
            this.Type_SHD_checkBox.AutoSize = true;
            this.Type_SHD_checkBox.Location = new System.Drawing.Point(238, 68);
            this.Type_SHD_checkBox.Name = "Type_SHD_checkBox";
            this.Type_SHD_checkBox.Size = new System.Drawing.Size(170, 19);
            this.Type_SHD_checkBox.TabIndex = 6;
            this.Type_SHD_checkBox.Tag = "SHD";
            this.Type_SHD_checkBox.Text = "Special High Alt Directional";
            this.Type_SHD_checkBox.UseVisualStyleBackColor = true;
            this.Type_SHD_checkBox.CheckedChanged += new System.EventHandler(this.Type_SHD_checkBox_CheckedChanged);
            // 
            // Type_HSD_checkBox
            // 
            this.Type_HSD_checkBox.AutoSize = true;
            this.Type_HSD_checkBox.Location = new System.Drawing.Point(238, 45);
            this.Type_HSD_checkBox.Name = "Type_HSD_checkBox";
            this.Type_HSD_checkBox.Size = new System.Drawing.Size(156, 19);
            this.Type_HSD_checkBox.TabIndex = 5;
            this.Type_HSD_checkBox.Tag = "HSD";
            this.Type_HSD_checkBox.Text = "High Alt Single Direction";
            this.Type_HSD_checkBox.UseVisualStyleBackColor = true;
            this.Type_HSD_checkBox.CheckedChanged += new System.EventHandler(this.Type_HSD_checkBox_CheckedChanged);
            // 
            // Type_H_checkBox
            // 
            this.Type_H_checkBox.AutoSize = true;
            this.Type_H_checkBox.Location = new System.Drawing.Point(239, 20);
            this.Type_H_checkBox.Name = "Type_H_checkBox";
            this.Type_H_checkBox.Size = new System.Drawing.Size(97, 19);
            this.Type_H_checkBox.TabIndex = 4;
            this.Type_H_checkBox.Tag = "H";
            this.Type_H_checkBox.Text = "High Altitude";
            this.Type_H_checkBox.UseVisualStyleBackColor = true;
            this.Type_H_checkBox.CheckedChanged += new System.EventHandler(this.Type_H_checkBox_CheckedChanged);
            // 
            // Type_SLD_checkBox
            // 
            this.Type_SLD_checkBox.AutoSize = true;
            this.Type_SLD_checkBox.Location = new System.Drawing.Point(60, 68);
            this.Type_SLD_checkBox.Name = "Type_SLD_checkBox";
            this.Type_SLD_checkBox.Size = new System.Drawing.Size(166, 19);
            this.Type_SLD_checkBox.TabIndex = 3;
            this.Type_SLD_checkBox.Tag = "SLD";
            this.Type_SLD_checkBox.Text = "Special Low Alt Directional";
            this.Type_SLD_checkBox.UseVisualStyleBackColor = true;
            this.Type_SLD_checkBox.CheckedChanged += new System.EventHandler(this.Type_SLD_checkBox_CheckedChanged);
            // 
            // Type_LSD_checkBox
            // 
            this.Type_LSD_checkBox.AutoSize = true;
            this.Type_LSD_checkBox.Location = new System.Drawing.Point(60, 45);
            this.Type_LSD_checkBox.Name = "Type_LSD_checkBox";
            this.Type_LSD_checkBox.Size = new System.Drawing.Size(152, 19);
            this.Type_LSD_checkBox.TabIndex = 2;
            this.Type_LSD_checkBox.Tag = "LSD";
            this.Type_LSD_checkBox.Text = "Low Alt Single Direction";
            this.Type_LSD_checkBox.UseVisualStyleBackColor = true;
            this.Type_LSD_checkBox.CheckedChanged += new System.EventHandler(this.Type_LSD_checkBox_CheckedChanged);
            // 
            // Type_L_checkBox
            // 
            this.Type_L_checkBox.AutoSize = true;
            this.Type_L_checkBox.Location = new System.Drawing.Point(60, 20);
            this.Type_L_checkBox.Name = "Type_L_checkBox";
            this.Type_L_checkBox.Size = new System.Drawing.Size(93, 19);
            this.Type_L_checkBox.TabIndex = 1;
            this.Type_L_checkBox.Tag = "L";
            this.Type_L_checkBox.Text = "Low Altitude";
            this.Type_L_checkBox.UseVisualStyleBackColor = true;
            this.Type_L_checkBox.CheckedChanged += new System.EventHandler(this.Type_L_checkBox_CheckedChanged);
            // 
            // Type_All_checkBox
            // 
            this.Type_All_checkBox.AutoSize = true;
            this.Type_All_checkBox.Checked = true;
            this.Type_All_checkBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Type_All_checkBox.Location = new System.Drawing.Point(11, 21);
            this.Type_All_checkBox.Name = "Type_All_checkBox";
            this.Type_All_checkBox.Size = new System.Drawing.Size(40, 19);
            this.Type_All_checkBox.TabIndex = 0;
            this.Type_All_checkBox.Tag = "All";
            this.Type_All_checkBox.Text = "All";
            this.Type_All_checkBox.UseVisualStyleBackColor = true;
            this.Type_All_checkBox.CheckedChanged += new System.EventHandler(this.Type_All_checkBox_CheckedChanged);
            // 
            // Message_TextBox
            // 
            this.Message_TextBox.Location = new System.Drawing.Point(21, 561);
            this.Message_TextBox.Multiline = true;
            this.Message_TextBox.Name = "Message_TextBox";
            this.Message_TextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.Message_TextBox.Size = new System.Drawing.Size(776, 90);
            this.Message_TextBox.TabIndex = 10;
            // 
            // Clip_button
            // 
            this.Clip_button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.Clip_button.Location = new System.Drawing.Point(21, 657);
            this.Clip_button.Name = "Clip_button";
            this.Clip_button.Size = new System.Drawing.Size(105, 31);
            this.Clip_button.TabIndex = 11;
            this.Clip_button.Text = "Build Message";
            this.Clip_button.UseVisualStyleBackColor = false;
            this.Clip_button.Click += new System.EventHandler(this.Clip_button_Click);
            // 
            // ARTCC_ComboBox
            // 
            this.ARTCC_ComboBox.FormattingEnabled = true;
            this.ARTCC_ComboBox.Location = new System.Drawing.Point(116, 70);
            this.ARTCC_ComboBox.Name = "ARTCC_ComboBox";
            this.ARTCC_ComboBox.Size = new System.Drawing.Size(52, 23);
            this.ARTCC_ComboBox.TabIndex = 12;
            this.ARTCC_ComboBox.SelectedIndexChanged += new System.EventHandler(this.ARTCC_ComboBox_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(68, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 15);
            this.label2.TabIndex = 13;
            this.label2.Text = "ARTCC";
            // 
            // Alias_button
            // 
            this.Alias_button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.Alias_button.Location = new System.Drawing.Point(132, 657);
            this.Alias_button.Name = "Alias_button";
            this.Alias_button.Size = new System.Drawing.Size(105, 31);
            this.Alias_button.TabIndex = 14;
            this.Alias_button.Text = "Copy as Alias";
            this.Alias_button.UseVisualStyleBackColor = false;
            this.Alias_button.Click += new System.EventHandler(this.Alias_button_Click);
            // 
            // Copied_label
            // 
            this.Copied_label.AutoSize = true;
            this.Copied_label.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.Copied_label.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.Copied_label.Location = new System.Drawing.Point(599, 657);
            this.Copied_label.Name = "Copied_label";
            this.Copied_label.Size = new System.Drawing.Size(193, 15);
            this.Copied_label.TabIndex = 15;
            this.Copied_label.Text = "Cell/Message copied to clipboard!";
            this.Copied_label.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(832, 691);
            this.Controls.Add(this.Copied_label);
            this.Controls.Add(this.Alias_button);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ARTCC_ComboBox);
            this.Controls.Add(this.Clip_button);
            this.Controls.Add(this.Message_TextBox);
            this.Controls.Add(this.Types_GroupBox);
            this.Controls.Add(this.OrigARTCC_TextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Orig_ComboBox);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.Filter_groupBox);
            this.Name = "Form1";
            this.Text = "FAA Preferred Route Alias Builder";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.Filter_groupBox.ResumeLayout(false);
            this.Filter_groupBox.PerformLayout();
            this.Types_GroupBox.ResumeLayout(false);
            this.Types_GroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DataGridView dataGridView1;
        private ComboBox Orig_ComboBox;
        private Label label1;
        private TextBox OrigARTCC_TextBox;
        private GroupBox Filter_groupBox;
        private RadioButton Dest_RadioButton;
        private RadioButton Origin_RadioButton;
        private GroupBox Types_GroupBox;
        private CheckBox Type_TEC_checkBox;
        private CheckBox Type_SHD_checkBox;
        private CheckBox Type_HSD_checkBox;
        private CheckBox Type_H_checkBox;
        private CheckBox Type_SLD_checkBox;
        private CheckBox Type_LSD_checkBox;
        private CheckBox Type_L_checkBox;
        private CheckBox Type_All_checkBox;
        private TextBox Message_TextBox;
        private Button Clip_button;
        private ComboBox ARTCC_ComboBox;
        private Label label2;
        private Button Alias_button;
        private Label Copied_label;
    }
}