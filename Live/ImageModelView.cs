using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Live
{
    internal class ImageModelView : INotifyPropertyChanged
    {
        private BitmapImage _image;
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
