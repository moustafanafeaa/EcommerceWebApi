using AutoMapper;
using EcommerceWebApi.Dto.CategoryDto;
using EcommerceWebApi.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _CategoryRepository;

        public CategoryController(ICategoryRepository CategoryRepository)
        {
            _CategoryRepository = CategoryRepository;
        }

        [HttpGet("Categories")]
        public async Task<IActionResult> GetAll()
        {
            if (ModelState.IsValid)
            {
                var Categories = await _CategoryRepository.GetAllAsync();

                return Categories.IsSucceeded == false ? NotFound(Categories.Message) : Ok(Categories.Model);
            }
            return BadRequest(ModelState);
        }



        [HttpGet("Id/{id}")]
        public async Task<IActionResult> GetByID(int id)
        {
            if (ModelState.IsValid)
            {
                var category = await _CategoryRepository.GetByIdAsync(id);

                return category.IsSucceeded == false ? NotFound(category.Message) : Ok(category.Model);
            }
            return BadRequest(ModelState);
        }


        [HttpGet("Name")]
        public async Task<IActionResult> GetByName(string name)
        {
            if (ModelState.IsValid)
            {
                var category = await _CategoryRepository.GetByNameAsync(name);

                return category.IsSucceeded == false ? NotFound(category.Message) : Ok(category.Model);
            }
            return BadRequest(ModelState);
        }



        [HttpGet("CategoryWithProducts/{categoryId}")]
        public async Task<IActionResult> GetCategoryWithProducts(int categoryId)
        {
            if (ModelState.IsValid)
            {
                var category = await _CategoryRepository.GetCategoryWithProductsAsync(categoryId);

                if (!category.IsSucceeded) return NotFound(category.Message);

                return Ok(category.Model);
            }
            return BadRequest(ModelState);
        }



        [Authorize(Policy = "AdminOnly")]
        [HttpPost("AddCategory")]
        public async Task<IActionResult> Add(CategoryDto dto)
        {
            var category = await _CategoryRepository.AddAsync(dto);

            if (!category.IsSucceeded)
            {
                return BadRequest(category.Message);
            }
            return Ok(category.Message);
        }



        [Authorize(Policy = "AdminOnly")]
        [HttpDelete("Id/{id}")]
        public async Task<IActionResult> DeleteById(int id)
        {
            var category = await _CategoryRepository.DeleteByIdAsync(id);

            if (!category.IsSucceeded)
            {
                return NotFound(category.Message);
            }
            return Ok(category.Message);
        }



        [Authorize(Policy = "AdminOnly")]
        [HttpDelete("Name")]
        public async Task<IActionResult> DeleteByName(string name)
        {
            var category = await _CategoryRepository.DeleteByNameAsync(name);

            if (!category.IsSucceeded)
            {
                return NotFound(category.Message);
            }
            return Ok(category.Message);
        }



        [Authorize(Policy = "AdminOnly")]
        [HttpPut("UpdateCategory/{id}")]
        public async Task<IActionResult> Update(int id,CategoryDto dto)
        {
            var category = await _CategoryRepository.UpdateAsync(id,dto);

            if (!category.IsSucceeded)
            {
                return NotFound(category.Message);
            }
            return Ok(category.Message);
        }
        


    }
}
