using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Live
{
    /// <summary>
    /// Interaction logic for ImageViewer.xaml
    /// </summary>
    public partial class ImageViewer : Window
    {

        public ImageViewer()
        {
            InitializeComponent();
        }

        public ImageViewer(byte[] bytes)
        {
            InitializeComponent();
            DataContext = new ImageModelView();
            var vm = DataContext as ImageModelView;
            vm.Image = BytesToImage(bytes);
        }


        private BitmapImage BytesToImage(byte[] buffer)
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
