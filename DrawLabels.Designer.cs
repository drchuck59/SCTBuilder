namespace SCTBuilder
{
    partial class DrawLabels
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
            this.Label1Label = new System.Windows.Forms.Label();
            this.LabelTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.LatTextBox = new System.Windows.Forms.TextBox();
            this.LonTextBox = new System.Windows.Forms.TextBox();
            this.FixImportGroupBox = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.IncludeSymbolCheckBox = new System.Windows.Forms.CheckBox();
            this.FixListDataGridView = new System.Windows.Forms.DataGridView();
            this.IdentifierLabel = new System.Windows.Forms.Label();
            this.IdentifierTextBox = new System.Windows.Forms.TextBox();
            this.DrawButton = new System.Windows.Forms.Button();
            this.OutputTextBox = new System.Windows.Forms.TextBox();
            this.Copy2ClipboardButton = new System.Windows.Forms.Button();
            this.ClearOutputButton = new System.Windows.Forms.Button();
            this.SaveOutput2FileButton = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.BearingTextBox = new System.Windows.Forms.TextBox();
            this.ScaleTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.FixImportGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FixListDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // Label1Label
            // 
            this.Label1Label.AutoSize = true;
            this.Label1Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label1Label.Location = new System.Drawing.Point(41, 15);
            this.Label1Label.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Label1Label.Name = "Label1Label";
            this.Label1Label.Size = new System.Drawing.Size(43, 17);
            this.Label1Label.TabIndex = 0;
            this.Label1Label.Text = "Label";
            // 
            // LabelTextBox
            // 
            this.LabelTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelTextBox.Location = new System.Drawing.Point(85, 12);
            this.LabelTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.LabelTextBox.Name = "LabelTextBox";
            this.LabelTextBox.Size = new System.Drawing.Size(135, 23);
            this.LabelTextBox.TabIndex = 1;
            this.toolTip1.SetToolTip(this.LabelTextBox, "Limited to printable ASCII chars");
            this.LabelTextBox.Click += new System.EventHandler(this.LabelTextBox_Click);
            this.LabelTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.LabelTextBox_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(27, 39);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Latitude";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(15, 63);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "Longitude";
            // 
            // LatTextBox
            // 
            this.LatTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LatTextBox.Location = new System.Drawing.Point(85, 38);
            this.LatTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.LatTextBox.Name = "LatTextBox";
            this.LatTextBox.Size = new System.Drawing.Size(135, 23);
            this.LatTextBox.TabIndex = 4;
            this.LatTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.LatTextBox_KeyPress);
            this.LatTextBox.Validated += new System.EventHandler(this.LatTextBox_Validated);
            // 
            // LonTextBox
            // 
            this.LonTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LonTextBox.Location = new System.Drawing.Point(85, 65);
            this.LonTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.LonTextBox.Name = "LonTextBox";
            this.LonTextBox.Size = new System.Drawing.Size(135, 23);
            this.LonTextBox.TabIndex = 5;
            this.LonTextBox.Validated += new System.EventHandler(this.LonTextBox_Validated);
            // 
            // FixImportGroupBox
            // 
            this.FixImportGroupBox.Controls.Add(this.label5);
            this.FixImportGroupBox.Controls.Add(this.IncludeSymbolCheckBox);
            this.FixImportGroupBox.Controls.Add(this.FixListDataGridView);
            this.FixImportGroupBox.Controls.Add(this.IdentifierLabel);
            this.FixImportGroupBox.Controls.Add(this.IdentifierTextBox);
            this.FixImportGroupBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FixImportGroupBox.Location = new System.Drawing.Point(232, 11);
            this.FixImportGroupBox.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.FixImportGroupBox.Name = "FixImportGroupBox";
            this.FixImportGroupBox.Padding = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.FixImportGroupBox.Size = new System.Drawing.Size(228, 234);
            this.FixImportGroupBox.TabIndex = 9;
            this.FixImportGroupBox.TabStop = false;
            this.FixImportGroupBox.Text = "Import From Fix";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(148, 15);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(69, 26);
            this.label5.TabIndex = 21;
            this.label5.Text = "(Double click\r\nrow to insert)";
            // 
            // IncludeSymbolCheckBox
            // 
            this.IncludeSymbolCheckBox.AutoSize = true;
            this.IncludeSymbolCheckBox.Location = new System.Drawing.Point(14, 203);
            this.IncludeSymbolCheckBox.Name = "IncludeSymbolCheckBox";
            this.IncludeSymbolCheckBox.Size = new System.Drawing.Size(175, 21);
            this.IncludeSymbolCheckBox.TabIndex = 20;
            this.IncludeSymbolCheckBox.Text = "Include Symbol over Fix";
            this.IncludeSymbolCheckBox.UseVisualStyleBackColor = true;
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
            this.FixListDataGridView.Size = new System.Drawing.Size(203, 154);
            this.FixListDataGridView.TabIndex = 2;
            this.toolTip1.SetToolTip(this.FixListDataGridView, "Double click fix to copy");
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
            // 
            // DrawButton
            // 
            this.DrawButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DrawButton.Location = new System.Drawing.Point(38, 214);
            this.DrawButton.Margin = new System.Windows.Forms.Padding(2);
            this.DrawButton.Name = "DrawButton";
            this.DrawButton.Size = new System.Drawing.Size(162, 31);
            this.DrawButton.TabIndex = 11;
            this.DrawButton.Text = "Generate Text Drawing";
            this.DrawButton.UseVisualStyleBackColor = true;
            this.DrawButton.Click += new System.EventHandler(this.DrawButton_Click);
            // 
            // OutputTextBox
            // 
            this.OutputTextBox.Location = new System.Drawing.Point(38, 250);
            this.OutputTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.OutputTextBox.Multiline = true;
            this.OutputTextBox.Name = "OutputTextBox";
            this.OutputTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.OutputTextBox.Size = new System.Drawing.Size(428, 454);
            this.OutputTextBox.TabIndex = 12;
            this.OutputTextBox.WordWrap = false;
            // 
            // Copy2ClipboardButton
            // 
            this.Copy2ClipboardButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Copy2ClipboardButton.Location = new System.Drawing.Point(239, 705);
            this.Copy2ClipboardButton.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.Copy2ClipboardButton.Name = "Copy2ClipboardButton";
            this.Copy2ClipboardButton.Size = new System.Drawing.Size(79, 52);
            this.Copy2ClipboardButton.TabIndex = 13;
            this.Copy2ClipboardButton.Text = "Copy to Clipboard";
            this.Copy2ClipboardButton.UseVisualStyleBackColor = true;
            this.Copy2ClipboardButton.Click += new System.EventHandler(this.Copy2ClipboardButton_Click);
            // 
            // ClearOutputButton
            // 
            this.ClearOutputButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.ClearOutputButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ClearOutputButton.Location = new System.Drawing.Point(389, 705);
            this.ClearOutputButton.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.ClearOutputButton.Name = "ClearOutputButton";
            this.ClearOutputButton.Size = new System.Drawing.Size(78, 52);
            this.ClearOutputButton.TabIndex = 15;
            this.ClearOutputButton.Text = "Clear Contents";
            this.ClearOutputButton.UseVisualStyleBackColor = false;
            this.ClearOutputButton.Click += new System.EventHandler(this.ClearOutputButton_Click);
            // 
            // SaveOutput2FileButton
            // 
            this.SaveOutput2FileButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SaveOutput2FileButton.Location = new System.Drawing.Point(313, 705);
            this.SaveOutput2FileButton.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.SaveOutput2FileButton.Name = "SaveOutput2FileButton";
            this.SaveOutput2FileButton.Size = new System.Drawing.Size(81, 52);
            this.SaveOutput2FileButton.TabIndex = 14;
            this.SaveOutput2FileButton.Text = "Save to File";
            this.SaveOutput2FileButton.UseVisualStyleBackColor = true;
            this.SaveOutput2FileButton.Click += new System.EventHandler(this.SaveOutput2FileButton_Click);
            // 
            // BearingTextBox
            // 
            this.BearingTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BearingTextBox.Location = new System.Drawing.Point(85, 94);
            this.BearingTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.BearingTextBox.MaxLength = 3;
            this.BearingTextBox.Name = "BearingTextBox";
            this.BearingTextBox.Size = new System.Drawing.Size(38, 23);
            this.BearingTextBox.TabIndex = 17;
            this.BearingTextBox.Text = "90";
            this.toolTip1.SetToolTip(this.BearingTextBox, "Default bearing is horizontal");
            this.BearingTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.BearingTextBox_KeyPress);
            this.BearingTextBox.Validated += new System.EventHandler(this.BearingTextBox_Validated);
            // 
            // ScaleTextBox
            // 
            this.ScaleTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ScaleTextBox.Location = new System.Drawing.Point(182, 94);
            this.ScaleTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.ScaleTextBox.Name = "ScaleTextBox";
            this.ScaleTextBox.Size = new System.Drawing.Size(38, 23);
            this.ScaleTextBox.TabIndex = 19;
            this.ScaleTextBox.Text = "1.0";
            this.toolTip1.SetToolTip(this.ScaleTextBox, "Scaling only! Default 1=0.25 NM square");
            this.ScaleTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ScaleTextBox_KeyPress);
            this.ScaleTextBox.Validated += new System.EventHandler(this.ScaleTextBox_Validated);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(27, 96);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 17);
            this.label3.TabIndex = 16;
            this.label3.Text = "Bearing";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(140, 96);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 17);
            this.label4.TabIndex = 18;
            this.label4.Text = "Scale";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(41, 119);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(174, 26);
            this.label6.TabIndex = 20;
            this.label6.Text = "NOTE: Label can be changed after\r\n            inserting FIX information.";
            // 
            // DrawLabels
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(519, 764);
            this.Controls.Add(this.FixImportGroupBox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.ScaleTextBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.BearingTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Copy2ClipboardButton);
            this.Controls.Add(this.ClearOutputButton);
            this.Controls.Add(this.SaveOutput2FileButton);
            this.Controls.Add(this.OutputTextBox);
            this.Controls.Add(this.DrawButton);
            this.Controls.Add(this.LonTextBox);
            this.Controls.Add(this.LatTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.LabelTextBox);
            this.Controls.Add(this.Label1Label);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "DrawLabels";
            this.Text = "Draw Labels";
            this.Load += new System.EventHandler(this.DrawLabels_Load);
            this.FixImportGroupBox.ResumeLayout(false);
            this.FixImportGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FixListDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Label1Label;
        private System.Windows.Forms.TextBox LabelTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox LatTextBox;
        private System.Windows.Forms.TextBox LonTextBox;
        private System.Windows.Forms.GroupBox FixImportGroupBox;
        private System.Windows.Forms.DataGridView FixListDataGridView;
        private System.Windows.Forms.Label IdentifierLabel;
        private System.Windows.Forms.TextBox IdentifierTextBox;
        private System.Windows.Forms.Button DrawButton;
        private System.Windows.Forms.TextBox OutputTextBox;
        private System.Windows.Forms.Button Copy2ClipboardButton;
        private System.Windows.Forms.Button ClearOutputButton;
        private System.Windows.Forms.Button SaveOutput2FileButton;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.TextBox BearingTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox ScaleTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox IncludeSymbolCheckBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
    }
}