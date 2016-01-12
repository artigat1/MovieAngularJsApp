using Microsoft.Data.Entity;

namespace MovieAngularJsApp.Models
{
    public class MoviesAppContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }
    }
}
