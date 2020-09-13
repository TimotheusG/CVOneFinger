using System;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

class Program
{
    static Color GetPixel(Point position)
    {
        using (var bitmap = new Bitmap(1, 1))
        {
            using (var graphics = Graphics.FromImage(bitmap))
            {
                graphics.CopyFromScreen(position, new Point(0, 0), new Size(1, 1));
            }
            return bitmap.GetPixel(0, 0);
        }
    }

    static void Main()
    {
        while(true)
        {
            GetColorNames();
            Thread.Sleep(1000);
        }        
    }

    private static void GetColorNames()
    {
        Color color = GetPixel(Cursor.Position);
        Console.WriteLine(color);
        var colorLookup = Enum.GetValues(typeof(KnownColor))
              .Cast<KnownColor>()
              .Select(Color.FromKnownColor)
              .ToLookup(c => c.ToArgb());
        foreach (var namedColor in colorLookup[color.ToArgb()])
        {
            Console.WriteLine(namedColor.Name);
        }
    }
}