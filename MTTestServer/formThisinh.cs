using System;
//using System.Collections.Generic;
//using System.ComponentModel;
using System.Data;
//using System.Drawing;
//using System.Linq;
//using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
//using System.Data.SqlClient;
//using System.Globalization;
//using System.IO;
//using Microsoft.Reporting.WinForms;

namespace ThiTracNghemUDCNTT
{
    public partial class formThisinh : Form
    {
        public formThisinh()
        {
            InitializeComponent();
        }

        private void formThisinh_Load(object sender, EventArgs e)
        {
            DataTable dt = Database.GetData("Select * from Thisinh");            
            if (dt.Rows.Count > 0)
            {
                lbTongsoluong.Text = dt.Rows.Count.ToString();
                loadPhongthiToCboPhongthiPd();
                //loadDanhSachThisinh();
                //loadAllDanhSachThisinh();
                loadDanhSachThisinhByPhongthi();
            }
            loadDsKithiToGridview();
            //Load Monthi to combobox Monthi
            string getMonthi = "Select * from Monthi order by Thutu";
            System.Data.DataTable tbMonthi = new System.Data.DataTable();
            tbMonthi = Database.GetData(getMonthi);
            cboMonthi.DisplayMember = "TenMonThi";
            cboMonthi.ValueMember = "MaMonThi";
            cboMonthi.DataSource = tbMonthi;

            //cboMonthiToPrint.DisplayMember = "TenMonThi";
            //cboMonthiToPrint.ValueMember = "MaMonThi";
            //cboMonthiToPrint.DataSource = tbMonthi;
            //-------------------------------------//
            string getDegoc = "Select * from Degoc order by Degoc";
            System.Data.DataTable tbDegoc = new System.Data.DataTable();
            tbDegoc = Database.GetData(getDegoc);
            cboDegoc.DataSource = tbDegoc;
            cboDegoc.DisplayMember = "Degoc";
            cboDegoc.ValueMember = "Degoc";
            //--------------------------------------//
            string showscore = (string)Database.ExecuteScalar("Select Variable from tblConfig where Name=@name","@name","showscore");
            if (showscore == "dung")
                rdShowscore.Checked = true;
            else rdUnshowscore.Checked = true;
        }

        public void loadPhongthiToCboPhongthiPd()
        {
            System.Data.DataTable tbMonthi = Database.GetData("Select distinct (Lop) from Thisinh");
            if (tbMonthi.Rows.Count >= 1)
            {
                //cboPhongthiPd.Items.Add(new Item("Chọn", 0));  
                cboPhongthiPd.DataSource = tbMonthi;
                cboPhongthiPd.ValueMember = "Lop";

                //cboPhongthi.Items.Add(new Item("Chọn", 0));            
                cboPhongthi.DataSource = tbMonthi;
                cboPhongthi.ValueMember = "Lop";
            }
        }

        public void loadAllDanhSachThisinh()
        {
            DataTable dtSql = Database.GetData("Select SBD,Hoten,Ten,ngaysinh,noisinh,Lop,TinhTrang from Thisinh order by STT");

            lbsoluong.Text = dtSql.Rows.Count.ToString();

            DataTable dt = new DataTable();
            dt.Columns.Add("SBD", typeof(string));
            dt.Columns.Add("Họ", typeof(string));
            dt.Columns.Add("Tên", typeof(string));
            dt.Columns.Add("Ngày sinh", typeof(string));
            dt.Columns.Add("Nơi sinh", typeof(string));
            dt.Columns.Add("Phòng thi", typeof(string));
            dt.Columns.Add("Tình trạng", typeof(string));

            for (int i = 0; i < dtSql.Rows.Count; i++)
            {
                DataRow dr1 = dt.NewRow();
                dr1["SBD"] = dtSql.Rows[i]["SBD"].ToString();
                dr1["Họ"] = dtSql.Rows[i]["Hoten"].ToString();
                dr1["Tên"] = dtSql.Rows[i]["Ten"].ToString();
                dr1["Ngày sinh"] = dtSql.Rows[i]["ngaysinh"].ToString();
                dr1["Nơi sinh"] = dtSql.Rows[i]["noisinh"].ToString();
                dr1["Phòng thi"] = dtSql.Rows[i]["lop"].ToString();

                if (dtSql.Rows[i]["Tinhtrang"].ToString() == "0")
                {
                    dr1["Tình trạng"] = "Chưa thi";
                }
                else if (dtSql.Rows[i]["Tinhtrang"].ToString() == "2") dr1["Tình trạng"] = "Đang thi";
                else dr1["Tình trạng"] = "Đã thi";

                dt.Rows.Add(dr1.ItemArray);
            }

            dgDanhsachthisinh.DataSource = dt;

            dgDanhsachthisinh.Columns["SBD"].ReadOnly = true;
            dgDanhsachthisinh.Columns["Họ"].ReadOnly = true;
            dgDanhsachthisinh.Columns["Tên"].ReadOnly = true;
            dgDanhsachthisinh.Columns["Ngày sinh"].ReadOnly = true;
            dgDanhsachthisinh.Columns["Nơi sinh"].ReadOnly = true;
            dgDanhsachthisinh.Columns["Phòng thi"].ReadOnly = true;
            dgDanhsachthisinh.Columns["Tình trạng"].ReadOnly = true;

            dgDanhsachthisinh.Columns["SBD"].Width = 75;
            dgDanhsachthisinh.Columns["Họ"].Width = 120;
            dgDanhsachthisinh.Columns["Tên"].Width = 58;
            dgDanhsachthisinh.Columns["Ngày sinh"].Width = 85;
            dgDanhsachthisinh.Columns["Nơi sinh"].Width = 80;
            dgDanhsachthisinh.Columns["Phòng thi"].Width = 60;
            dgDanhsachthisinh.Columns["Tình trạng"].Width = 65;
        }

        public void loadDanhSachThisinhByPhongthi()
        {
            DataTable dtSql = Database.GetData("Select SBD,Hoten,Ten,ngaysinh,noisinh,Lop,TinhTrang from Thisinh where Lop=@lop order by STT", "@lop", cboPhongthi.SelectedValue.ToString());

            lbsoluong.Text = dtSql.Rows.Count.ToString();

            DataTable dt = new DataTable();
            dt.Columns.Add("SBD", typeof(string));
            dt.Columns.Add("Họ", typeof(string));
            dt.Columns.Add("Tên", typeof(string));
            dt.Columns.Add("Ngày sinh", typeof(string));
            dt.Columns.Add("Nơi sinh", typeof(string));
            dt.Columns.Add("Phòng thi", typeof(string));
            dt.Columns.Add("Tình trạng", typeof(string));

            for (int i = 0; i < dtSql.Rows.Count; i++)
            {
                DataRow dr1 = dt.NewRow();
                dr1["SBD"] = dtSql.Rows[i]["SBD"].ToString();
                dr1["Họ"] = dtSql.Rows[i]["Hoten"].ToString();
                dr1["Tên"] = dtSql.Rows[i]["Ten"].ToString();
                dr1["Ngày sinh"] = dtSql.Rows[i]["ngaysinh"].ToString();
                dr1["Nơi sinh"] = dtSql.Rows[i]["noisinh"].ToString();
                dr1["Phòng thi"] = dtSql.Rows[i]["lop"].ToString();

                if (dtSql.Rows[i]["Tinhtrang"].ToString() == "0")
                {
                    dr1["Tình trạng"] = "Chưa thi";
                }
                else if (dtSql.Rows[i]["Tinhtrang"].ToString() == "2") dr1["Tình trạng"] = "Đang thi";
                else dr1["Tình trạng"] = "Đã thi";

                dt.Rows.Add(dr1.ItemArray);
            }

            dgDanhsachthisinh.DataSource = dt;

            dgDanhsachthisinh.Columns["SBD"].ReadOnly = true;
            dgDanhsachthisinh.Columns["Họ"].ReadOnly = true;
            dgDanhsachthisinh.Columns["Tên"].ReadOnly = true;
            dgDanhsachthisinh.Columns["Ngày sinh"].ReadOnly = true;
            dgDanhsachthisinh.Columns["Nơi sinh"].ReadOnly = true;
            dgDanhsachthisinh.Columns["Phòng thi"].ReadOnly = true;
            dgDanhsachthisinh.Columns["Tình trạng"].ReadOnly = true;

            dgDanhsachthisinh.Columns["SBD"].Width = 75;
            dgDanhsachthisinh.Columns["Họ"].Width = 120;
            dgDanhsachthisinh.Columns["Tên"].Width = 58;
            dgDanhsachthisinh.Columns["Ngày sinh"].Width = 85;
            dgDanhsachthisinh.Columns["Nơi sinh"].Width = 80;
            dgDanhsachthisinh.Columns["Phòng thi"].Width = 60;
            dgDanhsachthisinh.Columns["Tình trạng"].Width = 65;
        }

        private void btNhapdsThisinh_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialogThisinh.ShowDialog(); // Show the dialog.
            if (result == DialogResult.OK && openFileDialogThisinh.FileName != null) // Test result.
            {
                string file = openFileDialogThisinh.FileName;
                txtFile.Text = file;

                String constr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + txtFile.Text + ";Extended Properties='Excel 12.0 XML;HDR=YES;';";

                OleDbConnection con = new OleDbConnection(constr);
                OleDbCommand oconn = new OleDbCommand("Select * From [Sheet1$]", con);
                con.Open();

                OleDbDataAdapter sda = new OleDbDataAdapter(oconn);
                DataTable data = new DataTable();
                sda.Fill(data);

                for (int i = 0; i < data.Rows.Count; i++)
                {
                    string SBD = "";
                    string Hoten = "";
                    string Ten = "";
                    string Ngaysinh = "";
                    string Noisinh = "";
                    string Phongthi = "";
                    double Diemthi = 0.0;
                    int tinhtrang = 0;
                    string Bailam = "";
                    string Made = "";
                    int ThoigianKT = 0;
                    int ThoigianCL = 0;
                    int Stt = 0;


                    SBD = data.Rows[i][1].ToString();
                    Hoten = data.Rows[i][2].ToString();
                    Ten = data.Rows[i][3].ToString();
                    if (data.Rows[i][4].ToString().Length <= 6)
                    {
                        Ngaysinh = data.Rows[i][4].ToString().Trim();
                    }
                    else
                        Ngaysinh = data.Rows[i][4].ToString().Trim().Substring(0, 10);

                    Noisinh = data.Rows[i][5].ToString();
                    Phongthi = data.Rows[i][6].ToString();
                    if (SBD.Contains("."))
                    {
                        Stt = Convert.ToInt32(SBD.Trim().Substring(SBD.Trim().IndexOf(".") + 1, SBD.Trim().Length - (SBD.Trim().IndexOf(".") + 1)));
                    }
                    else Stt = i;

                    string cmd = "Insert Into Thisinh(SBD,Hoten,Ten,Ngaysinh,Noisinh,Lop,Tinhtrang,Diemthi,Bailam,Made,ThoiGianKT,ThoiGianCL,STT) values(@SBD,@Hoten,@Ten,@Ngaysinh,@Noisinh,@Lop,@Tinhtrang,@Diemthi,@Bailam,@Made,@ThoiGianKT,@ThoiGianCL,@stt) ";
                    //string cmd = "UPDATE ThiSinh SET DiemThi = @DiemThi, BaiLam = @BaiLam, MaDe = @MaDe, TinhTrang = @TinhTrang where SBD=@SBD";
                    Database.ExecuteNonQuery(cmd, "@SBD", SBD, "@Hoten", Hoten, "@Ten", Ten, "@Ngaysinh", Ngaysinh, "@Noisinh", Noisinh, "@Lop", Phongthi, "@Tinhtrang", tinhtrang, "@DiemThi", Diemthi, "@BaiLam", Bailam, "@MaDe", Made, "@ThoiGianKT", ThoigianKT, "@ThoiGianCL", ThoigianCL, "@stt", Stt);
                }
                MessageBox.Show("Hoàn thành việc tải danh sách");
                loadPhongthiToCboPhongthiPd();
                loadDanhSachThisinhByPhongthi();
                //loadDanhSachThisinh();
                //loadAllDanhSachThisinh();
            }
            else
            {
                MessageBox.Show("Bạn chưa chọn tập tin danh sách!");
            }
        }

        private void cboPhongthi_SelectedIndexChanged(object sender, EventArgs e)
        {
            //loadDanhSachThisinh();
            loadDanhSachThisinhByPhongthi();
        }

        private bool kiemtrathisinhdangthi(string sbd)
        {
            int tinhtrang = (int)Database.ExecuteScalar("Select Tinhtrang from Thisinh where SBD=@sbd", "@sbd", sbd);
            if (tinhtrang == 2) return true;
            else return false;
        }

        private bool kiemtraphongthidangthi(string phongthi)
        {
            DataTable dt = Database.GetData("Select * from Thisinh where Lop=@phong and TinhTrang=@tinhtrang", "@phong", phongthi, "@tinhtrang", 2);
            if (dt.Rows.Count >= 1) return true;
            else return false;
        }

        private bool kiemtracothisinhdangthi()
        {
            DataTable dt = Database.GetData("Select * from Thisinh where TinhTrang=@tinhtrang", "@tinhtrang", 2);
            if (dt.Rows.Count >= 1) return true;
            else return false;
        }

        private void btChinhsuaThisinh_Click(object sender, EventArgs e)
        {
            if (kiemtrathisinhdangthi(dgDanhsachthisinh.CurrentRow.Cells["SBD"].Value.ToString()) == false)
            {
                int tt = 0;
                UpdateThisinh frmut = new UpdateThisinh();
                //frmut.Show();
                frmut.txtSBD.Text = dgDanhsachthisinh.CurrentRow.Cells["SBD"].Value.ToString();
                frmut.txtHoten.Text = dgDanhsachthisinh.CurrentRow.Cells["Họ"].Value.ToString();
                frmut.txten.Text = dgDanhsachthisinh.CurrentRow.Cells["Tên"].Value.ToString();

                frmut.txtNoisinh.Text = dgDanhsachthisinh.CurrentRow.Cells["Nơi sinh"].Value.ToString();
                frmut.txtNgaysinh.Text = dgDanhsachthisinh.CurrentRow.Cells["Ngày sinh"].Value.ToString();

                string getMonthi = "Select distinct (Lop) from Thisinh";
                System.Data.DataTable tbMonthi = new System.Data.DataTable();
                tbMonthi = Database.GetData(getMonthi);
                frmut.cboPhongthi.DataSource = tbMonthi;
                frmut.cboPhongthi.ValueMember = "Lop";
                //cboPhongthi.DisplayIndex = 1;
                frmut.cboPhongthi.SelectedValue = dgDanhsachthisinh.CurrentRow.Cells["Phòng thi"].Value.ToString();//Convert.ToInt32(dgDanhsachthisinh.CurrentRow.Cells["Phòng thi"].Value.ToString());

                DataTable dataTable = new DataTable();
                dataTable.Columns.Add("Id");
                dataTable.Columns.Add("Name");
                dataTable.Rows.Add(0, "Chưa thi");
                dataTable.Rows.Add(1, "Đang thi");
                dataTable.Rows.Add(2, "Đã thi");
                frmut.cboTinhtrang.DataSource = dataTable;
                frmut.cboTinhtrang.DisplayMember = "Name";
                frmut.cboTinhtrang.ValueMember = "Id";
                if (dgDanhsachthisinh.CurrentRow.Cells["Tình trạng"].Value.ToString() == "Đang thi")
                    tt = 1;
                else if (dgDanhsachthisinh.CurrentRow.Cells["Tình trạng"].Value.ToString() == "Đã thi")
                    tt = 2;
                frmut.cboTinhtrang.SelectedValue = tt;//dgDanhsachthisinh.CurrentRow.Cells["Tình trạng"].Value.ToString();
                frmut.ShowDialog();
                loadDanhSachThisinhByPhongthi();
            }
            else
            {
                int tt = 0;
                UpdateThisinh frmut = new UpdateThisinh();
                //frmut.Show();
                frmut.txtSBD.Text = dgDanhsachthisinh.CurrentRow.Cells["SBD"].Value.ToString();
                frmut.txtSBD.Enabled = false;
                frmut.txtHoten.Text = dgDanhsachthisinh.CurrentRow.Cells["Họ"].Value.ToString();
                frmut.txtHoten.Enabled = false;
                frmut.txten.Text = dgDanhsachthisinh.CurrentRow.Cells["Tên"].Value.ToString();
                frmut.txten.Enabled = false;

                frmut.txtNoisinh.Text = dgDanhsachthisinh.CurrentRow.Cells["Nơi sinh"].Value.ToString();
                frmut.txtNoisinh.Enabled = false;
                frmut.txtNgaysinh.Text = dgDanhsachthisinh.CurrentRow.Cells["Ngày sinh"].Value.ToString();
                frmut.txtNgaysinh.Enabled = false;


                string getMonthi = "Select distinct (Lop) from Thisinh";
                System.Data.DataTable tbMonthi = new System.Data.DataTable();
                tbMonthi = Database.GetData(getMonthi);
                frmut.cboPhongthi.DataSource = tbMonthi;
                frmut.cboPhongthi.ValueMember = "Lop";
                //cboPhongthi.DisplayIndex = 1;
                frmut.cboPhongthi.SelectedValue = dgDanhsachthisinh.CurrentRow.Cells["Phòng thi"].Value.ToString();//Convert.ToInt32(dgDanhsachthisinh.CurrentRow.Cells["Phòng thi"].Value.ToString());

                DataTable dataTable = new DataTable();
                dataTable.Columns.Add("Id");
                dataTable.Columns.Add("Name");
                dataTable.Rows.Add(0, "Chưa thi");
                dataTable.Rows.Add(1, "Đang thi");
                dataTable.Rows.Add(2, "Đã thi");
                frmut.cboTinhtrang.DataSource = dataTable;
                frmut.cboTinhtrang.DisplayMember = "Name";
                frmut.cboTinhtrang.ValueMember = "Id";
                if (dgDanhsachthisinh.CurrentRow.Cells["Tình trạng"].Value.ToString() == "Đang thi")
                    tt = 1;
                else if (dgDanhsachthisinh.CurrentRow.Cells["Tình trạng"].Value.ToString() == "Đã thi")
                    tt = 2;
                frmut.cboTinhtrang.SelectedValue = tt;//dgDanhsachthisinh.CurrentRow.Cells["Tình trạng"].Value.ToString();
                frmut.ShowDialog();
                loadDanhSachThisinhByPhongthi();
                //MessageBox.Show("Thí sinh đang thi nên không cập nhật được!");
            }
        }

        private void btXoathisinh_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dgDanhsachthisinh.SelectedRows.Count; i++)
            {
                if (kiemtrathisinhdangthi(dgDanhsachthisinh.SelectedRows[i].Cells["SBD"].Value.ToString()) == false)
                {
                    DialogResult response = MessageBox.Show("Bạn có chắc chắn muốn xóa thí sinh SBD: " + dgDanhsachthisinh.SelectedRows[i].Cells["SBD"].Value.ToString(), "Xóa thí sinh",
                              MessageBoxButtons.YesNo,
                              MessageBoxIcon.Question,
                              MessageBoxDefaultButton.Button2);
                    if (response == DialogResult.Yes)
                    {
                        Database.ExecuteNonQuery("Delete from Thisinh where SBD=@sbd", "@sbd", dgDanhsachthisinh.SelectedRows[i].Cells["SBD"].Value.ToString());
                        Database.ExecuteNonQuery("Delete from Thisinh_BaithiNC where SBD=@sbd", "@sbd", dgDanhsachthisinh.SelectedRows[i].Cells["SBD"].Value.ToString());
                        MessageBox.Show("Đã xóa thí sinh có SBD: " + dgDanhsachthisinh.SelectedRows[i].Cells["SBD"].Value.ToString());
                    }
                    //loadDanhSachThisinh();
                    loadDanhSachThisinhByPhongthi();
                }
                else MessageBox.Show("Thí sinh đang thi nên không xóa được!");
            }
        }

        //private void btDeleteByPhong_Click(object sender, EventArgs e)
        //{
        //    if (kiemtraphongthidangthi(cboPhongthi.SelectedValue.ToString()) == false)
        //    {
        //        DialogResult response = MessageBox.Show("Bạn có chắc chắn muốn xóa thí sinh phòng thi số: " + cboPhongthi.SelectedValue.ToString(), "Xóa phòng thi",
        //                      MessageBoxButtons.YesNo,
        //                      MessageBoxIcon.Question,
        //                      MessageBoxDefaultButton.Button2);
        //        if (response == DialogResult.Yes)
        //        {
        //            Database.ExecuteNonQuery("Delete from Kithi where PhongThi=@phongthi", "@phongthi", cboPhongthi.SelectedValue.ToString());
        //            Database.ExecuteNonQuery("Delete from Thisinh where Lop=@phongthi", "@phongthi", cboPhongthi.SelectedValue.ToString());
        //            MessageBox.Show("Đã xóa thí sinh phòng thi số: " + cboPhongthi.SelectedValue.ToString());
        //        }
        //        loadDanhSachThisinh();
        //    }
        //    else MessageBox.Show("Phòng thi đang thi nên không thể xóa!");
        //}

        private void btXoatatca_Click(object sender, EventArgs e)
        {
            if (kiemtracothisinhdangthi() == false)
            {
                DialogResult response = MessageBox.Show("Bạn có chắc chắn muốn xóa tất cả thí sinh!", "Xóa tất cả",
                              MessageBoxButtons.YesNo,
                              MessageBoxIcon.Question,
                              MessageBoxDefaultButton.Button2);
                if (response == DialogResult.Yes)
                {
                    Database.ExecuteNonQuery("Delete from Kithi");
                    Database.ExecuteNonQuery("Delete from Thisinh");
                    Database.ExecuteNonQuery("Delete from Thisinh_BaithiNC");
                    MessageBox.Show("Đã xóa tất cả thí sinh!");
                }
                //loadDanhSachThisinh();
                loadAllDanhSachThisinh();
            }
            else MessageBox.Show("Có thí sinh đang thi nên không xóa được!");
        }

        //private void btInDsDiem_Click(object sender, EventArgs e)
        //{
        //    //rpForm rpform = new rpForm();
        //    //rpform.phongthi = cboPhongthi.SelectedValue.ToString();
        //    //rpform.ShowDialog();

        //    crpFormBangDiem crpform = new crpFormBangDiem();
        //    crpform.phongthi = cboPhongthi.SelectedValue.ToString();
        //    //crpform.MdiParent = this;
        //    crpform.ShowDialog();
        //}

        private void btInAllResult_Click(object sender, EventArgs e)
        {
            //rpForm rpform = new rpForm();
            //rpform.phongthi = "ALL";
            //rpform.ShowDialog();

            //crpFormBangDiem crpform = new crpFormBangDiem();
            //crpform.phongthi = "ALL";
            ////crpform.MdiParent = this;
            //crpform.ShowDialog();


            crpFormScoreList crp = new crpFormScoreList();
            crp.phongthi = "ALL";
            crp.ShowDialog();
        }

        public void loadDsKithiToGridview()
        {
            txtNgaythi.Text = DateTime.Today.ToShortDateString();
            //Load data to Gridview Phongthi            
            System.Data.DataTable dt = Database.GetData("SELECT Phongthi,MonThi,Degoc,Tinhtrang,ThoiGian FROM Kithi");
            if (dt.Rows.Count >= 1)
            {
                System.Data.DataTable table = new System.Data.DataTable();
                //table.Columns.Add("STT", typeof(int));
                table.Columns.Add("PhongThi", typeof(string));
                table.Columns.Add("MonThi", typeof(string));
                table.Columns.Add("Degoc", typeof(string));
                table.Columns.Add("ThoiGian", typeof(int));
                table.Columns.Add("Tinhtrang", typeof(string));

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr1 = table.NewRow();
                    //dr1["STT"] = i + 1;
                    dr1["PhongThi"] = dt.Rows[i]["Phongthi"].ToString();
                    dr1["MonThi"] = dt.Rows[i]["Monthi"].ToString();
                    dr1["DeGoc"] = dt.Rows[i]["DeGoc"].ToString();
                    dr1["ThoiGian"] = Convert.ToInt32(dt.Rows[i]["Thoigian"].ToString()) / 60;

                    if (Convert.ToInt32(dt.Rows[i]["Tinhtrang"]) == 0)
                    {
                        dr1["Tinhtrang"] = "Chưa cho thi";
                    }
                    else dr1["Tinhtrang"] = "Đã cho thi";

                    table.Rows.Add(dr1.ItemArray);
                }
                dgPhongthi_Dethi.DataSource = table;

                dgPhongthi_Dethi.Columns["PhongThi"].HeaderText = "PHÒNG THI";
                dgPhongthi_Dethi.Columns["MonThi"].HeaderText = "MÔN THI";
                dgPhongthi_Dethi.Columns["Degoc"].HeaderText = "ĐỀ GỐC";
                dgPhongthi_Dethi.Columns["ThoiGian"].HeaderText = "THỜI GIAN";
                dgPhongthi_Dethi.Columns["TinhTrang"].HeaderText = "TÌNH TRẠNG";

                //dgPhongthi_Dethi.Columns["STT"].Width = 40;
                dgPhongthi_Dethi.Columns["PhongThi"].Width = 70;
                dgPhongthi_Dethi.Columns["PhongThi"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                dgPhongthi_Dethi.Columns["MonThi"].Width = 150;
                dgPhongthi_Dethi.Columns["MonThi"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                dgPhongthi_Dethi.Columns["Degoc"].Width = 110;
                dgPhongthi_Dethi.Columns["ThoiGian"].Width = 50;
                dgPhongthi_Dethi.Columns["TinhTrang"].Width = 70;
                dgPhongthi_Dethi.Columns["TinhTrang"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            }
        }

        //private void btPhatde_Click(object sender, EventArgs e)
        //{
        //    if (txtNgaythi.Text == "")
        //    {
        //        MessageBox.Show("Chưa nhập ngày thi");
        //    }
        //    else if (cboMonthi.SelectedIndex < 0)
        //    {
        //        MessageBox.Show("Phải chọn môn thi");
        //    }
        //    else if (cboPhongthi.SelectedIndex < 0)
        //    {
        //        MessageBox.Show("Phải chọn phòng thi");
        //    }
        //    else if (cboDegoc.SelectedIndex < 0)
        //    {
        //        MessageBox.Show("Phải chọn đề gốc");
        //    }
        //    else if (kiemtracomade(cboDegoc.Text) == false)
        //    {
        //        MessageBox.Show("Đề gốc chưa có mã đề, yêu cầu tạo mã đề trước!");
        //    }
        //    else if (txtThoigian.Text.Trim() == "" || thoigianlaso() == false)
        //    {
        //        MessageBox.Show("Phải nhập thời gian thi và phải là số");
        //    }
        //    else
        //    {
        //        DataTable dt = Database.GetData("Select*from kithi where Phongthi=@phongthi and Monthi=@monthi", "@phongthi", cboPhongthi.Text, "@monthi", cboMonthi.Text.Trim());
        //        if (dt.Rows.Count > 0)
        //        {
        //            MessageBox.Show("Phòng thi đã được phát đề!");
        //        }
        //        else
        //        {
        //            Database.ExecuteNonQuery("Insert into Kithi(MonThi,NgayThi,ThoiGian,TinhTrang,DeGoc,PhongThi) values(@MonThi,@NgayThi,@ThoiGian,@TinhTrang,@DeGoc,@PhongThi)",
        //                "@MonThi", cboMonthi.Text,
        //                "@NgayThi", txtNgaythi.Text,
        //                "@ThoiGian", Convert.ToInt32(txtThoigian.Text) * 60,
        //                "@TinhTrang", 1,
        //                "@DeGoc", cboDegoc.Text,
        //                "@PhongThi", cboPhongthi.Text);

        //            DataTable dtthisinh = Database.GetData("Select SBD from ThiSinh where Lop=@lop", "@lop", cboPhongthi.Text);
        //            for (int i = 0; i < dtthisinh.Rows.Count; i++)
        //            {
        //                Database.ExecuteNonQuery("Insert into Thisinh_BaithiNC(SBD,DiemThi,MaDe,BaiLam,len,MaMonThi) values(@sbd,@diemthi,@made,@bailam,@len,@mamonthi)", "@sbd", dtthisinh.Rows[i]["SBD"].ToString(), "@diemthi", 0, "@made", "", "@bailam", "", "@len", 0, "@mamonthi",cboMonthi.SelectedValue);
        //            }
        //            //Database.ExecuteNonQuery("Update Kithi set TenKithi=@TenKiThi,MonThi=@MonThi,NgayThi=@NgayThi,ThoiGian=@ThoiGian,TinhTrang=@TinhTrang,DeGoc=@DeGoc where PhongThi=@PhongThi", "@TenKiThi", txtKithi.Text, "@MonThi", dgPhongthi_Dethi.Rows[e.RowIndex].Cells["cboMonthi"].Value.ToString(), "@NgayThi", txtNgaythi.Text, "@ThoiGian", dgPhongthi_Dethi.Rows[e.RowIndex].Cells["ThoiGianThi"].Value.ToString(), "@TinhTrang", 1, "@DeGoc", dgPhongthi_Dethi.Rows[e.RowIndex].Cells["cboDegoc"].Value.ToString(), "@PhongThi", dgPhongthi_Dethi.Rows[e.RowIndex].Cells["Phongthi"].Value.ToString());
        //            MessageBox.Show("Đã phát đề thành công phòng: " + cboPhongthi.Text);
        //            loadDsKithiToGridview();
        //        }
        //    }
        //}

        private void btPhatde_Click(object sender, EventArgs e)
        {
            string phongthi ="";
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
            }
        }

        private void btThuBai_Click(object sender, EventArgs e)
        {
            if (this.dgPhongthi_Dethi.SelectedRows.Count > 0)
            {
                DialogResult response = MessageBox.Show("Bạn có chắc chắn muốn thu bài các phòng đã chọn?", "Thu bài thí",
                                  MessageBoxButtons.YesNo,
                                  MessageBoxIcon.Question,
                                  MessageBoxDefaultButton.Button2);
                if (response == DialogResult.Yes)
                {
                    foreach (DataGridViewRow row in dgPhongthi_Dethi.SelectedRows)
                    {
                        string sql = "Update Thisinh set Tinhtrang=@tinhtrang where Lop=@phongthi and Tinhtrang=@hientai";
                        Database.ExecuteNonQuery(sql, "@phongthi", row.Cells["PhongThi"].Value.ToString(), "@tinhtrang", 1, "@hientai", 2);

                        Database.ExecuteNonQuery("Update Kithi set Tinhtrang=@tinhtrang where Phongthi=@phongthi and Tinhtrang=@ttHientai", "@tinhtrang", 0, "@phongthi", row.Cells["PhongThi"].Value.ToString(), "@ttHientai", 1);                        
                    }
                    MessageBox.Show("Đã thu bài các phòng!");
                    //loadDanhSachThisinh();
                    loadDanhSachThisinhByPhongthi();
                    loadDsKithiToGridview();
                }
            }
            else
            {
                MessageBox.Show("Bạn phải chọn phòng thi trước khi thu bài!");
            }           
        }

        private bool thoigianlaso()
        {
            bool ret = true;
            if (txtThoigian.Text != null)
            {
                char[] chars = txtThoigian.Text.Trim().ToCharArray();
                foreach (char c in chars)
                {
                    if (char.IsDigit(c) == false)
                    {
                        ret = false;
                    }
                }
            }
            return ret;
        }

        private void txtThoigian_TextChanged(object sender, EventArgs e)
        {
            if (thoigianlaso() == false)
                MessageBox.Show("Thời gian phải là số phút và không có khoảng trắng!");
        }

        public bool kiemtracomade(string degoc)
        {
            DataTable dt = Database.GetData("Select*from DeThi where Degoc = @degoc", "@degoc", degoc);
            if (dt.Rows.Count >= 1) return true;
            else return false;
        }

        //private void btInPhieutraloicanhan_Click(object sender, EventArgs e)
        //{
        //    DataTable DT = Database.GetData("Select SBD, HoTen, Ten,Ngaysinh,Noisinh,Lop,Diemthi,BaiLam,ts.MaDe,MonThi,KetCau_De from Thisinh as ts,Kithi as kt, Dethi as dt  where ts.Lop=kt.Phongthi and ts.MaDe=dt.MaDe and Lop=@phongthi order by SBD", "@phongthi", cboPhongthi.SelectedValue.ToString());
        //    if (DT.Rows.Count >= 1)
        //    {
        //        //crpFormPhieuTraLoi crpform = new crpFormPhieuTraLoi();
        //        //crpform.phongthi = cboPhongthi.SelectedValue.ToString();
        //        ////crpform.MdiParent = this;
        //        //crpform.ShowDialog();

        //        crpFormPTLSymbol crp = new crpFormPTLSymbol();
        //        crp.phongthi = cboPhongthi.SelectedValue.ToString();
        //        crp.ShowDialog();
        //    }
        //    else MessageBox.Show("Chưa có bài thi nào!");
        //}

        private void btInTatcaPhieutraloi_Click(object sender, EventArgs e)
        {
            //DataTable DT = Database.GetData("Select SBD,HoTen,Ten,Ngaysinh,Noisinh,Lop,Diemthi,BaiLam,ts.MaDe,MonThi,KetCau_De from Thisinh as ts,Kithi as kt, Dethi as dt  where ts.Lop=kt.Phongthi and ts.MaDe=dt.MaDe order by Lop, SBD");
            //DataTable DT = Database.GetData("Select tb.SBD,HoTen,Ten,NgaySinh,NoiSinh,Lop,tb.DiemThi,tb.MaDe,tb.Bailam,TenMonThi,KetCau_De from Thisinh as ts,Thisinh_BaithiNC as tb, MonThi as mt, Dethi as dt where ts.SBD=tb.SBD and tb.MaMonThi =mt.MaMonThi and tb.MaDe=dt.MaDe order by Lop");
            DataTable DT = Database.GetData("SELECT tb.SBD, ts.HoTen, ts.Ten, ts.NgaySinh, ts.NoiSinh, ts.Lop, tb.DiemThi, tb.MaDe, tb.BaiLam, mt.TenMonThi, dt.KetCau_De " +
            "FROM ThiSinh AS ts INNER JOIN Thisinh_BaithiNC AS tb ON ts.SBD = tb.SBD " +
            "INNER JOIN MonThi AS mt ON tb.MaMonThi = mt.MaMonThi " +
            "INNER JOIN DeThi AS dt ON tb.MaDe = dt.MaDe ORDER BY ts.Lop");
            if (DT.Rows.Count >= 1)
            {
                //crpFormPhieuTraLoi crpform = new crpFormPhieuTraLoi();
                ////crpform.phongthi = cboPhongthi.SelectedValue.ToString();
                ////crpform.MdiParent = this;
                //crpform.phongthi = "ALL";
                //crpform.ShowDialog();

                //crpFormPTLSymbol crp = new crpFormPTLSymbol();
                //crp.phongthi = "ALL";
                ////crp.monthi = cbo
                //crp.ShowDialog();

                crpFormAnswerSheet crp = new crpFormAnswerSheet();
                crp.phongthi = "ALL";
                //crp.monthi = cbo
                crp.ShowDialog();
            }
            else MessageBox.Show("Chưa có bài thi nào!");
        }

        private void rdShowscore_CheckedChanged(object sender, EventArgs e)
        {
            if (rdShowscore.Checked)
            {
                Database.ExecuteNonQuery("Update tblConfig set Variable=@var where Name=@name", "@var", "dung", "@name", "showscore");
                MessageBox.Show("Đã cấu hình cho hiển thị điểm!");
            }
        }

        private void rdUnshowscore_CheckedChanged(object sender, EventArgs e)
        {
            if (rdUnshowscore.Checked)
            {
                Database.ExecuteNonQuery("Update tblConfig set Variable=@var where Name=@name", "@var", "sai", "@name", "showscore");
                MessageBox.Show("Đã cấu hình KHÔNG cho hiển thị điểm!");
            }
        }

        private void btPrintByClass_Click(object sender, EventArgs e)
        {
            crpFormAnswerSheet crp = new crpFormAnswerSheet();            
            crp.phongthi = cboPhongthi.SelectedValue.ToString();
            crp.ShowDialog();
        }

        private void btPrintListByClass_Click(object sender, EventArgs e)
        {
            crpFormScoreList crpform = new crpFormScoreList();           
            crpform.phongthi = cboPhongthi.SelectedValue.ToString();
            //crpform.MdiParent = this;
            crpform.ShowDialog();
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            if (txtNgaythi.Text == "")
            {
                MessageBox.Show("Chưa nhập ngày thi");
            }
            else if (cboMonthi.SelectedIndex < 0)
            {
                MessageBox.Show("Phải chọn môn thi");
            }
            else if (cboPhongthi.SelectedIndex < 0)
            {
                MessageBox.Show("Phải chọn phòng thi");
            }
            else if (cboDegoc.SelectedIndex < 0)
            {
                MessageBox.Show("Phải chọn đề gốc");
            }
            else if (kiemtracomade(cboDegoc.Text) == false)
            {
                MessageBox.Show("Đề gốc chưa có mã đề, yêu cầu tạo mã đề trước!");
            }
            else if (txtThoigian.Text.Trim() == "" || thoigianlaso() == false)
            {
                MessageBox.Show("Phải nhập thời gian thi và phải là số");
            }
            else
            {
                DataTable dt = Database.GetData("Select*from kithi where Phongthi=@phongthi and Monthi=@monthi", "@phongthi", cboPhongthi.Text, "@monthi", cboMonthi.Text.Trim());
                if (dt.Rows.Count > 0)
                {
                    MessageBox.Show("Phòng thi đã được phân đề!");
                }
                else
                {
                    Database.ExecuteNonQuery("Insert into Kithi(MonThi,NgayThi,ThoiGian,TinhTrang,DeGoc,PhongThi) values(@MonThi,@NgayThi,@ThoiGian,@TinhTrang,@DeGoc,@PhongThi)",
                        "@MonThi", cboMonthi.Text,
                        "@NgayThi", txtNgaythi.Text,
                        "@ThoiGian", Convert.ToInt32(txtThoigian.Text) * 60,
                        "@TinhTrang", 0,
                        "@DeGoc", cboDegoc.Text,
                        "@PhongThi", cboPhongthi.Text);

                    DataTable dtthisinh = Database.GetData("Select SBD from ThiSinh where Lop=@lop", "@lop", cboPhongthi.Text);
                    for (int i = 0; i < dtthisinh.Rows.Count; i++)
                    {
                        Database.ExecuteNonQuery("Insert into Thisinh_BaithiNC(SBD,DiemThi,MaDe,BaiLam,len,MaMonThi) values(@sbd,@diemthi,@made,@bailam,@len,@mamonthi)", "@sbd", dtthisinh.Rows[i]["SBD"].ToString(), "@diemthi", 0, "@made", "", "@bailam", "", "@len", 0, "@mamonthi", cboMonthi.SelectedValue);
                    }
                    //Database.ExecuteNonQuery("Update Kithi set TenKithi=@TenKiThi,MonThi=@MonThi,NgayThi=@NgayThi,ThoiGian=@ThoiGian,TinhTrang=@TinhTrang,DeGoc=@DeGoc where PhongThi=@PhongThi", "@TenKiThi", txtKithi.Text, "@MonThi", dgPhongthi_Dethi.Rows[e.RowIndex].Cells["cboMonthi"].Value.ToString(), "@NgayThi", txtNgaythi.Text, "@ThoiGian", dgPhongthi_Dethi.Rows[e.RowIndex].Cells["ThoiGianThi"].Value.ToString(), "@TinhTrang", 1, "@DeGoc", dgPhongthi_Dethi.Rows[e.RowIndex].Cells["cboDegoc"].Value.ToString(), "@PhongThi", dgPhongthi_Dethi.Rows[e.RowIndex].Cells["Phongthi"].Value.ToString());
                    MessageBox.Show("Đã phân đề thành công phòng: " + cboPhongthi.Text);
                    loadDsKithiToGridview();
                }
            }
        }
    }
}
