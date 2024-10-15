using AltaP.Mappings;
using AltaP.ViewModels;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AltaP.Pages.Products;

public class CreateProductModel : PageModel
{
    private readonly IProductService _productService;
    private readonly Mapper _mapper = new();
    public CreateProductModel(IProductService productService)
    {
        _productService = productService;
    }

    [BindProperty]
    public ProductViewModel Product { get; set; }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        if (Product == null)
            RedirectToPage(PageReference.PRODUCTS_ALL);

        var productDto = _mapper.ProductViewModelToDto(Product!);
        await _productService.Create(productDto);

        return RedirectToPage(PageReference.PRODUCTS_ALL);
    }
}
