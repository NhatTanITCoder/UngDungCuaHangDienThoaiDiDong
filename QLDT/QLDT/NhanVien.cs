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
    public partial class NhanVien : Form
    {
        SqlConnection sqlconn = new SqlConnection(ConnectionSQL.conn());
        public NhanVien()
        {
            InitializeComponent();
        }
        
        //Load dữ liệu lên dataGridView
        private void Load_DataGridViewNV()
        {
            try
            {
                sqlconn.Open();
                string sql = "Select * From NhanVien";
                SqlDataAdapter sda = new SqlDataAdapter(sql, sqlconn);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                dataGridViewNV.DataSource = dt;
                sqlconn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bạn chưa đủ quyền để truy cập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
            }
        }

        private void NhanVien_Load(object sender, EventArgs e)
        {
            //+Load datagridview nhân viên và chỉ đọc
            Load_DataGridViewNV();
            dataGridViewNV.ReadOnly = true;
            dataGridViewNV.AllowUserToAddRows = false;
        }
        
        //Hàm disable các textbox
        private void disable()
        {
            txtMaNV.Enabled = false;
            txtTenNV.Enabled = false;
            txtDiaChi.Enabled = false;
            dateNgaySinh.Enabled = false;
            txtSDT.Enabled = false;
        }
        //hàm enable cacs textbox
        private void enable()
        {
            txtTenNV.Enabled = true;
            txtDiaChi.Enabled = true;
            dateNgaySinh.Enabled = true;
            txtSDT.Enabled = true;
        }
        //Hàm kiểm tra text rỗng
        private bool check_TextRong()
        {
            if (txtTenNV.Text == "")
            {
                MessageBox.Show("Hãy nhập tên nhân viên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenNV.Focus();
                return false;
            }
            else if (txtDiaChi.Text == "")
            {
                MessageBox.Show("Hãy nhập địa chỉ nhân viên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDiaChi.Focus();
                return false;
            }
            else if (txtSDT.Text == "")
            {
                MessageBox.Show("Hãy nhập số điện thoại khách hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSDT.Focus();
                return false;
            }
            else if (txtSDT.Text == "")
            {
                MessageBox.Show("Hãy nhập số điện thoại khách hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSDT.Focus();
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
                    if (txtMaNV.Text == "")
                    {
                        sqlconn.Open();
                        string sqlInsert = "INSERT INTO NHANVIEN(TenNV, DiaChiNV, NgaySinh, SdtNV) VALUES(N'" + txtTenNV.Text + "', N'" + txtDiaChi.Text + "', '" + dateNgaySinh.Text + "', '"+txtSDT.Text+"')";
                        SqlDataAdapter sda = new SqlDataAdapter(sqlInsert, sqlconn);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        dataGridViewNV.DataSource = dt;
                        sqlconn.Close();
                        NhanVien_Load(sender, e);
                        MessageBox.Show("Thêm khách hàng thành công");
                        disable();
                    }
                    if (txtMaNV.Text != "")
                    {
                        string sqlUpdate = "UPDATE NHANVIEN SET TenNV=N'" + txtTenNV.Text + "' , DiaChiNV=N'" + txtDiaChi.Text + "', NgaySinh='"+dateNgaySinh.Text+"', SdtNV='"+txtSDT.Text+"' Where MaNV="+txtMaNV.Text;
                        sqlconn.Open();
                        SqlDataAdapter sda = new SqlDataAdapter(sqlUpdate, sqlconn);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        dataGridViewNV.DataSource = dt;
                        sqlconn.Close();
                        NhanVien_Load(sender, e);
                        btnSua.Enabled = false;
                        btnXoa.Enabled = false;
                        disable();
                        MessageBox.Show("Sửa thông tin thành công");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bạn chưa được cấp quyền thêm nhân viên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
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
                    string sql = "Delete From NHANVIEN Where MaNV='" + txtMaNV.Text + "'";
                    SqlCommand cmd = new SqlCommand(sql, sqlconn);
                    cmd.ExecuteNonQuery();
                    sqlconn.Close();
                    Load_DataGridViewNV();
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
            txtMaNV.Text = "";
            txtTenNV.Text = "";
            txtDiaChi.Text = "";
            txtSDT.Text = "";
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

        private void dataGridViewNV_Click(object sender, EventArgs e)
        {
            disable();
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            txtMaNV.Text = dataGridViewNV.CurrentRow.Cells[0].Value.ToString();
            txtTenNV.Text = dataGridViewNV.CurrentRow.Cells[1].Value.ToString();
            txtDiaChi.Text = dataGridViewNV.CurrentRow.Cells[2].Value.ToString();
            dateNgaySinh.Text = dataGridViewNV.CurrentRow.Cells[3].Value.ToString();
            txtSDT.Text = dataGridViewNV.CurrentRow.Cells[4].Value.ToString();
                    
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            try
            {
                sqlconn.Open();
                string sql = "Select * From NhanVien Where TenNV Like N'%"+txtTimKiem.Text+"%'";
                SqlDataAdapter sda = new SqlDataAdapter(sql, sqlconn);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                dataGridViewNV.DataSource = ds.Tables[0];
                sqlconn.Close();
            }
            catch
            {
                MessageBox.Show("Tìm kiếm không thành công!", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            }
        }
    }
}
