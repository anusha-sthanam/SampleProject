using System;
using System.Collections.Generic;

namespace WebApi.Models.Orders
{
    public class OrderModel
    {
        public Guid UserOrderId { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime OrderDate { get; set; }
        public Dictionary<Guid, int> Products { get; set; }
    }
}