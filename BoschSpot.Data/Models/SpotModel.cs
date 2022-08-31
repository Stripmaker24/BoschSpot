using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BoschSpot.Data.Models
{
    public class SpotModel
    {
        [Required]
        public int ID { get; set; }
        [Required]
        public int ContenderID { get; set; }
        [Required]
        public string PhotoName { get; set; }
        [Required]
        public double Long { get; set; }
        [Required]
        public double Lat { get; set; }
        [Required]
        public string AccountID { get; set; }
        [Required]
        public int Points { get; set; }
        [Required]
        public DateTime SpotTimeDate { get; set; }

        [JsonIgnore]
        public ContenderModel Contender { get; set; }
        [JsonIgnore]
        public AccountModel Account { get; set; }
    }
}
