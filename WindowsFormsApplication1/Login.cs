using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Oracle.ManagedDataAccess.Client;
namespace WindowsFormsApplication1
{
    public partial class Login : Form
    {

        OracleConnection conn;
        public Login()
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
        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private bool AreTextFieldsEmpty()
        {
            if (string.IsNullOrWhiteSpace(textBox2.Text) ||
                string.IsNullOrWhiteSpace(textBox4.Text))
            {
                return true; // Returns true if any text box is empty
            }
            return false; // Returns false if all text boxes are filled
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            string pass;
            int user_id;
            if (AreTextFieldsEmpty())
            {
                MessageBox.Show("Please fill in all fields before submitting.");
            }
            else
            {
                ConnectDB();

                OracleCommand command1 = conn.CreateCommand();
                command1.CommandText = $"select USER_ID,PASSWORD from users where EMAIL ='{textBox2.Text}'";
                command1.CommandType = CommandType.Text;
                OracleDataReader dr = command1.ExecuteReader();
                dr.Read();
                user_id = dr.GetInt32(0);
                pass = dr.GetString(1);
                command1.Dispose();
                conn.Close();

                if (pass == textBox4.Text)
                {
                    MessageBox.Show("Correct Password");
                }
                else
                {
                    MessageBox.Show("Incorrect Password");
                }

                //Give code to connect to Register
            }
            

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Register register = new Register();
            register.Show();
            this.Hide();
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }
    }
}
