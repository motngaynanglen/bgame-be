namespace BG_IMPACT.Test.UnitTests
{
    public class SuplierTests
    {
        private ServiceProvider _serviceProvider;
        private ISupplierRepository _supplierRepository;
        [SetUp]
        public void Setup()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            var services = new ServiceCollection();
            services.DependencyInject(configuration);
            _serviceProvider = services.BuildServiceProvider();
            _supplierRepository = _serviceProvider.GetRequiredService<ISupplierRepository>();
        }

        [TearDown]
        public void TearDown()
        {
            if (_serviceProvider is IDisposable disposable)
                disposable.Dispose();
        }

        [Test]
        public async Task CreateSupplier_Successful()
        {
            var param = new
            {
                SupplierName = "Bộ đồ chơi cờ tỷ phú ",
                Address = "222 Đinh Bộ Lĩnh, phường 26, quận Bình Thạnh",
                Longitude = "106.123456",
                Latitude = "106.123456",
                Email = "tranchauphuongnam@gmail.com",
                PhoneNumber = "01223335364",
                ManagerID = "14ece14e-ace2-416a-92b8-56d92a7abcca",
                IsTest = true
            };
            var result = await _supplierRepository.spSupplierCreate(param);

            var dict = result as IDictionary<string, object>;

            Assert.IsNotNull(dict);
            Assert.IsTrue(long.TryParse(dict["Status"].ToString(), out _));
            Assert.IsNotNull(dict["Status"]);

            bool check = int.TryParse(dict["Status"].ToString(), out int count);
            Assert.IsTrue(check);
            Assert.That(count, Is.EqualTo(0));
        }

        [Test]
        public async Task UpdateSupplier_Successful()
        {
            var param = new
            {
                SupplierId = "caf2a04d-91d2-43d9-97cd-d9c10304a8af",
                SupplierName = "Hàng kí gửi loại 1",
                Address = "222 Đinh Bộ Lĩnh, phường 26, quận Bình Thạnh",
                Longitude = "10.220123456789",
                Latitude = "106.123456",
                Email = "bgcf@gmail.com",
                PhoneNumber = "03212256482",
                Status = "ACTIVE",
                ManagerId = "14ece14e-ace2-416a-92b8-56d92a7abcca",
                IsTest = true
            };
            var result = await _supplierRepository.spSupplierUpdate(param);

            var dict = result as IDictionary<string, object>;

            Assert.IsNotNull(dict);
            Assert.IsTrue(long.TryParse(dict["Status"].ToString(), out _));
            Assert.IsNotNull(dict["Status"]);

            bool check = int.TryParse(dict["Status"].ToString(), out int count);
            Assert.IsTrue(check);
            Assert.That(count, Is.EqualTo(0));
        }

        [Test]
        public async Task UpdateSupplier_NotExistSupplier()
        {
            var param = new
            {
                SupplierId = "caf2a04d-91d2-43d9-97cd-d9c10304a8a1",
                SupplierName = "Hàng kí gửi loại 1",
                Address = "222 Đinh Bộ Lĩnh, phường 26, quận Bình Thạnh",
                Longitude = "10.220123456789",
                Latitude = "106.123456",
                Email = "bgcf@gmail.com",
                PhoneNumber = "03212256482",
                Status = "ACTIVE",
                ManagerId = "14ece14e-ace2-416a-92b8-56d92a7abcca",
                IsTest = true
            };
            var result = await _supplierRepository.spSupplierUpdate(param);

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
