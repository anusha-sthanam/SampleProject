using System;

namespace BusinessEntities
{
    public class Product : IdObject
    {
        private string _name;
        private string _description;
        private decimal? _price;
        private int? _quantity;

        public string Name { get => _name; private set => _name = value; }
        public string Description { get => _description; private set => _description = value; }
        public decimal? Price { get => _price; private set => _price = value; }
        public int? Quantity { get => _quantity; private set => _quantity = value; }

        public void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name), "Product name is required.");

            _name = name;
        }

        public void SetDescription(string description)
        {
            _description = description ?? string.Empty;
        }

        public void SetPrice(decimal? price)
        {
            if (!price.HasValue || price < 0)
                throw new ArgumentOutOfRangeException(nameof(price), "Price must be a non-negative number.");

            _price = price.Value;
        }

        public void SetQuantity(int? quantity)
        {
            if (quantity <=0)
                throw new ArgumentOutOfRangeException(nameof(quantity), "Quantity cannot be zero or negative.");
            _quantity = quantity;
        }
    }
}
