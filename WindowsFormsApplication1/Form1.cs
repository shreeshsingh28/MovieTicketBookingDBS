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
    public partial class Form1 : Form
    {
        OracleConnection conn;

        public Form1()
        {
            InitializeComponent();
        }

        public void ConnectDB()
        {
            conn = new OracleConnection("DATA SOURCE=192.168.69.146:1521/XE;USER ID=SYSTEM;PASSWORD=1234");
            try
            {
                conn.Open();
                MessageBox.Show("Connected to Database !");
            }

            catch (Exception e1)
            {
                MessageBox.Show("Connection Error !");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ConnectDB();
            conn.Close();
          
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AdminLogin adminLogin = new AdminLogin();
            adminLogin.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            UserLogin userLogin = new UserLogin();
            userLogin.ShowDialog();
        }
    }
}
