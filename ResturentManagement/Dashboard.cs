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
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=SHAFAYET;Initial Catalog=Resturent;Integrated Security=True");


        private void Manager_Load(object sender, EventArgs e)
        {
            if(LoginConnection.type=="M")
            {
                buttonlogout.Visible = true;
                button2.Visible = true;
                button3.Visible = false;
                button4.Visible = true;
                button6.Visible = false;
                button7.Visible = true;
                button5.Visible = true;
                label4.Visible = false;
                label1.Visible = false;
                label2.Visible = false;
                label3.Visible = false;
                textCustomerName.Visible = false;
                textCustomerAddress.Visible = false;
                textCustomerMobile.Visible = false;
                button1.Visible = false;
                buttonReset.Visible = false; 

            }
            else if (LoginConnection.type == "S")
            {
                buttonlogout.Visible = true;
                button2.Visible = false;
                button3.Visible = true;
                button4.Visible = false;
                button5.Visible = false;
                button6.Visible = true;
                button7.Visible = false;
                label4.Visible = true;
                label1.Visible = true;
                label2.Visible = true;
                label3.Visible = true;
                textCustomerName.Visible = true;
                textCustomerAddress.Visible = true;
                textCustomerMobile.Visible = true;
                button1.Visible = true;
                buttonReset.Visible = true;

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            CustomerManager cm = new CustomerManager();
            cm.Show();
        }

        private void buttonlogout_Click(object sender, EventArgs e)
        {
            this.Hide();
            Loginform f = new Loginform();
            f.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Hide();
            FoodList f = new FoodList();
            f.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Salesman s = new Salesman();
            s.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            InvoiceSalesman iv = new InvoiceSalesman();
            iv.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            InvoiceManager im = new InvoiceManager();
            im.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (IsValid())
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO CustomerResturent VALUES(@name,@address,@mobile)", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@name", textCustomerName.Text);
                cmd.Parameters.AddWithValue("@address", textCustomerAddress.Text);
                cmd.Parameters.AddWithValue("@mobile", textCustomerMobile.Text);


                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("New Customer is Successfully inserterd", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);

               // GetCustomersRecord();
                // ResetButton();
            }
        }
        private bool IsValid()
        {
            if (textCustomerName.Text == string.Empty)
            {
                MessageBox.Show("Customer Name is required", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            ResetButton();
        }
        private void ResetButton()
        {
            
            textCustomerName.Clear();
            textCustomerAddress.Clear();
            textCustomerMobile.Clear();

            textCustomerName.Focus();
             
        }

        private void toolStripComboBox1_Click(object sender, EventArgs e)
        {

        }



        /// <summary>
        /// ////////calculator/////////////////
        /// </summary>

        double Value1, Value2, Result;
        string operation = "";

        private void button18_Click(object sender, EventArgs e)
        {
            result.Text = "0";
        }

        private void button8_Click_1(object sender, EventArgs e)
        {
            Value2 = double.Parse(result.Text);
            if (operation == "+")
            {
                Result = Value1 + Value2;
            }
            else if (operation == "-")
            {
                Result = Value1 - Value2;
            }
            else if (operation == "*")
            {
                Result = Value1 * Value2;
            }
            else if (operation == "/")
            {
                Result = Value1 / Value2;
            }
            else if (operation == "%")
            {
                Result = Result / 100;
            }

            result.Text = Result.ToString();
        }

        private void NumberButton(object sender, EventArgs e)
        {
            Button button = sender as Button;
            double number = double.Parse(button.Text);
            double numberView = double.Parse(result.Text);
            numberView = numberView * 10 + number;
            result.Text = numberView.ToString();
        }

        private void OperationButton(object sender, EventArgs e)
        {

            Button button = sender as Button;
            operation = button.Text;
            Value1 = double.Parse(result.Text);
            result.Text = "0";

        }

    }
}
