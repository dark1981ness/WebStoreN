using System.Collections.Generic;

namespace WebStore.Domain.DTO
{
    public record PageProductsDTO(IEnumerable<ProductDTO> Products, int TotalCount);
}
