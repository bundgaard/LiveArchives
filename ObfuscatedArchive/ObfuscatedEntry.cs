using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObfuscatedArchive
{
    [DebuggerDisplay("Entry: Offset = {Offset}, Size {Size}, Name = {Name.Value}")]
    public class ObfuscatedEntry : ObfuscatedRange
    {

        public uint Key { get; set; }
        public ObfuscatedName Name { get; set; }

        private ObfuscatedEntry(long offset, int size, uint key, ObfuscatedName name)
        {
            Offset = offset;
            Size = size;
            Name = name;
            Key = key;
        }



        public static ObfuscatedEntry? From(BinaryReader reader, ObfuscatedKey key)
        {
            try
            {
                var maxOffset = reader.BaseStream.Length;
                var offset = reader.ReadUInt32() ^ key.Value;
                var size = (int)(reader.ReadUInt32() ^ key.Value);

                var ok = offset < maxOffset && size <= maxOffset && offset <= maxOffset - size;

                if (offset == 0 || !ok)
                {
                    return null; // TODO: might be a hack but we are sure that we cannot have an offset at 0.
                }
                var entryKey = reader.ReadUInt32() ^ key.Value;
                var name = ObfuscatedName.From(reader, key);
                return new ObfuscatedEntry(offset, size, entryKey, name);
            }
            catch (EndOfStreamException)
            {
                throw new ObfuscatedEntryException("End of stream");
            }
            catch (Exception e)
            {
                throw new ObfuscatedEntryException($"Any other error {e.Message}");
            }
        }
    }
}
