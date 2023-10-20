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
        string str = "Data Source=ADMIN\\MSSQLSERVER01;Initial Catalog=QL_CHQA;Integrated Security=True";
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
            cbGT.Text = "Nam";
            txtTenKH.Focus();
        }

        void enable(Boolean a)
        {
            btnThem.Enabled = a;
            btnSua.Enabled = a;
            btnXoa.Enabled = a;
            btnLuu.Enabled = !a;
            btnKoLuu.Enabled = !a;
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
        public class Invoice
        {
            private string connectionString = "Data Source=ADMIN\\MSSQLSERVER01;Initial Catalog=QL_CHQA;Integrated Security=True";

            public string GenerateInvoiceCode()
            {
                string invoiceCode = "";

                // Kết nối đến cơ sở dữ liệu SQL
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Kiểm tra sự tồn tại của mã hóa đơn trong cơ sở dữ liệu
                    bool isDuplicate = true;
                    while (isDuplicate)
                    {
                        // Tạo số hóa đơn ngẫu nhiên
                        Random random = new Random();
                        int invoiceNumber = random.Next(1, 9999);


                        invoiceCode = $"KH{invoiceNumber}";

                        // Kiểm tra mã hóa đơn có tồn tại trong cơ sở dữ liệu không
                        string query = $"SELECT COUNT(*) FROM tb_KhachHang WHERE MaKH = '{invoiceCode}'";
                        SqlCommand command = new SqlCommand(query, connection);
                        int count = (int)command.ExecuteScalar();

                        if (count == 0)
                        {
                            // Mã hóa đơn không trùng, thoát khỏi vòng lặp
                            isDuplicate = false;
                        }
                    }
                }

                return invoiceCode;
            }
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            enable(false);
            txtTenKH.Focus();
            txtTenKH.Clear();
            txtSDT.Clear();
            txtEmail.Clear();
            txtDiaChi.Clear();
            txtMaKH.Clear();
            

            Invoice invoice = new Invoice();
            string invoiceCode1 = invoice.GenerateInvoiceCode();
            txtMaKH.Text = invoiceCode1;
            
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                command = connection.CreateCommand();
                command.CommandText = "delete from tb_KhachHang where MaKH ='" + txtMaKH.Text + "'";
                DialogResult dr = MessageBox.Show("Bạn có muốn xóa thông tin khách hàng " + txtMaKH.Text + " ?", "YES/NO", MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                {
                    command.ExecuteNonQuery();
                    loaddata();

                    MessageBox.Show("Xóa khách hàng thành công!");
                }
                
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
                if (txtTenKH.Text == "" || txtDiaChi.Text == "")
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ tên khách hàng và địa chỉ!");
                }
                else
                {
                    command = connection.CreateCommand();
                    command.CommandText = "update tb_KhachHang set TenKH=N'" + txtTenKH.Text + "',GioiTinh=N'" + cbGT.Text + "',NamSinh=CAST('" + dtNamSinh.Text + "' AS DATE), DiaChi=N'" + txtDiaChi.Text + "',SDT='" + txtSDT.Text + "', Email=N'" + txtEmail.Text + "' where MaKH='" + txtMaKH.Text + "'";
                    DialogResult dr = MessageBox.Show("Bạn có muốn sửa thông tin khách hàng " + txtMaKH.Text + " ?", "YES/NO", MessageBoxButtons.YesNo);
                    if (dr == DialogResult.Yes)
                    {
                        command.ExecuteNonQuery();
                        loaddata();
                        MessageBox.Show("Sửa thông tin khách hàng thành công!");
                    }
                }                                                   
            }

            catch
            {
                MessageBox.Show("Sửa thông tin khách hàng không thành công!");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            txtMaKH.Text = "";
            txtTenKH.Text = "";
            cbGT.Text = "";
            txtDiaChi.Text = "";
            txtSDT.Text = "";
            dtNamSinh.Text = "";
            txtEmail.Text = "";
        }

        private void btnKhoiTao_Click(object sender, EventArgs e)
        {
            
            try
            {
                if (txtTenKH.Text == "" || txtDiaChi.Text == "" )
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ tên khách hàng và địa chỉ!");
                }
                else
                {
                    command = connection.CreateCommand();
                    command.CommandText = "insert into tb_KhachHang values ('" + txtMaKH.Text + "',N'" + txtTenKH.Text + "',N'" + cbGT.Text + "',CAST('" + dtNamSinh.Text + "' AS DATE),'" + txtSDT.Text + "',N'" + txtDiaChi.Text + "' , N'" + txtEmail.Text + "' )";
                    command.ExecuteNonQuery();
                    loaddata();
                    MessageBox.Show("Thêm khách hàng thành công!");
                    enable(true);
                }
              
            }
            catch
            {
                MessageBox.Show("Thêm khách hàng không thành công!");
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            enable(true);
            txtTenKH.Clear();
            txtSDT.Clear();
            txtEmail.Clear();
            txtDiaChi.Clear();
            txtMaKH.Clear();
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            command = connection.CreateCommand();
            command.CommandText = "select * from tb_KhachHang where MaKH like '%"+txtTimKiem.Text+"%'";
            adapter.SelectCommand = command;
            table.Clear();
            adapter.Fill(table);
            dgvDS.DataSource = table;
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }
    }
}
