using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WebApi.Models.Orders
{
    public class OrderData : IdObjectData
    {
        public OrderData(Order order) : base(order)
        {
            UserOrderId = order.UserOrderId;
            UserName = order.UserName;
            UserEmail = order.UserEmail;
            TotalPrice = order.TotalPrice ?? 0;
            OrderDate = order.OrderDate;
            Products = order.Products.ToDictionary(p => p.Key, p => p.Value);
        }
        public Guid UserOrderId { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime OrderDate { get; set; }
        public Dictionary<Guid, int> Products { get; set; }
    }
}