using BusinessEntities;
using Common;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    [AutoRegister(AutoRegisterTypes.Singleton)]
    public class InMemoryProductRepository : IProductRepository
    {
        private readonly Dictionary<Guid, Product> _products = new Dictionary<Guid, Product>();

        /// <summary>
        /// Get an entity by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Product Get(Guid id)
        {
            _products.TryGetValue(id, out var product);
            return product;
        }

        public IEnumerable<Product> Get(string name = null, string description = null, decimal? price = null, int? quantity = null)
        {
            var query = _products.Values.AsQueryable();

            if (!string.IsNullOrWhiteSpace(name))
                query = query.Where(p => p.Name.Contains(name));

            if (!string.IsNullOrWhiteSpace(description))
                query = query.Where(p => string.Equals(p.Description, description, StringComparison.OrdinalIgnoreCase));

            if (price.HasValue)
                query = query.Where(p => p.Price >= price.Value);

            if (quantity.HasValue)
                query = query.Where(p => p.Quantity <= quantity.Value);

            return query.ToList();
        }

        public void Save(Product product)
        {
            _products[product.Id] = product;
        }

        public void Delete(Product product)
        {
            _products.Remove(product.Id);
        }

        public void DeleteAll()
        {
            _products.Clear();
        }
    }
}
