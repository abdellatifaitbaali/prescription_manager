using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;
using static Gestionnaire_des_Ordonnances.accueille;
using static Gestionnaire_des_Ordonnances.DataHandling;

namespace Gestionnaire_des_Ordonnances
{
    public partial class ajouter_controle : Form
    {
        public string np = s;
        DataHandling da = new DataHandling();

        public ajouter_controle()
        {
            InitializeComponent();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            textBox2.Enabled = true;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            textBox2.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string trait;
            if (quantite.Text == "" && periode.Text == "" && duree.Text == "")
            {
                trait = comboBox1.SelectedItem.ToString();
                listTraitement.Items.Add(trait);
            }

            if (quantite.Text == "" && periode.Text != "" && duree.Text != "")
            {
                trait = comboBox1.SelectedItem + " " + periode.Text + " " + comboBox2.SelectedItem + " par jour pour " + duree.Text + " jours ";
                listTraitement.Items.Add(trait);
            }

            if (quantite.Text != "" && periode.Text != "" && duree.Text != "")
            {
                trait = comboBox1.SelectedItem + " " + quantite.Text + " mg " + periode.Text + " " + comboBox2.SelectedItem +" par jour pour " + duree.Text + " jours ";
                listTraitement.Items.Add(trait);
            }
            
            comboBox1.Text = "";
            quantite.Clear();
            periode.Clear();
            duree.Clear();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            nompatient.Text = s.ToUpper();
            this.comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            da.fillCombo("traitements", comboBox1);
            this.comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
            da.fillCombo("types", comboBox2);
            this.comboBox3.DropDownStyle = ComboBoxStyle.DropDownList;
            da.fillCombo("analyses", comboBox3);
            if (comboBox1.Items.Count > 0) comboBox1.Text = comboBox1.Items[0].ToString();
            if (comboBox2.Items.Count > 0) comboBox2.Text = comboBox2.Items[0].ToString();
            if (comboBox3.Items.Count > 0) comboBox3.Text = comboBox3.Items[0].ToString();
            textBox2.Enabled = false;
            comboBox3.Enabled = false;
            button2.Enabled = false;
        }

        private void ajouter_Click(object sender, EventArgs e)
        {
            string datenextcont = "", traitement = "", analyse = "", idP = ""; 
            if (radioButton1.Checked)
            {
                datenextcont = textBox2.Value.ToString("d/M/yyyy"); 
            }
            else
            {
                datenextcont = "";
            }
            if(listTraitement.Items.Count == 0)
            {
                MessageBox.Show("La liste des traitements et vide, Veillez la remplir !!!");
            }
            else
            {
                foreach(string item in listTraitement.Items)
                {
                    traitement += item + "/";
                }
                traitement = traitement.Remove(traitement.Length - 1);
            }
            if (listAnalyse.Items.Count > 0)
            {
                foreach (string item in listAnalyse.Items)
                {
                    analyse += item + "/";
                }
                analyse = analyse.Remove(analyse.Length - 1);
            }

            DataTable table = new DataTable();
            da.idgrub(nompatient.Text, table);
            idP = table.Rows[0][0].ToString();

            string query = "INSERT INTO controle(date_Controle, description, date_Controle_Suivant, traitement, analyse, idP) VALUES ( @date_Controle, @description, @date_Controle_Suivant, @traitement, @analyse, @idP);";
            MySqlConnection databaseConnection = new MySqlConnection(connection);
            MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
            commandDatabase.Parameters.AddWithValue("@date_Controle", dateTimePicker1.Value.ToString("d/M/yyyy"));
            commandDatabase.Parameters.AddWithValue("@description", textBox3.Text);
            commandDatabase.Parameters.AddWithValue("@date_Controle_Suivant", datenextcont);
            commandDatabase.Parameters.AddWithValue("@traitement", traitement);
            commandDatabase.Parameters.AddWithValue("@analyse", analyse);
            commandDatabase.Parameters.AddWithValue("@idP", int.Parse(idP));
            commandDatabase.CommandTimeout = 60;

            if (textBox3.Text != "" && traitement != "")
            {
                try
                {
                    databaseConnection.Open();
                    MySqlDataReader myReader = commandDatabase.ExecuteReader();
                    MessageBox.Show("Contrôle ajouté avec succès");
                    databaseConnection.Close();
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Tous les champs sont obligatoires !");
            }
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(!listAnalyse.Items.Contains(comboBox3.SelectedItem))
            {
                listAnalyse.Items.Add(comboBox3.SelectedItem);
            }
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            comboBox3.Enabled = true;
            listAnalyse.Enabled = true;
            button2.Enabled = true;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            comboBox3.Enabled = false;
            listAnalyse.Enabled = false;
            button2.Enabled = false;
        }
    }
}
