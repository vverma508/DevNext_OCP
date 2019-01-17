using CommerceProject.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CommerceProject
{
    [TestClass]
    public class CartTotalShouldReturn
    {
        private Model.Cart _cart;
        private Model.PriceCalculator _priceCalculator;
        [TestInitialize]
        public void Setup()
        {
            _priceCalculator = new PriceCalculator();
            _cart = new Model.Cart(_priceCalculator);
        }

        [TestMethod]
        public void ZeroWhenEmpty()
        {
            Assert.AreEqual(0, _cart.TotalAmount());
        }

        [TestMethod]
        public void FiveWithOneEachItem()
        {
            _cart.Add(new OrderItem() { Quantity = 1, Sku = "EACH_WIDGET" });
            Assert.AreEqual(5.0m, _cart.TotalAmount());
        }

        [TestMethod]
        public void TwoWithHalfKiloWeightItem()
        {
            _cart.Add(new OrderItem() { Quantity = 500, Sku = "WEIGHT_PEANUTS" });
            Assert.AreEqual(2m, _cart.TotalAmount());
        }

        [TestMethod]
        public void EightyCentsWithTwoSpecialItem()
        {
            _cart.Add(new OrderItem() { Quantity = 2, Sku = "SPECIAL_CANDYBAR" });
            Assert.AreEqual(0.80m, _cart.TotalAmount());
        }
        [TestMethod]
        public void TwoDollarsWithSixSpecialItem()
        {
            _cart.Add(new OrderItem() { Quantity = 6, Sku = "SPECIAL_CANDYBAR" });
            Assert.AreEqual(2m, _cart.TotalAmount());
        }
        [TestMethod]
        public void FourDollarsWithFourBuy4Get1FreeItems()
        {
            _cart.Add(new OrderItem() { Quantity = 4, Sku = "B4GO_APPLE" });
            Assert.AreEqual(4m, _cart.TotalAmount());
        }
        [TestMethod]
        public void FourDollarsWithFiveBuy4Get1FreeItems()
        {
            _cart.Add(new OrderItem() { Quantity = 5, Sku = "B4GO_APPLE" });
            Assert.AreEqual(4m, _cart.TotalAmount());
        }

        //B10G5 free;
        [TestMethod]
        public void TwentyDollarsWithTenBuy10Get5FreeItems()
        {
            _cart.Add(new OrderItem() { Quantity = 10, Sku = "B10G5_APPLE" });
            Assert.AreEqual(40m, _cart.TotalAmount());
        }
        [TestMethod]
        public void TwentyDollarsWithFifteenBuy10Get5FreeItems()
        {
            _cart.Add(new OrderItem() { Quantity = 15, Sku = "B10G5_APPLE" });
            Assert.AreEqual(40m, _cart.TotalAmount());
        }
    }
}