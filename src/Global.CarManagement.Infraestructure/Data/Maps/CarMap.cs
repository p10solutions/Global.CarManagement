using Global.CarManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Global.CarManagement.Infraestructure.Data.Maps
{
    public class CarMap : IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> builder)
        {
            builder.ToTable("TB_CAR");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("ID");

            builder.Property(x => x.Name)
                .HasColumnName("NAME")
                .HasColumnType("varchar(200)");

            builder.Property(x => x.Price)
                .HasColumnName("PRICE")
                .HasColumnType("numeric(10,2)");

            builder.Property(x => x.CreateDate)
                .HasColumnName("DT_CREATE");

            builder.Property(x => x.UpdateDate)
                .HasColumnName("DT_UPDATE");

            builder.Property(x => x.Status)
                .HasColumnName("STATUS");

            builder.Property(x => x.BrandId)
                .HasColumnName("BRAND_ID");

            builder.Property(x => x.PhotoId)
                .HasColumnName("PHOTO_ID");

            builder.Property(x => x.Details)
                .HasColumnName("DETAILS")
                .HasColumnType("varchar(max)"); ;

            builder.HasOne(x => x.Brand)
                .WithMany()
                .HasForeignKey(x => x.BrandId)
                .IsRequired();

            builder.HasOne(x => x.Photo)
                .WithMany()
                .HasForeignKey(x => x.PhotoId)
                .IsRequired();
        }
    }
}
