using System.Collections.Generic;
using WebStore.Domain.Entities.Base;
using WebStore.Domain.Entities.Interfaces;

namespace WebStore.Domain.Entities
{
    //[Table("Brand")]
    public class Brand : NamedEntity, IOrderedEntity
    {
        //[Column("BrandOrder"), TypeName="long"]
        public int Order { get; set; }

        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
