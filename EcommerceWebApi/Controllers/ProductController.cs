using AutoMapper;
using EcommerceWebApi.Dto.Product;
using EcommerceWebApi.Dto.ProductDto;
using EcommerceWebApi.Interfaces;
using EcommerceWebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace EcommerceWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    

    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductController(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }
        [HttpGet("products")]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productRepository.GetAllAsync();
            return Ok(products);
        }


        [HttpGet("Id/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product =  await _productRepository.GetByIdAsync(id);
            if (product == null) 
                return BadRequest("No product with this Id");

            var result = _mapper.Map<GetProductDto>(product);

            return Ok(result);
        }


        [HttpGet("ProductName")]
        public async Task<IActionResult> GetByName(string ProductName)
        {
            var product = await _productRepository.GetByNameAsync(ProductName);
            if (product == null)
                return BadRequest("No product with this name");

            return Ok(product);
        }


        [HttpGet("CategoryName")]
        public async Task<IActionResult> GetByCategoryNameAsync(string category)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            

            var result = await _productRepository.GetByCategoryNameAsync(category);
            if (result == null)
                return BadRequest("no category with this name");

            return Ok(result);
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UpdateProductDto createProductDto)
        {
            if(!ModelState.IsValid) 
                return BadRequest(ModelState);

            var newProduct = await _productRepository.CreateAsync(createProductDto);
            var result = _mapper.Map<GetProductDto>(newProduct);


            return result == null ? BadRequest("This product already exist"): Ok(result);
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteById(int id)
        {
            var deletedProduct = await _productRepository.DeleteAsync(id);
            if (deletedProduct == null)
                return BadRequest("product not found");

            return Ok("Product deleted successfully");
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpPut]
        public async Task<IActionResult> Update(int id,[FromBody] UpdateProductDto Dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updatedProduct = await _productRepository.UpdateAsync(id, Dto);

            if (updatedProduct == null)
                return BadRequest("No Product with this Id");
            return Ok(updatedProduct);
        }
        

    }
}
