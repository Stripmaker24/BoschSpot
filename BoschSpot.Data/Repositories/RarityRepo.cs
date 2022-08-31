using BoschSpot.Data.Models;
using BoschSpot.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoschSpot.Data.Repositories
{
    public class RarityRepo : GenericRepo<RarityModel>, IRarityRepo
    {

        public RarityRepo(AppDbContext db) : base(db)
        {
        }
        public int GetPointsOfRarity(int ID)
        {
            var Points = _db.Rarity.Where(r => r.ID == ID).First().Points;
            return Points;
        }
    }
}
