using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using POD_3;
using POD_3.DAL.Entity.SubscriptionManagementMod;

namespace POD_3.Context
{
    public partial class DefaultContext : DbContext
    {
        public DefaultContext()
        {
        }

        public DbSet<SubscriptionCancellation> SubscriptionCancellations { get; set; } = null!;
        public DbSet<SubscriptionPlan> SubscriptionPlans { get; set; } = null!;
        public DbSet<UserSubscription> UserSubscriptions { get; set; }=null!;


        public DefaultContext(DbContextOptions<DefaultContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=Default");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            OnModelCreatingPartial(modelBuilder);

            SeedSubscriptionPlans(modelBuilder);

            modelBuilder.Entity<UserSubscription>()
            .HasOne(u => u.SubscriptionCancellation).WithOne(c => c.UserSubscription)
            .HasForeignKey<SubscriptionCancellation>(u => u.SubscriptionId);

            modelBuilder.Entity<SubscriptionPlan>()
            .HasMany(s => s.UserSubscriptions)
            .WithOne(u => u.SubscriptionPlan)
            .HasForeignKey(u => u.PlanId);

            modelBuilder.Entity<UserSubscription>()
           .HasCheckConstraint("chk_future_dates", "SubscriptionStartDate > GETDATE() AND SubscriptionEndDate > GETDATE()")
           .HasCheckConstraint("chk_payment_modes", "PaymentMode IN ('Card', 'NetBanking')")
           .HasCheckConstraint("chk_subscription_status", "SubscriptionStatus IN ('New', 'Renewed', 'Cancelled')");


        }

        private static void SeedSubscriptionPlans(ModelBuilder modelBuilder)
        {
            List<SubscriptionPlan> subscriptionPlans = new List<SubscriptionPlan>()
            {
                new SubscriptionPlan() { PlanId = 1, Name = "basic", PricePerMonth = 10 },
                new SubscriptionPlan() { PlanId = 2, Name = "pro", PricePerMonth = 50 }
            };

            modelBuilder.Entity<SubscriptionPlan>().HasData(subscriptionPlans); 
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);


    }
}
