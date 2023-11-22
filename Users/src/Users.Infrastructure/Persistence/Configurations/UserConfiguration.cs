using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Users.Domain.Entities;

namespace Users.Infrastructure.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);

        builder.Property(u => u.FullName)
            .HasMaxLength(256);

        builder.Property(u => u.NormalizedFullName)
            .HasMaxLength(256);

        builder.Property(u => u.FirstName)
            .HasMaxLength(256);

        builder.Property(u => u.NormalizedFirstName)
            .HasMaxLength(256);

        builder.Property(u => u.LastName)
            .HasMaxLength(256);

        builder.Property(u => u.NormalizedLastName)
            .HasMaxLength(256);

        builder.Property(u => u.Email)
            .HasMaxLength(256);

        builder.Property(u => u.NormalizedEmail)
            .HasMaxLength(256);

        builder.Property(u => u.UserName)
            .HasMaxLength(256);

        builder.Property(u => u.NormalizedUserName)
            .HasMaxLength(256);

        builder.Property(u => u.PhoneNumber)
            .HasMaxLength(256);

        builder.Property(u => u.ProfileImageUrl)
            .HasMaxLength(256);

        builder.HasIndex(u => u.NormalizedFullName)
            .HasDatabaseName("FullNameIndex");

        builder.HasIndex(u => u.NormalizedFirstName)
            .HasDatabaseName("FirstNameIndex");

        builder.HasIndex(u => u.NormalizedLastName)
            .HasDatabaseName("LastNameIndex");

        builder.HasIndex(u => u.NormalizedEmail)
            .HasDatabaseName("EmailIndex");

        builder.HasIndex(u => u.NormalizedUserName)
            .HasDatabaseName("UserNameIndex")
            .IsUnique();

        builder.HasMany(u => u.UserRoles)
            .WithOne(ur => ur.User)
            .HasForeignKey(ur => ur.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}