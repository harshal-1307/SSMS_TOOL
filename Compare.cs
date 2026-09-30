using Microsoft.SqlServer.Management.Smo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
//using Microsoft.CognitiveServices.Speech;

//using Microsoft.CognitiveServices.Speech.Extension.Embedded.SR;
//using Microsoft.CognitiveServices.Speech.Extension.ONNX.Runtime;
//using Microsoft.CognitiveServices.Speech.Extension.Telementry;


namespace SSMS_TOOL
{
    public partial class Compare : Form
    {
        public string _MainFilePath, _TextAreaData, _loginName, LOGIN_NAME, _filefolderpath;
        public Compare()
        {
            InitializeComponent();
            mainFileContentData.ScrollBars = RichTextBoxScrollBars.None;
        }


        public string MainFilePath
        {
            get
            {
                return _MainFilePath;
            }

            set
            {
                _MainFilePath = value;
                // mainFileText.Text = value;
            }
        }

        public string TextAreaData
        {
            get
            {
                return _TextAreaData;
            }

            set
            {
                _TextAreaData = value;
                // TextboxContentData.Text = value;
            }
        }
        public string FileFolderPath
        {
            get
            {
                return _filefolderpath;
            }

            set
            {
                _filefolderpath = value;
                FolderFilePath.Text = value;
            }
        }

        private int mainStartLine = 0;
        StringBuilder stringBuilder = new StringBuilder();

        private void Compare_Load(object sender, EventArgs e)
        {

            //try
            //{
            string fileContent = File.ReadAllText(_MainFilePath).Trim();
            string textContent = _TextAreaData.Trim();

            mainFileContentData.Multiline = true;
            mainFileContentData.WordWrap = false;
            mainFileContentData.ScrollBars = RichTextBoxScrollBars.Both;


            // mainFileContentData.Text = fileContent;
            //TextboxContentData.Text = textContent;

            mainStartLine = FindStartLine(fileContent, textContent ,"MainTable");

            string[] mainLine = fileContent.Split(
                new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);


            string[] textboxLine = _TextAreaData.Split(
                new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);


            int startIndex = mainStartLine - 1;
            int totalrightheight = mainFileContentData.ClientSize.Height;
            int lintHeight = mainFileContentData.Font.Height;

            int lines = totalrightheight / lintHeight;

            int diff = mainStartLine + textboxLine.Length;

            int TextAreaStartLine = FindStartLine(fileContent, textContent, "TextAreaTable");

            int TextAreaDiffStartLine = FindStartLine(fileContent, textContent, "TextAreaLength");

            int data = textboxLine.Length ;
            int j = mainStartLine;
            int c = TextAreaStartLine;
            mainLine = mainLine.Concat(Enumerable.Repeat(string.Empty , data)).ToArray();
            try
            {
                while (j <= diff && c < textboxLine.Length)
                {
                    string value = mainLine[j].ToString();

                    string value11 = textboxLine[c].ToString();

                    mainLine[j] = textboxLine[c].ToString();
                    j++;
                    c++;
                }
            }
            catch (Exception ex)
            {

            }

            mainFileContentData.Text = string.Join(Environment.NewLine, mainLine);

            //for (int i = mainStartLine; i < 291; i++)
            //{
            //    stringBuilder.AppendLine(_TextAreaData);
            //}

            // stringBuilder.AppendLine(_TextAreaData);

            //int value = textboxLine.Length;

            //StringBuilder stringBuilder1 = new StringBuilder();
            //stringBuilder1.Append(_TextAreaData);

            //if (TextboxContentData.Text.Contains(mainStartLine.ToString()))
            //{
            //    TextboxContentData.Text = TextboxContentData.Text.Replace(mainStartLine.ToString(),
            //        stringBuilder1.ToString());
            //}

            //int mainLineCoumt = mainFileContentData.Lines.Length;
            //vScrollBar1.Minimum = 0;
            //vScrollBar1.Maximum = Math.Max(0, mainLineCoumt - 1);
            //vScrollBar1.Value = 0;
            // }
            //catch (Exception ex)
            //{

            //}

        }

        private int FindStartLine(string mainText, string textAreText, string MainTable)
        {
            try
            {
                Match tablematch = Regex.Match(
                textAreText, @"CREATE\s+TABLE\s+(?:\[dbo\]\.)?\[?([A-Za-z0-9_]+)\]?",
                RegexOptions.IgnoreCase
                );

                string tablename = tablematch.Groups[1].Value;

                string[] mainLines = mainText.Split
                     (new[] { "\r\n", "\n" }, StringSplitOptions.None)
                     ;


                string[] TextAreLines = textAreText.Split
                     (new[] { "\r\n", "\n" }, StringSplitOptions.None)
                     ;


                if (MainTable == "MainTable")
                {
                    for (int i = 0; i < mainLines.Length; i++)
                    {
                        Match mainTableMatch = Regex.Match(
                             mainLines[i], $@"CREATE\s+TABLE\s+(?:\[dbo\]\.)?\[?{Regex.Escape(tablename)}\]?",
                        RegexOptions.IgnoreCase
                            );
                        if (mainTableMatch.Success)
                        {
                            return i;
                        }

                    }
                }
                if (MainTable == "TextAreaTable")
                {
                    for (int i = 0; i < mainLines.Length; i++)
                    {
                        Match AreaTableMatch = Regex.Match(
                             TextAreLines[i], $@"CREATE\s+TABLE\s+(?:\[dbo\]\.)?\[?{Regex.Escape(tablename)}\]?",
                        RegexOptions.IgnoreCase
                            );
                        if (AreaTableMatch.Success)
                        {
                            return i;
                        }

                    }
                }

                if (MainTable == "TextAreaLength")
                {
                    for (int i = 0; i < mainLines.Length; i++)
                    {
                        Match AreaTableLength = Regex.Match(
                             mainLines[i], @"SET\s+ANSI_NULLS\s+ON\s+GO",
                        RegexOptions.IgnoreCase
                            );
                        if (AreaTableLength.Success)
                        {
                            return i;
                        }

                    }
                }

            }
            catch (Exception ex )
            {

            }
            
            return -1;
        }

        private void Save_Click(object sender, EventArgs e)
        {
            string filepath = FolderFilePath.Text + ".sql";

            try
            {
                if (!string.IsNullOrEmpty(filepath))
                {
                    using (FileStream stream = new FileStream(
                             filepath, FileMode.Create, FileAccess.Write, FileShare.Read))

                    using (StreamWriter writer = new StreamWriter(stream))
                    {
                        writer.WriteLine();
                        writer.WriteLine(mainFileContentData.Text);
                    }
                    MessageBox.Show("File saved successfully");

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

        private void OpenFile_Click(object sender, EventArgs e)
        {
            try
            {
                string filePath = FolderFilePath.Text;
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


        //private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        //{
        //    int line = vScrollBar1.Value;


        //    if (line < mainFileContentData.Lines.Length)
        //    {
        //        int index = mainFileContentData.GetFirstCharIndexFromLine(line);

        //        if (index >= 0)
        //        {
        //            mainFileContentData.SelectionStart = index;
        //            mainFileContentData.ScrollToCaret();
        //        }
        //    }

        //}
    }
}
