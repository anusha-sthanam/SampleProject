using BusinessEntities;
using Common;
using Data.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Services.Orders
{
    [AutoRegister]
    public class DeleteOrderService : IDeleteOrderService
    {
        private readonly IOrderRepository _orderRepository;
        public DeleteOrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        /// <summary>
        /// Deletes an order by its ID.
        /// </summary>
        /// <param name="orderId">The ID of the order to delete.</param>
        public void DeleteOrder(Order order)
        {
            _orderRepository.Delete(order);
        }
        /// <summary>
        /// Deletes all orders.
        /// </summary>
        public void DeleteAllOrders()
        {
            _orderRepository.DeleteAll();
        }
    }
}
