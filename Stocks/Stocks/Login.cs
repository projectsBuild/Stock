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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        
       private void btnClear_Click(object sender, EventArgs e)
        {
            txtbUserName.Text = "";
            txtbPassword.Clear();
            txtbUserName.Focus();

        }

        private void btnLogin_Click(object sender, EventArgs e)
           
        {
            SqlConnection con = new SqlConnection("Data Source = HARI; Initial Catalog = Stock; Integrated Security = True");
            SqlDataAdapter da = new SqlDataAdapter("select * from dbo.login where username ='"+txtbUserName.Text+"' and password = '"+txtbPassword.Text+ " '", con);
            //"select * from dbo.login where username ='"+txtbUserName.Text+"' and password = '"+txtbPassword.Text+ " '"
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows .Count == 1)
            {
                this.Hide();
                StockMain main = new StockMain();
                main.Show();
            }
            else
            { MessageBox.Show("Invalid username and password...!", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
               btnClear_Click(sender, e);
            }

        }
    }
}
