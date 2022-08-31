using BoschSpot.App.Utilities.Validation;
using BoschSpot.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BoschSpot.App.ViewModels
{
    public class SpotServerCreateViewModel
    {
        public SpotModel Spot { get; set; }
        [CheckStringAttribute(AllowableValue = "'s-Hertogenbosch")]
        public string Township { get; set; }
    }
}
