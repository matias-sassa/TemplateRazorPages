using Core.Dto.Product;
using Core.Entities;
using Core.Interfaces;

namespace Core.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly Mapper _mapper = new();
    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public List<ProductDto> GetAll()
    {
        try
        {
            var products = _productRepository.GetAllWithLastPrice()
                                        .ToList();

            var productsDto = _mapper.ProductEntityToDtoList(products);

            productsDto.Select(p =>
            {
                var productPrice = products.FirstOrDefault(x => x.Id == p.Id)!.ProductPrices.FirstOrDefault();
                if (productPrice == null) return p;

                p.Price = productPrice!.Price;

                return p;
            }).ToList();

            return productsDto;
        }
        catch (Exception ex)
        {
            throw new Exception("Error when getting all products", ex);
        }
    }

    public async Task<ProductDto> FindAsync(Guid Id)
    {
        try
        {
            var product = await _productRepository.Find(Id);

            if (product == null)
                throw new Exception($"Product not found. Guid: {Id}");

            var productDto = _mapper.ProductEntityToDto(product);
            productDto.Price = product.ProductPrices.FirstOrDefault()!.Price;

            return productDto;
        }
        catch (Exception ex)
        {
            throw new Exception("Error when finding product", ex);
        }
    }

    public async Task Create(ProductDto productDto)
    {
        try
        {
            var product = _mapper.ProductDtoToEntity(productDto);
            product.CreatedDate = DateTime.Now;
            product.ModifiedDate = DateTime.Now;

            product.ProductPrices = new List<ProductPrice>{
            new ProductPrice
            {
                Price = productDto.Price,
                Date = DateTime.Now
            }};

            await _productRepository.AddAsync(product);
        }
        catch (Exception ex)
        {
            throw new Exception("Error when creating product", ex);
        }
    }

    public async Task Update(ProductEditDto productDto)
    {
        try
        {
            var productEntity = await _productRepository.Find(productDto.Id);

            if (productEntity == null)
                throw new Exception($"Product not found when editing. Guid: {productDto.Id}");

            productEntity.IsActive = productDto.IsActive;
            productEntity.Name = productDto.Name;
            productEntity.ModifiedDate = DateTime.Now;

            if (productDto.Price != null)
                productEntity.ProductPrices.Add(new()
                {
                    Price = productDto.Price.Value,
                    Date = DateTime.Now
                });

            await _productRepository.UpdateAsync(productEntity);
        }
        catch (Exception ex)
        {
            throw new Exception("Error when updating product", ex);
        }
    }

    public async Task ChangeStatus(Guid id, bool isActive)
    {
        try
        {
            var productEntity = await _productRepository.Find(id);

            if (productEntity == null)
                throw new Exception($"Product not found when editing. Guid: {id}");

            productEntity.IsActive = isActive;
            productEntity.ModifiedDate = DateTime.Now;

            await _productRepository.UpdateAsync(productEntity);
        }
        catch(Exception ex)
        {
            throw new Exception("Error when changing product status", ex);
        }
    }

    public async Task Delete(Guid id)
    {
        try
        {
            var productEntity = await _productRepository.Find(id);

            if (productEntity == null)
                throw new Exception($"Product not found when deleting. Guid: {id}");

            productEntity.IsDelete = true;
            productEntity.DeletedDate = DateTime.Now;

            await _productRepository.UpdateAsync(productEntity);
        }
        catch (Exception ex)
        {
            throw new Exception("Error when deleting product", ex);
        }
    }
}
