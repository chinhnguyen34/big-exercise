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

namespace WindowsFormsApp3
{
    public partial class Form1 : Form
    {
        SqlConnection connection;
        SqlCommand command;
        string str = "Data Source=DESKTOP-RETCKNI;Initial Catalog=QLSach;Integrated Security=True;TrustServerCertificate=True";
        SqlDataAdapter adapter = new SqlDataAdapter();
        DataTable table = new DataTable();  

        void LoadData()
        {
            command = connection.CreateCommand();
            command.CommandText = "select * from ThongTinSach";
            adapter.SelectCommand = command;
            table.Clear();
            adapter.Fill(table);
            dgv.DataSource = table;
            dgv.Columns[0].HeaderText = "Mã sách: ";
            dgv.Columns[0].HeaderText = "Tên sách: ";
            dgv.Columns[0].HeaderText = "Nhà xuất bản: ";
            dgv.Columns[0].HeaderText = "Năm xuất bản: ";
            dgv.Columns[0].HeaderText = "Giá tiền: ";
            dgv.Columns[0].HeaderText = "Lọai sách: ";
        }

        public Form1()
        {
            InitializeComponent();
            cbLoaiSach.SelectedIndex = 0;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            connection = new SqlConnection(str);
            connection.Open();
            LoadData();
        }

        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int i;
            i = dgv.CurrentRow.Index;
            txtMaSach.Text = dgv.Rows[i].Cells[0].Value.ToString();
            txtTenSach.Text = dgv.Rows[i].Cells[1].Value.ToString();
            txtNXB.Text = dgv.Rows[i].Cells[2].Value.ToString();
            dtpNamXB.Text = dgv.Rows[i].Cells[3].Value.ToString();
            txtGiaTien.Text = dgv.Rows[i].Cells[4].Value.ToString();
            cbLoaiSach.Text = dgv.Rows[i].Cells[5].Value.ToString();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (txtMaSach.Text != "" && txtTenSach.Text != "" && txtNXB.Text != "" && txtGiaTien.Text != "")
            {
                command = new SqlCommand("insert into ThongTinSach values (@MaSach, @TenSach, @NXB, @NamXB, @GiaTien, @LoaiSach)", connection);
                command.Parameters.AddWithValue("@MaSach", txtMaSach.Text);
                command.Parameters.AddWithValue("@TenSach", txtTenSach.Text);
                command.Parameters.AddWithValue("@NXB", txtNXB.Text);
                command.Parameters.AddWithValue("@NamXB", dtpNamXB.Value);
                command.Parameters.AddWithValue("@GiaTien", txtGiaTien.Text);
                command.Parameters.AddWithValue("@LoaiSach", cbLoaiSach.SelectedItem);
                command.ExecuteNonQuery();
                LoadData();
                MessageBox.Show("Bạn đã thêm!");
            }
            else
            {
                MessageBox.Show("Điền thiếu thông tin!");
            }    
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            command = new SqlCommand("update ThongTinSach set TenSach = @TenSach, NXB = @NXB, NamXB = @NamXB, GiaTien = @GiaTien, LoaiSach = @LoaiSach where MaSach = @MaSach", connection);
            command.Parameters.AddWithValue("@MaSach", txtMaSach.Text);
            command.Parameters.AddWithValue("@TenSach", txtTenSach.Text);
            command.Parameters.AddWithValue("@NXB", txtNXB.Text);
            command.Parameters.AddWithValue("@NamXB", dtpNamXB.Value);
            command.Parameters.AddWithValue("@GiaTien", txtGiaTien.Text);
            command.Parameters.AddWithValue("@LoaiSach", cbLoaiSach.SelectedItem);
            command.ExecuteNonQuery();
            LoadData();
            MessageBox.Show("Bạn đã sửa!");
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            command = new SqlCommand("delete from ThongTinSach where MaSach = @MaSach", connection);
            command.Parameters.AddWithValue("@MaSach", txtMaSach.Text);
            command.ExecuteNonQuery();
            LoadData();
            MessageBox.Show("Bạn đã xóa");
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtMaSach.Text = "";
            txtTenSach.Text = "";
            txtNXB.Text = "";
            txtGiaTien.Text = "";
        }

        private void txtGiaTien_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(!char.IsControl(e.KeyChar) &&  !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
                MessageBox.Show("Giá tiền phải là số!");
            }
        }
    }
}
