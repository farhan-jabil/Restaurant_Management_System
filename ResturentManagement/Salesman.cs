using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResturentManegment
{
    public partial class Salesman : Form
    {
        public Salesman()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=SHAFAYET;Initial Catalog=Resturent;Integrated Security=True");
        DataTable dt;
        SqlDataAdapter adpt;
        public int salesmanId;
        private void Salesman_Load(object sender, EventArgs e)
        {
            GetSalesmanRecord();
        }

        private void GetSalesmanRecord()
        {



            SqlCommand cmd = new SqlCommand("select * from tb_login", con);

            DataTable dt = new DataTable();

            con.Open();



            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            con.Close();

            SalesmanRecordDataGridView.DataSource = dt;//grid view name
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (IsValid())
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO tb_login VALUES(@name,@username,@password,@type,@address,@mobile)", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@name", textSalesmanName.Text);
                cmd.Parameters.AddWithValue("@username", textName.Text);
                cmd.Parameters.AddWithValue("@password", textPassword.Text);
                cmd.Parameters.AddWithValue("@type", textType.Text);
                cmd.Parameters.AddWithValue("@address", textSalesmanAddress.Text);
                cmd.Parameters.AddWithValue("@mobile", textSalesmanMobile.Text);


                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("New SalesMan is Successfully inserterd", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);

                GetSalesmanRecord();
                ResetButton();
            }
        }
        private bool IsValid()
        {
            if (textSalesmanName.Text == string.Empty)
            {
                MessageBox.Show("SalesMan Name is required", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ResetButton();
        }

        private void ResetButton()
        {
            salesmanId = 0;
            textSalesmanName.Clear();
            textName.Clear();
            textPassword.Clear();
            textSalesmanAddress.Clear();
            textSalesmanMobile.Clear();

            textSalesmanName.Focus();
        }

        private void SalesmanRecordDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            salesmanId= Convert.ToInt32(SalesmanRecordDataGridView.SelectedRows[0].Cells[0].Value);
            textSalesmanName.Text = SalesmanRecordDataGridView.SelectedRows[0].Cells[1].Value.ToString();

            textName.Text = SalesmanRecordDataGridView.SelectedRows[0].Cells[2].Value.ToString();
            textPassword.Text = SalesmanRecordDataGridView.SelectedRows[0].Cells[3].Value.ToString();
            textType.Text = SalesmanRecordDataGridView.SelectedRows[0].Cells[4].Value.ToString();

            textSalesmanAddress.Text = SalesmanRecordDataGridView.SelectedRows[0].Cells[5].Value.ToString();

            textSalesmanMobile.Text = SalesmanRecordDataGridView.SelectedRows[0].Cells[6].Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (salesmanId > 0)
            {
                SqlCommand cmd = new SqlCommand("UPDATE tb_login SET name=@name,username=@username,password=@password,type=@type,address=@address,mobile=@mobile WHERE id=@id", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@name", textSalesmanName.Text);
                cmd.Parameters.AddWithValue("@username", textName.Text);
                cmd.Parameters.AddWithValue("@password", textPassword.Text);
                cmd.Parameters.AddWithValue("@type", textType.Text);
                cmd.Parameters.AddWithValue("@address", textSalesmanAddress.Text);
                cmd.Parameters.AddWithValue("@mobile", textSalesmanMobile.Text);
                cmd.Parameters.AddWithValue("@id", this.salesmanId);


                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("SalesMan Information is Successfully Updated", "Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);

                GetSalesmanRecord();
                ResetButton();

            }
            else
            {
                MessageBox.Show("please, Select to Update SalesMan Information", "Select?", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (salesmanId > 0)
            {

                SqlCommand cmd = new SqlCommand("DELETE FROM tb_login WHERE id=@id", con);
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.AddWithValue("@id", this.salesmanId);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Salesman Information is Successfully Deleted", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);

                GetSalesmanRecord();
                ResetButton();
            }
            else
            {
                MessageBox.Show("please, Select to Delete Salesman Information", "Select?", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            SearchData(textBoxSearch.Text);
        }
        public void SearchData(string search)
        {

            con.Open();
            string query = "select * from tb_login where name like '%" + search + "%'";

            adpt = new SqlDataAdapter(query, con);
            dt = new DataTable();
            adpt.Fill(dt);
            SalesmanRecordDataGridView.DataSource = dt;
            con.Close();



        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            Dashboard d = new Dashboard();
            d.Show();
        }

        private void buttonlogout_Click(object sender, EventArgs e)
        {
            this.Hide();
            Loginform f = new Loginform();
            f.Show();
        }
    
    }
}
