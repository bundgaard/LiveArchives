
namespace ObfuscatedArchive
{
    [Serializable]
    internal class ObfuscatedEntryException : Exception
    {
        public ObfuscatedEntryException()
        {
        }

        public ObfuscatedEntryException(string? message) : base(message)
        {
        }

        public ObfuscatedEntryException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}