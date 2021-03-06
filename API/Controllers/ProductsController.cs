using Core.Repositories.Contracts;
using Core.Specifications.Product;
using EFModels.Data;
using EFModels.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IGenericRepository<Product> _productRepository;
    private readonly IGenericRepository<ProductBrand> _productBrandRepository;
    private readonly IGenericRepository<ProductType> _productTypeRepository;

    public ProductsController(
        IGenericRepository<Product> productRepository,
        IGenericRepository<ProductBrand> productBrandRepository,
        IGenericRepository<ProductType> productTypeRepository)
    {
        this._productRepository = productRepository;
        this._productBrandRepository = productBrandRepository;
        this._productTypeRepository = productTypeRepository;
    }

    [HttpGet]
    public async Task<ActionResult<List<Product>>> GetProducts()
    {
        var spec = new ProductWithTypesAndBrandsSpecification();

        var products = await _productRepository.GetAll(spec);

        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        var spec = new ProductWithTypesAndBrandsSpecification(id);

        var product = await _productRepository.GetWithSpec(spec);

        return Ok(product);
    }

    [HttpGet("brands")]
    public async Task<ActionResult<List<Product>>> GetProductBrands() =>
        Ok(await _productBrandRepository.GetAll());

    [HttpGet("types")]
    public async Task<ActionResult<List<Product>>> GetProductTypes() =>
        Ok(await _productTypeRepository.GetAll());
}