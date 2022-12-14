using BoschSpot.Data.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BoschSpot.App.ViewModels
{
    public class RegisterViewModel : IAccountViewModel
    {
        [Required(ErrorMessage = "Dit veld is verplicht")]
        [Display(Name = "Gebruikersnaam")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Dit veld is verplicht")]
        [DataType(DataType.Password)]
        [Display(Name = "Wachtwoord")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Dit veld is verplicht")]
        [Compare("Password", ErrorMessage ="Wachtwoord niet hetzelde")]
        [DataType(DataType.Password)]
        [Display(Name = "Herhaling wachtwoord")]
        public string ConfirmPassword { get; set; }
    }
}
