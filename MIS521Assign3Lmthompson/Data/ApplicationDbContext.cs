using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MIS521Assign3Lmthompson.Models;

namespace MIS521Assign3Lmthompson.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<MIS521Assign3Lmthompson.Models.Movie> Movie { get; set; }
        public DbSet<MIS521Assign3Lmthompson.Models.Actor> Actor { get; set; }
        public DbSet<MIS521Assign3Lmthompson.Models.MovieActor> MovieActor { get; set; }
    }
}