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
    public partial class Create_account : Form
    {
        public Create_account()
        {
            InitializeComponent();
        }
        SqlConnection con;
        SqlCommand cmd;
        private void Create_account_Load(object sender, EventArgs e)
        {
            { con = new SqlConnection("Data Source=DESKTOP-452MLMH;Initial Catalog=CarRental;Integrated Security=True"); }
            {
                SqlDataAdapter sda = new SqlDataAdapter("Select isnull(max(cast(U_ID as int)),0)+1 from user_table ", con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                txt_uid.Text = dt.Rows[0][0].ToString();
            }
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            if (txt_uid.Text == "" || txt_uname.Text == "" || txt_uname.Text == "")
            {
                MessageBox.Show("Please enter Username and Password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    con.Open();
                    cmd = new SqlCommand("Insert into user_table values('" + txt_uid.Text + "','" + txt_uname.Text + "','" + txt_password.Text + "')", con);
                    int i = cmd.ExecuteNonQuery();
                    if (i == 1)
                        MessageBox.Show("Data saved successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        MessageBox.Show("Data not saved", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    con.Close();
                }
                catch (SqlException)
                { MessageBox.Show("Database error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                catch
                { MessageBox.Show("Check again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            {
                txt_uid.Clear();
                txt_uname.Clear();
                txt_password.Clear();
            }
            {
                SqlDataAdapter sda = new SqlDataAdapter("Select isnull(max(cast(U_ID as int)),0)+1 from user_table ", con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                txt_uid.Text = dt.Rows[0][0].ToString();
            }
        }

        private void btn_back_Click(object sender, EventArgs e)
        {
            this.Hide();
            Load obj = new Load();
            obj.Show();
        }

        private void btn_finish_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login obj = new Login();
            obj.Show();
        }
    }
}
