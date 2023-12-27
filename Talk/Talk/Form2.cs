using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace синтез_речи
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private string path = @"..\..\settings.json";

        private void setJsonValue(string arg, string value)
        {
            string json1 = File.ReadAllText(path);
            dynamic jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject(json1);
            jsonObj[arg] = value;
            string output = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObj, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(path, output);
        }

        private string OpenDialog()
        {
            var opd = new OpenFileDialog();
            opd.Filter = "*.png | *.jpg";
            opd.ShowDialog();
            return opd.FileName;
        }

        private void label3_Click(object sender, EventArgs e)
        {
            try
            {
                Settings.pathToFileA0 = OpenDialog();
                label6.BackgroundImage = Image.FromFile(Settings.pathToFileA0);
                label6.Text = "";
                setJsonValue("pathToFileO", Settings.pathToFileA0);
            }
            catch (Exception) { }

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            try
            {
                if (Settings.pathToFileNull0 != null)
                {
                    label1.BackgroundImage = Image.FromFile(Settings.pathToFileNull0);
                }
                if (Settings.pathToFileA0 != null)
                {
                    label6.BackgroundImage = Image.FromFile(Settings.pathToFileA0)
                }
                if (Settings.pathToFileO0 != null)
                {
                    label2.BackgroundImage = Image.FromFile(Settings.pathToFileO0);
                }

            }
            catch (Exception) { }

            label6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            label2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            label1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
        }

        private void label7_Click(object sender, EventArgs e)
        {
            try
            {
                Settings.pathToFileNull0 = OpenDialog();
                label1.BackgroundImage = Image.FromFile(Settings.pathToFileNull0);
                label1.Text = "";
                setJsonValue("pathToFileNull", Settings.pathToFileNull0);
            }
            catch (Exception){}
        }

        private void label4_Click(object sender, EventArgs e)
        {
            try
            {
                Settings.pathToFileO0 = OpenDialog();
                label2.BackgroundImage = Image.FromFile(Settings.pathToFileO0);
                label2.Text = "";
                setJsonValue("pathToFileO", Settings.pathToFileO0);
            }
            catch (Exception) { }
        }
    }
}
