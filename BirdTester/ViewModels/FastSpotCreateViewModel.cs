using BoschSpot.App.Utilities.Validation;
using BoschSpot.Data.Models;
using BoschSpot.Data.Models.Interfaces;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BoschSpot.App.ViewModels
{
    public class FastSpotCreateViewModel : ISpotCreateViewModel
    {
        [Display(Name = "Spot Categorie")]
        public CategoryModel Category { get; set; }
        [Display(Name = "Spot Soort")]
        public ContenderModel Contender { get; set; }
        public int ContenderID { get; set; }
        [Required(ErrorMessage = "Dit veld is verplicht")]
        [Display(Name = "Foto")]
        [DataType(DataType.Upload)]
        [AllowedExtensions(new string[] { ".jpg", ".png" })]
        [JsonIgnore]
        public IFormFile Photo { get; set; }
        [Required(ErrorMessage = "Dit veld is verplicht")]
        [Display(Name = "Locatie")]
        public string Address { get; set; }
    }
}
