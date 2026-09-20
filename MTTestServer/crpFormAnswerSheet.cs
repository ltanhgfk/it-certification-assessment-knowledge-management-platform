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
    public partial class crpFormAnswerSheet : Form
    {
        public string phongthi = "";
        public string monthi = "";
        public crpFormAnswerSheet()
        {
            InitializeComponent();
        }

        private void crpFormAnswerSheet_Load(object sender, EventArgs e)
        {
            DataTable DT = new DataTable();
            if (phongthi == "ALL")
            {
                //DT = Database.GetData("Select*from Thisinh order by Lop, SBD");
                //DT = Database.GetData("Select SBD,HoTen,Ten,Ngaysinh,Noisinh,Lop,Diemthi,BaiLam,ts.MaDe,MonThi,KetCau_De, STT from Thisinh as ts,Kithi as kt, Dethi as dt  where ts.Lop=kt.Phongthi and ts.MaDe=dt.MaDe order by Lop, STT");
                DT = Database.GetData("SELECT tb.SBD, ts.HoTen, ts.Ten, ts.NgaySinh, ts.NoiSinh, ts.Lop, tb.DiemThi, tb.MaDe, tb.BaiLam, mt.TenMonThi, dt.KetCau_De " +
            "FROM ThiSinh AS ts INNER JOIN Thisinh_BaithiNC AS tb ON ts.SBD = tb.SBD " +
            "INNER JOIN MonThi AS mt ON tb.MaMonThi = mt.MaMonThi " +
            "INNER JOIN DeThi AS dt ON tb.MaDe = dt.MaDe ORDER BY ts.Lop");
            }
            else
            {
                //DT = Database.GetData("Select*from Thisinh where Lop=@phongthi order by SBD", "@phongthi", phongthi);
                //DT = Database.GetData("Select SBD, HoTen, Ten,Ngaysinh,Noisinh,Lop,Diemthi,BaiLam,ts.MaDe,MonThi,KetCau_De, STT from Thisinh as ts,Kithi as kt, Dethi as dt  where ts.Lop=kt.Phongthi and ts.MaDe=dt.MaDe and Lop=@phongthi order by STT", "@phongthi", phongthi);
                DT = Database.GetData("SELECT tb.SBD, ts.HoTen, ts.Ten, ts.NgaySinh, ts.NoiSinh, ts.Lop, tb.DiemThi, tb.MaDe, tb.BaiLam, mt.TenMonThi, dt.KetCau_De " +
            "FROM ThiSinh AS ts INNER JOIN Thisinh_BaithiNC AS tb ON ts.SBD = tb.SBD " +
            "INNER JOIN MonThi AS mt ON tb.MaMonThi = mt.MaMonThi " +
            "INNER JOIN DeThi AS dt ON tb.MaDe = dt.MaDe and Lop=@phongthi ORDER BY ts.Lop","@phongthi", phongthi);
            }

            //DT = Database.GetData("Select SBD, HoTen, Ten,Ngaysinh,Noisinh,Lop,Diemthi,BaiLam,ts.MaDe,MonThi,KetCau_De from Thisinh as ts,Kithi as kt, Dethi as dt  where ts.Lop=kt.Phongthi and ts.MaDe=dt.MaDe");
            //DT = Database.GetData("Select*from Thisinh where Lop=@phongthi order by SBD", "@phongthi", phongthi);
            CrystalReportPrintAnswerSheet crp = new CrystalReportPrintAnswerSheet();
            crp.Database.Tables[0].SetDataSource(DT);
            crystalReportViewerAnswerSheet.ReportSource = null;
            crystalReportViewerAnswerSheet.ReportSource = crp;
        }

        //private void crpFormAnswerSheet_Load(object sender, EventArgs e)
        //{
        //    DataTable DT = new DataTable();
        //    if (phongthi == "ALL")
        //    {
        //        //DT = Database.GetData("Select*from Thisinh order by Lop, SBD");
        //        DT = Database.GetData("Select SBD, HoTen, Ten,Ngaysinh,Noisinh,Lop,Diemthi,BaiLam,ts.MaDe,MonThi,KetCau_De, STT from Thisinh as ts,Kithi as kt, Dethi as dt  where ts.Lop=kt.Phongthi and ts.MaDe=dt.MaDe order by Lop, STT");
        //    }
        //    else
        //    {
        //        //DT = Database.GetData("Select*from Thisinh where Lop=@phongthi order by SBD", "@phongthi", phongthi);
        //        DT = Database.GetData("Select SBD, HoTen, Ten,Ngaysinh,Noisinh,Lop,Diemthi,BaiLam,ts.MaDe,MonThi,KetCau_De, STT from Thisinh as ts,Kithi as kt, Dethi as dt  where ts.Lop=kt.Phongthi and ts.MaDe=dt.MaDe and Lop=@phongthi order by STT", "@phongthi", phongthi);
        //    }

        //    //DT = Database.GetData("Select SBD, HoTen, Ten,Ngaysinh,Noisinh,Lop,Diemthi,BaiLam,ts.MaDe,MonThi,KetCau_De from Thisinh as ts,Kithi as kt, Dethi as dt  where ts.Lop=kt.Phongthi and ts.MaDe=dt.MaDe");
        //    //DT = Database.GetData("Select*from Thisinh where Lop=@phongthi order by SBD", "@phongthi", phongthi);
        //    CrystalReportPrintAnswerSheet crp = new CrystalReportPrintAnswerSheet();
        //    crp.Database.Tables[0].SetDataSource(DT);
        //    crystalReportViewerAnswerSheet.ReportSource = null;
        //    crystalReportViewerAnswerSheet.ReportSource = crp;
        //}        
    }
}
