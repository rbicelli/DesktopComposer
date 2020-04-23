using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace DesktopComposer.Implementation
{
    public class RegTweaks:ObservableCollection<RegTweak>
    {
        public RegTweak this[string UniqueKeyName]
        {
            get
            {
                return (from rtweak in this
                        where rtweak.UniqueKeyName == UniqueKeyName
                        select rtweak).FirstOrDefault();
            }
        }
        public void Sync(RegTweakDefinitions TweakDefinitions)
        {
            RegTweak t;

            foreach (RegTweakDefinition def in TweakDefinitions)
            {
                if (this[def.UniqueKeyName] == null)
                {
                    t = new RegTweak()
                    {
                        UniqueKeyName = def.UniqueKeyName,                    
                    };
                    this.Add(t);
                } 
                else
                { 
                    t = this[def.UniqueKeyName]; 
                }
                t.RootKey = def.RootKey;
                t.SubKey = def.SubKey;
                t.ValueName = def.ValueName;
                t.ValueType = def.ValueType;
                t.ValueDefault = def.ValueDefault;
                t.ValueDisabled = def.ValueDisabled;                
                
                //Link Tweak and its Definition
                def.LinkedRegTweak = t;
            }                 
        }
        
    }
}
