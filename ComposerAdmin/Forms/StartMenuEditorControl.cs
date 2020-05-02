using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DesktopComposer;
using DesktopComposer.Implementation;
using System.IO;

namespace ComposerAdmin.Forms
{
    public partial class StartMenuEditorControl : UserControl
    {
        private Shortcuts _Shortcuts;
        private FormShortcutProperties _shorcutProperties;
        public Shortcuts Shortcuts {
            get {
                return _Shortcuts;
            }
            set {
                _Shortcuts = value;
                Render();
            }
        }

        [Browsable(true)]
        [Category("Action")]
        [Description("Invoked when data is changed by user")]
        public event EventHandler DataChangedByUser;

        public StartMenuEditorControl()
        {
            InitializeComponent();

            //Add Shell Icons
            imlIcons.Images.Add("APPLICATION_DEFAULT", ShellIcon.GetSystemIcon(ShellIcon.SHSTOCKICONID.SIID_APPLICATION, ShellIcon.SHGSI.SHGSI_ICON | ShellIcon.SHGSI.SHGSI_SMALLICON));
            imlIcons.Images.Add("FOLDER_CLOSED", ShellIcon.GetSystemIcon(ShellIcon.SHSTOCKICONID.SIID_FOLDER, ShellIcon.SHGSI.SHGSI_ICON | ShellIcon.SHGSI.SHGSI_SMALLICON));
            imlIcons.Images.Add("FOLDER_OPEN", ShellIcon.GetSystemIcon(ShellIcon.SHSTOCKICONID.SIID_FOLDEROPEN, ShellIcon.SHGSI.SHGSI_ICON | ShellIcon.SHGSI.SHGSI_SMALLICON));
        }

        /// <summary>
        /// Bind Composition Object to treeview
        /// </summary>
        private void Render(bool Clear = true) {
            if (Clear) tvStartMenu.Nodes.Clear();
            TreeNode lNode;

            //Create Main Node
            lNode = tvStartMenu.Nodes.Add("\\", "Start");
            lNode.ImageKey = "START";
            lNode.SelectedImageKey = "START";            

            if (_Shortcuts != null)
            {
                foreach (DesktopComposer.Implementation.Shortcut s in _Shortcuts)
                {
                    //1. Find Relative Path
                    lNode = AddNode(s);
                }
            }
            else
            {

            }
        }

        private bool NodeIsFolder(TreeNode n)
        {
            if (n != null)
            {
                if (n.Tag == null)
                    return true;             
            }
            return false;
        }

        private bool NodeIsStart(TreeNode n)
        {
            if (n.Index == 0 & n.Level == 0)
                return true;
            else
                return false;
        }

        private TreeNode FindNodeFromPath(string Path, TreeNodeCollection Nodes, bool onlyFolders=false) {
            TreeNode lastNode=null;
            foreach (TreeNode n in Nodes)
            {
                if (n.FullPath == Path)
                {
                    if (onlyFolders)
                    {
                        if (NodeIsFolder(n))
                        {                           
                            return n;
                        }
                    }
                    else
                    { 
                        return n;
                    }
                }
                if (n.Nodes.Count > 0)
                {
                    lastNode = FindNodeFromPath(Path, n.Nodes, onlyFolders);
                }
            }
            return lastNode;
        }

        private TreeNode AddNode(DesktopComposer.Implementation.Shortcut shortcut)
        {
            TreeNode lastNode = null;
            TreeNode foundNode = null;
            string subPathAgg;
            char pathSeparator = '\\';
            bool createNode;

            subPathAgg = "Start";
            
            //Build Path
            foreach (string subPath in shortcut.MenuPath.Split(pathSeparator))
            {
                createNode = false;
                subPathAgg += subPath;
                
                foundNode = FindNodeFromPath(subPathAgg, tvStartMenu.Nodes, true);

                if (foundNode != null)
                {
                    if (NodeIsFolder(foundNode))
                        lastNode = foundNode;
                    else
                        createNode = true;                                         
                } else
                {
                    createNode = true;
                }
                
                if (subPath != "" && createNode)
                {                    
                    if (lastNode == null)
                        lastNode = tvStartMenu.Nodes.Add(subPath);
                    else
                        lastNode = lastNode.Nodes.Add(subPath);

                    lastNode.ImageKey = "FOLDER_CLOSED";
                    lastNode.SelectedImageKey = "FOLDER_CLOSED";
                }

                subPathAgg += pathSeparator;
            }
            //Set Shortcut to node Tag            
            lastNode = lastNode.Nodes.Add(shortcut.DisplayName);
            lastNode.Tag = shortcut;

            if (shortcut.IconCacheSmall != null)
            {
                if (!imlIcons.Images.ContainsKey(shortcut.ObjectID.ToString())) {
                    imlIcons.Images.Add(shortcut.ObjectID.ToString(), shortcut.IconCacheSmall);
                }
                lastNode.ImageKey = shortcut.ObjectID.ToString();
                lastNode.SelectedImageKey = lastNode.ImageKey;
            }
            else
            {
                lastNode.ImageKey = "APPLICATION_DEFAULT";
                lastNode.SelectedImageKey = "APPLICATION_DEFAULT";
                //Default Icon    
            }
            return lastNode;
        }

        private void NodeDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeView tv = (TreeView)sender;
            TreeNode node =e.Node;
            if (node != null) {
                DesktopComposer.Implementation.Shortcut shortcut = (DesktopComposer.Implementation.Shortcut)node.Tag;
                if (shortcut != null)
                {
                    ShortcutProperties();
                    //Refresh TreeNode
                    node.Text = shortcut.DisplayName;
                }
            }
        }     

        private void mniContextClick(object sender, EventArgs e)
        {
            ToolStripMenuItem i = (ToolStripMenuItem)sender;

            switch (i.Name) {
                case "mniProperties":
                    ShortcutProperties();
                    break;

                case "mniDelete":
                    ShortcutDelete();
                    break;

                case "mniRename":
                    StartRename();
                    break;

                case "mniAddShortcut":
                    ShortcutProperties(true);
                    break;

                case "mniAddMenu":                    
                    TreeNode n = tvStartMenu.SelectedNode.Nodes.Add("New Menu");                    
                    n.ImageKey = "FOLDER_CLOSED";
                    n.SelectedImageKey = "FOLDER_OPEN";                    
                    tvStartMenu.SelectedNode = n;
                    n.BeginEdit();
                    InvokeDataChanged();                    
                    break;
                    
            }
        }        

        private void InvokeDataChanged()
        {
            DataChangedByUser?.Invoke(this, new EventArgs());
        }

        private void ShortcutProperties(bool isNew = false)
        {
            _shorcutProperties = new FormShortcutProperties();
            DesktopComposer.Implementation.Shortcut shortcut;
            if (isNew)
            {
                shortcut = new DesktopComposer.Implementation.Shortcut
                {
                    MenuPath = NodeRelativePath(tvStartMenu.SelectedNode.FullPath,true)
                };
            }        
            else{
                shortcut = GetShorcutFromNode();
            }

            if (_shorcutProperties.ShowDialog(shortcut) == DialogResult.OK)
            {
                if (isNew)
                {
                    _Shortcuts.Add(shortcut);
                    AddNode(shortcut);
                } 
                else
                {
                    //Update Node
                }
                InvokeDataChanged();
            }
        }
                
        private void ShortcutDelete(TreeNode tn=null)
        {            
            if (tn == null) tn = tvStartMenu.SelectedNode;

            DesktopComposer.Implementation.Shortcut sh = GetShorcutFromNode(tn);
            if (sh != null) {                 
                Shortcuts.Remove(sh);
                tn.Remove();
            } else
            {
                // There's no Shortcut Object, So is a Menu
                if (tn.Nodes.Count > 0)
                {
                    foreach (TreeNode tnn in tn.Nodes)
                    {
                        //Recursion
                        ShortcutDelete(tnn);
                    }
                }
            }
            tn.Remove();
            InvokeDataChanged();
        }

        private void StartRename()
        {
            tvStartMenu.SelectedNode.BeginEdit();
        }

        private void ShortcutRename(TreeNode tn = null)
        {
            if (tn == null) tn = tvStartMenu.SelectedNode;

            DesktopComposer.Implementation.Shortcut sh = GetShorcutFromNode(tn);
            if (sh != null)
            {
                sh.DisplayName = tn.Text;                
            }
            else
            {
                UpdateShortcutsPath(tn);
            }
            InvokeDataChanged();
        }

        private void UpdateShortcutsPath(TreeNode tn)
        {
            DesktopComposer.Implementation.Shortcut sh = GetShorcutFromNode(tn);
            if (sh != null)
            {
                string nRelativePath = NodeRelativePath(tn.FullPath);

                sh.MenuPath = nRelativePath;

                Console.WriteLine("Setting Menu Path of {0},({1})", sh.DisplayName, tn.FullPath);
            } else
            {
                // There's no Shortcut Object, So is a Menu
                if (tn.Nodes.Count > 0)
                {
                    foreach (TreeNode tnn in tn.Nodes)
                    {
                        //Recursively Update the Path
                        UpdateShortcutsPath(tnn);
                    }
                }
            }
        }

        private string NodeRelativePath(string FullPath, bool LeaveLastpart = false)
        {
            if (!FullPath.Contains("\\")) return "";
            if (! LeaveLastpart) FullPath = FullPath.Remove(FullPath.LastIndexOf("\\"));
            FullPath = FullPath.Substring(FullPath.IndexOf("\\"));
            return FullPath;            
        }
        #region Treeview_Events

        private void tvNodeExpandCollapse(object sender, TreeViewEventArgs e)
        {
            if (NodeIsFolder(e.Node) & (NodeIsStart(e.Node)==false))
            {
                if (e.Node.IsExpanded)
                {
                    e.Node.ImageKey = "FOLDER_OPEN";
                    e.Node.SelectedImageKey = "FOLDER_OPEN";
                }
                else
                {
                    e.Node.ImageKey = "FOLDER_CLOSED";
                    e.Node.SelectedImageKey = "FOLDER_CLOSED";
                }
            }
        }

        private void NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right) tvStartMenu.SelectedNode = e.Node;
        }

        private void tvNodeAfterSelect(object sender, TreeViewEventArgs e)
        {
            // Enable/Disable Menus
            TreeNode n = e.Node;
            
            //Start Menu
            if (NodeIsStart(n))
            {
                mniAddMenu.Enabled = true;
                mniAddShortcut.Enabled = true;
                mniDelete.Enabled = false;
                mniProperties.Enabled = false;
                mniRename.Enabled = false;
            }
            else
            {
                //Shortcut Object
                if (NodeIsFolder(n) == false)
                {
                    mniAddMenu.Enabled = false;
                    mniAddShortcut.Enabled = false;
                    mniDelete.Enabled = true;
                    mniProperties.Enabled = true;
                    mniRename.Enabled = true;
                }
                else //Menu (Folder)
                {
                    mniAddMenu.Enabled = true;
                    mniAddShortcut.Enabled = true;
                    
                    if (e.Node.GetNodeCount(true) == 0)
                        mniDelete.Enabled = true;
                    else
                        mniDelete.Enabled = false;

                    mniProperties.Enabled = false;
                    mniRename.Enabled = true;
                }
            }
        }
        private void tvNodeAfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            if (e.Label != null)
            {
                if (e.Label.Length > 0)
                {
                    if (e.Label.IndexOfAny(new char[] { '/','\\' }) == -1)
                    {
                        // Stop editing without canceling the label change.
                        e.Node.EndEdit(false);
                        e.Node.Text = e.Label;
                        ShortcutRename();
                    }
                    else
                    {
                        /* Cancel the label edit action, inform the user, and 
                           place the node in edit mode again. */
                        e.CancelEdit = true;                        
                        e.Node.BeginEdit();
                    }
                }
                else
                {
                    /* Cancel the label edit action, inform the user, and 
                       place the node in edit mode again. */
                    e.CancelEdit = true;                    
                    e.Node.BeginEdit();
                }
            }            
        }

        private void NodeBeforeLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            if (e.Node.Level == 0 & e.Node.Index == 0)
            {
                e.CancelEdit = true;
            }
        }

        private void tvItemDrag(object sender, ItemDragEventArgs e)
        {
            TreeNode n = (TreeNode)e.Item ;
            //Avoid Drag of Start Node
            if (NodeIsStart(n)==false) { 
                // Move the dragged node when the left mouse button is used.
                if (e.Button == MouseButtons.Left)
                {
                    DoDragDrop(e.Item, DragDropEffects.Move);
                }

                // Copy the dragged node when the right mouse button is used.
                /*else if (e.Button == MouseButtons.Right)
                {
                    DoDragDrop(e.Item, DragDropEffects.Copy);
                }
                */
            }
        }

        // Set the target drop effect to the effect 
        // specified in the ItemDrag event handler.
        private void tvDragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                Console.WriteLine("File drag");
                e.Effect = DragDropEffects.Link;

                string[] dragFiles = (string[])e.Data.GetData(DataFormats.FileDrop);
                foreach (string dragFile in dragFiles)
                {
                    //If Dragged File is not a shortcut don't allow Drag
                    if (Path.GetExtension(dragFile) != ".lnk")
                        e.Effect = DragDropEffects.None;
                }


            }
        }

        // Select the node under the mouse pointer to indicate the 
        // expected drop location.
        private void tvDragOver(object sender, DragEventArgs e)
        {
            TreeNode n;
            // Retrieve the client coordinates of the mouse position.
            Point targetPoint = tvStartMenu.PointToClient(new Point(e.X, e.Y));

            // Select the node at the mouse position.
            n = tvStartMenu.GetNodeAt(targetPoint);
            
            tvStartMenu.SelectedNode = n;
        }

        private void tvDragDrop(object sender, DragEventArgs e)
        {
            // Retrieve the client coordinates of the drop location.
            Point targetPoint = tvStartMenu.PointToClient(new Point(e.X, e.Y));

            // Retrieve the node at the drop location.            
            TreeNode targetNode = tvStartMenu.GetNodeAt(targetPoint);
            if (targetNode == null) targetNode = tvStartMenu.Nodes[0];
            
            if (!NodeIsFolder(targetNode))
                targetNode = targetNode.Parent;

            // Retrieve the node that was dragged.
            TreeNode draggedNode = (TreeNode)e.Data.GetData(typeof(TreeNode));


            if (draggedNode == null)
            {
                //Dragging a file
                if (e.Effect == DragDropEffects.Link)
                {
                    string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                    DesktopComposer.Implementation.Shortcut s;

                    foreach (string file in files)
                    {
                        _Shortcuts.Add(file,targetNode.FullPath);
                        s = _Shortcuts[_Shortcuts.Count - 1];  //Select last added shortcut                      
                        s.MenuPath =  NodeRelativePath(targetNode.FullPath, true);
                        AddNode(s);                                                                                                
                    }
                    //Expand Target Node, ensure it is visible
                    InvokeDataChanged();
                    targetNode.Expand();
                }

            }
            else
            {
                // Confirm that the node at the drop location is not 
                // the dragged node or a descendant of the dragged node.
                if (!draggedNode.Equals(targetNode) && !ContainsNode(draggedNode, targetNode))
                {
                    // If it is a move operation, remove the node from its current 
                    // location and add it to the node at the drop location.
                    if (e.Effect == DragDropEffects.Move)
                    {
                        draggedNode.Remove();
                        targetNode.Nodes.Add(draggedNode);
                        //Update Node shortcut Paths
                        UpdateShortcutsPath(draggedNode);
                        InvokeDataChanged();
                    }


                    // If it is a copy operation, clone the dragged node 
                    // and add it to the node at the drop location.
                    /*
                    else if (e.Effect == DragDropEffects.Copy)
                    {
                        targetNode.Nodes.Add((TreeNode)draggedNode.Clone());
                    }
                    */
                    // Expand the node at the location 
                    // to show the dropped node.
                    targetNode.Expand();
                }
            }
            

        }
        // Determine whether one node is a parent 
        // or ancestor of a second node.
        private bool ContainsNode(TreeNode node1, TreeNode node2)
        {
            // Check the parent node of the second node.
            if (node2.Parent == null) return false;
            if (node2.Parent.Equals(node1)) return true;

            // If the parent node is not null or equal to the first node, 
            // call the ContainsNode method recursively using the parent of 
            // the second node.
            return ContainsNode(node1, node2.Parent);
        }

        #endregion
        private DesktopComposer.Implementation.Shortcut GetShorcutFromNode(TreeNode tn=null)
        {
            if (tn == null) tn = tvStartMenu.SelectedNode;
            DesktopComposer.Implementation.Shortcut sh = (DesktopComposer.Implementation.Shortcut)tn.Tag;
            return sh;
        }

            
    }
}
