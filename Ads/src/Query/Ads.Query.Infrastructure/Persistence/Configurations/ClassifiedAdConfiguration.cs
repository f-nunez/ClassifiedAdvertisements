using Ads.Query.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ads.Command.Infrastructure.Persistence.Configurations;

public class ClassifiedAdConfiguration : IEntityTypeConfiguration<ClassifiedAd>
{
    public void Configure(EntityTypeBuilder<ClassifiedAd> builder)
    {
        builder.Property(c => c.CreatedBy)
            .HasMaxLength(450);

        builder.Property(c => c.Description)
            .HasMaxLength(5000)
            .IsRequired();

        builder.Property(c => c.PublishedBy)
            .HasMaxLength(450);

        builder.Property(c => c.Title)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(c => c.UpdatedBy)
            .HasMaxLength(450);
    }
}