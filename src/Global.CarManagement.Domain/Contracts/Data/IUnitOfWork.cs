namespace Global.CarManagement.Domain.Contracts.Data
{
    public interface IUnitOfWork
    {
        Task CommitAsync();
    }
}
