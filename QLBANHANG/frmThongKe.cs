using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLBANHANG
{
    public partial class frmThongKe : Form
    {
        public frmThongKe()
        {
            InitializeComponent();
        }
        SqlConnection connection;
        SqlCommand command;
        string str = "Data Source=ADMIN\\MSSQLSERVER01;Initial Catalog=QL_CHQA;Integrated Security=True";
        SqlDataAdapter adapter = new SqlDataAdapter();
        DataTable table = new DataTable();

        void loaddata()
        {
            command = connection.CreateCommand();
            command.CommandText = "SELECT hd.MaHD, hd.NgayLap, hd.NguoiLap, hd.KhachHang, SUM(cthd.SoLuong * cthd.DonGia) AS TongThanhTien  FROM tb_HoaDon hd JOIN tb_CTHD cthd ON hd.MaHD = cthd.MaHD WHERE hd.NgayLap >= '" + dateTimePicker1.Text + "' AND hd.NgayLap <= '" + dateTimePicker2.Text + "' GROUP BY hd.MaHD, hd.NgayLap, hd.NguoiLap, hd.KhachHang;";
            adapter.SelectCommand = command;
            table.Clear();
            adapter.Fill(table);
            dgvHH.DataSource = table;
            int columnIndex = dgvHH.Columns[4].Index;
            decimal TongThanhTien = 0;
            foreach (DataGridViewRow row in dgvHH.Rows)
            {
                // Kiểm tra nếu hàng không phải là hàng mới (nếu có)
                if (!row.IsNewRow)
                {
                    // Lấy giá trị của ô trong cột thành tiền
                    decimal cellValue = Convert.ToDecimal(row.Cells[columnIndex].Value);

                    // Cộng giá trị của ô vào tổng
                    TongThanhTien += cellValue;
                }
            }
            lbThanhTien.Text = TongThanhTien.ToString();
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            loaddata();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void frmThongKe_Load(object sender, EventArgs e)
        {
            connection = new SqlConnection(str);
            connection.Open();
           
        }
    }
}
