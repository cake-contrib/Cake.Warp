namespace Cake.Warp.IntegrationTests
{
    using System.IO;
    using NUnit.Framework;

    [TestFixture]
    [Category("Integration")]
    public class ModuleInitializerTests
    {
        // See <https://github.com/nunit/docs/wiki/Platform-Attribute> for a partial list of valid platform values
        [TestCase("warp-runner", IncludePlatform = "Unix", ExpectedResult = true)]
        [TestCase("warp-runner", IncludePlatform = "Win", ExpectedResult = true)]
        public bool Test1(string runnerName)
        {
            string directory = typeof(Cake.Warp.ModuleInitializer).Assembly.Location;

            var expectedPath = Path.Combine(directory, runnerName);

            return File.Exists(expectedPath);
        }
    }
}
