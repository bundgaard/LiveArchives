using System.Diagnostics;

namespace ObfuscatedArchive
{
    [DebuggerDisplay("Archive Offset {Offset} Size {Size}")]
    public class ObfuscatedArchive : ObfuscatedRange
    {

        private readonly string _path;

        public ObfuscatedHeader Header { get; set; } // signature, version, key
        public List<ObfuscatedEntry> Entries { get; set; } // offset, size, key, name

        private ObfuscatedArchive(string path, BinaryReader reader, long offset, long size)
        {
            _path = path;

            Offset = offset;
            Size = size;
            Header = ObfuscatedHeader.From(reader);
            Entries = GetIndex(reader);
        }

        private List<ObfuscatedEntry> GetIndex(BinaryReader reader)
        {
            List<ObfuscatedEntry> result = [];
            ObfuscatedEntry? entry;
            while ((entry = ObfuscatedEntry.From(reader, Header.Key)) != null)
            {
                result.Add(entry);
            }
            return result;
        }

        public static ObfuscatedArchive From(string path)
        {
            try
            {
                using var reader = new BinaryReader(File.OpenRead(path));
                var position = reader.BaseStream.Position;
                var size = reader.BaseStream.Length;
                return new ObfuscatedArchive(path, reader, position, size);
            }
            catch (Exception)
            {
                throw new ObfuscatedArchiveException("Failed to create obfuscated archvie");
            }
        }
    }
}
