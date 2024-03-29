﻿using Explorer.Payments.Core.Domain;
using Explorer.Payments.Core.Domain.Converters;
using Explorer.Payments.Core.Domain.ShoppingSession;
using Microsoft.EntityFrameworkCore;

namespace Explorer.Payments.Infrastructure.Database;

public class PaymentsContext : DbContext
{
    public DbSet<TourPurchaseToken> PurchaseTokens { get; set; }
    public DbSet<ShoppingCart> ShoppingCarts { get; set; }
    public DbSet<Sale> Sales { get; set; }
    public DbSet<Coupon> Coupons { get; set; }
    public DbSet<Item> Items { get; set; }
    public DbSet<TouristWallet> TouristWallets { get; set; }
    public DbSet<PaymentRecord> PaymentRecords { get; set; }

    public PaymentsContext(DbContextOptions<PaymentsContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("payments");

        modelBuilder.Entity<ShoppingCart>()
            .Property(item => item.Items).HasColumnType("jsonb");

        modelBuilder.Entity<Item>()
            .ToTable("Items")
            .HasDiscriminator(i => i.Type)
            .HasValue<Item>(ItemType.Tour)
            .HasValue<BundleItem>(ItemType.Bundle);

        ConfigurePayments(modelBuilder);
    }

    private static void ConfigurePayments(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ShoppingCart>()
            .Property(c => c.Changes)
            .HasConversion(
                v => ShoppingCartEventsConverter.Write(v),
                v => ShoppingCartEventsConverter.Read(v)
            )
            .HasColumnType("jsonb");

        modelBuilder.Entity<Coupon>().HasIndex(c => c.Code).IsUnique();

        modelBuilder.Entity<Item>()
            .HasIndex(i => new { i.ItemId, i.Type })
            .IsUnique();

        modelBuilder.Entity<TourPurchaseToken>()
            .HasIndex(tpt => new { tpt.UserId, tpt.TourId })
            .IsUnique();
    }
}
