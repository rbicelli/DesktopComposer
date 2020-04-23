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

        public string Compose
        {
            get { return GetValue("compose"); }
        }

        public string Decompose
        {
            get { return GetValue("decompose"); }
        }

        public string DesktopPath
        {
            get { return GetValue("desktopdir"); }
        }

        public string StartMenuPath
        {
            get { return GetValue("startmenudir"); }
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
