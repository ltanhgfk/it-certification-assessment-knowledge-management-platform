using System;
//using System.Collections.Generic;
//using System.ComponentModel;
using System.Data;
//using System.Drawing;
//using System.IO;
//using System.Linq;
//using System.Text;
using System.Windows.Forms;
using Microsoft.Office.Interop.Word;
//using System.Drawing.Drawing2D;
using RTF;

namespace ThiTracNghemUDCNTT
{
    public partial class formDethi : Form
    {
        private const Keys CopyKey = Keys.Control | Keys.C;
        private const Keys PasteKey = Keys.Control | Keys.V;

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if ((keyData == CopyKey) || (keyData == PasteKey))
            {
                return true;
            }
            else
            {
                return base.ProcessCmdKey(ref msg, keyData);
            }
        }
        
        public formDethi()
        {
            InitializeComponent();
        }

        private void formDethi_Load(object sender, EventArgs e)
        {
            loadDataToMaTranDe();
            loadDegoc();
        }


        private void btLoadFile_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialogModun.ShowDialog(); // Show the dialog.
            string file = "";
            if (result == DialogResult.OK) // Test result.
            {
                file = openFileDialogModun.FileName;
                txtFile.Text = file;
            }
        }

        private void btSaveFile_Click(object sender, EventArgs e)
        {
            if (txtFile.Text != "")//Neu duong dan file khong trong
            {
                RTFBuilderbase sb = new RTFBuilder();   //tao doi tuongg 
                Globals.BuilderCode(sb, txtFile.Text);  //chuyen noi dung file word sang chuoi sb
                this.rtbDethi.Rtf = sb.ToString();      //hien thi chuoi sb len richtexteditor
                loadDataToMaTranDe();
            }
            else MessageBox.Show("Bạn chưa chọn tập tin ngân hàng câu hỏi!");
        }

        public void refreshMaTranDe()
        {
            string sql = "SELECT TenModun, COUNT(CauHoi) AS SoLuong FROM TracNghiem GROUP BY TenMoDun";
            System.Data.DataTable dt = new System.Data.DataTable();
            dt = Database.GetData(sql);
            dgMaTranDe.DataSource = dt;
        }

        public void loadDataToMaTranDe()
        {
            string sql = "SELECT TenModun, COUNT(CauHoi) AS SoLuong FROM TracNghiem GROUP BY TenMoDun";
            System.Data.DataTable dt = new System.Data.DataTable();
            dt = Database.GetData(sql);

            System.Data.DataTable table = new System.Data.DataTable();
            table.Columns.Add("TenModun", typeof(string));
            table.Columns.Add("Soluong", typeof(string));
            table.Columns.Add("CauhoitrenDe", typeof(string));

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr1 = table.NewRow();
                dr1["TenModun"] = dt.Rows[i]["TenMoDun"].ToString();
                dr1["Soluong"] = dt.Rows[i]["SoLuong"].ToString();
                dr1["CauhoitrenDe"] = 0;

                table.Rows.Add(dr1.ItemArray);
            }

            dgMaTranDe.DataSource = table;
            dgMaTranDe.Columns["TenMoDun"].HeaderText = "Tên mô đun";
            dgMaTranDe.Columns["TenMoDun"].Width = 165;
            dgMaTranDe.Columns["TenMoDun"].ReadOnly = true;
            dgMaTranDe.Columns["TenMoDun"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            //Add combobox to gridview
            DataGridViewComboBoxColumn dgvCmb = new DataGridViewComboBoxColumn();
            dgvCmb.HeaderText = "Tên môn thi";
            dgvCmb.Name = "cboMonthi";

            string getMonthi = "Select * from Monthi order by Thutu";
            System.Data.DataTable tbMonthi = new System.Data.DataTable();
            tbMonthi = Database.GetData(getMonthi);

            dgvCmb.DataSource = tbMonthi;
            dgvCmb.DisplayMember = "MaMonThi";
            dgvCmb.ValueMember = "MaMonThi";
            dgvCmb.DisplayIndex = 1;

            dgMaTranDe.Columns.Add(dgvCmb);
            dgMaTranDe.Columns["cboMonthi"].Width = 150;

            dgMaTranDe.Columns["Soluong"].HeaderText = "Số lượng";
            dgMaTranDe.Columns["Soluong"].Width = 70;
            dgMaTranDe.Columns["Soluong"].ReadOnly = true;
            dgMaTranDe.Columns["Soluong"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgMaTranDe.Columns["CauhoitrenDe"].HeaderText = "Câu hỏi/đề";
            dgMaTranDe.Columns["CauhoitrenDe"].Width = 75;
            dgMaTranDe.Columns["CauhoitrenDe"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        private void dgMaTranDe_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            DataGridViewColumn col = dgMaTranDe.Columns[e.ColumnIndex] as DataGridViewColumn;
            if (col.Name.ToLower() == "cauhoitrende")
            {
                DataGridViewTextBoxCell cell = dgMaTranDe[e.ColumnIndex, e.RowIndex] as DataGridViewTextBoxCell;
                if (cell != null)
                {
                    char[] chars = e.FormattedValue.ToString().ToCharArray();
                    foreach (char c in chars)
                    {
                        if (char.IsDigit(c) == false)
                        {
                            MessageBox.Show("Số câu hỏi/đề phải là số và không có khoảng trắng!");
                            e.Cancel = true;
                            break;
                        }
                    }
                }
            }
        }

        private void dgMaTranDe_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewColumn col = dgMaTranDe.Columns[e.ColumnIndex] as DataGridViewColumn;
            if (col.Name.ToLower() == "cauhoitrende")
            {
                if (Convert.ToInt32(dgMaTranDe.Rows[e.RowIndex].Cells["CauhoitrenDe"].Value.ToString()) > Convert.ToInt32(dgMaTranDe.Rows[e.RowIndex].Cells["Soluong"].Value.ToString()))
                {
                    MessageBox.Show("Số câu hỏi/đề phải là nhỏ hơn số lượng câu hỏi!");
                }
            }
        }

        private void btTaoDeGoc_Click(object sender, EventArgs e)  
        {            
            string sql = "";
            string tenmodun = "";
            string sqlInsert = "";
            System.Data.DataTable dt = new System.Data.DataTable();
            System.Data.DataTable dtcb = new System.Data.DataTable();
            System.Data.DataTable table = new System.Data.DataTable();
            int soluong = 0;
            dgMaTranDe.EndEdit();
            int kt = 0;
            int ktselectedcbo = 0;
            string ketcaude = "";
            int sttcb = 1;
            string options = "";
            int sl = 0;
            foreach (DataGridViewRow row in dgMaTranDe.Rows)
            {
                if (Convert.ToInt32(row.Cells["CauhoitrenDe"].Value) >= 1)
                {
                    sl = sl + 1;
                    if (Convert.ToInt32(row.Cells["CauhoitrenDe"].Value) > Convert.ToInt32(row.Cells["Soluong"].Value))
                    {
                        kt = kt + 1;
                    }

                    if (row.Cells["cboMonthi"].Value == null)
                    {
                        ktselectedcbo = ktselectedcbo + 1;
                    }
                }
            }
            if (sl == 0)
            {
                MessageBox.Show("Số lượng câu hỏi/đề phải lớn hơn 0!");
            }
            if (kt >= 1)
            {
                MessageBox.Show("Số câu hỏi/đề phải nhỏ hơn số lượng câu hỏi!");
            }
            else if (ktselectedcbo >= 1)
            {
                MessageBox.Show("Phải chọn tên môn thi!");
            }
            else
            {
                //bat đầu for lớn
                System.Data.DataTable dtMonthi = Database.GetData("Select * from Monthi order by Thutu");
                for (int k = 0; k < dtMonthi.Rows.Count; k++)
                {
                    table.Reset();
                    foreach (DataGridViewRow row in dgMaTranDe.Rows)
                    {
                        dtcb.Reset();
                        if (Convert.ToInt32(row.Cells["CauhoitrenDe"].Value) >= 1)
                        {
                            if (row.Cells["cboMonthi"].Value.ToString() == dtMonthi.Rows[k]["MaMonThi"].ToString())//"CNTTCB"
                            {
                                soluong = soluong + Convert.ToInt32(row.Cells["CauhoitrenDe"].Value);
                                tenmodun = row.Cells["cboMonthi"].Value.ToString();

                                sql = "select top(@soluongtrende) Cauhoi,DapAnDung,DaoDapAn,SoDapAn from Tracnghiem where Tenmodun=@tenmodun order by newid()";
                                dtcb = Database.GetData(sql, "@soluongtrende", Convert.ToInt32(row.Cells["CauhoitrenDe"].Value.ToString()), "@tenmodun", row.Cells["TenModun"].Value.ToString());

                                table.Merge(dtcb);
                            }
                        }
                    }
                    if (table.Rows.Count >= 1)
                    {
                        sttcb = (int)Database.ExecuteScalar("select count(degoc) as countdegoc from Degoc where TenDegoc = @tendegoc", "@tendegoc", tenmodun) + 1;
                        for (int i = 0; i < table.Rows.Count; i++)
                        {
                            options = "";
                            Database.ExecuteNonQuery("Insert into ChitietDegoc(Degoc,Cauhoi) values(@degoc,@cauhoi)", "@degoc", dtMonthi.Rows[k]["MaMonThi"].ToString() + "_" + sttcb.ToString("00"), "@cauhoi", table.Rows[i]["Cauhoi"].ToString());
                            System.Data.DataTable getOptions = Database.GetData("Select * from ChitietTracnghiem where Cauhoi=@cauhoi order by Position", "@cauhoi", table.Rows[i]["Cauhoi"].ToString());
                            for (int j = 0; j < getOptions.Rows.Count; j++)
                            {
                                options = options + getOptions.Rows[j]["Position"].ToString() + ";";
                            }
                            ketcaude = ketcaude + table.Rows[i]["Cauhoi"].ToString() + ";" + options + table.Rows[i]["DapAnDung"].ToString() + ";" + table.Rows[i]["SoDapAn"].ToString() + ";" + table.Rows[i]["DaoDapAn"].ToString() + "#";
                        }

                        sqlInsert = "Insert into Degoc(DeGoc,KetCau_De,TenDegoc,Soluong) values(@DeGoc,@KetCau_De,@TenDegoc,@Soluong)";
                        Database.ExecuteNonQuery(sqlInsert, "@DeGoc", dtMonthi.Rows[k]["MaMonThi"].ToString() + "_" + sttcb.ToString("00"), "@KetCau_De", ketcaude, "@TenDegoc", tenmodun, "@Soluong", soluong);
                        soluong = 0;
                        ketcaude = "";
                    }
                }
                //ket thuc for lớn 
            }
            loadDegoc();
        }


        //else
        //{
        //    sql = "select top(@soluongtrende) Cauhoi,DapAnDung,DaoDapAn,SoDapAn from Tracnghiem where Tenmodun=@tenmodun order by newid()";
        //    dt = Database.GetData(sql, "@soluongtrende", Convert.ToInt32(row.Cells["CauhoitrenDe"].Value.ToString()), "@tenmodun", row.Cells["TenModun"].Value.ToString());

        //    sttnc = (int)Database.ExecuteScalar("select count(degoc) as countdegoc from Degoc where TenDegoc = @tendegoc", "@tendegoc", row.Cells["cboMonthi"].Value.ToString()) + 1;
        //    for (int i = 0; i < dt.Rows.Count; i++)
        //    {
        //        options = "";
        //        Database.ExecuteNonQuery("Insert Into ChitietDegoc(Degoc,Cauhoi) values(@degoc,@cauhoi)", "@degoc", row.Cells["cboMonthi"].Value.ToString() + "_" + sttnc.ToString("00"), "@cauhoi", dt.Rows[i]["Cauhoi"].ToString());
        //        System.Data.DataTable countoptions = Database.GetData("Select * from ChitietTracnghiem where Cauhoi=@cauhoi order by Position", "@cauhoi", dt.Rows[i]["Cauhoi"].ToString());
        //        for (int j = 0; j < countoptions.Rows.Count; j++)
        //        {
        //            options = options + countoptions.Rows[j]["Position"].ToString() + ";";
        //        }
        //        ketcaude = ketcaude + dt.Rows[i]["Cauhoi"].ToString() + ";" + options + dt.Rows[i]["DapAnDung"].ToString() + ";" + dt.Rows[i]["SoDapAn"].ToString() + ";" + dt.Rows[i]["DaoDapAn"].ToString() + "#";
        //    }

        //    sqlInsert = "Insert Into Degoc(DeGoc,KetCau_De,TenDegoc, Soluong) values(@DeGoc,@KetCau_De,@TenDegoc,@Soluong)";
        //    Database.ExecuteNonQuery(sqlInsert, "@DeGoc", row.Cells["cboMonthi"].Value.ToString() + "_" + sttnc.ToString("00"), "@KetCau_De", ketcaude, "@TenDegoc", row.Cells["cboMonthi"].Value.ToString(), "@Soluong", Convert.ToInt32(row.Cells["CauhoitrenDe"].Value.ToString()));
        //    ketcaude = "";
        //}
        //private void btTaoDeGoc_Click(object sender, EventArgs e)  //phương thức này đúng nhưng mới chỉ trộn đề cnttcb 
        //{
        //    int sttnc = 0;
        //    string sql = "";
        //    string tenmodun = "";
        //    string sqlInsert = "";
        //    System.Data.DataTable dt = new System.Data.DataTable();
        //    System.Data.DataTable dtcb = new System.Data.DataTable();
        //    System.Data.DataTable table = new System.Data.DataTable();
        //    int soluong = 0;
        //    dgMaTranDe.EndEdit();
        //    int kt = 0;
        //    int ktselectedcbo = 0;
        //    string ketcaude = "";
        //    int sttcb = 1;
        //    string options = "";
        //    int sl = 0;
        //    foreach (DataGridViewRow row in dgMaTranDe.Rows)
        //    {
        //        if (Convert.ToInt32(row.Cells["CauhoitrenDe"].Value) >= 1)
        //        {
        //            sl = sl + 1;
        //            if (Convert.ToInt32(row.Cells["CauhoitrenDe"].Value) > Convert.ToInt32(row.Cells["Soluong"].Value))
        //            {
        //                kt = kt + 1;
        //            }

        //            if (row.Cells["cboMonthi"].Value == null)
        //            {
        //                ktselectedcbo = ktselectedcbo + 1;
        //            }
        //        }
        //    }
        //    if (sl == 0)
        //    {
        //        MessageBox.Show("Số lượng câu hỏi/đề phải lớn hơn 0!");
        //    }
        //    if (kt >= 1)
        //    {
        //        MessageBox.Show("Số câu hỏi/đề phải nhỏ hơn số lượng câu hỏi!");
        //    }
        //    else if (ktselectedcbo >= 1)
        //    {
        //        MessageBox.Show("Phải chọn tên môn thi!");
        //    }
        //    else
        //    {
        //        foreach (DataGridViewRow row in dgMaTranDe.Rows)
        //        {
        //            if (Convert.ToInt32(row.Cells["CauhoitrenDe"].Value) >= 1)
        //            {
        //                if (row.Cells["cboMonthi"].Value.ToString() == "CNTTCB")
        //                {
        //                    soluong = soluong + Convert.ToInt32(row.Cells["CauhoitrenDe"].Value);
        //                    tenmodun = row.Cells["cboMonthi"].Value.ToString();

        //                    sql = "select top(@soluongtrende) Cauhoi,DapAnDung,DaoDapAn,SoDapAn from Tracnghiem where Tenmodun=@tenmodun order by newid()";
        //                    dtcb = Database.GetData(sql, "@soluongtrende", Convert.ToInt32(row.Cells["CauhoitrenDe"].Value.ToString()), "@tenmodun", row.Cells["TenModun"].Value.ToString());

        //                    table.Merge(dtcb);
        //                }
        //                else
        //                {
        //                    sql = "select top(@soluongtrende) Cauhoi,DapAnDung,DaoDapAn,SoDapAn from Tracnghiem where Tenmodun=@tenmodun order by newid()";
        //                    dt = Database.GetData(sql, "@soluongtrende", Convert.ToInt32(row.Cells["CauhoitrenDe"].Value.ToString()), "@tenmodun", row.Cells["TenModun"].Value.ToString());

        //                    sttnc = (int)Database.ExecuteScalar("select count(degoc) as countdegoc from Degoc where TenDegoc = @tendegoc", "@tendegoc", row.Cells["cboMonthi"].Value.ToString()) + 1;
        //                    for (int i = 0; i < dt.Rows.Count; i++)
        //                    {
        //                        options = "";
        //                        Database.ExecuteNonQuery("Insert Into ChitietDegoc(Degoc,Cauhoi) values(@degoc,@cauhoi)", "@degoc", row.Cells["cboMonthi"].Value.ToString() + "_" + sttnc.ToString("00"), "@cauhoi", dt.Rows[i]["Cauhoi"].ToString());
        //                        System.Data.DataTable countoptions = Database.GetData("Select * from ChitietTracnghiem where Cauhoi=@cauhoi order by Position", "@cauhoi", dt.Rows[i]["Cauhoi"].ToString());
        //                        for (int j = 0; j < countoptions.Rows.Count; j++)
        //                        {
        //                            options = options + countoptions.Rows[j]["Position"].ToString() + ";";
        //                        }
        //                        ketcaude = ketcaude + dt.Rows[i]["Cauhoi"].ToString() + ";" + options + dt.Rows[i]["DapAnDung"].ToString() + ";" + dt.Rows[i]["SoDapAn"].ToString() + ";" + dt.Rows[i]["DaoDapAn"].ToString() + "#";
        //                    }

        //                    sqlInsert = "Insert Into Degoc(DeGoc,KetCau_De,TenDegoc, Soluong) values(@DeGoc,@KetCau_De,@TenDegoc,@Soluong)";
        //                    Database.ExecuteNonQuery(sqlInsert, "@DeGoc", row.Cells["cboMonthi"].Value.ToString() + "_" + sttnc.ToString("00"), "@KetCau_De", ketcaude, "@TenDegoc", row.Cells["cboMonthi"].Value.ToString(), "@Soluong", Convert.ToInt32(row.Cells["CauhoitrenDe"].Value.ToString()));
        //                    ketcaude = "";
        //                }
        //            }
        //        }
        //        if (table.Rows.Count >= 1)
        //        {
        //            sttcb = (int)Database.ExecuteScalar("select count(degoc) as countdegoc from Degoc where TenDegoc = @tendegoc", "@tendegoc", tenmodun) + 1;
        //            for (int i = 0; i < table.Rows.Count; i++)
        //            {
        //                options = "";
        //                Database.ExecuteNonQuery("Insert into ChitietDegoc(Degoc,Cauhoi) values(@degoc,@cauhoi)", "@degoc", "CNTTCB_" + sttcb.ToString("00"), "@cauhoi", table.Rows[i]["Cauhoi"].ToString());
        //                System.Data.DataTable getOptions = Database.GetData("Select * from ChitietTracnghiem where Cauhoi=@cauhoi order by Position", "@cauhoi", table.Rows[i]["Cauhoi"].ToString());
        //                for (int j = 0; j < getOptions.Rows.Count; j++)
        //                {
        //                    options = options + getOptions.Rows[j]["Position"].ToString() + ";";
        //                }
        //                ketcaude = ketcaude + table.Rows[i]["Cauhoi"].ToString() + ";" + options + table.Rows[i]["DapAnDung"].ToString() + ";" + table.Rows[i]["SoDapAn"].ToString() + ";" + table.Rows[i]["DaoDapAn"].ToString() + "#";
        //            }

        //            sqlInsert = "Insert into Degoc(DeGoc,KetCau_De,TenDegoc,Soluong) values(@DeGoc,@KetCau_De,@TenDegoc,@Soluong)";
        //            Database.ExecuteNonQuery(sqlInsert, "@DeGoc", "CNTTCB_" + sttcb.ToString("00"), "@KetCau_De", ketcaude, "@TenDegoc", tenmodun, "@Soluong", soluong);
        //            soluong = 0;
        //        }
        //    }
        //    loadDegoc();
        //}

        //private void btTaoDeGoc_Click(object sender, EventArgs e)
        //{
        //    int sttnc = 0;
        //    string tenmodun = "";
        //    System.Data.DataTable dt = new System.Data.DataTable();
        //    System.Data.DataTable dtcb = new System.Data.DataTable();
        //    System.Data.DataTable table = new System.Data.DataTable();
        //    int soluong = 0;
        //    int kt = 0;
        //    string ketcaude = "";
        //    int sttcb = 1;
        //    string options = "";

        //    dgMaTranDe.EndEdit();
        //    foreach (DataGridViewRow row in dgMaTranDe.Rows)
        //    {
        //        if (Convert.ToInt32(row.Cells["CauhoitrenDe"].Value) >= 1)
        //        {
        //            if (Convert.ToInt32(row.Cells["CauhoitrenDe"].Value) > Convert.ToInt32(row.Cells["Soluong"].Value))
        //            {
        //                kt = kt + 1;
        //            }
        //        }
        //    }
        //    if (kt >= 1)
        //    {
        //        MessageBox.Show("Số câu hỏi/đề phải nhỏ hơn số lượng câu hỏi!");
        //    }
        //    else
        //    {
        //        foreach (DataGridViewRow row in dgMaTranDe.Rows)
        //        {
        //            if (Convert.ToInt32(row.Cells["CauhoitrenDe"].Value) >= 1)
        //            {
        //                if (row.Cells["TenMoDun"].Value.ToString() == "Modun 1" || row.Cells["TenMoDun"].Value.ToString() == "Modun 2" || row.Cells["TenMoDun"].Value.ToString() == "Modun 3" || row.Cells["TenMoDun"].Value.ToString() == "Modun 4" || row.Cells["TenMoDun"].Value.ToString() == "Modun 5" || row.Cells["TenMoDun"].Value.ToString() == "Modun 6")
        //                {
        //                    soluong = soluong + Convert.ToInt32(row.Cells["CauhoitrenDe"].Value);
        //                    tenmodun = row.Cells["TenMoDun"].Value.ToString();
        //                    dtcb = Database.GetData("select top(@soluongtrende) Cauhoi,DapAnDung,DaoDapAn from Tracnghiem where Tenmodun=@tenmodun order by newid()", "@soluongtrende", Convert.ToInt32(row.Cells["CauhoitrenDe"].Value.ToString()), "@tenmodun", row.Cells["TenModun"].Value.ToString());
        //                    table.Merge(dtcb); // tạo de cntt cb
        //                }
        //                else
        //                {
        //                    dt = Database.GetData("select top(@soluongtrende) Cauhoi,DapAnDung,DaoDapAn,SoDapAn from Tracnghiem where Tenmodun=@tenmodun order by newid()", "@soluongtrende", Convert.ToInt32(row.Cells["CauhoitrenDe"].Value.ToString()), "@tenmodun", row.Cells["TenModun"].Value.ToString());
        //                    sttnc = (int)Database.ExecuteScalar("select count(DeGoc) as countdegoc from Degoc where DeGoc like '%'+ @tenmodun +'%'", "@tenmodun", row.Cells["TenModun"].Value.ToString().Trim()) + 1;
        //                    for (int i = 0; i < dt.Rows.Count; i++)
        //                    {
        //                        options = "";
        //                        Database.ExecuteNonQuery("Insert Into ChitietDegoc(Degoc,Cauhoi) values(@degoc,@cauhoi)", "@degoc", row.Cells["TenMoDun"].Value.ToString() + "_" + sttnc.ToString("00"), "@cauhoi", dt.Rows[i]["Cauhoi"].ToString());
        //                        System.Data.DataTable countoptions = Database.GetData("Select * from ChitietTracnghiem where Cauhoi=@cauhoi order by Position", "@cauhoi", dt.Rows[i]["Cauhoi"].ToString());
        //                        for (int j = 0; j < countoptions.Rows.Count; j++)
        //                        {
        //                            options = options + countoptions.Rows[j]["Position"].ToString() + ";";
        //                        }
        //                        ketcaude = ketcaude + dt.Rows[i]["Cauhoi"].ToString() + ";" + options + dt.Rows[i]["DapAnDung"].ToString() + ";" + dt.Rows[i]["SoDapAn"].ToString() + ";" + dt.Rows[i]["DaoDapAn"].ToString() + "#";
        //                    }
        //                    Database.ExecuteNonQuery("Insert Into Degoc(DeGoc,KetCau_De,Soluong) values(@DeGoc,@KetCau_De,@Soluong)", "@DeGoc", row.Cells["TenMoDun"].Value.ToString() + "_" + sttnc.ToString("00"), "@KetCau_De", ketcaude, "@Soluong", Convert.ToInt32(row.Cells["CauhoitrenDe"].Value.ToString()));
        //                    ketcaude = "";
        //                }
        //            }
        //        }
        //        if (table.Rows.Count >= 1)
        //        {
        //            sttcb = (int)Database.ExecuteScalar("select count(degoc) as countdegoc from Degoc where Degoc = @degoc", "@degoc", tenmodun) + 1;//sai cho nay
        //            for (int i = 0; i < table.Rows.Count; i++)
        //            {
        //                options = "";
        //                Database.ExecuteNonQuery("Insert into ChitietDegoc(Degoc,Cauhoi) values(@degoc,@cauhoi)", "@degoc", "CNTTCB_" + sttcb.ToString("00"), "@cauhoi", table.Rows[i]["Cauhoi"].ToString());
        //                System.Data.DataTable countoptions = Database.GetData("Select * from ChitietTracnghiem where Cauhoi=@cauhoi order by Position", "@cauhoi", table.Rows[i]["Cauhoi"].ToString());
        //                for (int j = 0; j < countoptions.Rows.Count; j++)
        //                {
        //                    options = options + countoptions.Rows[j]["Position"].ToString() + ";";
        //                }
        //                ketcaude = ketcaude + table.Rows[i]["Cauhoi"].ToString() + ";" + options + table.Rows[i]["DapAnDung"].ToString() + ";" + dt.Rows[i]["SoDapAn"].ToString() + ";" + table.Rows[i]["DaoDapAn"].ToString() + "#";
        //            }
        //            Database.ExecuteNonQuery("Insert into Degoc(DeGoc,KetCau_De,Soluong) values(@DeGoc,@KetCau_De,@Soluong)", "@DeGoc", "CNTTCB_" + sttcb.ToString("00"), "@KetCau_De", ketcaude, "@Soluong", soluong);
        //            soluong = 0;
        //        }
        //    }
        //    loadDegoc();
        //}

        //private void btTaoDeGoc_Click(object sender, EventArgs e)
        //{
        //    int sttnc = 0;
        //    string tenmodun = "";
        //    System.Data.DataTable dt = new System.Data.DataTable();
        //    System.Data.DataTable dtcb = new System.Data.DataTable();
        //    System.Data.DataTable table = new System.Data.DataTable();
        //    int soluong = 0;
        //    int kt = 0;
        //    string ketcaude = "";
        //    int sttcb = 1;
        //    string options = "";

        //    dgMaTranDe.EndEdit();
        //    foreach (DataGridViewRow row in dgMaTranDe.Rows)
        //    {
        //        if (Convert.ToInt32(row.Cells["CauhoitrenDe"].Value) >= 1)
        //        {
        //            if (Convert.ToInt32(row.Cells["CauhoitrenDe"].Value) > Convert.ToInt32(row.Cells["Soluong"].Value))
        //            {
        //                kt = kt + 1;
        //            }
        //        }
        //    }
        //    if (kt >= 1)
        //    {
        //        MessageBox.Show("Số câu hỏi/đề phải nhỏ hơn số lượng câu hỏi!");
        //    }
        //    else
        //    {
        //        foreach (DataGridViewRow row in dgMaTranDe.Rows)
        //        {
        //            if (Convert.ToInt32(row.Cells["CauhoitrenDe"].Value) >= 1)
        //            {
        //                if (row.Cells["TenMoDun"].Value.ToString() == "Modun 1" || row.Cells["TenMoDun"].Value.ToString() == "Modun 2" || row.Cells["TenMoDun"].Value.ToString() == "Modun 3" || row.Cells["TenMoDun"].Value.ToString() == "Modun 4" || row.Cells["TenMoDun"].Value.ToString() == "Modun 5" || row.Cells["TenMoDun"].Value.ToString() == "Modun 6")
        //                {
        //                    soluong = soluong + Convert.ToInt32(row.Cells["CauhoitrenDe"].Value);
        //                    tenmodun = row.Cells["TenMoDun"].Value.ToString();
        //                    dtcb = Database.GetData("select top(@soluongtrende) Cauhoi,DapAnDung,DaoDapAn from Tracnghiem where Tenmodun=@tenmodun order by newid()", "@soluongtrende", Convert.ToInt32(row.Cells["CauhoitrenDe"].Value.ToString()), "@tenmodun", row.Cells["TenModun"].Value.ToString());
        //                    table.Merge(dtcb);
        //                }
        //                else
        //                {
        //                    dt = Database.GetData("select top(@soluongtrende) Cauhoi,DapAnDung,DaoDapAn,SoDapAn from Tracnghiem where Tenmodun=@tenmodun order by newid()", "@soluongtrende", Convert.ToInt32(row.Cells["CauhoitrenDe"].Value.ToString()), "@tenmodun", row.Cells["TenModun"].Value.ToString());
        //                    sttnc = (int)Database.ExecuteScalar("select count(DeGoc) as countdegoc from Degoc where DeGoc like '%'+ @tenmodun +'%'", "@tenmodun", row.Cells["TenModun"].Value.ToString().Trim()) + 1;
        //                    for (int i = 0; i < dt.Rows.Count; i++)
        //                    {
        //                        options = "";
        //                        Database.ExecuteNonQuery("Insert Into ChitietDegoc(Degoc,Cauhoi) values(@degoc,@cauhoi)", "@degoc", row.Cells["TenMoDun"].Value.ToString() + "_" + sttnc.ToString("00"), "@cauhoi", dt.Rows[i]["Cauhoi"].ToString());
        //                        System.Data.DataTable countoptions = Database.GetData("Select * from ChitietTracnghiem where Cauhoi=@cauhoi order by Position", "@cauhoi", dt.Rows[i]["Cauhoi"].ToString());
        //                        for (int j = 0; j < countoptions.Rows.Count; j++)
        //                        {
        //                            options = options + countoptions.Rows[j]["Position"].ToString() + ";";
        //                        }
        //                        ketcaude = ketcaude + dt.Rows[i]["Cauhoi"].ToString() + ";" + options + dt.Rows[i]["DapAnDung"].ToString() + ";" + dt.Rows[i]["SoDapAn"].ToString() + ";" + dt.Rows[i]["DaoDapAn"].ToString() + "#";
        //                    }
        //                    Database.ExecuteNonQuery("Insert Into Degoc(DeGoc,KetCau_De,Soluong) values(@DeGoc,@KetCau_De,@Soluong)", "@DeGoc", row.Cells["TenMoDun"].Value.ToString() + "_" + sttnc.ToString("00"), "@KetCau_De", ketcaude, "@Soluong", Convert.ToInt32(row.Cells["CauhoitrenDe"].Value.ToString()));
        //                    ketcaude = "";
        //                }
        //            }
        //        }
        //        if (table.Rows.Count >= 1)
        //        {
        //            sttcb = (int)Database.ExecuteScalar("select count(degoc) as countdegoc from Degoc where TenDegoc = @tendegoc", "@tendegoc", tenmodun) + 1;
        //            for (int i = 0; i < table.Rows.Count; i++)
        //            {
        //                options = "";
        //                Database.ExecuteNonQuery("Insert into ChitietDegoc(Degoc,Cauhoi) values(@degoc,@cauhoi)", "@degoc", "CNTTCB_" + sttcb.ToString("00"), "@cauhoi", table.Rows[i]["Cauhoi"].ToString());
        //                System.Data.DataTable countoptions = Database.GetData("Select * from ChitietTracnghiem where Cauhoi=@cauhoi order by Position", "@cauhoi", table.Rows[i]["Cauhoi"].ToString());
        //                for (int j = 0; j < countoptions.Rows.Count; j++)
        //                {
        //                    options = options + countoptions.Rows[j]["Position"].ToString() + ";";
        //                }
        //                ketcaude = ketcaude + table.Rows[i]["Cauhoi"].ToString() + ";" + options + table.Rows[i]["DapAnDung"].ToString() + ";" + dt.Rows[i]["SoDapAn"].ToString() + ";" + table.Rows[i]["DaoDapAn"].ToString() + "#";
        //            }
        //            Database.ExecuteNonQuery("Insert into Degoc(DeGoc,KetCau_De,Soluong) values(@DeGoc,@KetCau_De,@Soluong)", "@DeGoc", "CNTTCB_" + sttcb.ToString("00"), "@KetCau_De", ketcaude, "@Soluong", soluong);
        //            soluong = 0;
        //        }
        //    }
        //    loadDegoc();
        //}

        public void refreshDegoc()
        {
            string sql = "Select Degoc, Soluong from Degoc order by Degoc";
            System.Data.DataTable table = new System.Data.DataTable();
            table = Database.GetData(sql);
            dgDeGoc.DataSource = table;
        }

        public void loadDegoc()
        {
            string sql = "Select Degoc, Soluong from Degoc order by Degoc";
            System.Data.DataTable table = new System.Data.DataTable();
            table = Database.GetData(sql);
            dgDeGoc.DataSource = table;
            dgDeGoc.Columns["Degoc"].HeaderText = "Tên đề gốc";
            dgDeGoc.Columns["Degoc"].Width = 140;

            dgDeGoc.Columns["Soluong"].HeaderText = "Số lượng";
            dgDeGoc.Columns["Soluong"].Width = 78;
            dgDeGoc.Columns["Soluong"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        public void loadMaDe(string degoc)
        {
            System.Data.DataTable table = new System.Data.DataTable();
            table = Database.GetData("Select MaDe from DeThi where Degoc=@degoc order by Degoc, Made", "@degoc", degoc);
            dgMaDe.DataSource = table;
            dgMaDe.Columns["MaDe"].HeaderText = "Mã đề thi";
            dgMaDe.Columns["MaDe"].Width = 160;
        }

        private void btTaoMaDe_Click(object sender, EventArgs e)
        {
            if (this.dgDeGoc.SelectedCells.Count > 0)
            {
                int stt = (int)Database.ExecuteScalar("select count(MaDe) as countmade from DeThi where Degoc = @tendegoc", "@tendegoc", dgDeGoc.SelectedCells[0].Value.ToString());
                for (int i = 0; i < udSoMaDe.Value; i++)
                {
                    System.Data.DataTable Chitietdegoc = Database.GetData("select * from ChitietDegoc where Degoc=@degoc order by newid()", "@degoc", dgDeGoc.SelectedCells[0].Value.ToString());
                    string ketcaumade = "";
                    for (int j = 0; j < Chitietdegoc.Rows.Count; j++)
                    {
                        string phuonganStr = "";
                        string phuongandung = "";
                        string option="";
                        System.Data.DataTable tracnghiem = Database.GetData("Select * from TracNghiem where Cauhoi=@cauhoi", "@cauhoi", Chitietdegoc.Rows[j]["Cauhoi"].ToString());
                        if (Convert.ToInt32(tracnghiem.Rows[0]["DaoDapAn"]) == 1)//==1 la dao dap an binh thuong ngau nhien
                        {
                            System.Data.DataTable options = Database.GetData("Select * from ChiTietTracNghiem where Cauhoi=@cauhoi order by newid()", "@cauhoi", Chitietdegoc.Rows[j]["Cauhoi"].ToString());
                            for (int k = 0; k < options.Rows.Count; k++)
                            {
                                option = options.Rows[k]["Position"].ToString();
                                if (option == tracnghiem.Rows[0]["DapAnDung"].ToString())
                                    phuongandung = (k + 1).ToString();
                                phuonganStr = phuonganStr + option + ";";
                            }
                            ketcaumade = ketcaumade + tracnghiem.Rows[0]["Cauhoi"].ToString() + ";" + phuonganStr + phuongandung + ";" + tracnghiem.Rows[0]["SoDapAn"].ToString() + ";" + tracnghiem.Rows[0]["DaoDapAn"].ToString() + "#";
                        }
                        else //nguoc lai thi khong dao dap an
                        {
                            //System.Data.DataTable options = Database.GetData("Select * from ChiTietTracNghiem where Cauhoi=@cauhoi order by Position", "@cauhoi", Chitietdegoc.Rows[j]["Cauhoi"].ToString());
                            //for (int k = 0; k < options.Rows.Count; k++)
                            //{
                            //    phuonganStr = phuonganStr + options.Rows[k]["Position"].ToString() + ";";
                            //}
                            ketcaumade = ketcaumade + tracnghiem.Rows[0]["Cauhoi"].ToString() + ";" + "1;2;3;4;" + tracnghiem.Rows[0]["DapAnDung"].ToString() + ";" + tracnghiem.Rows[0]["SoDapAn"].ToString() + ";" + tracnghiem.Rows[0]["DaoDapAn"].ToString() + "#";
                        }
                    }
                    stt = stt + i;
                    Database.ExecuteNonQuery("Insert Into Dethi(MaDe,KetCau_De,Degoc) values(@made,@ketcaude,@degoc)", "@made", dgDeGoc.SelectedCells[0].Value.ToString() + stt.ToString("00"), "@ketcaude", ketcaumade, "@degoc", dgDeGoc.SelectedCells[0].Value.ToString());
                }
                MessageBox.Show("Thực hiện thành công!");
                loadMaDe(dgDeGoc.SelectedCells[0].Value.ToString());
            }
            else MessageBox.Show("Bạn phải chọn đề gốc!");
        }

        private void dgDeGoc_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            RTFBuilderbase sb1 = new RTFBuilder();
            string ketcaude = (string)Database.ExecuteScalar("Select KetCau_De from DeGoc where Degoc = @degoc", "@degoc", dgDeGoc.Rows[e.RowIndex].Cells["Degoc"].Value.ToString());
            Globals.DisplayDethiOnRtf(sb1, ketcaude);
            this.rtbDethi.Rtf = sb1.ToString();
            loadMaDe(dgDeGoc.SelectedCells[0].Value.ToString());
        }

        private void dgMaDe_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            RTFBuilderbase sb1 = new RTFBuilder();
            string ketcaude = (string)Database.ExecuteScalar("Select KetCau_De from DeThi where MaDe = @made", "@made", dgMaDe.Rows[e.RowIndex].Cells["MaDe"].Value.ToString());
            Globals.DisplayDethiOnRtf(sb1, ketcaude);
            this.rtbDethi.Rtf = sb1.ToString();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            rtbDethi.ZoomFactor = rtbDethi.ZoomFactor + 0.2f;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            rtbDethi.ZoomFactor = rtbDethi.ZoomFactor - 0.2f;
        }

        private void ExportToWord_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                dialog.Filter = "Rich Text Format (*.rtf)|*.rtf|All files (*.*)|*.*";
                dialog.FileName = "Dethi_" + DateTime.Today.ToString("yyyy-MM-dd") + "_" + dgMaDe.CurrentCell.Value.ToString() + ".rtf";

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    rtbDethi.SaveFile(dialog.FileName);
                    MessageBox.Show("Xuất đề thi: " + dialog.FileName);
                }
            }
        }

        private void CreateDocument()
        {
            try
            {
                //Create an instance for word app
                Microsoft.Office.Interop.Word.Application winword = new Microsoft.Office.Interop.Word.Application();

                //Set animation status for word application
                //winword.ShowAnimation = false;

                //Set status for word application is to be visible or not.
                winword.Visible = false;

                //Create a missing variable for missing value
                object missing = System.Reflection.Missing.Value;

                //Create a new document
                Microsoft.Office.Interop.Word.Document document = winword.Documents.Add(ref missing, ref missing, ref missing, ref missing);

                //Add header into the document
                foreach (Microsoft.Office.Interop.Word.Section section in document.Sections)
                {
                    //Get the header range and add the header details.
                    Microsoft.Office.Interop.Word.Range headerRange = section.Headers[Microsoft.Office.Interop.Word.WdHeaderFooterIndex.wdHeaderFooterPrimary].Range;
                    headerRange.Fields.Add(headerRange, Microsoft.Office.Interop.Word.WdFieldType.wdFieldPage);
                    headerRange.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    headerRange.Font.ColorIndex = Microsoft.Office.Interop.Word.WdColorIndex.wdBlue;
                    headerRange.Font.Size = 10;
                    headerRange.Text = "Header text goes here";
                }

                //Add the footers into the document
                foreach (Microsoft.Office.Interop.Word.Section wordSection in document.Sections)
                {
                    //Get the footer range and add the footer details.
                    Microsoft.Office.Interop.Word.Range footerRange = wordSection.Footers[Microsoft.Office.Interop.Word.WdHeaderFooterIndex.wdHeaderFooterPrimary].Range;
                    footerRange.Font.ColorIndex = Microsoft.Office.Interop.Word.WdColorIndex.wdDarkRed;
                    footerRange.Font.Size = 10;
                    footerRange.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    footerRange.Text = "Footer text goes here";
                }

                //adding text to document
                document.Content.SetRange(0, 0);
                document.Content.Text = rtbDethi.Text;

                //Add paragraph with Heading 1 style
                Microsoft.Office.Interop.Word.Paragraph para1 = document.Content.Paragraphs.Add(ref missing);
                object styleHeading1 = "Heading 1";
                para1.Range.set_Style(ref styleHeading1);
                para1.Range.Text = "Para 1 text";
                para1.Range.InsertParagraphAfter();

                //Add paragraph with Heading 2 style
                Microsoft.Office.Interop.Word.Paragraph para2 = document.Content.Paragraphs.Add(ref missing);
                object styleHeading2 = "Heading 2";
                para2.Range.set_Style(ref styleHeading2);
                para2.Range.Text = "Para 2 text";
                para2.Range.InsertParagraphAfter();

                //Create a 5X5 table and insert some dummy record
                Table firstTable = document.Tables.Add(para1.Range, 5, 5, ref missing, ref missing);

                firstTable.Borders.Enable = 1;
                foreach (Row row in firstTable.Rows)
                {
                    foreach (Cell cell in row.Cells)
                    {
                        //Header row
                        if (cell.RowIndex == 1)
                        {
                            cell.Range.Text = "Column " + cell.ColumnIndex.ToString();
                            cell.Range.Font.Bold = 1;
                            //other format properties goes here
                            cell.Range.Font.Name = "verdana";
                            cell.Range.Font.Size = 10;
                            //cell.Range.Font.ColorIndex = WdColorIndex.wdGray25;                            
                            cell.Shading.BackgroundPatternColor = WdColor.wdColorGray25;
                            //Center alignment for the Header cells
                            cell.VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                            cell.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;

                        }
                        //Data row
                        else
                        {
                            cell.Range.Text = (cell.RowIndex - 2 + cell.ColumnIndex).ToString();
                        }
                    }
                }

                //Save the document
                object filename = @"d:\temp1.docx";
                document.SaveAs(ref filename);
                document.Close(ref missing, ref missing, ref missing);
                document = null;
                winword.Quit(ref missing, ref missing, ref missing);
                winword = null;
                MessageBox.Show("Document created successfully !");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btXoaModun_Click(object sender, EventArgs e)
        {
            if (this.dgMaTranDe.SelectedRows.Count > 0)
            {
                DialogResult response = MessageBox.Show("Bạn có chắc chắn muốn xóa các mô đun đã chọn?", "Xóa mô đun",
                              MessageBoxButtons.YesNo,
                              MessageBoxIcon.Question,
                              MessageBoxDefaultButton.Button2);
                if (response == DialogResult.Yes)
                {
                    foreach (DataGridViewRow row in dgMaTranDe.SelectedRows)
                    {
                        Database.ExecuteNonQuery("Delete from ChiTietTracNghiem where CauHoi in (Select Cauhoi from TracNghiem where TenMoDun=@tenmodun)", "@tenmodun", row.Cells["TenMoDun"].Value.ToString());
                        Database.ExecuteNonQuery("Delete from TracNghiem where TenMoDun=@tenmodun", "@tenmodun", row.Cells["TenMoDun"].Value.ToString());                        
                    }
                    MessageBox.Show("Đã xóa các mô đun được chọn!");
                }
                refreshMaTranDe();
            }
            else MessageBox.Show("Phải chọn mô đun cần xóa trước khi xóa!");
        }

        private void btXoaDegoc_Click(object sender, EventArgs e)
        {
            if (this.dgDeGoc.SelectedRows.Count > 0)
            {
                DialogResult response = MessageBox.Show("Bạn có chắc chắn muốn xóa các đề gốc đã chọn?", "Xóa Đề gốc",
                                  MessageBoxButtons.YesNo,
                                  MessageBoxIcon.Question,
                                  MessageBoxDefaultButton.Button2);
                if (response == DialogResult.Yes)
                {
                    foreach (DataGridViewRow row in dgDeGoc.SelectedRows)
                    {
                        Database.ExecuteNonQuery("Delete from ChiTietDegoc where Degoc = @degoc", "@degoc", row.Cells["Degoc"].Value.ToString());
                        Database.ExecuteNonQuery("Delete from DeThi where DeGoc = @degoc", "@degoc", row.Cells["DeGoc"].Value.ToString());
                        Database.ExecuteNonQuery("Delete from Degoc where Degoc=@degoc", "@degoc", row.Cells["Degoc"].Value.ToString());                        
                    }
                    MessageBox.Show("Đã xóa đề gốc!");
                }
                refreshDegoc();
            }
            else MessageBox.Show("Bạn phải chọn đề gốc trước khi xóa!");
        }

        private void btXoaMade_Click(object sender, EventArgs e)
        {
            if (this.dgMaDe.SelectedRows.Count > 0)
            {
                DialogResult response = MessageBox.Show("Bạn có chắc chắn muốn xóa các đề thi đã chọn?", "Xóa Đề Thi",
                              MessageBoxButtons.YesNo,
                              MessageBoxIcon.Question,
                              MessageBoxDefaultButton.Button2);
                if (response == DialogResult.Yes)
                {
                    foreach (DataGridViewRow row in dgMaDe.SelectedRows)
                    {
                        Database.ExecuteNonQuery("Delete from DeThi where MaDe = @made", "@made", row.Cells["MaDe"].Value.ToString());
                        MessageBox.Show("Đã xóa!");
                    }
                    loadMaDe(dgDeGoc.SelectedRows[0].Cells["Degoc"].Value.ToString());
                }
            }
            else MessageBox.Show("Phải chọn mã đề trước khi xóa!");
        }
       
    }
}
