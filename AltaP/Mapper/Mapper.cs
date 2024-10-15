using AltaP.ViewModels;
using Core.Dto.Product;
using Riok.Mapperly.Abstractions;

namespace AltaP.Mappings;

[Mapper]
public partial class Mapper
{
    public partial ProductDto ProductViewModelToDto(ProductViewModel product);
    public partial ProductEditDto ProductViewModelToEditDto(ProductViewModel product);
    public partial List<ProductDto> ProductViewModelToDtoList(List<ProductViewModel> product);

    public partial ProductViewModel ProductDtoToViewModel(ProductDto product);
    public partial List<ProductViewModel> ProductDtoToViewModelList(List<ProductDto> product);
}
