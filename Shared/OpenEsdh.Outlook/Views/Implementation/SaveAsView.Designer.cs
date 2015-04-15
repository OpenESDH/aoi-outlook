namespace OpenEsdh.Outlook.Views.Implementation
{
    partial class SaveAsView
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.OpenEsdhBrowser = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // OpenEsdhBrowser
            // 
            this.OpenEsdhBrowser.CausesValidation = false;
            this.OpenEsdhBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.OpenEsdhBrowser.Location = new System.Drawing.Point(0, 0);
            this.OpenEsdhBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.OpenEsdhBrowser.Name = "OpenEsdhBrowser";
            this.OpenEsdhBrowser.Size = new System.Drawing.Size(1178, 536);
            this.OpenEsdhBrowser.TabIndex = 0;
            // 
            // SaveAsView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1178, 536);
            this.Controls.Add(this.OpenEsdhBrowser);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "SaveAsView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "OpenESDH";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SaveAsView_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser OpenEsdhBrowser;
    }
}