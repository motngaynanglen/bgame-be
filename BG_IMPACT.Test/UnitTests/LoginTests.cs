using BG_IMPACT.DTO.Models.Configs.GlobalSetting;

namespace BG_IMPACT.Test.UnitTests
{
    public class LoginTests : TestBase
    {
        private IAccountRepository _accountRepository; 

        [SetUp]
        public void Setup()
        {
            _accountRepository = _serviceProvider.GetRequiredService<IAccountRepository>();
        }


        [Test]
        public async Task LoginTest_Staff_Successful()
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
        public async Task LoginTest_Manager_Successful()
        {
            var param = new
            {
                Username = "manager1st",
                Password = "manager1st",
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
        public async Task LoginTest_Customer_Successful()
        {
            var param = new
            {
                Username = "customer1st",
                Password = "customer1st",
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
        public async Task LoginTest_Admin_Successful()
        {
            var param = new
            {
                Username = "admin1st",
                Password = "admin1st",
                IsTest = true
            };

            Assert.IsTrue(param.Username == AppGlobals.Username);
            Assert.IsTrue(param.Password == AppGlobals.Password);   
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