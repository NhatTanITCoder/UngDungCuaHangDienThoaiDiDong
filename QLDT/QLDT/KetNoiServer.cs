using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLDT
{
    public partial class KetNoiServer : Form
    {
        public KetNoiServer()
        {
            InitializeComponent();
        }

        private void KetNoiServer_Load(object sender, EventArgs e)
        {
            _tenMay = txtTenMay.Text;
            _tenCSDL = txtCSDL.Text;
            _userID = txtUserID.Text;
            _passW = txtMatKhau.Text;
        }

        public static string _tenMay;
        public static string _tenCSDL;
        public static string _userID;
        public static string _passW;

        private void btnKetNoi_Click(object sender, EventArgs e)
        {
            string connectionString = string.Format(@"data source={0};initial catalog={1};uid={2};pwd={3};", txtTenMay.Text, txtCSDL.Text, txtUserID.Text, txtMatKhau.Text);
            _tenMay = txtTenMay.Text;
            _tenCSDL = txtCSDL.Text;
            _userID = txtUserID.Text;
            _passW = txtMatKhau.Text;
            try
            {
                ConnectionServer server = new ConnectionServer(connectionString);
                if (server.IsConnection)
                {
                    MessageBox.Show("Kết nối thành công máy: " + txtTenMay.Text + " chứa Database: " + txtCSDL.Text, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    TrangChu f = new TrangChu();
                    f.Show();
                }
                    
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult = MessageBox.Show("Bạn muốn thoát?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (DialogResult == DialogResult.OK)
            {
                Application.Exit();
            }
        }
        private void openChildForm(Form childForm)
        {
            childForm.Show();
        }
        private void btnDangKy_Click(object sender, EventArgs e)
        {
            openChildForm(new DangKy());
        }
    }
}
