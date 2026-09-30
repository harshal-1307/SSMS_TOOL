using Microsoft.Data.SqlClient;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SSMS_TOOL
{
    public partial class LoginForm : Form
    {
        private Server? _server;
        private Database? _database;
        public LoginForm()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void signin_Click(object sender, EventArgs e)
        {
            try
            {

                string Name = username.Text;
                string Password = password.Text;
                string LoginStatus = GetData(Name, Password);

                if (!string.IsNullOrEmpty(LoginStatus))
                {
                    FileSaveByAdmin fileSaveByAdmin = new FileSaveByAdmin();
                    fileSaveByAdmin.AdminName = Name;
                    fileSaveByAdmin.Show();
                    Application.OpenForms["Form1"]?.Hide();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Enter admin credentials are incorrect ");
                    username.Text = "";
                    password.Text = "";
                }
            }
            catch (Exception ex)
            {

            }
            
        }

        public string GetData(string username ,string password)
        {
            CommandType commandType;
            string serverName = "thwdbd10.smtbusa.us,4433";
            string databaseName = "CLIENTONBOARDING_GCR2";

            string connectionString =
                connectionString =
               $"Server={serverName}; Database ={databaseName};" +
               "Integrated Security=True;" +
               "TrustServerCertificate=True;";
           
            string Value = string.Empty;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand(
                       $"select 1 from AdminLoginCredentials where AdminName='{username}' and Password='{password}'", connection))
                    {
                        object result= cmd.ExecuteScalar();
                        if (result!=null && result !=DBNull.Value)
                        {
                            Value = result.ToString();
                        }
                    }
                }
                
            }
            catch (Exception ex)
            {

            }
            return Value;
        }

    }
}
