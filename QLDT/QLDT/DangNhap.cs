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
using System.Data.SqlClient;

namespace QLDT
{
    public partial class DangNhap : Form
    {
        SqlConnection sqlconn = new SqlConnection(ConnectionSQL.conn());
        
        public DangNhap()
        {
            InitializeComponent();
              
        }
        private void DangNhap_Load(object sender, EventArgs e)
        {
            
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void lbCloseForm_Click(object sender, EventArgs e)
        {
            DialogResult lose = MessageBox.Show("Bạn chắc chắn muốn thoát!", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (lose == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            try
            {
                sqlconn.Open();
                string tk = txtTenDN.Text;
                string mk = txtMatKhau.Text;
                string sql = "Select * From QL_NGUOIDUNG Where TenDangNhap = '" + tk + "' and MatKhau = '" + mk + "'";
                SqlCommand cmd = new SqlCommand(sql, sqlconn);
                SqlDataReader sdr = cmd.ExecuteReader();
                if (sdr.Read() == true)
                {
                    MessageBox.Show("Đăng nhập thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.None);
                    TrangChu f = new TrangChu();
                    f.Show();
                    this.Hide();
                }
                sqlconn.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(""+ex.StackTrace, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            } 
        }

        private void DangNhap_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void btnDangKy_Click(object sender, EventArgs e)
        {

        }

        private void btnDoiMK_Click(object sender, EventArgs e)
        {

        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult thoat = MessageBox.Show("Bạn chắc chắn muốn thoát!", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (thoat == DialogResult.Yes)
            {
                KetNoiServer f = new KetNoiServer();
                f.Show();
                this.Hide();
            }
        }

        

    }
}
