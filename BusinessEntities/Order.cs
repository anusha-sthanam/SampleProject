using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessEntities
{
    public class Order : IdObject
    {
        private DateTime _orderDate;
        private Dictionary<Guid, int> _products;
        private decimal? _totalPrice;
        private Guid _userOrderId;
        private string _userName;
        private string _userEmail;

        public DateTime OrderDate => _orderDate;
        public decimal? TotalPrice => _totalPrice;
        public Guid UserOrderId => _userOrderId;
        public Dictionary<Guid, int> Products => _products;
        public string UserName => _userName;
        public string UserEmail => _userEmail;


        public void SetOrderDate(DateTime orderDate)
        {
            if (orderDate == default(DateTime))
                throw new ArgumentException("Order date must be set", nameof(orderDate));
            _orderDate = orderDate;
        }

        public void SetProducts(Dictionary<Guid, int> products)
        {
            if (products == null || products.Count == 0)
                throw new ArgumentException("Products cannot be null or empty", nameof(products));

            if (products.Any(p => p.Key == Guid.Empty))
                throw new ArgumentException("All product Ids must be valid GUIDs.", nameof(products));

            if (products.Any(p => p.Value <= 0))
                throw new ArgumentException("All product quantities must be greater than zero.", nameof(products));
            _products = products;
        }

        public void SetTotalPrice(decimal totalPrice)
        {
            if (totalPrice < 0)
                throw new ArgumentException(nameof(totalPrice), "Total price cannot be negative");
            _totalPrice = totalPrice;
        }

        public void SetUserOrderId(Guid userOrderId)
        {
            if (userOrderId == Guid.Empty)
                throw new ArgumentException("User order Id must be a valid GUID", nameof(userOrderId));
            _userOrderId = userOrderId;
        }

        public void SetUserName(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
                throw new ArgumentException("User name cannot be null or empty", nameof(userName));
            _userName = userName;
        }

        public void SetUserEmail(string userEmail)
        {
            if (string.IsNullOrWhiteSpace(userEmail))
                throw new ArgumentException("User email cannot be null or empty", nameof(userEmail));
            _userEmail = userEmail;
        }
    }
}
