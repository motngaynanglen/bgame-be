namespace BG_IMPACT.Test.UnitTests
{
    public class StoreTests : TestBase
    {
        private IStoreRepository _storeRepository;
        [SetUp]
        public void Setup()
        {
            _storeRepository = _serviceProvider.GetRequiredService<IStoreRepository>();
        }

        [Test]
        public async Task Create_Successful()
        {
            var param = new
            {
                StoreName = "Cửa hàng Boardgame & Cafe Sinh Viên",
                Address = "222 Đinh Bộ Lĩnh, phường 26, quận Bình Thạnh",
                Hotline = "0123456789",
                Longtitude = "10.123456",
                Lattitude = "106.123456",
                Email = "bgcf@gmail.com",
                IsTest = true
            };
            var result = await _storeRepository.spStoreCreate(param);

            var dict = result as IDictionary<string, object>;

            Assert.IsNotNull(dict);
            Assert.IsTrue(long.TryParse(dict["Status"].ToString(), out _));
            Assert.IsNotNull(dict["Status"]);

            bool check = int.TryParse(dict["Status"].ToString(), out int count);
            Assert.IsTrue(check);
            Assert.That(count, Is.EqualTo(0));
        }

        [Test]
        public async Task Create_ExistStoreName()
        {
            var param = new
            {
                StoreName = "Cửa hàng Boardgame HĐ mới 2025",
                Address = "222 Đinh Bộ Lĩnh, phường 26, quận Bình Thạnh",
                Hotline = "0123456789",
                Longtitude = "10.123456",
                Lattitude = "106.123456",
                Email = "bgcf@gmail.com",
                IsTest = true
            };
            var result = await _storeRepository.spStoreCreate(param);

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