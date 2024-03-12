using System.Windows.Forms;
using System;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace WindowsFormsApplication1
{
    public partial class Register : Form
    {
        OracleConnection conn;
        public Register()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }
        public void ConnectDB()
        {

            conn = new OracleConnection("DATA SOURCE=192.168.56.1:1521/XE;USER ID=SYSTEM;PASSWORD=student");
            try
            {
                conn.Open();
                MessageBox.Show("Connected");
            }

            catch (Exception e1)
            {
            }

        }
        private bool AreTextFieldsEmpty()
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) ||
                string.IsNullOrWhiteSpace(textBox2.Text) ||
                string.IsNullOrWhiteSpace(textBox3.Text) ||
                string.IsNullOrWhiteSpace(textBox4.Text))
            {
                return true; // Returns true if any text box is empty
            }
            return false; // Returns false if all text boxes are filled
        }
        private void button1_Click(object sender, EventArgs e)
        {

            if (AreTextFieldsEmpty())
            {
                MessageBox.Show("Please fill in all fields before submitting.");
            }
            else
            {
                //seq.NextVal gives each user a unique id value
                ConnectDB();
                OracleCommand command1 = conn.CreateCommand();
                command1.CommandText = "insert into users(user_name, email, phone, password) values("+ "'" + textBox1.Text + "'" + "," + "'" + textBox2.Text + "'" + "," + "'" + textBox3.Text + "'" + "," + "'" + textBox4.Text + "'" + ")";
                command1.CommandType = CommandType.Text;
                command1.ExecuteNonQuery();
                MessageBox.Show("Inserted into Users Table");
                command1.Dispose();
                conn.Close();
                
                //Give code to connect to Home page
            }




        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void Register_Load(object sender, EventArgs e)
        {

        }
    }
}








