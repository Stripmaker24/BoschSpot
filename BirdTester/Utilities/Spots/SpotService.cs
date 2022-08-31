using BoschSpot.Utilities.Geolocation;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Geocoding;
using BoschSpot.Utilities.Accounts;
using BoschSpot.Data.Repositories.Interfaces;
using BoschSpot.Data.Models;
using BoschSpot.Data.Models.Interfaces;
using BoschSpot.App.ViewModels;

namespace BoschSpot.Utilities.Spots
{
    public class SpotService : ISpotService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ISpotRepo _spotRepo;
        private readonly IGeoService _geoService;
        private readonly IContenderRepo _contenderRepo;
        private readonly IRarityRepo _rarityRepo;
        private readonly IAccountService _accountService;
        private readonly ICategoryRepo _categoryRepo;
        private readonly IGroupRepo _groupRepo;

        public SpotService(IWebHostEnvironment webHostEnvironment, 
            ISpotRepo spotRepo, 
            IGeoService geoService, 
            IContenderRepo contenderRepo, 
            IRarityRepo rarityRepo,
            IAccountService accountService,
            ICategoryRepo categoryRepo,
            IGroupRepo groupRepo)
        {
            _webHostEnvironment = webHostEnvironment;
            _spotRepo = spotRepo;
            _geoService = geoService;
            _contenderRepo = contenderRepo;
            _rarityRepo = rarityRepo;
            _accountService = accountService;
            _categoryRepo = categoryRepo;
            _groupRepo = groupRepo;
        }

        public SpotsWithTotalScoreViewModel GetSpotsWithTotalScore(List<SpotReadViewModel> list) 
        {
            int totalscore = 0;
            foreach (var Spot in list)
            {
                totalscore += Spot.Spot.Points;
            }

            var SpotsWithTotalScore = new SpotsWithTotalScoreViewModel
            {
                Spots = list,
                TotalScore = totalscore
            };

            return SpotsWithTotalScore;
        }

        public async Task<bool> CreateSpot(ISpotCreateViewModel spot)
        {
            var accountID = _accountService.GetCurrentUserID();
            string UniqueFileName = null;
            if (spot.Photo != null)
            {
                string uploadFolder = Path.Combine(_webHostEnvironment.WebRootPath, "SpotImg");
                UniqueFileName = Guid.NewGuid().ToString() + "_" + spot.Photo.FileName;
                string filePath = Path.Combine(uploadFolder, UniqueFileName);
                spot.Photo.CopyTo(new FileStream(filePath, FileMode.Create));
            }
            var addresses = await _geoService.GetAddresses(spot.Address);
            var location = addresses.First().Coordinates;
            var Points = PointCalculation(spot.ContenderID, location, accountID);
            var newSpot = new SpotModel
            {
                ContenderID = spot.ContenderID,
                AccountID = accountID,
                PhotoName = UniqueFileName,
                Long = location.Longitude,
                Lat = location.Latitude,
                Points = Points,
                SpotTimeDate = DateTime.Now
            };
            if (addresses.First().AdminDistrict2.Equals("'s-Hertogenbosch"))
            {
                _spotRepo.CreateSpot(newSpot);
                return true;
            }
            return false;
        }

        public IEnumerable<SpotReadViewModel> GetMySpots()
        {
            var accountID = _accountService.GetCurrentUserID();
            var Spots = GettAllSpotsOfUserWithInfo(accountID);
            return Spots;
        }

        private int PointCalculation(int contenderID, Location location, string accountID) 
        {
            int totalPoints = 0;
            var Contender = _contenderRepo.GetSingle(contenderID);
            totalPoints += _rarityRepo.GetPointsOfRarity(Contender.RarityID);
            if (Contender.IsInDanger) 
            {
                totalPoints += 1;
            }
            var SpotList = GettAllSpotsOfUserWithInfo(accountID).ToList();
            if (!SpotList.Any(s => s.ContenderName == Contender.Name)) 
            {
                totalPoints += 2;
            }
            if (!SpotList.Any(s => s.Spot.Lat == location.Latitude && s.Spot.Long == location.Longitude)) 
            {
                totalPoints += 3;
            }
            return totalPoints;
        }

        public IEnumerable<SpotReadViewModel> GetAllSpotsWithInfo()
        {
            var Spots = _spotRepo.GetAll();
            var ReadSpots = CreateSpotList(Spots);
            return ReadSpots;
        }
        public IEnumerable<SpotReadViewModel> GettAllSpotsOfUserWithInfo(string ID)
        {
            var Spots = _spotRepo.GetSpotsOfUser(ID);
            var ReadSpots = CreateSpotList(Spots);
            return ReadSpots;
        }

        private IEnumerable<SpotReadViewModel> CreateSpotList(IEnumerable<SpotModel> Spots)
        {
            var ReadSpots = new List<SpotReadViewModel>();
            foreach (var spot in Spots)
            {
                var contender = _contenderRepo.GetSingle(spot.ContenderID);
                var group = _groupRepo.GetSingle(contender.GroupID);
                var category = _categoryRepo.GetSingle(group.CategoryID);
                var rarity = _rarityRepo.GetSingle(contender.RarityID);

                var ReadSpot = new SpotReadViewModel
                {
                    Spot = spot,
                    ContenderName = contender.Name,
                    GroupName = group.Group,
                    CategoryName = category.Category,
                    RarityType = rarity.Rarity
                };
                ReadSpots.Add(ReadSpot);
            }
            return ReadSpots;
        }
    }
}
