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
    public partial class wInHoaDonBanHang : Form
    {
        SqlConnection sqlconn = new SqlConnection(ConnectionSQL.conn());
        public wInHoaDonBanHang()
        {
            InitializeComponent();
        }

        private void wInHoaDonBanHang_Load(object sender, EventArgs e)
        {
            LoadMaHDB();
        }

        //Load combobox mã hóa đơn
        private void LoadMaHDB()
        {
            try
            {
                sqlconn.Open();
                string sql = "Select * From HOADONBAN";
                SqlDataAdapter sda = new SqlDataAdapter(sql, sqlconn);
                DataSet ds = new DataSet();
                sda.Fill(ds, "HOADONBAN");

                cbMaHDB.DataSource = ds.Tables[0];
                cbMaHDB.DisplayMember = "MaHDB";
                cbMaHDB.ValueMember = "MaHDB";
                sqlconn.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show("Lỗi: " + e.StackTrace);
            }
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            wReport_HoaDonBan rpt = new wReport_HoaDonBan();
            crystalReportViewer1.ReportSource = rpt;
            //rpt.SetDatabaseLogon("sa", "sa2012", "NHATTAN\\SQLEXPRESS", "DB_MobileStore_v2");
            //crystalReportViewer1.Refresh();
            //crystalReportViewer1.DisplayToolbar = false;
            //crystalReportViewer1.DisplayStatusBar = false;
            //rpt.SetParameterValue("", );

            SqlDataAdapter dap = new SqlDataAdapter("Select * from HoaDonBan Where MaHDB = "+cbMaHDB.Text, sqlconn);
            DataSet ds = new DataSet();
            dap.Fill(ds);
            rpt.SetDataSource(ds.Tables[0]);
            crystalReportViewer1.ReportSource = rpt;
            
        }
    }
}
