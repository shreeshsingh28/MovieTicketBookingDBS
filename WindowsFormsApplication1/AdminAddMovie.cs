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
    public partial class AdminAddMovie : Form
    {
        OracleConnection conn;
        public AdminAddMovie()
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
            try
            {
                ConnectDB();
                OracleCommand command1 = conn.CreateCommand();
                command1.CommandText = "insert into pmovie values('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "')";
                command1.CommandType = CommandType.Text;
                command1.ExecuteNonQuery();
                command1.Dispose();
                MessageBox.Show("Movie Added");
                conn.Close();
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();
            }
            catch(Exception e1)
            {
                MessageBox.Show("Error adding Data");
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            AdminDashboard adminDashboard = new AdminDashboard();
            adminDashboard.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AdminLogin login = new AdminLogin();
            login.ShowDialog();
        }

        private void AdminAddMovie_Load(object sender, EventArgs e)
        {

        }
    }
}
