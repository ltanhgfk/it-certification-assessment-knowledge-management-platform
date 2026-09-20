using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
//using System.Drawing;
//using System.Linq;
//using System.Text;
using System.Windows.Forms;
using System.Globalization;


namespace ThiTracNghemUDCNTT
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //this.TopMost = true;
            //this.FormBorderStyle = FormBorderStyle.None;
            //this.FormBorderStyle = System.Windows.Forms.
            this.WindowState = FormWindowState.Maximized;
        }

        //private void btDethi_Click(object sender, EventArgs e)
        //{
        //    // //string dt = DateTime.Today.GetDateTimeFormats().ToString();
        //    // string sysFormat = CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern;
        //    // DateTime expireddate = new DateTime();
        //    // DateTime dt = new DateTime(2020, 6, 6); 
        //    // string date   = String.Format("{0:" + sysFormat + "}", dt);
        //    // expireddate = Convert.ToDateTime(date);           

        //    // if (DateTime.Today >= expireddate)
        //    // {
        //    // }
        //    // else
        //    // {
        //        // formDethi frm = new formDethi();
        //        // //frm.Activate();
        //        // //frm.Show();
        //        // frm.ShowDialog();
        //    // }
			
        //        formDethi frm = new formDethi();
        //        //frm.Activate();
        //        //frm.Show();
        //        frm.ShowDialog();
			
        //}

        //private void btThisinh_Click(object sender, EventArgs e)
        //{
        //    // //string dt = DateTime.Today.GetDateTimeFormats().ToString();
        //    // string sysFormat = CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern;
        //    // DateTime expireddate = new DateTime();
        //    // DateTime dt = new DateTime(2020, 6, 6); //ngay 04/10/2018 
        //    // string date   = String.Format("{0:" + sysFormat + "}", dt);
        //    // expireddate = Convert.ToDateTime(date);

        //    // if (DateTime.Today >= expireddate)
        //    // {
        //    // }
        //    // else
        //    // {
        //        // formThisinh frm = new formThisinh();
        //        // //frm.Activate();
        //        // frm.ShowDialog();
        //    // }
        //    // //LoadFileWordRtf frm = new LoadFileWordRtf();
        //    // //frm.ShowDialog();
			
        //    formThisinh frm = new formThisinh();
        //    //frm.Activate();
        //    frm.ShowDialog();
        //}

        private void btDangNhap_Click(object sender, EventArgs e)
        {
            System.Data.DataTable dt = Database.GetData("Select * from Account where UserName=@username and Password=@password", "@UserName", txtUserName.Text.Trim(), "@password", Database.Encrypt(txtPassword.Text.Trim()));
            if (dt.Rows.Count >= 1)
            {
                this.Hide();
                //Login frmlog = new Login();
                //frmlog.Close();

                Home frm = new Home();
                frm.Show();
                

                /*
                if (dt.Rows[0]["Tinhtrang"].ToString().Trim() == "0")
                {
                    //Database.ExecuteNonQuery("Update Thisinh set Tinhtrang=@tinhtrang where SBD=@sbd", "@tinhtrang", 2, "@sbd", txtSBD.Text.Trim());
                    frmThongTin frm = new frmThongTin(this);
                    this.Hide();
                    frm.sbdthongtin = txtSBD.Text.Trim();
                    frm.ShowDialog();
                }
                else if (dt.Rows[0]["Tinhtrang"].ToString().Trim() == "1")
                {
                    MessageBox.Show("Bạn đã hoàn thành bài thi!");
                }
                else
                    MessageBox.Show("Số báo danh này đã vào thi, vui lòng liên hệ giám thị!");
                 */
            }
            else
            {
                MessageBox.Show("Username hoặc mật khẩu không tồn tại, vui lòng thử lại!");
            }
        }

        private void btReset_Click(object sender, EventArgs e)
        {
            txtUserName.Text ="";
            txtPassword.Text = "";
        }

        //private void btKythi_Click(object sender, EventArgs e)
        //{
        //    //getImageFromWord frm = new getImageFromWord();
        //    //frm.ShowDialog();
        //    formKythi frm = new formKythi();
        //    frm.ShowDialog();
        //}
    }
}
