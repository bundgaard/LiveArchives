using System.Diagnostics;
using System.Text;

namespace ObfuscatedArchive
{
    [DebuggerDisplay("Name: {Value}, Length = {Value.Length}")]
    public class ObfuscatedName : ObfuscatedRange
    {
        public string Value { get; set; }
        public List<byte> RawValue { get; set; }


        private ObfuscatedName(string value, List<byte> rawValue, long offset, int size)
        {
            Value = value;
            RawValue = rawValue;
            Offset = offset;
            Size = size;
        }
        public static ObfuscatedName From(BinaryReader reader, ObfuscatedKey key)
        {
            try
            {
                var position = reader.BaseStream.Position;
                int size = 0;

                var nameLength = reader.ReadUInt32() ^ key.Value;
                size += System.Runtime.InteropServices.Marshal.SizeOf(nameLength.GetType());

                var bytes = new byte[nameLength];
                var bytesRead = reader.Read(bytes);
                if (bytesRead == 0 || bytesRead != bytes.Length)
                {
                    throw new ObfuscatedEntryException($"Failed to read name at file offset: {reader.BaseStream.Position}");
                }
                StringBuilder sb = new();

                for (int i = 0; i < bytes.Length; ++i)
                {
                    var shiftAmount = (byte)(i << 3);
                    sb.Append($"{shiftAmount.ToString()},({key.Value >> shiftAmount}) ");
                    bytes[i] ^= (byte)(key.Value >> shiftAmount);


                }
                string shiftAMount = sb.ToString();
                var name = Encoding.UTF8.GetString(bytes);
                return new ObfuscatedName(name, [.. bytes], position, size);
            }
            catch (EndOfStreamException)
            {
                throw new ObfuscatedNameException("");
            }
            catch (Exception)
            {
                throw new ObfuscatedNameException("");
            }

        }
    }
}
