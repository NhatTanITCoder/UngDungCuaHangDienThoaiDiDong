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
    public partial class wInHoaDonNhapHang : Form
    {
        SqlConnection sqlconn = new SqlConnection(ConnectionSQL.conn());
        public wInHoaDonNhapHang()
        {
            InitializeComponent();
        }

        private void wInHoaDonNhapHang_Load(object sender, EventArgs e)
        {
            LoadMaHDN();
        }
        //Load combobox mã hóa đơn NHẬP
        private void LoadMaHDN()
        {
            try
            {
                sqlconn.Open();
                string sql = "Select * From HOADONNHAP";
                SqlDataAdapter sda = new SqlDataAdapter(sql, sqlconn);
                DataSet ds = new DataSet();
                sda.Fill(ds, "HOADONNHAP");

                cbMaHDB.DataSource = ds.Tables[0];
                cbMaHDB.DisplayMember = "MaHDN";
                cbMaHDB.ValueMember = "MaHDN";
                sqlconn.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show("Lỗi: " + e.StackTrace);
            }
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            wReport_HoaDonNhap rpt = new wReport_HoaDonNhap();
            crystalReportViewer1.ReportSource = rpt;
            //rpt.SetDatabaseLogon("sa", "sa2012", "NHATTAN\\SQLEXPRESS", "DB_MobileStore_v2");
            //crystalReportViewer1.Refresh();
            //crystalReportViewer1.DisplayToolbar = false;
            //crystalReportViewer1.DisplayStatusBar = false;
            //rpt.SetParameterValue("", );

            SqlDataAdapter dap = new SqlDataAdapter("Select DISTINCT * from HoaDonNhap Where MaHDN = " + cbMaHDB.Text, sqlconn);
            DataSet ds = new DataSet();
            dap.Fill(ds);
            rpt.SetDataSource(ds.Tables[0]);
            crystalReportViewer1.ReportSource = rpt;
        }
    }
}
