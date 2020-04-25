using System;
using System.Collections.Generic;
using System.IO;
using DesktopComposer.Implementation;
using System.Security.Principal;
using DesktopComposer;
using Microsoft.Win32;

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
        private const string GPO_AGENT_POLICYKEY = @"\Software\Policies\Sequence Software\Composer Agent\";
        private const string GPO_AGENT_ENABLE = "AgentEnable";
        private const string GPO_AGENT_DECOMPOSEATLOGOFF = "AgentDecomposeAtLogoffDisable";
        private const string GPO_AGENT_COMPOSITIONFILELOCATION = "CompositionFileLocation";
        private const string GPO_AGENT_LOGFILELOCATION = "AgentLogFileLocation";
        private const string GPO_AGENT_LOGSEVERITYTHRESHOLD = "AgentLogSeverityThreshold";

        #region Constructors
        public Composer(string CompositionFileName=null, string StartMenuPath=null, string DesktopPath=null)
        {                                              
            _composition = new Composition();
            _composerMap = new ComposerMap();
            
            _backupDirStartMenu = "StartMenu";
                        
            //Get Environment Folders
            _appdataDir = System.Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\DesktopComposer\\Agent";
            
            string LoggerFileName = _appdataDir + "\\Composer.log";

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
            int? isGPOLocked = (int?)Registry.GetValue(RegistryKeyFullPath, GPO_AGENT_ENABLE, 0);
            if ( isGPOLocked!=null & isGPOLocked==1)
            {                
                //If Locked by GPO the Reg Key Exists so it is safe to get values without care of errors
                //Open Logger Filename
                LoggerFileName = (string)Registry.GetValue(RegistryKeyFullPath, GPO_AGENT_LOGFILELOCATION, LoggerFileName);
                AppLogger.FileName = Environment.ExpandEnvironmentVariables(LoggerFileName);

                //Logger Severity Threshold
                AppLogger.SeverityThreshold = (LoggerSeverity)Registry.GetValue(RegistryKeyFullPath, GPO_AGENT_LOGSEVERITYTHRESHOLD, 2);

                //File Name: if Reg Key Exists alter the method parameter valu
                CompositionFileName = (string)Registry.GetValue(RegistryKeyFullPath, GPO_AGENT_COMPOSITIONFILELOCATION,CompositionFileName);
                
                bool.TryParse((string)Registry.GetValue(RegistryKeyFullPath, GPO_AGENT_DECOMPOSEATLOGOFF, "0"),out _decompose_disable);

                AppLogger.WriteLine("Settings Managed by Group Policy", LoggerSeverity.Warning);              
            }

            //Set Composition File Name
            _compositionFileName = Environment.ExpandEnvironmentVariables(CompositionFileName);
            //Set Logger
            AppLogger.FileName = Environment.ExpandEnvironmentVariables(LoggerFileName);
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
                
                AppLogger.WriteLine("Opened Composition Definition File: " + _compositionFileName);
                return true;
            }
            catch (Exception e)
            {
                AppLogger.WriteLine("Unable to load Composition Definition File " + _compositionFileName, LoggerSeverity.Critical, e );
                return false;
            }
        }

        private bool OpenCompositionMapFile()
        {
            try
            {
                if (File.Exists(_compositionMapFileName)) _composerMap.Deserialize(_compositionMapFileName);
                AppLogger.WriteLine("Composition Map Loaded");
                return true;
            }
            catch (Exception e)
            {
                AppLogger.WriteLine("Unable to Load Composition Map",LoggerSeverity.Critical,e);
                return false;
            }
        }
        

        public bool Compose()
        {
            bool ret;
            ret = OpenCompositionFile();
            ret = OpenCompositionMapFile();

            if (ret) ret = ComposeShortcuts();
            if (ret) ret = ComposeRegistry();
            
            //Set Composed Flag
            if (ret)
            {
                _composerMap.ComposedStatus = 1;
                _composerMap.Serialize(_compositionMapFileName);
                
                AppLogger.WriteLine("Composition Completed",LoggerSeverity.Info);
            } else {
                AppLogger.WriteLine("Composition Error", LoggerSeverity.Critical);
            }
            return ret;
        }

        public bool ComposeShortcuts()
        {
            bool ShortcutExists;
            CompositionElement ce;
            
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
                
                AppLogger.WriteLine("Pre-Checking Shortcuts of Composed Element {" + ce.objectID + "}");
                
                ShortcutExists = false;

                string composedPath, composerPath;                
                
                foreach (Shortcut s in _composition.Shortcuts)
                {                                                            
                    if (s.ObjectID.ToString() == ce.objectID.ToString())
                    {
                        AppLogger.WriteLine("Checking Shortcut \"" + s.DisplayName + "\" {" + s.ObjectID + "}");
                        //Check if Object is renamed or moved to another path
                        if (ce.Location == CompositionElementLocations.StartMenu)
                        {
                            AppLogger.WriteLine("Shortcut is Deployed on Start Menu");
                            composedPath = _startMenuPath;
                            composerPath = s.MenuPath;
                        } else
                        {
                            AppLogger.WriteLine("Shortcut is Deployed on Desktop");
                            composedPath = _desktopPath;
                            composerPath = string.Empty;
                        }
                        
                        //Path on composer and composed must be equal, otherwise delete composed file
                        if (( composerPath + '\\' + s.Filename) != (ce.FilePath) )
                        {
                            AppLogger.WriteLine("Composed path and Definition Path Mismatch. Deleting Shortcut File \"" + ce.FilePath + "\"");
                            DeleteFile(composedPath + '\\' + ce.FilePath);
                        } 
                        else 
                        {
                            AppLogger.WriteLine("Shortcut Already Exists");
                            ShortcutExists = true;
                        }
                                                    
                        break;
                    }                   
                }

                if (ShortcutExists == false)
                {
                    //Delete Shortcut
                    AppLogger.WriteLine("Composed Element does not Exists Anymore, Deleting File: \"" + ce.FilePath + "\"");
                    DeleteFile(ce.FilePath);                        
                }
            }            
            
            //Clear Composed Elements
            _composerMap.ComposedElements.Clear();

            AppLogger.WriteLine("Composing Shortcuts");
            
            foreach (Shortcut s in _composition.Shortcuts)
            {
                if (s.Deploy(_startMenuPath, _desktopPath, _userGroupMembership))
                {
                    AppLogger.WriteLine("Deployed Shortcut \"" + s.DisplayName + "\" {" + s.ObjectID + "}");
                    //If Deployed then add to deployed map
                    if (s.PutOnStartMenu) _composerMap.AddItem(s.ObjectID, s.FullPath, CompositionElementLocations.StartMenu);
                    if (s.PutOnDesktop) _composerMap.AddItem(s.ObjectID, s.Filename, CompositionElementLocations.Desktop);
                } else
                {
                    //Delete
                    AppLogger.WriteLine("NOT Deployed Shortcut \"" + s.DisplayName + "\" {" + s.ObjectID + "}");
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
                AppLogger.WriteLine("Error deleting file \"" + fileName + "\"", LoggerSeverity.Error, e);
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
                AppLogger.WriteLine("Deomposition Completed", LoggerSeverity.Info);
            } else
            {
                AppLogger.WriteLine("Deomposition Error", LoggerSeverity.Critical);
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
            AppLogger.WriteLine("Decomposing Shortcuts");
            //Get Decomposed Flag
            //If Composed then restore Shortcuts                       
            foreach (CompositionElement ce in _composerMap.ComposedElements)
            {
                AppLogger.WriteLine("Deleting Composed Item {" + ce.objectID + "} " + ce.FilePath);                
                DeleteFile(ce.FilePath);                 
            }
            _composerMap.ComposedElements.Clear();

            RestoreMenuFolder(_backupDirStartMenu, _startMenuPath, true);
            
            AppLogger.WriteLine("Shortcut Decomposition Complete");
            
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
            AppLogger.WriteLine("Backing Up Folder " + BackupDir + " to " + FolderBackupName);
            ret = CopyFolder(SourceFolder, BackupDir, true);
            if (ret) AppLogger.WriteLine("Backup Successful");
                else AppLogger.WriteLine("Backup Error");
            if (EmptyDirectoryAfter) ret = EmptyDirectory(SourceFolder);            
            return ret;
        }

        private bool RestoreMenuFolder(string FolderBackupName, String DestinationFolder, bool EmptyDirectoryAfter=false)
        {
            bool ret;
            string BackupDir = _appdataDir + "\\" + FolderBackupName;
            AppLogger.WriteLine("Restoring Folder " + BackupDir + " to " + DestinationFolder);
            ret = CopyFolder(BackupDir, DestinationFolder, true);
            if (ret) AppLogger.WriteLine("Restore Successful");
            else AppLogger.WriteLine("Restore Error");
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
                    AppLogger.WriteLine("Error while doing backup of file: \"" + lFile.FullName + "\"", LoggerSeverity.Error, ex);
                }
            }
            return true;
        }

        private bool EmptyDirectory(string directory)
        {

            if (!Directory.Exists(directory)) return true;
            
            AppLogger.WriteLine("Emptying Directory \"" + directory + "\"");
            
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
                AppLogger.WriteLine("Error deleting Directory \"" + directory + "\"", LoggerSeverity.Error, e);
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
