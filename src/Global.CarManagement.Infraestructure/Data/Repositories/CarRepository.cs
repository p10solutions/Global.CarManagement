using Global.CarManagement.Domain.Contracts.Data.Repositories;
using Global.CarManagement.Domain.Entities;
using Global.CarManagement.Domain.Models.Pagination;
using Global.CarManagement.Infraestructure.Data.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Global.CarManagement.Infraestructure.Data.Repositories
{
    public class CarRepository : ICarRepository
    {
        readonly CarManagementContext _context;

        public CarRepository(CarManagementContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Car Car)
            => await _context.Car.AddAsync(Car);

        public async Task<Car> GetByIdAsync(Guid id)
            => await _context.Car
                .Include(x => x.Brand)
                .Include(x => x.Photo)
                .SingleOrDefaultAsync(x => x.Id == id);

        public async Task<(IEnumerable<Car>, int)> GetAsync(string name, Paginator paginator)
        {
            var paginatedData = _context.Car
                .Include(x => x.Brand)
                .Include(x => x.Photo)
                .Where(x => string.IsNullOrEmpty(name) || x.Name.ToLower().Contains(name.ToLower()))
                .AddPagination(paginator);

            return (await paginatedData.Item1.ToListAsync(), paginatedData.Item2);
        }

        public void UpdatePartial(Car Car)
        {
            _context.Attach(Car);

            _context.Entry(Car).Property(x => x.Status).IsModified = true;
            _context.Entry(Car).Property(x => x.Name).IsModified = true;
            _context.Entry(Car).Property(x => x.Details).IsModified = true;
            _context.Entry(Car).Property(x => x.Price).IsModified = true;
            _context.Entry(Car).Property(x => x.BrandId).IsModified = true;
        }

        public void Update(Car car)
        {
            _context.Update(car);
        }

        public async Task<bool> ExistsAsync(string name, Guid brandId, Guid CarId)
            => await _context.Car.AnyAsync(x => x.Name == name && x.BrandId == brandId && x.Id != CarId);

        public async Task<bool> ExistsAsync(Guid id)
            => await _context.Car.AnyAsync(x => x.Id == id);

        public void Delete(Guid id)
        {
            var motorcycle = new Car() { Id = id };

            _context.Car.Attach(motorcycle);

            _context.Car.Remove(motorcycle);
        }
    }
}
