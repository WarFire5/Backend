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
            .HasOne(c => c.Owner)
            .WithMany(u => u.Coins);

        //modelBuilder.Entity<DeviceDto>()
        //    .Property(d => d.DeviceType)
        //    .HasConversion<string>();

        //modelBuilder.Entity<CoinDto>()
        //    .Property(c => c.CoinType)
        //    .HasConversion<string>();

        modelBuilder
           .Entity<DeviceDto>()
           .HasData(Enum
           .GetValues(typeof(DeviceType))
           .Cast<DeviceType>()
           .Select(e => new DeviceDto
           {
               Id = Guid.NewGuid(),
               DeviceName = e.ToString()
           })
           );
    }
}
