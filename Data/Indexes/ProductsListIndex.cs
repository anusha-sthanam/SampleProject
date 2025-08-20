using BusinessEntities;
using Raven.Abstractions.Indexing;
using Raven.Client.Indexes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Indexes
{
    public class ProductsListIndex : AbstractIndexCreationTask<Product>
    {
        public ProductsListIndex()
        {
            Map = products => from product in products
                              select new
                              {
                                  product.Name,
                                  product.Description,
                                  product.Price,
                                  product.Quantity
                              };

            Index(x => x.Name, FieldIndexing.Analyzed);
            Index(x => x.Description, FieldIndexing.NotAnalyzed);
            Index(x => x.Price, FieldIndexing.Default);
            Index(x => x.Quantity, FieldIndexing.Default);
        }
    }
}
