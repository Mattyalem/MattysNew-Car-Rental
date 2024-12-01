using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Car_Rental_system_new
{
    public partial class Main_menu : Form
    {
        public Main_menu()
        {
            InitializeComponent();
        }

        private void btn_customer_Click(object sender, EventArgs e)
        {
            this.Hide();
            Customer obj = new Customer();
            obj.Show();
        }

        private void btn_car_Click(object sender, EventArgs e)
        {
            this.Hide();
            Cars obj = new Cars();
            obj.Show();
        }

        private void btn_rent_Click(object sender, EventArgs e)
        {
            this.Hide();
            Rent_form obj = new Rent_form();
            obj.Show();
        }

        private void btn_return_Click(object sender, EventArgs e)
        {
            this.Hide();
            Return obj = new Return();
            obj.Show();
        }

        private void btn_users_Click(object sender, EventArgs e)
        {
            this.Hide();
            User obj = new User();
            obj.Show();
        }

        private void btn_logout_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login obj = new Login();
            obj.Show();
        }
    }
}
