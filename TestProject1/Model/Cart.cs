using System.Collections.Generic;
using System.Linq;

namespace CommerceProject.Model
{
    public interface IPriceCalculator
    {
        decimal CalculatePrice(OrderItem orderItem);
    }
    public interface IPriceRule
    {
        bool IsMatch(string sku);
        decimal CalculatePrice(OrderItem orderItem);
    }
    public class EachPriceRule : IPriceRule
    {
        public decimal CalculatePrice(OrderItem orderItem)
        {
            return orderItem.Quantity * 5m;
        }

        public bool IsMatch(string sku)
        {
            return sku.StartsWith("EACH");
        }
    }

    public class WeightPriceRule : IPriceRule
    {
        public decimal CalculatePrice(OrderItem orderItem)
        {
            return orderItem.Quantity * 4m / 1000;
        }

        public bool IsMatch(string sku)
        {
            return sku.StartsWith("WEIGHT");
        }
    }

    public class B10G5PriceRule : IPriceRule
    {
        public decimal CalculatePrice(OrderItem orderItem)
        {
            if (orderItem.Quantity >10)
            {
                return (orderItem.Quantity - 5) * 4m;
            }
            return (orderItem.Quantity) * 4m;
        }

        public bool IsMatch(string sku)
        {
            return sku.StartsWith("B10G5");
        }
    }


    public class B4G1PriceRule : IPriceRule
    {
        public decimal CalculatePrice(OrderItem orderItem)
        {
            if (orderItem.Quantity>4)
            {
                return (orderItem.Quantity - 1) * 1m;
            }
            return (orderItem.Quantity) * 1m;

        }

        public bool IsMatch(string sku)
        {
            return sku.StartsWith("B4GO");
        }
    }

    public class SpecialPriceRule : IPriceRule
    {
        public decimal CalculatePrice(OrderItem orderItem)
        {
            decimal price = 0;
            price = orderItem.Quantity * .4m;
            int setsOfThree = orderItem.Quantity / 3;
            price -= setsOfThree * .2m;
            return price;
        }

        public bool IsMatch(string sku)
        {
            return sku.StartsWith("SPECIAL");
        }
    }

    public class PriceCalculator : IPriceCalculator
    {
        private List<IPriceRule> PriceRules;
        public PriceCalculator()
        {
            PriceRules = new List<IPriceRule>
            {
                new EachPriceRule(),
                new WeightPriceRule(),
                new SpecialPriceRule(),
                new B10G5PriceRule(),
                new B4G1PriceRule()
            };
        }
        public decimal CalculatePrice(OrderItem orderItem)
        {
            return PriceRules.First(p=>p.IsMatch(orderItem.Sku)).CalculatePrice(orderItem);
        }
    }

    public class Cart
    {
        private readonly List<OrderItem> _items;
        private IPriceCalculator PriceCalculator;

        public Cart(IPriceCalculator priceCalculator)
        {
            _items = new List<OrderItem>();
            PriceCalculator = priceCalculator;
        }

        public IEnumerable<OrderItem> Items
        {
            get { return _items; }
        }

        public string CustomerEmail { get; set; }

        public void Add(OrderItem orderItem)
        {
            _items.Add(orderItem);
        }

        public decimal TotalAmount()
        {
            decimal total = 0m;
            foreach (OrderItem orderItem in Items)
            {
               total+= PriceCalculator.CalculatePrice(orderItem);              
            }
            return total;
        }
    }
}