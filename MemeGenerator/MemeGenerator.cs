using System.Drawing;

namespace MemeGeneratorProject
{
    public class MemeGenerator
    {
        public void PirquiGenerator(string text, string inputPath, string outputPath)
        {
            Bitmap bmp = CreateImage(text, inputPath);
            bmp.Save(outputPath);
        }

        public Bitmap PirquiGenerator(string text, string inputPath)
        {
            Bitmap bmp = CreateImage(text, inputPath);
            return bmp;
        }

        private Bitmap CreateImage(string text, string inputPath)
        {
            var bmp = new Bitmap(inputPath);
            var gra = Graphics.FromImage(bmp);

            string output = text.Replace("a", "i")
                               .Replace("e", "i")
                               .Replace("i", "i")
                               .Replace("o", "i")
                               .Replace("u", "i");

            var font = new Font("Impact", 35);
            var brush = Brushes.White;

            StringFormat format = new StringFormat { Alignment = StringAlignment.Center };

            var topPoint = new PointF(350, 0);
            gra.DrawString(text, font, brush, topPoint, format);

            var bottomPoint = new PointF(350, 450);
            gra.DrawString(output, font, brush, bottomPoint, format);
            return bmp;
        }

    }
}
