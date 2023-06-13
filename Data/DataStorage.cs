using LiteDB;
using Microsoft.AspNetCore.Server.HttpSys;
using System;
using URL_Shortener_API.Interfaces;
using URL_Shortener_API.Models;

namespace URL_Shortener_API.Data
{
    public class DataStorage : IDataStorage
    {
        private readonly LiteDatabase liteDb;
        

        public DataStorage(LiteDatabase liteDb)
        {
            this.liteDb = liteDb;
        }

        
        public string GetLongURL(string key)
        {
            var urlCollection = liteDb.GetCollection<URLMapping>("url_mappings");
            var urlMapping = urlCollection.FindOne(x => x.Key == key);

            return urlMapping?.LongURL;
        }

        public string CheckURLExists(string url)
        {
            var urlCollection = liteDb.GetCollection<URLMapping>("url_mappings");
            var existingUrlMapping = urlCollection.FindOne(x => x.LongURL == url);
            if (existingUrlMapping != null)
            {
                return "A shortened URL already exists: " + $"https://pyrc.com/api/v1/{existingUrlMapping.Key}";
            }

            return "x";
        }

        public void StoreURLMapping(string key, string longURL)
        {
            var urlCollection = liteDb.GetCollection<URLMapping>("url_mappings");
            var urlMapping = new URLMapping { Key = key, LongURL = longURL };
            urlCollection.Insert(urlMapping);
        }

        public bool CheckIfKeyExists(string key)
        {
            var urlCollection = liteDb.GetCollection<URLMapping>("url_mappings");
            var urlMapping = urlCollection.FindOne(x => x.Key == key);

            return urlMapping != null;
        }
    }
}
