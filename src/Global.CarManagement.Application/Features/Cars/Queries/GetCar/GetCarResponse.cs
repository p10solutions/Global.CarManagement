using Global.CarManagement.Domain.Entities;

namespace Global.CarManagement.Application.Features.Cars.Queries.GetCar
{
    public class GetCarResponse
    {
        public GetCarResponse(Guid id, string name, string details, double price, DateTime createDate, 
            DateTime? updateDate, EStatus status, Guid brandId, string brandName)
        {
            Id = id;
            Name = name;
            Details = details;
            Price = price;
            CreateDate = createDate;
            UpdateDate = updateDate;
            Status = status;
            BrandId = brandId;
            BrandName = brandName;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Details { get; set; }
        public double Price { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public EStatus Status { get; set; }
        public Guid BrandId { get; set; }
        public string BrandName { get; set; }
        public string Photo { get; set; }
    }
}
