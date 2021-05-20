using System.Data.Entity;
namespace Kutse_App.Models
{
    public class GuestDBInitializer: CreateDatabaseIfNotExists<GuestContext>
    {
        protected override void Seed(GuestContext db)
        {
            base.Seed(db);
        }
    }
}