using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Betacomare.ModelsServices;

public partial class AdventureWorksLt2019servicesContext : DbContext
{
    public AdventureWorksLt2019servicesContext(DbContextOptions<AdventureWorksLt2019servicesContext> options)
        : base(options) { }

    public virtual DbSet<Utenti> Utentis { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=AdventureWorksLT2019Services;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Utenti>(entity =>
        {
            entity.HasKey(e => e.Username);

            entity.ToTable("Utenti");

            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .HasColumnName("username");
            entity.Property(e => e.CustomerId)
                .ValueGeneratedOnAdd()
                .HasColumnName("CustomerID");
            entity.Property(e => e.PswHash).HasMaxLength(172);
            entity.Property(e => e.Salt).HasMaxLength(44);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
