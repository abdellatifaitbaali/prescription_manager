using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;
using static Gestionnaire_des_Ordonnances.DataHandling;

namespace Gestionnaire_des_Ordonnances
{
    public partial class modifier_patient : Form
    {
        DataHandling da = new DataHandling();
        public modifier_patient()
        {
            InitializeComponent();
        }

        private void Form7_Load(object sender, EventArgs e)
        {
            sexe.Items.AddRange(new string[] { "Homme", "Femme" });
            sexe.Text = sexe.Items[0].ToString();
            this.mutuelle.DropDownStyle = ComboBoxStyle.DropDownList;
            dataGridView1.Visible = false;
            try
            {
                connect();
                MySqlConnection mysqlCon = new MySqlConnection(connection);
                mysqlCon.Open();
                //da.Fill(dataGridView1, "SELECT * FROM patient;");
                //dataGridView1.Columns[0].Visible = false;
                da.fillCombo("mutuelles", mutuelle);
            }
            catch(Exception E)
            {
                MessageBox.Show(E.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                dataGridView1.Visible = true;
                try
                {
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
                }
                catch (Exception E)
                {
                    MessageBox.Show(E.Message);
                }
                //da.Fill(dataGridView1, query);
                dataGridView1.Columns[0].Visible = false;
            }
            else
            {
                dataGridView1.Visible = false;
            }
        }

        string idP;
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            idP = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            cin.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            nom.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            sexe.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            num.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            age.Value = int.Parse(dataGridView1.CurrentRow.Cells[5].Value.ToString());
            ville.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            mutuelle.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
        }

        private void modifierP_Click(object sender, EventArgs e)
        {
            string query = "UPDATE patient SET cin = @cin, nom = @nom, sexe = @sexe, num = @num, age = @age, ville = @ville, mutuelle = @mutuelle WHERE idP = @idP;";
            MySqlConnection databaseConnection = new MySqlConnection(connection);
            MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
            commandDatabase.Parameters.AddWithValue("@cin", cin.Text);
            commandDatabase.Parameters.AddWithValue("@nom", nom.Text);
            commandDatabase.Parameters.AddWithValue("@sexe", sexe.SelectedItem.ToString());
            commandDatabase.Parameters.AddWithValue("@num", num.Text);
            commandDatabase.Parameters.AddWithValue("@age", age.Value.ToString());
            commandDatabase.Parameters.AddWithValue("@ville", ville.Text);
            commandDatabase.Parameters.AddWithValue("@mutuelle", mutuelle.Text);
            commandDatabase.Parameters.AddWithValue("@idP", idP);
            commandDatabase.CommandTimeout = 60;

            try
            {
                databaseConnection.Open();
                MySqlDataReader myReader = commandDatabase.ExecuteReader();

                MessageBox.Show("Patient Modifié avec succès");
                da.Fill(dataGridView1, "SELECT * FROM patient;");
                refrech = true;
                databaseConnection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void supprimerP_Click(object sender, EventArgs e)
        {
            string query = "DELETE FROM patient WHERE idP = @idP;";
            string query2 = "DELETE FROM controle WHERE idP = @idP;";
            string query3 = "DELETE FROM rendezvous WHERE idP = @idP;";
            MySqlConnection databaseConnection = new MySqlConnection(connection);
            MySqlCommand commandDatabase = new MySqlCommand();
            commandDatabase.CommandTimeout = 60;
            MySqlDataReader myReader;

            try
            {
                databaseConnection.Open();
                commandDatabase = new MySqlCommand(query2, databaseConnection);
                commandDatabase.Parameters.AddWithValue("@idP", idP);
                myReader = commandDatabase.ExecuteReader();
                databaseConnection.Close();

                databaseConnection.Open();
                commandDatabase = new MySqlCommand(query3, databaseConnection);
                commandDatabase.Parameters.AddWithValue("@idP", idP);
                myReader = commandDatabase.ExecuteReader();
                databaseConnection.Close();

                databaseConnection.Open();
                commandDatabase = new MySqlCommand(query, databaseConnection);
                commandDatabase.Parameters.AddWithValue("@idP", idP);
                myReader = commandDatabase.ExecuteReader();
                databaseConnection.Close();

                databaseConnection.Open();
                MessageBox.Show("Patient Supprimé avec succès");
                da.Fill(dataGridView1, "SELECT * FROM patient;");
                refrech = true;
                databaseConnection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
