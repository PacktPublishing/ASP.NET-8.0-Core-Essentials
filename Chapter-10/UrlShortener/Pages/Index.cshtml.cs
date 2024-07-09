using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using UrlShortener.Data;
using UrlShortener.Models;

namespace UrlShortener.Pages;


public class IndexModel : PageModel
{
    private readonly UrlShortenerDbContext _context;

    public IndexModel(UrlShortenerDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public string OriginalUrl { get; set; }

    public IList<ShortUrl> ShortUrls { get; set; }

    public async Task OnGetAsync()
    {
        ShortUrls = await _context.ShortUrls.ToListAsync();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        if (!OriginalUrl.StartsWith("http://") && !OriginalUrl.StartsWith("https://"))
        {
            OriginalUrl = "http://" + OriginalUrl;
        }

        var shortUrl = new ShortUrl
        {
            Url = OriginalUrl,
            ShortenedUrl = GenerateShortUrl(),
            CreatedAt = DateTime.UtcNow
        };

        _context.ShortUrls.Add(shortUrl);
        await _context.SaveChangesAsync();

        return RedirectToPage();
    }

    private string GenerateShortUrl()
    {
        var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        var stringChars = new char[6];
        var random = new Random();

        for (int i = 0; i < stringChars.Length; i++)
        {
            stringChars[i] = chars[random.Next(chars.Length)];
        }

        return new string(stringChars);
    }
}

