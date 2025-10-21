using System.Text;
using System.Text.Json;

namespace LiveArchives
{
    internal class Archive
    {

        public static Archive Instance { get; } = new Archive();
        private string _filePath = string.Empty;
        private ObfuscatedArchive.ObfuscatedArchive? _archive = null;

        private Archive() { }

        public void Open(string filePath)
        {
            if (_filePath != filePath)
            {
                _filePath = filePath;
                // Load the archive.
                _archive = ObfuscatedArchive.ObfuscatedArchive.From(filePath);
            }
        }

        public void List()
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            var doc = JsonSerializer.SerializeToUtf8Bytes(_archive?.Entries, options);
            Console.WriteLine(Encoding.UTF8.GetString(doc));

        }
        public List<ObfuscatedArchive.ObfuscatedEntry> Search(string searchTerm)
        {
            var result = _archive?.Entries.Where(entry => entry.Name.Value.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                .ToList();
            return result ?? [];
        }
    }
}
