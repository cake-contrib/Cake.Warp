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

namespace Cake.Warp.Common
{
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
            => instance ?? (instance = new AddinConfiguration());

        /// <summary>
        ///   Gets the current path to the executing assembly directory.
        /// </summary>
        public string AssemblyDirectoryPath { get; }

        /// <summary>
        ///   Gets or sets the full file path to the warp binary file.
        /// </summary>
        /// <notes>
        ///   This should/will be set in the
        ///   <see cref="AddinInitializer.Initialize" />
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
