using BusinessEntities;
using Data.Indexes;
using Raven.Client;
using System.Collections.Generic;
using System.Linq;

namespace Data.Repositories
{
    public class ProductRepository : Repository<Product>,IProductRepository
    {
        private readonly IDocumentSession _documentSession;

        public ProductRepository(IDocumentSession documentSession) : base(documentSession)
        {
            _documentSession = documentSession;
        }

        public IEnumerable<Product> Get(string name = null, string description = null, decimal? price = null, int? quantity = null)
        {
            var query = _documentSession.Advanced.DocumentQuery<Product, ProductsListIndex>();
            var hasFirstParameter = false;

            if (!string.IsNullOrWhiteSpace(name))
            {
                query = query.WhereEquals("Name", name);
                hasFirstParameter = true;
            }
            if (description != null)
            {
                if (hasFirstParameter)
                {
                    query = query.AndAlso();
                }
                else
                {
                    hasFirstParameter = true;
                }
                query = query.Where($"Description:*{description}*");
            }
            if (price != null)
            {
                if (hasFirstParameter)
                {
                    query = query.AndAlso();
                }
                query = query.WhereEquals("Price", price);
            }
            if (quantity != null)
            {
                if (hasFirstParameter)
                {
                    query = query.AndAlso();
                }
                query = query.WhereEquals("Quantity", quantity);
            }

            return query.ToList();
        }

        public void DeleteAll()
        {
            base.DeleteAll<ProductsListIndex>();
        }
    }
}
