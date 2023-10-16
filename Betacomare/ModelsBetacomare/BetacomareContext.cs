using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Betacomare.ModelsBetacomare;

public partial class BetacomareContext : DbContext
{
    public BetacomareContext()
    {
    }

    public BetacomareContext(DbContextOptions<BetacomareContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<ShoppingCart> ShoppingCarts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=Betacomare;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Address>(entity =>
        {
            entity.ToTable("Address");

            entity.Property(e => e.AddressId).HasColumnName("AddressID");
            entity.Property(e => e.Address1)
                .HasMaxLength(50)
                .HasColumnName("Address");
            entity.Property(e => e.City).HasMaxLength(50);
            entity.Property(e => e.CountryRegion).HasMaxLength(50);
            entity.Property(e => e.PostalCode).HasMaxLength(15);
            entity.Property(e => e.State).HasMaxLength(50);
            entity.Property(e => e.StateProvince).HasMaxLength(50);
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .HasColumnName("username");

            entity.HasOne(d => d.UsernameNavigation).WithMany(p => p.Addresses)
                .HasForeignKey(d => d.Username)
                .HasConstraintName("FK_Address_Customer");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Username);

            entity.ToTable("Customer");

            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .HasColumnName("username");
            entity.Property(e => e.BirthDay).HasColumnType("date");
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.MiddleName).HasMaxLength(50);
            entity.Property(e => e.Phone).HasMaxLength(25);
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.Title).HasMaxLength(8);
        });

        modelBuilder.Entity<ShoppingCart>(entity =>
        {
            entity.HasKey(e => new { e.OrderId, e.ProductId });

            entity.ToTable("ShoppingCart");

            entity.Property(e => e.OrderId)
                .ValueGeneratedOnAdd()
                .HasColumnName("OrderID");
            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.AddressId).HasColumnName("AddressID");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .HasColumnName("username");

            entity.HasOne(d => d.Address).WithMany(p => p.ShoppingCarts)
                .HasForeignKey(d => d.AddressId)
                .HasConstraintName("FK_ShoppingCart_Address");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
