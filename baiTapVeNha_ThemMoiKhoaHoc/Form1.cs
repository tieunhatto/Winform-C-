using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace baiTapVeNha_ThemMoiKhoaHoc
{
    public partial class frmThemMoiKhoaHoc : Form
    {
        public frmThemMoiKhoaHoc()
        {
            InitializeComponent();
        }
        string chuoiKetNoi = @"Data Source=DESKTOP-7P62KP4;Initial Catalog=ChuoiKetNoiWinForm;Integrated Security=True";

        public void load()
        {
            SqlConnection cnn = new SqlConnection(chuoiKetNoi);
            try
            {
                cnn.Open();
                string sql = "select * from KhoaHoc";
                SqlDataAdapter da = new SqlDataAdapter(sql, cnn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvTableKhoaHoc.DataSource = dt;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Lỗi kết nối");

            }
        }
        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        Boolean addKhoaHoc = false;
        private void btnGhi_Click(object sender, EventArgs e)
        {
            SqlConnection cnn = new SqlConnection(chuoiKetNoi);
            try
            {
                if (txtMaKhoa.Text != "" && txtTenKhoa.Text != "")
                {
                    cnn.Open();
                    string sql = "insert into KhoaHoc values('" + txtMaKhoa.Text + "',N'" + txtTenKhoa.Text + "')";
                    SqlCommand cmd = new SqlCommand(sql, cnn);
                    int kq = (int)cmd.ExecuteNonQuery();
                    if (kq>0)
                    {
                        MessageBox.Show("Thêm thành công!");
                        load();
                    }
                    else
                        MessageBox.Show("Thêm thất bại!");
                        cnn.Close();
                }
                else
                    MessageBox.Show("Chưa nhập đủ thông tin!");
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message,"Lỗi kết nối!");
            }
        }

        private void frmThemMoiKhoaHoc_Load(object sender, EventArgs e)
        {
            SqlConnection cnn = new SqlConnection(chuoiKetNoi);
            cnn.Open();
            string sql = "select KhoaHoc.ID,MaKhoa,TenKhoa from KhoaHoc";// lấy hết dữ liệu trong bảng sinh viên
            SqlCommand com = new SqlCommand(sql, cnn);// Bắt đàu truy vấn
            com.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(com);// chuyển dữ liệu về
            DataTable dt = new DataTable();// tạo 1 kho ảo để lưu trũ dữ liệu
            da.Fill(dt);// đổ dữ liệu vào kho
            dgvTableKhoaHoc.DataSource = dt;// đổ dữ liệu vào dataGritView
            cnn.Close(); // đóng kêt nối
        }
    }
}
