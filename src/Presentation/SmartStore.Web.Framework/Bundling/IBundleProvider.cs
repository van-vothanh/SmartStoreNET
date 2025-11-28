using WebOptimizer;

namespace SmartStore.Web.Framework.Bundling
{
    public interface IBundleProvider
    {
        void RegisterBundles(BundleCollection bundles);

        int Priority { get; }
    }
}
