﻿using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Repository.Converters;

namespace Repository.Data;

public class HarmonyDataContext: DbContext
{
    public HarmonyDataContext()
    {
    }

    public HarmonyDataContext(DbContextOptions<HarmonyDataContext> options): base(options)
    {
    }
    
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Availability> Availabilities { get; set; }
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Option> Options { get; set; }
    public DbSet<Package> Packages { get; set; }
    public DbSet<Qualification> Qualifications { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<Quiz> Quizzes { get; set; }
    public DbSet<QuizQuestion> QuizQuestions { get; set; }
    public DbSet<Report> Reports { get; set; }
    public DbSet<Result> Results { get; set; }
    public DbSet<Appointment> Appointments { get; set; }
    public DbSet<Specialty> Specialties { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var currentTime = DateTime.UtcNow;

        foreach (var entry in ChangeTracker.Entries<BaseEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedAt = currentTime;
                    entry.Entity.UpdatedAt = currentTime;
                    break;

                case EntityState.Modified:
                    entry.Entity.UpdatedAt = currentTime;

                    entry.Property(e => e.CreatedAt).IsModified = false;
                    break;
            }
        }

        return await base.SaveChangesAsync(cancellationToken);
    } 
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")!; 
 
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json", true, true)
            .AddJsonFile($"appsettings.{environment}.json", true, true)
            .Build();

        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseMySQL(configuration.GetConnectionString("HarmonyDb"));
        }
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties<DateOnly>()
            .HaveConversion<DateOnlyConverter>()
            .HaveColumnType("DATE");

        configurationBuilder.Properties<TimeOnly>()
            .HaveConversion<TimeOnlyConverter>()
            .HaveColumnType("TIME");
        
        base.ConfigureConventions(configurationBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (var entity in modelBuilder.Model.GetEntityTypes())
        {
            entity.SetTableName(entity.GetTableName()?.ToLowerInvariant());
        }

        new AppointmentConfiguration().Configure(modelBuilder.Entity<Appointment>());
        new AvailabilityConfiguration().Configure(modelBuilder.Entity<Availability>());
        new BlogConfiguration().Configure(modelBuilder.Entity<Blog>());
        new OptionConfiguration().Configure(modelBuilder.Entity<Option>());
        new PackageConfiguration().Configure(modelBuilder.Entity<Package>());
        new QualificationConfiguration().Configure(modelBuilder.Entity<Qualification>());
        new QuizConfiguration().Configure(modelBuilder.Entity<Quiz>());
        new QuizQuestionConfiguration().Configure(modelBuilder.Entity<QuizQuestion>());
        new ReportConfiguration().Configure(modelBuilder.Entity<Report>());
        new ResultConfiguration().Configure(modelBuilder.Entity<Result>());
        new TransactionConfiguration().Configure(modelBuilder.Entity<Transaction>());
        
        base.OnModelCreating(modelBuilder);
    }
}