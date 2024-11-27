using Contracts.Common.Interfaces;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Product.API.Entities;
using Product.API.Persistence;
using Product.API.Repositories.Interfaces;

namespace Product.API.Repositories;

public class ProductRepository(ProductContext context, IUnitOfWork<ProductContext> unitOfWork) :RepositoryBaseAsync<CatalogProduct,Guid,ProductContext>(context, unitOfWork),IProductRepository
{
    
    public async Task<IEnumerable<CatalogProduct>?> GetAllProducts()
    {
        return await FindAll().ToListAsync();
    }

    public  Task<CatalogProduct?> GetProductByIdAsync(Guid id)
    {
        return  GetByIdAsync(id);
    }

    public async Task<CatalogProduct?> GetProductByNoAsync(string productNo)
    {
        return await FindByCondition(x=>x.No.Equals(productNo)).FirstOrDefaultAsync();
    }

    public Task CreateProduct(CatalogProduct product)
    {
        return CreateAsync(product);
    }

    public Task UpdateProduct(CatalogProduct product)
    {
        return UpdateAsync(product);
    }

    public async Task DeleteProduct(Guid id)
    {
       var product = await GetByIdAsync(id);
       if (product != null) await DeleteAsync(product);
    }
}