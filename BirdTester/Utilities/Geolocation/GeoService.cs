using Geocoding;
using Geocoding.Microsoft;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoschSpot.Utilities.Geolocation
{
    public class GeoService : IGeoService
    {
        public async Task<IEnumerable<BingAddress>> GetAddresses(string address)
        {
            List<Location> locations = new List<Location>();
            BingMapsGeocoder geocoder = new BingMapsGeocoder("AsYgyjep7Nsq3mqhIjbO2iZneLdbOuLUAW3iG7rIsIZyq88DOPUyuRjkCL7cqOCg");
            IEnumerable<BingAddress> addresses = await geocoder.GeocodeAsync(address);
            return addresses;
        }
    }
}
