using AutoMapper;
using Global.CarManagement.Domain.Entities;

namespace Global.CarManagement.Application.Features.Cars.Queries.GetCarById
{
    public class GetCarByIdMapper : Profile
    {
        public GetCarByIdMapper()
        {
            CreateMap<Car, GetCarByIdResponse>()
                .ForMember(dest => dest.BrandName, opt => opt.MapFrom(src => src.Brand.Name))
                .ForMember(dest => dest.Photo, opt => opt.MapFrom(src => src.Photo.Base64));
        }
    }
}
