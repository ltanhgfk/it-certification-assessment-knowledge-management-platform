using System;
//using System.Collections.Generic;
//using System.ComponentModel;
using System.Data;
//using System.Drawing;
using System.IO;
//using System.Linq;
//using System.Text;
using System.Windows.Forms;
//using Microsoft.Office.Interop.Word;
//using System.Drawing.Drawing2D;
using RTF;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace MTTestClient
{
    public partial class FrmBaiThiOutside : Form
    {
        frmThongTin _frmThongtin;
        public string sbdbaithi;
        public int tinhtrang;
        public string hoten;
        public string ngaysinh;
        public string noisinh;
        //public string degoc;
        //public string made;
        public int thoigian_bandau;
        public int thoigian_ts;
        //double Diem = 0.0;
        public string monthi = "";
        //System.Timers.Timer t;
        //int h, m, s;

        int thoigian = 0;//number of seconds of time

        // Structure contain information about low-level keyboard input event
        [StructLayout(LayoutKind.Sequential)]
        private struct KBDLLHOOKSTRUCT
        {
            public Keys key;
            public int scanCode;
            public int flags;
            public int time;
            public IntPtr extra;
        }

        //System level functions to be used for hook and unhook keyboard input
        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int id, LowLevelKeyboardProc callback, IntPtr hMod, uint dwThreadId);
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool UnhookWindowsHookEx(IntPtr hook);
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hook, int nCode, IntPtr wp, IntPtr lp);
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string name);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern short GetAsyncKeyState(Keys key);


        //Declaring Global objects
        private IntPtr ptrHook;
        private LowLevelKeyboardProc objKeyboardProcess;
        //private System.Timers.ElapsedEventHandler OnTimeEvent;
        // AUTOCAD_03;1#AUTOCAD_14;0#AUTOCAD_06;3#AUTOCAD_13;2#AUTOCAD_02;3#AUTOCAD_10;0#AUTOCAD_09;3#AUTOCAD_01;0#AUTOCAD_04;2#AUTOCAD_17;3#
        //AUTOCAD_01;4#AUTOCAD_04;4#AUTOCAD_13;0#AUTOCAD_17;4#AUTOCAD_02;4#AUTOCAD_06;2#AUTOCAD_10;4#AUTOCAD_14;4#AUTOCAD_03;2#AUTOCAD_09;4#
        public FrmBaiThiOutside(frmThongTin frmTT)
        {
            ProcessModule objCurrentModule = Process.GetCurrentProcess().MainModule; //Get Current Module
            objKeyboardProcess = new LowLevelKeyboardProc(captureKey); //Assign callback function each time keyboard process
            ptrHook = SetWindowsHookEx(13, objKeyboardProcess, GetModuleHandle(objCurrentModule.ModuleName), 0); //Setting Hook of Keyboard Process for current module

            InitializeComponent();
            _frmThongtin = frmTT;
        }

        private IntPtr captureKey(int nCode, IntPtr wp, IntPtr lp)
        {
            if (nCode >= 0)
            {
                KBDLLHOOKSTRUCT objKeyInfo = (KBDLLHOOKSTRUCT)Marshal.PtrToStructure(lp, typeof(KBDLLHOOKSTRUCT));

                if (objKeyInfo.key == Keys.RWin || objKeyInfo.key == Keys.LWin || objKeyInfo.key == Keys.Alt || objKeyInfo.key == Keys.Tab || objKeyInfo.key == Keys.F4) // Disabling Windows keys
                {
                    return (IntPtr)1;
                }
            }
            return CallNextHookEx(ptrHook, nCode, wp, lp);
        }

        private void FrmBaiThiOutside_Load(object sender, EventArgs e)
        {
            this.Text = "BÀI THI MÔN: " + monthi;
            loadThongtinThisinh();
            loadDethi();
            var startTime = DateTime.Now;
            myTimer.Interval = 1000;
            myTimer.Tick += (obj, args) => lbThoigian.Text = (TimeSpan.FromSeconds(thoigian) - (DateTime.Now - startTime)).ToString("hh\\:mm\\:ss");
            myTimer.Enabled = true;
        }

        //private void FrmBaiThiOutside_Load(object sender, EventArgs e)
        //{
        //    //t = new System.Timers.Timer();
        //    //t.Interval = 10;
        //    //t.Elapsed += OnTimeEvent;
        //    //t.Start();                       
        //    this.Text = "BÀI THI MÔN: " + monthi;

        //    loadThongtinThisinh();
        //    loadDethi();

        //    //timer1 = new System.Windows.Forms.Timer();
        //    //timer1.Interval = 1000;
        //    //timer1.Tick += new EventHandler(timer1_Tick);
        //    //timer1.Enabled = true;

        //    var startTime = DateTime.Now;
        //    myTimer.Interval = 1000;
        //    myTimer.Tick += (obj, args) => lbThoigian.Text = (TimeSpan.FromSeconds(thoigian) - (DateTime.Now - startTime)).ToString("hh\\:mm\\:ss");
        //    myTimer.Enabled = true;
        //}

        //private void timer1_Tick(object sender, EventArgs e)
        //{
        //    thoigian_ts--;
        //    txtResult.Text = thoigian_ts / 60 + ":" + ((thoigian_ts % 60) >= 10 ? (thoigian_ts % 60).ToString() : "0" + (thoigian_ts % 60));
        //}

        //private void OnTimeEvent(Object sender, System.Timers.ElapsedEventArgs e)
        //{
        //    Invoke(new Action(() =>
        //        {
        //            if (s == 60)
        //            {
        //                s = 0;
        //                m += 1;
        //            }
        //            if (m == 60)
        //            {
        //                m = 0;
        //                h += 1;
        //            }
        //            txtResult.Text = string.Format("{0}:{1}:{2}", h.ToString().PadLeft(2, '0'), m.ToString().PadLeft(2, '0'), s.ToString().PadLeft(2, '0'));
        //        }));
        //}

        private void myTimer_Tick(object sender, EventArgs e)
        {
            if (lbThoigian.Text == "00:00:00")
            {
                myTimer.Stop();
                chamdiem();
                MessageBox.Show("Hết giờ làm bài!");
                lbThoigian.Text = "00:00:00";
            }
        }

        public void loadThongtinThisinh()
        {
            lbHoten.Text = hoten;
            lbSBD.Text = sbdbaithi;
            lbNgaysinh.Text = ngaysinh;
            lbNoisinh.Text = noisinh;
        }

        public string TaoLaiKetCauDe(string ketcaudecu)
        {
            string ketcaudemoi = "";
            string cauhoi = "";
            string[] mangdegoc;
            mangdegoc = ketcaudecu.Split('#');
            for (int i = 0; i < mangdegoc.Length - 1; i++)
            {
                cauhoi = mangdegoc[i].Substring(0, mangdegoc[i].IndexOf(";"));
                ketcaudemoi = ketcaudemoi + cauhoi + ";" + "0" + "#";
            }
            return ketcaudemoi.Trim();
        }

        public void loadDethi()
        {
            //int thoigian_ts = 0;
            RTFBuilderbase sb1 = new RTFBuilder();
            if (tinhtrang == 2)//neu thi sinh chua thi ==0
            {
                //Khong  can cai nay vi ben thongtin da co =>>thoigian_ts = (int)Database.ExecuteScalar("Select ThoiGianCL from Thisinh where SBD=@sbd","@sbd",sbdbaithi);
                if (thoigian_ts >= 1)//neu la thi sinh nhan nop bai roi nhung cho thi lai
                {
                    string ketcaude = "";
                    string bailam_ts = "";
                    string sttstr = "";
                    string monthi="";
                    System.Data.DataTable dt = Database.GetData("Select * from Thisinh_BaithiNC where SBD=@sbd", "@sbd", sbdbaithi);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ketcaude = ketcaude + (string)Database.ExecuteScalar("Select KetCau_De from DeThi where MaDe=@made", "@made", dt.Rows[i]["MaDe"].ToString());//tai vi Globals.DisplayDethiOnRtf(sb1, ketcaude);  chi hieu ket cau de cu                        
                        //bailam_ts = bailam_ts + dt.Rows[i]["BaiLam"].ToString();
                        if (dt.Rows[i]["MaMonThi"].ToString() == "ACCESS")
                            monthi = "SỬ DỤNG HỆ QUẢN TRỊ CSDL";
                        else if (dt.Rows[i]["MaMonThi"].ToString() == "EXCEL")
                            monthi = "SỬ DỤNG BẢNG TÍNH NÂNG CAO";
                        else if (dt.Rows[i]["MaMonThi"].ToString() == "WORD")
                            monthi = "XỬ LÝ VĂN BẢN NÂNG CAO";
                        else monthi = "";

                        string stt = (ketcaude.Split('#').Length - 1) + "-" + monthi;
                        sttstr = sttstr + stt + "_";
                    }
                    Globals.DisplayDethiOnRtf(sb1, ketcaude, sttstr);
                    this.rtbDeThi.Rtf = sb1.ToString();
                    bailam_ts = (string)Database.ExecuteScalar("Select BaiLam from Thisinh where SBD=@sbd", "@sbd", sbdbaithi);
                    string[] mangketcaudethi = ketcaude.Split('#');
                    loadDgPhieuTraLoi(mangketcaudethi.Length - 1, bailam_ts);
                    thoigian = thoigian_ts;
                    Database.ExecuteNonQuery("Update Thisinh set Tinhtrang=@tinhtrang where SBD=@sbd", "@tinhtrang", 2, "@sbd", sbdbaithi);
                }
                else//Neu la thi sinh chua thi that su
                {
                    string ketcaubailam = "";
                    string ketcaude = "";
                    string bailam ="";
                    string mamonthi = "";
                    string sttstr = "";
                    System.Data.DataTable dtDethi = new System.Data.DataTable();
                    System.Data.DataTable dt = Database.GetData("Select * from Kithi where Phongthi = (Select Lop from Thisinh where SBD=@sbd)", "@sbd", sbdbaithi);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        dtDethi = Database.GetData("Select * from DeThi where Degoc=@degoc ORDER BY NEWID()", "@degoc", dt.Rows[i]["Degoc"].ToString());
                        bailam = TaoLaiKetCauDe(dtDethi.Rows[0]["KetCau_De"].ToString());
                        ketcaubailam = ketcaubailam + bailam;
                        ketcaude = ketcaude + dtDethi.Rows[0]["ketCau_De"].ToString();
                        mamonthi = dtDethi.Rows[0]["MaDe"].ToString().Substring(0, dtDethi.Rows[0]["MaDe"].ToString().IndexOf("_"));
                        //Database.ExecuteNonQuery("Insert into Thisinh_BaithiNC(SBD,DiemThi,MaDe,BaiLam,len) values(@sbd,@diemthi,@made,@bailam,@len)", "@sbd", sbdbaithi, "@diemthi", 0, "@made", dtDethi.Rows[0]["MaDe"].ToString(), "@bailam", "", "@len", bailam.Length);                       
                        Database.ExecuteNonQuery("Update Thisinh_BaithiNC set MaDe=@made,len=@len, Position=@pos where SBD=@sbd and MaMonThi = @mamonthi", "@sbd", sbdbaithi, "@made", dtDethi.Rows[0]["MaDe"].ToString(), "@len", bailam.Length, "@mamonthi", mamonthi, "@pos", i);
                        //sb1.AppendLine("PHẦN CÂU HỎI MÔN: " + dt.Rows[i]["MonThi"].ToString());
                        string stt = (ketcaude.Split('#').Length - 1) + "-" + dt.Rows[i]["MonThi"].ToString();
                        sttstr = sttstr + stt + "_";
                    }
                    Globals.DisplayDethiOnRtf(sb1, ketcaude, sttstr);  
                    this.rtbDeThi.Rtf = sb1.ToString();
                    thoigian = thoigian_bandau;
                    string[] mangketcaudethi = ketcaude.Split('#');
                    //string[] mangdegoc = dtDethi.Rows[0]["KetCau_De"].ToString().Split('#');
                    loadDgPhieuTraLoi(mangketcaudethi.Length - 1, "");

                    Database.ExecuteNonQuery("Update Thisinh set Tinhtrang=@tinhtrang,MaDe=@made,Bailam=@bailam,ThoigianKT=@thoigiankt,ThoiGianCL=@tgconlai where SBD=@sbd", "@tinhtrang", 2, "@made", "", "@bailam", ketcaubailam, "@thoigiankt", thoigian_bandau, "@tgconlai", thoigian_bandau, "@sbd", sbdbaithi);
                }
            }
            else MessageBox.Show("Số báo danh này không vào thi được, vui lòng liên hệ giám thị!");
            //else if (tinhtrang == 2)//neu thi sinh dang thi bi out ra
            //{
            //    DialogResult response = MessageBox.Show("Bạn chưa hoàn thành bài làm, tiếp tục làm?", "Vào thi",
            //                      MessageBoxButtons.YesNo,
            //                      MessageBoxIcon.Question,
            //                      MessageBoxDefaultButton.Button2);
            //    if (response == DialogResult.Yes)
            //    {
            //        string ketcaude = (string)Database.ExecuteScalar("Select KetCau_De from DeThi where MaDe=@made", "@made", made);
            //        Globals.DisplayDethiOnRtf(sb1, ketcaude);
            //        this.rtbDeThi.Rtf = sb1.ToString();
            //        thoigian = thoigian_ts;
            //        string[] mangdegoc = ketcaude.Split('#');
            //        string bailam_ts = (string)Database.ExecuteScalar("Select BaiLam from Thisinh where SBD=@sbd", "@sbd", sbdbaithi);
            //        loadDgPhieuTraLoi(mangdegoc.Length - 1, bailam_ts);
            //    }
            //}
        }

        public void loadDgPhieuTraLoi(int len, string bailam)
        {
            int j = 0;
            if (bailam != "")
            {
                string[] mangbailam = bailam.Split('#');
                for (int i = 0; i < len; i++)
                {
                    j = i + 1;
                    dgPhieuTraLoi.Rows.Add();
                    dgPhieuTraLoi.Rows[i].Cells["Cauhoi"].Value = "Câu " + j.ToString("00");
                    //if (mangbailam[i].Substring(mangbailam[i].IndexOf(";") + 1, 1) == "0")
                    //dgPhieuTraLoi.Rows[i].Cells[i].Value = false;
                    if (mangbailam[i].Substring(mangbailam[i].IndexOf(";") + 1, 1) == "1")
                        dgPhieuTraLoi.Rows[i].Cells[1].Value = true;
                    else if (mangbailam[i].Substring(mangbailam[i].IndexOf(";") + 1, 1) == "2")
                        dgPhieuTraLoi.Rows[i].Cells[2].Value = true;
                    else if (mangbailam[i].Substring(mangbailam[i].IndexOf(";") + 1, 1) == "3")
                        dgPhieuTraLoi.Rows[i].Cells[3].Value = true;
                    else if (mangbailam[i].Substring(mangbailam[i].IndexOf(";") + 1, 1) == "4")
                        dgPhieuTraLoi.Rows[i].Cells[4].Value = true;
                }
            }
            else
            {
                for (int i = 0; i < len; i++)
                {
                    j = i + 1;
                    dgPhieuTraLoi.Rows.Add();
                    dgPhieuTraLoi.Rows[i].Cells["Cauhoi"].Value = "Câu " + j.ToString("00");
                }
            }
        }

        private void ZoomIn_Click(object sender, EventArgs e)
        {
            rtbDeThi.ZoomFactor = rtbDeThi.ZoomFactor + 0.2f;
        }

        private void ZoomOut_Click(object sender, EventArgs e)
        {
            rtbDeThi.ZoomFactor = rtbDeThi.ZoomFactor - 0.2f;
        }

        private void dgPhieuTraLoi_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string[] mangbailam;
            string answer = "0";
            string strNew = "";
            if (e.ColumnIndex > 0)
            {
                if (Convert.ToBoolean(dgPhieuTraLoi.Rows[e.RowIndex].Cells[e.ColumnIndex].FormattedValue) == false)
                {
                    //MessageBox.Show("Row: " + e.RowIndex + ", Column: " + e.ColumnIndex);
                    //MessageBox.Show("Cell: " + dgPhieuTraLoi.Rows[e.RowIndex].Cells[e.ColumnIndex].FormattedValue.ToString());
                    for (int i = 1; i <= dgPhieuTraLoi.Columns.Count - 1; i++)
                    {
                        dgPhieuTraLoi.Rows[e.RowIndex].Cells[i].Value = false;
                    }
                }
                dgPhieuTraLoi.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = true;

                string bailam = (string)Database.ExecuteScalar("Select Bailam from Thisinh where SBD=@sbd", "@sbd", sbdbaithi);
                mangbailam = bailam.Split('#');
                for (int i = 1; i < dgPhieuTraLoi.Columns.Count; i++)
                {
                    if (Convert.ToBoolean(dgPhieuTraLoi.Rows[e.RowIndex].Cells[i].FormattedValue) == true)
                    {
                        answer = i.ToString();
                    }
                }
                mangbailam[e.RowIndex] = mangbailam[e.RowIndex].Substring(0, mangbailam[e.RowIndex].IndexOf(";")) + ";" + answer;

                for (int i = 0; i < mangbailam.Length - 1; i++)
                {
                    strNew = strNew + mangbailam[i].Trim() + "#";
                }
                Database.ExecuteNonQuery("Update Thisinh set Bailam=@bailam, ThoiGianCL=@tgconlai where SBD=@sbd", "@bailam", strNew, "@tgconlai", ConvertTextToSeconds(lbThoigian.Text.Trim()), "@sbd", sbdbaithi);
            }
            //else
            //{
            //    MessageBox.Show("Không click vào tiêu đề làm gì!");
            //}
        }

        private int ConvertTextToSeconds(string time)
        {
            int hour = Convert.ToInt32(time.Substring(0, 2));
            int minute = Convert.ToInt32(time.Substring(3, 2));
            int second = Convert.ToInt32(time.Substring(6, 2));
            return (hour * 3600 + minute * 60 + second);
        }

        private void dgPhieuTraLoi_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            ////Khuc nay la bien doi Checkbox thanh radiobutton
            ////if (e.ColumnIndex == dgPhieuTraLoi.Columns["A1"].Index && e.RowIndex >= 0)
            //    if (e.ColumnIndex >= 1 && e.RowIndex >= 0)
            //    {
            //        e.PaintBackground(e.ClipBounds, true);

            //        System.Drawing.Rectangle rectRadioButton = new System.Drawing.Rectangle();

            //        rectRadioButton.Width = 24;
            //        rectRadioButton.Height = 24;
            //        rectRadioButton.X = e.CellBounds.X + (e.CellBounds.Width - rectRadioButton.Width) / 2;
            //        rectRadioButton.Y = e.CellBounds.Y + (e.CellBounds.Height - rectRadioButton.Height) / 2;
            //        //ControlPaint.DrawRadioButton(e.Graphics, rectRadioButton, ButtonState.Checked);

            //        ControlPaint.DrawRadioButton(e.Graphics, rectRadioButton, ButtonState.Normal);

            //        if (e.FormattedValue == DBNull.Value || (bool)e.FormattedValue ==false)
            //        {
            //            ControlPaint.DrawRadioButton(e.Graphics, rectRadioButton, ButtonState.Normal);
            //        }
            //        else
            //        {
            //            ControlPaint.DrawRadioButton(e.Graphics, rectRadioButton, ButtonState.Checked);
            //        }
            //        e.Paint(e.ClipBounds, DataGridViewPaintParts.Focus);
            //        e.Handled = true;
            //    }

            //Khuc nay la thay doi size cua checkbox
            if (e.ColumnIndex >= 1 && e.RowIndex >= 0)
            {
                e.PaintBackground(e.CellBounds, true);
                ControlPaint.DrawCheckBox(e.Graphics, e.CellBounds.X + 3, e.CellBounds.Y + 3, e.CellBounds.Width - 8, e.CellBounds.Height - 8, (bool)e.FormattedValue ? ButtonState.Checked : ButtonState.Normal);
                e.Handled = true;
            }
        }

        private void btThoat_Click(object sender, EventArgs e)
        {
            int Tinhtrang = (int)Database.ExecuteScalar("Select Tinhtrang from Thisinh where SBD=@sbd", "@sbd", sbdbaithi);
            if (Tinhtrang == 1)
            {
                DialogResult response = MessageBox.Show("Bạn có chắc chắn muốn thoát!", "Thoát bài thi",
                              MessageBoxButtons.YesNo,
                              MessageBoxIcon.Question,
                              MessageBoxDefaultButton.Button2);
                if (response == DialogResult.Yes)
                {
                    _frmThongtin.Dispose();
                    _frmThongtin.Close();
                    this.Dispose();
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Bạn phải nộp bài trước khi thoát");
            }
        }
        //AUTOCAD_03;1;2;3;4;4;4;0#AUTOCAD_02;4;3;2;1;1;4;1#AUTOCAD_01;3;1;2;4;1;4;1#AUTOCAD_09;2;1;3;4;2;4;1#AUTOCAD_14;1;2;3;4;1;4;1#AUTOCAD_17;1;2;4;3;4;4;1#AUTOCAD_10;1;2;4;3;4;4;1#AUTOCAD_04;2;1;3;4;4;4;1#AUTOCAD_06;2;4;1;3;3;4;1#AUTOCAD_13;3;4;2;1;2;4;1#
        //AUTOCAD_17;2#AUTOCAD_02;2#AUTOCAD_04;2#AUTOCAD_09;2#AUTOCAD_13;2#AUTOCAD_03;2#AUTOCAD_10;2#AUTOCAD_06;2#AUTOCAD_14;2#AUTOCAD_01;2#

        public double TinhDiemThi()
        {
            string[] mangbailam;
            string[] mangDethi;
            int socaudung = 0;
            int tongsocau = 0;
            string trueanswer = "";
            string answer = "";

            double diemTB = 0.0;
            double diemtp = 0.0;
            System.Data.DataTable dtThisinh = Database.GetData("Select * from Thisinh_BaithiNC where SBD=@sbd order by Position", "@sbd", sbdbaithi);
            for (int j = 0; j < dtThisinh.Rows.Count; j++)
            {
                System.Data.DataTable dtDethi = Database.GetData("Select * from DeThi where MaDe=@made", "@made", dtThisinh.Rows[j]["MaDe"].ToString());

                mangbailam = dtThisinh.Rows[j]["Bailam"].ToString().Split('#');
                tongsocau = mangbailam.Length - 1;
                mangDethi = dtDethi.Rows[0]["KetCau_De"].ToString().Split('#');// loi cho nay
                for (int i = 0; i < tongsocau; i++)
                {
                    trueanswer = mangDethi[i].Substring(mangDethi[i].IndexOf(';') + 9, 1);
                    answer = mangbailam[i].Substring(mangbailam[i].IndexOf(';') + 1, 1);
                    if (trueanswer == answer)
                    {
                        socaudung = socaudung + 1;
                    }
                }
                //lbsocaudung.Text = socaudung + "/" + tongsocau;
                diemtp = (double)(10.0 * (double)socaudung) / (double)tongsocau;
                //Diem = Math.Round(diemthi, 1);
                Database.ExecuteNonQuery("Update Thisinh_BaithiNC set DiemThi=@diemthi where SBD=@sbd and MaDe=@made", "@diemthi", Math.Round(diemtp,1), "@sbd", sbdbaithi, "@made", dtThisinh.Rows[j]["MaDe"].ToString());
                diemTB = diemTB + diemtp;
                socaudung = 0;
            }

            diemTB = (double)(diemTB / (double)dtThisinh.Rows.Count);
            return Math.Round(diemTB, 1);
        }

        public void Capnhatbaithithanhphan()
        {
            string bailam_part = "";
            string made="";
            string bailam_total = (string)Database.ExecuteScalar("Select BaiLam from Thisinh where SBD=@sbd", "@sbd", sbdbaithi);
            System.Data.DataTable dt = Database.GetData("Select * from Thisinh_BaithiNC where SBD=@sbd order by Position asc", "@sbd", sbdbaithi);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                made = dt.Rows[i]["MaDe"].ToString();
                bailam_part = bailam_total.Substring(0, Convert.ToInt32(dt.Rows[i]["len"])).Trim();
                bailam_total = bailam_total.Substring(Convert.ToInt32(dt.Rows[i]["len"]), bailam_total.Length - Convert.ToInt32(dt.Rows[i]["len"])).Trim();

                Database.ExecuteNonQuery("Update Thisinh_BaithiNC set BaiLam=@bailam where SBD=@sbd and MaDe=@made", "@bailam", bailam_part, "@sbd", sbdbaithi, "@made", made);
            }
        }

        public bool chamdiem()
        {
            double DiemTB = 0.0;
            myTimer.Stop();
            myTimer.Enabled = false;

            Capnhatbaithithanhphan();
            DiemTB = TinhDiemThi();
            
            Database.ExecuteNonQuery("Update Thisinh set Tinhtrang=@tinhtrang, DiemThi=@diemthi, ThoiGianCL=@thoigianconlai where SBD=@sbd", "@tinhtrang", 1, "@diemthi",DiemTB, "@thoigianconlai", ConvertTextToSeconds(lbThoigian.Text.Trim()), "@sbd", sbdbaithi);

            /////////////////////////////////////////
            lbthongbaonopbai.Text = "Bạn đã nộp bài, vui lòng nhấn nút thoát để kết thúc!";
            pnInfo.Visible = true;
            lbthongbaonopbai.Visible = true;
            pictArrow.Visible = true;

            string showscore = (string)Database.ExecuteScalar("Select Variable from tblConfig where Name=@name","@name","showscore");
            if (showscore == "dung")
            {
                pnDiem.Visible = true;
                lbDiem.Text = "Điểm: " + string.Format("{0:N1}", DiemTB);
                lbDiem.Visible = true;
            }
            /////////////////////////////////////////

            //lbDiem.Text = TinhDiemThi().ToString();
            btNopbai.Enabled = false;
            dgPhieuTraLoi.Enabled = false;
            return true;
        }

        int dem_so_cau_hoi_chua_tra_loi()
        {
            int sl = 0;
            int tsl = 0;
            foreach (DataGridViewRow row in dgPhieuTraLoi.Rows)
            {
                for (int i = 1; i < dgPhieuTraLoi.Columns.Count; i++)
                {
                    if (Convert.ToBoolean(row.Cells[i].FormattedValue) == true)
                    {
                        sl = sl + 1;
                    }
                }
                tsl = tsl + 1;
            }
            return (tsl - sl);
        }

        private void btNopbai_Click(object sender, EventArgs e)
        {
            string warn = "";
            if (dem_so_cau_hoi_chua_tra_loi() >= 1)
            {
                warn = "Còn " + dem_so_cau_hoi_chua_tra_loi() + " câu hỏi chưa trả lời, bạn có chắc chắn muốn nộp bài!";
            }
            else warn = "Bạn có chắc chắn muốn nộp bài!";
            int Tinhtrang = (int)Database.ExecuteScalar("Select Tinhtrang from Thisinh where SBD=@sbd", "@sbd", sbdbaithi);
            if (Tinhtrang != 1)
            {
                DialogResult response = MessageBox.Show(warn, "Nộp bài thi",
                                  MessageBoxButtons.YesNo,
                                  MessageBoxIcon.Question,
                                  MessageBoxDefaultButton.Button2);
                if (response == DialogResult.Yes)
                {
                    if (chamdiem())
                    {
                        MessageBox.Show("Bạn đã nộp bài thành công, vui lòng nhấn nút Thoát để kết thúc!");
                    }
                    else MessageBox.Show("Bạn đã nộp bài thành công, vui lòng nhấn nút Thoát để kết thúc!!!");
                }
            }
            else
            {
                MessageBox.Show("Bạn đã nộp bài rồi, vui lòng nhấn nút Thoát để kết thúc!");
                btNopbai.Enabled = false;
                dgPhieuTraLoi.Enabled = false;
            }
            //Database.ExecuteNonQuery("Update Thisinh set Tinhtrang=@tinhtrang, DiemThi=@diemthi where SBD=@sbd", "@tinhtrang", 1,"@diemthi",TinhDiemThi(),"@sbd", sbdbaithi);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}

        //public void loadDethi11()
        //{
        //    //int thoigian_ts = 0;
        //    RTFBuilderbase sb1 = new RTFBuilder();
        //    if (tinhtrang == 0)//neu thi sinh chua thi ==0
        //    {
        //        //Khong  can cai nay vi ben thongtin da co =>>thoigian_ts = (int)Database.ExecuteScalar("Select ThoiGianCL from Thisinh where SBD=@sbd","@sbd",sbdbaithi);
        //        if (thoigian_ts >= 1)//neu la thi sinh nhan nop bai roi nhung cho thi lai
        //        {
        //            string ketcaude = "";
        //            string bailam_ts = "";                    
        //            System.Data.DataTable dt = Database.GetData("Select * from Thisinh_BaithiNC where SBD=@sbd", "@sbd", sbdbaithi);
        //            for (int i = 0; i < dt.Rows.Count; i++)
        //            {
        //                ketcaude = ketcaude + (string)Database.ExecuteScalar("Select KetCau_De from DeThi where MaDe=@made", "@made", dt.Rows[i]["MaDe"].ToString());//tai vi Globals.DisplayDethiOnRtf(sb1, ketcaude);  chi hieu ket cau de cu
        //                Globals.DisplayDethiOnRtf(sb1, ketcaude);
        //                bailam_ts = bailam_ts + dt.Rows[i]["BaiLam"].ToString();
        //            }
        //            this.rtbDeThi.Rtf = sb1.ToString();
        //            //string bailam_ts = (string)Database.ExecuteScalar("Select BaiLam from Thisinh where SBD=@sbd", "@sbd", sbdbaithi);

        //            loadDgPhieuTraLoi(ketcaude.Length - 1, bailam_ts);
        //            thoigian = thoigian_ts;
        //            Database.ExecuteNonQuery("Update Thisinh set Tinhtrang=@tinhtrang where SBD=@sbd", "@tinhtrang", 2, "@sbd", sbdbaithi);
        //        }
        //        else//Neu la thi sinh chua thi that su
        //        {
        //            string ketcaudemoi = "";                    
        //            System.Data.DataTable dtDethi = new System.Data.DataTable();
        //            System.Data.DataTable dt = Database.GetData("Select * from Kithi where Phongthi = (Select Lop from Thisinh where SBD=@sbd)", "@sbd", sbdbaithi);
        //            for (int i = 0; i < dt.Rows.Count; i++)
        //            {
        //                dtDethi = Database.GetData("Select * from DeThi where Degoc=@degoc ORDER BY NEWID()", "@degoc", dt.Rows[i]["Degoc"].ToString());
        //                ketcaudemoi = ketcaudemoi + TaoLaiKetCauDe(dtDethi.Rows[0]["KetCau_De"].ToString());

        //                Database.ExecuteNonQuery("Insert into Thisinh_BaithiNC(SBD,DiemThi,MaDe,BaiLam) values(@sbd,@diemthi,@made,@bailam)", "@sbd", sbdbaithi, "@diemthi", 0, "@made", dtDethi.Rows[0]["MaDe"].ToString(), "@bailam", TaoLaiKetCauDe(dtDethi.Rows[0]["KetCau_De"].ToString()));
        //                Globals.DisplayDethiOnRtf(sb1, dtDethi.Rows[0]["ketCau_De"].ToString());
        //            }
        //            this.rtbDeThi.Rtf = sb1.ToString();
        //            thoigian = thoigian_bandau;

        //            //string[] mangdegoc = dtDethi.Rows[0]["KetCau_De"].ToString().Split('#');
        //            loadDgPhieuTraLoi(ketcaudemoi.Length - 1, "");

        //            Database.ExecuteNonQuery("Update Thisinh set Tinhtrang=@tinhtrang,MaDe=@made,Bailam=@bailam,ThoigianKT=@thoigiankt,ThoiGianCL=@tgconlai where SBD=@sbd", "@tinhtrang", 2, "@made", "", "@bailam", ketcaudemoi, "@thoigiankt", thoigian_bandau, "@tgconlai", thoigian_bandau, "@sbd", sbdbaithi);
        //        }
        //    }
        //    else MessageBox.Show("Số báo danh này hiện đang làm bài, vui lòng liên hệ giám thị!");
        //    //else if (tinhtrang == 2)//neu thi sinh dang thi bi out ra
        //    //{
        //    //    DialogResult response = MessageBox.Show("Bạn chưa hoàn thành bài làm, tiếp tục làm?", "Vào thi",
        //    //                      MessageBoxButtons.YesNo,
        //    //                      MessageBoxIcon.Question,
        //    //                      MessageBoxDefaultButton.Button2);
        //    //    if (response == DialogResult.Yes)
        //    //    {
        //    //        string ketcaude = (string)Database.ExecuteScalar("Select KetCau_De from DeThi where MaDe=@made", "@made", made);
        //    //        Globals.DisplayDethiOnRtf(sb1, ketcaude);
        //    //        this.rtbDeThi.Rtf = sb1.ToString();
        //    //        thoigian = thoigian_ts;
        //    //        string[] mangdegoc = ketcaude.Split('#');
        //    //        string bailam_ts = (string)Database.ExecuteScalar("Select BaiLam from Thisinh where SBD=@sbd", "@sbd", sbdbaithi);
        //    //        loadDgPhieuTraLoi(mangdegoc.Length - 1, bailam_ts);
        //    //    }
        //    //}
        //}
        
        //private void myTimer_Tick(object sender, EventArgs e)
        //{
        //    counter--;
        //    if (counter == 0)
        //        timer1.Stop();
        //    lbThoigian.Text = counter.ToString();
        //}


        //private void button1_Click(object sender, EventArgs e)
        //{
        //    //RTFBuilderbase sb1 = new RTFBuilder();
        //    //string ketcaude = (string)Database.ExecuteScalar("Select TOP(1) KetCau_De from DeThi ORDER BY NEWID()");
        //    //Globals.DisplayDethiOnRtf(sb1, ketcaude);
        //    //this.rtbDeThi.Rtf = sb1.ToString();
        //    //string[] mangdegoc = ketcaude.Split('#');
        //    //loadDgPhieuTraLoi(mangdegoc.Length - 1);       
        //}

        //private void dgPhieuTraLoi_CellContentClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    var isChecked = (bool)dgPhieuTraLoi.Rows[e.RowIndex].Cells[e.ColumnIndex].FormattedValue;
        //    if (isChecked)
        //    {
        //        //MessageBox.Show("row:" + e.RowIndex + ", Column: " + e.ColumnIndex);
        //        foreach (DataGridViewColumn column in dgPhieuTraLoi.Columns)
        //        {
        //            if (column.Index != e.ColumnIndex && column.Index != 0)
        //            {
        //                //MessageBox.Show("Ô: " + dgPhieuTraLoi.Rows[e.RowIndex].Cells[e.ColumnIndex].FormattedValue.ToString());
        //                dgPhieuTraLoi.Rows[e.RowIndex].Cells[column.Index].Value = !isChecked;
        //            }
        //        }
        //    }
        //}

        //private void dgPhieuTraLoi_Paint(object sender, PaintEventArgs e)
        //{
        //    //ControlPaint.DrawCheckBox(e.Graphics, 11, 11, 22, 22, ButtonState.Checked);
        //    //ControlPaint.DrawCheckBox(e.Graphics, 11, 44, 33, 33, ButtonState.Checked);
        //    //ControlPaint.DrawCheckBox(e.Graphics, 11, 88, 44, 44, ButtonState.Checked);
        //}

        //private void dgPhieuTraLoi_CellContentClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    //clean al rows
        //    foreach (DataGridViewColumn column in dgPhieuTraLoi.Columns)
        //    {
        //        dgPhieuTraLoi.Rows[e.RowIndex].Cells[column.Index].Value = false;
        //    }

        //    //check select row
        //    dgPhieuTraLoi.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = true;
        //}


        //private void dgPhieuTraLoi_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        //{
        //    if (e.ColumnIndex == 1)//the index of the column you have checkbox
        //    {
        //        bool hasOtherCheckboxChecked = false;
        //        foreach (DataGridViewRow dgvr in dgPhieuTraLoi.Rows)
        //        {
        //            DataGridViewCheckBoxCell cell = (DataGridViewCheckBoxCell)dgvr.Cells[e.ColumnIndex];
        //            if (cell.Value == cell.TrueValue && dgvr.Index != e.RowIndex)
        //            {
        //                hasOtherCheckboxChecked = true;
        //                break;
        //            }
        //        }
        //        DataGridViewCheckBoxCell currentCell = (DataGridViewCheckBoxCell)dgPhieuTraLoi.Rows[e.RowIndex].Cells[e.ColumnIndex];
        //        if (currentCell.Value == currentCell.TrueValue && hasOtherCheckboxChecked)
        //        {
        //            currentCell.Value = currentCell.FalseValue;
        //        }
        //    }
        //}

        //private void dgPhieuTraLoi_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        //{
        //    //// Whatever index is your checkbox column
        //    //var rowIndex = e.RowIndex;
        //    var isChecked = (bool)dgPhieuTraLoi.Rows[e.RowIndex].Cells[e.ColumnIndex].FormattedValue;
        //    if (isChecked)
        //    {
        //        //MessageBox.Show("row:" + e.RowIndex + ", Column: " + e.ColumnIndex);
        //        foreach (DataGridViewColumn column in dgPhieuTraLoi.Columns)
        //        {
        //            if (column.Index != e.ColumnIndex && column.Index!=0)
        //            {
        //                //essageBox.Show("Ô: " + dgPhieuTraLoi.Rows[e.RowIndex].Cells[e.ColumnIndex].FormattedValue.ToString());
        //                dgPhieuTraLoi.Rows[e.RowIndex].Cells[column.Index].Value = !isChecked;
        //            }
        //        }
        //    }

        //    ///MessageBox.Show("row:" + e.RowIndex + ", Column: " + e.ColumnIndex);
        //    //MessageBox.Show("Ô: " + dgPhieuTraLoi.Rows[e.RowIndex].Cells[e.ColumnIndex].FormattedValue.ToString());
        //}    



        //private void dgRadioButton_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        //{
        //    {
        //        if (e.ColumnIndex == dgRadioButton.Columns["A1"].Index && e.RowIndex >= 0)
        //        {
        //            e.PaintBackground(e.ClipBounds, true);

        //            System.Drawing.Rectangle rectRadioButton = new System.Drawing.Rectangle();

        //            rectRadioButton.Width = 14;
        //            rectRadioButton.Height = 14;
        //            rectRadioButton.X = e.CellBounds.X + (e.CellBounds.Width - rectRadioButton.Width) / 2;
        //            rectRadioButton.Y = e.CellBounds.Y + (e.CellBounds.Height - rectRadioButton.Height) / 2;
        //            //ControlPaint.DrawRadioButton(e.Graphics, rectRadioButton, ButtonState.Checked);

        //            if (e.Value == DBNull.Value)
        //            {
        //                ControlPaint.DrawRadioButton(e.Graphics, rectRadioButton, ButtonState.Normal);
        //            }
        //            else
        //            {
        //                ControlPaint.DrawRadioButton(e.Graphics, rectRadioButton, ButtonState.Checked);
        //            }

        //            e.Paint(e.ClipBounds, DataGridViewPaintParts.Focus);

        //            e.Handled = true;

        //        }
        //    }
        //}   

        //public void loadDgPhieuTraLoi2(int len)
        //{
        //    int j = 0;
        //    for (int i = 0; i < len; i++)
        //    {
        //        j = i + 1;
        //        dgRadioButton.Rows.Add();
        //        dgRadioButton.Rows[i].Cells["Cauhoi1"].Value = "Câu " + j.ToString("00");
        //    }
        //}

        //private void dgPhieuTraLoi_CellClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    //if (e.ColumnIndex == 2 && e.RowIndex >= 0) //delete icon button is clicked
        //    //{
        //    //    //MessageBox.Show("aaaa");
        //    //    //DialogResult response = MessageBox.Show("Bạn có chắc chắn muốn xóa đề gốc: " + dgDeGoc.Rows[e.RowIndex].Cells["Degoc"].Value.ToString(), "Xóa Đề gốc",
        //    //}
        //    //else if(e.ColumnIndex == 2 && e.RowIndex >= 0)
        //    //MessageBox.Show("row:" + e.RowIndex + ", Column: " + e.ColumnIndex);
        //    //MessageBox.Show("Ô: " + dgPhieuTraLoi.Rows[e.RowIndex].Cells[e.ColumnIndex].FormattedValue.ToString());
        //}        
   
