using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Data;

public class AvailabilityConfiguration: IEntityTypeConfiguration<Availability>
{
    public void Configure(EntityTypeBuilder<Availability> builder)
    {
        builder
            .HasOne(x => x.Therapist)
            .WithMany(x => x.Availabilities)
            .HasForeignKey(x => x.TherapistId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}