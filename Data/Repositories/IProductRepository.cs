using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        IEnumerable<Product> Get(string name = null, string description = null, decimal? price = null, int? quantity = null );
        void DeleteAll();
    }
}
