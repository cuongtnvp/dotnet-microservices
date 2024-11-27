using Contracts.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Product.API.Entities;
using Product.API.Persistence;
using Product.API.Repositories.Interfaces;

namespace Product.API.Controllers;
[ApiController]
[Route("api/[controller]")]
// public class ProductController(IRepositoryBaseAsync<CatalogProduct, Guid, ProductContext> repository)
//     : ControllerBase
// {
//     private readonly IRepositoryBaseAsync<CatalogProduct,Guid,ProductContext>  _repository = repository;
//
//     // GET
//     [HttpGet]
//     public async Task<IActionResult> GetProducts()
//     {
//         var result = await _repository.FindAll().ToListAsync();
//         return Ok(result);
//     }
//     
// }
public class ProductController(IProductRepository repository)
    : ControllerBase
{
    // GET
    [HttpGet]
    public async Task<IActionResult> GetProducts()
    {
        var result = await repository.GetAllProducts();
        return Ok(result);
    }
    
}