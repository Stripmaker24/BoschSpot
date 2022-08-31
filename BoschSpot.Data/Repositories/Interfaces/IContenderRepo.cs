using BoschSpot.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoschSpot.Data.Repositories.Interfaces
{
    public interface IContenderRepo : IGenericRepo<ContenderModel>
    {
        IEnumerable<ContenderModel> GetContendersOfGroup(int group);
        List<ContenderModel> GetContendersOfCategory(int ID);
    }
}
