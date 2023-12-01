using System;
using System.Windows.Forms;

namespace Gestionnaire_des_Ordonnances
{
    public partial class edit_data : Form
    {
        public edit_data()
        {
            InitializeComponent();
        }

        private void Form8_Load(object sender, EventArgs e)
        {
            modifier_patient pat = new modifier_patient();
            pat.TopLevel = false;
            pat.AutoScroll = true;
            panel1.Controls.Add(pat);
            pat.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            modifier_patient f = new modifier_patient();
            f.TopLevel = false;
            f.AutoScroll = true;
            panel1.Controls.Add(f);
            f.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            midifier_mut_trait f = new midifier_mut_trait();
            f.TopLevel = false;
            f.AutoScroll = true;
            panel1.Controls.Add(f);
            f.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            modifier_controle f = new modifier_controle();
            f.TopLevel = false;
            f.AutoScroll = true;
            panel1.Controls.Add(f);
            f.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            modifier_login f = new modifier_login();
            f.TopLevel = false;
            f.AutoScroll = true;
            panel1.Controls.Add(f);
            f.Show();
        }
    }
}
