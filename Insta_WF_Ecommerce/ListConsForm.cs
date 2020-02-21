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
    public partial class ListConsForm : Form
    {
        ConnectionStringSettings cnx = ConfigurationManager.ConnectionStrings["EcomProject"];

        public ListConsForm()
        {
            InitializeComponent();
        }

        public String getConsumerData()
        {
            String data = "SELECT* FROM Consumer";
            return data;

        }

        private void ListConsForm_Load(object sender, EventArgs e)
        {

            using (var connection = new SqlConnection(cnx.ConnectionString))
            {
                string queryString = getConsumerData();

                var command = new SqlCommand(queryString, connection);
                connection.Open();

                var reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    dGVConsumer.Visible = true;
                    DataTable dt = new DataTable();
                    dt.Load(reader);

                    dGVConsumer.DataSource = dt;
                }
            }


        }

        private void ouvrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            String idSupplier = dGVConsumer.CurrentRow.Cells[0].Value.ToString();
            int id = int.Parse(idSupplier);
            UpdateConsumerForm uCF = new UpdateConsumerForm(id);
            uCF.Show();

        }

        public String deleteConsumer(int id)
        {
            String value = $"DELETE FROM Consumer WHERE idConsumer = {id}";
            return value;
        }
        private void supprimerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            String idSupplier = dGVConsumer.CurrentRow.Cells[0].Value.ToString();
            int id = int.Parse(idSupplier);
            using (var connection = new SqlConnection(cnx.ConnectionString))
            {
                String queryString = deleteConsumer(id);
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
                }else if (result == DialogResult.No)
                {
                    MessageBox.Show(result.ToString());
                } else {
                    MessageBox.Show(result.ToString());
                }
                
                ListConsForm_Load(sender, e);

                //this.Update();
                //this.Refresh();
                //dGVConsumer.Update();
                //dGVConsumer.Refresh();
                //dGVConsumer.Invalidate();

            }
        }
    }
}
