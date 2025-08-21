using BusinessEntities;
using Common;
using Core.Factories;
using Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Services.Orders
{
    [AutoRegister]
    public class CreateOrderService : ICreateOrderService
    {
        private readonly IUpdateOrderService _updateOrderService;
        private readonly IIdObjectFactory<Order> _orderFactory;
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
       
        public CreateOrderService(IUpdateOrderService updateOrderService, IIdObjectFactory<Order> orderFactory, IOrderRepository orderRepository, IProductRepository productRepository)
        {
            _updateOrderService = updateOrderService;
            _orderFactory = orderFactory;
            _orderRepository = orderRepository;
            _productRepository = productRepository;
        }

        public Order Create(Guid orderId, Guid userOrderId, string userName, string userEmail, DateTime orderDate, Dictionary<Guid, int> products)
        {
           if (products == null || !products.Any()) {
                throw new ArgumentException("Order must contain atleast one product");
            }

            if (userOrderId == Guid.Empty)
            {
                throw new ArgumentException("User order Id must be a valid GUID", nameof(userOrderId));
            }

            var existingOrder = _orderRepository.Get(orderId);
            if (existingOrder != null)
            {
                throw new InvalidOperationException($"An order with Id {orderId} already exists.");
            }

            if (string.IsNullOrWhiteSpace(userName))
            {
                throw new ArgumentException("User name cannot be null or empty", nameof(userName));
            }

            if (string.IsNullOrWhiteSpace(userEmail))
            {
                throw new ArgumentException("User email cannot be null or empty", nameof(userEmail));
            }

            var missingProducts = products.Where(p => _productRepository.Get(p.Key) == null).ToList();
            if (missingProducts.Any())
            {
                throw new ArgumentException($"The following products are not available: {string.Join(", ", missingProducts.Select(p => p.Key))}");
            }

            var order = _orderFactory.Create(orderId);
            var price = products.Sum(p => _productRepository.Get(p.Key).Price * p.Value);
            _updateOrderService.Update(order, userOrderId, userName, userEmail, orderDate, products, price);
            _orderRepository.Save(order);
            return order;

        }

    }
}
