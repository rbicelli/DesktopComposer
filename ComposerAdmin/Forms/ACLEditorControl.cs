using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DesktopComposer.Implementation;
using Tulpep.ActiveDirectoryObjectPicker;
using System.Collections;
using System.Security.Principal;
using static System.Windows.Forms.ListView;

namespace ComposerAdmin.Forms
{
    public partial class ACLEditorControl : UserControl
    {
        private ACLs _ACLs;
        
        [Browsable(true)]
        [Category("Action")]
        [Description("Invoked when Item Selection changes")]
        public event System.EventHandler ItemSelectionChanged;
        public ACLs ACLs
        {
            get
            {
                return _ACLs;
            }
            set
            {
                _ACLs = value;
                Render();
            }
        }

        // Workaround for delete/edit
        public int SelectedItemCount
        {
            get => lv.SelectedItems.Count;            
        }
        public ACLEditorControl()
        {            
            InitializeComponent();                        
        }

        public void Render()
        {
            ListViewItem li;
            if (_ACLs != null) { 
                foreach (ACL acl in _ACLs)
                {
                    if (!lv.Items.ContainsKey(acl.ObjectID.ToString())) { 
                         li = lv.Items.Add(acl.ObjectID.ToString(), acl.ObjectShortName,0);
                         li.Tag = acl;
                         RefreshListItem(li);
                    }
                }
            }
        }

        private void RefreshListItem(ListViewItem Litem)
        {
            ACL acl = (ACL)Litem.Tag;
            //Icon
            Litem.Text = acl.ObjectShortName;
            int imIndex=0;
            
            if (acl.ObjectType == ACLObjectType.User)
                imIndex = 0;
            else
                imIndex = 1;

            if (acl.Disabled)
            {
                imIndex += 4;
            } else { 
                if (acl.ACLType == ACLType.Deny)
                    imIndex += 2;
            }

            Litem.ImageIndex = imIndex;
        }
               
        private string SIDBytesToString(byte[] bytes)
        {
            try
            {
                var sid = new SecurityIdentifier(bytes, 0);
                return sid.ToString();
            }
            // ReSharper disable once EmptyGeneralCatchClause
            catch (Exception)
            {
            }

            return BytesToString(bytes);
        }

        private string BytesToString(byte[] bytes)
        {
            return "0x" + BitConverter.ToString(bytes).Replace('-', ' ');
        }

        public void ACLDelete()
        {
            ACL SelACL;
            foreach (ListViewItem i in lv.SelectedItems){
                SelACL = (ACL)i.Tag;
                ACLs.Remove(SelACL);
                i.Remove();
            }
        }

        public void OpenADPicker()
        {
            ACL addACL;
            int i, j;
            
            string objectSID;
            string objectName;
            string objectUPN;
            ACLObjectType objectType;
            string objectPath;
            objectType = ACLObjectType.User;


            ADPicker.AllowedObjectTypes = ObjectTypes.Users | ObjectTypes.Groups;
            ADPicker.DefaultObjectTypes = ObjectTypes.Users | ObjectTypes.Groups;
            ADPicker.AllowedLocations = Locations.All;
            ADPicker.DefaultLocations = Locations.LocalComputer;
            //ADPicker.DefaultLocations = Locations.JoinedDomain;
            ADPicker.MultiSelect = true;
            ADPicker.ShowAdvancedView = true;
            
            if (ADPicker.ShowDialog(this) == DialogResult.OK)
            {                
                DirectoryObject[] results = ADPicker.SelectedObjects;
                

                for (i=0; i < results.Length; i++)
                {
                    objectName = results[i].Name;
                    objectUPN = results[i].Upn;
                    objectSID = "";                    
                    objectPath = results[i].Path;
                    
                    //Object Type
                    switch (results[i].SchemaClassName.ToLower())
                    {
                        case "user":
                            objectType = ACLObjectType.User;
                            break;

                        case "group":
                            objectType = ACLObjectType.Group;
                            break;
                    }

                    //Fetch Attributes
                    for (j = 0; j < results[i].FetchedAttributes.Length; j++) {
                            
                        var attributeName = ADPicker.AttributesToFetch[j];
                        Console.WriteLine("Found Attribute Name: {0}", attributeName);
                        
                        var multivaluedAttribute = results[i].FetchedAttributes[j];
                            
                        if (!(multivaluedAttribute is IEnumerable) || multivaluedAttribute is byte[] || multivaluedAttribute is string)
                            multivaluedAttribute = new[] { multivaluedAttribute };
                            
                        foreach (var attribute in (IEnumerable)multivaluedAttribute)
                        {
                            if (attributeName.Equals("objectSid", StringComparison.OrdinalIgnoreCase))
                            objectSID = SIDBytesToString((byte[])attribute);
                        }
                    }

                    //Check if ACL Exists
                    if (ACLs[objectSID] == null)
                    {
                        addACL = new ACL() {
                            SID = objectSID,
                            ObjectName = objectName,
                            ObjectPath = objectPath,
                            ObjectType = objectType,
                            ObjectID = Guid.NewGuid()
                        };
                        _ACLs.Add(addACL);
                        Render();
                    }
                    

                }
            }            
        }

        private void lv_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {            
            ItemSelectionChanged(sender, e);
        }

        private void DenySelected(bool value)
        {
            ACL acl;
            foreach (ListViewItem li in lv.SelectedItems)
            {
                acl = (ACL)li.Tag;
                if (value)
                    acl.ACLType = ACLType.Deny;                
                else
                    acl.ACLType = ACLType.Allow;

                RefreshListItem(li);
            }
        }

        private void DisableSelected(bool value)
        {
            ACL acl;
            foreach (ListViewItem li in lv.SelectedItems)
            {
                acl = (ACL)li.Tag;
                acl.Disabled = value;
                RefreshListItem(li);
            }
        }

        private void CtxMenu_CheckChanged(object sender, EventArgs e)
        {
            ToolStripMenuItem mnuI = (ToolStripMenuItem)sender;
            switch (mnuI.Name)
            {
                case "ctxMenuDeny":
                    DenySelected(mnuI.Checked);
                    break;

                case "ctxMenuDisabled":
                    DisableSelected(mnuI.Checked);
                    break;
            }
        }

        private void ctxMenu_Open(object sender, CancelEventArgs e)
        {
            if (lv.SelectedItems.Count == 1)
            {
                ACL acl = (ACL)lv.SelectedItems[0].Tag;
                ctxMenuDisabled.Checked = acl.Disabled;
                ctxMenuDeny.Checked = (acl.ACLType == ACLType.Allow) ? false : true;                                
            } else
            {
                ctxMenuDeny.Checked = false;
                ctxMenuDisabled.Checked = false;
            }
        }
    }
}
