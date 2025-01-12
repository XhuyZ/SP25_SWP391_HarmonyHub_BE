using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Data;

public class BlogConfiguration: IEntityTypeConfiguration<Blog>
{
    public void Configure(EntityTypeBuilder<Blog> builder)
    {
        builder
            .HasOne(x => x.Therapist)
            .WithMany(x => x.Blogs)
            .HasForeignKey(x => x.TherapistId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}