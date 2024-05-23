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

namespace Project
{
    public partial class frmLogin : Form
    {

        private static string connectionString = "Data Source=MSI\\SQLEXPRESS;Initial Catalog=proje;Integrated Security=True;Encrypt=False";
        public static SqlConnection connect = new SqlConnection(connectionString);

        public frmLogin()
        {
            InitializeComponent();
        }
                    
        private void btn1_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = true;
            groupBox2.Visible = false;
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = false;
            groupBox2.Visible = true;
            
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            groupBox2.Visible = false;
           
        }      

        private void btnSignUp_Click(object sender, EventArgs e)
        {
            if (txtMail.Text == "" || txtPass.Text == "")            
                MessageBox.Show("Kayıt alanı boş geçilemez");
                           
            else
            {
                string query = "INSERT INTO login (name,surname,e_mail,password) VALUES (@name,@surname,@mail,@pass)";

                connect.Open();            
                SqlCommand command = new SqlCommand(query, connect);
                command.Parameters.AddWithValue("@name", txtName.Text);
                command.Parameters.AddWithValue("@surname", txtSurname.Text);
                command.Parameters.AddWithValue("@mail", txtMail.Text);
                command.Parameters.AddWithValue("@pass", txtPass.Text);
                command.ExecuteNonQuery();
                connect.Close();

                MessageBox.Show("Kayıt işleminiz tamamlanmıştır");               
            }
        }

      
        private void btnSignIn_Click(object sender, EventArgs e)
        {
            bool isThere = false;
            string query = "Select * from login";

            connect.Open();
            SqlCommand command = new SqlCommand(query, connect);
            SqlDataReader reader = command.ExecuteReader();       

            while (reader.Read())
            {
                if (txtEmail.Text == reader["e_mail"].ToString() && txtPassword.Text == reader["password"].ToString())
                {
                    isThere = true;              
                    break;
                }
                else
                    isThere = false;
            }
            connect.Close();

            if (isThere)
            {
                this.Hide();
                var page = new frmMainPage();
                page.Closed += (s, args) => this.Close();
                page.Show();
            }           
            
            else            
                MessageBox.Show("Hatalı giriş yaptınız");
                           
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {                  
            this.Hide();
            var forgotPass = new frmForgotPass();
            forgotPass.Closed += (s, args) => this.Close();
            forgotPass.Show();
        }
    }
}
