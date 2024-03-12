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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsApplication1
{
    public partial class CustomerPriority : Form
    {
        OracleConnection conn;
        public CustomerPriority()
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
            ConnectDB();

            OracleCommand command1 = conn.CreateCommand();
            command1.CommandText = "DELETE FROM pcustomer_priority";
            command1.CommandType = CommandType.Text;
            command1.ExecuteNonQuery();
            command1.Dispose();

            OracleCommand command2 = conn.CreateCommand();
            command2.CommandText = "INSERT INTO pcustomer_priority (email, first_name, last_name, Total_spend)\r\nSELECT\r\n    pc.u_email AS email,\r\n    pc.fname AS first_name,\r\n    pc.lname AS last_name,\r\n    COALESCE(\r\n        (SELECT SUM(pt.price)\r\n         FROM pticket pt\r\n         WHERE pt.u_email = pc.u_email\r\n        ), 0) AS Total_spend\r\nFROM\r\n    pcustomer pc\r\nORDER BY\r\n    Total_spend desc";
            command2.CommandType = CommandType.Text;
            command2.ExecuteNonQuery();
            command2.Dispose();

            OracleCommand comm1 = conn.CreateCommand();
            comm1.CommandText = "SELECT * FROM pcustomer_priority";
            comm1.CommandType = CommandType.Text;

            // Create an OracleDataAdapter to execute the command and fill the DataSet
            OracleDataAdapter da = new OracleDataAdapter(comm1.CommandText, conn);

            // Create a DataSet to hold the data
            DataSet ds = new DataSet();

            // Fill the DataSet with the result of the OracleCommand
            da.Fill(ds, "pcustomer_priority");

            // Set the DataGridView's DataSource to the table in the DataSet
            dataGridView1.DataSource = ds.Tables["pcustomer_priority"];

            // Clean up
            da.Dispose();
            comm1.Dispose();



            conn.Close();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            AdminLogin login = new AdminLogin();
            login.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AdminDashboard login = new AdminDashboard();
            login.ShowDialog();
        }

        private void CustomerPriority_Load(object sender, EventArgs e)
        {

        }
    }
}
