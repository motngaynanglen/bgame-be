using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BG_IMPACT.Test.UnitTests
{
    public class AccountTests : TestBase
    {
        private IAccountRepository _accountRepository;

        [SetUp]
        public void Setup()
        {
            _accountRepository = _serviceProvider.GetRequiredService<IAccountRepository>();
        }

        [Test]
        public async Task CreateCustomer_Success()
        {
            var param = new
            {
                username = "testuser_" + Guid.NewGuid().ToString("N").Substring(0, 8),
                password = "123456",
                phone_number = "0901234567",
                email = "test_" + Guid.NewGuid().ToString("N").Substring(0, 5) + "@mail.com",
                role = "CUSTOMER",
                full_name = "Nguyen Van A",
                date_of_birth = new DateTime(1990, 1, 1),
                IsTest = true 
            };

            var result = await _accountRepository.spAccountCreateCustomer(param);

            var dict = result as IDictionary<string, object>;
            Assert.IsNotNull(dict);
            Assert.That(int.Parse(dict["Status"].ToString()), Is.EqualTo(0));
        }

        [Test]
        public async Task CreateCustomer_Fail_UsernameExists()
        {
            var param = new
            {
                username = "customer1st",
                password = "123456",
                phone_number = "0909876543",
                email = "duplicate@mail.com",
                role = "CUSTOMER",
                full_name = "Tran Van B",
                date_of_birth = new DateTime(1995, 5, 5),
                IsTest = true
            };

            var result = await _accountRepository.spAccountCreateCustomer(param);

            var dict = result as IDictionary<string, object>;
            Assert.IsNotNull(dict);
            Assert.That(int.Parse(dict["Status"].ToString()), Is.EqualTo(1));
        }

        [Test]
        public async Task CreateStaff_Success()
        {
            var param = new
            {
                store_id = Guid.Parse("C0D8B9F4-23B0-4845-9E23-22989CD96316"),
                username = "staff_" + Guid.NewGuid().ToString("N").Substring(0, 6),
                password = "123456",
                phone_number = "0901234567",
                email = "staff_" + Guid.NewGuid().ToString("N").Substring(0, 5) + "@mail.com",
                role = "MANAGER",
                full_name = "Nguyen Van Staff",
                date_of_birth = new DateTime(1990, 1, 1),
                IsTest = true
            };

            var result = await _accountRepository.spAccountCreateManager(param);

            var dict = result as IDictionary<string, object>;
            Assert.IsNotNull(dict);
            Assert.That(int.Parse(dict["Status"].ToString()), Is.EqualTo(0));
        }

        [Test]
        public async Task CreateStaff_Fail_UsernameExists()
        {
            var param = new
            {
                store_id = Guid.Parse("C0D8B9F4-23B0-4845-9E23-22989CD96316"),
                username = "staffhang1",
                password = "123456",
                phone_number = "0901234567",
                email = "staff_" + Guid.NewGuid().ToString("N").Substring(0, 5) + "@mail.com",
                role = "MANAGER",
                full_name = "Nguyen Van Staff",
                date_of_birth = new DateTime(1990, 1, 1),
                IsTest = true
            };

            var result = await _accountRepository.spAccountCreateManager(param);

            var dict = result as IDictionary<string, object>;
            Assert.IsNotNull(dict);
            Assert.That(int.Parse(dict["Status"].ToString()), Is.EqualTo(1));
        }

        [Test]
        public async Task CreateStaff_Fail_StoreNotFound()
        {
            var param = new
            {
                store_id = Guid.Parse("C0D8B9F4-23B0-4845-9E23-22989CD96311"),
                username = "staff_" + Guid.NewGuid().ToString("N").Substring(0, 6),
                password = "123456",
                phone_number = "0901234567",
                email = "staff_" + Guid.NewGuid().ToString("N").Substring(0, 5) + "@mail.com",
                role = "MANAGER",
                full_name = "Nguyen Van Staff",
                date_of_birth = new DateTime(1990, 1, 1),
                IsTest = true
            };

            var result = await _accountRepository.spAccountCreateManager(param);

            var dict = result as IDictionary<string, object>;
            Assert.IsNotNull(dict);
            Assert.That(int.Parse(dict["Status"].ToString()), Is.EqualTo(2));
        }

        [Test]
        public async Task UpdateCustomer_Success()
        {
            var param = new
            {
                customer_id = Guid.Parse("1DA1B7F8-D21F-4922-8F4C-048BC3E973E9"),
                full_name = "Tran Van Missing",
                date_of_birth = new DateTime(2000, 1, 1),
                phone_number = "0911222333",
                updated_by = "test_user",
                IsTest = true
            };

            var result = await _accountRepository.spUpdateCustomerProfile(param);

            var dict = result as IDictionary<string, object>;
            Assert.IsNotNull(dict);
            Assert.That(int.Parse(dict["Status"].ToString()), Is.EqualTo(0));
        }

        [Test]
        public async Task UpdateCustomer_Fail_CustomerNotFound()
        {
            var param = new
            {
                customer_id = Guid.Parse("1DA1B7F8-D21F-4922-8F4C-048BC3E973E1"),
                full_name = "Tran Van Missing",
                date_of_birth = new DateTime(2000, 1, 1),
                phone_number = "0911222333",
                updated_by = "test_user",
                IsTest = true
            };

            var result = await _accountRepository.spUpdateCustomerProfile(param);

            var dict = result as IDictionary<string, object>;
            Assert.IsNotNull(dict);
            Assert.That(int.Parse(dict["Status"].ToString()), Is.EqualTo(1));
        }

        [Test]
        public async Task CreateManager_Success()
        {
            var param = new
            {
                store_id = Guid.Parse("C0D8B9F4-23B0-4845-9E23-22989CD96316"), // store phải tồn tại trong tblStore
                username = "manager_user_01",
                password = "123456",
                phone_number = "0901123456",
                email = "staff01@example.com",
                role = "MANAGER",
                full_name = "Staff Manager 01",
                date_of_birth = new DateTime(1990, 5, 15),
                IsTest = true
            };

            var result = await _accountRepository.spAccountCreateManager(param);

            var dict = result as IDictionary<string, object>;
            Assert.IsNotNull(dict);
            Assert.That(int.Parse(dict["Status"].ToString()), Is.EqualTo(0));
        }

        [Test]
        public async Task CreateManager_Fail_UsernameExists()
        {
            var param = new
            {
                store_id = Guid.Parse("C0D8B9F4-23B0-4845-9E23-22989CD96316"), // store phải tồn tại trong tblStore
                username = "manager1st",
                password = "123456",
                phone_number = "0901123456",
                email = "staff01@example.com",
                role = "MANAGER",
                full_name = "Staff Manager 01",
                date_of_birth = new DateTime(1990, 5, 15),
                IsTest = true
            };

            var result = await _accountRepository.spAccountCreateManager(param);

            var dict = result as IDictionary<string, object>;
            Assert.IsNotNull(dict);
            Assert.That(int.Parse(dict["Status"].ToString()), Is.EqualTo(1));
        }

        [Test]
        public async Task CreateManager_Fail_StoreNotFound()
        {
            var param = new
            {
                store_id = Guid.Parse("C0D8B9F4-23B0-4845-9E23-22989CD96311"), // store phải tồn tại trong tblStore
                username = "manager_user_01",
                password = "123456",
                phone_number = "0901123456",
                email = "staff01@example.com",
                role = "MANAGER",
                full_name = "Staff Manager 01",
                date_of_birth = new DateTime(1990, 5, 15),
                IsTest = true
            };

            var result = await _accountRepository.spAccountCreateManager(param);

            var dict = result as IDictionary<string, object>;
            Assert.IsNotNull(dict);
            Assert.That(int.Parse(dict["Status"].ToString()), Is.EqualTo(2));
        }
    }
}
