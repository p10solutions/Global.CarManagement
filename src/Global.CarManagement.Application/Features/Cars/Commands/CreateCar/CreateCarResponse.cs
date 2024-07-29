using Global.CarManagement.Domain.Entities;

namespace Global.CarManagement.Application.Features.Cars.Commands.CreateCar
{
    public class CreateCarResponse
    {
        public CreateCarResponse(Guid id, string name, string details, double price, DateTime createDate, DateTime? updateDate, EStatus status, Guid brandId, Guid photoId)
        {
            Id = id;
            Name = name;
            Details = details;
            Price = price;
            CreateDate = createDate;
            UpdateDate = updateDate;
            Status = status;
            BrandId = brandId;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Details { get; set; }
        public double Price { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public EStatus Status { get; set; }
        public Guid BrandId { get; set; }
        public Guid PhotoId { get; set; }
    }
}
