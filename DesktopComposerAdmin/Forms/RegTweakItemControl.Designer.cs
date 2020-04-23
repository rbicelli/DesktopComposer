namespace DesktopComposerAdmin.Forms
{
    partial class RegTweakItemControl
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
            this.labelLongDescription = new System.Windows.Forms.Label();
            this.chkSettingEnabled = new System.Windows.Forms.CheckBox();
            this.panelOptions = new System.Windows.Forms.Panel();
            this.lblValue = new System.Windows.Forms.Label();
            this.textText = new System.Windows.Forms.TextBox();
            this.comboList = new System.Windows.Forms.ComboBox();
            this.lblTweakSeparator = new System.Windows.Forms.Label();
            this.panelOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelLongDescription
            // 
            this.labelLongDescription.AutoSize = true;
            this.labelLongDescription.Location = new System.Drawing.Point(25, 22);
            this.labelLongDescription.Name = "labelLongDescription";
            this.labelLongDescription.Size = new System.Drawing.Size(106, 13);
            this.labelLongDescription.TabIndex = 1;
            this.labelLongDescription.Text = "labelLongDescription";
            // 
            // chkSettingEnabled
            // 
            this.chkSettingEnabled.AutoSize = true;
            this.chkSettingEnabled.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkSettingEnabled.Location = new System.Drawing.Point(7, 4);
            this.chkSettingEnabled.Name = "chkSettingEnabled";
            this.chkSettingEnabled.Size = new System.Drawing.Size(148, 17);
            this.chkSettingEnabled.TabIndex = 0;
            this.chkSettingEnabled.Text = "CheckSettingEnabled";
            this.chkSettingEnabled.UseVisualStyleBackColor = true;
            this.chkSettingEnabled.CheckedChanged += new System.EventHandler(this.chkSettingEnabled_CheckedChanged);
            // 
            // panelOptions
            // 
            this.panelOptions.Controls.Add(this.lblValue);
            this.panelOptions.Controls.Add(this.textText);
            this.panelOptions.Controls.Add(this.comboList);
            this.panelOptions.Location = new System.Drawing.Point(539, 2);
            this.panelOptions.Name = "panelOptions";
            this.panelOptions.Size = new System.Drawing.Size(214, 48);
            this.panelOptions.TabIndex = 2;
            // 
            // lblValue
            // 
            this.lblValue.AutoSize = true;
            this.lblValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblValue.Location = new System.Drawing.Point(3, 4);
            this.lblValue.Name = "lblValue";
            this.lblValue.Size = new System.Drawing.Size(39, 13);
            this.lblValue.TabIndex = 0;
            this.lblValue.Text = "Value";
            // 
            // textText
            // 
            this.textText.Location = new System.Drawing.Point(11, 20);
            this.textText.Name = "textText";
            this.textText.Size = new System.Drawing.Size(165, 20);
            this.textText.TabIndex = 1;
            this.textText.Visible = false;
            this.textText.Validating += new System.ComponentModel.CancelEventHandler(this.textText_OnValidating);
            this.textText.Validated += new System.EventHandler(this.textText_OnValidated);
            // 
            // comboList
            // 
            this.comboList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboList.FormattingEnabled = true;
            this.comboList.Location = new System.Drawing.Point(10, 19);
            this.comboList.Name = "comboList";
            this.comboList.Size = new System.Drawing.Size(188, 21);
            this.comboList.TabIndex = 2;
            this.comboList.Visible = false;
            this.comboList.SelectedIndexChanged += new System.EventHandler(this.comboList_SelectedIndexChanged);
            // 
            // lblTweakSeparator
            // 
            this.lblTweakSeparator.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblTweakSeparator.Location = new System.Drawing.Point(4, 50);
            this.lblTweakSeparator.Name = "lblTweakSeparator";
            this.lblTweakSeparator.Size = new System.Drawing.Size(106, 2);
            this.lblTweakSeparator.TabIndex = 8;
            // 
            // RegTweakItemControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblTweakSeparator);
            this.Controls.Add(this.panelOptions);
            this.Controls.Add(this.chkSettingEnabled);
            this.Controls.Add(this.labelLongDescription);
            this.Name = "RegTweakItemControl";
            this.Size = new System.Drawing.Size(756, 51);
            this.Load += new System.EventHandler(this.RegTweakItemControl_Load);
            this.Resize += new System.EventHandler(this.Control_OnResize);
            this.panelOptions.ResumeLayout(false);
            this.panelOptions.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label labelLongDescription;
        private System.Windows.Forms.CheckBox chkSettingEnabled;
        private System.Windows.Forms.Panel panelOptions;
        private System.Windows.Forms.Label lblValue;
        private System.Windows.Forms.TextBox textText;
        private System.Windows.Forms.ComboBox comboList;
        private System.Windows.Forms.Label lblTweakSeparator;
    }
}
