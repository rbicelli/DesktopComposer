using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DesktopComposer.Implementation;
using ComposerAdmin.Properties;
using Tulpep.ActiveDirectoryObjectPicker;

namespace ComposerAdmin.Forms
{
    public partial class FormComposerEditor : Form
    {
        private bool _documentUnsaved;

        private string _fileName;
        private string _windowTitle;
        
        public FormComposerEditor()
        {
            InitializeComponent();
            _windowTitle = this.Text;            
            _fileName = "";
            StartWithNewFile();            
        }

        public void StartWithNewFile()
        {
           if (PromptForSave())
            {
                _dComposition = new Composition();
                StartEditor.Shortcuts = _dComposition.Shortcuts;
                _fileName = "";
                SetWindowTitle();
            }
        }
        private Composition _dComposition;

        private void SetWindowTitle()
        {
            string fileTitle;
            if (_fileName!=""){
                fileTitle = Path.GetFileNameWithoutExtension(_fileName);
            } else
            {
                fileTitle = Resources.FILE_NEW_LABEL;
            }
            //Set document unsaved flag to false
            _documentUnsaved = false;
            this.Text = _windowTitle + " - " + fileTitle;
        }
             
        private string promptSave(string FileName=null)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = Resources.COMPOSITION_FILE_FILTER;
            saveFileDialog1.FilterIndex = 0;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                return saveFileDialog1.FileName;
            }

            return "";
        }
        
        private bool DocumentSave(bool SaveAs=false)
        {
            string lFileName;

            if ( (_fileName == "" | _fileName == null) | (SaveAs==true) )
                lFileName = promptSave();
            else
                lFileName = _fileName;

            if (lFileName != "" & lFileName != null)
            {
                _dComposition.Serialize(lFileName);
                _fileName = lFileName;
                SetWindowTitle();
                return true;
            } 
            else
            {
                return false;
            }

        }
        
        private void MenuClick(object sender, EventArgs e)
        {
            ToolStripMenuItem i = (ToolStripMenuItem)sender;            
            
            switch (i.Name)
            {
                case "miFileNew":
                    HandleMenuFileAction("FileNew");
                    break;

                case "miFileSave":
                    HandleMenuFileAction("FileSave");
                    break;

                case "miFileSaveAs":
                    HandleMenuFileAction("FileSaveAs");
                    break;

                case "miFileOpen":
                    HandleMenuFileAction("FileOpen");
                    break;                    
                    
            }
        }

        private void HandleMenuFileAction(string stringAction)
        {
            
            switch (stringAction)
            {
                case "FileNew":
                    StartWithNewFile();
                    break;

                case "FileSave":
                    DocumentSave();
                    break;

                case "FileSaveAs":
                    DocumentSave(true);
                    break;

                case "FileOpen":
                    if (PromptForSave())
                    {
                        OpenFileDialog oDialog = new OpenFileDialog();

                        oDialog.Filter = Resources.COMPOSITION_FILE_FILTER;
                        oDialog.FilterIndex = 0;
                        oDialog.RestoreDirectory = true;

                        if (oDialog.ShowDialog() == DialogResult.OK)
                        {
                            //Change Cursor
                            Cursor prevCursor = this.Cursor;
                            this.Cursor = Cursors.WaitCursor;


                            _fileName = oDialog.FileName;
                            if (_dComposition == null) _dComposition = new Composition();
                            if (_dComposition.Deserialize(_fileName))
                            {
                                StartEditor.Shortcuts = _dComposition.Shortcuts;
                            }

                            this.Cursor = prevCursor;
                            SetWindowTitle();
                        }
                    }
                    break;
            }
        }

        private void MenuImportShortcutsClick(object sender, EventArgs e)
        {
            ToolStripMenuItem i=(ToolStripMenuItem) sender;
            
            switch (i.Name)
            {
                case "miImportFromLocalComputerStartMenu":
                    HandleMenuImportShortcutsAction("ImportFromLocalComputerStartMenu");
                    break;
                case "miImportFromNetworkComputerStartMenu":
                    HandleMenuImportShortcutsAction("ImportFromNetworkComputerStartMenu");                    
                    break;
                case "miImportFromFolder":
                    HandleMenuImportShortcutsAction("ImportFromFolder");
                    break;
            }
                        
        }

        private void HandleMenuImportShortcutsAction(string stringAction)
        {
            string importPath = null;

            switch (stringAction)
            {
                case "ImportFromLocalComputerStartMenu":
                    importPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.CommonStartMenu);
                    break;
                case "ImportFromNetworkComputerStartMenu":
                    FormInputNetworkPath f = new FormInputNetworkPath();
                    importPath = f.ShowDialogNetworkPath();
                    break;
                case "ImportFromFolder":
                    FolderBrowserDialog oDialog = new FolderBrowserDialog();

                    oDialog.ShowNewFolderButton = false;
                    if (oDialog.ShowDialog() == DialogResult.OK)
                    {
                        importPath = oDialog.SelectedPath;

                    }
                    break;
            }

            if (importPath != null)
            {
                //Update Status Strip
                UpdateToolstripStatus(Resources.STATUSMSG_IMPORTING_MENUSTRUCTURE);

                //Change Cursor
                Cursor prevCursor = this.Cursor;
                this.Cursor = Cursors.WaitCursor;

                //_dComposition = new Composition();
                _dComposition.LoadShortcutsFromPath(importPath);
                StartEditor.Shortcuts = _dComposition.Shortcuts;

                //Restore Cursor
                this.Cursor = prevCursor;

                //Update Status Strip
                UpdateToolstripStatus(Resources.STATUSMSG_DONE, true);

                _documentUnsaved = true;
            }

        }

        private void TimerToolstripUpdate_Tick(object sender, EventArgs e)
        {
            StatusDisplay.Text = "";
            TimerToolstripUpdate.Enabled = false;
        }

        private void UpdateToolstripStatus(string TextStatus, bool EmptyAfter=false) {            
            StatusDisplay.Text = TextStatus;
            if (EmptyAfter==true) TimerToolstripUpdate.Enabled = true;
            Application.DoEvents();
        }

        private bool PromptForSave()
        {
            if (_documentUnsaved)
            {
                switch (MessageBox.Show(Resources.MESSAGE_PROMPT_SAVE, Resources.MESSAGE_TITLE_SAVE, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                {
                    case DialogResult.Yes:
                        //Save Document
                        return DocumentSave();
                        
                    case DialogResult.No:
                        return true;
                        
                    case DialogResult.Cancel:
                        return false;                        
                }
            }
            return true;
        }
        private void Form_OnClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = !PromptForSave();
        }

        private void StartEditor_OnDataChanged(object sender, EventArgs e)
        {
            _documentUnsaved = true;
        }

        private void MenuHelp_Click(object sender, EventArgs e)
        {

            ToolStripMenuItem i = (ToolStripMenuItem)sender;

            switch (i.Name)
            {
                case "mniHelpAbout":
                    FormAbout fa = new FormAbout();
                    fa.ShowDialog();
                    break;
            }
        
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void tsButtons_ButtonClick(object sender, ToolStripItemClickedEventArgs e)
        {            
            
            switch (e.ClickedItem.Name)
            {
                case "tsbFileNew":
                    HandleMenuFileAction("FileNew");
                    break;
                case "tsbFileOpen":
                    HandleMenuFileAction("FileOpen");
                    break;
                case "tsbFileSave":
                    HandleMenuFileAction("FileSave");
                    break;
                case "tsbFileSaveAs":
                    HandleMenuFileAction("FileSaveAs");
                    break;

                case "tsbImportLocalStartMenu":
                    HandleMenuImportShortcutsAction("ImportFromLocalComputerStartMenu");
                    break;
                case "tsbImportNetworkComputer":
                    HandleMenuImportShortcutsAction("ImportFromNetworkComputerStartMenu");
                    break;
                case "tsbImportFolder":
                    HandleMenuImportShortcutsAction("ImportFromFolder");
                    break;                    
            }
        }
    }
}
