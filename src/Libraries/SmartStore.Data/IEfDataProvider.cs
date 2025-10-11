using Microsoft.EntityFrameworkCore.ChangeTracking;
using SmartStore.Core.Data;

namespace SmartStore.Data
{
    public interface IEfDataProvider : IDataProvider
    {
        /// <summary>
        /// Get connection factory
        /// </summary>
        /// <returns>Connection factory</returns>
        IDbConnectionFactory GetConnectionFactory();

    }
}
