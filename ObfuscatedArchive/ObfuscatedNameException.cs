
namespace ObfuscatedArchive
{
    [Serializable]
    internal class ObfuscatedNameException : Exception
    {
        public ObfuscatedNameException()
        {
        }

        public ObfuscatedNameException(string? message) : base(message)
        {
        }

        public ObfuscatedNameException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}