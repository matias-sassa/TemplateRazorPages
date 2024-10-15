using AltaP.Mappings;
using AltaP.ViewModels;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AltaP.Pages.Products;

public class EditProductModel : PageModel
{
    private readonly IProductService _productService;
    private readonly Mapper _mapper = new();
    public EditProductModel(IProductService productService)
    {
        _productService = productService;
    }

    [BindProperty]
    public ProductViewModel Product { get; set; }

    public async Task<IActionResult> OnGetAsync(Guid id)
    {
        var productDto = await _productService.FindAsync(id);

        if (productDto == null)
        {
            return NotFound();
        }

        Product = _mapper.ProductDtoToViewModel(productDto);

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        if (Product == null)
            RedirectToPage(PageReference.PRODUCTS_ALL);

        var productEditDto = _mapper.ProductViewModelToEditDto(Product!);
        await _productService.Update(productEditDto);

        return RedirectToPage(PageReference.PRODUCTS_ALL);
    }
}
