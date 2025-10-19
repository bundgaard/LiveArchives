
namespace ObfuscatedArchive
{
    [Serializable]
    internal class ObfuscatedKeyException : Exception
    {
        public ObfuscatedKeyException()
        {
        }

        public ObfuscatedKeyException(string? message) : base(message)
        {
        }

        public ObfuscatedKeyException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}