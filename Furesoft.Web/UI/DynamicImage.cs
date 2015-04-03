﻿using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace Furesoft.Web.UI
{
    public class DynamicImage
    {
        public string Name { get; set; }
        public Style Style { get; set; }
        public int Width { get; private set; }
        public int Heigth { get; private set; }

        private Bitmap _buffer;
        private Graphics _gr;

        public DynamicImage(int width, int height)
        {
            Style = new Style();
            _buffer = new Bitmap(width, height);
            Width = width;
            Heigth = height;

            _gr = Graphics.FromImage(_buffer);
        }

        public void DrawRectangle(Rectangle rec, Color color)
        {
            _gr.DrawRectangle(new Pen(color), rec);
        }
        public void DrawImage(Point pos, Image img)
        {
            _gr.DrawImage(img, pos);
        }
        public void FillRectangle(Rectangle rec, Color color)
        {
            _gr.FillRectangle(new SolidBrush(color), rec);
        }
        public void Clear(Color c)
        {
            _gr.Clear(c);
        }

        public static implicit operator string(DynamicImage btn)
        {
            return btn.ToString();
        }

        public override string ToString()
        {
            var Src = Converter.ToWebString(_buffer, ImageFormat.Bmp);

            return string.Format("<img name='{0}' width='{1}' heigth='{2}' style='{3}' src='{4}' />", Name, Width, Heigth, Style, Src);
        }
    }
}