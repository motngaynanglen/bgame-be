using BG_IMPACT.Extensions;
using BG_IMPACT.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BG_IMPACT_Test
{
    public class ProductGroupTests
    {
        private ServiceProvider _serviceProvider; //Luôn có đi kèm với Setup
        private IProductGroupRepository _productGroupRepository; //Phụ thuộc vào API có sử dụng hay không

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
            _productGroupRepository = _serviceProvider.GetRequiredService<IProductGroupRepository>();
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
                GroupName = "BumbleBee",
                ManagerID = "14ece14e-ace2-416a-92b8-56d92a7abcca",
                IsTest = true
            };

            var result = await _productGroupRepository.spProductGroupCreate(param);

            var dict = result as IDictionary<string, object>;
            //1 if = 1 assert
            Assert.IsNotNull(dict); // = if (dict != null ...)
            Assert.IsTrue(Int64.TryParse(dict["Status"].ToString(), out _));
            Assert.IsNotNull(dict["Status"]);
            //_ = Int64.TryParse(dict["Status"].ToString(), out long count);

            bool check = Int64.TryParse(dict["Status"].ToString(), out _);
            Assert.IsTrue(check);
        }

        [Test]
        public async Task Create_ExistGroupName()
        {
            var param = new
            {
                GroupName = "UITEST 2",
                ManagerID = "14ece14e-ace2-416a-92b8-56d92a7abcca",
                IsTest = true
            };

            var result = await _productGroupRepository.spProductGroupCreate(param);

            var dict = result as IDictionary<string, object>;
            //1 if = 1 assert
            Assert.IsNotNull(dict); // = if (dict != null ...)
            Assert.IsTrue(Int64.TryParse(dict["Status"].ToString(), out _));
            Assert.IsNotNull(dict["Status"]);
            //_ = Int64.TryParse(dict["Status"].ToString(), out long count);

            bool check = Int64.TryParse(dict["Status"].ToString(), out _);
            Assert.IsTrue(check);
        }

        /*[Test]
        public async Task LoginTest_WrongUsernameOrPassword()
        {
            var param = new
            {
                Username = "staffhang1",
                Password = "staffhang2",
                IsTest = true
            };

            var result = await _productGroupRepository.spLogin(param);

            var dict = result as IDictionary<string, object>;
            Assert.IsNull(dict);
        }*/
    }
}