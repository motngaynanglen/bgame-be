using Azure.Core;
using CloudinaryDotNet.Actions;
using Dapper;
using System.Data;

namespace BG_IMPACT.Test.UnitTests
{

    public class OrderTests : TestBase
    {
        private IOrderRepository _orderRepository;
        private IOrderItemRepository _orderItemRepository;
      
        [SetUp]
        public void Setup()
        {    
            _orderRepository = _serviceProvider.GetRequiredService<IOrderRepository>();
            _orderItemRepository = _serviceProvider.GetRequiredService<IOrderItemRepository>();
        }

        public class OrderFormItemTest
        {
            public Guid StoreId { get; set; }
            public Guid ProductTemplateId { get; set; }
            public int Quantity { get; set; }
        }

        private static DataTable ConvertToDataTableTest(List<OrderFormItemTest> orders)
        {
            var table = new DataTable();
            table.Columns.Add("StoreID", typeof(Guid));
            table.Columns.Add("ProductTemplateID", typeof(Guid));
            table.Columns.Add("Quantity", typeof(int));

            foreach (var item in orders)
            {
                table.Rows.Add(item.StoreId, item.ProductTemplateId, item.Quantity);
            }

            return table;
        }

        //[Test]
        //public async Task CreateOrder_Success()
        //{
        //    List<OrderItem> list = new List<OrderItem>{
        //        new OrderItem
        //        {
        //            ProductTemplateID = Guid.Parse("529E76BD-3FFE-4B5D-8727-283620453E30"), // ID hợp lệ
        //            Quantity = 1
        //        },
        //        new OrderItem
        //        {
        //            ProductTemplateID = Guid.Parse("5386163D-D814-4D86-97D5-4D43F53DF1E8"), // ID hợp lệ
        //            Quantity = 1
        //        }
        //    };

        //    DataTable dt = new DataTable();
        //    dt.Columns.Add("StoreID", typeof(Guid));
        //    dt.Columns.Add("ProductTemplateID", typeof(Guid));
        //    dt.Columns.Add("Quantity", typeof(int));

        //    foreach (var item in list)
        //    {
        //        dt.Rows.Add(Guid.NewGuid(), item.ProductTemplateID, item.Quantity); // Or use actual StoreID
        //    }

        //    var param = new
        //    {
        //        CustomerID = "1DA1B7F8-D21F-4922-8F4C-048BC3E973E9",
        //        StaffID = "A44ED012-283F-48C4-BC44-0142D772409B",
        //        OrdersCreateForm = dt.AsTableValuedParameter("OrdersCreateFormItemType"),
        //        Email = "test@example.com",
        //        FullName = "Nguyễn Văn A",
        //        PhoneNumber = "0123456789",
        //        Address = "123 Đường ABC, Quận XYZ",
        //        Role = "STAFF",
        //        IsTest = true
        //    };

        //    var result = await _orderRepository.spOrderCreate(param);
        //    var dict = result as IDictionary<string, object>;

        //    Assert.IsNotNull(dict);
        //    Assert.IsTrue(long.TryParse(dict["Status"].ToString(), out _));
        //    Assert.IsNotNull(dict["Status"]);

        //    bool check = int.TryParse(dict["Status"].ToString(), out int count);
        //    Assert.IsTrue(check);
        //    Assert.That(count, Is.EqualTo(0));
        //}

        //[Test]
        //public async Task UpdateOrderDeliveryInfo_Success()
        //{
        //    var param = new
        //    {
        //        ProductGroupID = Guid.Parse("1a5fcd3c-2c20-4c8d-8f84-89ef324c4971"), // Thay bằng ID hợp lệ
        //        GroupName = "test@example.com",
        //        FullName = "Nguyễn Văn A",
        //        PhoneNumber = "0123456789",
        //        Address = "123 Đường ABC, Quận XYZ",
        //        OrdersCreateForm = ConvertToDataTableTest(new List<OrderFormItemTest>
        //        {
        //            new OrderFormItemTest
        //            {
        //                StoreId = Guid.Parse("C0D8B9F4-23B0-4845-9E23-22989CD96316"), // store id
        //                ProductTemplateId = Guid.Parse("2b60c3e2-7f51-4fa9-9a53-cc3ddc43d9bb"),
        //                Quantity = 2
        //            },
        //            new OrderFormItemTest
        //            {
        //                StoreId = Guid.Parse("C0D8B9F4-23B0-4845-9E23-22989CD96316"), // same or different store id
        //                ProductTemplateId = Guid.Parse("3c2ff7e3-6272-41b4-bab7-c6a1d345a123"),
        //                Quantity = 1
        //            }
        //        }).AsTableValuedParameter("OrdersCreateFormItemType"),
        //        IsTest = true
        //    };

        //    var result = await _orderRepository.spOrderUpdateDeliveryInfo(param);
        //    var dict = result as IDictionary<string, object>;

        //    Assert.IsNotNull(dict);
        //    Assert.IsTrue(long.TryParse(dict["Status"].ToString(), out _));
        //    Assert.IsNotNull(dict["Status"]);

        //    bool check = int.TryParse(dict["Status"].ToString(), out int count);
        //    Assert.IsTrue(check);
        //    Assert.That(count, Is.EqualTo(0));
        //}

        [Test]
        public async Task CreateOrderByCustomer_Success()
        {
            var param = new
            {
                CustomerID = Guid.Parse("6D616E95-290F-4215-940C-28A444A2A2C6"),
                Email = "test@example.com",
                FullName = "Nguyễn Văn A",
                PhoneNumber = "0123456789",
                Address = "123 Đường ABC, Quận XYZ",
                OrdersCreateForm = ConvertToDataTableTest(new List<OrderFormItemTest>
        {
            new OrderFormItemTest
            {
                StoreId = Guid.Parse("C0D8B9F4-23B0-4845-9E23-22989CD96316"),
                ProductTemplateId = Guid.Parse("A5165A8B-714A-4F86-88BC-B6B41338CD45"),
                Quantity = 1
            }
        }).AsTableValuedParameter("OrdersCreateFormItemType"),
                IsTest = true
            };

            var result = await _orderRepository.spOrderCreateByCustomer(param);
            var dict = result as IDictionary<string, object>;

            Console.WriteLine(dict["Message"] ?? string.Empty);

            Assert.IsNotNull(dict);
            Assert.IsTrue(int.TryParse(dict["Status"].ToString(), out var status));
            Assert.That(status, Is.EqualTo(0));
        }

        [Test]
        public async Task CreateOrderByCustomer_NoOrderItems()
        {
            var param = new
            {
                CustomerID = Guid.NewGuid(),
                Email = "test@example.com",
                FullName = "Nguyễn Văn A",
                PhoneNumber = "0123456789",
                Address = "123 Đường ABC, Quận XYZ",
                OrdersCreateForm = ConvertToDataTableTest(new List<OrderFormItemTest>()).AsTableValuedParameter("OrdersCreateFormItemType"),
                IsTest = true
            };

            var result = await _orderRepository.spOrderCreateByCustomer(param);
            var dict = result as IDictionary<string, object>;

            Console.WriteLine(dict["Message"] ?? string.Empty);

            Assert.IsNotNull(dict);
            Assert.IsTrue(int.TryParse(dict["Status"].ToString(), out var status));
            Assert.That(status, Is.EqualTo(1));
        }

        [Test]
        public async Task CreateOrderByCustomer_ProductTemplateNotFound()
        {
            var param = new
            {
                CustomerID = Guid.NewGuid(),
                Email = "test@example.com",
                FullName = "Nguyễn Văn B",
                PhoneNumber = "0987654321",
                Address = "456 Đường DEF, Quận UVW",
                OrdersCreateForm = ConvertToDataTableTest(new List<OrderFormItemTest>
                {
                    new OrderFormItemTest
                    {
                        StoreId = Guid.Parse("C0D8B9F4-23B0-4845-9E23-22989CD96316"),
                        ProductTemplateId = Guid.NewGuid(), 
                        Quantity = 1
                    }
                }).AsTableValuedParameter("OrdersCreateFormItemType"),
                IsTest = true
            };

            var result = await _orderRepository.spOrderCreateByCustomer(param);
            var dict = result as IDictionary<string, object>;

            Console.WriteLine(dict["Message"] ?? string.Empty);

            Assert.IsNotNull(dict);
            Assert.IsTrue(int.TryParse(dict["Status"].ToString(), out var status));
            Assert.That(status, Is.EqualTo(1));
        }

        [Test]
        public async Task CreateOrderByCustomer_HasUnpaidOrderToday()
        {
            var customerId = Guid.NewGuid();

            var param = new
            {
                CustomerID = Guid.Parse("1DA1B7F8-D21F-4922-8F4C-048BC3E973E9"),
                Email = "test@example.com",
                FullName = "Nguyễn Văn C",
                PhoneNumber = "0112233445",
                Address = "789 Đường GHI, Quận MNO",
                OrdersCreateForm = ConvertToDataTableTest(new List<OrderFormItemTest>
        {
            new OrderFormItemTest
            {
                StoreId = Guid.Parse("C0D8B9F4-23B0-4845-9E23-22989CD96316"),
                ProductTemplateId = Guid.Parse("A5165A8B-714A-4F86-88BC-B6B41338CD45"),
                Quantity = 1
            }
        }).AsTableValuedParameter("OrdersCreateFormItemType"),
                IsTest = true,
                IsUnpaid = true
            };

            var result = await _orderRepository.spOrderCreateByCustomer(param);
            var dict = result as IDictionary<string, object>;

            Console.WriteLine(dict["Message"] ?? string.Empty);

            Assert.IsNotNull(dict);
            Assert.IsTrue(int.TryParse(dict["Status"].ToString(), out var status));
            Assert.That(status, Is.EqualTo(4));
        }

        [Test]
        public async Task CreateOrderByCustomer_CanceledTooManyOrdersWithin6h()
        {
            var customerId = Guid.NewGuid();

            var param = new
            {
                CustomerID = Guid.Parse("1DA1B7F8-D21F-4922-8F4C-048BC3E973E9"),
                Email = "test@example.com",
                FullName = "Nguyễn Văn D",
                PhoneNumber = "0666777888",
                Address = "101 Đường JKL, Quận PQR",
                OrdersCreateForm = ConvertToDataTableTest(new List<OrderFormItemTest>
        {
            new OrderFormItemTest
            {
                StoreId = Guid.Parse("C0D8B9F4-23B0-4845-9E23-22989CD96316"),
                ProductTemplateId = Guid.Parse("A5165A8B-714A-4F86-88BC-B6B41338CD45"),
                Quantity = 1
            }
        }).AsTableValuedParameter("OrdersCreateFormItemType"),
                IsTest = true,
                IsCanceledTooManyOrdersWithin6h = true
            };

            var result = await _orderRepository.spOrderCreateByCustomer(param);
            var dict = result as IDictionary<string, object>;

            Console.WriteLine(dict["Message"] ?? string.Empty);

            Assert.IsNotNull(dict);
            Assert.IsTrue(int.TryParse(dict["Status"].ToString(), out var status));
            Assert.That(status, Is.EqualTo(4));
        }

        [Test]
        public async Task CreateOrderByCustomer_NotEnoughInventory()
        {
            var param = new
            {
                CustomerID = Guid.NewGuid(),
                Email = "test@example.com",
                FullName = "Nguyễn Văn E",
                PhoneNumber = "0999888777",
                Address = "202 Đường MNO, Quận STU",
                OrdersCreateForm = ConvertToDataTableTest(new List<OrderFormItemTest>
        {
            new OrderFormItemTest
            {
                StoreId = Guid.Parse("C0D8B9F4-23B0-4845-9E23-22989CD96316"),
                ProductTemplateId = Guid.Parse("A5165A8B-714A-4F86-88BC-B6B41338CD45"),
                Quantity = 99999 
            }
        }).AsTableValuedParameter("OrdersCreateFormItemType"),
                IsTest = true
            };

            var result = await _orderRepository.spOrderCreateByCustomer(param);
            var dict = result as IDictionary<string, object>;

            Console.WriteLine(dict["Message"] ?? string.Empty);

            Assert.IsNotNull(dict);
            Assert.IsTrue(int.TryParse(dict["Status"].ToString(), out var status));
            Assert.That(status, Is.EqualTo(4));
        }

        [Test]
        public async Task CreateOrderByStaff_StaffNotFound()
        {
            var param = new
            {
                StaffID = Guid.Parse("A44ED012-283F-48C4-BC44-0142D7724092"),
                CustomerID = (Guid?)null,
                PhoneNumber = "0123456789",
                ListProductIDs = "EF48A007-6CE2-4CCD-AE05-08EEC4FF2D49,FBD93C96-A1F6-4E05-B17F-2E220F50BC82",
                IsTest = true
            };

            var result = await _orderRepository.spOrderCreateByStaff(param);
            var dict = result as IDictionary<string, object>;

            Console.WriteLine(dict?["Message"] ?? string.Empty);

            Assert.IsNotNull(dict);
            Assert.IsTrue(int.TryParse(dict["Status"].ToString(), out var status));
            Assert.That(status, Is.EqualTo(1));
        }

        [Test]
        public async Task CreateOrderByStaff_InvalidProducts()
        {
            var param = new
            {
                StaffID = Guid.Parse("A44ED012-283F-48C4-BC44-0142D772409B"),
                CustomerID = (Guid?)null,
                PhoneNumber = "0123456789",
                ListProductIDs = "EF48A007-6CE2-4CCD-AE05-08EEC4FF2D41,FBD93C96-A1F6-4E05-B17F-2E220F50BC82",
                IsTest = true
            };

            var result = await _orderRepository.spOrderCreateByStaff(param);
            var dict = result as IDictionary<string, object>;

            Console.WriteLine(dict?["Message"] ?? string.Empty);

            Assert.IsNotNull(dict);
            Assert.IsTrue(int.TryParse(dict["Status"].ToString(), out var status));
            Assert.That(status, Is.EqualTo(1));
        }

        [Test]
        public async Task CreateOrderByStaff_Success()
        {
            var param = new
            {
                StaffID = Guid.Parse("A44ED012-283F-48C4-BC44-0142D772409B"),
                CustomerID = (Guid?)null,
                PhoneNumber = "0123456789",
                ListProductIDs = "B0CCCFA4-9AD7-485F-9C14-4B2EB55A9E31",
                IsTest = true
            };

            var result = await _orderRepository.spOrderCreateByStaff(param);
            var dict = result as IDictionary<string, object>;

            Console.WriteLine(dict?["Message"] ?? string.Empty);

            Assert.IsNotNull(dict);
            Assert.IsTrue(int.TryParse(dict["Status"].ToString(), out var status));
            Assert.That(status, Is.EqualTo(0));
        }

        [Test]
        public async Task UpdateOrderItem_StaffNotFound()
        {
            var param = new
            {
                StaffId = Guid.Parse("4026C681-6EF7-46D1-94D0-27AEA39D00E1"),
                OrderItemId = Guid.Parse("8B60619C-5A71-45EB-9FE3-5F835CCD869E"),
                ProductCode = "TQS-T0001-0000012",
                IsTest = true
            };

            var result = await _orderItemRepository.spOrderItemUpdateProduct(param);
            var dict = result as IDictionary<string, object>;

            Assert.That(dict, Is.Not.Null);
            Assert.That(int.Parse(dict["Status"].ToString()), Is.EqualTo(1));
        }

        // 2. Product not found -> Status = 1
        [Test]
        public async Task UpdateOrderItem_ProductNotFound()
        {
            var param = new
            {
                StaffId = Guid.Parse("4026C681-6EF7-46D1-94D0-27AEA39D00EB"),
                OrderItemId = Guid.Parse("8B60619C-5A71-45EB-9FE3-5F835CCD869E"),
                ProductCode = "TQS-T0001-0000123",
                IsTest = true
            };

            var result = await _orderItemRepository.spOrderItemUpdateProduct(param);
            var dict = result as IDictionary<string, object>;

            Assert.That(dict, Is.Not.Null);
            Assert.That(int.Parse(dict["Status"].ToString()), Is.EqualTo(1));
        }

        // 3. OrderItem not found -> Status = 1
        [Test]
        public async Task UpdateOrderItem_OrderItemNotFound()
        {
            var param = new
            {
                StaffId = Guid.Parse("4026C681-6EF7-46D1-94D0-27AEA39D00EB"),
                OrderItemId = Guid.Parse("8B60619C-5A71-45EB-9FE3-5F835CCD8691"),
                ProductCode = "TQS-T0001-0000012",
                IsTest = true
            };

            var result = await _orderItemRepository.spOrderItemUpdateProduct(param);
            var dict = result as IDictionary<string, object>;

            Assert.That(dict, Is.Not.Null);
            Assert.That(int.Parse(dict["Status"].ToString()), Is.EqualTo(1));
        }

        // 4. Duplicate product code -> Status = 3
        [Test]
        public async Task UpdateOrderItem_DuplicateProduct()
        {
            var param = new
            {
                StaffId = Guid.Parse("4026C681-6EF7-46D1-94D0-27AEA39D00EB"),
                OrderItemId = Guid.Parse("8B60619C-5A71-45EB-9FE3-5F835CCD869E"),
                ProductCode = "TQS-T0001-0000006",
                IsTest = true
            };

            var result = await _orderItemRepository.spOrderItemUpdateProduct(param);
            var dict = result as IDictionary<string, object>;

            Assert.That(dict, Is.Not.Null);
            Assert.That(int.Parse(dict["Status"].ToString()), Is.EqualTo(3));
        }

        // 5. Order status invalid -> Status = 3
        [Test]
        public async Task UpdateOrderItem_OrderStatusInvalid()
        {
            var param = new
            {
                StaffId = Guid.Parse("4026C681-6EF7-46D1-94D0-27AEA39D00EB"),
                OrderItemId = Guid.Parse("75226E56-167D-4218-A561-4960B9F22B41"),
                ProductCode = "COR0000007",
                IsTest = true
            };

            var result = await _orderItemRepository.spOrderItemUpdateProduct(param);
            var dict = result as IDictionary<string, object>;
            Console.WriteLine(dict["Message"] ?? string.Empty);

            Assert.That(dict, Is.Not.Null);
            Assert.That(int.Parse(dict["Status"].ToString()), Is.EqualTo(3));
        }

        [Test]
        public async Task UpdateOrderItem_ProductNotInSameStore()
        {
            var param = new
            {
                StaffId = Guid.Parse("4026C681-6EF7-46D1-94D0-27AEA39D00EB"),
                OrderItemId = Guid.Parse("8B60619C-5A71-45EB-9FE3-5F835CCD869E"),
                ProductCode = "TQS-T0001-0000015",
                IsTest = true
            };

            var result = await _orderItemRepository.spOrderItemUpdateProduct(param);
            var dict = result as IDictionary<string, object>;

            Assert.That(dict, Is.Not.Null);
            Assert.That(int.Parse(dict["Status"].ToString()), Is.EqualTo(2));
        }

        // 7. Staff not in order store -> Status = 2
        [Test]
        public async Task UpdateOrderItem_StaffNotInOrderStore()
        {
            var param = new
            {
                StaffId = Guid.Parse("A7465ED2-6153-4548-BBFA-1CEC362BE986"),
                OrderItemId = Guid.Parse("8B60619C-5A71-45EB-9FE3-5F835CCD869E"),
                ProductCode = "TQS-T0001-0000012",
                IsTest = true
            };

            var result = await _orderItemRepository.spOrderItemUpdateProduct(param);
            var dict = result as IDictionary<string, object>;

            Assert.That(dict, Is.Not.Null);
            Assert.That(int.Parse(dict["Status"].ToString()), Is.EqualTo(2));
        }

        // 8. Product template mismatch -> Status = 1
        [Test]
        public async Task UpdateOrderItem_TemplateMismatch()
        {
            var param = new
            {
                StaffId = Guid.Parse("4026C681-6EF7-46D1-94D0-27AEA39D00EB"),
                OrderItemId = Guid.Parse("8B60619C-5A71-45EB-9FE3-5F835CCD869E"),
                ProductCode = "TQSQC-T0001-0000020",
                IsTest = true
            };

            var result = await _orderItemRepository.spOrderItemUpdateProduct(param);
            var dict = result as IDictionary<string, object>;

            Assert.That(dict, Is.Not.Null);
            Assert.That(int.Parse(dict["Status"].ToString()), Is.EqualTo(1));
        }

        [Test]
        public async Task UpdateOrderItem_Success()
        {
            var param = new
            {
                StaffId = Guid.Parse("4026C681-6EF7-46D1-94D0-27AEA39D00EB"),
                OrderItemId = Guid.Parse("8B60619C-5A71-45EB-9FE3-5F835CCD869E"),
                ProductCode = "TQS-T0001-0000012",
                IsTest = true
            };

            var result = await _orderItemRepository.spOrderItemUpdateProduct(param);
            var dict = result as IDictionary<string, object>;

            Assert.That(dict, Is.Not.Null);
            Assert.That(int.Parse(dict["Status"].ToString()), Is.EqualTo(0));
            Assert.That(dict.ContainsKey("Data"));
        }
    }
}