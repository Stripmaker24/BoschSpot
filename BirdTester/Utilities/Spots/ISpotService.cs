using BoschSpot.App.ViewModels;
using BoschSpot.Data.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoschSpot.Utilities.Spots
{
    public interface ISpotService
    {
        SpotsWithTotalScoreViewModel GetSpotsWithTotalScore(List<SpotReadViewModel> list);
        Task<bool> CreateSpot(ISpotCreateViewModel spot);
        IEnumerable<SpotReadViewModel> GetMySpots();
        IEnumerable<SpotReadViewModel> GetAllSpotsWithInfo();
        IEnumerable<SpotReadViewModel> GettAllSpotsOfUserWithInfo(string ID);
    }
}
