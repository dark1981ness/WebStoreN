using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebStore.Controllers;
using WebStore.Domain.DTO;
using WebStore.Domain.ViewModels;
using WebStore.Interfaces.Services;
using Assert = Xunit.Assert;


namespace WebStore.Tests.Controllers
{
    [TestClass]
    public class CatalogControllerTests
    {
        [TestMethod]
        public void Details_Returns_with_Correct_View()
        {
            #region Arrange
            //Подготовка исходных данных
            //Подготовка ожидаемых результатов
            //Подготовка объекта тестирования

            const int expected_id = 1;
            const decimal expected_price = 10m;
            var expected_name = $"Product id {expected_id}";
            var product_data_mock = new Mock<IProductData>();

            product_data_mock
                .Setup(p => p.GetProductById(It.IsAny<int>()))
                .Returns<int>(id => new ProductDTO
                {
                    Id = id,
                    Name = $"Product id {id}",
                    Order = 1,
                    Price = expected_price,
                    ImageUrl = $"img_{id}.png",
                    Brand = new BrandDTO { Id = 1, Name = "Brand", Order = 1 },
                    Section = new SectionDTO { Id = 1, Name = "Section", Order = 1 }
                });

            var configuration_mock = new Mock<IConfiguration>();
            configuration_mock.Setup(c => c[It.IsAny<string>()])
                .Returns("3");

            var controller = new CatalogController(product_data_mock.Object, configuration_mock.Object);

            #endregion

            #region Act

            //Выполнение действия

            var result = controller.Details(expected_id);

            #endregion

            #region Assert

            //Проверка утверждений

            var view_result = Assert.IsType<ViewResult>(result);

            var model = Assert.IsAssignableFrom<ProductViewModel>(view_result.Model);

            Assert.Equal(expected_id, model.Id);
            Assert.Equal(expected_name, model.Name);
            Assert.Equal(expected_price, model.Price);

            #endregion
        }
    }
}
