using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Services.Products
{
    public interface IUpdateProductService
    {
        void Update(Product product, string name, string description, decimal? price, int? quantity);
    }
}
