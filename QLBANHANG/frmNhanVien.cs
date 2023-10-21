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
    public partial class frmNhanVien : Form
    {
        SqlConnection connection;
        SqlCommand command;
        string str = "Data Source=ADMIN\\MSSQLSERVER01;Initial Catalog=QL_CHQA;Integrated Security=True";
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
        public frmNhanVien()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            txtTenNv.Focus();
            connection = new SqlConnection(str);
            connection.Open();
            loaddata();
            cbGT.Text = "Nam";
        }

        void enable(Boolean a)
        {
            btnThem.Enabled = a;
            btnSua.Enabled = a;
            btnXoa.Enabled = a;
            btnLuu.Enabled = !a;
            btnKoLuu.Enabled = !a;
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


                        invoiceCode = $"NV{invoiceNumber}";

                        // Kiểm tra mã hóa đơn có tồn tại trong cơ sở dữ liệu không
                        string query = $"SELECT COUNT(*) FROM tb_NhanVien WHERE MaNV = '{invoiceCode}'";
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
            txtTenNv.Clear();
            txtMaNV.Clear();
            txtDiaChi.Clear();
            txtSDT.Clear();
            txtTenNv.Focus();
            
            Invoice invoice = new Invoice();
            string invoiceCode1 = invoice.GenerateInvoiceCode();
            txtMaNV.Text = invoiceCode1;
            
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
                DialogResult dr = MessageBox.Show("Bạn có muốn xóa nhân viên " + txtMaNV.Text + " ?", "YES/NO", MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                {
                    command.ExecuteNonQuery();
                    loaddata();

                    MessageBox.Show("Xóa nhân viên thành công!");
                }
                
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
                if (txtTenNv.Text == "" || txtDiaChi.Text == "" || txtSDT.Text == "")
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
                }
                else
                {
                    command = connection.CreateCommand();
                    command.CommandText = "update tb_NhanVien set TenNV=N'" + txtTenNv.Text + "',GioiTinh=N'" + cbGT.Text + "',NamSinh=CAST('" + dtNamSinh.Text + "' AS DATE), DiaChi=N'" + txtDiaChi.Text + "',SDT='" + txtSDT.Text + "' where MaNV='" + txtMaNV.Text + "'";
                    DialogResult dr = MessageBox.Show("Bạn có muốn sửa thông tin nhân viên " + txtMaNV.Text + " ?", "YES/NO", MessageBoxButtons.YesNo);
                    if (dr == DialogResult.Yes)
                    {
                        command.ExecuteNonQuery();
                        loaddata();
                        MessageBox.Show("Sửa thông tin nhân viên thành công!");
                    }
                }
                
                
            }

            catch
            {
                MessageBox.Show("Sửa thông tin nhân viên không thành công!");
            }
        }

        

        private void btnLuu_Click(object sender, EventArgs e)
        {
            
            try
            {
                if (txtTenNv.Text == "" || txtDiaChi.Text == "" || txtSDT.Text == "")
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
                }                
                else
                {
                    command = connection.CreateCommand();
                    command.CommandText = "insert into tb_NhanVien values ('" + txtMaNV.Text + "',N'" + txtTenNv.Text + "',N'" + cbGT.Text + "',CAST('" + dtNamSinh.Text + "' AS DATE),N'" + txtDiaChi.Text + "' , '" + txtSDT.Text + "' )";
                    command.ExecuteNonQuery();
                    loaddata();
                    MessageBox.Show("Thêm nhân viên thành công!");
                    enable(true);
                }
               
            }
            catch
            {
                MessageBox.Show("Thêm nhân viên không thành công!"); 
                enable(true);
            }
        }

        private void btnKoLuu_Click(object sender, EventArgs e)
        {
            enable(true);
            txtTenNv.Clear();
            txtMaNV.Clear();
            txtDiaChi.Clear();
            txtSDT.Clear();
            cbGT.Text = "â";
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            command = connection.CreateCommand();
            command.CommandText = "select * from tb_NhanVien where MaNV like '%"+txtTimKiem+"%'";
            adapter.SelectCommand = command;
            table.Clear();
            adapter.Fill(table);
            dgvDS.DataSource = table;
        }

        
    }
}
