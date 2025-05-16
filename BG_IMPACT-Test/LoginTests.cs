using BG_IMPACT.Extensions;
using BG_IMPACT.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Client;
using System;

namespace BG_IMPACT_Test
{
    public class LoginTests
    {
        private ServiceProvider _serviceProvider; //Luôn có đi kèm với Setup
        private IAccountRepository _accountRepository; //Phụ thuộc vào API có sử dụng hay không

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
            _accountRepository = _serviceProvider.GetRequiredService<IAccountRepository>();
        }

        [TearDown]
        public void TearDown()
        {
            if (_serviceProvider is IDisposable disposable)
                disposable.Dispose();
        }

        [Test]
        public async Task LoginTest_Successful()
        {
            var param = new
            {
                Username = "staffhang1",
                Password = "staffhang1",
                IsTest = true
            };

            var result = await _accountRepository.spLogin(param);

            var dict = result as IDictionary<string, object>;
            //1 if = 1 assert
            Assert.IsNotNull(dict); // = if (dict != null ...)
            //Assert.IsTrue(Int64.TryParse(dict["Status"].ToString(), out _));
            Assert.IsNotNull(dict["id"]);
            //_ = Int64.TryParse(dict["Status"].ToString(), out long count);

            bool check = Guid.TryParse(dict["id"].ToString(), out _);
            Assert.IsTrue(check);
        }

        [Test]
        public async Task LoginTest_WrongUsernameOrPassword()
        {
            var param = new
            {
                Username = "staffhang1",
                Password = "staffhang2",
                IsTest = true
            };

            var result = await _accountRepository.spLogin(param);

            var dict = result as IDictionary<string, object>;
            Assert.IsNull(dict);
        }
    }
}