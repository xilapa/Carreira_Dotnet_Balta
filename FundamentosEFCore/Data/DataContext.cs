using System;
using FundamentosEFCore.Models;
using Microsoft.EntityFrameworkCore;

namespace FundamentosEFCore.Data;

public class DataContext : DbContext
{

    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
        
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        optionsBuilder.LogTo(Console.WriteLine);

        if (!optionsBuilder.IsConfigured)
            optionsBuilder.UseInMemoryDatabase("TestDb");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<PostTagCount>(x =>
        {
            x.ToSqlQuery(@" SELECT Title, 
                            SELECT COUNT (Id) FROM Tags WHERE PostId = Id AS TagCount
                            FROM Posts ");
        });
    }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Post> Posts { get; set; }
    // public DbSet<PostTag> PostTags { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<User> Users { get; set; }
    // public DbSet<UserRole> UserRoles { get; set; }


}