using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
//using System.Drawing;
//using System.Linq;
//using System.Text;
using System.Windows.Forms;

namespace ThiTracNghemUDCNTT
{
    public partial class UpdateThisinh : Form
    {
        public UpdateThisinh()
        {
            InitializeComponent();
        }

        private void btUpdateThisinh_Click(object sender, EventArgs e)
        {
            string sql = "Update Thisinh set Hoten=@hoten,Ten=@ten,Ngaysinh=@ngaysinh,Noisinh=@noisinh,Lop=@phongthi,Tinhtrang=@tinhtrang where SBD=@sbd";
            Database.ExecuteNonQuery(sql, "@hoten", txtHoten.Text, "@ten", txten.Text, "@ngaysinh", txtNgaysinh.Text, "@noisinh", txtNoisinh.Text, "@phongthi", cboPhongthi.SelectedValue, "@tinhtrang", cboTinhtrang.SelectedValue, "@sbd", txtSBD.Text);
            this.Dispose();
            this.Close();
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
            this.Close();
        }

        private void UpdateThisinh_Load(object sender, EventArgs e)
        {
            cboPhongthi.DropDownStyle = ComboBoxStyle.DropDownList;
            cboTinhtrang.DropDownStyle = ComboBoxStyle.DropDownList;
        }        
    }
}
