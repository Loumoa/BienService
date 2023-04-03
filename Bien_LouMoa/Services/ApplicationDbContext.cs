using Bien_LouMoa.Models;
using Microsoft.EntityFrameworkCore;

namespace Bien_LouMoa.Services;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Bien> Biens { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Bien>(entity =>
        {
            entity.HasKey(e => e.IdBien);
            entity.Property(e => e.Adresse).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Surface).IsRequired().HasColumnType("numeric(15,1)");
            entity.Property(e => e.NbChambres).IsRequired();
            entity.Property(e => e.NbLits).IsRequired();
            entity.Property(e => e.NbSallesDeBain).IsRequired();
            entity.Property(e => e.IdUtilisateur).IsRequired();
        });

        base.OnModelCreating(modelBuilder);
    }
}