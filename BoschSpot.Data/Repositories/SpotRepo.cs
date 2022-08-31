using BoschSpot.Data.Models;
using BoschSpot.Data.Models.Interfaces;
using BoschSpot.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoschSpot.Data.Repositories
{
    public class SpotRepo : GenericRepo<SpotModel>, ISpotRepo
    {
        public SpotRepo(AppDbContext db) : base(db)
        { }
        public void CreateSpot(SpotModel Spot)
        {
            _db.Spot.Add(Spot);
            _db.SaveChanges();
        }

        public IEnumerable<SpotModel> GetSpotsOfUser(string ID) 
        {
            var Spots = _db.Spot.Where(s => s.AccountID == ID).ToList();
            return Spots;
        }
    }
}
