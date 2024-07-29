using Global.CarManagement.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Global.CarManagement.Api.Configuration
{
    public static class DataBaseConfig
    {
        public static void RunMigrations(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                var context = services.GetRequiredService<CarManagementContext>();
                context.Database.Migrate();
            }
        }
    }
}
