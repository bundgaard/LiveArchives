
namespace ObfuscatedArchive
{
    [Serializable]
    internal class ObfuscatedArchiveException : Exception
    {
        public ObfuscatedArchiveException()
        {
        }

        public ObfuscatedArchiveException(string? message) : base(message)
        {
        }

        public ObfuscatedArchiveException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}