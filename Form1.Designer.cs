namespace SCTBuilder
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.lblInfoSection_Caption = new System.Windows.Forms.Label();
            this.lblCycleInfo = new System.Windows.Forms.Label();
            this.txtDataFolder_label = new System.Windows.Forms.Label();
            this.txtDataFolder = new System.Windows.Forms.TextBox();
            this.txtOutputFolder = new System.Windows.Forms.TextBox();
            this.txtOutputFolder_label = new System.Windows.Forms.Label();
            this.cmdDataFolder = new System.Windows.Forms.Button();
            this.cmdOutputFolder = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.CboARTCC = new System.Windows.Forms.ComboBox();
            this.CboAirport = new System.Windows.Forms.ComboBox();
            this.grpAirspaceClass = new System.Windows.Forms.GroupBox();
            this.btnClassC = new System.Windows.Forms.RadioButton();
            this.btnClassB = new System.Windows.Forms.RadioButton();
            this.txtAsstFacilityEngineer = new System.Windows.Forms.TextBox();
            this.txtFacilityEngineer = new System.Windows.Forms.TextBox();
            this.grpSelectionMethod = new System.Windows.Forms.GroupBox();
            this.btnCircle = new System.Windows.Forms.RadioButton();
            this.btnSquare = new System.Windows.Forms.RadioButton();
            this.btnARTCC = new System.Windows.Forms.RadioButton();
            this.cmdWriteSCT = new System.Windows.Forms.Button();
            this.txtLatSouth = new System.Windows.Forms.TextBox();
            this.txtLongEast = new System.Windows.Forms.TextBox();
            this.txtLongWest = new System.Windows.Forms.TextBox();
            this.txtLatNorth = new System.Windows.Forms.TextBox();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.cmdInstructions = new System.Windows.Forms.Button();
            this.chkbxShowAll = new System.Windows.Forms.CheckBox();
            this.txtGridViewCount = new System.Windows.Forms.TextBox();
            this.lblUpdating = new System.Windows.Forms.Label();
            this.cmdUpdateGrid = new System.Windows.Forms.Button();
            this.lblFilesWarning = new System.Windows.Forms.Label();
            this.chkSSDName = new System.Windows.Forms.CheckBox();
            this.cboARTCC_label = new System.Windows.Forms.Label();
            this.cboAirport_label = new System.Windows.Forms.Label();
            this.labelInfoSection = new System.Windows.Forms.Label();
            this.txtAsstFacilityEngineer_label = new System.Windows.Forms.Label();
            this.txtFacilityEngineer_label = new System.Windows.Forms.Label();
            this.grpSquareLimits = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.nudEast = new System.Windows.Forms.NumericUpDown();
            this.nudSouth = new System.Windows.Forms.NumericUpDown();
            this.nudWest = new System.Windows.Forms.NumericUpDown();
            this.nudNorth = new System.Windows.Forms.NumericUpDown();
            this.lblMargins = new System.Windows.Forms.Label();
            this.txtLatSouth_label = new System.Windows.Forms.Label();
            this.txtLatEast_label = new System.Windows.Forms.Label();
            this.txtLatWest_label = new System.Windows.Forms.Label();
            this.txtLatNorth_label = new System.Windows.Forms.Label();
            this.grpCircle = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dgvAPT = new System.Windows.Forms.DataGridView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dgvVOR = new System.Windows.Forms.DataGridView();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.dgvNDB = new System.Windows.Forms.DataGridView();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.dgvFIX = new System.Windows.Forms.DataGridView();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.dgvRWY = new System.Windows.Forms.DataGridView();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.dgvAWY = new System.Windows.Forms.DataGridView();
            this.tabPage7 = new System.Windows.Forms.TabPage();
            this.dgvSID = new System.Windows.Forms.DataGridView();
            this.tabPage8 = new System.Windows.Forms.TabPage();
            this.dgvSTAR = new System.Windows.Forms.DataGridView();
            this.tabPage9 = new System.Windows.Forms.TabPage();
            this.dgvARB = new System.Windows.Forms.DataGridView();
            this.tabPage10 = new System.Windows.Forms.TabPage();
            this.dgvSUA = new System.Windows.Forms.DataGridView();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.lblLogo = new System.Windows.Forms.Label();
            this.cmdExit = new System.Windows.Forms.Button();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.cmdLocalSectors = new System.Windows.Forms.Button();
            this.chkAPTs = new System.Windows.Forms.CheckBox();
            this.chkVORs = new System.Windows.Forms.CheckBox();
            this.chkNDBs = new System.Windows.Forms.CheckBox();
            this.chkRWYs = new System.Windows.Forms.CheckBox();
            this.chkFIXes = new System.Windows.Forms.CheckBox();
            this.chkAWYs = new System.Windows.Forms.CheckBox();
            this.chkSSDs = new System.Windows.Forms.CheckBox();
            this.chkALL = new System.Windows.Forms.CheckBox();
            this.chkARBs = new System.Windows.Forms.CheckBox();
            this.panelSCTsections = new System.Windows.Forms.Panel();
            this.ChkSUA = new System.Windows.Forms.CheckBox();
            this.cmdAddSUAs = new System.Windows.Forms.Button();
            this.panelSUAs = new System.Windows.Forms.Panel();
            this.chkSUA_Danger = new System.Windows.Forms.CheckBox();
            this.chkSUA_Prohibited = new System.Windows.Forms.CheckBox();
            this.chkSUA_Restricted = new System.Windows.Forms.CheckBox();
            this.chkSUA_ClassD = new System.Windows.Forms.CheckBox();
            this.chkSUA_ClassC = new System.Windows.Forms.CheckBox();
            this.chkSUA_ClassB = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.chkOverwrite = new System.Windows.Forms.CheckBox();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.grpAirspaceClass.SuspendLayout();
            this.grpSelectionMethod.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.grpSquareLimits.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudEast)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSouth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudWest)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudNorth)).BeginInit();
            this.grpCircle.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAPT)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVOR)).BeginInit();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNDB)).BeginInit();
            this.tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFIX)).BeginInit();
            this.tabPage5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRWY)).BeginInit();
            this.tabPage6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAWY)).BeginInit();
            this.tabPage7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSID)).BeginInit();
            this.tabPage8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSTAR)).BeginInit();
            this.tabPage9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvARB)).BeginInit();
            this.tabPage10.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSUA)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.panelSCTsections.SuspendLayout();
            this.panelSUAs.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblInfoSection_Caption
            // 
            this.lblInfoSection_Caption.AutoSize = true;
            this.lblInfoSection_Caption.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInfoSection_Caption.Location = new System.Drawing.Point(155, 23);
            this.lblInfoSection_Caption.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblInfoSection_Caption.Name = "lblInfoSection_Caption";
            this.lblInfoSection_Caption.Size = new System.Drawing.Size(122, 17);
            this.lblInfoSection_Caption.TabIndex = 0;
            this.lblInfoSection_Caption.Text = "Data Cycle in use:";
            // 
            // lblCycleInfo
            // 
            this.lblCycleInfo.AutoSize = true;
            this.lblCycleInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCycleInfo.Location = new System.Drawing.Point(155, 41);
            this.lblCycleInfo.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblCycleInfo.Name = "lblCycleInfo";
            this.lblCycleInfo.Size = new System.Drawing.Size(143, 17);
            this.lblCycleInfo.TabIndex = 1;
            this.lblCycleInfo.Text = "No Active Cycle Data!";
            // 
            // txtDataFolder_label
            // 
            this.txtDataFolder_label.AutoSize = true;
            this.txtDataFolder_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDataFolder_label.Location = new System.Drawing.Point(28, 152);
            this.txtDataFolder_label.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.txtDataFolder_label.Name = "txtDataFolder_label";
            this.txtDataFolder_label.Size = new System.Drawing.Size(86, 17);
            this.txtDataFolder_label.TabIndex = 2;
            this.txtDataFolder_label.Text = "Data Folder:";
            // 
            // txtDataFolder
            // 
            this.txtDataFolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDataFolder.Location = new System.Drawing.Point(112, 150);
            this.txtDataFolder.Margin = new System.Windows.Forms.Padding(2);
            this.txtDataFolder.Name = "txtDataFolder";
            this.txtDataFolder.Size = new System.Drawing.Size(242, 23);
            this.txtDataFolder.TabIndex = 3;
            this.toolTip1.SetToolTip(this.txtDataFolder, "Folder containing FAA text files");
            this.txtDataFolder.Validating += new System.ComponentModel.CancelEventHandler(this.TxtDataFolder_Validating);
            this.txtDataFolder.Validated += new System.EventHandler(this.TxtDataFolder_Validated);
            // 
            // txtOutputFolder
            // 
            this.txtOutputFolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOutputFolder.Location = new System.Drawing.Point(112, 175);
            this.txtOutputFolder.Margin = new System.Windows.Forms.Padding(2);
            this.txtOutputFolder.Name = "txtOutputFolder";
            this.txtOutputFolder.Size = new System.Drawing.Size(242, 23);
            this.txtOutputFolder.TabIndex = 5;
            this.toolTip1.SetToolTip(this.txtOutputFolder, "Folder containing sector file");
            this.txtOutputFolder.Validated += new System.EventHandler(this.TxtOutputFolder_Validated);
            // 
            // txtOutputFolder_label
            // 
            this.txtOutputFolder_label.AutoSize = true;
            this.txtOutputFolder_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOutputFolder_label.Location = new System.Drawing.Point(17, 177);
            this.txtOutputFolder_label.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.txtOutputFolder_label.Name = "txtOutputFolder_label";
            this.txtOutputFolder_label.Size = new System.Drawing.Size(99, 17);
            this.txtOutputFolder_label.TabIndex = 4;
            this.txtOutputFolder_label.Text = "Output Folder:";
            // 
            // cmdDataFolder
            // 
            this.cmdDataFolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdDataFolder.Location = new System.Drawing.Point(358, 151);
            this.cmdDataFolder.Margin = new System.Windows.Forms.Padding(2);
            this.cmdDataFolder.Name = "cmdDataFolder";
            this.cmdDataFolder.Size = new System.Drawing.Size(28, 22);
            this.cmdDataFolder.TabIndex = 6;
            this.cmdDataFolder.Text = "...";
            this.toolTip1.SetToolTip(this.cmdDataFolder, "Browse to select data folder");
            this.cmdDataFolder.UseVisualStyleBackColor = true;
            this.cmdDataFolder.Click += new System.EventHandler(this.CmdDataFolder_Click);
            // 
            // cmdOutputFolder
            // 
            this.cmdOutputFolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdOutputFolder.Location = new System.Drawing.Point(358, 176);
            this.cmdOutputFolder.Margin = new System.Windows.Forms.Padding(2);
            this.cmdOutputFolder.Name = "cmdOutputFolder";
            this.cmdOutputFolder.Size = new System.Drawing.Size(28, 22);
            this.cmdOutputFolder.TabIndex = 7;
            this.cmdOutputFolder.Text = "...";
            this.toolTip1.SetToolTip(this.cmdOutputFolder, "Browse to select data folder");
            this.cmdOutputFolder.UseVisualStyleBackColor = true;
            this.cmdOutputFolder.Click += new System.EventHandler(this.CmdOutputFolder_Click);
            // 
            // CboARTCC
            // 
            this.CboARTCC.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CboARTCC.FormattingEnabled = true;
            this.CboARTCC.Location = new System.Drawing.Point(142, 286);
            this.CboARTCC.Margin = new System.Windows.Forms.Padding(2);
            this.CboARTCC.Name = "CboARTCC";
            this.CboARTCC.Size = new System.Drawing.Size(85, 25);
            this.CboARTCC.TabIndex = 8;
            this.toolTip1.SetToolTip(this.CboARTCC, "YOUR ARTCC that will be the name of the sector file (e.g., 1909_ZJX)");
            this.CboARTCC.Validated += new System.EventHandler(this.CboARTCC_Validated);
            // 
            // CboAirport
            // 
            this.CboAirport.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CboAirport.FormattingEnabled = true;
            this.CboAirport.Location = new System.Drawing.Point(142, 312);
            this.CboAirport.Margin = new System.Windows.Forms.Padding(2);
            this.CboAirport.Name = "CboAirport";
            this.CboAirport.Size = new System.Drawing.Size(85, 25);
            this.CboAirport.TabIndex = 10;
            this.toolTip1.SetToolTip(this.CboAirport, "The airport and lat/long where VRC will center when opened");
            this.CboAirport.Validated += new System.EventHandler(this.CboAirport_Validated);
            // 
            // grpAirspaceClass
            // 
            this.grpAirspaceClass.Controls.Add(this.btnClassC);
            this.grpAirspaceClass.Controls.Add(this.btnClassB);
            this.grpAirspaceClass.Location = new System.Drawing.Point(238, 296);
            this.grpAirspaceClass.Margin = new System.Windows.Forms.Padding(2);
            this.grpAirspaceClass.Name = "grpAirspaceClass";
            this.grpAirspaceClass.Padding = new System.Windows.Forms.Padding(2);
            this.grpAirspaceClass.Size = new System.Drawing.Size(89, 39);
            this.grpAirspaceClass.TabIndex = 12;
            this.grpAirspaceClass.TabStop = false;
            this.grpAirspaceClass.Text = "Class";
            this.toolTip1.SetToolTip(this.grpAirspaceClass, "You can select a Class B or C Airport");
            // 
            // btnClassC
            // 
            this.btnClassC.AutoSize = true;
            this.btnClassC.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClassC.Location = new System.Drawing.Point(46, 18);
            this.btnClassC.Margin = new System.Windows.Forms.Padding(2);
            this.btnClassC.Name = "btnClassC";
            this.btnClassC.Size = new System.Drawing.Size(32, 17);
            this.btnClassC.TabIndex = 1;
            this.btnClassC.TabStop = true;
            this.btnClassC.Text = "C";
            this.btnClassC.UseVisualStyleBackColor = true;
            this.btnClassC.CheckedChanged += new System.EventHandler(this.BtnClassC_CheckedChanged);
            // 
            // btnClassB
            // 
            this.btnClassB.AutoSize = true;
            this.btnClassB.Checked = true;
            this.btnClassB.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClassB.Location = new System.Drawing.Point(10, 17);
            this.btnClassB.Margin = new System.Windows.Forms.Padding(2);
            this.btnClassB.Name = "btnClassB";
            this.btnClassB.Size = new System.Drawing.Size(32, 17);
            this.btnClassB.TabIndex = 0;
            this.btnClassB.TabStop = true;
            this.btnClassB.Text = "B";
            this.btnClassB.UseVisualStyleBackColor = true;
            this.btnClassB.CheckedChanged += new System.EventHandler(this.BtnClassB_CheckedChanged);
            // 
            // txtAsstFacilityEngineer
            // 
            this.txtAsstFacilityEngineer.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAsstFacilityEngineer.Location = new System.Drawing.Point(142, 261);
            this.txtAsstFacilityEngineer.Margin = new System.Windows.Forms.Padding(2);
            this.txtAsstFacilityEngineer.Name = "txtAsstFacilityEngineer";
            this.txtAsstFacilityEngineer.Size = new System.Drawing.Size(210, 23);
            this.txtAsstFacilityEngineer.TabIndex = 17;
            this.toolTip1.SetToolTip(this.txtAsstFacilityEngineer, "Assistant Facilities Engineer (can be blank)");
            // 
            // txtFacilityEngineer
            // 
            this.txtFacilityEngineer.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFacilityEngineer.Location = new System.Drawing.Point(142, 236);
            this.txtFacilityEngineer.Margin = new System.Windows.Forms.Padding(2);
            this.txtFacilityEngineer.Name = "txtFacilityEngineer";
            this.txtFacilityEngineer.Size = new System.Drawing.Size(210, 23);
            this.txtFacilityEngineer.TabIndex = 15;
            this.toolTip1.SetToolTip(this.txtFacilityEngineer, "ARTCC Facilities Engineer (may not be blank)");
            this.txtFacilityEngineer.Validating += new System.ComponentModel.CancelEventHandler(this.TxtFacilityEngineer_Validating);
            this.txtFacilityEngineer.Validated += new System.EventHandler(this.TxtFacilityEngineer_Validated);
            // 
            // grpSelectionMethod
            // 
            this.grpSelectionMethod.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(241)))), ((int)(((byte)(255)))));
            this.grpSelectionMethod.Controls.Add(this.btnCircle);
            this.grpSelectionMethod.Controls.Add(this.btnSquare);
            this.grpSelectionMethod.Controls.Add(this.btnARTCC);
            this.grpSelectionMethod.Location = new System.Drawing.Point(31, 351);
            this.grpSelectionMethod.Margin = new System.Windows.Forms.Padding(2);
            this.grpSelectionMethod.Name = "grpSelectionMethod";
            this.grpSelectionMethod.Padding = new System.Windows.Forms.Padding(2);
            this.grpSelectionMethod.Size = new System.Drawing.Size(131, 81);
            this.grpSelectionMethod.TabIndex = 18;
            this.grpSelectionMethod.TabStop = false;
            this.grpSelectionMethod.Text = "Selection Method";
            this.toolTip1.SetToolTip(this.grpSelectionMethod, "Items may be selected by ARTCC boundary, a square, or a circle from the Center Ai" +
        "rport");
            // 
            // btnCircle
            // 
            this.btnCircle.AutoSize = true;
            this.btnCircle.Enabled = false;
            this.btnCircle.Location = new System.Drawing.Point(5, 59);
            this.btnCircle.Margin = new System.Windows.Forms.Padding(2);
            this.btnCircle.Name = "btnCircle";
            this.btnCircle.Size = new System.Drawing.Size(120, 17);
            this.btnCircle.TabIndex = 20;
            this.btnCircle.TabStop = true;
            this.btnCircle.Text = "Circle around center";
            this.btnCircle.UseVisualStyleBackColor = true;
            this.btnCircle.CheckedChanged += new System.EventHandler(this.BtnCircle_CheckedChanged);
            // 
            // btnSquare
            // 
            this.btnSquare.AutoSize = true;
            this.btnSquare.Location = new System.Drawing.Point(5, 37);
            this.btnSquare.Margin = new System.Windows.Forms.Padding(2);
            this.btnSquare.Name = "btnSquare";
            this.btnSquare.Size = new System.Drawing.Size(59, 17);
            this.btnSquare.TabIndex = 19;
            this.btnSquare.TabStop = true;
            this.btnSquare.Text = "Square";
            this.btnSquare.UseVisualStyleBackColor = true;
            this.btnSquare.CheckedChanged += new System.EventHandler(this.BtnSquare_CheckedChanged);
            // 
            // btnARTCC
            // 
            this.btnARTCC.AutoSize = true;
            this.btnARTCC.Checked = true;
            this.btnARTCC.Location = new System.Drawing.Point(5, 18);
            this.btnARTCC.Margin = new System.Windows.Forms.Padding(2);
            this.btnARTCC.Name = "btnARTCC";
            this.btnARTCC.Size = new System.Drawing.Size(108, 17);
            this.btnARTCC.TabIndex = 0;
            this.btnARTCC.TabStop = true;
            this.btnARTCC.Text = "ARTCC boundary";
            this.btnARTCC.UseVisualStyleBackColor = true;
            this.btnARTCC.CheckedChanged += new System.EventHandler(this.BtnARTCC_CheckedChanged);
            // 
            // cmdWriteSCT
            // 
            this.cmdWriteSCT.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.cmdWriteSCT.Enabled = false;
            this.cmdWriteSCT.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdWriteSCT.Location = new System.Drawing.Point(358, 14);
            this.cmdWriteSCT.Margin = new System.Windows.Forms.Padding(2);
            this.cmdWriteSCT.Name = "cmdWriteSCT";
            this.cmdWriteSCT.Size = new System.Drawing.Size(92, 49);
            this.cmdWriteSCT.TabIndex = 27;
            this.cmdWriteSCT.Text = "WRITE the Sector File";
            this.toolTip1.SetToolTip(this.cmdWriteSCT, "Release the Kraken!");
            this.cmdWriteSCT.UseVisualStyleBackColor = false;
            this.cmdWriteSCT.Click += new System.EventHandler(this.CmdWriteSCT_Click);
            // 
            // txtLatSouth
            // 
            this.txtLatSouth.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLatSouth.Location = new System.Drawing.Point(43, 69);
            this.txtLatSouth.Margin = new System.Windows.Forms.Padding(2);
            this.txtLatSouth.Name = "txtLatSouth";
            this.txtLatSouth.Size = new System.Drawing.Size(68, 21);
            this.txtLatSouth.TabIndex = 26;
            this.toolTip1.SetToolTip(this.txtLatSouth, "Southern Latitude of square");
            // 
            // txtLongEast
            // 
            this.txtLongEast.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLongEast.Location = new System.Drawing.Point(43, 93);
            this.txtLongEast.Margin = new System.Windows.Forms.Padding(2);
            this.txtLongEast.Name = "txtLongEast";
            this.txtLongEast.Size = new System.Drawing.Size(68, 21);
            this.txtLongEast.TabIndex = 24;
            this.toolTip1.SetToolTip(this.txtLongEast, "Eastern Longitude of square");
            // 
            // txtLongWest
            // 
            this.txtLongWest.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLongWest.Location = new System.Drawing.Point(43, 45);
            this.txtLongWest.Margin = new System.Windows.Forms.Padding(2);
            this.txtLongWest.Name = "txtLongWest";
            this.txtLongWest.Size = new System.Drawing.Size(68, 21);
            this.txtLongWest.TabIndex = 22;
            this.toolTip1.SetToolTip(this.txtLongWest, "Western Longitude of square");
            // 
            // txtLatNorth
            // 
            this.txtLatNorth.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLatNorth.Location = new System.Drawing.Point(43, 21);
            this.txtLatNorth.Margin = new System.Windows.Forms.Padding(2);
            this.txtLatNorth.Name = "txtLatNorth";
            this.txtLatNorth.Size = new System.Drawing.Size(68, 21);
            this.txtLatNorth.TabIndex = 20;
            this.toolTip1.SetToolTip(this.txtLatNorth, "Northern Latitude of square");
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(14, 17);
            this.numericUpDown1.Margin = new System.Windows.Forms.Padding(2);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(90, 20);
            this.numericUpDown1.TabIndex = 0;
            this.toolTip1.SetToolTip(this.numericUpDown1, "FUTURE CAPABILITY");
            // 
            // cmdInstructions
            // 
            this.cmdInstructions.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.cmdInstructions.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdInstructions.Location = new System.Drawing.Point(459, 14);
            this.cmdInstructions.Margin = new System.Windows.Forms.Padding(2);
            this.cmdInstructions.Name = "cmdInstructions";
            this.cmdInstructions.Size = new System.Drawing.Size(81, 49);
            this.cmdInstructions.TabIndex = 26;
            this.cmdInstructions.Text = "Click for Instructions";
            this.toolTip1.SetToolTip(this.cmdInstructions, "Open the instructions page");
            this.cmdInstructions.UseVisualStyleBackColor = false;
            this.cmdInstructions.Click += new System.EventHandler(this.CmdInstructions_Click);
            // 
            // chkbxShowAll
            // 
            this.chkbxShowAll.AutoSize = true;
            this.chkbxShowAll.BackColor = System.Drawing.Color.Transparent;
            this.chkbxShowAll.Location = new System.Drawing.Point(653, 461);
            this.chkbxShowAll.Margin = new System.Windows.Forms.Padding(2);
            this.chkbxShowAll.Name = "chkbxShowAll";
            this.chkbxShowAll.Size = new System.Drawing.Size(166, 17);
            this.chkbxShowAll.TabIndex = 28;
            this.chkbxShowAll.Text = "Ignore filter in gridview display";
            this.toolTip1.SetToolTip(this.chkbxShowAll, "Check to see all items in table");
            this.chkbxShowAll.UseVisualStyleBackColor = false;
            this.chkbxShowAll.CheckedChanged += new System.EventHandler(this.ChkbxShowAll_CheckedChanged);
            // 
            // txtGridViewCount
            // 
            this.txtGridViewCount.Location = new System.Drawing.Point(1020, 458);
            this.txtGridViewCount.Margin = new System.Windows.Forms.Padding(2);
            this.txtGridViewCount.Name = "txtGridViewCount";
            this.txtGridViewCount.Size = new System.Drawing.Size(91, 20);
            this.txtGridViewCount.TabIndex = 29;
            this.toolTip1.SetToolTip(this.txtGridViewCount, "Number displayed / Total in table");
            // 
            // lblUpdating
            // 
            this.lblUpdating.AutoSize = true;
            this.lblUpdating.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.lblUpdating.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUpdating.Location = new System.Drawing.Point(739, 90);
            this.lblUpdating.Name = "lblUpdating";
            this.lblUpdating.Size = new System.Drawing.Size(209, 24);
            this.lblUpdating.TabIndex = 1;
            this.lblUpdating.Text = "Updating.  Please wait...";
            this.lblUpdating.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.toolTip1.SetToolTip(this.lblUpdating, "The FAA data file is HUGE and can take up to a minute to read.");
            this.lblUpdating.Visible = false;
            // 
            // cmdUpdateGrid
            // 
            this.cmdUpdateGrid.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdUpdateGrid.Location = new System.Drawing.Point(653, 64);
            this.cmdUpdateGrid.Margin = new System.Windows.Forms.Padding(2);
            this.cmdUpdateGrid.Name = "cmdUpdateGrid";
            this.cmdUpdateGrid.Size = new System.Drawing.Size(81, 52);
            this.cmdUpdateGrid.TabIndex = 31;
            this.cmdUpdateGrid.Text = "Preview selections";
            this.toolTip1.SetToolTip(this.cmdUpdateGrid, "Select individual items by updating the grid view");
            this.cmdUpdateGrid.UseVisualStyleBackColor = true;
            this.cmdUpdateGrid.Click += new System.EventHandler(this.CmdUpdateGrid_Click);
            // 
            // lblFilesWarning
            // 
            this.lblFilesWarning.AutoSize = true;
            this.lblFilesWarning.BackColor = System.Drawing.Color.Yellow;
            this.lblFilesWarning.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFilesWarning.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.lblFilesWarning.Location = new System.Drawing.Point(410, 151);
            this.lblFilesWarning.Name = "lblFilesWarning";
            this.lblFilesWarning.Size = new System.Drawing.Size(192, 30);
            this.lblFilesWarning.TabIndex = 43;
            this.lblFilesWarning.Text = "All checked items will be written to\r\na single SCT2 file.\r\n";
            this.lblFilesWarning.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.toolTip1.SetToolTip(this.lblFilesWarning, "Check \'Write entire SCT file\' to generate sct2 file");
            this.lblFilesWarning.Visible = false;
            // 
            // chkSSDName
            // 
            this.chkSSDName.AutoSize = true;
            this.chkSSDName.Checked = true;
            this.chkSSDName.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSSDName.Location = new System.Drawing.Point(85, 81);
            this.chkSSDName.Margin = new System.Windows.Forms.Padding(2);
            this.chkSSDName.Name = "chkSSDName";
            this.chkSSDName.Size = new System.Drawing.Size(90, 17);
            this.chkSSDName.TabIndex = 44;
            this.chkSSDName.Text = "Use full name";
            this.toolTip1.SetToolTip(this.chkSSDName, "If checked, SCT diagrams will use full name for SIDs/STARs. If UNchecked, will us" +
        "e the abbreviation.");
            this.chkSSDName.UseVisualStyleBackColor = true;
            // 
            // cboARTCC_label
            // 
            this.cboARTCC_label.AutoSize = true;
            this.cboARTCC_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboARTCC_label.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cboARTCC_label.Location = new System.Drawing.Point(13, 288);
            this.cboARTCC_label.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.cboARTCC_label.Name = "cboARTCC_label";
            this.cboARTCC_label.Size = new System.Drawing.Size(131, 20);
            this.cboARTCC_label.TabIndex = 9;
            this.cboARTCC_label.Text = "Primary ARTCC:  ";
            this.cboARTCC_label.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboAirport_label
            // 
            this.cboAirport_label.AutoSize = true;
            this.cboAirport_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboAirport_label.Location = new System.Drawing.Point(12, 314);
            this.cboAirport_label.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.cboAirport_label.Name = "cboAirport_label";
            this.cboAirport_label.Size = new System.Drawing.Size(136, 20);
            this.cboAirport_label.TabIndex = 11;
            this.cboAirport_label.Text = "Center Airport:      ";
            this.cboAirport_label.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelInfoSection
            // 
            this.labelInfoSection.AutoSize = true;
            this.labelInfoSection.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.labelInfoSection.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelInfoSection.Location = new System.Drawing.Point(14, 214);
            this.labelInfoSection.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelInfoSection.Name = "labelInfoSection";
            this.labelInfoSection.Size = new System.Drawing.Size(338, 20);
            this.labelInfoSection.TabIndex = 13;
            this.labelInfoSection.Text = "[INFO] section                                                         ";
            // 
            // txtAsstFacilityEngineer_label
            // 
            this.txtAsstFacilityEngineer_label.AutoSize = true;
            this.txtAsstFacilityEngineer_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAsstFacilityEngineer_label.Location = new System.Drawing.Point(12, 261);
            this.txtAsstFacilityEngineer_label.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.txtAsstFacilityEngineer_label.Name = "txtAsstFacilityEngineer_label";
            this.txtAsstFacilityEngineer_label.Size = new System.Drawing.Size(140, 20);
            this.txtAsstFacilityEngineer_label.TabIndex = 16;
            this.txtAsstFacilityEngineer_label.Text = "Assistant FE:         ";
            this.txtAsstFacilityEngineer_label.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtFacilityEngineer_label
            // 
            this.txtFacilityEngineer_label.AutoSize = true;
            this.txtFacilityEngineer_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFacilityEngineer_label.Location = new System.Drawing.Point(12, 237);
            this.txtFacilityEngineer_label.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.txtFacilityEngineer_label.Name = "txtFacilityEngineer_label";
            this.txtFacilityEngineer_label.Size = new System.Drawing.Size(137, 20);
            this.txtFacilityEngineer_label.TabIndex = 14;
            this.txtFacilityEngineer_label.Text = "Facility Engineer:  ";
            this.txtFacilityEngineer_label.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // grpSquareLimits
            // 
            this.grpSquareLimits.Controls.Add(this.label5);
            this.grpSquareLimits.Controls.Add(this.label4);
            this.grpSquareLimits.Controls.Add(this.label3);
            this.grpSquareLimits.Controls.Add(this.label2);
            this.grpSquareLimits.Controls.Add(this.nudEast);
            this.grpSquareLimits.Controls.Add(this.nudSouth);
            this.grpSquareLimits.Controls.Add(this.nudWest);
            this.grpSquareLimits.Controls.Add(this.nudNorth);
            this.grpSquareLimits.Controls.Add(this.lblMargins);
            this.grpSquareLimits.Controls.Add(this.txtLatSouth_label);
            this.grpSquareLimits.Controls.Add(this.txtLatSouth);
            this.grpSquareLimits.Controls.Add(this.txtLatEast_label);
            this.grpSquareLimits.Controls.Add(this.txtLongEast);
            this.grpSquareLimits.Controls.Add(this.txtLatWest_label);
            this.grpSquareLimits.Controls.Add(this.txtLongWest);
            this.grpSquareLimits.Controls.Add(this.txtLatNorth_label);
            this.grpSquareLimits.Controls.Add(this.txtLatNorth);
            this.grpSquareLimits.Location = new System.Drawing.Point(166, 351);
            this.grpSquareLimits.Margin = new System.Windows.Forms.Padding(2);
            this.grpSquareLimits.Name = "grpSquareLimits";
            this.grpSquareLimits.Padding = new System.Windows.Forms.Padding(2);
            this.grpSquareLimits.Size = new System.Drawing.Size(196, 127);
            this.grpSquareLimits.TabIndex = 19;
            this.grpSquareLimits.TabStop = false;
            this.grpSquareLimits.Text = "Square Limits";
            this.grpSquareLimits.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(117, 95);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(13, 13);
            this.label5.TabIndex = 36;
            this.label5.Text = "+";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(117, 74);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(10, 13);
            this.label4.TabIndex = 35;
            this.label4.Text = "-";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(117, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(10, 13);
            this.label3.TabIndex = 34;
            this.label3.Text = "-";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(117, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(13, 13);
            this.label2.TabIndex = 33;
            this.label2.Text = "+";
            // 
            // nudEast
            // 
            this.nudEast.Location = new System.Drawing.Point(134, 95);
            this.nudEast.Margin = new System.Windows.Forms.Padding(2);
            this.nudEast.Name = "nudEast";
            this.nudEast.Size = new System.Drawing.Size(44, 20);
            this.nudEast.TabIndex = 32;
            this.nudEast.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // nudSouth
            // 
            this.nudSouth.Location = new System.Drawing.Point(134, 72);
            this.nudSouth.Margin = new System.Windows.Forms.Padding(2);
            this.nudSouth.Name = "nudSouth";
            this.nudSouth.Size = new System.Drawing.Size(44, 20);
            this.nudSouth.TabIndex = 31;
            this.nudSouth.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // nudWest
            // 
            this.nudWest.Location = new System.Drawing.Point(134, 47);
            this.nudWest.Margin = new System.Windows.Forms.Padding(2);
            this.nudWest.Name = "nudWest";
            this.nudWest.Size = new System.Drawing.Size(44, 20);
            this.nudWest.TabIndex = 30;
            this.nudWest.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // nudNorth
            // 
            this.nudNorth.Location = new System.Drawing.Point(134, 23);
            this.nudNorth.Margin = new System.Windows.Forms.Padding(2);
            this.nudNorth.Name = "nudNorth";
            this.nudNorth.Size = new System.Drawing.Size(44, 20);
            this.nudNorth.TabIndex = 29;
            this.nudNorth.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // lblMargins
            // 
            this.lblMargins.AutoSize = true;
            this.lblMargins.Location = new System.Drawing.Point(118, 1);
            this.lblMargins.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblMargins.Name = "lblMargins";
            this.lblMargins.Size = new System.Drawing.Size(70, 13);
            this.lblMargins.TabIndex = 28;
            this.lblMargins.Text = "Margins (NM)";
            // 
            // txtLatSouth_label
            // 
            this.txtLatSouth_label.AutoSize = true;
            this.txtLatSouth_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLatSouth_label.Location = new System.Drawing.Point(4, 72);
            this.txtLatSouth_label.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.txtLatSouth_label.Name = "txtLatSouth_label";
            this.txtLatSouth_label.Size = new System.Drawing.Size(39, 15);
            this.txtLatSouth_label.TabIndex = 27;
            this.txtLatSouth_label.Text = "South";
            // 
            // txtLatEast_label
            // 
            this.txtLatEast_label.AutoSize = true;
            this.txtLatEast_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLatEast_label.Location = new System.Drawing.Point(11, 96);
            this.txtLatEast_label.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.txtLatEast_label.Name = "txtLatEast_label";
            this.txtLatEast_label.Size = new System.Drawing.Size(31, 15);
            this.txtLatEast_label.TabIndex = 25;
            this.txtLatEast_label.Text = "East";
            // 
            // txtLatWest_label
            // 
            this.txtLatWest_label.AutoSize = true;
            this.txtLatWest_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLatWest_label.Location = new System.Drawing.Point(8, 47);
            this.txtLatWest_label.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.txtLatWest_label.Name = "txtLatWest_label";
            this.txtLatWest_label.Size = new System.Drawing.Size(34, 15);
            this.txtLatWest_label.TabIndex = 23;
            this.txtLatWest_label.Text = "West";
            // 
            // txtLatNorth_label
            // 
            this.txtLatNorth_label.AutoSize = true;
            this.txtLatNorth_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLatNorth_label.Location = new System.Drawing.Point(8, 24);
            this.txtLatNorth_label.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.txtLatNorth_label.Name = "txtLatNorth_label";
            this.txtLatNorth_label.Size = new System.Drawing.Size(37, 15);
            this.txtLatNorth_label.TabIndex = 21;
            this.txtLatNorth_label.Text = "North";
            // 
            // grpCircle
            // 
            this.grpCircle.Controls.Add(this.numericUpDown1);
            this.grpCircle.Location = new System.Drawing.Point(31, 436);
            this.grpCircle.Margin = new System.Windows.Forms.Padding(2);
            this.grpCircle.Name = "grpCircle";
            this.grpCircle.Padding = new System.Windows.Forms.Padding(2);
            this.grpCircle.Size = new System.Drawing.Size(123, 40);
            this.grpCircle.TabIndex = 20;
            this.grpCircle.TabStop = false;
            this.grpCircle.Text = "Radius of Circle (NM)";
            this.grpCircle.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(227)))), ((int)(((byte)(246)))));
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(25, 131);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 18);
            this.label1.TabIndex = 21;
            this.label1.Text = "File Folders";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Controls.Add(this.tabPage6);
            this.tabControl1.Controls.Add(this.tabPage7);
            this.tabControl1.Controls.Add(this.tabPage8);
            this.tabControl1.Controls.Add(this.tabPage9);
            this.tabControl1.Controls.Add(this.tabPage10);
            this.tabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(649, 116);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(466, 338);
            this.tabControl1.TabIndex = 22;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dgvAPT);
            this.tabPage1.Location = new System.Drawing.Point(4, 26);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage1.Size = new System.Drawing.Size(458, 308);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "APTs";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dgvAPT
            // 
            this.dgvAPT.AllowUserToAddRows = false;
            this.dgvAPT.AllowUserToDeleteRows = false;
            this.dgvAPT.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAPT.Location = new System.Drawing.Point(2, 2);
            this.dgvAPT.Margin = new System.Windows.Forms.Padding(2);
            this.dgvAPT.Name = "dgvAPT";
            this.dgvAPT.RowHeadersVisible = false;
            this.dgvAPT.RowHeadersWidth = 51;
            this.dgvAPT.RowTemplate.Height = 24;
            this.dgvAPT.Size = new System.Drawing.Size(456, 302);
            this.dgvAPT.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dgvVOR);
            this.tabPage2.Location = new System.Drawing.Point(4, 26);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage2.Size = new System.Drawing.Size(458, 308);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "VORs";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dgvVOR
            // 
            this.dgvVOR.AllowUserToAddRows = false;
            this.dgvVOR.AllowUserToDeleteRows = false;
            this.dgvVOR.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvVOR.Location = new System.Drawing.Point(2, 2);
            this.dgvVOR.Margin = new System.Windows.Forms.Padding(2);
            this.dgvVOR.Name = "dgvVOR";
            this.dgvVOR.RowHeadersVisible = false;
            this.dgvVOR.RowHeadersWidth = 51;
            this.dgvVOR.RowTemplate.Height = 24;
            this.dgvVOR.Size = new System.Drawing.Size(456, 302);
            this.dgvVOR.TabIndex = 1;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.dgvNDB);
            this.tabPage3.Location = new System.Drawing.Point(4, 26);
            this.tabPage3.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage3.Size = new System.Drawing.Size(458, 308);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "NDBs";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // dgvNDB
            // 
            this.dgvNDB.AllowUserToAddRows = false;
            this.dgvNDB.AllowUserToDeleteRows = false;
            this.dgvNDB.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvNDB.Location = new System.Drawing.Point(2, 2);
            this.dgvNDB.Margin = new System.Windows.Forms.Padding(2);
            this.dgvNDB.Name = "dgvNDB";
            this.dgvNDB.RowHeadersVisible = false;
            this.dgvNDB.RowHeadersWidth = 51;
            this.dgvNDB.RowTemplate.Height = 24;
            this.dgvNDB.Size = new System.Drawing.Size(456, 302);
            this.dgvNDB.TabIndex = 2;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.dgvFIX);
            this.tabPage4.Location = new System.Drawing.Point(4, 26);
            this.tabPage4.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage4.Size = new System.Drawing.Size(458, 308);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "FIXes";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // dgvFIX
            // 
            this.dgvFIX.AllowUserToAddRows = false;
            this.dgvFIX.AllowUserToDeleteRows = false;
            this.dgvFIX.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFIX.Location = new System.Drawing.Point(2, 2);
            this.dgvFIX.Margin = new System.Windows.Forms.Padding(2);
            this.dgvFIX.Name = "dgvFIX";
            this.dgvFIX.RowHeadersVisible = false;
            this.dgvFIX.RowHeadersWidth = 51;
            this.dgvFIX.RowTemplate.Height = 24;
            this.dgvFIX.Size = new System.Drawing.Size(456, 302);
            this.dgvFIX.TabIndex = 2;
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.dgvRWY);
            this.tabPage5.Location = new System.Drawing.Point(4, 26);
            this.tabPage5.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage5.Size = new System.Drawing.Size(458, 308);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "RWYs";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // dgvRWY
            // 
            this.dgvRWY.AllowUserToAddRows = false;
            this.dgvRWY.AllowUserToDeleteRows = false;
            this.dgvRWY.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRWY.Location = new System.Drawing.Point(2, 2);
            this.dgvRWY.Margin = new System.Windows.Forms.Padding(2);
            this.dgvRWY.Name = "dgvRWY";
            this.dgvRWY.RowHeadersVisible = false;
            this.dgvRWY.RowHeadersWidth = 51;
            this.dgvRWY.RowTemplate.Height = 24;
            this.dgvRWY.Size = new System.Drawing.Size(456, 302);
            this.dgvRWY.TabIndex = 2;
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.dgvAWY);
            this.tabPage6.Location = new System.Drawing.Point(4, 26);
            this.tabPage6.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage6.Size = new System.Drawing.Size(458, 308);
            this.tabPage6.TabIndex = 5;
            this.tabPage6.Text = "AWYs";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // dgvAWY
            // 
            this.dgvAWY.AllowUserToAddRows = false;
            this.dgvAWY.AllowUserToDeleteRows = false;
            this.dgvAWY.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAWY.Location = new System.Drawing.Point(2, 2);
            this.dgvAWY.Margin = new System.Windows.Forms.Padding(2);
            this.dgvAWY.Name = "dgvAWY";
            this.dgvAWY.RowHeadersVisible = false;
            this.dgvAWY.RowHeadersWidth = 51;
            this.dgvAWY.RowTemplate.Height = 24;
            this.dgvAWY.Size = new System.Drawing.Size(456, 302);
            this.dgvAWY.TabIndex = 2;
            // 
            // tabPage7
            // 
            this.tabPage7.Controls.Add(this.dgvSID);
            this.tabPage7.Location = new System.Drawing.Point(4, 26);
            this.tabPage7.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage7.Name = "tabPage7";
            this.tabPage7.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage7.Size = new System.Drawing.Size(458, 308);
            this.tabPage7.TabIndex = 6;
            this.tabPage7.Text = "SIDs";
            this.tabPage7.UseVisualStyleBackColor = true;
            // 
            // dgvSID
            // 
            this.dgvSID.AllowUserToAddRows = false;
            this.dgvSID.AllowUserToDeleteRows = false;
            this.dgvSID.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSID.Location = new System.Drawing.Point(2, 2);
            this.dgvSID.Margin = new System.Windows.Forms.Padding(2);
            this.dgvSID.Name = "dgvSID";
            this.dgvSID.RowHeadersVisible = false;
            this.dgvSID.RowHeadersWidth = 51;
            this.dgvSID.RowTemplate.Height = 24;
            this.dgvSID.Size = new System.Drawing.Size(456, 302);
            this.dgvSID.TabIndex = 2;
            // 
            // tabPage8
            // 
            this.tabPage8.Controls.Add(this.dgvSTAR);
            this.tabPage8.Location = new System.Drawing.Point(4, 26);
            this.tabPage8.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage8.Name = "tabPage8";
            this.tabPage8.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage8.Size = new System.Drawing.Size(458, 308);
            this.tabPage8.TabIndex = 7;
            this.tabPage8.Text = "STARs";
            this.tabPage8.UseVisualStyleBackColor = true;
            // 
            // dgvSTAR
            // 
            this.dgvSTAR.AllowUserToAddRows = false;
            this.dgvSTAR.AllowUserToDeleteRows = false;
            this.dgvSTAR.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSTAR.Location = new System.Drawing.Point(2, 2);
            this.dgvSTAR.Margin = new System.Windows.Forms.Padding(2);
            this.dgvSTAR.Name = "dgvSTAR";
            this.dgvSTAR.RowHeadersVisible = false;
            this.dgvSTAR.RowHeadersWidth = 51;
            this.dgvSTAR.RowTemplate.Height = 24;
            this.dgvSTAR.Size = new System.Drawing.Size(456, 302);
            this.dgvSTAR.TabIndex = 2;
            // 
            // tabPage9
            // 
            this.tabPage9.Controls.Add(this.dgvARB);
            this.tabPage9.Location = new System.Drawing.Point(4, 26);
            this.tabPage9.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage9.Name = "tabPage9";
            this.tabPage9.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage9.Size = new System.Drawing.Size(458, 308);
            this.tabPage9.TabIndex = 8;
            this.tabPage9.Text = "ARBs";
            this.tabPage9.UseVisualStyleBackColor = true;
            // 
            // dgvARB
            // 
            this.dgvARB.AllowUserToAddRows = false;
            this.dgvARB.AllowUserToDeleteRows = false;
            this.dgvARB.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvARB.Location = new System.Drawing.Point(2, 2);
            this.dgvARB.Margin = new System.Windows.Forms.Padding(2);
            this.dgvARB.Name = "dgvARB";
            this.dgvARB.RowHeadersVisible = false;
            this.dgvARB.RowHeadersWidth = 51;
            this.dgvARB.RowTemplate.Height = 24;
            this.dgvARB.Size = new System.Drawing.Size(456, 302);
            this.dgvARB.TabIndex = 3;
            // 
            // tabPage10
            // 
            this.tabPage10.Controls.Add(this.dgvSUA);
            this.tabPage10.Location = new System.Drawing.Point(4, 26);
            this.tabPage10.Name = "tabPage10";
            this.tabPage10.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage10.Size = new System.Drawing.Size(458, 308);
            this.tabPage10.TabIndex = 9;
            this.tabPage10.Text = "SUAs";
            this.tabPage10.UseVisualStyleBackColor = true;
            // 
            // dgvSUA
            // 
            this.dgvSUA.AllowUserToAddRows = false;
            this.dgvSUA.AllowUserToDeleteRows = false;
            this.dgvSUA.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSUA.Location = new System.Drawing.Point(1, 3);
            this.dgvSUA.Margin = new System.Windows.Forms.Padding(2);
            this.dgvSUA.Name = "dgvSUA";
            this.dgvSUA.RowHeadersVisible = false;
            this.dgvSUA.RowHeadersWidth = 51;
            this.dgvSUA.RowTemplate.Height = 24;
            this.dgvSUA.Size = new System.Drawing.Size(456, 302);
            this.dgvSUA.TabIndex = 4;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox2.BackgroundImage")));
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox2.Location = new System.Drawing.Point(4, 20);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(136, 59);
            this.pictureBox2.TabIndex = 24;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Click += new System.EventHandler(this.PictureBox2_Click);
            // 
            // lblLogo
            // 
            this.lblLogo.AutoSize = true;
            this.lblLogo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLogo.Location = new System.Drawing.Point(4, 5);
            this.lblLogo.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblLogo.Name = "lblLogo";
            this.lblLogo.Size = new System.Drawing.Size(107, 17);
            this.lblLogo.TabIndex = 25;
            this.lblLogo.Text = "Developed by";
            // 
            // cmdExit
            // 
            this.cmdExit.BackColor = System.Drawing.Color.Red;
            this.cmdExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdExit.Location = new System.Drawing.Point(1052, 14);
            this.cmdExit.Margin = new System.Windows.Forms.Padding(2);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Size = new System.Drawing.Size(76, 49);
            this.cmdExit.TabIndex = 30;
            this.cmdExit.Text = "EXIT";
            this.cmdExit.UseVisualStyleBackColor = false;
            this.cmdExit.Click += new System.EventHandler(this.CmdExit_Click);
            // 
            // cmdLocalSectors
            // 
            this.cmdLocalSectors.Location = new System.Drawing.Point(358, 67);
            this.cmdLocalSectors.Margin = new System.Windows.Forms.Padding(2);
            this.cmdLocalSectors.Name = "cmdLocalSectors";
            this.cmdLocalSectors.Size = new System.Drawing.Size(93, 49);
            this.cmdLocalSectors.TabIndex = 33;
            this.cmdLocalSectors.Text = "Add Local Sectors";
            this.cmdLocalSectors.UseVisualStyleBackColor = true;
            this.cmdLocalSectors.Click += new System.EventHandler(this.LocalSectors_Click);
            // 
            // chkAPTs
            // 
            this.chkAPTs.AutoSize = true;
            this.chkAPTs.Checked = true;
            this.chkAPTs.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAPTs.Location = new System.Drawing.Point(2, 21);
            this.chkAPTs.Margin = new System.Windows.Forms.Padding(2);
            this.chkAPTs.Name = "chkAPTs";
            this.chkAPTs.Size = new System.Drawing.Size(61, 17);
            this.chkAPTs.TabIndex = 34;
            this.chkAPTs.Text = "Airports";
            this.chkAPTs.UseVisualStyleBackColor = true;
            // 
            // chkVORs
            // 
            this.chkVORs.AutoSize = true;
            this.chkVORs.Checked = true;
            this.chkVORs.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkVORs.Location = new System.Drawing.Point(2, 42);
            this.chkVORs.Margin = new System.Windows.Forms.Padding(2);
            this.chkVORs.Name = "chkVORs";
            this.chkVORs.Size = new System.Drawing.Size(54, 17);
            this.chkVORs.TabIndex = 35;
            this.chkVORs.Text = "VORs";
            this.chkVORs.UseVisualStyleBackColor = true;
            // 
            // chkNDBs
            // 
            this.chkNDBs.AutoSize = true;
            this.chkNDBs.Checked = true;
            this.chkNDBs.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkNDBs.Location = new System.Drawing.Point(2, 61);
            this.chkNDBs.Margin = new System.Windows.Forms.Padding(2);
            this.chkNDBs.Name = "chkNDBs";
            this.chkNDBs.Size = new System.Drawing.Size(54, 17);
            this.chkNDBs.TabIndex = 36;
            this.chkNDBs.Text = "NDBs";
            this.chkNDBs.UseVisualStyleBackColor = true;
            // 
            // chkRWYs
            // 
            this.chkRWYs.AutoSize = true;
            this.chkRWYs.Checked = true;
            this.chkRWYs.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkRWYs.Location = new System.Drawing.Point(67, 21);
            this.chkRWYs.Margin = new System.Windows.Forms.Padding(2);
            this.chkRWYs.Name = "chkRWYs";
            this.chkRWYs.Size = new System.Drawing.Size(70, 17);
            this.chkRWYs.TabIndex = 37;
            this.chkRWYs.Text = "Runways";
            this.chkRWYs.UseVisualStyleBackColor = true;
            // 
            // chkFIXes
            // 
            this.chkFIXes.AutoSize = true;
            this.chkFIXes.Checked = true;
            this.chkFIXes.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkFIXes.Location = new System.Drawing.Point(2, 81);
            this.chkFIXes.Margin = new System.Windows.Forms.Padding(2);
            this.chkFIXes.Name = "chkFIXes";
            this.chkFIXes.Size = new System.Drawing.Size(50, 17);
            this.chkFIXes.TabIndex = 38;
            this.chkFIXes.Text = "Fixes";
            this.chkFIXes.UseVisualStyleBackColor = true;
            // 
            // chkAWYs
            // 
            this.chkAWYs.AutoSize = true;
            this.chkAWYs.Checked = true;
            this.chkAWYs.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAWYs.Location = new System.Drawing.Point(67, 42);
            this.chkAWYs.Margin = new System.Windows.Forms.Padding(2);
            this.chkAWYs.Name = "chkAWYs";
            this.chkAWYs.Size = new System.Drawing.Size(62, 17);
            this.chkAWYs.TabIndex = 39;
            this.chkAWYs.Text = "Airways";
            this.chkAWYs.UseVisualStyleBackColor = true;
            // 
            // chkSSDs
            // 
            this.chkSSDs.AutoSize = true;
            this.chkSSDs.Checked = true;
            this.chkSSDs.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSSDs.Location = new System.Drawing.Point(67, 63);
            this.chkSSDs.Margin = new System.Windows.Forms.Padding(2);
            this.chkSSDs.Name = "chkSSDs";
            this.chkSSDs.Size = new System.Drawing.Size(88, 17);
            this.chkSSDs.TabIndex = 40;
            this.chkSSDs.Text = "SIDs/STARs";
            this.chkSSDs.UseVisualStyleBackColor = true;
            this.chkSSDs.CheckedChanged += new System.EventHandler(this.ChkSSDs_CheckedChanged);
            // 
            // chkALL
            // 
            this.chkALL.AutoSize = true;
            this.chkALL.Checked = true;
            this.chkALL.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkALL.Location = new System.Drawing.Point(2, 2);
            this.chkALL.Margin = new System.Windows.Forms.Padding(2);
            this.chkALL.Name = "chkALL";
            this.chkALL.Size = new System.Drawing.Size(126, 17);
            this.chkALL.TabIndex = 41;
            this.chkALL.Text = "Write entire SCT2 file";
            this.chkALL.UseVisualStyleBackColor = true;
            this.chkALL.CheckedChanged += new System.EventHandler(this.ChkALL_CheckedChanged);
            // 
            // chkARBs
            // 
            this.chkARBs.AutoSize = true;
            this.chkARBs.Checked = true;
            this.chkARBs.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkARBs.Location = new System.Drawing.Point(2, 101);
            this.chkARBs.Margin = new System.Windows.Forms.Padding(2);
            this.chkARBs.Name = "chkARBs";
            this.chkARBs.Size = new System.Drawing.Size(67, 17);
            this.chkARBs.TabIndex = 42;
            this.chkARBs.Text = "ARTCCs";
            this.chkARBs.UseVisualStyleBackColor = true;
            // 
            // panelSCTsections
            // 
            this.panelSCTsections.Controls.Add(this.ChkSUA);
            this.panelSCTsections.Controls.Add(this.chkALL);
            this.panelSCTsections.Controls.Add(this.chkSSDName);
            this.panelSCTsections.Controls.Add(this.chkARBs);
            this.panelSCTsections.Controls.Add(this.chkAPTs);
            this.panelSCTsections.Controls.Add(this.chkSSDs);
            this.panelSCTsections.Controls.Add(this.chkRWYs);
            this.panelSCTsections.Controls.Add(this.chkAWYs);
            this.panelSCTsections.Controls.Add(this.chkVORs);
            this.panelSCTsections.Controls.Add(this.chkFIXes);
            this.panelSCTsections.Controls.Add(this.chkNDBs);
            this.panelSCTsections.Location = new System.Drawing.Point(408, 182);
            this.panelSCTsections.Name = "panelSCTsections";
            this.panelSCTsections.Size = new System.Drawing.Size(200, 126);
            this.panelSCTsections.TabIndex = 45;
            // 
            // ChkSUA
            // 
            this.ChkSUA.AutoSize = true;
            this.ChkSUA.Location = new System.Drawing.Point(66, 101);
            this.ChkSUA.Name = "ChkSUA";
            this.ChkSUA.Size = new System.Drawing.Size(127, 17);
            this.ChkSUA.TabIndex = 45;
            this.ChkSUA.Text = "Special Use Airspace";
            this.ChkSUA.UseVisualStyleBackColor = true;
            this.ChkSUA.CheckedChanged += new System.EventHandler(this.ChkSUA_CheckedChanged);
            // 
            // cmdAddSUAs
            // 
            this.cmdAddSUAs.Location = new System.Drawing.Point(459, 67);
            this.cmdAddSUAs.Margin = new System.Windows.Forms.Padding(2);
            this.cmdAddSUAs.Name = "cmdAddSUAs";
            this.cmdAddSUAs.Size = new System.Drawing.Size(93, 49);
            this.cmdAddSUAs.TabIndex = 46;
            this.cmdAddSUAs.Text = "Add SUAs (test)";
            this.cmdAddSUAs.UseVisualStyleBackColor = true;
            this.cmdAddSUAs.Click += new System.EventHandler(this.CmdAddSUAs_Click);
            // 
            // panelSUAs
            // 
            this.panelSUAs.Controls.Add(this.chkSUA_Danger);
            this.panelSUAs.Controls.Add(this.chkSUA_Prohibited);
            this.panelSUAs.Controls.Add(this.chkSUA_Restricted);
            this.panelSUAs.Controls.Add(this.chkSUA_ClassD);
            this.panelSUAs.Controls.Add(this.chkSUA_ClassC);
            this.panelSUAs.Controls.Add(this.chkSUA_ClassB);
            this.panelSUAs.Controls.Add(this.label6);
            this.panelSUAs.Location = new System.Drawing.Point(426, 312);
            this.panelSUAs.Name = "panelSUAs";
            this.panelSUAs.Size = new System.Drawing.Size(182, 90);
            this.panelSUAs.TabIndex = 47;
            this.panelSUAs.Visible = false;
            // 
            // chkSUA_Danger
            // 
            this.chkSUA_Danger.AutoSize = true;
            this.chkSUA_Danger.Location = new System.Drawing.Point(83, 61);
            this.chkSUA_Danger.Name = "chkSUA_Danger";
            this.chkSUA_Danger.Size = new System.Drawing.Size(61, 17);
            this.chkSUA_Danger.TabIndex = 6;
            this.chkSUA_Danger.Text = "Danger";
            this.chkSUA_Danger.UseVisualStyleBackColor = true;
            // 
            // chkSUA_Prohibited
            // 
            this.chkSUA_Prohibited.AutoSize = true;
            this.chkSUA_Prohibited.Location = new System.Drawing.Point(83, 43);
            this.chkSUA_Prohibited.Name = "chkSUA_Prohibited";
            this.chkSUA_Prohibited.Size = new System.Drawing.Size(73, 17);
            this.chkSUA_Prohibited.TabIndex = 5;
            this.chkSUA_Prohibited.Text = "Prohibited";
            this.chkSUA_Prohibited.UseVisualStyleBackColor = true;
            // 
            // chkSUA_Restricted
            // 
            this.chkSUA_Restricted.AutoSize = true;
            this.chkSUA_Restricted.Location = new System.Drawing.Point(83, 25);
            this.chkSUA_Restricted.Name = "chkSUA_Restricted";
            this.chkSUA_Restricted.Size = new System.Drawing.Size(74, 17);
            this.chkSUA_Restricted.TabIndex = 4;
            this.chkSUA_Restricted.Text = "Restricted";
            this.chkSUA_Restricted.UseVisualStyleBackColor = true;
            // 
            // chkSUA_ClassD
            // 
            this.chkSUA_ClassD.AutoSize = true;
            this.chkSUA_ClassD.Checked = true;
            this.chkSUA_ClassD.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSUA_ClassD.Location = new System.Drawing.Point(15, 61);
            this.chkSUA_ClassD.Name = "chkSUA_ClassD";
            this.chkSUA_ClassD.Size = new System.Drawing.Size(62, 17);
            this.chkSUA_ClassD.TabIndex = 3;
            this.chkSUA_ClassD.Text = "Class D";
            this.chkSUA_ClassD.UseVisualStyleBackColor = true;
            // 
            // chkSUA_ClassC
            // 
            this.chkSUA_ClassC.AutoSize = true;
            this.chkSUA_ClassC.Checked = true;
            this.chkSUA_ClassC.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSUA_ClassC.Location = new System.Drawing.Point(15, 42);
            this.chkSUA_ClassC.Name = "chkSUA_ClassC";
            this.chkSUA_ClassC.Size = new System.Drawing.Size(61, 17);
            this.chkSUA_ClassC.TabIndex = 2;
            this.chkSUA_ClassC.Text = "Class C";
            this.chkSUA_ClassC.UseVisualStyleBackColor = true;
            // 
            // chkSUA_ClassB
            // 
            this.chkSUA_ClassB.AutoSize = true;
            this.chkSUA_ClassB.Checked = true;
            this.chkSUA_ClassB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSUA_ClassB.Location = new System.Drawing.Point(15, 25);
            this.chkSUA_ClassB.Name = "chkSUA_ClassB";
            this.chkSUA_ClassB.Size = new System.Drawing.Size(61, 17);
            this.chkSUA_ClassB.TabIndex = 1;
            this.chkSUA_ClassB.Text = "Class B";
            this.chkSUA_ClassB.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(4, 4);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(128, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Select SUAs to be written";
            // 
            // chkOverwrite
            // 
            this.chkOverwrite.AutoSize = true;
            this.chkOverwrite.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkOverwrite.Location = new System.Drawing.Point(358, 122);
            this.chkOverwrite.Name = "chkOverwrite";
            this.chkOverwrite.Size = new System.Drawing.Size(170, 20);
            this.chkOverwrite.TabIndex = 48;
            this.chkOverwrite.Text = "Confirm overwrite of files";
            this.chkOverwrite.UseVisualStyleBackColor = true;
            this.chkOverwrite.CheckedChanged += new System.EventHandler(this.ChkOverwrite_CheckedChanged);
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel1.Location = new System.Drawing.Point(4, 83);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(119, 16);
            this.linkLabel1.TabIndex = 49;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "FAA 28-day NASR";
            this.toolTip1.SetToolTip(this.linkLabel1, "Open FAA 28-day NASR subscription");
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1139, 526);
            this.ControlBox = false;
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.chkOverwrite);
            this.Controls.Add(this.panelSUAs);
            this.Controls.Add(this.cmdAddSUAs);
            this.Controls.Add(this.panelSCTsections);
            this.Controls.Add(this.lblFilesWarning);
            this.Controls.Add(this.cmdLocalSectors);
            this.Controls.Add(this.cmdUpdateGrid);
            this.Controls.Add(this.cmdExit);
            this.Controls.Add(this.lblUpdating);
            this.Controls.Add(this.txtGridViewCount);
            this.Controls.Add(this.chkbxShowAll);
            this.Controls.Add(this.cmdWriteSCT);
            this.Controls.Add(this.cmdInstructions);
            this.Controls.Add(this.lblLogo);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.grpCircle);
            this.Controls.Add(this.grpSquareLimits);
            this.Controls.Add(this.grpSelectionMethod);
            this.Controls.Add(this.txtAsstFacilityEngineer);
            this.Controls.Add(this.txtAsstFacilityEngineer_label);
            this.Controls.Add(this.txtFacilityEngineer);
            this.Controls.Add(this.txtFacilityEngineer_label);
            this.Controls.Add(this.labelInfoSection);
            this.Controls.Add(this.grpAirspaceClass);
            this.Controls.Add(this.CboAirport);
            this.Controls.Add(this.CboARTCC);
            this.Controls.Add(this.cmdOutputFolder);
            this.Controls.Add(this.cmdDataFolder);
            this.Controls.Add(this.txtOutputFolder);
            this.Controls.Add(this.txtOutputFolder_label);
            this.Controls.Add(this.txtDataFolder);
            this.Controls.Add(this.txtDataFolder_label);
            this.Controls.Add(this.lblCycleInfo);
            this.Controls.Add(this.lblInfoSection_Caption);
            this.Controls.Add(this.cboARTCC_label);
            this.Controls.Add(this.cboAirport_label);
            this.Controls.Add(this.tabControl1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "SCT Builder";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.grpAirspaceClass.ResumeLayout(false);
            this.grpAirspaceClass.PerformLayout();
            this.grpSelectionMethod.ResumeLayout(false);
            this.grpSelectionMethod.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.grpSquareLimits.ResumeLayout(false);
            this.grpSquareLimits.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudEast)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSouth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudWest)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudNorth)).EndInit();
            this.grpCircle.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAPT)).EndInit();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvVOR)).EndInit();
            this.tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvNDB)).EndInit();
            this.tabPage4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFIX)).EndInit();
            this.tabPage5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRWY)).EndInit();
            this.tabPage6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAWY)).EndInit();
            this.tabPage7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSID)).EndInit();
            this.tabPage8.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSTAR)).EndInit();
            this.tabPage9.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvARB)).EndInit();
            this.tabPage10.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSUA)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.panelSCTsections.ResumeLayout(false);
            this.panelSCTsections.PerformLayout();
            this.panelSUAs.ResumeLayout(false);
            this.panelSUAs.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblInfoSection_Caption;
        private System.Windows.Forms.Label lblCycleInfo;
        private System.Windows.Forms.Label txtDataFolder_label;
        private System.Windows.Forms.TextBox txtDataFolder;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.TextBox txtOutputFolder;
        private System.Windows.Forms.Label txtOutputFolder_label;
        private System.Windows.Forms.Button cmdDataFolder;
        private System.Windows.Forms.Button cmdOutputFolder;
        private System.Windows.Forms.ComboBox CboARTCC;
        private System.Windows.Forms.Label cboARTCC_label;
        private System.Windows.Forms.Label cboAirport_label;
        private System.Windows.Forms.ComboBox CboAirport;
        private System.Windows.Forms.GroupBox grpAirspaceClass;
        private System.Windows.Forms.RadioButton btnClassC;
        private System.Windows.Forms.RadioButton btnClassB;
        private System.Windows.Forms.Label labelInfoSection;
        private System.Windows.Forms.TextBox txtAsstFacilityEngineer;
        private System.Windows.Forms.Label txtAsstFacilityEngineer_label;
        private System.Windows.Forms.TextBox txtFacilityEngineer;
        private System.Windows.Forms.Label txtFacilityEngineer_label;
        private System.Windows.Forms.GroupBox grpSelectionMethod;
        private System.Windows.Forms.RadioButton btnCircle;
        private System.Windows.Forms.RadioButton btnSquare;
        private System.Windows.Forms.RadioButton btnARTCC;
        private System.Windows.Forms.GroupBox grpSquareLimits;
        private System.Windows.Forms.NumericUpDown nudEast;
        private System.Windows.Forms.NumericUpDown nudSouth;
        private System.Windows.Forms.NumericUpDown nudWest;
        private System.Windows.Forms.NumericUpDown nudNorth;
        private System.Windows.Forms.Label lblMargins;
        private System.Windows.Forms.Label txtLatSouth_label;
        private System.Windows.Forms.TextBox txtLatSouth;
        private System.Windows.Forms.Label txtLatEast_label;
        private System.Windows.Forms.TextBox txtLongEast;
        private System.Windows.Forms.Label txtLatWest_label;
        private System.Windows.Forms.TextBox txtLongWest;
        private System.Windows.Forms.Label txtLatNorth_label;
        private System.Windows.Forms.TextBox txtLatNorth;
        private System.Windows.Forms.GroupBox grpCircle;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.TabPage tabPage7;
        private System.Windows.Forms.TabPage tabPage8;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label lblLogo;
        private System.Windows.Forms.DataGridView dgvAPT;
        private System.Windows.Forms.DataGridView dgvVOR;
        private System.Windows.Forms.DataGridView dgvNDB;
        private System.Windows.Forms.DataGridView dgvFIX;
        private System.Windows.Forms.DataGridView dgvRWY;
        private System.Windows.Forms.DataGridView dgvAWY;
        private System.Windows.Forms.DataGridView dgvSID;
        private System.Windows.Forms.DataGridView dgvSTAR;
        private System.Windows.Forms.Button cmdInstructions;
        private System.Windows.Forms.Button cmdWriteSCT;
        private System.Windows.Forms.CheckBox chkbxShowAll;
        private System.Windows.Forms.TextBox txtGridViewCount;
        private System.Windows.Forms.Label lblUpdating;
        private System.Windows.Forms.TabPage tabPage9;
        private System.Windows.Forms.DataGridView dgvARB;
        private System.Windows.Forms.Button cmdExit;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Button cmdUpdateGrid;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button cmdLocalSectors;
        private System.Windows.Forms.CheckBox chkAPTs;
        private System.Windows.Forms.CheckBox chkVORs;
        private System.Windows.Forms.CheckBox chkNDBs;
        private System.Windows.Forms.CheckBox chkRWYs;
        private System.Windows.Forms.CheckBox chkFIXes;
        private System.Windows.Forms.CheckBox chkAWYs;
        private System.Windows.Forms.CheckBox chkSSDs;
        private System.Windows.Forms.CheckBox chkALL;
        private System.Windows.Forms.CheckBox chkARBs;
        private System.Windows.Forms.Label lblFilesWarning;
        private System.Windows.Forms.CheckBox chkSSDName;
        private System.Windows.Forms.Panel panelSCTsections;
        private System.Windows.Forms.Button cmdAddSUAs;
        private System.Windows.Forms.CheckBox ChkSUA;
        private System.Windows.Forms.Panel panelSUAs;
        private System.Windows.Forms.CheckBox chkSUA_Danger;
        private System.Windows.Forms.CheckBox chkSUA_Prohibited;
        private System.Windows.Forms.CheckBox chkSUA_Restricted;
        private System.Windows.Forms.CheckBox chkSUA_ClassD;
        private System.Windows.Forms.CheckBox chkSUA_ClassC;
        private System.Windows.Forms.CheckBox chkSUA_ClassB;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TabPage tabPage10;
        private System.Windows.Forms.DataGridView dgvSUA;
        private System.Windows.Forms.CheckBox chkOverwrite;
        private System.Windows.Forms.LinkLabel linkLabel1;
    }
}

