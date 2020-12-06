using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DesktopComposer.Implementation;
using ComposerAdmin.Properties;
using System.Drawing;
using DesktopComposer;

namespace ComposerAdmin.Forms
{
    public partial class FormShortcutProperties : Form
    {
        private DesktopComposer.Implementation.Shortcut _shortcut;
        private ACLs _acls;
        private Icon _tempIcon;
        private string _tempIconLocation;
        public FormShortcutProperties()
        {
            InitializeComponent();
            aclEditor.ItemSelectionChanged += new EventHandler(aclEditor_ItemSelectionChanged);

            btnACLDelete.Enabled = false;
            Dictionary<string, string> WinModes = new Dictionary<string, string>();
            WinModes.Add("1", Resources.WINDOWMODE_NORMAL);
            WinModes.Add("3", Resources.WINDOWMODE_MINIMIZED);
            WinModes.Add("7", Resources.WINDOWMODE_MAXIMIZED);
            comboWinMode.DataSource = new BindingSource(WinModes, null);
            comboWinMode.DisplayMember = "Value";
            comboWinMode.ValueMember = "Key";
        }

        private void aclEditor_ItemSelectionChanged(object sender, EventArgs e)
        {
            if (aclEditor.SelectedItemCount > 0)
                btnACLDelete.Enabled = true;
            else
                btnACLDelete.Enabled = false;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void FormShortcutProperties_Load(object sender, EventArgs e)
        {

        }
        public DialogResult ShowDialog(DesktopComposer.Implementation.Shortcut shortcut)
        {
            if (shortcut == null) return DialogResult.Cancel;

            _shortcut = shortcut;
            _acls = (ACLs)shortcut.ACLs.Clone();

            textDisplayName.Text = _shortcut.DisplayName;
            textTarget.Text = _shortcut.TargetPath;
            textArguments.Text = _shortcut.Arguments;
            textHotkey.Text = _shortcut.Hotkey;
            textWorkingDirectory.Text = _shortcut.WorkingDirectory;
            aclEditor.ACLs = _acls;

            chkPinOnTaskBar.Checked = _shortcut.PutOnTaskBar;
            chkPutOnDesktop.Checked = _shortcut.PutOnDesktop;
            chkPutOnStartMenu.Checked = _shortcut.PutOnStartMenu;
            chkDeployDisabled.Checked = _shortcut.DeployDisabled;
            chkAclDenyByDefault.Checked = _shortcut.ACLDenyByDefault;
            chkCheckIfTargetExists.Checked = _shortcut.CheckIfTargetExists;

            comboWinMode.SelectedValue = _shortcut.WindowStyle.ToString();                       
                        
            if (_shortcut.IconCacheLarge != null)
                pBIcon.Image = _shortcut.IconCacheLarge.ToBitmap();

            _tempIcon = null;

            return this.ShowDialog();
        }
        

        private void BtnACLAdd_Click(object sender, EventArgs e)
        {
            aclEditor.OpenADPicker();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {                        
            _shortcut.DisplayName=(textDisplayName.Text);            
            _shortcut.TargetPath = textTarget.Text;
            _shortcut.Arguments = textArguments.Text;
            _shortcut.Hotkey = textHotkey.Text;
            _shortcut.WorkingDirectory = textWorkingDirectory.Text;
            _shortcut.PutOnStartMenu = chkPutOnStartMenu.Checked;
            _shortcut.PutOnTaskBar = chkPinOnTaskBar.Checked;
            _shortcut.PutOnDesktop = chkPutOnDesktop.Checked;
            _shortcut.DeployDisabled = chkDeployDisabled.Checked;
            _shortcut.ACLDenyByDefault = chkAclDenyByDefault.Checked;
            _shortcut.WindowStyle = int.Parse(comboWinMode.SelectedValue.ToString());
            _shortcut.CheckIfTargetExists = chkCheckIfTargetExists.Checked;
            _shortcut.ACLs = _acls;

            if (_tempIconLocation != null) {
                _shortcut.IconLocation = _tempIconLocation;
                _shortcut.FindIcon();
            }

            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnACLDelete_Click(object sender, EventArgs e)
        {
            aclEditor.ACLDelete();
        }

        private void btnChangeIcon_Click(object sender, EventArgs e)
        {            
            DialogResult result = iconPickerDialog.ShowDialog(this);
            if (result == DialogResult.OK)
            {                
                _tempIconLocation = iconPickerDialog.FileName + "," + iconPickerDialog.IconIndex.ToString();
                _tempIcon = ShellIcon.IconExtract(iconPickerDialog.FileName, iconPickerDialog.IconIndex, 1);

                _shortcut.FindIcon();
                if (_shortcut.IconCacheLarge != null)
                    pBIcon.Image = _tempIcon.ToBitmap();

            }
            iconPickerDialog.Dispose();
        }
    }
}
