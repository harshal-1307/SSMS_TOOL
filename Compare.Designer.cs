namespace SSMS_TOOL
{
    partial class Compare
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
            mainFileContentData = new RichTextBox();
            Save = new Button();
            FolderFilePath = new TextBox();
            OpenFile = new Button();
            SuspendLayout();
            // 
            // mainFileContentData
            // 
            mainFileContentData.Location = new Point(3, 58);
            mainFileContentData.Name = "mainFileContentData";
            mainFileContentData.Size = new Size(974, 718);
            mainFileContentData.TabIndex = 2;
            mainFileContentData.Text = "";
            // 
            // Save
            // 
            Save.Location = new Point(699, 19);
            Save.Name = "Save";
            Save.Size = new Size(125, 34);
            Save.TabIndex = 3;
            Save.Text = "SaveChanges";
            Save.UseVisualStyleBackColor = true;
            Save.Click += Save_Click;
            // 
            // FolderFilePath
            // 
            FolderFilePath.Enabled = false;
            FolderFilePath.Location = new Point(3, 21);
            FolderFilePath.Name = "FolderFilePath";
            FolderFilePath.Size = new Size(515, 31);
            FolderFilePath.TabIndex = 4;
            // 
            // OpenFile
            // 
            OpenFile.Location = new Point(830, 18);
            OpenFile.Name = "OpenFile";
            OpenFile.Size = new Size(147, 34);
            OpenFile.TabIndex = 5;
            OpenFile.Text = "OpenBelowFile";
            OpenFile.UseVisualStyleBackColor = true;
            OpenFile.Click += OpenFile_Click;
            // 
            // Compare
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(989, 788);
            Controls.Add(OpenFile);
            Controls.Add(FolderFilePath);
            Controls.Add(Save);
            Controls.Add(mainFileContentData);
            Name = "Compare";
            Text = "Compare";
            Load += Compare_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private RichTextBox mainFileContentData;
        private Button Save;
        private TextBox FolderFilePath;
        private Button OpenFile;
    }
}