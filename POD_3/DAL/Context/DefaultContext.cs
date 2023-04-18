using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using POD_3;
using POD_3.DAL.Entity.AccountManagementMod;
using POD_3.DAL.Entity.ContentManagementModule;
using POD_3.DAL.Entity.SubscriptionManagementMod;
using POD_3.DAL.Entity.SupportModule;

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

        public DbSet<SocialAccountTracker> SocialAccountTrackers { get; set; } = null!;
        public DbSet<SocialAccountType> SocialAccountTypes { get; set; } = null!;
        public DbSet<UserSocialAccount> UserSocialAccounts { get; set; } = null!;

        public DbSet<SubscriptionPlanLimit> SubscriptionPlansLimits { get; set; } = null!;
        public DbSet<UserPost> UserPosts { get; set; } = null!;

        public DbSet<SupportTicket> SupportTickets { get; set; } = null!;

        public DbSet<TicketSolution> TicketSolutions { get; set; } = null!;

        public DbSet<SubscriptionPlanSLA> SubscriptionPlanSLAs { get; set; } = null!;


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

            //Subscription Management Module
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

            //Account Management Module

            modelBuilder.Entity<UserSocialAccount>()
            .HasOne(u=>u.SocialAccountType).WithMany(s=>s.UserSocialAccounts)
            .HasForeignKey(u=>u.SocialAccountTypeId);

            modelBuilder.Entity<UserSocialAccount>()
            .HasMany(u=>u.SocialAccountTrackers).WithOne(s=>s.UserSocialAccount)
            .HasForeignKey(s=>s.AccountId);

            modelBuilder.Entity<SocialAccountTracker>()
            .HasCheckConstraint("chk_action", "Action IN ('AccountAdded', 'AccountRemoved', 'AccountPasswordChanged')");

            modelBuilder.Entity<UserSocialAccount>()
            .HasCheckConstraint("chk_subscription_name", "SubscriptionName IN ('basic','pro')");

            SeedSocialAccounts(modelBuilder);

            //Content Management Module

            modelBuilder.Entity<UserPost>()
           .HasCheckConstraint("chk_publish_date", "PublishOnDate >= GETDATE()")
           .HasCheckConstraint("chk_post_type", "PostType IN ('Text', 'Image', 'Video')")
           .HasCheckConstraint("chk_post_status", "PostStatus IN ('Scheduled', 'Cancelled')")
           .HasCheckConstraint("chk_network_type", "SocialNetworkType IN ('Facebook','Instagram','Twitter','Youtube','LinkedIn')");

            SeedSubscriptionPlanLimit(modelBuilder);

            //Support module

            modelBuilder.Entity<SupportTicket>()
            .HasOne(c => c.TicketSolution)
            .WithOne(p => p.SupportTickets)
            .HasForeignKey<TicketSolution>(p => p.Id);

            modelBuilder.Entity<SupportTicket>()
            .Property(p => p.CreatedOn)
             .HasDefaultValue(DateTime.Today);

            modelBuilder.Entity<SupportTicket>()
            .Property(p => p.TicketStatus)
            .HasDefaultValue("Open");

            modelBuilder.Entity<SupportTicket>()
            .HasCheckConstraint("CK_SupportTicket_ExpectedResolutionOn", "ExpectedResolutionOn > GETDATE()");

            modelBuilder.Entity<SupportTicket>()
            .HasCheckConstraint("CK_SupportTicket_TicketType", "TicketType IN ('Subscription', 'Billing', 'PostManagement', 'Others')");

            modelBuilder.Entity<SupportTicket>()
            .HasCheckConstraint("CK_SupportTicket_TicketStatus", "TicketStatus IN ('Open', 'Closed')");

            SeedSubscriptionPlanSLA(modelBuilder);

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

        private static void SeedSocialAccounts(ModelBuilder modelBuilder)
        {
            List<SocialAccountType> socialAccountTypes = new List<SocialAccountType>()
            {
                new SocialAccountType() { Id = 1, AccountType = "Facebook" },
                new SocialAccountType() { Id = 2, AccountType = "Instagram" },
                new SocialAccountType() { Id = 3, AccountType = "Twitter" },
                new SocialAccountType() { Id = 4, AccountType = "Youtube" },
                new SocialAccountType() { Id = 5, AccountType = "LinkedIn" }
            };

            modelBuilder.Entity<SocialAccountType>().HasData(socialAccountTypes);
        }

        private static void SeedSubscriptionPlanLimit(ModelBuilder modelBuilder)
        {
            List<SubscriptionPlanLimit> subscriptionPlanlimits = new List<SubscriptionPlanLimit>()
            {
                new SubscriptionPlanLimit() { Id = 1,PlanName = "basic", MonthlyScheduledPostLimit = 5 },
                new SubscriptionPlanLimit() { Id=2, PlanName = "Pro", MonthlyScheduledPostLimit = 150 }
            };
            modelBuilder.Entity<SubscriptionPlanLimit>().HasData(subscriptionPlanlimits);
        }
        private static void SeedSubscriptionPlanSLA(ModelBuilder modelBuilder)
        {
            List<SubscriptionPlanSLA> subscriptionPlanSLAs = new List<SubscriptionPlanSLA>()
            {
                new SubscriptionPlanSLA { Id = 1, PlanName = "basic", ExpectedSLAsInDays = 7 },
                new SubscriptionPlanSLA { Id = 2, PlanName = "pro", ExpectedSLAsInDays = 1 }
            };
            modelBuilder.Entity<SubscriptionPlanSLA>().HasData(subscriptionPlanSLAs);

        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);


    }
}
