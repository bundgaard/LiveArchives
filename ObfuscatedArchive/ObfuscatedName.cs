using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                for (int i = 0; i < bytes.Length; ++i)
                {
                    bytes[i] ^= (byte)(key.Value >> (i << 3));
                }
                var name = Encoding.UTF8.GetString(bytes);
                return new ObfuscatedName(name, [.. bytes], position, size);
            }
            catch (EndOfStreamException e)
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
