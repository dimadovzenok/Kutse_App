using System;
using System.ComponentModel.DataAnnotations;
namespace Kutse_App.Models
{
    public class Holidays
    {
        public int Id { get; set; }
        
        [Display(Name="Pidu nimi")]
        [Required(ErrorMessage = "Sisesta pidu nimi")]
        public string HoliName { get; set; }
        [Display(Name="Pidu kuupäev")]
        [Required(ErrorMessage = "Sisesta pidu kuupäev")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime HolidayDate { get; set; }

    }
}