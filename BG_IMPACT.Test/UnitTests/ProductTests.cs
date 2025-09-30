namespace BG_IMPACT.Test.UnitTests
{
    public class ProductTests : TestBase
    {
        private IProductRepository _productRepository;
        [SetUp]
        public void Setup()
        {
            _productRepository = _serviceProvider.GetRequiredService<IProductRepository>();
        }

        [Test]
        public async Task ChangeProductToSales_Success()
        {
            var param = new
            {
                Code = "TQSQ0000006",
                IsTest = true
            };
            var result = await _productRepository.spProductChangeToSales(param);

            var dict = result as IDictionary<string, object>;

            Assert.IsNotNull(dict);
            Assert.IsTrue(long.TryParse(dict["Status"].ToString(), out _));
            Assert.IsNotNull(dict["Status"]);

            bool check = int.TryParse(dict["Status"].ToString(), out int count);
            Assert.IsTrue(check);
            Assert.That(count, Is.EqualTo(0));
        }

        [Test]
        public async Task ChangeProductToSales_WrongCode()
        {
            var param = new
            {
                Code = "Nana-T0001-1234567",
                IsTest = true
            };
            var result = await _productRepository.spProductChangeToSales(param);

            var dict = result as IDictionary<string, object>;

            Assert.IsNotNull(dict);
            Assert.IsTrue(long.TryParse(dict["Status"].ToString(), out _));
            Assert.IsNotNull(dict["Status"]);

            bool check = int.TryParse(dict["Status"].ToString(), out int count);
            Assert.IsTrue(check);
            Assert.That(count, Is.EqualTo(1));
        }

        [Test]
        public async Task ChangeProductToSales_HaveABooking()
        {
            var param = new
            {
                Code = "TQSQ0000004",
                IsTest = true
            };
            var result = await _productRepository.spProductChangeToSales(param);

            var dict = result as IDictionary<string, object>;

            Assert.IsNotNull(dict);
            Assert.IsTrue(long.TryParse(dict["Status"].ToString(), out _));
            Assert.IsNotNull(dict["Status"]);

            bool check = int.TryParse(dict["Status"].ToString(), out int count);
            Assert.IsTrue(check);
            Assert.That(count, Is.EqualTo(2));
        }

        [Test]
        public async Task ChangeProductToRent_Success()
        {
            var param = new
            {
                Code = "TQSQC-T0001-0000009",
                IsTest = true
            };
            var result = await _productRepository.spProductChangeToRent(param);

            var dict = result as IDictionary<string, object>;

            Assert.IsNotNull(dict);
            Assert.IsTrue(long.TryParse(dict["Status"].ToString(), out _));
            Assert.IsNotNull(dict["Status"]);

            bool check = int.TryParse(dict["Status"].ToString(), out int count);
            Assert.IsTrue(check);
            Assert.That(count, Is.EqualTo(0));
        }

        [Test]
        public async Task ChangeProductToRent_WrongCode()
        {
            var param = new
            {
                Code = "Nana-T0001-1234544444467",
                IsTest = true
            };
            var result = await _productRepository.spProductChangeToRent(param);

            var dict = result as IDictionary<string, object>;

            Assert.IsNotNull(dict);
            Assert.IsTrue(long.TryParse(dict["Status"].ToString(), out _));
            Assert.IsNotNull(dict["Status"]);

            bool check = int.TryParse(dict["Status"].ToString(), out int count);
            Assert.IsTrue(check);
            Assert.That(count, Is.EqualTo(1));
        }

        [Test]
        public async Task CreateProductTemplate_Success()
        {
            var param = new
            {
                ProductGroupRefId = Guid.Parse("E959D29F-A1D5-4F74-903C-1AA933952D88"),
                ProductName = "Board Game A",
                Image = "image_a.png",
                Description = "Description of Board Game A",
                Publisher = "Publisher A",
                Age = 12,
                NumberOfPlayerMin = 2,
                NumberOfPlayerMax = 6,
                Duration = 60,
                Difficulty = 3,
                ListCategories = "Strategy, Card",
                Price = 500000,
                RentPrice = 200000,
                RentPricePerHour = 50000,
                ManagerID = Guid.NewGuid(),
                IsTest = true
            };

            var result = await _productRepository.spProductTemplateCreate(param);
            var dict = result as IDictionary<string, object>;

            Console.WriteLine(dict["Status"]);
            Assert.IsNotNull(dict);
            Assert.IsTrue(int.TryParse(dict["Status"].ToString(), out var status));
            Assert.That(status, Is.EqualTo(0));
        }

        [Test]
        public async Task CreateProductTemplate_ProductGroupNotFound()
        {
            var param = new
            {
                ProductGroupRefId = Guid.Parse("E959D29F-A1D5-4F74-903C-1AA933952D81"), 
                ProductName = "Board Game B",
                Image = "image_b.png",
                Description = "Description of Board Game B",
                Publisher = "Publisher B",
                Age = 10,
                NumberOfPlayerMin = 2,
                NumberOfPlayerMax = 4,
                Duration = 30,
                Difficulty = 2,
                ListCategories = "Family, Kids",
                Price = 300000,
                RentPrice = 100000,
                RentPricePerHour = 30000,
                ManagerID = Guid.NewGuid(),
                IsTest = true
            };

            var result = await _productRepository.spProductTemplateCreate(param);
            var dict = result as IDictionary<string, object>;

            Console.WriteLine(dict["Status"]);
            Assert.IsNotNull(dict);
            Assert.IsTrue(int.TryParse(dict["Status"].ToString(), out var status));
            Assert.That(status, Is.EqualTo(1));
        }
    }
}
