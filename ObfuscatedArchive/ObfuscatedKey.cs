using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObfuscatedArchive
{
    [DebuggerDisplay("Key: {RawValue}, {Value}")]
    public class ObfuscatedKey : ObfuscatedRange
    {

        public uint RawValue { get; }
        private ObfuscatedKey(uint key, long offset, int size)
        {
            RawValue = key;
            Offset = offset;
            Size = size;
        }

        public uint Value => 3 + RawValue * 9;
        public static ObfuscatedKey From(BinaryReader reader)
        {
            try
            {
                var position = reader.BaseStream.Position;
                var key = reader.ReadUInt32();

                return new ObfuscatedKey(key, position, 4);
            }
            catch (Exception)
            {
                throw new ObfuscatedKeyException("Failed to create ObfuscatedKey");
            }
        }
    }
}
