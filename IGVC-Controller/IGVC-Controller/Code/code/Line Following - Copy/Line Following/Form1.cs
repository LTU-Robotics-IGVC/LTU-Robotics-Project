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
using System.IO.Ports;

using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.Util;
using IGVC_Controller.DataIO;

namespace Line_Following
{
    public partial class Form1 : Form
    {
        Keyboard k;

        //Enter USB to serial adapter port here
        String port = "COM17";
        String phone = "COM25";
        SerialPort _serialport;
        SerialPort GPS;

        private Capture _capture1= null;
        //private Capture _capture2 = null;
        
        int current_motor_command = 0;//stop
        int threshold1 = 150;
        int threshold2 = 150;
        delegate void displayStringDel(String data, Label label);
        delegate void CheckBoxDel(CheckBox box, bool run);

        int leftWin = 0;
        int leftMiddleWin = 0;
        int middleWin = 0;
        int rightMiddleWin = 0;
        int rightWin = 0;

        int motor_command=0;

        List<double> latitude = new List<double> { };
        List<double> longitude = new List<double> { };

        //Autonomous Mode boolean
        bool Auto = false;

        int Hue1 = 0;
        int Hue2 = 0;
        int Value = 0;
        int Saturation = 0;
        int Gaus = 0;

        public Form1()
        {
            InitializeComponent();
            k = new Keyboard(this);

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Value = hScrollBar1.Value;
            Saturation = hScrollBar2.Value;
            Hue1 = hScrollBar5.Value;
            Hue2 = hScrollBar7.Value;
            Gaus = hScrollBar4.Value;

            try
            {
                _capture1 = new Capture(0);
                _capture1.Start();
                _capture1.ImageGrabbed += _capture1_ImageGrabbed;

                GPS = new SerialPort(phone);
                GPS.BaudRate = 4800;
                GPS.DataBits = 8;
                GPS.Parity = Parity.None;
                GPS.StopBits = StopBits.One;
                try { GPS.Open(); }
                catch{}

                _serialport = new SerialPort(port);
                _serialport.BaudRate = 9600;
                _serialport.DataBits = 8;
                _serialport.Parity = Parity.None;
                _serialport.StopBits = StopBits.One;
                //_serialport.DataReceived += DataRecievedHandler;
                try { _serialport.Open(); }
                catch(Exception ee)
                {
                    //MessageBox.Show(ee.ToString());
                }
            }
            catch (NullReferenceException excpt)
            {
                //MessageBox.Show(excpt.Message);
            }

            hScrollBar3.Value = threshold2;
        }

        /*private void DataRecievedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            displayStr(_serialport.ReadLine().ToString(), label2);
        }/**/

        void _capture1_ImageGrabbed(object sender, EventArgs e)
        {
            Image<Bgr, byte> frame = _capture1.RetrieveBgrFrame().Resize(Capture1.Width, Capture1.Height, Emgu.CV.CvEnum.INTER.CV_INTER_LINEAR);
            //lineBox.Image = frame;
            Image<Hsv, byte> hsvIMAGE = frame.Convert<Hsv, byte>();
            Image<Gray, byte> grayframe = frame.Convert<Gray, byte>();
           // Image<Gray, byte> bwimage = grayframe.ThresholdBinary(new Gray(0), new Gray(255));
            Image<Hsv, byte> barrelhsv = frame.Convert<Hsv, byte>(); ;
            Image<Gray, byte>[] channels = frame.Split();
            Image<Gray, byte> imgBlue = channels[0];
            Image<Gray, Byte> combine = imgBlue.And(imgBlue);
            Image<Gray, Byte> bwimage = imgBlue.ThresholdBinary(new Gray(0), new Gray(255));
            
            longitude.Clear();
            for (int w = 0; w < imgBlue.Width; w++)
                for (int h = 0; h < imgBlue.Height / 4; h++)
                {
                    if (imgBlue.Data[h, w, 0] > 30)
                        if ((imgBlue.Data[h, w, 0] - imgBlue.Height / 4 + h) < 0)
                            imgBlue.Data[h, w, 0] = 0;
                        else
                            imgBlue.Data[h, w, 0] = (byte)(imgBlue.Data[h, w, 0] - imgBlue.Height / 4 + h);
                    grayframe.Data[h, w, 0] = (byte)(imgBlue.Height / 4 - h);
                }
            for (int w = 0; w < imgBlue.Width; w++)
            {
                for (int h = 0; h < imgBlue.Height; h++)
                {
                    if ((imgBlue.Data[h, w, 0] > threshold1))
                    {
                        longitude.Add(imgBlue.Data[h, w, 0]);
                    }
                    else
                    {
                        combine.Data[h, w, 0] = 0;
                    }
                }
            }
            
            double newthresh = getStdDev(longitude, 1);
            for (int w = 0; w < imgBlue.Width; w++)
            {
                for (int h = 0; h < imgBlue.Height; h++)
                {
                    if ((imgBlue.Data[h, w, 0] > newthresh))
                    {
                        combine.Data[h, w, 0] = 255;
                    }
                    else
                    {
                        combine.Data[h, w, 0] = 0;
                    }
                }
            }
            lineBox.Image = combine;
            Image<Gray, Byte> combine2 = combine.ThresholdBinary(new Gray(10), new Gray(255));
            combine2.Not();
            combine2._SmoothGaussian(Gaus * 2 - 1);
            combine2.Not();
            combine2 = combine2.ThresholdBinary(new Gray(100), new Gray(255));

            //Run HSV Filter
            for(int w = 0;w<hsvIMAGE.Width;w++)
            {
                for(int h=0;h<hsvIMAGE.Height; h++)
                {
                    //if((hsvIMAGE.Data[h,w,0] > Hue1 - threshold2)&&(hsvIMAGE.Data[h,w,0] < Hue1 + threshold2))
                    //{
                    //    hsvIMAGE.Data[h, w, 2] = 254;
                    //    hsvIMAGE.Data[h, w, 1] = 254;
                    //}
                    //else
                    //{
                    //    hsvIMAGE.Data[h,w,2]=(byte)Value;
                    //    hsvIMAGE.Data[h, w, 1] = (byte)Saturation;
                    //}
                    
                    //barrel detection
                    if ((barrelhsv.Data[h, w, 0] > Hue2 -10) && (barrelhsv.Data[h, w, 0] < Hue2 +10))
                    {
                        barrelhsv.Data[h, w, 2] = 254;
                        barrelhsv.Data[h, w, 1] = 254;
                    }
                    else
                    {
                        barrelhsv.Data[h, w, 2] = (byte)Value;
                        barrelhsv.Data[h, w, 1] = (byte)Saturation;
                    }
                }
            }

            //hsvIMAGE._SmoothGaussian(Gaus * 2 - 1);
            //barrelhsv._SmoothGaussian(Gaus * 2 - 1);
            Image<Gray, byte> combined = combine2.Convert<Gray,byte>().ThresholdBinary(new Gray(10),new Gray(255));//hsvIMAGE.Convert<Gray, byte>().ThresholdBinary(new Gray(10), new Gray(255));
            combined = combined.Add(barrelhsv.Convert<Gray, byte>().ThresholdBinary(new Gray(10), new Gray(255)));
            //combined._SmoothGaussian(Gaus * 2 - 1);
            combined = combined.Convert<Gray, Byte>().ThresholdBinary(new Gray(threshold1), new Gray(255));//combined image

            Capture1.Image = imgBlue;//image to detect lines
            Capture1_BW.Image = combined;//combined;
            //lineBox.Image = combine2;
            imageBox10.Image = barrelhsv;//image to detect barrel

            //0 stop
            //1 left
            //2 forward
            //3 right
            
            if (Auto)
            {
                motor_command = taxiDriver(combined, true);

                if(k.isKeyDown(Keys.ShiftKey))
                {
                    CheckBox(checkBox1, false);
                }
            }
            else
            {
                displayStr(taxiDriver(combined,true).ToString(),label7);

                //Manual Drive keyboard input
                if (k.isKeyDown(Keys.W))
                {
                    if (k.isKeyUp(Keys.A) && k.isKeyUp(Keys.D))
                    {
                        motor_command = 2;//forward
                    }
                    else if(k.isKeyDown(Keys.A) && (k.isKeyUp(Keys.D)))
                    {
                        motor_command = 4;//slight Left
                    }
                    else if(k.isKeyUp(Keys.A) && (k.isKeyDown(Keys.D)))
                    {
                        motor_command = 5;//slight Right
                    }
                }
                else if (k.isKeyDown(Keys.A) && k.isKeyUp(Keys.W))
                {
                    motor_command = 1;//Left
                }
                else if (k.isKeyDown(Keys.D) && k.isKeyUp(Keys.W))
                {
                    motor_command = 3;//Right
                }
                else if (k.isKeyDown(Keys.Return))//start autonomos mode
                {
                    CheckBox(checkBox1, true);
                }
                else if (k.isKeyDown(Keys.CapsLock))
                {
                    _serialport.Write("P");
                    while(k.isKeyDown(Keys.CapsLock))
                    { /*do nothing*/}
                }
                else
                {
                    motor_command = CheckGPS();//Stop

                }
                
            }

            int drive = motor_command;//taxiDriver(bwFrame,true);//true means it is right image


            if(drive != current_motor_command)
            {
                switch (drive)
                {
                    case 1://LEft
                        {
                            try { _serialport.Write("L"); }
                            catch (Exception) { };
                            displayStr("Left     " + leftMiddleWin.ToString() + "  " + middleWin.ToString() + "  " + rightMiddleWin.ToString(), label1);
                            break;
                        }
                    case 2://Forward
                        {
                            try { _serialport.Write("F"); }
                            catch (Exception) { };
                            displayStr("Forward  " + leftMiddleWin.ToString() + "  " + middleWin.ToString() + "  " + rightMiddleWin.ToString(), label1);
                            break;
                        }
                    case 3://Right
                        {
                            try { _serialport.Write("R"); }
                            catch (Exception) { };
                            displayStr("Right    " + leftMiddleWin.ToString() + "  " + middleWin.ToString() + "  " + rightMiddleWin.ToString(), label1);
                            break;
                        }
                    case 0://Stop
                        {
                            try { _serialport.Write("S"); }
                            catch (Exception) { };
                            displayStr("Stop     " + leftMiddleWin.ToString() + "  " + middleWin.ToString() + "  " + rightMiddleWin.ToString(), label1);
                            break;
                        } 
                    case 4://slight Left
                        {
                            try { _serialport.Write("Q"); }
                            catch (Exception) { };
                            displayStr("Slight Left     " + leftMiddleWin.ToString() + "  " + middleWin.ToString() + "  " + rightMiddleWin.ToString(), label1);
                            break;
                        }
                    case 5://slgiht Right
                        {
                            try { _serialport.Write("E"); }
                            catch (Exception) { };
                            displayStr("Slight Right     " + leftMiddleWin.ToString() + "  " + middleWin.ToString() + "  " + rightMiddleWin.ToString(), label1);
                            break;
                        }
                }
                if (Auto)
                {
                    current_motor_command = drive;
                }
                current_motor_command = motor_command;
            }
        }

        public void CheckBox(CheckBox Box, bool run)
        {
            if(InvokeRequired)
            {
                CheckBoxDel del = CheckBox;
                this.BeginInvoke(del, Box, run);
            }
            else
            {
                Box.Checked = run;
            }
        }

        public void displayStr(string str, Label label)
        {
            if (InvokeRequired)
            {
                displayStringDel del = displayStr;
                this.BeginInvoke(del, str, label);
            }
            else
            {
                label.Text = str;
            }
        }

        int CheckGPS()
        {
            string NEMA = GPS.ReadLine();
            string[] NEMASplit = NEMA.Split(',');
            displayStr(NEMA, label10);            
            switch(NEMASplit[0])
            {
                case "$GPGGA"://find bearing
                    {
                        //displayStr(NEMA, label10);
                        return 0;
                    }
                default:
                    {
                        return 0;
                    }
            }
        }


        int taxiDriver(Emgu.CV.Image<Gray, Byte> bwImage, bool Right)
        {
            int width = bwImage.Width;
            int height = bwImage.Height;
            int windowwidth = width / 5;

            leftWin = 0;
            leftMiddleWin = 0;
            middleWin = 0;
            rightMiddleWin = 0;
            rightWin = 0;

            if (false)//if using one camera
            {
                for (int h = height-1; h >0; h--)
                {
                    for (int w = width-1; w > 0; w--)
                    {
                        if (bwImage.Data[h,w,0] == 255)//search for white pixels
                        {
                            if (w < windowwidth)
                                rightWin++;
                            else if (w < (windowwidth * 2))
                                rightMiddleWin++;
                            else if (w < (windowwidth * 3))
                                middleWin++;
                            else if (w < (windowwidth * 4))
                                leftMiddleWin++;
                            else
                                leftWin++;
                        }
                    }
                }
            }
            else
            {
                for (int h = height-250; h < height; h++)
                {
                    for (int w = 0; w < width; w++)
                    {
                        if (bwImage.Data[h, w,0] == 255)//search for white pixels
                        {
                            if (w < windowwidth)
                                leftWin++;
                            else if (w < (windowwidth * 2))
                                leftMiddleWin++;
                            else if (w < (windowwidth * 3))
                                middleWin++;
                            else if (w < (windowwidth * 4))
                                rightMiddleWin++;
                            else
                                rightWin++;
                        }
                    }
                }


            }

            //determine drive command
            if ((leftMiddleWin < 100) && (middleWin < 100) && (rightMiddleWin < 100))
            {
                if((leftWin <200) && (rightWin <200))
                     return 0;//stop
                else if ((leftWin > middleWin) && (leftMiddleWin > rightWin))
                    return 3;//right
                else if ((rightWin > middleWin) && (rightWin > leftWin))
                    return 1;//left
                else
                    return 2;//forward
            }
            else if ((leftMiddleWin > middleWin) && (leftMiddleWin > rightWin))
                return 3;//right
            else if ((rightMiddleWin > middleWin) && (rightMiddleWin > leftWin))
                return 1;//left
            else
                return 2;//forward
        }

        public void addLong(double x)
        {
            longitude.Add(x);
        }

        public double getMean(List<double> x)
        {
            if (x.Count > 0)
            {
                int length = x.Count - 1;
                double mean = 0;
                for (int i = 0; i <= length; i++)
                    mean += x[i];
                mean = mean / x.Count;
                return mean;
            }
            else
                return 0;
        }

        public double getStdDev(List<double> x, int std)
        {
            if (x.Count > 0)
            {
                double mean = getMean(x);
                double stdDev = 0;
                double variance = 0;
                int length = x.Count - 1;
                List<double> temp = new List<double>(x);
                for (int i = 0; i <= length; i++)
                    temp[i] = (temp[i] - mean) * (temp[i] - mean);
                for (int i = 0; i <= length; i++)
                    stdDev += temp[i];
                    stdDev = stdDev / temp.Count;
                variance = Math.Sqrt(stdDev);
                for (int i = 0; i <= length; i++)
                    if (Math.Abs(x[i] - mean) > variance)
                    {
                        x.RemoveAt(i);
                        length--;
                        i--;
                    }
                return getMean(x);
            }
            else { return 0; }
        }

        public float getMean(float[]x)
        {
            int length = x.Length-1;
            float mean=0;
            for (int i = 0; i < length; i++)
                mean += x[i];
            mean = mean / x.Length;
            return mean;
        }


        public float getStdDev(float[] x, int std)
        {
            if (x.Length > 0)
            {
                float mean = getMean(x);
                float[] temp = x;
                float stdDev = 0;
                int length = temp.Length - 1;
                for (int i = 0; i < length; i++)
                    temp[i] = (temp[i] - mean) * (temp[i] - mean);
                for (int i = 0; i < length; i++)
                    stdDev += temp[i];
                stdDev = stdDev / temp.Length;
                //for (int i = 0; i < length;i++)
                return stdDev;
            }
            else
                return 0;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            try { _serialport.Write("S"); }
            catch (Exception) { };

            _capture1.Stop();
            //_capture2.Stop();
            base.OnClosing(e);
        }
        #region ScrollBars
        private void hScrollBar3_Scroll(object sender, ScrollEventArgs e)
        {
            threshold1 = hScrollBar3.Value;
            displayStr(threshold1.ToString(), thresh);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            Auto = checkBox1.Checked;
            _serialport.Write("A");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _serialport.Write("C");//retrieve heading
        }

        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            Value = hScrollBar1.Value;
        }

        private void hScrollBar2_Scroll(object sender, ScrollEventArgs e)
        {
            Saturation = hScrollBar2.Value;
        }

        private void hScrollBar4_Scroll(object sender, ScrollEventArgs e)
        {
            Gaus = hScrollBar4.Value;
        }

        private void hScrollBar5_Scroll(object sender, ScrollEventArgs e)
        {
            Hue1 = hScrollBar5.Value;
            displayStr(Hue1.ToString(), linesThresh);
        }

        private void hScrollBar6_Scroll(object sender, ScrollEventArgs e)
        {
            threshold2 = hScrollBar6.Value;
        }

        private void hScrollBar7_Scroll(object sender, ScrollEventArgs e)
        {
            Hue2 = hScrollBar7.Value;
            displayStr(Hue2.ToString(), label8);
        }
#endregion
    }
}
