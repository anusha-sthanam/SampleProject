using BusinessEntities;
using System;
using System.Collections.Generic;

namespace Core.Services.Orders
{
    public interface IGetOrderService
    {
        Order GetOrderById(Guid orderId);
        IEnumerable<Order> GetAllOrders();
    }
}
