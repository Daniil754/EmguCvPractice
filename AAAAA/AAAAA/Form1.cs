using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Util;
using Emgu.CV.CvEnum;
using System.Diagnostics;
using Emgu.CV.OCR;
using Emgu.CV.VideoSurveillance;
using Emgu.CV.Features2D;



namespace AAAAA
{
    public partial class Form1 : Form
    {
        Image<Bgr, Byte> img;
        public Form1()
        {
            InitializeComponent();
        }
        //N1
        private void button1_Click(object sender, EventArgs e)
        {
            
            OpenFileDialog op = new OpenFileDialog();
           if (op.ShowDialog() == DialogResult.OK) 
            { 
            img = new Image<Bgr, byte>(op.FileName).Resize(387,433,Inter.Linear);
            pictureBox1.Image = img;
             }
    }
        //CellSharding
        private void button2_Click(object sender, EventArgs e)
        {
            
            Mat uimage = new Mat();
            CvInvoke.CvtColor(img, uimage, ColorConversion.Bgr2Gray);
            double cannyThreshold = trackBar1.Value; double cannyThresholdLinking = trackBar2.Value;
            Mat cannyEdges = new Mat();
            CvInvoke.Canny(uimage, cannyEdges, cannyThreshold, cannyThresholdLinking);
            Image<Bgr, Byte> i1 = new Image<Bgr, Byte>(img.Size);
            Mat ccanny = new Mat();
            CvInvoke.CvtColor(cannyEdges, ccanny, ColorConversion.Gray2Bgr);
            CvInvoke.Subtract(img, ccanny, i1);
            for (int i = 0; i < i1.Width; i++) for (int j = 0; j < i1.Height; j++)
                {
                    if (i1.Data[j, i, 0] <= 50) i1.Data[j, i, 0] = 0;
                    else
                    if (i1.Data[j, i, 0] <= 100) i1.Data[j, i, 0] = 25;
                    else
                    if (i1.Data[j, i, 0] <= 150) i1.Data[j, i, 0] = 180;
                    else
                    if (i1.Data[j, i, 0] <= 200) i1.Data[j, i, 0] = 210;
                    else
                        i1.Data[j, i, 0] = 255;

                    if (i1.Data[j, i, 1] <= 50) i1.Data[j, i, 1] = 0;
                    else
                    if (i1.Data[j, i, 1] <= 100) i1.Data[j, i, 1] = 25;
                    else
                    if (i1.Data[j, i, 1] <= 150) i1.Data[j, i, 1] = 180;
                    else
                    if (i1.Data[j, i, 1] <= 200) i1.Data[j, i, 1] = 210;
                    else
                        i1.Data[j, i, 1] = 255;

                    if (i1.Data[j, i, 2] <= 50) i1.Data[j, i, 2] = 0;
                    else
                    if (i1.Data[j, i, 2] <= 100) i1.Data[j, i, 2] = 25;
                    else
                    if (i1.Data[j, i, 2] <= 150) i1.Data[j, i, 2] = 180;
                    else
                    if (i1.Data[j, i, 2] <= 200) i1.Data[j, i, 2] = 210;
                    else
                        i1.Data[j, i, 2] = 255;
                    imageBox1.Image = i1;
                }


        }
        //Canny Filter
        private void button3_Click(object sender, EventArgs e)
        {
            Mat uimage = new Mat();
            CvInvoke.CvtColor(img, uimage, ColorConversion.Bgr2Gray);
            double cannyThreshold = trackBar1.Value; double cannyThresholdLinking = trackBar2.Value;
            Mat cannyEdges = new Mat();
            CvInvoke.Canny(uimage, cannyEdges, cannyThreshold, cannyThresholdLinking);
            imageBox1.Image = cannyEdges;
            
        }

        //N2
        private void button4_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            if (op.ShowDialog() == DialogResult.OK)
            {
                img = new Image<Bgr, byte>(op.FileName).Resize(387, 433, Inter.Linear);
                imageBox2.Image = img;
            }
        }
        //Вывод значений цветов изображения
        private void button5_Click(object sender, EventArgs e)
        {
            byte color=0;
            if (radioButton1.Checked)
            {
                for(int i = 0; i < img.Height; i++)
                {
                    for(int j = 0; j < img.Width; j++)
                    {
                        color += img.Data[i, j, 2];
                    }
                }
                MessageBox.Show("Значения красного " + color.ToString());
            }
            if (radioButton2.Checked)
            {
                for (int i = 0; i < img.Height; i++)
                {
                    for (int j = 0; j < img.Width; j++)
                    {
                        color += img.Data[i, j, 1];
                    }
                }
                MessageBox.Show("Значения зеленого " + color.ToString());
            }
            if (radioButton3.Checked)
            {
                for (int i = 0; i < img.Height; i++)
                {
                    for (int j = 0; j < img.Width; j++)
                    {
                        color += img.Data[i, j, 0];
                    }
                }
                MessageBox.Show("Значения Синего " + color.ToString());
            }
        }
        //Ч/Б
        private void button6_Click(object sender, EventArgs e)
        {
            Image<Gray, Byte> imageGrey = new Image<Gray, Byte>(img.Size);
            for (int i = 0; i < img.Height; i++)
            {
                for (int j = 0; j < img.Width; j++)
                {
                    imageGrey.Data[i, j, 0] = Convert.ToByte(0.299 * img.Data[i, j, 2] + 0.587 * img.Data[i, j, 1] + 0.114 * img.Data[i, j, 0]);
                }
            }
            imageBox3.Image = imageGrey;
        }
        //Sepia
        private void button7_Click(object sender, EventArgs e)
        {
            try 
            {
                Image<Bgr, byte> imageSepia = new Image<Bgr, byte>(img.Size);    
                for (int i = 0; i < img.Height; i++)
                {
                    for (int j = 0; j < img.Width; j++)
                    {
                        
                           byte red= (byte)(0.393*img.Data[i, j, 2] + img.Data[i, j, 1] * 0.769 + img.Data[i, j, 0] * 0.189);
                           byte green = (byte)(0.349 *img.Data[i, j, 2]  + img.Data[i, j, 1] * 0.686 + img.Data[i, j, 0] * 0.168);
                           byte blue =(byte)(0.272*img.Data[i, j, 2]  + img.Data[i, j, 1] * 0.534 + img.Data[i, j, 0] * 0.131);

                        if (red > 255)
                        {
                            imageSepia.Data[i,j,2] += 255;
                        }
                        else
                        {
                            imageSepia.Data[i,j,2] +=red ;
                        }

                        if (green > 255)
                        {
                            imageSepia.Data[i,j,1]+= 255;
                        }
                        else
                        {
                            imageSepia.Data[i, j, 1] += green;
                        }

                       if (blue >255)
                        {
                            imageSepia.Data[i,j,0]+= 255;
                        }
                        else
                        {
                            imageSepia.Data[i, j, 0] += blue;
                        }

                    }
                }
                imageBox3.Image = imageSepia;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            }
        //Blur B/W
        private void button8_Click(object sender, EventArgs e)
        {
            Image<Gray, byte> blur = new Image<Gray, byte>(img.Size);
            List<int> b = new List<int>();
            for (int i = 0; i < img.Height; i++)
            {
                for (int j = 0; j < img.Width; j++)
                {
                   blur.Data[i, j, 0] = Convert.ToByte(0.299 * img.Data[i, j, 2] + 0.587 * img.Data[i, j, 1] + 0.114 * img.Data[i, j, 0]);
                   b.Add(blur.Data[i, j, 0]);
                }
            }
            b.Sort();
           for(int i = 1; i < img.Height-1; i++)
            {
                for(int j = 1; j < img.Width-1; j++)
                {
                        blur.Data[i, j, 0] += Convert.ToByte(b[i]);
                    
                        int color = (int)((blur.Data[i,j,0]+blur.Data[i, j - 1, 0] + blur.Data[i + 1, j - 1, 0] + blur.Data[i - 1, j - 1, 0] + blur.Data[i - 1, j, 0] + blur.Data[i + 1, j, 0] + blur.Data[i - 1, j + 1, 0] + blur.Data[i, j + 1, 0] + blur.Data[i + 1, j - 1, 0]) / 9);

                    blur.Data[i, j, 0] = (byte)(color); 
                }
            }
           
            
            imageBox3.Image = blur;
        }
        //BlurRGB
        private void button9_Click(object sender, EventArgs e)
        {
            Image<Bgr, byte> blur = new Image<Bgr, byte>(img.Size);
            List<int> r = new List<int>();
            List<int> g = new List<int>();
            List<int> b = new List<int>();
            for (int i =0 ; i < img.Height; i++)
            {
                for (int j = 0; j < img.Width ; j++)
                {

                    r.Add(img.Data[i, j, 2]);
                    g.Add(img.Data[i, j, 1]);
                    b.Add(img.Data[i, j, 0]);
                }
            }
            r.Sort();
            g.Sort();
            b.Sort();
            for (int i = 1; i < img.Height - 1; i++)
            {
                for (int j = 1; j < img.Width - 1; j++)
                {
                    img.Data[i, j, 2] += (byte)(r[i]);
                    img.Data[i, j, 1] += (byte)(g[i]);
                    img.Data[i, j, 0] += (byte)(b[i]);

                    int rcolor = (int)((img.Data[i, j - 1, 2] + img.Data[i + 1, j - 1, 2] + img.Data[i - 1, j - 1, 2] + img.Data[i - 1, j, 2] + img.Data[i + 1, j, 2] + img.Data[i - 1, j + 1, 2] + img.Data[i, j + 1, 2] + img.Data[i + 1, j - 1, 2]) / 8);
                    int gcolor = (int)((img.Data[i, j - 1, 1] + img.Data[i + 1, j - 1, 1] + img.Data[i - 1, j - 1, 1] + img.Data[i - 1, j, 1] + img.Data[i + 1, j, 1] + img.Data[i - 1, j + 1, 1] + img.Data[i, j + 1, 1] + img.Data[i + 1, j - 1, 1]) / 8);
                    int bcolor = (int)((img.Data[i, j - 1, 0] + img.Data[i + 1, j - 1, 0] + img.Data[i - 1, j - 1, 0] + img.Data[i - 1, j, 0] + img.Data[i + 1, j, 0] + img.Data[i - 1, j + 1, 0] + img.Data[i, j + 1, 0] + img.Data[i + 1, j - 1, 0]) / 8);
                    img.Data[i, j, 2] = Convert.ToByte(rcolor);
                    img.Data[i, j, 1] = Convert.ToByte(gcolor);
                    img.Data[i, j, 0] = Convert.ToByte(bcolor);

          }
     }
            imageBox3.Image = img;
        }
        //HSV
        private void button10_Click(object sender, EventArgs e)
        {
            byte contrastvalue = (byte)(trackBar4.Value);
            byte brightnessvalue = (byte)(trackBar3.Value);
            Image<Hsv, byte> HSVimage = img.Convert<Hsv, byte>();
            for (int i = 0; i < img.Height ; i++)
            {
                for (int j = 0; j < img.Width ; j++)
                {
                    HSVimage.Data[i, j,0] *= contrastvalue;
                    HSVimage.Data[i, j, 1] += brightnessvalue;
                }
            }
            imageBox3.Image = HSVimage;
          }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.RowCount = 3;
            dataGridView1.ColumnCount = 3;
        }
        //матричный фильтр
        private void button11_Click(object sender, EventArgs e)
        {
            int[,] matrix = new int[3, 3];
            for(int i = 0; i < 3; i++)
            {
                for(int j = 0; j < 3; j++)
                {
                    matrix[i, j] = Convert.ToInt32(dataGridView1.Rows[i].Cells[j].Value);
                }
            }
            Image<Bgr, byte> result = new Image<Bgr, byte>(img.Size);
            

           
            for (int i = 1; i < img.Height-1; i++)
            {
                for(int j = 1; j < img.Width-1; j++)
                {



                    result.Data[i, j , 2] = (byte)(img.Data[i, j , 2] * matrix[1, 1]);
                    result.Data[i, j - 1, 2] = (byte)(img.Data[i, j - 1, 2] * matrix[1, 0]);
                    result.Data[i + 1, j - 1, 2] = (byte)(img.Data[i + 1, j - 1, 2] * matrix[2, 0]);
                    result.Data[i - 1, j - 1, 2] = (byte)(img.Data[i - 1, j - 1, 2] * matrix[0, 0]);
                    result.Data[i - 1, j, 2] = (byte)(img.Data[i - 1, j, 2] * matrix[0, 1]);
                    result.Data[i + 1, j, 2] = (byte)(img.Data[i + 1, j, 2] * matrix[2, 1]);
                    result.Data[i - 1, j + 1, 2] = (byte)(img.Data[i - 1, j + 1, 2] * matrix[0, 2]);
                    result.Data[i, j + 1, 2]= (byte)(img.Data[i, j + 1, 2] * matrix[1, 2]);
                    result.Data[i + 1, j + 1, 2] = (byte)(img.Data[i + 1, j + 1, 2] * matrix[2, 2]);

                    result.Data[i, j, 1] = (byte)(img.Data[i, j, 1] * matrix[1, 1]);
                    result.Data[i, j - 1, 1] = (byte)(img.Data[i, j - 1, 1] * matrix[1, 0]);
                    result.Data[i + 1, j - 1, 1] = (byte)(img.Data[i + 1, j - 1, 1] * matrix[2, 0]);
                    result.Data[i - 1, j - 1, 1] = (byte)(img.Data[i - 1, j - 1, 1] * matrix[0, 0]);
                    result.Data[i - 1, j, 1] = (byte)(img.Data[i - 1, j, 1] * matrix[0, 1]);
                    result.Data[i + 1, j, 1] = (byte)(img.Data[i + 1, j, 1] * matrix[2, 1]);
                    result.Data[i - 1, j + 1, 1] = (byte)(img.Data[i - 1, j + 1, 1] * matrix[0, 2]);
                    result.Data[i, j + 1, 1] = (byte)(img.Data[i, j + 1, 1] * matrix[1, 2]);
                    result.Data[i + 1, j + 1, 1] = (byte)(img.Data[i + 1, j + 1, 1] * matrix[2, 2]);

                    result.Data[i, j, 0] = (byte)(img.Data[i, j, 0] * matrix[1, 1]);
                    result.Data[i, j - 1, 0] = (byte)(img.Data[i, j - 1, 0] * matrix[1, 0]);
                    result.Data[i + 1, j - 1, 0] = (byte)(img.Data[i + 1, j - 1, 0] * matrix[2, 0]);
                    result.Data[i - 1, j - 1, 0] = (byte)(img.Data[i - 1, j - 1, 0] * matrix[0, 0]);
                    result.Data[i - 1, j, 0] = (byte)(img.Data[i - 1, j, 0] * matrix[0, 1]);
                    result.Data[i + 1, j, 0] = (byte)(img.Data[i + 1, j, 0] * matrix[2, 1]);
                    result.Data[i - 1, j + 1, 0] = (byte)(img.Data[i - 1, j + 1, 0] * matrix[0, 2]);
                    result.Data[i, j + 1, 0] = (byte)(img.Data[i, j + 1, 0] * matrix[1, 2]);
                    result.Data[i + 1, j + 1, 0] = (byte)(img.Data[i + 1, j + 1, 0] * matrix[2, 2]);


                }
            }
            

            imageBox3.Image = result;
        }
        //акварельный фильтр
        private void button12_Click(object sender, EventArgs e)
        {
            Image<Hsv, byte> mask;
            byte contrastvalue = (byte)(trackBar4.Value);
            byte brightnessvalue = (byte)(trackBar3.Value);
            List<int> r = new List<int>();
            List<int> g = new List<int>();
            List<int> b = new List<int>();
            OpenFileDialog op = new OpenFileDialog();
            Image<Hsv, byte> img1 = new Image<Hsv, byte>(img.Size);
            Image<Hsv, byte> src = img.Convert<Hsv, byte>();
            if (op.ShowDialog() == DialogResult.OK)
            {
                for (int i = 0; i < img.Height; i++)
                {
                    for (int j = 0; j < img.Width; j++)
                    {

                        r.Add(img.Data[i, j, 2]);
                        g.Add(img.Data[i, j, 1]);
                        b.Add(img.Data[i, j, 0]);
                    }
                }
                r.Sort();
                g.Sort();
                b.Sort();
                for (int i = 1; i < img.Height - 1; i++)
                {
                    for (int j = 1; j < img.Width - 1; j++)
                    {
                        img.Data[i, j, 2] += (byte)(r[i]);
                        img.Data[i, j, 1] += (byte)(g[i]);
                        img.Data[i, j, 0] += (byte)(b[i]);

                        int rcolor = (int)((img.Data[i, j - 1, 2] + img.Data[i + 1, j - 1, 2] + img.Data[i - 1, j - 1, 2] + img.Data[i - 1, j, 2] + img.Data[i + 1, j, 2] + img.Data[i - 1, j + 1, 2] + img.Data[i, j + 1, 2] + img.Data[i + 1, j - 1, 2]) / 8);
                        int gcolor = (int)((img.Data[i, j - 1, 1] + img.Data[i + 1, j - 1, 1] + img.Data[i - 1, j - 1, 1] + img.Data[i - 1, j, 1] + img.Data[i + 1, j, 1] + img.Data[i - 1, j + 1, 1] + img.Data[i, j + 1, 1] + img.Data[i + 1, j - 1, 1]) / 8);
                        int bcolor = (int)((img.Data[i, j - 1, 0] + img.Data[i + 1, j - 1, 0] + img.Data[i - 1, j - 1, 0] + img.Data[i - 1, j, 0] + img.Data[i + 1, j, 0] + img.Data[i - 1, j + 1, 0] + img.Data[i, j + 1, 0] + img.Data[i + 1, j - 1, 0]) / 8);
                        img.Data[i, j, 2] = Convert.ToByte(rcolor);
                        img.Data[i, j, 1] = Convert.ToByte(gcolor);
                        img.Data[i, j, 0] = Convert.ToByte(bcolor);

                    }
                }
                mask = new Image<Hsv, byte>(op.FileName).Resize(387,433,Inter.Linear); 
                img1=src.AddWeighted(mask,Convert.ToDouble(alpha.Text), Convert.ToDouble(beta.Text),Convert.ToDouble(gamma.Text));
               
                imageBox3.Image = img1;
            } 
        }
        //Cartoon filter
        private void button13_Click(object sender, EventArgs e)
        {
            Image<Gray, Byte> imageGrey = new Image<Gray, Byte>(img.Size); 
            Image<Gray, Byte> edges = new Image<Gray, Byte>(img.Cols, img.Rows);
            Image<Bgr, byte> edges1 = new Image<Bgr, byte>(img.Size);
            List<int> r = new List<int>();
            List<int> g = new List<int>();
            List<int> b = new List<int>();
          
            for (int i = 0; i < img.Height; i++)
            {
                for (int j = 0; j < img.Width; j++)
                {
                    imageGrey.Data[i, j, 0] = Convert.ToByte(0.299 * img.Data[i, j, 2] + 0.587 * img.Data[i, j, 1] + 0.114 * img.Data[i, j, 0]);
                   
                }
            }
            for (int i = 0; i < img.Height; i++)
            {
                for (int j = 0; j < img.Width; j++)
                {

                    r.Add(img.Data[i, j, 2]);
                    g.Add(img.Data[i, j, 1]);
                    b.Add(img.Data[i, j, 0]);
                }
            }
            r.Sort();
            g.Sort();
            b.Sort();
            for (int i = 1; i < img.Height - 1; i++)
            {
                for (int j = 1; j < img.Width - 1; j++)
                {
                    img.Data[i, j, 2] += (byte)(r[i]);
                    img.Data[i, j, 1] += (byte)(g[i]);
                    img.Data[i, j, 0] += (byte)(b[i]);

                    int rcolor = (int)((img.Data[i, j - 1, 2] + img.Data[i + 1, j - 1, 2] + img.Data[i - 1, j - 1, 2] + img.Data[i - 1, j, 2] + img.Data[i + 1, j, 2] + img.Data[i - 1, j + 1, 2] + img.Data[i, j + 1, 2] + img.Data[i + 1, j - 1, 2]) / 8);
                    int gcolor = (int)((img.Data[i, j - 1, 1] + img.Data[i + 1, j - 1, 1] + img.Data[i - 1, j - 1, 1] + img.Data[i - 1, j, 1] + img.Data[i + 1, j, 1] + img.Data[i - 1, j + 1, 1] + img.Data[i, j + 1, 1] + img.Data[i + 1, j - 1, 1]) / 8);
                    int bcolor = (int)((img.Data[i, j - 1, 0] + img.Data[i + 1, j - 1, 0] + img.Data[i - 1, j - 1, 0] + img.Data[i - 1, j, 0] + img.Data[i + 1, j, 0] + img.Data[i - 1, j + 1, 0] + img.Data[i, j + 1, 0] + img.Data[i + 1, j - 1, 0]) / 8);
                    img.Data[i, j, 2] = Convert.ToByte(rcolor);
                    img.Data[i, j, 1] = Convert.ToByte(gcolor);
                    img.Data[i, j, 0] = Convert.ToByte(bcolor);

                }
            }
            CvInvoke.AdaptiveThreshold(imageGrey, edges, trackBar5.Value, AdaptiveThresholdType.MeanC,ThresholdType.Binary, 9, 2);
            imageBox3.Image = img.And(img,edges); 

        }
        //Дополнение
        private void button14_Click(object sender, EventArgs e)
        {
            Image<Bgr, byte> mask;
            OpenFileDialog op = new OpenFileDialog();
            if (op.ShowDialog() == DialogResult.OK)
            {
                mask = new Image<Bgr, byte>(op.FileName).Resize(387, 433, Inter.Linear);
                imageBox3.Image = img.Or(mask);
            }
        }

        //Пересечение
        private void button15_Click(object sender, EventArgs e)
        {
            Image<Bgr, byte> mask;
            OpenFileDialog op = new OpenFileDialog();
            if (op.ShowDialog() == DialogResult.OK)
            {
                mask = new Image<Bgr, byte>(op.FileName).Resize(387, 433, Inter.Linear);
                imageBox3.Image = img.And(mask);
            }
        }
        //Исключение
        private void button16_Click(object sender, EventArgs e)
        {
            Image<Bgr, byte> mask;
            OpenFileDialog op = new OpenFileDialog();
            if (op.ShowDialog() == DialogResult.OK)
            {
                mask = new Image<Bgr, byte>(op.FileName).Resize(387, 433, Inter.Linear);
                imageBox3.Image = img.Xor(mask);
            }
        }

        //N3
        private void button17_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            if (op.ShowDialog() == DialogResult.OK)
            {
                img = new Image<Bgr, byte>(op.FileName).Resize(387, 433, Inter.Linear);
                imageBox4.Image = img;
            }
        }
        //Масштабирование
        private void button18_Click(object sender, EventArgs e)
        {
            double sX = Convert.ToDouble(textBox1.Text);
            double sY = Convert.ToDouble(textBox2.Text);
          
                Image<Bgr, byte> newimage = new Image<Bgr, byte>(img.Size);
                for (int i = 0; i < img.Height; i++)
                {
                    for (int j = 0; j < img.Width; j++)
                    {


                    img[i, j] = img[Convert.ToInt32(i * sY), Convert.ToInt32(j * sX)];


                    }
                }
                imageBox5.Image = img;
            
            
        }
        //Сдвиг
        private void button19_Click(object sender, EventArgs e)
        {
            double shift = Convert.ToDouble(textBox10.Text);
            for(int i = 0; i < img.Height; i++)
            {
                for(int j = 0; j < img.Width; j++)
                {
                    double y = i;
                    double x = j + shift * (img.Height - i);
                    if (x > img.Width)
                    {
                        x = img.Width;
                    }
                    img[i, j] = img[i, Convert.ToInt32(x)];
                   
                }
            }
            imageBox5.Image = img;
        }
        //Поворот
        private void button29_Click(object sender, EventArgs e)
        {
            double angle = Math.PI / Convert.ToDouble(textBox11.Text);
          
            for(int i = 0; i < img.Height; i++)
            {
                for(int j = 0; j < img.Width; j++)
                {
                   
                  img[i, j] = img[Convert.ToInt32(j * Math.Cos(angle) + i * Math.Sin(angle)), Convert.ToInt32(-j * Math.Sin(angle) + i * Math.Cos(angle))];
                }
            }
            imageBox5.Image = img;
        }
        //Отражение
        private void button30_Click(object sender, EventArgs e)
        {
            int qX = Convert.ToInt32(textBox12.Text);
            int qY = Convert.ToInt32(textBox13.Text);
            for(int i = 0; i < img.Height; i++)
            {
                for(int j = 0; j < img.Width; j++)
                {
                    img[i, j] = img[i * qY + img.Height, j * qX + img.Width];
                }
            }
            imageBox5.Image = img;
        }
        //Гомография
        private void button20_Click(object sender, EventArgs e)
        {
            Image<Bgr, byte> image = new Image<Bgr, byte>(img.Size);
            
            VectorOfPointF srcPoints = new VectorOfPointF();
            var masp = srcPoints.ToArray();

            float[,] sourcePoints = { {float.Parse(textBox14.Text), float.Parse(textBox15.Text) }, { float.Parse(textBox16.Text), float.Parse(textBox17.Text) }, { float.Parse(textBox18.Text), float.Parse(textBox19.Text) }, { float.Parse(textBox20.Text), float.Parse(textBox21.Text) } };

            var points = new PointF[4] { new PointF(30,4),new PointF( 4,2),new PointF(5,6), new PointF(6,5) };
            Emgu.CV.Matrix<float> sourceMat = new Matrix<float>(sourcePoints);


            VectorOfPointF destPoints = new VectorOfPointF();
            destPoints.Push(new PointF[] { new PointF(0, 0), new PointF(0, img.Rows - 1), new PointF(img.Cols - 1, img.Rows - 1), new PointF(img.Cols - 1, 0) });
            
            Matrix<Double> h = new Matrix<Double>(3, 3); 
       
            CvInvoke.FindHomography(sourceMat, destPoints, h);
            CvInvoke.WarpPerspective(img, image, h,image.Size);

            imageBox5.Image = image;

        }
        //N4
        private void button21_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            if (op.ShowDialog() == DialogResult.OK)
            {
                img = new Image<Bgr, byte>(op.FileName).Resize(387,433,Inter.Linear);
                imageBox6.Image = img;
            }
        }
        //Контуры
        Point[] points;
        private void button22_Click(object sender, EventArgs e)
        {
            Mat image = new Mat();
            CvInvoke.CvtColor(img, image, ColorConversion.Bgr2Gray);

            Mat pyrmg = new Mat();
            CvInvoke.PyrDown(image, pyrmg);
            CvInvoke.PyrUp(pyrmg, image);

            Mat tImage = new Mat();
            int threshold =Convert.ToInt32(textBox3.Text);
            CvInvoke.Threshold(image, tImage, threshold, 255, ThresholdType.Binary);
            VectorOfVectorOfPoint contours = new VectorOfVectorOfPoint();
            CvInvoke.FindContours(tImage, contours, null, RetrType.List, ChainApproxMethod.ChainApproxSimple);

             var newImage = img.CopyBlank();
            for(int i = 0; i < contours.Size; i++)
            {
               points = contours[i].ToArray();
               for(int j = 0; j < points.Length; j++)
                {
                    comboBox1.Items.Add(points[j]);
                    newImage.Draw(points, new Bgr(Color.GreenYellow), 2);
                }
               
            }
           
            imageBox7.Image = newImage;
        }
        //Примитивы
        private void button23_Click(object sender, EventArgs e)
        {
            int minRadius = Convert.ToInt32(textBox5.Text);
            int maxRadius = Convert.ToInt32(textBox6.Text);
            int minDistance = Convert.ToInt32(textBox7.Text);
            int acTreshold =Convert.ToInt32(textBox8.Text);
            int minArea = Convert.ToInt32(textBox4.Text);

            Mat image = new Mat();
            CvInvoke.CvtColor(img, image, ColorConversion.Bgr2Gray);

            Mat pyrmg = new Mat();
            CvInvoke.PyrDown(image, pyrmg);
            CvInvoke.PyrUp(pyrmg, image);

            Mat tImage = new Mat();
            int threshold = Convert.ToInt32(textBox3.Text);
            CvInvoke.Threshold(image, tImage, threshold,255,ThresholdType.Binary);

            VectorOfVectorOfPoint contours = new VectorOfVectorOfPoint();

            CvInvoke.FindContours(tImage, contours, null, RetrType.List, ChainApproxMethod.ChainApproxSimple);

            var approxContour = new VectorOfPoint();
            var newImage = img.CopyBlank();
            for (int i = 0; i < contours.Size; i++)
            {
                CvInvoke.ApproxPolyDP(contours[i], approxContour, CvInvoke.ArcLength(contours[i], true) *
                0.05, true);
                var pts = approxContour;
            
                if (CvInvoke.ContourArea(approxContour, false) > minArea)
                {
                    if (approxContour.Size == 3)
                    {
                        img.Draw(new Triangle2DF(pts[0], pts[1], pts[2]), new Bgr(Color.GreenYellow), 2);
                    }
                }
                if (CvInvoke.ContourArea(approxContour, false) > minArea)
                {
                    img.Draw(CvInvoke.MinAreaRect(approxContour), new Bgr(Color.GreenYellow), 2);
                }
            }
            List<CircleF> circles = new List<CircleF>(CvInvoke.HoughCircles(image,HoughType.Gradient,1.0, minDistance,100, acTreshold, minRadius,maxRadius));
            foreach (CircleF circle in circles)
                img.Draw(circle, new Bgr(Color.GreenYellow), 2);
            for(int i = 0; i <approxContour.Size; i++)
            {
                comboBox2.Items.Add(approxContour[i]);
               
            }
           
            imageBox7.Image = img;
        }
        private bool isRectangle(Point[] pts)
        {
            LineSegment2D[] edges = PointCollection.PolyLine(pts, true);

            for (int j = 0; j < edges.Length; j++)
            {
                double angle = Math.Abs(edges[(j + 1) %
                                        edges.Length].GetExteriorAngleDegree(edges[j])); if (angle < 80 || angle > 100)
                {
                    return false;
                }
            }
            return true;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            

        }
        //N5
        private void button24_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            if (op.ShowDialog() == DialogResult.OK)
            {
                img = new Image<Bgr, byte>(op.FileName).Resize(589, 433, Inter.Linear);
                imageBox8.Image = img;
            }
        }
        Rectangle rect;
        VectorOfVectorOfPoint contours;
        List<Rectangle> rects = new List<Rectangle>();
        //Обнаружение текста
        private void button25_Click(object sender, EventArgs e)
        {
            
            var thresh = img.Convert<Gray,byte>();
            thresh._ThresholdBinaryInv(new Gray(128), new Gray(255));
            thresh._Dilate(5);
           
            contours = new VectorOfVectorOfPoint();
            CvInvoke.FindContours(thresh, contours, null, RetrType.List,
            ChainApproxMethod.ChainApproxSimple);

            var output = img.Copy();

            for (int i = 0; i < contours.Size; i++)
            {
                if (CvInvoke.ContourArea(contours[i], false) > 50)
                {
                    comboBox3.Items.Add(contours[i]);
                    rect = CvInvoke.BoundingRectangle(contours[i]); 

                    output.Draw(rect, new Bgr(Color.Blue), 1);

                    rects.Add(rect);


                }
            }
            imageBox9.Image =output;
        }
        //Вывод текста в текстбокс
        private void button26_Click(object sender, EventArgs e)
        {
            img.ROI = rects[comboBox3.SelectedIndex];
            Image<Bgr, byte> roiImg;
            roiImg = img.Clone();
            Tesseract _ocr = new Tesseract("D:\\emgucv-windows-universal 3.0.0.2157\\Emgu.CV.OCR\\tessdata", "eng", OcrEngineMode.TesseractCubeCombined);
            _ocr.Recognize(roiImg);
            Tesseract.Character[] words = _ocr.GetCharacters();

            StringBuilder strBuilder = new StringBuilder();
            for (int j = 0; j < words.Length; j++)
            {
                strBuilder.Append(words[j].Text);
                textBox9.Text = strBuilder.ToString();
            }
           
        }
        
        //Обнаружение лиц
        private void button27_Click(object sender, EventArgs e)
        {
            List<Rectangle> faces = new List<Rectangle>();

            using (CascadeClassifier face = new
            CascadeClassifier("D:\\emgucv-windows-universal 3.0.0.2157\\bin\\haarcascade_frontalface_default.xml"))
            {
                using (Mat ugray = new Mat())
                {
                    CvInvoke.CvtColor(img, ugray, Emgu.CV.CvEnum.ColorConversion.Bgr2Gray);
                    Rectangle[] facesDetected = face.DetectMultiScale(ugray, 1.1, 10, new Size(20, 20));

                    faces.AddRange(facesDetected);
                }
            }
            foreach (Rectangle rect in faces)
                img.Draw(rect, new Bgr(Color.Yellow), 2);
            imageBox9.Image = img;
        }
        
      
        //отображение видео через timer
        private void timer1_Tick(object sender, EventArgs e)
        {
            while (FrameRate < totalframes)
            {
                imageBox10.Image = _videoCapture.QueryFrame();
            }
         
        }
        private Capture _videoCapture;
        double totalframes;
        double FrameRate;
        //загрузка видео
        private void button31_Click(object sender, EventArgs e)
        {
            int FPS = 30;
            OpenFileDialog op = new OpenFileDialog();
            op.ShowDialog();
            _videoCapture = new Capture(op.FileName);
            FrameRate = _videoCapture.GetCaptureProperty(Emgu.CV.CvEnum.CapProp.Fps);
            totalframes = _videoCapture.GetCaptureProperty(Emgu.CV.CvEnum.CapProp.FrameCount);
            timer1.Interval = 1000 / FPS;
            timer1.Tick += new EventHandler(timer1_Tick);
            timer1.Start();

        }
        private static BackgroundSubtractor _fgdetector;
        //Обнаружение объекта 
        private void button28_Click(object sender, EventArgs e)
        {
            List<Rectangle> objects = new List<Rectangle>();
            var frame = _videoCapture.QueryFrame();
            _fgdetector = new BackgroundSubtractorMOG2();

            Mat forgroundMask = new Mat();
            _fgdetector.Apply(frame, forgroundMask);
            
           

        }

        private void button32_Click(object sender, EventArgs e)
        {
            
           
            FastDetector detect = new FastDetector(50, true, FastDetector.DetectorType.Type9_16);
            MKeyPoint[] modelKeyPoints;
            modelKeyPoints = detect.Detect(img);
            foreach (MKeyPoint p in modelKeyPoints)
            {
                CvInvoke.Circle(output, Point.Round(p.Point), 3, new Bgr(Color.Blue).MCvScalar, 2);
            }

            byte[] status;
            PointF[] points = new PointF[1000];
            float[] err;
            PointF[] newPositions;
            CvInvoke.CalcOpticalFlowPyrLK(_videoCapture.QueryFrame(), _videoCapture.QueryFrame(), points, new Size(100, 100), 3, new MCvTermCriteria(5), out newPositions, out status, out err);


        }

        private void button33_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            if (op.ShowDialog() == DialogResult.OK)
            {
                img = new Image<Bgr, byte>(op.FileName).Resize(589, 433, Inter.Linear);
                imageBox10.Image = img;
            }
        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {

        }
    }
} 




