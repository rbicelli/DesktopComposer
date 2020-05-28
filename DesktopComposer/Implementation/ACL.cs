using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using DesktopComposer.Interfaces;

namespace DesktopComposer.Implementation
{
    public enum ACLType
    {
        Allow = 0,
        Deny = 1
    }

    public enum ACLObjectType
    {
        User = 1,
        Group = 2
    }
    public class ACL : IACL, ICloneable
    {
        [XmlAttribute]
        public Guid ObjectID { get; set; }

        [XmlAttribute]
        public String SID { get; set; }

        [XmlAttribute]
        public String ObjectName { get; set; }

        [XmlAttribute]
        public String ObjectPath { get; set; }

        [XmlAttribute]
        public ACLType ACLType { get; set; }

        [XmlAttribute]
        public bool Disabled { get; set; }

        [XmlAttribute]
        public ACLObjectType ObjectType { get; set; }

        [XmlIgnore]
        public string ObjectShortName{
            get { 
                if (ObjectPath != null)
                {
                    string[] str = ObjectPath.Split('/');
                    if (str.Length > 1)
                    {
                        return str[str.Length - 2] + '\\' + str[str.Length - 1];
                    }
                }
                return ObjectName;
            }

        }

        public object Clone()
        {
            ACL clonedACL = new ACL { 
                ObjectID = ObjectID,
                SID = SID,
                ObjectName = ObjectName,
                ObjectPath = ObjectPath,
                ACLType = ACLType,
                Disabled = Disabled,
                ObjectType = ObjectType
            };
                        
            return clonedACL;
        }

    }
}
