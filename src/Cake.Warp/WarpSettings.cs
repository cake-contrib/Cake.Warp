/*
 * MIT License
 *
 * Copyright (c) 2019-2020 Kim J. Nordmo and Contributors
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 */

namespace Cake.Warp
{
    using Cake.Core.IO;
    using Cake.Core.Tooling;
    using Cake.Warp.Common;
    using JetBrains.Annotations;

    /// <summary>
    /// Contains the settings used by <see cref="WarpRunner" />.
    /// </summary>
    /// <remarks>
    /// All properties are required
    /// (except <see cref="Platform" /> as this as just an alias
    /// for <see cref="Architecture" />).
    /// </remarks>
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
        [PublicAPI]
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
        /// This should only be the filename including the file extension.
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
