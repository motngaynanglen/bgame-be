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
        private ServiceProvider _serviceProvider; 
        private IProductGroupRepository _productGroupRepository; 

        [SetUp]
        public void Setup()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            var services = new ServiceCollection();
            services.DependencyInject(configuration);
            _serviceProvider = services.BuildServiceProvider();

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
                GroupName = "BumbleBeeeeeee1e",
                ManagerID = "14ece14e-ace2-416a-92b8-56d92a7abcca",
                IsTest = true
            };

            var result = await _productGroupRepository.spProductGroupCreate(param);

            var dict = result as IDictionary<string, object>;
           
            Assert.IsNotNull(dict); 
            Assert.IsTrue(Int64.TryParse(dict["Status"].ToString(), out _));
            Assert.IsNotNull(dict["Status"]);

            bool check = Int32.TryParse(dict["Status"].ToString(), out int count);
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
            Assert.IsTrue(Int64.TryParse(dict["Status"].ToString(), out _));
            Assert.IsNotNull(dict["Status"]);

            bool check = Int32.TryParse(dict["Status"].ToString(), out int count);
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
            Assert.IsTrue(Int64.TryParse(dict["Status"].ToString(), out _));
            Assert.IsNotNull(dict["Status"]);

            bool check = Int32.TryParse(dict["Status"].ToString(), out int count);
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
            Assert.IsTrue(Int64.TryParse(dict["Status"].ToString(), out _));
            Assert.IsNotNull(dict["Status"]);

            bool check = Int32.TryParse(dict["Status"].ToString(), out int count);
            Assert.IsTrue(check);
            Assert.That(count, Is.EqualTo(2));
        }


    }
}