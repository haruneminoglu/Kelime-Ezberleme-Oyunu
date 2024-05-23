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
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Project
{
    public partial class frmAddWord : Form
    {
        SqlConnection connect = frmLogin.connect;

        public frmAddWord()
        {
            InitializeComponent();
           
        }
    
        private void clearFields()
        {
            txtEnglish.Clear();
            txtTurkish.Clear();
            txtSentence.Clear();
            txtPicture.Clear();

            pictureBox1.Image = null;
        }

              
        private void btnChoose_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            pictureBox1.ImageLocation = openFileDialog1.FileName;
            txtPicture.Text = openFileDialog1.FileName;
        }

        
        
        private void btnSave_Click(object sender, EventArgs e)
        {
            string query = "INSERT INTO unknown(english,turkish,sentence,picture) VALUES (@eng,@turk,@sntc,@pic) ";

            connect.Open();                    
            SqlCommand command = new SqlCommand(query, connect);
            command.Parameters.AddWithValue("@eng", txtEnglish.Text);
            command.Parameters.AddWithValue("@turk", txtTurkish.Text);
            command.Parameters.AddWithValue("@sntc", txtSentence.Text);
            command.Parameters.AddWithValue("@pic", txtPicture.Text);                  
            command.ExecuteNonQuery();
            connect.Close();           

            clearFields();
            
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            var page = new frmMainPage();
            page.Closed += (s, args) => this.Close();
            page.Show();
        }
    }
}
