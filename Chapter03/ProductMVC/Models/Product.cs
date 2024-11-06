using System.ComponentModel.DataAnnotations;

namespace ProductMVC.Models;

public class Product
{
    public int Id { get; set; }

    [Required(ErrorMessage = "The field Name is required")]
    [MinLength(3, ErrorMessage = "The field Name must have at least 3 characters")]
    public string Name { get; set; }
    public decimal Price { get; set; }
}
