using Global.CarManagement.Domain.Entities;
using Global.CarManagement.Domain.Models.Pagination;

namespace Global.CarManagement.Domain.Contracts.Data.Repositories
{
    public interface ICarRepository
    {
        Task AddAsync(Car Car);
        void UpdatePartial(Car Car);
        void Update(Car Car);
        Task<(IEnumerable<Car>, int)> GetAsync(string name, Paginator paginator);
        Task<Car> GetByIdAsync(Guid id);
        Task<bool> ExistsAsync(string name, Guid brandId, Guid CarId);
        Task<bool> ExistsAsync(Guid id);
        public void Delete(Guid id);
    }
}
