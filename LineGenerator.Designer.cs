namespace SCTBuilder
{
    partial class LineGenerator
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LineGenerator));
            this.StartGroupBox = new System.Windows.Forms.GroupBox();
            this.StartLongitudeTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.StartLatitudeTextBox = new System.Windows.Forms.TextBox();
            this.startPointLatitudeLabel = new System.Windows.Forms.Label();
            this.EndPointGroupBo = new System.Windows.Forms.GroupBox();
            this.EndLongitudeTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.EndLatitudeTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.FixImportGroupBox = new System.Windows.Forms.GroupBox();
            this.FixListDataGridView = new System.Windows.Forms.DataGridView();
            this.IdentifierLabel = new System.Windows.Forms.Label();
            this.IdentifierTextBox = new System.Windows.Forms.TextBox();
            this.ImportFix2StartButton = new System.Windows.Forms.Button();
            this.ImportFix2EndButton = new System.Windows.Forms.Button();
            this.CalcEndButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.CalcDistFeetRadioButton = new System.Windows.Forms.RadioButton();
            this.CalcDistMeterRadioButton = new System.Windows.Forms.RadioButton();
            this.CalcDistSMRadioButton = new System.Windows.Forms.RadioButton();
            this.CalcDistNMRadioButton = new System.Windows.Forms.RadioButton();
            this.CalcDistanceTextBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.CalcMagVarTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.CalcBearingTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.GEORadioButton = new System.Windows.Forms.RadioButton();
            this.SSDRadioButton = new System.Windows.Forms.RadioButton();
            this.ARTCCRadioButton = new System.Windows.Forms.RadioButton();
            this.AirwayRadioButton = new System.Windows.Forms.RadioButton();
            this.CopyEnd2StartButton = new System.Windows.Forms.Button();
            this.CopyStart2EndButton = new System.Windows.Forms.Button();
            this.SwitchStartEndButton = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label12 = new System.Windows.Forms.Label();
            this.DashedLineLengthTextBox = new System.Windows.Forms.TextBox();
            this.DotDashedLineRadioButton = new System.Windows.Forms.RadioButton();
            this.DashedLineRadioButton = new System.Windows.Forms.RadioButton();
            this.SolidLineRadioButton = new System.Windows.Forms.RadioButton();
            this.PrefixLabel = new System.Windows.Forms.Label();
            this.SuffixLabel = new System.Windows.Forms.Label();
            this.PrefixTextBox = new System.Windows.Forms.TextBox();
            this.SuffixTextBox = new System.Windows.Forms.TextBox();
            this.AddNextButton = new System.Windows.Forms.Button();
            this.AddLineButton = new System.Windows.Forms.Button();
            this.SaveOutput2FileButton = new System.Windows.Forms.Button();
            this.OutputTextBox = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.ClearOutputButton = new System.Windows.Forms.Button();
            this.Copy2ClipboardButton = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.LatLongHelpButton = new System.Windows.Forms.Button();
            this.ColorValueTextBox = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.ColorGroupBox = new System.Windows.Forms.GroupBox();
            this.ColorValueLabel = new System.Windows.Forms.Label();
            this.ColorNameTextBox = new System.Windows.Forms.TextBox();
            this.ColorNameLabel = new System.Windows.Forms.Label();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.LineDistTextBox = new System.Windows.Forms.TextBox();
            this.LineDistLabel = new System.Windows.Forms.Label();
            this.LineBrgTextBox = new System.Windows.Forms.TextBox();
            this.LineBrgLabel = new System.Windows.Forms.Label();
            this.UseFIXNamesCheckBox = new System.Windows.Forms.CheckBox();
            this.StartFixLabel = new System.Windows.Forms.Label();
            this.StartFixTextBox = new System.Windows.Forms.TextBox();
            this.EndFixTextBox = new System.Windows.Forms.TextBox();
            this.EndFixTextBoxLabel = new System.Windows.Forms.Label();
            this.StartGroupBox.SuspendLayout();
            this.EndPointGroupBo.SuspendLayout();
            this.FixImportGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FixListDataGridView)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.ColorGroupBox.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // StartGroupBox
            // 
            this.StartGroupBox.Controls.Add(this.StartFixTextBox);
            this.StartGroupBox.Controls.Add(this.StartFixLabel);
            this.StartGroupBox.Controls.Add(this.StartLongitudeTextBox);
            this.StartGroupBox.Controls.Add(this.label1);
            this.StartGroupBox.Controls.Add(this.StartLatitudeTextBox);
            this.StartGroupBox.Controls.Add(this.startPointLatitudeLabel);
            this.StartGroupBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StartGroupBox.Location = new System.Drawing.Point(31, 88);
            this.StartGroupBox.Name = "StartGroupBox";
            this.StartGroupBox.Size = new System.Drawing.Size(284, 157);
            this.StartGroupBox.TabIndex = 0;
            this.StartGroupBox.TabStop = false;
            this.StartGroupBox.Text = "Start point coordinates";
            // 
            // StartLongitudeTextBox
            // 
            this.StartLongitudeTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StartLongitudeTextBox.Location = new System.Drawing.Point(110, 77);
            this.StartLongitudeTextBox.Name = "StartLongitudeTextBox";
            this.StartLongitudeTextBox.Size = new System.Drawing.Size(160, 28);
            this.StartLongitudeTextBox.TabIndex = 2;
            this.StartLongitudeTextBox.Validated += new System.EventHandler(this.StartLongitudeTextBox_Validated);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 81);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 25);
            this.label1.TabIndex = 2;
            this.label1.Text = "Longitude";
            // 
            // StartLatitudeTextBox
            // 
            this.StartLatitudeTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StartLatitudeTextBox.Location = new System.Drawing.Point(110, 43);
            this.StartLatitudeTextBox.Name = "StartLatitudeTextBox";
            this.StartLatitudeTextBox.Size = new System.Drawing.Size(160, 28);
            this.StartLatitudeTextBox.TabIndex = 1;
            this.StartLatitudeTextBox.Validated += new System.EventHandler(this.StartLatitudeTextBox_Validated);
            // 
            // startPointLatitudeLabel
            // 
            this.startPointLatitudeLabel.AutoSize = true;
            this.startPointLatitudeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.startPointLatitudeLabel.Location = new System.Drawing.Point(6, 44);
            this.startPointLatitudeLabel.Name = "startPointLatitudeLabel";
            this.startPointLatitudeLabel.Size = new System.Drawing.Size(81, 25);
            this.startPointLatitudeLabel.TabIndex = 0;
            this.startPointLatitudeLabel.Text = "Latitude";
            // 
            // EndPointGroupBo
            // 
            this.EndPointGroupBo.Controls.Add(this.EndFixTextBox);
            this.EndPointGroupBo.Controls.Add(this.EndFixTextBoxLabel);
            this.EndPointGroupBo.Controls.Add(this.EndLongitudeTextBox);
            this.EndPointGroupBo.Controls.Add(this.label2);
            this.EndPointGroupBo.Controls.Add(this.EndLatitudeTextBox);
            this.EndPointGroupBo.Controls.Add(this.label3);
            this.EndPointGroupBo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EndPointGroupBo.Location = new System.Drawing.Point(31, 307);
            this.EndPointGroupBo.Name = "EndPointGroupBo";
            this.EndPointGroupBo.Size = new System.Drawing.Size(284, 154);
            this.EndPointGroupBo.TabIndex = 1;
            this.EndPointGroupBo.TabStop = false;
            this.EndPointGroupBo.Text = "End point coordinates";
            // 
            // EndLongitudeTextBox
            // 
            this.EndLongitudeTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EndLongitudeTextBox.Location = new System.Drawing.Point(111, 75);
            this.EndLongitudeTextBox.Name = "EndLongitudeTextBox";
            this.EndLongitudeTextBox.Size = new System.Drawing.Size(160, 28);
            this.EndLongitudeTextBox.TabIndex = 4;
            this.EndLongitudeTextBox.Validated += new System.EventHandler(this.EndLongitudeTextBox_Validated);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(7, 76);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 25);
            this.label2.TabIndex = 2;
            this.label2.Text = "Longitude";
            // 
            // EndLatitudeTextBox
            // 
            this.EndLatitudeTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EndLatitudeTextBox.Location = new System.Drawing.Point(110, 41);
            this.EndLatitudeTextBox.Name = "EndLatitudeTextBox";
            this.EndLatitudeTextBox.Size = new System.Drawing.Size(160, 28);
            this.EndLatitudeTextBox.TabIndex = 3;
            this.EndLatitudeTextBox.Validated += new System.EventHandler(this.EndLatitudeTextBox_Validated);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(6, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 25);
            this.label3.TabIndex = 0;
            this.label3.Text = "Latitude";
            // 
            // FixImportGroupBox
            // 
            this.FixImportGroupBox.Controls.Add(this.FixListDataGridView);
            this.FixImportGroupBox.Controls.Add(this.IdentifierLabel);
            this.FixImportGroupBox.Controls.Add(this.IdentifierTextBox);
            this.FixImportGroupBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FixImportGroupBox.Location = new System.Drawing.Point(385, 88);
            this.FixImportGroupBox.Name = "FixImportGroupBox";
            this.FixImportGroupBox.Size = new System.Drawing.Size(342, 354);
            this.FixImportGroupBox.TabIndex = 8;
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
            this.FixListDataGridView.MultiSelect = false;
            this.FixListDataGridView.Name = "FixListDataGridView";
            this.FixListDataGridView.ReadOnly = true;
            this.FixListDataGridView.RowHeadersVisible = false;
            this.FixListDataGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.FixListDataGridView.RowTemplate.Height = 28;
            this.FixListDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.FixListDataGridView.Size = new System.Drawing.Size(304, 279);
            this.FixListDataGridView.TabIndex = 2;
            this.FixListDataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.FixListDataGridView_CellContentClick);
            // 
            // IdentifierLabel
            // 
            this.IdentifierLabel.AutoSize = true;
            this.IdentifierLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IdentifierLabel.Location = new System.Drawing.Point(16, 33);
            this.IdentifierLabel.Name = "IdentifierLabel";
            this.IdentifierLabel.Size = new System.Drawing.Size(85, 25);
            this.IdentifierLabel.TabIndex = 1;
            this.IdentifierLabel.Text = "Identifier";
            // 
            // IdentifierTextBox
            // 
            this.IdentifierTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IdentifierTextBox.Location = new System.Drawing.Point(107, 28);
            this.IdentifierTextBox.Name = "IdentifierTextBox";
            this.IdentifierTextBox.Size = new System.Drawing.Size(100, 30);
            this.IdentifierTextBox.TabIndex = 0;
            this.IdentifierTextBox.TabStop = false;
            this.IdentifierTextBox.TextChanged += new System.EventHandler(this.IdentifierTextBox_TextChanged);
            // 
            // ImportFix2StartButton
            // 
            this.ImportFix2StartButton.BackColor = System.Drawing.Color.White;
            this.ImportFix2StartButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ImportFix2StartButton.BackgroundImage")));
            this.ImportFix2StartButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ImportFix2StartButton.Enabled = false;
            this.ImportFix2StartButton.Location = new System.Drawing.Point(321, 170);
            this.ImportFix2StartButton.Name = "ImportFix2StartButton";
            this.ImportFix2StartButton.Size = new System.Drawing.Size(58, 57);
            this.ImportFix2StartButton.TabIndex = 3;
            this.ImportFix2StartButton.TabStop = false;
            this.ImportFix2StartButton.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.ImportFix2StartButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.ImportFix2StartButton.UseVisualStyleBackColor = false;
            this.ImportFix2StartButton.Click += new System.EventHandler(this.ImportFix2StartButton_Click);
            // 
            // ImportFix2EndButton
            // 
            this.ImportFix2EndButton.BackColor = System.Drawing.Color.White;
            this.ImportFix2EndButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ImportFix2EndButton.BackgroundImage")));
            this.ImportFix2EndButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ImportFix2EndButton.Enabled = false;
            this.ImportFix2EndButton.Location = new System.Drawing.Point(321, 353);
            this.ImportFix2EndButton.Name = "ImportFix2EndButton";
            this.ImportFix2EndButton.Size = new System.Drawing.Size(58, 57);
            this.ImportFix2EndButton.TabIndex = 4;
            this.ImportFix2EndButton.TabStop = false;
            this.ImportFix2EndButton.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.ImportFix2EndButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.ImportFix2EndButton.UseVisualStyleBackColor = false;
            this.ImportFix2EndButton.Click += new System.EventHandler(this.ImportFix2EndButton_Click);
            // 
            // CalcEndButton
            // 
            this.CalcEndButton.BackColor = System.Drawing.Color.White;
            this.CalcEndButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("CalcEndButton.BackgroundImage")));
            this.CalcEndButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CalcEndButton.Enabled = false;
            this.CalcEndButton.Location = new System.Drawing.Point(276, 467);
            this.CalcEndButton.Name = "CalcEndButton";
            this.CalcEndButton.Size = new System.Drawing.Size(106, 78);
            this.CalcEndButton.TabIndex = 5;
            this.CalcEndButton.TabStop = false;
            this.CalcEndButton.UseVisualStyleBackColor = false;
            this.CalcEndButton.Click += new System.EventHandler(this.CalcEndButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.CalcDistanceTextBox);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.CalcMagVarTextBox);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.CalcBearingTextBox);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(385, 467);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(342, 226);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Calculate End Point";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.CalcDistFeetRadioButton);
            this.groupBox2.Controls.Add(this.CalcDistMeterRadioButton);
            this.groupBox2.Controls.Add(this.CalcDistSMRadioButton);
            this.groupBox2.Controls.Add(this.CalcDistNMRadioButton);
            this.groupBox2.Location = new System.Drawing.Point(39, 157);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(286, 63);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Measured as";
            // 
            // CalcDistFeetRadioButton
            // 
            this.CalcDistFeetRadioButton.AutoSize = true;
            this.CalcDistFeetRadioButton.Location = new System.Drawing.Point(84, 28);
            this.CalcDistFeetRadioButton.Name = "CalcDistFeetRadioButton";
            this.CalcDistFeetRadioButton.Size = new System.Drawing.Size(47, 29);
            this.CalcDistFeetRadioButton.TabIndex = 2;
            this.CalcDistFeetRadioButton.Text = "ft";
            this.CalcDistFeetRadioButton.UseVisualStyleBackColor = true;
            // 
            // CalcDistMeterRadioButton
            // 
            this.CalcDistMeterRadioButton.AutoSize = true;
            this.CalcDistMeterRadioButton.Location = new System.Drawing.Point(218, 28);
            this.CalcDistMeterRadioButton.Name = "CalcDistMeterRadioButton";
            this.CalcDistMeterRadioButton.Size = new System.Drawing.Size(53, 29);
            this.CalcDistMeterRadioButton.TabIndex = 4;
            this.CalcDistMeterRadioButton.Text = "m";
            this.CalcDistMeterRadioButton.UseVisualStyleBackColor = true;
            // 
            // CalcDistSMRadioButton
            // 
            this.CalcDistSMRadioButton.AutoSize = true;
            this.CalcDistSMRadioButton.Checked = true;
            this.CalcDistSMRadioButton.Location = new System.Drawing.Point(10, 28);
            this.CalcDistSMRadioButton.Name = "CalcDistSMRadioButton";
            this.CalcDistSMRadioButton.Size = new System.Drawing.Size(68, 29);
            this.CalcDistSMRadioButton.TabIndex = 1;
            this.CalcDistSMRadioButton.TabStop = true;
            this.CalcDistSMRadioButton.Text = "SM";
            this.CalcDistSMRadioButton.UseVisualStyleBackColor = true;
            // 
            // CalcDistNMRadioButton
            // 
            this.CalcDistNMRadioButton.AutoSize = true;
            this.CalcDistNMRadioButton.Location = new System.Drawing.Point(144, 28);
            this.CalcDistNMRadioButton.Name = "CalcDistNMRadioButton";
            this.CalcDistNMRadioButton.Size = new System.Drawing.Size(68, 29);
            this.CalcDistNMRadioButton.TabIndex = 3;
            this.CalcDistNMRadioButton.Text = "NM";
            this.CalcDistNMRadioButton.UseVisualStyleBackColor = true;
            // 
            // CalcDistanceTextBox
            // 
            this.CalcDistanceTextBox.Location = new System.Drawing.Point(165, 121);
            this.CalcDistanceTextBox.Name = "CalcDistanceTextBox";
            this.CalcDistanceTextBox.Size = new System.Drawing.Size(100, 30);
            this.CalcDistanceTextBox.TabIndex = 2;
            this.CalcDistanceTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CalcDistanceTextBox_KeyPress);
            this.CalcDistanceTextBox.Validated += new System.EventHandler(this.CalcDistanceTextBox_Validated);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(67, 124);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(88, 25);
            this.label6.TabIndex = 4;
            this.label6.Text = "Distance";
            // 
            // CalcMagVarTextBox
            // 
            this.CalcMagVarTextBox.Location = new System.Drawing.Point(165, 79);
            this.CalcMagVarTextBox.Name = "CalcMagVarTextBox";
            this.CalcMagVarTextBox.Size = new System.Drawing.Size(100, 30);
            this.CalcMagVarTextBox.TabIndex = 1;
            this.CalcMagVarTextBox.TextChanged += new System.EventHandler(this.CalcMagVarTextBox_TextChanged);
            this.CalcMagVarTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CalcMagVarTextBox_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(26, 79);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(133, 25);
            this.label5.TabIndex = 2;
            this.label5.Text = "Mag Variation";
            // 
            // CalcBearingTextBox
            // 
            this.CalcBearingTextBox.Location = new System.Drawing.Point(165, 37);
            this.CalcBearingTextBox.Name = "CalcBearingTextBox";
            this.CalcBearingTextBox.Size = new System.Drawing.Size(100, 30);
            this.CalcBearingTextBox.TabIndex = 0;
            this.CalcBearingTextBox.TextChanged += new System.EventHandler(this.CalcBearingTextBox_TextChanged);
            this.CalcBearingTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CalcBearingTextBox_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(21, 37);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(134, 25);
            this.label4.TabIndex = 0;
            this.label4.Text = "Bearing (Deg)";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.UseFIXNamesCheckBox);
            this.groupBox3.Controls.Add(this.GEORadioButton);
            this.groupBox3.Controls.Add(this.SSDRadioButton);
            this.groupBox3.Controls.Add(this.ARTCCRadioButton);
            this.groupBox3.Controls.Add(this.AirwayRadioButton);
            this.groupBox3.Location = new System.Drawing.Point(30, 672);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(335, 98);
            this.groupBox3.TabIndex = 7;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Desired SCT format";
            // 
            // GEORadioButton
            // 
            this.GEORadioButton.AutoSize = true;
            this.GEORadioButton.Location = new System.Drawing.Point(133, 26);
            this.GEORadioButton.Name = "GEORadioButton";
            this.GEORadioButton.Size = new System.Drawing.Size(70, 24);
            this.GEORadioButton.TabIndex = 1;
            this.GEORadioButton.Text = "GEO";
            this.GEORadioButton.UseVisualStyleBackColor = true;
            // 
            // SSDRadioButton
            // 
            this.SSDRadioButton.AutoSize = true;
            this.SSDRadioButton.Checked = true;
            this.SSDRadioButton.Location = new System.Drawing.Point(17, 28);
            this.SSDRadioButton.Name = "SSDRadioButton";
            this.SSDRadioButton.Size = new System.Drawing.Size(110, 24);
            this.SSDRadioButton.TabIndex = 0;
            this.SSDRadioButton.TabStop = true;
            this.SSDRadioButton.Text = "SID|STAR";
            this.SSDRadioButton.UseVisualStyleBackColor = true;
            this.SSDRadioButton.CheckedChanged += new System.EventHandler(this.SSDRadioButton_CheckedChanged);
            // 
            // ARTCCRadioButton
            // 
            this.ARTCCRadioButton.AutoSize = true;
            this.ARTCCRadioButton.Location = new System.Drawing.Point(17, 58);
            this.ARTCCRadioButton.Name = "ARTCCRadioButton";
            this.ARTCCRadioButton.Size = new System.Drawing.Size(127, 24);
            this.ARTCCRadioButton.TabIndex = 3;
            this.ARTCCRadioButton.Text = "ARTCC|SUA";
            this.ARTCCRadioButton.UseVisualStyleBackColor = true;
            this.ARTCCRadioButton.CheckedChanged += new System.EventHandler(this.ARTCCRadioButton_CheckedChanged);
            // 
            // AirwayRadioButton
            // 
            this.AirwayRadioButton.AutoSize = true;
            this.AirwayRadioButton.Location = new System.Drawing.Point(209, 26);
            this.AirwayRadioButton.Name = "AirwayRadioButton";
            this.AirwayRadioButton.Size = new System.Drawing.Size(80, 24);
            this.AirwayRadioButton.TabIndex = 4;
            this.AirwayRadioButton.Text = "Airway";
            this.AirwayRadioButton.UseVisualStyleBackColor = true;
            this.AirwayRadioButton.CheckedChanged += new System.EventHandler(this.AirwayRadioButton_CheckedChanged);
            // 
            // CopyEnd2StartButton
            // 
            this.CopyEnd2StartButton.BackColor = System.Drawing.Color.White;
            this.CopyEnd2StartButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("CopyEnd2StartButton.BackgroundImage")));
            this.CopyEnd2StartButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CopyEnd2StartButton.Enabled = false;
            this.CopyEnd2StartButton.Location = new System.Drawing.Point(77, 251);
            this.CopyEnd2StartButton.Name = "CopyEnd2StartButton";
            this.CopyEnd2StartButton.Size = new System.Drawing.Size(58, 50);
            this.CopyEnd2StartButton.TabIndex = 20;
            this.CopyEnd2StartButton.TabStop = false;
            this.toolTip1.SetToolTip(this.CopyEnd2StartButton, "Copy End point coordinate to Start point coordinate");
            this.CopyEnd2StartButton.UseVisualStyleBackColor = false;
            this.CopyEnd2StartButton.Click += new System.EventHandler(this.CopyEnd2StartButton_Click);
            // 
            // CopyStart2EndButton
            // 
            this.CopyStart2EndButton.BackColor = System.Drawing.Color.White;
            this.CopyStart2EndButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("CopyStart2EndButton.BackgroundImage")));
            this.CopyStart2EndButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CopyStart2EndButton.Enabled = false;
            this.CopyStart2EndButton.Location = new System.Drawing.Point(142, 250);
            this.CopyStart2EndButton.Name = "CopyStart2EndButton";
            this.CopyStart2EndButton.Size = new System.Drawing.Size(58, 50);
            this.CopyStart2EndButton.TabIndex = 21;
            this.CopyStart2EndButton.TabStop = false;
            this.toolTip1.SetToolTip(this.CopyStart2EndButton, "Copy Start point coordinate to End point coordinate");
            this.CopyStart2EndButton.UseVisualStyleBackColor = false;
            this.CopyStart2EndButton.Click += new System.EventHandler(this.CopyStart2EndButton_Click);
            // 
            // SwitchStartEndButton
            // 
            this.SwitchStartEndButton.BackColor = System.Drawing.Color.White;
            this.SwitchStartEndButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("SwitchStartEndButton.BackgroundImage")));
            this.SwitchStartEndButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.SwitchStartEndButton.Enabled = false;
            this.SwitchStartEndButton.Location = new System.Drawing.Point(227, 251);
            this.SwitchStartEndButton.Name = "SwitchStartEndButton";
            this.SwitchStartEndButton.Size = new System.Drawing.Size(58, 50);
            this.SwitchStartEndButton.TabIndex = 22;
            this.SwitchStartEndButton.TabStop = false;
            this.toolTip1.SetToolTip(this.SwitchStartEndButton, "Exchange Start and End point coordinates");
            this.SwitchStartEndButton.UseVisualStyleBackColor = false;
            this.SwitchStartEndButton.Click += new System.EventHandler(this.SwitchStartEndButton_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label12);
            this.groupBox4.Controls.Add(this.DashedLineLengthTextBox);
            this.groupBox4.Controls.Add(this.DotDashedLineRadioButton);
            this.groupBox4.Controls.Add(this.DashedLineRadioButton);
            this.groupBox4.Controls.Add(this.SolidLineRadioButton);
            this.groupBox4.Location = new System.Drawing.Point(30, 567);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(336, 99);
            this.groupBox4.TabIndex = 4;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Line Type";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(29, 62);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(191, 20);
            this.label12.TabIndex = 4;
            this.label12.Text = "Dashed Line Length (NM)";
            // 
            // DashedLineLengthTextBox
            // 
            this.DashedLineLengthTextBox.Location = new System.Drawing.Point(226, 59);
            this.DashedLineLengthTextBox.Name = "DashedLineLengthTextBox";
            this.DashedLineLengthTextBox.Size = new System.Drawing.Size(71, 26);
            this.DashedLineLengthTextBox.TabIndex = 3;
            this.toolTip1.SetToolTip(this.DashedLineLengthTextBox, "Length of dashed line. Suggested: short-0.5, medium 2.0, long 6.0)");
            this.DashedLineLengthTextBox.TextChanged += new System.EventHandler(this.DashedLineLengthTextBox_TextChanged);
            // 
            // DotDashedLineRadioButton
            // 
            this.DotDashedLineRadioButton.AutoSize = true;
            this.DotDashedLineRadioButton.Location = new System.Drawing.Point(180, 28);
            this.DotDashedLineRadioButton.Name = "DotDashedLineRadioButton";
            this.DotDashedLineRadioButton.Size = new System.Drawing.Size(121, 24);
            this.DotDashedLineRadioButton.TabIndex = 2;
            this.DotDashedLineRadioButton.Text = "Dot-Dashed";
            this.DotDashedLineRadioButton.UseVisualStyleBackColor = true;
            // 
            // DashedLineRadioButton
            // 
            this.DashedLineRadioButton.AutoSize = true;
            this.DashedLineRadioButton.Location = new System.Drawing.Point(84, 28);
            this.DashedLineRadioButton.Name = "DashedLineRadioButton";
            this.DashedLineRadioButton.Size = new System.Drawing.Size(90, 24);
            this.DashedLineRadioButton.TabIndex = 1;
            this.DashedLineRadioButton.Text = "Dashed";
            this.DashedLineRadioButton.UseVisualStyleBackColor = true;
            // 
            // SolidLineRadioButton
            // 
            this.SolidLineRadioButton.AutoSize = true;
            this.SolidLineRadioButton.Checked = true;
            this.SolidLineRadioButton.Location = new System.Drawing.Point(10, 30);
            this.SolidLineRadioButton.Name = "SolidLineRadioButton";
            this.SolidLineRadioButton.Size = new System.Drawing.Size(69, 24);
            this.SolidLineRadioButton.TabIndex = 0;
            this.SolidLineRadioButton.TabStop = true;
            this.SolidLineRadioButton.Text = "Solid";
            this.SolidLineRadioButton.UseVisualStyleBackColor = true;
            // 
            // PrefixLabel
            // 
            this.PrefixLabel.AutoSize = true;
            this.PrefixLabel.Location = new System.Drawing.Point(20, 788);
            this.PrefixLabel.Name = "PrefixLabel";
            this.PrefixLabel.Size = new System.Drawing.Size(48, 20);
            this.PrefixLabel.TabIndex = 24;
            this.PrefixLabel.Text = "Prefix";
            // 
            // SuffixLabel
            // 
            this.SuffixLabel.AutoSize = true;
            this.SuffixLabel.Location = new System.Drawing.Point(20, 820);
            this.SuffixLabel.Name = "SuffixLabel";
            this.SuffixLabel.Size = new System.Drawing.Size(78, 20);
            this.SuffixLabel.TabIndex = 25;
            this.SuffixLabel.Text = "Comment";
            // 
            // PrefixTextBox
            // 
            this.PrefixTextBox.Location = new System.Drawing.Point(104, 785);
            this.PrefixTextBox.Name = "PrefixTextBox";
            this.PrefixTextBox.Size = new System.Drawing.Size(258, 26);
            this.PrefixTextBox.TabIndex = 5;
            this.toolTip1.SetToolTip(this.PrefixTextBox, "Prefix for titles and sections");
            this.PrefixTextBox.TextChanged += new System.EventHandler(this.PrefixTextBox_TextChanged);
            // 
            // SuffixTextBox
            // 
            this.SuffixTextBox.Location = new System.Drawing.Point(104, 817);
            this.SuffixTextBox.Name = "SuffixTextBox";
            this.SuffixTextBox.Size = new System.Drawing.Size(258, 26);
            this.SuffixTextBox.TabIndex = 6;
            this.toolTip1.SetToolTip(this.SuffixTextBox, "Comment after line description. Semicolon NOT required.");
            // 
            // AddNextButton
            // 
            this.AddNextButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AddNextButton.Location = new System.Drawing.Point(154, 469);
            this.AddNextButton.Name = "AddNextButton";
            this.AddNextButton.Size = new System.Drawing.Size(112, 71);
            this.AddNextButton.TabIndex = 2;
            this.AddNextButton.Text = "Add and Next";
            this.AddNextButton.UseVisualStyleBackColor = true;
            // 
            // AddLineButton
            // 
            this.AddLineButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AddLineButton.Location = new System.Drawing.Point(36, 469);
            this.AddLineButton.Name = "AddLineButton";
            this.AddLineButton.Size = new System.Drawing.Size(112, 71);
            this.AddLineButton.TabIndex = 4;
            this.AddLineButton.Text = "Add Line";
            this.AddLineButton.UseVisualStyleBackColor = true;
            this.AddLineButton.Click += new System.EventHandler(this.AddLineButton_Click);
            // 
            // SaveOutput2FileButton
            // 
            this.SaveOutput2FileButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SaveOutput2FileButton.Location = new System.Drawing.Point(496, 788);
            this.SaveOutput2FileButton.Name = "SaveOutput2FileButton";
            this.SaveOutput2FileButton.Size = new System.Drawing.Size(121, 80);
            this.SaveOutput2FileButton.TabIndex = 11;
            this.SaveOutput2FileButton.Text = "Save to File";
            this.SaveOutput2FileButton.UseVisualStyleBackColor = true;
            this.SaveOutput2FileButton.Click += new System.EventHandler(this.SaveOutput2FileButton_Click);
            // 
            // OutputTextBox
            // 
            this.OutputTextBox.Location = new System.Drawing.Point(31, 874);
            this.OutputTextBox.Multiline = true;
            this.OutputTextBox.Name = "OutputTextBox";
            this.OutputTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.OutputTextBox.Size = new System.Drawing.Size(696, 143);
            this.OutputTextBox.TabIndex = 13;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(33, 848);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(131, 20);
            this.label9.TabIndex = 32;
            this.label9.Text = "Current Contents";
            // 
            // ClearOutputButton
            // 
            this.ClearOutputButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.ClearOutputButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ClearOutputButton.Location = new System.Drawing.Point(610, 788);
            this.ClearOutputButton.Name = "ClearOutputButton";
            this.ClearOutputButton.Size = new System.Drawing.Size(117, 80);
            this.ClearOutputButton.TabIndex = 12;
            this.ClearOutputButton.Text = "Clear Contents";
            this.ClearOutputButton.UseVisualStyleBackColor = false;
            this.ClearOutputButton.Click += new System.EventHandler(this.ClearOutputButton_Click);
            // 
            // Copy2ClipboardButton
            // 
            this.Copy2ClipboardButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Copy2ClipboardButton.Location = new System.Drawing.Point(385, 788);
            this.Copy2ClipboardButton.Name = "Copy2ClipboardButton";
            this.Copy2ClipboardButton.Size = new System.Drawing.Size(118, 80);
            this.Copy2ClipboardButton.TabIndex = 10;
            this.Copy2ClipboardButton.Text = "Copy to Clipboard";
            this.Copy2ClipboardButton.UseVisualStyleBackColor = true;
            this.Copy2ClipboardButton.Click += new System.EventHandler(this.Copy2ClipboardButton_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(23, 13);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(0, 20);
            this.label10.TabIndex = 35;
            // 
            // LatLongHelpButton
            // 
            this.LatLongHelpButton.Location = new System.Drawing.Point(282, 62);
            this.LatLongHelpButton.Name = "LatLongHelpButton";
            this.LatLongHelpButton.Size = new System.Drawing.Size(33, 31);
            this.LatLongHelpButton.TabIndex = 36;
            this.LatLongHelpButton.Text = "?";
            this.toolTip1.SetToolTip(this.LatLongHelpButton, "Display information on entering coordinates");
            this.LatLongHelpButton.UseVisualStyleBackColor = true;
            this.LatLongHelpButton.Click += new System.EventHandler(this.LatLongHelpButton_Click);
            // 
            // ColorValueTextBox
            // 
            this.ColorValueTextBox.Location = new System.Drawing.Point(235, 26);
            this.ColorValueTextBox.Name = "ColorValueTextBox";
            this.ColorValueTextBox.Size = new System.Drawing.Size(100, 26);
            this.ColorValueTextBox.TabIndex = 3;
            this.toolTip1.SetToolTip(this.ColorValueTextBox, "Double-click for color selection dialog");
            this.ColorValueTextBox.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ColorValueTextBox_MouseDoubleClick);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(30, 13);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(376, 32);
            this.label11.TabIndex = 37;
            this.label11.Text = "Line Generator w/ Calculator";
            // 
            // ColorGroupBox
            // 
            this.ColorGroupBox.Controls.Add(this.ColorValueTextBox);
            this.ColorGroupBox.Controls.Add(this.ColorValueLabel);
            this.ColorGroupBox.Controls.Add(this.ColorNameTextBox);
            this.ColorGroupBox.Controls.Add(this.ColorNameLabel);
            this.ColorGroupBox.Location = new System.Drawing.Point(388, 705);
            this.ColorGroupBox.Name = "ColorGroupBox";
            this.ColorGroupBox.Size = new System.Drawing.Size(342, 65);
            this.ColorGroupBox.TabIndex = 38;
            this.ColorGroupBox.TabStop = false;
            this.ColorGroupBox.Text = "Line Color (optional))";
            // 
            // ColorValueLabel
            // 
            this.ColorValueLabel.AutoSize = true;
            this.ColorValueLabel.Location = new System.Drawing.Point(178, 27);
            this.ColorValueLabel.Name = "ColorValueLabel";
            this.ColorValueLabel.Size = new System.Drawing.Size(50, 20);
            this.ColorValueLabel.TabIndex = 2;
            this.ColorValueLabel.Text = "Value";
            // 
            // ColorNameTextBox
            // 
            this.ColorNameTextBox.Location = new System.Drawing.Point(71, 28);
            this.ColorNameTextBox.Name = "ColorNameTextBox";
            this.ColorNameTextBox.Size = new System.Drawing.Size(100, 26);
            this.ColorNameTextBox.TabIndex = 1;
            // 
            // ColorNameLabel
            // 
            this.ColorNameLabel.AutoSize = true;
            this.ColorNameLabel.Location = new System.Drawing.Point(13, 28);
            this.ColorNameLabel.Name = "ColorNameLabel";
            this.ColorNameLabel.Size = new System.Drawing.Size(51, 20);
            this.ColorNameLabel.TabIndex = 0;
            this.ColorNameLabel.Text = "Name";
            // 
            // colorDialog1
            // 
            this.colorDialog1.AnyColor = true;
            // 
            // groupBox5
            // 
            this.groupBox5.BackColor = System.Drawing.Color.YellowGreen;
            this.groupBox5.Controls.Add(this.LineDistTextBox);
            this.groupBox5.Controls.Add(this.LineDistLabel);
            this.groupBox5.Controls.Add(this.LineBrgTextBox);
            this.groupBox5.Controls.Add(this.LineBrgLabel);
            this.groupBox5.Location = new System.Drawing.Point(447, 13);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(280, 69);
            this.groupBox5.TabIndex = 39;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Line Information";
            // 
            // LineDistTextBox
            // 
            this.LineDistTextBox.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.LineDistTextBox.Enabled = false;
            this.LineDistTextBox.Location = new System.Drawing.Point(195, 27);
            this.LineDistTextBox.Name = "LineDistTextBox";
            this.LineDistTextBox.Size = new System.Drawing.Size(67, 26);
            this.LineDistTextBox.TabIndex = 3;
            this.LineDistTextBox.TabStop = false;
            // 
            // LineDistLabel
            // 
            this.LineDistLabel.AutoSize = true;
            this.LineDistLabel.Location = new System.Drawing.Point(117, 30);
            this.LineDistLabel.Name = "LineDistLabel";
            this.LineDistLabel.Size = new System.Drawing.Size(72, 20);
            this.LineDistLabel.TabIndex = 2;
            this.LineDistLabel.Text = "Distance";
            // 
            // LineBrgTextBox
            // 
            this.LineBrgTextBox.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.LineBrgTextBox.Enabled = false;
            this.LineBrgTextBox.Location = new System.Drawing.Point(48, 26);
            this.LineBrgTextBox.Name = "LineBrgTextBox";
            this.LineBrgTextBox.Size = new System.Drawing.Size(64, 26);
            this.LineBrgTextBox.TabIndex = 1;
            this.LineBrgTextBox.TabStop = false;
            // 
            // LineBrgLabel
            // 
            this.LineBrgLabel.AutoSize = true;
            this.LineBrgLabel.Location = new System.Drawing.Point(8, 29);
            this.LineBrgLabel.Name = "LineBrgLabel";
            this.LineBrgLabel.Size = new System.Drawing.Size(34, 20);
            this.LineBrgLabel.TabIndex = 0;
            this.LineBrgLabel.Text = "Brg";
            // 
            // UseFIXNamesCheckBox
            // 
            this.UseFIXNamesCheckBox.AutoSize = true;
            this.UseFIXNamesCheckBox.Location = new System.Drawing.Point(167, 57);
            this.UseFIXNamesCheckBox.Name = "UseFIXNamesCheckBox";
            this.UseFIXNamesCheckBox.Size = new System.Drawing.Size(146, 24);
            this.UseFIXNamesCheckBox.TabIndex = 5;
            this.UseFIXNamesCheckBox.Text = "Use FIX names";
            this.UseFIXNamesCheckBox.UseVisualStyleBackColor = true;
            // 
            // StartFixLabel
            // 
            this.StartFixLabel.AutoSize = true;
            this.StartFixLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StartFixLabel.Location = new System.Drawing.Point(16, 114);
            this.StartFixLabel.Name = "StartFixLabel";
            this.StartFixLabel.Size = new System.Drawing.Size(38, 25);
            this.StartFixLabel.TabIndex = 3;
            this.StartFixLabel.Text = "Fix";
            // 
            // StartFixTextBox
            // 
            this.StartFixTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StartFixTextBox.Location = new System.Drawing.Point(111, 111);
            this.StartFixTextBox.Name = "StartFixTextBox";
            this.StartFixTextBox.Size = new System.Drawing.Size(160, 28);
            this.StartFixTextBox.TabIndex = 4;
            // 
            // EndFixTextBox
            // 
            this.EndFixTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EndFixTextBox.Location = new System.Drawing.Point(110, 109);
            this.EndFixTextBox.Name = "EndFixTextBox";
            this.EndFixTextBox.Size = new System.Drawing.Size(160, 28);
            this.EndFixTextBox.TabIndex = 41;
            // 
            // EndFixTextBoxLabel
            // 
            this.EndFixTextBoxLabel.AutoSize = true;
            this.EndFixTextBoxLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EndFixTextBoxLabel.Location = new System.Drawing.Point(11, 110);
            this.EndFixTextBoxLabel.Name = "EndFixTextBoxLabel";
            this.EndFixTextBoxLabel.Size = new System.Drawing.Size(38, 25);
            this.EndFixTextBoxLabel.TabIndex = 40;
            this.EndFixTextBoxLabel.Text = "Fix";
            // 
            // LineGenerator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(767, 1029);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.ColorGroupBox);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.LatLongHelpButton);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.Copy2ClipboardButton);
            this.Controls.Add(this.ClearOutputButton);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.OutputTextBox);
            this.Controls.Add(this.SaveOutput2FileButton);
            this.Controls.Add(this.AddLineButton);
            this.Controls.Add(this.AddNextButton);
            this.Controls.Add(this.SuffixTextBox);
            this.Controls.Add(this.PrefixTextBox);
            this.Controls.Add(this.SuffixLabel);
            this.Controls.Add(this.PrefixLabel);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.SwitchStartEndButton);
            this.Controls.Add(this.CopyStart2EndButton);
            this.Controls.Add(this.CopyEnd2StartButton);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.CalcEndButton);
            this.Controls.Add(this.ImportFix2EndButton);
            this.Controls.Add(this.ImportFix2StartButton);
            this.Controls.Add(this.FixImportGroupBox);
            this.Controls.Add(this.EndPointGroupBo);
            this.Controls.Add(this.StartGroupBox);
            this.MaximizeBox = false;
            this.Name = "LineGenerator";
            this.Text = "FEBU -> Line Generator";
            this.Load += new System.EventHandler(this.LineGenerator_Load);
            this.StartGroupBox.ResumeLayout(false);
            this.StartGroupBox.PerformLayout();
            this.EndPointGroupBo.ResumeLayout(false);
            this.EndPointGroupBo.PerformLayout();
            this.FixImportGroupBox.ResumeLayout(false);
            this.FixImportGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FixListDataGridView)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ColorGroupBox.ResumeLayout(false);
            this.ColorGroupBox.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox StartGroupBox;
        private System.Windows.Forms.TextBox StartLongitudeTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox StartLatitudeTextBox;
        private System.Windows.Forms.Label startPointLatitudeLabel;
        private System.Windows.Forms.GroupBox EndPointGroupBo;
        private System.Windows.Forms.TextBox EndLongitudeTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox EndLatitudeTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox FixImportGroupBox;
        private System.Windows.Forms.Label IdentifierLabel;
        private System.Windows.Forms.TextBox IdentifierTextBox;
        private System.Windows.Forms.Button ImportFix2StartButton;
        private System.Windows.Forms.Button ImportFix2EndButton;
        private System.Windows.Forms.Button CalcEndButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton CalcDistFeetRadioButton;
        private System.Windows.Forms.RadioButton CalcDistMeterRadioButton;
        private System.Windows.Forms.RadioButton CalcDistSMRadioButton;
        private System.Windows.Forms.RadioButton CalcDistNMRadioButton;
        private System.Windows.Forms.TextBox CalcDistanceTextBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox CalcMagVarTextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox CalcBearingTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton SSDRadioButton;
        private System.Windows.Forms.RadioButton ARTCCRadioButton;
        private System.Windows.Forms.RadioButton AirwayRadioButton;
        private System.Windows.Forms.RadioButton GEORadioButton;
        private System.Windows.Forms.Button CopyEnd2StartButton;
        private System.Windows.Forms.Button CopyStart2EndButton;
        private System.Windows.Forms.Button SwitchStartEndButton;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.RadioButton DashedLineRadioButton;
        private System.Windows.Forms.RadioButton SolidLineRadioButton;
        private System.Windows.Forms.Label PrefixLabel;
        private System.Windows.Forms.Label SuffixLabel;
        private System.Windows.Forms.TextBox PrefixTextBox;
        private System.Windows.Forms.TextBox SuffixTextBox;
        private System.Windows.Forms.Button AddNextButton;
        private System.Windows.Forms.Button AddLineButton;
        private System.Windows.Forms.Button SaveOutput2FileButton;
        private System.Windows.Forms.TextBox OutputTextBox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button ClearOutputButton;
        private System.Windows.Forms.Button Copy2ClipboardButton;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button LatLongHelpButton;
        private System.Windows.Forms.DataGridView FixListDataGridView;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox DashedLineLengthTextBox;
        private System.Windows.Forms.RadioButton DotDashedLineRadioButton;
        private System.Windows.Forms.GroupBox ColorGroupBox;
        private System.Windows.Forms.TextBox ColorValueTextBox;
        private System.Windows.Forms.Label ColorValueLabel;
        private System.Windows.Forms.TextBox ColorNameTextBox;
        private System.Windows.Forms.Label ColorNameLabel;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.TextBox LineDistTextBox;
        private System.Windows.Forms.Label LineDistLabel;
        private System.Windows.Forms.TextBox LineBrgTextBox;
        private System.Windows.Forms.Label LineBrgLabel;
        private System.Windows.Forms.TextBox StartFixTextBox;
        private System.Windows.Forms.Label StartFixLabel;
        private System.Windows.Forms.TextBox EndFixTextBox;
        private System.Windows.Forms.Label EndFixTextBoxLabel;
        private System.Windows.Forms.CheckBox UseFIXNamesCheckBox;
    }
}