using System.Data.Entity;
namespace Kutse_App.Models
{
    public class HolidayDBInitializer: CreateDatabaseIfNotExists<HolidayContext>
    {
        protected override void Seed(HolidayContext db)
        {
            base.Seed(db);
        }
    }
}