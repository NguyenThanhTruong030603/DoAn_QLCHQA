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
    public partial class Form3 : Form
    {
        SqlConnection connection;
        SqlCommand command;
        string str = "Data Source=ADMIN\\MSSQLSERVER01;Initial Catalog=QL_BanHang;Integrated Security=True";
        SqlDataAdapter adapter = new SqlDataAdapter();
        DataTable table = new DataTable();
        void loaddata()
        {
            command = connection.CreateCommand();
            command.CommandText = "select * from tb_KhachHang";
            adapter.SelectCommand = command;
            table.Clear();
            adapter.Fill(table);
            dgvDS.DataSource = table;

        }
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            connection = new SqlConnection(str);
            connection.Open();
            loaddata();
        }

        private void dgvDS_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMaKH.ReadOnly = true;
            int i;
            i = dgvDS.CurrentRow.Index;
            txtMaKH.Text = dgvDS.Rows[i].Cells[0].Value.ToString();
            txtTenKH.Text = dgvDS.Rows[i].Cells[1].Value.ToString();
            cbGT.Text = dgvDS.Rows[i].Cells[2].Value.ToString();
            dtNamSinh.Text = dgvDS.Rows[i].Cells[3].Value.ToString();
            txtSDT.Text = dgvDS.Rows[i].Cells[4].Value.ToString();
            txtDiaChi.Text = dgvDS.Rows[i].Cells[5].Value.ToString();   
            txtEmail.Text = dgvDS.Rows[i].Cells[6].Value.ToString();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                command = connection.CreateCommand();
                command.CommandText = "insert into tb_KhachHang values ('" + txtMaKH.Text + "',N'" +txtTenKH.Text + "',N'" + cbGT.Text + "',CAST('" + dtNamSinh.Text + "' AS DATE),'" + txtSDT.Text + "',N'" + txtDiaChi.Text + "' , N'"+txtEmail.Text+"' )";
                command.ExecuteNonQuery();
                loaddata();
                MessageBox.Show("Thêm khách hàng thành công!");
            }
            catch
            {
                MessageBox.Show("Thêm khách hàng không thành công!");
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                command = connection.CreateCommand();
                command.CommandText = "delete from tb_KhachHang where MaKH ='" + txtMaKH.Text + "'";
                command.ExecuteNonQuery();
                loaddata();

                MessageBox.Show("Xóa khách hàng thành công!");
            }
            catch
            {
                MessageBox.Show("Xóa khách hàng không thành công!");
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                command = connection.CreateCommand();
                command.CommandText = "update tb_KhachHang set TenKH=N'" + txtTenKH.Text + "',GioiTinh=N'" + cbGT.Text + "',NamSinh=CAST('" + dtNamSinh.Text + "' AS DATE), DiaChi=N'" + txtDiaChi.Text + "',SDT='" + txtSDT.Text + "' where MaKH='" + txtMaKH.Text + "'";
                command.ExecuteNonQuery();
                loaddata();
                MessageBox.Show("Sửa thông tin khách hàng thành công!");
            }

            catch
            {
                MessageBox.Show("Sửa thông tin khách hàng không thành công!");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtMaKH.ReadOnly = false;
            txtMaKH.Text = "";
            txtTenKH.Text = "";
            cbGT.Text = "";
            txtDiaChi.Text = "";
            txtSDT.Text = "";
            dtNamSinh.Text = "";
            txtEmail.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            txtMaKH.Text = "";
            txtTenKH.Text = "";
            cbGT.Text = "";
            txtDiaChi.Text = "";
            txtSDT.Text = "";
            dtNamSinh.Text = "";
            txtEmail.Text = "";
        }
    }
}
