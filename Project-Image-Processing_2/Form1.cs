using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.IO;


namespace Project_Image_Processing_2
{
    public partial class Form1 : Form
    {
        Image file;
        
        Bitmap newBitmap;
        Boolean opened = false;
        int lastCol = 0;
        int brightness = 0;
        float contrast = 0;
        float gamma = 1;
        
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult dr = openFileDialog1.ShowDialog(); 
            if (dr == DialogResult.OK)
            {
                file = Image.FromFile(openFileDialog1.FileName);
                newBitmap = new Bitmap(openFileDialog1.FileName);
                pictureBox1.Image = file;
                opened = true;
            }
           
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            Bitmap rbmp = new Bitmap(openFileDialog1.FileName);
            for (int y = 0; y < rbmp.Height; y++)
            {
                for(int x = 0; x < rbmp.Width; x++)
                {
                    Color p = rbmp.GetPixel(x, y);
                    int a = p.A;
                    int r = p.R;
                    int g = p.G;
                    int b = p.B;

                   rbmp.SetPixel(x,y,Color.FromArgb(a,r,0,0));
                }
            }
            pictureBox2.Image = rbmp;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            Bitmap gbmp = new Bitmap(openFileDialog1.FileName);
            for (int y = 0; y < gbmp.Height; y++)
            {
                for (int x = 0; x < gbmp.Width; x++)
                {
                    Color p = gbmp.GetPixel(x, y);
                    int a = p.A;
                    int r = p.R;
                    int g = p.G;
                    int b = p.B;

                    gbmp.SetPixel(x, y, Color.FromArgb(a, 0, g, 0));
                }
            }
            pictureBox2.Image = gbmp;
        }


        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {

            Bitmap bbmp = new Bitmap(openFileDialog1.FileName);
            for (int y = 0; y < bbmp.Height; y++)
            {
                for (int x = 0; x < bbmp.Width; x++)
                {
                    Color p = bbmp.GetPixel(x, y);
                    int a = p.A;
                    int r = p.R;
                    int g = p.G;
                    int b = p.B;

                    bbmp.SetPixel(x, y, Color.FromArgb(a, 0, 0, b));
                }
            }
            pictureBox2.Image = bbmp;
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {

            Bitmap rgbbmp = new Bitmap(openFileDialog1.FileName);
            for (int y = 0; y < rgbbmp.Height; y++)
            {
                for (int x = 0; x < rgbbmp.Width; x++)
                {
                    Color p = rgbbmp.GetPixel(x, y);
                    int a = p.A;
                    int r = p.R;
                    int g = p.G;
                    int b = p.B;

                    rgbbmp.SetPixel(x, y, Color.FromArgb(a, r, g, b));
                }
            }
            pictureBox2.Image = rgbbmp;
        }
        
        
        private void button11_Click_1(object sender, EventArgs e)
        {
            if(newBitmap != null)
            {
                newBitmap.RotateFlip(RotateFlipType.RotateNoneFlipY);
                pictureBox2.Image = newBitmap;
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (newBitmap != null)
            {
                newBitmap.RotateFlip(RotateFlipType.RotateNoneFlipX);
                pictureBox2.Image = newBitmap;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult dr = saveFileDialog1.ShowDialog();
            if (dr == DialogResult.OK)
            {
                if (opened)
                {
                    if (saveFileDialog1.FileName.Substring(saveFileDialog1.FileName.Length - 3).ToLower() == "bmp")
                    {
                        pictureBox2.Image.Save(saveFileDialog1.FileName, ImageFormat.Bmp);
                    }

                    if (saveFileDialog1.FileName.Substring(saveFileDialog1.FileName.Length - 3).ToLower() == "jpg")
                    {
                        pictureBox2.Image.Save(saveFileDialog1.FileName, ImageFormat.Jpeg);
                    }

                    if (saveFileDialog1.FileName.Substring(saveFileDialog1.FileName.Length - 3).ToLower() == "png")
                    {
                        pictureBox2.Image.Save(saveFileDialog1.FileName, ImageFormat.Png);
                    }

                    if (saveFileDialog1.FileName.Substring(saveFileDialog1.FileName.Length - 3).ToLower() == "gif")
                    {
                        pictureBox2.Image.Save(saveFileDialog1.FileName, ImageFormat.Gif);
                    }
                }
                else
                {
                    MessageBox.Show("You need to open an image! ");
                }

            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            for(int x = 0; x < newBitmap.Width; x++)
            {
                for(int y = 0; y < newBitmap.Height; y++)
                {
                    Color origanColor = newBitmap.GetPixel(x, y);
                    int grayScale = (int)((origanColor.R * .3)  + (origanColor.G  * .59) + (origanColor.B * .11));
                    Color newColor = Color.FromArgb(grayScale, grayScale, grayScale);

                    newBitmap.SetPixel(x, y, newColor);

                }    
            }
            pictureBox2.Image = newBitmap;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = null;
            pictureBox2.Image = null;   
        }

        private void button5_Click(object sender, EventArgs e)
        {
            for (int x = 1; x < newBitmap.Width; x++)
            {
                for (int y = 1; y < newBitmap.Height; y++)
                {
                    try
                    {
                        Color prevX = newBitmap.GetPixel(x - 1, y);
                        Color nextX = newBitmap.GetPixel(x + 1, y);
                        Color prevY = newBitmap.GetPixel(x, y - 1);
                        Color nextY = newBitmap.GetPixel(x, y + 1);

                        int avgR = (int)((prevX.R + nextX.R + prevY.R + nextY.R) / 4);
                        int avgG = (int)((prevX.G + nextX.G + prevY.G + nextY.G) / 4);
                        int avgB = (int)((prevX.B + nextX.B + prevY.B + nextY.B) / 4);

                        newBitmap.SetPixel(x, y, Color.FromArgb(avgR, avgG, avgB));
                    }
                    catch (Exception) { }
                }
            }
            pictureBox2.Image = newBitmap;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            for (int x = 0; x < newBitmap.Width; x++)
            {
                for (int y = 0; y < newBitmap.Height; y++)
                {
                    Color pixel = newBitmap.GetPixel(x, y);
                    int red = pixel.R;
                    int green = pixel.G;
                    int blue = pixel.B;

                    newBitmap.SetPixel(x, y, Color.FromArgb(255-red, 255-green, 255-blue));
                }
            }
            pictureBox2.Image = newBitmap;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Bitmap nB = new Bitmap(newBitmap.Width, newBitmap.Height);
          
            for(int x = 1; x <= newBitmap.Width - 1; x++)
            {
                for(int y = 1; y <= newBitmap.Height - 1; y++)
                {
                    nB.SetPixel(x, y, Color.DarkGray);
                }    
            } 
            
            for(int x = 1; x <= newBitmap.Width - 1; x++)
            {
                for(int y = 1; y <= newBitmap.Height - 1; y++)
                {
                    try
                    {
                        Color pixel = newBitmap.GetPixel(x, y);
                        int colVal = (pixel.R + pixel.G + pixel.B);
                        if(lastCol == 0) lastCol = (pixel.R + pixel.G + pixel.B);
                        int diff;
                        if(colVal > lastCol)
                        {
                            diff = colVal - lastCol;
                        }
                        else diff = lastCol - colVal;
                        if(diff > 100)
                        {
                            nB.SetPixel(x, y, Color.Gray);
                            lastCol = colVal;
                        }

                    }
                    catch (Exception) { }
                }  
            }    
            for(int y = 1; y <= newBitmap.Height -1; y++)
            {
                for(int x = 1; x <= newBitmap.Width -1; x++)
                {
                    try
                    {
                        Color pixel = newBitmap.GetPixel(x, y);
                        int colVal = (pixel.R + pixel.G + pixel.B);
                        if (lastCol == 0) lastCol = (pixel.R + pixel.G + pixel.B);
                        int diff;
                        if (colVal > lastCol)
                        {
                            diff = colVal - lastCol;
                        }
                        else diff = lastCol - colVal;
                        if (diff > 100)
                        {
                            nB.SetPixel(x, y, Color.Gray);
                            lastCol = colVal;
                        }

                    }
                    catch (Exception) { }
                }
            }
            pictureBox2.Image = nB;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (!opened)
            {
                MessageBox.Show("Open an Image then apply changes");
            }
            else
            {


                Image img = pictureBox1.Image;                            
                Bitmap bmpInverted = new Bitmap(img.Width, img.Height);   
                ImageAttributes ia = new ImageAttributes();                 
                ColorMatrix cmPicture = new ColorMatrix(new float[][]       
                {
                    new float[]{1+0.3f, 0, 0, 0, 0},
                    new float[]{0, 1+0f, 0, 0, 0},
                    new float[]{0, 0, 1+5f, 0, 0},
                    new float[]{0, 0, 0, 1, 0},
                    new float[]{0, 0, 0, 0, 1}
                });
                ia.SetColorMatrix(cmPicture);           
                Graphics g = Graphics.FromImage(bmpInverted); 
                g.DrawImage(img, new Rectangle(0, 0, img.Width, img.Height), 0, 0, img.Width, img.Height, GraphicsUnit.Pixel, ia);


                g.Dispose();                           
                pictureBox2.Image = bmpInverted;

            }

        }

        private void button9_Click(object sender, EventArgs e)
        {
            pictureBox1.MouseDown += new MouseEventHandler(pictureBox1_MouseDown);
            pictureBox1.MouseMove += new MouseEventHandler(pictureBox1_MouseMove);
            pictureBox1.MouseEnter += new EventHandler(pictureBox1_MouseEnter);
            Controls.Add(pictureBox1);
        }
        int crpX, crpY, rectW, rectH;
        public Pen crpPen = new Pen(Color.White);
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                Cursor = Cursors.Cross;
                crpPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
                
                // for crop rectangle
                // first click on image
                crpX = e.X;
                crpY = e.Y;

                
            }    
        }

        private void button10_Click(object sender, EventArgs e)
        {
            label2.Text = "Changed" + rectW + "," + rectH;
            Cursor = Cursors.Default;
            // Cropped Image
            Bitmap bmp2 = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            pictureBox1.DrawToBitmap(bmp2, pictureBox1.ClientRectangle);

            Bitmap crpImg = new Bitmap(rectW, rectH);

            for(int i = 0; i < rectW; i++)
            {
                for(int y = 0; y < rectH; y++)
                {
                    Color pxlclr = bmp2.GetPixel(crpX + i, crpY + y);
                    crpImg.SetPixel(i, y, pxlclr);
                } 
            }
            pictureBox2.Image = (Image)crpImg;
            pictureBox2.SizeMode = PictureBoxSizeMode.CenterImage;
            MessageBox.Show("Crop Image Successful!");
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            label6.Text = trackBar1.Value.ToString();
            pictureBox2.Image = AdjustBrightness(newBitmap, trackBar1.Value);
        }
        public static Bitmap AdjustBrightness(Bitmap Image, int Value)
        {
            Bitmap TempBitmap = Image;
            float FinalValue = (float)Value / 255.0f;

            Bitmap NewBitmap = new Bitmap(TempBitmap.Width, TempBitmap.Height);
            
            Graphics NewGraphics = Graphics.FromImage(NewBitmap);

            float[][] FloatColorMatrix ={
                     new float[] {1, 0, 0, 0, 0},
                     new float[] {0, 1, 0, 0, 0},
                     new float[] {0, 0, 1, 0, 0},
                     new float[] {0, 0, 0, 1, 0},
                     new float[] {FinalValue, FinalValue, FinalValue, 1, 1}
                 };

            ColorMatrix NewColorMatrix = new ColorMatrix(FloatColorMatrix);
            
            ImageAttributes Attributes = new ImageAttributes();
            
            Attributes.SetColorMatrix(NewColorMatrix);
            
            NewGraphics.DrawImage(TempBitmap, new Rectangle(0, 0, TempBitmap.Width, TempBitmap.Height), 0, 0, TempBitmap.Width, TempBitmap.Height, GraphicsUnit.Pixel, Attributes);
           
            Attributes.Dispose();
            
            NewGraphics.Dispose();
            
            return NewBitmap;
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            label7.Text = trackBar2.Value.ToString();

            contrast = 0.04f * trackBar2.Value;

            Bitmap bm = new Bitmap(newBitmap.Width, newBitmap.Height);

            Graphics g = Graphics.FromImage(bm);
            ImageAttributes ia = new ImageAttributes();

            ColorMatrix cm = new ColorMatrix(new float[][] {
                            new float[] {contrast, 0f, 0f, 0f, 0f},
                            new float[]{0f, contrast, 0f, 0f, 0f},
                            new float[]{0f, 0f, contrast, 0f, 0f},
                            new float[]{0f, 0f, 0f, 1f, 0f},
            
                            new float[]{0.001f, 0.001f, 0.001f, 0f, 1f}});

            ia.SetColorMatrix(cm);

            g.DrawImage(newBitmap, new Rectangle(0, 0, newBitmap.Width, newBitmap.Height), 0, 0, newBitmap.Width, newBitmap.Height, GraphicsUnit.Pixel, ia);
            g.Dispose();
            ia.Dispose();

            pictureBox2.Image = bm;
        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            label9.Text = trackBar3.Value.ToString();

            gamma = 0.04f * trackBar3.Value;

            Bitmap bm = new Bitmap(newBitmap.Width, newBitmap.Height);
            Graphics g = Graphics.FromImage(bm);
            ImageAttributes ia = new ImageAttributes();

            ia.SetGamma(gamma);
            g.DrawImage(newBitmap, new Rectangle(0, 0, newBitmap.Width, newBitmap.Height), 0, 0, newBitmap.Width, newBitmap.Height, GraphicsUnit.Pixel, ia);
            g.Dispose();
            ia.Dispose();

            pictureBox2.Image = bm;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            
        }
       

        private void trackBar4_Scroll(object sender, EventArgs e)
        {
            label10.Text = trackBar4.Value.ToString();

            if (trackBar4.Value > 0)
            {
                pictureBox2.Image = null;
                pictureBox2.Image = ZoomPicture(imgOriginal, new Size(trackBar4.Value, trackBar4.Value));
            }
        }
        private Image imgOriginal;
        public Image ZoomPicture(Image img, Size size)
        {
            pictureBox1.Image = Image.FromFile(openFileDialog1.FileName);
            imgOriginal = pictureBox1.Image;
            Bitmap bm = new Bitmap(imgOriginal, Convert.ToInt32(imgOriginal.Width * size.Width), Convert.ToInt32(imgOriginal.Height * size.Height));
            Graphics gpu = Graphics.FromImage(bm);
            gpu.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            return bm;
        }
        PictureBox org;
        private void Form1_Load(object sender, EventArgs e)
        {
            trackBar4.Minimum = 0;
            trackBar4.Maximum = 5;
            trackBar4.SmallChange = 1;
            trackBar4.LargeChange = 1;
            trackBar4.UseWaitCursor = false;

            // reduce flickering
            this.DoubleBuffered = true;
            org = new PictureBox();
            org.Image = pictureBox2.Image;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            base.OnMouseEnter(e);
            Cursor = Cursors.Cross;
        }
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if(e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                pictureBox1.Refresh();
                rectW = e.X - crpX;
                rectH = e.Y - crpY;
                Graphics g = pictureBox1.CreateGraphics();
                g.DrawRectangle(crpPen, crpX, crpY, rectW, rectH);
                g.Dispose();
            }
        }
        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            Cursor = Cursors.Default;
        }

    }
}
