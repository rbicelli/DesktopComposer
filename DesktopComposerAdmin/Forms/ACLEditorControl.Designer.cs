namespace DesktopComposerAdmin.Forms
{
    partial class ACLEditorControl
    {
        /// <summary> 
        /// Variabile di progettazione necessaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Pulire le risorse in uso.
        /// </summary>
        /// <param name="disposing">ha valore true se le risorse gestite devono essere eliminate, false in caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Codice generato da Progettazione componenti

        /// <summary> 
        /// Metodo necessario per il supporto della finestra di progettazione. Non modificare 
        /// il contenuto del metodo con l'editor di codice.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ACLEditorControl));
            this.lv = new System.Windows.Forms.ListView();
            this.imlIcons = new System.Windows.Forms.ImageList(this.components);
            this.ADPicker = new Tulpep.ActiveDirectoryObjectPicker.DirectoryObjectPickerDialog();
            this.ctxMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ctxMenuDeny = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxMenuDisabled = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // lv
            // 
            this.lv.ContextMenuStrip = this.ctxMenu;
            resources.ApplyResources(this.lv, "lv");
            this.lv.HideSelection = false;
            this.lv.LargeImageList = this.imlIcons;
            this.lv.Name = "lv";
            this.lv.SmallImageList = this.imlIcons;
            this.lv.UseCompatibleStateImageBehavior = false;
            this.lv.View = System.Windows.Forms.View.List;
            this.lv.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lv_ItemSelectionChanged);
            // 
            // imlIcons
            // 
            this.imlIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imlIcons.ImageStream")));
            this.imlIcons.TransparentColor = System.Drawing.Color.Transparent;
            this.imlIcons.Images.SetKeyName(0, "dsuiext_4099.ico");
            this.imlIcons.Images.SetKeyName(1, "dsuiext_4108.ico");
            this.imlIcons.Images.SetKeyName(2, "userdeny_4099.ico");
            this.imlIcons.Images.SetKeyName(3, "groupdeny.ico");
            this.imlIcons.Images.SetKeyName(4, "userdisabled.ico");
            this.imlIcons.Images.SetKeyName(5, "groupdisabled.ico");
            // 
            // ADPicker
            // 
            this.ADPicker.AttributesToFetch.Add("objectSid");
            this.ADPicker.AttributesToFetch.Add("sAMAccountName");
            // 
            // ctxMenu
            // 
            this.ctxMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ctxMenuDeny,
            this.ctxMenuDisabled,
            this.deleteToolStripMenuItem});
            this.ctxMenu.Name = "ctxMenu";
            resources.ApplyResources(this.ctxMenu, "ctxMenu");
            this.ctxMenu.Opening += new System.ComponentModel.CancelEventHandler(this.ctxMenu_Open);
            // 
            // ctxMenuDeny
            // 
            this.ctxMenuDeny.CheckOnClick = true;
            this.ctxMenuDeny.Name = "ctxMenuDeny";
            resources.ApplyResources(this.ctxMenuDeny, "ctxMenuDeny");
            this.ctxMenuDeny.CheckedChanged += new System.EventHandler(this.CtxMenu_CheckChanged);
            // 
            // ctxMenuDisabled
            // 
            this.ctxMenuDisabled.CheckOnClick = true;
            this.ctxMenuDisabled.Name = "ctxMenuDisabled";
            resources.ApplyResources(this.ctxMenuDisabled, "ctxMenuDisabled");
            this.ctxMenuDisabled.CheckedChanged += new System.EventHandler(this.CtxMenu_CheckChanged);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            resources.ApplyResources(this.deleteToolStripMenuItem, "deleteToolStripMenuItem");
            // 
            // ACLEditorControl
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lv);
            this.Name = "ACLEditorControl";
            this.ctxMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lv;
        private System.Windows.Forms.ImageList imlIcons;
        private Tulpep.ActiveDirectoryObjectPicker.DirectoryObjectPickerDialog ADPicker;
        private System.Windows.Forms.ContextMenuStrip ctxMenu;
        private System.Windows.Forms.ToolStripMenuItem ctxMenuDeny;
        private System.Windows.Forms.ToolStripMenuItem ctxMenuDisabled;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
    }
}
