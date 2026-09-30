using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Sdk.Sfc;
using Microsoft.SqlServer.Management.Smo;
using System.Collections.Specialized;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO.MemoryMappedFiles;
using System.Management;
using System.Text;
using System.Text.RegularExpressions;
using System.Timers;
using System.Windows.Forms;
using static Azure.Core.HttpHeader;
using static System.ComponentModel.Design.ObjectSelectorEditor;
using Smoview= Microsoft.SqlServer.Management.Smo.View;

namespace SSMS_TOOL
{
    public partial class Form1 : Form
    {
        private string SaveFilename;
        private Server? _server;
        private Database? _database;

        private System.Windows.Forms.Timer? timer;
        public Form1()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            comboBox1.SelectedItem = "G+ to COB";
            selectScript.Hide();
            SelectQueryInput.Hide();
            //timer = new System.Windows.Forms.Timer();
            //timer.Interval = 5000;
            //timer.Tick += timer_Tick;
            //timer.Start();
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
                    comboBox2.DataSource = AdminNames
                        .Select(x => x.ProjectNames)
                        .Distinct()
                        .ToList();
                    comboBox2.SelectedIndex = 0;

                    FolderPath.DataSource = FilePath
                        .Select(x => x.FilePath)
                        .Distinct()
                        .ToList();
                    FolderPath.SelectedIndex = 0;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("error at biding dropdown");
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex == -1)
                return;

            string SelectProject = comboBox2.Text;

            var admins = AdminNames
                .Where(x => x.ProjectNames == SelectProject)
                .Select(x => x.AdminNames)
                .Distinct()
                .ToList();
            adminName.DataSource = admins;
            adminName.SelectedIndex = 0;

            if (FolderPath.SelectedIndex == -1)
                return;
            string SelectedProjectFolderPath = comboBox2.Text;

            var FileFolderPath = FilePath
                .Where(x => x.ProjectNames == SelectedProjectFolderPath)
                .Select(x => x.FilePath)
                .Distinct()
                .ToList();
            FolderPath.DataSource = FileFolderPath;
            FolderPath.SelectedIndex = 0;
        }


        private void AddDeclareVar_Click(object sender, EventArgs e)
        {
            string data = DeclareTextbox.Text;
            string value = Value.Text;
            TextArea.AppendText(Environment.NewLine);
            TextArea.AppendText("set " + '@' + data + '=' + $"\"{value}\"" + Environment.NewLine);
        }

        private void remove_Click(object sender, EventArgs e)
        {
            DeclareTextbox.Text = "";
            Value.Text = "";
        }


        private void connect_Click(object sender, EventArgs e)
        {
            //string serverName = "thwdbd10.smtbusa.us,4433";
            //string databaseName = "CLIENTONBOARDING_GCR2_DM";

            string proceduresData = sp_input.Text;
            string TablesData = tbl_input.Text;

            //if (!string.IsNullOrEmpty(proceduresData) || !string.IsNullOrEmpty(TablesData))
            //{
            //    
            //}
            //else
            //{
            //    MessageBox.Show("Please provide inputs.");
            //}

            try
            {
                if (!string.IsNullOrEmpty(NameOfServer.Text) && !string.IsNullOrEmpty(dbName.Text))
                {
                    string connectionString =
                    connectionString =
                    $"Server={NameOfServer.Text}; Database = {dbName.Text};" +
                    "Integrated Security=True;" +
                    "TrustServerCertificate=True;";
                    string TextAreaContent = TextArea.Text.Trim();

                    List<string> exisitingObjects = new List<string>();
                    List<string> NewObjectsContent = new List<string>();

                    if (!string.IsNullOrEmpty(proceduresData) || !string.IsNullOrEmpty(TablesData))
                    {
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
                                _server.Databases[dbName.Text];

                            if (_database == null)
                            {
                                MessageBox.Show(
                                    $"Database '{dbName.Text}' not found.");

                                return;
                            }
                            else
                            {
                                StringBuilder s = new StringBuilder();
                                StringCollection script = new StringCollection();
                                StringCollection dropScript = new StringCollection();
                                StringCollection Procedures = new StringCollection();
                                StringCollection _storedProcedure1 = new StringCollection();
                                StringCollection Functions = new StringCollection();
                                StringCollection View = new StringCollection();

                                Table _table = new Table();
                                StoredProcedure? _storedProcedure = new StoredProcedure();
                                UserDefinedFunction userDefinedFunction = new UserDefinedFunction();

                                ScriptingOptions options = new ScriptingOptions
                                {
                                    ScriptDrops = false,
                                    ScriptSchema = true,
                                    ScriptData = false,
                                    NoCollation = true,
                                    DriAll = true,
                                    IncludeIfNotExists = true
                                };

                                ScriptingOptions dropoptions = new ScriptingOptions
                                {
                                    ScriptDrops = true,
                                    NoCollation = true
                                };

                                ScriptingOptions FunOption = new ScriptingOptions
                                {
                                    ScriptDrops = false,
                                    ScriptSchema = true,
                                    ScriptData = false,
                                    IncludeIfNotExists = true
                                };
                                //Procedures Script
                                string result = string.Empty;

                                //string ProceduresName = sp_input.Text;
                                //List<string> _storedProcedureList =
                                //    ProceduresName
                                //    .Split(",", StringSplitOptions.RemoveEmptyEntries)
                                //    .Select(x => x.Trim())
                                //    .Where(x => !string.IsNullOrWhiteSpace(x))
                                //    .ToList();

                                StringBuilder FinalstringBuilder = new StringBuilder();

                                StringBuilder Ifexist = new StringBuilder();

                                StringBuilder CreatePart = new StringBuilder();
                                bool insideCreate = false;


                                //checking duplicate data in textarea .

                                string[] ProFunViewNames = proceduresData
                                        .Split(",", StringSplitOptions.RemoveEmptyEntries)
                                        .Select(x => x.Trim())
                                        .ToArray();

                                string normalizedContent = Regex.Replace(
                                    TextAreaContent, @"\s+", " "
                                    );

                                foreach (string name in ProFunViewNames)
                                {
                                    _storedProcedure = _database.StoredProcedures[name, "dbo"];
                                    userDefinedFunction = _database.UserDefinedFunctions[name, "dbo"];
                                    Smoview? view = _database.Views[name, "dbo"];

                                    if (_storedProcedure != null)
                                    {
                                        Procedures = _storedProcedure.Script(options);
                                        if (normalizedContent.Contains($"CREATE PROCEDURE [dbo].[{name}]", StringComparison.OrdinalIgnoreCase))
                                        {
                                            exisitingObjects.Add($"procedure : {name}");
                                        }
                                        else
                                        {
                                            NewObjectsContent.Add(name);
                                        }
                                    }
                                    else if (view != null)
                                    {
                                        Procedures = view.Script(options);
                                        if (normalizedContent.Contains($"CREATE VIEW [dbo].[{name}]", StringComparison.OrdinalIgnoreCase))
                                        {
                                            exisitingObjects.Add($"View : {name}");
                                        }
                                        else
                                        {
                                            NewObjectsContent.Add(name);
                                        }
                                    }
                                    else if (userDefinedFunction != null)
                                    {
                                        Procedures = userDefinedFunction.Script(options);
                                        if (normalizedContent.Contains($"CREATE FUNCTION [dbo].[{name}]", StringComparison.OrdinalIgnoreCase)
                                            || normalizedContent.Contains($"CREATE FUNCTION dbo.{name}", StringComparison.OrdinalIgnoreCase))
                                        {
                                            exisitingObjects.Add($"Function : {name}");
                                        }
                                        else
                                        {
                                            NewObjectsContent.Add(name);
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show($"provide {name} input is invalid and not present in DB.");
                                        // return;
                                    }
                                }

                                if (NewObjectsContent.Count > 0)
                                {
                                    foreach (string names in NewObjectsContent)
                                    {

                                        foreach (string data in Procedures)
                                        {
                                            s.AppendLine(data);
                                        }

                                        //replace unNessary data 

                                        if (s.ToString().Contains($"EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[{names}] AS'"))
                                        {
                                            s.Replace($"SET QUOTED_IDENTIFIER ON\r\nIF NOT EXISTS", "SET QUOTED_IDENTIFIER ON\r\nIF EXISTS");
                                            s.Replace($"EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[{names}] AS'"
                                                , $"DROP PROCEDURE [dbo].[{names}]");
                                            //As per existing script class we got script as ALTER , so we replace this by create
                                            s.Replace("ALTER", "\n GO \n CREATE");
                                        }
                                        else if (s.ToString().Contains($"execute dbo.sp_executesql @statement = N"))
                                        {
                                            s.Replace($"SET QUOTED_IDENTIFIER ON\r\nIF NOT EXISTS", "SET QUOTED_IDENTIFIER ON\r\nIF EXISTS");
                                            s.Replace($"execute dbo.sp_executesql @statement = N", $"DROP FUNCTION [dbo].[{names}]\nEND");
                                            s.Replace("CREATE", "GO \n \n CREATE");
                                            s.Replace("''", "'");
                                            //s = new StringBuilder(
                                            //Regex.Replace(s.ToString(), @"'\s*END", "", RegexOptions.IgnoreCase)); //
                                            s = new StringBuilder(
                                                Regex.Replace(s.ToString(), @"END\s*'", "END \n", RegexOptions.IgnoreCase)); //'\s*END|

                                        }
                                        else if (s.ToString().Contains($"EXEC dbo.sp_executesql @statement = N"))
                                        {
                                            s.Replace($"SET QUOTED_IDENTIFIER ON\r\nIF NOT EXISTS", "SET QUOTED_IDENTIFIER ON\r\nIF EXISTS");
                                            s.Replace($"EXEC dbo.sp_executesql @statement = N", $"BEGIN \n DROP View [dbo].[{names}] \n END \n GO");
                                            result = s.ToString();
                                            result = string.Join(Environment.NewLine,
                                                result.Split(Environment.NewLine)
                                                .Where(l => l.Trim() != "'"));
                                            s.Clear();
                                            s.AppendLine(result);
                                        }
                                    }

                                }

                                //Procedures Script


                                // Tables Script

                                //List<string> tables = TableName
                                //    .Split(",", StringSplitOptions.RemoveEmptyEntries)
                                //    .Select(x => x.Trim())
                                //    .Where(x => !string.IsNullOrWhiteSpace(x))
                                //    .ToList()
                                //    ;

                                string TableName = tbl_input.Text;
                                // "CashHubAccountSSI , CashHubAccountSSILog ,  CashHubAccount";

                                string[] tables = TableName
                                        .Split(",", StringSplitOptions.RemoveEmptyEntries)
                                        .Select(x => x.Trim())
                                        .ToArray();


                                string normalizedTablesContent = Regex.Replace(
                                                TextAreaContent, @"\s+", " "
                                                );


                                foreach (string tablesName in tables)
                                {
                                    string tbale = tablesName.Trim();
                                    _table = _database.Tables[tbale, "dbo"];

                                    if (_table == null)
                                    {
                                        MessageBox.Show($"The foliowing table is not present in db \n" + string.Join("\n", tbale));
                                        //return;
                                    }
                                    else
                                    {
                                        if (normalizedTablesContent.Contains($"CREATE TABLE [dbo].[{tablesName}]", StringComparison.OrdinalIgnoreCase))
                                        {
                                            exisitingObjects.Add($"Table : {tablesName}");
                                        }
                                        else
                                        {
                                            NewObjectsContent.Add(tablesName);
                                            script = _table.Script(options);
                                            //dropScript = _table.Script(dropoptions);

                                            // StringBuilder for combine all data .
                                            foreach (string Dropline in dropScript)
                                            {
                                                s.AppendLine(Dropline);
                                            }

                                            foreach (string line in script)
                                            {
                                                s.AppendLine();
                                                s.AppendLine(line);
                                                s.AppendLine("GO");
                                            }

                                            //Another way to combine script .
                                            // s.Append(string.Join(Environment.NewLine, script.Cast<string>()));

                                        }
                                    }
                                }

                                // Tables Script

                                TextArea.AppendText(s.ToString());
                                TextArea.AppendText(Environment.NewLine);


                                if (exisitingObjects.Count > 0)
                                {
                                    if (NewObjectsContent.Count > 0)
                                    {
                                        MessageBox.Show("The following objectd already exist in .sql files\n"
                                                               + string.Join("\n", exisitingObjects) + "\n" +
                                        "The following objects are printed \n" + string.Join("\n", NewObjectsContent));
                                    }
                                    else
                                    {
                                        MessageBox.Show("The following objectd already exist in .sql files\n"
                                       + string.Join("\n", exisitingObjects));
                                    }

                                }


                                // this for single table script 

                                //string.Join(Environment.NewLine, dropScript.Cast<String>())
                                //+ Environment.NewLine
                                // "GO" + Environment.NewLine
                                //+ string.Join(Environment.NewLine, script.Cast<String>());

                                //MessageBox.Show(
                                //    $"Connected successfully!\n\n" +
                                //    $"Server: {_server.Name}\n" +
                                //    $"Database: {_database.Name}\n" +
                                //    $"Version: {serverVersion}");
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(
                                $"Connection failed:\n\n{ex.Message}");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please provide inputs.");
                    }
                }
                else
                {
                    MessageBox.Show("please provide server credentials ");
                }
            }
            catch (IOException ex)
            {

            }


        }

        public string CreateVariablesScript(string selectQuery)
        {

            Match Match = Regex.Match
            (
            selectQuery, @"SELECT\s+(\w+)\s+FROM\s+(\w+)(.*)", RegexOptions.IgnoreCase | RegexOptions.Singleline
            );

            Match MatchDecVar = Regex.Match
            (
            selectQuery, @"^\s*SELECT\s+(@[A-Za-z_]\w*)\s*=+[A-Za-z_]\w*\s+FROM\s+(\w+)(.*)", RegexOptions.IgnoreCase | RegexOptions.Singleline
            );

            //=\s * ([A - Za - z_]\w *)\s + FROM\s + (\w +)(.*)

            StringBuilder stringBuilder = new StringBuilder();

            if (Match.Success)
            {
                string columnName = Match.Groups[1].Value;
                string tableName = Match.Groups[2].Value;
                string remainingPart = Match.Groups[3].Value.Trim();
                Table table = new Table();
                Column column = new Column();

                if (!string.IsNullOrEmpty(NameOfServer.Text) && !string.IsNullOrEmpty(dbName.Text))
                {
                    string connectionString =
                        connectionString =
                        $"Server={NameOfServer.Text}; Database = {dbName.Text};" +
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
                            _server.Databases[dbName.Text];

                        if (_database == null)
                        {
                            MessageBox.Show(
                                $"Database '{dbName.Text}' not found.");
                        }
                        else
                        {

                            table = _database.Tables[tableName, "dbo"];
                            column = table.Columns[columnName];
                            string dataType = column.DataType.Name;
                            string varName = "@V_" + columnName;
                            stringBuilder.AppendLine
                            (
                            $"DECLARE {varName} {dataType} " + ";" + Environment.NewLine +
                            $"SELECT {varName} = {columnName} FROM {table} {remainingPart}"
                            );

                        }
                    }
                    catch (Exception ex)
                    {

                    }
                }
                else
                {
                    MessageBox.Show("please provide server credentials ");
                }
            }
            else if (MatchDecVar.Success)
            {
                stringBuilder.AppendLine(selectQuery.ToString());
            }
            return stringBuilder.ToString();


        }

        private void selectScript_Click(object sender, EventArgs e)
        {
            String InputselectQuery = SelectQueryInput.Text;

            List<string> MultiSelect = InputselectQuery
                .Split(';', StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Trim())
                .Where(x => x.StartsWith("SELECT", StringComparison.OrdinalIgnoreCase))
                .ToList();
            string result = string.Empty;
            foreach (string selectQuery in MultiSelect)
            {
                result = CreateVariablesScript(selectQuery);
                TextArea.AppendText(result);
                TextArea.AppendText(Environment.NewLine);
            }

        }


        private void AuotTablesScript_Click(object sender, EventArgs e)
        {
            string filepath = string.Empty;
            UserAllocation userAllocation = new UserAllocation();
            StringBuilder s1 = new StringBuilder();

            using (OpenFileDialog fileDialog = new OpenFileDialog())
            {
                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    filepath = fileDialog.FileName;


                    using StreamReader reader = new StreamReader(filepath);
                    string? headerline = reader.ReadLine();
                    string[] headers = headerline.Split("|");
                    string[] detectedTypes = new string[headers.Length];

                    bool[] nullable = new bool[headers.Length];

                    Array.Fill(detectedTypes, "UNKNOWN");
                    string? line;

                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] values = line.Split("|");
                        for (int i = 0; i < headers.Length && i < values.Length; i++)
                        {
                            string value = values[i].Trim();

                            if (string.IsNullOrEmpty(value))
                            {
                                nullable[i] = true;
                                continue;
                            }

                            detectedTypes[i] =
                                GetFinalType(
                                detectedTypes[i],
                                DetectType(value)
                                );
                        }
                    }


                    s1.AppendLine
                        (
                        $"CREATE TABLE [dbo].{StagingTable.Text}" + Environment.NewLine
                        + "("
                        + Environment.NewLine
                        + StagingTable.Text + "_Id" + " INT IDENTITY(1,1) NOT NULL ,"
                        ).ToString();
                    for (int i = 0; i < headers.Length; i++)
                    {
                        string nullType = nullable[i] ? "NULL" : "NOT NULL";
                        //Console.WriteLine($"{detectedTypes[i]}");
                        s1.AppendLine($"[{headers[i]}]  {detectedTypes[i]} {nullType} ,");
                    }
                    s1.AppendLine(")");
                    TextArea.AppendText(s1.ToString());
                    TextArea.AppendText(Environment.NewLine);
                }
            }

        }

        public static string DetectType(string value)
        {
            // For Date Formt
            string[] formats = { "dd/MM/yyyy", "yyyy-MM-dd", "MM/dd/yyyy", "dd-MM-yyyy" };

            if (int.TryParse(value, out _))
                return "INT";
            if (long.TryParse(value, out _))
                return "BIGINT";

            if (DateTime.TryParseExact(value, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
                return "DATE";

            if (bool.TryParse(value, out _))
                return "BIT";

            return "VARCHAR(255)";
        }

        public static string GetFinalType(string oldType, string newType)
        {
            if (oldType == "UNKNOWN")
                return newType;

            if (oldType == newType)
                return oldType;

            if ((oldType == "INT") && (newType == "BIGINT") ||
                (oldType == "BIGINT") && (newType == "INT"))
                return "BIGINT";

            if (IsNumber(oldType) && newType == "DECIMAL")
                return "DECIMAL(18,2)";

            return "VARCHAR(100)";
        }
        public static bool IsNumber(string type)
        {
            return type == "INT" || type == "BIGINT" || type == "DECIMAL";
        }

        private void addComment_Click(object sender, EventArgs e)
        {
            string values = comboBox1.SelectedItem?.ToString();
            string input = inputComment.Text;
            TextArea.AppendText($"------------------------------------" + input + " " + values + "------------------------------------" + Environment.NewLine);
            TextArea.AppendText(Environment.NewLine);
        }

        private void ClearText_Click(object sender, EventArgs e)
        {
            TextArea.SelectedText = "";
        }

        private void save_Click(object sender, EventArgs e)
        {
            string filepath = FolderPath.Text + ".sql";

            string proceduresData = sp_input.Text;
            string TablesData = tbl_input.Text;

            try
            {
                if (!string.IsNullOrEmpty(filepath))
                { 
                   using (FileStream stream = new FileStream(
                            filepath, FileMode.Append, FileAccess.Write, FileShare.Read))

                        using (StreamWriter writer = new StreamWriter(stream))
                        {
                            writer.WriteLine();
                            writer.WriteLine(TextArea.Text);
                        }
                        MessageBox.Show("File saved successfully");
                        
                    //if (!string.IsNullOrEmpty(proceduresData) || !string.IsNullOrEmpty(TablesData))
                    //{
                    //    string existingContent = File.ReadAllText(filepath);
                    //    string NewContent = TextArea.Text;

                    //    List<string> exisitingObjects = new List<string>();
                    //    List<string> NewObjectsContent = new List<string>();

                    //    string[] ProFunViewNames = proceduresData
                    //     .Split(",", StringSplitOptions.RemoveEmptyEntries)
                    //     .Select(x => x.Trim())
                    //     .ToArray();

                    //    string[] MultipleTables = TablesData
                    //        .Split(",", StringSplitOptions.RemoveEmptyEntries)
                    //        .Select(x => x.Trim())
                    //        .ToArray();

                    //    foreach (string tablesName in MultipleTables)
                    //    {
                    //        if (existingContent.Contains($"CREATE TABLE [dbo].[{tablesName}]", StringComparison.OrdinalIgnoreCase))
                    //        {
                    //            exisitingObjects.Add($"Table : {tablesName}");
                    //        }
                    //    }

                    //    foreach (string name in ProFunViewNames)
                    //    {
                    //        if (existingContent.Contains($"CREATE  PROCEDURE [dbo].[{name}]", StringComparison.OrdinalIgnoreCase))
                    //        {
                    //            exisitingObjects.Add($"procedure : {name}");
                    //        }
                    //        if (existingContent.Contains($"CREATE FUNCTION dbo.{name}", StringComparison.OrdinalIgnoreCase))
                    //        {
                    //            exisitingObjects.Add($"Function : {name}");
                    //        }
                    //        if (existingContent.Contains($"CREATE VIEW [dbo].[{name}]", StringComparison.OrdinalIgnoreCase))
                    //        {
                    //            exisitingObjects.Add($"View : {name}");
                    //        }
                    //    }

                    //    if (exisitingObjects.Count == 0)
                    //    {
                    //        if (!string.IsNullOrEmpty(filepath))
                    //        {
                    //            int MaxAttempts = 5;

                    //            for (int attempt = 0; attempt <= MaxAttempts; attempt++)
                    //            {
                    //                try
                    //                {
                    //                    using (FileStream stream = new FileStream(
                    //                        filepath, FileMode.Append, FileAccess.Write, FileShare.Read))

                    //                    using (StreamWriter writer = new StreamWriter(stream))
                    //                    {
                    //                        writer.WriteLine();
                    //                        writer.WriteLine(TextArea.Text);
                    //                    }
                    //                    MessageBox.Show("File saved successfully");
                    //                    return;
                    //                }
                    //                catch (IOException ex)
                    //                {
                    //                    if (attempt == MaxAttempts)
                    //                    {
                    //                        MessageBox.Show("Unable to write to file .\n" + ex.Message);
                    //                        return;
                    //                    }
                    //                    Thread.Sleep(500);
                    //                }
                    //            }
                    //        }
                    //        else
                    //        {
                    //            MessageBox.Show("Please select folder path ");
                    //        }
                    //    }

                    //    if (exisitingObjects.Count > 0)
                    //    {
                    //        MessageBox.Show("The following objectd already exist in .sql files\n\n"
                    //            + string.Join("\n", exisitingObjects));
                    //    }

                    //}
                    //else
                    //{
                    //    MessageBox.Show("Please provide inputs.");
                    //}
                }
                else
                {
                    MessageBox.Show("Please choose file.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void connect1_Click(object sender, EventArgs e)
        {
            string connectionName = NameOfServer.Text.Trim();

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

                    BindProjectDropDonw(connectionName, dbName.Text);

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

        private void login_Click(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
        }


        private void openfile_Click(object sender, EventArgs e)
        {
            try
            {
                string filePath = FolderPath.Text;
                if (!string.IsNullOrEmpty(filePath))
                {
                    Process.Start(@"C:\Program Files\Notepad++\notepad++.exe", filePath + ".sql");
                }
                else
                {
                    MessageBox.Show("Please select file first.");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("unable to process" + ex.Message);
            }

        }

        private void modify_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(FolderPath.Text))
            {
                MessageBox.Show("please verify provided folder path.");
            }
            else
            {
                string file = FolderPath.Text + ".sql";
                string FileContent = File.ReadAllText(file);
                if (!string.IsNullOrEmpty(TextArea.Text) && File.Exists(file) && !string.IsNullOrWhiteSpace(FileContent))
                {
                    Compare compare = new Compare();
                    compare.MainFilePath = file;
                    compare.TextAreaData = TextArea.Text.Trim();
                    compare.FileFolderPath = FolderPath.Text.Trim();
                    compare.Show();
                }
                else if (string.IsNullOrWhiteSpace(TextArea.Text))
                {
                    MessageBox.Show("Input area is empty.");
                }
                else
                {
                    MessageBox.Show("file is empty");
                }
            }
                
        }
    }

}
