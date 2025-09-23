using System;

namespace SmartStore.Core.Compatibility
{
    /// <summary>
    /// Compatibility attribute for EF6 Index attribute.
    /// In EF Core, indexes should be configured using Fluent API in OnModelCreating.
    /// This attribute is kept for backward compatibility during migration.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class IndexAttribute : Attribute
    {
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

        public string Name { get; set; }
        public bool IsUnique { get; set; }
        public int Order { get; set; }
    }
}
