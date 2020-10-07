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
    public static class AddinInitializer
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
