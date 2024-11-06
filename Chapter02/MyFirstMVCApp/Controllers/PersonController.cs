using Microsoft.AspNetCore.Mvc;
using MyFirstMVCApp.Models;

namespace MyFirstMVCApp.Controllers;

public class PersonController : Controller 
{
    public IActionResult Index()
    {
        return View();
    }

    public JsonResult GetPeople()
    {
        var model = new List<PersonModel>
        {
            new PersonModel("Person 1", new DateTime(1980, 12, 11) ),
            new PersonModel("Person 2", new DateTime(1983, 12, 15))
        };

        return Json(model);
    }

    public IActionResult Register(PersonModel personModel)
    {
        return RedirectToAction("Result", new { message = $"The {personModel.Name} was registered with success!" });
    }

    public IActionResult Result(string message)
    {
        ViewData["Message"] = message;
        return View();
    }
}