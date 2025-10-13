// TODO: Migrate to ASP.NET Core
namespace SmartStore.Core
{
    public interface IMergedData
    {
        object MergedDataValues { get; set; }
        bool MergedDataIgnore { get; set; }
    }
}
