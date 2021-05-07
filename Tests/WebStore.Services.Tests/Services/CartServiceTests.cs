using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStore.Domain;
using WebStore.Domain.DTO;
using WebStore.Domain.Entities;
using WebStore.Domain.ViewModels;
using WebStore.Interfaces.Services;
using WebStore.Services.Services;
using Assert = Xunit.Assert;

namespace WebStore.Services.Tests.Services
{
    [TestClass]
    public class CartServiceTests
    {
        private Cart _cart;

        private Mock<IProductData> _productDataMock;
        private Mock<ICartStore> _cartStoreMock;

        private ICartServices _cartService;

        [TestInitialize]
        public void Initialize()
        {
            _cart = new Cart
            {
                Items = new List<CartItem>
                {
                    new() { ProductId = 1, Quantity = 1 },
                    new() { ProductId = 2, Quantity = 3 },
                }
            };

            _productDataMock = new Mock<IProductData>();
            _productDataMock
                .Setup(c => c.GetProducts(It.IsAny<ProductFilter>()))
                .Returns(new PageProductsDTO(new[]
                {
                    new ProductDTO
                    {
                        Id = 1,
                        Name = "Product 1",
                        Price = 1.1m,
                        Order = 1,
                        ImageUrl = "Img_1.png",
                        Brand = new BrandDTO { Id = 1, Name = "Brand 1", Order = 1 },
                        Section = new SectionDTO{Id = 1, Name = "Section 1", Order = 1 }
                    },
                    new ProductDTO
                    {
                        Id = 2,
                        Name = "Product 2",
                        Price = 2.2m,
                        Order = 2,
                        ImageUrl = "Img_1.png",
                        Brand = new BrandDTO { Id = 2, Name = "Brand 2", Order = 2 },
                        Section = new SectionDTO{Id = 2, Name = "Section 2", Order = 2 }
                    },
                    new ProductDTO
                    {
                        Id =31,
                        Name = "Product 3",
                        Price = 3.3m,
                        Order = 3,
                        ImageUrl = "Img_3.png",
                        Brand = new BrandDTO { Id = 3, Name = "Brand 3", Order = 3 },
                        Section = new SectionDTO{Id = 3, Name = "Section 3", Order = 3 }
                    },

                }), 3);
            _cartStoreMock = new Mock<ICartStore>();
            _cartStoreMock.Setup(c => c.Cart).Returns(_cart);

            _cartService = new CartService(_cartStoreMock.Object, _productDataMock.Object);
        }

        [TestMethod]
        public void Cart_Class_ItemsCount_returns_Correct_Quantity()
        {
            var cart = _cart;
            var expected_items_count = _cart.Items.Sum(i => i.Quantity);

            var actual_items_count = cart.ItemsCount;

            Assert.Equal(expected_items_count, actual_items_count);
        }

        [TestMethod]
        public void CartViewModel_Returns_Correct_ItemsCount()
        {
            var cart_view_model = new CartViewModel
            {
                Items = new[]
                {
                    ( new ProductViewModel { Id = 1, Name = "Product 1", Price = 0.5m }, 1 ),
                    ( new ProductViewModel { Id = 2, Name = "Product 2", Price = 1.5m }, 3 ),
                }
            };
            const int expected_count = 4;

            var actual_count = cart_view_model.ItemsCount;

            Assert.Equal(expected_count, actual_count);
        }

        [TestMethod]
        public void CartViewModel_Returns_Correct_TotalPrice()
        {
            var cart_view_model = new CartViewModel
            {
                Items = new[]
                {
                    ( new ProductViewModel { Id = 1, Name = "Product 1", Price = 0.5m }, 1 ),
                    ( new ProductViewModel { Id = 2, Name = "Product 2", Price = 1.5m }, 3 ),
                }
            };
            var expected_total_price = cart_view_model.Items.Sum(item => item.quantity * item.product.Price);

            var actual_total_price = cart_view_model.TotalPrice;

            Assert.Equal(expected_total_price, actual_total_price);
        }

        [TestMethod]
        public void CartService_Add_WorkCorrect()
        {
            _cart.Items.Clear();

            const int expected_id = 5;
            const int expected_items_count = 1;

            _cartService.Add(expected_id);

            Assert.Equal(expected_items_count, _cart.ItemsCount);

            Assert.Single(_cart.Items);

            Assert.Equal(expected_id, _cart.Items.First().ProductId);
        }

        [TestMethod]
        public void CartService_Remove_Correct_Item()
        {
            const int item_id = 1;
            const int expected_product_id = 2;

            _cartService.Remove(item_id);

            Assert.Single(_cart.Items);

            Assert.Equal(expected_product_id, _cart.Items.Single().ProductId);
        }

        [TestMethod]
        public void CartService_Clear_ClearCart()
        {
            _cartService.Clear();

            Assert.Empty(_cart.Items);
        }

        [TestMethod]
        public void CartService_Decrement_Correct()
        {
            const int item_id = 2;
            const int expected_quantity = 2;
            const int expectes_items_count = 3;
            const int expected_products_count = 2;

            _cartService.Decrement(item_id);

            Assert.Equal(expectes_items_count, _cart.ItemsCount);
            Assert.Equal(expected_products_count, _cart.Items.Count);
            var items = _cart.Items.ToArray();
            Assert.Equal(item_id, items[1].ProductId);
            Assert.Equal(expected_quantity, items[1].Quantity);
        }

        [TestMethod]
        public void CartService_Remove_Item_When_Decrement_to_0()
        {
            const int item_id = 1;
            const int expected_items_count = 3;

            _cartService.Decrement(item_id);

            Assert.Equal(expected_items_count, _cart.ItemsCount);
            Assert.Single(_cart.Items);
        }

        [TestMethod]
        public void CartService_GetViewModel_WorkCorrect()
        {
            Debug.WriteLine("Тестирование преобразования корзины в модель-представления");

            const int expected_items_count = 4;
            const decimal expected_first_product_price = 1.1m;

            var result = _cartService.GetViewModel();

            Assert.Equal(expected_items_count, result.ItemsCount);
            Assert.Equal(expected_first_product_price, result.Items.First().product.Price);
        }
    }
}
