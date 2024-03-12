using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsApplication1
{
    public partial class UserSelectMoviecs : Form
    {
        OracleConnection conn;
        int price;
        string show_id;
        int left, req;
        UserDashboard user;
        public UserSelectMoviecs()
        {
            InitializeComponent();
        }

        public UserSelectMoviecs(UserDashboard user)
        {
            InitializeComponent();
            this.user = user;
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


        private void comboBox1_DropDown(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            ConnectDB();
            OracleCommand command1 = conn.CreateCommand();
            command1.CommandText = "select tname from ptheatre";
            command1.CommandType = CommandType.Text;
            OracleDataReader dr = command1.ExecuteReader();
            while (dr.Read())
            {
                comboBox1.Items.Add(dr["tname"].ToString());
            }
            command1.Dispose();
            conn.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_DropDown(object sender, EventArgs e)
        {
            comboBox2.Items.Clear();
            ConnectDB();
            OracleCommand command1 = conn.CreateCommand();
            command1.CommandText = "select screen_no from pscreen where tname = '" + comboBox1.GetItemText(comboBox1.SelectedItem) + "'";
            command1.CommandType = CommandType.Text;
            OracleDataReader dr = command1.ExecuteReader();
            while (dr.Read())
            {
                comboBox2.Items.Add(dr["screen_no"].ToString());
            }
            command1.Dispose();
            conn.Close();
        }

        private void comboBox3_DropDown(object sender, EventArgs e)
        {
            comboBox3.Items.Clear();
            ConnectDB();
            OracleCommand command1 = conn.CreateCommand();
            command1.CommandText = "select DISTINCT movie_name from pmovie_show";
            command1.CommandType = CommandType.Text;
            OracleDataReader dr = command1.ExecuteReader();
            while (dr.Read())
            {
                comboBox3.Items.Add(dr["movie_name"].ToString());
            }
            command1.Dispose();
            conn.Close();
        }

        private void comboBox4_DropDown(object sender, EventArgs e)
        {
            comboBox4.Items.Clear();
            ConnectDB();
            OracleCommand command1 = conn.CreateCommand();
            command1.CommandText = "select timings from pmovie_show where movie_name = '" + comboBox3.GetItemText(comboBox3.SelectedItem) + "'";
            command1.CommandType = CommandType.Text;
            OracleDataReader dr = command1.ExecuteReader();
            while (dr.Read())
            {
                comboBox4.Items.Add(dr["timings"].ToString());
            }
            command1.Dispose();
            conn.Close();
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            string formattedDateString="";
            if (DateTime.TryParseExact(comboBox4.GetItemText(comboBox4.SelectedItem), "dd-MM-yyyy h:mm:ss tt", null, System.Globalization.DateTimeStyles.None, out DateTime dateTime))
            {
                // Format the DateTime object to the desired string representation
                formattedDateString = dateTime.ToString("dd-MMM-yyyy h:mm:ss tt").ToUpper(); // Convert to uppercase for month abbreviation
            }
            ConnectDB();
            OracleCommand command1 = conn.CreateCommand();
            command1.CommandText = "select seats_left from pmovie_show where movie_name = '" + comboBox3.GetItemText(comboBox3.SelectedItem) + "' and timings='" + formattedDateString + "'";
            command1.CommandType = CommandType.Text;
            OracleDataReader dr = command1.ExecuteReader();
            dr.Read();
            textBox1.Text = dr["seats_left"].ToString();   
            command1.Dispose();
            conn.Close();

            ConnectDB();
            OracleCommand command2 = conn.CreateCommand();
            command2.CommandText = "select show_id from pmovie_show where movie_name = '" + comboBox3.GetItemText(comboBox3.SelectedItem) + "' and timings='" + formattedDateString + "'";
            command2.CommandType = CommandType.Text;
            OracleDataReader dr1 = command2.ExecuteReader();
            dr1.Read();
            show_id = dr1["show_id"].ToString();
            command2.Dispose();
            conn.Close();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            
            int.TryParse(textBox1.Text, out left);
            int.TryParse(textBox2.Text, out req);

            if (left < req)
            {
                textBox3.Clear();
                MessageBox.Show("Seats not available");
            }
            else
            {
                price = req * 100;
                textBox3.Text = price.ToString();
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OracleParameter tidParameter = new OracleParameter("p_tid", OracleDbType.Varchar2, 20)
            {
                Direction = System.Data.ParameterDirection.Output
            };

            ConnectDB();

                using (OracleCommand command = new OracleCommand("insert_ticket_and_return_tid", conn))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    // Input parameters
                    command.Parameters.Add("p_u_email", OracleDbType.Varchar2).Value = label9.Text;
                    command.Parameters.Add("p_show_id", OracleDbType.Varchar2).Value = show_id;
                    command.Parameters.Add("p_price", OracleDbType.Int64).Value = price;
                    command.Parameters.Add("p_screen_no", OracleDbType.Varchar2).Value = comboBox2.GetItemText(comboBox2.SelectedItem);
                    command.Parameters.Add("p_tname", OracleDbType.Varchar2).Value = comboBox1.GetItemText(comboBox1.SelectedItem);
                    command.Parameters.Add("P_no_of_seats", OracleDbType.Varchar2).Value = textBox2.Text;

                    // Output parameter
                    command.Parameters.Add(tidParameter);

                    command.ExecuteNonQuery();

                    // Display the generated Tid
                    string generatedTid = tidParameter.Value.ToString();
                    MessageBox.Show("Generated Tid: " + generatedTid);
                }
    

            int newno = left - req;
            OracleCommand command2 = conn.CreateCommand();
            command2.CommandText = "update pmovie_show set seats_left = '" + newno.ToString() + "' where show_id = '" + show_id + "'";
            command2.CommandType = CommandType.Text;
            command2.ExecuteNonQuery();
            command2.Dispose();
            conn.Close();


        }

        private void button3_Click(object sender, EventArgs e)
        {
            UserLogin login = new UserLogin();
            login.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            UserDashboard userDashboard = new UserDashboard(this);
            userDashboard.ShowDialog();
        }

        private void UserSelectMoviecs_Load(object sender, EventArgs e)
        {
            label9.Text = user.label5.Text;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ConnectDB();
            OracleCommand command2 = conn.CreateCommand();
            command2.CommandText = "SELECT movie_name\r\nFROM pmovie_show\r\nWHERE seats_left = (\r\n    SELECT MIN(seats_left)\r\n    FROM pmovie_show\r\n)\r\n";
            command2.CommandType = CommandType.Text;
            OracleDataReader dr1 = command2.ExecuteReader();
            dr1.Read();
            MessageBox.Show(dr1["movie_name"].ToString());
            command2.Dispose();
            conn.Close();

        }

        private void button5_Click(object sender, EventArgs e)
        {
            ConnectDB();
            OracleCommand command2 = conn.CreateCommand();
            command2.CommandText = "SELECT tname\r\nFROM pscreen\r\nGROUP BY tname\r\nORDER BY SUM(total_seats) DESC\r\nFETCH FIRST ROW ONLY\r\n";
            command2.CommandType = CommandType.Text;
            OracleDataReader dr1 = command2.ExecuteReader();
            dr1.Read();
            MessageBox.Show(dr1.GetString(0));
            command2.Dispose();
            conn.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ConnectDB();

            // Assuming conn is an OracleConnection object that is properly initialized and opened.

            using (OracleCommand command2 = conn.CreateCommand())
            {
                command2.CommandText = "SELECT timings " +
                                       "FROM pmovie_show " +
                                       "WHERE timings = ( " +
                                       "    SELECT MIN(timings) " +
                                       "    FROM pmovie_show " +
                                       "    WHERE movie_name = :movieName)";
                command2.CommandType = CommandType.Text;

                // Assuming comboBox3 is a ComboBox containing the movie name.
                command2.Parameters.Add(new OracleParameter("movieName", comboBox3.GetItemText(comboBox3.SelectedItem)));

                using (OracleDataReader dr1 = command2.ExecuteReader())
                {
                    if (dr1.Read())
                    {
                        MessageBox.Show(dr1.GetDateTime(0).ToString());  // Assuming timings is of type timestamp
                    }
                    else
                    {
                        MessageBox.Show("No results found.");
                    }
                }
            }

            // Make sure to properly close the connection when done.
            conn.Close();


        }

        private void label9_Click(object sender, EventArgs e)
        {

        }
    }
}
