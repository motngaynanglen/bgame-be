namespace BG_IMPACT.Test.UnitTests
{
    class OrderItemProductTests
    {
        private ServiceProvider _serviceProvider; //Luôn có đi kèm với Setup
        private IOrderItemRepository _orderItemRepository;
        [SetUp]
        public void Setup()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            var services = new ServiceCollection();
            services.DependencyInject(configuration);
            _serviceProvider = services.BuildServiceProvider();
            _orderItemRepository = _serviceProvider.GetRequiredService<IOrderItemRepository>();
        }

        [TearDown]
        public void TearDown()
        {
            if (_serviceProvider is IDisposable disposable)
                disposable.Dispose();
        }

        [Test]
        public async Task UpdateOrderItemProduct_Successful()
        {
            var param = new
            {
                StaffId = "A44ED012-283F-48C4-BC44-0142D772409B",
                OrderItemId = "FEABF94B-57C5-431D-9731-2A7132A9E168", // Đơn hàng tại cửa hàng 1
                ProductCode = "Nana-T0001-0000040", // Code bộ Nana bỏ vào order
                IsTest = true
            };
            var result = await _orderItemRepository.spOrderItemUpdateProduct(param);

            var dict = result as IDictionary<string, object>;

            Assert.IsNotNull(dict);
            Assert.IsTrue(long.TryParse(dict["Status"].ToString(), out _));
            Assert.IsNotNull(dict["Status"]);

            bool check = int.TryParse(dict["Status"].ToString(), out int count);
            Assert.IsTrue(check);

            Assert.That(count, Is.EqualTo(0));
        }

        [Test]
        public async Task UpdateOrderItemProduct_Wrong()
        {
            var param = new
            {
                StaffId = "A44ED012-283F-48C4-BC44-0142D772409B",
                OrderItemId = "FEABF94B-57C5-431D-9731-2A7132A9E168",
                ProductCode = "123",
                IsTest = true
            };
            var result = await _orderItemRepository.spOrderItemUpdateProduct(param);

            var dict = result as IDictionary<string, object>;

            Assert.IsNotNull(dict);
            Assert.IsTrue(long.TryParse(dict["Status"].ToString(), out _));
            Assert.IsNotNull(dict["Status"]);

            bool check = int.TryParse(dict["Status"].ToString(), out int count);
            Assert.IsTrue(check);
            Assert.That(count, Is.EqualTo(1));
        }
    }
}
