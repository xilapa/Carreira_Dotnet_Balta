using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using TodoApp.Domain.Entities;

namespace Infra.Contexts;

public class TodoDataContext : DbContext, IUnitOfWork
{
    public TodoDataContext(DbContextOptions<TodoDataContext> options) : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TodoItem>()
            .ToTable("Todo")
            .HasKey(t => t.Id);

        modelBuilder.Entity<TodoItem>()
            .Property(t => t.Id).HasColumnType("uniqueidentifier");

        modelBuilder.Entity<TodoItem>()
            .Property(t => t.Title)
            .HasMaxLength(200)
            .HasColumnType("varchar(200)")
            .IsRequired();

        modelBuilder.Entity<TodoItem>()
            .Property(t => t.Done)
            .HasColumnType("bit")
            .IsRequired();

        modelBuilder.Entity<TodoItem>()
            .HasOne<User>()
            .WithMany()
            .HasForeignKey(t => t.UserId);

        modelBuilder.Entity<TodoItem>()
            .HasIndex(t => t.UserId);

        modelBuilder.Entity<User>()
            .HasKey(u => u.Id);

        modelBuilder.Entity<User>()
            .Property(u => u.GoogleId)
            .HasMaxLength(100)
            .HasColumnType("varchar(100)")
            .IsRequired();

        modelBuilder.Entity<User>()
            .Property(u => u.Name)
            .HasMaxLength(200)
            .HasColumnType("varchar(200)")
            .IsRequired();

        modelBuilder.Entity<User>()
            .Property(u => u.Active)
            .HasColumnType("bit")
            .IsRequired();
    }

    public DbSet<TodoItem> Todos { get; set; }
}