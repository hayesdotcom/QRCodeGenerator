using System;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using AForge;
using AForge.Video;
using AForge.Video.DirectShow;
using ZXing;
using ZXing.Aztec;


namespace QRCodeGenerator
{
    public partial class Form1 : Form
    {
        private FilterInfoCollection CaptureDevice;
        private VideoCaptureDevice FinalFrame;
        public Form1()
        {
            InitializeComponent();
        }


        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CaptureDevice = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo Device in CaptureDevice)
                comboBox1.Items.Add(Device.Name);

            comboBox1.SelectedIndex = 0;
            FinalFrame = new VideoCaptureDevice();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FinalFrame = new VideoCaptureDevice(CaptureDevice[comboBox1.SelectedIndex].MonikerString);
            FinalFrame.NewFrame += new NewFrameEventHandler(FinalFrame_NewFrame);
            FinalFrame.Start();
        }

        private void FinalFrame_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            pictureBox1.Image = (Bitmap)eventArgs.Frame.Clone();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (FinalFrame.IsRunning == true)
                FinalFrame.Stop();
        }

        private void timer2_Tick(Object sender, EventArgs e)
        {



        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void timer2_Tick_1(object sender, EventArgs e)
        {
            BarcodeReader reader = new BarcodeReader();
            Result result = reader.Decode((Bitmap)pictureBox1.Image);

            try
            {
                string decoded = result.ToString();
                if (decoded != "")
                {
                    textBox1.Text = decoded;
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            timer2.Start();
        }
    }

}