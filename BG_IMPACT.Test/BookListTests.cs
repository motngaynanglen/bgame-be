using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BG_IMPACT_Test
{
    public class BookListTests
    {
        private ServiceProvider _serviceProvider; //Luôn có đi kèm với Setup
        private IBookListRepository _booklistRepository;
        [SetUp]
        public void Setup()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            var services = new ServiceCollection();
            services.DependencyInject(configuration);
            _serviceProvider = services.BuildServiceProvider();
            _booklistRepository = _serviceProvider.GetRequiredService<IBookListRepository>();
        }

        [TearDown]
        public void TearDown()
        {
            if (_serviceProvider is IDisposable disposable)
                disposable.Dispose();
        }

        //[Test]
        //public async Task CreateTemplate_Successful()
        //{
        //    var param = new
        //    {
        //        ProductGroupRefId = "559b3fcf-d970-417c-a22c-42115ec91129",
        //        ProductName = "Tam quốc sát 2027",
        //        Image = "https://res.cloudinary.com/dh8gc9kkz/image/upload/v1743335124/BGIMPACT/uynelzpje4tmj8mlh2kn.jpg",
        //        Price = "100000",
        //        RentPrice = "50000",
        //        RentPricePerHour = "10000",
        //        HardRank = "0",
        //        Age = "14",
        //        NumberOfPlayerMin = "3",
        //        NumberOfPlayerMax = "4",
        //        Description = "Tam Quốc Sát",
        //        ManagerID = "14ece14e-ace2-416a-92b8-56d92a7abcca",
        //        IsTest = true
        //    };
        //    var result = await _booklistRepository.spProductCreateTemplate(param);

        //    var dict = result as IDictionary<string, object>;

        //    Assert.IsNotNull(dict);
        //    Assert.IsTrue(Int64.TryParse(dict["Status"].ToString(), out _));
        //    Assert.IsNotNull(dict["Status"]);

        //    bool check = Int32.TryParse(dict["Status"].ToString(), out int count);
        //    Assert.IsTrue(check);
        //    Assert.That(count, Is.EqualTo(0));
        //}
    }
}
