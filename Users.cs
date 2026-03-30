using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using Microsoft.Data.SqlClient;
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

            // FIX: Force the text to be black so it's visible against the white background
            UserDGV.DefaultCellStyle.ForeColor = Color.Black;
            UserDGV.DefaultCellStyle.BackColor = Color.White;


            // Ensure alternating rows aren't hiding text either
            UserDGV.AlternatingRowsDefaultCellStyle.ForeColor = Color.Black;

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
            /*if (Key == 0)

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
            } */

           
        
            if (Key == 0)
            {
                MessageBox.Show("Select a User to delete.");
                return;
            }

            // Confirmation helps prevent accidental data loss
            DialogResult dialogResult = MessageBox.Show("Are you sure? This will delete the User AND ALL system data!", "WARNING", MessageBoxButtons.YesNo);

            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    Con.Open();

                    // 1. Create a single command object
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = Con;

                    // 2. Execute deletes one by one using the same command object
                    // Order matters if you have Foreign Keys! Delete children first.

                    cmd.CommandText = "DELETE FROM BookingTbl";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "DELETE FROM CustomerTbl";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "DELETE FROM DriverTbl";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "DELETE FROM VehicleTbl";
                    cmd.ExecuteNonQuery();

                    // 3. Finally, delete the specific user
                    cmd.CommandText = "DELETE FROM UserTbl WHERE UId = @UKey";
                    cmd.Parameters.AddWithValue("@UKey", Key);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Entire database has been wiped and User removed.");

                    Con.Close();
                    ShowUsers();
                    Clear();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show("Error during wipe: " + Ex.Message);
                    if (Con.State == ConnectionState.Open) Con.Close();
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
            Key = 0;

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
            /*if (UnameTb.Text == "" || PhoneTb.Text == "" || PasswordTb.Text == "")

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
                    cmd.Parameters.AddWithValue("@UPa", PasswordTb.Text);
                    
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
            } */


            if (UnameTb.Text == "" || PhoneTb.Text == "" || PasswordTb.Text == "")
            {
                MessageBox.Show("Missing Information");
                return;
            }

            try
            {
                Con.Open();

                // 1. Check if a user already exists
                SqlCommand checkCmd = new SqlCommand("SELECT COUNT(*) FROM UserTbl", Con);
                int userCount = (int)checkCmd.ExecuteScalar();

                if (userCount >= 1)
                {
                    MessageBox.Show("A user already exists. You must delete the current user before adding a new one.");
                    Con.Close();
                    return; // Stop the execution here
                }

                // 2. If no user exists, proceed with the insert
                SqlCommand cmd = new SqlCommand("insert into UserTbl (UName,Uphone,Upassword ) values (@UN, @UP, @UPa)", Con);
                cmd.Parameters.AddWithValue("@UN", UnameTb.Text);
                cmd.Parameters.AddWithValue("@UP", PhoneTb.Text);
                cmd.Parameters.AddWithValue("@UPa", PasswordTb.Text);

                cmd.ExecuteNonQuery();
                MessageBox.Show("User Recorded Successfully");

                Con.Close();
                ShowUsers();
                Clear();
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Error: " + Ex.Message);
                if (Con.State == ConnectionState.Open) Con.Close();
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

         private void EditBtn_Click(object sender, EventArgs e)
         {
             if (UnameTb.Text == ""  || PhoneTb.Text == "" || PasswordTb.Text == "")

             {
                 MessageBox.Show("Missing Information");
                 return;
             }
             else
             {
                 try
                 {
                     Con.Open();
                     SqlCommand cmd = new SqlCommand("update UserTbl set UName=@UN, Uphone=@UP, Upassword=@UPa where  UId=@UKey", Con);
                     cmd.Parameters.AddWithValue("@UN", UnameTb.Text);
                     cmd.Parameters.AddWithValue("@UP", PhoneTb.Text);
                     cmd.Parameters.AddWithValue("@UPa", PasswordTb.Text);
                     cmd.Parameters.AddWithValue("@UKey", Key);
                     cmd.ExecuteNonQuery();
                     MessageBox.Show("User Updated");

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

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            Dashboard Obj = new Dashboard();
            Obj.Show();
            this.Hide();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            Login Obj = new Login();
            Obj.Show();
            this.Hide();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Users Obj = new Users();
            Obj.Show();
            this.Hide();
        }
    }
    }

