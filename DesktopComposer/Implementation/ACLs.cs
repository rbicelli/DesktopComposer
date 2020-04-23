
using System;
using System.Collections.ObjectModel;
using System.Linq;
using DesktopComposer.Interfaces;

namespace DesktopComposer.Implementation
{
    public class ACLs : ObservableCollection<ACL>, ICloneable
    {
        public ACL this[string sid]
        {
            get
            {
                return (from acl in this
                        where acl.SID == sid
                        select acl).FirstOrDefault();
            }

        }        
        
        public object Clone() {
            ACLs ClonedAcls = new ACLs();
            foreach (ACL acl in this)
            {
                ClonedAcls.Add((ACL)acl.Clone());
            }
            return ClonedAcls;
        }
    }
}
