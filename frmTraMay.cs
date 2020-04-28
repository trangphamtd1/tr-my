using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using btlquanlycuahanginternet.Class;


namespace btlquanlycuahanginternet
{
    public partial class frmTraMay : Form
    {
        DataTable tableThueMay;
        string strNhan;
        string strNhanGT;
        string strGtri;
        string strGtriNhan;
        string strNGT;
        public frmTraMay()
        {
            InitializeComponent();
        }

        private void frmTraMay_Load(object sender, EventArgs e)
        {
            Class.functions.Connect();
            Class.functions.FillCombo("select MaNV, TenNV from NhanVien ", cboMaNV, "MaNV", "MaNV");
            cboMaNV.SelectedIndex = -1;
            loadDataToGridView();
            txtGioVao.Text = strNhan;
            txtMaPhong.Text = strNhanGT;
            txtTenKhach.Text = strGtri;
            txtMaMay.Text = strGtriNhan;
            txtNgayThue.Text = strNGT;
        }
        private void loadDataToGridView()
        {
            string sql = " select  MaPhong , MaMay , TenKhach ,NgayThue, GioVao, GioRa,MaNV,TongTien from ThueMay  ";
            DataTable table = Class.functions.GetDataToTable(sql);
            dataGridView_TraMay.DataSource = table;
        }
        public void resetvalues()
        {
            txtGioRa.Text = DateTime.Now.ToString();
            txtGioVao.Text = "";
            txtMaMay.Text = "";
            txtMaPhong.Text = "";
            txtNgayThue.Text = "";
            txtTenKhach.Text = "";
            txtThanhToan.Text = "0";
            cboMaNV.Text = "";
        }
       public string Message
        {
            get { return strNhan; }
            set { strNhan = value; }

        }
        public string Message1
        {
            get { return strNhanGT; }
            set { strNhanGT = value; }
        }
        public string Message2
        {
            get { return strGtri; }
            set { strGtri = value; }
                    
        }
        public string Message3
        {
            get { return strGtriNhan; }
            set { strGtriNhan = value; }
        }
        public string Message4
        {
            get { return strNGT; }
            set { strNGT = value; }
        }
        private void btnLuu_Click(object sender, EventArgs e)
        {
            // do đã đổ dữ liệu từ form chọn máy sang form trả máy 
            // txtTenKhach, txtMaPhong, , txtNgayThue, txtMaMay sẽ ko Null
            string sql;
            if (txtMaSTT.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập Số thứ tự ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaSTT.Focus();
                return;
            }
            if (cboMaNV.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập mã nhân viên ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboMaNV.Focus();
                return;
            }
            sql = " insert into ThueMay values ('" + txtMaSTT.Text + "','" + txtMaPhong.Text + "','" + txtMaMay.Text + "','" + txtTenKhach.Text + "','" +
                txtNgayThue.Text + "','" + txtGioVao.Text + "','" + txtGioRa.Text + "','" + cboMaNV.SelectedValue+ "','" + txtThanhToan.Text + "','')";
            Class.functions.RunSQL(sql);
            resetvalues();
            loadDataToGridView();
        }

        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            txtGioRa.Text = DateTime.Now.TimeOfDay.ToString();
            double tt;
            DateTime GioRa = DateTime.Now;
            DateTime GioVao = DateTime.Parse(txtGioVao.Text);
            TimeSpan interval = GioRa.Subtract(GioVao);
            MessageBox.Show(" thời gian bạn sử dụng máy tính: " + interval.Hours + " tiếng," + interval.Minutes + " phút", " thông báo"
                , MessageBoxButtons.OK, MessageBoxIcon.Information);
            if (interval.Hours <= 1)
            {
                tt = 6000;
            }
            else
            {
                tt = interval.Hours * 6000 + interval.Minutes * 100;
            }
            txtThanhToan.Text = tt.ToString();
            MessageBox.Show(" số tiền bạn cần thanh toán là: " + tt, " thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

