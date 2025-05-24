using BG_IMPACT.Command.Product.Commands;
using BG_IMPACT.Command.Store.Commands;
using BG_IMPACT.Extensions;
using BG_IMPACT.Repositories.Implementations;
using BG_IMPACT.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BG_IMPACT.Command.Store.Commands.CreateStoreCommand;
using static System.Net.Mime.MediaTypeNames;

namespace BG_IMPACT_Test
{
    public class ProductTests
    {
        private ServiceProvider _serviceProvider; //Luôn có đi kèm với Setup
        private IProductRepository _productRepository;
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
            _productRepository = _serviceProvider.GetRequiredService<IProductRepository>();
        }

        [TearDown]
        public void TearDown()
        {
            if (_serviceProvider is IDisposable disposable)
                disposable.Dispose();
        }

        [Test]
        public async Task CreateTemplate_Successful()
        {
            // Arrange
            var param = new 
            {
                  ProductGroupRefId = "",
                  ProductName = "Tam quốc sát 2025",
                  Image = "https://res.cloudinary.com/dh8gc9kkz/image/upload/v1743335124/BGIMPACT/uynelzpje4tmj8mlh2kn.jpg",
                  Price = "100000",
                  RentPrice = "50000",
                  RentPricePerHour = "10000",
                  HardRank = "",
                  Age = "14",
                  NumberOfPlayerMin = "3",
                  NumberOfPlayerMax = "4",
                  Description = "Tam Quốc Sát",
                  ManagerID = "14ece14e-ace2-416a-92b8-56d92a7abcca",
                  IsTest = true
            };
            var result = await _productRepository.spProductCreateTemplate(param);

            var dict = result as IDictionary<string, object>;

            Assert.IsNotNull(dict); // = if (dict != null ...)
            Assert.IsTrue(Int64.TryParse(dict["Status"].ToString(), out _));
            Assert.IsNotNull(dict["Status"]);
            //_ = Int64.TryParse(dict["Status"].ToString(), out long count);

            bool check = Int64.TryParse(dict["Status"].ToString(), out _);
            Assert.IsTrue(check);
        }
    }
}
