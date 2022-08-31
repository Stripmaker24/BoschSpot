using BoschSpot.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BoschSpot.Data.Repositories.Interfaces
{
    public interface IGroupRepo: IGenericRepo<GroupsModel>
    {
        IEnumerable<GroupsModel> GetGroupsOfCategory(int ID);
    } 
}
