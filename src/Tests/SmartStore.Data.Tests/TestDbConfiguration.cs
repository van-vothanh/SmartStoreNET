using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace SmartStore.Data.Tests
{
    public class TestDbConfiguration : DbConfiguration
    {
        public TestDbConfiguration()
        {
            base.SetDefaultConnectionFactory(new SqlCeConnectionFactory("System.Data.SqlServerCe.4.0"));
        }
    }
}
