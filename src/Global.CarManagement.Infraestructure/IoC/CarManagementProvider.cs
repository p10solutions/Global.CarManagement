using Global.CarManagement.Application.Features.Cars.Commands.CreateCar;
using Global.CarManagement.Application.Features.Cars.Commands.UpdateCar;
using Global.CarManagement.Application.Features.Cars.Queries.GetCar;
using Global.CarManagement.Domain.Contracts.Data;
using Global.CarManagement.Domain.Contracts.Data.Repositories;
using Global.CarManagement.Domain.Contracts.Events;
using Global.CarManagement.Domain.Contracts.Notifications;
using Global.CarManagement.Infraestructure.Data;
using Global.CarManagement.Infraestructure.Data.Repositories;
using Global.CarManagement.Infraestructure.Events.Cars;
using Global.CarManagement.Infraestructure.Validation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Global.CarManagement.Infraestructure.IoC
{
    public static class CarManagementProvider
    {
        public static IServiceCollection AddProviders(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));

            var connectionString = configuration.GetConnectionString("CarManagement");

            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(FailFastValidator<,>));
            services.AddScoped<INotificationsHandler, NotificationHandler>();
            services.AddDbContextPool<CarManagementContext>(opt => opt.UseSqlServer(connectionString));
            services.AddTransient<ICarRepository, CarRepository>();
            services.AddTransient<ICarProducer, CarProducer>();
            services.AddTransient<ICarProducer, CarProducer>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddAutoMapper(typeof(CreateCarMapper));
            services.AddAutoMapper(typeof(UpdateCarMapper));
            services.AddAutoMapper(typeof(GetCarMapper));

            return services;
        }
    }
}
