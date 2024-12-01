using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Car_Rental_system_new
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }
        SqlConnection con;
        SqlCommand cmd;
        private void Login_Load(object sender, EventArgs e)
        {
            con = new SqlConnection("Data Source=DESKTOP-452MLMH;Initial Catalog=CarRental;Integrated Security=True");
        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            if (txt_username.Text == "" || txt_password.Text == "")
            {
                MessageBox.Show("Please enter Username and Password", "Error", MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            else
            {
                string query = "select * from user_table where Username = '" + txt_username.Text + "' and U_password = '" + txt_password.Text + "' ";
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows.Count == 1)
                {
                    Main_menu obj = new Main_menu();
                    obj.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Wrong Username or Password", "Error");
                }
                con.Close();
            }
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            txt_username.Clear();
            txt_password.Clear();
        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_back_Click(object sender, EventArgs e)
        {
            this.Hide();
            Load obj = new Load();
            obj.Show();
        }
    }
}
