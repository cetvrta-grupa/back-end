﻿using Explorer.BuildingBlocks.Core.Domain;
using Explorer.Encounters.Core.Domain.Converters;
using Explorer.Encounters.Core.Domain.Encounters;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Tours.Core.Domain.TourExecutions;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace Explorer.Encounters.Infrastructure.Database
{
    public class EncountersContext : DbContext
    {
        public DbSet<Encounter> Encounter { get; set; }
        public DbSet<EncounterRequest> EncounterRequests { get; set; }
        public DbSet<HiddenLocationEncounter> HiddenLocationEncounter { get; set; }
        public DbSet<SocialEncounter> SocialEncounter { get; set; }
        public DbSet<EncounterExecution> EncounterExecution { get; set; }


        public EncountersContext(DbContextOptions<EncountersContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("encounters");
            modelBuilder.Ignore<DomainEvent>();

            modelBuilder.Entity<Encounter>().ToTable("Encounter");
            modelBuilder.Entity<HiddenLocationEncounter>().ToTable("HiddenLocationEncounter");
            modelBuilder.Entity<SocialEncounter>().ToTable("SocialEncounter");
            modelBuilder.Entity<EncounterExecution>()
                .ToTable("EncounterExecution")
                .HasOne(e => e.Encounter)
                .WithMany()
                .HasForeignKey(e => e.EncounterId)
                .IsRequired();

            modelBuilder.Entity<EncounterExecution>()
                  .Property(t => t.Changes)
                  .HasConversion(
                      v => SocialEncounterEventConverter.Write(v),
                      v => SocialEncounterEventConverter.Read(v)
                  )
                  .HasColumnType("jsonb");

            modelBuilder.Entity<Encounter>()
                  .Property(t => t.Changes)
                  .HasConversion(
                      v => SocialEncounterEventConverter.Write(v),
                      v => SocialEncounterEventConverter.Read(v)
                  )
                  .HasColumnType("jsonb");
            // modelBuilder.Entity<Encounter>().Property(item => item.HiddenLocationEncounter).HasColumnType("jsonb");
            //modelBuilder.Entity<Encounter>().Property(item => item.SocialEncounter).HasColumnType("jsonb");
        }
    }
}
