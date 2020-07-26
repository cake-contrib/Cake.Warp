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
    using System;
    using Cake.Core;
    using Cake.Core.IO;
    using Cake.Testing;
    using Cake.Warp.Common;
    using NUnit.Framework;

    [TestFixture]
    [TestOf(typeof(WarpRunner))]
    public class WarpRunnerTests
    {
        [Test]
        public void Should_Throw_ArgumentNullException_If_Settings_Is_Null()
        {
            void FixtureResult()
            {
                var fixture = new WarpRunnerFixture { Settings = null };

                fixture.Run();
            }

            Assert.That(FixtureResult,
                Throws.ArgumentNullException
                    .With.Message.Contains("settings"));
        }

        [Test]
        public void Should_Use_Assembly_Warp_File_If_No_ToolPath_Is_Overridden()
        {
            var fixture = new WarpRunnerFixture();
            fixture.GivenExpectedSettings();

            var result = fixture.Run();

            Assert.That(result.Path.FullPath,
                Is.Not.Null
                    .And.SamePath(AddinConfiguration.Instance.WarpFilePath));
        }

        [Test]
        public void Should_Throw_If_Warp_Executable_Was_Not_Found()
        {
            void FixtureResult()
            {
                var fixture = new WarpRunnerFixture();
                fixture.GivenExpectedSettingsWithoutTool();
                fixture.Run();
            }

            Assert.That(FixtureResult,
                Throws.TypeOf<CakeException>()
                    .With.Message.EqualTo(
                        "Warp Packer: Could not locate executable."
                    ));
        }

        [Test]
        public void Should_Throw_If_Process_Has_Non_Zero_Exit_Code()
        {
            void FixtureResult()
            {
                var fixture = new WarpRunnerFixture();
                fixture.GivenExpectedSettings();
                fixture.GivenProcessExitsWithCode(1);
                fixture.Run();
            }

            Assert.That(FixtureResult,
                Throws.TypeOf<CakeException>()
                    .With.Message.EqualTo(
                        "Warp Packer: Process returned an error (exit code 1)."));
        }

        [TestCase("/bin/tools/Warp/warp-packer", ExpectedResult = "/bin/tools/Warp/warp-packer")]
        [TestCase("/bin/tools/Warp/warp-packer.exe", ExpectedResult = "/bin/tools/Warp/warp-packer.exe")]
        [TestCase("./tools/Warp/warp-packer", ExpectedResult = "/Working/tools/Warp/warp-packer")]
        [TestCase("./tools/Warp/warp-packer.exe", ExpectedResult = "/Working/tools/Warp/warp-packer.exe")]
        public string Should_Use_Warp_Runner_From_Tool_Path_If_Provided(
            string toolPath)
        {
            var fixture = new WarpRunnerFixture();
            fixture.GivenExpectedSettings();
            fixture.Settings.ToolPath = toolPath;
            fixture.GivenSettingsToolPathExist();

            var result = fixture.Run();

            Assert.That(result.Path, Is.Not.Null);
            return result.Path.FullPath;
        }

        [TestCase("C:/Warp/warp.exe", ExpectedResult = "C:/Warp/warp.exe", IncludePlatform = "win")]
        public string Should_Use_Warp_Runner_From_Tool_Path_If_Provided_OnWindows(
            string toolPath)
        {
            var fixture = new WarpRunnerFixture();
            fixture.GivenExpectedSettings();
            fixture.Settings.ToolPath = toolPath;
            fixture.GivenSettingsToolPathExist();

            var result = fixture.Run();

            Assert.That(result.Path, Is.Not.Null);
            return result.Path.FullPath;
        }

        [TestCase(WarpPlatforms.LinuxX64, "linux-x64")]
        [TestCase(WarpPlatforms.MacOSX64, "macos-x64")]
        [TestCase(WarpPlatforms.WindowsX64, "windows-x64")]
        public void Should_Set_Architecture(WarpPlatforms platform, string expected)
        {
            var fixture = new WarpRunnerFixture();
            fixture.GivenExpectedSettings();
            fixture.Settings.Platform = platform;

            var result = fixture.Run();

            Assert.That(result.Args, Does.Contain("--arch " + expected));
        }

        [Test]
        public void Should_Set_InputDirectory()
        {
            var fixture = new WarpRunnerFixture();
            fixture.GivenExpectedSettings();
            Assume.That(fixture.Settings.InputDirectory, Is.Not.Null);

            var result = fixture.Run();
            var expectedDirectory = new FilePath(Environment.CurrentDirectory);

            Assert.That(result.Args, Does.Contain($"--input_dir \"{expectedDirectory}\""));
        }

        [Test]
        public void Should_Set_ExecFileName()
        {
            var fixture = new WarpRunnerFixture();
            fixture.GivenExpectedSettings();
            Assume.That(fixture.Settings.ExecutableName, Is.Not.Null.And.Not.Empty);

            var result = fixture.Run();

            Assert.That(result.Args, Does.Contain("--exec \"Cake.Warp.Demo.exe\""));
        }

        [Test]
        public void Should_Set_OutputPath()
        {
            var fixture = new WarpRunnerFixture();
            fixture.GivenExpectedSettings();
            Assume.That(fixture.Settings.InputDirectory, Is.Not.Null);

            var result = fixture.Run();

            Assert.That(result.Args, Does.Contain("--output \"cake-warp-demo\""));
        }

        [Test]
        public void Should_Throw_ArgumentNullException_When_InputDir_Is_Null()
        {
            void FixtureResult()
            {
                var fixture = new WarpRunnerFixture();
                fixture.GivenExpectedSettings();
                fixture.Settings.InputDirectory = null;

                fixture.Run();
            }

            Assert.That(FixtureResult, Throws.ArgumentNullException
                .With.Message.Contains(nameof(WarpSettings.InputDirectory)));
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("            ")]
        public void Should_Throw_ArgumentNullException_When_ExecutableName_Is_Null_Or_Empty(string value)
        {
            void FixtureResult()
            {
                var fixture = new WarpRunnerFixture();
                fixture.GivenExpectedSettings();
                fixture.Settings.ExecutableName = value;

                fixture.Run();
            }

            Assert.That(FixtureResult, Throws.ArgumentNullException
                .With.Message.Contains(nameof(WarpSettings.ExecutableName)));
        }

        [Test]
        public void Should_Throw_ArgumentNullException_When_OutputFilePath_Is_Null()
        {
            void FixtureResult()
            {
                var fixture = new WarpRunnerFixture();
                fixture.GivenExpectedSettings();
                fixture.Settings.OutputFilePath = null;

                fixture.Run();
            }

            Assert.That(FixtureResult, Throws.ArgumentNullException
                .With.Message.Contains(nameof(WarpSettings.OutputFilePath)));
        }
    }
}
