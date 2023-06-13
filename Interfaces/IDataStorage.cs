namespace URL_Shortener_API.Interfaces
{
    public interface IDataStorage
    {
        string GetLongURL(string key);
        void StoreURLMapping(string key, string longURL);
        bool CheckIfKeyExists(string key);
        string CheckURLExists(string url);
    }
}
