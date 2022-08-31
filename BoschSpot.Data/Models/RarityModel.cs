using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BoschSpot.Data.Models
{
    public class RarityModel
    {
        [Required]
        public int ID { get; set; }
        [Required]
        public string Rarity { get; set; }
        [Required]
        public int Points { get; set; }

        public List<ContenderModel> Contenders { get; set; }
    }
}
