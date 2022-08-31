using BoschSpot.Data.Models;
using BoschSpot.Data.Models.Interfaces;
using BoschSpot.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace BoschSpot.Data.Repositories
{
    public class AccountRepo : IAccountRepo
    {
        private readonly AppDbContext _db;

        public AccountRepo(AppDbContext db)
        {
            _db = db;
        }

        public AccountModel CheckAuthentication(IAccountViewModel model)
        {
            var User = _db.Account.Where(u => u.Username == model.Username).First();
            if (CheckPassword(model.Password, User.Password)) 
            {
                return User;
            }
            return null;
        }

        public void CreateAccount(IAccountViewModel model)
        {
            var newAccount = new AccountModel
            {
                ID = Guid.NewGuid().ToString().Substring(0, Guid.NewGuid().ToString().IndexOf("-")),
                Username = model.Username,
                Password = HashPassword(model.Password)
            };
            _db.Account.Add(newAccount);
            _db.SaveChanges();
        }

        private static string HashPassword(string password) 
        {
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);
            var pdkdf2 = new Rfc2898DeriveBytes(password, salt, 100000);
            byte[] hash = pdkdf2.GetBytes(20);
            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);
            string savedPasswordHash = Convert.ToBase64String(hashBytes);
            return savedPasswordHash;
        }
        private static bool CheckPassword(string givenPassword, string storedPassword) 
        {
            byte[] hashBytes = Convert.FromBase64String(storedPassword);
            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);
            var pbkdf2 = new Rfc2898DeriveBytes(givenPassword, salt, 100000);
            byte[] hash = pbkdf2.GetBytes(20);
            for (int i = 0; i < 20; i++)
            {
                if (hashBytes[i + 16] != hash[i]) 
                {
                    return false;
                }
            }
            return true;
        }
    }
}
