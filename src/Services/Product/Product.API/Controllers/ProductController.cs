using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Contracts.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Product.API.Entities;
using Product.API.Persistence;
using Product.API.Repositories.Interfaces;
using Shared.Dtos;

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
public class ProductController(IProductRepository repository,IMapper mapper)
    : ControllerBase
{
    #region CRUD

    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> GetProduct(Guid id)
    {
        var product = await repository.GetProductByIdAsync(id);
        if (product == null)
        {
            return NotFound();
        }
        var result = mapper.Map<ProductDto>(product);
        return Ok(result);
    }
    #endregion
    // GET
    [HttpGet]
    public async Task<IActionResult> GetProducts()
    {
        var products = await repository.GetAllProducts();
        var result = mapper.Map<IEnumerable<ProductDto>>(products);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct(CreateProductDto productDto)
    {
        var product = mapper.Map<CatalogProduct>(productDto);
        await repository.CreateProduct(product);
        await repository.SaveChangesAsync();
        var result = mapper.Map<ProductDto>(product);
        return Ok(result);
    }

    [HttpPut("{id:Guid}")]
    public async Task<IActionResult> UpdateProduct([Required] Guid id,[FromBody] UpdateProductDto updateProductDto)
    {
        var product = await repository.GetProductByIdAsync(id);
        if (product == null)
        {
            return NotFound();
        }
        var updatedProduct = mapper.Map(updateProductDto, product);
        await repository.UpdateProduct(updatedProduct);
        await repository.SaveChangesAsync();
        var result = mapper.Map<ProductDto>(updatedProduct);
        return Ok(result);
    }

    [HttpDelete("{id:Guid}")]
    public async Task<IActionResult> DeleteProduct([Required] Guid id)
    {
        var product = await repository.GetProductByIdAsync(id);
        if(product == null) return NotFound();
        await repository.DeleteProduct(id);
        await repository.SaveChangesAsync();
        return NoContent();
    }
 #region Additional Resources

 [HttpGet("get-product-by-no/{productNo}")]
 public async Task<IActionResult> GetProductByNo([Required]string productNo)
 {
     var product = await repository.GetProductByNoAsync(productNo);
     if (product == null) return NotFound();
     var result = mapper.Map<ProductDto>(product);
     return Ok(result);
 }
 #endregion
}