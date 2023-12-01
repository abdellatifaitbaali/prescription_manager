using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Windows.Forms;
using static Gestionnaire_des_Ordonnances.DataHandling;
using static Gestionnaire_des_Ordonnances.liste_controle;

namespace Gestionnaire_des_Ordonnances
{
    public partial class imprimer : Form
    {
        DataHandling da = new DataHandling();
        public imprimer()
        {
            InitializeComponent();
        }

        private void Form10_Load(object sender, EventArgs e)
        {
            button1.BringToFront();
            button2.BringToFront();
            string query2 = "SELECT traitement FROM controle WHERE idC = " + idCont + ";";
            string query1 = "SELECT nom, age FROM patient WHERE idP in (SELECT idP FROM controle WHERE idP = " + idP + ");";
            try
            {
                connect();
                MySqlConnection mysqlCon = new MySqlConnection(connection);
                mysqlCon.Open();

                MySqlDataAdapter MyDA = new MySqlDataAdapter
                {
                    SelectCommand = new MySqlCommand(query1, mysqlCon)
                };

                DataTable table = new DataTable();
                MyDA.Fill(table);
                labelNom.Text = table.Rows[0][0].ToString().ToUpper();
                labelNom2.Text = table.Rows[0][0].ToString().ToUpper();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            try
            {
                connect();
                MySqlConnection mysqlCon = new MySqlConnection(connection);
                mysqlCon.Open();

                MySqlDataAdapter MyDA = new MySqlDataAdapter
                {
                    SelectCommand = new MySqlCommand(query2, mysqlCon)
                };
                //MessageBox.Show(idP.ToString() + " " + idCont.ToString());
                DataTable table = new DataTable();
                MyDA.Fill(table);
                int len = table.Rows[0][0].ToString().Split(char.Parse("/")).Length;
                //MessageBox.Show(len.ToString());
                string item;
                if (len > 0 && len <= 12)
                {
                    labelTrait.Text = "";
                    labelt1.Text = "";
                    item = table.Rows[0][0].ToString().Split(char.Parse("/"))[0];
                    labelTrait.Visible = true;
                    labelt1.Visible = true;
                    if (item.Split(char.Parse(" ")).Length < 10)
                    {
                        labelTrait.Text = item.Split(char.Parse(" "))[0].ToUpper();
                        for (int i = 1; i < item.Split(char.Parse(" ")).Length; i++)
                        {
                            labelt1.Text += item.Split(char.Parse(" "))[i] + " ";
                        }
                    }
                    else
                    {
                        labelTrait.Text = item.Split(char.Parse(" "))[0].ToUpper() + " " + item.Split(char.Parse(" "))[1] + " " + item.Split(char.Parse(" "))[2];
                        for (int i = 3; i < item.Split(char.Parse(" ")).Length; i++)
                        {
                            labelt1.Text += item.Split(char.Parse(" "))[i] + " ";
                        }
                    }
                    if (len > 1)
                    {
                        labelTrait2.Text = "";
                        labelt2.Text = "";
                        item = table.Rows[0][0].ToString().Split(char.Parse("/"))[1];
                        labelTrait2.Visible = true;
                        labelt2.Visible = true;
                        if (item.Split(char.Parse(" ")).Length < 10)
                        {
                            labelTrait2.Text = item.Split(char.Parse(" "))[0].ToUpper();
                            for (int i = 1; i < item.Split(char.Parse(" ")).Length; i++)
                            {
                                labelt2.Text += item.Split(char.Parse(" "))[i] + " ";
                            }
                        }
                        else
                        {
                            labelTrait2.Text = item.Split(char.Parse(" "))[0].ToUpper() + " " + item.Split(char.Parse(" "))[1] + " " + item.Split(char.Parse(" "))[2];
                            for (int i = 3; i < item.Split(char.Parse(" ")).Length; i++)
                            {
                                labelt2.Text += item.Split(char.Parse(" "))[i] + " ";
                            }
                        }
                        if (len > 2)
                        {
                            labelTrait3.Text = "";
                            labelt3.Text = "";
                            item = table.Rows[0][0].ToString().Split(char.Parse("/"))[2];
                            labelTrait3.Visible = true;
                            labelt3.Visible = true;
                            if (item.Split(char.Parse(" ")).Length < 10)
                            {
                                labelTrait3.Text = item.Split(char.Parse(" "))[0].ToUpper();
                                for (int i = 1; i < item.Split(char.Parse(" ")).Length; i++)
                                {
                                    labelt3.Text += item.Split(char.Parse(" "))[i] + " ";
                                }
                            }
                            else
                            {
                                labelTrait3.Text = item.Split(char.Parse(" "))[0].ToUpper() + " " + item.Split(char.Parse(" "))[1] + " " + item.Split(char.Parse(" "))[2];
                                for (int i = 3; i < item.Split(char.Parse(" ")).Length; i++)
                                {
                                    labelt3.Text += item.Split(char.Parse(" "))[i] + " ";
                                }
                            }
                            if (len > 3)
                            {
                                labelTrait4.Text = "";
                                labelt4.Text = "";
                                item = table.Rows[0][0].ToString().Split(char.Parse("/"))[3];
                                labelTrait4.Visible = true;
                                labelt4.Visible = true;
                                if (item.Split(char.Parse(" ")).Length < 10)
                                {
                                    labelTrait4.Text = item.Split(char.Parse(" "))[0].ToUpper();
                                    for (int i = 1; i < item.Split(char.Parse(" ")).Length; i++)
                                    {
                                        labelt4.Text += item.Split(char.Parse(" "))[i] + " ";
                                    }
                                }
                                else
                                {
                                    labelTrait4.Text = item.Split(char.Parse(" "))[0].ToUpper() + " " + item.Split(char.Parse(" "))[1] + " " + item.Split(char.Parse(" "))[2];
                                    for (int i = 3; i < item.Split(char.Parse(" ")).Length; i++)
                                    {
                                        labelt4.Text += item.Split(char.Parse(" "))[i] + " ";
                                    }
                                }
                                if (len > 4)
                                {
                                    labelTrait5.Text = "";
                                    labelt5.Text = "";
                                    item = table.Rows[0][0].ToString().Split(char.Parse("/"))[4];
                                    labelTrait5.Visible = true;
                                    labelt5.Visible = true;
                                    if (item.Split(char.Parse(" ")).Length < 10)
                                    {
                                        labelTrait5.Text = item.Split(char.Parse(" "))[0].ToUpper();
                                        for (int i = 1; i < item.Split(char.Parse(" ")).Length; i++)
                                        {
                                            labelt5.Text += item.Split(char.Parse(" "))[i] + " ";
                                        }
                                    }
                                    else
                                    {
                                        labelTrait5.Text = item.Split(char.Parse(" "))[0].ToUpper() + " " + item.Split(char.Parse(" "))[1] + " " + item.Split(char.Parse(" "))[2];
                                        for (int i = 3; i < item.Split(char.Parse(" ")).Length; i++)
                                        {
                                            labelt5.Text += item.Split(char.Parse(" "))[i] + " ";
                                        }
                                    }
                                    if (len > 5)
                                    {
                                        labelTrait6.Text = "";
                                        labelt6.Text = "";
                                        item = table.Rows[0][0].ToString().Split(char.Parse("/"))[5];
                                        labelTrait6.Visible = true;
                                        labelt6.Visible = true;
                                        if (item.Split(char.Parse(" ")).Length < 10)
                                        {
                                            labelTrait6.Text = item.Split(char.Parse(" "))[0].ToUpper();
                                            for (int i = 1; i < item.Split(char.Parse(" ")).Length; i++)
                                            {
                                                labelt6.Text += item.Split(char.Parse(" "))[i] + " ";
                                            }
                                        }
                                        else
                                        {
                                            labelTrait6.Text = item.Split(char.Parse(" "))[0].ToUpper() + " " + item.Split(char.Parse(" "))[1] + " " + item.Split(char.Parse(" "))[2];
                                            for (int i = 3; i < item.Split(char.Parse(" ")).Length; i++)
                                            {
                                                labelt6.Text += item.Split(char.Parse(" "))[i] + " ";
                                            }
                                        }
                                        if (len > 6)
                                        {
                                            labelTrait7.Text = "";
                                            labelt7.Text = "";
                                            item = table.Rows[0][0].ToString().Split(char.Parse("/"))[6];
                                            labelTrait7.Visible = true;
                                            labelt7.Visible = true;
                                            if (item.Split(char.Parse(" ")).Length < 10)
                                            {
                                                labelTrait7.Text = item.Split(char.Parse(" "))[0].ToUpper();
                                                for (int i = 1; i < item.Split(char.Parse(" ")).Length; i++)
                                                {
                                                    labelt7.Text += item.Split(char.Parse(" "))[i] + " ";
                                                }
                                            }
                                            else
                                            {
                                                labelTrait7.Text = item.Split(char.Parse(" "))[0].ToUpper() + " " + item.Split(char.Parse(" "))[1] + " " + item.Split(char.Parse(" "))[2];
                                                for (int i = 3; i < item.Split(char.Parse(" ")).Length; i++)
                                                {
                                                    labelt7.Text += item.Split(char.Parse(" "))[i] + " ";
                                                }
                                            }
                                            if (len > 7)
                                            {
                                                labelTrait8.Text = "";
                                                labelt8.Text = "";
                                                item = table.Rows[0][0].ToString().Split(char.Parse("/"))[7];
                                                labelTrait8.Visible = true;
                                                labelt8.Visible = true;
                                                if (item.Split(char.Parse(" ")).Length < 10)
                                                {
                                                    labelTrait8.Text = item.Split(char.Parse(" "))[0].ToUpper();
                                                    for (int i = 1; i < item.Split(char.Parse(" ")).Length; i++)
                                                    {
                                                        labelt8.Text += item.Split(char.Parse(" "))[i] + " ";
                                                    }
                                                }
                                                else
                                                {
                                                    labelTrait8.Text = item.Split(char.Parse(" "))[0].ToUpper() + " " + item.Split(char.Parse(" "))[1] + " " + item.Split(char.Parse(" "))[2];
                                                    for (int i = 3; i < item.Split(char.Parse(" ")).Length; i++)
                                                    {
                                                        labelt8.Text += item.Split(char.Parse(" "))[i] + " ";
                                                    }
                                                }
                                                if (len > 8)
                                                {
                                                    labelTrait9.Text = "";
                                                    labelt9.Text = "";
                                                    item = table.Rows[0][0].ToString().Split(char.Parse("/"))[8];
                                                    labelTrait9.Visible = true;
                                                    labelt9.Visible = true;
                                                    if (item.Split(char.Parse(" ")).Length < 10)
                                                    {
                                                        labelTrait9.Text = item.Split(char.Parse(" "))[0].ToUpper();
                                                        for (int i = 1; i < item.Split(char.Parse(" ")).Length; i++)
                                                        {
                                                            labelt9.Text += item.Split(char.Parse(" "))[i] + " ";
                                                        }
                                                    }
                                                    else
                                                    {
                                                        labelTrait9.Text = item.Split(char.Parse(" "))[0].ToUpper() + " " + item.Split(char.Parse(" "))[1] + " " + item.Split(char.Parse(" "))[2];
                                                        for (int i = 3; i < item.Split(char.Parse(" ")).Length; i++)
                                                        {
                                                            labelt9.Text += item.Split(char.Parse(" "))[i] + " ";
                                                        }
                                                    }
                                                    if (len > 9)
                                                    {
                                                        labelTrait10.Text = "";
                                                        labelt10.Text = "";
                                                        item = table.Rows[0][0].ToString().Split(char.Parse("/"))[9];
                                                        labelTrait10.Visible = true;
                                                        labelt10.Visible = true;
                                                        if (item.Split(char.Parse(" ")).Length < 10)
                                                        {
                                                            labelTrait10.Text = item.Split(char.Parse(" "))[0].ToUpper();
                                                            for (int i = 1; i < item.Split(char.Parse(" ")).Length; i++)
                                                            {
                                                                labelt10.Text += item.Split(char.Parse(" "))[i] + " ";
                                                            }
                                                        }
                                                        else
                                                        {
                                                            labelTrait10.Text = item.Split(char.Parse(" "))[0].ToUpper() + " " + item.Split(char.Parse(" "))[1] + " " + item.Split(char.Parse(" "))[2];
                                                            for (int i = 3; i < item.Split(char.Parse(" ")).Length; i++)
                                                            {
                                                                labelt10.Text += item.Split(char.Parse(" "))[i] + " ";
                                                            }
                                                        }
                                                        if (len > 10)
                                                        {
                                                            labelTrait11.Text = "";
                                                            labelt11.Text = "";
                                                            item = table.Rows[0][0].ToString().Split(char.Parse("/"))[10];
                                                            labelTrait11.Visible = true;
                                                            labelt11.Visible = true;
                                                            if (item.Split(char.Parse(" ")).Length < 10)
                                                            {
                                                                labelTrait11.Text = item.Split(char.Parse(" "))[0].ToUpper();
                                                                for (int i = 1; i < item.Split(char.Parse(" ")).Length; i++)
                                                                {
                                                                    labelt11.Text += item.Split(char.Parse(" "))[i] + " ";
                                                                }
                                                            }
                                                            else
                                                            {
                                                                labelTrait11.Text = item.Split(char.Parse(" "))[0].ToUpper() + " " + item.Split(char.Parse(" "))[1] + " " + item.Split(char.Parse(" "))[2];
                                                                for (int i = 3; i < item.Split(char.Parse(" ")).Length; i++)
                                                                {
                                                                    labelt11.Text += item.Split(char.Parse(" "))[i] + " ";
                                                                }
                                                            }
                                                            if (len > 11)
                                                            {
                                                                labelTrait12.Text = "";
                                                                labelt12.Text = "";
                                                                item = table.Rows[0][0].ToString().Split(char.Parse("/"))[11];
                                                                labelTrait12.Visible = true;
                                                                labelt12.Visible = true;
                                                                if (item.Split(char.Parse(" ")).Length < 10)
                                                                {
                                                                    labelTrait12.Text = item.Split(char.Parse(" "))[0].ToUpper();
                                                                    for (int i = 1; i < item.Split(char.Parse(" ")).Length; i++)
                                                                    {
                                                                        labelt12.Text += item.Split(char.Parse(" "))[i] + " ";
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    labelTrait12.Text = item.Split(char.Parse(" "))[0].ToUpper() + " " + item.Split(char.Parse(" "))[1] + " " + item.Split(char.Parse(" "))[2];
                                                                    for (int i = 3; i < item.Split(char.Parse(" ")).Length; i++)
                                                                    {
                                                                        labelt12.Text += item.Split(char.Parse(" "))[i] + " ";
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Ordonnance dépassé le nombre maximale (12)!");
                }
            }
            catch (Exception E)
            {
                //throw E;
                MessageBox.Show(E.Message);
            }
            //labelDate.Text = DateTime.Now.ToString("d/M/yyyy");
            //labelNom.Text += "   " + CalculateAge(dateN) + "ans";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Print(panel1);
        }

        private void Print(Panel pnl)
        {
            try
            {
                PrinterSettings ps = new PrinterSettings();
                PaperSize paperSize = new PaperSize("A5 (210 x 148 mm)", 0, 0);
                ps.DefaultPageSettings.PaperSize = paperSize;
                getprinters(pnl);
                printPreviewDialog1.Document = printDocument1;
                printDocument1.PrintPage += new PrintPageEventHandler(printDocument1_PrintPage);
                printPreviewDialog1.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private Bitmap memoryimg;
        private void getprinters(Panel pnl)
        {
            memoryimg = new Bitmap(pnl.Width, pnl.Height);
            pnl.DrawToBitmap(memoryimg, new Rectangle(0, 0, pnl.Width, pnl.Height));
        }

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            Rectangle pagearea = e.PageBounds;
            e.Graphics.DrawImage(memoryimg, (pagearea.Width / 2) - (panel1.Width / 2), 70);//(pagearea.Width / 2) - (panel1.Width / 2), 130);//this.panel1.Location.Y);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Print(panel2);
        }

        //private void PrintDocument1_PrintPage(object sender, PrintPageEventArgs e)
        //{
        //    Bitmap b = new Bitmap(panel1.Width, panel1.Height);
        //    panel1.DrawToBitmap(b, new Rectangle(0, 0, panel1.Width, panel1.Height));
        //    Rectangle pagearea = e.PageBounds;
        //    e.Graphics.DrawImage(b, (pagearea.Width / 2) - (panel1.Width / 2), 130);//this.panel1.Location.Y);
        //}

        //private void Print()
        //{
        //    PrintPreviewDialog printPreviewDialog1 = new PrintPreviewDialog();
        //    var paperSize = printDocument1.PrinterSettings.PaperSizes.Cast<PaperSize>().FirstOrDefault(e => e.PaperName == "A5");
        //    printDocument1.PrinterSettings.DefaultPageSettings.PaperSize = paperSize;
        //    printPreviewDialog1.Document = printDocument1;
        //    printPreviewDialog1.ShowDialog();
        //}
    }
}
