namespace OpenEsdh._2013.Outlook
{
    partial class OpenESDHRibbon : Microsoft.Office.Tools.Ribbon.RibbonBase
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public OpenESDHRibbon()
            : base(Globals.Factory.GetRibbonFactory())
        {
            InitializeComponent();
        }

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
            this.tab1 = this.Factory.CreateRibbonTab();
            this.group1 = this.Factory.CreateRibbonGroup();
            this.tab2 = this.Factory.CreateRibbonTab();
            this.group2 = this.Factory.CreateRibbonGroup();
            this.btnSaveFile = this.Factory.CreateRibbonButton();
            this.btnSaveAsSend = this.Factory.CreateRibbonButton();
            this.tab1.SuspendLayout();
            this.group1.SuspendLayout();
            this.tab2.SuspendLayout();
            this.group2.SuspendLayout();
            // 
            // tab1
            // 
            this.tab1.ControlId.ControlIdType = Microsoft.Office.Tools.Ribbon.RibbonControlIdType.Office;
            this.tab1.ControlId.OfficeId = "TabNewMailMessage";
            this.tab1.Groups.Add(this.group1);
            this.tab1.Label = "TabNewMailMessage";
            this.tab1.Name = "tab1";
            // 
            // group1
            // 
            this.group1.Items.Add(this.btnSaveFile);
            this.group1.Label = "Open Esdh";
            this.group1.Name = "group1";
            this.group1.Position = this.Factory.RibbonPosition.BeforeOfficeId("GroupSend");
            // 
            // tab2
            // 
            this.tab2.ControlId.ControlIdType = Microsoft.Office.Tools.Ribbon.RibbonControlIdType.Office;
            this.tab2.ControlId.OfficeId = "TabReadMessage";
            this.tab2.Groups.Add(this.group2);
            this.tab2.Label = "TabReadMessage";
            this.tab2.Name = "tab2";
            // 
            // group2
            // 
            this.group2.Items.Add(this.btnSaveAsSend);
            this.group2.Label = "Open Esdh";
            this.group2.Name = "group2";
            this.group2.Position = this.Factory.RibbonPosition.BeforeOfficeId("GroupRespond");
            // 
            // btnSaveFile
            // 
            this.btnSaveFile.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.btnSaveFile.Label = "Journalisér";
            this.btnSaveFile.Name = "btnSaveFile";
            this.btnSaveFile.ShowImage = true;
            this.btnSaveFile.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnSaveFile_Click);
            // 
            // btnSaveAsSend
            // 
            this.btnSaveAsSend.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.btnSaveAsSend.Label = "Journalisér";
            this.btnSaveAsSend.Name = "btnSaveAsSend";
            this.btnSaveAsSend.ShowImage = true;
            this.btnSaveAsSend.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnSaveAsSend_Click);
            // 
            // OpenESDHRibbon
            // 
            this.Name = "OpenESDHRibbon";
            this.RibbonType = "Microsoft.Outlook.Mail.Compose, Microsoft.Outlook.Mail.Read";
            this.Tabs.Add(this.tab1);
            this.Tabs.Add(this.tab2);
            this.Load += new Microsoft.Office.Tools.Ribbon.RibbonUIEventHandler(this.OpenESDHRibbon_Load);
            this.tab1.ResumeLayout(false);
            this.tab1.PerformLayout();
            this.group1.ResumeLayout(false);
            this.group1.PerformLayout();
            this.tab2.ResumeLayout(false);
            this.tab2.PerformLayout();
            this.group2.ResumeLayout(false);
            this.group2.PerformLayout();

        }

        #endregion

        internal Microsoft.Office.Tools.Ribbon.RibbonTab tab1;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup group1;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnSaveFile;
        private Microsoft.Office.Tools.Ribbon.RibbonTab tab2;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup group2;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnSaveAsSend;
    }

    partial class ThisRibbonCollection
    {
        internal OpenESDHRibbon OpenESDHRibbon
        {
            get { return this.GetRibbon<OpenESDHRibbon>(); }
        }
    }
}
