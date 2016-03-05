using System.ComponentModel.DataAnnotations;

namespace HQF.Tutorial.WebAPI.Indentity2.Models
{
    public class ClaimBindingModel
    {
        [Required]
        [Display(Name = "Claim Type")]
        public string Type { get; set; }

        [Required]
        [Display(Name = "Claim Value")]
        public string Value { get; set; }
    }
}