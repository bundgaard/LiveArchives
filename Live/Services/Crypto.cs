using System.Security.Cryptography;
namespace Live.Services
{
    internal class Crypto
    {
        public void Foo(string password, byte[] data)
        {
            using var derivedKey = new Rfc2898DeriveBytes(password, 32, 1_000000, HashAlgorithmName.SHA1);
            var entropy = derivedKey?.GetBytes(32); // 256 bits
            var encrypted = ProtectedData.Protect(data, entropy, DataProtectionScope.CurrentUser);
        }
    }
}
