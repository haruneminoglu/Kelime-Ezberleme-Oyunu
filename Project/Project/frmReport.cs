using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project
{
    public partial class frmReport : Form
    {
        public frmReport()
        {
            InitializeComponent();
        }

        SqlConnection connect = frmLogin.connect;

        int total, known, unknown;
        double successRate;

        private void countRows()
        {
            connect.Open();
            string qryKnown = "SELECT COUNT (*) FROM known";
            string qryUnknown = "SELECT COUNT (*) FROM unknown";

            SqlCommand cmdKnown = new SqlCommand(qryKnown, connect);
            SqlCommand cmdUnknown = new SqlCommand(qryUnknown, connect);

            known = (int)cmdKnown.ExecuteScalar();
            unknown = (int)cmdUnknown.ExecuteScalar();

            connect.Close();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            var back = new frmMainPage();
            back.Closed += (s, args) => this.Close();
            back.Show();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            string report = "Toplam kelimeler:" + lblTotal.Text + "\n\nÖğrendiğim kelimeler:" + lblKnown.Text +
                "\n\nÖğrenmekte olduğum kelimeler:" + lblUnknown.Text + "\n\nBaşarı oranı:" + lblSuccess.Text;

            e.Graphics.DrawString(report, new Font("Arial", 18, FontStyle.Regular), Brushes.Black,new Point(10,10));
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.ShowDialog();
        }


  
        private void frmReport_Load(object sender, EventArgs e)
        {
            countRows();
            total = known + unknown;
            successRate =Math.Round ((double)known / total * 100,2);

            lblTotal.Text = total.ToString();
            lblKnown.Text = known.ToString();
            lblUnknown.Text = unknown.ToString();
            lblSuccess.Text = "%"+successRate.ToString();

            chart1.Series["s1"].IsValueShownAsLabel = true;
            chart1.Series["s1"].Points.AddXY("Öğrendiğim", known);
            chart1.Series["s1"].Points.AddXY("Öğrenmekte olduğum", unknown);          
        }
    }
}
