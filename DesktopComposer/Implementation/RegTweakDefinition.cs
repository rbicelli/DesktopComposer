using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using DesktopComposer.Interfaces;
using Microsoft.Win32;

namespace DesktopComposer.Implementation
{
    public enum RegTweakDisplayType
    {
        Bool,
        BoolWithLeaveSetting,
        List,
        Text
    }

    public enum RegTweakDisableMode
    {
        SetFalse,
        SetEmpty,
        Delete        
    }

    /// <summary>
    /// Registry Tweak Definition
    /// Almost Every Serialized Parameter is represented as string in order to easily mantain
    /// the serialized data
    /// </summary>
    public class RegTweakDefinition : IRegTweakDefinition
    {
        private RegTweakDisplayType _displayType;        
        
        private string _valueDisplayMode;
        private string _valueEnabled;

        [XmlIgnore]        
        public RegTweakDisplayType DisplayType { get => _displayType; }

        [XmlIgnore]
        public RegTweak LinkedRegTweak { get; set;}

        [XmlIgnore]
        public string ValueEnabled {
            get => _valueEnabled; 
            set { _valueEnabled = value; } 
        }

        [XmlAttribute]
        public string UniqueKeyName { get; set; }

        [XmlAttribute]
        public string RootKey { get; set; }

        [XmlAttribute]
        public string SubKey { get; set; }

        [XmlAttribute]
        public string ValueName { get; set; }

        [XmlAttribute]
        public string ValueDefault { get; set; }                

        [XmlAttribute]
        public string ValueDisabled { get; set; }

        [XmlAttribute]
        public string ValueType { get; set; }

        [XmlAttribute]
        public string Description { get; set; }

        [XmlAttribute]
        public string LongDescription { get; set; }

        [XmlAttribute]
        public string OsCompatibility { get; set; }

        [XmlAttribute]
        public string ValueDisplayMode
        {
            get => _valueDisplayMode;
            set
            {
                _valueDisplayMode = value;
                switch (value.ToLower())
                {
                    case "bool":
                        _displayType = RegTweakDisplayType.Bool;
                        BoolValueMap();
                        break;

                    case "bool-leavedefault":
                        _displayType = RegTweakDisplayType.BoolWithLeaveSetting;
                        break;

                    case "list":
                        _displayType = RegTweakDisplayType.List;
                        break;

                    case "text":
                    default:
                        _displayType = RegTweakDisplayType.Text;
                        break;
                }
            }
        }

        [XmlAttribute]
        public List<KeyValuePair<string, string>> ValueMap { get; set; }

        [XmlAttribute]
        public List<KeyValuePair<string, string>> LangDescription { get; set; }

        [XmlAttribute]
        public List<KeyValuePair<string, string>> LangLongDescription { get; set; }      

        public string GetBoolValue(bool TrueOrFalse)
        {
            if (TrueOrFalse == true) return "1";
            else 
                return "0";
        }

        private void BoolValueMap()
        {            
            if (_valueEnabled==null)
            {
                _valueEnabled = "1";
            }
        }
    }
}
