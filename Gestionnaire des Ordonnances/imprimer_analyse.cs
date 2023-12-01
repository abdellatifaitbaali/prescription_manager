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
    public partial class imprimer_analyse : Form
    {
        DataHandling da = new DataHandling();
        public imprimer_analyse()
        {
            InitializeComponent();
        }

        private void imprimer_analyse_Load(object sender, EventArgs e)
        {
            string query2 = "SELECT analyse FROM controle WHERE idC = " + idCont + ";";
            string query1 = "SELECT nom, age FROM patient WHERE idP = (SELECT idP FROM controle WHERE idC = " + idCont + ");";
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
                //MessageBox.Show(idCont);
                DataTable table = new DataTable();
                MyDA.Fill(table);
                int len = table.Rows[0][0].ToString().Split(char.Parse("/")).Length;
                //MessageBox.Show(len.ToString());
                if (len > 0)
                {
                    labelt1.Text = "¤ ";
                    labelt1.Visible = true;
                    foreach(string item in table.Rows[0][0].ToString().Split(char.Parse("/")))
                    {
                        labelt1.Text += item + "\n";
                    }
                }
            }
            catch(Exception E)
            {
                throw E;
                //throw;
            }
            //labelDate.Text = DateTime.Now.ToString("d/M/yyyy");
            //labelNom.Text += "   " + CalculateAge(dateN) + "ans";
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Print();
        }

        //private void Print(Panel pnl)
        //{
        //    try
        //    {
        //        PrinterSettings ps = new PrinterSettings();
        //        PaperSize paperSize = new PaperSize("A5 (210 x 148 mm)", 0, 0);
        //        ps.DefaultPageSettings.PaperSize = paperSize;
        //        getprinters(pnl);
        //        printPreviewDialog1.Document = printDocument1;
        //        printDocument1.PrintPage += new PrintPageEventHandler(printDocument1_PrintPage);
        //        printPreviewDialog1.ShowDialog();
        //    }
        //    catch(Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}

        //private Bitmap memoryimg;
        //private void getprinters(Panel pnl)
        //{
        //    memoryimg = new Bitmap(pnl.Width, pnl.Height);
        //    pnl.DrawToBitmap(memoryimg, new Rectangle(0, 0, pnl.Width, pnl.Height));
        //}

        //private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        //{
        //    Rectangle pagearea = e.PageBounds;
        //    e.Graphics.DrawImage(memoryimg, (pagearea.Width / 2) - (panel1.Width / 2), this.panel1.Location.Y);
        //}

        private void PrintDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            Bitmap b = new Bitmap(panel1.Width, panel1.Height);
            panel1.DrawToBitmap(b, new Rectangle(0, 0, panel1.Width, panel1.Height));
            Rectangle pagearea = e.PageBounds;
            e.Graphics.DrawImage(b, (pagearea.Width / 2) - (panel1.Width / 2), 130);//this.panel1.Location.Y);
        }

        private void Print()
        {
            PrintPreviewDialog printPreviewDialog1 = new PrintPreviewDialog();
            var paperSize = printDocument1.PrinterSettings.PaperSizes.Cast<PaperSize>().FirstOrDefault(e => e.PaperName == "A5");
            printDocument1.PrinterSettings.DefaultPageSettings.PaperSize = paperSize;
            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.ShowDialog();
        }
    }
}
