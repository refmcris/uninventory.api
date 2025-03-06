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

    public virtual DbSet<Categories> Categories { get; set; }

    public virtual DbSet<Equipment> Equipment { get; set; }

    public virtual DbSet<Loans> Loans { get; set; }

    public virtual DbSet<Role> Role { get; set; }

    public virtual DbSet<User> User { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_general_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Categories>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PRIMARY");

            entity.ToTable("categories");

            entity.Property(e => e.CategoryId).HasColumnType("int(11)");
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Equipment>(entity =>
        {
            entity.HasKey(e => e.EquipmentId).HasName("PRIMARY");

            entity.ToTable("equipment");

            entity.HasIndex(e => e.CategoryId, "FK_equipment_categories");

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

            entity.HasOne(d => d.Category).WithMany(p => p.Equipment)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_equipment_categories");
        });

        modelBuilder.Entity<Loans>(entity =>
        {
            entity.HasKey(e => e.LoanId).HasName("PRIMARY");

            entity.ToTable("loans");

            entity.HasIndex(e => e.EquipmentId, "FK_loans_equipment");

            entity.HasIndex(e => e.UserId, "FK_loans_user");

            entity.Property(e => e.LoanId).HasColumnType("int(11)");
            entity.Property(e => e.EndDate).HasColumnType("datetime");
            entity.Property(e => e.EquipmentId).HasColumnType("int(11)");
            entity.Property(e => e.StartDate)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("datetime");
            entity.Property(e => e.Status).HasDefaultValueSql("'0'");
            entity.Property(e => e.UserId).HasColumnType("int(11)");

            entity.HasOne(d => d.Equipment).WithMany(p => p.Loans)
                .HasForeignKey(d => d.EquipmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_loans_equipment");

            entity.HasOne(d => d.User).WithMany(p => p.Loans)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_loans_user");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PRIMARY");

            entity.ToTable("role");

            entity.Property(e => e.RoleId).HasColumnType("int(11)");
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PRIMARY");

            entity.ToTable("user");

            entity.HasIndex(e => e.UserRole, "FK_user_role");

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
                .HasColumnType("int(11)")
                .HasColumnName("userRole");

            entity.HasOne(d => d.UserRoleNavigation).WithMany(p => p.User)
                .HasForeignKey(d => d.UserRole)
                .HasConstraintName("FK_user_role");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
