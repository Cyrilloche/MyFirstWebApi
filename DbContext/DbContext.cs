using Microsoft.EntityFrameworkCore;
using MyFirstWebAPI.Models;

namespace MyFirstWebApi.Context
{
    public class MyFirstWebApiDbContext : DbContext
    {
        // On dit que la table pour les User va s'appeler Users
        
        public MyFirstWebApiDbContext(DbContextOptions<MyFirstWebApiDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // L'entité User pour chaque user prendra la cé primaire user.Id
            modelBuilder.Entity<User>().HasKey(user => user.Id);
        }
        public DbSet<User> Users { get; set; }
    }
}