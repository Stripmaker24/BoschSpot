using BoschSpot.Data.Models;
using BoschSpot.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoschSpot.Data.Repositories
{
    public class ContenderRepo : GenericRepo<ContenderModel>, IContenderRepo
    {
        public ContenderRepo(AppDbContext db): base(db)
        {
        }

        public List<ContenderModel> GetContendersOfCategory(int ID)
        {
            var Contenders = new List<ContenderModel>();
            if (ID == 0) 
            {
                Contenders = GetAll().ToList(); 
            }
            var Groups = _db.Group.Where(g => g.CategoryID == ID).ToList();
            foreach (var group in Groups)
            {
                var SubContenders = _db.Contender.Where(c => c.GroupID == group.ID).ToList();
                Contenders.AddRange(SubContenders);
            }
            var orderdContenders = Contenders.OrderBy(c => c.Name).ToList();
            return orderdContenders;
        }

        public IEnumerable<ContenderModel> GetContendersOfGroup(int group)
        {
            var Contenders = _db.Contender.Where(b => b.GroupID == group).ToList();
            return Contenders;
        }
    }
}
