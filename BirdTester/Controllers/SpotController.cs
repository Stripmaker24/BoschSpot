using BoschSpot.App.Utilities.ModelState;
using BoschSpot.App.ViewModels;
using BoschSpot.Data.Repositories.Interfaces;
using BoschSpot.Utilities.Accounts;
using BoschSpot.Utilities.Geolocation;
using BoschSpot.Utilities.Spots;
using Geocoding;
using Geocoding.Microsoft;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BoschSpot.Controllers
{
    [Authorize]
    public class SpotController : Controller
    {
        private readonly IContenderRepo _contenderRepo;
        private readonly ISpotService _spotService;
        private readonly ICategoryRepo _categoryRepo;
        private readonly IAccountService _accountService;

        public SpotController(IContenderRepo contenderRepo,  
            ISpotService spotService, 
            ICategoryRepo categoryRepo,
            IAccountService accountService)
        {
            _contenderRepo = contenderRepo;
            _spotService = spotService;
            _categoryRepo = categoryRepo;
            _accountService = accountService;
        }
        public IActionResult Index()
        {
            var UserID = _accountService.GetCurrentUserID();
            var Spots = _spotService.GetSpotsWithTotalScore(_spotService.GetAllSpotsWithInfo().ToList());
            var mySpots = _spotService.GetSpotsWithTotalScore(_spotService.GettAllSpotsOfUserWithInfo(UserID).ToList());
            var AllSpots = new List<SpotsWithTotalScoreViewModel>
            {
                Spots,
                mySpots
            };
            return View(AllSpots);
        }
        [ImportModelState]
        public IActionResult Create(int ID) 
        {
            var contender = _contenderRepo.GetSingle(ID);
            if (TempData["Spot"] != null) 
            {
                SpotCreateViewModel spots = JsonConvert.DeserializeObject<SpotCreateViewModel>(TempData["Spot"].ToString());
                spots.Contender = contender;
                return View(spots);
            }
            return View(new SpotCreateViewModel { Contender = contender, ContenderID = ID });
        }
        [ImportModelState]
        public IActionResult FastCreate() 
        {
            var fastForm = new FastSpotCreateViewModel();
            if (TempData["FastSpot"] != null)
            {
                FastSpotCreateViewModel spots = JsonConvert.DeserializeObject<FastSpotCreateViewModel>(TempData["FastSpot"].ToString());
                return View(spots);
            }
            return View(fastForm);
        }

        [Route("GetSpotsForMap")]
        public JsonResult GetSpotsForMap() 
        {
            var Spots = _spotService.GetAllSpotsWithInfo();
            return new JsonResult(Spots);
        }

        [Route("GetMySpotsForMap")]
        public JsonResult GetMySpotsForMap() 
        {
            var Spots = _spotService.GetMySpots();
            return new JsonResult(Spots);
        }

        [Route("GetCategories")]
        public JsonResult GetCategoriesForFastSpot() 
        {
            var Categories = _categoryRepo.GetAll();
            return new JsonResult(Categories);
        }

        [Route("GetContendersOfCategory")]
        public JsonResult GetContendersOfCategoryForFastSpot(int ID) 
        {
            var Contenders = _contenderRepo.GetContendersOfCategory(ID);
            return new JsonResult(Contenders);
        }
        [HttpPost]
        [ExportModelState]
        public async Task<IActionResult> Create(SpotCreateViewModel scvm) 
        {
            if (ModelState.IsValid) 
            {
                var createdSpot = await _spotService.CreateSpot(scvm);
                if (createdSpot)
                {
                    TempData["Spot"] = null;
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("Address", "Locatie valt buiten de gemeente 's-Hertogenbosch");

            }
            TempData["Spot"] = JsonConvert.SerializeObject(scvm);
            return RedirectToAction("Create", new { ID = scvm.ContenderID });
        }

        [HttpPost]
        [ExportModelState]
        public async Task<IActionResult> FastCreate(FastSpotCreateViewModel fscvm)
        {
            if (ModelState.IsValid)
            {
                var createdSpot = await _spotService.CreateSpot(fscvm);
                if (createdSpot)
                {
                    TempData["FastSpot"] = null;
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("Address", "Locatie valt buiten de gemeente 's-Hertogenbosch");
            }
            TempData["FastSpot"] = JsonConvert.SerializeObject(fscvm);
            return RedirectToAction("FastCreate");
        }
    }
}
