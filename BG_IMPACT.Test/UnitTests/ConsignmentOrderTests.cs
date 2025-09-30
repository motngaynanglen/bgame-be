using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BG_IMPACT.Test.UnitTests
{
    public class ConsignmentOrderTests : TestBase
    {
        private IConsignmentOrderRepository _consignmentOrderRepository;

        [SetUp]
        public void Setup()
        {
            _consignmentOrderRepository = _serviceProvider.GetRequiredService<IConsignmentOrderRepository>();
        }

        [Test]
        public async Task CreateConsignmentOrderByStaff_Success()
        {
            var param = new
            {
                CustomerID = (Guid?)null,
                StaffID = Guid.Parse("B69B4F23-ED62-48C9-AE32-10E7C8259CF4"),
                CustomerName = "Nguyen Van A",
                CustomerPhone = "0909123456",
                ProductName = "Sản phẩm thử",
                Email = "test@example.com",
                Description = "Mô tả đơn hàng thử",
                Condition = 1,
                Missing = "Không có",
                ExpectedPrice = 1000.0,
                SalePrice = 900.0,
                Images = "",
                IsTest = true
            };

            var result = await _consignmentOrderRepository.spConsignmentOrderCreateByStaff(param);
            var dict = result as IDictionary<string, object>;

            Assert.That(dict, Is.Not.Null);
            Assert.That(int.Parse(dict["Status"].ToString()), Is.EqualTo(0));
            Assert.That(dict.ContainsKey("ConsignmentOrderID"));
        }

        [Test]
        public async Task CreateConsignmentOrderByStaff_Fail_StaffNotExist()
        {
            var param = new
            {
                CustomerID = (Guid?)null,
                StaffID = Guid.Parse("B69B4F23-ED62-48C9-AE32-10E7C8259CF1"),
                CustomerName = "Nguyen Van A",
                CustomerPhone = "0909123456",
                ProductName = "Sản phẩm thử",
                Email = "test@example.com",
                Description = "Mô tả đơn hàng thử",
                Condition = 1,
                Missing = "Không có",
                ExpectedPrice = 1000.0,
                SalePrice = 900.0,
                Images = "",
                IsTest = true
            };

            var result = await _consignmentOrderRepository.spConsignmentOrderCreateByStaff(param);
            var dict = result as IDictionary<string, object>;

            Assert.That(dict, Is.Not.Null);
            Assert.That(int.Parse(dict["Status"].ToString()), Is.EqualTo(2));
        }

        [Test]
        public async Task CreateConsignmentOrderByStaff_Fail_InvalidExpectedPrice()
        {
            var param = new
            {
                CustomerID = (Guid?)null,
                StaffID = Guid.Parse("B69B4F23-ED62-48C9-AE32-10E7C8259CF4"),
                CustomerName = "Nguyen Van A",
                CustomerPhone = "0909123456",
                ProductName = "Sản phẩm thử",
                Email = "test@example.com",
                Description = "Mô tả đơn hàng thử",
                Condition = 1,
                Missing = "Không có",
                ExpectedPrice = -1000.0,
                SalePrice = 900.0,
                Images = "",
                IsTest = true
            };

            var result = await _consignmentOrderRepository.spConsignmentOrderCreateByStaff(param);
            var dict = result as IDictionary<string, object>;

            Assert.That(dict, Is.Not.Null);
            Assert.That(int.Parse(dict["Status"].ToString()), Is.EqualTo(1)); 
        }

        [Test]
        public async Task CreateConsignmentOrderByStaff_Fail_InvalidSalesPrice()
        {
            var param = new
            {
                CustomerID = (Guid?)null,
                StaffID = Guid.Parse("B69B4F23-ED62-48C9-AE32-10E7C8259CF4"),
                CustomerName = "Nguyen Van A",
                CustomerPhone = "0909123456",
                ProductName = "Sản phẩm thử",
                Email = "test@example.com",
                Description = "Mô tả đơn hàng thử",
                Condition = 1,
                Missing = "Không có",
                ExpectedPrice = 1000.0,
                SalePrice = -900.0,
                Images = "",
                IsTest = true
            };

            var result = await _consignmentOrderRepository.spConsignmentOrderCreateByStaff(param);
            var dict = result as IDictionary<string, object>;

            Assert.That(dict, Is.Not.Null);
            Assert.That(int.Parse(dict["Status"].ToString()), Is.EqualTo(3));
        }

        [Test]
        public async Task UpdateConsignmentOrderByStaff_Success()
        {
            var param = new
            {
                ConsignmentOrderID = Guid.Parse("71F1DD08-7E0B-4ADC-86FE-0FEE6D6BB5B6"),
                CustomerID = (Guid?)null,
                StaffID = Guid.Parse("4026C681-6EF7-46D1-94D0-27AEA39D00EB"),
                CustomerName = "Nguyen Van B",
                CustomerPhone = "0912345678",
                ProductName = "Sản phẩm mới",
                Description = "Mô tả cập nhật",
                Condition = 1,
                Missing = "Không có",
                ExpectedPrice = 1200.0,
                SalePrice = 1100.0,
                Images = "",
                IsTest = true
            };

            var result = await _consignmentOrderRepository.spConsignmentOrderUpdateByStaff(param);
            var dict = result as IDictionary<string, object>;

            Assert.That(dict, Is.Not.Null);
            Assert.That(int.Parse(dict["Status"].ToString()), Is.EqualTo(0));
        }

        [Test]
        public async Task UpdateConsignmentOrderByStaff_Fail_InvalidExpectedPrice()
        {
            var param = new
            {
                ConsignmentOrderID = Guid.Parse("71F1DD08-7E0B-4ADC-86FE-0FEE6D6BB5B6"),
                CustomerID = (Guid?)null,
                StaffID = Guid.Parse("4026C681-6EF7-46D1-94D0-27AEA39D00EB"),
                CustomerName = "Nguyen Van B",
                CustomerPhone = "0912345678",
                ProductName = "Sản phẩm mới",
                Description = "Mô tả cập nhật",
                Condition = 1,
                Missing = "Không có",
                ExpectedPrice = -1200.0,
                SalePrice = 1100.0,
                Images = "",
                IsTest = true
            };

            var result = await _consignmentOrderRepository.spConsignmentOrderUpdateByStaff(param);
            var dict = result as IDictionary<string, object>;

            Assert.That(dict, Is.Not.Null);
            Assert.That(int.Parse(dict["Status"].ToString()), Is.EqualTo(1));
        }

        [Test]
        public async Task UpdateConsignmentOrderByStaff_Fail_StaffNotExist()
        {
            var param = new
            {
                ConsignmentOrderID = Guid.Parse("71F1DD08-7E0B-4ADC-86FE-0FEE6D6BB5B6"),
                CustomerID = (Guid?)null,
                StaffID = Guid.Parse("4026C681-6EF7-46D1-94D0-27AEA39D00E1"),
                CustomerName = "Nguyen Van B",
                CustomerPhone = "0912345678",
                ProductName = "Sản phẩm mới",
                Description = "Mô tả cập nhật",
                Condition = 1,
                Missing = "Không có",
                ExpectedPrice = 1200.0,
                SalePrice = 1100.0,
                Images = "",
                IsTest = true
            };

            var result = await _consignmentOrderRepository.spConsignmentOrderUpdateByStaff(param);
            var dict = result as IDictionary<string, object>;

            Assert.That(dict, Is.Not.Null);
            Assert.That(int.Parse(dict["Status"].ToString()), Is.EqualTo(2));
        }

        [Test]
        public async Task UpdateConsignmentOrderByStaff_Fail_InvalidSalePrice()
        {
            var param = new
            {
                ConsignmentOrderID = Guid.Parse("71F1DD08-7E0B-4ADC-86FE-0FEE6D6BB5B6"),
                CustomerID = (Guid?)null,
                StaffID = Guid.Parse("4026C681-6EF7-46D1-94D0-27AEA39D00EB"),
                CustomerName = "Nguyen Van B",
                CustomerPhone = "0912345678",
                ProductName = "Sản phẩm mới",
                Description = "Mô tả cập nhật",
                Condition = 1,
                Missing = "Không có",
                ExpectedPrice = 1200.0,
                SalePrice = -1100.0,
                Images = "",
                IsTest = true
            };

            var result = await _consignmentOrderRepository.spConsignmentOrderUpdateByStaff(param);
            var dict = result as IDictionary<string, object>;

            Assert.That(dict, Is.Not.Null);
            Assert.That(int.Parse(dict["Status"].ToString()), Is.EqualTo(3));
        }

        [Test]
        public async Task UpdateConsignmentOrderByStaff_Fail_StaffNotSameStore()
        {
            var param = new
            {
                ConsignmentOrderID = Guid.Parse("71F1DD08-7E0B-4ADC-86FE-0FEE6D6BB5B6"),
                CustomerID = (Guid?)null,
                StaffID = Guid.Parse("A7465ED2-6153-4548-BBFA-1CEC362BE986"),
                CustomerName = "Nguyen Van B",
                CustomerPhone = "0912345678",
                ProductName = "Sản phẩm mới",
                Description = "Mô tả cập nhật",
                Condition = 1,
                Missing = "Không có",
                ExpectedPrice = 1200.0,
                SalePrice = 1100.0,
                Images = "",
                IsTest = true
            };

            var result = await _consignmentOrderRepository.spConsignmentOrderUpdateByStaff(param);
            var dict = result as IDictionary<string, object>;

            Assert.That(dict, Is.Not.Null);
            Assert.That(int.Parse(dict["Status"].ToString()), Is.EqualTo(4));
        }

        [Test]
        public async Task CancelConsignmentOrderByStaff_Success()
        {
            var param = new
            {
                StaffID = Guid.Parse("4026C681-6EF7-46D1-94D0-27AEA39D00EB"),
                ConsignmentOrderID = Guid.Parse("71F1DD08-7E0B-4ADC-86FE-0FEE6D6BB5B6"),
                IsTest = true
            };

            var result = await _consignmentOrderRepository.spConsignmentOrderCancelByStaff(param);
            var dict = result as IDictionary<string, object>;

            Assert.That(dict, Is.Not.Null);
            Assert.That(int.Parse(dict["Status"].ToString()), Is.EqualTo(0));
            Assert.That(dict["Message"].ToString(), Is.EqualTo("Hủy đơn hàng thành công"));
        }

        [Test]
        public async Task CancelConsignmentOrderByStaff_Fail_OrderNotExist()
        {
            var param = new
            {
                StaffID = Guid.Parse("4026C681-6EF7-46D1-94D0-27AEA39D00EB"),
                ConsignmentOrderID = Guid.Parse("71F1DD08-7E0B-4ADC-86FE-0FEE6D6BB5B1"),
                IsTest = true
            };

            var result = await _consignmentOrderRepository.spConsignmentOrderCancelByStaff(param);
            var dict = result as IDictionary<string, object>;

            Assert.That(dict, Is.Not.Null);
            Assert.That(int.Parse(dict["Status"].ToString()), Is.EqualTo(1));
            Assert.That(dict["Message"].ToString(), Is.EqualTo("Không tìm thấy đơn hàng"));
        }

        [Test]
        public async Task CancelConsignmentOrderByStaff_Fail_StaffNotExist()
        {
            var param = new
            {
                StaffID = Guid.Parse("4026C681-6EF7-46D1-94D0-27AEA39D00E1"),
                ConsignmentOrderID = Guid.Parse("71F1DD08-7E0B-4ADC-86FE-0FEE6D6BB5B6"),
                IsTest = true
            };

            var result = await _consignmentOrderRepository.spConsignmentOrderCancelByStaff(param);
            var dict = result as IDictionary<string, object>;

            Assert.That(dict, Is.Not.Null);
            Assert.That(int.Parse(dict["Status"].ToString()), Is.EqualTo(2));
            Assert.That(dict["Message"].ToString(), Is.EqualTo("Không tìm thấy nhân viên hoặc nhân viên không thuộc cửa hàng nào"));
        }

        [Test]
        public async Task CancelConsignmentOrderByStaff_Fail_StaffDifferentStore()
        {
            var param = new
            {
                StaffID = Guid.Parse("A7465ED2-6153-4548-BBFA-1CEC362BE986"),
                ConsignmentOrderID = Guid.Parse("71F1DD08-7E0B-4ADC-86FE-0FEE6D6BB5B6"),
                IsTest = true
            };

            var result = await _consignmentOrderRepository.spConsignmentOrderCancelByStaff(param);
            var dict = result as IDictionary<string, object>;

            Assert.That(dict, Is.Not.Null);
            Assert.That(int.Parse(dict["Status"].ToString()), Is.EqualTo(2));
            Assert.That(dict["Message"].ToString(), Is.EqualTo("Bạn không có quyền cập nhật đơn hàng này"));
        }

    }
}
