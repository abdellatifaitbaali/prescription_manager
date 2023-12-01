using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;
using static Reception.DataHandling;

namespace Reception
{
    public partial class acceuille : Form
    {
        DataHandling da = new DataHandling();
        public acceuille()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //dataGridView1.Visible = false;
            dateTimePicker1.Enabled = false;
            dateTimePicker1.CustomFormat = "d/M/yyyy";
            createF();
            if (!checkCred())
            {
                link_database f5 = new link_database();
                this.Hide();
                f5.ShowDialog();
                this.Show();
            }
            else
            {
                try
                {
                    connect();
                    Rf();
                }
                catch(Exception E)
                {
                    MessageBox.Show(E.Message);
                }
                //MessageBox.Show(this.Controls.IndexOf(button2).ToString());
            }

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
            modifier_patient f = new modifier_patient();
            //this.Hide();
            f.ShowDialog();
            this.Show();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            p = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            nomp = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            ajouter_rendez_vous f = new ajouter_rendez_vous();
            //this.Hide();
            f.ShowDialog();
            this.Show();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked)
            {
                srch(dateTimePicker1.Value.ToString("d/M/yyyy"));
            }
            else
            {
                srch(DateTime.Now.Date.ToString("d/M/yyyy"));
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
        }

        private void dataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            s = dataGridView2.CurrentRow.Cells[8].Value.ToString();
            nomr = dataGridView2.CurrentRow.Cells[2].Value.ToString();
            rendez_vous f = new rendez_vous();
            //this.Hide();
            f.ShowDialog();
            this.Show();
        }

        private void Rf()
        {
            da.Fill(dataGridView1, "SELECT DISTINCT p.* FROM patient p JOIN controle c on p.idP = c.idP WHERE c.date_Controle_Suivant = DATE_FORMAT(CURDATE(),'%e/%c/%Y');");
            dataGridView1.Columns[0].Visible = false;
            da.Fill(dataGridView2, "SELECT p.*, r.* FROM rendezvous r JOIN patient p on r.idP = p.idP WHERE r.date_Rendez_Vous = DATE_FORMAT(CURDATE(),'%e/%c/%Y') ORDER BY r.Rendez_Vous;");
            dataGridView2.Columns[0].Visible = false;
            dataGridView2.Columns[3].Visible = false;
            dataGridView2.Columns[6].Visible = false;
            dataGridView2.Columns[8].Visible = false;
            dataGridView2.Columns[11].Visible = false;
        }

        private void label5_Click(object sender, EventArgs e)
        {
            Rf();
            if (checkBox1.Checked)
            {
                srch(dateTimePicker1.Value.ToString("d/M/yyyy"));
            }
            else
            {
                srch(DateTime.Now.Date.ToString("d/M/yyyy"));
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.Clear();
            if (checkBox1.Checked == true)
            {
                dateTimePicker1.Enabled = true;
                da.Fill(dataGridView1, "SELECT DISTINCT p.* FROM patient p JOIN controle c on p.idP = c.idP WHERE c.date_Controle_Suivant = '" + dateTimePicker1.Value.ToString("d/M/yyyy") + "';");
                dataGridView1.Columns[0].Visible = false;
                da.Fill(dataGridView2, "SELECT p.*, r.* FROM rendezvous r JOIN patient p on r.idP = p.idP WHERE r.date_Rendez_Vous = '" + dateTimePicker1.Value.ToString("d/M/yyyy") + "';");
                dataGridView2.Columns[0].Visible = false;
                dataGridView2.Columns[3].Visible = false;
                dataGridView2.Columns[6].Visible = false;
                //dataGridView2.Columns[7].Visible = false;
                dataGridView2.Columns[9].Visible = false;
            }
            else
            {
                dateTimePicker1.Enabled = false;
                da.Fill(dataGridView1, "SELECT DISTINCT p.* FROM patient p JOIN controle c on p.idP = c.idP WHERE c.date_Controle_Suivant = DATE_FORMAT(CURDATE(),'%e/%c/%Y');");
                dataGridView1.Columns[0].Visible = false;
                da.Fill(dataGridView2, "SELECT p.*, r.* FROM rendezvous r JOIN patient p on r.idP = p.idP WHERE r.date_Rendez_Vous = DATE_FORMAT(CURDATE(),'%e/%c/%Y') ORDER BY r.Rendez_Vous;");
                dataGridView2.Columns[0].Visible = false;
                dataGridView2.Columns[3].Visible = false;
                dataGridView2.Columns[6].Visible = false;
                //dataGridView2.Columns[7].Visible = false;
                dataGridView2.Columns[9].Visible = false;
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            da.Fill(dataGridView1, "SELECT DISTINCT p.* FROM patient p JOIN controle c on p.idP = c.idP WHERE c.date_Controle_Suivant = '" + dateTimePicker1.Value.ToString("d/M/yyyy") + "';");
            dataGridView1.Columns[0].Visible = false;
            da.Fill(dataGridView2, "SELECT p.*, r.* FROM rendezvous r JOIN patient p on r.idP = p.idP WHERE r.date_Rendez_Vous = '" + dateTimePicker1.Value.ToString("d/M/yyyy") + "';");
            dataGridView2.Columns[0].Visible = false;
            dataGridView2.Columns[3].Visible = false;
            dataGridView2.Columns[6].Visible = false;
            //dataGridView2.Columns[7].Visible = false;
            dataGridView2.Columns[9].Visible = false;
        }

        private void srch(string sDate)
        {
            if (textBox1.Text != "")
            {
                //dataGridView1.Visible = true;
                string query = "SELECT * from patient where LOCATE(@searchTerm, cin) > 0 OR LOCATE(@searchTerm, nom) > 0;";
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
                mysqlCon.Close();
                //da.Fill(dataGridView1, query);
                dataGridView1.Columns[0].Visible = false;
                string query2 = "SELECT p.*, r.* FROM rendezvous r JOIN patient p on r.idP = p.idP WHERE (LOCATE(@searchTerm, p.cin) > 0 OR LOCATE(@searchTerm, nom) > 0) AND r.date_Rendez_Vous = @sDate ORDER BY r.Rendez_Vous;";
                mysqlCon.Open();

                cmd = new MySqlCommand(query2, mysqlCon);
                cmd.Parameters.AddWithValue("@searchTerm", textBox1.Text);
                cmd.Parameters.AddWithValue("@sDate", sDate);

                MyDA = new MySqlDataAdapter();
                MyDA.SelectCommand = cmd;

                table = new DataTable();
                MyDA.Fill(table);

                bSource = new BindingSource();
                bSource.DataSource = table;

                dataGridView2.DataSource = bSource;
                dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridView2.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                dataGridView2.Height = 20 + dataGridView2.Rows.Count * 21;
                mysqlCon.Close();
                //da.Fill(dataGridView2, "SELECT p.*, r.* FROM rendezvous r JOIN patient p on r.idP = p.idP WHERE (LOCATE(@searchTerm, p.cin) > 0 OR LOCATE(@searchTerm, nom) > 0) AND r.date_Rendez_Vous = @sDate ORDER BY r.Rendez_Vous;");
                dataGridView2.Columns[0].Visible = false;
                dataGridView2.Columns[3].Visible = false;
                dataGridView2.Columns[6].Visible = false;
                //dataGridView2.Columns[7].Visible = false;
                dataGridView2.Columns[9].Visible = false;
            }
            else
            {
                Rf();
            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            link_database f = new link_database();
            //this.Hide();
            f.ShowDialog();
            this.Show();
        }
    }
}
