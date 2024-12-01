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
    public partial class Rent_form : Form
    {
        public Rent_form()
        {
            InitializeComponent();
        }
        SqlConnection con;
        SqlCommand cmd;

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

        private void fillregno()
        {
            con.Open();
            string query = "select Reg_no from cars where Availability = '" + "YES" + "'";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("Reg_no", typeof(string));
            dt.Load(rdr);
            comboBox1.ValueMember = "Reg_no";
            comboBox1.DataSource = dt;
            con.Close();
        }
        private void fillcustomerID()
        {
            con.Open();
            string query = "select  C_ID from customer";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("C_ID", typeof(string));
            dt.Load(rdr);
            comboBox2.ValueMember = "C_ID";
            comboBox2.DataSource = dt;
            con.Close();
        }

        private void fillcustomername()
        {
            //con.Open();
            string query = "select * from customer where C_ID= " + comboBox2.SelectedValue.ToString() + "";
            SqlCommand cmd = new SqlCommand(query, con);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            { txt_name.Text = dr["First_name"].ToString(); }
            con.Close();
        }

        private void updateonrent()
        {
            con.Open();
            cmd = new SqlCommand("Update cars set Availability = '" + "NO" + "' where Reg_no = '" + comboBox1.SelectedValue.ToString() + "' ", con);
            int i = cmd.ExecuteNonQuery();
            con.Close();
        }
        private void Rent_Load(object sender, EventArgs e)
        {
            { con = new SqlConnection("Data Source=DESKTOP-452MLMH;Initial Catalog=CarRental;Integrated Security=True"); }
            {
                SqlDataAdapter sda = new SqlDataAdapter("Select isnull(max(cast( Rent_ID as int)),0)+1 from rent_table ", con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                txt_id.Text = dt.Rows[0][0].ToString();
            }
            {
                fillregno();
                fillcustomerID();
            }
            {
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("select*from rent_table", con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                dataGridView1.DataSource = dt;
                con.Close();
            }
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            if (txt_id.Text == "" || txt_name.Text == "" || txt_price.Text == "" || comboBox1.Text == "" || comboBox2.Text == "" || dateTimePicker1.Text == "" || dateTimePicker2.Text == "")
            {
                MessageBox.Show("Please enter data", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    con.Open();
                    cmd = new SqlCommand("Insert into rent_table values('" + txt_id.Text + "','" + comboBox1.Text + "','" + comboBox2.Text + "','" + txt_name.Text + "','" + dateTimePicker1.Value + "','" + dateTimePicker2.Value + "','" + txt_price.Text + "', '" + comboBox3.Text + "')",con);
                    int i = cmd.ExecuteNonQuery();
                    if (i == 1)
                        MessageBox.Show("Data saved successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        MessageBox.Show("Data not saved", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    con.Close();
                    updateonrent();
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
            if (txt_id.Text == "" || txt_name.Text == "" || txt_price.Text == "" || comboBox1.Text == "" || comboBox2.Text == "" || dateTimePicker1.Text == "" || dateTimePicker2.Text == "")
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
                        cmd = new SqlCommand("Update rent set Reg_ID = '" + comboBox1.Text + "' , Customer_ID = '" + comboBox2.Text + "', C_name = '" + txt_name.Text + "', Rent_date = '" + dateTimePicker1.Value + "', Return_date = '" + dateTimePicker2.Value + "',Price = '" + txt_price.Text + "', Rent_status = '" + comboBox3.Text + "' where Rent_ID = '" + txt_id.Text + "' ", con);
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
            if (txt_id.Text == "" || txt_name.Text == "" || txt_price.Text == "" || comboBox1.Text == "" || comboBox2.Text == "" || dateTimePicker1.Text == "" || dateTimePicker2.Text == "")
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
                        cmd = new SqlCommand("delete from rent_table where Rent_ID = '" + txt_id.Text + "'", con);
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
                txt_name.Clear();
                txt_price.Clear();
            }
            {
                SqlDataAdapter sda = new SqlDataAdapter("Select isnull(max(cast( Rent_ID as int)),0)+1 from rent_table ", con);
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
            txt_id.Text = dataGridView1.CurrentRow.Cells["Rent_ID"].Value.ToString();
            comboBox1.Text = dataGridView1.CurrentRow.Cells["Reg_ID"].Value.ToString();
            comboBox2.Text = dataGridView1.CurrentRow.Cells["Customer_ID"].Value.ToString();
            txt_name.Text = dataGridView1.CurrentRow.Cells["C_name"].Value.ToString();
            dateTimePicker1.Text = dataGridView1.CurrentRow.Cells["Rent_date"].Value.ToString();
            dateTimePicker2.Text = dataGridView1.CurrentRow.Cells["Return_date"].Value.ToString();
            txt_price.Text = dataGridView1.CurrentRow.Cells["Price"].Value.ToString();
            comboBox3.Text = dataGridView1.CurrentRow.Cells["Rent_status"].Value.ToString();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillcustomername();
        }
    }
}
