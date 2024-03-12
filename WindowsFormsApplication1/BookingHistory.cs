using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;

namespace WindowsFormsApplication1
{
    public partial class BookingHistory : Form
    {
        UserDashboard f1;
        OracleConnection conn;
        public BookingHistory()
        {
            InitializeComponent();
        }
        public BookingHistory(UserDashboard fr1)
        {
            InitializeComponent();
            this.f1 = fr1;
        }
        public void ConnectDB()
        {
            conn = new OracleConnection("DATA SOURCE=192.168.69.146:1521/XE;USER ID=SYSTEM;PASSWORD=1234");
            try
            {
                conn.Open();
                MessageBox.Show("Connected");
            }
            catch (Exception e1)
            {
                MessageBox.Show("Error in connection !");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ConnectDB(); // This method should open your Oracle connection

            OracleCommand comm1 = conn.CreateCommand();
            comm1.CommandText = "select tid,show_id,no_of_seats,price,tname,screen_no from pticket where u_email = '" + label3.Text + "'";
            comm1.CommandType = CommandType.Text;

            // Create an OracleDataAdapter to execute the command and fill the DataSet
            OracleDataAdapter da = new OracleDataAdapter(comm1.CommandText, conn);

            // Create a DataSet to hold the data
            DataSet ds = new DataSet();

            // Fill the DataSet with the result of the OracleCommand
            da.Fill(ds, "pticket");

            // Set the DataGridView's DataSource to the table in the DataSet
            dataGridView1.DataSource = ds.Tables["pticket"];

            // Clean up
            da.Dispose();
            comm1.Dispose();
            conn.Close();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            UserLogin login = new UserLogin();
            login.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            UserDashboard login = new UserDashboard(this);
            login.Show();
        }

        private void BookingHistory_Load(object sender, EventArgs e)
        {
            label3.Text = f1.label5.Text;
        }
    }
}

