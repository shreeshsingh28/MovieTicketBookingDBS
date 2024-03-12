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
    public partial class AdminAddShow : Form
    {
        OracleConnection conn;
        public AdminAddShow()
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
                command1.CommandText = "insert into pmovie_show values('" + textBox1.Text + "','" + comboBox1.GetItemText(comboBox1.SelectedItem) + "','" + comboBox2.GetItemText(comboBox2.SelectedItem) + "','" + textBox4.Text + "','50')";
                command1.CommandType = CommandType.Text;
                command1.ExecuteNonQuery();
                command1.Dispose();
                MessageBox.Show("Show Added");
                conn.Close();
                textBox1.Clear();
                textBox4.Clear();
            }
            catch(Exception e1)
            {
                MessageBox.Show("Error adding data !");
                textBox1.Clear();
                textBox4.Clear();
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            AdminDashboard adminDashboard = new AdminDashboard();
            adminDashboard.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AdminLogin login = new AdminLogin();
            login.Show();
        }

        private void AdminAddShow_Load(object sender, EventArgs e)
        {

        }

        private void comboBox1_DropDown(object sender, EventArgs e)
        {

        }

        private void comboBox2_DropDown(object sender, EventArgs e)
        {
            comboBox2.Items.Clear();
            ConnectDB();
            OracleCommand command1 = conn.CreateCommand();
            command1.CommandText = "select tname from ptheatre";
            command1.CommandType = CommandType.Text;
            OracleDataReader dr = command1.ExecuteReader();
            while (dr.Read())
            {
                comboBox2.Items.Add(dr["tname"].ToString());
            }
            command1.Dispose();
            conn.Close();
        }

        private void comboBox1_DropDown_1(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            ConnectDB();
            OracleCommand command1 = conn.CreateCommand();
            command1.CommandText = "select movie_name from pmovie";
            command1.CommandType = CommandType.Text;
            OracleDataReader dr = command1.ExecuteReader();
            while (dr.Read())
            {
                comboBox1.Items.Add(dr["movie_name"].ToString());
            }
            command1.Dispose();
            conn.Close();
        }
    }
}
