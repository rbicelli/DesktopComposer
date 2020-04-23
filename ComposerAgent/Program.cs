using System.IO;


namespace ComposerAgent
{
    class Program
    {
        static void Main(string[] args)
        {

            //ConsoleHandler.AllocateConsole();

            UtilityArguments arguments = new UtilityArguments(args);            

            if (arguments.Compose != null)
            {
                if (File.Exists(arguments.Compose))
                {
                    string desktopPath = (arguments.DesktopPath == null) ? "" : arguments.DesktopPath;
                    string startPath = (arguments.StartMenuPath == null) ? "" : arguments.StartMenuPath;
                    Compose(arguments.Compose, startPath, desktopPath);
                    return;
                }
            }

            if (arguments.Decompose != null)
            {
                if (File.Exists(arguments.Decompose))
                {
                    string desktopPath = (arguments.DesktopPath == null) ? "" : arguments.DesktopPath;
                    string startPath = (arguments.StartMenuPath == null) ? "" : arguments.StartMenuPath;
                    Decompose(arguments.Decompose, startPath, desktopPath);
                    return;
                }
            }

            if (arguments.Install)
            {                
                Install();
                return;
            }

            if (arguments.Uninstall)
            {
                Uninstall();
                return;
            }

            //ConsoleHandler.FreeConsole();
        }

        private static void Compose(string FileName, string StartMenuPath, string DesktopPath)
        {
            Composer _lcomposer = new Composer(FileName, StartMenuPath, DesktopPath);
            _lcomposer.Compose();
        }

        private static void Decompose(string FileName, string StartMenuPath, string DesktopPath)
        {
            Composer _lcomposer = new Composer(FileName, StartMenuPath, DesktopPath);
            _lcomposer.Decompose();
        }

        private static void Install() {
            AgentInstaller i = new AgentInstaller();
            i.Install();
        }

        private static void Uninstall() {
            AgentInstaller i = new AgentInstaller();
            i.Uninstall();            
        }
    }
}
