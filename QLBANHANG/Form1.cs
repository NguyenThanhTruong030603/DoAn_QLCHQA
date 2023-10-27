using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QLBANHANG.Model1;

namespace QLBANHANG
{
    public partial class Form1 : Form
    {
        string hd = "";
        public Form1(string hd)
        {
            InitializeComponent();
            this.hd = hd;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            HangHoaContext context = new HangHoaContext();
            List<tb_CTHD> listCTHD = context.tb_CTHD.Where(s => s.MaHD == hd).ToList(); //l y t t c sv
            List<reportclass> listReport = new List<reportclass>();
            foreach (tb_CTHD i in listCTHD)
            {
                reportclass temp = new reportclass();
                temp.MaHD = i.MaHD;
                temp.TenHang = i.tb_HangHoa.TenHang;
                temp.SoLuong = i.SoLuong;
                temp.DonGia = i.DonGia;
       
                listReport.Add(temp);
            }
            this.reportViewer1.LocalReport.ReportPath = "Report1.rdlc";
            var reportDataSource = new ReportDataSource("DataSet1", listReport);

            this.reportViewer1.LocalReport.DataSources.Clear();
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource);
            this.reportViewer1.RefreshReport();
        }

        private void reportViewer1_Load(object sender, EventArgs e)
        {

        }
    }
}
