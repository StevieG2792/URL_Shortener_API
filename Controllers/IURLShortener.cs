namespace URLShortener.Controllers
{
    public interface IURLShortener
    {
        string ShortenURL(string url);
        string RetrieveURL(string key);
    }
}