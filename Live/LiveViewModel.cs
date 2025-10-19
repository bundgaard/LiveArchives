using ObfuscatedArchive;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
namespace Live
{
    class LiveViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private ObfuscatedArchive.ObfuscatedArchive? _obfuscatedArchive;
        public ObfuscatedArchive.ObfuscatedArchive Archive
        {
            get => _obfuscatedArchive; set
            {
                _obfuscatedArchive = value;
                OnPropertyChanged(nameof(Archive));
                OnPropertyChanged(nameof(Entries));
            }
        }
        private string _filePath = "";
        public string FilePath
        {
            get => _filePath; set
            {
                if (_filePath == value) return;
                _filePath = value;
                OnPropertyChanged(nameof(FilePath));
            }
        }

        public ObservableCollection<ObfuscatedEntry> Entries => Archive?.Entries != null ? new ObservableCollection<ObfuscatedEntry>(Archive.Entries) : [];
        public ICommand ViewEntryCommand { get; }
        public ICommand OnLoadCommand { get; }
        private void OnLoad()
        {
            Archive = ObfuscatedArchive.ObfuscatedArchive.From(FilePath);
        }
        public LiveViewModel()
        {
            
            ViewEntryCommand = new RelayCommand(obj =>
            {
                if (obj == null) return;
                ViewEntry((ObfuscatedEntry)obj);
            });

            OnLoadCommand = new RelayCommand(obj => 
            {
                OnLoad();
            });
        }


        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private byte[] DeobfuscateBytes(ObfuscatedEntry entry)
        {
            var bytes = entry.GetBytes(_filePath);
            var keyGen = new Keygenerator(entry.Key);
            var key = keyGen.Next();
            
            for(int i = 0; i < bytes.Length;) 
            {
                bytes[i] ^= (byte)(key >> (i << 3));
                ++i;
                if (0 == (i & 3))
                {
                    key = keyGen.Next();
                }
            }
            return bytes;

        }
        private void ViewEntry(ObfuscatedEntry entry)
        {

            if (entry != null && IsImage(entry))
            {
                // Show image logic here
                var bytes = DeobfuscateBytes(entry);
                var imageViewer = new ImageViewer(bytes);
                imageViewer.Show();

            }
        }

        private bool IsImage(ObfuscatedEntry entry)
        {
            var extension = System.IO.Path.GetExtension(entry.Name.Value)?.ToLower();
            return extension == ".png" || extension == ".jpg" || extension == ".jpeg" || extension == ".bmp";
        }
    }
}

