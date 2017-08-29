namespace Plugin.MapsPlugin.Abstractions
{
    /// <summary>
    /// Type of navigation to initiate.
    /// </summary>
    public enum NavigationType
    {
        /// <summary>
        /// OS Default (usually driving)
        /// </summary>
        Default,
        /// <summary>
        /// Driving navigation
        /// </summary>
        Driving,
        /// <summary>
        /// Walking navigation
        /// </summary>
        Walking
    }
}
