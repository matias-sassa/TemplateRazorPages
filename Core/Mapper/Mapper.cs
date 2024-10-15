using Core.Dto.Product;
using Core.Entities;
using Riok.Mapperly.Abstractions;

namespace Core.Services;

[Mapper]
public partial class Mapper
{
    public partial Product ProductDtoToEntity (ProductDto product);
    public partial List<Product> ProductDtoToEntityList (List<ProductDto> product);

    public partial List<ProductDto> ProductEntityToDtoList(List<Product> product);
    public partial ProductDto ProductEntityToDto(Product product);
}
