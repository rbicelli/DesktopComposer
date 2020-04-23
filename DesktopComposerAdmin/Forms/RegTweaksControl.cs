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
    public partial class RegTweaksControl : UserControl
    {
        private RegTweakDefinitions _regTweakDefs;        
        public RegTweaksControl()
        {
            InitializeComponent();
        }

        [Browsable(true)]
        [Category("Action")]
        [Description("Invoked when data is changed by user")]
        public event EventHandler DataChangedByUser;

        public RegTweakDefinitions RegTweakDefinitions
        {
            get {
                return _regTweakDefs;
            }
            set
            {
                _regTweakDefs = value;
                if (_regTweakDefs!=null) Render();
            }
        }

        public void Render()
        {            
            flPanel.Controls.Clear();
            foreach (RegTweakDefinition rtd in _regTweakDefs)
            {
                RegTweakItemControl rti = new RegTweakItemControl();
                rti.RegTweakDefinition = rtd;
                rti.DataChangedByUser += new EventHandler(TweakItemControls_Datachanged);
                flPanel.Controls.Add(rti);
                flPanel.SetFlowBreak(rti, true);
            }
        }

        private void TweakItemControls_Datachanged(object sender, EventArgs e)
        {
            InvokeDataChanged();
        }

        private void Control_OnResize(object sender, EventArgs e)
        {
            foreach(Control c in flPanel.Controls)
            {
                RegTweakItemControl t = (RegTweakItemControl)c;
                t.TriggerResize();                
            }
        }

        private void InvokeDataChanged()
        {
            DataChangedByUser?.Invoke(this, new EventArgs());
        }
    }
}
