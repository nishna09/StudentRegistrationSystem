using Moq;
using RepositoryLibrary.Entities;
using ServicesLibrary.Services;
using StudentRegistrationSystem;

namespace TestServicesProject
{
    public class Tests
    {
        private readonly IStudentServices StudentServices;
        [SetUp]
        public void Setup(IStudentServices studentServices)
        {
            StudentServices = new StudentServices();
        }
        [Test]
        public void Test1()
        {
            var mock = new Mock<IStudentDAL>();
            //whenever the GetInventoryItems method is called, return a new list of inventory as defined below
            //In this case a new class(stub) like the one commented does not need to be created
            mock.Setup(z => z.Get()).Returns(new List<Student>()
            {
                new InventoryItem()
                {
                    InventoryItemId = 1,
                    CountInStock = 1,
                    Price=70
                },
                new InventoryItem()
                {
                    InventoryItemId = 2,
                    CountInStock = 3,
                    Price=370
                },
                new InventoryItem()
                {
                    InventoryItemId = 3,
                    CountInStock = 5,
                    Price=900
                },
                new InventoryItem()
                {
                    InventoryItemId = 4,
                    CountInStock = 6,
                    Price=845
                },
                new InventoryItem()
                {
                    InventoryItemId = 5,
                    CountInStock = 6,
                    Price=1000
                }
            });
            Assert.Pass();
        }
    }
}