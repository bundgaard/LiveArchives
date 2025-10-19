
namespace ObfuscatedArchive
{
    [Serializable]
    internal class ObfuscatedHeaderException : Exception
    {
        public ObfuscatedHeaderException()
        {
        }

        public ObfuscatedHeaderException(string? message) : base(message)
        {
        }

        public ObfuscatedHeaderException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}