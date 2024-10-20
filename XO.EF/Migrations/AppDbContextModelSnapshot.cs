﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ReposatoryPatternWithUOW.EF;

#nullable disable

namespace XO.EF.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ReposatoryPatternWithUOW.Core.Models.EmailVerificationCode", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("Code")
                        .HasMaxLength(10)
                        .HasColumnType("varchar");

                    b.Property<DateTime>("ExpiresAt")
                        .HasColumnType("datetime2");

                    b.HasKey("UserId", "Code");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("EmailVerificationCodes");
                });

            modelBuilder.Entity("ReposatoryPatternWithUOW.Core.Models.Game", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("Player1")
                        .HasColumnType("int");

                    b.Property<int>("Player2")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Game");
                });

            modelBuilder.Entity("ReposatoryPatternWithUOW.Core.Models.IdentityTokenVerification", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("Token")
                        .HasMaxLength(500)
                        .IsUnicode(false)
                        .HasColumnType("varchar(500)");

                    b.Property<DateTime>("ExpiresAt")
                        .HasColumnType("datetime2");

                    b.HasKey("UserId", "Token");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("IdentityTokenVerifications");
                });

            modelBuilder.Entity("ReposatoryPatternWithUOW.Core.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int?>("Points")
                        .HasColumnType("int");

                    b.Property<string>("ProfilePicture")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.HasIndex("Email");

                    b.HasIndex("UserName")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ReposatoryPatternWithUOW.Core.Models.UserConnection", b =>
                {
                    b.Property<string>("ConnectionId")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<bool>("IsPlaying")
                        .HasColumnType("bit");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("ConnectionId");

                    b.HasIndex("UserId");

                    b.ToTable("UserConnections");
                });

            modelBuilder.Entity("ReposatoryPatternWithUOW.Core.Models.UserGame", b =>
                {
                    b.Property<string>("GameId")
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("GameId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("UserGame");
                });

            modelBuilder.Entity("XO.Core.Models.GamePlay", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("Move")
                        .HasColumnType("int");

                    b.Property<int?>("PlayerId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("GamePlays");
                });

            modelBuilder.Entity("ReposatoryPatternWithUOW.Core.Models.EmailVerificationCode", b =>
                {
                    b.HasOne("ReposatoryPatternWithUOW.Core.Models.User", "User")
                        .WithOne("EmailVerificationCode")
                        .HasForeignKey("ReposatoryPatternWithUOW.Core.Models.EmailVerificationCode", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("ReposatoryPatternWithUOW.Core.Models.IdentityTokenVerification", b =>
                {
                    b.HasOne("ReposatoryPatternWithUOW.Core.Models.User", "User")
                        .WithOne("IdentityTokenVerification")
                        .HasForeignKey("ReposatoryPatternWithUOW.Core.Models.IdentityTokenVerification", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("ReposatoryPatternWithUOW.Core.Models.UserConnection", b =>
                {
                    b.HasOne("ReposatoryPatternWithUOW.Core.Models.User", "User")
                        .WithMany("UserConnections")
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ReposatoryPatternWithUOW.Core.Models.UserGame", b =>
                {
                    b.HasOne("ReposatoryPatternWithUOW.Core.Models.Game", null)
                        .WithMany()
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ReposatoryPatternWithUOW.Core.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ReposatoryPatternWithUOW.Core.Models.User", b =>
                {
                    b.Navigation("EmailVerificationCode")
                        .IsRequired();

                    b.Navigation("IdentityTokenVerification")
                        .IsRequired();

                    b.Navigation("UserConnections");
                });
#pragma warning restore 612, 618
        }
    }
}
