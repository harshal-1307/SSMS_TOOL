using Microsoft.Data.SqlClient;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SSMS_TOOL
{
    public partial class DashboardData : Form
    {
        Server _server = new Server();

        public string _serverName, _databaseName, _loginName, LOGIN_NAME;

        public string ServerName
        {
            get
            {
                return _serverName;
            }

            set
            {
                _serverName = value;
                Server.Text = value;
            }
        }

        public string DatabaseName
        {
            get
            {
                return _databaseName;
            }

            set
            {
                _databaseName = value;
                NameOfDatabase.Text = value;
            }
        }
        public string LoginName
        {
            get
            {
                return _loginName;
            }

            set
            {
                _loginName = value;
                LOGIN_NAME = value;
            }
        }

        List<AdminData> FilePath1 = new List<AdminData>();
        List<AdminData> Projects = new List<AdminData>();
        List<AdminData> ProjectPerson = new List<AdminData>();

        public DashboardData()
        {
            InitializeComponent();
            Server.Text = ServerName;
            NameOfDatabase.Text = DatabaseName;
            this.Load += DashboardData_Load;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        public void DashboardData_Load(object sender, EventArgs e)
        {

            label1.Text = $" {LoginName}'s Project Dashboard";
            try
            {
                string connectionString =
                connectionString =
                $"Server={Server.Text}; Database = {NameOfDatabase.Text};" +
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
                        $"select DISTINCT UserName,ProjectAllocation,AdminName,FilePath,Filename from dbo.UserCredenatials where AdminName='{LOGIN_NAME}'", connection))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                FilePath1.Add(new AdminData
                                {
                                    ProjectNames = reader["ProjectAllocation"].ToString(),
                                    FilePath = reader["FilePath"].ToString() + "\\" + reader["Filename"].ToString()
                                });

                                Projects.Add(new AdminData
                                {
                                    ProjectNames = reader["ProjectAllocation"].ToString()
                                });

                                ProjectPerson.Add(new AdminData
                                {
                                    ProjectNames = reader["ProjectAllocation"].ToString(),
                                    UserName = reader["UserName"].ToString()
                                });
                            }
                        }
                    }

                    Project.DataSource = Projects
                       .Select(x => x.ProjectNames)
                       .Distinct()
                       .ToList();
                    Project.SelectedIndex = 0;

                    AssignPerson.DataSource = ProjectPerson
                        .Select(x => x.UserName)
                        .Distinct()
                        .ToList();
                    AssignPerson.SelectedIndex = 0;

                    FilePath.DataSource = FilePath1
                        .Select(x => x.FilePath)
                        .Distinct()
                        .ToList();
                    FilePath.SelectedIndex = 0;

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("error at biding dropdown");
            }
        }

        private void Project_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Project.SelectedIndex == -1)
                return;

            string projectName = Project.Text;

            var person = ProjectPerson
                    .Where(x => x.ProjectNames == projectName)
                    .Select(x => x.UserName)
                    .Distinct()
                    .ToList();
            AssignPerson.DataSource = person;
            AssignPerson.SelectedIndex = 0;


            var FILEPATH = FilePath1
                    .Where(x => x.ProjectNames == projectName)
                    .Select(x => x.FilePath)
                    .Distinct()
                    .ToList();
            FilePath.DataSource = FILEPATH;
            FilePath.SelectedIndex = 0;
        }

        private void Gotofile_Click(object sender, EventArgs e)
        {
            string filePath = FilePath.Text;
            Process.Start(@"C:\Program Files\Notepad++\notepad++.exe", filePath);
        }

    }
}
