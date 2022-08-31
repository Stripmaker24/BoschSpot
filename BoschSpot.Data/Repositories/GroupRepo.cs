using BoschSpot.Data.Models;
using BoschSpot.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BoschSpot.Data.Repositories
{
    public class GroupRepo : GenericRepo<GroupsModel>, IGroupRepo
    {
        public GroupRepo(AppDbContext db): base(db)
        {}

        public IEnumerable<GroupsModel> GetGroupsOfCategory(int ID)
        {
            var Group = _db.Group.Where( g => g.CategoryID == ID).ToList();
            return Group;
        }
    }
}
