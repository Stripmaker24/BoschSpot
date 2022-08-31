using BoschSpot.Data.Models;
using BoschSpot.Data.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoschSpot.Data.Repositories.Interfaces
{
    public interface IAccountRepo
    {
        void CreateAccount(IAccountViewModel registerViewModel);
        AccountModel CheckAuthentication(IAccountViewModel loginViewModel);
    }
}
