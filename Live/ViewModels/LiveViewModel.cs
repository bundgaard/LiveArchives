using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Windows.Input;
using ObfuscatedArchive;

namespace Live.ViewModels
{
    internal class LiveViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public event EventHandler? OnShutdown;

        public void RaiseShutdown()
        {
            OnShutdown?.Invoke(this, EventArgs.Empty);
        }

        private ObfuscatedArchive.ObfuscatedArchive? _obfuscatedArchive;

        public ObfuscatedArchive.ObfuscatedArchive? Archive
        {
            get => _obfuscatedArchive ?? null;
            set
            {
                _obfuscatedArchive = value;
                OnPropertyChanged(nameof(Archive));
                OnPropertyChanged(nameof(Entries));
            }
        }

        private ObservableCollection<string> _previousEntries = new();

        public ObservableCollection<string> PreviousEntries
        {
            get => _previousEntries;
            set
            {
                _previousEntries = value;
                OnPropertyChanged(nameof(PreviousEntries));
            }
        }

        private string _filePath = "";

        public string FilePath
        {
            get => _filePath; set
            {
                if (_filePath == value) return;
                _filePath = value;
                if (!string.IsNullOrEmpty(value) && !PreviousEntries.Contains(value))
                {
                    PreviousEntries.Add(value);
                }
                OnPropertyChanged(nameof(FilePath));
            }
        }

        public ObservableCollection<ObfuscatedEntry> Entries => Archive?.Entries != null ? new ObservableCollection<ObfuscatedEntry>(Archive.Entries) : [];
        public ICommand ViewEntryCommand { get; }
        public ICommand OnLoadCommand { get; }

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
            OnShutdown += (s, e) =>
            {
                SaveLastEntries();
            };
            LoadLastEntries();
        }

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void LoadLastEntries()
        {
            if (File.Exists("previous_entries.json"))
            {
                Debug.WriteLine("Loading previous entries...");
                string json = File.ReadAllText("previous_entries.json");
                var entries = JsonSerializer.Deserialize<ObservableCollection<string>>(json);
                if (entries != null)
                {
                    PreviousEntries = entries;
                }
            }
        }

        private void SaveLastEntries()
        {
            string json = JsonSerializer.Serialize(PreviousEntries);
            File.WriteAllText("previous_entries.json", json);
        }

        private void OnLoad()
        {
            Archive = ObfuscatedArchive.ObfuscatedArchive.From(FilePath);
        }

        private byte[] DeobfuscateBytes(ObfuscatedEntry entry)
        {
            var bytes = entry.GetBytes(_filePath);
            var keyGen = new Keygenerator(entry.Key);
            var key = keyGen.Next();

            for (int i = 0; i < bytes.Length;)
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