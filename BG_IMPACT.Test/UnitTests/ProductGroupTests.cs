namespace BG_IMPACT.Test.UnitTests
{
    public class ProductGroupTests : TestBase
    {
        private IProductGroupRepository _productGroupRepository;

        [SetUp]
        public void Setup()
        {
            _productGroupRepository = _serviceProvider.GetRequiredService<IProductGroupRepository>();
        }

        [Test]
        public async Task Create_Successful()
        {
            var param = new
            {
                GroupName = "BumbleBeeeeeee1e",
                ManagerID = "14ece14e-ace2-416a-92b8-56d92a7abcca",
                IsTest = true
            };

            var result = await _productGroupRepository.spProductGroupCreate(param);

            var dict = result as IDictionary<string, object>;

            Assert.IsNotNull(dict);
            Assert.IsTrue(long.TryParse(dict["Status"].ToString(), out _));
            Assert.IsNotNull(dict["Status"]);

            bool check = int.TryParse(dict["Status"].ToString(), out int count);
            Assert.IsTrue(check);
            Assert.That(count, Is.EqualTo(0));
        }

        [Test]
        public async Task Create_ExistGroupName()
        {
            var param = new
            {
                GroupName = "UITEST 3",
                ManagerID = "14ece14e-ace2-416a-92b8-56d92a7abcca",
                IsTest = true
            };

            var result = await _productGroupRepository.spProductGroupCreate(param);

            var dict = result as IDictionary<string, object>;
            Assert.IsNotNull(dict);
            Assert.IsTrue(long.TryParse(dict["Status"].ToString(), out _));
            Assert.IsNotNull(dict["Status"]);

            bool check = int.TryParse(dict["Status"].ToString(), out int count);
            Assert.IsTrue(check);
            Assert.That(count, Is.EqualTo(1));
        }

        [Test]
        public async Task UpdateProductGroup_Successful()
        {
            var param = new
            {
                ProductGroupID = "62aa0d3b-edfa-414d-99e6-039ceaf62a19",
                GroupName = "UITEST 1",
                IsTest = true
            };

            var result = await _productGroupRepository.spProductGroupUpdate(param);

            var dict = result as IDictionary<string, object>;

            Assert.IsNotNull(dict);
            Assert.IsTrue(long.TryParse(dict["Status"].ToString(), out _));
            Assert.IsNotNull(dict["Status"]);

            bool check = int.TryParse(dict["Status"].ToString(), out int count);
            Assert.IsTrue(check);
            Assert.That(count, Is.EqualTo(1));
        }

        [Test]
        public async Task UpdateProductGroup_NotFoundProductGroup()
        {
            var param = new
            {
                ProductGroupID = "62aa0d3b-edfa-414d-99e6-039ceaf62a11",
                GroupName = "UITEST 111111",
                IsTest = true
            };

            var result = await _productGroupRepository.spProductGroupUpdate(param);

            var dict = result as IDictionary<string, object>;

            Assert.IsNotNull(dict);
            Assert.IsTrue(long.TryParse(dict["Status"].ToString(), out _));
            Assert.IsNotNull(dict["Status"]);

            bool check = int.TryParse(dict["Status"].ToString(), out int count);
            Assert.IsTrue(check);
            Assert.That(count, Is.EqualTo(2));
        }


    }
}