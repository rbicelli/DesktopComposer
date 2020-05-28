using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ComposerAdmin.Forms;

namespace ComposerAdmin
{
    static class Program
    {        
        /// <summary>
        /// Punto di ingresso principale dell'applicazione.
        /// </summary>
        [STAThread]
        static void Main()
        {                                    
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            using (FormComposerEditor f = new FormComposerEditor())
            {
                Application.Run(f);
            }
        }
        
    }
}
