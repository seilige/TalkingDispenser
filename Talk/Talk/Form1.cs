using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using Newtonsoft.Json;
using System.IO;

namespace синтез_речи
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            Settings set = new Settings();
            InitializeComponent();
        }

        AudioRecorder r;
        internal WaveInEventArgs arg;

        private void Form1_Load(object sender, EventArgs e)
        {
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            
            r = new AudioRecorder();
            r.f = this;
            r.Start();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            r.Stop();
        }
        public short BAToInt16(byte[] bytes, int index)
        {
            short value = BitConverter.ToInt16(bytes, index);
            return value;
        }

        public void changeBackground(string pathToImage)
        {
            this.BackgroundImage = Image.FromFile(pathToImage);
            
            string s1 = pathToImage;
            string s2 = pathToImage;
            int i = 0;

            System.Windows.Forms.Timer t = new System.Windows.Forms.Timer();
            t.Interval = 500;

            t.Tick += (s, a) => this.BackgroundImage = Image.FromFile(i % 2 == 0 ? s1 : s2);
            t.Tick += (s, a) => { this.BackgroundImage = Image.FromFile(i % 2 == 0 ? s1 : s2); i++; };
            t.Enabled = true;
            
            this.BackgroundImage = Image.FromFile(pathToImage);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            if (arg == null) return;
            for (int x = 10; x < arg.BytesRecorded / 2; x += 10)
            {

                int y = BAToInt16(arg.Buffer, x * 2);
                try
                {
                    int str2 = y / 30 + Height / 2;

                    if (str2 < 331)
                    {
                        if(Settings.pathToFileA0 != "null")
                        {
                            changeBackground(Settings.pathToFileA0);
                        }
                    }

                    else if (str2 > 331)
                    {
                        if (Settings.pathToFileO0 != "null")
                        {
                            changeBackground(Settings.pathToFileO0);
                        }
                    }
                    else
                    {
                        if (Settings.pathToFileNull0 != "null")
                        {
                            changeBackground(Settings.pathToFileNull0);
                        }
                    }
                }
                catch (Exception e1) { }
            }
        }

        private void label1_Click_1(object sender, EventArgs e)
        { 
            Form2 form = new Form2();
            form.Show();
        }
    }

    public class Seting
    {
        public string pathToFileO;
        public string pathToFileA;
        public string pathToFileNull;
    }

    public class Settings
    {
        static Settings()
        {
            string path = @"..\..\settings.json";
            var data = JsonConvert.DeserializeObject<Seting>(File.ReadAllText(path));

            pathToFileO0 = data.pathToFileO;
            pathToFileA0 = data.pathToFileA;
            pathToFileNull0 = data.pathToFileNull;
        }
        
        public static string pathToFileO0;
        public static string pathToFileA0;
        public static string pathToFileNull0;
    }

    internal class AudioRecorder
    {
        public WaveIn waveSource = null;
        public Form1 f;
        public AudioRecorder()
        {
            f = null;
        }
        public void Start()
        {
            waveSource = new WaveIn();
            waveSource.WaveFormat = new WaveFormat(44100, 16, 1);
            waveSource.DeviceNumber = 0;
            waveSource.BufferMilliseconds = 100;
            waveSource.DataAvailable += new EventHandler<WaveInEventArgs>(waveSource_DataAvailable);
            waveSource.RecordingStopped += new EventHandler<StoppedEventArgs>(waveSource_RecordingStopped);

            waveSource.StartRecording();
        }

        public void Stop()
        {
            waveSource.StopRecording();
        }

        private void waveSource_DataAvailable(object sender, WaveInEventArgs e)
        {
            Pen p = new Pen(Color.Black);
            f.arg = e;
            f.Invalidate();
        }

        private void waveSource_RecordingStopped(object sender, StoppedEventArgs e)
        {
            if (waveSource != null)
            {
                waveSource.Dispose();
                waveSource = null;
            }
        }
    }
}
