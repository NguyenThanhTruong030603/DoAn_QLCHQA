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
using System.Windows.Input;
using static System.Collections.Specialized.BitVector32;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace QLBANHANG
{
    public partial class Form4 : Form
    {
        SqlConnection connection;
        SqlCommand command;
        string str = "Data Source=ADMIN\\MSSQLSERVER01;Initial Catalog=QL_CHQA;Integrated Security=True";
        SqlDataAdapter adapter = new SqlDataAdapter();
        DataTable table = new DataTable();
        DataTable table2 = new DataTable();
        


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
            command.CommandText = "select * from tb_CTHD where MaHD ='" + txtMa.Text + "'";
            adapter.SelectCommand = command;
            table2.Clear();
            adapter.Fill(table2);
            dgvDSHH.DataSource = table2;
            int columnIndex = dgvDSHH.Columns[3].Index;
            decimal TongThanhTien = 0;
            foreach (DataGridViewRow row in dgvDSHH.Rows)
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
        public Form4()
        {
            InitializeComponent();
            

        }
        
        private void Form4_Load(object sender, EventArgs e)
        {
            connection = new SqlConnection(str);
            connection.Open();
            loaddata();
            cbNhanVien.Focus();

            txtMa.Enabled=false;
            cbNhanVien.Enabled = false;
            cbKhachHang.Enabled = false;
            cbtenHH.Enabled = false;
            txtSL.Enabled = false;
            
          
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
            command.CommandText = "select TenHang from tb_HangHoa ";
            SqlDataReader reader3 = command.ExecuteReader();
            List<string> HHlist = new List<string>();
           
            while (reader3.Read())
            {
                string name = reader3["TenHang"].ToString();
                HHlist.Add(name);
               
            }
            reader3.Close();
            cbtenHH.DataSource = HHlist;

           




        }

        private void dgvDSHD_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            cbtenHH.Enabled = true;
            txtSL.Enabled = true;
            btnThem.Enabled = true;
            btnBot.Enabled = true;
            
            int i;
            i = dgvDSHD.CurrentRow.Index;
            txtMa.Text = dgvDSHD.Rows[i].Cells[0].Value.ToString();
            dtNgayLap.Text = dgvDSHD.Rows[i].Cells[1].Value.ToString();
            cbNhanVien.Text = dgvDSHD.Rows[i].Cells[2].Value.ToString();
            cbKhachHang.Text = dgvDSHD.Rows[i].Cells[3].Value.ToString();
            loaddata2();
        }

       

        private void txtSL_TextChanged(object sender, EventArgs e)
        {

            try
            {
                command = connection.CreateCommand();
                command.CommandText = "select DonGia from tb_HangHoa where TenHang=N'" + cbtenHH.Text + "';";
                string selectedcode = cbtenHH.SelectedItem.ToString();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    string gia = reader["DonGia"].ToString();
                    txtDonGia.Text = gia;
                }
                reader.Close();
                decimal giatien = decimal.Parse(txtDonGia.Text);
                int sluong;
                if (txtSL.Text == "")
                {
                    sluong = 0;
                }
                else
                {
                     sluong = int.Parse(txtSL.Text);
                }
                decimal thanhtien = giatien * sluong;
                //lbThanhTien.Text = thanhtien.ToString();
            }
            catch
            {
                txtSL.Text = "";
                MessageBox.Show("Không được nhập kí tự khác hoặc số âm!");
                
            }
        }

      

        private void cbtenHH_SelectedIndexChanged(object sender, EventArgs e)
        {
            command = connection.CreateCommand();
            command.CommandText = "select DonGia,MaHang from tb_HangHoa where TenHang=N'" + cbtenHH.Text + "';";
            string selectedcode = cbtenHH.SelectedItem.ToString();
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                string gia = reader["DonGia"].ToString();
                txtDonGia.Text = gia;
                string ma = reader["MaHang"].ToString();
                cbHangHoa.Text = ma;
            }
            reader.Close();
           
        }

        //private void Insert(int selectedRow)
        //{
        //    decimal donGia = decimal.Parse(txtDonGia.Text);
        //    int soLuong = int.Parse(txtSL.Text);
        //    decimal thanhTien = donGia * soLuong;
        //    dgvDSHH.Rows [selectedRow].Cells [0].Value = cbHangHoa.Text;
        //    dgvDSHH.Rows [selectedRow].Cells [1].Value = cbtenHH.Text;
        //    dgvDSHH.Rows [selectedRow].Cells [2].Value = txtDonGia.Text;
        //    dgvDSHH.Rows [selectedRow].Cells [3].Value =txtSL.Text;
        //    dgvDSHH.Rows [selectedRow].Cells[4].Value = thanhTien;
           
        //}

      

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtSL.Text) > 0)
            {
                try
                {
                    command = connection.CreateCommand();
                    command.CommandText = "UPDATE tb_HangHoa SET SoLuong = SoLuong - " + int.Parse(txtSL.Text) + " WHERE MaHang = '" + cbHangHoa.Text + "'";
                    command.ExecuteNonQuery();




                }
                catch
                {
                    MessageBox.Show("Số lượng trong kho không đủ");
                }
                try
                {
                    command = connection.CreateCommand();
                    command.CommandText = "insert into tb_CTHD values('" + txtMa.Text + "','" + cbHangHoa.Text + "'," + int.Parse(txtSL.Text) + "," + int.Parse(txtSL.Text) + "*" + int.Parse(txtDonGia.Text) + ")";
                    command.ExecuteNonQuery();
                    loaddata2();

                }
                catch
                {
                    MessageBox.Show("Hàng hóa này đã tồn tại trong hóa đơn");
                }

            }
            else
            {
                MessageBox.Show("Số lượng phải lớn hơn 0 ");
            }
        }

        

        private void btnBot_Click(object sender, EventArgs e)
        {
            try
            {


                int i;
                i = dgvDSHH.CurrentRow.Index;
                command = connection.CreateCommand();
                command.CommandText = "UPDATE tb_HangHoa SET SoLuong = SoLuong + @SoLuong WHERE MaHang = @MaHang";


                command.Parameters.AddWithValue("@SoLuong", dgvDSHH.Rows[i].Cells[2].Value);
                command.Parameters.AddWithValue("@MaHang", dgvDSHH.Rows[i].Cells[1].Value);
                command.ExecuteNonQuery();
                command.CommandText = "delete from tb_CTHD where MaHH =@MaHang";
                DialogResult dr = MessageBox.Show("Bạn có muốn xóa hàng hóa " + dgvDSHH.Rows[i].Cells[1].Value + " khỏi hóa đơn "+txtMa.Text+" ?", "YES/NO", MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                {
                    command.ExecuteNonQuery();
                    loaddata2();
                    MessageBox.Show("Xóa thành công!");
                }
                
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            cbNhanVien.Focus();

            cbNhanVien.Enabled = true;
            cbKhachHang.Enabled = true;
            btnSave.Enabled = true;
            
            dtNgayLap.Value = DateTime.Now.Date;

            Invoice invoice = new Invoice();
            string invoiceCode1 = invoice.GenerateInvoiceCode();
            txtMa.Text = invoiceCode1;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

                cbNhanVien.Enabled = false;
                cbKhachHang.Enabled = false;
                cbtenHH.Enabled = true;
                txtSL.Enabled = true;
                btnThem.Enabled = true;
                btnBot.Enabled = true;
                btnSave.Enabled = false;
            cbtenHH.Focus();
               

                try
                {
                    command = connection.CreateCommand();
                    command.CommandText = "insert into tb_HoaDon values ('" + txtMa.Text + "',CAST('" + dtNgayLap.Text + "' AS DATE),'" + cbNhanVien.Text + "',N'" + cbKhachHang.Text + "' )";
                    command.ExecuteNonQuery();
                    loaddata();
                    MessageBox.Show("Thêm hóa đơn thành công!");
                    loaddata2();
            }
                catch
                {
                    MessageBox.Show("Thêm hóa đơn không thành công!");
                }

            
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            
                try
                {
                    command = connection.CreateCommand();
                    command.CommandText = "delete from tb_HoaDon where MaHD ='" + txtMa.Text + "'";
                DialogResult dr = MessageBox.Show("Bạn có muốn xóa hóa đơn " + txtMa.Text + " ?", "YES/NO", MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                {
                    command.ExecuteNonQuery();
                    loaddata();
                    MessageBox.Show("Xóa hóa đơn thành công!");
                }
               
                }
                catch
                {
                    MessageBox.Show("Xóa hóa đơn không thành công!");
                }
            
        }

        private void txtMa_TextChanged(object sender, EventArgs e)
        {

        }
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

                       
                        invoiceCode = $"HD{invoiceNumber}";

                        // Kiểm tra mã hóa đơn có tồn tại trong cơ sở dữ liệu không
                        string query = $"SELECT COUNT(*) FROM tb_HoaDon WHERE MaHD = '{invoiceCode}'";
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

        
      

       

       

       
    }

