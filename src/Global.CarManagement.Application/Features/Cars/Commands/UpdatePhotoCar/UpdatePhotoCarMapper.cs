using AutoMapper;
using Global.CarManagement.Domain.Entities;
using Global.CarManagement.Domain.Models.Events.Cars;

namespace Global.CarManagement.Application.Features.Cars.Commands.UpdatePhotoCar
{
    public class UpdatePhotoCarMapper : Profile
    {
        public UpdatePhotoCarMapper()
        {
            CreateMap<Car, UpdatePhotoCarResponse>();
            CreateMap<Car, UpdatedCarEvent>();
        }
    }
}
