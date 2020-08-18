using Api.Entities.Configuration;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace Entities
{
    public class RepositoryContext : IdentityDbContext<IdentityUser<Guid>, IdentityRole<Guid>, Guid, IdentityUserClaim<Guid>, IdentityUserRole<Guid>, IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
    {
        public RepositoryContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new PersonConfiguration());
            modelBuilder.ApplyConfiguration(new AddressConfiguration());

            modelBuilder.Entity<IdentityUser<Guid>>().ToTable("IdentityUsers");
            modelBuilder.Entity<IdentityRole<Guid>>().ToTable("IdentityRoles");
            modelBuilder.Entity<IdentityRoleClaim<Guid>>().ToTable("IdentityRoleClaims");
            modelBuilder.Entity<IdentityUserClaim<Guid>>().ToTable("IdentityUserClaims");
            modelBuilder.Entity<IdentityUserLogin<Guid>>().ToTable("IdentityUserLogins");
            modelBuilder.Entity<IdentityUserRole<Guid>>().ToTable("IdentityUserRoles");
            modelBuilder.Entity<IdentityUserToken<Guid>>().ToTable("IdentityUserTokens");
        }

        public DbSet<Person> Persons { get; set; }
        public DbSet<Address> Addresses { get; set; }
    }
}
