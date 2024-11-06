using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UrlShortener.Data;

namespace UrlShortener.Extensions;

public static class ApplicationExtensions
{
    public static void MapShortenerRoute(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("{id}", async (HttpContext context, [FromServices] UrlShortenerDbContext dbContext, string id) =>
        {
            var shortCode = context.Request.Path.Value.Trim('/');
            var shortUrl = await dbContext.ShortUrls.FirstOrDefaultAsync(u => u.ShortenedUrl == shortCode);

            if (shortUrl != null)
            {    
                context.Response.StatusCode = 302;
                context.Response.Headers.Location = shortUrl.Url;
            }
            else
            {
                context.Response.StatusCode = 404;
            }
        });
    }
}