using Entities.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrasctruture.Configuration
{
    public class ContextBase : IdentityDbContext<ApplicationUser>
    {

        public ContextBase(DbContextOptions<ContextBase> options) : base(options)
        {
        }

        public DbSet<Produto> Produtos { get; set; }
        public DbSet<CompraUsuario> CompraUsuario { get; set; }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(getStringConnectionConfig());
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ApplicationUser>().ToTable("AspNetUsers").HasKey(t => t.Id);

            base.OnModelCreating(builder);
        }

        private string getStringConnectionConfig()
        {
            string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=EcommerceDB;Trusted_Connection=true";
            return connectionString;
        }
    }
}
