namespace ComposerAdmin.Forms
{
    partial class FormShortcutProperties
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormShortcutProperties));
            this.tabCMain = new System.Windows.Forms.TabControl();
            this.tabPGeneral = new System.Windows.Forms.TabPage();
            this.chkCheckIfTargetExists = new System.Windows.Forms.CheckBox();
            this.chkDeployDisabled = new System.Windows.Forms.CheckBox();
            this.chkPutOnStartMenu = new System.Windows.Forms.CheckBox();
            this.chkPinOnTaskBar = new System.Windows.Forms.CheckBox();
            this.chkPutOnDesktop = new System.Windows.Forms.CheckBox();
            this.btnChangeIcon = new System.Windows.Forms.Button();
            this.textArguments = new System.Windows.Forms.TextBox();
            this.lbArgs = new System.Windows.Forms.Label();
            this.comboWinMode = new System.Windows.Forms.ComboBox();
            this.lbWindowMode = new System.Windows.Forms.Label();
            this.textHotkey = new System.Windows.Forms.TextBox();
            this.lbShortcutKeys = new System.Windows.Forms.Label();
            this.textWorkingDirectory = new System.Windows.Forms.TextBox();
            this.lbWorkingDirectory = new System.Windows.Forms.Label();
            this.textTarget = new System.Windows.Forms.TextBox();
            this.lbTarget = new System.Windows.Forms.Label();
            this.textDisplayName = new System.Windows.Forms.TextBox();
            this.lbShortcutDisplayName = new System.Windows.Forms.Label();
            this.pBIcon = new System.Windows.Forms.PictureBox();
            this.tabPACL = new System.Windows.Forms.TabPage();
            this.chkAclDenyByDefault = new System.Windows.Forms.CheckBox();
            this.btnACLDelete = new System.Windows.Forms.Button();
            this.BtnACLAdd = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.aclEditor = new ComposerAdmin.Forms.ACLEditorControl();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.imlIcons = new System.Windows.Forms.ImageList(this.components);
            this.tabCMain.SuspendLayout();
            this.tabPGeneral.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pBIcon)).BeginInit();
            this.tabPACL.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabCMain
            // 
            this.tabCMain.Controls.Add(this.tabPGeneral);
            this.tabCMain.Controls.Add(this.tabPACL);
            resources.ApplyResources(this.tabCMain, "tabCMain");
            this.tabCMain.Name = "tabCMain";
            this.tabCMain.SelectedIndex = 0;
            // 
            // tabPGeneral
            // 
            this.tabPGeneral.Controls.Add(this.chkCheckIfTargetExists);
            this.tabPGeneral.Controls.Add(this.chkDeployDisabled);
            this.tabPGeneral.Controls.Add(this.chkPutOnStartMenu);
            this.tabPGeneral.Controls.Add(this.chkPinOnTaskBar);
            this.tabPGeneral.Controls.Add(this.chkPutOnDesktop);
            this.tabPGeneral.Controls.Add(this.btnChangeIcon);
            this.tabPGeneral.Controls.Add(this.textArguments);
            this.tabPGeneral.Controls.Add(this.lbArgs);
            this.tabPGeneral.Controls.Add(this.comboWinMode);
            this.tabPGeneral.Controls.Add(this.lbWindowMode);
            this.tabPGeneral.Controls.Add(this.textHotkey);
            this.tabPGeneral.Controls.Add(this.lbShortcutKeys);
            this.tabPGeneral.Controls.Add(this.textWorkingDirectory);
            this.tabPGeneral.Controls.Add(this.lbWorkingDirectory);
            this.tabPGeneral.Controls.Add(this.textTarget);
            this.tabPGeneral.Controls.Add(this.lbTarget);
            this.tabPGeneral.Controls.Add(this.textDisplayName);
            this.tabPGeneral.Controls.Add(this.lbShortcutDisplayName);
            this.tabPGeneral.Controls.Add(this.pBIcon);
            resources.ApplyResources(this.tabPGeneral, "tabPGeneral");
            this.tabPGeneral.Name = "tabPGeneral";
            this.tabPGeneral.UseVisualStyleBackColor = true;
            // 
            // chkCheckIfTargetExists
            // 
            resources.ApplyResources(this.chkCheckIfTargetExists, "chkCheckIfTargetExists");
            this.chkCheckIfTargetExists.Name = "chkCheckIfTargetExists";
            this.chkCheckIfTargetExists.UseVisualStyleBackColor = true;
            // 
            // chkDeployDisabled
            // 
            resources.ApplyResources(this.chkDeployDisabled, "chkDeployDisabled");
            this.chkDeployDisabled.Name = "chkDeployDisabled";
            this.chkDeployDisabled.UseVisualStyleBackColor = true;
            // 
            // chkPutOnStartMenu
            // 
            resources.ApplyResources(this.chkPutOnStartMenu, "chkPutOnStartMenu");
            this.chkPutOnStartMenu.Name = "chkPutOnStartMenu";
            this.chkPutOnStartMenu.UseVisualStyleBackColor = true;
            // 
            // chkPinOnTaskBar
            // 
            resources.ApplyResources(this.chkPinOnTaskBar, "chkPinOnTaskBar");
            this.chkPinOnTaskBar.Name = "chkPinOnTaskBar";
            this.chkPinOnTaskBar.UseVisualStyleBackColor = true;
            // 
            // chkPutOnDesktop
            // 
            resources.ApplyResources(this.chkPutOnDesktop, "chkPutOnDesktop");
            this.chkPutOnDesktop.Name = "chkPutOnDesktop";
            this.chkPutOnDesktop.UseVisualStyleBackColor = true;
            // 
            // btnChangeIcon
            // 
            resources.ApplyResources(this.btnChangeIcon, "btnChangeIcon");
            this.btnChangeIcon.Name = "btnChangeIcon";
            this.btnChangeIcon.UseVisualStyleBackColor = true;
            // 
            // textArguments
            // 
            resources.ApplyResources(this.textArguments, "textArguments");
            this.textArguments.Name = "textArguments";
            // 
            // lbArgs
            // 
            resources.ApplyResources(this.lbArgs, "lbArgs");
            this.lbArgs.Name = "lbArgs";
            // 
            // comboWinMode
            // 
            this.comboWinMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboWinMode.FormattingEnabled = true;
            resources.ApplyResources(this.comboWinMode, "comboWinMode");
            this.comboWinMode.Name = "comboWinMode";
            // 
            // lbWindowMode
            // 
            resources.ApplyResources(this.lbWindowMode, "lbWindowMode");
            this.lbWindowMode.Name = "lbWindowMode";
            this.lbWindowMode.Click += new System.EventHandler(this.label3_Click);
            // 
            // textHotkey
            // 
            resources.ApplyResources(this.textHotkey, "textHotkey");
            this.textHotkey.Name = "textHotkey";
            // 
            // lbShortcutKeys
            // 
            resources.ApplyResources(this.lbShortcutKeys, "lbShortcutKeys");
            this.lbShortcutKeys.Name = "lbShortcutKeys";
            // 
            // textWorkingDirectory
            // 
            resources.ApplyResources(this.textWorkingDirectory, "textWorkingDirectory");
            this.textWorkingDirectory.Name = "textWorkingDirectory";
            // 
            // lbWorkingDirectory
            // 
            resources.ApplyResources(this.lbWorkingDirectory, "lbWorkingDirectory");
            this.lbWorkingDirectory.Name = "lbWorkingDirectory";
            // 
            // textTarget
            // 
            resources.ApplyResources(this.textTarget, "textTarget");
            this.textTarget.Name = "textTarget";
            // 
            // lbTarget
            // 
            resources.ApplyResources(this.lbTarget, "lbTarget");
            this.lbTarget.Name = "lbTarget";
            // 
            // textDisplayName
            // 
            resources.ApplyResources(this.textDisplayName, "textDisplayName");
            this.textDisplayName.Name = "textDisplayName";
            // 
            // lbShortcutDisplayName
            // 
            resources.ApplyResources(this.lbShortcutDisplayName, "lbShortcutDisplayName");
            this.lbShortcutDisplayName.Name = "lbShortcutDisplayName";
            // 
            // pBIcon
            // 
            resources.ApplyResources(this.pBIcon, "pBIcon");
            this.pBIcon.Name = "pBIcon";
            this.pBIcon.TabStop = false;
            // 
            // tabPACL
            // 
            this.tabPACL.Controls.Add(this.chkAclDenyByDefault);
            this.tabPACL.Controls.Add(this.btnACLDelete);
            this.tabPACL.Controls.Add(this.BtnACLAdd);
            this.tabPACL.Controls.Add(this.label4);
            this.tabPACL.Controls.Add(this.aclEditor);
            resources.ApplyResources(this.tabPACL, "tabPACL");
            this.tabPACL.Name = "tabPACL";
            this.tabPACL.UseVisualStyleBackColor = true;
            // 
            // chkAclDenyByDefault
            // 
            resources.ApplyResources(this.chkAclDenyByDefault, "chkAclDenyByDefault");
            this.chkAclDenyByDefault.Name = "chkAclDenyByDefault";
            this.chkAclDenyByDefault.UseVisualStyleBackColor = true;
            // 
            // btnACLDelete
            // 
            resources.ApplyResources(this.btnACLDelete, "btnACLDelete");
            this.btnACLDelete.Name = "btnACLDelete";
            this.btnACLDelete.UseVisualStyleBackColor = true;
            this.btnACLDelete.Click += new System.EventHandler(this.btnACLDelete_Click);
            // 
            // BtnACLAdd
            // 
            resources.ApplyResources(this.BtnACLAdd, "BtnACLAdd");
            this.BtnACLAdd.Name = "BtnACLAdd";
            this.BtnACLAdd.UseVisualStyleBackColor = true;
            this.BtnACLAdd.Click += new System.EventHandler(this.BtnACLAdd_Click);
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // aclEditor
            // 
            this.aclEditor.ACLs = null;
            resources.ApplyResources(this.aclEditor, "aclEditor");
            this.aclEditor.Name = "aclEditor";
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            resources.ApplyResources(this.btnOK, "btnOK");
            this.btnOK.Name = "btnOK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // imlIcons
            // 
            this.imlIcons.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
            resources.ApplyResources(this.imlIcons, "imlIcons");
            this.imlIcons.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // FormShortcutProperties
            // 
            this.AcceptButton = this.btnOK;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.tabCMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormShortcutProperties";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Load += new System.EventHandler(this.FormShortcutProperties_Load);
            this.tabCMain.ResumeLayout(false);
            this.tabPGeneral.ResumeLayout(false);
            this.tabPGeneral.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pBIcon)).EndInit();
            this.tabPACL.ResumeLayout(false);
            this.tabPACL.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabCMain;
        private System.Windows.Forms.TabPage tabPGeneral;
        private System.Windows.Forms.TabPage tabPACL;
        private System.Windows.Forms.ComboBox comboWinMode;
        private System.Windows.Forms.Label lbWindowMode;
        private System.Windows.Forms.TextBox textHotkey;
        private System.Windows.Forms.Label lbShortcutKeys;
        private System.Windows.Forms.TextBox textWorkingDirectory;
        private System.Windows.Forms.Label lbWorkingDirectory;
        private System.Windows.Forms.TextBox textTarget;
        private System.Windows.Forms.Label lbTarget;
        private System.Windows.Forms.TextBox textDisplayName;
        private System.Windows.Forms.Label lbShortcutDisplayName;
        private System.Windows.Forms.PictureBox pBIcon;
        private System.Windows.Forms.Button btnACLDelete;
        private System.Windows.Forms.Button BtnACLAdd;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TextBox textArguments;
        private System.Windows.Forms.Label lbArgs;
        private System.Windows.Forms.ImageList imlIcons;
        private ACLEditorControl aclEditor;
        private System.Windows.Forms.Button btnChangeIcon;
        private System.Windows.Forms.CheckBox chkAclDenyByDefault;
        private System.Windows.Forms.CheckBox chkCheckIfTargetExists;
        private System.Windows.Forms.CheckBox chkDeployDisabled;
        private System.Windows.Forms.CheckBox chkPutOnStartMenu;
        private System.Windows.Forms.CheckBox chkPinOnTaskBar;
        private System.Windows.Forms.CheckBox chkPutOnDesktop;
    }
}