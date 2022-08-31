using BoschSpot.App.ViewModels;
using BoschSpot.Data.Models;
using BoschSpot.Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoschSpot.Controllers
{
    public class HandbookController : Controller
    {
        private readonly IContenderRepo _contenderRepo;
        private readonly IGroupRepo _groupRepo;
        private readonly ICategoryRepo _categoryRepo;

        public HandbookController( IContenderRepo contenderRepo, IGroupRepo groupRepo, ICategoryRepo categoryRepo)
        {
            _contenderRepo = contenderRepo;
            _groupRepo = groupRepo;
            _categoryRepo = categoryRepo;
        }

        public IActionResult Index()
        {
            var Categories = _categoryRepo.GetAll();
            return View("Index", Categories);
        }

        public IActionResult Groups(int ID) 
        {
            var Groups = _groupRepo.GetGroupsOfCategory(ID);
            return View("Groups", Groups);
        }

        public IActionResult GroupList(int? ID, int CategoryID) 
        {
            IEnumerable<ContenderModel> Contenders;
            if (ID != null)
            {
                Contenders = _contenderRepo.GetContendersOfGroup(ID.Value);
            }
            else 
            {
                Contenders = _contenderRepo.GetAll();
            }
            var model = new GroupsWithCategoryIDViewModel()
            {
                Contenders = Contenders,
                CategoryID = CategoryID
            };
            return View("GroupList", model);
        }

        public IActionResult Contender(int ID) 
        {
            var Bird = _contenderRepo.GetSingle(ID);
            return View("Contender", Bird);
        }
    }
}
