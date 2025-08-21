using BusinessEntities;
using System;
using System.Collections.Generic;

namespace Core.Services.Orders
{
    public interface IUpdateOrderService
    {
      /// <summary>
        /// Updates an existing order with the specified details.
        /// </summary>
        /// <param name="orderId">The unique identifier of the order to update.</param>
        /// <param name="name">The new name for the order.</param>
        /// <param name="description">The new description for the order.</param>
        /// <param name="price">The new price for the order.</param>
        /// <param name="quantity">The new quantity for the order.</param>
        void Update(Order order, Guid userOrderId, string userName, string userEmail, DateTime orderDate, Dictionary<Guid, int> products, decimal? price);
    }
}
