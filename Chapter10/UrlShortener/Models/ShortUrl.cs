using System.ComponentModel.DataAnnotations;

namespace UrlShortener.Models;

public class ShortUrl
{
    public int Id { get; set; }
    
    [Required]
    [Url]
    public string Url { get; set; }
    
    public string ShortenedUrl { get; set; }
    
    public DateTime CreatedAt { get; set; }
}