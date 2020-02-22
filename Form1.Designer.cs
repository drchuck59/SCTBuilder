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
            this.txtAsstFacilityEngineer = new System.Windows.Forms.TextBox();
            this.txtFacilityEngineer = new System.Windows.Forms.TextBox();
            this.txtLatSouth = new System.Windows.Forms.TextBox();
            this.txtLongEast = new System.Windows.Forms.TextBox();
            this.txtLongWest = new System.Windows.Forms.TextBox();
            this.txtLatNorth = new System.Windows.Forms.TextBox();
            this.chkbxShowAll = new System.Windows.Forms.CheckBox();
            this.txtGridViewCount = new System.Windows.Forms.TextBox();
            this.lblUpdating = new System.Windows.Forms.Label();
            this.lblFilesWarning = new System.Windows.Forms.Label();
            this.chkSSDName = new System.Windows.Forms.CheckBox();
            this.ChkSUA = new System.Windows.Forms.CheckBox();
            this.panelSUAs = new System.Windows.Forms.Panel();
            this.chkSUA_Danger = new System.Windows.Forms.CheckBox();
            this.chkSUA_Prohibited = new System.Windows.Forms.CheckBox();
            this.chkSUA_Restricted = new System.Windows.Forms.CheckBox();
            this.chkSUA_ClassD = new System.Windows.Forms.CheckBox();
            this.chkSUA_ClassC = new System.Windows.Forms.CheckBox();
            this.chkSUA_ClassB = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cboARTCC_label = new System.Windows.Forms.Label();
            this.cboAirport_label = new System.Windows.Forms.Label();
            this.txtAsstFacilityEngineer_label = new System.Windows.Forms.Label();
            this.txtFacilityEngineer_label = new System.Windows.Forms.Label();
            this.FilterGroupBox = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
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
            this.lblLogo = new System.Windows.Forms.Label();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.generateFilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectedComponentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.entireSCTFilelongToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openDataFolderToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.selectOutputFolderToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.savePreferencesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LineGeneratorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.arcGneratorradToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.arcGeneratorstartendToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.procedureTurnToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.racetrackholdToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.runwayMarksToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.labelGeneratorforDiagramsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.iLSGeneratorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.facilitiesListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.xML2SCTToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.goToFAA28dayNASRToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.instructToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openDataFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectOutputFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitProgramToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.SCTtoolStripButton = new System.Windows.Forms.ToolStripButton();
            this.TxtToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.gridViewToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.InfoGroupBox = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.preferencesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.confirmOverwriteOfFilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.savePreferencesToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.FoldersGroupBox = new System.Windows.Forms.GroupBox();
            this.CenterLatLabel = new System.Windows.Forms.Label();
            this.CenterLatTextBox = new System.Windows.Forms.TextBox();
            this.CenterLonTextBox = new System.Windows.Forms.TextBox();
            this.CenterLonLabel = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panelSUAs.SuspendLayout();
            this.FilterGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudEast)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSouth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudWest)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudNorth)).BeginInit();
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
            this.panelSCTsections.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.InfoGroupBox.SuspendLayout();
            this.FoldersGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblInfoSection_Caption
            // 
            this.lblInfoSection_Caption.AutoSize = true;
            this.lblInfoSection_Caption.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInfoSection_Caption.Location = new System.Drawing.Point(22, 97);
            this.lblInfoSection_Caption.Name = "lblInfoSection_Caption";
            this.lblInfoSection_Caption.Size = new System.Drawing.Size(147, 20);
            this.lblInfoSection_Caption.TabIndex = 0;
            this.lblInfoSection_Caption.Text = "Data Cycle in use:";
            // 
            // lblCycleInfo
            // 
            this.lblCycleInfo.AutoSize = true;
            this.lblCycleInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCycleInfo.Location = new System.Drawing.Point(22, 119);
            this.lblCycleInfo.Name = "lblCycleInfo";
            this.lblCycleInfo.Size = new System.Drawing.Size(174, 20);
            this.lblCycleInfo.TabIndex = 1;
            this.lblCycleInfo.Text = "No Active Cycle Data!";
            // 
            // txtDataFolder_label
            // 
            this.txtDataFolder_label.AutoSize = true;
            this.txtDataFolder_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDataFolder_label.Location = new System.Drawing.Point(13, 37);
            this.txtDataFolder_label.Name = "txtDataFolder_label";
            this.txtDataFolder_label.Size = new System.Drawing.Size(45, 20);
            this.txtDataFolder_label.TabIndex = 2;
            this.txtDataFolder_label.Text = "Data";
            // 
            // txtDataFolder
            // 
            this.txtDataFolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDataFolder.Location = new System.Drawing.Point(64, 30);
            this.txtDataFolder.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtDataFolder.Name = "txtDataFolder";
            this.txtDataFolder.Size = new System.Drawing.Size(321, 27);
            this.txtDataFolder.TabIndex = 3;
            this.toolTip1.SetToolTip(this.txtDataFolder, "Folder containing FAA text files");
            this.txtDataFolder.Validating += new System.ComponentModel.CancelEventHandler(this.TxtDataFolder_Validating);
            this.txtDataFolder.Validated += new System.EventHandler(this.TxtDataFolder_Validated);
            // 
            // txtOutputFolder
            // 
            this.txtOutputFolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOutputFolder.Location = new System.Drawing.Point(64, 62);
            this.txtOutputFolder.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtOutputFolder.Name = "txtOutputFolder";
            this.txtOutputFolder.Size = new System.Drawing.Size(321, 27);
            this.txtOutputFolder.TabIndex = 5;
            this.toolTip1.SetToolTip(this.txtOutputFolder, "Folder containing sector file");
            this.txtOutputFolder.Validated += new System.EventHandler(this.TxtOutputFolder_Validated);
            // 
            // txtOutputFolder_label
            // 
            this.txtOutputFolder_label.AutoSize = true;
            this.txtOutputFolder_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOutputFolder_label.Location = new System.Drawing.Point(-1, 67);
            this.txtOutputFolder_label.Name = "txtOutputFolder_label";
            this.txtOutputFolder_label.Size = new System.Drawing.Size(59, 20);
            this.txtOutputFolder_label.TabIndex = 4;
            this.txtOutputFolder_label.Text = "Output";
            // 
            // cmdDataFolder
            // 
            this.cmdDataFolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdDataFolder.Location = new System.Drawing.Point(392, 30);
            this.cmdDataFolder.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmdDataFolder.Name = "cmdDataFolder";
            this.cmdDataFolder.Size = new System.Drawing.Size(37, 27);
            this.cmdDataFolder.TabIndex = 6;
            this.cmdDataFolder.Text = "...";
            this.toolTip1.SetToolTip(this.cmdDataFolder, "Browse to select data folder");
            this.cmdDataFolder.UseVisualStyleBackColor = true;
            this.cmdDataFolder.Click += new System.EventHandler(this.CmdDataFolder_Click);
            // 
            // cmdOutputFolder
            // 
            this.cmdOutputFolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdOutputFolder.Location = new System.Drawing.Point(391, 63);
            this.cmdOutputFolder.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmdOutputFolder.Name = "cmdOutputFolder";
            this.cmdOutputFolder.Size = new System.Drawing.Size(37, 27);
            this.cmdOutputFolder.TabIndex = 7;
            this.cmdOutputFolder.Text = "...";
            this.toolTip1.SetToolTip(this.cmdOutputFolder, "Browse to select data folder");
            this.cmdOutputFolder.UseVisualStyleBackColor = true;
            this.cmdOutputFolder.Click += new System.EventHandler(this.CmdOutputFolder_Click);
            // 
            // CboARTCC
            // 
            this.CboARTCC.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CboARTCC.FormattingEnabled = true;
            this.CboARTCC.Location = new System.Drawing.Point(136, 70);
            this.CboARTCC.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.CboARTCC.Name = "CboARTCC";
            this.CboARTCC.Size = new System.Drawing.Size(112, 26);
            this.CboARTCC.TabIndex = 8;
            this.toolTip1.SetToolTip(this.CboARTCC, "YOUR ARTCC that will be the name of the sector file (e.g., 1909_ZJX)");
            this.CboARTCC.Validated += new System.EventHandler(this.CboARTCC_Validated);
            // 
            // CboAirport
            // 
            this.CboAirport.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CboAirport.FormattingEnabled = true;
            this.CboAirport.Location = new System.Drawing.Point(335, 70);
            this.CboAirport.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.CboAirport.Name = "CboAirport";
            this.CboAirport.Size = new System.Drawing.Size(101, 26);
            this.CboAirport.TabIndex = 10;
            this.toolTip1.SetToolTip(this.CboAirport, "The airport and lat/long where VRC will center when opened");
            this.CboAirport.Validated += new System.EventHandler(this.CboAirport_Validated);
            // 
            // txtAsstFacilityEngineer
            // 
            this.txtAsstFacilityEngineer.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAsstFacilityEngineer.Location = new System.Drawing.Point(136, 39);
            this.txtAsstFacilityEngineer.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtAsstFacilityEngineer.Name = "txtAsstFacilityEngineer";
            this.txtAsstFacilityEngineer.Size = new System.Drawing.Size(300, 24);
            this.txtAsstFacilityEngineer.TabIndex = 17;
            this.toolTip1.SetToolTip(this.txtAsstFacilityEngineer, "Assistant Facilities Engineer (can be blank)");
            // 
            // txtFacilityEngineer
            // 
            this.txtFacilityEngineer.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFacilityEngineer.Location = new System.Drawing.Point(136, 10);
            this.txtFacilityEngineer.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtFacilityEngineer.Name = "txtFacilityEngineer";
            this.txtFacilityEngineer.Size = new System.Drawing.Size(300, 24);
            this.txtFacilityEngineer.TabIndex = 15;
            this.toolTip1.SetToolTip(this.txtFacilityEngineer, "ARTCC Facilities Engineer (may not be blank)");
            this.txtFacilityEngineer.Validating += new System.ComponentModel.CancelEventHandler(this.TxtFacilityEngineer_Validating);
            this.txtFacilityEngineer.Validated += new System.EventHandler(this.TxtFacilityEngineer_Validated);
            // 
            // txtLatSouth
            // 
            this.txtLatSouth.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLatSouth.Location = new System.Drawing.Point(51, 98);
            this.txtLatSouth.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtLatSouth.Name = "txtLatSouth";
            this.txtLatSouth.Size = new System.Drawing.Size(89, 24);
            this.txtLatSouth.TabIndex = 26;
            this.toolTip1.SetToolTip(this.txtLatSouth, "Southern Latitude of square");
            // 
            // txtLongEast
            // 
            this.txtLongEast.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLongEast.Location = new System.Drawing.Point(51, 127);
            this.txtLongEast.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtLongEast.Name = "txtLongEast";
            this.txtLongEast.Size = new System.Drawing.Size(89, 24);
            this.txtLongEast.TabIndex = 24;
            this.toolTip1.SetToolTip(this.txtLongEast, "Eastern Longitude of square");
            // 
            // txtLongWest
            // 
            this.txtLongWest.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLongWest.Location = new System.Drawing.Point(51, 68);
            this.txtLongWest.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtLongWest.Name = "txtLongWest";
            this.txtLongWest.Size = new System.Drawing.Size(89, 24);
            this.txtLongWest.TabIndex = 22;
            this.toolTip1.SetToolTip(this.txtLongWest, "Western Longitude of square");
            // 
            // txtLatNorth
            // 
            this.txtLatNorth.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLatNorth.Location = new System.Drawing.Point(51, 38);
            this.txtLatNorth.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtLatNorth.Name = "txtLatNorth";
            this.txtLatNorth.Size = new System.Drawing.Size(89, 24);
            this.txtLatNorth.TabIndex = 20;
            this.toolTip1.SetToolTip(this.txtLatNorth, "Northern Latitude of square");
            // 
            // chkbxShowAll
            // 
            this.chkbxShowAll.AutoSize = true;
            this.chkbxShowAll.BackColor = System.Drawing.Color.Transparent;
            this.chkbxShowAll.Location = new System.Drawing.Point(892, 629);
            this.chkbxShowAll.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.chkbxShowAll.Name = "chkbxShowAll";
            this.chkbxShowAll.Size = new System.Drawing.Size(219, 21);
            this.chkbxShowAll.TabIndex = 28;
            this.chkbxShowAll.Text = "Ignore filter in gridview display";
            this.toolTip1.SetToolTip(this.chkbxShowAll, "Check to see all items in table");
            this.chkbxShowAll.UseVisualStyleBackColor = false;
            this.chkbxShowAll.CheckedChanged += new System.EventHandler(this.ChkbxShowAll_CheckedChanged);
            // 
            // txtGridViewCount
            // 
            this.txtGridViewCount.Location = new System.Drawing.Point(1380, 626);
            this.txtGridViewCount.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtGridViewCount.Name = "txtGridViewCount";
            this.txtGridViewCount.Size = new System.Drawing.Size(120, 22);
            this.txtGridViewCount.TabIndex = 29;
            this.toolTip1.SetToolTip(this.txtGridViewCount, "Number displayed / Total in table");
            // 
            // lblUpdating
            // 
            this.lblUpdating.AutoSize = true;
            this.lblUpdating.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.lblUpdating.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUpdating.Location = new System.Drawing.Point(590, 131);
            this.lblUpdating.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblUpdating.Name = "lblUpdating";
            this.lblUpdating.Size = new System.Drawing.Size(270, 29);
            this.lblUpdating.TabIndex = 1;
            this.lblUpdating.Text = "Updating.  Please wait...";
            this.lblUpdating.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.toolTip1.SetToolTip(this.lblUpdating, "The FAA data file is HUGE and can take up to a minute to read.");
            this.lblUpdating.Visible = false;
            // 
            // lblFilesWarning
            // 
            this.lblFilesWarning.AutoSize = true;
            this.lblFilesWarning.BackColor = System.Drawing.Color.Yellow;
            this.lblFilesWarning.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFilesWarning.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.lblFilesWarning.Location = new System.Drawing.Point(494, 202);
            this.lblFilesWarning.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFilesWarning.Name = "lblFilesWarning";
            this.lblFilesWarning.Size = new System.Drawing.Size(231, 36);
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
            this.chkSSDName.Location = new System.Drawing.Point(28, 219);
            this.chkSSDName.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.chkSSDName.Name = "chkSSDName";
            this.chkSSDName.Size = new System.Drawing.Size(116, 21);
            this.chkSSDName.TabIndex = 44;
            this.chkSSDName.Text = "Use full name";
            this.toolTip1.SetToolTip(this.chkSSDName, "If checked, SCT diagrams will use full name for SIDs/STARs. If UNchecked, will us" +
        "e the abbreviation.");
            this.chkSSDName.UseVisualStyleBackColor = true;
            // 
            // ChkSUA
            // 
            this.ChkSUA.AutoSize = true;
            this.ChkSUA.Enabled = false;
            this.ChkSUA.Location = new System.Drawing.Point(3, 243);
            this.ChkSUA.Margin = new System.Windows.Forms.Padding(4);
            this.ChkSUA.Name = "ChkSUA";
            this.ChkSUA.Size = new System.Drawing.Size(164, 21);
            this.ChkSUA.TabIndex = 45;
            this.ChkSUA.Text = "Special Use Airspace";
            this.toolTip1.SetToolTip(this.ChkSUA, "Coming soon");
            this.ChkSUA.UseVisualStyleBackColor = true;
            this.ChkSUA.CheckedChanged += new System.EventHandler(this.ChkSUA_CheckedChanged);
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
            this.panelSUAs.Enabled = false;
            this.panelSUAs.Location = new System.Drawing.Point(520, 521);
            this.panelSUAs.Margin = new System.Windows.Forms.Padding(4);
            this.panelSUAs.Name = "panelSUAs";
            this.panelSUAs.Size = new System.Drawing.Size(243, 110);
            this.panelSUAs.TabIndex = 47;
            this.toolTip1.SetToolTip(this.panelSUAs, "Future use");
            this.panelSUAs.Visible = false;
            // 
            // chkSUA_Danger
            // 
            this.chkSUA_Danger.AutoSize = true;
            this.chkSUA_Danger.Location = new System.Drawing.Point(110, 80);
            this.chkSUA_Danger.Margin = new System.Windows.Forms.Padding(4);
            this.chkSUA_Danger.Name = "chkSUA_Danger";
            this.chkSUA_Danger.Size = new System.Drawing.Size(77, 21);
            this.chkSUA_Danger.TabIndex = 6;
            this.chkSUA_Danger.Text = "Danger";
            this.chkSUA_Danger.UseVisualStyleBackColor = true;
            // 
            // chkSUA_Prohibited
            // 
            this.chkSUA_Prohibited.AutoSize = true;
            this.chkSUA_Prohibited.Location = new System.Drawing.Point(110, 58);
            this.chkSUA_Prohibited.Margin = new System.Windows.Forms.Padding(4);
            this.chkSUA_Prohibited.Name = "chkSUA_Prohibited";
            this.chkSUA_Prohibited.Size = new System.Drawing.Size(94, 21);
            this.chkSUA_Prohibited.TabIndex = 5;
            this.chkSUA_Prohibited.Text = "Prohibited";
            this.chkSUA_Prohibited.UseVisualStyleBackColor = true;
            // 
            // chkSUA_Restricted
            // 
            this.chkSUA_Restricted.AutoSize = true;
            this.chkSUA_Restricted.Location = new System.Drawing.Point(110, 35);
            this.chkSUA_Restricted.Margin = new System.Windows.Forms.Padding(4);
            this.chkSUA_Restricted.Name = "chkSUA_Restricted";
            this.chkSUA_Restricted.Size = new System.Drawing.Size(94, 21);
            this.chkSUA_Restricted.TabIndex = 4;
            this.chkSUA_Restricted.Text = "Restricted";
            this.chkSUA_Restricted.UseVisualStyleBackColor = true;
            // 
            // chkSUA_ClassD
            // 
            this.chkSUA_ClassD.AutoSize = true;
            this.chkSUA_ClassD.Checked = true;
            this.chkSUA_ClassD.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSUA_ClassD.Location = new System.Drawing.Point(20, 80);
            this.chkSUA_ClassD.Margin = new System.Windows.Forms.Padding(4);
            this.chkSUA_ClassD.Name = "chkSUA_ClassD";
            this.chkSUA_ClassD.Size = new System.Drawing.Size(78, 21);
            this.chkSUA_ClassD.TabIndex = 3;
            this.chkSUA_ClassD.Text = "Class D";
            this.chkSUA_ClassD.UseVisualStyleBackColor = true;
            // 
            // chkSUA_ClassC
            // 
            this.chkSUA_ClassC.AutoSize = true;
            this.chkSUA_ClassC.Checked = true;
            this.chkSUA_ClassC.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSUA_ClassC.Location = new System.Drawing.Point(20, 57);
            this.chkSUA_ClassC.Margin = new System.Windows.Forms.Padding(4);
            this.chkSUA_ClassC.Name = "chkSUA_ClassC";
            this.chkSUA_ClassC.Size = new System.Drawing.Size(77, 21);
            this.chkSUA_ClassC.TabIndex = 2;
            this.chkSUA_ClassC.Text = "Class C";
            this.chkSUA_ClassC.UseVisualStyleBackColor = true;
            // 
            // chkSUA_ClassB
            // 
            this.chkSUA_ClassB.AutoSize = true;
            this.chkSUA_ClassB.Checked = true;
            this.chkSUA_ClassB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSUA_ClassB.Location = new System.Drawing.Point(20, 35);
            this.chkSUA_ClassB.Margin = new System.Windows.Forms.Padding(4);
            this.chkSUA_ClassB.Name = "chkSUA_ClassB";
            this.chkSUA_ClassB.Size = new System.Drawing.Size(77, 21);
            this.chkSUA_ClassB.TabIndex = 1;
            this.chkSUA_ClassB.Text = "Class B";
            this.chkSUA_ClassB.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(5, 10);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(167, 17);
            this.label6.TabIndex = 0;
            this.label6.Text = "Select SUAs to be written";
            // 
            // cboARTCC_label
            // 
            this.cboARTCC_label.AutoSize = true;
            this.cboARTCC_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboARTCC_label.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cboARTCC_label.Location = new System.Drawing.Point(13, 83);
            this.cboARTCC_label.Name = "cboARTCC_label";
            this.cboARTCC_label.Size = new System.Drawing.Size(114, 18);
            this.cboARTCC_label.TabIndex = 9;
            this.cboARTCC_label.Text = "Primary ARTCC";
            this.cboARTCC_label.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboAirport_label
            // 
            this.cboAirport_label.AutoSize = true;
            this.cboAirport_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboAirport_label.Location = new System.Drawing.Point(254, 73);
            this.cboAirport_label.Name = "cboAirport_label";
            this.cboAirport_label.Size = new System.Drawing.Size(75, 20);
            this.cboAirport_label.TabIndex = 11;
            this.cboAirport_label.Text = "Main Apt";
            this.cboAirport_label.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtAsstFacilityEngineer_label
            // 
            this.txtAsstFacilityEngineer_label.AutoSize = true;
            this.txtAsstFacilityEngineer_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAsstFacilityEngineer_label.Location = new System.Drawing.Point(15, 56);
            this.txtAsstFacilityEngineer_label.Name = "txtAsstFacilityEngineer_label";
            this.txtAsstFacilityEngineer_label.Size = new System.Drawing.Size(91, 18);
            this.txtAsstFacilityEngineer_label.TabIndex = 16;
            this.txtAsstFacilityEngineer_label.Text = "Assistant FE";
            this.txtAsstFacilityEngineer_label.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtFacilityEngineer_label
            // 
            this.txtFacilityEngineer_label.AutoSize = true;
            this.txtFacilityEngineer_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFacilityEngineer_label.Location = new System.Drawing.Point(15, 27);
            this.txtFacilityEngineer_label.Name = "txtFacilityEngineer_label";
            this.txtFacilityEngineer_label.Size = new System.Drawing.Size(115, 18);
            this.txtFacilityEngineer_label.TabIndex = 14;
            this.txtFacilityEngineer_label.Text = "Facility Engineer";
            this.txtFacilityEngineer_label.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FilterGroupBox
            // 
            this.FilterGroupBox.Controls.Add(this.button1);
            this.FilterGroupBox.Controls.Add(this.label8);
            this.FilterGroupBox.Controls.Add(this.label5);
            this.FilterGroupBox.Controls.Add(this.label4);
            this.FilterGroupBox.Controls.Add(this.label3);
            this.FilterGroupBox.Controls.Add(this.label2);
            this.FilterGroupBox.Controls.Add(this.nudEast);
            this.FilterGroupBox.Controls.Add(this.nudSouth);
            this.FilterGroupBox.Controls.Add(this.nudWest);
            this.FilterGroupBox.Controls.Add(this.nudNorth);
            this.FilterGroupBox.Controls.Add(this.lblMargins);
            this.FilterGroupBox.Controls.Add(this.txtLatSouth_label);
            this.FilterGroupBox.Controls.Add(this.txtLatSouth);
            this.FilterGroupBox.Controls.Add(this.txtLatEast_label);
            this.FilterGroupBox.Controls.Add(this.txtLongEast);
            this.FilterGroupBox.Controls.Add(this.txtLatWest_label);
            this.FilterGroupBox.Controls.Add(this.txtLongWest);
            this.FilterGroupBox.Controls.Add(this.txtLatNorth_label);
            this.FilterGroupBox.Controls.Add(this.txtLatNorth);
            this.FilterGroupBox.Location = new System.Drawing.Point(12, 390);
            this.FilterGroupBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.FilterGroupBox.Name = "FilterGroupBox";
            this.FilterGroupBox.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.FilterGroupBox.Size = new System.Drawing.Size(446, 156);
            this.FilterGroupBox.TabIndex = 19;
            this.FilterGroupBox.TabStop = false;
            this.FilterGroupBox.Text = "Selection square";
            this.FilterGroupBox.Visible = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(273, 28);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(112, 47);
            this.button1.TabIndex = 38;
            this.button1.Text = "Use Primary ARTCC limits";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(244, 90);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(192, 34);
            this.label8.TabIndex = 37;
            this.label8.Text = "Use this square to select the \r\narea to output SCT sections.\r\n";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(150, 130);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(16, 17);
            this.label5.TabIndex = 36;
            this.label5.Text = "+";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(150, 104);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(13, 17);
            this.label4.TabIndex = 35;
            this.label4.Text = "-";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(150, 74);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(13, 17);
            this.label3.TabIndex = 34;
            this.label3.Text = "-";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(150, 43);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(16, 17);
            this.label2.TabIndex = 33;
            this.label2.Text = "+";
            // 
            // nudEast
            // 
            this.nudEast.Location = new System.Drawing.Point(173, 130);
            this.nudEast.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.nudEast.Name = "nudEast";
            this.nudEast.Size = new System.Drawing.Size(59, 22);
            this.nudEast.TabIndex = 32;
            this.nudEast.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // nudSouth
            // 
            this.nudSouth.Location = new System.Drawing.Point(173, 102);
            this.nudSouth.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.nudSouth.Name = "nudSouth";
            this.nudSouth.Size = new System.Drawing.Size(59, 22);
            this.nudSouth.TabIndex = 31;
            this.nudSouth.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // nudWest
            // 
            this.nudWest.Location = new System.Drawing.Point(173, 70);
            this.nudWest.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.nudWest.Name = "nudWest";
            this.nudWest.Size = new System.Drawing.Size(59, 22);
            this.nudWest.TabIndex = 30;
            this.nudWest.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // nudNorth
            // 
            this.nudNorth.Location = new System.Drawing.Point(173, 41);
            this.nudNorth.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.nudNorth.Name = "nudNorth";
            this.nudNorth.Size = new System.Drawing.Size(59, 22);
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
            this.lblMargins.Location = new System.Drawing.Point(151, 14);
            this.lblMargins.Name = "lblMargins";
            this.lblMargins.Size = new System.Drawing.Size(93, 17);
            this.lblMargins.TabIndex = 28;
            this.lblMargins.Text = "Margins (NM)";
            // 
            // txtLatSouth_label
            // 
            this.txtLatSouth_label.AutoSize = true;
            this.txtLatSouth_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLatSouth_label.Location = new System.Drawing.Point(-1, 102);
            this.txtLatSouth_label.Name = "txtLatSouth_label";
            this.txtLatSouth_label.Size = new System.Drawing.Size(47, 18);
            this.txtLatSouth_label.TabIndex = 27;
            this.txtLatSouth_label.Text = "South";
            // 
            // txtLatEast_label
            // 
            this.txtLatEast_label.AutoSize = true;
            this.txtLatEast_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLatEast_label.Location = new System.Drawing.Point(8, 131);
            this.txtLatEast_label.Name = "txtLatEast_label";
            this.txtLatEast_label.Size = new System.Drawing.Size(38, 18);
            this.txtLatEast_label.TabIndex = 25;
            this.txtLatEast_label.Text = "East";
            // 
            // txtLatWest_label
            // 
            this.txtLatWest_label.AutoSize = true;
            this.txtLatWest_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLatWest_label.Location = new System.Drawing.Point(5, 70);
            this.txtLatWest_label.Name = "txtLatWest_label";
            this.txtLatWest_label.Size = new System.Drawing.Size(43, 18);
            this.txtLatWest_label.TabIndex = 23;
            this.txtLatWest_label.Text = "West";
            // 
            // txtLatNorth_label
            // 
            this.txtLatNorth_label.AutoSize = true;
            this.txtLatNorth_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLatNorth_label.Location = new System.Drawing.Point(5, 42);
            this.txtLatNorth_label.Name = "txtLatNorth_label";
            this.txtLatNorth_label.Size = new System.Drawing.Size(45, 18);
            this.txtLatNorth_label.TabIndex = 21;
            this.txtLatNorth_label.Text = "North";
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
            this.tabControl1.Location = new System.Drawing.Point(886, 204);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(621, 416);
            this.tabControl1.TabIndex = 22;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dgvAPT);
            this.tabPage1.Location = new System.Drawing.Point(4, 29);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage1.Size = new System.Drawing.Size(613, 383);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "APTs";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dgvAPT
            // 
            this.dgvAPT.AllowUserToAddRows = false;
            this.dgvAPT.AllowUserToDeleteRows = false;
            this.dgvAPT.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAPT.Location = new System.Drawing.Point(6, 43);
            this.dgvAPT.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgvAPT.Name = "dgvAPT";
            this.dgvAPT.RowHeadersVisible = false;
            this.dgvAPT.RowHeadersWidth = 51;
            this.dgvAPT.RowTemplate.Height = 24;
            this.dgvAPT.Size = new System.Drawing.Size(608, 372);
            this.dgvAPT.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dgvVOR);
            this.tabPage2.Location = new System.Drawing.Point(4, 29);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage2.Size = new System.Drawing.Size(613, 383);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "VORs";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dgvVOR
            // 
            this.dgvVOR.AllowUserToAddRows = false;
            this.dgvVOR.AllowUserToDeleteRows = false;
            this.dgvVOR.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvVOR.Location = new System.Drawing.Point(3, 7);
            this.dgvVOR.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgvVOR.Name = "dgvVOR";
            this.dgvVOR.RowHeadersVisible = false;
            this.dgvVOR.RowHeadersWidth = 51;
            this.dgvVOR.RowTemplate.Height = 24;
            this.dgvVOR.Size = new System.Drawing.Size(608, 372);
            this.dgvVOR.TabIndex = 1;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.dgvNDB);
            this.tabPage3.Location = new System.Drawing.Point(4, 29);
            this.tabPage3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage3.Size = new System.Drawing.Size(613, 383);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "NDBs";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // dgvNDB
            // 
            this.dgvNDB.AllowUserToAddRows = false;
            this.dgvNDB.AllowUserToDeleteRows = false;
            this.dgvNDB.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvNDB.Location = new System.Drawing.Point(3, 7);
            this.dgvNDB.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgvNDB.Name = "dgvNDB";
            this.dgvNDB.RowHeadersVisible = false;
            this.dgvNDB.RowHeadersWidth = 51;
            this.dgvNDB.RowTemplate.Height = 24;
            this.dgvNDB.Size = new System.Drawing.Size(608, 372);
            this.dgvNDB.TabIndex = 2;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.dgvFIX);
            this.tabPage4.Location = new System.Drawing.Point(4, 29);
            this.tabPage4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage4.Size = new System.Drawing.Size(613, 383);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "FIXes";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // dgvFIX
            // 
            this.dgvFIX.AllowUserToAddRows = false;
            this.dgvFIX.AllowUserToDeleteRows = false;
            this.dgvFIX.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFIX.Location = new System.Drawing.Point(3, 7);
            this.dgvFIX.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgvFIX.Name = "dgvFIX";
            this.dgvFIX.RowHeadersVisible = false;
            this.dgvFIX.RowHeadersWidth = 51;
            this.dgvFIX.RowTemplate.Height = 24;
            this.dgvFIX.Size = new System.Drawing.Size(608, 372);
            this.dgvFIX.TabIndex = 2;
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.dgvRWY);
            this.tabPage5.Location = new System.Drawing.Point(4, 29);
            this.tabPage5.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage5.Size = new System.Drawing.Size(613, 383);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "RWYs";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // dgvRWY
            // 
            this.dgvRWY.AllowUserToAddRows = false;
            this.dgvRWY.AllowUserToDeleteRows = false;
            this.dgvRWY.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRWY.Location = new System.Drawing.Point(3, 7);
            this.dgvRWY.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgvRWY.Name = "dgvRWY";
            this.dgvRWY.RowHeadersVisible = false;
            this.dgvRWY.RowHeadersWidth = 51;
            this.dgvRWY.RowTemplate.Height = 24;
            this.dgvRWY.Size = new System.Drawing.Size(608, 372);
            this.dgvRWY.TabIndex = 2;
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.dgvAWY);
            this.tabPage6.Location = new System.Drawing.Point(4, 29);
            this.tabPage6.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage6.Size = new System.Drawing.Size(613, 383);
            this.tabPage6.TabIndex = 5;
            this.tabPage6.Text = "AWYs";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // dgvAWY
            // 
            this.dgvAWY.AllowUserToAddRows = false;
            this.dgvAWY.AllowUserToDeleteRows = false;
            this.dgvAWY.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAWY.Location = new System.Drawing.Point(3, 7);
            this.dgvAWY.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgvAWY.Name = "dgvAWY";
            this.dgvAWY.RowHeadersVisible = false;
            this.dgvAWY.RowHeadersWidth = 51;
            this.dgvAWY.RowTemplate.Height = 24;
            this.dgvAWY.Size = new System.Drawing.Size(608, 372);
            this.dgvAWY.TabIndex = 2;
            // 
            // tabPage7
            // 
            this.tabPage7.Controls.Add(this.dgvSID);
            this.tabPage7.Location = new System.Drawing.Point(4, 29);
            this.tabPage7.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage7.Name = "tabPage7";
            this.tabPage7.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage7.Size = new System.Drawing.Size(613, 383);
            this.tabPage7.TabIndex = 6;
            this.tabPage7.Text = "SIDs";
            this.tabPage7.UseVisualStyleBackColor = true;
            // 
            // dgvSID
            // 
            this.dgvSID.AllowUserToAddRows = false;
            this.dgvSID.AllowUserToDeleteRows = false;
            this.dgvSID.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSID.Location = new System.Drawing.Point(3, 7);
            this.dgvSID.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgvSID.Name = "dgvSID";
            this.dgvSID.RowHeadersVisible = false;
            this.dgvSID.RowHeadersWidth = 51;
            this.dgvSID.RowTemplate.Height = 24;
            this.dgvSID.Size = new System.Drawing.Size(608, 372);
            this.dgvSID.TabIndex = 2;
            // 
            // tabPage8
            // 
            this.tabPage8.Controls.Add(this.dgvSTAR);
            this.tabPage8.Location = new System.Drawing.Point(4, 29);
            this.tabPage8.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage8.Name = "tabPage8";
            this.tabPage8.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage8.Size = new System.Drawing.Size(613, 383);
            this.tabPage8.TabIndex = 7;
            this.tabPage8.Text = "STARs";
            this.tabPage8.UseVisualStyleBackColor = true;
            // 
            // dgvSTAR
            // 
            this.dgvSTAR.AllowUserToAddRows = false;
            this.dgvSTAR.AllowUserToDeleteRows = false;
            this.dgvSTAR.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSTAR.Location = new System.Drawing.Point(3, 7);
            this.dgvSTAR.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgvSTAR.Name = "dgvSTAR";
            this.dgvSTAR.RowHeadersVisible = false;
            this.dgvSTAR.RowHeadersWidth = 51;
            this.dgvSTAR.RowTemplate.Height = 24;
            this.dgvSTAR.Size = new System.Drawing.Size(608, 372);
            this.dgvSTAR.TabIndex = 2;
            // 
            // tabPage9
            // 
            this.tabPage9.Controls.Add(this.dgvARB);
            this.tabPage9.Location = new System.Drawing.Point(4, 29);
            this.tabPage9.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage9.Name = "tabPage9";
            this.tabPage9.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage9.Size = new System.Drawing.Size(613, 383);
            this.tabPage9.TabIndex = 8;
            this.tabPage9.Text = "ARBs";
            this.tabPage9.UseVisualStyleBackColor = true;
            // 
            // dgvARB
            // 
            this.dgvARB.AllowUserToAddRows = false;
            this.dgvARB.AllowUserToDeleteRows = false;
            this.dgvARB.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvARB.Location = new System.Drawing.Point(3, 7);
            this.dgvARB.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgvARB.Name = "dgvARB";
            this.dgvARB.RowHeadersVisible = false;
            this.dgvARB.RowHeadersWidth = 51;
            this.dgvARB.RowTemplate.Height = 24;
            this.dgvARB.Size = new System.Drawing.Size(608, 372);
            this.dgvARB.TabIndex = 3;
            // 
            // tabPage10
            // 
            this.tabPage10.Controls.Add(this.dgvSUA);
            this.tabPage10.Location = new System.Drawing.Point(4, 29);
            this.tabPage10.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage10.Name = "tabPage10";
            this.tabPage10.Padding = new System.Windows.Forms.Padding(4);
            this.tabPage10.Size = new System.Drawing.Size(613, 383);
            this.tabPage10.TabIndex = 9;
            this.tabPage10.Text = "SUAs";
            this.tabPage10.UseVisualStyleBackColor = true;
            // 
            // dgvSUA
            // 
            this.dgvSUA.AllowUserToAddRows = false;
            this.dgvSUA.AllowUserToDeleteRows = false;
            this.dgvSUA.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSUA.Location = new System.Drawing.Point(2, 9);
            this.dgvSUA.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgvSUA.Name = "dgvSUA";
            this.dgvSUA.RowHeadersVisible = false;
            this.dgvSUA.RowHeadersWidth = 51;
            this.dgvSUA.RowTemplate.Height = 24;
            this.dgvSUA.Size = new System.Drawing.Size(608, 372);
            this.dgvSUA.TabIndex = 4;
            // 
            // lblLogo
            // 
            this.lblLogo.AutoSize = true;
            this.lblLogo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.lblLogo.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLogo.Location = new System.Drawing.Point(21, 71);
            this.lblLogo.Name = "lblLogo";
            this.lblLogo.Size = new System.Drawing.Size(467, 29);
            this.lblLogo.TabIndex = 25;
            this.lblLogo.Text = "VATSIM Facility Engineers\' Build Utility";
            // 
            // chkAPTs
            // 
            this.chkAPTs.AutoSize = true;
            this.chkAPTs.Checked = true;
            this.chkAPTs.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAPTs.Location = new System.Drawing.Point(3, 30);
            this.chkAPTs.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.chkAPTs.Name = "chkAPTs";
            this.chkAPTs.Size = new System.Drawing.Size(79, 21);
            this.chkAPTs.TabIndex = 34;
            this.chkAPTs.Text = "Airports";
            this.chkAPTs.UseVisualStyleBackColor = true;
            // 
            // chkVORs
            // 
            this.chkVORs.AutoSize = true;
            this.chkVORs.Checked = true;
            this.chkVORs.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkVORs.Location = new System.Drawing.Point(3, 102);
            this.chkVORs.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.chkVORs.Name = "chkVORs";
            this.chkVORs.Size = new System.Drawing.Size(67, 21);
            this.chkVORs.TabIndex = 35;
            this.chkVORs.Text = "VORs";
            this.chkVORs.UseVisualStyleBackColor = true;
            // 
            // chkNDBs
            // 
            this.chkNDBs.AutoSize = true;
            this.chkNDBs.Checked = true;
            this.chkNDBs.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkNDBs.Location = new System.Drawing.Point(3, 125);
            this.chkNDBs.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.chkNDBs.Name = "chkNDBs";
            this.chkNDBs.Size = new System.Drawing.Size(66, 21);
            this.chkNDBs.TabIndex = 36;
            this.chkNDBs.Text = "NDBs";
            this.chkNDBs.UseVisualStyleBackColor = true;
            // 
            // chkRWYs
            // 
            this.chkRWYs.AutoSize = true;
            this.chkRWYs.Checked = true;
            this.chkRWYs.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkRWYs.Location = new System.Drawing.Point(3, 54);
            this.chkRWYs.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.chkRWYs.Name = "chkRWYs";
            this.chkRWYs.Size = new System.Drawing.Size(87, 21);
            this.chkRWYs.TabIndex = 37;
            this.chkRWYs.Text = "Runways";
            this.chkRWYs.UseVisualStyleBackColor = true;
            // 
            // chkFIXes
            // 
            this.chkFIXes.AutoSize = true;
            this.chkFIXes.Checked = true;
            this.chkFIXes.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkFIXes.Location = new System.Drawing.Point(3, 150);
            this.chkFIXes.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.chkFIXes.Name = "chkFIXes";
            this.chkFIXes.Size = new System.Drawing.Size(62, 21);
            this.chkFIXes.TabIndex = 38;
            this.chkFIXes.Text = "Fixes";
            this.chkFIXes.UseVisualStyleBackColor = true;
            // 
            // chkAWYs
            // 
            this.chkAWYs.AutoSize = true;
            this.chkAWYs.Checked = true;
            this.chkAWYs.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAWYs.Location = new System.Drawing.Point(3, 78);
            this.chkAWYs.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.chkAWYs.Name = "chkAWYs";
            this.chkAWYs.Size = new System.Drawing.Size(78, 21);
            this.chkAWYs.TabIndex = 39;
            this.chkAWYs.Text = "Airways";
            this.chkAWYs.UseVisualStyleBackColor = true;
            // 
            // chkSSDs
            // 
            this.chkSSDs.AutoSize = true;
            this.chkSSDs.Checked = true;
            this.chkSSDs.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSSDs.Location = new System.Drawing.Point(4, 197);
            this.chkSSDs.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.chkSSDs.Name = "chkSSDs";
            this.chkSSDs.Size = new System.Drawing.Size(107, 21);
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
            this.chkALL.Location = new System.Drawing.Point(3, 7);
            this.chkALL.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.chkALL.Name = "chkALL";
            this.chkALL.Size = new System.Drawing.Size(164, 21);
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
            this.chkARBs.Location = new System.Drawing.Point(3, 174);
            this.chkARBs.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.chkARBs.Name = "chkARBs";
            this.chkARBs.Size = new System.Drawing.Size(83, 21);
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
            this.panelSCTsections.Location = new System.Drawing.Point(491, 241);
            this.panelSCTsections.Margin = new System.Windows.Forms.Padding(4);
            this.panelSCTsections.Name = "panelSCTsections";
            this.panelSCTsections.Size = new System.Drawing.Size(267, 273);
            this.panelSCTsections.TabIndex = 45;
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1555, 28);
            this.menuStrip1.TabIndex = 50;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.generateFilesToolStripMenuItem,
            this.preferencesToolStripMenuItem,
            this.openDataFolderToolStripMenuItem1,
            this.savePreferencesToolStripMenuItem,
            this.selectOutputFolderToolStripMenuItem1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(46, 24);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // generateFilesToolStripMenuItem
            // 
            this.generateFilesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectedComponentsToolStripMenuItem,
            this.entireSCTFilelongToolStripMenuItem});
            this.generateFilesToolStripMenuItem.Name = "generateFilesToolStripMenuItem";
            this.generateFilesToolStripMenuItem.Size = new System.Drawing.Size(233, 26);
            this.generateFilesToolStripMenuItem.Text = "Generate files";
            // 
            // selectedComponentsToolStripMenuItem
            // 
            this.selectedComponentsToolStripMenuItem.Name = "selectedComponentsToolStripMenuItem";
            this.selectedComponentsToolStripMenuItem.Size = new System.Drawing.Size(235, 26);
            this.selectedComponentsToolStripMenuItem.Text = "Selected components";
            // 
            // entireSCTFilelongToolStripMenuItem
            // 
            this.entireSCTFilelongToolStripMenuItem.Name = "entireSCTFilelongToolStripMenuItem";
            this.entireSCTFilelongToolStripMenuItem.Size = new System.Drawing.Size(235, 26);
            this.entireSCTFilelongToolStripMenuItem.Text = "Entire SCT file (long)";
            this.entireSCTFilelongToolStripMenuItem.Click += new System.EventHandler(this.CmdWriteSCT_Click);
            // 
            // openDataFolderToolStripMenuItem1
            // 
            this.openDataFolderToolStripMenuItem1.Name = "openDataFolderToolStripMenuItem1";
            this.openDataFolderToolStripMenuItem1.Size = new System.Drawing.Size(233, 26);
            this.openDataFolderToolStripMenuItem1.Text = "Select data folder...";
            this.openDataFolderToolStripMenuItem1.Click += new System.EventHandler(this.CmdDataFolder_Click);
            // 
            // selectOutputFolderToolStripMenuItem1
            // 
            this.selectOutputFolderToolStripMenuItem1.Name = "selectOutputFolderToolStripMenuItem1";
            this.selectOutputFolderToolStripMenuItem1.Size = new System.Drawing.Size(233, 26);
            this.selectOutputFolderToolStripMenuItem1.Text = "Select output folder...";
            this.selectOutputFolderToolStripMenuItem1.Click += new System.EventHandler(this.CmdOutputFolder_Click);
            // 
            // savePreferencesToolStripMenuItem
            // 
            this.savePreferencesToolStripMenuItem.Name = "savePreferencesToolStripMenuItem";
            this.savePreferencesToolStripMenuItem.Size = new System.Drawing.Size(233, 26);
            this.savePreferencesToolStripMenuItem.Text = "Save preferences...";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(233, 26);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.CmdExit_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.LineGeneratorToolStripMenuItem,
            this.arcGneratorradToolStripMenuItem,
            this.arcGeneratorstartendToolStripMenuItem,
            this.procedureTurnToolStripMenuItem,
            this.racetrackholdToolStripMenuItem,
            this.runwayMarksToolStripMenuItem,
            this.labelGeneratorforDiagramsToolStripMenuItem,
            this.iLSGeneratorToolStripMenuItem,
            this.facilitiesListToolStripMenuItem,
            this.xML2SCTToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(58, 24);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // LineGeneratorToolStripMenuItem
            // 
            this.LineGeneratorToolStripMenuItem.Name = "LineGeneratorToolStripMenuItem";
            this.LineGeneratorToolStripMenuItem.Size = new System.Drawing.Size(257, 26);
            this.LineGeneratorToolStripMenuItem.Text = "Line generator";
            this.LineGeneratorToolStripMenuItem.Click += new System.EventHandler(this.LineGeneratorToolStripMenuItem_Click);
            // 
            // arcGneratorradToolStripMenuItem
            // 
            this.arcGneratorradToolStripMenuItem.Name = "arcGneratorradToolStripMenuItem";
            this.arcGneratorradToolStripMenuItem.Size = new System.Drawing.Size(257, 26);
            this.arcGneratorradToolStripMenuItem.Text = "Arc gnerator (radials)";
            // 
            // arcGeneratorstartendToolStripMenuItem
            // 
            this.arcGeneratorstartendToolStripMenuItem.Name = "arcGeneratorstartendToolStripMenuItem";
            this.arcGeneratorstartendToolStripMenuItem.Size = new System.Drawing.Size(257, 26);
            this.arcGeneratorstartendToolStripMenuItem.Text = "Arc generator (start/end)";
            // 
            // procedureTurnToolStripMenuItem
            // 
            this.procedureTurnToolStripMenuItem.Name = "procedureTurnToolStripMenuItem";
            this.procedureTurnToolStripMenuItem.Size = new System.Drawing.Size(257, 26);
            this.procedureTurnToolStripMenuItem.Text = "Procedure turn";
            // 
            // racetrackholdToolStripMenuItem
            // 
            this.racetrackholdToolStripMenuItem.Name = "racetrackholdToolStripMenuItem";
            this.racetrackholdToolStripMenuItem.Size = new System.Drawing.Size(257, 26);
            this.racetrackholdToolStripMenuItem.Text = "Racetrack (hold)";
            // 
            // runwayMarksToolStripMenuItem
            // 
            this.runwayMarksToolStripMenuItem.Name = "runwayMarksToolStripMenuItem";
            this.runwayMarksToolStripMenuItem.Size = new System.Drawing.Size(257, 26);
            this.runwayMarksToolStripMenuItem.Text = "Runway Marks";
            // 
            // labelGeneratorforDiagramsToolStripMenuItem
            // 
            this.labelGeneratorforDiagramsToolStripMenuItem.Name = "labelGeneratorforDiagramsToolStripMenuItem";
            this.labelGeneratorforDiagramsToolStripMenuItem.Size = new System.Drawing.Size(257, 26);
            this.labelGeneratorforDiagramsToolStripMenuItem.Text = "Label generator";
            // 
            // iLSGeneratorToolStripMenuItem
            // 
            this.iLSGeneratorToolStripMenuItem.Name = "iLSGeneratorToolStripMenuItem";
            this.iLSGeneratorToolStripMenuItem.Size = new System.Drawing.Size(257, 26);
            this.iLSGeneratorToolStripMenuItem.Text = "ILS generator";
            // 
            // facilitiesListToolStripMenuItem
            // 
            this.facilitiesListToolStripMenuItem.Name = "facilitiesListToolStripMenuItem";
            this.facilitiesListToolStripMenuItem.Size = new System.Drawing.Size(257, 26);
            this.facilitiesListToolStripMenuItem.Text = "Facilities list";
            // 
            // xML2SCTToolStripMenuItem
            // 
            this.xML2SCTToolStripMenuItem.Name = "xML2SCTToolStripMenuItem";
            this.xML2SCTToolStripMenuItem.Size = new System.Drawing.Size(257, 26);
            this.xML2SCTToolStripMenuItem.Text = "XML2SCT";
            this.xML2SCTToolStripMenuItem.Click += new System.EventHandler(this.XML2SCTToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.goToFAA28dayNASRToolStripMenuItem,
            this.instructToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(55, 24);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // goToFAA28dayNASRToolStripMenuItem
            // 
            this.goToFAA28dayNASRToolStripMenuItem.Name = "goToFAA28dayNASRToolStripMenuItem";
            this.goToFAA28dayNASRToolStripMenuItem.Size = new System.Drawing.Size(220, 26);
            this.goToFAA28dayNASRToolStripMenuItem.Text = "FAA 28-day NASR";
            this.goToFAA28dayNASRToolStripMenuItem.ToolTipText = "Opens in your default browser";
            this.goToFAA28dayNASRToolStripMenuItem.Click += new System.EventHandler(this.GoToFAA28dayNASRToolStripMenuItem_Click);
            // 
            // instructToolStripMenuItem
            // 
            this.instructToolStripMenuItem.Name = "instructToolStripMenuItem";
            this.instructToolStripMenuItem.Size = new System.Drawing.Size(220, 26);
            this.instructToolStripMenuItem.Text = "Display Instructions";
            this.instructToolStripMenuItem.Click += new System.EventHandler(this.CmdInstructions_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(220, 26);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.AboutToolStripMenuItem_Click);
            // 
            // openDataFolderToolStripMenuItem
            // 
            this.openDataFolderToolStripMenuItem.Name = "openDataFolderToolStripMenuItem";
            this.openDataFolderToolStripMenuItem.Size = new System.Drawing.Size(270, 34);
            this.openDataFolderToolStripMenuItem.Text = "Open Data Folder...";
            // 
            // selectOutputFolderToolStripMenuItem
            // 
            this.selectOutputFolderToolStripMenuItem.Name = "selectOutputFolderToolStripMenuItem";
            this.selectOutputFolderToolStripMenuItem.Size = new System.Drawing.Size(32, 19);
            this.selectOutputFolderToolStripMenuItem.Text = "Select Output Folder...";
            // 
            // exitProgramToolStripMenuItem1
            // 
            this.exitProgramToolStripMenuItem1.Name = "exitProgramToolStripMenuItem1";
            this.exitProgramToolStripMenuItem1.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.exitProgramToolStripMenuItem1.Size = new System.Drawing.Size(32, 19);
            this.exitProgramToolStripMenuItem1.Text = "Exit Program";
            this.exitProgramToolStripMenuItem1.ToolTipText = "Exits this program";
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SCTtoolStripButton,
            this.TxtToolStripButton,
            this.gridViewToolStripButton,
            this.toolStripButton1,
            this.toolStripButton2});
            this.toolStrip1.Location = new System.Drawing.Point(0, 28);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1555, 31);
            this.toolStrip1.TabIndex = 51;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // SCTtoolStripButton
            // 
            this.SCTtoolStripButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.SCTtoolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("SCTtoolStripButton.Image")));
            this.SCTtoolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SCTtoolStripButton.Name = "SCTtoolStripButton";
            this.SCTtoolStripButton.Size = new System.Drawing.Size(151, 28);
            this.SCTtoolStripButton.Text = "Generate SCT file";
            this.SCTtoolStripButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.SCTtoolStripButton.Click += new System.EventHandler(this.CmdWriteSCT_Click);
            // 
            // TxtToolStripButton
            // 
            this.TxtToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("TxtToolStripButton.Image")));
            this.TxtToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TxtToolStripButton.Name = "TxtToolStripButton";
            this.TxtToolStripButton.Size = new System.Drawing.Size(213, 28);
            this.TxtToolStripButton.Text = "Generate selected sections";
            // 
            // gridViewToolStripButton
            // 
            this.gridViewToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("gridViewToolStripButton.Image")));
            this.gridViewToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.gridViewToolStripButton.Name = "gridViewToolStripButton";
            this.gridViewToolStripButton.Size = new System.Drawing.Size(154, 28);
            this.gridViewToolStripButton.Text = "Update Grid View";
            this.gridViewToolStripButton.Click += new System.EventHandler(this.CmdUpdateGrid_Click);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(188, 28);
            this.toolStripButton1.Text = "Generate Local Sectors";
            this.toolStripButton1.Click += new System.EventHandler(this.LocalSectors_Click);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(135, 28);
            this.toolStripButton2.Text = "Generate SUAs";
            this.toolStripButton2.Click += new System.EventHandler(this.CmdAddSUAs_Click);
            // 
            // InfoGroupBox
            // 
            this.InfoGroupBox.Controls.Add(this.textBox1);
            this.InfoGroupBox.Controls.Add(this.label1);
            this.InfoGroupBox.Controls.Add(this.CenterLonTextBox);
            this.InfoGroupBox.Controls.Add(this.CenterLonLabel);
            this.InfoGroupBox.Controls.Add(this.CenterLatTextBox);
            this.InfoGroupBox.Controls.Add(this.CenterLatLabel);
            this.InfoGroupBox.Controls.Add(this.label7);
            this.InfoGroupBox.Controls.Add(this.txtAsstFacilityEngineer);
            this.InfoGroupBox.Controls.Add(this.txtAsstFacilityEngineer_label);
            this.InfoGroupBox.Controls.Add(this.txtFacilityEngineer);
            this.InfoGroupBox.Controls.Add(this.txtFacilityEngineer_label);
            this.InfoGroupBox.Controls.Add(this.CboAirport);
            this.InfoGroupBox.Controls.Add(this.CboARTCC);
            this.InfoGroupBox.Controls.Add(this.cboARTCC_label);
            this.InfoGroupBox.Controls.Add(this.cboAirport_label);
            this.InfoGroupBox.Location = new System.Drawing.Point(12, 194);
            this.InfoGroupBox.Name = "InfoGroupBox";
            this.InfoGroupBox.Size = new System.Drawing.Size(446, 191);
            this.InfoGroupBox.TabIndex = 52;
            this.InfoGroupBox.TabStop = false;
            this.InfoGroupBox.Text = "[INFO]";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(62, 141);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(374, 34);
            this.label7.TabIndex = 18;
            this.label7.Text = "Other [INFO] wil be added when FAA data is selected.\r\nEvery output file will have" +
    " the [INFO] header for reference.";
            // 
            // preferencesToolStripMenuItem
            // 
            this.preferencesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.confirmOverwriteOfFilesToolStripMenuItem,
            this.savePreferencesToolStripMenuItem1});
            this.preferencesToolStripMenuItem.Name = "preferencesToolStripMenuItem";
            this.preferencesToolStripMenuItem.Size = new System.Drawing.Size(233, 26);
            this.preferencesToolStripMenuItem.Text = "Preferences";
            // 
            // confirmOverwriteOfFilesToolStripMenuItem
            // 
            this.confirmOverwriteOfFilesToolStripMenuItem.CheckOnClick = true;
            this.confirmOverwriteOfFilesToolStripMenuItem.Name = "confirmOverwriteOfFilesToolStripMenuItem";
            this.confirmOverwriteOfFilesToolStripMenuItem.Size = new System.Drawing.Size(260, 26);
            this.confirmOverwriteOfFilesToolStripMenuItem.Text = "Confirm overwrite of files";
            this.confirmOverwriteOfFilesToolStripMenuItem.CheckedChanged += new System.EventHandler(this.confirmOverwriteOfFilesToolStripMenuItem_CheckedChanged);
            // 
            // savePreferencesToolStripMenuItem1
            // 
            this.savePreferencesToolStripMenuItem1.Name = "savePreferencesToolStripMenuItem1";
            this.savePreferencesToolStripMenuItem1.Size = new System.Drawing.Size(260, 26);
            this.savePreferencesToolStripMenuItem1.Text = "Save preferences";
            // 
            // FoldersGroupBox
            // 
            this.FoldersGroupBox.Controls.Add(this.txtDataFolder);
            this.FoldersGroupBox.Controls.Add(this.txtDataFolder_label);
            this.FoldersGroupBox.Controls.Add(this.txtOutputFolder_label);
            this.FoldersGroupBox.Controls.Add(this.txtOutputFolder);
            this.FoldersGroupBox.Controls.Add(this.cmdDataFolder);
            this.FoldersGroupBox.Controls.Add(this.cmdOutputFolder);
            this.FoldersGroupBox.Location = new System.Drawing.Point(12, 552);
            this.FoldersGroupBox.Name = "FoldersGroupBox";
            this.FoldersGroupBox.Size = new System.Drawing.Size(446, 100);
            this.FoldersGroupBox.TabIndex = 53;
            this.FoldersGroupBox.TabStop = false;
            this.FoldersGroupBox.Text = "Folders";
            // 
            // CenterLatLabel
            // 
            this.CenterLatLabel.AutoSize = true;
            this.CenterLatLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CenterLatLabel.Location = new System.Drawing.Point(16, 114);
            this.CenterLatLabel.Name = "CenterLatLabel";
            this.CenterLatLabel.Size = new System.Drawing.Size(28, 18);
            this.CenterLatLabel.TabIndex = 19;
            this.CenterLatLabel.Text = "Lat";
            this.toolTip1.SetToolTip(this.CenterLatLabel, "VRC will open centered on these coordinates");
            // 
            // CenterLatTextBox
            // 
            this.CenterLatTextBox.Location = new System.Drawing.Point(49, 112);
            this.CenterLatTextBox.Name = "CenterLatTextBox";
            this.CenterLatTextBox.Size = new System.Drawing.Size(111, 22);
            this.CenterLatTextBox.TabIndex = 20;
            this.toolTip1.SetToolTip(this.CenterLatTextBox, "VRC will open centered on these coordinates");
            // 
            // CenterLonTextBox
            // 
            this.CenterLonTextBox.Location = new System.Drawing.Point(204, 112);
            this.CenterLonTextBox.Name = "CenterLonTextBox";
            this.CenterLonTextBox.Size = new System.Drawing.Size(110, 22);
            this.CenterLonTextBox.TabIndex = 22;
            this.toolTip1.SetToolTip(this.CenterLonTextBox, "VRC will open centered on these coordinates");
            // 
            // CenterLonLabel
            // 
            this.CenterLonLabel.AutoSize = true;
            this.CenterLonLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CenterLonLabel.Location = new System.Drawing.Point(167, 114);
            this.CenterLonLabel.Name = "CenterLonLabel";
            this.CenterLonLabel.Size = new System.Drawing.Size(33, 18);
            this.CenterLonLabel.TabIndex = 21;
            this.CenterLonLabel.Text = "Lon";
            this.toolTip1.SetToolTip(this.CenterLonLabel, "VRC will open centered on these coordinates");
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(384, 112);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(50, 22);
            this.textBox1.TabIndex = 24;
            this.toolTip1.SetToolTip(this.textBox1, "VRC will use this to correct to true bearings");
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(319, 115);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 18);
            this.label1.TabIndex = 23;
            this.label1.Text = "Mag Var";
            this.toolTip1.SetToolTip(this.label1, "VRC will use this to correct to true bearings");
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1555, 768);
            this.Controls.Add(this.FoldersGroupBox);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.panelSUAs);
            this.Controls.Add(this.panelSCTsections);
            this.Controls.Add(this.lblFilesWarning);
            this.Controls.Add(this.lblUpdating);
            this.Controls.Add(this.txtGridViewCount);
            this.Controls.Add(this.chkbxShowAll);
            this.Controls.Add(this.lblLogo);
            this.Controls.Add(this.FilterGroupBox);
            this.Controls.Add(this.lblCycleInfo);
            this.Controls.Add(this.lblInfoSection_Caption);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.InfoGroupBox);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Facility Engineers\' Build Utility";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panelSUAs.ResumeLayout(false);
            this.panelSUAs.PerformLayout();
            this.FilterGroupBox.ResumeLayout(false);
            this.FilterGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudEast)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSouth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudWest)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudNorth)).EndInit();
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
            this.panelSCTsections.ResumeLayout(false);
            this.panelSCTsections.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.InfoGroupBox.ResumeLayout(false);
            this.InfoGroupBox.PerformLayout();
            this.FoldersGroupBox.ResumeLayout(false);
            this.FoldersGroupBox.PerformLayout();
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
        private System.Windows.Forms.TextBox txtAsstFacilityEngineer;
        private System.Windows.Forms.Label txtAsstFacilityEngineer_label;
        private System.Windows.Forms.TextBox txtFacilityEngineer;
        private System.Windows.Forms.Label txtFacilityEngineer_label;
        private System.Windows.Forms.GroupBox FilterGroupBox;
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
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.TabPage tabPage7;
        private System.Windows.Forms.TabPage tabPage8;
        private System.Windows.Forms.Label lblLogo;
        private System.Windows.Forms.DataGridView dgvAPT;
        private System.Windows.Forms.DataGridView dgvVOR;
        private System.Windows.Forms.DataGridView dgvNDB;
        private System.Windows.Forms.DataGridView dgvFIX;
        private System.Windows.Forms.DataGridView dgvRWY;
        private System.Windows.Forms.DataGridView dgvAWY;
        private System.Windows.Forms.DataGridView dgvSID;
        private System.Windows.Forms.DataGridView dgvSTAR;
        private System.Windows.Forms.CheckBox chkbxShowAll;
        private System.Windows.Forms.TextBox txtGridViewCount;
        private System.Windows.Forms.Label lblUpdating;
        private System.Windows.Forms.TabPage tabPage9;
        private System.Windows.Forms.DataGridView dgvARB;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
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
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem goToFAA28dayNASRToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem instructToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem generateFilesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selectedComponentsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem entireSCTFilelongToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openDataFolderToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem selectOutputFolderToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem savePreferencesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem LineGeneratorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem arcGneratorradToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem arcGeneratorstartendToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem procedureTurnToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem racetrackholdToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem runwayMarksToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem labelGeneratorforDiagramsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem iLSGeneratorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem facilitiesListToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openDataFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selectOutputFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitProgramToolStripMenuItem1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton SCTtoolStripButton;
        private System.Windows.Forms.ToolStripButton TxtToolStripButton;
        private System.Windows.Forms.ToolStripButton gridViewToolStripButton;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripMenuItem xML2SCTToolStripMenuItem;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox InfoGroupBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ToolStripMenuItem preferencesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem confirmOverwriteOfFilesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem savePreferencesToolStripMenuItem1;
        private System.Windows.Forms.GroupBox FoldersGroupBox;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox CenterLonTextBox;
        private System.Windows.Forms.Label CenterLonLabel;
        private System.Windows.Forms.TextBox CenterLatTextBox;
        private System.Windows.Forms.Label CenterLatLabel;
    }
}

