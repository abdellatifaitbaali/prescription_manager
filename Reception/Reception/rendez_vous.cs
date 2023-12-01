using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using static Reception.DataHandling;

namespace Reception
{
    public partial class rendez_vous : Form
    {
        DataHandling da = new DataHandling();
        public rendez_vous()
        {
            InitializeComponent();
        }

        List<string> tlist = times;
        private void rendez_vous_Load(object sender, EventArgs e)
        {
            label2.Text = nomr.ToUpper();
            dateTimePicker1.CustomFormat = "d/M/yyyy";
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            ComboBox tmp = new ComboBox();
            da.fillTime(tmp, dateTimePicker1.Value.ToString(("d/M/yyyy")));
            foreach (string item in tlist)
            {
                if (!tmp.Items.Contains(item))
                {
                    comboBox1.Items.Add(item);
                }
            }

            try
            {
                comboBox1.Text = comboBox1.Items[0].ToString();
            }
            catch
            {
                MessageBox.Show("Rendez-vous Indisponible! ");
            }
        }

        private void modifierR_Click(object sender, EventArgs e)
        {
            string query = "UPDATE rendezvous SET date_Rendez_Vous = '" + dateTimePicker1.Value.ToString("d/M/yyyy") + "', Rendez_Vous = '" + comboBox1.SelectedItem + "' WHERE idR = @idR;";
            MySqlConnection databaseConnection = new MySqlConnection(connection);
            MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
            commandDatabase.Parameters.AddWithValue("@idR", int.Parse(s));
            commandDatabase.CommandTimeout = 60;

            try
            {
                databaseConnection.Open();
                MySqlDataReader myReader = commandDatabase.ExecuteReader();

                MessageBox.Show("Rendez-Vous Modifié avec succès");
                databaseConnection.Close();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void supprimerR_Click(object sender, EventArgs e)
        {
            string query = "DELETE FROM rendezvous WHERE idR = @idR;";
            MySqlConnection databaseConnection = new MySqlConnection(connection);
            MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
            commandDatabase.Parameters.AddWithValue("@idR", int.Parse(s));
            commandDatabase.CommandTimeout = 60;

            try
            {
                databaseConnection.Open();
                MySqlDataReader myReader = commandDatabase.ExecuteReader();

                MessageBox.Show("Rendez-Vous Supprimé avec succès");
                databaseConnection.Close();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            ComboBox tmp = new ComboBox();
            da.fillTime(tmp, dateTimePicker1.Value.ToString(("d/M/yyyy")));
            foreach (string item in tlist)
            {
                if (!tmp.Items.Contains(item))
                {
                    comboBox1.Items.Add(item);
                }
            }

            try
            {
                comboBox1.Text = comboBox1.Items[0].ToString();
            }
            catch
            {
                MessageBox.Show("Rendez-vous Indisponible! ");
            }
        }
    }
}
