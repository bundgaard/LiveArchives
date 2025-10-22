using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Live.Services
{
    internal static class BitmapService
    {
        internal static BitmapImage BytesToImage(byte[] buffer)
        {
            var image = new BitmapImage();
            using var memoryStream = new MemoryStream(buffer);
            image.BeginInit();
            image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
            image.CacheOption = BitmapCacheOption.OnLoad;
            image.StreamSource = memoryStream;
            image.EndInit();
            image.Freeze();
            return image;
        }
    }
}
