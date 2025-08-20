using BusinessEntities;
using Common;
using System;

namespace Core.Services.Products
{
    [AutoRegister(AutoRegisterTypes.Scope)]
    public class UpdateProductService : IUpdateProductService
    {
        public void Update(Product product, string name, string description, decimal? price, int? quantity)
        {
            ValidateInputs(product, name, description, price, quantity);
            product.SetName(name);
            product.SetDescription(description);
            product.SetPrice(price);
            product.SetQuantity(quantity);
        }

        private void ValidateInputs(Product product, string name, string description, decimal? price, int? quantity)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product), "Product cannot be null.");
            }
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name), "Name was not provided.");
            }
            if (!quantity.HasValue)
            {
                throw new ArgumentOutOfRangeException(nameof(quantity), "Quantity has to be provided.");
            }
            if (quantity <=0)
            {
                throw new ArgumentOutOfRangeException(nameof(quantity), "Quantity cannot be zero or negative.");
            }
            if (price.HasValue && price < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(price), "Price cannot be negative.");
            }
        }
    }
}
