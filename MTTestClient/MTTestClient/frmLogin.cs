using System;
//using System.Collections.Generic;
//using System.ComponentModel;
using System.Data;
//using System.Text;
using System.Windows.Forms;

namespace MTTestClient
{
    public partial class frmLogin : Form
    {      

        public frmLogin()
        {
            InitializeComponent();
        }        

        private void frmLogin_Load(object sender, EventArgs e)
        {
            txtSBD.CharacterCasing = CharacterCasing.Upper;
        }

        //private void txtSBD_TextChanged(object sender, EventArgs e)
        //{
        //    DataTable dt = Database.GetData("Select * from Thisinh where SBD=@sbd", "@sbd", txtSBD.Text.Trim());
        //    if (dt.Rows.Count >= 1)
        //    {
        //        //this.Close();
        //        frmThongTin frm = new frmThongTin();
        //        frm.ShowDialog();
        //    }
        //    else
        //    {
        //        MessageBox.Show("Số báo danh " + txtSBD.Text + " không tồn tại, liên hệ giám thị để đăng nhập!");
        //    }
        //}

        private void txtSBD_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DataTable dt = Database.GetData("Select * from Thisinh where SBD=@sbd", "@sbd", txtSBD.Text.Trim());
                if (dt.Rows.Count >= 1)
                {
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
                }
                else
                {
                    MessageBox.Show("Số báo danh " + txtSBD.Text + " không tồn tại, liên hệ giám thị để đăng nhập!");
                }
            }
        }
    }       
}
