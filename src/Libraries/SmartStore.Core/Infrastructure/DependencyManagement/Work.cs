// TODO: Migrate to ASP.NET Core DI
namespace SmartStore.Core.Infrastructure.DependencyManagement
{
    public class Work<T>
    {
        public T Value => default(T);
    }
    
    public class ContainerManager
    {
    }
}
