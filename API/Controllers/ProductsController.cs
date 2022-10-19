using API.Infrastucture.Errors;
using AutoMapper;
using Core.Repositories.Contracts;
using Core.Specifications.Product;
using EFModels.Entities;
using Microsoft.AspNetCore.Mvc;
using Models.Product;

namespace API.Controllers;

public class ProductsController : BaseApiController
{
    private readonly IGenericRepository<Product> _productRepository;
    private readonly IGenericRepository<ProductBrand> _productBrandRepository;
    private readonly IGenericRepository<ProductType> _productTypeRepository;
    private readonly IMapper _mapper;

    public ProductsController(
        IGenericRepository<Product> productRepository,
        IGenericRepository<ProductBrand> productBrandRepository,
        IGenericRepository<ProductType> productTypeRepository,
        IMapper mapper)
    {
        this._productRepository = productRepository;
        this._productBrandRepository = productBrandRepository;
        this._productTypeRepository = productTypeRepository;
        this._mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<List<ProductToReturnDto>>> GetProducts([FromQuery]ProductSpecParams productParams)
    {
        var spec = new ProductWithTypesAndBrandsSpecification(productParams);

        var products = await _productRepository.GetAll(spec);

        return Ok(_mapper
            .Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products));
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
    {
        var spec = new ProductWithTypesAndBrandsSpecification(id);

        var product = await _productRepository.GetWithSpec(spec);

        if (product == null) return NotFound(new ApiResponse(404));

        return Ok(_mapper
            .Map<Product, ProductToReturnDto>(product));
    }

    [HttpGet("brands")]
    public async Task<ActionResult<List<Product>>> GetProductBrands() =>
        Ok(await _productBrandRepository.GetAll());

    [HttpGet("types")]
    public async Task<ActionResult<List<Product>>> GetProductTypes() =>
        Ok(await _productTypeRepository.GetAll());
}