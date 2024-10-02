using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Resources;

namespace CaptchaLibrary
{
    public class CaptchaGenerator
    {
        private static Random rand = new Random();
        private static string[] fonts = { "Arial", "Verdana", "Times New Roman", "Courier New" };
        private static Color[] colors = { Color.Red, Color.Blue, Color.Black, Color.Green, Color.Purple };

        public Bitmap GenerateCaptcha(string inputText)
        {
            int width = 200, height = 80;
            Bitmap bitmap = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(bitmap);

            // Load random background image from resources
            var backgroundImage = GetRandomBackgroundImage();
            g.DrawImage(backgroundImage, 0, 0, width, height);

            g.SmoothingMode = SmoothingMode.AntiAlias;

            for (int i = 0; i < inputText.Length; i++)
            {
                char c = inputText[i];

                // Randomize font, color, and angle
                string fontName = fonts[rand.Next(fonts.Length)];
                Font font = new Font(fontName, rand.Next(24, 36), FontStyle.Bold);
                Brush brush = new SolidBrush(colors[rand.Next(colors.Length)]);
                float angle = rand.Next(-45, 45);

                // Rotate the character
                g.TranslateTransform(30 * i + 20, rand.Next(20, 40));
                g.RotateTransform(angle);
                g.DrawString(c.ToString(), font, brush, 0, 0);
                g.ResetTransform();
            }

            // Add noise
            AddNoise(g, width, height);

            g.Flush();
            return bitmap;
        }

        private void AddNoise(Graphics g, int width, int height)
        {
            Pen pen = new Pen(Color.Gray);
            for (int i = 0; i < 10; i++)
            {
                g.DrawLine(pen, rand.Next(width), rand.Next(height), rand.Next(width), rand.Next(height));
            }

            for (int i = 0; i < 10; i++)
            {
                g.FillEllipse(new SolidBrush(Color.LightGray), rand.Next(width), rand.Next(height), 20, 20);
            }
        }

        private Image GetRandomBackgroundImage()
        {
            ResourceManager rm = new ResourceManager("CaptchaLibrary.Resources", typeof(CaptchaGenerator).Assembly);
            return (Image)rm.GetObject("" + rand.Next(1, 10));
        }
    }
}
