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
    public partial class Customer : Form
    {
        public Customer()
        {
            InitializeComponent();
        }
        SqlConnection con;
        SqlCommand cmd;

        private void populate()
        {
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("select*from customer", con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            con.Close();
        }
        private void Customer_Load(object sender, EventArgs e)
        {
            { con = new SqlConnection("Data Source=DESKTOP-452MLMH;Initial Catalog=CarRental;Integrated Security=True"); }
            {
                SqlDataAdapter sda = new SqlDataAdapter("Select isnull(max(cast(C_ID as int)),0)+1 from customer ", con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                txt_id.Text = dt.Rows[0][0].ToString();
            }
            {
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("select*from customer", con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                dataGridView1.DataSource = dt;
                con.Close();
            }
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            if (txt_id.Text == "" || txt_fname.Text == "" || txt_lname.Text == "" || txt_address.Text == "" || txt_license.Text == "" || txt_phone.Text == "")
            {
                MessageBox.Show("Please enter the Name, License no, Contact no and Address", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    con.Open();
                    cmd = new SqlCommand("Insert into customer values('" + txt_id.Text + "','" + txt_fname.Text + "','" + txt_lname.Text + "','" + txt_address.Text + "','" + txt_license.Text + "','" + txt_phone.Text + "','" + dateTimePicker1.Value + "')", con);
                    int i = cmd.ExecuteNonQuery();
                    if (i == 1)
                        MessageBox.Show("Data saved successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        MessageBox.Show("Data not saved", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    con.Close();
                    populate();
                }
                catch (SqlException)
                { MessageBox.Show("Database error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                catch
                { MessageBox.Show("Check again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            if (txt_id.Text == "" || txt_fname.Text == "" || txt_lname.Text == "" || txt_address.Text == "" || txt_license.Text == "" || txt_phone.Text == "")
            {
                MessageBox.Show("Please enter data", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    DialogResult dr = MessageBox.Show("Do you really want to Update?", "Infromation", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (dr.ToString() == "Yes")
                    {
                        con.Open();
                        cmd = new SqlCommand("Update customer set First_name = '" + txt_fname.Text + "' , Last_name= '" + txt_lname.Text + "', U_Address = '" + txt_address.Text + "', License = '" + txt_license.Text + "', Phone_no = '" + txt_phone.Text + "', DOB = '" + dateTimePicker1.Value + "' where C_ID= '" + txt_id.Text + "' ", con);
                        int i = cmd.ExecuteNonQuery();
                        if (i == 1)
                            MessageBox.Show("Data Update Successfully", "Infromation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        else
                            MessageBox.Show("Data Cannot Update", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        con.Close();
                        populate();
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
            if (txt_id.Text == "" || txt_fname.Text == "" || txt_lname.Text == "" || txt_address.Text == "" || txt_license.Text == "" || txt_phone.Text == "")
            {
                MessageBox.Show("Please enter data", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    DialogResult dr = MessageBox.Show("Do you really want to Delete?", "Infromation", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (dr.ToString() == "Yes")
                    {
                        con.Open();
                        cmd = new SqlCommand("delete from customer where C_ID = '" + txt_id.Text + "'", con);
                        int i = cmd.ExecuteNonQuery();
                        if (i == 1)
                            MessageBox.Show("Data Delete Successfully", "Infromation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        else
                            MessageBox.Show("Data Cannot Delete", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        con.Close();
                        populate();
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

        private void btn_clear_Click(object sender, EventArgs e)
        {

            {
                txt_id.Clear();
                txt_fname.Clear();
                txt_lname.Clear();
                txt_address.Clear();
                txt_license.Clear();
                txt_phone.Clear();
            }
            {
                SqlDataAdapter sda = new SqlDataAdapter("Select isnull(max(cast(C_ID as int)),0)+1 from customer ", con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                txt_id.Text = dt.Rows[0][0].ToString();
            }
        }

         private void btn_back_Click(object sender, EventArgs e)
         {
            this.Hide();
            Main_menu main = new Main_menu();
            main.Show();
         }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txt_id.Text = dataGridView1.CurrentRow.Cells["C_ID"].Value.ToString();
            txt_fname.Text = dataGridView1.CurrentRow.Cells["First_name"].Value.ToString();
            txt_lname.Text = dataGridView1.CurrentRow.Cells["Last_name"].Value.ToString();
            txt_address.Text = dataGridView1.CurrentRow.Cells["U_Address"].Value.ToString();
            txt_license.Text = dataGridView1.CurrentRow.Cells["License"].Value.ToString();
            txt_phone.Text = dataGridView1.CurrentRow.Cells["Phone_No"].Value.ToString();
            dateTimePicker1.Text = dataGridView1.CurrentRow.Cells["DOB"].Value.ToString();
        }
    }
}