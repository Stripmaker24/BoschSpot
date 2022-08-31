using BoschSpot.Data.Models;
using BoschSpot.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoschSpot.Data.Repositories
{
    public class CategoryRepo : GenericRepo<CategoryModel>, ICategoryRepo
    {
        public CategoryRepo(AppDbContext db):base(db)
        {}
    }
}
