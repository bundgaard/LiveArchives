using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
using Live.Services;
using Live.ViewModels;

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
            Init(null);
        }
        private void Window_SourceInitialized(object? sender, System.EventArgs e)
        {
            var hwnd = new System.Windows.Interop.WindowInteropHelper(this).Handle;
            if (hwnd != IntPtr.Zero)
            {
                Immersive.Immersive.EnableImmersiveDarkMode(hwnd, true);
            }

        }
        public ImageViewer(byte[] bytes)
        {
            InitializeComponent();
            Init(bytes);
        }

        private void Init(byte[]? bytes)
        {
            SourceInitialized += Window_SourceInitialized;

            DataContext = new ImageModelView();
            if (bytes == null || bytes.Length == 0)
                return;
            var vm = DataContext as ImageModelView ?? new ImageModelView();
            vm.Image = BitmapService.BytesToImage(bytes);
        }


    }
}
