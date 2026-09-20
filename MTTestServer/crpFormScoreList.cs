using System;
//using System.Collections.Generic;
//using System.ComponentModel;
using System.Data;
//using System.Drawing;
//using System.Linq;
//using System.Text;
using System.Windows.Forms;
//using CrystalDecisions.CrystalReports.Engine;
//using CrystalDecisions.Shared;
//using System.Data.SqlClient;
//using System.Configuration;

namespace ThiTracNghemUDCNTT
{
    public partial class crpFormScoreList : Form
    {
        public string phongthi;
        public string monthi;
        public string sql;
        public crpFormScoreList()
        {
            InitializeComponent();
        }

        private void crpForm_Load(object sender, EventArgs e)
        {
            ////ReportDocument rptDoc = new ReportDocument();
            //dsThisinh dsthisinh = new dsThisinh();
            //DataTable DT = new DataTable();
            //crpThisinh crpthisinh = new crpThisinh();

            //// Just set the name of data table
            ////DT.TableName = "Crystal Report Example";
            ////dsKetQuaThi listkqt = new ThiTracNghemUDCNTT.dsKetQuaThi();
            
            //if (phongthi == "ALL")
            //{
            //    DT = Database.GetData("Select*from Thisinh order by Lop, SBD");
            //}
            //else
            //{
            //    DT = Database.GetData("Select*from Thisinh where Lop=@phongthi order by SBD", "@phongthi", phongthi);
            //}
            //dsthisinh.Tables[0].Merge(DT);
            ////crpthisinh.SetDataSource(dsthisinh);
            //crpthisinh.SetDataSource(DT);
            ////rptDoc.SetDataSource(DT);
            //crpReportViewer.ReportSource = crpthisinh;
            //this.crpReportViewer.RefreshReport();

/////////////////////////////////////////////////////////////////////////////////////////////////

            //CrystalReportThisinh crpthisinh = new CrystalReportThisinh();
            //dsThisinh ds = new dsThisinh();
            ////dsKetQuaThi listkqt = new ThiTracNghemUDCNTT.dsKetQuaThi();
            DataTable DT = new DataTable();
            if (phongthi == "ALL")
            {
                //DT = Database.GetData("Select SBD,HoTen,Ten,NgaySinh,NoiSinh,Lop,Thisinh.TinhTrang,DiemThi,MaDe,MonThi,STT from Thisinh,KiThi where Thisinh.Lop=Kithi.Phongthi order by Lop, STT");
                //DT = Database.GetData("Select ts.SBD,HoTen,Ten,NgaySinh,NoiSinh,Lop,Thisinh.TinhTrang,bt.DiemThi,bt.MaDe,MonThi,STT from Thisinh as ts,KiThi,Thisinh_BaithiNC as bt where ts.Lop=Kithi.Phongthi and ts.SBD=bt.SBD order by Lop, STT");
                DT = Database.GetData("Select tb.SBD,HoTen,Ten,NgaySinh,NoiSinh,Lop,ts.TinhTrang,tb.DiemThi,tb.MaDe,TenMonThi,STT from Thisinh as ts,Thisinh_BaithiNC as tb, MonThi as mt where ts.SBD=tb.SBD and tb.MaMonThi=mt.MaMonThi order by Lop, STT");
            }
            else
            {
                //DT = Database.GetData("Select SBD,HoTen,Ten,NgaySinh,NoiSinh,Lop,Thisinh.TinhTrang,DiemThi,MaDe,MonThi,STT from Thisinh,KiThi where Thisinh.Lop=Kithi.Phongthi and Lop=@phongthi order by STT", "@phongthi", phongthi);
                //DT = Database.GetData("Select ts.SBD,HoTen,Ten,NgaySinh,NoiSinh,Lop,Thisinh.TinhTrang,bt.DiemThi,bt.MaDe,MonThi,STT from Thisinh as ts,KiThi,Thisinh_BaithiNC as bt where ts.Lop=Kithi.Phongthi and ts.SBD=bt.SBD and Lop=@phongthi order by Lop, STT", "@phongthi", phongthi);
                //DT = Database.GetData("Select distinct ts.SBD,HoTen,Ten,NgaySinh,NoiSinh,Lop,ts.TinhTrang,bt.DiemThi,bt.MaDe,MonThi,ts.STT from Thisinh as ts,KiThi,Thisinh_BaithiNC as bt where ts.Lop=Kithi.Phongthi and ts.SBD=bt.SBD order by Lop, ts.STT");
                DT = Database.GetData("Select tb.SBD,HoTen,Ten,NgaySinh,NoiSinh,Lop,ts.TinhTrang,tb.DiemThi,tb.MaDe,TenMonThi,STT from Thisinh as ts,Thisinh_BaithiNC as tb, MonThi as mt where ts.SBD=tb.SBD and tb.MaMonThi=mt.MaMonThi and Lop=@phongthi order by Lop, STT", "@phongthi", phongthi);
            }
            //ds.Merge(DT);
            //crpthisinh.SetDataSource(ds.Tables[0]);
            //crpReportViewer.ReportSource = crpthisinh;
            //this.crpReportViewer.Refresh();          

            CrystalReportPrintScoreList crp = new CrystalReportPrintScoreList();           
            crp.Database.Tables[0].SetDataSource(DT);
            crystalReportViewerDiemThi.ReportSource = null;
            crystalReportViewerDiemThi.ReportSource = crp;
        }
    }
}
