namespace TestService
{
    partial class ProjectInstaller
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.WEAVAOTInstaller1 = new System.ServiceProcess.ServiceProcessInstaller();
            this.WEAVAOTServiceInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // WEAVAOTInstaller1
            // 
            this.WEAVAOTInstaller1.Account = System.ServiceProcess.ServiceAccount.LocalService;
            this.WEAVAOTInstaller1.Password = null;
            this.WEAVAOTInstaller1.Username = null;
            // 
            // WEAVAOTServiceInstaller
            // 
            this.WEAVAOTServiceInstaller.ServiceName = "WEAVAOT";
            this.WEAVAOTServiceInstaller.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.WEAVAOTInstaller1,
            this.WEAVAOTServiceInstaller});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller WEAVAOTInstaller1;
        private System.ServiceProcess.ServiceInstaller WEAVAOTServiceInstaller;
    }
}