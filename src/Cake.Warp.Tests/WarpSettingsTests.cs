namespace Cake.Warp.Tests
{
    using System;
    using System.IO;
    using NUnit.Framework;

    public class WarpSettingsTests
    {

        [Test]
        public void Constructor_Should_Set_ToolPath_To_Warp_Binary_On_Default()
        {
            var settings = new WarpSettings();
            var expectedDir = Path.GetDirectoryName(
                settings.GetType().Assembly.Location);
            var expectedFilename = "warp-packer"
                + (Environment.OSVersion.Platform == PlatformID.Win32NT
                   ? ".exe" : "");
            var expectedPath = Path.Combine(expectedDir, expectedFilename);

            Assert.That(settings.ToolPath.FullPath,
                Is.Not.Null.And.SamePath(expectedPath));
        }
    }
}
