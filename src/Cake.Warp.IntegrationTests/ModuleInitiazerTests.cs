namespace Cake.Warp.IntegrationTests
{
    using System;
    using System.IO;
    using NUnit.Framework;

    [TestFixture]
    [Category("Integration")]
    public class ModuleInitializerTests
    {
        private string addinAssemblyDirectory;

        [SetUp]
        public void SetAddinAssemblyDirectory()
        {
            string directory = typeof(Cake.Warp.ModuleInitializer).Assembly.Location;
            var attr = File.GetAttributes(directory);
            if ((attr & FileAttributes.Directory) != FileAttributes.Directory)
            {
                directory = Path.GetDirectoryName(directory);
            }
            addinAssemblyDirectory = directory;
        }

        // See <https://github.com/nunit/docs/wiki/Platform-Attribute> for a partial list of valid platform values
        [TestCase("warp-packer", IncludePlatform = "Unix", ExpectedResult = true)]
        [TestCase("warp-packer.exe", IncludePlatform = "Win", ExpectedResult = true)]
        [TestCase("warp-packer", IncludePlatform = "Win", ExpectedResult = false,
            TestName = "Should_Not_Have_Saved_Incorrect_Platform_Executable_To_Assembly_Path")]
        [TestCase("warp-packer.exe", IncludePlatform = "Unix", ExpectedResult = false,
            TestName = "Should_Not_Have_Saved_Incorrect_Platform_Executable_To_Assembly_Path")]
        public bool Should_Have_Saved_Correct_Executable_To_Assembly_Path(string runnerName)
        {
            // The file should have been saved to path already in the setup method
            var expectedPath = Path.Combine(addinAssemblyDirectory, runnerName);

            return File.Exists(expectedPath);
        }
    }
}
