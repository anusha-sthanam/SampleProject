using BusinessEntities;
using System;
using System.Collections.Generic;

namespace Data.Repositories
{
    public interface IOrderRepository : IRepository<Order>
    {
        IEnumerable<Order> GetResults(Guid? userOrderId = null, string userName = null, string userEmail = null, DateTime? orderDate = null, IEnumerable<Guid> products = null);
        void DeleteAll();
    }
}
