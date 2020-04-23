using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using DesktopComposer.Implementation;
using System.Security.Principal;
using System.Net.NetworkInformation;
using Microsoft.SqlServer.Server;
using DesktopComposer;
using System.Collections.ObjectModel;
using System.Runtime.Remoting.Messaging;
using System.Security.Policy;
using Microsoft.Win32;
using System.Diagnostics;

namespace ComposerAgent
{
    class Composer
    {
        private Composition _composition;
        private string _desktopPath;
        private string _startMenuPath;        
        private string _backupDirStartMenu;
        private string _appdataDir;
        private string _compositionMapFileName;
        private string _compositionFileName;

        private bool _decompose_disable;        

        private List<string> _userGroupMembership;

        private ComposerMap _composerMap;


        //Group Policy Constants
        private const string GPO_AGENT_ROOTKEY = @"HKEY_CURRENT_USER";
        private const string GPO_AGENT_POLICYKEY = @"\Software\Policies\DesktopComposer\ComposerAgent\";
        private const string GPO_AGENT_ENABLE = "AgentEnable";
        private const string GPO_AGENT_DECOMPOSEATLOGOFF = "AgentDecomposeAtLogoffDisable";
        private const string GPO_AGENT_COMPOSITIONFILELOCATION = "CompositionFileLocation";
        private const string GPO_AGENT_LOGFILELOCATION = "AgentLogFileLocation";        

        #region Constructors
        public Composer(string CompositionFileName=null, string StartMenuPath=null, string DesktopPath=null)
        {                                              
            _composition = new Composition();
            _composerMap = new ComposerMap();
            
            _backupDirStartMenu = "StartMenu";
            
            string LoggerFileName = _appdataDir + "ComposerLog.log";

            //Get Environment Folders
            _appdataDir = System.Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\DesktopComposer\\Agent";            

            _userGroupMembership = GetUserMembership();

            //Create Directories
            if (!Directory.Exists(_appdataDir))
                Directory.CreateDirectory(_appdataDir);           

            _compositionMapFileName= _appdataDir + "\\CompositionMap.xml";
            

            if (StartMenuPath == null)
                _startMenuPath = System.Environment.GetFolderPath(Environment.SpecialFolder.StartMenu);
            else
                _startMenuPath = StartMenuPath;

            if (DesktopPath == null)
                _desktopPath = System.Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            else
                _desktopPath = DesktopPath;

            //Open Composition File
            //Read From GPO

            //Is GPO Enabled?
            string RegistryKeyFullPath = GPO_AGENT_ROOTKEY + GPO_AGENT_POLICYKEY;
            if ((int)Registry.GetValue(RegistryKeyFullPath, GPO_AGENT_ENABLE, 0) == 1)
            {                
                //File Name: if Reg Key Exists alter the method parameter valu
                CompositionFileName = (string)Registry.GetValue(RegistryKeyFullPath, GPO_AGENT_COMPOSITIONFILELOCATION,CompositionFileName);
                LoggerFileName = (string)Registry.GetValue(RegistryKeyFullPath, GPO_AGENT_LOGFILELOCATION, LoggerFileName);

                bool.TryParse((string)Registry.GetValue(RegistryKeyFullPath, GPO_AGENT_DECOMPOSEATLOGOFF, "0"),out _decompose_disable);
            }

            //Set Composition File Name
            _compositionFileName = Environment.ExpandEnvironmentVariables(CompositionFileName);
            //Set Logger
            FileLogger.FileName = Environment.ExpandEnvironmentVariables(LoggerFileName);
        }
        #endregion
        private bool OpenCompositionFile()
        {
            try
            {
                //Deserializing from an UNC readonly Path produces unexpected results, so let's create a temporary copy
                string TempCompositionFileName = Path.GetTempPath() + Guid.NewGuid().ToString() + ".dctmp";
                File.Copy(_compositionFileName, TempCompositionFileName, true);
                
                _composition.Deserialize(TempCompositionFileName);
                DeleteFile(TempCompositionFileName);
                return true;
            }
            catch (Exception e)
            {
                FileLogger.WriteLine("Unable to load Composition File (" + e.Message + ")");
                return false;
            }
        }

        private bool OpenCompositionMapFile()
        {
            try
            {
                if (File.Exists(_compositionMapFileName)) _composerMap.Deserialize(_compositionMapFileName);
                return true;
            }
            catch (Exception e)
            {
                FileLogger.WriteLine("Unable to load Composition File (" + e.Message + ")");
                return false;
            }
        }
        

        public bool Compose()
        {
            bool ret;
            
            ret = ComposeShortcuts();
            if (ret) ret = ComposeRegistry();
            
            //Set Composed Flag
            if (ret)
            {
                _composerMap.ComposedStatus = 1;
                _composerMap.Serialize(_compositionMapFileName);
            }
            return ret;
        }

        public bool ComposeShortcuts()
        {
            bool ShortcutExists;
            CompositionElement ce;

            //Load Composer Map
           
            //Get Decomposed Flag
            
            //Compose Shortcuts

            //If Desktop is Composed don't make a backup
            if (_composerMap.ComposedStatus != 1) {
                //Backup Start Menu
                BackupMenuFolder(_startMenuPath, _backupDirStartMenu, true);                   
            }

            //Delete Items Deleted            
            //Instead of foreach loop collection in reverse order to avoid eception @removal
            for (int i=_composerMap.ComposedElements.Count -1 ; i>=0; i--)
            {
                ce = _composerMap.ComposedElements[i];
                
                ShortcutExists = false;

                string composedPath, composerPath;

                foreach (Shortcut s in _composition.Shortcuts)
                {                                                            
                    if (s.ObjectID.ToString() == ce.objectID.ToString())
                    {
                        //Check if Object is renamed or moced to another path
                        if (ce.Location == CompositionElementLocations.StartMenu)
                        {
                            composedPath = _startMenuPath;
                            composerPath = s.MenuPath;
                        } else
                        {
                            composedPath = _desktopPath;
                            composerPath = string.Empty;
                        }
                        
                        //Path on composer and composed must be equal, otherwise delete composed file
                        if (( composerPath + '\\' + s.Filename) != (ce.FilePath) )
                        {
                            FileLogger.WriteLine("Delete previously composed item " + ce.FilePath);
                            DeleteFile(composedPath + '\\' + ce.FilePath);
                        } 
                        else 
                        {
                            ShortcutExists = true;
                        }
                                                    
                        break;
                    }                   
                }

                if (ShortcutExists == false)
                {
                    //Delete Shortcut
                    FileLogger.WriteLine("Deleting shortcut " + ce.FilePath);
                    DeleteFile(ce.FilePath);                        
                }
            }            
            
            //Clear Composed Elements
            _composerMap.ComposedElements.Clear();

            foreach (Shortcut s in _composition.Shortcuts)
            {
                if (s.Deploy(_startMenuPath, _desktopPath, _userGroupMembership))
                {
                    //If Deployed then add to deployed map
                    if (s.PutOnStartMenu) _composerMap.AddItem(s.ObjectID, s.FullPath, CompositionElementLocations.StartMenu);
                    if (s.PutOnDesktop) _composerMap.AddItem(s.ObjectID, s.Filename, CompositionElementLocations.Desktop);
                } else
                {
                    //Delete
                    DeleteFile(_startMenuPath + s.FullPath);
                    DeleteFile(_desktopPath + '\\' + s.Filename);
                }
            }            
            return true;
        }

        private bool DeleteFile(string fileName)
        {
            try
            {
                if (File.Exists(fileName)) File.Delete(fileName);
                return true;
            } 
            catch (Exception e)
            {
                FileLogger.WriteLine("Error deleting file.", e);
                return false;
            }
        }
        public bool ComposeRegistry()
        {
            foreach (RegTweak tweak in _composition.RegTweaks)
            {
                // Write to registry
                if (tweak.SetRegistryValue())
                {
                    _composerMap.AddRegItem(tweak.UniqueKeyName, tweak.ValueType, tweak.GetRootKey(), tweak.SubKey, tweak.ValueName, tweak.RestoreValue);
                }
            }
            return true;
        }

        public bool Decompose()
        {
            bool ret;

            ret = DecomposeShortcuts();
            if (ret) ret = DecomposeRegistry();

            //Set Composed Flag
            if (ret)
            {
                _composerMap.ComposedStatus = 0;
                _composerMap.Serialize(_compositionMapFileName);
            }
            return ret;
        }

        /// <summary>
        /// Decomposition of Shortcuts
        /// Currently runs decomposition only if previous composition has occoured, in order non to restore an empty start menu.
        /// </summary>
        /// <param name="force"></param>
        /// <returns></returns>
        public bool DecomposeShortcuts()
        {
            //Get Decomposed Flag
            //If Composed then restore Shortcuts                       
            foreach (CompositionElement ce in _composerMap.ComposedElements)
            {                
                DeleteFile(ce.FilePath);                 
            }
            _composerMap.ComposedElements.Clear();

            RestoreMenuFolder(_backupDirStartMenu, _startMenuPath, true);
            
            return true;
        }

        public bool DecomposeRegistry()
        {
            foreach (CompositionRegElement relm in _composerMap.ComposedRegElements)
            {
                relm.SaveToRegistry();
            }
            _composerMap.ComposedRegElements.Clear();
            return true;
        }

        private bool BackupMenuFolder(string SourceFolder, string FolderBackupName, bool EmptyDirectoryAfter=true) {
            bool ret;
            string BackupDir = _appdataDir + "\\" + FolderBackupName;
            ret = CopyFolder(SourceFolder, BackupDir, true);
            if (EmptyDirectoryAfter) ret = EmptyDirectory(SourceFolder);
            return ret;
        }

        private bool RestoreMenuFolder(string FolderBackupName, String DestinationFolder, bool EmptyDirectoryAfter=false)
        {
            bool ret;
            string BackupDir = _appdataDir + "\\" + FolderBackupName;
            ret = CopyFolder(BackupDir, DestinationFolder, true);
            if (EmptyDirectoryAfter) ret = EmptyDirectory(DestinationFolder);
            return ret;
        }
        #region FileSystem Operations
        private bool CopyFolder(string SourceFolder, string DestinationFolder, bool MoveFiles=false)
        {
            string relPath;

            if (!Directory.Exists(DestinationFolder))
                            Directory.CreateDirectory(DestinationFolder);           

            List<System.IO.FileInfo> files;

            DirectoryIterator di = new DirectoryIterator();
            files = di.TraverseTree(SourceFolder, ".lnk");

            foreach (System.IO.FileInfo lFile in files)
            {
                relPath = lFile.FullName.Replace(SourceFolder, DestinationFolder);
                try
                {
                    if (!Directory.Exists(Path.GetDirectoryName(relPath))) Directory.CreateDirectory(Path.GetDirectoryName(relPath));                                            
                        
                    File.Copy(lFile.FullName, relPath, true);
                    
                    //Delete if copied (Move - used in backup before composition)
                    if (MoveFiles == true) File.Delete(lFile.FullName);                    
                }
                catch (Exception ex)
                {
                    FileLogger.WriteLine("Error while doing backup of file: " + lFile.FullName + " (" + ex.Message + ")");
                }
            }
            return true;
        }

        private bool EmptyDirectory(string directory)
        {

            if (!Directory.Exists(directory)) return true;                      
            
            foreach (string dir in Directory.GetDirectories(directory))
            {
                EmptyDirectory(dir);                
            }

            try
            {
                if (Directory.GetFiles(directory).Length == 0)
                    Directory.Delete(directory);
            } catch (Exception e)
            {
                FileLogger.WriteLine("Error deleting Directory " + directory, e);
            }
            
            return true;
        }
        #endregion
        private List<string> GetUserMembership()
        {
            List<string> SIDs=new List<string>();
            
            IntPtr accountToken = WindowsIdentity.GetCurrent().Token;
            WindowsIdentity windowsIdentity = new WindowsIdentity(accountToken);

            //User SID
            SIDs.Add(windowsIdentity.Owner.ToString());

            IdentityReferenceCollection irc = windowsIdentity.Groups;
            foreach (IdentityReference ir in irc)
            {
                SIDs.Add(ir.Value);
            }
            return SIDs;
        }
    }
}
