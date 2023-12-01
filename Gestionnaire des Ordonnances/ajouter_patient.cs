using MySql.Data.MySqlClient;
using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using static Gestionnaire_des_Ordonnances.DataHandling;


namespace Gestionnaire_des_Ordonnances
{
    public partial class ajouter_patient : Form
    {
        DataHandling da = new DataHandling();
        public ajouter_patient()
        {
            InitializeComponent();
        }

        private void ajouter_Click(object sender, EventArgs e)
        {
            if (errorProvider1.GetError(nom).Length == 0 && errorProvider2.GetError(num).Length == 0 && errorProvider3.GetError(ville).Length == 0)
            {
                accueille f = new accueille();
                string query = "INSERT INTO patient(cin, nom, sexe, num, age, ville, mutuelle) VALUES ( @cin, @nom, @sexe, @num, @age, @ville, @mutuelle);";
                //da.execQuery(query);
                MySqlConnection databaseConnection = new MySqlConnection(connection);
                MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                commandDatabase.Parameters.AddWithValue("@cin", cin.Text);
                commandDatabase.Parameters.AddWithValue("@nom", nom.Text);
                commandDatabase.Parameters.AddWithValue("@sexe", sexe.SelectedItem.ToString());
                commandDatabase.Parameters.AddWithValue("@num", num.Text);
                commandDatabase.Parameters.AddWithValue("@age", age.Value.ToString());
                commandDatabase.Parameters.AddWithValue("@ville", ville.Text);
                commandDatabase.Parameters.AddWithValue("@mutuelle", mutuelle.Text);
                commandDatabase.CommandTimeout = 60;

                try
                {
                    databaseConnection.Open();
                    MySqlDataReader myReader = commandDatabase.ExecuteReader();

                    databaseConnection.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                MessageBox.Show("Patient Ajouté avec succès");
                f.label1.Text = "";
                //refrech = true;
                this.Close();
            }
            else
            {
                MessageBox.Show(this, "Veillez remplir tous les champs Correctement !");
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            sexe.Items.AddRange(new string[]{"Homme", "Femme"});
            sexe.Text = sexe.Items[0].ToString();
            this.mutuelle.DropDownStyle = ComboBoxStyle.DropDownList;
            try
            {
                connect();
                da.fillCombo("mutuelles", mutuelle);
            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message);
            }
            //errorProvider1.SetError(cin, "Champ Obligatoire !");
        }

        private void nom_TextChanged(object sender, EventArgs e)
        {
            if (nom.Text == "")
            {
                errorProvider1.SetError(nom, "Champ Obligatoire !");
            }
            else
            {
                errorProvider1.Clear();
            }
        }

        private void num_TextChanged(object sender, EventArgs e)
        {
            Regex reg = new Regex(@"^\d{10,12}$");
            if (!reg.IsMatch(num.Text))
            {
                errorProvider2.SetError(num, "Champ Obligatoire !");
            }
            else
            {
                errorProvider2.Clear();
            }
        }

        private void ville_TextChanged(object sender, EventArgs e)
        {
            if (ville.Text == "")
            {
                errorProvider3.SetError(ville, "Champ Obligatoire !");
            }
            else
            {
                errorProvider3.Clear();
            }
        }

        private void cin_TextChanged(object sender, EventArgs e)
        {
            //if (cin.Text == "")
            //{
            //    errorProvider1.SetError(cin, "Champ Obligatoire !");
            //}
            //else
            //{
            //    errorProvider1.Clear();
            //}
        }
    }
}
