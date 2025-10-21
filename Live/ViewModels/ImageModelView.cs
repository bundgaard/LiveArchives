using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media.Imaging;

namespace Live.ViewModels
{
    internal class ImageModelView : INotifyPropertyChanged
    {
        private BitmapImage _image = new();
        public BitmapImage Image
        {
            get => _image; set
            {
                if (_image != value)
                {
                    _image = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(Image));
                }
            }
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
