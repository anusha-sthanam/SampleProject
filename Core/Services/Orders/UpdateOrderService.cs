using BusinessEntities;
using Common;
using Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Services.Orders
{
    [AutoRegister(AutoRegisterTypes.Scope)]
    public class UpdateOrderService : IUpdateOrderService
    {
        private IProductRepository _productRepository;

        public UpdateOrderService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public void Update(Order order, Guid userOrderId, string userName, string userEmail, DateTime orderDate, Dictionary<Guid, int> products, decimal? price)
        {
            ValidateInputs(order, userOrderId, userName, userEmail, orderDate, products, price);

            order.SetUserOrderId(userOrderId);
            order.SetUserName(userName);
            order.SetUserEmail(userEmail);
            order.SetOrderDate(orderDate);
            order.SetProducts(products);
            var newPrice = products.Sum(p => _productRepository.Get(p.Key).Price * p.Value);
            order.SetTotalPrice(newPrice ?? 0);
        }

        private void ValidateInputs(Order order, Guid userOrderId, string userName, string userEmail, DateTime orderDate, Dictionary<Guid, int> products, decimal? price)
        {
            if (order == null)
            {
                throw new ArgumentException(nameof(order), "Order cannot be null");
            }
            if (userOrderId == Guid.Empty)
            {
                throw new ArgumentException("User order Id must be a valid GUID", nameof(userOrderId));
            }
            if (string.IsNullOrWhiteSpace(userName))
            {
                throw new ArgumentException("User name cannot be null or empty", nameof(userName));
            }
            if (string.IsNullOrWhiteSpace(userEmail))
            {
                throw new ArgumentException("User email cannot be null or empty", nameof(userEmail));
            }
            if (products == null || products.Count == 0)
            {
                throw new ArgumentException("Products cannot be null or empty", nameof(products));
            }
        }

    }
}
