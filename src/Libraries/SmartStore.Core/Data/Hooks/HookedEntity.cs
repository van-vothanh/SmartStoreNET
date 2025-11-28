using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace SmartStore.Core.Data.Hooks
{
    public class HookedEntity
    {
        private EntityEntry _entry;

        public HookedEntity(EntityEntry entry)
        {
            _entry = entry;
        }

        public EntityEntry Entry
        {
            get { return _entry; }
        }

        public Type EntityType
        {
            get { return _entry.Entity.GetType(); }
        }

        public BaseEntity Entity
        {
            get { return _entry.Entity as BaseEntity; }
        }

        public EntityState State
        {
            get { return _entry.State; }
        }

        public bool HasStateChanged
        {
            get
            {
                var currentState = _entry.State;
                return InitialState != currentState;
            }
        }

        public EntityState InitialState
        {
            get;
            internal set;
        }

        public IDictionary<string, object> ModifiedProperties
        {
            get
            {
                var props = new Dictionary<string, object>();
                
                if (_entry.State == EntityState.Modified)
                {
                    foreach (var prop in _entry.Properties)
                    {
                        if (prop.IsModified)
                        {
                            props[prop.Metadata.Name] = prop.CurrentValue;
                        }
                    }
                }
                
                return props;
            }
        }

        public bool IsPropertyModified(string propertyName)
        {
            Guard.NotEmpty(propertyName, nameof(propertyName));

            if (_entry.State == EntityState.Modified)
            {
                return _entry.Property(propertyName).IsModified;
            }

            return false;
        }
    }
}
