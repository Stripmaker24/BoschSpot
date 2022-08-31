using BoschSpot.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoschSpot.App.ViewModels
{
    public class GroupsWithCategoryIDViewModel
    {
        public IEnumerable<ContenderModel> Contenders { get; set; }
        public int CategoryID { get; set; }
    }
}
