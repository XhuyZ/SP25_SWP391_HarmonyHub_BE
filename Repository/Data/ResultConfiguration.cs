using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Data;

public class ResultConfiguration: IEntityTypeConfiguration<Result>
{
    public void Configure(EntityTypeBuilder<Result> builder)
    {
        builder
            .HasOne(x => x.Quiz)
            .WithMany(x => x.Results)
            .HasForeignKey(x => x.QuizId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}