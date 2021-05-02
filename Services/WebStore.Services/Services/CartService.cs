using System.Linq;
using WebStore.Domain;
using WebStore.Domain.Entities;
using WebStore.Domain.ViewModels;
using WebStore.Interfaces.Services;
using WebStore.Services.Mapping;

namespace WebStore.Services.Services
{
    public class CartService : ICartServices
    {
        private readonly IProductData _productData;
        private readonly ICartStore _cartStore;

        public CartService(ICartStore cartStore, IProductData productData)
        {
            _productData = productData;
            _cartStore = cartStore;
        }

        public void Add(int id)
        {
            var cart = _cartStore.Cart;
            var item = cart.Items.FirstOrDefault(i => i.ProductId == id);

            if (item is null)
                cart.Items.Add(new CartItem { ProductId = id });
            else
                item.Quantity++;

            _cartStore.Cart = cart;
        }

        public void Clear()
        {
            var cart = _cartStore.Cart;
            cart.Items.Clear();
            _cartStore.Cart = cart;
        }

        public void Decrement(int id)
        {
            var cart = _cartStore.Cart;
            var item = cart.Items.FirstOrDefault(i => i.ProductId == id);

            if (item is null) return;

            if (item.Quantity > 0)
                item.Quantity--;

            if (item.Quantity <= 0)
                cart.Items.Remove(item);

            _cartStore.Cart = cart;
        }

        public CartViewModel GetViewModel()
        {
            var cart = _cartStore.Cart;
            var products = _productData.GetProducts(new ProductFilter
            {
                Ids = cart.Items.Select(item => item.ProductId).ToArray()
            });

            var product_views = products.FromDTO().ToView().ToDictionary(p => p.Id);

            return new CartViewModel
            {
                Items = cart.Items
                .Where(item => product_views.ContainsKey(item.ProductId))
                .Select(item => (product_views[item.ProductId], item.Quantity))
            };
        }

        public void Remove(int id)
        {
            var cart = _cartStore.Cart;
            var item = cart.Items.FirstOrDefault(i => i.ProductId == id);
            if (item is null) return;
            cart.Items.Remove(item);
            _cartStore.Cart = cart;
        }
    }
}
