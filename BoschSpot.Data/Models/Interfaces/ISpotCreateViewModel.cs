using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoschSpot.Data.Models.Interfaces
{
    public interface ISpotCreateViewModel
    {
        public ContenderModel Contender { get; set; }
        public int ContenderID { get; set; }
        public IFormFile Photo { get; set; }
        public string Address { get; set; }
    }
}
