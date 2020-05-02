namespace ComposerAdmin.Forms
{
    partial class FormComposerEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormComposerEditor));
            this.mStripMain = new System.Windows.Forms.MenuStrip();
            this.miFile = new System.Windows.Forms.ToolStripMenuItem();
            this.miFileNew = new System.Windows.Forms.ToolStripMenuItem();
            this.miFileOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.miFileSave = new System.Windows.Forms.ToolStripMenuItem();
            this.miFileSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.miStart = new System.Windows.Forms.ToolStripMenuItem();
            this.miStartGetFromDir = new System.Windows.Forms.ToolStripMenuItem();
            this.miImportFromLocalComputerStartMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.miImportFromNetworkComputerStartMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.miImportFromFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.tsMenuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.mniHelpAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.StatusDisplay = new System.Windows.Forms.ToolStripStatusLabel();
            this.TimerToolstripUpdate = new System.Windows.Forms.Timer(this.components);
            this.tsButtons = new System.Windows.Forms.ToolStrip();
            this.tsbFileNew = new System.Windows.Forms.ToolStripButton();
            this.tsbFileOpen = new System.Windows.Forms.ToolStripButton();
            this.tsbFileSave = new System.Windows.Forms.ToolStripButton();
            this.tsbSaveAs = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbImportLocalStartMenu = new System.Windows.Forms.ToolStripButton();
            this.tsbImportNetworkComputer = new System.Windows.Forms.ToolStripButton();
            this.tsbImportFolder = new System.Windows.Forms.ToolStripButton();
            this.StartEditor = new ComposerAdmin.Forms.StartMenuEditorControl();
            this.mStripMain.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.tsButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // mStripMain
            // 
            this.mStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miFile,
            this.miStart,
            this.tsMenuHelp});
            resources.ApplyResources(this.mStripMain, "mStripMain");
            this.mStripMain.Name = "mStripMain";
            // 
            // miFile
            // 
            this.miFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miFileNew,
            this.miFileOpen,
            this.miFileSave,
            this.miFileSaveAs});
            this.miFile.Name = "miFile";
            resources.ApplyResources(this.miFile, "miFile");
            // 
            // miFileNew
            // 
            this.miFileNew.Image = global::ComposerAdmin.Properties.Resources._new;
            this.miFileNew.Name = "miFileNew";
            resources.ApplyResources(this.miFileNew, "miFileNew");
            this.miFileNew.Click += new System.EventHandler(this.MenuClick);
            // 
            // miFileOpen
            // 
            this.miFileOpen.Image = global::ComposerAdmin.Properties.Resources.folder_page;
            this.miFileOpen.Name = "miFileOpen";
            resources.ApplyResources(this.miFileOpen, "miFileOpen");
            this.miFileOpen.Click += new System.EventHandler(this.MenuClick);
            // 
            // miFileSave
            // 
            this.miFileSave.Image = global::ComposerAdmin.Properties.Resources.disk;
            this.miFileSave.Name = "miFileSave";
            resources.ApplyResources(this.miFileSave, "miFileSave");
            this.miFileSave.Click += new System.EventHandler(this.MenuClick);
            // 
            // miFileSaveAs
            // 
            this.miFileSaveAs.Image = global::ComposerAdmin.Properties.Resources.drive_disk;
            this.miFileSaveAs.Name = "miFileSaveAs";
            resources.ApplyResources(this.miFileSaveAs, "miFileSaveAs");
            this.miFileSaveAs.Click += new System.EventHandler(this.MenuClick);
            // 
            // miStart
            // 
            this.miStart.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miStartGetFromDir});
            this.miStart.Name = "miStart";
            resources.ApplyResources(this.miStart, "miStart");
            // 
            // miStartGetFromDir
            // 
            this.miStartGetFromDir.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miImportFromLocalComputerStartMenu,
            this.miImportFromNetworkComputerStartMenu,
            this.miImportFromFolder});
            this.miStartGetFromDir.Name = "miStartGetFromDir";
            resources.ApplyResources(this.miStartGetFromDir, "miStartGetFromDir");
            // 
            // miImportFromLocalComputerStartMenu
            // 
            this.miImportFromLocalComputerStartMenu.Image = global::ComposerAdmin.Properties.Resources.computer_go;
            this.miImportFromLocalComputerStartMenu.Name = "miImportFromLocalComputerStartMenu";
            resources.ApplyResources(this.miImportFromLocalComputerStartMenu, "miImportFromLocalComputerStartMenu");
            this.miImportFromLocalComputerStartMenu.Click += new System.EventHandler(this.MenuImportShortcutsClick);
            // 
            // miImportFromNetworkComputerStartMenu
            // 
            this.miImportFromNetworkComputerStartMenu.Image = global::ComposerAdmin.Properties.Resources.computer_link;
            this.miImportFromNetworkComputerStartMenu.Name = "miImportFromNetworkComputerStartMenu";
            resources.ApplyResources(this.miImportFromNetworkComputerStartMenu, "miImportFromNetworkComputerStartMenu");
            this.miImportFromNetworkComputerStartMenu.Click += new System.EventHandler(this.MenuImportShortcutsClick);
            // 
            // miImportFromFolder
            // 
            this.miImportFromFolder.Image = global::ComposerAdmin.Properties.Resources.folder_link;
            this.miImportFromFolder.Name = "miImportFromFolder";
            resources.ApplyResources(this.miImportFromFolder, "miImportFromFolder");
            this.miImportFromFolder.Click += new System.EventHandler(this.MenuImportShortcutsClick);
            // 
            // tsMenuHelp
            // 
            this.tsMenuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mniHelpAbout});
            this.tsMenuHelp.Name = "tsMenuHelp";
            resources.ApplyResources(this.tsMenuHelp, "tsMenuHelp");
            // 
            // mniHelpAbout
            // 
            this.mniHelpAbout.Name = "mniHelpAbout";
            resources.ApplyResources(this.mniHelpAbout, "mniHelpAbout");
            this.mniHelpAbout.Click += new System.EventHandler(this.MenuHelp_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusDisplay});
            resources.ApplyResources(this.statusStrip1, "statusStrip1");
            this.statusStrip1.Name = "statusStrip1";
            // 
            // StatusDisplay
            // 
            this.StatusDisplay.Name = "StatusDisplay";
            resources.ApplyResources(this.StatusDisplay, "StatusDisplay");
            // 
            // TimerToolstripUpdate
            // 
            this.TimerToolstripUpdate.Interval = 5000;
            this.TimerToolstripUpdate.Tick += new System.EventHandler(this.TimerToolstripUpdate_Tick);
            // 
            // tsButtons
            // 
            this.tsButtons.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbFileNew,
            this.tsbFileOpen,
            this.tsbFileSave,
            this.tsbSaveAs,
            this.toolStripSeparator1,
            this.tsbImportLocalStartMenu,
            this.tsbImportNetworkComputer,
            this.tsbImportFolder});
            resources.ApplyResources(this.tsButtons, "tsButtons");
            this.tsButtons.Name = "tsButtons";
            this.tsButtons.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.tsButtons_ButtonClick);
            // 
            // tsbFileNew
            // 
            this.tsbFileNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbFileNew.Image = global::ComposerAdmin.Properties.Resources._new;
            resources.ApplyResources(this.tsbFileNew, "tsbFileNew");
            this.tsbFileNew.Name = "tsbFileNew";
            // 
            // tsbFileOpen
            // 
            this.tsbFileOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbFileOpen.Image = global::ComposerAdmin.Properties.Resources.folder_page;
            resources.ApplyResources(this.tsbFileOpen, "tsbFileOpen");
            this.tsbFileOpen.Name = "tsbFileOpen";
            // 
            // tsbFileSave
            // 
            this.tsbFileSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbFileSave.Image = global::ComposerAdmin.Properties.Resources.disk;
            resources.ApplyResources(this.tsbFileSave, "tsbFileSave");
            this.tsbFileSave.Name = "tsbFileSave";
            // 
            // tsbSaveAs
            // 
            this.tsbSaveAs.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbSaveAs.Image = global::ComposerAdmin.Properties.Resources.drive_disk;
            resources.ApplyResources(this.tsbSaveAs, "tsbSaveAs");
            this.tsbSaveAs.Name = "tsbSaveAs";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // tsbImportLocalStartMenu
            // 
            this.tsbImportLocalStartMenu.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbImportLocalStartMenu.Image = global::ComposerAdmin.Properties.Resources.computer_go;
            resources.ApplyResources(this.tsbImportLocalStartMenu, "tsbImportLocalStartMenu");
            this.tsbImportLocalStartMenu.Name = "tsbImportLocalStartMenu";
            // 
            // tsbImportNetworkComputer
            // 
            this.tsbImportNetworkComputer.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbImportNetworkComputer.Image = global::ComposerAdmin.Properties.Resources.computer_link;
            resources.ApplyResources(this.tsbImportNetworkComputer, "tsbImportNetworkComputer");
            this.tsbImportNetworkComputer.Name = "tsbImportNetworkComputer";
            // 
            // tsbImportFolder
            // 
            this.tsbImportFolder.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbImportFolder.Image = global::ComposerAdmin.Properties.Resources.folder_link;
            resources.ApplyResources(this.tsbImportFolder, "tsbImportFolder");
            this.tsbImportFolder.Name = "tsbImportFolder";
            // 
            // StartEditor
            // 
            resources.ApplyResources(this.StartEditor, "StartEditor");
            this.StartEditor.Name = "StartEditor";
            this.StartEditor.Shortcuts = null;
            this.StartEditor.DataChangedByUser += new System.EventHandler(this.StartEditor_OnDataChanged);
            // 
            // FormComposerEditor
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.StartEditor);
            this.Controls.Add(this.tsButtons);
            this.Controls.Add(this.mStripMain);
            this.Controls.Add(this.statusStrip1);
            this.MainMenuStrip = this.mStripMain;
            this.Name = "FormComposerEditor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_OnClosing);
            this.mStripMain.ResumeLayout(false);
            this.mStripMain.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tsButtons.ResumeLayout(false);
            this.tsButtons.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip mStripMain;
        private System.Windows.Forms.ToolStripMenuItem miFile;
        private System.Windows.Forms.ToolStripMenuItem miFileNew;
        private System.Windows.Forms.ToolStripMenuItem miFileOpen;
        private System.Windows.Forms.ToolStripMenuItem miFileSave;
        private System.Windows.Forms.ToolStripMenuItem miFileSaveAs;
        private System.Windows.Forms.ToolStripMenuItem miStart;
        private System.Windows.Forms.ToolStripMenuItem miStartGetFromDir;
        private System.Windows.Forms.ToolStripMenuItem miImportFromLocalComputerStartMenu;
        private System.Windows.Forms.ToolStripMenuItem miImportFromNetworkComputerStartMenu;
        private System.Windows.Forms.ToolStripMenuItem miImportFromFolder;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel StatusDisplay;
        private System.Windows.Forms.Timer TimerToolstripUpdate;
        private System.Windows.Forms.ToolStripMenuItem tsMenuHelp;
        private System.Windows.Forms.ToolStripMenuItem mniHelpAbout;
        private StartMenuEditorControl StartEditor;
        private System.Windows.Forms.ToolStrip tsButtons;
        private System.Windows.Forms.ToolStripButton tsbFileNew;
        private System.Windows.Forms.ToolStripButton tsbFileOpen;
        private System.Windows.Forms.ToolStripButton tsbFileSave;
        private System.Windows.Forms.ToolStripButton tsbSaveAs;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbImportLocalStartMenu;
        private System.Windows.Forms.ToolStripButton tsbImportNetworkComputer;
        private System.Windows.Forms.ToolStripButton tsbImportFolder;
    }
}