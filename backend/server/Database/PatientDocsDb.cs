using Microsoft.EntityFrameworkCore;
using PatientDocs.Models;

namespace PatientDocs.Data
{
    /// <summary>
    /// Database context for PatientDocs system.
    /// Manages all database operations and entity relationships.
    /// </summary>
    public class HealthcareDbContext : DbContext
    {
        // Entity sets - these become database tables
        public DbSet<User> Users { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Document> Documents { get; set; }

        /// <summary>
        /// Constructor - configures the database connection
        /// </summary>
        public HealthcareDbContext(DbContextOptions<HealthcareDbContext> options) : base(options)
        {
        }

        /// <summary>
        /// Configures entity relationships and database schema
        /// </summary>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure User entity
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(255);
                entity.HasIndex(e => e.Email).IsUnique(); // Email must be unique
            });

            // Configure Patient entity
            modelBuilder.Entity<Patient>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.MRN).IsRequired().HasMaxLength(50);
                entity.HasIndex(e => e.MRN).IsUnique(); // MRN must be unique
            });

            // Configure Note entity with relationships
            modelBuilder.Entity<Note>(entity =>
            {
                entity.HasKey(e => e.Id);
                
                // Note belongs to one Patient
                entity.HasOne<User>()
                    .WithMany()
                    .HasForeignKey(e => e.AuthorUserId)
                    .OnDelete(DeleteBehavior.Restrict); // Don't delete notes if user is deleted
                
                // Note belongs to one Patient
                entity.HasOne<Patient>()
                    .WithMany()
                    .HasForeignKey(e => e.PatientId)
                    .OnDelete(DeleteBehavior.Cascade); // Delete notes if patient is deleted
            });

            // Configure Document entity with relationships
            modelBuilder.Entity<Document>(entity =>
            {
                entity.HasKey(e => e.Id);
                
                // Document belongs to one Patient
                entity.HasOne<Patient>()
                    .WithMany()
                    .HasForeignKey(e => e.PatientId)
                    .OnDelete(DeleteBehavior.Cascade); // Delete documents if patient is deleted
                
                // Document uploaded by one User
                entity.HasOne<User>()
                    .WithMany()
                    .HasForeignKey(e => e.UploadedByUserId)
                    .OnDelete(DeleteBehavior.Restrict); // Don't delete documents if user is deleted
            });
        }
    }
}
