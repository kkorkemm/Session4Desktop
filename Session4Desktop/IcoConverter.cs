using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Session4Desktop
{
    public class IcoConverter
    {
        public static void ConvertIco()
        {
            using (FileStream stream = File.OpenWrite(@"C:\C#\Kazan-Neft\Session4Desktop\Session4Desktop\Images\logo.ico"))
            {
                Bitmap bitmap = (Bitmap)Image.FromFile(@"C:\C#\Kazan-Neft\Session4Desktop\Session4Desktop\Images\KN En Colors.png");
                Icon.FromHandle(bitmap.GetHicon()).Save(stream);
            }
        }
    }
}
