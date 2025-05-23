﻿
using Microsoft.EntityFrameworkCore;

namespace Assignment0.Entities
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<UserAccount> UserAccounts { get; set; }

        public DbSet<CountryTable> CountryTables { get; set; }

        public DbSet<StateTable> StateTables { get; set; }

        public DbSet<CityTable> CityTables { get; set; }

    }
}
