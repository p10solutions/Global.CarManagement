using Global.CarManagement.Domain.Contracts.Data;

namespace Global.CarManagement.Infraestructure.Data
{
    public class UnitOfWork: IUnitOfWork
    {
        readonly CarManagementContext _context;

        public UnitOfWork(CarManagementContext context)
        {
            _context = context;
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
