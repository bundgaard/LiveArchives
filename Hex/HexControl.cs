using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Media;

namespace Hex
{
    public class HexControl : Control, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;


        protected void OnRender(DrawingContext context)
        {

        }
    }
}
