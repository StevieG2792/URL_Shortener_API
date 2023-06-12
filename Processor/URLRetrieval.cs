using URLShortener.Controllers;
using LiteDB;
using URL_Shortener_API.Interfaces;
using URL_Shortener_API.Models;

namespace URL_Shortener_API.Processor
{
    public class URLRetrieval : IURLRetrieval
    {
        private readonly LiteDatabase liteDb;

        public URLRetrieval(LiteDatabase liteDb)
        {
            this.liteDb = liteDb;
        }
        public string RetrieveURL(string shortURL)
        {
            string[] splitURL = shortURL.Split('/');
            string key = splitURL[splitURL.Length - 1];

            var urlCollection = liteDb.GetCollection<URLMapping>("url_mappings");
            var urlMapping = urlCollection.FindOne(x => x.Key == key);

            return urlMapping?.LongURL;
        }
    }
}
