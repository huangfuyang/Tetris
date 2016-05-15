using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Drawing;

namespace Tetris
{
    public class BrushData
    {
        public ImageBrush none;
        public ImageBrush brush1;
        public ImageBrush brush2;
        public ImageBrush brush3;
        public ImageBrush brush4;
        public ImageBrush brush5;
        public ImageBrush brush6;
        public ImageBrush brush7;
        public ImageBrush block8;


        public BrushData()
        {
            none = new ImageBrush();

            brush1 = new ImageBrush(GetImage(Properties.Resources._1));
            brush2 = new ImageBrush(GetImage(Properties.Resources._2));
            brush3 = new ImageBrush(GetImage(Properties.Resources._3));
            brush4 = new ImageBrush(GetImage(Properties.Resources._4));
            brush5 = new ImageBrush(GetImage(Properties.Resources._5));
            brush6 = new ImageBrush(GetImage(Properties.Resources._6));
            brush7 = new ImageBrush(GetImage(Properties.Resources._7));
            block8 = new ImageBrush(GetImage(Properties.Resources._8));
        }

        public ImageBrush getBrush(int typeId)
        {
            switch (typeId)
            { 
                default:
                    return none;
                case 0:
                    return brush1;
                case 1:
                    return brush2;
                case 2:
                    return brush3;
                case 3:
                    return brush4;
                case 4:
                    return brush5;
                case 5:
                    return brush6;
                case 6:
                    return brush7;
            }
        }

        private System.Windows.Media.Imaging.BitmapImage GetImage(System.Drawing.Bitmap image)
        {
            System.Windows.Media.Imaging.BitmapImage bmp = new System.Windows.Media.Imaging.BitmapImage();
            System.IO.MemoryStream mem = new System.IO.MemoryStream();
            image.Save(mem, System.Drawing.Imaging.ImageFormat.Png);
            bmp.BeginInit();
            bmp.StreamSource = mem;
            bmp.EndInit();
            return bmp;
        }

    }

}
