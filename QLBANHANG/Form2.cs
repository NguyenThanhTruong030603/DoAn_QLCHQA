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
    public partial class Form2 : Form
    {
        SqlConnection connection;
        SqlCommand command;
        string str = "Data Source=ADMIN\\MSSQLSERVER01;Initial Catalog=QL_BanHang;Integrated Security=True";
        SqlDataAdapter adapter = new SqlDataAdapter();
        DataTable table = new DataTable();
        void loaddata()
        {
            command = connection.CreateCommand();
            command.CommandText = "select * from tb_NhanVien";
            adapter.SelectCommand = command;
            table.Clear();
            adapter.Fill(table);
            dgvDS.DataSource = table;

        }
        public Form2()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            connection = new SqlConnection(str);
            connection.Open();
            loaddata();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                command = connection.CreateCommand();
                command.CommandText = "insert into tb_NhanVien values ('" + txtMaNV.Text + "',N'" + txtTenNv.Text + "',N'" + cbGT.Text + "',CAST('" + dtNamSinh.Text + "' AS DATE),N'"+txtDiaChi.Text+"' , '"+txtSDT.Text+"',N'' )";
                command.ExecuteNonQuery();
                loaddata();
                MessageBox.Show("Thêm nhân viên thành công!");
            }
            catch
            {
                MessageBox.Show("Thêm nhân viên không thành công!");
            }
        }

        private void dtgvDS_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMaNV.ReadOnly = true;
            int i;
            i = dgvDS.CurrentRow.Index;
            txtMaNV.Text = dgvDS.Rows[i].Cells[0].Value.ToString();
            txtTenNv.Text = dgvDS.Rows[i].Cells[1].Value.ToString();
            cbGT.Text = dgvDS.Rows[i].Cells[2].Value.ToString();
            dtNamSinh.Text = dgvDS.Rows[i].Cells[3].Value.ToString();
            txtDiaChi.Text = dgvDS.Rows[i].Cells[4].Value.ToString();
            txtSDT.Text = dgvDS.Rows[i].Cells[5].Value.ToString();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                command = connection.CreateCommand();
                command.CommandText = "delete from tb_NhanVien where MaNV ='" + txtMaNV.Text + "'";
                command.ExecuteNonQuery();
                loaddata();

                MessageBox.Show("Xóa nhân viên thành công!");
            }
            catch
            {
                MessageBox.Show("Xóa nhân viên không thành công!");
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                command = connection.CreateCommand();
                command.CommandText = "update tb_NhanVien set TenNV=N'" + txtTenNv.Text + "',GioiTinh=N'" + cbGT.Text + "',NamSinh=CAST('" + dtNamSinh.Text +"' AS DATE), DiaChi=N'"+txtDiaChi.Text+"',SDT='"+txtSDT.Text+"' where MaNV='" + txtMaNV.Text + "'";
                command.ExecuteNonQuery();
                loaddata();
                MessageBox.Show("Sửa nhân viên thành công!");
            }

            catch
            {
                MessageBox.Show("Sửa nhân viên không thành công!");
            }
        }

        private void btnKhoiTao_Click(object sender, EventArgs e)
        {
            txtMaNV.ReadOnly = false;
            txtMaNV.Text = "";
            txtTenNv.Text = "";
            cbGT.Text = "";
            txtDiaChi.Text = "";
            txtSDT.Text = "";
            dtNamSinh.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            txtMaNV.Text = "";
            txtTenNv.Text = "";
            cbGT.Text = "";
            txtDiaChi.Text = "";
            txtSDT.Text = "";
            dtNamSinh.Text = "";
        }
    }
}
