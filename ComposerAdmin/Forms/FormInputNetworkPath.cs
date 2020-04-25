using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ComposerAdmin.Properties;

namespace ComposerAdmin.Forms
{
    public partial class FormInputNetworkPath : Form
    {
        string _networkPath;
        public FormInputNetworkPath()
        {
            _networkPath = null;
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            //Check if Network Path For Stert Menu Exists
            string lPath = @"\\" + TextComputerName.Text + @"\C$\ProgramData\Microsoft\Windows\Start Menu";
            if (Directory.Exists(lPath))
            {
                _networkPath = lPath;
                this.Close();
            } else
            {
                MessageBox.Show(Resources.MESSAGE_PATH_NOT_FOUND, Resources.MESSAGE_PATH_NOT_FOUND, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }
        public string ShowDialogNetworkPath()
        {            
            this.ShowDialog();
            return _networkPath;
        }
    }

   
}
