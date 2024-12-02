using Microsoft.AspNetCore.Mvc;

namespace Product.API.Controllers;

public class HomeController:ControllerBase
{
    public IActionResult Index()=> Redirect("~/swagger");
}