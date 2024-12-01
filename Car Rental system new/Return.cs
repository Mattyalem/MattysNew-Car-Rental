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
    public partial class Return : Form
    {
        public Return()
        {
            InitializeComponent();
        }
        SqlConnection con;
        SqlCommand cmd;

        private void updateonreturn()
        {
            con.Open();
            cmd = new SqlCommand("Update rent_table set Rent_status = '" + "Returned" + "' where Reg_ID = '" + txt_regno.Text + "' ", con);
            int i = cmd.ExecuteNonQuery();
            con.Close();
            populate();
        }
        private void updatecars()
        {
            con.Open();
            cmd = new SqlCommand("Update cars set Availability = '" + "YES" + "' where Reg_no = '" + txt_regno.Text + "' ", con);
            int i = cmd.ExecuteNonQuery();
            con.Close();
            populate();
        }

        private void populate()
        {
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("select*from rent_table", con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            con.Close();
        }
        private void populate1()
        {
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("select*from returntbl", con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            dataGridView2.DataSource = ds.Tables[0];
            con.Close();
        }
        private void Return_Load(object sender, EventArgs e)
        {
            { con = new SqlConnection("Data Source=DESKTOP-452MLMH;Initial Catalog=CarRental;Integrated Security=True"); }
            {
                SqlDataAdapter sda = new SqlDataAdapter("Select isnull(max(cast( Return_ID as int)),0)+1 from returntbl ", con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                txt_id.Text = dt.Rows[0][0].ToString();
            }
            {
                {
                    con.Open();
                    SqlDataAdapter sda = new SqlDataAdapter("select*from returntbl", con);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    dataGridView2.DataSource = dt;
                    con.Close();
                }
            }
            populate();
        }

        private void btn_add_Click(object sender, EventArgs e)
        {

            if (txt_id.Text == "" || txt_regno.Text == "" || txt_custid.Text == "" || txt_delay.Text == "" || txt_echarges.Text == "" || dateTimePicker1.Text == "")
            {
                MessageBox.Show("Please enter data", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    con.Open();
                    cmd = new SqlCommand("Insert into returntbl values('" + txt_id.Text + "','" + txt_regno.Text + "','" + txt_custid.Text + "','" + dateTimePicker1.Value + "','" + txt_delay.Text + "','" + txt_echarges.Text + "')", con);
                    int i = cmd.ExecuteNonQuery();
                    if (i == 1)
                        MessageBox.Show("Data saved successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        MessageBox.Show("Data not saved", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    con.Close();
                    populate1();
                    updateonreturn();
                    updatecars();

                }
                catch (SqlException)
                { MessageBox.Show("Database error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                catch
                { MessageBox.Show("Check again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            if (txt_id.Text == "" || txt_regno.Text == "" || txt_custid.Text == "" || txt_delay.Text == "" || txt_echarges.Text == "" || dateTimePicker1.Text == "")
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
                        cmd = new SqlCommand("Update returntbl set Registration_ID = '" + txt_regno.Text + "' , Customer_id = '" + txt_custid.Text + "', Return_date = '" + dateTimePicker1.Value + "', Delay = '" + txt_delay.Text + "',Extra_charges = '" + txt_echarges.Text + "' where Return_ID = '" + txt_id.Text + "' ", con);
                        int i = cmd.ExecuteNonQuery();
                        if (i == 1)
                            MessageBox.Show("Data Update Successfully", "Infromation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        else
                            MessageBox.Show("Data Cannot Update", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        con.Close();
                        populate1();
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
            if (txt_id.Text == "" || txt_regno.Text == "" || txt_custid.Text == "" || txt_delay.Text == "" || txt_echarges.Text == "" || dateTimePicker1.Text == "")
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
                        cmd = new SqlCommand("delete from returntbl where Return_ID = '" + txt_id.Text + "'", con);
                        int i = cmd.ExecuteNonQuery();
                        if (i == 1)
                            MessageBox.Show("Data Deleted Successfully", "Infromation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        else
                            MessageBox.Show("Data Cannot Delete", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        con.Close();
                        populate1();
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
                txt_regno.Clear();
                txt_custid.Clear();
                txt_delay.Clear();
                txt_echarges.Clear();
            }
            {
                SqlDataAdapter sda = new SqlDataAdapter("Select isnull(max(cast( Return_ID as int)),0)+1 from returntbl ", con);
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
            txt_regno.Text = dataGridView1.CurrentRow.Cells["Reg_ID"].Value.ToString();
            txt_custid.Text = dataGridView1.CurrentRow.Cells["Customer_ID"].Value.ToString();
            dateTimePicker1.Text = dataGridView1.CurrentRow.Cells["Return_date"].Value.ToString();
            DateTime d1 = DateTime.Now;
            DateTime d2 = dateTimePicker1.Value.Date;
            TimeSpan t = d1 - d2;
            int delay = Convert.ToInt32(t.TotalDays);
            if (delay <= 0)
            {
                txt_delay.Text = "0";
                txt_echarges.Text = "0";
            }
            else
            {
                txt_delay.Text = "" + delay;
                txt_echarges.Text = "" + (delay * 500);
            }
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txt_id.Text = dataGridView2.CurrentRow.Cells["Return_ID"].Value.ToString();
            txt_regno.Text = dataGridView2.CurrentRow.Cells["Registration_ID"].Value.ToString();
            txt_custid.Text = dataGridView2.CurrentRow.Cells["Customer_id"].Value.ToString();
            dateTimePicker1.Text = dataGridView2.CurrentRow.Cells["Return_date"].Value.ToString();
            txt_delay.Text = dataGridView2.CurrentRow.Cells["Delay"].Value.ToString();
            txt_echarges.Text = dataGridView2.CurrentRow.Cells["Extra_charges"].Value.ToString();
        }
    }
}
