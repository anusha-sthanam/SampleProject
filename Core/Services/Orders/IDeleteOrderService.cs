using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Services.Orders
{
    public interface IDeleteOrderService
    {
        /// <summary>
        /// Deletes an order by its ID.
        /// </summary>
        /// <param name="orderId">The ID of the order to delete.</param>
        void DeleteOrder(Order order);

        /// <summary>
        /// Deletes all orders.
        /// </summary>
        void DeleteAllOrders();
    }
}
