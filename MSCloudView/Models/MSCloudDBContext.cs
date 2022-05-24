using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MSCloudView.Models
{
    public partial class MSCloudDBContext : DbContext
    {
        public MSCloudDBContext()
        {
        }

        public MSCloudDBContext(DbContextOptions<MSCloudDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Athlete> Athletes { get; set; } = null!;
        public virtual DbSet<Country> Countries { get; set; } = null!;
        public virtual DbSet<Result> Results { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=MSCloudDB;Integrated Security=True;Encrypt=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Athlete>(entity =>
            {
                entity.HasKey(e => e.IdAthlete)
                    .HasName("PK__ATHLETE__ED60EB0252667994");

                entity.ToTable("ATHLETE");

                entity.Property(e => e.AthleteName)
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdCountryNavigation)
                    .WithMany(p => p.Athletes)
                    .HasForeignKey(d => d.IdCountry)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ATHLETE__IdCount__286302EC");
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.HasKey(e => e.IdCountry)
                    .HasName("PK__COUNTRY__F99F104D78A631FD");

                entity.ToTable("COUNTRY");

                entity.Property(e => e.CountryCode)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.CountryName)
                    .HasMaxLength(60)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Result>(entity =>
            {
                entity.HasKey(e => e.IdResult)
                    .HasName("PK__RESULTS__C6DFC446570C60CE");

                entity.ToTable("RESULTS");

                entity.Property(e => e.ArranqueKg).HasColumnName("ArranqueKG");

                entity.Property(e => e.EnvionKg).HasColumnName("EnvionKG");

                entity.Property(e => e.TotalPesoKg).HasColumnName("TotalPesoKG");

                entity.HasOne(d => d.IdAthleteNavigation)
                    .WithMany(p => p.Results)
                    .HasForeignKey(d => d.IdAthlete)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__RESULTS__IdAthle__29572725");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
