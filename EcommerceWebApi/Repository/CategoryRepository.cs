using AutoMapper;
using EcommerceWebApi.Data;
using EcommerceWebApi.Dto;
using EcommerceWebApi.Dto.CategoryDto;
using EcommerceWebApi.Interfaces;
using EcommerceWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace EcommerceWebApi.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public CategoryRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ResponseDto> GetAllAsync()
        {
            var Categories = await _context.Categories.ToListAsync();

            if (Categories == null && Categories.Count == 0)
            {
                return new ResponseDto
                {
                    IsSucceeded = false,
                    Message = "There is no Categories yet."
                };
            }

            var result = _mapper.Map<IEnumerable<CategoryDto>>(Categories);

            return new ResponseDto
            {
                IsSucceeded = true,
                Model = result
            };
        }

        public async Task<ResponseDto> GetByIdAsync(int Id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c=>c.Id == Id);

            if (category == null)
            {
                return new ResponseDto
                {
                    IsSucceeded = false,
                    Message = "No Category with this id"
                };
            }

           var result = _mapper.Map<CategoryDto>(category);
            return new ResponseDto
            {
                IsSucceeded = true,
                Model = result
            };
        }
        
        public async Task<ResponseDto> GetByNameAsync(string Name)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Name == Name);

            if (category == null)
            {
                return new ResponseDto
                {
                    IsSucceeded = false,
                    Message = "No Category With This Name"
                };
            }

            var result = _mapper.Map<CategoryDto>(category);
            return new ResponseDto
            {
                IsSucceeded = true,
                Model = result
            };
        }
        
        public async Task<ResponseDto> GetCategoryWithProductsAsync(int Id)
        {
            var category = await _context.Categories
                .Include(p => p.Products)
                .ThenInclude(pc => pc.ProductColors)
                .FirstOrDefaultAsync(p=>p.Id == Id);

            if (category == null)
            {
                return new ResponseDto
                {
                    Message = "there is no Category with this Id",
                    IsSucceeded = false
                };
            }

            var categoryDto = _mapper.Map<CategoryWithProductsDto>(category);
            if (categoryDto.Products.Count < 1)
            {
                return new ResponseDto
                {
                    Message = "This Category Have Not Any Product Yet",
                    IsSucceeded = false
                };
            }


            return new ResponseDto
            {
                IsSucceeded = true
                , Model = categoryDto
            };
            

        }

        public async Task<ResponseDto> AddAsync(CategoryDto dto)
        {
            if (await _context.Categories.AnyAsync(c => c.Name == dto.Name))
            {
                return new ResponseDto
                {
                   Message=  "This category is already exists.",
                   IsSucceeded = false
                };
            }
        
            var newCategory = _mapper.Map<Category>(dto);
            var result = await _context.AddAsync(newCategory);
            var entity = _context.Entry(newCategory);

            if (entity.State != EntityState.Added)
            {
                return new ResponseDto
                {
                    Message = "Failed to add this category.",
                    IsSucceeded = false
                };
            }

            await _context.SaveChangesAsync();
            return new ResponseDto
            {
                IsSucceeded = true,
                Message = $"Category \"{newCategory.Name}\" now created successfully.",
                Model = newCategory
            };
        }


        public async Task<ResponseDto> DeleteByIdAsync(int Id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == Id);

            if (category == null)
            {
                return new ResponseDto
                {
                    Message = "No category with this ID",
                    IsSucceeded = false
                };
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return new ResponseDto
            {
                Model = category,
                Message = $"Category \"{category.Name}\" deleted successfully",
                IsSucceeded = true
            };
        }


        public async Task<ResponseDto> DeleteByNameAsync(string name)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Name == name);

            if (category == null)
            {
                return new ResponseDto
                {
                    Message = "No category with this Name",
                    IsSucceeded = false
                };
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return new ResponseDto
            {
                Model = category,
                Message = $"Category \"{category.Name}\" deleted successfully",
                IsSucceeded = true
            };
        }


        public async Task<ResponseDto> UpdateAsync(int Id, CategoryDto dto)
        {
            var category = _context.Categories.FirstOrDefault(c => c.Id == Id);

            if (category == null)
            {
                return new ResponseDto
                {
                    Message = "No category with this Id",
                    IsSucceeded = false
                };
            }
            var oldCategory = category.Name;

            category.Name = dto.Name;
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();

            return new ResponseDto
            {
                Model = dto.Name,
                Message = $"the Name of category \"{oldCategory}\" Now is \"{dto.Name}\"",
                IsSucceeded = true

            };
        }

        
    }
}
