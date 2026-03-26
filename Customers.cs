using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Transport_Management_System
{
    public partial class Customers : Form
    {
        public Customers()
        {
            InitializeComponent();
            ShowCustomers();
        }

        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\User\Documents\TransportDb.mdf;Integrated Security=True;Connect Timeout=30");


        private void Clear()
        {
            CustNameTb.Text = "";
            CustGenCb.SelectedIndex = -1;
            CustPhoneTb.Text = "";
            CustAddTb.Text = "";
            
        }



        private void ShowCustomers()
        {
            Con.Open();
            string Query = "select * from CustomerTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            CustomerDGV.DataSource = ds.Tables[0];
            Con.Close();

        }



        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (CustNameTb.Text == "" || CustGenCb.SelectedIndex == -1 || CustAddTb.Text == "" || CustPhoneTb.Text == "")

            {
                MessageBox.Show("Missing Information");
                return;
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into CustomerTbl (CustName,CustAdd,CustPhone,CustGen ) values (@CN, @CA, @CP, @CG )", Con);
                    cmd.Parameters.AddWithValue("@CN", CustNameTb.Text);
                    cmd.Parameters.AddWithValue("@CA", CustAddTb.Text);
                    cmd.Parameters.AddWithValue("@CP", CustPhoneTb.Text);
                    cmd.Parameters.AddWithValue("@CG", CustGenCb.SelectedItem.ToString());
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Customer Recorded");

                    Con.Close();
                    ShowCustomers();
                    Clear();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);


                }
            }

        }

        private void CustPhoneTb_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        int Key=0;
        private void button2_Click(object sender, EventArgs e)
        {
            if (Key == 0)

            {
                MessageBox.Show("Select a Customer");
                return;
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("delete from CustomerTbl where CustId = @CustKey ", Con);
                    cmd.Parameters.AddWithValue("@CustKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Customer Deleted");

                    Con.Close();
                    ShowCustomers();
                    Clear();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);


                }
            }
        }

        private void CustomerDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        { 
            CustNameTb.Text = CustomerDGV.SelectedRows[0].Cells[0].Value.ToString();
            CustAddTb.Text = CustomerDGV.SelectedRows[0].Cells[1].Value.ToString();
            CustPhoneTb.Text = CustomerDGV.SelectedRows[0].Cells[2].Value.ToString();
            CustGenCb.SelectedItem = CustomerDGV.SelectedRows[0].Cells[3].Value.ToString();

            if (string.IsNullOrWhiteSpace(CustNameTb.Text))
            {
                Key = 0;
            }
            else if (CustomerDGV.SelectedRows.Count > 0)
            {
                int.TryParse(CustomerDGV.SelectedRows[0].Cells[0].Value.ToString(), out Key);
            }

            

        }
    }
}   
