using BreakableLime.Authentication.models;
using BreakableLime.Repository.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BreakableLime.Repository {
  public class ApplicationDbContext : IdentityDbContext<ApplicationIdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ContainerImage>()
                .HasOne(c => c.Owner)
                .WithMany(c => c.OwnedImages);
            
            
            base.OnModelCreating(builder);
        }

        public DbSet<ContainerImage> Images { get; set; }
        
    }
}
