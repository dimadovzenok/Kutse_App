using System.ComponentModel.DataAnnotations;

namespace Kutse_App.Models
{
    public class Guest
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Sisesta nimi")]
        [Display(Name="Nimi")]
        public string Name { get; set; }
        
        [Display(Name="Mail")]
        [Required(ErrorMessage = "Sisesta email")]
        [RegularExpression(@".+\@.+\..+", ErrorMessage = "Valesti sisestatud email")]
        public string Email { get; set; }
        
        [Display(Name="Number")]
        [Required(ErrorMessage = "Sisesta telefonu number")]
        [RegularExpression(@"\+372.+", ErrorMessage = "Number alguses peal olema +372")]
        public string Phone { get; set; }
        
        [Display(Name="Tulemus")]
        [Required(ErrorMessage = "Sisesta oma valik")]
        public bool? WillAttend { get; set; }
    }
}