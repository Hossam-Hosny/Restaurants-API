using AutoMapper;
using Restaurant.Domain.Entities;

namespace Restaurant.Application.Restaurants.DTOs;

public class RestaurantProfile : Profile
{


    public RestaurantProfile()
    {
        CreateMap<RestaurantModel, RestaurantDTO>()
            .ForMember(d => d.City,opt => opt.MapFrom(   src =>  src.Address==null?null: src.Address.City))
            .ForMember(d=> d.PostalCode, opt=> opt.MapFrom(src => src.Address == null? null:src.Address.PostalCode))
            .ForMember(d => d.Street,opt => opt.MapFrom(src => src.Address==null?null:src.Address.Street))
            .ForMember(d => d.Dishes,opt => opt.MapFrom(src => src.Dishes));

        CreateMap<CreateRestaurantDTO, RestaurantModel>()
            .ForMember(d => d.Address,opt => opt.MapFrom(
                src => new Address
                {
                    City = src.City,
                    PostalCode = src.PostalCode,
                    Street = src.Street
                }))
            .ForMember(d => d.Dishes, opt => opt.MapFrom(src => src.Dishes))
            ;
    
    }




}
