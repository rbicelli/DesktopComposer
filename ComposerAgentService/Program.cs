using System;
using System.Collections.Generic;
using System.Configuration.Install;
using System.IO;
using System.Linq;
using System.Reflection;
using System.ServiceProcess;
using System.Text;

namespace ComposerAgentService
{
    static class Program
    {
        /// <summary>
        /// Punto di ingresso principale dell'applicazione.
        /// </summary>
        static void Main(string[] args)
        {
            AppLogger.FileName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\LoggedEvents.txt";
            AppLogger.SeverityThreshold = LoggerSeverity.Debug;

            if (System.Environment.UserInteractive)
            {
                string parameter = string.Concat(args);
                switch (parameter)
                {
                    case "--install":
                        ManagedInstallerClass.InstallHelper(new string[] { Assembly.GetExecutingAssembly().Location });
                        break;

                    case "--uninstall":
                        ManagedInstallerClass.InstallHelper(new string[] { "/u", Assembly.GetExecutingAssembly().Location });
                        break;
                }
            }
            else
            {
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[]
                {
                new AgentService()
                };
                ServiceBase.Run(ServicesToRun);
            }


            
        }
    }
}
