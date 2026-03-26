using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Transport_Management_System
{
    public partial class Vehicles : Form
    {
        public Vehicles()
        {
            InitializeComponent();
            
            ShowVehicles();
            GetDrivers();  
        }

        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\User\Documents\TransportDb.mdf;Integrated Security=True;Connect Timeout=30");


        private void Clear()
        {
            LPlateTb.Text = "";
            MarkCb.SelectedIndex = -1;
            ModelTb.Text = "";
            VYearCb.SelectedIndex = -1;
            EngTypeCb.SelectedIndex = -1;
            ColorTb.Text = "";
            MilleageTb.Text = "";
            TypeCb.SelectedIndex = -1;
            BookedCb.SelectedIndex = -1;
        }



        private void ShowVehicles()
        {
            Con.Open();
            string Query = "select * from VehicleTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            VehicleDGV.DataSource = ds.Tables[0];
            Con.Close();

        }


        private void GetDrivers()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("select * from DriverTbl", Con);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("DrName", typeof(String));
            dt.Load(rdr);
            DriverCb.ValueMember = "DrName";
            DriverCb.DataSource = dt;
            Con.Close();
        }
        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (LPlateTb.Text == "" || MarkCb.SelectedIndex == -1 || ModelTb.Text == "" || VYearCb.SelectedIndex == -1 || EngTypeCb.SelectedIndex == -1 || ColorTb.Text == "" || MilleageTb.Text == "" || TypeCb.SelectedIndex == -1 || BookedCb.SelectedIndex == -1)

            {
                MessageBox.Show("Missing Information");
                return;
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into VehicleTbl (VLp,Vmark,Vmodel,VYear,VEngType,VColor,VMilleage,VType,Booked,Driver ) values (@VP, @Vma, @Vmo, @VY, @VEng, @VCo, @VMi, @VTy, @VB,@DR)", Con);
                    cmd.Parameters.AddWithValue("@VP", LPlateTb.Text);
                    cmd.Parameters.AddWithValue("@Vma", MarkCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@Vmo", ModelTb.Text);
                    cmd.Parameters.AddWithValue("@VY", VYearCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@VEng", EngTypeCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@VCo", ColorTb.Text);
                    cmd.Parameters.AddWithValue("@VMi", MilleageTb.Text);
                    cmd.Parameters.AddWithValue("@VTy", TypeCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@VB", BookedCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@Dr", DriverCb.SelectedValue.ToString());
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Vehicle Recorded");

                    Con.Close();
                    ShowVehicles();
                    Clear();   
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);


                }
            }
        }



        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (LPlateTb.Text == "" || MarkCb.SelectedIndex == -1 || ModelTb.Text == "" || VYearCb.SelectedIndex == -1 || EngTypeCb.SelectedIndex == -1 || ColorTb.Text == "" || MilleageTb.Text == "" || TypeCb.SelectedIndex == -1 || BookedCb.SelectedIndex == -1)
            {
                MessageBox.Show("Missing Information");
                return;
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("update VehicleTbl set Vmark=@Vma,Vmodel=@Vmo,VYear=@VY,VEngType=@VEng,VColor=@VCo,VMilleage=@VMi,VType=@VTy,Booked=@VB ,Driver=@Dr where VLp=@VP", Con);
                    cmd.Parameters.AddWithValue("@VP", LPlateTb.Text);
                    cmd.Parameters.AddWithValue("@Vma", MarkCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@Vmo", ModelTb.Text);
                    cmd.Parameters.AddWithValue("@VY", VYearCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@VEng", EngTypeCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@VCo", ColorTb.Text);
                    cmd.Parameters.AddWithValue("@VMi", MilleageTb.Text);
                    cmd.Parameters.AddWithValue("@VTy", TypeCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@VB", BookedCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@Dr", DriverCb.SelectedValue.ToString());
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Vehicle Updated");
                    Con.Close();
                    ShowVehicles();
                    Clear();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }



        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (LPlateTb.Text == "")

            {
                MessageBox.Show("Select a Vehicle");
                return;
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("delete from VehicleTbl where VLp = @VPlate ", Con);
                    cmd.Parameters.AddWithValue("@VPlate", LPlateTb.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Vehicle Deleted");

                    Con.Close();
                    ShowVehicles();
                    Clear();  
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);


                }
            }

        }   

        private void VehicleDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            LPlateTb.Text = VehicleDGV.SelectedRows[0].Cells[0].Value.ToString();
            MarkCb.SelectedItem = VehicleDGV.SelectedRows[0].Cells[1].Value.ToString();
            ModelTb.Text = VehicleDGV.SelectedRows[0].Cells[2].Value.ToString();
            VYearCb.SelectedItem = VehicleDGV.SelectedRows[0].Cells[3].Value.ToString();
            EngTypeCb.SelectedItem = VehicleDGV.SelectedRows[0].Cells[4].Value.ToString();
            ColorTb.Text = VehicleDGV.SelectedRows[0].Cells[5].Value.ToString();
            MilleageTb.Text = VehicleDGV.SelectedRows[0].Cells[6].Value.ToString();
            TypeCb.SelectedItem = VehicleDGV.SelectedRows[0].Cells[7].Value.ToString();
            BookedCb.SelectedItem = VehicleDGV.SelectedRows[0].Cells[8].Value.ToString();
        }


        private void pictureBox8_Click(object sender, EventArgs e)
        {
          
        }


        private void panel8_Paint(object sender, PaintEventArgs e)
        {
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label13_Click(object sender, EventArgs e)
        {
        }

        private void guna2CircleProgressBar1_ValueChanged(object sender, EventArgs e)
        {
        }

        private void label2_Click(object sender, EventArgs e)
        {
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Customers Obj = new Customers();
            Obj.Show();
            this.Hide();
        }

        private void label3_Click(object sender, EventArgs e)
        {
        }

        private void panel7_Paint(object sender, PaintEventArgs e)
        {
        }

        private void label6_Click(object sender, EventArgs e)
        {
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            Login Obj = new Login();
            Obj.Show();
            this.Hide();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
        }

        private void label4_Click(object sender, EventArgs e)
        {
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            Bookings Obj = new Bookings();
            Obj.Show();
            this.Hide();
        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {
        }

        private void label5_Click(object sender, EventArgs e)
        {
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            Dashboard Obj = new Dashboard();
            Obj.Show();
            this.Hide();
        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {
        }

        private void guna2DataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Drivers Obj = new Drivers();
            Obj.Show();
            this.Hide();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void label11_Click(object sender, EventArgs e)
        {
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void label16_Click(object sender, EventArgs e)
        {
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
        }

        private void label15_Click(object sender, EventArgs e)
        {
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void label14_Click(object sender, EventArgs e)
        {
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void button3_Click(object sender, EventArgs e)
        {
            EditBtn_Click(sender, e);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Save button in Designer maps here; call SaveBtn_Click
            SaveBtn_Click(sender, e);
        }

        private void label10_Click(object sender, EventArgs e)
        {
        }

        private void label9_Click(object sender, EventArgs e)
        {
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void label8_Click(object sender, EventArgs e)
        {
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void label7_Click(object sender, EventArgs e)
        {
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            DeleteBtn_Click(sender, e); 
        }

        private void Vehicles_Load(object sender, EventArgs e)
        {

        }
    }
}
