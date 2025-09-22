using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartStore.Core.Domain.Common;
using SmartStore.Core.Domain.Customers;

namespace SmartStore.Data.Mapping.Customers
{
    public partial class CustomerMap : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customer");
            builder.HasKey(c => c.Id);
            
            builder.Property(u => u.Username).HasMaxLength(500);
            builder.Property(u => u.Email).HasMaxLength(500);
            builder.Property(u => u.SystemName).HasMaxLength(500);
            builder.Property(u => u.Password).HasMaxLength(500);
            builder.Property(u => u.PasswordSalt).HasMaxLength(500);
            builder.Property(u => u.LastIpAddress).HasMaxLength(100);

            builder.Property(u => u.Title).HasMaxLength(100);
            builder.Property(u => u.Salutation).HasMaxLength(50);
            builder.Property(u => u.FirstName).HasMaxLength(225);
            builder.Property(u => u.LastName).HasMaxLength(225);
            builder.Property(u => u.FullName).HasMaxLength(450);
            builder.Property(u => u.Company).HasMaxLength(255);
            builder.Property(u => u.CustomerNumber).HasMaxLength(100);

            builder.Ignore(u => u.PasswordFormat);

            // Configure indexes (migrated from [Index] attributes)
            builder.HasIndex(c => c.Deleted);
            builder.HasIndex(c => new { c.Deleted, c.IsSystemAccount })
                   .HasDatabaseName("IX_Customer_Deleted_IsSystemAccount");
            builder.HasIndex(c => c.IsSystemAccount);
            builder.HasIndex(c => c.SystemName);
            builder.HasIndex(c => c.LastIpAddress)
                   .HasDatabaseName("IX_Customer_LastIpAddress");

            // Many-to-many relationship with Address
            builder.HasMany<Address>(c => c.Addresses)
                   .WithMany()
                   .UsingEntity(j => j.ToTable("CustomerAddresses"));

            // Optional relationships
            builder.HasOne<Address>(c => c.BillingAddress)
                   .WithMany()
                   .HasForeignKey("BillingAddressId")
                   .IsRequired(false);
                   
            builder.HasOne<Address>(c => c.ShippingAddress)
                   .WithMany()
                   .HasForeignKey("ShippingAddressId")
                   .IsRequired(false);
        }
    }
}
