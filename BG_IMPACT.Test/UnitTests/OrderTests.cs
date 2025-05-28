namespace BG_IMPACT.Test.UnitTests
{

    public class OrderTests
    {
        private ServiceProvider _serviceProvider; //Luôn có đi kèm với Setup
        private IOrderRepository _orderRepository;
        public void Setup()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            var services = new ServiceCollection();
            services.DependencyInject(configuration);
            _serviceProvider = services.BuildServiceProvider();
            _orderRepository = _serviceProvider.GetRequiredService<IOrderRepository>();
        }

        [TearDown]
        public void TearDown()
        {
            if (_serviceProvider is IDisposable disposable)
                disposable.Dispose();
        }


        [Test]
        public async Task CreateOrder_Success()
        {
            var param = new
            {
                CustomerID = Guid.Parse("1a5fcd3c-2c20-4c8d-8f84-89ef324c4971"), // Thay bằng ID hợp lệ
                Email = "test@example.com",
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