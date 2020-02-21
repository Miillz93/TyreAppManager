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
    public partial class ListSupForm : Form
    {

        ConnectionStringSettings cnx = ConfigurationManager.ConnectionStrings["EcomProject"];
        public ListSupForm()
        {
            InitializeComponent();
        }
        public String getSupplierData()
        {
            String values = "SELECT * FROM Supplier";
            return values;
        }

        private void ListSupForm_Load(object sender, EventArgs e)
        {

            using (var connection = new SqlConnection(cnx.ConnectionString))
            {
                string queryString = getSupplierData();

                var command = new SqlCommand(queryString, connection);
                connection.Open();

                var reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    dGVSupplier.Visible = true;
                    DataTable dt = new DataTable();
                    dt.Load(reader);

                    dGVSupplier.DataSource = dt;
                }

            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            String idSupplier = dGVSupplier.CurrentRow.Cells[0].Value.ToString();
            int id = int.Parse(idSupplier);
            UpdateSupplierForm uCF = new UpdateSupplierForm(id);
            uCF.Show();
        }

        public String deleteSupplier(int id)
        {
            String value = $"DELETE FROM Supplier WHERE IdSupplier = {id}";
            return value;
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            String idSupplier = dGVSupplier.CurrentRow.Cells[0].Value.ToString();
            int id = int.Parse(idSupplier);
            using (var connection = new SqlConnection(cnx.ConnectionString))
            {
                String queryString = deleteSupplier(id);
                connection.Open();
                var dCmd = new SqlCommand(queryString, connection);
                dCmd.CommandType = CommandType.Text;

                DialogResult result = MessageBox.Show("Mon Message ?", "Important ", MessageBoxButtons.YesNoCancel,
MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    dCmd.ExecuteNonQuery();
                    connection.Close();
                    //MessageBox.Show(result.ToString());
                }
                else if (result == DialogResult.No)
                {
                    MessageBox.Show(result.ToString());
                }
                else
                {
                    MessageBox.Show(result.ToString());
                }

                ListSupForm_Load(sender, e);

                //this.Update();
                //this.Refresh();
                //dGVConsumer.Update();
                //dGVConsumer.Refresh();
                //dGVConsumer.Invalidate();
            }
        }
    }
}
