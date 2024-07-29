using AutoMapper;
using Global.CarManagement.Domain.Entities;
using Global.CarManagement.Domain.Models.Events.Cars;

namespace Global.CarManagement.Application.Features.Cars.Commands.UpdateCar
{
    public class UpdateCarMapper : Profile
    {
        public UpdateCarMapper()
        {
            CreateMap<UpdateCarCommand, Car>()
                .ForMember(dest => dest.UpdateDate, opt => opt.MapFrom(src=> DateTime.Now));
            CreateMap<Car, UpdateCarResponse>();
            CreateMap<Car, UpdatedCarEvent>();
        }
    }
}
