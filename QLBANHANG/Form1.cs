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
        string str = "Data Source=ADMIN\\MSSQLSERVER01;Initial Catalog=QL_CHQA;Integrated Security=True";
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
            enable(true);
            txtTen.Focus();
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


                        invoiceCode = $"HH{invoiceNumber}";

                        // Kiểm tra mã hóa đơn có tồn tại trong cơ sở dữ liệu không
                        string query = $"SELECT COUNT(*) FROM tb_HangHoa WHERE MaHang = '{invoiceCode}'";
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
            txtMaHH.Clear();
            txtDonGia.Clear();
            txtSL.Clear();
            txtTen.Clear();
            Invoice invoice = new Invoice();
            string invoiceCode1 = invoice.GenerateInvoiceCode();
            txtMaHH.Text = invoiceCode1;


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
                DialogResult dr = MessageBox.Show("Bạn có muốn xóa hàng hóa "+txtMaHH.Text+" ?", "YES/NO", MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                {
                    command.ExecuteNonQuery();
                    loaddata();
                    MessageBox.Show("Xóa hàng hóa thành công!");
                }
                

                
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
                if (txtTen.Text == "" || txtSL.Text == "" || txtDonGia.Text == "")
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
                }
                else if (!int.TryParse(txtSL.Text, out int soLuong) || !int.TryParse(txtDonGia.Text, out int donGia))
                {
                    MessageBox.Show("Số lượng và đơn giá phải là số nguyên!");
                }
                else if (int.Parse(txtSL.Text) >= 0 && int.Parse(txtDonGia.Text) >= 0)
                {
                    DialogResult dr = MessageBox.Show("Bạn có muốn sửa thông tin hàng hóa " + txtMaHH.Text + " ?", "YES/NO", MessageBoxButtons.YesNo);
                    if (dr == DialogResult.Yes)
                    {
                        command.ExecuteNonQuery();
                        loaddata();
                        MessageBox.Show("Sửa thông tin hàng hóa thành công!");
                    }

                }
                else
                {
                    MessageBox.Show("Số lượng và đơn giá phải lớn hơn hoặc bằng 0!");
                }
            
                command = connection.CreateCommand();
                command.CommandText = "update tb_HangHoa set TenHang=N'" + txtTen.Text + "',DonGia=" + txtDonGia.Text + ",SoLuong=" + txtSL.Text + " where MaHang='" + txtMaHH.Text + "'";

                
            }
            catch
            {
                MessageBox.Show("Sửa thông tin hàng hóa không thành công!");
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
        void enable(Boolean a)
        {
            btnThem.Enabled = a;
            btnSua.Enabled = a;
            btnXoa.Enabled = a;
            btnLuu.Enabled = !a;
            btnKoluu.Enabled = !a;
        }
       

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            
         
            try
            {
                if (txtTen.Text == "" || txtSL.Text == "" || txtDonGia.Text == "")
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
                }
                else if (!int.TryParse(txtSL.Text, out int soLuong) || !int.TryParse(txtDonGia.Text, out int donGia))
                {
                    MessageBox.Show("Số lượng và đơn giá phải là số nguyên!");
                }
                else if ( int.Parse(txtSL.Text) >=0 && int.Parse(txtDonGia.Text) >=0)
                {
                    command = connection.CreateCommand();
                    command.CommandText = "insert into tb_HangHoa values ('" + txtMaHH.Text + "',N'" + txtTen.Text + "'," + txtDonGia.Text + "," + txtSL.Text + " )";
                    command.ExecuteNonQuery();
                    loaddata();
                    MessageBox.Show("Thêm hàng hóa thành công!");
                    enable(true);
                }
                else
                {
                    MessageBox.Show("Số lượng và đơn giá phải lớn hơn hoặc bằng 0!");
                }
            }
            catch
            {
                MessageBox.Show("Thêm hàng hóa không thành công!");
            }

        }

        private void btnKoluu_Click(object sender, EventArgs e)
        {
            enable(true);
            txtMaHH.Clear();
            txtDonGia.Clear();
            txtSL.Clear();
            txtTen.Clear();
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            command = connection.CreateCommand();
            command.CommandText = "select * from tb_HangHOA where MaHang like '%"+txtTimKiem+"%'";
            adapter.SelectCommand = command;
            table.Clear();
            adapter.Fill(table);
            dgvHH.DataSource = table;
        }
    }
}
