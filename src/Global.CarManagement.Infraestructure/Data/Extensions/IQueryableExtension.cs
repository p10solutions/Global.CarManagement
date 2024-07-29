using Global.CarManagement.Domain.Models.Pagination;

namespace Global.CarManagement.Infraestructure.Data.Extensions
{
    public static class IQueryableExtension
    {
        public static (IQueryable<T>, int) AddPagination<T>(this IQueryable<T> query,
            Paginator paginator) where T : class
        {
            var count = query.Count();

            var skip = (paginator.CurrentPage - 1) * paginator.PageSize;
            return (query.Skip(skip).Take(paginator.PageSize), count);
        }
    }
}
