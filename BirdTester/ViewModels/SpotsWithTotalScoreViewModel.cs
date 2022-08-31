using BoschSpot.Data.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoschSpot.App.ViewModels
{
    public class SpotsWithTotalScoreViewModel
    {
        public IEnumerable<SpotReadViewModel> Spots { get; set; }
        public int TotalScore { get; set; }
    }
}
