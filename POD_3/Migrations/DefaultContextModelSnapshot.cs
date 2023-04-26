﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using POD_3.Context;

#nullable disable

namespace POD_3.Migrations
{
    [DbContext(typeof(DefaultContext))]
    partial class DefaultContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("POD_3.DAL.Entity.AccountManagementMod.SocialAccountTracker", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("AccountId")
                        .HasColumnType("int");

                    b.Property<string>("Action")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<DateTime>("Date")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2");

                    b.Property<int?>("SocialAccountTypeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.HasIndex("SocialAccountTypeId");

                    b.ToTable("SocialAccountTrackers");

                    b.HasCheckConstraint("chk_action", "Action IN ('AccountAdded', 'AccountRemoved', 'AccountPasswordChanged')");
                });

            modelBuilder.Entity("POD_3.DAL.Entity.AccountManagementMod.SocialAccountType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("AccountType")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.HasKey("Id");

                    b.ToTable("SocialAccountTypes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AccountType = "Facebook"
                        },
                        new
                        {
                            Id = 2,
                            AccountType = "Instagram"
                        },
                        new
                        {
                            Id = 3,
                            AccountType = "Twitter"
                        },
                        new
                        {
                            Id = 4,
                            AccountType = "Youtube"
                        },
                        new
                        {
                            Id = 5,
                            AccountType = "LinkedIn"
                        });
                });

            modelBuilder.Entity("POD_3.DAL.Entity.AccountManagementMod.UserSocialAccount", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("EncryptedPassword")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("LoginId")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("SocialAccountTypeId")
                        .HasColumnType("int");

                    b.Property<string>("SubscriptionName")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.HasKey("Id");

                    b.HasIndex("SocialAccountTypeId");

                    b.ToTable("UserSocialAccounts");

                    b.HasCheckConstraint("chk_subscription_name", "SubscriptionName IN ('basic','pro')");
                });

            modelBuilder.Entity("POD_3.DAL.Entity.ContentManagementModule.SubscriptionPlanLimit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("MonthlyScheduledPostLimit")
                        .HasColumnType("int");

                    b.Property<string>("PlanName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("SubscriptionPlansLimits");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            MonthlyScheduledPostLimit = 5,
                            PlanName = "basic"
                        },
                        new
                        {
                            Id = 2,
                            MonthlyScheduledPostLimit = 150,
                            PlanName = "Pro"
                        });
                });

            modelBuilder.Entity("POD_3.DAL.Entity.ContentManagementModule.UserPost", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("IsScheduledPost")
                        .HasColumnType("bit");

                    b.Property<string>("PostAttachmentURL")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("PostContentText")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PostStatus")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("PostType")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<DateTime>("PostedOn")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("PublishOnDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("PublishOnTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("SocialNetworkType")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("UserName")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.HasKey("Id");

                    b.ToTable("UserPosts");

                    b.HasCheckConstraint("chk_network_type", "SocialNetworkType IN ('Facebook','Instagram','Twitter','Youtube','LinkedIn')");

                    b.HasCheckConstraint("chk_post_status", "PostStatus IN ('Scheduled', 'Cancelled')");

                    b.HasCheckConstraint("chk_post_type", "PostType IN ('Text', 'Image', 'Video')");

                    b.HasCheckConstraint("chk_publish_date", "PublishOnDate >= GETDATE()");
                });

            modelBuilder.Entity("POD_3.DAL.Entity.SubscriptionManagementMod.SubscriptionCancellation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CancellationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("CancellationReason")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("SubscriptionId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SubscriptionId")
                        .IsUnique();

                    b.ToTable("SubscriptionCancellations");
                });

            modelBuilder.Entity("POD_3.DAL.Entity.SubscriptionManagementMod.SubscriptionPlan", b =>
                {
                    b.Property<int>("PlanId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PlanId"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("PricePerMonth")
                        .HasColumnType("int");

                    b.HasKey("PlanId");

                    b.ToTable("SubscriptionPlans");

                    b.HasData(
                        new
                        {
                            PlanId = 1,
                            Name = "basic",
                            PricePerMonth = 10
                        },
                        new
                        {
                            PlanId = 2,
                            Name = "pro",
                            PricePerMonth = 25
                        });
                });

            modelBuilder.Entity("POD_3.DAL.Entity.SubscriptionManagementMod.UserSubscription", b =>
                {
                    b.Property<int>("SubscriptionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SubscriptionId"), 1L, 1);

                    b.Property<double>("AmountPaid")
                        .HasColumnType("float");

                    b.Property<string>("PaymentMode")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("PlanId")
                        .HasColumnType("int");

                    b.Property<DateTime>("SubscriptionEndDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("SubscriptionStartDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("SubscriptionStatus")
                        .IsRequired()
                        .HasMaxLength(12)
                        .HasColumnType("nvarchar(12)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.HasKey("SubscriptionId");

                    b.HasIndex("PlanId");

                    b.HasIndex("UserId");

                    b.ToTable("UserSubscriptions");

                    b.HasCheckConstraint("chk_future_dates", "SubscriptionStartDate > GETDATE() AND SubscriptionEndDate > GETDATE()");

                    b.HasCheckConstraint("chk_payment_modes", "PaymentMode IN ('Card', 'NetBanking')");

                    b.HasCheckConstraint("chk_subscription_status", "SubscriptionStatus IN ('New', 'Renewed', 'Cancelled')");
                });

            modelBuilder.Entity("POD_3.DAL.Entity.SupportModule.SubscriptionPlanSLA", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("ExpectedSLAsInDays")
                        .HasColumnType("int");

                    b.Property<string>("PlanName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.ToTable("SubscriptionPlanSLAs");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ExpectedSLAsInDays = 7,
                            PlanName = "basic"
                        },
                        new
                        {
                            Id = 2,
                            ExpectedSLAsInDays = 1,
                            PlanName = "pro"
                        });
                });

            modelBuilder.Entity("POD_3.DAL.Entity.SupportModule.SupportTicket", b =>
                {
                    b.Property<int>("TicketId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TicketId"), 1L, 1);

                    b.Property<DateTime>("CreatedOn")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2023, 4, 26, 0, 0, 0, 0, DateTimeKind.Local));

                    b.Property<DateTime>("ExpectedResolutionOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("RaisedByUserName")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("TicketDetails")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("TicketStatus")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)")
                        .HasDefaultValue("Open");

                    b.Property<string>("TicketSummary")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("TicketType")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.HasKey("TicketId");

                    b.ToTable("SupportTickets");

                    b.HasCheckConstraint("CK_SupportTicket_ExpectedResolutionOn", "ExpectedResolutionOn > GETDATE()");

                    b.HasCheckConstraint("CK_SupportTicket_TicketStatus", "TicketStatus IN ('Open', 'Closed')");

                    b.HasCheckConstraint("CK_SupportTicket_TicketType", "TicketType IN ('Subscription', 'Billing', 'PostManagement', 'Others')");
                });

            modelBuilder.Entity("POD_3.DAL.Entity.SupportModule.TicketSolution", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ResolutionDetails")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("ResolvedByUserName")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<DateTime>("ResolvedOn")
                        .HasColumnType("datetime2");

                    b.Property<int>("SupportTicketId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SupportTicketId")
                        .IsUnique();

                    b.ToTable("TicketSolutions");
                });

            modelBuilder.Entity("POD_3.DAL.Entity.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "admin123@gmail.com",
                            Password = "240BE518FABD2724DDB6F04EEB1DA5967448D7E831C08C8FA822809F74C720A9",
                            Role = "SupportExecutive"
                        });
                });

            modelBuilder.Entity("POD_3.DAL.Entity.AccountManagementMod.SocialAccountTracker", b =>
                {
                    b.HasOne("POD_3.DAL.Entity.AccountManagementMod.UserSocialAccount", "UserSocialAccount")
                        .WithMany("SocialAccountTrackers")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("POD_3.DAL.Entity.AccountManagementMod.SocialAccountType", null)
                        .WithMany("SocialAccountTrackers")
                        .HasForeignKey("SocialAccountTypeId");

                    b.Navigation("UserSocialAccount");
                });

            modelBuilder.Entity("POD_3.DAL.Entity.AccountManagementMod.UserSocialAccount", b =>
                {
                    b.HasOne("POD_3.DAL.Entity.AccountManagementMod.SocialAccountType", "SocialAccountType")
                        .WithMany("UserSocialAccounts")
                        .HasForeignKey("SocialAccountTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SocialAccountType");
                });

            modelBuilder.Entity("POD_3.DAL.Entity.SubscriptionManagementMod.SubscriptionCancellation", b =>
                {
                    b.HasOne("POD_3.DAL.Entity.SubscriptionManagementMod.UserSubscription", "UserSubscription")
                        .WithOne("SubscriptionCancellation")
                        .HasForeignKey("POD_3.DAL.Entity.SubscriptionManagementMod.SubscriptionCancellation", "SubscriptionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserSubscription");
                });

            modelBuilder.Entity("POD_3.DAL.Entity.SubscriptionManagementMod.UserSubscription", b =>
                {
                    b.HasOne("POD_3.DAL.Entity.SubscriptionManagementMod.SubscriptionPlan", "SubscriptionPlan")
                        .WithMany("UserSubscriptions")
                        .HasForeignKey("PlanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("POD_3.DAL.Entity.User", "User")
                        .WithOne("UserSubscription")
                        .HasForeignKey("POD_3.DAL.Entity.SubscriptionManagementMod.UserSubscription", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SubscriptionPlan");

                    b.Navigation("User");
                });

            modelBuilder.Entity("POD_3.DAL.Entity.SupportModule.TicketSolution", b =>
                {
                    b.HasOne("POD_3.DAL.Entity.SupportModule.SupportTicket", "SupportTickets")
                        .WithOne("TicketSolution")
                        .HasForeignKey("POD_3.DAL.Entity.SupportModule.TicketSolution", "SupportTicketId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SupportTickets");
                });

            modelBuilder.Entity("POD_3.DAL.Entity.AccountManagementMod.SocialAccountType", b =>
                {
                    b.Navigation("SocialAccountTrackers");

                    b.Navigation("UserSocialAccounts");
                });

            modelBuilder.Entity("POD_3.DAL.Entity.AccountManagementMod.UserSocialAccount", b =>
                {
                    b.Navigation("SocialAccountTrackers");
                });

            modelBuilder.Entity("POD_3.DAL.Entity.SubscriptionManagementMod.SubscriptionPlan", b =>
                {
                    b.Navigation("UserSubscriptions");
                });

            modelBuilder.Entity("POD_3.DAL.Entity.SubscriptionManagementMod.UserSubscription", b =>
                {
                    b.Navigation("SubscriptionCancellation")
                        .IsRequired();
                });

            modelBuilder.Entity("POD_3.DAL.Entity.SupportModule.SupportTicket", b =>
                {
                    b.Navigation("TicketSolution");
                });

            modelBuilder.Entity("POD_3.DAL.Entity.User", b =>
                {
                    b.Navigation("UserSubscription");
                });
#pragma warning restore 612, 618
        }
    }
}
