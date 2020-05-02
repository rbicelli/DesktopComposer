using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Principal;
using System.ServiceProcess;
using System.Text;
using Cassia;

namespace ComposerAgentService
{
    public partial class AgentService : ServiceBase
    {
        public AgentService()
        {
            InitializeComponent();           
        }

        protected override void OnStart(string[] args)
        {
            AppLogger.WriteLine("Service Started");
        }

        protected override void OnStop()
        {
            AppLogger.WriteLine("Service Stopped");
        }

        private ITerminalServicesManager _manager = new TerminalServicesManager();
        
        protected override void OnSessionChange(SessionChangeDescription changeDescription)
        {
            string strExec = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\ComposerAgent.exe";
            int iRet;
            try
            {
                switch (changeDescription.Reason)
                {
                    case SessionChangeReason.ConsoleConnect:
                        AppLogger.WriteLine("Console Connection");
                        break;

                    case SessionChangeReason.ConsoleDisconnect:
                        AppLogger.WriteLine("Console Disconnection");
                        break;

                    case SessionChangeReason.RemoteConnect:
                        AppLogger.WriteLine("Remote Connection");
                        break;

                    case SessionChangeReason.RemoteDisconnect:
                        AppLogger.WriteLine("Remote Disconnection");
                        break;

                    case SessionChangeReason.SessionLock:
                        AppLogger.WriteLine("Session Lock");
                        break;

                    case SessionChangeReason.SessionUnlock:
                        AppLogger.WriteLine("Session Unlock");
                        break;

                    case SessionChangeReason.SessionLogon:
                        AppLogger.WriteLine("Session Logon");                        
                        //iRet = ProcessExtensions.StartProcessAsCurrentUser(changeDescription.SessionId, strExec, " -compose");
                        //AppLogger.WriteLine("Result of Logon process call: {0}", iRet.ToString());
                        break;

                    case SessionChangeReason.SessionLogoff:
                        AppLogger.WriteLine("Session Logoff");                        
                        //iRet = ProcessExtensions.StartProcessAsCurrentUser(changeDescription.SessionId, strExec, " -decompose");
                        //AppLogger.WriteLine("Result of Logon process call: {0}", iRet.ToString());
                        break;

                    case SessionChangeReason.SessionRemoteControl:
                        AppLogger.WriteLine("Session Remote Control");
                        break;

                    default:
                        AppLogger.WriteLine("Unhandled Event");
                        break;
                }
                WriteSessionInfo(changeDescription.SessionId);
            }
            catch (Exception e)
            {
                AppLogger.WriteLine("Error Querying Session Information", e);
            }
        }

        private void WriteSessionInfo(int sessionId)
        {
            ITerminalServicesSession session;
            NTAccount UserACt;

            using (ITerminalServer server = _manager.GetLocalServer())
            {
                server.Open();
                session = server.GetSession(sessionId);                
            }
                                                
            AppLogger.WriteLine("Session ID: " + session.SessionId);
            if (session.UserAccount != null)
            {
                UserACt = session.UserAccount;
                
                SecurityIdentifier ActSID = (SecurityIdentifier)UserACt.Translate(typeof(SecurityIdentifier));                
                String sidString = ActSID.ToString();
                AppLogger.WriteLine("User Account Name: " + UserACt.Value);
                AppLogger.WriteLine("User Account SID: " + sidString);

            }
            if (session.ClientIPAddress != null)
            {
                AppLogger.WriteLine("IP Address: " + session.ClientIPAddress);
            }
            AppLogger.WriteLine("Window Station: " + session.WindowStationName);
            AppLogger.WriteLine("Client Build Number: " + session.ClientBuildNumber);
            AppLogger.WriteLine("State: " + session.ConnectionState);
            AppLogger.WriteLine("Connect Time: " + session.ConnectTime);
            AppLogger.WriteLine("Logon Time: " + session.LoginTime);
            AppLogger.WriteLine("Idle Time: " + session.IdleTime);
            AppLogger.WriteLine(
                string.Format("Client Display: {0}x{1} with {2} bits per pixel",
                              session.ClientDisplay.HorizontalResolution, session.ClientDisplay.VerticalResolution,
                              session.ClientDisplay.BitsPerPixel));
            AppLogger.WriteLine("---------------------------------------------------------------------");
            AppLogger.WriteLine("---------------------------------------------------------------------");
        }
       
    }
}
