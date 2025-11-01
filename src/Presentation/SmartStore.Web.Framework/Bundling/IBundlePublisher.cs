// using WebOptimizer; // TODO: Migrate bundling

namespace SmartStore.Web.Framework.Bundling
{
    public interface IBundlePublisher
    {
        void RegisterBundles(BundleCollection bundles);
    }
}
