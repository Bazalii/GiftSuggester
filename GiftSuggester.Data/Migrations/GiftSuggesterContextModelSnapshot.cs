﻿// <auto-generated />
using System;
using GiftSuggester.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace GiftSuggester.Data.Migrations
{
    [DbContext(typeof(GiftSuggesterContext))]
    partial class GiftSuggesterContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("GiftSuggester.Data.Gifts.Models.GiftDbModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid>("CreatorId")
                        .HasColumnType("uuid")
                        .HasColumnName("creator_id");

                    b.Property<Guid>("GroupId")
                        .HasColumnType("uuid")
                        .HasColumnName("group_id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<Guid>("PresenterId")
                        .HasColumnType("uuid")
                        .HasColumnName("presenter_id");

                    b.Property<Guid>("RecipientId")
                        .HasColumnType("uuid")
                        .HasColumnName("recipient_id");

                    b.HasKey("Id")
                        .HasName("pk_gifts");

                    b.ToTable("gifts", (string)null);
                });

            modelBuilder.Entity("GiftSuggester.Data.Groups.Models.GroupDbModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<Guid>("OwnerId")
                        .HasColumnType("uuid")
                        .HasColumnName("owner_id");

                    b.HasKey("Id")
                        .HasName("pk_groups");

                    b.HasIndex("OwnerId")
                        .HasDatabaseName("ix_groups_owner_id");

                    b.ToTable("groups", (string)null);
                });

            modelBuilder.Entity("GiftSuggester.Data.Users.Models.UserDbModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateOnly>("DateOfBirth")
                        .HasColumnType("date")
                        .HasColumnName("date_of_birth");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("email");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("login");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("password");

                    b.HasKey("Id")
                        .HasName("pk_users");

                    b.HasIndex("Email")
                        .IsUnique()
                        .HasDatabaseName("ix_users_email");

                    b.HasIndex("Login")
                        .IsUnique()
                        .HasDatabaseName("ix_users_login");

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("groups_admins", b =>
                {
                    b.Property<Guid>("AdminsId")
                        .HasColumnType("uuid")
                        .HasColumnName("admins_id");

                    b.Property<Guid>("GroupDbModelId")
                        .HasColumnType("uuid")
                        .HasColumnName("group_db_model_id");

                    b.HasKey("AdminsId", "GroupDbModelId")
                        .HasName("pk_groups_admins");

                    b.HasIndex("GroupDbModelId")
                        .HasDatabaseName("ix_groups_admins_group_db_model_id");

                    b.ToTable("groups_admins", (string)null);
                });

            modelBuilder.Entity("groups_members", b =>
                {
                    b.Property<Guid>("GroupDbModel1Id")
                        .HasColumnType("uuid")
                        .HasColumnName("group_db_model1id");

                    b.Property<Guid>("MembersId")
                        .HasColumnType("uuid")
                        .HasColumnName("members_id");

                    b.HasKey("GroupDbModel1Id", "MembersId")
                        .HasName("pk_groups_members");

                    b.HasIndex("MembersId")
                        .HasDatabaseName("ix_groups_members_members_id");

                    b.ToTable("groups_members", (string)null);
                });

            modelBuilder.Entity("GiftSuggester.Data.Groups.Models.GroupDbModel", b =>
                {
                    b.HasOne("GiftSuggester.Data.Users.Models.UserDbModel", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_groups_users_owner_id");

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("groups_admins", b =>
                {
                    b.HasOne("GiftSuggester.Data.Users.Models.UserDbModel", null)
                        .WithMany()
                        .HasForeignKey("AdminsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_groups_admins_users_admins_id");

                    b.HasOne("GiftSuggester.Data.Groups.Models.GroupDbModel", null)
                        .WithMany()
                        .HasForeignKey("GroupDbModelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_groups_admins_groups_group_db_model_id");
                });

            modelBuilder.Entity("groups_members", b =>
                {
                    b.HasOne("GiftSuggester.Data.Groups.Models.GroupDbModel", null)
                        .WithMany()
                        .HasForeignKey("GroupDbModel1Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_groups_members_groups_group_db_model1id");

                    b.HasOne("GiftSuggester.Data.Users.Models.UserDbModel", null)
                        .WithMany()
                        .HasForeignKey("MembersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_groups_members_users_members_id");
                });
#pragma warning restore 612, 618
        }
    }
}
