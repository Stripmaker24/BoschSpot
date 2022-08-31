using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoschSpot.Utilities.Accounts
{
    public interface IAccountService
    {
        string GetCurrentUserID();
    }
}
