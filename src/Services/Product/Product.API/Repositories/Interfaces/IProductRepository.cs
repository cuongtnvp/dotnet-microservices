using Contracts.Common.Interfaces;
using Product.API.Entities;
using Product.API.Persistence;

namespace Product.API.Repositories.Interfaces;

public interface IProductRepository:IRepositoryBaseAsync<CatalogProduct,Guid,ProductContext>
{
    Task<IEnumerable<CatalogProduct>?> GetAllProducts();
    Task<CatalogProduct?> GetProductByIdAsync(Guid id);
    Task<CatalogProduct?> GetProductByNoAsync(string productNo);
    Task CreateProduct(CatalogProduct product);
    Task UpdateProduct(CatalogProduct product);
    Task DeleteProduct(Guid id);
}