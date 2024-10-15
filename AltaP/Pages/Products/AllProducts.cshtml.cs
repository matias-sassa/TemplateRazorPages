using AltaP.Mappings;
using AltaP.ViewModels;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;

namespace AltaP.Pages.Products;

public class AllProductsModel : PageModel
{
    [BindProperty]
    public List<ProductViewModel> Products { get; set; }

    private readonly IProductService _productService;
    private readonly Mapper _mapper = new();
    public AllProductsModel(IProductService productService)
    {
        _productService = productService;
        Products = new();
    }

    public void OnGet()
    {
        var productsDto = _productService.GetAll();

        var products = _mapper.ProductDtoToViewModelList(productsDto);
        Products = products;
    }

    public async Task<IActionResult> OnPostActivationAsync([FromBody] ActivationRequest request)
    {
        try
        {
            await _productService.ChangeStatus(request.Id, request.IsActive);

            string prefix = request.IsActive ? "Activado" : "Desactivado";
            string result = $"{prefix} con éxito!";

            return new JsonResult(result);
        }
        catch
        {
            return this.NotFound("Error al cambiar el estado del producto");
        }
    }

    public async Task<IActionResult> OnPostDeleteAsync([FromBody] Guid id)
    {
        try
        {
            await _productService.Delete(id);
            return new JsonResult("Eliminado con éxito!");
        }
        catch
        {
            return this.NotFound("Error al eliminar el producto");
        }
    }
}

public class ActivationRequest
{
    public Guid Id { get; set; }
    public bool IsActive { get; set; }
}
