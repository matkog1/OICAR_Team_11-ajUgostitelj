﻿using AutoMapper;
using WebAPI.DTOs;
using WebAPI.Models;

namespace WebAPI.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Order, OrderDto>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status)).ReverseMap();
            CreateMap<Order, OrderStatusDto>().ReverseMap();
            CreateMap<OrderDto, OrderStatusDto>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status)).ReverseMap();
            CreateMap<Table, TableDto>().ReverseMap();
            CreateMap<User, LoginDto>().ReverseMap();
            CreateMap<User, RegisterDto>().ReverseMap();
            CreateMap<User, ChangePasswordDto>().ReverseMap();
            CreateMap<Notification, NotificationDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, CreateUserDto>().ReverseMap();
            CreateMap<User, UpdateUserDto>().ReverseMap();
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<InventoryDto, Inventory>()
                .ForMember(dest => dest.Product, opt => opt.Ignore()); 
            CreateMap<Inventory, InventoryDto>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name));
            CreateMap<Role, RoleDto>().ReverseMap();
            CreateMap<OrderItem, OrderItemDto>().ReverseMap();
            CreateMap<Payment, PaymentDto>().ReverseMap();
            CreateMap<StockCheck, StockCheckDto>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name));
            CreateMap<StockCheckDto, StockCheckDto>()
                .ForMember(dest => dest.ProductName, opt => opt.Ignore());
            CreateMap<Review,ReviewDTO>().ReverseMap();
        }
    }
}
