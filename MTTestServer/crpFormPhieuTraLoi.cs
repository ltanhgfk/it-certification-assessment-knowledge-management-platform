using System;
//using System.Collections.Generic;
//using System.ComponentModel;
using System.Data;
//using System.Drawing;
//using System.Linq;
//using System.Text;
using System.Windows.Forms;

namespace ThiTracNghemUDCNTT
{
    public partial class crpFormPhieuTraLoi : Form
    {
        public string phongthi;
        public crpFormPhieuTraLoi()
        {
            InitializeComponent();
        }

        private void crpFormPhieuTraLoi_Load(object sender, EventArgs e)
        {
            DataTable DT = new DataTable();
            if (phongthi == "ALL")
            {
                //DT = Database.GetData("Select*from Thisinh order by Lop, SBD");
                DT = Database.GetData("Select SBD, HoTen, Ten,Ngaysinh,Noisinh,Lop,Diemthi,BaiLam,ts.MaDe,MonThi,KetCau_De from Thisinh as ts,Kithi as kt, Dethi as dt  where ts.Lop=kt.Phongthi and ts.MaDe=dt.MaDe order by Lop, STT");          
            }
            else
            {
                //DT = Database.GetData("Select*from Thisinh where Lop=@phongthi order by SBD", "@phongthi", phongthi);
                DT = Database.GetData("Select SBD, HoTen, Ten,Ngaysinh,Noisinh,Lop,Diemthi,BaiLam,ts.MaDe,MonThi,KetCau_De from Thisinh as ts,Kithi as kt, Dethi as dt  where ts.Lop=kt.Phongthi and ts.MaDe=dt.MaDe and Lop=@phongthi order by STT", "@phongthi", phongthi);
            }
            
            //DT = Database.GetData("Select SBD, HoTen, Ten,Ngaysinh,Noisinh,Lop,Diemthi,BaiLam,ts.MaDe,MonThi,KetCau_De from Thisinh as ts,Kithi as kt, Dethi as dt  where ts.Lop=kt.Phongthi and ts.MaDe=dt.MaDe");
            //DT = Database.GetData("Select*from Thisinh where Lop=@phongthi order by SBD", "@phongthi", phongthi);
            CrystalReportInPhieuTraLoi crp = new CrystalReportInPhieuTraLoi();
            crp.Database.Tables[0].SetDataSource(DT);            
            crystalReportViewerPhieuTraLoi.ReportSource = null;
            crystalReportViewerPhieuTraLoi.ReportSource = crp;
        }
    }
}
