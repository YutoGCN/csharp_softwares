using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _230413_plt_test
{
    public partial class plt_test : Form
    {
        public plt_test()
        {
            InitializeComponent();
            this.pictureBox1.MouseWheel += PictureBox1_MouseWheel;
        }

        private void PictureBox1_MouseWheel(object sender, MouseEventArgs e)
        {
            System.Drawing.Point sp = System.Windows.Forms.Cursor.Position;
            System.Drawing.Point cp = this.PointToClient(sp);
            int x = cp.X;
            int y = cp.Y;

            Graphics g = pictureBox1.CreateGraphics();
            int width = (int)g.VisibleClipBounds.Width;
            int height = (int)g.VisibleClipBounds.Height;

            if (e.Delta > 0)
            {
                mandelpltsetparam(x_center += step * (x - (width / 2)), y_center += step * (y - (height / 2)), step / 1.2);
            }
            else
            {
                mandelpltsetparam(x_center += step * (x - (width / 2)), y_center += step * (y - (height / 2)), step / 0.8);
            }
        }

        private double x_center;
        private double y_center;
        private double step = 2.0/540;

        private void button1_Click(object sender, EventArgs e)
        {
            Mandelpltreset();
        }

        private void mandelpltsetparam(double _x_center, double _y_center, double _step)
        {
            x_center = _x_center;
            y_center = _y_center;
            step = _step;
            Graphics g = pictureBox1.CreateGraphics();

            int width = (int)g.VisibleClipBounds.Width;
            int height = (int)g.VisibleClipBounds.Height;

            mandelplt(g, width, height, x_center, y_center, step);
        }

        public void Mandelpltreset()
        {
            mandelpltsetparam(-0.5, 0, 2.0 / 540);
        }

        private void mandelplt(Graphics g,int width,int height,double x_center, double y_center,double step)
        {
            Bitmap picData = new Bitmap(width, height);

            double xmin = x_center -width * step/2;
            double ymin = y_center -height * step/2;
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    // 複素数の計算
                    double a = xmin + x * step;
                    double b = ymin + y * step;
                    double ca = a;
                    double cb = b;
                    int n = 0;
                    while (n < 255)
                    {
                        double aa = a * a - b * b;
                        double bb = 2 * a * b;
                        a = aa + ca;
                        b = bb + cb;
                        if (a * a + b * b > 4)
                        {
                            break;
                        }
                        n++;
                    }

                    // 色の設定
                    picData.SetPixel(x, y, Color.FromArgb(n, n, n ));
                }
            }

            g.DrawImage(picData, 0, 0);
            g.Dispose();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            System.Drawing.Point sp = System.Windows.Forms.Cursor.Position;
            System.Drawing.Point cp = this.PointToClient(sp);
            int x = cp.X;
            int y = cp.Y;

            Graphics g = pictureBox1.CreateGraphics();
            int width = (int)g.VisibleClipBounds.Width;
            int height = (int)g.VisibleClipBounds.Height;

            mandelpltsetparam(x_center += step * (x-width/2), y_center += step * (y-height/2), step / 1.2);
        }
    }
}
