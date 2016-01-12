using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;

namespace MovieAngularJsApp.Models
{
    public class ApplicationUser : IdentityUser { }

    public class MoviesAppContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Movie> Movies { get; set; }
    }
}
