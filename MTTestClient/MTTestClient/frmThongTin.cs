using System;
//using System.Collections.Generic;
//using System.ComponentModel;
using System.Data;
//using System.Drawing;
//using System.Linq;
//using System.Text;
using System.Windows.Forms;

namespace MTTestClient
{
    public partial class frmThongTin : Form
    {      
       //khai báo biến kiểu Form1 giữ giá trị form1 truyền qua
       frmLogin _frmLogin;
       public string sbdthongtin;
       //public int tinhtrang_ts;
       //public string tt_hoten;
       //public string tt_ngaysinh;
       //public string tt_noisinh;
       //public string made;
       public int thoigian_ts;

       public frmThongTin(frmLogin _frm)
        {
            InitializeComponent();
            _frmLogin = _frm;
        }

        private void frmThongTin_Load(object sender, EventArgs e)
        {           
            DataTable dt = new DataTable();
            dt = Database.GetData("Select* from Thisinh where SBD=@sbd","@sbd",sbdthongtin);
            lbHoten.Text = dt.Rows[0]["Hoten"].ToString() + " " + dt.Rows[0]["Ten"].ToString();
            lbSbd.Text = dt.Rows[0]["SBD"].ToString();
            lbNgaysinh.Text = dt.Rows[0]["Ngaysinh"].ToString();
            lbNoisinh.Text = dt.Rows[0]["Noisinh"].ToString();

            //made = dt.Rows[0]["MaDe"].ToString().Trim();
            thoigian_ts = Convert.ToInt32(dt.Rows[0]["ThoiGianCL"]);
            //tinhtrang_ts = Convert.ToInt32(dt.Rows[0]["Tinhtrang"]);
            this.ActiveControl = btVaothi;
            _frmLogin.Hide();           
        }        

        private void btTrolai_Click(object sender, EventArgs e)
        {
            _frmLogin.Show();
            this.Hide();
            //this.Dispose();
            //this.Close();
        }

        private void btVaothi_Click(object sender, EventArgs e)
        {
            int tinhtrang_ts=-1;
            //string monthi = (string)Database.ExecuteScalar("Select Monthi from Kithi where Phongthi=(select Lop from Thisinh where SBD=@sbd)", "@sbd", sbdthongtin);
            //Kiem tra phong thi ma thi sinh nay thi da phat de chua?
            System.Data.DataTable dtkithi = Database.GetData("select * from Kithi where Phongthi=(select Lop from Thisinh where SBD=@sbd) and TinhTrang=@tinhtrang", "@sbd", sbdthongtin, "@tinhtrang", 1);
            if (dtkithi.Rows.Count >= 1)
            {
                if (dtkithi.Rows[0]["Tinhtrang"].ToString() == "1")//neu phong thi nay da phat de thi...
                {
                    //Kiem tra thi sinh nay da thi hay chua?
                    tinhtrang_ts = (int)Database.ExecuteScalar("Select TinhTrang From Thisinh where SBD=@sbd", "@sbd", sbdthongtin);
                    if (tinhtrang_ts == 0) //||tinhtrang_ts == 2)//neu thi sinh chua thi =0  hoac neu thi sinh dang thi =2 => do bi out ra khi chua ket thuc bai thi;
                    {
                        //FrmBaiThi frmbaithi = new FrmBaiThi(this);
                        //this.Hide();
                        //frmbaithi.sbdbaithi = sbdthongtin;
                        //frmbaithi.hoten = lbHoten.Text.Trim();
                        //frmbaithi.ngaysinh = lbNgaysinh.Text;
                        //frmbaithi.noisinh = lbNoisinh.Text;
                        //frmbaithi.monthi = dtkithi.Rows[0]["MonThi"].ToString().Trim();
                        //frmbaithi.degoc = dtkithi.Rows[0]["DeGoc"].ToString().Trim();
                        //frmbaithi.thoigian_bandau = (int)dtkithi.Rows[0]["ThoiGian"];
                        //frmbaithi.tinhtrang = tinhtrang_ts;
                        //frmbaithi.made = made;
                        //frmbaithi.thoigian_ts = thoigian_ts;
                        //frmbaithi.ShowDialog();

                        Database.ExecuteNonQuery("Update Thisinh set Tinhtrang=@tinhtrang where SBD=@sbd", "@tinhtrang", 2, "@sbd", sbdthongtin);                     

                        int tg = 0;
                        FrmBaiThiOutside frmbaithi = new FrmBaiThiOutside(this);
                        this.Hide();
                        frmbaithi.sbdbaithi = sbdthongtin;
                        frmbaithi.hoten = lbHoten.Text.Trim();
                        frmbaithi.ngaysinh = lbNgaysinh.Text;
                        frmbaithi.noisinh = lbNoisinh.Text;
                        if (dtkithi.Rows.Count > 1)
                        {
                            frmbaithi.monthi = "CÔNG NGHỆ THÔNG TIN NÂNG CAO";                            
                        }
                        else frmbaithi.monthi = dtkithi.Rows[0]["MonThi"].ToString().Trim();
                        
                        for (int i = 0; i < dtkithi.Rows.Count; i++)
                        {
                            tg = tg + (int)dtkithi.Rows[i]["ThoiGian"];
                        }
                        //frmbaithi.monthi = dtkithi.Rows[0]["MonThi"].ToString().Trim();
                        //frmbaithi.degoc = dtkithi.Rows[0]["DeGoc"].ToString().Trim();
                        frmbaithi.thoigian_bandau = tg;
                        frmbaithi.tinhtrang = 2;//tinhtrang_ts;
                        //frmbaithi.made = made;
                        frmbaithi.thoigian_ts = thoigian_ts;
                        frmbaithi.ShowDialog();
                    }
                    else if (tinhtrang_ts == 1)
                    {
                        MessageBox.Show("Bạn đã hoàn thành bài làm và nộp bài rồi!");                       
                    }
                    else //nguoc lai ==1 la thi sinh da thi roi
                    {
                        MessageBox.Show("Số báo danh này đã vào thi, vui lòng liên hệ giám thị!");
                    }
                }
                else
                    MessageBox.Show("Phòng thi này đã thu bài xong, bạn không thể vào thi nữa!");
                   
            }
            else //nguoc lai phong thi nay chua phat de
            {
                MessageBox.Show("Đề chưa phát, vui lòng đợi giám thị phát đề!");
            }            
        }

        private void Huongdan_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmHuongdan frm = new frmHuongdan();
            frm.ShowDialog();
        }        

        private void frmThongTin_FormClosed(object sender, FormClosedEventArgs e)
        {
            _frmLogin.Dispose();
            _frmLogin.Close();
        }          
    }
}
