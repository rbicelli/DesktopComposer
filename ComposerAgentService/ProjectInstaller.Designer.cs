namespace ComposerAgentService
{
    partial class ProjectInstaller
    {
        /// <summary>
        /// Variabile di progettazione necessaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Pulire le risorse in uso.
        /// </summary>
        /// <param name="disposing">ha valore true se le risorse gestite devono essere eliminate, false in caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Codice generato da Progettazione componenti

        /// <summary>
        /// Metodo necessario per il supporto della finestra di progettazione. Non modificare
        /// il contenuto del metodo con l'editor di codice.
        /// </summary>
        private void InitializeComponent()
        {
            this.ComposerAgentProcInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.ComposerAgentInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // ComposerAgentProcInstaller
            // 
            this.ComposerAgentProcInstaller.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.ComposerAgentProcInstaller.Password = null;
            this.ComposerAgentProcInstaller.Username = null;
            // 
            // ComposerAgentInstaller
            // 
            this.ComposerAgentInstaller.Description = "Provides Desktop Composer Functionalities";
            this.ComposerAgentInstaller.DisplayName = "Desktop Composer Agent Service";
            this.ComposerAgentInstaller.ServiceName = "DesktopComposerAgent";
            this.ComposerAgentInstaller.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.ComposerAgentProcInstaller,
            this.ComposerAgentInstaller});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller ComposerAgentProcInstaller;
        private System.ServiceProcess.ServiceInstaller ComposerAgentInstaller;
    }
}