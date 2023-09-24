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
using static System.Collections.Specialized.BitVector32;

namespace QLBANHANG
{
    public partial class Form4 : Form
    {
        SqlConnection connection;
        SqlCommand command;
        string str = "Data Source=ADMIN\\MSSQLSERVER01;Initial Catalog=QL_BanHang;Integrated Security=True";
        SqlDataAdapter adapter = new SqlDataAdapter();
        DataTable table = new DataTable();
        void loaddata()
        {
            command = connection.CreateCommand();
            command.CommandText = "select * from tb_HoaDon";
            adapter.SelectCommand = command;
            table.Clear();
            adapter.Fill(table);
            dgvDSHD.DataSource = table;

        }
        void loaddata2()
        {
            command = connection.CreateCommand();
            command.CommandText = "select * from tb_CTHD";
            adapter.SelectCommand = command;
            table.Clear();
            adapter.Fill(table);
            dgvDSHH.DataSource = table;

        }
        public Form4()
        {
            InitializeComponent();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            connection = new SqlConnection(str);
            connection.Open();
            loaddata();
            command = connection.CreateCommand();
            command.CommandText = "select MaKH from tb_KhachHang ";
            SqlDataReader reader = command.ExecuteReader();
            List<string> KHlist = new List<string>();
            while (reader.Read())
            {
                string name = reader["MaKH"].ToString();
                KHlist.Add(name);
            }
            reader.Close();
            cbKhachHang.DataSource = KHlist;
            connection.Close();
            connection.Open();
            command = connection.CreateCommand();
            command.CommandText = "select MaNV from tb_NhanVien ";
            SqlDataReader reader2 = command.ExecuteReader();
            List<string> NVlist = new List<string>();
            while (reader2.Read())
            {
                string name = reader2["MaNV"].ToString();
                NVlist.Add(name);
            }
            reader2.Close();
            cbNhanVien.DataSource = NVlist;

            command = connection.CreateCommand();
            command.CommandText = "select MaHang from tb_HangHoa ";
            SqlDataReader reader3 = command.ExecuteReader();
            List<string> HHlist = new List<string>();
            while (reader3.Read())
            {
                string name = reader3["MaHang"].ToString();
                HHlist.Add(name);
            }
            reader3.Close();
            cbHangHoa.DataSource = HHlist;
        }

        private void txtNgayLap_TextChanged(object sender, EventArgs e)
        {
            

        }

        private void cmbKhachHang_SelectedIndexChanged(object sender, EventArgs e)
        {
         
        }
    }
}
