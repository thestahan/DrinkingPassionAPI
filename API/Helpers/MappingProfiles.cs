﻿using API.Dtos.Accounts;
using API.Dtos.Cocktails;
using API.Dtos.Ingredients;
using API.Dtos.Products;
using AutoMapper;
using Core.Entities;
using Core.Entities.Identity;

namespace API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<ProductUnit, ProductToReturnDto>();
            CreateMap<ProductUnitToAddDto, ProductUnit>();
            CreateMap<ProductType, ProductTypeToReturnDto>();
            CreateMap<ProductTypeToAddDto, ProductType>();
            CreateMap<Cocktail, CocktailToReturnDto>()
                .ForMember(
                    dest => dest.Picture,
                    opt => opt.MapFrom<CocktailUrlResolver>());
            CreateMap<Ingredient, IngredientToReturnDto>()
                .ForMember(
                    dest => dest.Name,
                    opt => opt.MapFrom(src => src.Product.Name));
            CreateMap<CocktailToAddDto, Cocktail>();
            CreateMap<IngredientToAddDto, Ingredient>();
            CreateMap<Product, ProductToReturnDto>()
                .ForMember(
                    dest => dest.ProductUnit,
                    opt => opt.MapFrom(src => src.ProductUnit.Name))
                .ForMember(
                    dest => dest.ProductType,
                    opt => opt.MapFrom(src => src.ProductType.Name));
            CreateMap<ProductToAddDto, Product>();
            CreateMap<RegisterDto, AppUser>()
                .ForMember(
                    dest => dest.UserName,
                    opt => opt.MapFrom(src => src.Email));
        }
    }
}
