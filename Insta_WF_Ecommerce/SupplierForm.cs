using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Insta_WF_Ecommerce
{
    public partial class SupplierForm : Form
    {
        ConnectionStringSettings cnx = ConfigurationManager.ConnectionStrings["EcomProject"];
        public SupplierForm()
        {
            InitializeComponent();
        }

        public String InsertSupplier()
        {
            String values = $"INSERT INTO Supplier (FirstName, Address, PhoneNumber, Email, SocialR, SiretNumber, RcsNumber) " +
                $"VALUES ('{sTxtName.Text}','{sTxtAddress.Text}','{sTxtTel.Text}','{sTxtEmail.Text}','{sTxtSR.Text}','{sTxtSNumber.Text}','{sTxtRcs.Text}')";

            return values;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var connection = new SqlConnection(cnx.ConnectionString))
            {

                //int idSupplier = int.Parse(cBSupplier.SelectedValue.ToString());

                string queryString = InsertSupplier();
                connection.Open();
                var dCmd = new SqlCommand(queryString, connection);
                dCmd.CommandType = CommandType.Text;
                dCmd.ExecuteNonQuery();
                // string ab = dCmd.ExecuteNonQuery().ToString();
                //MessageBox.Show(ab);
                connection.Close();
                this.Close();
            }
        }
    }
}
