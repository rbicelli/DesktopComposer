using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ComposerAgent
{
    public class UtilityArguments : InputArguments
    {
        public bool Install
        {
            get { return KeyPresent("install"); }
        }

        public bool Uninstall
        {
            get { return KeyPresent("uninstall"); }
        }

        public bool Compose
        {
            get { return KeyPresent("compose"); }
        }

        public bool Decompose
        {
            get { return KeyPresent("decompose"); }
        }

        public bool NoConsole
        {
            get { return KeyPresent("noconsole"); }
        }

        public string Filename
        {            
            get { return GetValue("file"); }
        }

        public string DesktopPath
        {
            get { return GetValue("desktopdir"); }
        }

        public string StartMenuPath
        {
            get { return GetValue("startdir"); }
        }

        public UtilityArguments(string[] args) : base(args)
        {
        }

        protected bool GetBoolValue(string key)
        {
            string adjustedKey;
            if (ContainsKey(key, out adjustedKey))
            {
                bool res;
                bool.TryParse(_parsedArguments[adjustedKey], out res);
                return res;
            }
            return false;
        }

        protected bool KeyPresent(string key)
        {
            string adjustedKey;
            if (ContainsKey(key, out adjustedKey))
            {
                return true;
            }  
            else
            {
                return false;
            }          
        }

    }
}
