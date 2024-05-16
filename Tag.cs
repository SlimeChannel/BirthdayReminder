using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirthdayReminder
{
    public class Tag
    {
        private string? name;
        private Color color;
        public string? Name
        {
            get { return name; }
            set { name = value; }
        }
        public string Color
        {
            get { return ColorTranslator.ToHtml(color); }
            set { color = ColorTranslator.FromHtml(value); }
        }
        public string BorderColor
        {
            get
            {
                
                if(!IsDarkColor(color))
                    return ColorTranslator.ToHtml(System.Drawing.Color.FromArgb(color.R + 128 < 255 ? color.R + 128 : 255, color.G + 128 < 255 ? color.G + 128 : 255, color.B + 128 < 255 ? color.B + 128 : 255));
                return ColorTranslator.ToHtml(System.Drawing.Color.FromArgb(color.R - 128 > 0 ? color.R - 128 : 0, color.G - 128 > 0 ? color.G - 128 : 0, color.B - 128 > 0 ? color.B - 128 : 0));
            }
        }
        public string TextColor
        {
            get
            {
                if (IsDarkColor(color))
                    return "#FFF";
                return "#000";
            }
        }
        double CalculateRelativeLuminance(Color color)
        {
            double r = color.R / 255.0;
            double g = color.G / 255.0;
            double b = color.B / 255.0;

            r = r <= 0.03928 ? r / 12.92 : Math.Pow((r + 0.055) / 1.055, 2.4);
            g = g <= 0.03928 ? g / 12.92 : Math.Pow((g + 0.055) / 1.055, 2.4);
            b = b <= 0.03928 ? b / 12.92 : Math.Pow((b + 0.055) / 1.055, 2.4);

            return 0.2126 * r + 0.7152 * g + 0.0722 * b;
        }

        bool IsDarkColor(Color color)
        {
            const double darkThreshold = 0.179;

            double relativeLuminance = CalculateRelativeLuminance(color);

            return relativeLuminance < darkThreshold;
        }
    }
}
