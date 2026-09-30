namespace SSMS_TOOL
{
    partial class FileSaveByAdmin
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            mainFolder = new Button();
            creation = new Button();
            filename = new TextBox();
            save = new Button();
            cancel = new Button();
            projectName = new TextBox();
            assingToWhom = new TextBox();
            dbName = new ComboBox();
            connectToDb = new Button();
            serverName = new TextBox();
            dashboard = new Button();
            label1 = new Label();
            adminName = new Label();
            dashboard_data = new LinkLabel();
            SuspendLayout();
            // 
            // mainFolder
            // 
            mainFolder.Location = new Point(163, 385);
            mainFolder.Name = "mainFolder";
            mainFolder.Size = new Size(182, 34);
            mainFolder.TabIndex = 0;
            mainFolder.Text = "Create Main Folder";
            mainFolder.UseVisualStyleBackColor = true;
            mainFolder.Click += mainFolder_Click;
            // 
            // creation
            // 
            creation.Location = new Point(376, 385);
            creation.Name = "creation";
            creation.Size = new Size(112, 34);
            creation.TabIndex = 1;
            creation.Text = "Creation";
            creation.UseVisualStyleBackColor = true;
            creation.Click += creation_Click;
            // 
            // filename
            // 
            filename.Enabled = false;
            filename.Location = new Point(163, 284);
            filename.Name = "filename";
            filename.PlaceholderText = "Enter file name for your project";
            filename.Size = new Size(325, 31);
            filename.TabIndex = 2;
            // 
            // save
            // 
            save.Enabled = false;
            save.Font = new Font("Segoe UI", 9F);
            save.Location = new Point(217, 321);
            save.Name = "save";
            save.Size = new Size(98, 35);
            save.TabIndex = 4;
            save.Text = "save";
            save.UseVisualStyleBackColor = true;
            save.Click += save_Click;
            // 
            // cancel
            // 
            cancel.Enabled = false;
            cancel.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cancel.Location = new Point(321, 321);
            cancel.Name = "cancel";
            cancel.Size = new Size(98, 35);
            cancel.TabIndex = 5;
            cancel.Text = "cancel";
            cancel.UseVisualStyleBackColor = true;
            cancel.Click += cancel_Click;
            // 
            // projectName
            // 
            projectName.Enabled = false;
            projectName.Location = new Point(163, 210);
            projectName.Name = "projectName";
            projectName.PlaceholderText = "Enter project name";
            projectName.Size = new Size(325, 31);
            projectName.TabIndex = 6;
            // 
            // assingToWhom
            // 
            assingToWhom.Enabled = false;
            assingToWhom.Location = new Point(163, 247);
            assingToWhom.Name = "assingToWhom";
            assingToWhom.PlaceholderText = "Project assign to whom";
            assingToWhom.Size = new Size(325, 31);
            assingToWhom.TabIndex = 7;
            // 
            // dbName
            // 
            dbName.FormattingEnabled = true;
            dbName.Location = new Point(304, 91);
            dbName.Name = "dbName";
            dbName.Size = new Size(336, 33);
            dbName.TabIndex = 9;
            // 
            // connectToDb
            // 
            connectToDb.Font = new Font("Segoe UI", 9F);
            connectToDb.Location = new Point(238, 141);
            connectToDb.Name = "connectToDb";
            connectToDb.Size = new Size(126, 39);
            connectToDb.TabIndex = 11;
            connectToDb.Text = "connect ";
            connectToDb.UseVisualStyleBackColor = true;
            connectToDb.Click += button1_Click;
            // 
            // serverName
            // 
            serverName.Location = new Point(38, 91);
            serverName.Multiline = true;
            serverName.Name = "serverName";
            serverName.PlaceholderText = "Enter server name";
            serverName.Size = new Size(242, 33);
            serverName.TabIndex = 12;
            // 
            // dashboard
            // 
            dashboard.Font = new Font("Segoe UI", 9F);
            dashboard.Location = new Point(392, 141);
            dashboard.Name = "dashboard";
            dashboard.Size = new Size(147, 39);
            dashboard.TabIndex = 13;
            dashboard.Text = "LoadDashboard";
            dashboard.UseVisualStyleBackColor = true;
            dashboard.Visible = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F);
            label1.Location = new Point(506, 9);
            label1.Name = "label1";
            label1.Size = new Size(43, 32);
            label1.TabIndex = 14;
            label1.Text = "Hii";
            // 
            // adminName
            // 
            adminName.AutoSize = true;
            adminName.Location = new Point(533, 9);
            adminName.Name = "adminName";
            adminName.Size = new Size(0, 25);
            adminName.TabIndex = 15;
            // 
            // dashboard_data
            // 
            dashboard_data.AutoSize = true;
            dashboard_data.Location = new Point(38, 16);
            dashboard_data.Name = "dashboard_data";
            dashboard_data.Size = new Size(138, 25);
            dashboard_data.TabIndex = 16;
            dashboard_data.TabStop = true;
            dashboard_data.Text = "dashboard data";
            dashboard_data.LinkClicked += dashboard_data_LinkClicked;
            // 
            // FileSaveByAdmin
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(667, 486);
            Controls.Add(dashboard_data);
            Controls.Add(adminName);
            Controls.Add(label1);
            Controls.Add(dashboard);
            Controls.Add(serverName);
            Controls.Add(connectToDb);
            Controls.Add(dbName);
            Controls.Add(assingToWhom);
            Controls.Add(projectName);
            Controls.Add(cancel);
            Controls.Add(save);
            Controls.Add(filename);
            Controls.Add(creation);
            Controls.Add(mainFolder);
            Name = "FileSaveByAdmin";
            Text = "s";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button mainFolder;
        private Button creation;
        private TextBox filename;
        private Button save;
        private Button cancel;
        private TextBox projectName;
        private TextBox assingToWhom;
        private ComboBox dbName;
        private Button connectToDb;
        private TextBox serverName;
        private Button dashboard;
        private Label label1;
        private Label adminName;
        private LinkLabel dashboard_data;
    }
}