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
            this.AlignLeftButton = new System.Windows.Forms.Button();
            this.AlignCenterButton = new System.Windows.Forms.Button();
            this.AlignRightButton = new System.Windows.Forms.Button();
            this.FixImportGroupBox = new System.Windows.Forms.GroupBox();
            this.FixListDataGridView = new System.Windows.Forms.DataGridView();
            this.IdentifierLabel = new System.Windows.Forms.Label();
            this.IdentifierTextBox = new System.Windows.Forms.TextBox();
            this.CopyClipButton = new System.Windows.Forms.Button();
            this.DrawButton = new System.Windows.Forms.Button();
            this.OutputTextBox = new System.Windows.Forms.TextBox();
            this.Copy2ClipboardButton = new System.Windows.Forms.Button();
            this.ClearOutputButton = new System.Windows.Forms.Button();
            this.SaveOutput2FileButton = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.BearingTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.ScaleTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.FixImportGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FixListDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // Label1Label
            // 
            this.Label1Label.AutoSize = true;
            this.Label1Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label1Label.Location = new System.Drawing.Point(61, 92);
            this.Label1Label.Name = "Label1Label";
            this.Label1Label.Size = new System.Drawing.Size(60, 25);
            this.Label1Label.TabIndex = 0;
            this.Label1Label.Text = "Label";
            // 
            // LabelTextBox
            // 
            this.LabelTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelTextBox.Location = new System.Drawing.Point(127, 87);
            this.LabelTextBox.Name = "LabelTextBox";
            this.LabelTextBox.Size = new System.Drawing.Size(200, 30);
            this.LabelTextBox.TabIndex = 1;
            this.LabelTextBox.Click += new System.EventHandler(this.LabelTextBox_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(40, 130);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 25);
            this.label1.TabIndex = 2;
            this.label1.Text = "Latitude";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(23, 166);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 25);
            this.label2.TabIndex = 3;
            this.label2.Text = "Longitude";
            // 
            // LatTextBox
            // 
            this.LatTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LatTextBox.Location = new System.Drawing.Point(127, 127);
            this.LatTextBox.Name = "LatTextBox";
            this.LatTextBox.Size = new System.Drawing.Size(200, 30);
            this.LatTextBox.TabIndex = 4;
            this.LatTextBox.Validated += new System.EventHandler(this.LatTextBox_Validated);
            // 
            // LonTextBox
            // 
            this.LonTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LonTextBox.Location = new System.Drawing.Point(127, 163);
            this.LonTextBox.Name = "LonTextBox";
            this.LonTextBox.Size = new System.Drawing.Size(200, 30);
            this.LonTextBox.TabIndex = 5;
            this.LonTextBox.Validated += new System.EventHandler(this.LonTextBox_Validated);
            // 
            // AlignLeftButton
            // 
            this.AlignLeftButton.Location = new System.Drawing.Point(333, 91);
            this.AlignLeftButton.Name = "AlignLeftButton";
            this.AlignLeftButton.Size = new System.Drawing.Size(75, 30);
            this.AlignLeftButton.TabIndex = 6;
            this.AlignLeftButton.Text = "Left";
            this.AlignLeftButton.UseVisualStyleBackColor = true;
            this.AlignLeftButton.Click += new System.EventHandler(this.AlignLeftButton_Click);
            // 
            // AlignCenterButton
            // 
            this.AlignCenterButton.Location = new System.Drawing.Point(414, 92);
            this.AlignCenterButton.Name = "AlignCenterButton";
            this.AlignCenterButton.Size = new System.Drawing.Size(75, 30);
            this.AlignCenterButton.TabIndex = 7;
            this.AlignCenterButton.Text = "Center";
            this.AlignCenterButton.UseVisualStyleBackColor = true;
            this.AlignCenterButton.Click += new System.EventHandler(this.AlignCenterButton_Click);
            // 
            // AlignRightButton
            // 
            this.AlignRightButton.Location = new System.Drawing.Point(495, 92);
            this.AlignRightButton.Name = "AlignRightButton";
            this.AlignRightButton.Size = new System.Drawing.Size(75, 30);
            this.AlignRightButton.TabIndex = 8;
            this.AlignRightButton.Text = "Right";
            this.AlignRightButton.UseVisualStyleBackColor = true;
            this.AlignRightButton.Click += new System.EventHandler(this.AlignRightButton_Click);
            // 
            // FixImportGroupBox
            // 
            this.FixImportGroupBox.Controls.Add(this.FixListDataGridView);
            this.FixImportGroupBox.Controls.Add(this.IdentifierLabel);
            this.FixImportGroupBox.Controls.Add(this.IdentifierTextBox);
            this.FixImportGroupBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FixImportGroupBox.Location = new System.Drawing.Point(348, 130);
            this.FixImportGroupBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.FixImportGroupBox.Name = "FixImportGroupBox";
            this.FixImportGroupBox.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.FixImportGroupBox.Size = new System.Drawing.Size(342, 248);
            this.FixImportGroupBox.TabIndex = 9;
            this.FixImportGroupBox.TabStop = false;
            this.FixImportGroupBox.Text = "Import From Fix";
            // 
            // FixListDataGridView
            // 
            this.FixListDataGridView.AllowUserToAddRows = false;
            this.FixListDataGridView.AllowUserToDeleteRows = false;
            this.FixListDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader;
            this.FixListDataGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
            this.FixListDataGridView.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            this.FixListDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.FixListDataGridView.Location = new System.Drawing.Point(21, 69);
            this.FixListDataGridView.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.FixListDataGridView.MultiSelect = false;
            this.FixListDataGridView.Name = "FixListDataGridView";
            this.FixListDataGridView.ReadOnly = true;
            this.FixListDataGridView.RowHeadersVisible = false;
            this.FixListDataGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.FixListDataGridView.RowTemplate.Height = 28;
            this.FixListDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.FixListDataGridView.Size = new System.Drawing.Size(304, 166);
            this.FixListDataGridView.TabIndex = 2;
            this.toolTip1.SetToolTip(this.FixListDataGridView, "Double click fix to copy");
            this.FixListDataGridView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.FixListDataGridView_CellDoubleClick);
            // 
            // IdentifierLabel
            // 
            this.IdentifierLabel.AutoSize = true;
            this.IdentifierLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IdentifierLabel.Location = new System.Drawing.Point(16, 32);
            this.IdentifierLabel.Name = "IdentifierLabel";
            this.IdentifierLabel.Size = new System.Drawing.Size(85, 25);
            this.IdentifierLabel.TabIndex = 1;
            this.IdentifierLabel.Text = "Identifier";
            // 
            // IdentifierTextBox
            // 
            this.IdentifierTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IdentifierTextBox.Location = new System.Drawing.Point(107, 28);
            this.IdentifierTextBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.IdentifierTextBox.Name = "IdentifierTextBox";
            this.IdentifierTextBox.Size = new System.Drawing.Size(100, 30);
            this.IdentifierTextBox.TabIndex = 0;
            this.IdentifierTextBox.TabStop = false;
            this.IdentifierTextBox.TextChanged += new System.EventHandler(this.IdentifierTextBox_TextChanged);
            // 
            // CopyClipButton
            // 
            this.CopyClipButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CopyClipButton.Location = new System.Drawing.Point(576, 66);
            this.CopyClipButton.Name = "CopyClipButton";
            this.CopyClipButton.Size = new System.Drawing.Size(114, 59);
            this.CopyClipButton.TabIndex = 10;
            this.CopyClipButton.Text = "Copy From Other Form";
            this.CopyClipButton.UseVisualStyleBackColor = true;
            this.CopyClipButton.Click += new System.EventHandler(this.CopyClipButton_Click);
            // 
            // DrawButton
            // 
            this.DrawButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DrawButton.Location = new System.Drawing.Point(57, 330);
            this.DrawButton.Name = "DrawButton";
            this.DrawButton.Size = new System.Drawing.Size(243, 48);
            this.DrawButton.TabIndex = 11;
            this.DrawButton.Text = "Generate Text Drawing";
            this.DrawButton.UseVisualStyleBackColor = true;
            this.DrawButton.Click += new System.EventHandler(this.DrawButton_Click);
            // 
            // OutputTextBox
            // 
            this.OutputTextBox.Location = new System.Drawing.Point(57, 384);
            this.OutputTextBox.Multiline = true;
            this.OutputTextBox.Name = "OutputTextBox";
            this.OutputTextBox.Size = new System.Drawing.Size(640, 696);
            this.OutputTextBox.TabIndex = 12;
            this.OutputTextBox.WordWrap = false;
            // 
            // Copy2ClipboardButton
            // 
            this.Copy2ClipboardButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Copy2ClipboardButton.Location = new System.Drawing.Point(359, 1085);
            this.Copy2ClipboardButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Copy2ClipboardButton.Name = "Copy2ClipboardButton";
            this.Copy2ClipboardButton.Size = new System.Drawing.Size(118, 80);
            this.Copy2ClipboardButton.TabIndex = 13;
            this.Copy2ClipboardButton.Text = "Copy to Clipboard";
            this.Copy2ClipboardButton.UseVisualStyleBackColor = true;
            // 
            // ClearOutputButton
            // 
            this.ClearOutputButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.ClearOutputButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ClearOutputButton.Location = new System.Drawing.Point(584, 1085);
            this.ClearOutputButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ClearOutputButton.Name = "ClearOutputButton";
            this.ClearOutputButton.Size = new System.Drawing.Size(117, 80);
            this.ClearOutputButton.TabIndex = 15;
            this.ClearOutputButton.Text = "Clear Contents";
            this.ClearOutputButton.UseVisualStyleBackColor = false;
            this.ClearOutputButton.Click += new System.EventHandler(this.ClearOutputButton_Click);
            // 
            // SaveOutput2FileButton
            // 
            this.SaveOutput2FileButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SaveOutput2FileButton.Location = new System.Drawing.Point(470, 1085);
            this.SaveOutput2FileButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.SaveOutput2FileButton.Name = "SaveOutput2FileButton";
            this.SaveOutput2FileButton.Size = new System.Drawing.Size(122, 80);
            this.SaveOutput2FileButton.TabIndex = 14;
            this.SaveOutput2FileButton.Text = "Save to File";
            this.SaveOutput2FileButton.UseVisualStyleBackColor = true;
            // 
            // BearingTextBox
            // 
            this.BearingTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BearingTextBox.Location = new System.Drawing.Point(127, 199);
            this.BearingTextBox.Name = "BearingTextBox";
            this.BearingTextBox.Size = new System.Drawing.Size(200, 30);
            this.BearingTextBox.TabIndex = 17;
            this.BearingTextBox.Text = "90";
            this.BearingTextBox.Validated += new System.EventHandler(this.BearingTextBox_Validated);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(40, 202);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 25);
            this.label3.TabIndex = 16;
            this.label3.Text = "Bearing";
            // 
            // ScaleTextBox
            // 
            this.ScaleTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ScaleTextBox.Location = new System.Drawing.Point(127, 235);
            this.ScaleTextBox.Name = "ScaleTextBox";
            this.ScaleTextBox.Size = new System.Drawing.Size(200, 30);
            this.ScaleTextBox.TabIndex = 19;
            this.ScaleTextBox.Text = "0.5";
            this.ScaleTextBox.Validated += new System.EventHandler(this.ScaleTextBox_Validated);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(7, 238);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(112, 25);
            this.label4.TabIndex = 18;
            this.label4.Text = "Scale (NM)";
            // 
            // DrawLabels
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(778, 1175);
            this.Controls.Add(this.ScaleTextBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.BearingTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Copy2ClipboardButton);
            this.Controls.Add(this.ClearOutputButton);
            this.Controls.Add(this.SaveOutput2FileButton);
            this.Controls.Add(this.OutputTextBox);
            this.Controls.Add(this.DrawButton);
            this.Controls.Add(this.CopyClipButton);
            this.Controls.Add(this.FixImportGroupBox);
            this.Controls.Add(this.AlignRightButton);
            this.Controls.Add(this.AlignCenterButton);
            this.Controls.Add(this.AlignLeftButton);
            this.Controls.Add(this.LonTextBox);
            this.Controls.Add(this.LatTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.LabelTextBox);
            this.Controls.Add(this.Label1Label);
            this.Name = "DrawLabels";
            this.Text = "DrawLabels";
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
        private System.Windows.Forms.Button AlignLeftButton;
        private System.Windows.Forms.Button AlignCenterButton;
        private System.Windows.Forms.Button AlignRightButton;
        private System.Windows.Forms.GroupBox FixImportGroupBox;
        private System.Windows.Forms.DataGridView FixListDataGridView;
        private System.Windows.Forms.Label IdentifierLabel;
        private System.Windows.Forms.TextBox IdentifierTextBox;
        private System.Windows.Forms.Button CopyClipButton;
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
    }
}