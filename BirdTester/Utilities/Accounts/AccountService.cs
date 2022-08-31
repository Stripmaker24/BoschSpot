using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BoschSpot.Utilities.Accounts
{
    public class AccountService : IAccountService
    {
        private readonly IHttpContextAccessor _httpContext;

        public AccountService(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }
        public string GetCurrentUserID()
        {
            Claim account = _httpContext.HttpContext.User.Claims.Where(c => c.Type == ClaimTypes.Sid).First();
            var accountID = account.Value;
            return accountID;
        }
    }
}
