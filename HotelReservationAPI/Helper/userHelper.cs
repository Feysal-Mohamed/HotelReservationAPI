using System.Collections.Generic;
using System.Reflection.Emit;
using HotelReservationAPI.MODEL;
using Microsoft.EntityFrameworkCore;

namespace HotelReservationAPI.Helper
{
    public class userHelper : DbContext
    {
        public userHelper(DbContextOptions<userHelper> options) : base(options) { }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();
        }
    }
}
