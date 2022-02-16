using System;
using System.Collections.Generic;
using FundamentosEFCore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FundamentosEFCore.Data.Mappings;

public class PostMap : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder.ToTable("Posts");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();

        builder.Property(p => p.LastUpdateDate)
                .IsRequired()
                .HasColumnName("LastUpdateDate")
                .HasColumnType("SMALLDATETIME")
                // .HasDefaultValueSql("GETDATE()")
                .HasDefaultValue(DateTime.Now.ToUniversalTime())
                .ValueGeneratedOnAddOrUpdate();

        builder.HasIndex(u => u.Slug, "IX_Post_Slug")
                .IsUnique();

        builder.HasOne(p => p.Author)
                .WithMany(a => a.Posts)
                .HasConstraintName("FK_Post_Author")
                .OnDelete(DeleteBehavior.ClientCascade);

        builder.HasMany(p => p.Tags)
                .WithMany(t => t.Posts)
                .UsingEntity<Dictionary<string, object>>(
                        "PostTag",
                        j => j
                            .HasOne<Tag>()
                            .WithMany()
                            .HasForeignKey("TagId")
                            .HasConstraintName("FK_PostTag_TagId")
                            .OnDelete(DeleteBehavior.Cascade),
                        j => j
                            .HasOne<Post>()
                            .WithMany()
                            .HasForeignKey("PostId")
                            .HasConstraintName("FK_PostTag_PostId")
                            .OnDelete(DeleteBehavior.ClientCascade));
    }
}