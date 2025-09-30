using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BG_IMPACT.Test.UnitTests
{
    public class SupplyItemTests : TestBase
    {
        private ISupplyItemRepository _supplyItemRepository;

        [SetUp]
        public void Setup()
        {
            _supplyItemRepository = _serviceProvider.GetRequiredService<ISupplyItemRepository>();
        }

        [Test]
        public async Task UpdateSupplyItemPrice_Success()
        {
            var param = new
            {
                SupplyItemID = Guid.Parse("C0489BE8-7A87-4600-8AB1-100C750A6241"),
                Price = 10000.0
            };

            var result = await _supplyItemRepository.spSupplyItemUpdatePrice(param);
            var dict = result as IDictionary<string, object>;

            Assert.IsNotNull(dict);
            Assert.IsTrue(int.TryParse(dict["Status"].ToString(), out var status));
            Assert.That(status, Is.EqualTo(0));
        }

        [Test]
        public async Task UpdateSupplyItemPrice_NotFoundItem()
        {
            var param = new
            {
                SupplyItemID = Guid.Parse("C0489BE8-7A87-4600-8AB1-100C750A6243"),
                Price = 5000.0
            };

            var result = await _supplyItemRepository.spSupplyItemUpdatePrice(param);
            var dict = result as IDictionary<string, object>;

            Assert.IsNotNull(dict);
            Assert.IsTrue(int.TryParse(dict["Status"].ToString(), out var status));
            Assert.That(status, Is.EqualTo(1));
        }
    }
}
