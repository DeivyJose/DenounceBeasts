using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DenounceBeastsApi.Models;

public partial class DenounceBeastsDbContext : DbContext
{
    public DenounceBeastsDbContext()
    {
    }

    public DenounceBeastsDbContext(DbContextOptions<DenounceBeastsDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Municipality> Municipalities { get; set; }

    public virtual DbSet<Sector> Sectors { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost,1433;Database=DenounceBeastsDb;User Id=sa;Password=Deivy@play972;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Municipality>(entity =>
        {
            entity.HasKey(e => e.MunicipalityId).HasName("PK__Municipa__009B60D500C59F56");

            entity.Property(e => e.Name).HasMaxLength(100);

            entity.HasOne(d => d.Sector).WithMany(p => p.Municipalities)
                .HasForeignKey(d => d.SectorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Municipality_Sector");
        });

        modelBuilder.Entity<Sector>(entity =>
        {
            entity.HasKey(e => e.SectorId).HasName("PK__Sectors__755E57E94F07BDE3");

            entity.Property(e => e.Name).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
