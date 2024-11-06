using System.Threading.RateLimiting;
using Microsoft.AspNetCore.Builder; 
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;

namespace CommonMiddlewares.Middlewares;

public static class CommonMiddlewareExtensions 
{ 
    public static IServiceCollection AddCustomRateLimiting(this IServiceCollection services) 
    { 
        services.AddRateLimiter(options =>
        {
            // Fixed Window Rate Limiting Policy
            options.AddPolicy("fixed", context =>
                RateLimitPartition.GetFixedWindowLimiter(
                    partitionKey: context.Request.Headers["X-Forwarded-For"].ToString(),
                    factory: partition => new FixedWindowRateLimiterOptions
                    {
                        PermitLimit = 5,
                        Window = TimeSpan.FromMinutes(1),
                        QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                        QueueLimit = 2
                    }));

            // Sliding Window Rate Limiting Policy
            options.AddPolicy("sliding", context =>
                RateLimitPartition.GetSlidingWindowLimiter(
                    partitionKey: context.Request.Headers["X-Forwarded-For"].ToString(),
                    factory: partition => new SlidingWindowRateLimiterOptions
                    {
                        PermitLimit = 5,
                        Window = TimeSpan.FromMinutes(1),
                        SegmentsPerWindow = 3,
                        QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                        QueueLimit = 2
                    }));

            // Token Bucket Rate Limiting Policy
            options.AddPolicy("tokenBucket", context =>
                RateLimitPartition.GetTokenBucketLimiter(
                    partitionKey: context.Request.Headers["X-Forwarded-For"].ToString(),
                    factory: partition => new TokenBucketRateLimiterOptions
                    {
                        TokenLimit = 10,
                        TokensPerPeriod = 5,
                        ReplenishmentPeriod = TimeSpan.FromSeconds(10),
                        QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                        QueueLimit = 2
                    }));
        }); 
  
        return services; 
    } 
  
    public static void UseCommonApplicationMiddleware(this IApplicationBuilder app) 
    { 
        app.UseMiddleware<ErrorHandlingMiddleware>(); 
        app.UseMiddleware<PerformanceLoggingMiddleware>(); 
        app.UseRateLimiter(); 
    } 
} 