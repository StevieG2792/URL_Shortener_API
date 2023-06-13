using LiteDB;
using URL_Shortener_API.Interfaces;
using URL_Shortener_API.Models;

namespace URL_Shortener_API.Processor
{
    public class URLShortening : IURLShortener
    {
        private readonly IKeyGenerator keyGenerator;
        private readonly IDataStorage dataStorage;

        public URLShortening(IKeyGenerator keyGenerator, IDataStorage dataStorage)
        {
            this.keyGenerator = keyGenerator;
            this.dataStorage = dataStorage;
        }

        public string ShortenURL(string url)
        {
            var existingUrlMapping = dataStorage.CheckURLExists(url);
            if (existingUrlMapping != "x")
            {
                return existingUrlMapping;
            }

            string key = keyGenerator.GenerateKey();
            dataStorage.StoreURLMapping(key, url);

            return $"https://pyrc.com/api/v1/{key}";
        }


    }
}