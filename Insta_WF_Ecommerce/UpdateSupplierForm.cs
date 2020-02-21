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
    public partial class UpdateSupplierForm : Form
    {
        int id;
        ConnectionStringSettings cnx = ConfigurationManager.ConnectionStrings["EcomProject"];

        public UpdateSupplierForm()
        {
            //InitializeComponent();
        }

        public UpdateSupplierForm(int id)
        {
            InitializeComponent();
            this.id = id;
        }


        public String updateSupplier(int id)
        {
            String value = $"UPDATE Supplier SET FirstName='{uTxtName.Text}', Address='{uTxtAddress.Text}', PhoneNumber='{uTxtPhone.Text}', " +
                $"Email='{uTxtEmail.Text}', SocialR='{uTxtSocialR.Text}', SiretNumber='{uTxtSiret.Text}', RcsNumber='{uTxtRcs.Text}'" +
                $" WHERE idSupplier={id}";
            return value;
        }
        public String getConsumerInfo(int id)
        {
            String value = $"SELECT Firstname, Address, PhoneNumber, Email, SocialR, SiretNumber, RcsNumber" +
                $" FROM Supplier WHERE IdSupplier={id}";
            return value;
        }

        private void UpdateSupplierForm_Load(object sender, EventArgs e)
        {

            MessageBox.Show(this.id.ToString());
            using (var connection = new SqlConnection(cnx.ConnectionString))
            {
                string queryString = getConsumerInfo(this.id);

                var command = new SqlCommand(queryString, connection);
                connection.Open();

                var reader = command.ExecuteReader();

                if (reader.Read())
                {
                    uTxtName.Text = reader["FirstName"].ToString();
                    uTxtAddress.Text = reader["Address"].ToString();
                    uTxtPhone.Text = reader["PhoneNumber"].ToString();
                    uTxtEmail.Text = reader["Email"].ToString();
                    uTxtSocialR.Text = reader["SocialR"].ToString();
                    uTxtSiret.Text = reader["SiretNumber"].ToString();
                    uTxtRcs.Text = reader["RcsNumber"].ToString();

                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            using (var connection = new SqlConnection(cnx.ConnectionString))
            {
                String queryString = updateSupplier(this.id);
                connection.Open();
                var dCmd = new SqlCommand(queryString, connection);
                dCmd.CommandType = CommandType.Text;

                /*try
                {
                    dCmd.Parameters.AddWithValue("idConsumer", this.id);
                    return dCmd.ExecuteNonQuery();
                }catch(Exception e)
                {
                    throw e;
                }
                finally
                {
                    dCmd.Dispose();
                    connection.Close();
                    connection.Dispose();
                }*/
                dCmd.ExecuteNonQuery();
                //connection.Close();
                this.Close();
                //ListSupForm lF = new ListSupForm();
                //lF.Show();

            }
        }
    }
}
