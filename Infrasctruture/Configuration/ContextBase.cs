using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrasctruture.Configuration
{
    public class ContextBase : DbContext
    {

        public ContextBase(DbContextOptions<ContextBase> options) : base(options)
        {
        }

        public DbSet<Produto> Produtos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(getStringConnectionConfig());
            base.OnConfiguring(optionsBuilder);
        }

        private string getStringConnectionConfig()
        {
            string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=EcommerceDB;Trusted_Connection=true";
            return connectionString;
        }
    }
}
