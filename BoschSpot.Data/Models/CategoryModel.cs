using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BoschSpot.Data.Models
{
    public class CategoryModel
    {
        [Required]
        public int ID { get; set; }
        [Required]
        public string Category { get; set; }

        public List<GroupsModel> Groups { get; set; }
    }
}
