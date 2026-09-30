using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar;

namespace SSMS_TOOL
{
    public partial class FileSaveByAdmin : Form
    {
        private Server? _server;
        private Database? _database;
        UserAllocation userAllocation = new UserAllocation();

        private string _adminName;
        public string AdminName
        {
            get
            {
                return _adminName;
            }
            set
            {
                _adminName = value;
                if (adminName != null)
                {
                    adminName.Text = value;
                    adminName.Font = new Font(adminName.Font.FontFamily, 12, FontStyle.Bold);
                }
            }
        }

        public FileSaveByAdmin()
        {

            InitializeComponent();
            //  BindProjectDropDonw();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        List<AdminData> AdminNames = new List<AdminData>();
        List<AdminData> FilePath = new List<AdminData>();



        private void BindProjectDropDonw(string serverName, string dbname)
        {
            try
            {
                string connectionString =
                connectionString =
                $"Server={serverName}; Database = {dbname};" +
                "Integrated Security=True;" +
                "TrustServerCertificate=True;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand checkcmd = new SqlCommand(
                        @"select case when object_id('dbo.UserCredenatials','U') is not null then 1 else 0 end", connection))
                    {

                        int TableExists = Convert.ToInt32(checkcmd.ExecuteScalar());

                        if (TableExists == 0)
                        {
                            //MessageBox.Show("UserCredentials table not found .");
                            return;
                        }
                    }

                    using (SqlCommand cmd = new SqlCommand(
                        "select DISTINCT ProjectAllocation,AdminName,FilePath,Filename from dbo.UserCredenatials", connection))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                AdminNames.Add(new AdminData
                                {
                                    ProjectNames = reader["ProjectAllocation"].ToString(),
                                    AdminNames = reader["AdminName"].ToString()
                                });
                                FilePath.Add(new AdminData
                                {
                                    ProjectNames = reader["ProjectAllocation"].ToString(),
                                    FilePath = reader["FilePath"].ToString() + "\\" + reader["Filename"].ToString()
                                });
                            }
                        }
                    }
                    dbName.DataSource = AdminNames
                        .Select(x => x.ProjectNames)
                        .Distinct()
                        .ToList();
                    dbName.SelectedIndex = 0;


                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("error at biding dropdown");
            }
        }


        private void mainFolder_Click(object sender, EventArgs e)
        {
            save.Enabled = true;
            filename.Enabled = true;
            cancel.Enabled = true;
            projectName.Enabled = true;
            assingToWhom.Enabled = true;
        }

        private void creation_Click(object sender, EventArgs e)
        {
            Form1 form = new Form1();
            form.ShowDialog();
            this.Hide();
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            filename.Text = "";
            save.Enabled = true;
            filename.Enabled = true;
            cancel.Enabled = true;
            projectName.Enabled = true;
            assingToWhom.Enabled = true;
        }

        private void save_Click(object sender, EventArgs e)
        {
            try
            {
                using SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.FileName = filename.Text;
                saveFileDialog.Filter = "SQL Files (*.sql)|*.sql";

                if (string.IsNullOrEmpty(projectName.Text) || string.IsNullOrEmpty(filename.Text) || string.IsNullOrEmpty(assingToWhom.Text))
                {
                    MessageBox.Show("Please enter all details");
                }
                else
                {
                    string EnterName = assingToWhom.Text;

                    if (!string.IsNullOrEmpty(EnterName))
                    {
                        List<string> Names = EnterName
                            .Split(',', StringSplitOptions.RemoveEmptyEntries)
                            .Select(x => x.Trim())
                             .Where(x => !string.IsNullOrWhiteSpace(x))
                            .ToList();

                        userAllocation.UserName = Names;
                        userAllocation.AdminName = AdminName;
                        userAllocation.FileName = filename.Text;
                        userAllocation.ProjectAllocation = projectName.Text;

                        if (saveFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            userAllocation.FilePath = Path.GetDirectoryName(saveFileDialog.FileName);
                            GetData(userAllocation);
                            File.WriteAllText(saveFileDialog.FileName, "");
                            MessageBox.Show("File saved successfully");
                            filename.Text = "";
                            assingToWhom.Text = "";
                            projectName.Text = "";
                        }
                    }
                }
            }
            catch(Exception ex)
            {

            }
        }

        public void GetData(UserAllocation userAllocation)
        {
            CommandType commandType;
            string serverName = "thwdbd10.smtbusa.us,4433";
            string databaseName = "CLIENTONBOARDING_GCR2";

            string connectionString =
                connectionString =
               $"Server={serverName}; Database ={databaseName};" +
               "Integrated Security=True;" +
               "TrustServerCertificate=True;";
            try
            {
                // trying to connecting to SQL server .
                SqlConnection sqlConnection =
                    new SqlConnection(connectionString);

                sqlConnection.Open();

                // give the connection to SMO .
                ServerConnection serverConnection =
                    new ServerConnection(sqlConnection);

                // check server name.
                _server = new Server(serverConnection);

                // Test connection
                string serverVersion = _server.Information.Version.ToString();

                // Get the database
                _database =
                    _server.Databases[databaseName];

                if (_database == null)
                {
                    MessageBox.Show(
                        $"Database '{databaseName}' not found.");
                }
                else
                {
                    using (SqlCommand cmd = new SqlCommand("p_UserAssignToProject", sqlConnection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        foreach (string username in userAllocation.UserName)
                        {
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@UserName", username);
                            cmd.Parameters.AddWithValue("@ProjectAllocation", userAllocation.ProjectAllocation);
                            cmd.Parameters.AddWithValue("@FileName", userAllocation.FileName);
                            cmd.Parameters.AddWithValue("@AdminName", userAllocation.AdminName);
                            cmd.Parameters.AddWithValue("@FilePath", userAllocation.FilePath);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connectionName = serverName.Text.Trim();

            if (!string.IsNullOrEmpty(connectionName))
            {
                try
                {
                    // give the connection to SMO .
                    ServerConnection serverConnection =
                        new ServerConnection(connectionName);

                    serverConnection.LoginSecure = true;

                    // check server name.
                    _server = new Server(serverConnection);

                    _server.ConnectionContext.Connect();

                    dbName.Items.Clear();

                    foreach (Database db in _server.Databases)
                    {
                        dbName.Items.Add(db.Name);
                    }
                    _server.ConnectionContext.Disconnect();

                    MessageBox.Show("databases loaded sucessfully .");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Unable to connect to SQL server \n\n" + ex);
                }
            }
            else
            {
                MessageBox.Show("please provide server credentials ");
            }
        }

        private void dashboard_data_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            DashboardData dashboardData = new DashboardData();
            dashboardData.ServerName = serverName.Text;
            dashboardData.DatabaseName = dbName.Text;
            dashboardData.LoginName = AdminName;
            dashboardData.Show();
        }
    }
}
