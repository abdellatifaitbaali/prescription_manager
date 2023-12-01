using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using static Reception.DataHandling;

namespace Reception
{
    public partial class ajouter_rendez_vous : Form
    {
        DataHandling da = new DataHandling();
        public ajouter_rendez_vous()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.Items.Count > 0)
            {
                string query = "INSERT INTO rendezvous(date_Rendez_Vous, Rendez_Vous, idP) VALUES ('" + dateTimePicker1.Value.ToString("d/M/yyyy") + "', '" + comboBox1.SelectedItem + "', @idP);";
                MySqlConnection databaseConnection = new MySqlConnection(connection);
                MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                commandDatabase.Parameters.AddWithValue("@idP", int.Parse(p));
                commandDatabase.CommandTimeout = 60;

                try
                {
                    databaseConnection.Open();
                    MySqlDataReader myReader = commandDatabase.ExecuteReader();

                    databaseConnection.Close();
                    comboBox1.Items.Remove(comboBox1.SelectedItem);
                    //da.execQuery(query);
                    //MessageBox.Show("Rendez-Vous Ajouté avec succès");
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Rendez-vous Indisponible! ");
            }
        }

        List<string> tlist = times;
        private void ajouter_rendez_vous_Load(object sender, EventArgs e)
        {
            try
            {
                comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
                dateTimePicker1.CustomFormat = "d/M/yyyy";
                label2.Text = nomp.ToUpper();
                ComboBox tmp = new ComboBox();
                da.fillTime(tmp, dateTimePicker1.Value.ToString(("d/M/yyyy")));
                foreach (string item in tlist)
                {
                    if (!tmp.Items.Contains(item))
                    {
                        comboBox1.Items.Add(item);
                    }
                }

                comboBox1.Text = comboBox1.Items[0].ToString();
            }
            catch(Exception E)
            {
                MessageBox.Show(E.Message);
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            comboBox1.Enabled = true;
            comboBox1.Items.Clear();
            ComboBox tmp = new ComboBox();
            da.fillTime(tmp, dateTimePicker1.Value.ToString("d/M/yyyy"));
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
                //MessageBox.Show("Rendez-vous Indisponible! ");
                comboBox1.Enabled = false;
            }
        }
    }
}
