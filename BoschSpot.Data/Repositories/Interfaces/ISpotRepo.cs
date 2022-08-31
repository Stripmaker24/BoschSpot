using BoschSpot.Data.Models;
using BoschSpot.Data.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoschSpot.Data.Repositories.Interfaces
{
    public interface ISpotRepo : IGenericRepo<SpotModel>
    {
        void CreateSpot(SpotModel Spot);
        IEnumerable<SpotModel> GetSpotsOfUser(string ID);
    }
}
