using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebStore.ViewModels
{
    public class CartViewModel
    {
        public IEnumerable<(ProductViewModel product, int quantity)> Items { get; set; }

        public int ItemsCount => Items?.Sum(item => item.quantity) ?? 0;
        public decimal TotalPrice => Items?.Sum(item => item.product.Price * item.quantity) ?? 0;
    }
}
