using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BoschSpot.Data.Models
{
    public class GroupsModel
    {
        [Required]
        public int ID { get; set; }
        [Required]
        public string Group { get; set; }
        [Required]
        public int CategoryID { get; set; }

        public List<ContenderModel> Contenders { get; set; }
        public CategoryModel Category { get; set; }
    }
}
