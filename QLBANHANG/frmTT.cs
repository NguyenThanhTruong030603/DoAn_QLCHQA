using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLBANHANG
{
    public partial class frmTT : Form
    {
        public frmTT()
        {
            InitializeComponent();
        }
        private Form CurrentFormChild;
        private void OpenChildForm(Form childForm)
        {
            if (CurrentFormChild != null)
            {
                CurrentFormChild.Close();
            }
            CurrentFormChild = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panel_body.Controls.Add(childForm);
            childForm.BringToFront();
            //childForm.AutoSizeMode= AutoSizeMode.GrowAndShrink;
            childForm.Show();
            childForm.Size = panel_body.Size;

        }
       

       

       
        private void btnTHONGKE_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmThongKe());

            label1.Text = btnTHONGKE.Text;
        }

        private void btnHH_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Form1());

            label1.Text = btnHH.Text;
        }

        private void btnNV_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Form2());

            label1.Text = btnNV.Text;
        }

        private void btnKH_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Form3());

            label1.Text = btnKH.Text;
        }

        private void btnHD_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Form4());

            label1.Text = btnHD.Text;
        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            if (CurrentFormChild != null)
            {
                CurrentFormChild.Close();
            }
            label1.Text = "Trang chủ";
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void frmTT_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dr = MessageBox.Show("Bạn có muốn đóng ứng dụng?", "YES/NO", MessageBoxButtons.YesNo);
            if (dr == DialogResult.No)
            {
                e.Cancel=true;
            }
        }
    }
}

