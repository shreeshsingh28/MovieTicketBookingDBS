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
    public partial class AdminLogin : Form
    {
        OracleConnection conn;
        public AdminLogin()
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
            command1.CommandText = "select adminlogin('" + textBox1.Text + "','" + textBox2.Text + "') from dual";
            command1.CommandType = CommandType.Text;
            OracleDataReader dr = command1.ExecuteReader();
            dr.Read();
            string str = dr.GetString(0);
            command1.Dispose();
            conn.Close();

            if(str == "true")
            {
                MessageBox.Show("success");
                AdminDashboard adminDashboard = new AdminDashboard();
                adminDashboard.Show();
            }
            else 
            {
                MessageBox.Show("failed");
            }
            command1.Dispose();
            conn.Close();


        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.ShowDialog();
        }

        private void AdminLogin_Load(object sender, EventArgs e)
        {

        }
    }
}
