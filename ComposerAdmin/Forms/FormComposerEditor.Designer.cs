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
            this.TabMain = new System.Windows.Forms.TabControl();
            this.tabPMenuEditor = new System.Windows.Forms.TabPage();
            this.StartEditor = new ComposerAdmin.Forms.StartMenuEditorControl();
            this.tabPDesktopOptions = new System.Windows.Forms.TabPage();
            this.rTweaks = new ComposerAdmin.Forms.RegTweaksControl();
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
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsMenuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.mniHelpAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.StatusDisplay = new System.Windows.Forms.ToolStripStatusLabel();
            this.TimerToolstripUpdate = new System.Windows.Forms.Timer(this.components);
            this.TabMain.SuspendLayout();
            this.tabPMenuEditor.SuspendLayout();
            this.tabPDesktopOptions.SuspendLayout();
            this.mStripMain.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // TabMain
            // 
            this.TabMain.Controls.Add(this.tabPMenuEditor);
            this.TabMain.Controls.Add(this.tabPDesktopOptions);
            resources.ApplyResources(this.TabMain, "TabMain");
            this.TabMain.Name = "TabMain";
            this.TabMain.SelectedIndex = 0;
            // 
            // tabPMenuEditor
            // 
            this.tabPMenuEditor.Controls.Add(this.StartEditor);
            resources.ApplyResources(this.tabPMenuEditor, "tabPMenuEditor");
            this.tabPMenuEditor.Name = "tabPMenuEditor";
            this.tabPMenuEditor.UseVisualStyleBackColor = true;
            // 
            // StartEditor
            // 
            resources.ApplyResources(this.StartEditor, "StartEditor");
            this.StartEditor.Name = "StartEditor";
            this.StartEditor.Shortcuts = null;
            this.StartEditor.DataChangedByUser += new System.EventHandler(this.StartEditor_OnDataChanged);
            // 
            // tabPDesktopOptions
            // 
            this.tabPDesktopOptions.Controls.Add(this.rTweaks);
            resources.ApplyResources(this.tabPDesktopOptions, "tabPDesktopOptions");
            this.tabPDesktopOptions.Name = "tabPDesktopOptions";
            this.tabPDesktopOptions.UseVisualStyleBackColor = true;
            // 
            // rTweaks
            // 
            resources.ApplyResources(this.rTweaks, "rTweaks");
            this.rTweaks.Name = "rTweaks";
            this.rTweaks.RegTweakDefinitions = null;
            this.rTweaks.DataChangedByUser += new System.EventHandler(this.StartEditor_OnDataChanged);
            // 
            // mStripMain
            // 
            this.mStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miFile,
            this.miStart,
            this.toolStripMenuItem1,
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
            this.miFileNew.Name = "miFileNew";
            resources.ApplyResources(this.miFileNew, "miFileNew");
            this.miFileNew.Click += new System.EventHandler(this.MenuClick);
            // 
            // miFileOpen
            // 
            this.miFileOpen.Name = "miFileOpen";
            resources.ApplyResources(this.miFileOpen, "miFileOpen");
            this.miFileOpen.Click += new System.EventHandler(this.MenuClick);
            // 
            // miFileSave
            // 
            this.miFileSave.Name = "miFileSave";
            resources.ApplyResources(this.miFileSave, "miFileSave");
            this.miFileSave.Click += new System.EventHandler(this.MenuClick);
            // 
            // miFileSaveAs
            // 
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
            this.miImportFromLocalComputerStartMenu.Name = "miImportFromLocalComputerStartMenu";
            resources.ApplyResources(this.miImportFromLocalComputerStartMenu, "miImportFromLocalComputerStartMenu");
            this.miImportFromLocalComputerStartMenu.Click += new System.EventHandler(this.MenuImportShortcutsClick);
            // 
            // miImportFromNetworkComputerStartMenu
            // 
            this.miImportFromNetworkComputerStartMenu.Name = "miImportFromNetworkComputerStartMenu";
            resources.ApplyResources(this.miImportFromNetworkComputerStartMenu, "miImportFromNetworkComputerStartMenu");
            this.miImportFromNetworkComputerStartMenu.Click += new System.EventHandler(this.MenuImportShortcutsClick);
            // 
            // miImportFromFolder
            // 
            this.miImportFromFolder.Name = "miImportFromFolder";
            resources.ApplyResources(this.miImportFromFolder, "miImportFromFolder");
            this.miImportFromFolder.Click += new System.EventHandler(this.MenuImportShortcutsClick);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            resources.ApplyResources(this.toolStripMenuItem1, "toolStripMenuItem1");
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
            // FormComposerEditor
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.TabMain);
            this.Controls.Add(this.mStripMain);
            this.Controls.Add(this.statusStrip1);
            this.MainMenuStrip = this.mStripMain;
            this.Name = "FormComposerEditor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_OnClosing);
            this.TabMain.ResumeLayout(false);
            this.tabPMenuEditor.ResumeLayout(false);
            this.tabPDesktopOptions.ResumeLayout(false);
            this.mStripMain.ResumeLayout(false);
            this.mStripMain.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl TabMain;
        private System.Windows.Forms.TabPage tabPMenuEditor;
        private System.Windows.Forms.TabPage tabPDesktopOptions;
        private System.Windows.Forms.MenuStrip mStripMain;
        private System.Windows.Forms.ToolStripMenuItem miFile;
        private System.Windows.Forms.ToolStripMenuItem miFileNew;
        private System.Windows.Forms.ToolStripMenuItem miFileOpen;
        private System.Windows.Forms.ToolStripMenuItem miFileSave;
        private System.Windows.Forms.ToolStripMenuItem miFileSaveAs;
        private System.Windows.Forms.ToolStripMenuItem miStart;
        private System.Windows.Forms.ToolStripMenuItem miStartGetFromDir;
        private StartMenuEditorControl StartEditor;
        private System.Windows.Forms.ToolStripMenuItem miImportFromLocalComputerStartMenu;
        private System.Windows.Forms.ToolStripMenuItem miImportFromNetworkComputerStartMenu;
        private System.Windows.Forms.ToolStripMenuItem miImportFromFolder;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel StatusDisplay;
        private System.Windows.Forms.Timer TimerToolstripUpdate;
        private RegTweaksControl rTweaks;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem tsMenuHelp;
        private System.Windows.Forms.ToolStripMenuItem mniHelpAbout;
    }
}