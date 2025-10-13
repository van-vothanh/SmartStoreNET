// TODO: Migrate to IFileProvider
namespace SmartStore.Core.IO
{
    public interface IVirtualFolder
    {
        string Path { get; }
    }
    
    public interface IVirtualPathProvider
    {
    }
}
