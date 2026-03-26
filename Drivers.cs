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
    public partial class Drivers : Form
    {
        public Drivers()
        {
            InitializeComponent();

            // --- FIXES FOR MISSING CONTENT ---

            // 1. Adds scrollbars automatically if content is outside the window
            this.AutoScroll = true;

            // 2. Ensures the form starts in the middle of your screen
            this.StartPosition = FormStartPosition.CenterScreen;

            // 3. Optional: Uncomment the line below to make the window open maximized
            // this.WindowState = FormWindowState.Maximized;

            GetCars();
            ShowDrivers();
        }
        int Key = 0;

        // Keep your existing event handlers below
        private void pictureBox1_Click(object sender, EventArgs e) { }
        private void label1_Click(object sender, EventArgs e) { }
        private void pictureBox2_Click(object sender, EventArgs e) { }
        private void label2_Click(object sender, EventArgs e) { }
        private void label3_Click(object sender, EventArgs e) { }
        private void label4_Click(object sender, EventArgs e) { }
        private void label5_Click(object sender, EventArgs e) { }
        private void panel1_Paint(object sender, PaintEventArgs e) { }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void guna2CircleProgressBar1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void Drivers_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\User\Documents\TransportDb.mdf;Integrated Security=True;Connect Timeout=30");

        private void GetCars()
        {
            Con.Open(); 
            SqlCommand cmd = new SqlCommand("select * from VehicleTbl", Con);
            SqlDataReader rdr;
            rdr =  cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("Vlp", typeof(String));
            dt.Load(rdr);
            VehicleCb.ValueMember = "Vlp";
            VehicleCb.DataSource = dt;

            Con.Close();    

        }


        private void Clear()
        {
            DrNameTb.Text = "";
            GenCb.SelectedIndex = -1;
            VehicleCb.SelectedIndex = -1;
            PhoneTb.Text = "";
            DrAdd.Text = "";

        }



        private void ShowDrivers()
        {
            Con.Open();
            string Query = "select * from DriverTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            DriverDGV.DataSource = ds.Tables[0];
            Con.Close();

        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (DrNameTb.Text == "" || GenCb.SelectedIndex == -1 || PhoneTb.Text == "" || DrAdd.Text == "" || RatingCb.SelectedIndex == -1 )

            {
                MessageBox.Show("Missing Information");
                return;
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into DriverTbl (DrName,DrVehicle,Drphone,DrAdd,DrDOB,DrJoinDate,DrGen,DrRating) values (@DRN, @DrV, @DrP,@DRA,@DRD,@DRJ, @DrG,@DrR )", Con);
                    cmd.Parameters.AddWithValue("@DRN", DrNameTb.Text);
                    cmd.Parameters.AddWithValue("@DRV", VehicleCb.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@DRP", PhoneTb.Text);
                    cmd.Parameters.AddWithValue("@DRA", DrAdd.Text);
                    cmd.Parameters.AddWithValue("@DRD", DOB.Value.ToString());
                    cmd.Parameters.AddWithValue("@DRJ", JoinDate.Value.ToString());
                    cmd.Parameters.AddWithValue("@DRG", GenCb.SelectedItem?.ToString() ?? "");
                    cmd.Parameters.AddWithValue("@DRR", RatingCb.SelectedItem?.ToString() ?? "");
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Driver Recorded");

                    Con.Close();
                    ShowDrivers();
                    Clear();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);


                }
            }

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void DriverDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DrNameTb.Text = DriverDGV.SelectedRows[0].Cells[1].Value.ToString();
            VehicleCb.SelectedItem = DriverDGV.SelectedRows[0].Cells[2].Value.ToString();
            PhoneTb.Text = DriverDGV.SelectedRows[0].Cells[3].Value.ToString();
            DrAdd.Text = DriverDGV.SelectedRows[0].Cells[4].Value.ToString();
            DOB.Text = DriverDGV.SelectedRows[0].Cells[5].Value.ToString();
            JoinDate.Text = DriverDGV.SelectedRows[0].Cells[6].Value.ToString();
            GenCb.Text = DriverDGV.SelectedRows[0].Cells[7].Value.ToString();
            RatingCb.Text = DriverDGV.SelectedRows[0].Cells[8].Value.ToString();

            if (string.IsNullOrWhiteSpace(DrNameTb.Text))
            {
                Key = 0;
            }
            else if (DriverDGV.SelectedRows.Count > 0)
            {
                int.TryParse(DriverDGV.SelectedRows[0].Cells[0].Value.ToString(), out Key);
            }
        }
    }
}