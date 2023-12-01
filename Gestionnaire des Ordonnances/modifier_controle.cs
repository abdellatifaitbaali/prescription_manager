using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Globalization;
using System.Windows.Forms;
using static Gestionnaire_des_Ordonnances.DataHandling;

namespace Gestionnaire_des_Ordonnances
{
    public partial class modifier_controle : Form
    {
        DataHandling da = new DataHandling();
        public modifier_controle()
        {
            InitializeComponent();
        }

        string idC, idP, nom;
        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                dataGridView1.Visible = true;
                string query = "SELECT c.idC, c.idP, p.nom, c.date_Controle, c.description, c.date_Controle_Suivant, c.analyse, c.traitement FROM patient p join controle c on p.idP = c.idP WHERE p.idP in (SELECT idP from patient where LOCATE(@searchTerm, cin) > 0 OR LOCATE(@searchTerm, nom) > 0);";
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
                dataGridView1.Columns[1].Visible = false;
            }
            else
            {
                dataGridView1.Visible = false;
                //da.Fill(dataGridView1, "SELECT p.nom, c.dateC, c.descr, c.dateNextC, c.trait as 'Traitement' FROM patient p join controle c on p.idP = c.idP;");
            }
        }

        private void Form9_Load(object sender, EventArgs e)
        {
            dataGridView1.Visible = false;
            //da.Fill(dataGridView1, "SELECT p.nom, c.dateC, c.descr, c.dateNextC, c.trait as 'Traitement' FROM patient p join controle c on p.idP = c.idP;");
            //dataGridView1.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            da.fillCombo("traitements", comboBox1);
            this.comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
            da.fillCombo("types", comboBox2);
            this.comboBox3.DropDownStyle = ComboBoxStyle.DropDownList;
            da.fillCombo("analyses", comboBox3);
        }

        private void ModifierP_Click(object sender, EventArgs e)
        {
            string trait = "", analyse = "";
            foreach(string item in listTraitement.Items)
            {
                trait += item + "/";
            }
            trait = trait.Remove(trait.Length - 1);

            if (listAnalyse.Items.Count > 0)
            {
                foreach (string item in listAnalyse.Items)
                {
                    analyse += item + "/";
                }
                analyse = analyse.Remove(analyse.Length - 1);
            }
            //MessageBox.Show(idC + " " + idP);

            string query = "UPDATE controle SET date_Controle = @date_Controle, description = @description, date_Controle_Suivant = @date_Controle_Suivant, analyse = @analyse, traitement = @traitement WHERE idC = @idC;";
            MySqlConnection databaseConnection = new MySqlConnection(connection);
            MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
            commandDatabase.Parameters.AddWithValue("@date_Controle", dateC.Value.ToString("d/M/yyyy"));
            commandDatabase.Parameters.AddWithValue("@description", descr.Text);
            commandDatabase.Parameters.AddWithValue("@date_Controle_Suivant", dateNextC.Value.ToString("d/M/yyyy"));
            commandDatabase.Parameters.AddWithValue("@traitement", trait);
            commandDatabase.Parameters.AddWithValue("@analyse", analyse);
            commandDatabase.Parameters.AddWithValue("@idC", idC);
            commandDatabase.CommandTimeout = 60;

            try
            {
                databaseConnection.Open();
                MySqlDataReader myReader = commandDatabase.ExecuteReader();
                MessageBox.Show("Contrôle Modifié avec succès");
                databaseConnection.Close();

                string qr = "SELECT c.idC, c.idP, p.nom, c.date_Controle, c.description, c.date_Controle_Suivant, c.analyse, c.traitement FROM patient p join controle c on p.idP = c.idP WHERE p.idP in (SELECT idP from patient where LOCATE( @nom, nom) > 0);";
                databaseConnection.Open();
                MySqlCommand cmd = new MySqlCommand(qr, databaseConnection);
                cmd.Parameters.AddWithValue("@nom", textBox1.Text);

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
                //da.Fill(dataGridView1, "");
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].Visible = false;
                //dataGridView1.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                databaseConnection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
            descr.Clear();
            listTraitement.Items.Clear();
            quantite.Clear();
            periode.Clear();
            duree.Clear();
            listAnalyse.Items.Clear();
        }

        private void SupprimerP_Click(object sender, EventArgs e)
        {
            string query = "DELETE FROM controle WHERE idC = @idC;";
            MySqlConnection databaseConnection = new MySqlConnection(connection);
            MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
            commandDatabase.Parameters.AddWithValue("@idC", idC);
            commandDatabase.CommandTimeout = 60;

            try
            {
                databaseConnection.Open();
                MySqlDataReader myReader = commandDatabase.ExecuteReader();
                databaseConnection.Close();
                MessageBox.Show("Contrôle Supprimé avec succès");
                string qr = "SELECT c.idC, c.idP, p.nom, c.date_Controle, c.description, c.date_Controle_Suivant, c.analyse, c.traitement FROM patient p join controle c on p.idP = c.idP WHERE p.idP in (SELECT idP from patient where LOCATE( @nom, nom) > 0);";
                databaseConnection.Open();
                MySqlCommand cmd = new MySqlCommand(qr, databaseConnection);
                cmd.Parameters.AddWithValue("@nom", textBox1.Text);

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
                //da.Fill(dataGridView1, "SELECT c.idC, c.idP, p.nom, c.date_Controle, c.description, c.date_Controle_Suivant, c.analyse, c.traitement FROM patient p join controle c on p.idP = c.idP WHERE p.idP in (SELECT idP from patient where LOCATE('" + textBox1.Text + "', nom) > 0);");
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].Visible = false;
                databaseConnection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            descr.Clear();
            listTraitement.Items.Clear();
            quantite.Clear();
            periode.Clear();
            duree.Clear();
            listAnalyse.Items.Clear();
        }

        int index;

        private void Button1_Click(object sender, EventArgs e)
        {
            string trait;
            if (quantite.Text == "")
            {
                trait = comboBox1.SelectedItem + " " + periode.Text + " " + comboBox2.SelectedItem + " par jour pour " + duree.Text + " jours ";
                listTraitement.Items.Add(trait);
            }

            if (quantite.Text != "" && periode.Text != "" && duree.Text != "")
            {
                trait = comboBox1.SelectedItem + " " + quantite.Text + " mg " + periode.Text + " " + comboBox2.SelectedItem + " par jour pour " + duree.Text + " jours ";
                listTraitement.Items.Add(trait);
            }

            comboBox1.Text = "";
            quantite.Clear();
            periode.Clear();
            duree.Clear();
        }

        private void ListTraitement_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listTraitement.SelectedIndex >= 0 && !listTraitement.SelectedItem.Equals(""))
            {
                index = listTraitement.SelectedIndex;
                //textBox2.Text = listTraitement.Items[index].ToString();
                if (listTraitement.Items[index].ToString().Split(char.Parse(" ")).Length < 10)
                {
                    comboBox1.Text = listTraitement.Items[index].ToString().Split(char.Parse(" "))[0];
                    quantite.Text = "";
                    periode.Text = listTraitement.Items[index].ToString().Split(char.Parse(" "))[1];
                    comboBox2.Text = listTraitement.Items[index].ToString().Split(char.Parse(" "))[2];
                    duree.Text = listTraitement.Items[index].ToString().Split(char.Parse(" "))[6];
                }
                else
                {
                    comboBox1.Text = listTraitement.Items[index].ToString().Split(char.Parse(" "))[0];
                    quantite.Text = listTraitement.Items[index].ToString().Split(char.Parse(" "))[1];
                    periode.Text = listTraitement.Items[index].ToString().Split(char.Parse(" "))[3];
                    comboBox2.Text = listTraitement.Items[index].ToString().Split(char.Parse(" "))[4];
                    duree.Text = listTraitement.Items[index].ToString().Split(char.Parse(" "))[8];
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (!listAnalyse.Items.Contains(comboBox3.SelectedItem))
            {
                listAnalyse.Items.Add(comboBox3.SelectedItem);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            listAnalyse.Items.Remove(listAnalyse.SelectedItem);
        }

        private void listAnalyse_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (listAnalyse.SelectedIndex >= 0 && !listAnalyse.SelectedItem.Equals(""))
            //{
            //    index = listAnalyse.SelectedIndex;
            //    //textBox3.Text = listAnalyse.Items[index].ToString();
            //    if (listAnalyse.Items.Count > 0)
            //    {
            //        comboBox3.Text = listTraitement.Items[index].ToString();
            //    }
            //}
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            listTraitement.Items.Clear();
            listAnalyse.Items.Clear();
            idC = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            idP = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            nom = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            DateTime datec;
            DateTime datenextc;
            DateTime.TryParseExact(dataGridView1.CurrentRow.Cells[3].Value.ToString(), formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out datec);
            dateC.Value = datec;
            descr.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            if (!dataGridView1.CurrentRow.Cells[5].Value.ToString().Equals(""))
            {
                DateTime.TryParseExact(dataGridView1.CurrentRow.Cells[5].Value.ToString(), formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out datenextc);
                dateNextC.Value = datenextc;
            }
            listAnalyse.Items.AddRange(dataGridView1.CurrentRow.Cells[6].Value.ToString().Split(char.Parse("/")));
            listAnalyse.Items.Remove("");
            listTraitement.Items.AddRange(dataGridView1.CurrentRow.Cells[7].Value.ToString().Split(char.Parse("/")));
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (listTraitement.SelectedIndex >= 0)
            {
                listTraitement.Focus();
                index = listTraitement.SelectedIndex;
                listTraitement.Items.RemoveAt(index);
                comboBox1.Text = "";
                quantite.Clear();
                periode.Clear();
                duree.Clear();
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            if (listTraitement.SelectedIndex >= 0)
            {
                listTraitement.Focus();
                if (quantite.Text == "")
                {
                    listTraitement.Items[index] = comboBox1.SelectedItem + " " + periode.Text + " " + comboBox2.SelectedItem + " par jour pour " + duree.Text + " jours ";
                }
                else
                {
                    listTraitement.Items[index] = comboBox1.SelectedItem + " " + quantite.Text + " mg " + periode.Text + " " + comboBox2.SelectedItem + " par jour pour " + duree.Text + " jours ";
                }
                comboBox1.Text = "";
                quantite.Clear();
                periode.Clear();
                duree.Clear();
            }
        }
    }
}
