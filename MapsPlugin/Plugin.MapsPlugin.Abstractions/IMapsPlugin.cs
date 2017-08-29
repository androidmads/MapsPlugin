using System.Threading.Tasks;

namespace Plugin.MapsPlugin.Abstractions
{
    /// <summary>
    /// Interface for MapsPlugin
    /// </summary>
    public interface IMapsPlugin
    {
        /// <summary>
        /// Add Pin to specific latitude and longitude.
        /// </summary>
        /// <param name="name">Label to display</param>
        /// <param name="latitude">Lat</param>
        /// <param name="longitude">Long</param>
        /// <param name="zoomLevel">Type of navigation</param>
        Task<bool> PinTo(string name, double latitude, double longitude, int zoomLevel);

        /// <summary>
        /// Navigate to specific latitude and longitude.
        /// </summary>
        /// <param name="name">Label to display</param>
        /// <param name="latitude">Lat</param>
        /// <param name="longitude">Long</param>
        /// <param name="navigationType">Type of navigation</param>
        Task<bool> NavigateTo(string name, double latitude, double longitude, NavigationType navigationType = NavigationType.Default);

        /// <summary>
        /// Navigate to an address
        /// </summary>
        /// <param name="name">Label to display</param>
        /// <param name="street">Street</param>
        /// <param name="city">City</param>
        /// <param name="state">Sate</param>
        /// <param name="zip">Zip</param>
        /// <param name="country">Country</param>
        /// <param name="countryCode">Country Code if applicable</param>
        /// <param name="navigationType">Navigation type</param>
        Task<bool> NavigateTo(string name, string street, string city, string state, string zip, string country, string countryCode, NavigationType navigationType = NavigationType.Default);

    }
}
