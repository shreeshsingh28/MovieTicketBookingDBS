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
    public partial class UserLogin : Form
    {
        OracleConnection conn;
        public UserLogin()
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
            command1.CommandText = "select cuslogin('" + textBox1.Text + "','" + textBox2.Text + "') from dual";
            command1.CommandType = CommandType.Text;
            OracleDataReader dr = command1.ExecuteReader();
            dr.Read();
            string str = dr.GetString(0);
            command1.Dispose();
            conn.Close();

            if (str == "true")
            {
                MessageBox.Show("Login Success !");

                UserDashboard userDashboard = new UserDashboard(this);
                userDashboard.Show();
            }
            else
            {
                MessageBox.Show("Login Failed !");
            }
            command1.Dispose();
            conn.Close();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            NewUserRegistration newUserRegistration = new NewUserRegistration();
            newUserRegistration.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form1 form = new Form1();
            form.ShowDialog();
        }

        private void UserLogin_Load(object sender, EventArgs e)
        {

        }
    }
}
