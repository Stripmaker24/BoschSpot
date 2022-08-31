using BoschSpot.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoschSpot.Data.Repositories.Interfaces
{
    public interface IRarityRepo : IGenericRepo<RarityModel>
    {
        int GetPointsOfRarity(int ID);
    }
}
