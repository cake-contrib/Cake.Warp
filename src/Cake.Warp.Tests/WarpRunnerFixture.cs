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
