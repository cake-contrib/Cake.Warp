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

namespace Cake.Warp.Tests
{
    using Cake.Testing;
    using Cake.Testing.Fixtures;

    internal class WarpRunnerFixture : ToolFixture<WarpSettings>
    {
        public WarpRunnerFixture()
            : base("warp-packer")
        {
        }

        public void GivenExpectedSettings()
        {
            this.SetSettings();
            this.GivenSettingsToolPathExist();
        }

        public void GivenExpectedSettingsWithoutTool()
        {
            this.SetSettings();
            this.GivenDefaultToolDoNotExist();
        }

        protected override void RunTool()
        {
            var tool = new WarpRunner(FileSystem, Environment, ProcessRunner, Tools);
            tool.Run(Settings);
        }

        private void SetSettings()
        {
            this.Settings = new WarpSettings
            {
                Architecture = WarpPlatforms.LinuxX64,
                ExecutableName = "Cake.Warp.Demo.exe",
                InputDirectory = System.Environment.CurrentDirectory,
                OutputFilePath = "./cake-warp-demo",
            };
        }
    }
}
