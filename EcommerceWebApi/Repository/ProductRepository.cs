using AutoMapper;
using EcommerceWebApi.Data;
using EcommerceWebApi.Dto.Product;
using EcommerceWebApi.Dto.ProductDto;
using EcommerceWebApi.Interfaces;
using EcommerceWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace EcommerceWebApi.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ProductRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<IEnumerable<GetProductDto>> GetAllAsync()
        {
            var Products = await _context.Products
                .Include(p=>p.Category)
                .Include(p => p.ProductColors)
                .ToListAsync();

            return _mapper.Map<IEnumerable<GetProductDto>>(Products);
        }
        
        public async Task<Product> GetByIdAsync(int id)
        {
            var product = await _context.Products
                .Include(p=>p.Category)
                .Include(p=>p.ProductColors)
                .FirstOrDefaultAsync(x => x.Id == id);

            return product == null ? null : product;
        }

        public async Task<GetProductDto> GetByNameAsync(string name)
        {
            var product = await _context.Products
                .Include(p=>p.Category)
                .Include(p=>p.ProductColors)
                .FirstOrDefaultAsync(p=>p.Name == name);

            if (product == null)
                return null;

            return product == null ? null : _mapper.Map<GetProductDto>(product);
            
        }
        public async Task<IEnumerable<GetProductDto>> GetByCategoryNameAsync(string CategoryName)
        {
            var validCategory = await _context.Categories.FirstOrDefaultAsync(c=>c.Name ==  CategoryName);
            if (validCategory == null)
                return null;
            var Products = await _context.Products.Where(p => p.Category!.Name == CategoryName)
                .Include(p => p.Category).Include(p => p.ProductColors).ToListAsync();

            if (Products == null || Products.Count == 0)
            {
                return Enumerable.Empty<GetProductDto>();
            }
            var result = _mapper.Map<IEnumerable<GetProductDto>>(Products);
            return result;
        }

        public async Task<Product> CreateAsync(UpdateProductDto createProductDto)
        {
            var productExis = await _context.Products.FirstOrDefaultAsync(p =>p.Name== createProductDto.Name);
            if (productExis != null)
            {
                return null;
            }
            var category = await _context.Categories
                .FirstOrDefaultAsync(c => c.Name == createProductDto.CategoryName);

            if (category == null)
            {
                category = new Category { Name = createProductDto.CategoryName };
                await _context.Categories.AddAsync(category);
            }

            var product = _mapper.Map<Product>(createProductDto);
            product.Category = category;

            if (createProductDto.ProductColors != null)
            {
                product.ProductColors.Clear();
                product.ProductColors = createProductDto.ProductColors
                    .Select(color => new ProductColors { Color = color })
                    .ToList();
            }           
           // product.ProductColors = createProductDto.ProductColors.Select(c => new ProductColors { Color = c }).ToList();

            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();

            return product;
        }

        public async Task<GetProductDto> UpdateAsync(int id, UpdateProductDto Dto)
        {
            var oldProduct = await GetByIdAsync(id);

            if (oldProduct == null)
                return null;

            //update new data in old product
            //_mapper.Map(Dto,oldProduct);
            Console.WriteLine($"Old Product: {oldProduct.Name}, {oldProduct.Price}, {oldProduct.Quantity}");
            Console.WriteLine($"Dto: {Dto.Name}, {Dto.Price}, {Dto.Quantity}");

            if (!string.IsNullOrEmpty(Dto.CategoryName))
            {
                var category = await _context.Categories
                    .FirstOrDefaultAsync(x => x.Name == Dto.CategoryName);

                if (category == null)
                {
                    category = new Category { Name = Dto.CategoryName };
                    _context.Categories.Add(category);
                }

                oldProduct.Category = category;
            }
            if (Dto.ProductColors  != null)
            {
                oldProduct.ProductColors.Clear();
                oldProduct.ProductColors = Dto.ProductColors
                    .Select(color => new ProductColors { Color = color })
                    .ToList();
            }

            //it's doesn't make price and quantity = 0 when it's not in the body

            if (Dto.Price.HasValue)
                oldProduct.Price = Dto.Price.Value;

                 
            
            if (Dto.Quantity.HasValue)
                oldProduct.Quantity = Dto.Quantity.Value;

            await _context.SaveChangesAsync();
            Console.WriteLine($"Updated Product: {oldProduct.Name}, {oldProduct.Price}, {oldProduct.Quantity}, {oldProduct.Category.Name}");
            foreach (var color in oldProduct.ProductColors)
            {
                Console.WriteLine($"Color: {color.Color}");
            }
            return _mapper.Map<GetProductDto>(oldProduct);
        }
        public async Task<Product> DeleteAsync(int id)
        {
            var product = await GetByIdAsync(id);
            if (product == null)
                return null;

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return product;
        }

    }
}
