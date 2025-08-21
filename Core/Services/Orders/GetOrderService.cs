using BusinessEntities;
using Common;
using Data.Repositories;
using System;
using System.Collections.Generic;

namespace Core.Services.Orders
{
    [AutoRegister]
    public class GetOrderService : IGetOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public GetOrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public Order GetOrderById(Guid orderId)
        {
            // Retrieve the order from the repository
            return _orderRepository.Get(orderId);
        }

        public IEnumerable<Order> GetAllOrders()
        {
            return _orderRepository.GetResults();
        }
    }
}
