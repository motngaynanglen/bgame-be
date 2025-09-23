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
        public async Task CreateTemplate_Successful()
        {
            var param = new
            {
                ProductGroupRefId = "559b3fcf-d970-417c-a22c-42115ec91129",
                ProductName = "Tam quốc sát 2027",
                Image = "https://res.cloudinary.com/dh8gc9kkz/image/upload/v1743335124/BGIMPACT/uynelzpje4tmj8mlh2kn.jpg",
                Price = "100000",
                RentPrice = "50000",
                RentPricePerHour = "10000",
                HardRank = "0",
                Age = "14",
                NumberOfPlayerMin = "3",
                NumberOfPlayerMax = "4",
                Description = "Tam Quốc Sát",
                ManagerID = "14ece14e-ace2-416a-92b8-56d92a7abcca",
                IsTest = true
            };
            var result = await _productRepository.spProductCreateTemplate(param);

            var dict = result as IDictionary<string, object>;

            Assert.IsNotNull(dict);
            Assert.IsTrue(long.TryParse(dict["Status"].ToString(), out _));
            Assert.IsNotNull(dict["Status"]);

            bool check = int.TryParse(dict["Status"].ToString(), out int count);
            Assert.IsTrue(check);
            Assert.That(count, Is.EqualTo(0));
        }

        [Test]
        public async Task CreateTemplate_NotExistProductGroupRef()
        {
            var param = new
            {
                ProductGroupRefId = "559b3fcf-d970-417c-a22c-42115ec91122",
                ProductName = "Tam quốc sát 2025",
                Image = "https://res.cloudinary.com/dh8gc9kkz/image/upload/v1743335124/BGIMPACT/uynelzpje4tmj8mlh2kn.jpg",
                Price = "100000",
                RentPrice = "50000",
                RentPricePerHour = "10000",
                HardRank = "0",
                Age = "14",
                NumberOfPlayerMin = "3",
                NumberOfPlayerMax = "4",
                Description = "Tam Quốc Sát",
                ManagerID = "14ece14e-ace2-416a-92b8-56d92a7abcca",
                IsTest = true
            };
            var result = await _productRepository.spProductCreateTemplate(param);

            var dict = result as IDictionary<string, object>;

            Assert.IsNotNull(dict);
            Assert.IsTrue(long.TryParse(dict["Status"].ToString(), out _));
            Assert.IsNotNull(dict["Status"]);

            bool check = int.TryParse(dict["Status"].ToString(), out int count);
            Assert.IsTrue(check);
            Assert.That(count, Is.EqualTo(1));
        }

        [Test]
        public async Task CreateProduct_Successful()
        {
            var param = new
            {
                ProductTemplateId = "08c3b7d0-88f8-4901-9719-90eceb04190b",
                ManagerID = "14ece14e-ace2-416a-92b8-56d92a7abcca",
                Number = "10",
                IsTest = true
            };
            var result = await _productRepository.spProductCreate(param);

            var dict = result as IDictionary<string, object>;

            Assert.IsNotNull(dict);
            Assert.IsTrue(long.TryParse(dict["Status"].ToString(), out _));
            Assert.IsNotNull(dict["Status"]);

            bool check = int.TryParse(dict["Status"].ToString(), out int count);
            Assert.IsTrue(check);
            Assert.That(count, Is.EqualTo(0));
        }

        [Test]
        public async Task CreateProduct_NotExistProductTemplate()
        {
            var param = new
            {
                ProductTemplateId = "08c3b7d0-88f8-4901-9719-90eceb041902",
                ManagerID = "14ece14e-ace2-416a-92b8-56d92a7abcca",
                Number = "10",
                IsTest = true
            };
            var result = await _productRepository.spProductCreate(param);

            var dict = result as IDictionary<string, object>;

            Assert.IsNotNull(dict);
            Assert.IsTrue(long.TryParse(dict["Status"].ToString(), out _));
            Assert.IsNotNull(dict["Status"]);

            bool check = int.TryParse(dict["Status"].ToString(), out int count);
            Assert.IsTrue(check);
            Assert.That(count, Is.EqualTo(1));
        }


        [Test]
        public async Task CreateProduct_NotExisTemplatetInStore()
        {
            var param = new
            {
                ProductTemplateId = "08c3b7d0-88f8-4901-9719-90eceb04190b",
                ManagerID = "14ece14e-ace2-416a-92b8-56d92a7abcaca",
                Number = "10",
                IsTest = true
            };
            var result = await _productRepository.spProductCreate(param);

            var dict = result as IDictionary<string, object>;

            Assert.IsNotNull(dict);
            Assert.IsTrue(long.TryParse(dict["Status"].ToString(), out _));
            Assert.IsNotNull(dict["Status"]);

            bool check = int.TryParse(dict["Status"].ToString(), out int count);
            Assert.IsTrue(check);
            Assert.That(count, Is.EqualTo(2));
        }

        [Test]
        public async Task CreateProduct_NotZeroNumber()
        {
            var param = new
            {
                ProductTemplateId = "08c3b7d0-88f8-4901-9719-90eceb04190b",
                ManagerID = "14ece14e-ace2-416a-92b8-56d92a7abcca",
                Number = "0",
                IsTest = true
            };
            var result = await _productRepository.spProductCreate(param);

            var dict = result as IDictionary<string, object>;

            Assert.IsNotNull(dict);
            Assert.IsTrue(long.TryParse(dict["Status"].ToString(), out _));
            Assert.IsNotNull(dict["Status"]);

            bool check = int.TryParse(dict["Status"].ToString(), out int count);
            Assert.IsTrue(check);
            Assert.That(count, Is.EqualTo(3));
        }

        [Test]
        public async Task CreateProduct_NotExistManager()
        {
            var param = new
            {
                ProductTemplateId = "08c3b7d0-88f8-4901-9719-90eceb04190b",
                ManagerID = "124ece14e-ace2-416a-92b8-56d92a7abccaa",
                Number = "0",
                IsTest = true
            };
            var result = await _productRepository.spProductCreate(param);

            var dict = result as IDictionary<string, object>;

            Assert.IsNotNull(dict);
            Assert.IsTrue(long.TryParse(dict["Status"].ToString(), out _));
            Assert.IsNotNull(dict["Status"]);

            bool check = int.TryParse(dict["Status"].ToString(), out int count);
            Assert.IsTrue(check);
            Assert.That(count, Is.EqualTo(4));
        }

        [Test]
        public async Task CreateProductUnknow_Successful()
        {
            var param = new
            {
                GroupName = "Bài Mèo NềE",
                Prefix = "CAT",
                GroupRefName = "Mèo NềE",
                ProductName = "Mèo NềE2025",
                Image = "",
                Price = "100000",
                Description = "Đây là bềEbài mèo nềEdềEchơi, thân thiện với các bạn trẻ",
                RentPrice = "50000",
                RentPricePerHour = "20000",
                IsTest = true
            };
            var result = await _productRepository.spProductCreateUnknown(param);

            var dict = result as IDictionary<string, object>;

            Assert.IsNotNull(dict);
            Assert.IsTrue(long.TryParse(dict["Status"].ToString(), out _));
            Assert.IsNotNull(dict["Status"]);

            bool check = int.TryParse(dict["Status"].ToString(), out int count);
            Assert.IsTrue(check);
            Assert.That(count, Is.EqualTo(0));
        }

        [Test]
        public async Task CreateProductUnknow_ExistProductGroupName()
        {
            var param = new
            {
                GroupName = "Bang!!!",
                Prefix = "BANGG",
                GroupRefName = "BANG BANG",
                ProductName = "BANG BANG 2025",
                Image = "",
                Price = "100000",
                Description = "Đây là bềEBang, thân thiện với các bạn trẻ",
                RentPrice = "50000",
                RentPricePerHour = "20000",
                IsTest = true
            };
            var result = await _productRepository.spProductCreateUnknown(param);

            var dict = result as IDictionary<string, object>;

            Assert.IsNotNull(dict);
            Assert.IsTrue(long.TryParse(dict["Status"].ToString(), out _));
            Assert.IsNotNull(dict["Status"]);

            bool check = int.TryParse(dict["Status"].ToString(), out int count);
            Assert.IsTrue(check);
            Assert.That(count, Is.EqualTo(1));
        }

        [Test]
        public async Task CreateProductUnknow_ExistProductGroupRefName()
        {
            var param = new
            {
                GroupName = "Splendor 2025",
                Prefix = "SPLER",
                GroupRefName = "Splendor",
                ProductName = "Splendor 2025",
                Image = "",
                Price = "100000",
                Description = "Đây là bềESplendor, thân thiện với các bạn trẻ",
                RentPrice = "50000",
                RentPricePerHour = "20000",
                IsTest = true
            };
            var result = await _productRepository.spProductCreateUnknown(param);

            var dict = result as IDictionary<string, object>;

            Assert.IsNotNull(dict);
            Assert.IsTrue(long.TryParse(dict["Status"].ToString(), out _));
            Assert.IsNotNull(dict["Status"]);

            bool check = int.TryParse(dict["Status"].ToString(), out int count);
            Assert.IsTrue(check);
            Assert.That(count, Is.EqualTo(2));
        }

        [Test]
        public async Task CreateProductUnknow_ExistProductName()
        {
            var param = new
            {
                GroupName = "Tam Quốc Sát Sinh Tử",
                Prefix = "TQSTTTT",
                GroupRefName = "TamQuocSat",
                ProductName = "Tam quốc sát 2025",
                Image = "",
                Price = "200000",
                Description = "Đây là bềETam Quốc Sát, thân thiện với các thanh thiếu niên",
                RentPrice = "150000",
                RentPricePerHour = "120000",
                IsTest = true
            };
            var result = await _productRepository.spProductCreateUnknown(param);

            var dict = result as IDictionary<string, object>;

            Assert.IsNotNull(dict);
            Assert.IsTrue(long.TryParse(dict["Status"].ToString(), out _));
            Assert.IsNotNull(dict["Status"]);

            bool check = int.TryParse(dict["Status"].ToString(), out int count);
            Assert.IsTrue(check);
            Assert.That(count, Is.EqualTo(3));
        }

        [Test]
        public async Task CreateProductUnknow_NotValueRentPrice()
        {
            var param = new
            {
                GroupName = "Bài Mèo Nổ",
                Prefix = "CAT",
                GroupRefName = "Bài Mèo Nổ",
                ProductName = "Mèo Nổ 2025",
                Image = "",
                Price = "100000",
                Description = "Đây là bài mèo nổ dễ chơi, thân thiện với các bạn trẻ",
                RentPrice = "0",
                RentPricePerHour = "20000",
                IsTest = true
            };
            var result = await _productRepository.spProductCreateUnknown(param);

            var dict = result as IDictionary<string, object>;

            Assert.IsNotNull(dict);
            Assert.IsTrue(long.TryParse(dict["Status"].ToString(), out _));
            Assert.IsNotNull(dict["Status"]);

            bool check = int.TryParse(dict["Status"].ToString(), out int count);
            Assert.IsTrue(check);
            Assert.That(count, Is.EqualTo(4));
        }

        [Test]
        public async Task CreateProductUnknow_PrefixIsExist()
        {
            var param = new
            {
                GroupName = "Bài Splendor",
                Prefix = "Splendor",
                GroupRefName = "Bài Splendor",
                ProductName = "Bài Splendor 2025",
                Image = "",
                Price = "123000",
                Description = "Đây là bài slendor với lối chơi thân thiện với các bạn trẻ",
                RentPrice = "15000",
                RentPricePerHour = "20000",
                IsTest = true
            };
            var result = await _productRepository.spProductCreateUnknown(param);

            var dict = result as IDictionary<string, object>;

            Assert.IsNotNull(dict);
            Assert.IsTrue(long.TryParse(dict["Status"].ToString(), out _));
            Assert.IsNotNull(dict["Status"]);

            bool check = int.TryParse(dict["Status"].ToString(), out int count);
            Assert.IsTrue(check);
            Assert.That(count, Is.EqualTo(5));
        }

        [Test]
        public async Task ChangeProductToSales_Success()
        {
            var param = new
            {
                Code = "Nana-T0001-0000003",
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
                Code = "CCN-T0001-0000002",
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
                Code = "Nana-T0001-0000025",
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
        public async Task UpdateProduct_Success()
        {
            var param = new
            {
                ProductID = "529E76BD-3FFE-4B5D-8727-283620453E30",
                ProductName = "Nana",
                Image = "Nana",
                Price = 1,
                RentPrice = 1,
                RentPricePerHour = 1,
                Publisher = "Nana",
                Age = 1,
                NumberOfPlayerMin = 1,
                NumberOfPlayerMax = 1,
                Difficulty = 1,
                Description = "Nana",
                UserID = "14ece14e-ace2-416a-92b8-56d92a7abcca",
                IsTest = true
            };
            var result = await _productRepository.spProductUpdate(param);

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
