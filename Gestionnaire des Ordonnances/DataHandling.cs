using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.IO;
using static Gestionnaire_des_Ordonnances.link_database;

namespace Gestionnaire_des_Ordonnances
{
    public class DataHandling
    {
        public static string[] formats = {"d/M/yyyy", "d-M-yyyy"};
        public static string connection;
        public static bool refrech = false;
        public void Fill(DataGridView dgv, string sqlCom)
        {
            try
            {
                connect();
                MySqlConnection mysqlCon = new MySqlConnection(connection);
                mysqlCon.Open();

                MySqlDataAdapter MyDA = new MySqlDataAdapter();
                MyDA.SelectCommand = new MySqlCommand(sqlCom, mysqlCon);

                DataTable table = new DataTable();
                MyDA.Fill(table);

                BindingSource bSource = new BindingSource();
                bSource.DataSource = table;

                dgv.DataSource = bSource;
                dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                dgv.Height = 20 + dgv.Rows.Count * 21;
            }
            catch
            {
                MessageBox.Show("Veillez Connecter la Base de Données d'abord !!!");
            }
        }

        public void FillIdC(string nom, out string idC, out string idP)
        {
            idC = idP = "";
            string sqlCom = "SELECT idC, idP FROM controle WHERE idP in (SELECT idP from patient where nom = '" + nom + "' );";
            try
            {
                connect();
                MySqlConnection mysqlCon = new MySqlConnection(connection);
                mysqlCon.Open();

                MySqlDataAdapter MyDA = new MySqlDataAdapter();
                MyDA.SelectCommand = new MySqlCommand(sqlCom, mysqlCon);

                DataTable table = new DataTable();
                MyDA.Fill(table);

                idC = table.Rows[0][0].ToString();
                idP = table.Rows[0][1].ToString();
                //dgv.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            catch
            {
                MessageBox.Show("Veillez Connecter la Base de Données d'abord !!!");
            }
        }

        public void execQuery(string query)
        {
            MySqlConnection databaseConnection = new MySqlConnection(connection);
            MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
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
        }

        public void fillCombo(string nom, ComboBox combo)
        {
            string query = "SELECT * FROM " + nom + ";";
            MySqlConnection databaseConnection = new MySqlConnection(connection);
            MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
            commandDatabase.CommandTimeout = 60;
            combo.Items.Clear();
            try
            {
                databaseConnection.Open();
                MySqlDataReader myReader = commandDatabase.ExecuteReader();

                if (myReader.HasRows)
                {
                    while (myReader.Read())
                    {
                        combo.Items.Add(myReader[0].ToString());
                    }
                }

                databaseConnection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void idgrub(string nom, DataTable table)
        {
            try
            {
                MySqlConnection mysqlCon = new MySqlConnection(connection);
                MySqlDataAdapter MyDA = new MySqlDataAdapter();
                string command = "SELECT idP FROM patient WHERE nom = @nom;";
                MySqlCommand cmd = new MySqlCommand(command, mysqlCon);
                cmd.Parameters.AddWithValue("@nom", nom);
                mysqlCon.Open();
                MyDA.SelectCommand = cmd;
                MyDA.Fill(table);
            }
            catch
            {
                MessageBox.Show("Veillez Connecter la Base de Données d'abord !!!");
            }
        }

        static public string Encode(string toEncode)
        {
            byte[] toEncodeAsBytes
                  = ASCIIEncoding.ASCII.GetBytes(toEncode);
            string returnValue
                  = Convert.ToBase64String(toEncodeAsBytes);
            return returnValue;
        }

        static public string Decode(string encodedData)
        {
            byte[] encodedDataAsBytes
                = Convert.FromBase64String(encodedData);
            string returnValue =
               ASCIIEncoding.ASCII.GetString(encodedDataAsBytes);
            return returnValue;
        }

        public static string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Gestionnaire des ordonnances";
        public static string filePath = path + @"\Database Connection.txt";
        public static string lgn = path + @"\Login Credentials.txt";

        static public bool checkCred()
        {
            FileInfo fileInfo = new FileInfo(filePath);
            if (Directory.Exists(path) && fileInfo.Exists && fileInfo.Length > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        static public bool checkLogin()
        {
            if (LoginDataReader().Length > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static void LoginDataInit(string admin_login, string admin_pass, string user_login, string user_pass)
        {
            string query = "INSERT INTO login VALUE('" + admin_login + "', '" + Encode(admin_pass) + "','admin');";
            string query2 = "INSERT INTO login VALUE('" + user_login + "', '" + Encode(user_pass) + "','user');";
            MySqlConnection databaseConnection = new MySqlConnection(connection);
            MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
            MySqlCommand commandDatabase2 = new MySqlCommand(query2, databaseConnection);
            commandDatabase.CommandTimeout = 60;

            try
            {
                databaseConnection.Open();
                MySqlDataReader myReader = commandDatabase.ExecuteReader();
                myReader.Close();
                MySqlDataReader myReader2 = commandDatabase2.ExecuteReader();
                myReader2.Close();
                databaseConnection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static void LoginDataWriter(string admin_login, string admin_pass, string user_login, string user_pass)
        {
            string query = "UPDATE login SET username = '" + admin_login + "', password = '" + Encode(admin_pass) + "' WHERE role = 'admin';";
            string query2 = "UPDATE login SET username = '" + user_login + "', password = '" + Encode(user_pass) + "' WHERE role = 'user';";
            MySqlConnection databaseConnection = new MySqlConnection(connection);
            MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
            MySqlCommand commandDatabase2 = new MySqlCommand(query2, databaseConnection);
            commandDatabase.CommandTimeout = 60;

            try
            {
                databaseConnection.Open();
                MySqlDataReader myReader = commandDatabase.ExecuteReader();
                myReader.Close();
                MySqlDataReader myReader2 = commandDatabase2.ExecuteReader();
                myReader2.Close();
                databaseConnection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static string LoginDataReader()
        {
            string query = "SELECT * FROM login";
            MySqlConnection databaseConnection = new MySqlConnection(connection);
            MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
            commandDatabase.CommandTimeout = 60;
            Dictionary<string, List<string>> loginDict = new Dictionary<string, List<string>>();
            string credtext = "";
            try
            {
                databaseConnection.Open();
                MySqlDataReader myReader = commandDatabase.ExecuteReader();

                if (myReader.HasRows)
                {
                    while (myReader.Read())
                    {
                        loginDict.Add(myReader[2].ToString(), new List<string> { myReader[0].ToString(), myReader[1].ToString() });
                    }
                }
                databaseConnection.Close();
                credtext = loginDict["admin"][0] + "/" + loginDict["admin"][1] + "/" + loginDict["user"][0] + "/" + loginDict["user"][1];
                return credtext;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return credtext;
            }
        }

        public static void writer(string host, string port, string user, string password, string database)
        {
            FileInfo info = new FileInfo(filePath);
            StreamWriter writer = new StreamWriter(filePath);
            if (!info.Exists)
            {
                File.Create(filePath);
            }
            writer.WriteLine(host + "/" + port + "/" + user + "/" + Encode(password) + "/" + database);
            writer.Close();

        }

        public static string reader()
        {
            string readText;
            var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            var streamReader = new StreamReader(fileStream, Encoding.UTF8);
            readText = streamReader.ReadToEnd();
            fileStream.Close();
            return readText;
        }

        public static void createF()
        {
            DirectoryInfo dInfo = new DirectoryInfo(path);
            if (!dInfo.Exists)
            {
                Directory.CreateDirectory(path);
            }
            FileInfo fInfo = new FileInfo(filePath);
            if (!fInfo.Exists)
            {
                var myFile = File.Create(filePath);
                myFile.Close();
            }
        }

        public static void connect()
        {
            string[] all = reader().Split(char.Parse("/"));
            string host = all[0];
            string port = all[1];
            string user = all[2];
            string password = Decode(all[3]);
            string database = all[4];
            connection = "datasource =" + host + "; port =" + port + "; username =" + user + "; password =" + password + "; database =" + database + ";";
            MySqlConnection mysqlCon = new MySqlConnection(connection);
            mysqlCon.Open();
            
        }

        public static string CalculateAge(DateTime dateN)
        {
            var today = DateTime.Today;
            var age = today.Year - dateN.Year;
            //if (dateN.Date > today.AddYears(-age)) age--;

            return age.ToString();
        }
    }
}
