using BoschSpot.Data.Models;
using BoschSpot.Data.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoschSpot.App.ViewModels
{
    public class SpotReadViewModel
    {
        public SpotModel Spot { get; set; }
        public string ContenderName { get; set; }
        public string CategoryName { get; set; }
        public string GroupName { get; set; }
        public string RarityType { get; set; }
    }
}
