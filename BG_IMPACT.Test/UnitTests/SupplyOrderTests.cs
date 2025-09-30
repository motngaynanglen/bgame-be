using BG_IMPACT.Business.Command.SupplyOrder.Commands;
using Dapper;
using System.Data;

namespace BG_IMPACT.Test.UnitTests
{
    public class SupplyOrderTests : TestBase
    {
        private ISupplyOrderRepository _supplyOrderRepository;

        [SetUp]
        public void Setup()
        {
            _supplyOrderRepository = _serviceProvider.GetRequiredService<ISupplyOrderRepository>();
        }

        public class SupplyItemTest
        {
            public string Name { get; set; } = string.Empty;
            public long Quantity { get; set; }
        }

        private static DataTable ConvertToDataTableTest(List<SupplyItemTest> items)
        {
            var table = new DataTable();
            table.Columns.Add("name", typeof(string));
            table.Columns.Add("quantity", typeof(string)); // vì trong SQL là nchar(10)

            foreach (var item in items)
            {
                table.Rows.Add(item.Name, item.Quantity);
            }

            return table;
        }

        [Test]
        public async Task CreateSupplyOrder_Success()
        {
            var param = new
            {
                StoreId = Guid.Parse("A7634A36-C84E-4665-A579-19BDBFE8F89E"),
                SupplierId = Guid.Parse("CAF2A04D-91D2-43D9-97CD-D9C10304A8AF"),
                Title = "Phiếu nhập thành công",
                UserId = "test_user",
                SupplyItems = ConvertToDataTableTest(new List<SupplyItemTest>
                {
                    new SupplyItemTest { Name = "Bút bi", Quantity = 10 },
                    new SupplyItemTest { Name = "Vở ô ly", Quantity = 20 }
                }).AsTableValuedParameter("SupplyItemInputType"),
                IsTest = true
            };

            var result = await _supplyOrderRepository.spSupplyOrderCreate(param);
            var dict = result as IDictionary<string, object>;

            Console.WriteLine(dict?["Message"] ?? string.Empty);

            Assert.IsNotNull(dict);
            Assert.IsTrue(int.TryParse(dict["Status"].ToString(), out var status));
            Assert.That(status, Is.EqualTo(0));
        }

        [Test]
        public async Task CreateSupplyOrder_Fail_InvalidStoreId()
        {
            var param = new
            {
                StoreId = Guid.Parse("CAF2A04D-91D2-43D9-97CD-D9C10304A8AF"),
                SupplierId = Guid.Parse("CAF2A04D-91D2-43D9-97CD-D9C10304A8AF"),
                Title = "Phiếu nhập lỗi store",
                UserId = "test_user",
                SupplyItems = ConvertToDataTableTest(new List<SupplyItemTest>
            {
                new SupplyItemTest { Name = "Thước kẻ", Quantity = 5 }
            }).AsTableValuedParameter("SupplyItemInputType"),
                IsTest = true
            };

            var result = await _supplyOrderRepository.spSupplyOrderCreate(param);
            var dict = result as IDictionary<string, object>;

            Console.WriteLine(dict?["Message"] ?? string.Empty);

            Assert.IsNotNull(dict);
            Assert.IsTrue(int.TryParse(dict["Status"].ToString(), out var status));
            Assert.That(status, Is.EqualTo(2)); 
        }

        [Test]
        public async Task CreateSupplyOrder_Fail_InvalidSupplierId()
        {
            var param = new
            {
                StoreId = Guid.Parse("A7634A36-C84E-4665-A579-19BDBFE8F89E"),
                SupplierId = Guid.Parse("A7634A36-C84E-4665-A579-19BDBFE8F89E"),
                Title = "Phiếu nhập lỗi supplier",
                UserId = "test_user",
                SupplyItems = ConvertToDataTableTest(new List<SupplyItemTest>
            {
                new SupplyItemTest { Name = "Tẩy", Quantity = 3 }
            }).AsTableValuedParameter("SupplyItemInputType"),
                IsTest = true
            };

            var result = await _supplyOrderRepository.spSupplyOrderCreate(param);
            var dict = result as IDictionary<string, object>;

            Console.WriteLine(dict?["Message"] ?? string.Empty);

            Assert.IsNotNull(dict);
            Assert.IsTrue(int.TryParse(dict["Status"].ToString(), out var status));
            Assert.That(status, Is.EqualTo(1)); 
        }

        [Test]
        public async Task CreateSupplyOrder_Fail_EmptyItems()
        {
            var param = new
            {
                StoreId = Guid.Parse("A7634A36-C84E-4665-A579-19BDBFE8F89E"),
                SupplierId = Guid.Parse("CAF2A04D-91D2-43D9-97CD-D9C10304A8AF"),
                Title = "Phiếu nhập lỗi không có item",
                UserId = "test_user",
                SupplyItems = ConvertToDataTableTest(new List<SupplyItemTest>()) // empty list
                    .AsTableValuedParameter("SupplyItemInputType"),
                IsTest = true
            };

            var result = await _supplyOrderRepository.spSupplyOrderCreate(param);
            var dict = result as IDictionary<string, object>;

            Console.WriteLine(dict?["Message"] ?? string.Empty);

            Assert.IsNotNull(dict);
            Assert.IsTrue(int.TryParse(dict["Status"].ToString(), out var status));
            Assert.That(status, Is.EqualTo(3)); // vì không có item
        }
    }
}
