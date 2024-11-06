using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using ProductMVC.Models;
using ProductMVC.Services;

namespace ProductMVC.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    private readonly ProductService _productService;

    public ProductController(ProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(_productService.GetAll());
    }

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        var product = _productService.Get(id);

        if (product == null) return NotFound();

        return Ok(product);
    }

    [HttpPost]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType<Product>(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult Post(Product product)
    {
        if (!IsModelValid(product)) return BadRequest(ModelState);    

        _productService.Add(product);
        return CreatedAtAction(nameof(Get), new { id = product.Id }, product);
    }

    [HttpPut]
    public IActionResult Put(int id, Product updatedProduct)
    {
        if (!IsModelValid(updatedProduct)) return BadRequest(ModelState);

        _productService.Update(id, updatedProduct);
        return NoContent();
    }
    [HttpDelete]
    public IActionResult Delete(int id)
    {
        _productService.Delete(id);
        return NoContent();
    }

    private bool IsModelValid(Product product)
    {
        if (product is null) return false;

        if (product.Price <= 0) ModelState.AddModelError("Price", "The field Price must be greater than zero");

        return ModelState.IsValid;
    }

}