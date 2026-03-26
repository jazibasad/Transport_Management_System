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
    public partial class Users : Form
    {
        public Users()
        {
            InitializeComponent();
            ShowUsers();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Users_Load(object sender, EventArgs e)
        {

        }

        int Key=0;
        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (Key == 0)

            {
                MessageBox.Show("Select a User");
                return;
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("delete from UserTbl where UId = @UKey ", Con);
                    cmd.Parameters.AddWithValue("@UKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("User Deleted");

                    Con.Close();
                    ShowUsers();
                    Clear();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);


                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit(); 
        }


        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\User\Documents\TransportDb.mdf;Integrated Security=True;Connect Timeout=30");


        private void Clear()
        {
            UnameTb.Text = "";
            PhoneTb.Text = "";
            PasswordTb.Text = "";

        }

        private void ShowUsers()
        {
            Con.Open();
            string Query = "select * from UserTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            UserDGV.DataSource = ds.Tables[0];
            Con.Close();

        }


        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (UnameTb.Text == "" || PhoneTb.Text == "" || PasswordTb.Text == "")

            {
                MessageBox.Show("Missing Information");
                return;
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into UserTbl (UName,Uphone,Upassword ) values (@UN, @UP, @UPa)", Con);
                    cmd.Parameters.AddWithValue("@UN", UnameTb.Text);
                    cmd.Parameters.AddWithValue("@UP", PhoneTb.Text);
                    cmd.Parameters.AddWithValue("@Upa", PasswordTb.Text);
                    
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("User Recorded");

                    Con.Close();
                    ShowUsers();
                    Clear();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);


                }
            }

        }

        private void UserDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            UnameTb.Text = UserDGV.SelectedRows[0].Cells[1].Value.ToString();
            PhoneTb.Text = UserDGV.SelectedRows[0].Cells[2].Value.ToString();
            PasswordTb.Text = UserDGV.SelectedRows[0].Cells[3].Value.ToString();
           

            if (string.IsNullOrWhiteSpace(UnameTb.Text))
            {
                Key = 0;
            }
            else if (UserDGV.SelectedRows.Count > 0)
            {
                int.TryParse(UserDGV.SelectedRows[0].Cells[0].Value.ToString(), out Key);
            }
        }
    }
}
