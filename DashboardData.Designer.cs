namespace SSMS_TOOL
{
    partial class DashboardData
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
            AssignPerson = new ComboBox();
            FilePath = new ComboBox();
            label1 = new Label();
            Server = new TextBox();
            NameOfDatabase = new TextBox();
            Project = new ComboBox();
            Gotofile = new Button();
            SuspendLayout();
            // 
            // AssignPerson
            // 
            AssignPerson.FormattingEnabled = true;
            AssignPerson.Location = new Point(293, 200);
            AssignPerson.Name = "AssignPerson";
            AssignPerson.Size = new Size(205, 33);
            AssignPerson.TabIndex = 2;
            // 
            // FilePath
            // 
            FilePath.FormattingEnabled = true;
            FilePath.Location = new Point(69, 249);
            FilePath.Name = "FilePath";
            FilePath.Size = new Size(429, 33);
            FilePath.TabIndex = 3;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(140, 25);
            label1.Name = "label1";
            label1.Size = new Size(59, 25);
            label1.TabIndex = 4;
            label1.Text = "label1";
            // 
            // Server
            // 
            Server.Location = new Point(21, 112);
            Server.Name = "Server";
            Server.Size = new Size(253, 31);
            Server.TabIndex = 5;
            // 
            // NameOfDatabase
            // 
            NameOfDatabase.Location = new Point(293, 112);
            NameOfDatabase.Name = "NameOfDatabase";
            NameOfDatabase.Size = new Size(253, 31);
            NameOfDatabase.TabIndex = 8;
            // 
            // Project
            // 
            Project.FormattingEnabled = true;
            Project.Location = new Point(69, 200);
            Project.Name = "Project";
            Project.Size = new Size(205, 33);
            Project.TabIndex = 9;
            Project.SelectedIndexChanged += Project_SelectedIndexChanged;
            // 
            // Gotofile
            // 
            Gotofile.Location = new Point(231, 297);
            Gotofile.Name = "Gotofile";
            Gotofile.Size = new Size(112, 34);
            Gotofile.TabIndex = 10;
            Gotofile.Text = "Gotofile";
            Gotofile.UseVisualStyleBackColor = true;
            Gotofile.Click += Gotofile_Click;
            // 
            // DashboardData
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(566, 387);
            Controls.Add(Gotofile);
            Controls.Add(Project);
            Controls.Add(NameOfDatabase);
            Controls.Add(Server);
            Controls.Add(label1);
            Controls.Add(FilePath);
            Controls.Add(AssignPerson);
            Name = "DashboardData";
            Text = "DashboardData";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox serverNaen;
        private ComboBox comboBox2;
        private ComboBox AssignPerson;
        private ComboBox FilePath;
        private Label label1;
        private TextBox Server;
        private TextBox NameOfDatabase;
        private ComboBox Project;
        private Button Gotofile;
    }
}