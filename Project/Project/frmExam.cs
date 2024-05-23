using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using Project;
using System.Security.Cryptography;
using System.Xml.Linq;
using System.Diagnostics.Eventing.Reader;


namespace Project
{
    public partial class frmExam : Form
    {       
        public frmExam()
        {
            InitializeComponent();
     
        }

        SqlConnection connect = frmLogin.connect;

        int rows;
        private void numOfRows()
        {
            string query = "SELECT COUNT (*) FROM unknown";
            
            connect.Open();          
            SqlCommand command = new SqlCommand(query, connect);
            rows = (int)command.ExecuteScalar();
            connect.Close();
        }

        string eng, turk, sntc, pic;
        
        int strk, id;
        int i = 0,counter=0;


        DateTime today = DateTime.Now;
        DateTime date;

        private void readWord()
        {
            numOfRows();

            if (i < rows)
            {
                string query = "SELECT * FROM unknown";

                connect.Open();           
                SqlCommand command = new SqlCommand(query, connect);
                SqlDataReader reader = command.ExecuteReader();

                int j = 0;
                i++;

                while (j < i)
                {
                    reader.Read();
                    j++;
                }

                strk = reader.GetInt32(5);
                date = (DateTime)reader["date"];

                DateTime oneDay = date.AddDays(1);
                DateTime oneWeek = date.AddDays(7);
                DateTime oneMonth = date.AddMonths(1);
                DateTime threeMonth = date.AddMonths(3);
                DateTime sixMonth = date.AddMonths(6);
                DateTime oneYear = date.AddYears(1);


                if ((strk == 0) && (counter < frmSettings.freq))
                {
                    eng = reader["english"].ToString();
                    turk = reader["turkish"].ToString();
                    sntc = reader["sentence"].ToString();
                    pic = reader["picture"].ToString();
                    id = reader.GetInt32(0);

                    connect.Close();
                    lblEnglish.Text = eng;

                    counter++;
                }

                else if ((strk == 1 && oneDay.Date <= today.Date) || (strk == 2 && oneWeek.Date <= today.Date) || (strk == 3 && oneMonth.Date <= today.Date) || (strk == 4 && threeMonth.Date <= today.Date) || (strk == 5 && sixMonth.Date <= today.Date) || (strk == 6 && oneYear.Date <= today.Date))
                {
                    eng = reader["english"].ToString();
                    turk = reader["turkish"].ToString();
                    sntc = reader["sentence"].ToString();
                    pic = reader["picture"].ToString();
                    id = reader.GetInt32(0);

                    connect.Close();
                    lblEnglish.Text = eng;
                }

                else
                {
                    connect.Close();
                    readWord();                   
                }                
            }

            else
            {
                groupBox1.Visible = true;
                panel1.Visible = false;

                lblCorrect.Text = correct.ToString();
                lblIncorrect.Text = incorrect.ToString();
            }
               
                                    
        }
        
        private void setStreak()
        {
            string query = "UPDATE unknown SET streak = '" + strk + "' WHERE id='" + id + "'";
           
            connect.Open();       
            SqlCommand command = new SqlCommand(query, connect);
            command.ExecuteNonQuery();
            connect.Close();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            var page = new frmMainPage();
            page.Closed += (s, args) => this.Close();
            page.Show();
        }

        private void setDate()
        {
            string query = "UPDATE unknown SET date = GETDATE() WHERE id='" + id + "'";

            connect.Open();   
            SqlCommand command = new SqlCommand(query, connect);          
            command.ExecuteNonQuery();      
            connect.Close();
        }
        private void btnStart_Click(object sender, EventArgs e)
        {              
            btnStart.Visible = false;
            panel1.Visible = true;

            readWord();           
        }

        int correct = 0, incorrect = 0;

        private void btnCheck_Click(object sender, EventArgs e)
        {
            panel2.Visible = true;

            if (turk == txtTurkish.Text)
            {
                
                lblCheck.Text = "Doğru cevap";
                lblCheck.ForeColor = Color.Green;

                correct++;
                strk++;

                if (strk == 7)
                {
                    string query = "INSERT INTO known(id,english) VALUES (@id,@eng)";
                    string queryDelete = "DELETE FROM unknown WHERE id =(" + id + ")";
                   
                    connect.Open();                  
                    SqlCommand command = new SqlCommand(query, connect);
                    SqlCommand cmdDelete = new SqlCommand(queryDelete, connect);
                                       
                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@eng", eng); 
                    
                    command.ExecuteNonQuery();
                    cmdDelete.ExecuteNonQuery();

                    connect.Close();                    
                }

                else
                {
                    setStreak();
                    setDate();
                }
            
            }

            else
            {
                
                lblCheck.Text = "Yanlış cevap";
                lblCheck.ForeColor = Color.Red;

                incorrect++;
                strk = 0;
                setStreak();
                setDate();
            }

            pictureBox2.ImageLocation = pic;
            lblSentence.Text = sntc;
            lblTurk.Text = turk;                            
        }
       
        private void btnContinue_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
            txtTurkish.Clear();         
            readWord();                       
        }
    }
}
