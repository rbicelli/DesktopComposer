using System;
using System.IO;
using System.Runtime.InteropServices;

namespace ComposerAgent
{
    class Program
    {
        private static int _exitCode = 1;
        private static bool _noconsole = false;
        static void Main(string[] args)
        {            
            
            UtilityArguments arguments = new UtilityArguments(args);
            
            _noconsole = (arguments.NoConsole);

            if (_noconsole == false) ConsoleHandler.AllocateConsole();

            if (arguments.Compose)
            {
                if (File.Exists(arguments.Filename) | (arguments.Filename==null))
                {
                    string desktopPath = arguments.DesktopPath;
                    string startPath = arguments.StartMenuPath;
                    Compose(arguments.Filename, startPath, desktopPath);
                    AppExit();
                }
            }

            if (arguments.Decompose)
            {
                if (File.Exists(arguments.Filename) | (arguments.Filename == null))
                {
                    string desktopPath = arguments.DesktopPath;
                    string startPath = arguments.StartMenuPath;
                    Decompose(arguments.Filename, startPath, desktopPath);
                    AppExit();
                }
            }

            if (arguments.Install)
            {                
                Install();
                AppExit();
            }

            if (arguments.Uninstall)
            {
                Uninstall();
                AppExit();
            }            
        }

        private static void AppExit()
        {
            if (_noconsole==false) ConsoleHandler.FreeConsole();
            Environment.Exit(_exitCode);
        }

        private static void Compose(string FileName, string StartMenuPath, string DesktopPath)
        {
            Composer _lcomposer = new Composer(FileName, StartMenuPath, DesktopPath);
            if (_lcomposer.Compose()) _exitCode = 0;
        }

        private static void Decompose(string FileName, string StartMenuPath, string DesktopPath)
        {
            Composer _lcomposer = new Composer(FileName, StartMenuPath, DesktopPath);
            if (_lcomposer.Decompose()) _exitCode = 0;
        }

        private static void Install() {
            AgentInstaller i = new AgentInstaller();
            if (i.Install()) _exitCode = 0;
        }

        private static void Uninstall() {
            AgentInstaller i = new AgentInstaller();
            if (i.Uninstall()) _exitCode = 0;
        }
    }
}
