using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Services.Products
{
    public interface IGetProductService
    {
        Product GetProductById(Guid productId);
        IEnumerable<Product> GetProducts(string name = null, string description = null, decimal? price = null, int? quantity = null);
    }
}
