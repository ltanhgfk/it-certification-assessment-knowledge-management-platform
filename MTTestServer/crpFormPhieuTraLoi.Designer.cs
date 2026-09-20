namespace ThiTracNghemUDCNTT
{
    partial class crpFormPhieuTraLoi
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
            this.crystalReportViewerPhieuTraLoi = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.CrystalReportInPhieuTraLoi1 = new ThiTracNghemUDCNTT.CrystalReportInPhieuTraLoi();
            this.SuspendLayout();
            // 
            // crystalReportViewerPhieuTraLoi
            // 
            this.crystalReportViewerPhieuTraLoi.ActiveViewIndex = 0;
            this.crystalReportViewerPhieuTraLoi.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crystalReportViewerPhieuTraLoi.Cursor = System.Windows.Forms.Cursors.Default;
            this.crystalReportViewerPhieuTraLoi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crystalReportViewerPhieuTraLoi.Location = new System.Drawing.Point(0, 0);
            this.crystalReportViewerPhieuTraLoi.Name = "crystalReportViewerPhieuTraLoi";
            this.crystalReportViewerPhieuTraLoi.ReportSource = this.CrystalReportInPhieuTraLoi1;
            this.crystalReportViewerPhieuTraLoi.Size = new System.Drawing.Size(1225, 741);
            this.crystalReportViewerPhieuTraLoi.TabIndex = 0;
            this.crystalReportViewerPhieuTraLoi.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
            // 
            // crpFormPhieuTraLoi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1225, 741);
            this.Controls.Add(this.crystalReportViewerPhieuTraLoi);
            this.Name = "crpFormPhieuTraLoi";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PHIẾU TRẢ LỜI TRẮC NGHIỆM";
            this.Load += new System.EventHandler(this.crpFormPhieuTraLoi_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private CrystalDecisions.Windows.Forms.CrystalReportViewer crystalReportViewerPhieuTraLoi;
        private CrystalReportInPhieuTraLoi CrystalReportInPhieuTraLoi1;


    }
}