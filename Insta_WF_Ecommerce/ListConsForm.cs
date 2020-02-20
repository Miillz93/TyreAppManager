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
            //MessageBox.Show(idSupplier);
            UpdateConsumerForm uCF = new UpdateConsumerForm(id);
            uCF.Show();

            //textbox1.Text = dataGridViewP.CurrentRow.Cells[0].Value.ToString();
            //textbox2.Text = dataGridViewP.CurrentRow.Cells[1].Value.ToString();
        }
    }
}
