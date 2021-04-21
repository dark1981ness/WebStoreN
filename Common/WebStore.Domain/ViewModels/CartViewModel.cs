using System.Collections.Generic;
using System.Linq;

namespace WebStore.Domain.ViewModels
{
    public class CartViewModel
    {
        public IEnumerable<(ProductViewModel product, int quantity)> Items { get; set; }

        public int ItemsCount => Items?.Sum(item => item.quantity) ?? 0;
        public decimal TotalPrice => Items?.Sum(item => item.product.Price * item.quantity) ?? 0;
    }
}
