using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BoschSpot.Data.Models
{
    public class AccountModel
    {
        [Required]
        public string ID { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }

        public List<SpotModel> Spots { get; set; }
    }
}
