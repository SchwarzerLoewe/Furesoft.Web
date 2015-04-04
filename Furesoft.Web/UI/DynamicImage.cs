﻿using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Xml;
using Furesoft.Web.UI.Base;

namespace Furesoft.Web.UI
{
    public class DynamicImage
    {
        public int Width { get; private set; }
        public int Heigth { get; private set; }

        private Image _buffer;
        private Graphics _gr;

        public static DynamicImage FromXml(string src)
        {
            var doc = new XmlDocument();
            doc.LoadXml(src);

            if (doc.DocumentElement.Name == "image")
            {
                var ret = new DynamicImage(int.Parse(doc.DocumentElement.Attributes["width"].Value), int.Parse(doc.DocumentElement.Attributes["height"].Value));

                foreach (XmlNode el in doc.DocumentElement.ChildNodes)
                {
                    if (el.Name == "rec")
                    {
                        ret.DrawRectangle(new Rectangle(toint(el, "x"), toint(el, "y"), toint(el, "width"), toint(el, "heigth")), Color.FromName(el.Attributes["color"].Value));
                    }
                    else if (el.Name == "frec")
                    {
                        ret.FillRectangle(new Rectangle(toint(el, "x"), toint(el, "y"), toint(el, "width"), toint(el, "heigth")), Color.FromName(el.Attributes["color"].Value));
                    }
                }

                return ret;
            }

            return null;
        }

        private static int toint(XmlNode node, string name)
        {
            return int.Parse(node.Attributes[name].Value);
        }

        public DynamicImage(int width, int height)
        {
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
            var Src = Converter.ToWebString(_buffer, ImageFormat.Png);

            return Src;
        }
    }
}