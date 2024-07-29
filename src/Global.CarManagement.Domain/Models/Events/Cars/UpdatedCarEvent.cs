using Global.CarManagement.Domain.Entities;

namespace Global.CarManagement.Domain.Models.Events.Cars
{
    public record UpdatedCarEvent
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public EStatus Status { get; set; }
        public Guid BrandId { get; set; }
    }
}
