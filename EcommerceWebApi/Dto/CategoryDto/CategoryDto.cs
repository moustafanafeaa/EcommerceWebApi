using System.ComponentModel.DataAnnotations;

namespace EcommerceWebApi.Dto.CategoryDto
{
    public class CategoryDto
    {
        [Required]
        [StringLength(100,MinimumLength = 1)]
        public string Name { get; set; }
    }

}
