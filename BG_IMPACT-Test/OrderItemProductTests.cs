using Azure.Core;
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
    class OrderItemProductTests
    {
        private ServiceProvider _serviceProvider; //Luôn có đi kèm với Setup
        private IOrderItemRepository _orderItemRepository;
        [SetUp]
        public void Setup()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            var services = new ServiceCollection();
            services.DependencyInject(configuration);
            _serviceProvider = services.BuildServiceProvider();
            _orderItemRepository = _serviceProvider.GetRequiredService<IOrderItemRepository>();
        }

        [TearDown]
        public void TearDown()
        {
            if (_serviceProvider is IDisposable disposable)
                disposable.Dispose();
        }

        [Test]
        public async Task UpdateOrderItemProduct_Successful()
        {
            var param = new
            {
                StaffId = "a44ed012-283f-48c4-bc44-0142d772409b", // Staff cửa hàng 1
                OrderItemId = "be1cd4ac-4804-4eb9-8272-1ec9792dcbf8", // Đơn hàng tại cửa hàng 1
                ProductCode = "Nana-T0001-0000040", // Code bộ Nana bỏ vào order
                IsTest = true
            };
            var result = await _orderItemRepository.spOrderItemUpdateProduct(param);

            var dict = result as IDictionary<string, object>;

            Assert.IsNotNull(dict);
            Assert.IsTrue(Int64.TryParse(dict["Status"].ToString(), out _));
            Assert.IsNotNull(dict["Status"]);

            bool check = Int32.TryParse(dict["Status"].ToString(), out int count);
            Assert.IsTrue(check);
            Assert.That(count, Is.EqualTo(0));
        }

        [Test]
        public async Task UpdateOrderItemProduct_Wrongg()
        {
            var param = new
            {
                StaffId = "a44ed012-283f-48c4-bc44-0142d772409b",
                OrderItemId = "",
                ProductCode = "",
                IsTest = true
            };
            var result = await _orderItemRepository.spOrderItemUpdateProduct(param);

            var dict = result as IDictionary<string, object>;

            Assert.IsNotNull(dict);
            Assert.IsTrue(Int64.TryParse(dict["Status"].ToString(), out _));
            Assert.IsNotNull(dict["Status"]);

            bool check = Int32.TryParse(dict["Status"].ToString(), out int count);
            Assert.IsTrue(check);
            Assert.That(count, Is.EqualTo(0));
        }

        [Test]
        public async Task UpdateOrderItemProduct_Wrong()
        {
            var param = new
            {
                StaffId = "",
                OrderItemId = "",
                ProductCode = "",
                IsTest = true
            };
            var result = await _orderItemRepository.spOrderItemUpdateProduct(param);

            var dict = result as IDictionary<string, object>;

            Assert.IsNotNull(dict);
            Assert.IsTrue(Int64.TryParse(dict["Status"].ToString(), out _));
            Assert.IsNotNull(dict["Status"]);

            bool check = Int32.TryParse(dict["Status"].ToString(), out int count);
            Assert.IsTrue(check);
            Assert.That(count, Is.EqualTo(1));
        }
    }
}
