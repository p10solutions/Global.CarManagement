using AutoMapper;
using Global.CarManagement.Domain.Entities;
using Global.CarManagement.Domain.Models.Events.Cars;

namespace Global.CarManagement.Application.Features.Cars.Commands.CreateCar
{
    public class CreateCarMapper : Profile
    {
        public CreateCarMapper()
        {
            CreateMap<CreateCarCommand, Car>()
                .ForMember(dest => dest.Photo, opt => opt.MapFrom(src => new Photo(src.Photo)));
            CreateMap<Car, CreateCarResponse>();
            CreateMap<Car, CreatedCarEvent>();
        }
    }
}
