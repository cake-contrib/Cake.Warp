namespace Cake.Warp
{
    /// <summary>
    /// Module class responible for the logic needed
    /// to run when the library is loaded.
    /// </summary>
    public static class ModuleInitializer
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
        public static void Initialize()
        {
            // TODO
        }
    }
}
