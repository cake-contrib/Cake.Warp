namespace Cake.Warp
{
    using Cake.Core;
    using Cake.Core.Annotations;
    using Cake.Core.IO;
    using JetBrains.Annotations;

    /// <summary>
    /// <para>
    /// Runs the warp binary (embedded in addin) to create a
    /// stand-alone packed binary for supported platforms.
    /// </para>
    /// <para>
    /// In order to use the commands for this addin, you will need
    /// to include the following in your cake script.
    /// </para>
    /// <code>
    /// #addin nuget:?package=Cake.Warp&amp;version=0.1.0
    /// </code>
    /// </summary>
    [PublicAPI]
    [CakeAliasCategory("Compilation")]
    public static class WarpAliases
    {
        /// <summary>
        /// Runs the warp packer binary on the specified
        /// <paramref name="inputDirectory" />, and outputs the
        /// resulting packed binary to <paramref name="outputFilePath" />.
        /// </summary>
        /// <param name="context">The cake context</param>
        /// <param name="inputDirectory">
        /// The directory that should be packed into the resulting binary
        /// file.
        /// </param>
        /// <param name="executableName">
        /// The name of the file that should be executed when the
        /// packed binary is launched by the user. (Needs to already exist
        /// in the <paramref name="inputDirectory" />).
        /// </param>
        /// <param name="outputFilePath">
        /// The location where the created packed binary file should
        /// be created.
        /// </param>
        /// <param name="architecture">
        /// The architecture/platform to create the binary file for.
        /// See <see cref="WarpPlatforms" /> for valid values.
        /// </param>
        /// <example>
        /// This example shows a basic call to create the packed binary,
        /// by first calling dotnet publish to create the program.
        /// <code>
        /// #addin nuget:?package=Cake.Warp&amp;version=0.1.0
        ///
        /// Task("Create-Warp-Binary")
        ///     .Does(() =>
        /// {
        ///     DotNetCorePublish("./src/Cake.Warp.Demo", new DotNetCorePublishSettings {
        ///         Framework         = "netcoreapp2.0",
        ///         RuntimeIdentifier = "linux-x64",
        ///         Configuration     = "Release",
        ///         OutputDirectory   = "./artifacts/output"
        ///     });
        ///     Warp("./artifacts/output",
        ///          "Cake.Warp.Demo", // Must include .exe if creating for windows
        ///          "./artifacts/cake-warp-demo",
        ///          WarpPlatforms.LinuxX64
        ///     );
        /// });
        /// </code>
        /// </example>
        [CakeMethodAlias]
        public static void Warp(
            this ICakeContext context,
            DirectoryPath inputDirectory,
            string executableName,
            FilePath outputFilePath,
            WarpPlatforms architecture
        )
        {
            Warp(context, new WarpSettings
            {
                InputDirectory = inputDirectory,
                ExecutableName = executableName,
                OutputFilePath = outputFilePath,
                Architecture   = architecture
            });
        }

        /// <summary>
        /// Runs the warp packer binary with the specified
        /// <paramref name="settings" />.
        /// </summary>
        /// <param name="context">The cake context</param>
        /// <param name="settings">
        /// The settings to use when creating the packed binary file.
        /// </param>
        /// <example>
        /// This example shows a basic call to create the packed binary,
        /// by first calling dotnet publish to create the program.
        /// <code>
        /// #addin nuget:?package=Cake.Warp&amp;version=0.1.0
        ///
        /// Task("Create-Warp-Binary")
        ///     .Does(() =>
        /// {
        ///     DotNetCorePublish("./src/Cake.Warp.Demo", new DotNetCorePublishSettings {
        ///         Framework         = "netcoreapp2.0",
        ///         RuntimeIdentifier = "win-x64",
        ///         Configuration     = "Release",
        ///         OutputDirectory   = "./artifacts/output"
        ///     });
        ///     Warp(new WarpSettings {
        ///         InputDirectory = "./artifacts/output",
        ///         ExecutableName = "Cake.Warp.Demo.exe,
        ///         OutputFilePath = "./artifacts/cake-warp-demo",
        ///         Architecture   = WarpPlatform.WindowsX64"
        ///     });
        /// });
        /// </code>
        /// </example>
        [CakeMethodAlias]
        public static void Warp(
            this ICakeContext context,
            WarpSettings settings
        )
        {
            var runner = new WarpRunner(
                context.FileSystem,
                context.Environment,
                context.ProcessRunner,
                context.Tools
            );
            runner.Run(settings);
        }
    }
}
