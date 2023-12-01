using System;
using System.Windows.Forms;
using static Gestionnaire_des_Ordonnances.DataHandling;

namespace Gestionnaire_des_Ordonnances
{
    public partial class modifier_login : Form
    {
        DataHandling da = new DataHandling();
        public modifier_login()
        {
            InitializeComponent();
        }

        private void textBox2_MouseClick(object sender, MouseEventArgs e)
        {
            if(admin_pass.UseSystemPasswordChar == true)
            {
                admin_pass.UseSystemPasswordChar = false;
            }
            else
            {
                admin_pass.UseSystemPasswordChar = true;
            }
        }

        private void textBox3_MouseClick(object sender, MouseEventArgs e)
        {
            if (user_pass.UseSystemPasswordChar == true)
            {
                user_pass.UseSystemPasswordChar = false;
            }
            else
            {
                user_pass.UseSystemPasswordChar = true;
            }
        }

        private void modifier_login_Load(object sender, EventArgs e)
        {
            string[] res = LoginDataReader().Split(char.Parse("/"));
            admin_name.Text = res[0];
            admin_pass.Text = Decode(res[1]);
            user_name.Text = res[2];
            user_pass.Text = Decode(res[3]);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if(!admin_name.Equals(user_name))
                {
                    LoginDataWriter(admin_name.Text, admin_pass.Text, user_name.Text, user_pass.Text);
                    MessageBox.Show("Informations updated successfully!");
                    admin_pass.UseSystemPasswordChar = true;
                    user_pass.UseSystemPasswordChar = true;
                }
                else
                {
                    MessageBox.Show("Veuillez choisisser un nom d'utilisateur different de l'admin!");
                }
            }
            catch(Exception E)
            {
                MessageBox.Show(E.Message);
            }
        }
    }
}
