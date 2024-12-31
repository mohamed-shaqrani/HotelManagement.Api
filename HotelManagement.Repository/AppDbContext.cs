using HotelManagement.Core.Entities.RoomManagement;
using HotelManagement.Core.Entities.UserManagement;
using HotelManagement.Core.Enums;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement.Repository;

public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<RoomType> RoomTypes { get; set; }
    public DbSet<RoomImage> RoomImages { get; set; }


    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>(entity =>
        {
            entity.UseTptMappingStrategy();

            entity.Property(e => e.Role)
            .HasConversion(r => r.ToString(), r => (Role)Enum.Parse(typeof(Role), r));

            entity.HasIndex(e => e.Username).IsUnique();
            entity.HasIndex(e => e.Email).IsUnique();
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasIndex(e => e.Phone).IsUnique();
        });
    }
}
