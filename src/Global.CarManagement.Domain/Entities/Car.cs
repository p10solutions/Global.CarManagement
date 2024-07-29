namespace Global.CarManagement.Domain.Entities
{
    public class Car
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Details { get; set; }
        public double Price { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public EStatus Status { get; set; }
        public Guid PhotoId { get; set; }
        public Guid BrandId { get; set; }
        public Brand? Brand { get; set; }
        public Photo? Photo { get; set; }

        public Car()
        {
            
        }

        public Car(string name, double price, EStatus status, Guid brandId, string details, Guid photoId)
        {
            Id = Guid.NewGuid();
            Name = name;
            Price = price;
            CreateDate = DateTime.Now;
            Status = status;
            BrandId = brandId;
            Details = details;
            PhotoId = photoId;
        }
    }
}
