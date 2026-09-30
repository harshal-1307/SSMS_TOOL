namespace SSMS_TOOL
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Value = new TextBox();
            tableLayoutPanel1 = new TableLayoutPanel();
            DDValue = new Label();
            DeclareTextbox = new TextBox();
            DeclarationData = new Label();
            TextArea = new RichTextBox();
            AddDeclareVar = new Button();
            remove = new Button();
            Getdata = new Button();
            sp_input = new TextBox();
            tbl_input = new TextBox();
            AuotTablesScript = new Button();
            StagingTable = new TextBox();
            selectScript = new Button();
            SelectQueryInput = new TextBox();
            comboBox1 = new ComboBox();
            addComment = new Button();
            ClearText = new Button();
            inputComment = new TextBox();
            save = new Button();
            comboBox2 = new ComboBox();
            adminName = new ComboBox();
            FolderPath = new ComboBox();
            connect1 = new Button();
            dbName = new ComboBox();
            NameOfServer = new TextBox();
            login = new Button();
            openfile = new Button();
            modify = new Button();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // Value
            // 
            Value.Location = new Point(152, 32);
            Value.Name = "Value";
            Value.Size = new Size(293, 31);
            Value.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 66.6666641F));
            tableLayoutPanel1.Controls.Add(Value, 1, 1);
            tableLayoutPanel1.Controls.Add(DDValue, 1, 0);
            tableLayoutPanel1.Controls.Add(DeclareTextbox, 0, 1);
            tableLayoutPanel1.Controls.Add(DeclarationData, 0, 0);
            tableLayoutPanel1.Location = new Point(12, 45);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 42.64706F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 57.35294F));
            tableLayoutPanel1.Size = new Size(448, 68);
            tableLayoutPanel1.TabIndex = 1;
            // 
            // DDValue
            // 
            DDValue.AutoSize = true;
            DDValue.Location = new Point(152, 0);
            DDValue.Name = "DDValue";
            DDValue.Size = new Size(54, 25);
            DDValue.TabIndex = 3;
            DDValue.Text = "Value";
            // 
            // DeclareTextbox
            // 
            DeclareTextbox.Location = new Point(3, 32);
            DeclareTextbox.Name = "DeclareTextbox";
            DeclareTextbox.Size = new Size(143, 31);
            DeclareTextbox.TabIndex = 2;
            // 
            // DeclarationData
            // 
            DeclarationData.AutoSize = true;
            DeclarationData.Location = new Point(3, 0);
            DeclarationData.Name = "DeclarationData";
            DeclarationData.Size = new Size(138, 25);
            DeclarationData.TabIndex = 4;
            DeclarationData.Text = "DeclarationData";
            DeclarationData.TextAlign = ContentAlignment.TopCenter;
            // 
            // TextArea
            // 
            TextArea.Location = new Point(12, 367);
            TextArea.Name = "TextArea";
            TextArea.Size = new Size(758, 424);
            TextArea.TabIndex = 2;
            TextArea.Text = "";
            // 
            // AddDeclareVar
            // 
            AddDeclareVar.Location = new Point(41, 119);
            AddDeclareVar.Name = "AddDeclareVar";
            AddDeclareVar.Size = new Size(112, 34);
            AddDeclareVar.TabIndex = 4;
            AddDeclareVar.Text = "Add Data";
            AddDeclareVar.UseVisualStyleBackColor = true;
            AddDeclareVar.Click += AddDeclareVar_Click;
            // 
            // remove
            // 
            remove.Location = new Point(196, 119);
            remove.Name = "remove";
            remove.Size = new Size(112, 34);
            remove.TabIndex = 5;
            remove.Text = "remove";
            remove.UseVisualStyleBackColor = true;
            remove.Click += remove_Click;
            // 
            // Getdata
            // 
            Getdata.Location = new Point(658, 119);
            Getdata.Name = "Getdata";
            Getdata.Size = new Size(112, 34);
            Getdata.TabIndex = 6;
            Getdata.Text = "GetData";
            Getdata.UseVisualStyleBackColor = true;
            Getdata.Click += connect_Click;
            // 
            // sp_input
            // 
            sp_input.Location = new Point(497, 45);
            sp_input.Name = "sp_input";
            sp_input.PlaceholderText = "Enter procedure,function,view names here";
            sp_input.Size = new Size(445, 31);
            sp_input.TabIndex = 7;
            // 
            // tbl_input
            // 
            tbl_input.Location = new Point(497, 82);
            tbl_input.Name = "tbl_input";
            tbl_input.PlaceholderText = "Enter table names here";
            tbl_input.Size = new Size(445, 31);
            tbl_input.TabIndex = 8;
            // 
            // AuotTablesScript
            // 
            AuotTablesScript.Location = new Point(727, 230);
            AuotTablesScript.Name = "AuotTablesScript";
            AuotTablesScript.Size = new Size(169, 34);
            AuotTablesScript.TabIndex = 9;
            AuotTablesScript.Text = "AutoTableScript";
            AuotTablesScript.UseVisualStyleBackColor = true;
            AuotTablesScript.Click += AuotTablesScript_Click;
            // 
            // StagingTable
            // 
            StagingTable.Location = new Point(678, 182);
            StagingTable.Name = "StagingTable";
            StagingTable.PlaceholderText = "Enter auto creation table name";
            StagingTable.Size = new Size(264, 31);
            StagingTable.TabIndex = 10;
            // 
            // selectScript
            // 
            selectScript.Location = new Point(796, 767);
            selectScript.Name = "selectScript";
            selectScript.Size = new Size(109, 34);
            selectScript.TabIndex = 11;
            selectScript.Text = "selectScript";
            selectScript.UseVisualStyleBackColor = true;
            selectScript.Click += selectScript_Click;
            // 
            // SelectQueryInput
            // 
            SelectQueryInput.Location = new Point(796, 728);
            SelectQueryInput.Name = "SelectQueryInput";
            SelectQueryInput.PlaceholderText = "Enter your select query here";
            SelectQueryInput.Size = new Size(109, 31);
            SelectQueryInput.TabIndex = 12;
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "G+ to COB", "Businees to COB", "COB to BaNC'S", "BaNC'S to COB", "COB to CDS" });
            comboBox1.Location = new Point(373, 219);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(264, 33);
            comboBox1.TabIndex = 13;
            // 
            // addComment
            // 
            addComment.Location = new Point(439, 258);
            addComment.Name = "addComment";
            addComment.Size = new Size(140, 34);
            addComment.TabIndex = 14;
            addComment.Text = "Add comment";
            addComment.UseVisualStyleBackColor = true;
            addComment.Click += addComment_Click;
            // 
            // ClearText
            // 
            ClearText.Location = new Point(776, 404);
            ClearText.Name = "ClearText";
            ClearText.Size = new Size(160, 34);
            ClearText.TabIndex = 15;
            ClearText.Text = "Clear";
            ClearText.UseVisualStyleBackColor = true;
            ClearText.Click += ClearText_Click;
            // 
            // inputComment
            // 
            inputComment.Location = new Point(373, 182);
            inputComment.Name = "inputComment";
            inputComment.PlaceholderText = "Enter your project comment";
            inputComment.Size = new Size(264, 31);
            inputComment.TabIndex = 16;
            // 
            // save
            // 
            save.Location = new Point(776, 367);
            save.Name = "save";
            save.Size = new Size(160, 31);
            save.TabIndex = 17;
            save.Text = "MoveToMain";
            save.UseVisualStyleBackColor = true;
            save.Click += save_Click;
            // 
            // comboBox2
            // 
            comboBox2.FormattingEnabled = true;
            comboBox2.Location = new Point(12, 328);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new Size(206, 33);
            comboBox2.TabIndex = 18;
            comboBox2.SelectedIndexChanged += comboBox2_SelectedIndexChanged;
            // 
            // adminName
            // 
            adminName.FormattingEnabled = true;
            adminName.Location = new Point(224, 328);
            adminName.Name = "adminName";
            adminName.Size = new Size(209, 33);
            adminName.TabIndex = 19;
            // 
            // FolderPath
            // 
            FolderPath.FormattingEnabled = true;
            FolderPath.Location = new Point(439, 328);
            FolderPath.Name = "FolderPath";
            FolderPath.Size = new Size(497, 33);
            FolderPath.TabIndex = 20;
            // 
            // connect1
            // 
            connect1.Location = new Point(91, 258);
            connect1.Name = "connect1";
            connect1.Size = new Size(141, 34);
            connect1.TabIndex = 21;
            connect1.Text = "connect";
            connect1.UseVisualStyleBackColor = true;
            connect1.Click += connect1_Click;
            // 
            // dbName
            // 
            dbName.FormattingEnabled = true;
            dbName.Location = new Point(15, 219);
            dbName.Name = "dbName";
            dbName.Size = new Size(293, 33);
            dbName.TabIndex = 22;
            // 
            // NameOfServer
            // 
            NameOfServer.Location = new Point(15, 182);
            NameOfServer.Name = "NameOfServer";
            NameOfServer.PlaceholderText = "Enter server name here";
            NameOfServer.Size = new Size(293, 31);
            NameOfServer.TabIndex = 23;
            // 
            // login
            // 
            login.Location = new Point(796, 5);
            login.Name = "login";
            login.Size = new Size(146, 34);
            login.TabIndex = 24;
            login.Text = "login";
            login.UseVisualStyleBackColor = true;
            login.Click += login_Click;
            // 
            // openfile
            // 
            openfile.Location = new Point(776, 291);
            openfile.Name = "openfile";
            openfile.Size = new Size(160, 31);
            openfile.TabIndex = 28;
            openfile.Text = "OpenBelowFile";
            openfile.UseVisualStyleBackColor = true;
            openfile.Click += openfile_Click;
            // 
            // modify
            // 
            modify.Location = new Point(658, 291);
            modify.Name = "modify";
            modify.Size = new Size(112, 34);
            modify.TabIndex = 29;
            modify.Text = "Modify";
            modify.UseVisualStyleBackColor = true;
            modify.Click += modify_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(948, 813);
            Controls.Add(modify);
            Controls.Add(openfile);
            Controls.Add(login);
            Controls.Add(NameOfServer);
            Controls.Add(dbName);
            Controls.Add(connect1);
            Controls.Add(FolderPath);
            Controls.Add(adminName);
            Controls.Add(comboBox2);
            Controls.Add(save);
            Controls.Add(inputComment);
            Controls.Add(ClearText);
            Controls.Add(addComment);
            Controls.Add(comboBox1);
            Controls.Add(SelectQueryInput);
            Controls.Add(selectScript);
            Controls.Add(StagingTable);
            Controls.Add(AuotTablesScript);
            Controls.Add(tbl_input);
            Controls.Add(sp_input);
            Controls.Add(Getdata);
            Controls.Add(remove);
            Controls.Add(AddDeclareVar);
            Controls.Add(TextArea);
            Controls.Add(tableLayoutPanel1);
            Name = "Form1";
            Text = "Form1";
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox Value;
        private TableLayoutPanel tableLayoutPanel1;
        private TextBox DeclareTextbox;
        private Label DDValue;
        private Label DeclarationData;
        private RichTextBox TextArea;
        private Button AddDeclareVar;
        private Button remove;
        private Button Getdata;
        private TextBox sp_input;
        private TextBox tbl_input;
        private Button AuotTablesScript;
        private TextBox StagingTable;
        private Button selectScript;
        private TextBox SelectQueryInput;
        private ComboBox comboBox1;
        private Button addComment;
        private Button ClearText;
        private TextBox inputComment;
        private Button save;
        private ComboBox comboBox2;
        private ComboBox ProjectnamesProjectNames;
        private ComboBox comboBox3;
        private ComboBox adminName;
        private ComboBox FolderPath;
        private Button connect1;
        private ComboBox dbName;
        private TextBox NameOfServer;
        private Button login;
        private Button openfile;
        private Button fileopen;
        private Button modify;
    }
}
