using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace DesktopComposer.Implementation
{
    public class Composition
    {
        private Shortcuts _shortcuts;        
        
        public Shortcuts Shortcuts
        {
            get => _shortcuts;            
        }

        public Composition()
        {
            _shortcuts = new Shortcuts();            
        }

        public void LoadShortcutsFromPath(string path)
        {
            List<System.IO.FileInfo> files;
            DirectoryIterator di = new DirectoryIterator();
            files = di.TraverseTree(path,".lnk");

            foreach (System.IO.FileInfo lFile in files)
            _shortcuts.Add(lFile.FullName, path);            
        }

        public void Serialize(string Filename)
        {
            XmlSerializer xsSubmit = new XmlSerializer(typeof(Composition));
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
            XmlSerializer serializer = new XmlSerializer(typeof(Composition));
            Composition subReq = this;

            try { 
            using (Stream reader = new FileStream(Filename, FileMode.Open))
            {
                // Call the Deserialize method to restore the object's state.
                subReq = (Composition)serializer.Deserialize(reader);
            }
                this._shortcuts = subReq.Shortcuts;                
                return true;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

    }
}
