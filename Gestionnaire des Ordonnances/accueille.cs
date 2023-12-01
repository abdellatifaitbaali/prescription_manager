using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;
using static Gestionnaire_des_Ordonnances.DataHandling;

namespace Gestionnaire_des_Ordonnances
{
    public partial class accueille : Form
    {
        DataHandling da = new DataHandling();
        public static string s;
        public static BindingSource source = new BindingSource();

        public accueille()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.Visible = false;
            createF();
            if (!checkCred())
            {
                link_database f5 = new link_database();
                this.Hide();
                f5.ShowDialog();
                this.Show();
            }
            //else
            //{
            //    da.Fill(dataGridView1, "SELECT * FROM patient;");
            //    //MessageBox.Show(this.Controls.IndexOf(button2).ToString());
            //}

            //da.Fill(dataGridView1, "SELECT * FROM patient;");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ajouter_patient f = new ajouter_patient();
            //this.Hide();
            f.ShowDialog();
            this.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ajouter_controle f = new ajouter_controle();
            //this.Hide();
            f.ShowDialog();
            this.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            link_database f = new link_database();
            //this.Hide();
            f.ShowDialog();
            this.Show();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if(textBox1.Text != "")
            {
                dataGridView1.Visible = true;
                //string query = "SELECT * from patient where LOCATE('" + textBox1.Text + "', cin) > 0 OR LOCATE('" + textBox1.Text + "', nom) > 0;";
                try
                {
                    string query = "SELECT * from patient where LOCATE(@searchTerm, cin) > 0 OR LOCATE(@searchTerm, nom) > 0;";
                    connect();
                    MySqlConnection mysqlCon = new MySqlConnection(connection);
                    mysqlCon.Open();

                    MySqlCommand cmd = new MySqlCommand(query, mysqlCon);
                    cmd.Parameters.AddWithValue("@searchTerm", textBox1.Text);

                    MySqlDataAdapter MyDA = new MySqlDataAdapter();
                    MyDA.SelectCommand = cmd;

                    DataTable table = new DataTable();
                    MyDA.Fill(table);

                    BindingSource bSource = new BindingSource();
                    bSource.DataSource = table;

                    dataGridView1.DataSource = bSource;
                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                    dataGridView1.Height = 20 + dataGridView1.Rows.Count * 21;
                }
                catch(Exception E)
                {
                    MessageBox.Show(E.Message);
                }
                //da.Fill(dataGridView1, query);
                dataGridView1.Columns[0].Visible = false;
            }
            else
            {
                //da.Fill(dataGridView1, "SELECT * FROM patient;");
                dataGridView1.Visible = false;
            }
        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            s = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            Form f = new liste_controle();
            
            //this.Hide();
            f.ShowDialog();
            this.Show();
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            this.Refresh();
            da.Fill(dataGridView1, "SELECT * FROM patient;");
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            edit_data f = new edit_data();
            //this.Hide();
            f.ShowDialog();
            this.Show();
        }

        private void accueille_Enter(object sender, EventArgs e)
        {
            if (refrech == true)
            {
                da.Fill(dataGridView1, "SELECT * FROM patient;");
                refrech = false;
            }
        }

        private void accueille_MouseEnter(object sender, EventArgs e)
        {
            if (refrech == true)
            {
                da.Fill(dataGridView1, "SELECT * FROM patient;");
                refrech = false;
            }
        }

        private void dataGridView1_Enter(object sender, EventArgs e)
        {
            if (refrech == true)
            {
                da.Fill(dataGridView1, "SELECT * FROM patient;");
                refrech = false;
            }
        }

        private void dataGridView1_MouseEnter(object sender, EventArgs e)
        {
            if (refrech == true)
            {
                da.Fill(dataGridView1, "SELECT * FROM patient;");
                refrech = false;
            }
        }
    }
}
