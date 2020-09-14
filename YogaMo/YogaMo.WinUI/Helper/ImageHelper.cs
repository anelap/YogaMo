using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YogaMo.WinUI.Helper
{
    public static class ImageHelper
    {

        public static byte[] ImageToByte(Image img)
        {
            using (var stream = new MemoryStream())
            {
                img.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                return stream.ToArray();
            }
        }

        public static Image ByteToImage(byte[] profilePicture)
        {
            using (var stream = new MemoryStream(profilePicture))
            {
                return Image.FromStream(stream);
            }
        }
    }
}
