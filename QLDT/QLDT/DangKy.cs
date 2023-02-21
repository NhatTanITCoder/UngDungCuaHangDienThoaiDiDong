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

namespace QLDT
{
    public partial class DangKy : Form
    {
        SqlConnection sqlconn = new SqlConnection(@"data source=NHATTAN\SQLEXPRESS;initial catalog=DB_MobileStore_v2;uid=sa; pwd=sa2012");
        public DangKy()
        {
            InitializeComponent();
        }

        private void btnDK_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dr = MessageBox.Show("Bạn chắc chắn muốn tạo tài khoản", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    sqlconn.Open();
                    string sql1 = "sp_addlogin '"+txtTenTK.Text+"','"+txtPass.Text+"'"; 
                    SqlCommand cmd1 = new SqlCommand(sql1, sqlconn);
                    cmd1.ExecuteNonQuery();
                    sqlconn.Close();
                    sqlconn.Open();
                    string sql2 = "sp_adduser '" + txtTenTK.Text + "',N'" + txtHoTen.Text + "'";
                    SqlCommand cmd2 = new SqlCommand(sql2, sqlconn);
                    cmd2.ExecuteNonQuery();
                    sqlconn.Close();
                    MessageBox.Show("Tạo thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch
            {
                MessageBox.Show("Không thành công!", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            }
        }
        private void openChildForm(Form childForm)
        {
            childForm.Show();
        }
        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult thoat = MessageBox.Show("Bạn chắc chắn muốn thoát!", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (thoat == DialogResult.Yes)
            {
                openChildForm(new KetNoiServer());
            }
        }
    }
}
