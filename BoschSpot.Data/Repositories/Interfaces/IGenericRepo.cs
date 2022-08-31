using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoschSpot.Data.Repositories.Interfaces
{
    public interface IGenericRepo<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetSingle(int id);
    }
}
