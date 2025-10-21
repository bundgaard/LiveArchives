using System.Text;

namespace ObfuscatedArchive
{
    public class ObfuscatedHeader
    {
        public string Signature { get; }
        public byte Version { get; }
        public ObfuscatedKey Key { get; }
        private ObfuscatedHeader(string signature, byte version, ObfuscatedKey key)
        {
            Signature = signature;
            Version = version;
            Key = key;
        }

        public static ObfuscatedHeader From(BinaryReader reader)
        {
            // We could use a constructor, but I find this more readable.
            // This always starts from position 0 of reader.
            // Signature up to 0x00
            // Version 1 byte 
            // Key 4 byte

            try
            {
                var bytes = new List<byte>();
                byte b;
                while ((b = reader.ReadByte()) != 0)
                {
                    bytes.Add(b);
                }
                var Signature = Encoding.UTF8.GetString([.. bytes]);
                var Version = reader.ReadByte();
                var Key = ObfuscatedKey.From(reader);
                return new ObfuscatedHeader(Signature, Version, Key);
            }
            catch (EndOfStreamException)
            {
                throw new ObfuscatedHeaderException("Signature was not discovered");
            }

        }
    }
}
