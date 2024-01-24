using CustardCards.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CustardCards.Data
{
    public class CustardCardsDbContext : DbContext
    {
        public DbSet<Room> Rooms { get; set; }
        public DbSet<User> Users { get; set; }

        public CustardCardsDbContext(DbContextOptions<CustardCardsDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
            .HasOne(u => u.Room)
            .WithMany(r => r.Users)
            .HasForeignKey(u => u.RoomId)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Room>()
                .Property(p => p.Id)
                .HasDefaultValue(Guid.NewGuid());

            modelBuilder.Entity<User>().HasIndex(u => new { u.IsModerator, u.Name });
        }
    }
}
