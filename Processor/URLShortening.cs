using LiteDB;
using URL_Shortener_API.Interfaces;
using URL_Shortener_API.Models;

namespace URL_Shortener_API.Processor
{
    public class URLShortening : IURLShortener
    {
        private readonly IKeyGenerator keyGenerator;
        private readonly LiteDatabase liteDb;

        public URLShortening(IKeyGenerator keyGenerator, LiteDatabase liteDb)
        {
            this.keyGenerator = keyGenerator;
            this.liteDb = liteDb;
        }

        public string ShortenURL(string url)
        {
            var urlCollection = liteDb.GetCollection<URLMapping>("url_mappings");
            var existingUrlMapping = urlCollection.FindOne(x => x.LongURL == url);
            if (existingUrlMapping != null)
            {
                return "A shortened URL already exists: " + $"https://pyrc.com/api/v1/{existingUrlMapping.Key}";
            }
            string key = keyGenerator.GenerateKey();


            var urlMapping = new URLMapping { Key = key, LongURL = url };
            urlCollection.Insert(urlMapping);

            return $"https://pyrc.com/api/v1/{key}";
        }


    }
}