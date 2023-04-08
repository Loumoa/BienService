using BienLocatif_LouMoa.Models;
using Microsoft.EntityFrameworkCore;

namespace BienLocatif_LouMoa.Services;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<BienLocatif> BienLocatifs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BienLocatif>(entity =>
        {
            entity.HasKey(e => e.IdBien);
            entity.Property(e => e.Adresse).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Surface).IsRequired().HasColumnType("numeric(15,1)");
            entity.Property(e => e.NbChambres).IsRequired();
            entity.Property(e => e.NbLits).IsRequired();
            entity.Property(e => e.NbSallesDeBain).IsRequired();
            entity.Property(e => e.IdProprietaire).IsRequired();
        });

        base.OnModelCreating(modelBuilder);
    }
}