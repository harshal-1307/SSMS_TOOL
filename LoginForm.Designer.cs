namespace SSMS_TOOL
{
    partial class LoginForm
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
            username = new TextBox();
            password = new TextBox();
            signup = new Button();
            panel1 = new Panel();
            label1 = new Label();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // username
            // 
            username.Location = new Point(111, 159);
            username.Name = "username";
            username.Size = new Size(313, 31);
            username.TabIndex = 0;
            // 
            // password
            // 
            password.Location = new Point(111, 196);
            password.Name = "password";
            password.PasswordChar = '*';
            password.Size = new Size(313, 31);
            password.TabIndex = 1;
            // 
            // signup
            // 
            signup.Location = new Point(199, 287);
            signup.Name = "signup";
            signup.Size = new Size(112, 34);
            signup.TabIndex = 2;
            signup.Text = "sign in";
            signup.UseVisualStyleBackColor = true;
            signup.Click += signin_Click;
            // 
            // panel1
            // 
            panel1.Controls.Add(label1);
            panel1.Location = new Point(165, 45);
            panel1.Name = "panel1";
            panel1.Size = new Size(209, 46);
            panel1.TabIndex = 3;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(49, 10);
            label1.Name = "label1";
            label1.Size = new Size(114, 25);
            label1.TabIndex = 0;
            label1.Text = "Admin Login";
            // 
            // LoginForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(575, 366);
            Controls.Add(panel1);
            Controls.Add(signup);
            Controls.Add(password);
            Controls.Add(username);
            Name = "LoginForm";
            Text = "LoginForm";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox username;
        private TextBox password;
        private Button signup;
        private Panel panel1;
        private Label label1;
    }
}