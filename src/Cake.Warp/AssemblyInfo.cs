using System.Runtime.CompilerServices;

// Would prefer not to do this, but we have
// the WarpRunner as an internal. Therefor we
// must (unless we want to make use of reflection).
[assembly: InternalsVisibleTo("Cake.Warp.Tests")]
