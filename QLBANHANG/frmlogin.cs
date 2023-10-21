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
    public partial class frmlogin : Form
    {
        public frmlogin()
        {
            InitializeComponent();
        }

        
        SqlConnection connection;
        SqlCommand command;
        string str = "Data Source=ADMIN\\MSSQLSERVER01;Initial Catalog=QL_CHQA;Integrated Security=True";
        SqlDataAdapter adapter = new SqlDataAdapter();
        DataTable table = new DataTable();
        

        

        

     

        

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            try
            {
                string tk = txtTaikhoan.Text;
                string mk = txtMatkhau.Text;
                command = connection.CreateCommand();
                command.CommandText = "select * from tb_TaiKhoan where TenTaiKhoan = '" + tk + "' and Matkhau= '" + mk + "'";
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read() == true)
                {

                    frmTrangChu form = new frmTrangChu(tk);
                    this.Hide();
                    //MessageBox.Show("Đăng nhập thành công!");
                    form.ShowDialog();
                    this.Close();

                }
                else
                {
                    MessageBox.Show("Sai tên tài khoản hoặc mật khẩu!");
                }
            }
            catch
            {
                MessageBox.Show("Lỗi kết nối!");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Bạn có muốn đóng ứng dụng?", "YES/NO", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                Close();
            }
        }

        private void frmlogin_Load_1(object sender, EventArgs e)
        {
            connection = new SqlConnection(str);
            connection.Open();
            txtTaikhoan.Focus();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
