using Azure.Core;
using BG_IMPACT.Command.Store.Commands;
using BG_IMPACT.Extensions;
using BG_IMPACT.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BG_IMPACT.Command.Store.Commands.CreateStoreCommand;

namespace BG_IMPACT_Test
{
    public class StoreTests
    {
        private ServiceProvider _serviceProvider; //Luôn có đi kèm với Setup
        private IStoreRepository _storeRepository;
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
            _storeRepository = _serviceProvider.GetRequiredService<IStoreRepository>();
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
            // Arrange
            var request = new CreateStoreCommand
            {
                StoreName = "Cửa hàng Boardgame HĐ mới 2025",
                Address = "333 Đinh Tiên Hoàng, phường 26, quận Bình Thạnh",
                Hotline = "0123456789",
                Longtitude = "10.123456",
                Lattitude = "106.123456",
                Email = "bgvnsshcm@gmail.com"
            };

            var handler = new CreateStoreCommandHandler(_storeRepository);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("200", result.StatusCode);
            Assert.IsTrue(result.Message.Contains("thành công"));
        }

    }
}