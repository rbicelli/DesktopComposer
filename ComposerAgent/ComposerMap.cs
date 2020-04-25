using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.Win32;

namespace ComposerAgent
{
    #region Elements-Shortcuts
    public enum CompositionElementLocations
    {
        StartMenu,
        Desktop
    }
    public interface ICompositionElement
    {
        string FilePath { get; }        
        Guid objectID { get; }
    }

    public class CompositionElement : ICompositionElement
    {
        [XmlAttribute]
        public Guid objectID { get; set; }
        
        [XmlAttribute]
        public string FilePath { get; set; }

        [XmlAttribute]
        public CompositionElementLocations Location { get; set; }
    }

    public class CompositionElements : ObservableCollection<CompositionElement>
    {

    }
    #endregion

    #region Elements-Registry
    public interface ICompositionRegElement
    {
        string UniqueKeyName { get; }
        string ValueName { get; }        
    }

    public class CompositionRegElement : ICompositionRegElement
    {                
        [XmlAttribute]
        public string UniqueKeyName { get; set; }

        [XmlAttribute]
        public string ValueName { get; set; }
        
        [XmlAttribute]
        public string ValueType { get; set; }

        [XmlAttribute]
        public string RootKey { get; set; }

        [XmlAttribute]
        public string SubKey { get; set; }

        [XmlAttribute]
        public string Value { get; set; }

        [XmlIgnore]
        public RegistryValueKind ValueKind { 
            get {
                switch (ValueType.ToLower())
                {
                    case "qword":
                        //Integer
                        return RegistryValueKind.QWord;                        

                    case "dword":
                        return RegistryValueKind.DWord;                        

                    case "string":
                    default:
                        return RegistryValueKind.String;                        
                }
            } 
        }

        [XmlIgnore]
        public RegistryHive Hive { 
            get { 
                switch (RootKey.ToUpper())
                {
                    case "HKEY_LOCAL_MACHINE":
                        return RegistryHive.LocalMachine;
                    
                    case "HKEY_CURRENT_USER":
                    default:
                        return RegistryHive.CurrentUser;
                }
            } 
        }
        

        private object ParsedValue(string ParamValue)
        {
            long lngValue = 0;
            long.TryParse(ParamValue, out lngValue);

            switch (ValueType.ToLower())
            {
                case "qword":
                    //Integer                    
                    return (int)lngValue;

                case "dword":
                    return lngValue;

                case "string":
                default:
                    return ParamValue;
            }
        }

        public bool SaveToRegistry()
        {
            string registrykey = RootKey + '\\' + SubKey;            
            object regValue;

            if (Value.ToLower() == "[delete]") {
                try
                {
                    RegistryKey rKey;
                    rKey = RegistryKey.OpenBaseKey(Hive, RegistryView.Default);
                    rKey = rKey.OpenSubKey(SubKey, true);
                    if (rKey!=null) rKey.DeleteValue(ValueName);
                } catch (Exception e)
                {
                    AppLogger.WriteLine("Error: ", e);
                }
            }
            else 
            {
                //Set Value
                try
                {
                    regValue = ParsedValue(Value);
                    Registry.SetValue(registrykey, ValueName, regValue, ValueKind);
                } catch (Exception e)
                {
                    AppLogger.WriteLine("Error: ", e);
                }
            }
            return true;
        }
        
    }

    public class CompositionRegElements : ObservableCollection<CompositionRegElement>
    {

    }
    #endregion

    public class ComposerMap
    {
        private CompositionElements _ComposedElements;
        private CompositionRegElements _ComposedRegElements;
        public CompositionElements ComposedElements {
            get => _ComposedElements; 
        }

        public CompositionRegElements ComposedRegElements
        {
            get => _ComposedRegElements;
        }
        
        [XmlAttribute]
        public int ComposedStatus { get; set; }
        
        public ComposerMap()
        {
            _ComposedElements = new CompositionElements();
            _ComposedRegElements = new CompositionRegElements();
        }

        public void AddItem(Guid ItemGuid, string ItemPath, CompositionElementLocations ItemLocation)
        {
            CompositionElement e = new CompositionElement()
            {
                objectID = ItemGuid,
                FilePath = ItemPath,
                Location = ItemLocation
            };
            _ComposedElements.Add(e);
        }
       
        public void AddRegItem(string uniqueKeyName, string valueType, string rootKey, string subKey, string valueName, string value)
        {
            CompositionRegElement e = new CompositionRegElement() {
                UniqueKeyName = uniqueKeyName,
                RootKey = rootKey,
                ValueName = valueName,
                SubKey = subKey,
                Value = value,
                ValueType = valueType
            };
            _ComposedRegElements.Add(e);
        }
        
        #region Serialization
        public void Serialize(string Filename)
        {
            XmlSerializer xsSubmit = new XmlSerializer(typeof(ComposerMap));
            var subReq = this;

            using (var sww = new StreamWriter(Filename))
            {
                using (XmlWriter writer = XmlWriter.Create(sww))
                {
                    xsSubmit.Serialize(writer, subReq);
                }
            }
        }

        public bool Deserialize(string Filename)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ComposerMap));
            ComposerMap subReq = this;

            try
            {
                using (Stream reader = new FileStream(Filename, FileMode.Open))
                {
                    // Call the Deserialize method to restore the object's state.
                    subReq = (ComposerMap)serializer.Deserialize(reader);
                    _ComposedElements = subReq.ComposedElements;
                    _ComposedRegElements = subReq.ComposedRegElements;
                    this.ComposedStatus = subReq.ComposedStatus;
                }
                
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        #endregion

    }
}
