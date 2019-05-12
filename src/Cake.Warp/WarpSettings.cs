namespace Cake.Warp
{
    using Cake.Core.IO;
    using Cake.Core.Tooling;
    using Cake.Warp.Common;

    /// <summary>
    /// Contains the settings used by <see cref="WarpRunner" />.
    /// </summary>
    public sealed class WarpSettings : ToolSettings
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WarpSettings" /> class,
        /// with ToolPath set to the extracted warp binary file.
        /// </summary>
        /// <remarks>ToolPath can be overridden if desired.</remarks>
        public WarpSettings()
        {
            this.ToolPath = AddinConfiguration.Instance.WarpFilePath;
        }

        /// <summary>
        /// Gets or sets the platform and architecture to create the
        /// self-contained application for.
        /// </summary>
        public WarpPlatforms Architecture { get; set; }

        /// <summary>
        /// Gets or sets the platform and architecture to create the
        /// self-contained application for.
        /// </summary>
        /// <remarks>
        /// This is just an alias for <see cref="Architecture" />.
        /// </remarks>
        public WarpPlatforms Platform
        {
            get => this.Architecture;
            set => this.Architecture = value;
        }

        /// <summary>
        /// Gets or sets the name of the executable to run after the
        /// the created application is being executed by a user.
        /// </summary>
        /// <remarks>
        /// This should only be the filaname including the file extension.
        /// </remarks>
        public string ExecutableName { get; set; }

        /// <summary>
        /// Gets or sets the directory that will be packed into the
        /// self-contained application.
        /// </summary>
        public DirectoryPath InputDirectory { get; set; }

        /// <summary>
        /// Gets or sets the output path to where the self-contained application
        /// should be created.
        /// </summary>
        public FilePath OutputFilePath { get; set; }
    }
}
