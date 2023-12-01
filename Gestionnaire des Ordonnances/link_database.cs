using MySql.Data.MySqlClient;
using System;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using static Gestionnaire_des_Ordonnances.DataHandling;

namespace Gestionnaire_des_Ordonnances
{
    public partial class link_database : Form
    {
        DataHandling da = new DataHandling();
        public link_database()
        {
            InitializeComponent();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            if (checkCred())
            {
                try
                {
                    connect();
                    label6.ForeColor = Color.Green;
                    label6.Text = "Connecté";
                    string[] all = reader().Split(char.Parse("/"));
                    textBox3.Text = all[0];
                    textBox4.Text = all[1];
                    textBox1.Text = all[2];
                    textBox2.Text = Decode(all[3]);
                    textBox5.Text = all[4];
                }
                catch
                {
                    MessageBox.Show("Invalid Host Address or port is closed !!!");
                    label6.ForeColor = Color.Red;
                    label6.Text = "Non Connecté";
                }
            }
            else
            {
                connection = "datasource = " + textBox3.Text + "; port = " + textBox4.Text + "; username = " + textBox1.Text + "; password =" + textBox2.Text + "; database = " + textBox5.Text + ";";
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if(errorProvider1.GetError(textBox3).Length == 0 && errorProvider2.GetError(textBox4).Length == 0)
            {
                writer(textBox3.Text, textBox4.Text, textBox1.Text, textBox2.Text, textBox5.Text);
                try
                {
                    connect();
                    label6.ForeColor = Color.Green;
                    label6.Text = "Connecté";
                }
                catch (Exception E)
                {
                    MessageBox.Show(E.Message);
                    label6.ForeColor = Color.Red;
                    label6.Text = "Non Connecté";
                }
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            Regex reg = new Regex(@"\b\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}\b");
            if (!reg.IsMatch(textBox3.Text))
            {
                errorProvider1.SetError(textBox3, "Invalide Format");
            }
            else
            {
                errorProvider1.Clear();
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            Regex reg = new Regex(@"\b\d{1,5}\b");
            if (!reg.IsMatch(textBox4.Text))
            {
                errorProvider2.SetError(textBox4, "Invalide Format");
            }
            else
            {
                errorProvider2.Clear();
            }
        }

        private void textBox2_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("Click to view the password", textBox2);
        }

        private void textBox2_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBox2.UseSystemPasswordChar == true)
            {
                textBox2.UseSystemPasswordChar = false;
            }
            else
            {
                textBox2.UseSystemPasswordChar = true;
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            textBox2.UseSystemPasswordChar = true;
        }

        private void import_Click(object sender, EventArgs e)
        {
            Restore();
        }

        private void export_Click(object sender, EventArgs e)
        {
            Backup();
        }

        private void Backup()
        {
            try
            {
                string file = path + @"\backup.sql";
                using (MySqlConnection conn = new MySqlConnection(connection))
                {
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        using (MySqlBackup mb = new MySqlBackup(cmd))
                        {
                            cmd.Connection = conn;
                            conn.Open();
                            mb.ExportToFile(file);
                            conn.Close();
                        }
                    }
                }
                MessageBox.Show("Database Exported Successfully!");
            }
            catch(Exception E)
            {
                MessageBox.Show(E.Message);
            }
        }

        private void Restore()
        {
            try
            {
                string file = path + @"\backup.sql";
                using (MySqlConnection conn = new MySqlConnection(connection))
                {
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        using (MySqlBackup mb = new MySqlBackup(cmd))
                        {
                            cmd.Connection = conn;
                            conn.Open();
                            mb.ImportFromFile(file);
                            conn.Close();
                        }
                    }
                }
                MessageBox.Show("Database Filled Successfully!");
            }
            catch(Exception E)
            {
                MessageBox.Show(E.Message);
            }
        }
    }
}
