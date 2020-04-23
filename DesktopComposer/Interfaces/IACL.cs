using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesktopComposer.Interfaces
{
    public interface IACL
    {
        String SID { get; }
        String ObjectName { get; }        
        Guid ObjectID { get; }
    }
}
