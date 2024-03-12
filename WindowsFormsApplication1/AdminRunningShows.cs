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
    public partial class AdminRunningShows : Form
    {
        OracleConnection conn;
        public AdminRunningShows()
        {
            InitializeComponent();
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
            comm1.CommandText = "SELECT * from pmovie_show";
            comm1.CommandType = CommandType.Text;

            // Create an OracleDataAdapter to execute the command and fill the DataSet
            OracleDataAdapter da = new OracleDataAdapter(comm1.CommandText, conn);

            // Create a DataSet to hold the data
            DataSet ds = new DataSet();

            // Fill the DataSet with the result of the OracleCommand
            da.Fill(ds, "pmovie_show");

            // Set the DataGridView's DataSource to the table in the DataSet
            dataGridView1.DataSource = ds.Tables["pmovie_show"];

            // Clean up
            da.Dispose();
            comm1.Dispose();
            conn.Close();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            AdminDashboard adminDashboard = new AdminDashboard();
            adminDashboard.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AdminLogin adminLogin = new AdminLogin();
            adminLogin.ShowDialog();
        }

        private void AdminRunningShows_Load(object sender, EventArgs e)
        {

        }
    }
}
