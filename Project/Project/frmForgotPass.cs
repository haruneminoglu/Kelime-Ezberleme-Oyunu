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
using System.Net;
using System.Net.Mail;



namespace Project
{
    public partial class frmForgotPass : Form
    {
        SqlConnection connect = frmLogin.connect;
        string randomCode,to;
        
        public frmForgotPass()
        {
            InitializeComponent();
        }
      
        private void btnSend_Click(object sender, EventArgs e)
        {
            string from, pass, messageBody;

            Random rnd = new Random();
            randomCode = (rnd.Next(999999)).ToString();

            MailMessage message = new MailMessage();
            to = (txtMail.Text).ToString();

            from = "yazilimyapimi123@outlook.com";
            pass = "yazilimYapimi_123";
            messageBody = "Sıfırlama kodunuz: " + randomCode;

            message.To.Add(to);
            message.From = new MailAddress(from);
            message.Body = messageBody;
            message.Subject = "Parola sıfırlama kodu";

            SmtpClient smtp = new SmtpClient("smtp-mail.outlook.com");

            smtp.EnableSsl = true;
            smtp.Port = 587;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Credentials = new NetworkCredential(from, pass);

            try
            {
                smtp.Send(message);
                MessageBox.Show("Kod başarıyla gönderildi");
            } catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnVerify_Click(object sender, EventArgs e)
        {
            if (randomCode == (txtCode.Text).ToString())
            {
                to = txtMail.Text;
                groupBox1.Visible = false;
                groupBox2.Visible = true;
            }

            else
            MessageBox.Show("Girilen kod hatalı");
            
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            string query = "UPDATE login SET password = @newPass WHERE e_mail='"+to+"'";

            connect.Open();         
            SqlCommand command = new SqlCommand(query,connect);
            command.Parameters.AddWithValue("@newPass", txtNewPass.Text.ToString());
            command.ExecuteNonQuery();
            connect.Close();

            MessageBox.Show("Sıfırlama işlemi başarılı");
           
            this.Hide();
            var back = new frmLogin();
            back.Closed += (s, args) => this.Close();
            back.Show();

        }

        private void frmForgotPass_Load(object sender, EventArgs e)
        {
            groupBox2.Visible = false;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            var login = new frmLogin();
            login.Closed += (s, args) => this.Close();
            login.Show();
        }
    }
}
