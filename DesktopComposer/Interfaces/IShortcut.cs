using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesktopComposer.Interfaces
{
    public interface IShortcut
    {
        string MenuPath { get; }
        string TargetPath { get; }
        string DisplayName { get; }
        string FullPath { get; }
        Guid ObjectID { get; }
    
    }
}
