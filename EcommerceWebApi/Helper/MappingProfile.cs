using AutoMapper;
using EcommerceWebApi.Dto;
using EcommerceWebApi.Dto.CartDto;
using EcommerceWebApi.Dto.CartItemsDto;
using EcommerceWebApi.Dto.CategoryDto;
using EcommerceWebApi.Dto.Order;
using EcommerceWebApi.Dto.OrderItemDto;
using EcommerceWebApi.Dto.Product;
using EcommerceWebApi.Dto.ProductColorDto;
using EcommerceWebApi.Dto.ProductDto;
using EcommerceWebApi.Dto.UserDto;
using EcommerceWebApi.Models;
using System.Drawing;

namespace EcommerceWebApi.Helper
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            //USER
            CreateMap<RegisterUserDto, User>();
            CreateMap<UpdateUserDto, User>();
            CreateMap<UserDto, User>().ReverseMap();

            //PRODUCT
            CreateMap<Product, GetProductDto>();

            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<ProductColors, GetProductColorDto>();

            CreateMap<CreateProductDto, Product>()
                .ForMember(dest => dest.ProductColors, opt => opt.MapFrom(
                src => src.ProductColores.Select(
                color => new ProductColors { Color = color }).ToList()));

            CreateMap<UpdateProductDto, Product>()
                .ForMember(dest => dest.ProductColors, opt => opt.MapFrom(
                src => src.ProductColors.Select(
                color => new ProductColors { Color = color }).ToList()))
                .ForMember(dest => dest.Category, opt => opt.Ignore())
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<Category, CategoryWithProductsDto>();
            CreateMap<Product, ProductsInCategoryDto>();

            CreateMap<Cart, GetCartDto>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.user.UserName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.user.Email))
                .ForMember(dest => dest.CartItemDtos, opt => opt.MapFrom(src => src.CartItems))
            .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.CartItems.Sum(ci => ci.Quantity * ci.Product.Price)));

            CreateMap<CartItem, GetCartItemsDto>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.ProductPrice, opt => opt.MapFrom(src => src.Product.Price))
                .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.Quantity * src.Product.Price));



            CreateMap<AddCartDto, Cart>();
            CreateMap<CartItemDto, CartItem>().ReverseMap();

            CreateMap<AddOrderDto, Order>().ReverseMap();
            CreateMap<AddOrderItemDto, OrderItem>()
                .ForMember(dest => dest.Price, opt => opt.Ignore());
        }
    }
}
