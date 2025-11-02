using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartStore.ShippingByWeight.Domain;

namespace SmartStore.ShippingByWeight.Data
{
    public partial class ShippingByWeightRecordMap : EntityTypeConfiguration<ShippingByWeightRecord>
    {
        public ShippingByWeightRecordMap()
        {
            this.ToTable("ShippingByWeight");
            this.HasKey(x => x.Id);
            this.Property(x => x.Zip).IsOptional();
        }
    }
}