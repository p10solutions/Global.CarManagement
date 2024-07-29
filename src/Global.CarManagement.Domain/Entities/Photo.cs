namespace Global.CarManagement.Domain.Entities
{
    public class Photo
    {
        public Photo(string base64)
        {
            Id = Guid.NewGuid();
            Base64 = base64;
        }

        public Guid Id { get; set; }
        public string Base64 { get; set; }
    }
}
