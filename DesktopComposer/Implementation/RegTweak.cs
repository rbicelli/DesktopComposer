using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using DesktopComposer.Interfaces;
using Microsoft.Win32;

namespace DesktopComposer.Implementation
{
    public class RegTweak: IRegTweak
    {
        private RegistryValueKind _valueKind;
        private string _valueType;
        private string _restoreValue;
        
        private RegistryHive _regHive;

        [XmlAttribute]
        public string UniqueKeyName { get; set; }        

        [XmlAttribute]
        public string RootKey { get; set; }

        [XmlAttribute]
        public string SubKey { get; set; }

        [XmlAttribute]
        public string ValueName { get; set; }      

        [XmlAttribute]
        public string ValueType {
            get {
                return _valueType;
            }
            set
            {
                _valueType = value;
                switch (value.ToLower())
                {
                    case "dword":
                        _valueKind = RegistryValueKind.DWord;
                        break;
                    case "qword":
                        _valueKind = RegistryValueKind.QWord;
                        break;
                    case "string":
                        _valueKind = RegistryValueKind.String;
                        break;
                }
            } 
        }

        [XmlAttribute]
        public string  ValueEnabled { get; set; }

        [XmlAttribute]
        public String ValueDisabled { get; set; }

        [XmlAttribute]
        public String ValueDefault { get; set; }


        [XmlIgnore]
        public string RestoreValue { get => _restoreValue; }        

        [XmlAttribute]
        public bool Enabled { get; set; }

        [XmlAttribute]
        public RegTweakDisableMode DisableMode { get; set; }
        
        public string GetRootKey()
        {
            switch (RootKey.ToUpper())
            {                
                case "HKLM":
                case "HKEY_LOCAL_MACHINE":
                    _regHive = RegistryHive.LocalMachine;
                    return "HKEY_LOCAL_MACHINE";
                case "HKCU":
                case "HKEY_CURRENT_USER":
                default:                
                    _regHive = RegistryHive.CurrentUser;
                    return "HKEY_CURRENT_USER";
            }
        }
        
        private object ParsedValue(string ParamValue)
        {
            long lngValue;
            long.TryParse(ParamValue, out lngValue);

            switch (_valueKind)
            {
                case RegistryValueKind.DWord:
                    //Integer
                    return (int)lngValue;                    

                case RegistryValueKind.QWord:
                    return lngValue;

                case RegistryValueKind.String:
                default:
                    return ParamValue;
            }
        }        
                
        public bool SetRegistryValue()
        {
            string registrykey = GetRootKey() + '\\' +  SubKey;
            object oldValue = null;
            object regValue;
            bool valueCreated = false;

            try
            {
                //No matter what I do, always backup the old value
                oldValue = Registry.GetValue(registrykey, ValueName, null);
                if (oldValue == null)
                    _restoreValue = ValueDefault;
                else
                    _restoreValue = oldValue.ToString();
            } catch (Exception e)
            {
                Console.WriteLine("Error: {0}", e);
                //Do Nothing
            }     
                
            if (Enabled)                 
            {
                //Value is enabled
                try { 
                    regValue = ParsedValue(ValueEnabled);
                    Registry.SetValue(registrykey, ValueName, regValue, _valueKind);
                    valueCreated = true;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error: {0}", e);
                }
            } 
            else
            {
                //Value is Disabled
                RegistryKey rKey;
                if (ValueDisabled.ToLower() == "[delete]") {
                    if (oldValue != null) { 
                        //Delete Registry Key
                        try
                        {
                            rKey = RegistryKey.OpenBaseKey(_regHive, RegistryView.Default);
                            rKey = rKey.OpenSubKey(SubKey, true);
                            rKey.DeleteValue(ValueName);
                            valueCreated = true;
                        } catch (Exception e)
                        {
                            //Do Nothing
                            Console.WriteLine("Error: {0}", e.Message);
                        }
                    }
                } else
                {
                    try
                    {
                        regValue = ParsedValue(ValueDisabled);
                        Registry.SetValue(registrykey, ValueName, regValue, _valueKind);
                        valueCreated = true;
                    } catch (Exception e)
                    {
                        Console.WriteLine("Error: {0}", e);
                    }
                }
            }
            return valueCreated;       
        }
    }
}
