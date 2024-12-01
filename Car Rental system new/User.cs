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
    public partial class User : Form
    {
        public User()
        {
            InitializeComponent();
        }
        SqlConnection con;
        SqlCommand cmd;
        private void User_Load(object sender, EventArgs e)
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

        private void btn_update_Click(object sender, EventArgs e)
        {
            if (txt_uid.Text == "" || txt_uname.Text == "" || txt_uname.Text == "")
            {
                MessageBox.Show("Please enter Username and Password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    DialogResult dr = MessageBox.Show("Do you really want to Update?", "Infromation", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (dr.ToString() == "Yes")
                    {
                        con.Open();
                        cmd = new SqlCommand("Update user_table set Username = '" + txt_uname.Text + "' , U_password = '" + txt_password.Text + "' where U_ID= '" + txt_uid.Text + "' ", con);
                        int i = cmd.ExecuteNonQuery();
                        if (i == 1)
                            MessageBox.Show("Data Update Successfully", "Infromation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        else
                            MessageBox.Show("Data Cannot Update", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        con.Close();
                    }
                }
                catch (SqlException)
                {
                    MessageBox.Show("Database Error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception)
                {
                    MessageBox.Show("Please check again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            if (txt_uid.Text == "" || txt_uname.Text == "" || txt_uname.Text == "")
            {
                MessageBox.Show("Please enter Username and Password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    DialogResult dr = MessageBox.Show("Do you really want to Delete?", "Infromation", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (dr.ToString() == "Yes")
                    {
                        con.Open();
                        cmd = new SqlCommand("delete from user_table where U_ID = '" + txt_uid.Text + "'", con);
                        int i = cmd.ExecuteNonQuery();
                        if (i == 1)
                            MessageBox.Show("Data Delete Successfully", "Infromation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        else
                            MessageBox.Show("Data Cannot Delete", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        con.Close();
                    }
                }
                catch (SqlException)
                {
                    MessageBox.Show("Database Error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception)
                {
                    MessageBox.Show("Please check again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        private void button1_Click(object sender, EventArgs e)
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
            Main_menu main = new Main_menu();
            main.Show();
        }
    }
}
