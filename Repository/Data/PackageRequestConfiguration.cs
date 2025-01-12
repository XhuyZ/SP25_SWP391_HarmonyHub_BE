using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Data;

public class PackageRequestConfiguration : IEntityTypeConfiguration<PackageRequest>
{
    public void Configure(EntityTypeBuilder<PackageRequest> builder)
    {
        builder
            .HasKey(x => new { x.RequestId, x.PackageId });

        builder
            .HasOne(x => x.Request)
            .WithMany(x => x.PackageRequests)
            .HasForeignKey(x => x.RequestId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(x => x.Package)
            .WithMany(x => x.PackageRequests)
            .HasForeignKey(x => x.PackageId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}