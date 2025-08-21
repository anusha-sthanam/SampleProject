using BusinessEntities;
using Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Data.Repositories
{
    [AutoRegister(AutoRegisterTypes.Singleton)]
    public class InMemoryOrderRepository : IOrderRepository
    {
        private readonly Dictionary<Guid, Order> _orders = new Dictionary<Guid, Order>();

        public Order Get(Guid id)
        {
            _orders.TryGetValue(id, out var order);
            return order;
        }

        public IEnumerable<Order> GetResults(Guid? userOrderId = null, string userName = null, string userEmail = null, DateTime? orderDate = null, IEnumerable<Guid> products = null)
        {
            var query = _orders.Values.AsQueryable();

            if (userOrderId.HasValue)
            {
                query = query.Where(o => o.UserOrderId == userOrderId.Value);
            }

            if (orderDate.HasValue)
            {
                query = query.Where(o => o.OrderDate.Date == orderDate.Value.Date);
            }

            if (!string.IsNullOrWhiteSpace(userName))
            {
                query = query.Where(o => o.UserName != null && o.UserName.IndexOf(userName, StringComparison.OrdinalIgnoreCase) >= 0);
            }

            if (!string.IsNullOrWhiteSpace(userEmail))
            {
                query = query.Where(o => o.UserEmail != null && o.UserEmail.IndexOf(userEmail, StringComparison.OrdinalIgnoreCase) >= 0);
            }
            return query.ToList();
        }

        public void Save(Order order)
        {
            _orders[order.Id] = order;
        }

        public void Delete(Order order)
        {
            _orders.Remove(order.Id);
        }

        public void DeleteAll()
        {
            _orders.Clear();
        }
    }
}
