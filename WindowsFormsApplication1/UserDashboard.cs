using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class UserDashboard : Form
    {
        UserLogin login;
        BookingHistory bookingHistory;
        UserSelectMoviecs selectMoviecs;
        public UserDashboard()
        {
            InitializeComponent();
        }
        public UserDashboard(UserSelectMoviecs selectMoviecs)
        {
            InitializeComponent();
            this.selectMoviecs = selectMoviecs;
        }
        public UserDashboard(UserLogin login)
        {
            InitializeComponent();
            this.login = login;
        }
        public UserDashboard(BookingHistory bookingHistory)
        {
            InitializeComponent();
            this.bookingHistory = bookingHistory;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            UserLogin login = new UserLogin();
            login.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            BookingHistory history = new BookingHistory(this);
            history.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UserSelectMoviecs moviecs = new UserSelectMoviecs(this);
            moviecs.ShowDialog();
        }

        private void UserDashboard_Load(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {
            label5.Text = selectMoviecs.label9.Text;
        }

        private void label1_Click(object sender, EventArgs e)
        {
            label5.Text = login.textBox1.Text;
        }

        private void label5_Click(object sender, EventArgs e)
        {
            label5.Text = bookingHistory.label3.Text;

        }
    }
}
