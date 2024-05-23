using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project
{
    public partial class frmSettings : Form
    {
        public frmSettings()
        {
            InitializeComponent();
        }
   

       public static int freq=10;
        private void btnSave_Click(object sender, EventArgs e)
        {
            freq = Convert.ToInt32(txtFreq.Text);
            MessageBox.Show("Değişilikler kaydedildi");
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            var back = new frmMainPage();
            back.Closed += (s, args) => this.Close();
            back.Show();
        }
    }
}
