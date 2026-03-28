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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }



        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\User\Documents\TransportDb.mdf;Integrated Security=True;Connect Timeout=30");
        public static string User;
        private void button3_Click(object sender, EventArgs e)
        {
            Con.Open();
            string Query = "select count(*) from UserTbl where UName='" + UnameTb.Text + "' and UPassword='" + PasswordTb.Text + "'";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows[0][0].ToString() == "1")
            {
                User = UnameTb.Text;
                Dashboard Obj = new Dashboard();
                Obj.Show();
                this.Hide();

            }
            else
            {
                MessageBox.Show("Wrong Username or Password");
                UnameTb.Text = "";
                PasswordTb.Text = "";
                Con.Close();
            }
        }
        

        private void label2_Click(object sender, EventArgs e)
        {
            AdminLogin Obj = new AdminLogin();
            Obj.Show();
            this.Hide();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();

        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }
    }
}
