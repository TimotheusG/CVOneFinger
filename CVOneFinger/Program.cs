using System;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Threading;
using System.Windows.Forms;
using WindowsInput;
using WindowsInput.Native;

class Program
{
    public static InputSimulator sim = new InputSimulator();
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
        Thread.Sleep(5000);
        while (true)
        {
            if(!CheckBlue() && !CheckRed())
            {
                CheckBoss();
            }
            
            Thread.Sleep(300);   
        }        
    }

    private static bool CheckBoss()
    {
        Point point = new Point(1250, 224);
        Color color = GetPixel(point);
        if (color.R > 200)
        {
            sim.Keyboard.KeyPress(VirtualKeyCode.RIGHT);
            return true;
        }
        if (color.B > 200)
        {
            sim.Keyboard.KeyPress(VirtualKeyCode.LEFT);
            return true;
        }
        return false;
    }

    private static bool CheckRed()
    {
        Point point = new Point(1339, 759);
        Color color = GetPixel(point);
        if(color.R > 200)
        {
            sim.Keyboard.KeyPress(VirtualKeyCode.RIGHT);
            return true;
        }
        return false;
    }

    private static bool CheckBlue()
    {
        Point point = new Point(1200, 763);
        Color color = GetPixel(point);
        if (color.B > 200)
        {
            sim.Keyboard.KeyPress(VirtualKeyCode.LEFT);
            return true;
        }
        return false;
    }

    private static void GetColorNames()
    {
        Point point = Cursor.Position;
        Color color = GetPixel(point);
        string s = "At Point: " + point + ", ";
        s += color;
        string s1 = "";
        var colorLookup = Enum.GetValues(typeof(KnownColor))
              .Cast<KnownColor>()
              .Select(Color.FromKnownColor)
              .ToLookup(c => c.ToArgb());
        foreach (var namedColor in colorLookup[color.ToArgb()])
        {
            s1 += namedColor.Name + " ";
        }
        s += " " + s1;
        Console.WriteLine(s);
    }
}