using System;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using SmartStore.Core.Data;
using SmartStore.Core.Domain.Cms;

namespace SmartStore.Data
{
    public class SmartObjectContext : DbContext, IDbContext
    {
        public SmartObjectContext(DbContextOptions<SmartObjectContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Apply all entity configurations from the assembly
            var typesToRegister = from t in Assembly.GetExecutingAssembly().GetTypes()
                                  where t.Namespace.HasValue() &&
                                        t.BaseType != null &&
                                        t.BaseType.IsGenericType
                                  let genericType = t.BaseType.GetGenericTypeDefinition()
                                  where genericType == typeof(IEntityTypeConfiguration<>)
                                  select t;

            foreach (var type in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.ApplyConfiguration(configurationInstance);
            }

            // Apply configurations manually if needed
            modelBuilder.Entity<MenuRecord>();
            modelBuilder.Entity<MenuItemRecord>();

            base.OnModelCreating(modelBuilder);
        }

        // Implement IDbContext interface methods
        public bool HooksEnabled { get; set; } = true;
        public bool AutoCommitEnabled { get; set; } = true;
        public string Alias { get; set; }
        
        // Add other required IDbContext members as needed
    }
}
