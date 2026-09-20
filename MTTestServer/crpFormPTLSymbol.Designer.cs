namespace ThiTracNghemUDCNTT
{
    partial class crpFormPTLSymbol
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
            this.crystalReportViewerPTLSymbol = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.CrystalReportInPTLSymbol1 = new ThiTracNghemUDCNTT.CrystalReportInPTLSymbol();
            this.SuspendLayout();
            // 
            // crystalReportViewerPTLSymbol
            // 
            this.crystalReportViewerPTLSymbol.ActiveViewIndex = 0;
            this.crystalReportViewerPTLSymbol.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crystalReportViewerPTLSymbol.Cursor = System.Windows.Forms.Cursors.Default;
            this.crystalReportViewerPTLSymbol.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crystalReportViewerPTLSymbol.Location = new System.Drawing.Point(0, 0);
            this.crystalReportViewerPTLSymbol.Name = "crystalReportViewerPTLSymbol";
            this.crystalReportViewerPTLSymbol.ReportSource = this.CrystalReportInPTLSymbol1;
            this.crystalReportViewerPTLSymbol.Size = new System.Drawing.Size(1225, 741);
            this.crystalReportViewerPTLSymbol.TabIndex = 0;
            // 
            // crpFormPTLSymbol
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1225, 741);
            this.Controls.Add(this.crystalReportViewerPTLSymbol);
            this.Name = "crpFormPTLSymbol";
            this.Text = "crpFormPTLSymbol";
            this.Load += new System.EventHandler(this.crpFormPTLSymbol_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private CrystalDecisions.Windows.Forms.CrystalReportViewer crystalReportViewerPTLSymbol;
        private CrystalReportInPTLSymbol CrystalReportInPTLSymbol1;
    }
}