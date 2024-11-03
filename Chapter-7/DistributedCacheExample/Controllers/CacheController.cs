using Microsoft.AspNetCore.Mvc;

using Microsoft.Extensions.Caching.Distributed;

using System.Text.Json;

using System.Text;



namespace DistributedCacheExample.Controllers;

[ApiController]

[Route("api/[controller]")]

public class CacheController : ControllerBase

{

    private readonly IDistributedCache _cache;


    public CacheController(IDistributedCache cache)
    {

        _cache = cache;
    }



    [HttpGet("{key}")]

    public async Task<IActionResult> Get(string key)
    {

        var cachedData = await _cache.GetStringAsync(key);

        if (string.IsNullOrEmpty(cachedData))
        {

            return NotFound();

        }



        var data = JsonSerializer.Deserialize<MyData>(cachedData);

        return Ok(data);

    }



    [HttpPost]

    public async Task<IActionResult> Post([FromBody] MyData data)

    {

        var cacheKey = data.Key;

        var serializedData = JsonSerializer.Serialize(data);

        var options = new DistributedCacheEntryOptions()

            .SetSlidingExpiration(TimeSpan.FromMinutes(5))

            .SetAbsoluteExpiration(TimeSpan.FromHours(1));



        await _cache.SetStringAsync(cacheKey, serializedData, options);



        return CreatedAtAction(nameof(Get), new { key = cacheKey }, data);

    }

}



public class MyData
{

    public string Key { get; set; }

    public string Value { get; set; }

}