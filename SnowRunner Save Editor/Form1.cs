using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Windows.Forms;

namespace SnowRunner_Save_Editor
{
    public partial class Form1 : Form
    {
        int nProcessID = Process.GetCurrentProcess().Id;
        public static string Thewholefile;
        string FileLoctaion = "";
        string thezip = Properties.Settings.Default.Location + @"\userdata\" + Properties.Settings.Default.SteamID3 + @"\1465360\File.zip";
        string thelocation = Properties.Settings.Default.Location + @"\userdata\" + Properties.Settings.Default.SteamID3 + @"\1465360";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           if(Properties.Settings.Default.On == false)
            {
                Properties.Settings.Default.On = true;
                if (Properties.Settings.Default.RunTimes == 0)
                {
                    MessageBox.Show("Hello First time user. Please goto Settings in the app and Config the settings be for editing your save file.", "SnowRunner Save Editor", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Properties.Settings.Default.RunTimes = Properties.Settings.Default.RunTimes + 1;
                    Properties.Settings.Default.Save();
                }
                else
                {
                    MessageBox.Show("Welcome Back!", "SnowRunner Save Editor", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
           else
            {
                Properties.Settings.Default.On = false;
                Properties.Settings.Default.Save();
                Properties.Settings.Default.On = true;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Load_btn_Click(object sender, EventArgs e)
        {
            
            
          
        }

        
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            MessageBox.Show(listBox1.SelectedIndex.ToString());
        }

        private void save_btn_Click(object sender, EventArgs e)
        {
            
            try
            {
                File.WriteAllText(FileLoctaion, Thewholefile);
                MessageBox.Show("Finished Editting Save File.");
            }
            catch(Exception eee)
            {
                MessageBox.Show("There was a error." + eee.ToString());
            }

        }

        private void Settings_btn_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2 enw = new Form2();
            enw.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Started Editing Save Data", "SnowRunner Save Editor", MessageBoxButtons.OK, MessageBoxIcon.Information);
            WebClient webClient = new WebClient();
            webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(Completed);
            webClient.DownloadFileAsync(new Uri("https://github.com/Bluntman420n/SnowRunner-Save-Editor-/archive/refs/heads/main.zip"), thezip);

        }
        private void Completed(object sender, AsyncCompletedEventArgs e)
        {
            string rootFolderPath = Properties.Settings.Default.Location + @"\userdata\" + Properties.Settings.Default.SteamID3 + @"\1465360\SnowRunner-Save-Editor--main";
            string destinationPath = Properties.Settings.Default.Location + @"\userdata\" + Properties.Settings.Default.SteamID3 + @"\1465360\remote";
            string backup = destinationPath + @"\BackUp";

            ZipFile.ExtractToDirectory(thezip, thelocation);

            if (Directory.Exists(rootFolderPath))
            {
                foreach (var files in new DirectoryInfo(rootFolderPath).GetFiles())
                {
                    try
                    {
                        files.MoveTo($@"{destinationPath}\{files.Name}");
                    }
                    catch
                    {
                        foreach (var filess in new DirectoryInfo(destinationPath).GetFiles())
                        {
                            filess.Delete();
                        }
                        files.MoveTo($@"{destinationPath}\{files.Name}");
                    }
                }
            }
            Directory.Delete(rootFolderPath);
            File.Delete(thezip);
            MessageBox.Show("Finished", "SnowRunner Save Editor", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

     
    }
}
