using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace QLDT
{
    public partial class TrangChu : Form
    {
        public TrangChu()
        {
            InitializeComponent();
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void lableExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void labelMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
            
        }

        private void labelZoom_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            labelZoom.Visible = false;
            label2.Visible = true;
        }

        private void label2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            labelZoom.Visible = true;
            label2.Visible = false;
        }

        private Form activeForm = null;
        private void openChildForm(Form childForm)
        {
            pictureBox2.Visible = false;
            label3.Visible = false;
            label4.Visible = false;
            label8.Visible = false;
            lbGioHeThong.Visible = false;
            if (activeForm != null)
                activeForm.Close();
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panelMain.Controls.Add(childForm);
            panelMain.Tag = childForm;
            childForm.Show();

        }

        private void TrangChu_Load(object sender, EventArgs e)
        {
            String user_ID = KetNoiServer._userID;
            label6.Text = user_ID;
        }

        private void btnNhanVien_Click_1(object sender, EventArgs e)
        {
            openChildForm(new NhanVien());
        }

        private void btnKhachHang_Click(object sender, EventArgs e)
        {
            openChildForm(new KhachHang());
        }

        private void btnSanPham_Click(object sender, EventArgs e)
        {
            openChildForm(new SanPham());
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lbGioHeThong.Text = DateTime.Now.ToString();
        }

        private void btnThuongHieu_Click(object sender, EventArgs e)
        {
            openChildForm(new ThuongHieu());
        }

        private void btnNhaCC_Click(object sender, EventArgs e)
        {
            openChildForm(new NhaCungCap());
        }

        private void btnBanHang_Click(object sender, EventArgs e)
        {
            openChildForm(new BanHang());
        }

        private void btnNhapHang_Click(object sender, EventArgs e)
        {
            openChildForm(new NhapHang());
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            openChildForm(new ThongKe());
        }

        private void btnHeThong_Click(object sender, EventArgs e)
        {
            openChildForm(new HeThong());
        }

        private void lbLogo_Click(object sender, EventArgs e)
        {
            if (activeForm != null)
            {
                activeForm.Close();
                pictureBox2.Visible = true;
                label3.Visible = true;
                label4.Visible = true;
                label8.Visible = true;
                lbGioHeThong.Visible = true;
            }else
            {
                pictureBox2.Visible = true;
                label3.Visible = true;
                label4.Visible = true;
                label8.Visible = true;
                lbGioHeThong.Visible = true;
            }
        }

        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            DialogResult = MessageBox.Show("Bạn muốn đăng xuất?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (DialogResult == DialogResult.OK)
            {
                this.Close();
                KetNoiServer f = new KetNoiServer();
                f.Show();
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {
            
        }

        private void label6_TextChanged(object sender, EventArgs e)
        {
            String user_ID = KetNoiServer._userID;
            label6.Text = user_ID;
        }

        

        
        

    }
}
