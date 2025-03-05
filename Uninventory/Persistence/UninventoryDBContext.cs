using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;
using Uninventory.Persistence.Models;

namespace Uninventory.Persistence;

public partial class UninventoryDBContext : DbContext
{
    public UninventoryDBContext()
    {
    }

    public UninventoryDBContext(DbContextOptions<UninventoryDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Equipment> Equipment { get; set; }

    public virtual DbSet<User> User { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_general_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Equipment>(entity =>
        {
            entity.HasKey(e => e.EquipmentId).HasName("PRIMARY");

            entity.ToTable("equipment");

            entity.Property(e => e.EquipmentId)
                .HasColumnType("int(11)")
                .HasColumnName("equipmentId");
            entity.Property(e => e.CategoryId).HasColumnType("int(11)");
            entity.Property(e => e.CreatedAt)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("timestamp");
            entity.Property(e => e.Location).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.PurchaseDate).HasColumnType("datetime");
            entity.Property(e => e.SerialNumber)
                .HasMaxLength(50)
                .HasColumnName("serialNumber");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");
            entity.Property(e => e.WarrantyDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PRIMARY");

            entity.ToTable("user");

            entity.Property(e => e.UserId)
                .HasColumnType("int(11)")
                .HasColumnName("userId");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("timestamp")
                .HasColumnName("createdAt");
            entity.Property(e => e.Delete)
                .HasDefaultValueSql("'0'")
                .HasColumnName("delete");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("email");
            entity.Property(e => e.FullName)
                .HasMaxLength(50)
                .HasColumnName("fullName");
            entity.Property(e => e.UserPassword)
                .HasMaxLength(50)
                .HasColumnName("userPassword");
            entity.Property(e => e.UserRole)
                .HasMaxLength(50)
                .HasColumnName("userRole");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
