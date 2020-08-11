namespace SCTBuilder
{
    partial class ArcGenerator
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ArcGenerator));
            this.label11 = new System.Windows.Forms.Label();
            this.FixImportGroupBox = new System.Windows.Forms.GroupBox();
            this.FixListDataGridView = new System.Windows.Forms.DataGridView();
            this.IdentifierLabel = new System.Windows.Forms.Label();
            this.IdentifierTextBox = new System.Windows.Forms.TextBox();
            this.CenterGroupBox = new System.Windows.Forms.GroupBox();
            this.CenterFixTextBox = new System.Windows.Forms.TextBox();
            this.StartFixLabel = new System.Windows.Forms.Label();
            this.CenterLongitudeTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.CenterLatitudeTextBox = new System.Windows.Forms.TextBox();
            this.startPointLatitudeLabel = new System.Windows.Forms.Label();
            this.Fix2CenterButton = new System.Windows.Forms.Button();
            this.StartGroupBox = new System.Windows.Forms.GroupBox();
            this.StartFixTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.StartLongitudeTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.StartLatitudeTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.FixToSTartButton = new System.Windows.Forms.Button();
            this.EndGroupBox = new System.Windows.Forms.GroupBox();
            this.EndFixTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.EndLongitudeTextBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.EndLatitudeTextBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.FixToEndButton = new System.Windows.Forms.Button();
            this.CenterRadiusLabel = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.CalcDistanceTextBox = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.StartBrgTextBox = new System.Windows.Forms.TextBox();
            this.EndBrgTestBox = new System.Windows.Forms.TextBox();
            this.SCTFormatGroupBox = new System.Windows.Forms.GroupBox();
            this.UseFIXNamesCheckBox = new System.Windows.Forms.CheckBox();
            this.GEORadioButton = new System.Windows.Forms.RadioButton();
            this.SSDRadioButton = new System.Windows.Forms.RadioButton();
            this.ARTCCRadioButton = new System.Windows.Forms.RadioButton();
            this.AirwayRadioButton = new System.Windows.Forms.RadioButton();
            this.Copy2ClipboardButton = new System.Windows.Forms.Button();
            this.ClearOutputButton = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.OutputTextBox = new System.Windows.Forms.TextBox();
            this.SaveOutput2FileButton = new System.Windows.Forms.Button();
            this.SuffixTextBox = new System.Windows.Forms.TextBox();
            this.PrefixTextBox = new System.Windows.Forms.TextBox();
            this.SuffixLabel = new System.Windows.Forms.Label();
            this.PrefixLabel = new System.Windows.Forms.Label();
            this.AddArcByRadialsButton = new System.Windows.Forms.Button();
            this.AddArcByCoordsButton = new System.Windows.Forms.Button();
            this.MagOffsetLabel = new System.Windows.Forms.Label();
            this.FixImportGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FixListDataGridView)).BeginInit();
            this.CenterGroupBox.SuspendLayout();
            this.StartGroupBox.SuspendLayout();
            this.EndGroupBox.SuspendLayout();
            this.SCTFormatGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(11, 9);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(128, 24);
            this.label11.TabIndex = 38;
            this.label11.Text = "Arc Generator";
            // 
            // FixImportGroupBox
            // 
            this.FixImportGroupBox.Controls.Add(this.FixListDataGridView);
            this.FixImportGroupBox.Controls.Add(this.IdentifierLabel);
            this.FixImportGroupBox.Controls.Add(this.IdentifierTextBox);
            this.FixImportGroupBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FixImportGroupBox.Location = new System.Drawing.Point(252, 87);
            this.FixImportGroupBox.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.FixImportGroupBox.Name = "FixImportGroupBox";
            this.FixImportGroupBox.Padding = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.FixImportGroupBox.Size = new System.Drawing.Size(228, 230);
            this.FixImportGroupBox.TabIndex = 39;
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
            this.FixListDataGridView.Location = new System.Drawing.Point(14, 45);
            this.FixListDataGridView.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.FixListDataGridView.MultiSelect = false;
            this.FixListDataGridView.Name = "FixListDataGridView";
            this.FixListDataGridView.ReadOnly = true;
            this.FixListDataGridView.RowHeadersVisible = false;
            this.FixListDataGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.FixListDataGridView.RowTemplate.Height = 28;
            this.FixListDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.FixListDataGridView.Size = new System.Drawing.Size(203, 181);
            this.FixListDataGridView.TabIndex = 2;
            this.FixListDataGridView.Click += new System.EventHandler(this.FixListDataGridView_Click);
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
            this.IdentifierTextBox.MaxLength = 6;
            this.IdentifierTextBox.Name = "IdentifierTextBox";
            this.IdentifierTextBox.Size = new System.Drawing.Size(68, 23);
            this.IdentifierTextBox.TabIndex = 0;
            this.IdentifierTextBox.TabStop = false;
            this.IdentifierTextBox.TextChanged += new System.EventHandler(this.IdentifierTextBox_TextChanged);
            // 
            // CenterGroupBox
            // 
            this.CenterGroupBox.Controls.Add(this.CenterFixTextBox);
            this.CenterGroupBox.Controls.Add(this.StartFixLabel);
            this.CenterGroupBox.Controls.Add(this.CenterLongitudeTextBox);
            this.CenterGroupBox.Controls.Add(this.label1);
            this.CenterGroupBox.Controls.Add(this.CenterLatitudeTextBox);
            this.CenterGroupBox.Controls.Add(this.startPointLatitudeLabel);
            this.CenterGroupBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CenterGroupBox.Location = new System.Drawing.Point(16, 87);
            this.CenterGroupBox.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.CenterGroupBox.Name = "CenterGroupBox";
            this.CenterGroupBox.Padding = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.CenterGroupBox.Size = new System.Drawing.Size(189, 103);
            this.CenterGroupBox.TabIndex = 40;
            this.CenterGroupBox.TabStop = false;
            this.CenterGroupBox.Text = "Center coordinates";
            // 
            // CenterFixTextBox
            // 
            this.CenterFixTextBox.Enabled = false;
            this.CenterFixTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CenterFixTextBox.Location = new System.Drawing.Point(73, 74);
            this.CenterFixTextBox.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.CenterFixTextBox.MaxLength = 6;
            this.CenterFixTextBox.Name = "CenterFixTextBox";
            this.CenterFixTextBox.Size = new System.Drawing.Size(108, 21);
            this.CenterFixTextBox.TabIndex = 4;
            // 
            // StartFixLabel
            // 
            this.StartFixLabel.AutoSize = true;
            this.StartFixLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StartFixLabel.Location = new System.Drawing.Point(11, 74);
            this.StartFixLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.StartFixLabel.Name = "StartFixLabel";
            this.StartFixLabel.Size = new System.Drawing.Size(25, 17);
            this.StartFixLabel.TabIndex = 3;
            this.StartFixLabel.Text = "Fix";
            // 
            // CenterLongitudeTextBox
            // 
            this.CenterLongitudeTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CenterLongitudeTextBox.Location = new System.Drawing.Point(73, 51);
            this.CenterLongitudeTextBox.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.CenterLongitudeTextBox.MaxLength = 13;
            this.CenterLongitudeTextBox.Name = "CenterLongitudeTextBox";
            this.CenterLongitudeTextBox.Size = new System.Drawing.Size(108, 21);
            this.CenterLongitudeTextBox.TabIndex = 2;
            this.CenterLongitudeTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CenterLongitudeTextBox_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(4, 53);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Longitude";
            // 
            // CenterLatitudeTextBox
            // 
            this.CenterLatitudeTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CenterLatitudeTextBox.Location = new System.Drawing.Point(73, 27);
            this.CenterLatitudeTextBox.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.CenterLatitudeTextBox.MaxLength = 12;
            this.CenterLatitudeTextBox.Name = "CenterLatitudeTextBox";
            this.CenterLatitudeTextBox.Size = new System.Drawing.Size(108, 21);
            this.CenterLatitudeTextBox.TabIndex = 1;
            this.CenterLatitudeTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CenterLatitudeTextBox_KeyPress);
            this.CenterLatitudeTextBox.Validated += new System.EventHandler(this.CenterLatitudeTextBox_Validated);
            // 
            // startPointLatitudeLabel
            // 
            this.startPointLatitudeLabel.AutoSize = true;
            this.startPointLatitudeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.startPointLatitudeLabel.Location = new System.Drawing.Point(4, 29);
            this.startPointLatitudeLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.startPointLatitudeLabel.Name = "startPointLatitudeLabel";
            this.startPointLatitudeLabel.Size = new System.Drawing.Size(59, 17);
            this.startPointLatitudeLabel.TabIndex = 0;
            this.startPointLatitudeLabel.Text = "Latitude";
            // 
            // Fix2CenterButton
            // 
            this.Fix2CenterButton.BackColor = System.Drawing.Color.White;
            this.Fix2CenterButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Fix2CenterButton.BackgroundImage")));
            this.Fix2CenterButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Fix2CenterButton.Enabled = false;
            this.Fix2CenterButton.Location = new System.Drawing.Point(209, 114);
            this.Fix2CenterButton.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.Fix2CenterButton.Name = "Fix2CenterButton";
            this.Fix2CenterButton.Size = new System.Drawing.Size(39, 38);
            this.Fix2CenterButton.TabIndex = 41;
            this.Fix2CenterButton.TabStop = false;
            this.Fix2CenterButton.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.Fix2CenterButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.Fix2CenterButton.UseVisualStyleBackColor = false;
            this.Fix2CenterButton.Click += new System.EventHandler(this.ImportFix2CenterButton_Click);
            // 
            // StartGroupBox
            // 
            this.StartGroupBox.Controls.Add(this.StartFixTextBox);
            this.StartGroupBox.Controls.Add(this.label2);
            this.StartGroupBox.Controls.Add(this.StartLongitudeTextBox);
            this.StartGroupBox.Controls.Add(this.label3);
            this.StartGroupBox.Controls.Add(this.StartLatitudeTextBox);
            this.StartGroupBox.Controls.Add(this.label4);
            this.StartGroupBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StartGroupBox.Location = new System.Drawing.Point(527, 87);
            this.StartGroupBox.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.StartGroupBox.Name = "StartGroupBox";
            this.StartGroupBox.Padding = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.StartGroupBox.Size = new System.Drawing.Size(189, 103);
            this.StartGroupBox.TabIndex = 42;
            this.StartGroupBox.TabStop = false;
            this.StartGroupBox.Text = "Start point coordinates";
            // 
            // StartFixTextBox
            // 
            this.StartFixTextBox.Enabled = false;
            this.StartFixTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StartFixTextBox.Location = new System.Drawing.Point(73, 74);
            this.StartFixTextBox.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.StartFixTextBox.MaxLength = 6;
            this.StartFixTextBox.Name = "StartFixTextBox";
            this.StartFixTextBox.Size = new System.Drawing.Size(108, 21);
            this.StartFixTextBox.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(11, 74);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(25, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "Fix";
            // 
            // StartLongitudeTextBox
            // 
            this.StartLongitudeTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StartLongitudeTextBox.Location = new System.Drawing.Point(73, 51);
            this.StartLongitudeTextBox.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.StartLongitudeTextBox.MaxLength = 13;
            this.StartLongitudeTextBox.Name = "StartLongitudeTextBox";
            this.StartLongitudeTextBox.Size = new System.Drawing.Size(108, 21);
            this.StartLongitudeTextBox.TabIndex = 2;
            this.StartLongitudeTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.StartLongitudeTextBox_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(4, 53);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 17);
            this.label3.TabIndex = 2;
            this.label3.Text = "Longitude";
            // 
            // StartLatitudeTextBox
            // 
            this.StartLatitudeTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StartLatitudeTextBox.Location = new System.Drawing.Point(73, 27);
            this.StartLatitudeTextBox.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.StartLatitudeTextBox.MaxLength = 12;
            this.StartLatitudeTextBox.Name = "StartLatitudeTextBox";
            this.StartLatitudeTextBox.Size = new System.Drawing.Size(108, 21);
            this.StartLatitudeTextBox.TabIndex = 1;
            this.StartLatitudeTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.StartLatitudeTextBox_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(4, 29);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 17);
            this.label4.TabIndex = 0;
            this.label4.Text = "Latitude";
            // 
            // FixToSTartButton
            // 
            this.FixToSTartButton.BackColor = System.Drawing.Color.White;
            this.FixToSTartButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("FixToSTartButton.BackgroundImage")));
            this.FixToSTartButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.FixToSTartButton.Enabled = false;
            this.FixToSTartButton.Location = new System.Drawing.Point(484, 115);
            this.FixToSTartButton.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.FixToSTartButton.Name = "FixToSTartButton";
            this.FixToSTartButton.Size = new System.Drawing.Size(39, 38);
            this.FixToSTartButton.TabIndex = 43;
            this.FixToSTartButton.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.FixToSTartButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.FixToSTartButton.UseVisualStyleBackColor = false;
            this.FixToSTartButton.Click += new System.EventHandler(this.FixToSTartButton_Click);
            // 
            // EndGroupBox
            // 
            this.EndGroupBox.Controls.Add(this.EndFixTextBox);
            this.EndGroupBox.Controls.Add(this.label5);
            this.EndGroupBox.Controls.Add(this.EndLongitudeTextBox);
            this.EndGroupBox.Controls.Add(this.label6);
            this.EndGroupBox.Controls.Add(this.EndLatitudeTextBox);
            this.EndGroupBox.Controls.Add(this.label7);
            this.EndGroupBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EndGroupBox.Location = new System.Drawing.Point(527, 192);
            this.EndGroupBox.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.EndGroupBox.Name = "EndGroupBox";
            this.EndGroupBox.Padding = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.EndGroupBox.Size = new System.Drawing.Size(189, 103);
            this.EndGroupBox.TabIndex = 44;
            this.EndGroupBox.TabStop = false;
            this.EndGroupBox.Text = "End point coordinates";
            // 
            // EndFixTextBox
            // 
            this.EndFixTextBox.Enabled = false;
            this.EndFixTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EndFixTextBox.Location = new System.Drawing.Point(73, 74);
            this.EndFixTextBox.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.EndFixTextBox.MaxLength = 6;
            this.EndFixTextBox.Name = "EndFixTextBox";
            this.EndFixTextBox.Size = new System.Drawing.Size(108, 21);
            this.EndFixTextBox.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(11, 74);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(25, 17);
            this.label5.TabIndex = 3;
            this.label5.Text = "Fix";
            // 
            // EndLongitudeTextBox
            // 
            this.EndLongitudeTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EndLongitudeTextBox.Location = new System.Drawing.Point(73, 51);
            this.EndLongitudeTextBox.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.EndLongitudeTextBox.MaxLength = 13;
            this.EndLongitudeTextBox.Name = "EndLongitudeTextBox";
            this.EndLongitudeTextBox.Size = new System.Drawing.Size(108, 21);
            this.EndLongitudeTextBox.TabIndex = 2;
            this.EndLongitudeTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.EndLongitudeTextBox_KeyPress);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(4, 53);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(71, 17);
            this.label6.TabIndex = 2;
            this.label6.Text = "Longitude";
            // 
            // EndLatitudeTextBox
            // 
            this.EndLatitudeTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EndLatitudeTextBox.Location = new System.Drawing.Point(73, 27);
            this.EndLatitudeTextBox.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.EndLatitudeTextBox.MaxLength = 12;
            this.EndLatitudeTextBox.Name = "EndLatitudeTextBox";
            this.EndLatitudeTextBox.Size = new System.Drawing.Size(108, 21);
            this.EndLatitudeTextBox.TabIndex = 1;
            this.EndLatitudeTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.EndLatitudeTextBox_KeyPress);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(4, 29);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(59, 17);
            this.label7.TabIndex = 0;
            this.label7.Text = "Latitude";
            // 
            // FixToEndButton
            // 
            this.FixToEndButton.BackColor = System.Drawing.Color.White;
            this.FixToEndButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("FixToEndButton.BackgroundImage")));
            this.FixToEndButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.FixToEndButton.Enabled = false;
            this.FixToEndButton.Location = new System.Drawing.Point(484, 226);
            this.FixToEndButton.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.FixToEndButton.Name = "FixToEndButton";
            this.FixToEndButton.Size = new System.Drawing.Size(39, 38);
            this.FixToEndButton.TabIndex = 45;
            this.FixToEndButton.TabStop = false;
            this.FixToEndButton.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.FixToEndButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.FixToEndButton.UseVisualStyleBackColor = false;
            this.FixToEndButton.Click += new System.EventHandler(this.FixToEndButton_Click);
            // 
            // CenterRadiusLabel
            // 
            this.CenterRadiusLabel.AutoSize = true;
            this.CenterRadiusLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CenterRadiusLabel.Location = new System.Drawing.Point(15, 65);
            this.CenterRadiusLabel.Name = "CenterRadiusLabel";
            this.CenterRadiusLabel.Size = new System.Drawing.Size(222, 16);
            this.CenterRadiusLabel.TabIndex = 46;
            this.CenterRadiusLabel.Text = "Arc from center, radius and bearings";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(524, 49);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(211, 32);
            this.label8.TabIndex = 47;
            this.label8.Text = "Arc from start and end coordinates\r\n(Distance determines diameter)";
            // 
            // CalcDistanceTextBox
            // 
            this.CalcDistanceTextBox.Location = new System.Drawing.Point(111, 192);
            this.CalcDistanceTextBox.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.CalcDistanceTextBox.MaxLength = 3;
            this.CalcDistanceTextBox.Name = "CalcDistanceTextBox";
            this.CalcDistanceTextBox.Size = new System.Drawing.Size(68, 20);
            this.CalcDistanceTextBox.TabIndex = 48;
            this.CalcDistanceTextBox.TextChanged += new System.EventHandler(this.CalcDistanceTextBox_TextChanged);
            this.CalcDistanceTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CalcDistanceTextBox_KeyPress);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(24, 196);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(83, 16);
            this.label9.TabIndex = 49;
            this.label9.Text = "Radius (NM)";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(15, 218);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(92, 16);
            this.label10.TabIndex = 50;
            this.label10.Text = "Start Brg (true)";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(18, 237);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(89, 16);
            this.label12.TabIndex = 51;
            this.label12.Text = "End Brg (true)";
            // 
            // StartBrgTextBox
            // 
            this.StartBrgTextBox.Location = new System.Drawing.Point(111, 214);
            this.StartBrgTextBox.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.StartBrgTextBox.MaxLength = 3;
            this.StartBrgTextBox.Name = "StartBrgTextBox";
            this.StartBrgTextBox.Size = new System.Drawing.Size(68, 20);
            this.StartBrgTextBox.TabIndex = 52;
            this.StartBrgTextBox.TextChanged += new System.EventHandler(this.StartBrgTextBox_TextChanged);
            this.StartBrgTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.StartBrgTextBox_KeyPress);
            // 
            // EndBrgTestBox
            // 
            this.EndBrgTestBox.Location = new System.Drawing.Point(111, 237);
            this.EndBrgTestBox.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.EndBrgTestBox.MaxLength = 3;
            this.EndBrgTestBox.Name = "EndBrgTestBox";
            this.EndBrgTestBox.Size = new System.Drawing.Size(68, 20);
            this.EndBrgTestBox.TabIndex = 53;
            this.EndBrgTestBox.TextChanged += new System.EventHandler(this.EndBrgTestBox_TextChanged);
            this.EndBrgTestBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.EndBrgTestBox_KeyPress);
            // 
            // SCTFormatGroupBox
            // 
            this.SCTFormatGroupBox.Controls.Add(this.UseFIXNamesCheckBox);
            this.SCTFormatGroupBox.Controls.Add(this.GEORadioButton);
            this.SCTFormatGroupBox.Controls.Add(this.SSDRadioButton);
            this.SCTFormatGroupBox.Controls.Add(this.ARTCCRadioButton);
            this.SCTFormatGroupBox.Controls.Add(this.AirwayRadioButton);
            this.SCTFormatGroupBox.Location = new System.Drawing.Point(5, 399);
            this.SCTFormatGroupBox.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.SCTFormatGroupBox.Name = "SCTFormatGroupBox";
            this.SCTFormatGroupBox.Padding = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.SCTFormatGroupBox.Size = new System.Drawing.Size(233, 64);
            this.SCTFormatGroupBox.TabIndex = 54;
            this.SCTFormatGroupBox.TabStop = false;
            this.SCTFormatGroupBox.Text = "Desired SCT2 format";
            // 
            // UseFIXNamesCheckBox
            // 
            this.UseFIXNamesCheckBox.AutoSize = true;
            this.UseFIXNamesCheckBox.Location = new System.Drawing.Point(111, 38);
            this.UseFIXNamesCheckBox.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.UseFIXNamesCheckBox.Name = "UseFIXNamesCheckBox";
            this.UseFIXNamesCheckBox.Size = new System.Drawing.Size(98, 17);
            this.UseFIXNamesCheckBox.TabIndex = 5;
            this.UseFIXNamesCheckBox.Text = "Use FIX names";
            this.UseFIXNamesCheckBox.UseVisualStyleBackColor = true;
            // 
            // GEORadioButton
            // 
            this.GEORadioButton.AutoSize = true;
            this.GEORadioButton.Location = new System.Drawing.Point(89, 17);
            this.GEORadioButton.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.GEORadioButton.Name = "GEORadioButton";
            this.GEORadioButton.Size = new System.Drawing.Size(48, 17);
            this.GEORadioButton.TabIndex = 1;
            this.GEORadioButton.Text = "GEO";
            this.GEORadioButton.UseVisualStyleBackColor = true;
            // 
            // SSDRadioButton
            // 
            this.SSDRadioButton.AutoSize = true;
            this.SSDRadioButton.Checked = true;
            this.SSDRadioButton.Location = new System.Drawing.Point(11, 18);
            this.SSDRadioButton.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.SSDRadioButton.Name = "SSDRadioButton";
            this.SSDRadioButton.Size = new System.Drawing.Size(74, 17);
            this.SSDRadioButton.TabIndex = 0;
            this.SSDRadioButton.TabStop = true;
            this.SSDRadioButton.Text = "SID|STAR";
            this.SSDRadioButton.UseVisualStyleBackColor = true;
            // 
            // ARTCCRadioButton
            // 
            this.ARTCCRadioButton.AutoSize = true;
            this.ARTCCRadioButton.Location = new System.Drawing.Point(11, 38);
            this.ARTCCRadioButton.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.ARTCCRadioButton.Name = "ARTCCRadioButton";
            this.ARTCCRadioButton.Size = new System.Drawing.Size(85, 17);
            this.ARTCCRadioButton.TabIndex = 3;
            this.ARTCCRadioButton.Text = "ARTCC|SUA";
            this.ARTCCRadioButton.UseVisualStyleBackColor = true;
            // 
            // AirwayRadioButton
            // 
            this.AirwayRadioButton.AutoSize = true;
            this.AirwayRadioButton.Location = new System.Drawing.Point(139, 17);
            this.AirwayRadioButton.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.AirwayRadioButton.Name = "AirwayRadioButton";
            this.AirwayRadioButton.Size = new System.Drawing.Size(56, 17);
            this.AirwayRadioButton.TabIndex = 4;
            this.AirwayRadioButton.Text = "Airway";
            this.AirwayRadioButton.UseVisualStyleBackColor = true;
            // 
            // Copy2ClipboardButton
            // 
            this.Copy2ClipboardButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Copy2ClipboardButton.Location = new System.Drawing.Point(492, 497);
            this.Copy2ClipboardButton.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.Copy2ClipboardButton.Name = "Copy2ClipboardButton";
            this.Copy2ClipboardButton.Size = new System.Drawing.Size(79, 52);
            this.Copy2ClipboardButton.TabIndex = 57;
            this.Copy2ClipboardButton.Text = "Copy to Clipboard";
            this.Copy2ClipboardButton.UseVisualStyleBackColor = true;
            this.Copy2ClipboardButton.Click += new System.EventHandler(this.Copy2ClipboardButton_Click);
            // 
            // ClearOutputButton
            // 
            this.ClearOutputButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.ClearOutputButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ClearOutputButton.Location = new System.Drawing.Point(642, 497);
            this.ClearOutputButton.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.ClearOutputButton.Name = "ClearOutputButton";
            this.ClearOutputButton.Size = new System.Drawing.Size(78, 52);
            this.ClearOutputButton.TabIndex = 59;
            this.ClearOutputButton.Text = "Clear Contents";
            this.ClearOutputButton.UseVisualStyleBackColor = false;
            this.ClearOutputButton.Click += new System.EventHandler(this.ClearOutputButton_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(255, 382);
            this.label13.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(86, 13);
            this.label13.TabIndex = 63;
            this.label13.Text = "Current Contents";
            // 
            // OutputTextBox
            // 
            this.OutputTextBox.Location = new System.Drawing.Point(254, 399);
            this.OutputTextBox.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.OutputTextBox.Multiline = true;
            this.OutputTextBox.Name = "OutputTextBox";
            this.OutputTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.OutputTextBox.Size = new System.Drawing.Size(465, 94);
            this.OutputTextBox.TabIndex = 60;
            this.OutputTextBox.WordWrap = false;
            // 
            // SaveOutput2FileButton
            // 
            this.SaveOutput2FileButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SaveOutput2FileButton.Location = new System.Drawing.Point(566, 497);
            this.SaveOutput2FileButton.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.SaveOutput2FileButton.Name = "SaveOutput2FileButton";
            this.SaveOutput2FileButton.Size = new System.Drawing.Size(81, 52);
            this.SaveOutput2FileButton.TabIndex = 58;
            this.SaveOutput2FileButton.Text = "Save to File";
            this.SaveOutput2FileButton.UseVisualStyleBackColor = true;
            this.SaveOutput2FileButton.Click += new System.EventHandler(this.SaveOutput2FileButton_Click);
            // 
            // SuffixTextBox
            // 
            this.SuffixTextBox.Location = new System.Drawing.Point(307, 349);
            this.SuffixTextBox.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.SuffixTextBox.MaxLength = 255;
            this.SuffixTextBox.Name = "SuffixTextBox";
            this.SuffixTextBox.Size = new System.Drawing.Size(173, 20);
            this.SuffixTextBox.TabIndex = 56;
            // 
            // PrefixTextBox
            // 
            this.PrefixTextBox.Location = new System.Drawing.Point(307, 327);
            this.PrefixTextBox.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.PrefixTextBox.MaxLength = 26;
            this.PrefixTextBox.Name = "PrefixTextBox";
            this.PrefixTextBox.Size = new System.Drawing.Size(173, 20);
            this.PrefixTextBox.TabIndex = 55;
            // 
            // SuffixLabel
            // 
            this.SuffixLabel.AutoSize = true;
            this.SuffixLabel.Location = new System.Drawing.Point(251, 350);
            this.SuffixLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.SuffixLabel.Name = "SuffixLabel";
            this.SuffixLabel.Size = new System.Drawing.Size(51, 13);
            this.SuffixLabel.TabIndex = 62;
            this.SuffixLabel.Text = "Comment";
            // 
            // PrefixLabel
            // 
            this.PrefixLabel.AutoSize = true;
            this.PrefixLabel.Location = new System.Drawing.Point(251, 329);
            this.PrefixLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.PrefixLabel.Name = "PrefixLabel";
            this.PrefixLabel.Size = new System.Drawing.Size(33, 13);
            this.PrefixLabel.TabIndex = 61;
            this.PrefixLabel.Text = "Prefix";
            // 
            // AddArcByRadialsButton
            // 
            this.AddArcByRadialsButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AddArcByRadialsButton.Location = new System.Drawing.Point(252, 497);
            this.AddArcByRadialsButton.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.AddArcByRadialsButton.Name = "AddArcByRadialsButton";
            this.AddArcByRadialsButton.Size = new System.Drawing.Size(99, 46);
            this.AddArcByRadialsButton.TabIndex = 64;
            this.AddArcByRadialsButton.Text = "Generate Arc";
            this.AddArcByRadialsButton.UseVisualStyleBackColor = true;
            // 
            // AddArcByCoordsButton
            // 
            this.AddArcByCoordsButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AddArcByCoordsButton.Location = new System.Drawing.Point(635, 297);
            this.AddArcByCoordsButton.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.AddArcByCoordsButton.Name = "AddArcByCoordsButton";
            this.AddArcByCoordsButton.Size = new System.Drawing.Size(81, 45);
            this.AddArcByCoordsButton.TabIndex = 65;
            this.AddArcByCoordsButton.Text = "Calculate Arc";
            this.AddArcByCoordsButton.UseVisualStyleBackColor = true;
            // 
            // MagOffsetLabel
            // 
            this.MagOffsetLabel.AutoSize = true;
            this.MagOffsetLabel.Location = new System.Drawing.Point(11, 261);
            this.MagOffsetLabel.Name = "MagOffsetLabel";
            this.MagOffsetLabel.Size = new System.Drawing.Size(233, 13);
            this.MagOffsetLabel.TabIndex = 66;
            this.MagOffsetLabel.Text = "*For magnetic heading SUBTRACT  00 degrees";
            // 
            // ArcGenerator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(756, 558);
            this.Controls.Add(this.MagOffsetLabel);
            this.Controls.Add(this.AddArcByCoordsButton);
            this.Controls.Add(this.AddArcByRadialsButton);
            this.Controls.Add(this.Copy2ClipboardButton);
            this.Controls.Add(this.ClearOutputButton);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.OutputTextBox);
            this.Controls.Add(this.SaveOutput2FileButton);
            this.Controls.Add(this.SuffixTextBox);
            this.Controls.Add(this.PrefixTextBox);
            this.Controls.Add(this.SuffixLabel);
            this.Controls.Add(this.PrefixLabel);
            this.Controls.Add(this.SCTFormatGroupBox);
            this.Controls.Add(this.EndBrgTestBox);
            this.Controls.Add(this.StartBrgTextBox);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.CalcDistanceTextBox);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.CenterRadiusLabel);
            this.Controls.Add(this.FixToEndButton);
            this.Controls.Add(this.EndGroupBox);
            this.Controls.Add(this.FixToSTartButton);
            this.Controls.Add(this.StartGroupBox);
            this.Controls.Add(this.Fix2CenterButton);
            this.Controls.Add(this.CenterGroupBox);
            this.Controls.Add(this.FixImportGroupBox);
            this.Controls.Add(this.label11);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ArcGenerator";
            this.Text = "Arc Generator";
            this.Load += new System.EventHandler(this.ArcGenerator_Load);
            this.FixImportGroupBox.ResumeLayout(false);
            this.FixImportGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FixListDataGridView)).EndInit();
            this.CenterGroupBox.ResumeLayout(false);
            this.CenterGroupBox.PerformLayout();
            this.StartGroupBox.ResumeLayout(false);
            this.StartGroupBox.PerformLayout();
            this.EndGroupBox.ResumeLayout(false);
            this.EndGroupBox.PerformLayout();
            this.SCTFormatGroupBox.ResumeLayout(false);
            this.SCTFormatGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.GroupBox FixImportGroupBox;
        private System.Windows.Forms.DataGridView FixListDataGridView;
        private System.Windows.Forms.Label IdentifierLabel;
        private System.Windows.Forms.TextBox IdentifierTextBox;
        private System.Windows.Forms.GroupBox CenterGroupBox;
        private System.Windows.Forms.TextBox CenterFixTextBox;
        private System.Windows.Forms.Label StartFixLabel;
        private System.Windows.Forms.TextBox CenterLongitudeTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox CenterLatitudeTextBox;
        private System.Windows.Forms.Label startPointLatitudeLabel;
        private System.Windows.Forms.Button Fix2CenterButton;
        private System.Windows.Forms.GroupBox StartGroupBox;
        private System.Windows.Forms.TextBox StartFixTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox StartLongitudeTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox StartLatitudeTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button FixToSTartButton;
        private System.Windows.Forms.GroupBox EndGroupBox;
        private System.Windows.Forms.TextBox EndFixTextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox EndLongitudeTextBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox EndLatitudeTextBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button FixToEndButton;
        private System.Windows.Forms.Label CenterRadiusLabel;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox CalcDistanceTextBox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox StartBrgTextBox;
        private System.Windows.Forms.TextBox EndBrgTestBox;
        private System.Windows.Forms.GroupBox SCTFormatGroupBox;
        private System.Windows.Forms.CheckBox UseFIXNamesCheckBox;
        private System.Windows.Forms.RadioButton GEORadioButton;
        private System.Windows.Forms.RadioButton SSDRadioButton;
        private System.Windows.Forms.RadioButton ARTCCRadioButton;
        private System.Windows.Forms.RadioButton AirwayRadioButton;
        private System.Windows.Forms.Button Copy2ClipboardButton;
        private System.Windows.Forms.Button ClearOutputButton;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox OutputTextBox;
        private System.Windows.Forms.Button SaveOutput2FileButton;
        private System.Windows.Forms.TextBox SuffixTextBox;
        private System.Windows.Forms.TextBox PrefixTextBox;
        private System.Windows.Forms.Label SuffixLabel;
        private System.Windows.Forms.Label PrefixLabel;
        private System.Windows.Forms.Button AddArcByRadialsButton;
        private System.Windows.Forms.Button AddArcByCoordsButton;
        private System.Windows.Forms.Label MagOffsetLabel;
    }
}