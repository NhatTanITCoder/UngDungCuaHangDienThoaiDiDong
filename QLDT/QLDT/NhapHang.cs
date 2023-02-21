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
    public partial class NhapHang : Form
    {
        SqlConnection sqlconn = new SqlConnection(ConnectionSQL.conn());
        public NhapHang()
        {
            InitializeComponent();
            txtMaHDN.Enabled = false;
            Disable_CT_HDNhap();
            btnSua.Enabled = false;
            btnXoaSP.Enabled = false;
            txtTongTien.Enabled = false;
        }
        private void Enable_HDNhap()
        {
            cbxTenNV.Enabled = true;
            cbxNCC.Enabled = true;
            dateNgayLapHD.Enabled = true;
            txtGhiChu.Enabled = true;
            btnThemNCC.Enabled = true;
            btnThemHDN.Enabled = true;
        }
        private void disable_HDNhap()
        {
            cbxTenNV.Enabled = false;
            cbxNCC.Enabled = false;
            dateNgayLapHD.Enabled = false;
            txtGhiChu.Enabled = false;
            btnThemNCC.Enabled = false;
            btnThemHDN.Enabled = false;
        }
        private void Enable_CT_HDNhap()
        {
            cbxSP.Enabled = true;
            txtSL.Enabled = true;
            txtSL.Enabled = true;
            btnLuu.Enabled = true;
            btnThemSP.Enabled = true;
        }
        private void Disable_CT_HDNhap()
        {
            cbxSP.Enabled = false;
            txtSLTon.Enabled = false;
            txtSL.Enabled = false;
            txtSL.Enabled = false;
            txtThanhTien.Enabled = false;
            txtDGN.Enabled = false;
            btnLuu.Enabled = false;
        }

        private void btnThemNCC_Click(object sender, EventArgs e)
        {
            openChildForm(new NhaCungCap());
        }
        private void openChildForm(Form childForm)
        {
            childForm.Show();
        }
        //Hàm load combobox tên nhân viên, value mã nhân viên
        private void LoadNhanVien_Combobox()
        {
            try
            {
                sqlconn.Open();
                string sql = "Select * From NhanVien";
                SqlDataAdapter sda = new SqlDataAdapter(sql, sqlconn);
                DataSet ds = new DataSet();
                sda.Fill(ds, "NhanVien");

                cbxTenNV.DataSource = ds.Tables[0];
                cbxTenNV.DisplayMember = "TenNV";
                cbxTenNV.ValueMember = "MaNV";
                sqlconn.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show("Lỗi: " + e.StackTrace);
            }
        }
        //Hàm load combobox nhà cung cấp, value mã nhà cung cấp
        private void LoadNhaCC_Combobox()
        {
            try
            {
                sqlconn.Open();
                string sql = "Select * From NHACUNGCAP";
                SqlDataAdapter sda = new SqlDataAdapter(sql, sqlconn);
                DataSet ds = new DataSet();
                sda.Fill(ds, "NHACUNGCAP");

                cbxNCC.DataSource = ds.Tables[0];
                cbxNCC.DisplayMember = "TenNCC";
                cbxNCC.ValueMember = "MaNCC";
                sqlconn.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show("Lỗi: " + e.StackTrace);
            }
        }
        //Hàm load combobox sản phẩm, value mã sản phẩm
        private void LoadSanPham_Combobox()
        {
            try
            {
                sqlconn.Open();
                string sql = "Select * From SanPham";
                SqlDataAdapter sda = new SqlDataAdapter(sql, sqlconn);
                DataSet ds = new DataSet();
                sda.Fill(ds, "SanPham");

                cbxSP.DataSource = ds.Tables[0];
                cbxSP.DisplayMember = "TenSP";
                cbxSP.ValueMember = "MaSP";
                sqlconn.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show("Lỗi: " + e.StackTrace);
            }
        }
        //Load dữ liệu lên datagridview nhập hàng
        private void Load_GridView()
        {
            try
            {
                sqlconn.Open();
                string sql = "SELECT CT.MAHDN,TENSP,SOLUONGNHAP,CT.DONGIANHAP,TONGTIEN FROM CT_HDNHAP CT,SANPHAM S WHERE S.MASP=CT.MASP AND MAHDN>=ALL(SELECT TOP 1 MAHDN FROM CT_HDNHAP ORDER BY MAHDN DESC )";
                SqlDataAdapter sda = new SqlDataAdapter(sql, sqlconn);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                dataGridView1.DataSource = dt;
                sqlconn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Load bị lỗi: " + ex.Message);
            }
        }

        private void NhapHang_Load(object sender, EventArgs e)
        {
            LoadSanPham_Combobox();
            LoadNhaCC_Combobox();
            LoadNhanVien_Combobox();
        }
        //Thêm hóa đơn nhập mới
        private void btnThemHDN_Click(object sender, EventArgs e)
        {
            sqlconn.Open();
            string sqlInsert = "INSERT INTO HOADONNHAP(MANCC,MANV,NGAYLAPHDN,GHICHU) VALUES(" + cbxNCC.SelectedValue.ToString() + ", " + cbxTenNV.SelectedValue.ToString() + ", '" + dateNgayLapHD.Text + "', N'" + txtGhiChu.Text + "')";
            SqlDataAdapter sda = new SqlDataAdapter(sqlInsert, sqlconn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            sqlconn.Close();
            MessageBox.Show("Thêm sản phẩm thành công");
            disable_HDNhap();
            Enable_CT_HDNhap();
        }
        //...
        public string GetMaHD()
        {
            string sql = "SELECT TOP 1 MAX(MAHDN) AS MA FROM HOADONNHAP";
            SqlDataAdapter sda = new SqlDataAdapter(sql, sqlconn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0]["MA"].ToString();
            }
            else return "";
        }
        //Tính tổng tiền
        public string Load_TongTien()
        {
            txtMaHDN.Text = GetMaHD();
            string sql = "SELECT THANHTIENHDN TT FROM HOADONNHAP WHERE MAHDN=" + txtMaHDN.Text + "";
            SqlDataAdapter sda = new SqlDataAdapter(sql, sqlconn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0]["TT"].ToString();
            }
            else return "0";
        }
        //Lưu mặt hàng vào hóa đơn nhập
        private void btnLuu_Click(object sender, EventArgs e)
        {
            txtMaHDN.Text = GetMaHD();
            sqlconn.Open();
            string sqlInsert = "INSERT INTO CT_HDNHAP(MAHDN,MASP,SOLUONGNHAP,DONGIANHAP,TONGTIEN) VALUES(" + txtMaHDN.Text + ", " + cbxSP.SelectedValue.ToString() + ", " + txtSL.Text + "," + txtDGN.Text + "," + txtThanhTien.Text + ")";
            SqlDataAdapter sda = new SqlDataAdapter(sqlInsert, sqlconn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dataGridView1.DataSource = dt;
            sqlconn.Close();
            MessageBox.Show("Thêm sản phẩm thành công");
            Load_GridView();
            txtTongTien.Text = Load_TongTien();
            Disable_CT_HDNhap();
        }
        //New - cho phép thêm dữ liệu hàng mới
        private void btnThemSP_Click(object sender, EventArgs e)
        {
            Enable_CT_HDNhap();
            disable_HDNhap();
        }
        //xóa mặt hàng đã chọn
        private void btnXoaSP_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Bạn chắc chắn muốn xóa", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                sqlconn.Open();
                string sql = "Delete From CT_HDNHAP Where MAHDN=" + txtMaHDN.Text + " AND MASP=" + cbxSP.SelectedValue.ToString() + "";
                SqlCommand cmd = new SqlCommand(sql, sqlconn);
                cmd.ExecuteNonQuery();
                sqlconn.Close();
                Load_GridView();
                MessageBox.Show("Xóa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            Disable_CT_HDNhap();
            Load_TongTien();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string sqlUpdate = "UPDATE CT_HDNHAP SET SOLUONGNHAP=" + txtSL.Text + ",TONGTIEN=" + txtThanhTien.Text + "  WHERE MAHDN=" + txtMaHDN.Text + " AND MASP=" + cbxSP.SelectedValue.ToString() + "";
            sqlconn.Open();
            SqlDataAdapter sda = new SqlDataAdapter(sqlUpdate, sqlconn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dataGridView1.DataSource = dt;
            sqlconn.Close();
            MessageBox.Show("Sửa sản phẩm thành công");
            Load_GridView();
            Disable_CT_HDNhap();
            Load_TongTien();
        }

        private void btnHDNMoi_Click(object sender, EventArgs e)
        {
            txtGhiChu.Text = "";
            txtDGN.Text = "";
            txtSL.Text = "";
            txtSLTon.Text = "";
            txtSL.Text = "";
            txtThanhTien.Text = "";
            Enable_HDNhap();
        }

        private void cbxSP_SelectionChangeCommitted(object sender, EventArgs e)
        {
            string sql = "Select DonGiaNhap,SoLuongTon From SanPham where MaSP=" + cbxSP.SelectedValue.ToString() + "";
            SqlDataAdapter sda = new SqlDataAdapter(sql, sqlconn);

            //sqlconn.Open();
            DataSet ds = new DataSet();
            sda.Fill(ds, "SanPham");

            txtDGN.Text = ds.Tables[0].Rows[0]["DonGiaNhap"].ToString();
            txtSLTon.Text = ds.Tables[0].Rows[0]["SoLuongTon"].ToString();
            sqlconn.Close();
        }

        private void txtSL_TextChanged(object sender, EventArgs e)
        {
            float sl = 0;
            float dongia = 0;
            if (txtSL.Text != "")
            {
                sl = float.Parse(txtSL.Text);
            }
            if (txtDGN.Text != "")
            {
                dongia = float.Parse(txtDGN.Text);
            }
            txtThanhTien.Text = (sl * dongia).ToString();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult thoat = MessageBox.Show("Bạn chắc chắn muốn thoát!", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (thoat == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            try
            {
                txtMaHDN.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                cbxSP.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                txtSL.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                txtDGN.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                txtThanhTien.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            }
            catch
            {
                MessageBox.Show("DataGridView Chưa có giá trị!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtSL.Enabled = true;
            btnSua.Enabled = true;
            btnXoaSP.Enabled = true;
        }

        private void btnInHDN_Click(object sender, EventArgs e)
        {
            openChildForm(new wInHoaDonNhapHang());
        }

    }
}
