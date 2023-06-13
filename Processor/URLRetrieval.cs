using URLShortener.Controllers;
using LiteDB;
using URL_Shortener_API.Interfaces;
using URL_Shortener_API.Models;

namespace URL_Shortener_API.Processor
{
    public class URLRetrieval : IURLRetrieval
    {
        private readonly IDataStorage dataStorage;

        public URLRetrieval(IDataStorage dataStorage)
        {
            this.dataStorage = dataStorage;
        }
        public string RetrieveURL(string shortURL)
        {
            string[] splitURL = shortURL.Split('/');
            string key = splitURL[splitURL.Length - 1];

            return dataStorage.GetLongURL(key);

        }
    }
}
