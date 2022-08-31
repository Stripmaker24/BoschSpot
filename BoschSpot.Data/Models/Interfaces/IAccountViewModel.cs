using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoschSpot.Data.Models.Interfaces
{
    public interface IAccountViewModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
