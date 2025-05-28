namespace BG_IMPACT.Test.UnitTests
{
    public class ProductGroupRefTests
    {
        private ServiceProvider _serviceProvider; //Luôn có đi kèm với Setup
        private IProductGroupRefRepository _productGroupRefRepository; //Phụ thuộc vào API có sử dụng hay không

        [SetUp]
        public void Setup()
        {
            //Giữ nguyên và copy lại
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            var services = new ServiceCollection();
            services.DependencyInject(configuration);
            _serviceProvider = services.BuildServiceProvider();

            //Inject các Repo vào để sử dụng
            _productGroupRefRepository = _serviceProvider.GetRequiredService<IProductGroupRefRepository>();
        }

        [TearDown]
        public void TearDown()
        {
            if (_serviceProvider is IDisposable disposable)
                disposable.Dispose();
        }

        [Test]
        public async Task Create_Successful()
        {
            var param = new
            {
                GroupId = "90ecca84-0f64-4610-a42d-021303794d9a",
                Prefix = "UIT22",
                GroupRefName = "UITest 2 Test",
                Description = "abcxyz",
                ManagerID = "14ece14e-ace2-416a-92b8-56d92a7abcca",
                IsTest = true
            };

            var result = await _productGroupRefRepository.spProductGroupRefCreate(param);

            var dict = result as IDictionary<string, object>;
            //1 if = 1 assert
            Assert.IsNotNull(dict); // = if (dict != null ...)
            Assert.IsTrue(long.TryParse(dict["Status"].ToString(), out _));
            Assert.IsNotNull(dict["Status"]);
            //_ = Int64.TryParse(dict["Status"].ToString(), out long count);

            bool check = int.TryParse(dict["Status"].ToString(), out int count);
            Assert.IsTrue(check);
            Assert.That(count, Is.EqualTo(0));
        }

        [Test]
        public async Task Create_ExistGroupRefName()
        {
            var param = new
            {
                GroupId = "90ecca84-0f64-4610-a42d-021303794d9a",
                Prefix = "UIT22",
                GroupRefName = "Splendor",
                Description = "abcxyz",
                ManagerID = "14ece14e-ace2-416a-92b8-56d92a7abcca",
                IsTest = true
            };

            var result = await _productGroupRefRepository.spProductGroupRefCreate(param);

            var dict = result as IDictionary<string, object>;
            //1 if = 1 assert
            Assert.IsNotNull(dict); // = if (dict != null ...)
            Assert.IsTrue(long.TryParse(dict["Status"].ToString(), out _));
            Assert.IsNotNull(dict["Status"]);
            //_ = Int64.TryParse(dict["Status"].ToString(), out long count);

            bool check = int.TryParse(dict["Status"].ToString(), out int count);
            Assert.IsTrue(check);
            Assert.That(count, Is.EqualTo(1));
        }

        [Test]
        public async Task Create_ExistPrefix()
        {
            var param = new
            {
                GroupId = "90ecca84-0f64-4610-a42d-021303794d9a",
                Prefix = "UIT2",
                GroupRefName = "UITest 2 Test",
                Description = "abcxyz",
                ManagerID = "14ece14e-ace2-416a-92b8-56d92a7abcca",
                IsTest = true
            };

            var result = await _productGroupRefRepository.spProductGroupRefCreate(param);

            var dict = result as IDictionary<string, object>;
            //1 if = 1 assert
            Assert.IsNotNull(dict); // = if (dict != null ...)
            Assert.IsTrue(long.TryParse(dict["Status"].ToString(), out _));
            Assert.IsNotNull(dict["Status"]);
            //_ = Int64.TryParse(dict["Status"].ToString(), out long count);

            bool check = int.TryParse(dict["Status"].ToString(), out int count);
            Assert.IsTrue(check);
            Assert.That(count, Is.EqualTo(2));
        }

        [Test]
        public async Task UpdateProductGroupRef_Success()
        {
            var param = new
            {
                GroupRefID = "e959d29f-a1d5-4f74-903c-1aa933952d88",
                GroupRefName = "Splendorr",
                Description = "Test case",
                IsTest = true
            };

            var result = await _productGroupRefRepository.spProductGroupRefUpdate(param);

            var dict = result as IDictionary<string, object>;
            //1 if = 1 assert
            Assert.IsNotNull(dict); // = if (dict != null ...)
            Assert.IsTrue(long.TryParse(dict["Status"].ToString(), out _));
            Assert.IsNotNull(dict["Status"]);
            //_ = Int64.TryParse(dict["Status"].ToString(), out long count);

            bool check = int.TryParse(dict["Status"].ToString(), out int count);
            Assert.IsTrue(check);
            Assert.That(count, Is.EqualTo(1));
        }

        [Test]
        public async Task UpdateProductGroupRef_NotExistGroupRefID()
        {
            var param = new
            {
                GroupRefID = "90ecca84-0f64-4610-a42d-021303794d9z",
                GroupRefName = "UIT2",
                Description = "UITest 2 Test",
                IsTest = true
            };

            var result = await _productGroupRefRepository.spProductGroupRefUpdate(param);

            var dict = result as IDictionary<string, object>;
            //1 if = 1 assert
            Assert.IsNotNull(dict); // = if (dict != null ...)
            Assert.IsTrue(long.TryParse(dict["Status"].ToString(), out _));
            Assert.IsNotNull(dict["Status"]);
            //_ = Int64.TryParse(dict["Status"].ToString(), out long count);

            bool check = int.TryParse(dict["Status"].ToString(), out int count);
            Assert.IsTrue(check);
            Assert.That(count, Is.EqualTo(3));
        }

    }
















































































































































}
