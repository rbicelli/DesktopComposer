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
        public void Install()
        {
            WindowsPrincipal pricipal = new WindowsPrincipal(WindowsIdentity.GetCurrent());
            bool hasAdministrativeRight = pricipal.IsInRole(WindowsBuiltInRole.Administrator);

            if (!hasAdministrativeRight)
            {
                RunElevated(System.Reflection.Assembly.GetExecutingAssembly().CodeBase, "-install");
            }
            else
            {
                //Create Active Directory Group
                try
                {
                    Console.WriteLine("Adding Security Group " + Constants.LOCAL_SECURITYGROUP_NAME);
                    var ad = new DirectoryEntry("WinNT://" + Environment.MachineName + ",computer");
                    DirectoryEntry newGroup = ad.Children.Add(Constants.LOCAL_SECURITYGROUP_NAME, "group");
                    newGroup.Invoke("Put", new object[] { "Description", Constants.LOCAL_SECURITYGROUP_DESC });
                    newGroup.CommitChanges();
                    //Common Start Menu
                    Console.WriteLine("Setting Security ACLs to target directories");
                    AddDirectorySecurity(System.Environment.GetFolderPath(Environment.SpecialFolder.CommonStartMenu), Constants.LOCAL_SECURITYGROUP_NAME);
                    AddDirectorySecurity(System.Environment.GetFolderPath(Environment.SpecialFolder.CommonDesktopDirectory), Constants.LOCAL_SECURITYGROUP_NAME);
                }
                catch
                {
                    Console.WriteLine("Installation Error");
                    return;
                }
                //TODO: Set ACL on created group
            }
        }

        /// <summary>
        /// Uninstallation Process
        /// Rollbacks Installation Process
        /// Run ELevated if started as normal user
        /// </summary>
        public void Uninstall()
        {
            WindowsPrincipal pricipal = new WindowsPrincipal(WindowsIdentity.GetCurrent());
            bool hasAdministrativeRight = pricipal.IsInRole(WindowsBuiltInRole.Administrator);

            if (!hasAdministrativeRight)
            {
                RunElevated(System.Reflection.Assembly.GetExecutingAssembly().CodeBase, "-uninstall");
            }
            else
            {
                //Delete Local Group                                                
                try
                {
                    RemoveDirectorySecurity(System.Environment.GetFolderPath(Environment.SpecialFolder.CommonStartMenu), Constants.LOCAL_SECURITYGROUP_NAME);
                    AddDirectorySecurity(System.Environment.GetFolderPath(Environment.SpecialFolder.CommonDesktopDirectory), Constants.LOCAL_SECURITYGROUP_NAME);
                    var ad = new DirectoryEntry("WinNT://" + Environment.MachineName + ",computer");
                    var group = ad.Children.Find(Constants.LOCAL_SECURITYGROUP_NAME, "group");
                    ad.Children.Remove(group);
                }
                catch
                {
                    Console.WriteLine("Error during uninstallation");
                    return;
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
                Process.Start(processInfo);
                return true;
            }
            catch (Win32Exception)
            {
                //Do nothing. Probably the user canceled the UAC window

            }
            return false;
        }

        // Adds an ACL entry on the specified directory for the specified account.
        public static void AddDirectorySecurity(string FileName, string Account)
        {
            // Create a new DirectoryInfo object.
            DirectoryInfo dInfo = new DirectoryInfo(FileName);
            FileSystemRights Rights = FileSystemRights.ReadAndExecute;
            AccessControlType ControlType = AccessControlType.Deny;


            // Get a DirectorySecurity object that represents the 
            // current security settings.
            DirectorySecurity dSecurity = dInfo.GetAccessControl();

            // Add the FileSystemAccessRule to the security settings. 
            dSecurity.AddAccessRule(new FileSystemAccessRule(Account, Rights, ControlType));
            dSecurity.AddAccessRule(new FileSystemAccessRule(Account, Rights, InheritanceFlags.ContainerInherit, PropagationFlags.InheritOnly, ControlType));
            dSecurity.AddAccessRule(new FileSystemAccessRule(Account, Rights, InheritanceFlags.ObjectInherit, PropagationFlags.InheritOnly, ControlType));

            // Set the new access settings.
            dInfo.SetAccessControl(dSecurity);
        }

        // Removes an ACL entry on the specified directory for the specified account.
        public static void RemoveDirectorySecurity(string FileName, string Account)
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
        }
    }
}
