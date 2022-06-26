﻿using System;
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
    public partial class FoodList : Form
    {
        public FoodList()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=SHAFAYET;Initial Catalog=Resturent;Integrated Security=True");

        DataTable dt;
        SqlDataAdapter adpt;
        public int foodId;

        private void FoodList_Load(object sender, EventArgs e)
        {
            GetFoodList();
        }
        private void GetFoodList()
        {



            SqlCommand cmd = new SqlCommand("select * from foodTable", con);

            DataTable dt = new DataTable();

            con.Open();



            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            con.Close();

            FoodListDataGridView.DataSource = dt;//grid view name
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (IsValid())
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO foodTable VALUES(@foodname,@price)", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@foodname", textFoodName.Text);
                cmd.Parameters.AddWithValue("@price", textFoodPrice.Text);
                


                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("New food is Successfully inserterd", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);

                GetFoodList();
                ResetButton();
            }
        }

        private bool IsValid()
        {
            if (textFoodName.Text == string.Empty)
            {
                MessageBox.Show("Food Name is required", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }


        
        private void ResetButton()
        {
            foodId = 0;
            textFoodName.Clear();
            textFoodPrice.Clear();


            textFoodName.Focus();
        }

        private void FoodListDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            foodId = Convert.ToInt32(FoodListDataGridView.SelectedRows[0].Cells[0].Value);
            textFoodName.Text = FoodListDataGridView.SelectedRows[0].Cells[1].Value.ToString();

            textFoodPrice.Text = FoodListDataGridView.SelectedRows[0].Cells[2].Value.ToString();

            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (foodId > 0)
            {
                SqlCommand cmd = new SqlCommand("UPDATE foodTable SET foodname=@foodname,price=@price WHERE foodid=@foodid", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@foodname", textFoodName.Text);
                cmd.Parameters.AddWithValue("@price", textFoodPrice.Text);
                cmd.Parameters.AddWithValue("@foodid", this.foodId);



                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("New food is Successfully Updated", "Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);

                GetFoodList();
                ResetButton();

            }
            else
            {
                MessageBox.Show("please, Select to Update food Information", "Select?", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (foodId > 0)
            {

                SqlCommand cmd = new SqlCommand("DELETE FROM foodTable WHERE foodid=@foodid", con);
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.AddWithValue("@foodid", this.foodId);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Food-item Information is Successfully Deleted", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);

                GetFoodList();
                ResetButton();
            }
            else
            {
                MessageBox.Show("please, Select to Delete Food-item Information", "Select?", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        


        private void buttonBack_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            Dashboard d = new Dashboard();
            d.Show();
        }

        private void textBoxFoodSearch_TextChanged(object sender, EventArgs e)
        {
            SearchData(textBoxFoodSearch.Text);
        }

        public void SearchData(string search)
        {

            con.Open();
            string query = "select * from foodTable where foodname like '%" + search + "%'";

            adpt = new SqlDataAdapter(query, con);
            dt = new DataTable();
            adpt.Fill(dt);
            FoodListDataGridView.DataSource = dt;
            con.Close();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            ResetButton();
        }

        private void buttonlogout_Click(object sender, EventArgs e)
        {
            this.Hide();
            Loginform f = new Loginform();
            f.Show();

        }

        private void FoodListDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
