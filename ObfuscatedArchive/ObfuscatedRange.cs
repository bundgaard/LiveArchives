using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObfuscatedArchive
{
    public class ObfuscatedRange
    {

        public long Offset { get; set; }
        public long Size { get; set; }

        public async Task<Memory<byte>> GetBytesAsync(string filePath)
        {
            var buffer = new byte[Size];
            Memory<byte> memory = new(buffer);
            using var stream = new FileStream(filePath, FileMode.Open);
            stream.Seek(Offset, SeekOrigin.Begin);
            await stream.ReadAsync(memory);
            return memory;
        }

        public byte[] GetBytes(string filePath)
        {
            var buffer = new byte[Size];
            using var stream = new FileStream(filePath, FileMode.Open);
            stream.Seek(Offset, SeekOrigin.Begin);
            stream.Read(buffer);
            return buffer;

        }
    }
}
