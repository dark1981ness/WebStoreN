using System.Collections.Generic;
using System.Linq;
using WebStore.Domain.DTO;
using WebStore.Domain.Entities;

namespace WebStore.Services.Mapping
{
    public static class SectionMapper
    {
        public static SectionDTO ToDTO(this Section section) => section is null
            ? null
            : new SectionDTO
            {
                Id = section.Id,
                Name = section.Name,
                Order = section.Order,
                ParentId = section.ParentId
            };

        public static Section FromDTO(this SectionDTO section) => section is null
            ? null
            : new Section
            {
                Id = section.Id,
                Name = section.Name,
                Order = section.Order,
                ParentId = section.ParentId
            };

        public static IEnumerable<SectionDTO> ToDTO(this IEnumerable<Section> section) => section.Select(ToDTO);

        public static IEnumerable<Section> FromDTO(this IEnumerable<SectionDTO> section) => section.Select(FromDTO);
    }
}
