using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BoschSpot.Data.Models
{
    public class ContenderModel
    {
        [Required]
        public int ID { get; set; }
        [Required]
        public string LatinName { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public bool IsInDanger { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Recognition { get; set; }
        [Required]
        public int GroupID { get; set; }
        public string PhotoUrl { get; set; }
        public string Source { get; set; }
        [Required]  
        public int RarityID { get; set; }

        public List<SpotModel> Spots { get; set; }
        public RarityModel Rarity { get; set; }
        public GroupsModel Groups { get; set; }
    }
}
