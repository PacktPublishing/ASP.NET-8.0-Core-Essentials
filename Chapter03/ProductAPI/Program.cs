using System.Net.Http.Headers;
using ProductApi.Models;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

List<Product> products = new List<Product>();

app.MapGet("/Products", () => Results.Ok(products));

app.MapGet("/Product/{id}", (int id) =>
{
    var product = products.FirstOrDefault(p => p.Id == id);
   
    if (product == null)
    {
        return Results.NotFound();
    }
   
    return Results.Ok(product);
});

app.MapPost("/Product", (Product product) =>
{
    if (product is null)
    {
        return Results.BadRequest();
    }
    
    product.Id = products.Count + 1;
    products.Add(product);
   
    // This is the basic way to return a response with a 200 status code
    //return Results.Ok(product);

    // This is the way to return a response with a 201 status code, more appropriate for request which creates a new resource
    return Results.Created($"/Product/{product.Id}", product);
});

app.MapPut("/Product/{id}", (int id, Product product) =>
{
    if (product is null)
    {
        return Results.NoContent(); // No Content status code represents a successful request that has no response body
    }

    var oldProduct = products.FirstOrDefault(p => p.Id == id);
   
    if (oldProduct == null)
    {
        return Results.NotFound();
    }
   
    oldProduct.Name = product.Name;
    oldProduct.Price = product.Price;
   
    return Results.Ok(oldProduct);
});

app.MapDelete("/Product/{id}", (int id) => 
{
    var product = products.FirstOrDefault(p => p.Id == id);

    if (product is null) return Results.NotFound();

    products.Remove(product);

    return Results.NoContent();
});


app.Run();

