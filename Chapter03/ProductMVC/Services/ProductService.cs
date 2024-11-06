using System.Collections.Generic;
using System.Linq;
using ProductMVC.Models;

namespace ProductMVC.Services;

public class ProductService
{
    private List<Product> products = new List<Product>()
        {
            new Product() { Id = 1, Name = "Keyboard", Price = 100 },
            new Product() { Id = 2, Name = "Mouse", Price = 50 },
            new Product() { Id = 3, Name = "Monitor", Price = 500 }
        };

    public Product Get(int id)
    {
        return products.FirstOrDefault(p => p.Id == id);
    }

    public List<Product> GetAll()
    {
        return products;
    }

    public void Add(Product product)
    {
        products.Add(product);
    }

    public void Update(int id, Product updatedProduct)
    {
        var product = products.FirstOrDefault(p => p.Id == id);
        if (product != null)
        {
            product.Name = updatedProduct.Name;
            product.Price = updatedProduct.Price;
        }
    }

    public void Delete(int id)
    {
        var product = products.FirstOrDefault(p => p.Id == id);
        if (product != null)
        {
            products.Remove(product);
        }
    }
}
