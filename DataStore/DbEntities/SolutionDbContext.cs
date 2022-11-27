using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DataStore.DbEntities;

public partial class SolutionDbContext : DbContext
{
    public SolutionDbContext()
    {
    }

    public SolutionDbContext(DbContextOptions<SolutionDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        //        => optionsBuilder.UseSqlServer("Server=LAPTOP-RL1NE58R\\SQLEXPRESS; Initial Catalog=SolutionDB;Integrated Security=True; Encrypt=False");

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");

            entity.Property(e => e.Active).HasDefaultValueSql("((1))");
            entity.Property(e => e.City)
                .HasMaxLength(80)
                .IsUnicode(false);
            entity.Property(e => e.Country)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CreatedOnUtc)
                .HasDefaultValueSql("(getutcdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.MiddleName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PasswordHasg).HasMaxLength(128);
            entity.Property(e => e.PasswordSalt).HasMaxLength(128);
            entity.Property(e => e.PostCode).HasMaxLength(10);
            entity.Property(e => e.State)
                .HasMaxLength(40)
                .IsUnicode(false);
            entity.Property(e => e.Street)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
