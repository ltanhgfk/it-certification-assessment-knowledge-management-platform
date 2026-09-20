using System;
//using System.Collections.Generic;
//using System.ComponentModel;
using System.Data;
//using System.Drawing;
//using System.Linq;
//using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Text;
using System.Security.Cryptography;
//using System.Data.SqlClient;
//using System.Globalization;
//using System.IO;
//using Microsoft.Reporting.WinForms;

namespace ThiTracNghemUDCNTT
{
    public partial class formAccount : Form
    {
        bool update = false;
        string user = "";

        public formAccount()
        {
            InitializeComponent();
        }

        private void formThisinh_Load(object sender, EventArgs e)
        {          
            loadDsKithiToGridview();            
        }


        //private void btChinhsuaThisinh_Click(object sender, EventArgs e)
        //{
        //    if (kiemtrathisinhdangthi(dgDanhsachthisinh.CurrentRow.Cells["SBD"].Value.ToString()) == false)
        //    {
        //        int tt = 0;
        //        UpdateThisinh frmut = new UpdateThisinh();
        //        //frmut.Show();
        //        frmut.txtSBD.Text = dgDanhsachthisinh.CurrentRow.Cells["SBD"].Value.ToString();
        //        frmut.txtHoten.Text = dgDanhsachthisinh.CurrentRow.Cells["Họ"].Value.ToString();
        //        frmut.txten.Text = dgDanhsachthisinh.CurrentRow.Cells["Tên"].Value.ToString();

        //        frmut.txtNoisinh.Text = dgDanhsachthisinh.CurrentRow.Cells["Nơi sinh"].Value.ToString();
        //        frmut.txtNgaysinh.Text = dgDanhsachthisinh.CurrentRow.Cells["Ngày sinh"].Value.ToString();

        //        string getMonthi = "Select distinct (Lop) from Thisinh";
        //        System.Data.DataTable tbMonthi = new System.Data.DataTable();
        //        tbMonthi = Database.GetData(getMonthi);
        //        frmut.cboPhongthi.DataSource = tbMonthi;
        //        frmut.cboPhongthi.ValueMember = "Lop";
        //        //cboPhongthi.DisplayIndex = 1;
        //        frmut.cboPhongthi.SelectedValue = dgDanhsachthisinh.CurrentRow.Cells["Phòng thi"].Value.ToString();//Convert.ToInt32(dgDanhsachthisinh.CurrentRow.Cells["Phòng thi"].Value.ToString());

        //        DataTable dataTable = new DataTable();
        //        dataTable.Columns.Add("Id");
        //        dataTable.Columns.Add("Name");
        //        dataTable.Rows.Add(0, "Chưa thi");
        //        dataTable.Rows.Add(1, "Đang thi");
        //        dataTable.Rows.Add(2, "Đã thi");
        //        frmut.cboTinhtrang.DataSource = dataTable;
        //        frmut.cboTinhtrang.DisplayMember = "Name";
        //        frmut.cboTinhtrang.ValueMember = "Id";
        //        if (dgDanhsachthisinh.CurrentRow.Cells["Tình trạng"].Value.ToString() == "Đang thi")
        //            tt = 1;
        //        else if (dgDanhsachthisinh.CurrentRow.Cells["Tình trạng"].Value.ToString() == "Đã thi")
        //            tt = 2;
        //        frmut.cboTinhtrang.SelectedValue = tt;//dgDanhsachthisinh.CurrentRow.Cells["Tình trạng"].Value.ToString();
        //        frmut.ShowDialog();
        //        loadDanhSachThisinhByPhongthi();
        //    }
        //    else
        //    {
        //        int tt = 0;
        //        UpdateThisinh frmut = new UpdateThisinh();
        //        //frmut.Show();
        //        frmut.txtSBD.Text = dgDanhsachthisinh.CurrentRow.Cells["SBD"].Value.ToString();
        //        frmut.txtSBD.Enabled = false;
        //        frmut.txtHoten.Text = dgDanhsachthisinh.CurrentRow.Cells["Họ"].Value.ToString();
        //        frmut.txtHoten.Enabled = false;
        //        frmut.txten.Text = dgDanhsachthisinh.CurrentRow.Cells["Tên"].Value.ToString();
        //        frmut.txten.Enabled = false;

        //        frmut.txtNoisinh.Text = dgDanhsachthisinh.CurrentRow.Cells["Nơi sinh"].Value.ToString();
        //        frmut.txtNoisinh.Enabled = false;
        //        frmut.txtNgaysinh.Text = dgDanhsachthisinh.CurrentRow.Cells["Ngày sinh"].Value.ToString();
        //        frmut.txtNgaysinh.Enabled = false;


        //        string getMonthi = "Select distinct (Lop) from Thisinh";
        //        System.Data.DataTable tbMonthi = new System.Data.DataTable();
        //        tbMonthi = Database.GetData(getMonthi);
        //        frmut.cboPhongthi.DataSource = tbMonthi;
        //        frmut.cboPhongthi.ValueMember = "Lop";
        //        //cboPhongthi.DisplayIndex = 1;
        //        frmut.cboPhongthi.SelectedValue = dgDanhsachthisinh.CurrentRow.Cells["Phòng thi"].Value.ToString();//Convert.ToInt32(dgDanhsachthisinh.CurrentRow.Cells["Phòng thi"].Value.ToString());

        //        DataTable dataTable = new DataTable();
        //        dataTable.Columns.Add("Id");
        //        dataTable.Columns.Add("Name");
        //        dataTable.Rows.Add(0, "Chưa thi");
        //        dataTable.Rows.Add(1, "Đang thi");
        //        dataTable.Rows.Add(2, "Đã thi");
        //        frmut.cboTinhtrang.DataSource = dataTable;
        //        frmut.cboTinhtrang.DisplayMember = "Name";
        //        frmut.cboTinhtrang.ValueMember = "Id";
        //        if (dgDanhsachthisinh.CurrentRow.Cells["Tình trạng"].Value.ToString() == "Đang thi")
        //            tt = 1;
        //        else if (dgDanhsachthisinh.CurrentRow.Cells["Tình trạng"].Value.ToString() == "Đã thi")
        //            tt = 2;
        //        frmut.cboTinhtrang.SelectedValue = tt;//dgDanhsachthisinh.CurrentRow.Cells["Tình trạng"].Value.ToString();
        //        frmut.ShowDialog();
        //        loadDanhSachThisinhByPhongthi();
        //        //MessageBox.Show("Thí sinh đang thi nên không cập nhật được!");
        //    }
        //}

        //private void btXoathisinh_Click(object sender, EventArgs e)
        //{
        //    for (int i = 0; i < dgDanhsachthisinh.SelectedRows.Count; i++)
        //    {
        //        if (kiemtrathisinhdangthi(dgDanhsachthisinh.SelectedRows[i].Cells["SBD"].Value.ToString()) == false)
        //        {
        //            DialogResult response = MessageBox.Show("Bạn có chắc chắn muốn xóa thí sinh SBD: " + dgDanhsachthisinh.SelectedRows[i].Cells["SBD"].Value.ToString(), "Xóa thí sinh",
        //                      MessageBoxButtons.YesNo,
        //                      MessageBoxIcon.Question,
        //                      MessageBoxDefaultButton.Button2);
        //            if (response == DialogResult.Yes)
        //            {
        //                Database.ExecuteNonQuery("Delete from Thisinh where SBD=@sbd", "@sbd", dgDanhsachthisinh.SelectedRows[i].Cells["SBD"].Value.ToString());
        //                Database.ExecuteNonQuery("Delete from Thisinh_BaithiNC where SBD=@sbd", "@sbd", dgDanhsachthisinh.SelectedRows[i].Cells["SBD"].Value.ToString());
        //                MessageBox.Show("Đã xóa thí sinh có SBD: " + dgDanhsachthisinh.SelectedRows[i].Cells["SBD"].Value.ToString());
        //            }
        //            //loadDanhSachThisinh();
        //            loadDanhSachThisinhByPhongthi();
        //        }
        //        else MessageBox.Show("Thí sinh đang thi nên không xóa được!");
        //    }
        //}

        //private void btXoatatca_Click(object sender, EventArgs e)
        //{
        //    if (kiemtracothisinhdangthi() == false)
        //    {
        //        DialogResult response = MessageBox.Show("Bạn có chắc chắn muốn xóa tất cả thí sinh!", "Xóa tất cả",
        //                      MessageBoxButtons.YesNo,
        //                      MessageBoxIcon.Question,
        //                      MessageBoxDefaultButton.Button2);
        //        if (response == DialogResult.Yes)
        //        {
        //            Database.ExecuteNonQuery("Delete from Kithi");
        //            Database.ExecuteNonQuery("Delete from Thisinh");
        //            Database.ExecuteNonQuery("Delete from Thisinh_BaithiNC");
        //            MessageBox.Show("Đã xóa tất cả thí sinh!");
        //        }
        //        //loadDanhSachThisinh();
        //        loadAllDanhSachThisinh();
        //    }
        //    else MessageBox.Show("Có thí sinh đang thi nên không xóa được!");
        //}


        public void loadDsKithiToGridview()
        {
            //txtNgaythi.Text = DateTime.Today.ToShortDateString();
            //Load data to Gridview Phongthi            
            System.Data.DataTable dt = Database.GetData("SELECT * FROM Account where usertype <> 'host'");
            if (dt.Rows.Count >= 1)
            {
                System.Data.DataTable table = new System.Data.DataTable();
                //table.Columns.Add("STT", typeof(int));
                table.Columns.Add("UserName", typeof(string));
                table.Columns.Add("Password", typeof(string));
                table.Columns.Add("UserType", typeof(string));                

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr1 = table.NewRow();
                    //dr1["STT"] = i + 1;
                    dr1["UserName"] = dt.Rows[i]["UserName"].ToString();
                    dr1["Password"] = Database.Decrypt(dt.Rows[i]["Password"].ToString().Replace(" ","+"));
                    dr1["UserType"] = dt.Rows[i]["UserType"].ToString();               


                    table.Rows.Add(dr1.ItemArray);
                }
                dgPhongthi_Dethi.DataSource = table;

                this.dgPhongthi_Dethi.GridColor = Color.BlueViolet;
                this.dgPhongthi_Dethi.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
                this.dgPhongthi_Dethi.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;

                dgPhongthi_Dethi.Columns["UserName"].HeaderText = "TÊN ĐĂNG NHẬP";
                dgPhongthi_Dethi.Columns["Password"].HeaderText = "MẬT KHẨU";
                dgPhongthi_Dethi.Columns["UserType"].HeaderText = "QUYỀN";
                
                //dgPhongthi_Dethi.Columns["STT"].Width = 40;
                dgPhongthi_Dethi.Columns["UserName"].Width = 150;
                dgPhongthi_Dethi.Columns["UserName"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                dgPhongthi_Dethi.Columns["Password"].Width = 150;
                dgPhongthi_Dethi.Columns["Password"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                dgPhongthi_Dethi.Columns["UserType"].Width = 140;
                dgPhongthi_Dethi.Columns["UserType"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            }
        }

        

        private void btPhatde_Click(object sender, EventArgs e)
        {
            update = true;
            user = this.dgPhongthi_Dethi.SelectedRows[0].Cells["UserName"].Value.ToString();

            txtUserName.Text = this.dgPhongthi_Dethi.SelectedRows[0].Cells["UserName"].Value.ToString();
            txtPassword.Text = this.dgPhongthi_Dethi.SelectedRows[0].Cells["Password"].Value.ToString();
            cboUserType.Text = this.dgPhongthi_Dethi.SelectedRows[0].Cells["UserType"].Value.ToString();

            //dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString())
            //dataGridView1.SelectedRows[0].Cells[0].Value 

            /*string phongthi ="";
            if (this.dgPhongthi_Dethi.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow row in dgPhongthi_Dethi.SelectedRows)
                {
                    phongthi = row.Cells["PhongThi"].Value.ToString();
                    Database.ExecuteNonQuery("Update Kithi set TinhTrang=@tinhtrang where PhongThi=@phongthi", "@tinhtrang", 1, "@phongthi", phongthi);
                }
                MessageBox.Show("Đã cho thi thành công!");
                loadDsKithiToGridview();
            }
            else
            {
                MessageBox.Show("Bạn phải chọn phòng thi cần cho thi!");
            }*/
        }

        private void btThuBai_Click(object sender, EventArgs e)
        {
            if (this.dgPhongthi_Dethi.SelectedRows.Count > 0)
            {
                DialogResult response = MessageBox.Show("Bạn có chắc chắn muốn xóa các tài khoản đã chọn?", "Xóa tài khoản",
                                  MessageBoxButtons.YesNo,
                                  MessageBoxIcon.Question,
                                  MessageBoxDefaultButton.Button2);
                if (response == DialogResult.Yes)
                {
                    foreach (DataGridViewRow row in dgPhongthi_Dethi.SelectedRows)
                    {
                        string sql = "Delete Account where username=@username and password=@pass";
                        Database.ExecuteNonQuery(sql, "@username", row.Cells["username"].Value.ToString(), "@pass", row.Cells["password"].Value.ToString());                        
                    }
                    MessageBox.Show("Đã xóa xong!");
                    //loadDanhSachThisinh();                    
                    loadDsKithiToGridview();
                }
            }
            else
            {
                MessageBox.Show("Bạn phải chọn tài khoản cần xóa trước khi xóa!");
            }           
        }
        

        private void btSave_Click(object sender, EventArgs e)
        {
            if (txtUserName.Text.Trim() == "")
            {
                MessageBox.Show("Chưa nhập tên đăng nhập");
            }           
            else if (txtPassword.Text.Trim() == "")
            {
                MessageBox.Show("Chưa nhập mật khẩu");
            }
           /* else if (!Regex.IsMatch(txtPassword.Text.Trim(), "^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$")) // "^[a-z0-9._-]*$"))
            {
                MessageBox.Show("Mật khẩu phải có cả số, chữ và ký tự đặc biệt");
            }*/
            else if (cboUserType.SelectedIndex < 0 || cboUserType.Text.Trim()=="")
            {
                MessageBox.Show("Chưa chọn quyền người dùng");
            }            
            else
            {
                if (update == true)
                {
                    Database.ExecuteNonQuery("Update Account set username=@username, password=@pass, usertype=@usertype where username=@user",
                            "@username", txtUserName.Text.Trim(),
                            "@pass", Database.Encrypt(txtPassword.Text.Trim()),
                            "@usertype", cboUserType.Text,
                            "@user", user);

                    MessageBox.Show("Cập nhật thành công!");
                    loadDsKithiToGridview();
                }
                else
                {
                    DataTable dt = Database.GetData("Select*from Account where username=@username", "@username", txtUserName.Text.Trim());
                    if (dt.Rows.Count > 0)
                    {
                        MessageBox.Show("Tên người dùng đã có, xin chọn lại tên khác!");
                    }
                    else
                    {
                        Database.ExecuteNonQuery("Insert into Account(username, password, usertype) values(@username,@pass,@usertype)",
                            "@username", txtUserName.Text.Trim(),
                            "@pass", Database.Encrypt(txtPassword.Text.Trim()),
                            "@usertype", cboUserType.Text);

                        MessageBox.Show("Cập nhật thành công!");
                        loadDsKithiToGridview();
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            update = false;
            txtUserName.Text = "";
            txtPassword.Text = "";
        }
    }
}

/*
dt = new DataTable();
dt.Load(reader);

DataRow row = dt.NewRow();
row["col1"] = "Something";
row["col2"] = "Something else";

dt.Rows.InsertAt(row, 0);

ComboxBox1.ValueMember = "col1";
ComboxBox1.DisplayMember = "col2";
ComboxBox1.DataSource = dt;
*/