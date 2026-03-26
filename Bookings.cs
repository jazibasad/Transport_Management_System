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
    public partial class Bookings : Form
    {
        public Bookings()
        {
            InitializeComponent();
            GetCustomers();
            ShowBookings();
            GetCars();
            UnameLbl.Text = Login.User;
        }


        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\User\Documents\TransportDb.mdf;Integrated Security=True;Connect Timeout=30");

        private void GetCustomers()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("select * from CustomerTbl", Con);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("CustName", typeof(String));
            dt.Load(rdr);
            CustCb.ValueMember = "CustName";
            CustCb.DataSource = dt;
            Con.Close();
        }


       private void GetDrivers()
        {
            Con.Open();
            string Query = "select * from VehicleTbl where Vlp = '" + VehicleCb.SelectedValue.ToString()+ "'" ;
            SqlCommand cmd = new SqlCommand(Query, Con);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                DriverTb.Text = dr["Driver"].ToString();
            }
            Con.Close();
        }
        

        private void GetCars()
        {
            string Isbooked = "No";
            Con.Open();
            SqlCommand cmd = new SqlCommand("select * from VehicleTbl where Booked='" + Isbooked + "'", Con);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("Vlp", typeof(String));
            dt.Load(rdr);
            VehicleCb.ValueMember = "Vlp";
            VehicleCb.DataSource = dt;
            Con.Close();
        }




        private void Clear()
        {
            CustCb.SelectedIndex = -1;
            VehicleCb.SelectedIndex = -1;
            DriverTb.Text = "";
            AmountTb.Text = "";

        }



        private void ShowBookings()
        {
            Con.Open();
            string Query = "select * from BookingTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            BookingDGV.DataSource = ds.Tables[0];
            Con.Close();

        }


        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void UpdateVehicle()
        {
            try
            {
                Con.Open();
                SqlCommand cmd = new SqlCommand("update VehicleTbl set Booked=@VB where VLp=@VP", Con);
                cmd.Parameters.AddWithValue("@VP", VehicleCb.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@VB", "Yes");
                cmd.ExecuteNonQuery();
                MessageBox.Show("Vehicle Updated");
                Con.Close();
                Clear();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }
        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (CustCb.SelectedIndex == -1  || VehicleCb.SelectedIndex == -1 || DriverTb.Text == "" || AmountTb.Text == "")

            {
                MessageBox.Show("Missing Information");
                return;
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into BookingTbl (CustName,Vehicle,Driver,PickupDate, DropoffDate,Amount,BUser) values (@CN, @Veh, @Dri, @PDate, @DDate,@Am, @UN)", Con);
                    cmd.Parameters.AddWithValue("@CN", CustCb.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@Veh", VehicleCb.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@Dri", DriverTb.Text);
                    cmd.Parameters.AddWithValue("@PDate", PickUpDate.Value.Date);
                    cmd.Parameters.AddWithValue("@DDate", RetDate.Value.Date);
                    cmd.Parameters.AddWithValue("@Am", AmountTb.Text);
                    cmd.Parameters.AddWithValue("@UN", UnameLbl.Text);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Vehicle Booked");

                    Con.Close();
                    ShowBookings();
                    UpdateVehicle();
                    GetCars();
                    Clear();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);


                }
            }
        }

        private void BookingDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void VehicleCb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GetDrivers();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Vehicles Obj = new Vehicles();
            Obj.Show();
            this.Hide();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Customers Obj = new Customers();
            Obj.Show();
            this.Hide();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Drivers Obj = new Drivers();
            Obj.Show();
            this.Hide();
        }

        private void UnameLbl_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {

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
    }
}
