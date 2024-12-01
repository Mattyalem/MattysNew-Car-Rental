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
    public partial class Cars : Form
    {
        public Cars()
        {
            InitializeComponent();
        }
        SqlConnection con;
        SqlCommand cmd;

        private void populate()
        {
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("select*from cars", con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            con.Close();
        }
        private void Cars_Load(object sender, EventArgs e)
        {
            { con = new SqlConnection("Data Source=DESKTOP-452MLMH;Initial Catalog=CarRental;Integrated Security=True"); }
            {
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("select*from cars", con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                dataGridView1.DataSource = dt;
                con.Close();
            }
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            if (txt_brand.Text == "" || txt_regno.Text == "" || txt_type.Text == "" || txt_insu.Text == "" || txt_model.Text == "" || txt_dprice.Text == "")
            {
                MessageBox.Show("Please enter data", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    con.Open();
                    cmd = new SqlCommand("Insert into cars values('" + txt_regno.Text + "','" + txt_type.Text + "','" + txt_brand.Text + "','" + txt_model.Text + "','" + txt_dprice.Text + "','" + txt_insu.Text + "','" + comboBox1.SelectedItem.ToString() + "')", con);
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
            if (txt_regno.Text == "" || txt_brand.Text == "" || txt_type.Text == "" || txt_model.Text == "" || txt_dprice.Text == "" || txt_insu.Text == "")
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
                        cmd = new SqlCommand("Update cars set Type = '" + txt_type.Text + "' , Brand = '" + txt_brand.Text + "', Model = '" + txt_model.Text + "', D_price = '" + txt_dprice.Text + "', Insuarance_no = '" + txt_insu.Text + "', Availability = '" + comboBox1.SelectedItem.ToString() + "' where Reg_no = '" + txt_regno.Text + "' ", con);
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
            if (txt_regno.Text == "" || txt_brand.Text == "" || txt_type.Text == "" || txt_model.Text == "" || txt_dprice.Text == "" || txt_insu.Text == "")
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
                        cmd = new SqlCommand("delete from cars where Reg_no = '" + txt_regno.Text + "'", con);
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
            txt_regno.Clear();
            txt_type.Clear();
            txt_brand.Clear();
            txt_model.Clear();
            txt_dprice.Clear();
            txt_insu.Clear();
        }

        private void btn_back_Click(object sender, EventArgs e)
        {
            this.Hide();
            Main_menu main = new Main_menu();
            main.Show();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txt_regno.Text = dataGridView1.CurrentRow.Cells["Reg_no"].Value.ToString();
            txt_type.Text = dataGridView1.CurrentRow.Cells["Type"].Value.ToString();
            txt_brand.Text = dataGridView1.CurrentRow.Cells["Brand"].Value.ToString();
            txt_model.Text = dataGridView1.CurrentRow.Cells["Model"].Value.ToString();
            txt_dprice.Text = dataGridView1.CurrentRow.Cells["D_price"].Value.ToString();
            txt_insu.Text = dataGridView1.CurrentRow.Cells["Insuarance_no"].Value.ToString();
            comboBox1.Text = dataGridView1.CurrentRow.Cells["Availability"].Value.ToString();
        }
    }
}
