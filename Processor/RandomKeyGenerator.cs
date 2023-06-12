using URL_Shortener_API.Interfaces;

namespace URL_Shortener_API.Processor
{
    public class RandomKeyGenerator : IKeyGenerator
    {
        public string GenerateKey()
        {
            const string chars = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            char[] key = new char[6];
            Random random = new Random();

            for (int i = 0; i < key.Length; i++)
            {
                key[i] = chars[random.Next(chars.Length)];
            }

            return new string(key);
        }
    }
}