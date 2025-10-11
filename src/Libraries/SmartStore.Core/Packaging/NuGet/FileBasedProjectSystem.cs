using System;
using System.IO;
using System.Runtime.Versioning;
// TODO: Migrate to direct file system operations
// using NuGet;

namespace SmartStore.Core.Packaging
{
    // TODO: Migrate to direct file system operations
    // NuGet.Core IProjectSystem is obsolete
    /*
    public class FileBasedProjectSystem : PhysicalFileSystem, IProjectSystem
    {
        public FileBasedProjectSystem(string root) : base(root)
        {
        }

        public FrameworkName TargetFramework => new FrameworkName(".NETFramework, Version=4.7.2");

        public void AddFrameworkReference(string name)
        {
        }

        public void AddImport(string targetPath, ProjectImportLocation location)
        {
        }

        public dynamic GetPropertyValue(string propertyName)
        {
            return null;
        }

        public bool IsSupportedFile(string path)
        {
            return true;
        }

        public string ProjectName => Path.GetFileName(Root);

        public bool ReferenceExists(string name)
        {
            return false;
        }

        public void RemoveImport(string targetPath)
        {
        }

        public void RemoveReference(string name)
        {
        }

        public void AddReference(string referencePath, Stream stream)
        {
        }

        public bool FileExistsInProject(string path)
        {
            return FileExists(path);
        }
    }
    */
}
