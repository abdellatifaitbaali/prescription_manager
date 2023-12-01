using Gestionnaire_des_Ordonnances;
using System;
using System.Windows.Forms;
using static Gestionnaire_des_Ordonnances.DataHandling;

namespace login
{
    public partial class Login : Form
    {
        DataHandling da = new DataHandling();
        public Login()
        {
            InitializeComponent();
        }

        string admin, admin_pwd, user, user_pwd;

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textBox2.Focus();
            }
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                login();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.Equals(""))
            {
                errorProvider1.SetError(textBox1, "Champ Obligatoire!");
            }
            else
            {
                errorProvider1.Clear();
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text.Equals(""))
            {
                errorProvider2.SetError(textBox2, "Champ Obligatoire!");
            }
            else
            {
                errorProvider2.Clear();
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            login();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            try
            {
                createF();
                connect();
                if (checkLogin())
                {
                    string[] all = LoginDataReader().Split(char.Parse("/"));
                    admin = all[0];
                    admin_pwd = Decode(all[1]);
                    user = all[2];
                    user_pwd = Decode(all[3]);
                }
                else
                {
                    LoginDataInit("admin", "admin", "user", "user");
                    string[] all = LoginDataReader().Split(char.Parse("/"));
                    admin = all[0];
                    admin_pwd = Decode(all[1]);
                    user = all[2];
                    user_pwd = Decode(all[3]);
                }
            }
            catch(Exception E)
            {
                MessageBox.Show(E.Message);
            }
        }

        private void login()
        {
            if (!textBox1.Text.Equals(admin) && !textBox1.Text.Equals(user))
            {
                errorProvider1.SetError(textBox1, "Nom d'utilisateur Incorrect!");
            }

            if (!textBox2.Text.Equals(admin_pwd) && !textBox2.Text.Equals(user_pwd))
            {
                errorProvider2.SetError(textBox2, "Mot de passe Incorrect! ");
            }

            if (textBox1.Text.Equals(admin) && !textBox2.Text.Equals(admin_pwd))
            {
                errorProvider2.SetError(textBox2, "Mot de passe Incorrect! ");
            }

            if (textBox1.Text.Equals(user) && !textBox2.Text.Equals(user_pwd))
            {
                errorProvider2.SetError(textBox2, "Mot de passe Incorrect! ");
            }

            if ((textBox1.Text.Equals(admin) && textBox2.Text.Equals(admin_pwd)) || (textBox1.Text.Equals(user) && textBox2.Text.Equals(user_pwd)))
            {
                if (textBox1.Text.Equals(admin) && textBox2.Text.Equals(admin_pwd))
                {
                    Gestionnaire_des_Ordonnances.accueille f = new Gestionnaire_des_Ordonnances.accueille();
                    this.Hide();
                    f.ShowDialog();
                    this.Close();
                }
                else if (textBox1.Text.Equals(user) && textBox2.Text.Equals(user_pwd))
                {
                    Reception.acceuille f = new Reception.acceuille();
                    this.Hide();
                    f.ShowDialog();
                    this.Close();
                }
            }
        }
    }
}
