using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.IconLib;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using DesktopComposer.Interfaces;
using IWshRuntimeLibrary;

namespace DesktopComposer.Implementation
{
    public class Shortcut : IShortcut
    {        
        public ACLs ACLs;

        [XmlAttribute]
        public Guid ObjectID { get; set; }

        public string MenuPath { get; set; }

        [XmlAttribute]
        public bool PutOnDesktop { get; set; }

        [XmlAttribute]
        public bool PutOnTaskBar { get; set; }

        [XmlAttribute]
        public bool PutOnStartMenu { get; set; }

        [XmlAttribute]
        public bool CheckIfTargetExists { get; set; }

        [XmlAttribute]
        public string TargetPath { get; set; }

        [XmlAttribute]
        public string Arguments { get; set; }

        [XmlAttribute]
        public string WorkingDirectory { get; set; }

        [XmlAttribute]
        public string DisplayName { get; set; }

        [XmlAttribute]
        public string ExtType { get; set; }

        [XmlAttribute]
        public string Description { get; set; }

        [XmlAttribute]
        public string Comment { get; set; }

        [XmlAttribute]
        public string IconLocation { get; set; }       
       
        [XmlIgnore]
        public Icon IconCacheSmall { get; set; }        

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        [XmlElement("IconCacheSmall")]
        public byte[] IconCacheSmallSerialized
        {            
            get
            { 
                // serialize                                
                if (IconCacheSmall == null) return null;
                MultiIcon mIcon = new MultiIcon();
                SingleIcon sIcon = mIcon.Add("smallicon");
                sIcon.CreateFrom(IconCacheLarge.ToBitmap(), IconOutputFormat.Vista);
                using (MemoryStream ms = new MemoryStream())
                {                                        
                    sIcon.Save(ms);
                    return ms.ToArray();
                }
            }
            set
            { 
                // deserialize
                if (value == null)
                {
                    IconCacheSmall = null;
                }
                else
                {
                    MultiIcon mIcon = new MultiIcon();
                    SingleIcon sIcon = mIcon.Add("largeicon");
                    using (MemoryStream ms = new MemoryStream(value))
                    {
                        sIcon.Load(ms);
                        IconCacheSmall = sIcon.Icon;                        
                    }                    
                }
            }
        }

        [XmlIgnore]
        public Icon IconCacheLarge { get; set; }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        [XmlElement("IconCacheLarge")]
        public byte[] IconCacheLargeSerialized
        {
            get
            {   // serialize                
                
                if (IconCacheLarge == null) return null;

                MultiIcon mIcon = new MultiIcon();
                SingleIcon sIcon = mIcon.Add("largeicon");
                sIcon.CreateFrom(IconCacheLarge.ToBitmap(), IconOutputFormat.Vista);
                
                using (MemoryStream ms = new MemoryStream())
                {
                    //IconCacheLarge.Save(ms);
                    sIcon.Save(ms);
                    return ms.ToArray();                    
                }
            }
            set
            { // deserialize
                if (value == null)
                {
                    IconCacheLarge = null;
                }
                else
                {

                    MultiIcon mIcon = new MultiIcon();
                    SingleIcon sIcon = mIcon.Add("largeicon");
                    using (MemoryStream ms = new MemoryStream(value))
                    {
                        sIcon.Load(ms);
                        IconCacheLarge = sIcon.Icon;                        
                    }
                }
            }
        }


        [XmlAttribute]
        public string Hotkey { get; set; }

        [XmlAttribute]
        public int WindowStyle { get; set; }

        [XmlAttribute]
        public bool DeployDisabled { get; set; }

        [XmlAttribute]
        public bool ACLDenyByDefault { get; set; }

        [XmlIgnore]
        public string FullPath {
            get => MenuPath + '\\' + DisplayName + ".lnk";
        }

        [XmlIgnore]
        public String Filename
        {
            get => DisplayName + ".lnk";
        }

        public void FindIcon(string SearchPath=null)
        {
            string ReplaceCDrivePath;            

            string[] IconData = IconLocation.Split(',');
            string IconPath = "";
            int IconIndex = 0;
            if (IconData.Count() == 2)
            {
                IconIndex = int.Parse(IconData[1]);
                IconPath = IconData[0];                
            }            
            
            if (IconPath == "")
                IconPath = TargetPath;
                            
            if (SearchPath != null) { 
                string root = Path.GetPathRoot(SearchPath);
                //Check if is a UNC PATH, so replace C:\ with \\SERVER\SHARE\
                if (root.StartsWith(@"\\"))
                {
                    string[] segs = SearchPath.Split('\\');
                    ReplaceCDrivePath = @"\\" + segs[2] + @"\" + segs[3] + @"\";
                    IconPath = IconPath.Replace("C:\\", ReplaceCDrivePath);
                }
            }

            if (System.IO.File.Exists(IconPath))
            {
                //IconCacheLarge = ShellIcon.IconExtract(IconPath,IconIndex,1);
                Icon lIcon = ShellIcon.IconExtract(IconPath, IconIndex, 1);
                IconCacheLarge = lIcon;
                lIcon = ShellIcon.IconExtract(IconPath, IconIndex);
                IconCacheSmall = lIcon;
            }
        }

        public bool IsDeployAllowed(List<string> ACLMemberOf)
        {            
            //Deployment is disabled
            if (DeployDisabled) return false;
            
            //There's no ACL Defined and the Is allowed by default
            if ((ACLMemberOf == null) & (ACLDenyByDefault == false)) return true;
            if ((ACLs.Count == 0) & (ACLDenyByDefault == false)) return true;

            bool bRet = false;

            foreach (ACL acl in ACLs) { 
                foreach (string aclmemberof in ACLMemberOf) {                     
                    //If ACL is disabled
                    if (acl.SID == aclmemberof & acl.Disabled==false)
                    {
                        //If ACL is type of deny then return false
                        if (acl.ACLType == ACLType.Deny)
                            return false;
                        bRet = true;
                    }
                }                
            }
            return bRet;
        }

        public bool Deploy(string StartMenuPath, string DesktopPath, List<string> ACLMemberOf=null)
        {
            bool bDeploy;
            
            bDeploy = IsDeployAllowed(ACLMemberOf);
                                   
            if (bDeploy == false) return false;

            if (CheckIfTargetExists) //Check if target Exists is flagged, so check. 
            {
                if (FileUtils.Exists(Environment.ExpandEnvironmentVariables(TargetPath)) == false) return false;
            }

            //Application
            if (PutOnStartMenu) {
                SaveShortcut(StartMenuPath + FullPath, StartMenuPath + MenuPath);                
            }

            //Desktop
            if (PutOnDesktop)
            {
                try
                {
                    SaveShortcut(DesktopPath + '\\' + DisplayName + ".lnk");
                } catch
                {
                    return false;
                }
            }

            return true;
        }

        private void SaveShortcut(string DestinationFilePath, string StartMenuFullPath = "")
        {            
            WshShell WinShell = new WshShell();
            WshShortcut WinShortcut;

            WinShortcut = WinShell.CreateShortcut(DestinationFilePath);

            //Set Shortcut Properties
            WinShortcut.TargetPath = TargetPath;
            WinShortcut.WorkingDirectory = WorkingDirectory;
            WinShortcut.Arguments = Arguments;
            WinShortcut.Hotkey = Hotkey;
            //If no Icon is specified then use default Icon of application (Useful for UWP Applications)
            if ( (IconLocation!=null) & ( IconLocation!="") )
                WinShortcut.IconLocation = IconLocation;
            WinShortcut.WindowStyle = WindowStyle;

            if (StartMenuFullPath!="") { 
                if (!Directory.Exists(StartMenuFullPath)) Directory.CreateDirectory(StartMenuFullPath);
            }
                        
            WinShortcut.Save();            
        }
               
        public Shortcut()
        {
            ACLs = new ACLs();
            WindowStyle = 1;
            ObjectID = Guid.NewGuid();
        }
    }
    
}
