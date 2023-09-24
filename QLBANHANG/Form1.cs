using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Windows.Input;


namespace QLBANHANG
{
    public partial class Form1 : Form
    {
        SqlConnection connection;
        SqlCommand command;
        string str = "Data Source=ADMIN\\MSSQLSERVER01;Initial Catalog=QL_BanHang;Integrated Security=True";
        SqlDataAdapter adapter = new SqlDataAdapter();
        DataTable table = new DataTable();
        void loaddata()
        {
            command = connection.CreateCommand();
            command.CommandText = "select * from tb_HangHOA";
            adapter.SelectCommand = command;
            table.Clear();
            adapter.Fill(table);
            dgvHH.DataSource = table;
            
        }
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            connection = new SqlConnection(str);
            connection.Open();
            loaddata();
        }

        private void dtgvDS_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMaHH.ReadOnly = true;
            int i;
            i = dgvHH.CurrentRow.Index;
            txtMaHH.Text = dgvHH.Rows[i].Cells[0].Value.ToString();
            txtTen.Text = dgvHH.Rows[i].Cells[1].Value.ToString();
            txtDonGia.Text = dgvHH.Rows[i].Cells[2].Value.ToString();
            txtSL.Text = dgvHH.Rows[i].Cells[3].Value.ToString();
            
        }

        private void txtMa_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                command = connection.CreateCommand();
                command.CommandText = "insert into tb_HangHoa values ('" + txtMaHH.Text + "',N'" + txtTen.Text + "'," + txtDonGia.Text + "," + txtSL.Text + " )";
                command.ExecuteNonQuery();
                loaddata();
                MessageBox.Show("Thêm hàng hóa thành công!");
            }
            catch
            {
                MessageBox.Show("Thêm hàng hóa không thành công!");
            }
           
        }

        private void txtDonGia_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                command = connection.CreateCommand();
                command.CommandText = "delete from tb_HangHoa where MaHang ='" + txtMaHH.Text + "'";
                command.ExecuteNonQuery();
                loaddata();

                MessageBox.Show("Xóa hàng hóa thành công!");
            }
            catch
            {
                MessageBox.Show("Xóa hàng hóa không thành công!");
            }
            
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                command = connection.CreateCommand();
                command.CommandText = "update tb_HangHoa set TenHang=N'" + txtTen.Text + "',DonGia=" + txtDonGia.Text + ",SoLuong=" + txtSL.Text + " where MaHang='" + txtMaHH.Text + "'";
                command.ExecuteNonQuery();
                loaddata();
                MessageBox.Show("Sửa hàng hóa thành công!");
            }
                
            catch
            {
                MessageBox.Show("Sửa hàng hóa không thành công!");
            }
        }

     

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtMaHH.ReadOnly = false;
          
            txtTen.Text = "";
            txtSL.Text = "";
            txtMaHH.Text = "";
            txtDonGia.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
          
            txtTen.Text = "";
            txtSL.Text = "";
            txtMaHH.Text = "";
            txtDonGia.Text = "";
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
