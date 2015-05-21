namespace salescampaignschedule.winser
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
            this.SalesCampaignScheduleServiceProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.SalesCampaignScheduleServiceInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // SalesCampaignScheduleServiceProcessInstaller
            // 
            this.SalesCampaignScheduleServiceProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.SalesCampaignScheduleServiceProcessInstaller.Password = null;
            this.SalesCampaignScheduleServiceProcessInstaller.Username = null;
            // 
            // SalesCampaignScheduleServiceInstaller
            // 
            this.SalesCampaignScheduleServiceInstaller.ServiceName = "Sales Campaign Schedule Service";
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.SalesCampaignScheduleServiceProcessInstaller,
            this.SalesCampaignScheduleServiceInstaller});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller SalesCampaignScheduleServiceProcessInstaller;
        private System.ServiceProcess.ServiceInstaller SalesCampaignScheduleServiceInstaller;
    }
}