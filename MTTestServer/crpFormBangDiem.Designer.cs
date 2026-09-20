namespace ThiTracNghemUDCNTT
{
    partial class crpFormBangDiem
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
            this.crystalReportViewerDiemThi = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.CrystalReportInDiemThi1 = new ThiTracNghemUDCNTT.CrystalReportInDiemThi();
            this.SuspendLayout();
            // 
            // crystalReportViewerDiemThi
            // 
            this.crystalReportViewerDiemThi.ActiveViewIndex = 0;
            this.crystalReportViewerDiemThi.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crystalReportViewerDiemThi.Cursor = System.Windows.Forms.Cursors.Default;
            this.crystalReportViewerDiemThi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crystalReportViewerDiemThi.Location = new System.Drawing.Point(0, 0);
            this.crystalReportViewerDiemThi.Name = "crystalReportViewerDiemThi";
            this.crystalReportViewerDiemThi.ReportSource = this.CrystalReportInDiemThi1;
            this.crystalReportViewerDiemThi.Size = new System.Drawing.Size(1225, 741);
            this.crystalReportViewerDiemThi.TabIndex = 1;
            this.crystalReportViewerDiemThi.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
            // 
            // crpFormBangDiem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1225, 741);
            this.Controls.Add(this.crystalReportViewerDiemThi);
            this.IsMdiContainer = true;
            this.Name = "crpFormBangDiem";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "KẾT QUẢ THI";
            this.Load += new System.EventHandler(this.crpForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private CrystalDecisions.Windows.Forms.CrystalReportViewer crystalReportViewerDiemThi;
        private CrystalReportInDiemThi CrystalReportInDiemThi1;



    }
}