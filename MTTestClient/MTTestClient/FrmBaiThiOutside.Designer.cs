namespace MTTestClient
{
    partial class FrmBaiThiOutside
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
            //if (disposing && (components != null))
            //{
            //    components.Dispose();
            //}
            //base.Dispose(disposing);

            if (disposing && (components != null))
            {

                components.Dispose();
            }
            if (ptrHook != System.IntPtr.Zero)
            {
                UnhookWindowsHookEx(ptrHook);
                ptrHook =System.IntPtr.Zero;
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmBaiThiOutside));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btThoat = new System.Windows.Forms.Button();
            this.btNopbai = new System.Windows.Forms.Button();
            this.lbThoigian = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbNoisinh = new System.Windows.Forms.Label();
            this.lbNgaysinh = new System.Windows.Forms.Label();
            this.lbSBD = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lbHoten = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.myTimer = new System.Windows.Forms.Timer(this.components);
            this.rtbDeThi = new System.Windows.Forms.RichTextBox();
            this.ZoomIn = new System.Windows.Forms.PictureBox();
            this.ZoomOut = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pnDiem = new System.Windows.Forms.Panel();
            this.lbDiem = new System.Windows.Forms.Label();
            this.pictArrow = new System.Windows.Forms.PictureBox();
            this.pnInfo = new System.Windows.Forms.Panel();
            this.lbthongbaonopbai = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dgPhieuTraLoi = new System.Windows.Forms.DataGridView();
            this.Cauhoi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.a = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.b = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.c = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.d = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ZoomIn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ZoomOut)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.pnDiem.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictArrow)).BeginInit();
            this.pnInfo.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgPhieuTraLoi)).BeginInit();
            this.SuspendLayout();
            // 
            // btThoat
            // 
            this.btThoat.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btThoat.Location = new System.Drawing.Point(116, 9);
            this.btThoat.Name = "btThoat";
            this.btThoat.Size = new System.Drawing.Size(91, 43);
            this.btThoat.TabIndex = 2;
            this.btThoat.Text = "Thoát";
            this.btThoat.UseVisualStyleBackColor = true;
            this.btThoat.Click += new System.EventHandler(this.btThoat_Click);
            // 
            // btNopbai
            // 
            this.btNopbai.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btNopbai.Location = new System.Drawing.Point(8, 9);
            this.btNopbai.Name = "btNopbai";
            this.btNopbai.Size = new System.Drawing.Size(91, 43);
            this.btNopbai.TabIndex = 21;
            this.btNopbai.Text = "Nộp bài";
            this.btNopbai.UseVisualStyleBackColor = true;
            this.btNopbai.Click += new System.EventHandler(this.btNopbai_Click);
            // 
            // lbThoigian
            // 
            this.lbThoigian.AutoSize = true;
            this.lbThoigian.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbThoigian.Font = new System.Drawing.Font("Times New Roman", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbThoigian.ForeColor = System.Drawing.Color.White;
            this.lbThoigian.Location = new System.Drawing.Point(63, 25);
            this.lbThoigian.Name = "lbThoigian";
            this.lbThoigian.Size = new System.Drawing.Size(118, 33);
            this.lbThoigian.TabIndex = 23;
            this.lbThoigian.Text = "09:09:09";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.lbNoisinh);
            this.panel1.Controls.Add(this.lbNgaysinh);
            this.panel1.Controls.Add(this.lbSBD);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.lbHoten);
            this.panel1.Location = new System.Drawing.Point(16, 244);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(217, 161);
            this.panel1.TabIndex = 24;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // lbNoisinh
            // 
            this.lbNoisinh.AutoSize = true;
            this.lbNoisinh.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbNoisinh.ForeColor = System.Drawing.Color.White;
            this.lbNoisinh.Location = new System.Drawing.Point(73, 132);
            this.lbNoisinh.Name = "lbNoisinh";
            this.lbNoisinh.Size = new System.Drawing.Size(72, 21);
            this.lbNoisinh.TabIndex = 6;
            this.lbNoisinh.Text = "Noi sinh";
            // 
            // lbNgaysinh
            // 
            this.lbNgaysinh.AutoSize = true;
            this.lbNgaysinh.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbNgaysinh.ForeColor = System.Drawing.Color.White;
            this.lbNgaysinh.Location = new System.Drawing.Point(85, 98);
            this.lbNgaysinh.Name = "lbNgaysinh";
            this.lbNgaysinh.Size = new System.Drawing.Size(89, 21);
            this.lbNgaysinh.TabIndex = 5;
            this.lbNgaysinh.Text = "Ngay sinh ";
            // 
            // lbSBD
            // 
            this.lbSBD.AutoSize = true;
            this.lbSBD.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbSBD.ForeColor = System.Drawing.Color.White;
            this.lbSBD.Location = new System.Drawing.Point(106, 66);
            this.lbSBD.Name = "lbSBD";
            this.lbSBD.Size = new System.Drawing.Size(41, 21);
            this.lbSBD.TabIndex = 4;
            this.lbSBD.Text = "Sbd";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(2, 132);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 21);
            this.label3.TabIndex = 3;
            this.label3.Text = "Nơi sinh:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(2, 98);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 21);
            this.label2.TabIndex = 2;
            this.label2.Text = "Ngày sinh:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(2, 66);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 21);
            this.label1.TabIndex = 1;
            this.label1.Text = "Số báo danh:";
            // 
            // lbHoten
            // 
            this.lbHoten.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbHoten.ForeColor = System.Drawing.Color.White;
            this.lbHoten.Location = new System.Drawing.Point(7, 6);
            this.lbHoten.Name = "lbHoten";
            this.lbHoten.Size = new System.Drawing.Size(201, 51);
            this.lbHoten.TabIndex = 0;
            this.lbHoten.Text = "Ho va ten";
            this.lbHoten.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.btNopbai);
            this.panel2.Controls.Add(this.btThoat);
            this.panel2.Location = new System.Drawing.Point(16, 417);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(217, 62);
            this.panel2.TabIndex = 25;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(30, 8);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(153, 152);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 22;
            this.pictureBox1.TabStop = false;
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.pictureBox1);
            this.panel3.Location = new System.Drawing.Point(16, 70);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(217, 168);
            this.panel3.TabIndex = 26;
            // 
            // myTimer
            // 
            this.myTimer.Tick += new System.EventHandler(this.myTimer_Tick);
            // 
            // rtbDeThi
            // 
            this.rtbDeThi.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rtbDeThi.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbDeThi.Location = new System.Drawing.Point(14, 19);
            this.rtbDeThi.Margin = new System.Windows.Forms.Padding(10);
            this.rtbDeThi.Name = "rtbDeThi";
            this.rtbDeThi.ReadOnly = true;
            this.rtbDeThi.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.rtbDeThi.Size = new System.Drawing.Size(803, 698);
            this.rtbDeThi.TabIndex = 0;
            this.rtbDeThi.Text = "";
            // 
            // ZoomIn
            // 
            this.ZoomIn.BackColor = System.Drawing.Color.White;
            this.ZoomIn.Image = ((System.Drawing.Image)(resources.GetObject("ZoomIn.Image")));
            this.ZoomIn.Location = new System.Drawing.Point(742, 721);
            this.ZoomIn.Name = "ZoomIn";
            this.ZoomIn.Size = new System.Drawing.Size(34, 30);
            this.ZoomIn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.ZoomIn.TabIndex = 19;
            this.ZoomIn.TabStop = false;
            this.ZoomIn.Click += new System.EventHandler(this.ZoomIn_Click);
            // 
            // ZoomOut
            // 
            this.ZoomOut.BackColor = System.Drawing.Color.White;
            this.ZoomOut.Image = ((System.Drawing.Image)(resources.GetObject("ZoomOut.Image")));
            this.ZoomOut.Location = new System.Drawing.Point(782, 721);
            this.ZoomOut.Name = "ZoomOut";
            this.ZoomOut.Size = new System.Drawing.Size(34, 30);
            this.ZoomOut.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.ZoomOut.TabIndex = 20;
            this.ZoomOut.TabStop = false;
            this.ZoomOut.Click += new System.EventHandler(this.ZoomOut_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.pnDiem);
            this.groupBox1.Controls.Add(this.pictArrow);
            this.groupBox1.Controls.Add(this.panel2);
            this.groupBox1.Controls.Add(this.pnInfo);
            this.groupBox1.Controls.Add(this.lbThoigian);
            this.groupBox1.Location = new System.Drawing.Point(0, -6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(249, 758);
            this.groupBox1.TabIndex = 28;
            this.groupBox1.TabStop = false;
            // 
            // pnDiem
            // 
            this.pnDiem.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnDiem.Controls.Add(this.lbDiem);
            this.pnDiem.Location = new System.Drawing.Point(16, 669);
            this.pnDiem.Name = "pnDiem";
            this.pnDiem.Size = new System.Drawing.Size(217, 69);
            this.pnDiem.TabIndex = 26;
            this.pnDiem.Visible = false;
            // 
            // lbDiem
            // 
            this.lbDiem.AutoSize = true;
            this.lbDiem.Font = new System.Drawing.Font("Times New Roman", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbDiem.ForeColor = System.Drawing.Color.Yellow;
            this.lbDiem.Location = new System.Drawing.Point(12, 16);
            this.lbDiem.MinimumSize = new System.Drawing.Size(190, 40);
            this.lbDiem.Name = "lbDiem";
            this.lbDiem.Size = new System.Drawing.Size(190, 40);
            this.lbDiem.TabIndex = 0;
            this.lbDiem.Text = "Diem";
            this.lbDiem.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbDiem.Visible = false;
            // 
            // pictArrow
            // 
            this.pictArrow.Image = ((System.Drawing.Image)(resources.GetObject("pictArrow.Image")));
            this.pictArrow.Location = new System.Drawing.Point(129, 481);
            this.pictArrow.Name = "pictArrow";
            this.pictArrow.Size = new System.Drawing.Size(100, 68);
            this.pictArrow.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictArrow.TabIndex = 24;
            this.pictArrow.TabStop = false;
            this.pictArrow.Visible = false;
            // 
            // pnInfo
            // 
            this.pnInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnInfo.Controls.Add(this.lbthongbaonopbai);
            this.pnInfo.Location = new System.Drawing.Point(16, 552);
            this.pnInfo.Name = "pnInfo";
            this.pnInfo.Size = new System.Drawing.Size(217, 109);
            this.pnInfo.TabIndex = 0;
            this.pnInfo.Visible = false;
            // 
            // lbthongbaonopbai
            // 
            this.lbthongbaonopbai.BackColor = System.Drawing.Color.Firebrick;
            this.lbthongbaonopbai.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbthongbaonopbai.ForeColor = System.Drawing.Color.White;
            this.lbthongbaonopbai.Location = new System.Drawing.Point(3, 3);
            this.lbthongbaonopbai.Name = "lbthongbaonopbai";
            this.lbthongbaonopbai.Size = new System.Drawing.Size(209, 101);
            this.lbthongbaonopbai.TabIndex = 0;
            this.lbthongbaonopbai.Text = "Bạn đã nộp bài, vui lòng nhấn nút thoát để kết thúc!";
            this.lbthongbaonopbai.UseVisualStyleBackColor = false;
            this.lbthongbaonopbai.Visible = false;
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox3.Controls.Add(this.dgPhieuTraLoi);
            this.groupBox3.Controls.Add(this.ZoomOut);
            this.groupBox3.Controls.Add(this.ZoomIn);
            this.groupBox3.Controls.Add(this.rtbDeThi);
            this.groupBox3.Location = new System.Drawing.Point(248, -5);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(1123, 751);
            this.groupBox3.TabIndex = 30;
            this.groupBox3.TabStop = false;
            // 
            // dgPhieuTraLoi
            // 
            this.dgPhieuTraLoi.AllowUserToAddRows = false;
            this.dgPhieuTraLoi.AllowUserToDeleteRows = false;
            this.dgPhieuTraLoi.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgPhieuTraLoi.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgPhieuTraLoi.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgPhieuTraLoi.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgPhieuTraLoi.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Cauhoi,
            this.a,
            this.b,
            this.c,
            this.d});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgPhieuTraLoi.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgPhieuTraLoi.Location = new System.Drawing.Point(817, 19);
            this.dgPhieuTraLoi.Name = "dgPhieuTraLoi";
            this.dgPhieuTraLoi.RowHeadersVisible = false;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgPhieuTraLoi.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgPhieuTraLoi.RowTemplate.Height = 30;
            this.dgPhieuTraLoi.Size = new System.Drawing.Size(293, 726);
            this.dgPhieuTraLoi.TabIndex = 1;
            this.dgPhieuTraLoi.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgPhieuTraLoi_CellClick);
            this.dgPhieuTraLoi.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgPhieuTraLoi_CellPainting);
            // 
            // Cauhoi
            // 
            this.Cauhoi.Frozen = true;
            this.Cauhoi.HeaderText = "Câu hỏi";
            this.Cauhoi.Name = "Cauhoi";
            this.Cauhoi.ReadOnly = true;
            // 
            // a
            // 
            this.a.FalseValue = "0";
            this.a.HeaderText = "A";
            this.a.Name = "a";
            this.a.TrueValue = "1";
            this.a.Width = 45;
            // 
            // b
            // 
            this.b.FalseValue = "0";
            this.b.HeaderText = "B";
            this.b.Name = "b";
            this.b.TrueValue = "2";
            this.b.Width = 45;
            // 
            // c
            // 
            this.c.FalseValue = "0";
            this.c.FillWeight = 200F;
            this.c.HeaderText = "C";
            this.c.Name = "c";
            this.c.TrueValue = "3";
            this.c.Width = 45;
            // 
            // d
            // 
            this.d.FalseValue = "0";
            this.d.HeaderText = "D";
            this.d.Name = "d";
            this.d.TrueValue = "4";
            this.d.Width = 45;
            // 
            // FrmBaiThiOutside
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.MidnightBlue;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1370, 745);
            this.ControlBox = false;
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.IsMdiContainer = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmBaiThiOutside";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BÀI THI";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmBaiThiOutside_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ZoomIn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ZoomOut)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.pnDiem.ResumeLayout(false);
            this.pnDiem.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictArrow)).EndInit();
            this.pnInfo.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgPhieuTraLoi)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btThoat;
        private System.Windows.Forms.Button btNopbai;
        private System.Windows.Forms.Label lbThoigian;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbHoten;
        private System.Windows.Forms.Label lbNoisinh;
        private System.Windows.Forms.Label lbNgaysinh;
        private System.Windows.Forms.Label lbSBD;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Timer myTimer;
        private System.Windows.Forms.RichTextBox rtbDeThi;
        private System.Windows.Forms.PictureBox ZoomIn;
        private System.Windows.Forms.PictureBox ZoomOut;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataGridView dgPhieuTraLoi;
        private System.Windows.Forms.Panel pnInfo;
        private System.Windows.Forms.PictureBox pictArrow;
        private System.Windows.Forms.Button lbthongbaonopbai;
        private System.Windows.Forms.Panel pnDiem;
        private System.Windows.Forms.Label lbDiem;
        private System.Windows.Forms.DataGridViewTextBoxColumn Cauhoi;
        private System.Windows.Forms.DataGridViewCheckBoxColumn a;
        private System.Windows.Forms.DataGridViewCheckBoxColumn b;
        private System.Windows.Forms.DataGridViewCheckBoxColumn c;
        private System.Windows.Forms.DataGridViewCheckBoxColumn d;
    }
}

