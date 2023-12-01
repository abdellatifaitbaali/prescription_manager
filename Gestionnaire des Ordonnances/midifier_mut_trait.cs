using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;
using static Gestionnaire_des_Ordonnances.DataHandling;

namespace Gestionnaire_des_Ordonnances
{
    public partial class midifier_mut_trait : Form
    {
        DataHandling da = new DataHandling();
        public midifier_mut_trait()
        {
            InitializeComponent();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            this.comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            this.comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
            this.comboBox3.DropDownStyle = ComboBoxStyle.DropDownList;
            this.comboBox4.DropDownStyle = ComboBoxStyle.DropDownList;
            connect();
            try
            {
                MySqlConnection mysqlCon = new MySqlConnection(connection);
                mysqlCon.Open();
                da.fillCombo("mutuelles", comboBox1);
                comboBox1.Text = comboBox1.Items[0].ToString();
                da.fillCombo("traitements", comboBox2);
                comboBox2.Text = comboBox2.Items[0].ToString();
                da.fillCombo("types", comboBox3);
                comboBox3.Text = comboBox3.Items[0].ToString();
                da.fillCombo("analyses", comboBox4);
                comboBox4.Text = comboBox4.Items[0].ToString();
            }
            catch
            {
                MessageBox.Show("Database Is empty !");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(!textBox1.Text.Equals(""))
            {
                string query = "INSERT INTO mutuelles VALUES (@val);";
                MySqlConnection databaseConnection = new MySqlConnection(connection);
                MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                commandDatabase.Parameters.AddWithValue("@val", textBox1.Text);
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
                //da.execQuery(query);
                //MessageBox.Show("Mutuelle Ajouté avec succès");
                da.fillCombo("mutuelles", comboBox1);
                comboBox1.Text = textBox1.Text;
                textBox1.Clear();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!textBox2.Text.Equals(""))
            {
                string query = "INSERT INTO traitements VALUES (@val);";
                MySqlConnection databaseConnection = new MySqlConnection(connection);
                MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                commandDatabase.Parameters.AddWithValue("@val", textBox2.Text);
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
                //da.execQuery(query);
                //MessageBox.Show("Traitement Ajouté avec succès");
                da.fillCombo("traitements", comboBox2);
                comboBox2.Text = textBox2.Text;
                textBox2.Clear();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!textBox3.Text.Equals(""))
            {
                string query = "INSERT INTO types VALUES (@val);";
                MySqlConnection databaseConnection = new MySqlConnection(connection);
                MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                commandDatabase.Parameters.AddWithValue("@val", textBox3.Text);
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
                //da.execQuery(query);
                //MessageBox.Show("Type Ajouté avec succès");
                da.fillCombo("types", comboBox3);
                comboBox3.Text = textBox3.Text;
                textBox3.Clear();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string query = "DELETE FROM mutuelles WHERE mutuelle = @val;";
            MySqlConnection databaseConnection = new MySqlConnection(connection);
            MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
            commandDatabase.Parameters.AddWithValue("@val", comboBox1.SelectedItem);
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
            //da.execQuery(query);
            //MessageBox.Show("Mutuelle Supprimer avec succès");
            da.fillCombo("mutuelles", comboBox1);
            if (comboBox1.Items.Count > 0) comboBox1.Text = comboBox1.Items[0].ToString();
            else comboBox1.Text = "";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string query = "DELETE FROM traitements WHERE traitement = @val;";
            MySqlConnection databaseConnection = new MySqlConnection(connection);
            MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
            commandDatabase.Parameters.AddWithValue("@val", comboBox2.SelectedItem);
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
            //da.execQuery(query);
            //MessageBox.Show("Traitement Supprimer avec succès");
            da.fillCombo("traitements", comboBox2);
            if (comboBox2.Items.Count > 0) comboBox2.Text = comboBox2.Items[0].ToString();
            else comboBox2.Text = "";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string query = "DELETE FROM types WHERE type = @val;";
            MySqlConnection databaseConnection = new MySqlConnection(connection);
            MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
            commandDatabase.Parameters.AddWithValue("@val", comboBox3.SelectedItem);
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
            //da.execQuery(query);
            //MessageBox.Show("Type Supprimer avec succès");
            da.fillCombo("types", comboBox3);
            if (comboBox3.Items.Count > 0) comboBox3.Text = comboBox3.Items[0].ToString();
            else comboBox3.Text = "";
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (!textBox4.Text.Equals(""))
            {
                string query = "INSERT INTO analyses VALUES (@val);";
                MySqlConnection databaseConnection = new MySqlConnection(connection);
                MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                commandDatabase.Parameters.AddWithValue("@val", textBox4.Text);
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
                //da.execQuery(query);
                //MessageBox.Show("Analyse Ajouté avec succès");
                da.fillCombo("analyses", comboBox4);
                comboBox4.Text = textBox4.Text;
                textBox4.Clear();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string query = "DELETE FROM analyses WHERE analyse = @val;";
            MySqlConnection databaseConnection = new MySqlConnection(connection);
            MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
            commandDatabase.Parameters.AddWithValue("@val", comboBox4.SelectedItem);
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
            //da.execQuery(query);
            //MessageBox.Show("Analyse Supprimer avec succès");
            da.fillCombo("analyses", comboBox4);
            if (comboBox4.Items.Count > 0) comboBox4.Text = comboBox4.Items[0].ToString();
            else comboBox4.Text = "";
        }
    }
}
