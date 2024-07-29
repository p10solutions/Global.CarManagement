using AutoMapper;
using Global.CarManagement.Domain.Entities;

namespace Global.CarManagement.Application.Features.Cars.Queries.GetCar
{
    public class GetCarMapper : Profile
    {
        public GetCarMapper()
        {
            CreateMap<Car, GetCarResponse>()
                .ForMember(dest => dest.BrandName, opt => opt.MapFrom(src => src.Brand.Name))
                .ForMember(dest => dest.Photo, opt => opt.MapFrom(src => src.Photo.Base64));
        }
    }
}
