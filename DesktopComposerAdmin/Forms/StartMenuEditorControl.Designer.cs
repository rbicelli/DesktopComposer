namespace DesktopComposerAdmin.Forms
{
    partial class StartMenuEditorControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StartMenuEditorControl));
            this.tvStartMenu = new System.Windows.Forms.TreeView();
            this.ctxMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mniAddShortcut = new System.Windows.Forms.ToolStripMenuItem();
            this.mniAddMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.mniDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.mniRename = new System.Windows.Forms.ToolStripMenuItem();
            this.mniProperties = new System.Windows.Forms.ToolStripMenuItem();
            this.imlIcons = new System.Windows.Forms.ImageList(this.components);
            this.ctxMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // tvStartMenu
            // 
            this.tvStartMenu.AllowDrop = true;
            this.tvStartMenu.ContextMenuStrip = this.ctxMenu;
            this.tvStartMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvStartMenu.ImageIndex = 0;
            this.tvStartMenu.ImageList = this.imlIcons;
            this.tvStartMenu.LabelEdit = true;
            this.tvStartMenu.Location = new System.Drawing.Point(0, 0);
            this.tvStartMenu.Name = "tvStartMenu";
            this.tvStartMenu.SelectedImageIndex = 0;
            this.tvStartMenu.Size = new System.Drawing.Size(287, 180);
            this.tvStartMenu.TabIndex = 0;
            this.tvStartMenu.BeforeLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.NodeBeforeLabelEdit);
            this.tvStartMenu.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.tvNodeAfterLabelEdit);
            this.tvStartMenu.AfterCollapse += new System.Windows.Forms.TreeViewEventHandler(this.tvNodeExpandCollapse);
            this.tvStartMenu.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.tvNodeExpandCollapse);
            this.tvStartMenu.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.tvItemDrag);
            this.tvStartMenu.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvNodeAfterSelect);
            this.tvStartMenu.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.NodeMouseClick);
            this.tvStartMenu.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.NodeDoubleClick);
            this.tvStartMenu.DragDrop += new System.Windows.Forms.DragEventHandler(this.tvDragDrop);
            this.tvStartMenu.DragEnter += new System.Windows.Forms.DragEventHandler(this.tvDragEnter);
            this.tvStartMenu.DragOver += new System.Windows.Forms.DragEventHandler(this.tvDragOver);
            // 
            // ctxMenu
            // 
            this.ctxMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mniAddShortcut,
            this.mniAddMenu,
            this.mniDelete,
            this.mniRename,
            this.mniProperties});
            this.ctxMenu.Name = "ctxMenu";
            this.ctxMenu.Size = new System.Drawing.Size(148, 114);
            // 
            // mniAddShortcut
            // 
            this.mniAddShortcut.Name = "mniAddShortcut";
            this.mniAddShortcut.Size = new System.Drawing.Size(147, 22);
            this.mniAddShortcut.Text = "Add Shortcut";
            this.mniAddShortcut.Click += new System.EventHandler(this.mniContextClick);
            // 
            // mniAddMenu
            // 
            this.mniAddMenu.Name = "mniAddMenu";
            this.mniAddMenu.Size = new System.Drawing.Size(147, 22);
            this.mniAddMenu.Text = "Add Menu";
            this.mniAddMenu.Click += new System.EventHandler(this.mniContextClick);
            // 
            // mniDelete
            // 
            this.mniDelete.Name = "mniDelete";
            this.mniDelete.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.mniDelete.Size = new System.Drawing.Size(147, 22);
            this.mniDelete.Text = "Delete";
            this.mniDelete.Click += new System.EventHandler(this.mniContextClick);
            // 
            // mniRename
            // 
            this.mniRename.Name = "mniRename";
            this.mniRename.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.mniRename.Size = new System.Drawing.Size(147, 22);
            this.mniRename.Text = "Rename";
            this.mniRename.Click += new System.EventHandler(this.mniContextClick);
            // 
            // mniProperties
            // 
            this.mniProperties.Name = "mniProperties";
            this.mniProperties.Size = new System.Drawing.Size(147, 22);
            this.mniProperties.Text = "Properties";
            this.mniProperties.Click += new System.EventHandler(this.mniContextClick);
            // 
            // imlIcons
            // 
            this.imlIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imlIcons.ImageStream")));
            this.imlIcons.TransparentColor = System.Drawing.Color.Transparent;
            this.imlIcons.Images.SetKeyName(0, "START");
            // 
            // StartMenuEditorControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tvStartMenu);
            this.Name = "StartMenuEditorControl";
            this.Size = new System.Drawing.Size(287, 180);
            this.ctxMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ImageList imlIcons;
        private System.Windows.Forms.ContextMenuStrip ctxMenu;
        private System.Windows.Forms.ToolStripMenuItem mniAddShortcut;
        private System.Windows.Forms.ToolStripMenuItem mniAddMenu;
        private System.Windows.Forms.ToolStripMenuItem mniDelete;
        private System.Windows.Forms.ToolStripMenuItem mniProperties;
        private System.Windows.Forms.TreeView tvStartMenu;
        private System.Windows.Forms.ToolStripMenuItem mniRename;
    }
}
