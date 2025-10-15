using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartStore.Shipping.Domain;

namespace SmartStore.Shipping.Data
{
    public class ByTotalRecordMap : EntityTypeConfiguration<ShippingByTotalRecord>
    {
        public ByTotalRecordMap()
        {
            this.ToTable("ShippingByTotal");
            this.HasKey(x => x.Id);

            this.Property(x => x.Zip).IsOptional().HasMaxLength(400);
        }
    }
}
