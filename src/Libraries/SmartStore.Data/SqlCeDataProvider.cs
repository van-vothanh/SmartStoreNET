using System.Data.Common;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Data.SqlClient;

namespace SmartStore.Data
{
    public class SqlCeDataProvider : IEfDataProvider
    {
        public virtual IDbConnectionFactory GetConnectionFactory()
        {
            return new SqlCeConnectionFactory("System.Data.SqlServerCe.4.0");
        }

        public bool StoredProceduresSupported => false;

        public DbParameter GetParameter()
        {
            return new SqlParameter();
        }

        public string ProviderInvariantName => "System.Data.SqlServerCe.4.0";
    }
}
