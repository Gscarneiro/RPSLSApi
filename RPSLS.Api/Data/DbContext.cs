using Microsoft.EntityFrameworkCore;
using RPSLS.Api.Data.DTO;
using System.Reflection.Metadata;

namespace RPSLS.Api.Data
{
    public class RPSLSDbContext(DbContextOptions<RPSLSDbContext> options) : DbContext(options)
    {
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Player> Players { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Room>(entity => {

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasMany(e => e.GamesList)
                .WithOne(e => e.Room)
                .HasForeignKey(e => e.RoomId)
                .HasPrincipalKey(e => e.Id);

                entity.HasOne(e => e.PlayerOne)
                .WithMany(e => e.PlayerOneRoomsList)
                .HasForeignKey(e => e.PlayerOneId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasPrincipalKey(e => e.Id);

                entity.HasOne(e => e.PlayerTwo)
                .WithMany(e => e.PlayerTwoRoomsList)
                .HasForeignKey(e => e.PlayerTwoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasPrincipalKey(e => e.Id);
            });

            modelBuilder.Entity<Game>(entity => {

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(e => e.Room)
                .WithMany(e => e.GamesList)
                .HasForeignKey(e => e.RoomId)
                .HasPrincipalKey(e => e.Id);
            });

            modelBuilder.Entity<Player>(entity => {

                entity.Property(e => e.Id).ValueGeneratedNever();

            });
        }
    }
}
