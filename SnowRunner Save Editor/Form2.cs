using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace SnowRunner_Save_Editor
{
    public partial class Form2 : Form
    {
        string FileLoctaion;


        public Form2()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox1.Text = Properties.Settings.Default.Location;
            if(Properties.Settings.Default.SteamID3 == "")
            {
            }
            else
            {
                textBox2.Text = Properties.Settings.Default.SteamID3;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
            Form1 ne = new Form1();
            ne.Show();
        }

        private void Settings_btn_Click(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog openFD = new FolderBrowserDialog();

            openFD.Description = "Where is Your Steam Folder Located";


            if (openFD.ShowDialog() == DialogResult.OK)
            {
                FileLoctaion = openFD.SelectedPath.ToString();
            }
            textBox1.Text = FileLoctaion;

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://steamid.io/lookup");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {


                string[] thetest = textBox2.Text.Split(':');
                Properties.Settings.Default.Location = textBox1.Text;
                Properties.Settings.Default.SteamID3 = thetest[2];
                Properties.Settings.Default.Save();
                textBox1.Text = Properties.Settings.Default.Location;
                textBox2.Text = Properties.Settings.Default.SteamID3;
            }
            catch
            {
                Properties.Settings.Default.Location = textBox1.Text;
                Properties.Settings.Default.Save();
                textBox1.Text = Properties.Settings.Default.Location;
                textBox2.Text = Properties.Settings.Default.SteamID3;

            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
        }

        private void textBox2_MouseEnter(object sender, EventArgs e)
        {

        }
    }
}