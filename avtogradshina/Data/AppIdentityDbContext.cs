using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using avtogradshina.Models.Admin;
using Microsoft.AspNetCore.Identity;
using avtogradshina.Models;

namespace avtogradshina.Models
{
    public class AppIdentityDbContext : IdentityDbContext<User>
    {
        public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options)
        : base(options) { Database.EnsureCreated(); }
    }
}