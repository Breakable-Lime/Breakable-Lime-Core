using BreakableLime.Authentication.models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace BreakableLime.Repository {
  public class ApplicationDbContext : IdentityDbContext<ApplicationIdentityUser>
    {

    }
}
