using Microsoft.AspNetCore.Mvc;

namespace Product.API.Controllers;

public class ProductController : ControllerBase
{
    // GET
    [HttpGet("api/products")]
    public async Task<IActionResult> GetProducts()
    {
        return new OkObjectResult("cuong");
    }
}