/*
    Example of inline middleware in ASP.NET Core
*/
app.Run(async context =>
{
    await context.Response.WriteAsync("Hello Inline middleware!");
});


/*
    Example or routing middleware in ASP.NET Core
*/
app.Map("/SomeRoute", someRouteApp =>
{
    someRouteApp.Use(async (context, next) =>
    {
        Console.WriteLine("In SomeRoute middleware");
        await context.Response.WriteAsync("Hello from the SomeRoute middleware!");
    });
});
/*
   Example of an inline middleware that logs the request and response
*/
app.Use(async (context, next) =>
{
    // Log the request 
    Console.WriteLine($"Request: {context.Request.Method} {context.Request.Path}");
    await next.Invoke();
    // Log the response 
    Console.WriteLine($"Response: {context.Response.StatusCode}");
});