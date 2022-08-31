using Geocoding;
using Geocoding.Microsoft;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoschSpot.Utilities.Geolocation
{
    public interface IGeoService
    {
        Task<IEnumerable<BingAddress>> GetAddresses(string address);
    }
}
