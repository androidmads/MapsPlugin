using Plugin.MapsPlugin.Abstractions;
using System;
using Uri = Android.Net.Uri;
using Debug = System.Diagnostics.Debug;
using Android.Content;
using Android.App;
using System.Threading.Tasks;
using System.Globalization;

namespace Plugin.MapsPlugin
{
    /// <summary>
    /// Implementation for Feature
    /// </summary>
    public class MapsPluginImplementation : IMapsPlugin
    {
        /// <summary>
        /// Navigate to a particular Coordinate
        /// </summary>
        /// <param name="name">Label to display</param>
        /// <param name="latitude">Latitude</param>
        /// <param name="longitude">Longitude</param>
        /// <param name="zoomLevel">Zoom Level</param>
        public Task<bool> PinTo(string name, double latitude, double longitude, int zoomLevel)
        {
            try
            {
                string uriBegin = "geo:" + latitude + "," + longitude;
                string query = latitude + "," + longitude + "(" + name + ")";
                string encodedQuery = Uri.Encode(query);
                string uriString = uriBegin + "?q=" + encodedQuery + "&z=" + zoomLevel;
                Uri uri = Uri.Parse(uriString);
                Intent intent = new Intent(Intent.ActionView, uri);
                if (TryIntent(intent))
                    return Task.FromResult(true);
            }
            catch (Exception e)
            {

            }
            return Task.FromResult(false);
        }

        /// <summary>
        /// Navigate to a particular Coordinate
        /// </summary>
        /// <param name="name">Label to display</param>
        /// <param name="latitude">Latitude</param>
        /// <param name="longitude">Longitude</param>
        /// <param name="navigationType">Navigation type</param>
        public Task<bool> NavigateTo(string name, double latitude, double longitude, NavigationType navigationType = NavigationType.Default)
        {
            var uri = string.Empty;
            if (string.IsNullOrWhiteSpace(name))
            {
                uri = string.Format("http://maps.google.com/maps?&daddr={0},{1}",
                    latitude.ToString(CultureInfo.InvariantCulture),
                    longitude.ToString(CultureInfo.InvariantCulture));
            }
            else
            {
                uri = string.Format("http://maps.google.com/maps?&daddr={0},{1} ({2})", 
                    latitude.ToString(CultureInfo.InvariantCulture),
                    longitude.ToString(CultureInfo.InvariantCulture), name);
            }
            var intent = new Intent(Intent.ActionView, Uri.Parse(uri));

            if (TryIntent(intent))
                return Task.FromResult(true);

            var uri2 = string.Empty;
            if (string.IsNullOrWhiteSpace(name))
                uri2 = string.Format("geo:{0},{1}?q={0},{1}", latitude.ToString(CultureInfo.InvariantCulture), longitude.ToString(CultureInfo.InvariantCulture));
            else
                uri2 = string.Format("geo:{0},{1}?q={0},{1}({2})", latitude.ToString(CultureInfo.InvariantCulture), longitude.ToString(CultureInfo.InvariantCulture), name);

            if (TryIntent(new Intent(Intent.ActionView, Uri.Parse(uri2))))
                return Task.FromResult(true);

            if (TryIntent(new Intent(Intent.ActionView, Uri.Parse(uri))))
                return Task.FromResult(true);

            Debug.WriteLine("No map apps found, unable to navigate");
            return Task.FromResult(false);
        }

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
        public Task<bool> NavigateTo(string name, string street, string city, string state, string zip, string country, string countryCode, NavigationType navigationType = NavigationType.Default)
        {
            if (string.IsNullOrWhiteSpace(name))
                name = string.Empty;
            if (string.IsNullOrWhiteSpace(street))
                street = string.Empty;
            if (string.IsNullOrWhiteSpace(city))
                city = string.Empty;
            if (string.IsNullOrWhiteSpace(state))
                state = string.Empty;
            if (string.IsNullOrWhiteSpace(zip))
                zip = string.Empty;
            if (string.IsNullOrWhiteSpace(country))
                country = string.Empty;

            var uri = String.Format("http://maps.google.com/maps?q={0} {1}, {2} {3} {4}", street, city, state, zip, country);
            var intent = new Intent(Intent.ActionView, Uri.Parse(uri));            

            if (TryIntent(intent))
                return Task.FromResult(true);

            var uri2 = String.Format("geo:0,0?q={0} {1} {2} {3} {4}", street, city, state, zip, country);

            if (TryIntent(new Intent(Intent.ActionView, Uri.Parse(uri2))))
                return Task.FromResult(true);

            if (TryIntent(new Intent(Intent.ActionView, Uri.Parse(uri))))
                return Task.FromResult(true);

            Debug.WriteLine("No map apps found, unable to navigate");
            return Task.FromResult(false);
        }

        private bool TryIntent(Intent intent)
        {
            try
            {
                intent.SetClassName("com.google.android.apps.maps", "com.google.android.maps.MapsActivity");
                intent.SetFlags(ActivityFlags.ClearTop);
                intent.SetFlags(ActivityFlags.NewTask);
                Application.Context.StartActivity(intent);
                return true;
            }
            catch (ActivityNotFoundException)
            {
                return false;
            }
        }

    }
}