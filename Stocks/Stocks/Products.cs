using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Stocks
{
    public partial class Products : Form
    {
        private readonly string ProductCode;

        public Products()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Products_Load(object sender, EventArgs e)
        {
            cbxStatus.SelectedIndex = 0;
            LoadGrid();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source = HARI; Initial Catalog = Stock; Integrated Security = True");
            con.Open();
            var sqlQuery = "";
            bool status = false;
            if (cbxStatus.SelectedIndex==0)
            {
                status = true;
            }
            else
            {
                status = false;
            }
            if (IfProductExists(con, tbxProductCode.Text))
                {
                MessageBox.Show("Update");
                sqlQuery = @"UPDATE [dbo].[Products1]
   SET [ProductName] = '" + tbxProductName.Text + "',[ProductStatus] = '" + status + "' WHERE [ProductCode] = '" + tbxProductCode.Text + "'";
            }
            else
            {
              sqlQuery=  @"INSERT INTO [dbo].[Products1]
           ([ProductCode]
           ,[ProductName]
           ,[ProductStatus])
     VALUES
           ('" + tbxProductCode.Text + "', '" + tbxProductName.Text + "','" + status + "')";
                MessageBox.Show("row inserted");
            }
            SqlCommand cmd = new SqlCommand(sqlQuery, con);
                cmd.ExecuteNonQuery();
            
            con.Close();
            LoadGrid();
           

        }
        private bool IfProductExists(SqlConnection con, string prodCode )
        { 
            SqlDataAdapter da = new SqlDataAdapter("select 1 from [Products1] where [ProductCode] = '"+prodCode+"'", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)

                return true;
            else
                return false;
            
        }
        public void LoadGrid()
        {
            SqlConnection con = new SqlConnection("Data Source = HARI; Initial Catalog = Stock; Integrated Security = True");
            SqlDataAdapter da = new SqlDataAdapter("select * from [dbo].[Products1]", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.Rows.Clear();
            foreach (DataRow item in dt.Rows)
            {
                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells[0].Value = item["ProductCode"].ToString();
                dataGridView1.Rows[n].Cells[1].Value = item["ProductName"].ToString();
                if ((bool)item["ProductStatus"])
                {
                    dataGridView1.Rows[n].Cells[2].Value = "Active";
                }
                else
                {
                    dataGridView1.Rows[n].Cells[2].Value = "Deactivate";

                }
            }
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            //tbxProductCode.Text = dataGridView1.Rows[0].Cells[0].Value.ToString();
            //tbxProductName.Text = dataGridView1.Rows[0].Cells[1].Value.ToString();
            //if (dataGridView1.Rows [0].Cells[2].Value.ToString ()=="Active")
            //{
            //    cbxStatus.SelectedIndex = 0;
            //}
            //else
            //{
            //    cbxStatus.SelectedIndex = 1;
            //}

           
        }

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            tbxProductCode.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            tbxProductName.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            if (dataGridView1.SelectedRows [0].Cells[2].Value.ToString() == "Active")
            {
                cbxStatus.SelectedIndex = 0;
            }
            else
            {
                cbxStatus.SelectedIndex = 1;
            }

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source = HARI; Initial Catalog = Stock; Integrated Security = True");
            var sqlQuery = "";
            if (IfProductExists(con, tbxProductCode.Text))
            {
                
                MessageBox.Show("Delete");
                sqlQuery = "delete from Products1 where [ProductCode]='" + tbxProductCode.Text + "'";
                con.Open();
                SqlCommand cmd = new SqlCommand(sqlQuery, con);
                cmd.ExecuteNonQuery();

                con.Close();

            }
            else
            {
              
                MessageBox.Show("Record doesn't exists");
            }

            
            LoadGrid();
        }
    }
}
 