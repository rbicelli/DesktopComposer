using System;
using System.ComponentModel;
using System.Diagnostics;
using System.DirectoryServices;
using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Threading.Tasks;

namespace ComposerAgent
{
    class AgentInstaller
    {       

        /// <summary>
        /// Installation Process
        /// - Creates Local User Group
        /// - Sets Deny ACL on Common Start Menu for Local User Group Created
        /// Run Elevated if started as normal user 
        /// </summary>        
        public bool Install()
        {
            Console.WriteLine("Installing...");
            WindowsPrincipal pricipal = new WindowsPrincipal(WindowsIdentity.GetCurrent());
            bool hasAdministrativeRight = pricipal.IsInRole(WindowsBuiltInRole.Administrator);
            bool ret = true;
            if (!hasAdministrativeRight)
            {
                Console.WriteLine("Running Installer in Elevated Mode...");
                
                ret = RunElevated(System.Reflection.Assembly.GetExecutingAssembly().CodeBase, "-install");
                if (ret) Console.WriteLine("Installation OK"); else Console.WriteLine("Installation ERROR");
                return ret;            
            }
            else
            {
                //Create Active Directory Group
                try
                {
                    Console.Write("Adding Security Group {0} ...", Constants.LOCAL_SECURITYGROUP_NAME);
                    var ad = new DirectoryEntry("WinNT://" + Environment.MachineName + ",computer");
                    DirectoryEntry newGroup = ad.Children.Add(Constants.LOCAL_SECURITYGROUP_NAME, "group");
                    newGroup.Invoke("Put", new object[] { "Description", Constants.LOCAL_SECURITYGROUP_DESC });
                    newGroup.CommitChanges();
                    Console.WriteLine(" [OK]");
                                        
                    Console.Write("Setting Security ACLs to target directories...");
                    ret = AddDirectorySecurity(System.Environment.GetFolderPath(Environment.SpecialFolder.CommonStartMenu), Constants.LOCAL_SECURITYGROUP_NAME);
                    ret = AddDirectorySecurity(System.Environment.GetFolderPath(Environment.SpecialFolder.CommonDesktopDirectory), Constants.LOCAL_SECURITYGROUP_NAME);
                    Console.WriteLine(" [OK]");
                    return ret;
                }
                catch
                {
                    Console.WriteLine(" [ERROR]");
                    return false;
                }                
            }
        }

        /// <summary>
        /// Uninstallation Process
        /// Rollbacks Installation Process
        /// Run Elevated if started as normal user
        /// </summary>
        public bool Uninstall()
        {
            WindowsPrincipal pricipal = new WindowsPrincipal(WindowsIdentity.GetCurrent());
            bool hasAdministrativeRight = pricipal.IsInRole(WindowsBuiltInRole.Administrator);
            bool ret = true;

            if (!hasAdministrativeRight)
            {
                Console.WriteLine("Running Installer in Elevated Mode...");                
                ret = RunElevated(System.Reflection.Assembly.GetExecutingAssembly().CodeBase, "-uninstall");                
                if (ret) Console.WriteLine("Uninstallation OK"); else Console.WriteLine("Uninstallation ERROR");
                return ret;
            }
            else
            {
                //Delete Local Group                                                
                try
                {
                    Console.Write("Setting Security ACLs to target directories...");

                    RemoveDirectorySecurity(System.Environment.GetFolderPath(Environment.SpecialFolder.CommonStartMenu), Constants.LOCAL_SECURITYGROUP_NAME);
                    AddDirectorySecurity(System.Environment.GetFolderPath(Environment.SpecialFolder.CommonDesktopDirectory), Constants.LOCAL_SECURITYGROUP_NAME);
                    
                    Console.WriteLine(" [OK]");

                    Console.Write("Removing Security Group {0} ...", Constants.LOCAL_SECURITYGROUP_NAME);                    
                    var ad = new DirectoryEntry("WinNT://" + Environment.MachineName + ",computer");
                    var group = ad.Children.Find(Constants.LOCAL_SECURITYGROUP_NAME, "group");
                    ad.Children.Remove(group);                    
                    Console.WriteLine(" [OK]");
                    return true;
                }
                catch
                {
                    Console.WriteLine(" [ERROR]");
                    return false;
                }
            }
        }


        private static bool RunElevated(string fileName, string args)
        {
            ProcessStartInfo processInfo = new ProcessStartInfo();
            processInfo.Verb = "runas";
            processInfo.FileName = fileName;
            processInfo.Arguments = args;
            try
            {
                using (Process myProcess = Process.Start(processInfo)) {
                    do
                    {
                      //Do Nothing  
                    } while (!myProcess.WaitForExit(250));                    
                    if (myProcess.ExitCode == 0) return true;
                }
                return false;
            }

            catch (Win32Exception)
            {
                //Do nothing. Probably the user canceled the UAC window
                return false;
            }            
        }

        // Adds an ACL entry on the specified directory for the specified account.
        public static bool AddDirectorySecurity(string FileName, string Account)
        {
            // Create a new DirectoryInfo object.
            DirectoryInfo dInfo = new DirectoryInfo(FileName);
            FileSystemRights Rights = FileSystemRights.ReadAndExecute;
            AccessControlType ControlType = AccessControlType.Deny;

            try
            {
                // Get a DirectorySecurity object that represents the 
                // current security settings.
                DirectorySecurity dSecurity = dInfo.GetAccessControl();

                // Add the FileSystemAccessRule to the security settings. 
                dSecurity.AddAccessRule(new FileSystemAccessRule(Account, Rights, ControlType));
                dSecurity.AddAccessRule(new FileSystemAccessRule(Account, Rights, InheritanceFlags.ContainerInherit, PropagationFlags.InheritOnly, ControlType));
                dSecurity.AddAccessRule(new FileSystemAccessRule(Account, Rights, InheritanceFlags.ObjectInherit, PropagationFlags.InheritOnly, ControlType));

                // Set the new access settings.
                dInfo.SetAccessControl(dSecurity);
                return true;
            } catch
            {
                return false;
            } 
        }

        // Removes an ACL entry on the specified directory for the specified account.
        public static bool RemoveDirectorySecurity(string FileName, string Account)
        {
            try
            {
                // Create a new DirectoryInfo object.
                DirectoryInfo dInfo = new DirectoryInfo(FileName);
                FileSystemRights Rights = FileSystemRights.ReadAndExecute;
                AccessControlType ControlType = AccessControlType.Deny;

                // Get a DirectorySecurity object that represents the 
                // current security settings.
                DirectorySecurity dSecurity = dInfo.GetAccessControl();
                // Add the FileSystemAccessRule to the security settings. 
                dSecurity.RemoveAccessRule(new FileSystemAccessRule(Account, Rights, ControlType));
                dSecurity.RemoveAccessRule(new FileSystemAccessRule(Account, Rights, InheritanceFlags.ContainerInherit, PropagationFlags.InheritOnly, ControlType));
                dSecurity.RemoveAccessRule(new FileSystemAccessRule(Account, Rights, InheritanceFlags.ObjectInherit, PropagationFlags.InheritOnly, ControlType));

                // Set the new access settings.
                dInfo.SetAccessControl(dSecurity);
                return true;
            } catch
            {
                return false;
            }
        }
    }
}
