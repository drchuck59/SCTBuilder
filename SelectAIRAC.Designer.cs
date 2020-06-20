namespace SCTBuilder
{
    partial class SelectAIRAC
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.YearComboBox = new System.Windows.Forms.ComboBox();
            this.YearComboBox_Label = new System.Windows.Forms.Label();
            this.CycleComboBox = new System.Windows.Forms.ComboBox();
            this.CycleComboBox_Label = new System.Windows.Forms.Label();
            this.ConfirmationLabel = new System.Windows.Forms.Label();
            this.MyButtonCancel = new System.Windows.Forms.Button();
            this.OKButton = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.ContinueButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // YearComboBox
            // 
            this.YearComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.YearComboBox.FormattingEnabled = true;
            this.YearComboBox.Location = new System.Drawing.Point(27, 42);
            this.YearComboBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.YearComboBox.Name = "YearComboBox";
            this.YearComboBox.Size = new System.Drawing.Size(82, 24);
            this.YearComboBox.TabIndex = 0;
            this.YearComboBox.SelectedIndexChanged += new System.EventHandler(this.YearComboBox_SelectedIndexChanged);
            // 
            // YearComboBox_Label
            // 
            this.YearComboBox_Label.AutoSize = true;
            this.YearComboBox_Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.YearComboBox_Label.Location = new System.Drawing.Point(25, 27);
            this.YearComboBox_Label.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.YearComboBox_Label.Name = "YearComboBox_Label";
            this.YearComboBox_Label.Size = new System.Drawing.Size(81, 17);
            this.YearComboBox_Label.TabIndex = 1;
            this.YearComboBox_Label.Text = "Select Year";
            // 
            // CycleComboBox
            // 
            this.CycleComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CycleComboBox.FormattingEnabled = true;
            this.CycleComboBox.Location = new System.Drawing.Point(123, 42);
            this.CycleComboBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.CycleComboBox.Name = "CycleComboBox";
            this.CycleComboBox.Size = new System.Drawing.Size(82, 24);
            this.CycleComboBox.TabIndex = 2;
            this.CycleComboBox.SelectedIndexChanged += new System.EventHandler(this.CycleComboBox_SelectedIndexChanged);
            // 
            // CycleComboBox_Label
            // 
            this.CycleComboBox_Label.AutoSize = true;
            this.CycleComboBox_Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CycleComboBox_Label.Location = new System.Drawing.Point(122, 27);
            this.CycleComboBox_Label.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.CycleComboBox_Label.Name = "CycleComboBox_Label";
            this.CycleComboBox_Label.Size = new System.Drawing.Size(85, 17);
            this.CycleComboBox_Label.TabIndex = 3;
            this.CycleComboBox_Label.Text = "Select Cycle";
            // 
            // ConfirmationLabel
            // 
            this.ConfirmationLabel.AutoSize = true;
            this.ConfirmationLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ConfirmationLabel.Location = new System.Drawing.Point(27, 71);
            this.ConfirmationLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.ConfirmationLabel.Name = "ConfirmationLabel";
            this.ConfirmationLabel.Size = new System.Drawing.Size(125, 17);
            this.ConfirmationLabel.TabIndex = 4;
            this.ConfirmationLabel.Text = "Cycle Confirmation";
            // 
            // MyButtonCancel
            // 
            this.MyButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.MyButtonCancel.Location = new System.Drawing.Point(217, 148);
            this.MyButtonCancel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MyButtonCancel.Name = "MyButtonCancel";
            this.MyButtonCancel.Size = new System.Drawing.Size(48, 22);
            this.MyButtonCancel.TabIndex = 5;
            this.MyButtonCancel.Text = "Cancel";
            this.MyButtonCancel.UseVisualStyleBackColor = true;
            this.MyButtonCancel.Click += new System.EventHandler(this.MyButtonCancel_Click);
            // 
            // OKButton
            // 
            this.OKButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OKButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OKButton.Location = new System.Drawing.Point(18, 142);
            this.OKButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(185, 29);
            this.OKButton.TabIndex = 6;
            this.OKButton.Text = "Download this AIRAC";
            this.OKButton.UseVisualStyleBackColor = true;
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(18, 172);
            this.progressBar1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(185, 16);
            this.progressBar1.TabIndex = 7;
            // 
            // ContinueButton
            // 
            this.ContinueButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.ContinueButton.Enabled = false;
            this.ContinueButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ContinueButton.Location = new System.Drawing.Point(18, 141);
            this.ContinueButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ContinueButton.Name = "ContinueButton";
            this.ContinueButton.Size = new System.Drawing.Size(185, 27);
            this.ContinueButton.TabIndex = 8;
            this.ContinueButton.Text = "Done - Click to continue";
            this.ContinueButton.UseVisualStyleBackColor = true;
            this.ContinueButton.Visible = false;
            this.ContinueButton.Click += new System.EventHandler(this.ContinueButton_Click);
            // 
            // SelectAIRAC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.CancelButton = this.MyButtonCancel;
            this.ClientSize = new System.Drawing.Size(291, 202);
            this.ControlBox = false;
            this.Controls.Add(this.ContinueButton);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.OKButton);
            this.Controls.Add(this.MyButtonCancel);
            this.Controls.Add(this.ConfirmationLabel);
            this.Controls.Add(this.CycleComboBox_Label);
            this.Controls.Add(this.CycleComboBox);
            this.Controls.Add(this.YearComboBox_Label);
            this.Controls.Add(this.YearComboBox);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "SelectAIRAC";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox YearComboBox;
        private System.Windows.Forms.Label YearComboBox_Label;
        private System.Windows.Forms.ComboBox CycleComboBox;
        private System.Windows.Forms.Label CycleComboBox_Label;
        private System.Windows.Forms.Label ConfirmationLabel;
        private System.Windows.Forms.Button MyButtonCancel;
        private System.Windows.Forms.Button OKButton;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button ContinueButton;
    }
}
