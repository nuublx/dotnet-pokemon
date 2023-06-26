using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_rpg.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }
        public DbSet<Character> Characters => Set<Character>();
        public DbSet<User> Users => Set<User>();
        protected override void OnModelCreating(ModelBuilder modelBuilder){
            modelBuilder.Entity<Character>().HasKey(c =>c.Id);
            modelBuilder.Entity<User>().HasKey(u => u.Id);
        }
    }
}