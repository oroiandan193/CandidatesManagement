using CandidatesManagement.Core.Entities;
using CandidatesManagement.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CandidatesManagement.Persistence.Sql.Persistence
{
    internal partial class JobCandidatesDbContext : DbContext
    {
        public JobCandidatesDbContext()
        {
        }

        public JobCandidatesDbContext(DbContextOptions options) : base(options)
        {
        }

        public virtual DbSet<JobCandidate> JobCandidates { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<JobCandidate>(ConfigureJobCandidate);
        }

        private void ConfigureJobCandidate(EntityTypeBuilder<JobCandidate> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.FirstName)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(c => c.LastName)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(c => c.PhoneNumber)
                .HasConversion(
                    v => v != null ? v.Value : null,
                    v => v != null ? new PhoneNumber(v) : null)
                .HasMaxLength(20)
                .IsRequired(false);

            builder.Property(c => c.EmailAddress)
                .HasConversion(
                    v => v.Value,
                    v => new EmailAddress(v))
                .HasMaxLength(100)
                .IsRequired();

            builder.HasIndex(c => c.EmailAddress)
                .IsUnique();

            builder.Property(c => c.LinkedInProfile)
                .HasConversion(
                    v => v != null ? v.Value : null,
                    v => v != null ? new Url(v) : null)
                .HasMaxLength(200)
                .IsRequired(false);

            builder.Property(c => c.GithubProfile)
                .HasConversion(
                    v => v != null ? v.Value : null,
                    v => v != null ? new Url(v) : null)
                .HasMaxLength(200)
                .IsRequired(false);

            builder.OwnsOne(c => c.TimeInterval, a =>
            {
                a.Property(p => p.Start)
                    .HasColumnName("IntervalStart")
                    .IsRequired(false);

                a.Property(p => p.End)
                    .HasColumnName("IntervalEnd")
                    .IsRequired(false);
            });

            builder.Property(c => c.Version)
                .IsRowVersion();
        }
    }
}
