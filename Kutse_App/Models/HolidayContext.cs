using System.Data.Entity;
using System.Data.Entity.Migrations;

namespace Kutse_App.Models
{
    public class HolidayContext: DbContext
    {
        public DbSet<Holidays> Holidayses { get; set; }
    }
}