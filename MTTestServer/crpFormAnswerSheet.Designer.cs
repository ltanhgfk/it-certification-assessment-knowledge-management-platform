namespace ThiTracNghemUDCNTT
{
    partial class crpFormAnswerSheet
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
            this.crystalReportViewerAnswerSheet = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.CrystalReportPrintAnswerSheet1 = new ThiTracNghemUDCNTT.CrystalReportPrintAnswerSheet();
            this.SuspendLayout();
            // 
            // crystalReportViewerAnswerSheet
            // 
            this.crystalReportViewerAnswerSheet.ActiveViewIndex = 0;
            this.crystalReportViewerAnswerSheet.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crystalReportViewerAnswerSheet.Cursor = System.Windows.Forms.Cursors.Default;
            this.crystalReportViewerAnswerSheet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crystalReportViewerAnswerSheet.Location = new System.Drawing.Point(0, 0);
            this.crystalReportViewerAnswerSheet.Name = "crystalReportViewerAnswerSheet";
            this.crystalReportViewerAnswerSheet.ReportSource = this.CrystalReportPrintAnswerSheet1;
            this.crystalReportViewerAnswerSheet.Size = new System.Drawing.Size(1225, 741);
            this.crystalReportViewerAnswerSheet.TabIndex = 0;
            // 
            // crpFormAnswerSheet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1225, 741);
            this.Controls.Add(this.crystalReportViewerAnswerSheet);
            this.Name = "crpFormAnswerSheet";
            this.Text = "IN PHIẾU TRẢ LỜI";
            this.Load += new System.EventHandler(this.crpFormAnswerSheet_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private CrystalDecisions.Windows.Forms.CrystalReportViewer crystalReportViewerAnswerSheet;
        private CrystalReportPrintAnswerSheet CrystalReportPrintAnswerSheet1;
    }
}