namespace BG_IMPACT.Test.UnitTests
{
    public class BookListTests : TestBase
    {
        private IBookListRepository _bookListRepository;
        private IBookItemRepository _bookItemRepository;
        [SetUp]
        public void Setup()
        {
            _bookListRepository = _serviceProvider.GetRequiredService<IBookListRepository>();
            _bookItemRepository = _serviceProvider.GetRequiredService<IBookItemRepository>();

        }


        [Test]
        public async Task CreateBookListByCustomer_CustomerNotFound()
        {
            var param = new
            {
                CustomerID = Guid.Parse("1DA1B7F8-D21F-4922-8F4C-048BC3E973E1"), 
                FromSlot = 1,
                ToSlot = 2,
                BookDate = DateTimeOffset.UtcNow,
                StoreID = Guid.Parse("C0D8B9F4-23B0-4845-9E23-22989CD96316"),
                TableIDListString = "715B3B4A-0689-49CC-A40B-95E6304ADEC6",
                ProductTemplateIDListString = "9640EE63-76E0-424C-B9BA-53A456A3DD2F",
                IsTest = true
            };

            var result = await _bookListRepository.spBookListCreateByCustomer(param);
            var dict = result as IDictionary<string, object>;

            Console.WriteLine(dict?["Message"] ?? string.Empty);

            Assert.IsNotNull(dict);
            Assert.IsTrue(int.TryParse(dict["Status"].ToString(), out var status));
            Assert.That(status, Is.EqualTo(1));
        }

        [Test]
        public async Task CreateBookListByCustomer_NotEnoughInventory()
        {
            var param = new
            {
                CustomerID = Guid.Parse("1DA1B7F8-D21F-4922-8F4C-048BC3E973E9"),
                FromSlot = 1,
                ToSlot = 2,
                BookDate = DateTimeOffset.UtcNow,
                StoreID = Guid.Parse("C0D8B9F4-23B0-4845-9E23-22989CD96316"),
                TableIDListString = "715B3B4A-0689-49CC-A40B-95E6304ADEC6",
                ProductTemplateIDListString = "9640EE63-76E0-424C-B9BA-53A456A3DD2F",
                IsTest = true,
                IsInventoryFull = true
            };

            var result = await _bookListRepository.spBookListCreateByCustomer(param);
            var dict = result as IDictionary<string, object>;

            Console.WriteLine(dict?["Message"] ?? string.Empty);

            Assert.IsNotNull(dict);
            Assert.IsTrue(int.TryParse(dict["Status"].ToString(), out var status));
            Assert.That(status, Is.EqualTo(7));
        }

        [Test]
        public async Task CreateBookListByCustomer_Success()
        {
            var param = new
            {
                CustomerID = Guid.Parse("1DA1B7F8-D21F-4922-8F4C-048BC3E973E9"),
                FromSlot = 1,
                ToSlot = 2,
                BookDate = DateTimeOffset.UtcNow,
                StoreID = Guid.Parse("C0D8B9F4-23B0-4845-9E23-22989CD96316"),
                TableIDListString = "715B3B4A-0689-49CC-A40B-95E6304ADEC6",
                ProductTemplateIDListString = "9640EE63-76E0-424C-B9BA-53A456A3DD2F",
                IsTest = true
            };

            var result = await _bookListRepository.spBookListCreateByCustomer(param);
            var dict = result as IDictionary<string, object>;

            Console.WriteLine(dict?["Message"] ?? string.Empty);

            Assert.IsNotNull(dict);
            Assert.IsTrue(int.TryParse(dict["Status"].ToString(), out var status));
            Assert.That(status, Is.EqualTo(0));
            Assert.IsTrue(dict.ContainsKey("id"));
        }

        [Test]
        public async Task CreateBookListByStaff_CustomerNotFoundAndNoStaff()
        {
            var param = new
            {
                CustomerId = Guid.Parse("1DA1B7F8-D21F-4922-8F4C-048BC3E973E9"),
                StaffId = (Guid?)null,
                FromSlot = 1,
                ToSlot = 2,
                BookDate = DateTimeOffset.UtcNow,
                TableIDListString = "715B3B4A-0689-49CC-A40B-95E6304ADEC6",
                ProductIDListString = "DF304A77-74D6-4E1B-A92B-03E316B0D6FD,9721784A-AE29-4DEE-BC10-01D3CC089D5F",
                IsTest = true
            };

            var result = await _bookListRepository.spBookListCreateByStaff(param);
            var dict = result as IDictionary<string, object>;

            Console.WriteLine(dict?["Message"] ?? string.Empty);

            Assert.IsNotNull(dict);
            Assert.IsTrue(int.TryParse(dict["Status"].ToString(), out var status));
            Assert.That(status, Is.EqualTo(1));
        }

        [Test]
        public async Task CreateBookListByStaff_StaffNotActive()
        {
            var param = new
            {
                CustomerId = Guid.Parse("1DA1B7F8-D21F-4922-8F4C-048BC3E973E9"),
                StaffId = Guid.Parse("B3489265-3870-4600-AC51-AEFC42822C56"),
                FromSlot = 1,
                ToSlot = 2,
                BookDate = DateTimeOffset.UtcNow,
                TableIDListString = "715B3B4A-0689-49CC-A40B-95E6304ADEC6",
                ProductIDListString = "DF304A77-74D6-4E1B-A92B-03E316B0D6FD,9721784A-AE29-4DEE-BC10-01D3CC089D5F",
                IsTest = true
            };

            var result = await _bookListRepository.spBookListCreateByStaff(param);
            var dict = result as IDictionary<string, object>;

            Console.WriteLine(dict?["Message"] ?? string.Empty);

            Assert.IsNotNull(dict);
            Assert.IsTrue(int.TryParse(dict["Status"].ToString(), out var status));
            Assert.That(status, Is.EqualTo(9));
        }

        [Test]
        public async Task CreateBookListByStaff_ProductNotExist()
        {
            var param = new
            {
                CustomerId = Guid.Parse("1DA1B7F8-D21F-4922-8F4C-048BC3E973E9"),
                StaffId = Guid.Parse("A44ED012-283F-48C4-BC44-0142D772409B"),
                FromSlot = 1,
                ToSlot = 2,
                BookDate = DateTimeOffset.UtcNow,
                TableIDListString = "715B3B4A-0689-49CC-A40B-95E6304ADEC6",
                ProductIDListString = "DF304A77-74D6-4E1B-A92B-03E316B0D6F1,9721784A-AE29-4DEE-BC10-01D3CC089D5F",
                IsTest = true
            };

            var result = await _bookListRepository.spBookListCreateByStaff(param);
            var dict = result as IDictionary<string, object>;

            Console.WriteLine(dict?["Message"] ?? string.Empty);
            Console.WriteLine(dict?["Data"] ?? string.Empty);

            Assert.IsNotNull(dict);
            Assert.IsTrue(int.TryParse(dict["Status"].ToString(), out var status));
            Assert.That(status, Is.EqualTo(1));
            // Data contains invalid IDs string (optional to assert)
        }

        // 6. Success -> Status = 0 and returns id
        [Test]
        public async Task CreateBookListByStaff_Success()
        {
            var param = new
            {
                CustomerId = Guid.Parse("1DA1B7F8-D21F-4922-8F4C-048BC3E973E9"),
                StaffId = Guid.Parse("A44ED012-283F-48C4-BC44-0142D772409B"),
                FromSlot = 1,
                ToSlot = 2,
                BookDate = DateTimeOffset.UtcNow,
                TableIDListString = "715B3B4A-0689-49CC-A40B-95E6304ADEC6",
                ProductIDListString = "DF304A77-74D6-4E1B-A92B-03E316B0D6FD,9721784A-AE29-4DEE-BC10-01D3CC089D5F",
                IsTest = true
            };

            var result = await _bookListRepository.spBookListCreateByStaff(param);
            var dict = result as IDictionary<string, object>;

            Console.WriteLine(dict?["Message"] ?? string.Empty);

            Assert.IsNotNull(dict);
            Assert.IsTrue(int.TryParse(dict["Status"].ToString(), out var status));
            Assert.That(status, Is.EqualTo(0));
            Assert.IsTrue(dict.ContainsKey("id"));
            Assert.IsNotNull(dict["id"]);
        }

        [Test]
        public async Task UpdateBookItemProduct_Success()
        {
            var param = new
            {
                StaffId = Guid.Parse("B69B4F23-ED62-48C9-AE32-10E7C8259CF4"),
                BookItemId = Guid.Parse("EEAD8113-061D-4B5E-83D8-002ACB8A6AC0"),
                ProductCode = "TQSQ0000006",
                IsTest = true
            };

            var result = await _bookItemRepository.spBookItemUpdateProduct(param);
            var dict = result as IDictionary<string, object>;

            Assert.IsNotNull(dict);
            Assert.That(int.Parse(dict["Status"].ToString()), Is.EqualTo(0));
        }

        [Test]
        public async Task UpdateBookItemProduct_Fail_StaffNotFound()
        {
            var param = new
            {
                StaffId = Guid.Parse("B69B4F23-ED62-48C9-AE32-10E7C8259CF1"),
                BookItemId = Guid.Parse("EEAD8113-061D-4B5E-83D8-002ACB8A6AC0"),
                ProductCode = "TQSQ0000005",
                IsTest = true
            };

            var result = await _bookItemRepository.spBookItemUpdateProduct(param);
            var dict = result as IDictionary<string, object>;

            Assert.That(int.Parse(dict["Status"].ToString()), Is.EqualTo(1));
        }

        [Test]
        public async Task UpdateBookItemProduct_Fail_BookItemNotFound()
        {
            var param = new
            {
                StaffId = Guid.Parse("B69B4F23-ED62-48C9-AE32-10E7C8259CF4"),
                BookItemId = Guid.Parse("EEAD8113-061D-4B5E-83D8-002ACB8A6AC1"),
                ProductCode = "TQSQ0000005",
                IsTest = true
            };

            var result = await _bookItemRepository.spBookItemUpdateProduct(param);
            var dict = result as IDictionary<string, object>;

            Assert.That(int.Parse(dict["Status"].ToString()), Is.EqualTo(1));
        }

        [Test]
        public async Task UpdateBookItemProduct_Fail_WrongStore()
        {
            var param = new
            {
                StaffId = Guid.Parse("B69B4F23-ED62-48C9-AE32-10E7C8259CF4"),
                BookItemId = Guid.Parse("EEAD8113-061D-4B5E-83D8-002ACB8A6AC0"),
                ProductCode = "TQSQ0000005",
                IsTest = true
            };

            var result = await _bookItemRepository.spBookItemUpdateProduct(param);
            var dict = result as IDictionary<string, object>;

            Assert.That(int.Parse(dict["Status"].ToString()), Is.EqualTo(1));
        }

        [Test]
        public async Task UpdateBookItemProduct_Fail_NoPermission()
        {
            var param = new
            {
                StaffId = Guid.Parse("A7465ED2-6153-4548-BBFA-1CEC362BE986"),
                BookItemId = Guid.Parse("EEAD8113-061D-4B5E-83D8-002ACB8A6AC0"),
                ProductCode = "TQSQ0000004",
                IsTest = true
            };

            var result = await _bookItemRepository.spBookItemUpdateProduct(param);
            var dict = result as IDictionary<string, object>;

            Console.WriteLine(dict["Message"] ?? string.Empty);

            Assert.That(int.Parse(dict["Status"].ToString()), Is.EqualTo(2));
        }

        [Test]
        public async Task UpdateBookItemProduct_Fail_WrongTemplate()
        {
            var param = new
            {
                StaffId = Guid.Parse("B69B4F23-ED62-48C9-AE32-10E7C8259CF4"),
                BookItemId = Guid.Parse("EEAD8113-061D-4B5E-83D8-002ACB8A6AC0"),
                ProductCode = "CORT0000004",
                IsTest = true
            };

            var result = await _bookItemRepository.spBookItemUpdateProduct(param);
            var dict = result as IDictionary<string, object>;

            Assert.That(int.Parse(dict["Status"].ToString()), Is.EqualTo(1));
        }

        [Test]
        public async Task CancelBookList_Success_AsOwner()
        {
            var param = new
            {
                UserId = Guid.Parse("46160623-09EA-4D1C-93E5-C57E3D5DFED2"),
                BookListId = Guid.Parse("C9FCBBBA-7ECD-468E-9292-0AD3024831A4"),
                IsAdmin = false,
                IsTest = true
            };

            var result = await _bookListRepository.spBookListCancel(param);
            var dict = result as IDictionary<string, object>;

            Assert.That(dict, Is.Not.Null);
            Assert.That(int.Parse(dict["Status"].ToString()), Is.EqualTo(0));
        }

        [Test]
        public async Task CancelBookList_Success_AsAdmin()
        {
            var param = new
            {
                UserId = (Guid?)null,
                BookListId = Guid.Parse("C9FCBBBA-7ECD-468E-9292-0AD3024831A4"),
                IsAdmin = true,
                IsTest = true
            };

            var result = await _bookListRepository.spBookListCancel(param);
            var dict = result as IDictionary<string, object>;

            Assert.That(dict, Is.Not.Null);
            Assert.That(int.Parse(dict["Status"].ToString()), Is.EqualTo(0));
        }

        [Test]
        public async Task CancelBookList_Fail_NotOwner()
        {
            var param = new
            {
                UserId = Guid.Parse("46160623-09EA-4D1C-93E5-C57E3D5DFED1"),
                BookListId = Guid.Parse("C9FCBBBA-7ECD-468E-9292-0AD3024831A4"),
                IsAdmin = false,
                IsTest = true
            };

            var result = await _bookListRepository.spBookListCancel(param);
            var dict = result as IDictionary<string, object>;

            Assert.That(dict, Is.Not.Null);
            Assert.That(int.Parse(dict["Status"].ToString()), Is.EqualTo(2));
        }

        [Test]
        public async Task CancelBookList_Fail_BookListNotFound()
        {
            var param = new
            {
                UserId = Guid.Parse("46160623-09EA-4D1C-93E5-C57E3D5DFED2"),
                BookListId = Guid.Parse("C9FCBBBA-7ECD-468E-9292-0AD3024831A1"),
                IsAdmin = false,
                IsTest = true
            };

            var result = await _bookListRepository.spBookListCancel(param);
            var dict = result as IDictionary<string, object>;

            Assert.That(dict, Is.Not.Null);
            Assert.That(int.Parse(dict["Status"].ToString()), Is.EqualTo(1));
        }

        [Test]
        public async Task CancelBookList_Fail_InvalidStatus()
        {
            var param = new
            {
                UserId = Guid.Parse("1DA1B7F8-D21F-4922-8F4C-048BC3E973E9"),
                BookListId = Guid.Parse("DC4BE04E-BCDD-4A09-A220-82A7DD755FDF"),
                IsAdmin = false,
                IsTest = true
            };

            var result = await _bookListRepository.spBookListCancel(param);
            var dict = result as IDictionary<string, object>;

            Assert.That(dict, Is.Not.Null);
            Assert.That(int.Parse(dict["Status"].ToString()), Is.EqualTo(3));
        }
    }
}
