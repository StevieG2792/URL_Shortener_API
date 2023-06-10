using Microsoft.AspNetCore.Mvc;
using System;

namespace URLShortener.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UrlShortenerController : ControllerBase
    {
        private readonly IURLShortener urlShortener;

        public UrlShortenerController(IURLShortener urlShortener)
        {
            this.urlShortener = urlShortener;
        }

        [HttpPost]
        public IActionResult ShortenURL([FromBody] string url)
        {
            if (string.IsNullOrEmpty(url))
                return BadRequest("URL cannot be empty.");

            string shortenedUrl = urlShortener.ShortenURL(url);
            return Ok(shortenedUrl);
        }

        [HttpGet("{key}")]
        public IActionResult RetrieveURL(string key)
        {
            string longURL = urlShortener.RetrieveURL(key);
            if (longURL != null)
                return Ok(longURL);
            else
                return NotFound();
        }
    }
}