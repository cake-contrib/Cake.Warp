namespace Cake.Warp.Common
{
    using System;
    using System.IO;
    using System.Reflection;
    using System.Runtime.InteropServices;

    /// <summary>
    ///   Configuration class responsible for holding
    ///   Addin values only used internally.
    /// </summary>
    internal sealed class AddinConfiguration
    {
        private static AddinConfiguration instance;

        private AddinConfiguration()
        {
            this.AssemblyDirectoryPath = Assembly.GetExecutingAssembly().Location;
            var attr = File.GetAttributes(this.AssemblyDirectoryPath);
            if ((attr & FileAttributes.Directory) != FileAttributes.Directory)
            {
                this.AssemblyDirectoryPath = Path.GetDirectoryName(this.AssemblyDirectoryPath);
            }
        }

        /// <summary>
        ///   Gets the singleton instance of the addin configuration class.
        /// </summary>
        public static AddinConfiguration Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AddinConfiguration();
                }

                return instance;
            }
        }

        /// <summary>
        ///   Gets the current path to the executing assembly directory.
        /// </summary>
        public string AssemblyDirectoryPath { get; }

        /// <summary>
        ///   Gets or sets the full file path to the warp binary file.
        /// </summary>
        /// <notes>
        ///   This should/will be set in the
        ///   <see cref="ModuleInitializer.Initialize" />
        ///   during library loading.
        /// </notes>
        public string WarpFilePath { get; set; }

        /// <summary>
        ///   Gets a value indicating whether we are running
        ///   on the Linux platform or not.
        /// </summary>
        public bool IsLinux { get; }
            = RuntimeInformation.IsOSPlatform(OSPlatform.Linux);

        /// <summary>
        ///   Gets a value indicating whether we are running
        ///   on the Mac OSX platform or not.
        /// </summary>
        public bool IsMacOS { get; }
            = RuntimeInformation.IsOSPlatform(OSPlatform.OSX);

        /// <summary>
        ///   Gets a value indicating whether we are running
        ///   on the Windows platform or not.
        /// </summary>
        public bool IsWindows { get; }
            = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
    }
}
