using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;
using static Gestionnaire_des_Ordonnances.accueille;
using static Gestionnaire_des_Ordonnances.DataHandling;

namespace Gestionnaire_des_Ordonnances
{
    public partial class liste_controle : Form
    {
        DataHandling da = new DataHandling();
        BindingSource source = new BindingSource();

        public liste_controle()
        {
            InitializeComponent();
        }

        public static int idP;
        private void Form6_Load(object sender, EventArgs e)
        {
            nompatient.Text = s.ToUpper();
            DataTable table = new DataTable();
            da.idgrub(nompatient.Text, table);
            if (table != null && table.Rows.Count > 0)
            {
                idP = int.Parse(table.Rows[0][0].ToString());
                string query = "SELECT p.nom, c.idC, c.date_Controle, c.description, c.date_Controle_Suivant, c.analyse, c.traitement FROM patient p join controle c on p.idP = c.idP WHERE c.idP = @idP;";
                MySqlConnection mysqlCon = new MySqlConnection(connection);
                mysqlCon.Open();

                MySqlCommand cmd = new MySqlCommand(query, mysqlCon);
                cmd.Parameters.AddWithValue("@idP", idP);

                MySqlDataAdapter MyDA = new MySqlDataAdapter();
                MyDA.SelectCommand = cmd;

                DataTable table2 = new DataTable();
                MyDA.Fill(table2);

                BindingSource bSource = new BindingSource();
                bSource.DataSource = table2;

                dataGridView1.DataSource = bSource;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                dataGridView1.Height = 20 + dataGridView1.Rows.Count * 21;
                //da.Fill(dataGridView1, "SELECT p.nom, c.idC, c.date_Controle, c.description, c.date_Controle_Suivant, c.analyse, c.traitement FROM patient p join controle c on p.idP = c.idP WHERE c.idP = " + idP + ";");
                dataGridView1.Columns[1].Visible = false;
            }
        }

        private void ajouter_Click(object sender, EventArgs e)
        {
            Form f = new ajouter_controle();
            //this.Hide();
            f.ShowDialog();
            this.Show();
            string query = "SELECT p.nom, c.idC, c.date_Controle, c.description, c.date_Controle_Suivant, c.analyse, c.traitement FROM patient p join controle c on p.idP = c.idP WHERE c.idP = @idP;";
            MySqlConnection mysqlCon = new MySqlConnection(connection);
            mysqlCon.Open();

            MySqlCommand cmd = new MySqlCommand(query, mysqlCon);
            cmd.Parameters.AddWithValue("@idP", idP);

            MySqlDataAdapter MyDA = new MySqlDataAdapter();
            MyDA.SelectCommand = cmd;

            DataTable table2 = new DataTable();
            MyDA.Fill(table2);

            BindingSource bSource = new BindingSource();
            bSource.DataSource = table2;

            dataGridView1.DataSource = bSource;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView1.Height = 20 + dataGridView1.Rows.Count * 21;
            //da.Fill(dataGridView1, "SELECT p.nom, c.idC, c.date_Controle, c.description, c.date_Controle_Suivant, c.analyse, c.traitement FROM patient p join controle c on p.idP = c.idP WHERE c.idP = " + idP + ";");
            dataGridView1.Columns[1].Visible = false;
        }

        public static int idCont;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            idCont = int.Parse(dataGridView1.CurrentRow.Cells[1].Value.ToString());
            if (dataGridView1.CurrentCell.ColumnIndex == 5)
            {
                imprimer_analyse f = new imprimer_analyse();
                this.Hide();
                f.ShowDialog();
                this.Show();
            }
            if (dataGridView1.CurrentCell.ColumnIndex == 6)
            {
                imprimer f = new imprimer();
                this.Hide();
                f.ShowDialog();
                this.Show();
            }
        }
    }
}
