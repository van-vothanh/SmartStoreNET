using System;
using Microsoft.EntityFrameworkCore;

// Compatibility stubs for Entity Framework 6 to EF Core migration

namespace System.Data.Entity
{
    // EF6 to EF Core compatibility stubs
    public class DbEntityEntry
    {
        public object Entity { get; set; }
        public Microsoft.EntityFrameworkCore.EntityState State { get; set; }
    }

    // Use EF Core's EntityState
    public enum EntityState
    {
        Detached = Microsoft.EntityFrameworkCore.EntityState.Detached,
        Unchanged = Microsoft.EntityFrameworkCore.EntityState.Unchanged,
        Added = Microsoft.EntityFrameworkCore.EntityState.Added,
        Deleted = Microsoft.EntityFrameworkCore.EntityState.Deleted,
        Modified = Microsoft.EntityFrameworkCore.EntityState.Modified
    }
}

namespace System.Data.Entity.Infrastructure
{
    public class DbEntityEntry
    {
        public object Entity { get; set; }
        public Microsoft.EntityFrameworkCore.EntityState State { get; set; }
    }
}

namespace System.ComponentModel.DataAnnotations.Schema
{
    // EF6 Index attribute compatibility - map to EF Core IndexAttribute
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true)]
    public class IndexAttribute : Attribute
    {
        public string Name { get; set; }
        public bool IsUnique { get; set; }
        public int Order { get; set; }

        public IndexAttribute()
        {
        }

        public IndexAttribute(string name)
        {
            Name = name;
        }

        public IndexAttribute(string name, int order)
        {
            Name = name;
            Order = order;
        }
    }
}

// Additional EF6 compatibility types
namespace System.Data.SqlServerCe
{
    public class SqlCeConnection
    {
        public string ConnectionString { get; set; }
    }
}
