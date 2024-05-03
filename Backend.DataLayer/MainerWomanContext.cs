using Backend.Core.DTOs;
using Backend.Core.Enums;
using Microsoft.EntityFrameworkCore;

namespace Backend.DataLayer;

public class MainerWomanContext : DbContext
{
    public DbSet<UserDto> Users { get; set; }
    public DbSet<DeviceDto> Devices { get; set; }
    public DbSet<CoinDto> Coins { get; set; }

    public MainerWomanContext(DbContextOptions<MainerWomanContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
              .Entity<DeviceDto>()
              .HasOne(d => d.Owner)
              .WithMany(u => u.Devices);

        modelBuilder
            .Entity<CoinDto>()
            .HasOne(c => c.Device)
            .WithMany(u => u.Coins);

        modelBuilder.HasPostgresEnum<DeviceType>();
        modelBuilder.HasPostgresEnum<CoinType>();
    }
}
