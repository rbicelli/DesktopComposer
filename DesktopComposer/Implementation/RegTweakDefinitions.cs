using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace DesktopComposer.Implementation
{
    public class RegTweakDefinitions: ObservableCollection<RegTweakDefinition>
    {
        public RegTweakDefinition this[string UniqueKeyName]
        {
            get
            {
                return (from rtweak in this
                        where rtweak.UniqueKeyName == UniqueKeyName
                        select rtweak).FirstOrDefault();
            }

        }
    }
}
