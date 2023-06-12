using Microsoft.AspNetCore.Mvc;
using System;
using URL_Shortener_API.Interfaces;

namespace URLShortener.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UrlShortenerController : ControllerBase
    {
        private readonly IURLShortener urlShortener;
        private readonly IURLRetrieval urlRetrieval;

        public UrlShortenerController(IURLShortener urlShortener, IURLRetrieval urlRetrieval)
        {
            this.urlShortener = urlShortener;
            this.urlRetrieval = urlRetrieval;
        }

        [HttpPost]
        public IActionResult ShortenURL([FromBody] string url)
        {
            if (string.IsNullOrEmpty(url))
                return BadRequest("URL cannot be empty.");

            string shortenedUrl = urlShortener.ShortenURL(url);
            return Ok(shortenedUrl);
        }

        [HttpGet]
        public IActionResult RetrieveURL(string shortURL)
        {
            string longURL = urlRetrieval.RetrieveURL(shortURL);
            if (longURL != null)
                return Ok(longURL);
            else
                return NotFound();
        }
    }
}