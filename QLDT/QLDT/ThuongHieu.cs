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
    public partial class ThuongHieu : Form
    {
        public ThuongHieu()
        {
            InitializeComponent();
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
        }
        SqlConnection sqlconn = new SqlConnection(ConnectionSQL.conn());
        //Load khách hàng lên datagridview
        private void Load_GridViewTH()
        {
            try
            {
                sqlconn.Open();
                string sql = "Select * From THUONGHIEU";
                SqlDataAdapter sda = new SqlDataAdapter(sql, sqlconn);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                dataGridViewKH.DataSource = dt;
                sqlconn.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show("Bạn chưa đủ quyền để truy cập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
            }
        }
        //Hàm disable các textbox
        private void disable()
        {
            txtMaTH.Enabled = false;
            txtTenTH.Enabled = false;
        }
        //hàm enable cacs textbox
        private void enable()
        {
            txtTenTH.Enabled = true;
        }
        //load form thương hiệu
        private void ThuongHieu_Load(object sender, EventArgs e)
        {
            //+Load datagridview Khách hàng và chỉ đọc
            Load_GridViewTH();
            dataGridViewKH.ReadOnly = true;
            dataGridViewKH.AllowUserToAddRows = false;
        }
        //Hàm kiểm tra text rỗng
        private bool check_TextRong()
        {
            if (txtTenTH.Text == "")
            {
                MessageBox.Show("Hãy nhập tên thương hiệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenTH.Focus();
                return false;
            }
            return true;
        }
        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                if (check_TextRong() == true)
                {
                    if (txtMaTH.Text == "")
                    {
                        sqlconn.Open();
                        string sqlInsert = "INSERT INTO THUONGHIEU(MaThuongHieu, TenThuongHieu) VALUES(" + txtMaTH.Text + ", N'" + txtTenTH.Text + "')";
                        SqlDataAdapter sda = new SqlDataAdapter(sqlInsert, sqlconn);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        dataGridViewKH.DataSource = dt;
                        sqlconn.Close();
                        ThuongHieu_Load(sender, e);
                        MessageBox.Show("Thêm thương hiệu thành công");
                        disable();
                    }
                    if (txtMaTH.Text != "")
                    {
                        string sqlUpdate = "UPDATE THUONGHIEU SET TenThuongHieu = N'" + txtTenTH.Text + "'";
                        sqlconn.Open();
                        SqlDataAdapter sda = new SqlDataAdapter(sqlUpdate, sqlconn);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        dataGridViewKH.DataSource = dt;
                        sqlconn.Close();
                        ThuongHieu_Load(sender, e);
                        btnSua.Enabled = false;
                        btnXoa.Enabled = false;
                        disable();
                        MessageBox.Show("Sửa thông tin thành công");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            enable();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dr = MessageBox.Show("Bạn chắc chắn muốn xóa", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    sqlconn.Open();
                    string sql = "Delete From ThuongHieu Where MaKH='" + txtMaTH.Text + "'";
                    SqlCommand cmd = new SqlCommand(sql, sqlconn);
                    cmd.ExecuteNonQuery();
                    sqlconn.Close();
                    Load_GridViewTH();
                    MessageBox.Show("Xóa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnSua.Enabled = false;
                    btnXoa.Enabled = false;
                }
            }
            catch
            {
                MessageBox.Show("Không được xóa thông tin này!", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            txtMaTH.Text = "";
            txtTenTH.Text = "";
            enable();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult thoat = MessageBox.Show("Bạn chắc chắn muốn thoát!", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (thoat == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void dataGridViewKH_Click(object sender, EventArgs e)
        {
            disable();
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            txtMaTH.Text = dataGridViewKH.CurrentRow.Cells[0].Value.ToString();
            txtTenTH.Text = dataGridViewKH.CurrentRow.Cells[1].Value.ToString();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            try
            {
                sqlconn.Open();
                string sql = "Select * From ThuongHieu Where TenThuongHieu Like N'%" + txtTimKiem.Text + "%'";
                SqlDataAdapter sda = new SqlDataAdapter(sql, sqlconn);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                dataGridViewKH.DataSource = ds.Tables[0];
                sqlconn.Close();
            }
            catch
            {
                MessageBox.Show("Tìm kiếm không thành công!", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            }
        }
        
    }
}
