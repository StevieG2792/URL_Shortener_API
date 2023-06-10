using LiteDB;

namespace URLShortener.Controllers
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
                return "A shortened URL already exists: " + $"https://pyrc.com/api/v1/shortenurl/{existingUrlMapping.Key}";
            }
            string key = keyGenerator.GenerateKey();

            
            var urlMapping = new URLMapping { Key = key, LongURL = url };
            urlCollection.Insert(urlMapping);

            return $"https://pyrc.com/api/v1/shortenurl/{key}";
        }

        public string RetrieveURL(string key)
        {
            var urlCollection = liteDb.GetCollection<URLMapping>("url_mappings");
            var urlMapping = urlCollection.FindOne(x => x.Key == key);

            return urlMapping?.LongURL;
        }
    }
}