namespace dTTP_Reader
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.stateComboBox = new System.Windows.Forms.ComboBox();
            this.cityComboBox = new System.Windows.Forms.ComboBox();
            this.RecordDataGridView = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.CycleLabel = new System.Windows.Forms.Label();
            this.SearchButton = new System.Windows.Forms.Button();
            this.ExitButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.AddedCheckBox = new System.Windows.Forms.CheckBox();
            this.ChangedCheckBox = new System.Windows.Forms.CheckBox();
            this.DeletedCheckBox = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.AirportDiagramsCheckBox = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.RecordDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // stateComboBox
            // 
            this.stateComboBox.FormattingEnabled = true;
            this.stateComboBox.Location = new System.Drawing.Point(12, 67);
            this.stateComboBox.Name = "stateComboBox";
            this.stateComboBox.Size = new System.Drawing.Size(121, 23);
            this.stateComboBox.TabIndex = 0;
            this.stateComboBox.SelectedIndexChanged += new System.EventHandler(this.stateComboBox_SelectedIndexChanged);
            // 
            // cityComboBox
            // 
            this.cityComboBox.FormattingEnabled = true;
            this.cityComboBox.Location = new System.Drawing.Point(155, 67);
            this.cityComboBox.Name = "cityComboBox";
            this.cityComboBox.Size = new System.Drawing.Size(121, 23);
            this.cityComboBox.TabIndex = 1;
            this.cityComboBox.SelectedIndexChanged += new System.EventHandler(this.cityComboBox_SelectedIndexChanged);
            // 
            // RecordDataGridView
            // 
            this.RecordDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.RecordDataGridView.Location = new System.Drawing.Point(12, 125);
            this.RecordDataGridView.Name = "RecordDataGridView";
            this.RecordDataGridView.RowTemplate.Height = 25;
            this.RecordDataGridView.Size = new System.Drawing.Size(663, 292);
            this.RecordDataGridView.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 15);
            this.label1.TabIndex = 6;
            this.label1.Text = "State";
            // 
            // CycleLabel
            // 
            this.CycleLabel.AutoSize = true;
            this.CycleLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.CycleLabel.Location = new System.Drawing.Point(12, 9);
            this.CycleLabel.Name = "CycleLabel";
            this.CycleLabel.Size = new System.Drawing.Size(52, 21);
            this.CycleLabel.TabIndex = 8;
            this.CycleLabel.Text = "label2";
            // 
            // SearchButton
            // 
            this.SearchButton.Location = new System.Drawing.Point(12, 96);
            this.SearchButton.Name = "SearchButton";
            this.SearchButton.Size = new System.Drawing.Size(75, 23);
            this.SearchButton.TabIndex = 9;
            this.SearchButton.Text = "Search";
            this.SearchButton.UseVisualStyleBackColor = true;
            this.SearchButton.Click += new System.EventHandler(this.SearchButton_Click);
            // 
            // ExitButton
            // 
            this.ExitButton.Location = new System.Drawing.Point(600, 96);
            this.ExitButton.Name = "ExitButton";
            this.ExitButton.Size = new System.Drawing.Size(75, 23);
            this.ExitButton.TabIndex = 10;
            this.ExitButton.Text = "Exit";
            this.ExitButton.UseVisualStyleBackColor = true;
            this.ExitButton.Click += new System.EventHandler(this.ExitButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(155, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(28, 15);
            this.label2.TabIndex = 11;
            this.label2.Text = "City";
            // 
            // AddedCheckBox
            // 
            this.AddedCheckBox.AutoSize = true;
            this.AddedCheckBox.Location = new System.Drawing.Point(293, 71);
            this.AddedCheckBox.Name = "AddedCheckBox";
            this.AddedCheckBox.Size = new System.Drawing.Size(61, 19);
            this.AddedCheckBox.TabIndex = 12;
            this.AddedCheckBox.Text = "Added";
            this.AddedCheckBox.UseVisualStyleBackColor = true;
            // 
            // ChangedCheckBox
            // 
            this.ChangedCheckBox.AutoSize = true;
            this.ChangedCheckBox.Location = new System.Drawing.Point(360, 70);
            this.ChangedCheckBox.Name = "ChangedCheckBox";
            this.ChangedCheckBox.Size = new System.Drawing.Size(74, 19);
            this.ChangedCheckBox.TabIndex = 13;
            this.ChangedCheckBox.Text = "Changed";
            this.ChangedCheckBox.UseVisualStyleBackColor = true;
            // 
            // DeletedCheckBox
            // 
            this.DeletedCheckBox.AutoSize = true;
            this.DeletedCheckBox.Location = new System.Drawing.Point(440, 71);
            this.DeletedCheckBox.Name = "DeletedCheckBox";
            this.DeletedCheckBox.Size = new System.Drawing.Size(66, 19);
            this.DeletedCheckBox.TabIndex = 14;
            this.DeletedCheckBox.Text = "Deleted";
            this.DeletedCheckBox.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button1.BackgroundImage")));
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button1.Location = new System.Drawing.Point(539, 7);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(136, 55);
            this.button1.TabIndex = 15;
            this.button1.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // AirportDiagramsCheckBox
            // 
            this.AirportDiagramsCheckBox.AutoSize = true;
            this.AirportDiagramsCheckBox.Location = new System.Drawing.Point(531, 71);
            this.AirportDiagramsCheckBox.Name = "AirportDiagramsCheckBox";
            this.AirportDiagramsCheckBox.Size = new System.Drawing.Size(144, 19);
            this.AirportDiagramsCheckBox.TabIndex = 16;
            this.AirportDiagramsCheckBox.Text = "Airport Diagrams Only";
            this.AirportDiagramsCheckBox.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(702, 439);
            this.Controls.Add(this.AirportDiagramsCheckBox);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.DeletedCheckBox);
            this.Controls.Add(this.ChangedCheckBox);
            this.Controls.Add(this.AddedCheckBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ExitButton);
            this.Controls.Add(this.SearchButton);
            this.Controls.Add(this.CycleLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.RecordDataGridView);
            this.Controls.Add(this.cityComboBox);
            this.Controls.Add(this.stateComboBox);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "dTTP Reader";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.RecordDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ComboBox stateComboBox;
        private ComboBox cityComboBox;
        private DataGridView RecordDataGridView;
        private Label label1;
        private Label CycleLabel;
        private Button SearchButton;
        private Button ExitButton;
        private Label label2;
        private CheckBox AddedCheckBox;
        private CheckBox ChangedCheckBox;
        private CheckBox DeletedCheckBox;
        private Button button1;
        private CheckBox AirportDiagramsCheckBox;
    }
}