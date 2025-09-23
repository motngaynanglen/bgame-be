using Azure.Core;
using CloudinaryDotNet.Actions;
using Dapper;
using System.Data;

namespace BG_IMPACT.Test.UnitTests
{

    public class OrderTests : TestBase
    {
        private IOrderRepository _orderRepository;
        public void Setup()
        {    
            _orderRepository = _serviceProvider.GetRequiredService<IOrderRepository>();
        }

        [Test]
        public async Task CreateOrder_Success()
        {
            List<OrderItem> list = new List<OrderItem>{
                new OrderItem
                {
                    ProductTemplateID = Guid.Parse("529E76BD-3FFE-4B5D-8727-283620453E30"), // ID hợp lệ
                    Quantity = 1
                },
                new OrderItem
                {
                    ProductTemplateID = Guid.Parse("5386163D-D814-4D86-97D5-4D43F53DF1E8"), // ID hợp lệ
                    Quantity = 1
                }
            };

            DataTable dt = new DataTable();
            dt.Columns.Add("StoreID", typeof(Guid));
            dt.Columns.Add("ProductTemplateID", typeof(Guid));
            dt.Columns.Add("Quantity", typeof(int));

            foreach (var item in list)
            {
                dt.Rows.Add(Guid.NewGuid(), item.ProductTemplateID, item.Quantity); // Or use actual StoreID
            }

            var param = new
            {
                CustomerID = "1DA1B7F8-D21F-4922-8F4C-048BC3E973E9",
                StaffID = "A44ED012-283F-48C4-BC44-0142D772409B",
                OrdersCreateForm = dt.AsTableValuedParameter("OrdersCreateFormItemType"),
                Email = "test@example.com",
                FullName = "Nguyễn Văn A",
                PhoneNumber = "0123456789",
                Address = "123 Đường ABC, Quận XYZ",
                Role = "STAFF",
                IsTest = true
            };

            var result = await _orderRepository.spOrderCreate(param);
            var dict = result as IDictionary<string, object>;

            Assert.IsNotNull(dict);
            Assert.IsTrue(long.TryParse(dict["Status"].ToString(), out _));
            Assert.IsNotNull(dict["Status"]);

            bool check = int.TryParse(dict["Status"].ToString(), out int count);
            Assert.IsTrue(check);
            Assert.That(count, Is.EqualTo(0));
        }

        [Test]
        public async Task UpdateOrderDeliveryInfo_Success()
        {
            var param = new
            {
                ProductGroupID = Guid.Parse("1a5fcd3c-2c20-4c8d-8f84-89ef324c4971"), // Thay bằng ID hợp lệ
                GroupName = "test@example.com",
                FullName = "Nguyễn Văn A",
                PhoneNumber = "0123456789",
                Address = "123 Đường ABC, Quận XYZ",
                OrderItems = new List<OrderItem>
                {
                new OrderItem
                {
                    ProductTemplateID = Guid.Parse("2b60c3e2-7f51-4fa9-9a53-cc3ddc43d9bb"), // ID hợp lệ
                    Quantity = 2
                },
                new OrderItem
                {
                    ProductTemplateID = Guid.Parse("3c2ff7e3-6272-41b4-bab7-c6a1d345a123"), // ID hợp lệ
                    Quantity = 1
                }
                },
                IsTest = true
            };

            var result = await _orderRepository.spOrderUpdateDeliveryInfo(param);
            var dict = result as IDictionary<string, object>;

            Assert.IsNotNull(dict);
            Assert.IsTrue(long.TryParse(dict["Status"].ToString(), out _));
            Assert.IsNotNull(dict["Status"]);

            bool check = int.TryParse(dict["Status"].ToString(), out int count);
            Assert.IsTrue(check);
            Assert.That(count, Is.EqualTo(0));
        }

    }
}