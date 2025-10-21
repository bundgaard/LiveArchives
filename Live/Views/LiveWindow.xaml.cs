using System.Windows;

namespace Live
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class LiveWindow : Window
    {
        public LiveWindow()
        {
            InitializeComponent();
            SourceInitialized += Window_SourceInitialized;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            (DataContext as ViewModels.LiveViewModel)?.RaiseShutdown();
        }

        private void Window_SourceInitialized(object? sender, System.EventArgs e)
        {
            var hwnd = new System.Windows.Interop.WindowInteropHelper(this).Handle;
            if (hwnd != IntPtr.Zero)
            {
                Immersive.Immersive.EnableImmersiveDarkMode(hwnd, true);
            }
            
        }
    }
}