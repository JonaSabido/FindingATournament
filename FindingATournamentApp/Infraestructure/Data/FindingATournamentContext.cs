using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using FindingATournamentApp.Domain.Entities;

#nullable disable

namespace FindingATournamentApp.Infraestructure.Data
{
    public partial class FindingATournamentContext : DbContext
    {
        public FindingATournamentContext()
        {
        }

        public FindingATournamentContext(DbContextOptions<FindingATournamentContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Clube> Clubes { get; set; }
        public virtual DbSet<Discipline> Disciplines { get; set; }
        public virtual DbSet<ServiceClub> ServiceClubs { get; set; }
        public virtual DbSet<Tournament> Tournaments { get; set; }


        /*
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=JONA;Database=FindingATournament;User Id=sa;Password=.l.");
            }
        } */

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Modern_Spanish_CI_AS");

            modelBuilder.Entity<Clube>(entity =>
            {
                entity.Property(e => e.ClubAddress)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasColumnName("Club_Address");

                entity.Property(e => e.ClubContactNumber)
                    .IsRequired()
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .HasColumnName("Club_ContactNumber");

                entity.Property(e => e.ClubLatitude).HasColumnName("Club_Latitude");

                entity.Property(e => e.ClubLength).HasColumnName("Club_Length");

                entity.Property(e => e.ClubName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Club_Name");

                entity.Property(e => e.ClubSchedule)
                    .IsRequired()
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("Club_Schedule");
            });

            modelBuilder.Entity<Discipline>(entity =>
            {
                entity.Property(e => e.DiscName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Disc_Name");
            });

            modelBuilder.Entity<ServiceClub>(entity =>
            {
                entity.ToTable("Service_Club");

                entity.Property(e => e.DiferentCapacity).HasColumnName("Diferent_Capacity");

                entity.Property(e => e.IdDiscServices).HasColumnName("Id_Disc_Services");

                entity.Property(e => e.NumPeople).HasColumnName("Num_People");

                entity.Property(e => e.ServiceSchedule)
                    .IsRequired()
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("Service_Schedule");

                entity.Property(e => e.ServicesName)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("Services_Name");

                entity.Property(e => e.SpeciEqReq).HasColumnName("Speci_Eq_Req");

                entity.Property(e => e.SpeciEquip)
                    .IsUnicode(false)
                    .HasColumnName("Speci_Equip");

                entity.HasOne(d => d.IdDiscServicesNavigation)
                    .WithMany(p => p.ServiceClubs)
                    .HasForeignKey(d => d.IdDiscServices)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ID_DISCIPLINES");
            });

            modelBuilder.Entity<Tournament>(entity =>
            {
                entity.Property(e => e.AvailableTeams).HasColumnName("Available_Teams");

                entity.Property(e => e.IdDiscTournament).HasColumnName("Id_Disc_Tournament");

                entity.Property(e => e.NumTeams).HasColumnName("Num_Teams");

                entity.Property(e => e.NumberRounds).HasColumnName("Number_Rounds");

                entity.Property(e => e.RegisCost)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Regis_Cost");

                entity.Property(e => e.TournamentName)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("Tournament_Name");

                entity.Property(e => e.TournamentRules)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasColumnName("Tournament_Rules");

                entity.Property(e => e.TournamentType)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Tournament_Type");

                entity.HasOne(d => d.IdDiscTournamentNavigation)
                    .WithMany(p => p.Tournaments)
                    .HasForeignKey(d => d.IdDiscTournament)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ID_DISCIPLINES_TOURNA");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
