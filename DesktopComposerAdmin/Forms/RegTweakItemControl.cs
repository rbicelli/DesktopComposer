using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DesktopComposer.Implementation;

namespace DesktopComposerAdmin.Forms
{
    public partial class RegTweakItemControl : UserControl
    {
        private RegTweakDefinition _regTweakDef;
        private bool _initializing;

        [Browsable(true)]
        [Category("Action")]
        [Description("Invoked when data is changed by user")]
        public event EventHandler DataChangedByUser;
        public RegTweakItemControl()
        {
            InitializeComponent();            
        }

        public RegTweakDefinition RegTweakDefinition
        {
            get
            {
                return _regTweakDef;
            }
            set {
                _regTweakDef = value;
                Render();
            }
        }

        public void Render()
        {
            _initializing = true;

            chkSettingEnabled.Text = _regTweakDef.Description;
            labelLongDescription.Text = _regTweakDef.LongDescription;
            
            switch (_regTweakDef.DisplayType)
            {
                case RegTweakDisplayType.Bool:
                    panelOptions.Visible = false;
                    break;
                
                case RegTweakDisplayType.List:
                    panelOptions.Visible = true;
                    comboList.Visible = true;
                    RenderCombo();
                    //Populate Combox
                    break;
                
                case RegTweakDisplayType.Text:
                    panelOptions.Visible = true;
                    textText.Visible = true;
                    textText.Width = comboList.Width;
                    if (_regTweakDef.LinkedRegTweak != null)
                        textText.Text = _regTweakDef.LinkedRegTweak.ValueEnabled;
                    break;

            }

            if (_regTweakDef.LinkedRegTweak!=null)
                chkSettingEnabled.Checked = _regTweakDef.LinkedRegTweak.Enabled;

            _initializing = false;
        }

        private void RenderCombo()
        {
            
            Dictionary<string, string> d = _regTweakDef.ValueMap.ToDictionary(x=>x.Key,x=>x.Value);
            comboList.DataSource = new BindingSource(_regTweakDef.ValueMap,null);
            comboList.DisplayMember = "Value";
            comboList.ValueMember = "Key";                        

            if  ( (_regTweakDef.ValueMap != null) & (_regTweakDef.LinkedRegTweak.ValueEnabled!=null) )
            {
                comboList.SelectedValue = _regTweakDef.LinkedRegTweak.ValueEnabled;
            } 
        }
        private void RegTweakItemControl_Load(object sender, EventArgs e)
        {
            TriggerResize();
            Application.DoEvents();
        }

        public void TriggerResize()
        {
            Width = Parent.Width;
        }

        private void Control_OnResize(object sender, EventArgs e)
        {
            panelOptions.Left = Width - panelOptions.Width - 10;
            if (panelOptions.Visible)
                labelLongDescription.MaximumSize = new Size (( panelOptions.Left - labelLongDescription.Left -5), 0);
            lblTweakSeparator.Width = Width - lblTweakSeparator.Left;
        }

        private void chkSettingEnabled_CheckedChanged(object sender, EventArgs e)
        {
            if (_initializing == false)
            {                
                _regTweakDef.LinkedRegTweak.Enabled = chkSettingEnabled.Checked;
                InvokeDataChanged();
            }
        }

        private void comboList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((comboList.SelectedValue != null) & (_initializing==false))
                _regTweakDef.LinkedRegTweak.ValueEnabled = ((KeyValuePair<string, string>)comboList.SelectedItem).Key;
            InvokeDataChanged();
        }

        private void textText_OnValidating(object sender, CancelEventArgs e)
        {
            long longValue = 0;
            int intValue = 0;
            
            switch (_regTweakDef.ValueType.ToUpper())
            {
                case "DWORD":
                    if (int.TryParse(textText.Text, out intValue) == false)
                        e.Cancel = true;
                    break;
                case "QWORD":
                    if (long.TryParse(textText.Text, out longValue) == false)
                        e.Cancel = true;
                    break;
            }
            
        }

        private void textText_OnValidated(object sender, EventArgs e)
        {
            _regTweakDef.LinkedRegTweak.ValueEnabled = textText.Text;
            InvokeDataChanged();
        }

        private void InvokeDataChanged()
        {
            DataChangedByUser?.Invoke(this, new EventArgs());
        }
    }
}
