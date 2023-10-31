using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DocumentEdit
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            //Save
            string txt = richTextBox1.Text.Trim();
            if (txt == "" || txt == "Please write your ower documnet ...")
            {
                return;
            }
            //SaveFileDialog dialog = new SaveFileDialog();
            //dialog.FileName = DateTime.Now.ToString("yyyy-MM-dd");

            ////设置文件类型 
            //dialog.Filter = "文本|*.txt";

            ////设置默认文件类型显示顺序 
            //dialog.FilterIndex = 1;

            ////保存对话框是否记忆上次打开的目录 
            //dialog.RestoreDirectory = true;
            //if (dialog.ShowDialog() != DialogResult.OK)
            //{
            //    return;
            //}
            bool result =  WritrToTxt(txt);

            if (result)
            {
                MessageBox.Show("OK!");
            }
            else
            {
                MessageBox.Show("保存失败!");
            }
        }

        private bool WritrToTxt(string txt)
        {
            bool result = true;
            try
            {
                string time = DateTime.Now.ToString();

                string TxtName = DateTime.Now.ToString("yyyy-MM-dd");

                string NewTxt = "";

                string FilePath = lblPath.Text;
                if (FilePath != "")
                {
                    if (File.Exists(FilePath))
                    {
                        File.Delete(FilePath);
                    }
                    NewTxt = txt;
                }
                else
                {
                    NewTxt = "" + time + "\n" + txt + "\n   \n";
                }

                //byte[] bytes = Encoding.Default.GetBytes(NewTxt);

                //NewTxt = Convert.ToBase64String(bytes);
                string Files = Application.StartupPath + @"\MyDocumnet";
                //如果路径不存在则创建
                if (Directory.Exists(Files) == false)
                {
                    Directory.CreateDirectory(Files);
                }
                string filePath = Files + @"\" + TxtName + ".txt";
                File.AppendAllText(filePath, NewTxt);
                lblPath.Text = filePath;
            }
            catch
            {
                result = false;
            }
            return result;
        }
        private bool ChangeTxt(string txt)
        {
            bool result = true;
            try
            {
                string time = DateTime.Now.ToString();

                string TxtName = DateTime.Now.ToString("yyyy-MM-dd");

                //change txt 
                string NewTxt = "" + time + "\n" + txt + "\n   \n";
                //byte[] bytes = Encoding.Default.GetBytes(NewTxt);

                //NewTxt = Convert.ToBase64String(bytes);

                //如果路径不存在则创建
                if (Directory.Exists(@"D:\MyDocumnet") == false)
                {
                    Directory.CreateDirectory(@"D:\MyDocumnet");
                }
                string path = @"D:\MyDocumnet\" + TxtName + ".txt";
                File.AppendAllText(path, NewTxt);
            }
            catch
            {
                result = false;
            }
            return result;
        }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //show dialog
            OpenFileDialog dialog = new OpenFileDialog();   //定义打开文件

            dialog.InitialDirectory = Application.StartupPath + @"\MyDocumnet";     //Application.StartupPath; //初始路径,这
            dialog.Filter = "All files(*.*)|*.*|txt files(*.txt)|*.txt";    //设置打开类型
            dialog.FilterIndex = 2;                  //文件类型的显示顺序（上一行.txt设为第二位）
            dialog.RestoreDirectory = true;         //对话框记忆之前打开的目录

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                lblPath.Text = dialog.FileName.ToString();//获得完整路径在textBox1中显示
                StreamReader sr = new StreamReader(dialog.FileName);//, Encoding.Default
                string txt = sr.ReadToEnd();
                //base64 to txt
                //byte[] bytes = Convert.FromBase64String(txt);
                //string NewTxt = Encoding.Default.GetString(bytes);

                richTextBox1.Text = txt;
                sr.Close();
            }

        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void richTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control)
            {
                if (e.KeyCode == Keys.N)
                {
                    newToolStripMenuItem_Click(null, null);
                }
                if (e.KeyCode == Keys.O)
                {
                    OpenToolStripMenuItem_Click(null, null);
                }
                if (e.KeyCode == Keys.S)
                {
                    toolStripButton1_Click(null, null);
                }
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            string txt = richTextBox1.Text.Trim();
            if (txt == "" || txt == "Please write your ower documnet ...")
            {
                lblPath.Text = "";
            }
        }
    }
}
