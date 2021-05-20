using System.Data.Entity;
namespace Kutse_App.Models
{
    public class GuestContext: DbContext
    {
        public DbSet<Guest> Guests { get; set; }
    }
}