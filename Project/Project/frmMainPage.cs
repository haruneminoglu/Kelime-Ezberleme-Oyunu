using Project;
using System;
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
    public partial class frmMainPage : Form
    {
        public frmMainPage()
        {
            InitializeComponent();
        }


        private void btnAddWord_Click(object sender, EventArgs e)
        {
            this.Hide();
            var addWord = new frmAddWord();
            addWord.Closed += (s, args) => this.Close();
            addWord.Show();

        }

        private void btnExam_Click(object sender, EventArgs e)
        {
            this.Hide();
            var exam= new frmExam();
            exam.Closed += (s, args) => this.Close();
            exam.Show();

        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            this.Hide();
            var report = new frmReport();
            report.Closed += (s, args) => this.Close();
            report.Show();

        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            this.Hide();
            var settings = new frmSettings();
            settings.Closed += (s, args) => this.Close();
            settings.Show();

        }

       
    }
}
