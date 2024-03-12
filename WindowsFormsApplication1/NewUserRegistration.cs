using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;

namespace WindowsFormsApplication1
{
    public partial class NewUserRegistration : Form
    {
        OracleConnection conn;
        public NewUserRegistration()
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

        static bool IsValidEmail(string email)
        {
            // Define a regular expression pattern for a simple email validation
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

            // Create a Regex object with the specified pattern
            Regex regex = new Regex(pattern);

            // Use the regex.IsMatch method to check if the email matches the pattern
            return regex.IsMatch(email);
        }
        static bool IsNumericString(string input)
        {
            // Use a regular expression to check if the input contains only numeric characters
            return Regex.IsMatch(input, @"^\d+$");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (IsValidEmail(textBox1.Text))
            {
                MessageBox.Show("Valid email address!");
            }
            else
            {
                MessageBox.Show("Invalid email address.");
                textBox1.Clear();
            }
            if (IsNumericString(textBox6.Text) && IsNumericString(textBox7.Text) && textBox6.Text.Length == 10 && textBox7.Text.Length == 10)
            {
                MessageBox.Show("Valid Number.");
            }
            else
            {
                MessageBox.Show("Invalid Number.");
                textBox6.Clear();
                textBox7.Clear();
            }
            try 
            {
                ConnectDB();
                OracleCommand command1 = conn.CreateCommand();
                command1.CommandText = "insert into pcustomer values('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + textBox5.Text + "')";
                command1.CommandType = CommandType.Text;
                command1.ExecuteNonQuery();
                command1.Dispose();

                OracleCommand command2 = conn.CreateCommand();
                command2.CommandText = "insert into pcustomer_phone values('" + textBox1.Text + "','" + textBox6.Text + "')";
                command2.CommandType = CommandType.Text;
                command2.ExecuteNonQuery();
                command2.Dispose();

                OracleCommand command3 = conn.CreateCommand();
                command3.CommandText = "insert into pcustomer_phone values('" + textBox1.Text + "','" + textBox7.Text + "')";
                command3.CommandType = CommandType.Text;
                command3.ExecuteNonQuery();
                MessageBox.Show("User Added !");
                command3.Dispose();

                conn.Close();

                UserLogin login = new UserLogin();
                login.ShowDialog();
            }
            catch (Exception ex) 
            {
                MessageBox.Show("Error Adding Data");
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            UserLogin login = new UserLogin();
            login.ShowDialog();
        }

        private void NewUserRegistration_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }
    }
}
