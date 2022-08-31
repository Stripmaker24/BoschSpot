using BoschSpot.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoschSpot.Data.Repositories
{
    public class GenericRepo<T> : IGenericRepo<T> where T : class 
    {
        protected readonly AppDbContext _db;

        public GenericRepo(AppDbContext db)
        {
            _db = db;
        }

        public IEnumerable<T> GetAll()
        {
            var list = _db.Set<T>().ToList();
            return list;
        }

        public T GetSingle(int id)
        {
            var single = _db.Set<T>().Find(id);
            return single;
        }
    }
}
