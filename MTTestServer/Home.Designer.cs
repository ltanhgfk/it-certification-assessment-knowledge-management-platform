namespace ThiTracNghemUDCNTT
{
    partial class Home
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
            this.shapeContainer1 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.rectangleShape1 = new Microsoft.VisualBasic.PowerPacks.RectangleShape();
            this.btDethi = new System.Windows.Forms.Button();
            this.btThisinh = new System.Windows.Forms.Button();
            this.btAccount = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // shapeContainer1
            // 
            this.shapeContainer1.Location = new System.Drawing.Point(0, 0);
            this.shapeContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.shapeContainer1.Name = "shapeContainer1";
            this.shapeContainer1.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] {
            this.rectangleShape1});
            this.shapeContainer1.Size = new System.Drawing.Size(1030, 749);
            this.shapeContainer1.TabIndex = 0;
            this.shapeContainer1.TabStop = false;
            // 
            // rectangleShape1
            // 
            this.rectangleShape1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.rectangleShape1.BackColor = System.Drawing.Color.White;
            this.rectangleShape1.Location = new System.Drawing.Point(0, 1);
            this.rectangleShape1.Name = "rectangleShape1";
            this.rectangleShape1.Size = new System.Drawing.Size(157, 778);
            // 
            // btDethi
            // 
            this.btDethi.BackColor = System.Drawing.Color.Maroon;
            this.btDethi.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold);
            this.btDethi.ForeColor = System.Drawing.Color.White;
            this.btDethi.Location = new System.Drawing.Point(13, 134);
            this.btDethi.Name = "btDethi";
            this.btDethi.Size = new System.Drawing.Size(131, 62);
            this.btDethi.TabIndex = 1;
            this.btDethi.Text = "ĐỀ THI";
            this.btDethi.UseVisualStyleBackColor = false;
            this.btDethi.Click += new System.EventHandler(this.btDethi_Click);
            // 
            // btThisinh
            // 
            this.btThisinh.BackColor = System.Drawing.Color.Green;
            this.btThisinh.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold);
            this.btThisinh.ForeColor = System.Drawing.Color.White;
            this.btThisinh.Location = new System.Drawing.Point(12, 252);
            this.btThisinh.Name = "btThisinh";
            this.btThisinh.Size = new System.Drawing.Size(131, 62);
            this.btThisinh.TabIndex = 2;
            this.btThisinh.Text = "CHO THI";
            this.btThisinh.UseVisualStyleBackColor = false;
            this.btThisinh.Click += new System.EventHandler(this.btThisinh_Click);
            // 
            // btAccount
            // 
            this.btAccount.BackColor = System.Drawing.Color.Maroon;
            this.btAccount.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.btAccount.ForeColor = System.Drawing.Color.White;
            this.btAccount.Location = new System.Drawing.Point(13, 19);
            this.btAccount.Name = "btAccount";
            this.btAccount.Size = new System.Drawing.Size(131, 67);
            this.btAccount.TabIndex = 4;
            this.btAccount.Text = "QUẢN LÝ NGƯỜI DÙNG";
            this.btAccount.UseVisualStyleBackColor = false;
            this.btAccount.Click += new System.EventHandler(this.btAccount_Click);
            // 
            // Home
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ClientSize = new System.Drawing.Size(1030, 749);
            this.Controls.Add(this.btAccount);
            this.Controls.Add(this.btThisinh);
            this.Controls.Add(this.btDethi);
            this.Controls.Add(this.shapeContainer1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.IsMdiContainer = true;
            this.MaximizeBox = false;
            this.Name = "Home";
            this.Text = "MTTEST SOFTWARE";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Home_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Home_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer1;
        private Microsoft.VisualBasic.PowerPacks.RectangleShape rectangleShape1;
        private System.Windows.Forms.Button btDethi;
        private System.Windows.Forms.Button btThisinh;
        private System.Windows.Forms.Button btAccount;
    }
}

