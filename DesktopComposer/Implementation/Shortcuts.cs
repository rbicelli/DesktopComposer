using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using DesktopComposer.Interfaces;
using IWshRuntimeLibrary;

namespace DesktopComposer.Implementation
{
    public class Shortcuts : ObservableCollection<Shortcut>
    {
        
        public void Add(string shortcutPath, string basePath="")
        {
            //Open Shortcut File            
            WshShell WinShell = new WshShell();
            WshShortcut WinShortcut;
            Shortcut lShortcut;
            
            //REFACTOR: Method LoadFromFile in Shortcut
            WinShortcut = WinShell.CreateShortcut(shortcutPath);
            string relativePath="";
            string shortcutFileName = "";
            foreach (Shortcut LocalShortcut in this)
            {
                //TODO: Add other criteria (e.g. Arguments)
                if (LocalShortcut.TargetPath == WinShortcut.TargetPath) return;
            }

            //Relative Path
            if (basePath != "")
            {
                relativePath = shortcutPath.Replace(basePath, "");
            }

            shortcutFileName = Path.GetFileName(shortcutPath);
            relativePath = Path.GetDirectoryName(relativePath);

            lShortcut = new Shortcut
            {
                DisplayName = Path.GetFileNameWithoutExtension(shortcutPath),
                TargetPath = WinShortcut.TargetPath,
                WorkingDirectory = WinShortcut.WorkingDirectory,
                Arguments = WinShortcut.Arguments,
                Hotkey = WinShortcut.Hotkey,
                IconLocation = WinShortcut.IconLocation,
                ObjectID = Guid.NewGuid(),
                MenuPath = relativePath,
                WindowStyle = WinShortcut.WindowStyle,
                PutOnStartMenu = true                
            };            
            lShortcut.FindIcon(shortcutPath);
            this.Add(lShortcut);
        }
        

    }
}
