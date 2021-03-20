using System.Collections.Generic;
using OA_DataAccess.Entities;
using OA_DataAccess.Repositories;
using OA_Repository.DTO;
using OA_Service.Implementation;
using Xunit;

namespace Tests
{
    public class ProductServiceTests
    {
        [Fact]
        public void GetItemByIdWithId1()
        {
            var unitOfWork = new EfUnitOfWork();
            var itemService = new ProductService(unitOfWork);
            var actual = itemService.GetProductById(1);
            var item = new ProductDTO
            {
                Id = 1, Name = "Samsung s20", Category = "mobile phones", Description = "good for You", Price = 500M
            };
            Assert.Equal(actual.Name, item.Name);
        }

        [Fact]
        public void ShowAllItemsMustBeIEnumerableType()
        {
            var unitOfWork = new EfUnitOfWork();
            ProductService itemService = new ProductService(unitOfWork);
            var actual = itemService.GetAllProducts();
            Assert.IsAssignableFrom<IEnumerable<ProductDTO>>(actual);
        }

        [Fact]
        public void AddNewItem()
        {
            var unitOfWork = new EfUnitOfWork();
            var itemService = new ProductService(unitOfWork);
            var expected = 4;
            var item = new ProductDTO();
            itemService.CreateProduct(item);
            var actual = 0;
            foreach (var el in itemService.GetAllProducts())
            {
                actual++;
            }
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void IsName_WithNull_ReturnFalse()
        {
            bool actual = OrderDTO.IsName(null);

            Assert.False(actual);
        }

        [Fact]
        public void IsName_WithBlankString_ReturnFalse()
        {
            bool actual = OrderDTO.IsName(" ");

            Assert.False(actual);
        }

        [Fact]
        public void IsName_WithGoodName_ReturnTrue()
        {
            bool actual = OrderDTO.IsName("Food");

            Assert.True(actual);
        }
    }
}
