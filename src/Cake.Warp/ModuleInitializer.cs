namespace Cake.Warp
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using Cake.Warp.Common;
    using JetBrains.Annotations;
    using Resourcer;

    /// <summary>
    /// Module class responsible for the logic needed
    /// to run when the library is loaded.
    /// </summary>
    public static class ModuleInitializer
    {
        /// <summary>
        /// The actual method that will be called during
        /// the loading of the addin library (by Module.Fody IL weaving).
        /// </summary>
        /// <notes>
        ///   While this method is public, it should never be
        ///   called manually. It should only be called
        ///   using the existing IL weaving.
        /// </notes>
        [UsedImplicitly]
        public static void Initialize()
        {
            var assemblyDirectory = AddinConfiguration.Instance.AssemblyDirectoryPath;
            var warpFileName = "warp-packer";
            if (AddinConfiguration.Instance.IsWindows)
            {
                // Would prefer to get the file extension preferred by
                // system instead of hard-coding this.
                // Not found anything related to this.
                warpFileName += ".exe";
            }

            var fullPathToFile = Path.Combine(assemblyDirectory, warpFileName);
            AddinConfiguration.Instance.WarpFilePath = fullPathToFile;

            if (File.Exists(fullPathToFile))
            {
                return;
            }

            using (var resourceStream = GetWarpResource())
                using (var fileStream = File.Create(fullPathToFile))
                {
                    // Is there perhaps a better way, than doing this
                    resourceStream.CopyTo(fileStream);
                }

            if (!AddinConfiguration.Instance.IsWindows)
            {
                // This is required, otherwise we won't be able to run warp-packer
                var process = Process.Start("chmod", $"755 \"{fullPathToFile}\"");
                if (process == null)
                {
                    throw new NullReferenceException("Unable to run chmod on warp packer");
                }

                process.WaitForExit();
            }
        }

        private static Stream GetWarpResource()
        {
            if (AddinConfiguration.Instance.IsLinux)
            {
                return Resource.AsStream("warp.linux-x64.warp-packer");
            }
            else if (AddinConfiguration.Instance.IsMacOS)
            {
                // ReSharper disable once StringLiteralTypo
                return Resource.AsStream("warp.macos-x64.warp-packer");
            }
            else if (AddinConfiguration.Instance.IsWindows)
            {
                return Resource.AsStream("warp.windows-x64.warp-packer.exe");
            }
            else
            {
                throw new PlatformNotSupportedException("The resource file was not found for the current platform");
            }
        }
    }
}
